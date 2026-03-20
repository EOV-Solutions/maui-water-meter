namespace WaterMeter.Maui;

/// <summary>
/// Water Meter SDK for .NET MAUI - Camera-based OCR scanning for water meter reading using AI.
/// 
/// Usage:
/// <code>
/// // 1. Register in MauiProgram.cs
/// builder.UseWaterMeter();
/// 
/// // 2. Initialize license (required)
/// var result = await WaterMeterSdk.InitializeLicenseAsync("YOUR_LICENSE_KEY");
/// 
/// // 3. Scan water meter
/// var scan = await WaterMeterSdk.ScanAsync();
/// Console.WriteLine($"Reading: {scan.Text}");
/// </code>
/// </summary>
public static class WaterMeterSdk
{
    /// <summary>SDK Version.</summary>
    public const string Version = "1.2.0";

    private static IWaterMeterService? _service;

    /// <summary>
    /// Get the platform-specific service implementation.
    /// </summary>
    internal static IWaterMeterService Service
    {
        get => _service ?? throw new InvalidOperationException(
            "WaterMeter SDK not initialized. Call builder.UseWaterMeter() in MauiProgram.cs");
        set => _service = value;
    }

    // ========== License Management ==========

    /// <summary>
    /// Initialize SDK with license key (required before scanning).
    /// </summary>
    /// <param name="licenseKey">License key string.</param>
    /// <param name="metadataInfo">Optional metadata for admin tracking.</param>
    /// <param name="deviceUser">Optional device user identifier.</param>
    public static Task<LicenseResult> InitializeLicenseAsync(
        string licenseKey,
        Dictionary<string, object>? metadataInfo = null,
        string? deviceUser = null) =>
        Service.InitializeLicenseAsync(licenseKey, metadataInfo, deviceUser);

    /// <summary>
    /// Check if SDK license is valid.
    /// </summary>
    public static Task<LicenseResult> IsLicenseValidAsync() =>
        Service.IsLicenseValidAsync();

    /// <summary>
    /// Initialize the SDK engine (iOS only, Android auto-initializes).
    /// </summary>
    public static Task<bool> InitializeAsync(InitializeOptions? options = null) =>
        Service.InitializeAsync(options);

    // ========== Camera Scanner ==========

    /// <summary>
    /// Open camera scanner to scan water meter number.
    /// </summary>
    public static Task<ScanResult> ScanAsync(ScanOptions? options = null) =>
        Service.ScanAsync(options);

    // ========== Image Recognition ==========

    /// <summary>
    /// Recognize water meter reading from base64 encoded image.
    /// </summary>
    public static Task<ScanResult> RecognizeBase64Async(string base64Image) =>
        Service.RecognizeBase64Async(base64Image);

    /// <summary>
    /// Recognize water meter reading from file path.
    /// </summary>
    public static Task<ScanResult> RecognizeFileAsync(string filePath) =>
        Service.RecognizeFileAsync(filePath);

    // ========== Permissions ==========

    /// <summary>
    /// Check if camera permission is granted.
    /// </summary>
    public static Task<PermissionResult> CheckPermissionAsync() =>
        Service.CheckPermissionAsync();

    /// <summary>
    /// Request camera permission.
    /// </summary>
    public static Task<PermissionResult> RequestPermissionAsync() =>
        Service.RequestPermissionAsync();

    // ========== Utility ==========

    /// <summary>
    /// Get SDK version.
    /// </summary>
    public static Task<string> GetVersionAsync() =>
        Service.GetVersionAsync();

    /// <summary>
    /// Check if SDK is initialized.
    /// </summary>
    public static Task<bool> IsInitializedAsync() =>
        Service.IsInitializedAsync();

    /// <summary>
    /// Reset SDK (release resources).
    /// </summary>
    public static Task ResetAsync() =>
        Service.ResetAsync();

    /// <summary>
    /// Open SDK settings screen.
    /// </summary>
    public static Task OpenSettingsAsync() =>
        Service.OpenSettingsAsync();

    // ========== Helpers ==========

    /// <summary>
    /// Format meter reading with decimal point.
    /// </summary>
    /// <param name="text">Raw reading text (e.g., "12345678").</param>
    /// <param name="decimalPlaces">Number of decimal places (default: 3).</param>
    /// <returns>Formatted reading (e.g., "12345.678").</returns>
    /// <summary>
    /// Format meter reading by removing leading zeros.
    /// </summary>
    /// <param name="text">Raw reading text (e.g., "001244")</param>
    /// <returns>Formatted reading (e.g., "1244")</returns>
    public static string FormatReading(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        var result = text.TrimStart('0');
        return string.IsNullOrEmpty(result) ? "0" : result;
    }
}
