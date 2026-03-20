using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Eov.Watermeter.Camera {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.eov.watermeter.camera']/interface[@name='CameraCallback']"
	[Register ("com/eov/watermeter/camera/CameraCallback", "", "Com.Eov.Watermeter.Camera.ICameraCallbackInvoker")]
	public partial interface ICameraCallback : IJavaObject, IJavaPeerable {
		private static readonly JniPeerMembers _members = new XAPeerMembers ("com/eov/watermeter/camera/CameraCallback", typeof (ICameraCallback), isInterface: true);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.eov.watermeter.camera']/interface[@name='CameraCallback']/method[@name='onCameraClosed' and count(parameter)=0]"
		[Register ("onCameraClosed", "()V", "GetOnCameraClosedHandler:Com.Eov.Watermeter.Camera.ICameraCallbackInvoker, WaterMeter.Maui")]
		void OnCameraClosed ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.eov.watermeter.camera']/interface[@name='CameraCallback']/method[@name='onCameraError' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("onCameraError", "(Ljava/lang/String;)V", "GetOnCameraError_Ljava_lang_String_Handler:Com.Eov.Watermeter.Camera.ICameraCallbackInvoker, WaterMeter.Maui")]
		void OnCameraError (string? p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.eov.watermeter.camera']/interface[@name='CameraCallback']/method[@name='onCameraOpened' and count(parameter)=0]"
		[Register ("onCameraOpened", "()V", "GetOnCameraOpenedHandler:Com.Eov.Watermeter.Camera.ICameraCallbackInvoker, WaterMeter.Maui")]
		void OnCameraOpened ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.eov.watermeter.camera']/interface[@name='CameraCallback']/method[@name='onPhotoCaptured' and count(parameter)=1 and parameter[1][@type='android.graphics.Bitmap']]"
		[Register ("onPhotoCaptured", "(Landroid/graphics/Bitmap;)V", "GetOnPhotoCaptured_Landroid_graphics_Bitmap_Handler:Com.Eov.Watermeter.Camera.ICameraCallbackInvoker, WaterMeter.Maui")]
		void OnPhotoCaptured (global::Android.Graphics.Bitmap? p0);

		private static Delegate? cb_onPreviewFrame_Landroid_graphics_Bitmap_;
#pragma warning disable 0169
		private static Delegate GetOnPreviewFrame_Landroid_graphics_Bitmap_Handler ()
		{
			if (cb_onPreviewFrame_Landroid_graphics_Bitmap_ == null)
				cb_onPreviewFrame_Landroid_graphics_Bitmap_ = JNINativeWrapper.CreateDelegate (new _JniMarshal_PPL_V (n_OnPreviewFrame_Landroid_graphics_Bitmap_));
			return cb_onPreviewFrame_Landroid_graphics_Bitmap_;
		}

		private static void n_OnPreviewFrame_Landroid_graphics_Bitmap_ (IntPtr jnienv, IntPtr native__this, IntPtr native_bitmap)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Com.Eov.Watermeter.Camera.ICameraCallback> (jnienv, native__this, JniHandleOwnership.DoNotTransfer)!;
			var bitmap = global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (native_bitmap, JniHandleOwnership.DoNotTransfer);
			__this.OnPreviewFrame (bitmap);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.eov.watermeter.camera']/interface[@name='CameraCallback']/method[@name='onPreviewFrame' and count(parameter)=1 and parameter[1][@type='android.graphics.Bitmap']]"
		[Register ("onPreviewFrame", "(Landroid/graphics/Bitmap;)V", "GetOnPreviewFrame_Landroid_graphics_Bitmap_Handler:Com.Eov.Watermeter.Camera.ICameraCallback, WaterMeter.Maui")]
		virtual unsafe void OnPreviewFrame (global::Android.Graphics.Bitmap? bitmap)
		{
			const string __id = "onPreviewFrame.(Landroid/graphics/Bitmap;)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((bitmap == null) ? IntPtr.Zero : ((global::Java.Lang.Object) bitmap).Handle);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				global::System.GC.KeepAlive (bitmap);
			}
		}

	}

	[global::Android.Runtime.Register ("com/eov/watermeter/camera/CameraCallback", DoNotGenerateAcw=true)]
	internal partial class ICameraCallbackInvoker : global::Java.Lang.Object, ICameraCallback {
		static readonly JniPeerMembers _members = new XAPeerMembers ("com/eov/watermeter/camera/CameraCallback", typeof (ICameraCallbackInvoker));

		static IntPtr java_class_ref {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		[global::System.Diagnostics.DebuggerBrowsable (global::System.Diagnostics.DebuggerBrowsableState.Never)]
		[global::System.ComponentModel.EditorBrowsable (global::System.ComponentModel.EditorBrowsableState.Never)]
		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		IntPtr class_ref;

		public static ICameraCallback? GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<ICameraCallback> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException ($"Unable to convert instance of type '{JNIEnv.GetClassNameFromInstance (handle)}' to type 'com.eov.watermeter.camera.CameraCallback'.");
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public ICameraCallbackInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate? cb_onCameraClosed;
#pragma warning disable 0169
		static Delegate GetOnCameraClosedHandler ()
		{
			if (cb_onCameraClosed == null)
				cb_onCameraClosed = JNINativeWrapper.CreateDelegate (new _JniMarshal_PP_V (n_OnCameraClosed));
			return cb_onCameraClosed;
		}

		static void n_OnCameraClosed (IntPtr jnienv, IntPtr native__this)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Com.Eov.Watermeter.Camera.ICameraCallback> (jnienv, native__this, JniHandleOwnership.DoNotTransfer)!;
			__this.OnCameraClosed ();
		}
#pragma warning restore 0169

		IntPtr id_onCameraClosed;
		public unsafe void OnCameraClosed ()
		{
			if (id_onCameraClosed == IntPtr.Zero)
				id_onCameraClosed = JNIEnv.GetMethodID (class_ref, "onCameraClosed", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_onCameraClosed);
		}

		static Delegate? cb_onCameraError_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetOnCameraError_Ljava_lang_String_Handler ()
		{
			if (cb_onCameraError_Ljava_lang_String_ == null)
				cb_onCameraError_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate (new _JniMarshal_PPL_V (n_OnCameraError_Ljava_lang_String_));
			return cb_onCameraError_Ljava_lang_String_;
		}

		static void n_OnCameraError_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Com.Eov.Watermeter.Camera.ICameraCallback> (jnienv, native__this, JniHandleOwnership.DoNotTransfer)!;
			var p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.OnCameraError (p0);
		}
#pragma warning restore 0169

		IntPtr id_onCameraError_Ljava_lang_String_;
		public unsafe void OnCameraError (string? p0)
		{
			if (id_onCameraError_Ljava_lang_String_ == IntPtr.Zero)
				id_onCameraError_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "onCameraError", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString ((string?)p0);
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (native_p0);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_onCameraError_Ljava_lang_String_, __args);
			JNIEnv.DeleteLocalRef (native_p0);
		}

		static Delegate? cb_onCameraOpened;
