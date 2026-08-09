using My_Project.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace My_Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            KryptonManager manager = new KryptonManager();
            manager.GlobalPaletteMode = PaletteMode.Office2010Blue;

            Application.Run(new LoginScreen());
        }
    }
}
