using NonInvasiveKeyboardHookLibrary;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace LoLkiller
{
    static class Program
    {

        static void Main()
        {
            string appGuid = ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value.ToString();

            string mutexId = string.Format("Global\\{{{0}}}", appGuid);

            bool createdNew;

            var allowEveryoneRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow);

            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);

            using (var mutex = new Mutex(false, mutexId, out createdNew, securitySettings))
            {
                var hasHandle = false;
                try
                {
                    try
                    {
                        hasHandle = mutex.WaitOne(5000, false);
                        if (hasHandle == false)
                            throw new TimeoutException("Timeout waiting for exclusive access");
                    }
                    catch (AbandonedMutexException)
                    {
                        hasHandle = true;
                    }

                    var keyboardHookManager = new KeyboardHookManager();
                    keyboardHookManager.Start();

                    var lolKiller = new LoLKiller();
                    keyboardHookManager.RegisterHotkey(ModifierKeys.Alt, 115, () =>
                    {
                        lolKiller.Kill();
                    });

                    Form form = new Form1();
                    Application.Run();
                    form.Hide();
                }
                finally
                {
                    if (hasHandle)
                        mutex.ReleaseMutex();
                }
            }
        }
    }
}
