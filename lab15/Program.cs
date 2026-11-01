using System;
using System.Windows.Forms;
using StudentMvp.Views;

namespace StudentMvp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmStudent());
        }
    }
}
