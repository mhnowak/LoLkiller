using System.Windows.Forms;

namespace LoLkiller
{
    static class Program
    {
        static void Main()
        {
            LoLKillHook hook = new LoLKillHook();
            Form form = new Form1();
            Application.Run();
            form.Hide();
        }
    }
}
