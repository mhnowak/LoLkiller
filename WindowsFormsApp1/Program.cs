using System;

using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            LoLKillHook hook = new LoLKillHook();
            Form form = new Form1();
            Application.Run();
            form.Hide();
        }
    }
}
