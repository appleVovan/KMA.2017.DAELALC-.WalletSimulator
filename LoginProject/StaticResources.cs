﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginProject
{
    static class StaticResources
    {
        private static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        internal static readonly string ClientDirPath = Path.Combine(AppData, "WalletSimulator");
        internal static readonly string ClientLogDirPath = Path.Combine(ClientDirPath, "Logs");
    }
}
