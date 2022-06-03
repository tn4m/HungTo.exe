using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Utilities
{
	// Token: 0x02000003 RID: 3
	internal class GlobalKeyboardHook
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000004 RID: 4 RVA: 0x00002088 File Offset: 0x00000288
		// (remove) Token: 0x06000005 RID: 5 RVA: 0x000020C0 File Offset: 0x000002C0
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event KeyEventHandler KeyDown;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000006 RID: 6 RVA: 0x000020F8 File Offset: 0x000002F8
		// (remove) Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event KeyEventHandler KeyUp;

		// Token: 0x06000008 RID: 8 RVA: 0x00002165 File Offset: 0x00000365
		public GlobalKeyboardHook()
		{
			this.hook();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000218C File Offset: 0x0000038C
		~GlobalKeyboardHook()
		{
			this.unhook();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021BC File Offset: 0x000003BC
		public void hook()
		{
			IntPtr hInstance = GlobalKeyboardHook.LoadLibrary("User32");
			this.hhook = GlobalKeyboardHook.SetWindowsHookEx(13, new GlobalKeyboardHook.keyboardHookProc(this.hookProc), hInstance, 0U);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021F0 File Offset: 0x000003F0
		public void unhook()
		{
			GlobalKeyboardHook.UnhookWindowsHookEx(this.hhook);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002200 File Offset: 0x00000400
		public int hookProc(int code, int wParam, ref GlobalKeyboardHook.keyboardHookStruct lParam)
		{
			bool flag = code >= 0;
			if (flag)
			{
				Keys vkCode = (Keys)lParam.vkCode;
				bool flag2 = this.HookedKeys.Contains(vkCode);
				if (flag2)
				{
					KeyEventArgs keyEventArgs = new KeyEventArgs(vkCode);
					bool flag3 = (wParam == 256 || wParam == 260) && this.KeyDown != null;
					if (flag3)
					{
						this.KeyDown(this, keyEventArgs);
					}
					else
					{
						bool flag4 = (wParam == 257 || wParam == 261) && this.KeyUp != null;
						if (flag4)
						{
							this.KeyUp(this, keyEventArgs);
						}
					}
					bool handled = keyEventArgs.Handled;
					if (handled)
					{
						return 1;
					}
				}
			}
			return GlobalKeyboardHook.CallNextHookEx(this.hhook, code, wParam, ref lParam);
		}

		// Token: 0x0600000D RID: 13
		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowsHookEx(int idHook, GlobalKeyboardHook.keyboardHookProc callback, IntPtr hInstance, uint threadId);

		// Token: 0x0600000E RID: 14
		[DllImport("user32.dll")]
		private static extern bool UnhookWindowsHookEx(IntPtr hInstance);

		// Token: 0x0600000F RID: 15
		[DllImport("user32.dll")]
		private static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref GlobalKeyboardHook.keyboardHookStruct lParam);

		// Token: 0x06000010 RID: 16
		[DllImport("kernel32.dll")]
		private static extern IntPtr LoadLibrary(string lpFileName);

		// Token: 0x04000002 RID: 2
		private const int WH_KEYBOARD_LL = 13;

		// Token: 0x04000003 RID: 3
		private const int WM_KEYDOWN = 256;

		// Token: 0x04000004 RID: 4
		private const int WM_KEYUP = 257;

		// Token: 0x04000005 RID: 5
		private const int WM_SYSKEYDOWN = 260;

		// Token: 0x04000006 RID: 6
		private const int WM_SYSKEYUP = 261;

		// Token: 0x04000007 RID: 7
		public List<Keys> HookedKeys = new List<Keys>();

		// Token: 0x04000008 RID: 8
		private IntPtr hhook = IntPtr.Zero;

		// Token: 0x02000007 RID: 7
		// (Invoke) Token: 0x06000028 RID: 40
		public delegate int keyboardHookProc(int code, int wParam, ref GlobalKeyboardHook.keyboardHookStruct lParam);

		// Token: 0x02000008 RID: 8
		public struct keyboardHookStruct
		{
			// Token: 0x04000011 RID: 17
			public int vkCode;

			// Token: 0x04000012 RID: 18
			public int scanCode;

			// Token: 0x04000013 RID: 19
			public int flags;

			// Token: 0x04000014 RID: 20
			public int time;

			// Token: 0x04000015 RID: 21
			public int dwExtraInfo;
		}
	}
}