#pragma warning disable 0169
		static Delegate GetOnCameraOpenedHandler ()
		{
			if (cb_onCameraOpened == null)
				cb_onCameraOpened = JNINativeWrapper.CreateDelegate (new _JniMarshal_PP_V (n_OnCameraOpened));
			return cb_onCameraOpened;
		}

		static void n_OnCameraOpened (IntPtr jnienv, IntPtr native__this)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Com.Eov.Watermeter.Camera.ICameraCallback> (jnienv, native__this, JniHandleOwnership.DoNotTransfer)!;
			__this.OnCameraOpened ();
		}
#pragma warning restore 0169

		IntPtr id_onCameraOpened;
		public unsafe void OnCameraOpened ()
		{
			if (id_onCameraOpened == IntPtr.Zero)
				id_onCameraOpened = JNIEnv.GetMethodID (class_ref, "onCameraOpened", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_onCameraOpened);
		}

		static Delegate? cb_onPhotoCaptured_Landroid_graphics_Bitmap_;
#pragma warning disable 0169
		static Delegate GetOnPhotoCaptured_Landroid_graphics_Bitmap_Handler ()
		{
			if (cb_onPhotoCaptured_Landroid_graphics_Bitmap_ == null)
				cb_onPhotoCaptured_Landroid_graphics_Bitmap_ = JNINativeWrapper.CreateDelegate (new _JniMarshal_PPL_V (n_OnPhotoCaptured_Landroid_graphics_Bitmap_));
			return cb_onPhotoCaptured_Landroid_graphics_Bitmap_;
		}

		static void n_OnPhotoCaptured_Landroid_graphics_Bitmap_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			var __this = global::Java.Lang.Object.GetObject<global::Com.Eov.Watermeter.Camera.ICameraCallback> (jnienv, native__this, JniHandleOwnership.DoNotTransfer)!;
			var p0 = global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.OnPhotoCaptured (p0);
		}
#pragma warning restore 0169

		IntPtr id_onPhotoCaptured_Landroid_graphics_Bitmap_;
		public unsafe void OnPhotoCaptured (global::Android.Graphics.Bitmap? p0)
		{
			if (id_onPhotoCaptured_Landroid_graphics_Bitmap_ == IntPtr.Zero)
				id_onPhotoCaptured_Landroid_graphics_Bitmap_ = JNIEnv.GetMethodID (class_ref, "onPhotoCaptured", "(Landroid/graphics/Bitmap;)V");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_onPhotoCaptured_Landroid_graphics_Bitmap_, __args);
		}

	}
}
