using System;

[assembly:global::Android.Runtime.NamespaceMapping (Java = "com.eov.watermeter", Managed="Com.Eov.Watermeter")]
[assembly:global::Android.Runtime.NamespaceMapping (Java = "com.eov.watermeter.camera", Managed="Com.Eov.Watermeter.Camera")]
[assembly:global::Android.Runtime.NamespaceMapping (Java = "com.eov.watermeter.license", Managed="Com.Eov.Watermeter.License")]
[assembly:global::Android.Runtime.NamespaceMapping (Java = "com.eov.watermeter.model", Managed="Com.Eov.Watermeter.Model")]
[assembly:global::Android.Runtime.NamespaceMapping (Java = "com.eov.watermeter.ocr", Managed="Com.Eov.Watermeter.Ocr")]
[assembly:global::Android.Runtime.NamespaceMapping (Java = "com.eov.watermeter.training", Managed="Com.Eov.Watermeter.Training")]
[assembly:global::Android.Runtime.NamespaceMapping (Java = "com.eov.watermeter.ui", Managed="Com.Eov.Watermeter.UI")]
[assembly:global::Android.Runtime.NamespaceMapping (Java = "com.eov.watermeter.util", Managed="Com.Eov.Watermeter.Util")]
[assembly:global::Android.Runtime.NamespaceMapping (Java = "com.eov.watermeter.views", Managed="Com.Eov.Watermeter.Views")]

delegate float _JniMarshal_PP_F (IntPtr jnienv, IntPtr klass);
delegate int _JniMarshal_PP_I (IntPtr jnienv, IntPtr klass);
delegate long _JniMarshal_PP_J (IntPtr jnienv, IntPtr klass);
delegate IntPtr _JniMarshal_PP_L (IntPtr jnienv, IntPtr klass);
delegate void _JniMarshal_PP_V (IntPtr jnienv, IntPtr klass);
delegate bool _JniMarshal_PP_Z (IntPtr jnienv, IntPtr klass);
delegate void _JniMarshal_PPF_V (IntPtr jnienv, IntPtr klass, float p0);
delegate void _JniMarshal_PPFF_V (IntPtr jnienv, IntPtr klass, float p0, float p1);
delegate IntPtr _JniMarshal_PPI_L (IntPtr jnienv, IntPtr klass, int p0);
delegate void _JniMarshal_PPI_V (IntPtr jnienv, IntPtr klass, int p0);
delegate IntPtr _JniMarshal_PPII_L (IntPtr jnienv, IntPtr klass, int p0, int p1);
delegate void _JniMarshal_PPII_V (IntPtr jnienv, IntPtr klass, int p0, int p1);
delegate bool _JniMarshal_PPIII_Z (IntPtr jnienv, IntPtr klass, int p0, int p1, int p2);
delegate void _JniMarshal_PPJ_V (IntPtr jnienv, IntPtr klass, long p0);
delegate IntPtr _JniMarshal_PPJLIIII_L (IntPtr jnienv, IntPtr klass, long p0, IntPtr p1, int p2, int p3, int p4, int p5);
delegate IntPtr _JniMarshal_PPL_L (IntPtr jnienv, IntPtr klass, IntPtr p0);
delegate void _JniMarshal_PPL_V (IntPtr jnienv, IntPtr klass, IntPtr p0);
delegate bool _JniMarshal_PPL_Z (IntPtr jnienv, IntPtr klass, IntPtr p0);
delegate void _JniMarshal_PPLFL_V (IntPtr jnienv, IntPtr klass, IntPtr p0, float p1, IntPtr p2);
delegate IntPtr _JniMarshal_PPLIIII_L (IntPtr jnienv, IntPtr klass, IntPtr p0, int p1, int p2, int p3, int p4);
delegate bool _JniMarshal_PPLL_Z (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1);
delegate bool _JniMarshal_PPLLIIL_Z (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, int p2, int p3, IntPtr p4);
delegate void _JniMarshal_PPLLL_V (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, IntPtr p2);
delegate long _JniMarshal_PPLLLIIL_J (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, IntPtr p2, int p3, int p4, IntPtr p5);
delegate bool _JniMarshal_PPLLLIIL_Z (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, IntPtr p2, int p3, int p4, IntPtr p5);
delegate bool _JniMarshal_PPLLLIILIF_Z (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, IntPtr p2, int p3, int p4, IntPtr p5, int p6, float p7);
delegate void _JniMarshal_PPLLLLL_V (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4);
delegate IntPtr _JniMarshal_PPZ_L (IntPtr jnienv, IntPtr klass, bool p0);
delegate void _JniMarshal_PPZ_V (IntPtr jnienv, IntPtr klass, bool p0);
delegate void _JniMarshal_PPZF_V (IntPtr jnienv, IntPtr klass, bool p0, float p1);
delegate void _JniMarshal_PPZII_V (IntPtr jnienv, IntPtr klass, bool p0, int p1, int p2);
#if !NET
namespace System.Runtime.Versioning {
    [System.Diagnostics.Conditional("NEVER")]
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Enum | AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Module | AttributeTargets.Property | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
    internal sealed class SupportedOSPlatformAttribute : Attribute {
        public SupportedOSPlatformAttribute (string platformName) { }
    }
}
#endif

