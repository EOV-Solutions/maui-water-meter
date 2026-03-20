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
namespace ObjCRuntime {
	[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
	static partial class Trampolines {
		[UnmanagedFunctionPointerAttribute (CallingConvention.Cdecl)]
		[UserDelegateType (typeof (global::System.Action<bool>))]
		unsafe internal delegate void DActionArity1V0 (IntPtr block, byte obj);
		//
		// This class bridges native block invocations that call into C#
		//
		static internal class SDActionArity1V0 {
			[Preserve (Conditional = true)]
			[UnmanagedCallersOnly]
			[UserDelegateType (typeof (global::System.Action<bool>))]
			internal static unsafe void Invoke (IntPtr block, byte obj) {
				var del = BlockLiteral.GetTarget<global::System.Action<bool>> (block);
				if (del is not null) {
					del (obj != 0);
				}
			}
			internal static unsafe BlockLiteral CreateNullableBlock (global::System.Action<bool>? callback)
			{
				if (callback is null)
					return default (BlockLiteral);
				return CreateBlock (callback);
			}
			[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
			internal static unsafe BlockLiteral CreateBlock (global::System.Action<bool> callback)
			{
				delegate* unmanaged<IntPtr, byte, void> trampoline = &Invoke;
				return new BlockLiteral (trampoline, callback, typeof (SDActionArity1V0), nameof (Invoke));
			}
		} /* class SDActionArity1V0 */
		internal sealed class NIDActionArity1V0 : TrampolineBlockBase {
			DActionArity1V0 invoker;
			[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
			public unsafe NIDActionArity1V0 (BlockLiteral *block) : base (block)
			{
				invoker = block->GetDelegateForBlock<DActionArity1V0> ();
			}
			[Preserve (Conditional=true)]
			[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
			public unsafe static global::System.Action<bool>? Create (IntPtr block)
			{
				if (block == IntPtr.Zero)
					return null;
				var del = (global::System.Action<bool>) GetExistingManagedDelegate (block);
				return del ?? new NIDActionArity1V0 ((BlockLiteral *) block).Invoke;
			}
			[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
			unsafe void Invoke (bool obj)
			{
				invoker (BlockPointer, obj ? (byte) 1 : (byte) 0);
			}
		} /* class NIDActionArity1V0 */
		[UnmanagedFunctionPointerAttribute (CallingConvention.Cdecl)]
		[UserDelegateType (typeof (global::System.Action<bool, string>))]
		unsafe internal delegate void DActionArity2V0 (IntPtr block, byte arg1, NativeHandle arg2);
		//
		// This class bridges native block invocations that call into C#
		//
		static internal class SDActionArity2V0 {
			[Preserve (Conditional = true)]
			[UnmanagedCallersOnly]
			[UserDelegateType (typeof (global::System.Action<bool, string>))]
			internal static unsafe void Invoke (IntPtr block, byte arg1, NativeHandle arg2) {
				var del = BlockLiteral.GetTarget<global::System.Action<bool, string>> (block);
				if (del is not null) {
					del (arg1 != 0, CFString.FromHandle (arg2)!);
				}
			}
			internal static unsafe BlockLiteral CreateNullableBlock (global::System.Action<bool, string>? callback)
			{
				if (callback is null)
					return default (BlockLiteral);
				return CreateBlock (callback);
			}
			[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
			internal static unsafe BlockLiteral CreateBlock (global::System.Action<bool, string> callback)
			{
				delegate* unmanaged<IntPtr, byte, NativeHandle, void> trampoline = &Invoke;
				return new BlockLiteral (trampoline, callback, typeof (SDActionArity2V0), nameof (Invoke));
			}
		} /* class SDActionArity2V0 */
		internal sealed class NIDActionArity2V0 : TrampolineBlockBase {
			DActionArity2V0 invoker;
			[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
			public unsafe NIDActionArity2V0 (BlockLiteral *block) : base (block)
			{
				invoker = block->GetDelegateForBlock<DActionArity2V0> ();
			}
			[Preserve (Conditional=true)]
			[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
			public unsafe static global::System.Action<bool, string>? Create (IntPtr block)
			{
				if (block == IntPtr.Zero)
					return null;
				var del = (global::System.Action<bool, string>) GetExistingManagedDelegate (block);
				return del ?? new NIDActionArity2V0 ((BlockLiteral *) block).Invoke;
			}
			[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
			unsafe void Invoke (bool arg1, string arg2)
			{
				if (arg2 is null)
					ObjCRuntime.ThrowHelper.ThrowArgumentNullException (nameof (arg2));
				var nsarg2 = CFString.CreateNative (arg2);
				invoker (BlockPointer, arg1 ? (byte) 1 : (byte) 0, nsarg2);
				CFString.ReleaseNative (nsarg2);
			}
		} /* class NIDActionArity2V0 */
	}
}
