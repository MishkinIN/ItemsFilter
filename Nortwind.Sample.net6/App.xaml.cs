using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Northwind.NET.Sample {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        static readonly string assemblyShortName;
        static App() {
            Assembly a = typeof(App).Assembly;
            assemblyShortName = a.ToString().Split(',')[0];
        }
        internal static void LogException(Exception ex) {
            int identLvl = Debug.IndentLevel;
            do {
                Debug.WriteLine(ex.Message);
                Debug.Indent();
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
                ex = ex.InnerException;
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
            } while (ex != null);
            Debug.IndentLevel = identLvl;
            return;
        }
        public static Uri MakePackUri(string relativeFile) {
            return new Uri($"pack://application:,,,/{assemblyShortName};component/{relativeFile}");
        }
    }
}
