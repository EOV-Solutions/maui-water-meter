using Foundation;
using ObjCRuntime;
using UIKit;
using AVFoundation;

namespace WaterMeter.Maui.Platforms.iOS;

/// <summary>
/// iOS implementation of IWaterMeterService.
/// Bridges C# calls to iOS WaterMeterSDK.framework via ObjC bindings.
/// </summary>
public class WaterMeterServiceiOS : NSObject, IWaterMeterService, IWMCameraScannerDelegateObjC
{
    private const string SdkVersion = "1.2.0";
    private bool _licenseInitialized;
    private TaskCompletionSource<ScanResult>? _scanTcs;

    // ========== License Management ==========

    public Task<LicenseResult> InitializeLicenseAsync(string licenseKey, Dictionary<string, object>? metadataInfo = null, string? deviceUser = null)
    {
        var tcs = new TaskCompletionSource<LicenseResult>();

        NSDictionary? metadata = null;
        if (metadataInfo != null)
        {
            var keys = metadataInfo.Keys.Select(k => new NSString(k)).ToArray();
            var values = metadataInfo.Values.Select(v => NSObject.FromObject(v)).ToArray();
            metadata = NSDictionary.FromObjectsAndKeys(values, keys);
        }

        WaterMeterSDKBinding.Shared.InitializeLicenseWithLicenseKey(
            licenseKey,
            metadata,
            deviceUser,
            (success, errorMessage) =>
            {
                _licenseInitialized = success;
                var status = (int)WaterMeterSDKBinding.Shared.LicenseStatus;
                tcs.TrySetResult(new LicenseResult
                {
                    Valid = success,
                    Status = (LicenseStatus)status,
                    Message = success ? GetStatusMessage(status) : (errorMessage ?? "License activation failed")
                });
            });

        return tcs.Task;
    }

    public Task<LicenseResult> IsLicenseValidAsync()
    {
        var valid = WaterMeterSDKBinding.Shared.IsLicenseValid;
        var status = (int)WaterMeterSDKBinding.Shared.LicenseStatus;
        return Task.FromResult(new LicenseResult
        {
            Valid = valid,
            Status = (LicenseStatus)status,
            Message = GetStatusMessage(status)
        });
    }

    public Task<bool> InitializeAsync(InitializeOptions? options = null)
    {
        options ??= new InitializeOptions();

        var config = new WMPredictorConfigurationObjC(
            threadCount: options.ThreadCount,
            cpuPowerMode: 1, // Normal
            useGPU: options.UseGPU,
            maxSideLength: options.MaxSideLength,
            detectionThreshold: 0.5f,
            recognitionThreshold: 0.7f);

        NSError? error = null;
        WaterMeterSDKBinding.Shared.InitializeWithBundle(NSBundle.MainBundle, config, out error);

        return Task.FromResult(error == null);
    }

    /// <summary>
    /// Auto-initialize OCR model if not already initialized.
    /// Matches Cordova's ensureInitializedWithCompletion: pattern.
    /// </summary>
    private void EnsureInitialized()
    {
        if (WaterMeterSDKBinding.Shared.IsInitialized)
            return;

        var config = new WMPredictorConfigurationObjC(
            threadCount: 2,
            cpuPowerMode: 1, // Normal
            useGPU: false,
            maxSideLength: 480, // Model trained with 480x480
            detectionThreshold: 0.5f,
            recognitionThreshold: 0.7f);

        NSError? error = null;
        WaterMeterSDKBinding.Shared.InitializeWithBundle(NSBundle.MainBundle, config, out error);

        if (error != null)
            throw new Exception($"SDK initialization failed: {error.LocalizedDescription}");
    }

    // ========== Camera Scanner ==========

