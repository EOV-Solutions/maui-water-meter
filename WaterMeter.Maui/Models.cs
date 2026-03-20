namespace WaterMeter.Maui;

/// <summary>
/// License status codes matching native SDK.
/// </summary>
public enum LicenseStatus
{
    /// <summary>SDK not initialized</summary>
    NotInitialized = 0,
    /// <summary>License is valid</summary>
    Valid = 1,
    /// <summary>License has expired</summary>
    Expired = 2,
    /// <summary>License in grace period</summary>
    GracePeriod = 3,
    /// <summary>Invalid license key</summary>
    Invalid = 4,
    /// <summary>License has been blocked</summary>
    Blocked = 5,
    /// <summary>Quota exceeded, sync required</summary>
    QuotaExceeded = 6
}

/// <summary>
/// Result from license initialization/validation.
/// </summary>
public class LicenseResult
{
    /// <summary>Whether the license is valid.</summary>
    public bool Valid { get; set; }

    /// <summary>License status code.</summary>
    public LicenseStatus Status { get; set; }

    /// <summary>Status message.</summary>
    public string? Message { get; set; }

    public override string ToString() =>
        $"LicenseResult(Valid={Valid}, Status={Status}, Message={Message})";
}

/// <summary>
/// OCR scan result from camera scanner or image recognition.
/// </summary>
public class ScanResult
{
    /// <summary>Recognized meter reading text.</summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>Recognition confidence (0.0 - 1.0).</summary>
    public double Confidence { get; set; }

    /// <summary>Whether the scan was successful.</summary>
    public bool Success { get; set; }

    /// <summary>Path to captured image (if available).</summary>
    public string? ImagePath { get; set; }

    /// <summary>Base64 encoded image (if available).</summary>
    public string? ImageBase64 { get; set; }

    /// <summary>Formatted reading with decimal point.</summary>
    public string? FormattedReading { get; set; }

    /// <summary>Whether the result is considered reliable.</summary>
    public bool IsReliable { get; set; }

    /// <summary>Error or status message (if any).</summary>
    public string? Message { get; set; }

    public override string ToString() =>
        $"ScanResult(Text={Text}, Confidence={Confidence:F2}, Success={Success})";
}

/// <summary>
/// Options for camera scanner.
/// </summary>
public class ScanOptions
{
    /// <summary>Custom title for scanner screen. Default: "Quét số đồng hồ"</summary>
    public string Title { get; set; } = "Quét số đồng hồ";

    /// <summary>Show close button. Default: true</summary>
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>Auto close after scan result. Default: true</summary>
    public bool AutoCloseOnResult { get; set; } = true;

    /// <summary>Auto capture when meter detected. Uses SDK settings if null.</summary>
    public bool? AutoCapture { get; set; }

    /// <summary>Minimum confidence threshold (0.0 - 1.0). Uses SDK settings if null.</summary>
    public double? MinConfidence { get; set; }

    /// <summary>Max width for saved image in pixels.</summary>
    public int? ImageMaxWidth { get; set; }

    /// <summary>Max height for saved image in pixels.</summary>
    public int? ImageMaxHeight { get; set; }
}

/// <summary>
/// SDK initialization options (iOS only, Android auto-initializes).
/// </summary>
public class InitializeOptions
{
    /// <summary>Number of threads for inference. Default: 2</summary>
    public int ThreadCount { get; set; } = 2;

    /// <summary>Whether to use GPU acceleration. Default: false</summary>
    public bool UseGPU { get; set; } = false;

    /// <summary>Maximum image dimension. Default: 480</summary>
    public int MaxSideLength { get; set; } = 480;
}

/// <summary>
/// Permission check result.
/// </summary>
public class PermissionResult
{
    /// <summary>Whether permission is granted.</summary>
    public bool Granted { get; set; }

    /// <summary>Permission status string.</summary>
    public string? Status { get; set; }
}
