namespace WaterMeter.Maui;

/// <summary>
/// Interface for Water Meter SDK platform-specific implementations.
/// </summary>
public interface IWaterMeterService
{
    // ========== License Management ==========

    /// <summary>
    /// Initialize SDK with license key (required before scanning).
    /// </summary>
    /// <param name="licenseKey">License key string.</param>
    /// <param name="metadataInfo">Optional metadata for admin tracking.</param>
    /// <param name="deviceUser">Optional device user identifier.</param>
    /// <returns>License result.</returns>
    Task<LicenseResult> InitializeLicenseAsync(string licenseKey, Dictionary<string, object>? metadataInfo = null, string? deviceUser = null);

    /// <summary>
    /// Check if SDK license is valid.
    /// </summary>
    Task<LicenseResult> IsLicenseValidAsync();

    /// <summary>
    /// Initialize the SDK engine (iOS only, Android auto-initializes).
    /// </summary>
    Task<bool> InitializeAsync(InitializeOptions? options = null);

    // ========== Camera Scanner ==========

    /// <summary>
    /// Open camera scanner to scan water meter number.
    /// </summary>
    /// <param name="options">Scanner options.</param>
    /// <returns>Scan result with recognized text and confidence.</returns>
    Task<ScanResult> ScanAsync(ScanOptions? options = null);

    // ========== Image Recognition ==========

    /// <summary>
    /// Recognize water meter reading from base64 encoded image.
    /// </summary>
    /// <param name="base64Image">Base64 encoded image (with or without data URL prefix).</param>
    Task<ScanResult> RecognizeBase64Async(string base64Image);

    /// <summary>
    /// Recognize water meter reading from file path.
    /// </summary>
    /// <param name="filePath">Path to image file.</param>
    Task<ScanResult> RecognizeFileAsync(string filePath);

    // ========== Permissions ==========

    /// <summary>
    /// Check if camera permission is granted.
    /// </summary>
    Task<PermissionResult> CheckPermissionAsync();

    /// <summary>
    /// Request camera permission.
    /// </summary>
    Task<PermissionResult> RequestPermissionAsync();

    // ========== Utility ==========

    /// <summary>
    /// Get SDK version.
    /// </summary>
    Task<string> GetVersionAsync();

    /// <summary>
    /// Check if SDK is initialized.
    /// </summary>
    Task<bool> IsInitializedAsync();

    /// <summary>
    /// Reset SDK (release resources).
    /// </summary>
    Task ResetAsync();

    /// <summary>
    /// Open SDK settings screen.
    /// </summary>
    Task OpenSettingsAsync();
}
