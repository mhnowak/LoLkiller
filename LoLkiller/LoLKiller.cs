using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace LoLkiller
{
    class LoLKiller
    {
		const String leagueWindowName = "League of Legends (TM) Client";
		const String leagueOfLegendsProcessName = "League of Legends.exe";

		public void Kill()
		{
			if (GetActiveWindowTitle() == leagueWindowName)
			{
				var info = new ProcessStartInfo
				{
					FileName = "CMD.exe",
					Arguments = $"/C wmic process where name='{leagueOfLegendsProcessName}' delete",
					CreateNoWindow = true,
					UseShellExecute = false
				};
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
	}
}
