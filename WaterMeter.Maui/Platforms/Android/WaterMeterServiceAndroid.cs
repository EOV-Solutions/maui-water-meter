using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Util;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Com.Eov.Watermeter;
using Com.Eov.Watermeter.Ocr;
using Com.Eov.Watermeter.UI;
using Com.Eov.Watermeter.Model;
using Org.Json;

namespace WaterMeter.Maui.Platforms.Android;

/// <summary>
/// Android implementation of IWaterMeterService.
/// Bridges C# calls to Android Water Meter SDK via Java/Kotlin bindings.
/// </summary>
public class WaterMeterServiceAndroid : IWaterMeterService
{
    private const string Tag = "WaterMeterMAUI";
    private const string SdkVersion = "1.2.0";
    private const int RequestCameraScan = 3001;
    private const int RequestCameraPermission = 3002;
    private const int RequestSettings = 3003;

    private TaskCompletionSource<ScanResult>? _scanTcs;
    private bool _licenseInitialized;

    private Activity CurrentActivity =>
        Microsoft.Maui.ApplicationModel.Platform.CurrentActivity
        ?? throw new InvalidOperationException("No current Activity available.");

    public WaterMeterServiceAndroid()
    {
        // Register activity result handler
        Microsoft.Maui.ApplicationModel.Platform.ActivityStateChanged += OnActivityStateChanged;
    }

    private void OnActivityStateChanged(object? sender, Microsoft.Maui.ApplicationModel.ActivityStateChangedEventArgs e)
    {
        // Handle activity results if needed
    }

    // ========== License Management ==========

    public Task<LicenseResult> InitializeLicenseAsync(string licenseKey, Dictionary<string, object>? metadataInfo = null, string? deviceUser = null)
    {
        var tcs = new TaskCompletionSource<LicenseResult>();
        var activity = CurrentActivity;

        JSONObject? metadata = null;
        if (metadataInfo != null)
        {
            metadata = new JSONObject();
            foreach (var kvp in metadataInfo)
            {
                metadata.Put(kvp.Key, kvp.Value?.ToString());
            }
        }

        Log.Debug(Tag, $"InitializeLicense: key={licenseKey}, metadata={metadata}, user={deviceUser}");

        WaterMeterSDK.Initialize(
            activity.ApplicationContext!,
            licenseKey,
            metadata,
            deviceUser,
            new LicenseCallbackImpl(
                onSuccess: () =>
                {
                    _licenseInitialized = true;
                    activity.RunOnUiThread(() =>
                    {
                        tcs.TrySetResult(new LicenseResult
                        {
                            Valid = true,
                            Status = (LicenseStatus)WaterMeterSDK.LicenseStatus,
                            Message = WaterMeterSDK.StatusMessage
                        });
                    });
                },
                onError: (errorMessage) =>
                {
                    activity.RunOnUiThread(() =>
                    {
                        tcs.TrySetResult(new LicenseResult
                        {
                            Valid = false,
                            Status = (LicenseStatus)WaterMeterSDK.LicenseStatus,
                            Message = errorMessage
                        });
                    });
                }
            )
        );

        return tcs.Task;
    }

    public Task<LicenseResult> IsLicenseValidAsync()
    {
        var valid = WaterMeterSDK.IsLicenseValid;
        return Task.FromResult(new LicenseResult
        {
            Valid = valid,
            Status = (LicenseStatus)WaterMeterSDK.LicenseStatus,
            Message = WaterMeterSDK.StatusMessage
        });
    }

    public Task<bool> InitializeAsync(InitializeOptions? options = null)
    {
        // Auto-initialize PredictorManager with OCR model
        try
        {
            var manager = PredictorManager.Instance!;
            if (!manager.IsInitialized)
            {
                var activity = CurrentActivity;
                var initialized = manager.Init(
                    activity.ApplicationContext!,
                    "models/ch_PP-OCRv2",
                    "labels/ppocr_keys_v1.txt",
                    0, // useOpencl
                    options?.ThreadCount ?? 4,
                    "LITE_POWER_HIGH");
                return Task.FromResult(initialized);
            }
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Log.Error(Tag, $"InitializeAsync failed: {ex.Message}");
            return Task.FromResult(false);
        }
    }

    // ========== Camera Scanner ==========

