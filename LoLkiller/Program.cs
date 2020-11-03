using System.Threading;
using System.Windows.Forms;

namespace LoLkiller
{
    static class Program
    {
        private static Mutex mutex = null;

        static void Main()
        {
            mutex = new Mutex(true, Application.ProductName, out bool createdNew);

            if (!createdNew) return;

            new LoLKillHook();
            Form form = new Form1();
            Application.Run();
            form.Hide();
        }
    }
}
