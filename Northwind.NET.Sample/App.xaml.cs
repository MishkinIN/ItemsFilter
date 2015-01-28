// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.Diagnostics;
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
