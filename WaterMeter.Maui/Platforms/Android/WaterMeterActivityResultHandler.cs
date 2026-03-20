using Android.App;
using Android.Content;

namespace WaterMeter.Maui.Platforms.Android;

/// <summary>
/// MainActivity base class that handles activity results for Water Meter SDK.
/// Your MainActivity should inherit from this or manually call HandleActivityResult.
/// </summary>
public static class WaterMeterActivityResultHandler
{
    /// <summary>
    /// Call this from your MainActivity's OnActivityResult to handle scanner results.
    /// </summary>
    public static bool HandleActivityResult(int requestCode, Result resultCode, Intent? data)
    {
        return ActivityResultCallbackHelper.HandleResult(requestCode, resultCode, data);
    }
}
