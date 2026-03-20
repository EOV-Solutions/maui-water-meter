using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WaterMeter.Maui;

/// <summary>
/// Extension methods for registering Water Meter SDK with MAUI.
/// </summary>
public static class WaterMeterExtensions
{
    /// <summary>
    /// Register Water Meter SDK services with MAUI application.
    /// Call this in MauiProgram.cs: builder.UseWaterMeter();
    /// </summary>
    public static MauiAppBuilder UseWaterMeter(this MauiAppBuilder builder)
    {
#if ANDROID
        var service = new Platforms.Android.WaterMeterServiceAndroid();
#elif IOS
        var service = new Platforms.iOS.WaterMeterServiceiOS();
#else
        throw new PlatformNotSupportedException("WaterMeter SDK only supports Android and iOS.");
#endif
        builder.Services.AddSingleton<IWaterMeterService>(service);
        WaterMeterSdk.Service = service;
        return builder;
    }
}
