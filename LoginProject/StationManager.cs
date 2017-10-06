using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoginProject
{
    public static class StationManager
    {
        public static User CurrentUser { get; set; }

        internal static void ShutDown(int exitCode)
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(exitCode);
        }
    }
}
