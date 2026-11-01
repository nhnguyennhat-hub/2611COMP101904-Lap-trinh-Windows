using System;
using System.Windows.Forms;
using EduManager.GUI;

namespace EduManager
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmMonHoc());
        }
    }
}
