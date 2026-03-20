//
// Auto-generated from generator.cs, do not edit
//
// We keep references to objects, so warning 414 is expected
#pragma warning disable 414
using System;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.Versioning;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using UIKit;
using GLKit;
using Metal;
using CoreML;
using MapKit;
using Photos;
using ModelIO;
using Network;
using SceneKit;
using Contacts;
using Security;
using Messages;
using AudioUnit;
using CoreVideo;
using CoreMedia;
using QuickLook;
using CoreImage;
using SpriteKit;
using Foundation;
using CoreMotion;
using ObjCRuntime;
using AddressBook;
using MediaPlayer;
using GameplayKit;
using CoreGraphics;
using CoreLocation;
using AVFoundation;
using NewsstandKit;
using FileProvider;
using CoreAnimation;
using CoreFoundation;
using NetworkExtension;
using MetalPerformanceShadersGraph;
#nullable enable
#if !NET
using NativeHandle = System.IntPtr;
#endif
namespace WaterMeter.Maui.Platforms.iOS {
	[Protocol (Name = "_TtP13WaterMeterSDK28WMCameraScannerDelegate_ObjC_", WrapperType = typeof (WMCameraScannerDelegateObjCWrapper))]
	[ProtocolMember (IsRequired = true, IsProperty = false, IsStatic = false, Name = "Scanner", Selector = "scanner:didScanResult:", ParameterType = new Type [] { typeof (WaterMeter.Maui.Platforms.iOS.WMCameraScanner), typeof (WaterMeter.Maui.Platforms.iOS.WMScanResultObjC) }, ParameterByRef = new bool [] { false, false })]
	[ProtocolMember (IsRequired = false, IsProperty = false, IsStatic = false, Name = "ScannerDidFail", Selector = "scanner:didFailWithError:", ParameterType = new Type [] { typeof (WaterMeter.Maui.Platforms.iOS.WMCameraScanner), typeof (NSError) }, ParameterByRef = new bool [] { false, false })]
	[ProtocolMember (IsRequired = false, IsProperty = false, IsStatic = false, Name = "ScannerDidCancel", Selector = "scannerDidCancel:", ParameterType = new Type [] { typeof (WaterMeter.Maui.Platforms.iOS.WMCameraScanner) }, ParameterByRef = new bool [] { false })]
	public partial interface IWMCameraScannerDelegateObjC : INativeObject, IDisposable
	{
		[global::Foundation.RequiredMember]
		[Export ("scanner:didScanResult:")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void Scanner (WMCameraScanner scanner, WMScanResultObjC result)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		internal static void _Scanner (IWMCameraScannerDelegateObjC This, WMCameraScanner scanner, WMScanResultObjC result)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
		[global::Foundation.OptionalMember]
		[Export ("scanner:didFailWithError:")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void ScannerDidFail (WMCameraScanner scanner, NSError error)
		{
			_ScannerDidFail (this, scanner, error);
		}
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		internal static void _ScannerDidFail (IWMCameraScannerDelegateObjC This, WMCameraScanner scanner, NSError error)
		{
			var scanner__handle__ = scanner!.GetNonNullHandle (nameof (scanner));
			var error__handle__ = error!.GetNonNullHandle (nameof (error));
			global::ApiDefinition.Messaging.void_objc_msgSend_NativeHandle_NativeHandle (This.Handle, Selector.GetHandle ("scanner:didFailWithError:"), scanner__handle__, error__handle__);
		}
		[global::Foundation.OptionalMember]
		[Export ("scannerDidCancel:")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void ScannerDidCancel (WMCameraScanner scanner)
		{
			_ScannerDidCancel (this, scanner);
		}
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		internal static void _ScannerDidCancel (IWMCameraScannerDelegateObjC This, WMCameraScanner scanner)
		{
			var scanner__handle__ = scanner!.GetNonNullHandle (nameof (scanner));
			global::ApiDefinition.Messaging.void_objc_msgSend_NativeHandle (This.Handle, Selector.GetHandle ("scannerDidCancel:"), scanner__handle__);
		}
		[DynamicDependencyAttribute ("Scanner(WaterMeter.Maui.Platforms.iOS.WMCameraScanner,WaterMeter.Maui.Platforms.iOS.WMScanResultObjC)")]
		[DynamicDependencyAttribute ("ScannerDidCancel(WaterMeter.Maui.Platforms.iOS.WMCameraScanner)")]
		[DynamicDependencyAttribute ("ScannerDidFail(WaterMeter.Maui.Platforms.iOS.WMCameraScanner,Foundation.NSError)")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		static IWMCameraScannerDelegateObjC ()
		{
			GC.KeepAlive (null);
		}
	}
	/// <summary>Extension methods to the <see cref="IWMCameraScannerDelegateObjC" /> interface to support all the methods from the _TtP13WaterMeterSDK28WMCameraScannerDelegate_ObjC_ protocol.</summary>
	/// <remarks>
	///   <para>The extension methods for <see cref="IWMCameraScannerDelegateObjC" /> interface allow developers to treat instances of the interface as having all the optional methods of the original _TtP13WaterMeterSDK28WMCameraScannerDelegate_ObjC_ protocol. Since the interface only contains the required members, these extension methods allow developers to call the optional members of the protocol.</para>
	/// </remarks>
	public unsafe static partial class WMCameraScannerDelegateObjC_Extensions {
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static void ScannerDidFail (this IWMCameraScannerDelegateObjC This, WMCameraScanner scanner, NSError error)
		{
			var scanner__handle__ = scanner!.GetNonNullHandle (nameof (scanner));
			var error__handle__ = error!.GetNonNullHandle (nameof (error));
			global::ApiDefinition.Messaging.void_objc_msgSend_NativeHandle_NativeHandle (This.Handle, Selector.GetHandle ("scanner:didFailWithError:"), scanner__handle__, error__handle__);
		}
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static void ScannerDidCancel (this IWMCameraScannerDelegateObjC This, WMCameraScanner scanner)
		{
			var scanner__handle__ = scanner!.GetNonNullHandle (nameof (scanner));
			global::ApiDefinition.Messaging.void_objc_msgSend_NativeHandle (This.Handle, Selector.GetHandle ("scannerDidCancel:"), scanner__handle__);
		}
	}
	internal unsafe sealed class WMCameraScannerDelegateObjCWrapper : BaseWrapper, IWMCameraScannerDelegateObjC {
		[Preserve (Conditional = true)]
		public WMCameraScannerDelegateObjCWrapper (NativeHandle handle, bool owns)
			: base (handle, owns)
		{
		}
		[Export ("scanner:didScanResult:")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public void Scanner (WMCameraScanner scanner, WMScanResultObjC result)
		{
			var scanner__handle__ = scanner!.GetNonNullHandle (nameof (scanner));
			var result__handle__ = result!.GetNonNullHandle (nameof (result));
			global::ApiDefinition.Messaging.void_objc_msgSend_NativeHandle_NativeHandle (this.Handle, Selector.GetHandle ("scanner:didScanResult:"), scanner__handle__, result__handle__);
		}
	}
}
namespace WaterMeter.Maui.Platforms.iOS {
	[Protocol(Name = "_TtP13WaterMeterSDK28WMCameraScannerDelegate_ObjC_")]
	[Register("ApiDefinition__WaterMeter_Maui_Platforms_iOS_WMCameraScannerDelegateObjC", false)]
	[Model]
	public unsafe abstract partial class WMCameraScannerDelegateObjC : NSObject, IWMCameraScannerDelegateObjC {
		/// <summary>Creates a new <see cref="WMCameraScannerDelegateObjC" /> with default values.</summary>
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("init")]
		protected WMCameraScannerDelegateObjC () : base (NSObjectFlag.Empty)
		{
			IsDirectBinding = false;
			InitializeHandle (global::ApiDefinition.Messaging.IntPtr_objc_msgSendSuper (this.SuperHandle, global::ObjCRuntime.Selector.GetHandle ("init")), "init");
		}

		/// <summary>Constructor to call on derived classes to skip initialization and merely allocate the object.</summary>
		/// <param name="t">Unused sentinel value, pass NSObjectFlag.Empty.</param>
		/// <remarks>
		///     <para>
		///         This constructor should be called by derived classes when they completely construct the object in managed code and merely want the runtime to allocate and initialize the <see cref="Foundation.NSObject" />.
		///         This is required to implement the two-step initialization process that Objective-C uses, the first step is to perform the object allocation, the second step is to initialize the object.
		///         When developers invoke this constructor, they take advantage of a direct path that goes all the way up to <see cref="Foundation.NSObject" /> to merely allocate the object's memory and bind the Objective-C and C# objects together.
		///         The actual initialization of the object is up to the developer.
		///     </para>
		///     <para>
		///         This constructor is typically used by the binding generator to allocate the object, but prevent the actual initialization to take place.
		///         Once the allocation has taken place, the constructor has to initialize the object.
		///         With constructors generated by the binding generator this means that it manually invokes one of the "init" methods to initialize the object.
		///     </para>
		///     <para>It is the developer's responsibility to completely initialize the object if they chain up using this constructor chain.</para>
		///     <para>
		///         In general, if the developer's constructor invokes the corresponding base implementation, then it should also call an Objective-C init method.
		///         If this is not the case, developers should instead chain to the proper constructor in their class.
		///     </para>
		///     <para>
		///         The argument value is ignored and merely ensures that the only code that is executed is the construction phase is the basic <see cref="Foundation.NSObject" /> allocation and runtime type registration.
		///         Typically the chaining would look like this:
		///     </para>
		///     <example>
		///             <code lang="csharp lang-csharp"><![CDATA[
		/// //
		/// // The NSObjectFlag constructor merely allocates the object and registers the C# class with the Objective-C runtime if necessary.
		/// // No actual initXxx method is invoked, that is done later in the constructor
		/// //
		/// // This is taken from the iOS SDK's source code for the UIView class:
		/// //
		/// [Export ("initWithFrame:")]
		/// public UIView (System.Drawing.RectangleF frame) : base (NSObjectFlag.Empty)
		/// {
		///     // Invoke the init method now.
		///     var initWithFrame = new Selector ("initWithFrame:").Handle;
		///     if (IsDirectBinding) {
		///         Handle = ObjCRuntime.Messaging.IntPtr_objc_msgSend_CGRect (this.Handle, initWithFrame, frame);
		///     } else {
		///         Handle = ObjCRuntime.Messaging.IntPtr_objc_msgSendSuper_CGRect (this.SuperHandle, initWithFrame, frame);
		///     }
		/// }
		/// ]]></code>
		///     </example>
		/// </remarks>
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		protected WMCameraScannerDelegateObjC (NSObjectFlag t) : base (t)
		{
			IsDirectBinding = false;
		}

		/// <summary>A constructor used when creating managed representations of unmanaged objects. Called by the runtime.</summary>
		/// <param name="handle">Pointer (handle) to the unmanaged object.</param>
		/// <remarks>
		///     <para>
		///         This constructor is invoked by the runtime infrastructure (<see cref="ObjCRuntime.Runtime.GetNSObject(System.IntPtr)" />) to create a new managed representation for a pointer to an unmanaged Objective-C object.
		///         Developers should not invoke this method directly, instead they should call <see cref="ObjCRuntime.Runtime.GetNSObject(System.IntPtr)" /> as it will prevent two instances of a managed object pointing to the same native object.
		///     </para>
		/// </remarks>
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		protected internal WMCameraScannerDelegateObjC (NativeHandle handle) : base (handle)
		{
			IsDirectBinding = false;
		}

		[Export ("scanner:didScanResult:")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void Scanner (WMCameraScanner scanner, WMScanResultObjC result)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
		[Export ("scannerDidCancel:")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void ScannerDidCancel (WMCameraScanner scanner)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
		[Export ("scanner:didFailWithError:")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public virtual void ScannerDidFail (WMCameraScanner scanner, NSError error)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
	} /* class WMCameraScannerDelegateObjC */
}