    public Task<ScanResult> ScanAsync(ScanOptions? options = null)
    {
        var activity = CurrentActivity;
        options ??= new ScanOptions();

        if (!HasCameraPermission())
        {
            throw new InvalidOperationException("Camera permission not granted. Call RequestPermissionAsync() first.");
        }

        _scanTcs = new TaskCompletionSource<ScanResult>();

        try
        {
            var intent = new Intent(activity, typeof(CameraScanActivity));

            if (!string.IsNullOrEmpty(options.Title))
                intent.PutExtra(CameraScanActivity.ExtraTitle, options.Title);

            intent.PutExtra(CameraScanActivity.ExtraShowCloseButton, options.ShowCloseButton);
            intent.PutExtra(CameraScanActivity.ExtraAutoCloseOnResult, options.AutoCloseOnResult);

            if (options.ImageMaxWidth.HasValue)
                intent.PutExtra(CameraScanActivity.ExtraImageMaxWidth, options.ImageMaxWidth.Value);

            if (options.ImageMaxHeight.HasValue)
                intent.PutExtra(CameraScanActivity.ExtraImageMaxHeight, options.ImageMaxHeight.Value);

            // Register for activity result
            ActivityResultCallbackHelper.RegisterCallback(RequestCameraScan, (resultCode, data) =>
            {
                HandleScanResult(resultCode, data);
            });

            activity.StartActivityForResult(intent, RequestCameraScan);
        }
        catch (Exception ex)
        {
            _scanTcs.TrySetException(new Exception($"Failed to launch scanner: {ex.Message}", ex));
        }

        return _scanTcs.Task;
    }

    private void HandleScanResult(Result resultCode, Intent? data)
    {
        if (_scanTcs == null) return;

        if (resultCode == Result.Ok && data != null)
        {
            var text = data.GetStringExtra(CameraScanActivity.ExtraResultText) ?? "";
            var confidence = data.GetFloatExtra(CameraScanActivity.ExtraResultConfidence, 0f);
            var imagePath = data.GetStringExtra(CameraScanActivity.ExtraResultImagePath);

            // Convert image to base64 for cross-platform compatibility (matches Cordova)
            string? imageBase64 = null;
            if (!string.IsNullOrEmpty(imagePath))
            {
                imageBase64 = ConvertImageToBase64(imagePath);
            }

            _scanTcs.TrySetResult(new ScanResult
            {
                Text = text,
                Confidence = confidence,
                Success = !string.IsNullOrEmpty(text),
                ImagePath = imagePath,
                ImageBase64 = imageBase64,
                IsReliable = confidence >= 0.7,
                FormattedReading = !string.IsNullOrEmpty(text) ? WaterMeterSdk.FormatReading(text) : null
            });
        }
        else
        {
            _scanTcs.TrySetResult(new ScanResult
            {
                Text = "",
                Confidence = 0,
                Success = false,
                Message = "User cancelled"
            });
        }

        _scanTcs = null;
    }

    // ========== Image Recognition ==========

