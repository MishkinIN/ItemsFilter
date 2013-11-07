using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Northwind.NET.Sample.View {
    class NullToUnsetValueConverter:IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (Object.ReferenceEquals(null, value))
                return DependencyProperty.UnsetValue;
            else
                return value;
        }
    }
}
