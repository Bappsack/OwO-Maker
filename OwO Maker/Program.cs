using System;
using System.Windows.Forms;

namespace OwO_Maker
{
    static class Program
    {
        public static bool botRunning { get; set; }
        public static Form1 form { get; set; }
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();

            Application.Run(form);
        }
    }
}