    public Task<ScanResult> RecognizeBase64Async(string base64Image)
    {
        return Task.Run(() =>
        {
            var base64Data = base64Image;
            if (base64Data.Contains("base64,"))
                base64Data = base64Data[(base64Data.IndexOf("base64,") + 7)..];

            var imageBytes = Base64.Decode(base64Data, Base64Flags.Default);
            var bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes!.Length);

            if (bitmap == null)
                throw new Exception("Failed to decode base64 image");

            return PerformOCR(bitmap);
        });
    }

    public Task<ScanResult> RecognizeFileAsync(string filePath)
    {
        return Task.Run(() =>
        {
            var path = filePath;
            if (path.StartsWith("file://"))
                path = path[7..];

            var bitmap = BitmapFactory.DecodeFile(path);
            if (bitmap == null)
                throw new Exception($"Failed to load image from: {path}");

            return PerformOCR(bitmap);
        });
    }

    private ScanResult PerformOCR(Bitmap bitmap)
    {
        var activity = CurrentActivity;
        var manager = PredictorManager.Instance!;

        if (!manager.IsInitialized)
        {
            var initialized = manager.Init(
                activity.ApplicationContext!,
                "models/ch_PP-OCRv2",
                "labels/ppocr_keys_v1.txt",
                0, // useOpencl
                4, // cpuThreadNum
                "LITE_POWER_HIGH");
            if (!initialized)
                throw new Exception("Failed to initialize OCR predictor");
        }

        var predictor = manager.Predictor;
        if (predictor == null)
            throw new Exception("OCR predictor not available");

        predictor.SetInputImage(bitmap);
        predictor.RunModel(1, 1, 1);

        // Get results — matches Cordova: predictor.outputResult() + predictor.conf_rec()
        var text = predictor.OutputResult() ?? "";
        float confidence = 0f;
        try
        {
            // Call conf_rec() via reflection because C# binding may have name conflict
            // (protected field conf_rec shadows the public method conf_rec())
            var confRecMethod = Java.Lang.Class.FromType(typeof(Predictor)).GetDeclaredMethod("conf_rec");
            if (confRecMethod != null)
            {
                confRecMethod.Accessible = true;
                var result = confRecMethod.Invoke(predictor);
                if (result is Java.Lang.Number number)
                    confidence = number.FloatValue();
            }
        }
        catch (Exception ex)
        {
            Log.Warn(Tag, $"Failed to get conf_rec via reflection: {ex.Message}");
            // Fallback: if text was found, assume reasonable confidence
            if (!string.IsNullOrEmpty(text))
                confidence = 0.5f;
        }

        bitmap.Recycle();

        return new ScanResult
        {
            Text = text,
            Confidence = confidence,
            Success = !string.IsNullOrEmpty(text),
            IsReliable = confidence >= 0.7,
            FormattedReading = !string.IsNullOrEmpty(text) ? WaterMeterSdk.FormatReading(text) : null
        };
    }

    // ========== Permissions ==========

    public Task<PermissionResult> CheckPermissionAsync()
    {
        return Task.FromResult(new PermissionResult
        {
            Granted = HasCameraPermission(),
            Status = HasCameraPermission() ? "granted" : "denied"
        });
    }

    public async Task<PermissionResult> RequestPermissionAsync()
    {
        if (HasCameraPermission())
        {
            return new PermissionResult { Granted = true, Status = "granted" };
        }

        var status = await Microsoft.Maui.ApplicationModel.Permissions.RequestAsync<Microsoft.Maui.ApplicationModel.Permissions.Camera>();
        return new PermissionResult
        {
            Granted = status == Microsoft.Maui.ApplicationModel.PermissionStatus.Granted,
            Status = status.ToString().ToLower()
        };
    }

    private bool HasCameraPermission()
    {
        return ContextCompat.CheckSelfPermission(CurrentActivity, global::Android.Manifest.Permission.Camera)
            == Permission.Granted;
    }

    // ========== Utility ==========

    public Task<string> GetVersionAsync() => Task.FromResult(SdkVersion);

    public Task<bool> IsInitializedAsync()
    {
        // Match Cordova: check PredictorManager.isInitialized()
        try
        {
            var manager = PredictorManager.Instance;
            return Task.FromResult(manager?.IsInitialized ?? false);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public Task ResetAsync()
    {
        // Match Cordova: call PredictorManager.release()
        try
        {
            PredictorManager.Instance?.Release();
            Log.Info(Tag, "SDK resources released");
        }
        catch (Exception ex)
        {
            Log.Warn(Tag, $"Error releasing SDK: {ex.Message}");
        }
        _licenseInitialized = false;
        return Task.CompletedTask;
    }

    public Task OpenSettingsAsync()
    {
        var activity = CurrentActivity;
        var intent = new Intent(activity, typeof(CameraSettingsActivity));
        activity.StartActivityForResult(intent, RequestSettings);
        return Task.CompletedTask;
    }

    // ========== Helpers ==========

    /// <summary>
    /// Convert image file to base64 data URL (matches Cordova convertImageToBase64).
    /// </summary>
    private static string? ConvertImageToBase64(string imagePath)
    {
        try
        {
            var file = new Java.IO.File(imagePath);
            if (!file.Exists())
                return null;

            // Decode with inSampleSize to reduce memory (matches Cordova)
            var options = new BitmapFactory.Options { InSampleSize = 2 };
            var bitmap = BitmapFactory.DecodeFile(imagePath, options);
            if (bitmap == null)
                return null;

            using var stream = new System.IO.MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Jpeg!, 80, stream);
            bitmap.Recycle();

            var base64 = Convert.ToBase64String(stream.ToArray());
            return $"data:image/jpeg;base64,{base64}";
        }
        catch (Exception ex)
        {
            Log.Warn(Tag, $"Error converting image to base64: {ex.Message}");
            return null;
        }
    }

    // ========== Inner Classes ==========

    private class LicenseCallbackImpl : Java.Lang.Object, WaterMeterSDK.ILicenseCallback
    {
        private readonly Action _onSuccess;
        private readonly Action<string> _onError;

        public LicenseCallbackImpl(Action onSuccess, Action<string> onError)
        {
            _onSuccess = onSuccess;
            _onError = onError;
        }

        public void OnSuccess() => _onSuccess();
        public void OnError(string? errorMessage) => _onError(errorMessage ?? "Unknown error");
    }
}

/// <summary>
/// Helper to handle activity results in MAUI.
/// </summary>
internal static class ActivityResultCallbackHelper
{
    private static readonly Dictionary<int, Action<Result, Intent?>> _callbacks = new();

    public static void RegisterCallback(int requestCode, Action<Result, Intent?> callback)
    {
        _callbacks[requestCode] = callback;
    }

    public static bool HandleResult(int requestCode, Result resultCode, Intent? data)
    {
        if (_callbacks.TryGetValue(requestCode, out var callback))
        {
            callback(resultCode, data);
            _callbacks.Remove(requestCode);
            return true;
        }
        return false;
    }
}
