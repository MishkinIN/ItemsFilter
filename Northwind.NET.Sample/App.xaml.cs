// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.Diagnostics;
using System.Reflection;
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
        public static Uri MakePackUri(string relativeFile) {
            string uriString = $"pack://application:,,,/{assemblyShortName};component/{relativeFile}";
            return new Uri(uriString);
        }

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
