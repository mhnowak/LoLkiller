using System;
using System.Runtime.InteropServices;

namespace LoLkiller
{
	class LoLKillHook
	{
		public delegate int keyboardHookProc(int code, int wParam, ref keyboardHook lParam);

		public struct keyboardHook
		{
			public int vkCode;
			public int scanCode;
			public int flags;
			public int time;
			public int dwExtraInfo;
		}

		const int WH_KEYBOARD_LL = 13;
		const int WM_KEYDOWN = 0x100;
		const int WM_KEYUP = 0x101;
		const int WM_SYSKEYDOWN = 0x104;
		const int WM_SYSKEYUP = 0x105;

		const int ALT = 164;
		const int F4 = 115;

		IntPtr hhook = IntPtr.Zero;

		bool isAltPressed = false;
		bool isF4Pressed = false;

		public LoLKillHook()
		{
			hook();
		}

		~LoLKillHook()
		{
			unhook();
		}

		public void hook()
		{
			IntPtr hInstance = LoadLibrary("User32");
			hhook = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, hInstance, 0);
		}

		public void unhook()
		{
			UnhookWindowsHookEx(hhook);
		}

		public int hookProc(int code, int wParam, ref keyboardHook lParam)
		{
			if (code >= 0)
			{
				int vkCode = lParam.vkCode;
				if (vkCode == ALT)
				{
					if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
					{
						if (isF4Pressed) _handleAltF4();
						isAltPressed = true;
					}
					else if (wParam == WM_KEYUP || wParam == WM_SYSKEYUP)
					{
						isAltPressed = false;
					}
				}
				else if (vkCode == F4)
				{
					if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
					{
						if (isAltPressed) _handleAltF4();
						isF4Pressed = true;
					}
					else if (wParam == WM_KEYUP || wParam == WM_SYSKEYUP)
					{
						isF4Pressed = false;
					}
				}
			}
			return CallNextHookEx(hhook, code, wParam, ref lParam);
		}

		private void _handleAltF4()
		{
			// Finds the game process to kill
			System.Diagnostics.Process.Start("CMD.exe", "/C wmic process where name='League of Legends.exe' delete");
		}

		[DllImport("user32.dll")]
		static extern IntPtr SetWindowsHookEx(int idHook, keyboardHookProc callback, IntPtr hInstance, uint threadId);

		[DllImport("user32.dll")]
		static extern bool UnhookWindowsHookEx(IntPtr hInstance);

		[DllImport("user32.dll")]
		static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref keyboardHook lParam);

		[DllImport("kernel32.dll")]
		static extern IntPtr LoadLibrary(string lpFileName);
	}
}
