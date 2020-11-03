using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace LoLkiller
{
	class LoLKillHook
	{
		public delegate int keyboardHookProc(int code, int wParam, ref KeyboardHook lParam);

		public struct KeyboardHook
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
		const String leagueWindowName = "League of Legends (TM) Client";
		const String leagueOfLegendsProcessName = "League of Legends.exe";

		IntPtr hhook = IntPtr.Zero;

		bool isAltPressed = false;
		bool isF4Pressed = false;

		public LoLKillHook()
		{
			Hook();
		}

		~LoLKillHook()
		{
			UnHook();
		}

		public void Hook()
		{
			IntPtr hInstance = LoadLibrary("User32");
			hhook = SetWindowsHookEx(WH_KEYBOARD_LL, HookProc, hInstance, 0);
		}

		public void UnHook()
		{
			UnhookWindowsHookEx(hhook);
		}

		public int HookProc(int code, int wParam, ref KeyboardHook lParam)
		{
			if (code >= 0)
			{
				int vkCode = lParam.vkCode;
				if (vkCode == ALT)
				{
					if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
					{
						if (isF4Pressed) KillLeagueOfLegends();
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
						if (isAltPressed) KillLeagueOfLegends();
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

		private void KillLeagueOfLegends()
		{
			if (GetActiveWindowTitle() == leagueWindowName)
            {
				var info = new ProcessStartInfo();
				info.FileName = "CMD.exe";
				info.Arguments = $"/C wmic process where name='{leagueOfLegendsProcessName}' delete";
				info.CreateNoWindow = true;
				info.UseShellExecute = false; 
				Process.Start(info);
			}
		}

		private string GetActiveWindowTitle()
		{
			const int nChars = 256;
			StringBuilder Buff = new StringBuilder(nChars);
			IntPtr handle = GetForegroundWindow();

			if (GetWindowText(handle, Buff, nChars) > 0)
			{
				return Buff.ToString();
			}
			return null;
		}

		[DllImport("user32.dll")]
		static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

		[DllImport("user32.dll")]
		static extern IntPtr SetWindowsHookEx(int idHook, keyboardHookProc callback, IntPtr hInstance, uint threadId);

		[DllImport("user32.dll")]
		static extern bool UnhookWindowsHookEx(IntPtr hInstance);

		[DllImport("user32.dll")]
		static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref KeyboardHook lParam);

		[DllImport("kernel32.dll")]
		static extern IntPtr LoadLibrary(string lpFileName);
	}
}
