using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Northwind.NET.Sample {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        internal static void LogException(Exception ex) {
            int identLvl = Debug.IndentLevel;
            do {
                Debug.WriteLine(ex.Message);
                Debug.Indent();
                ex = ex.InnerException;
            } while (ex != null);
            Debug.IndentLevel = identLvl;
            return;
        }
    }
}
