using System;
using Foundation;
using ObjCRuntime;
using UIKit;
using AVFoundation;
using CoreMedia;

namespace WaterMeter.Maui.Platforms.iOS;

/// <summary>
/// C# binding for WaterMeterSDK.framework native iOS classes.
/// Maps Swift classes (with mangled ObjC runtime names) to managed types.
/// </summary>

// WaterMeterSDK main class (Swift: WaterMeterSDK)
[BaseType(typeof(NSObject), Name = "_TtC13WaterMeterSDK13WaterMeterSDK")]
interface WaterMeterSDKBinding
{
    [Static, Export("shared")]
    WaterMeterSDKBinding Shared { get; }

    [Static, Export("sdkVersion")]
    string SdkVersion { get; }

    [Export("isInitialized")]
    bool IsInitialized { get; }

    [Export("isLicenseValid")]
    bool IsLicenseValid { get; }

    [Export("licenseStatus")]
    nint LicenseStatus { get; }

    [Export("initializeWithBundle:configuration:error:")]
    void InitializeWithBundle(NSBundle bundle, [NullAllowed] WMPredictorConfigurationObjC configuration, out NSError error);

    [Export("reset")]
    void Reset();

    [Export("initializeLicenseWithLicenseKey:completion:")]
    void InitializeLicenseWithLicenseKey(string licenseKey, Action<bool, string?> completion);

    [Export("initializeLicenseWithLicenseKey:metadataInfo:deviceUser:completion:")]
    void InitializeLicenseWithLicenseKey(string licenseKey, [NullAllowed] NSDictionary metadataInfo, [NullAllowed] string deviceUser, Action<bool, string?> completion);

    [Export("recognizeWithImage:error:")]
    [return: NullAllowed]
    WMOCRScanResultObjC RecognizeWithImage(UIImage image, out NSError error);

    [Static, Export("checkCameraPermission")]
    nint CheckCameraPermission();

    [Static, Export("requestCameraPermissionWithCompletion:")]
    void RequestCameraPermissionWithCompletion(Action<bool> completion);

    [Export("presentScannerWithConfiguration:delegate:from:error:")]
    void PresentScannerWithConfiguration([NullAllowed] WMScannerConfigurationObjC configuration, [NullAllowed] IWMCameraScannerDelegateObjC scannerDelegate, UIViewController viewController, out NSError error);
}

// WMCameraScanner (Swift mangled name)
[BaseType(typeof(NSObject), Name = "_TtC13WaterMeterSDK15WMCameraScanner")]
interface WMCameraScanner
{
}

// WMCameraScannerDelegate_ObjC protocol (Swift mangled name)
[Protocol(Name = "_TtP13WaterMeterSDK28WMCameraScannerDelegate_ObjC_"), Model]
[BaseType(typeof(NSObject))]
interface WMCameraScannerDelegateObjC
{
    [Abstract]
    [Export("scanner:didScanResult:")]
    void Scanner(WMCameraScanner scanner, WMScanResultObjC result);

    [Export("scanner:didFailWithError:")]
    void ScannerDidFail(WMCameraScanner scanner, NSError error);

    [Export("scannerDidCancel:")]
    void ScannerDidCancel(WMCameraScanner scanner);
}

interface IWMCameraScannerDelegateObjC { }

// WMScanResult_ObjC (Swift mangled name)
[BaseType(typeof(NSObject), Name = "_TtC13WaterMeterSDK17WMScanResult_ObjC")]
interface WMScanResultObjC
{
    [Export("status")]
    nint Status { get; }

    [Export("ocrResult")]
    [NullAllowed]
    WMOCRScanResultObjC OcrResult { get; }

    [Export("text")]
    string Text { get; }

    [Export("confidence")]
    float Confidence { get; }

    [Export("imagePath")]
    [NullAllowed]
    string ImagePath { get; }

    [Export("isSuccess")]
    bool IsSuccess { get; }

    [Export("success")]
    bool Success { get; }
}

// WMOCRScanResult_ObjC (Swift mangled name)
[BaseType(typeof(NSObject), Name = "_TtC13WaterMeterSDK20WMOCRScanResult_ObjC")]
interface WMOCRScanResultObjC
{
    [Export("text")]
    string Text { get; }

    [Export("confidence")]
    float Confidence { get; }

    [Export("classificationLabel")]
    nint ClassificationLabel { get; }

    [Export("classificationScore")]
    float ClassificationScore { get; }

    [Export("processingTimeMs")]
    double ProcessingTimeMs { get; }

    [Export("imagePath")]
    [NullAllowed]
    string ImagePath { get; }

    [Export("isReliable")]
    bool IsReliable { get; }

    [Export("formattedReading")]
    string FormattedReading { get; }

    [Export("success")]
    bool Success { get; }
}

// WMPredictorConfiguration_ObjC (Swift mangled name)
[BaseType(typeof(NSObject), Name = "_TtC13WaterMeterSDK29WMPredictorConfiguration_ObjC")]
interface WMPredictorConfigurationObjC
{
    [Export("threadCount")]
    nint ThreadCount { get; set; }

    [Export("cpuPowerMode")]
    nint CpuPowerMode { get; set; }

    [Export("useGPU")]
    bool UseGPU { get; set; }

    [Export("maxSideLength")]
    nint MaxSideLength { get; set; }

    [Export("detectionThreshold")]
    float DetectionThreshold { get; set; }

    [Export("recognitionThreshold")]
    float RecognitionThreshold { get; set; }

    [Export("initWithThreadCount:cpuPowerMode:useGPU:maxSideLength:detectionThreshold:recognitionThreshold:")]
    NativeHandle Constructor(nint threadCount, nint cpuPowerMode, bool useGPU, nint maxSideLength, float detectionThreshold, float recognitionThreshold);
}

// WMScannerConfiguration_ObjC (Swift mangled name)
[BaseType(typeof(NSObject), Name = "_TtC13WaterMeterSDK27WMScannerConfiguration_ObjC")]
interface WMScannerConfigurationObjC
{
    [Export("autoCapture")]
    bool AutoCapture { get; set; }

    [Export("minConfidence")]
    float MinConfidence { get; set; }

    [Export("flashEnabled")]
    bool FlashEnabled { get; set; }

    [Export("showCloseButton")]
    bool ShowCloseButton { get; set; }

    [Export("title")]
    [NullAllowed]
    string Title { get; set; }

    [Export("imageMaxWidth")]
    nint ImageMaxWidth { get; set; }

    [Export("imageMaxHeight")]
    nint ImageMaxHeight { get; set; }

    [Export("initWithAutoCaptureSet:minConfidenceSet:flashEnabled:showCloseButton:title:imageMaxWidth:imageMaxHeight:")]
    NativeHandle Constructor(nint autoCaptureSet, float minConfidenceSet, bool flashEnabled, bool showCloseButton, [NullAllowed] string title, nint imageMaxWidth, nint imageMaxHeight);
}

// WMSettingsViewController (Swift mangled name)
[BaseType(typeof(UIViewController), Name = "_TtC13WaterMeterSDK24WMSettingsViewController")]
interface WMSettingsViewController
{
}

// WMCameraViewController (Swift mangled name)
[BaseType(typeof(UIViewController), Name = "_TtC13WaterMeterSDK22WMCameraViewController")]
interface WMCameraViewController
{
}