    public Task<ScanResult> ScanAsync(ScanOptions? options = null)
    {
        options ??= new ScanOptions();
        _scanTcs = new TaskCompletionSource<ScanResult>();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            try
            {
                // Auto-initialize OCR model if needed (matches Cordova)
                EnsureInitialized();

                var viewController = GetTopViewController();
                if (viewController == null)
                {
                    _scanTcs.TrySetException(new Exception("No view controller available"));
                    return;
                }

                // Build scanner configuration
                nint autoCaptureSet = options.AutoCapture.HasValue
                    ? (options.AutoCapture.Value ? 1 : 0) : -1;
                float minConfidenceSet = options.MinConfidence.HasValue
                    ? (float)options.MinConfidence.Value : -1f;

                var config = new WMScannerConfigurationObjC(
                    autoCaptureSet: autoCaptureSet,
                    minConfidenceSet: minConfidenceSet,
                    flashEnabled: false,
                    showCloseButton: options.ShowCloseButton,
                    title: options.Title,
                    imageMaxWidth: options.ImageMaxWidth ?? 0,
                    imageMaxHeight: options.ImageMaxHeight ?? 0);

                NSError? error = null;
                WaterMeterSDKBinding.Shared.PresentScannerWithConfiguration(
                    config,
                    this, // delegate
                    viewController,
                    out error);

                if (error != null)
                {
                    _scanTcs.TrySetException(new Exception($"Scanner error: {error.LocalizedDescription}"));
                }
            }
            catch (Exception ex)
            {
                _scanTcs.TrySetException(new Exception($"Failed to launch scanner: {ex.Message}", ex));
            }
        });

        return _scanTcs.Task;
    }

    // ========== WMCameraScannerDelegate_ObjC ==========

    public void Scanner(WMCameraScanner scanner, WMScanResultObjC result)
    {
        if (_scanTcs == null) return;

        // Convert imagePath to base64 for cross-platform compatibility (matches Cordova)
        string? imageBase64 = null;
        var imagePath = result.ImagePath;
        if (!string.IsNullOrEmpty(imagePath))
        {
            imageBase64 = ConvertImageToBase64(imagePath);
        }

        var scanResult = new ScanResult
        {
            Text = result.Text ?? "",
            Confidence = result.Confidence,
            Success = result.Success,
            ImagePath = imagePath,
            ImageBase64 = imageBase64,
            IsReliable = result.OcrResult?.IsReliable ?? false,
            FormattedReading = result.OcrResult?.FormattedReading
        };

        _scanTcs.TrySetResult(scanResult);
        _scanTcs = null;
    }

    public void ScannerDidFail(WMCameraScanner scanner, NSError error)
    {
        _scanTcs?.TrySetResult(new ScanResult
        {
            Text = "",
            Confidence = 0,
            Success = false,
            Message = error.LocalizedDescription
        });
        _scanTcs = null;
    }

    public void ScannerDidCancel(WMCameraScanner scanner)
    {
        _scanTcs?.TrySetResult(new ScanResult
        {
            Text = "",
            Confidence = 0,
            Success = false,
            Message = "User cancelled"
        });
        _scanTcs = null;
    }

    // ========== Image Recognition ==========

    public Task<ScanResult> RecognizeBase64Async(string base64Image)
    {
        return Task.Run(() =>
        {
            // Auto-initialize OCR model if needed (matches Cordova)
            EnsureInitialized();

            var base64Data = base64Image;
            if (base64Data.Contains("base64,"))
                base64Data = base64Data[(base64Data.IndexOf("base64,") + 7)..];

            var data = new NSData(base64Data, NSDataBase64DecodingOptions.IgnoreUnknownCharacters);
            var image = UIImage.LoadFromData(data);

            if (image == null)
                throw new Exception("Failed to decode base64 image");

            return PerformRecognition(image);
        });
    }

    public Task<ScanResult> RecognizeFileAsync(string filePath)
    {
        return Task.Run(() =>
        {
            // Auto-initialize OCR model if needed (matches Cordova)
            EnsureInitialized();

            var path = filePath;
            if (path.StartsWith("file://"))
            {
                var url = NSUrl.FromString(path);
                path = url?.Path ?? path;
            }

            var image = UIImage.FromFile(path);
            if (image == null)
                throw new Exception($"Failed to load image from: {path}");

            return PerformRecognition(image);
        });
    }

    private ScanResult PerformRecognition(UIImage image)
    {
        NSError? error = null;
        var ocrResult = WaterMeterSDKBinding.Shared.RecognizeWithImage(image, out error);

        if (error != null || ocrResult == null)
        {
            return new ScanResult
            {
                Text = "",
                Confidence = 0,
                Success = false,
                Message = error?.LocalizedDescription ?? "Recognition failed"
            };
        }

        return new ScanResult
        {
            Text = ocrResult.Text ?? "",
            Confidence = ocrResult.Confidence,
            Success = ocrResult.Success,
            ImagePath = ocrResult.ImagePath,
            IsReliable = ocrResult.IsReliable,
            FormattedReading = ocrResult.FormattedReading
        };
    }

    // ========== Permissions ==========

    public Task<PermissionResult> CheckPermissionAsync()
    {
        var status = WaterMeterSDKBinding.CheckCameraPermission();
        return Task.FromResult(new PermissionResult
        {
            Granted = status == 3, // AVAuthorizationStatusAuthorized
            Status = status switch
            {
                0 => "not_determined",
                1 => "restricted",
                2 => "denied",
                3 => "authorized",
                _ => "unknown"
            }
        });
    }

    public Task<PermissionResult> RequestPermissionAsync()
    {
        var tcs = new TaskCompletionSource<PermissionResult>();

        WaterMeterSDKBinding.RequestCameraPermissionWithCompletion((granted) =>
        {
            tcs.TrySetResult(new PermissionResult
            {
                Granted = granted,
                Status = granted ? "authorized" : "denied"
            });
        });

        return tcs.Task;
    }

    // ========== Utility ==========

    public Task<string> GetVersionAsync()
    {
        return Task.FromResult(WaterMeterSDKBinding.SdkVersion ?? SdkVersion);
    }

    public Task<bool> IsInitializedAsync()
    {
        return Task.FromResult(WaterMeterSDKBinding.Shared.IsInitialized);
    }

    public Task ResetAsync()
    {
        WaterMeterSDKBinding.Shared.Reset();
        _licenseInitialized = false;
        return Task.CompletedTask;
    }

    public Task OpenSettingsAsync()
    {
        var tcs = new TaskCompletionSource();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            var vc = GetTopViewController();
            if (vc == null)
            {
                tcs.TrySetResult();
                return;
            }

            var settingsVC = new WMSettingsViewController();
            var navVC = new UINavigationController(settingsVC);
            vc.PresentViewController(navVC, true, () => tcs.TrySetResult());
        });

        return tcs.Task;
    }

    // ========== Helpers ==========

    /// <summary>
    /// Convert image file to base64 data URL (matches Cordova ocrResultToDictionary).
    /// </summary>
    private static string? ConvertImageToBase64(string imagePath)
    {
        try
        {
            var data = NSData.FromFile(imagePath);
            if (data == null) return null;

            var base64 = data.GetBase64EncodedString(NSDataBase64EncodingOptions.None);
            return $"data:image/jpeg;base64,{base64}";
        }
        catch
        {
            return null;
        }
    }

    private UIViewController? GetTopViewController()
    {
        var window = UIApplication.SharedApplication.ConnectedScenes
            .OfType<UIWindowScene>()
            .SelectMany(s => s.Windows)
            .FirstOrDefault(w => w.IsKeyWindow);

        var vc = window?.RootViewController;
        while (vc?.PresentedViewController != null)
            vc = vc.PresentedViewController;

        return vc;
    }

    private string GetStatusMessage(int status) => status switch
    {
        0 => "SDK not initialized",
        1 => "License is valid",
        2 => "License has expired",
        3 => "License in grace period",
        4 => "Invalid license key",
        5 => "License has been blocked",
        6 => "Quota exceeded, sync required",
        _ => "Unknown status"
    };
}
