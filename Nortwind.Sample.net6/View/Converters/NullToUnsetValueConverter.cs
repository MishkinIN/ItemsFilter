// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Northwind.NET.Sample.View {
    public class NullToUnsetValueConverter : IValueConverter {
        public object? Convert(object? value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value is null)
                return string.Empty;
            else
                return value;
        }

        public object ConvertBack(object? value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value is string s && s == String.Empty) {
                return null;
            }
            return value;
        }
    }
}
