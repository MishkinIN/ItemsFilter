using Northwind.NET.EF6Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Northwind.NET.Sample.View {
    class ProductToNameConverter:IValueConverter {
        // Summary:
        //     Converts a value.
        //
        // Parameters:
        //   value:
        //     The value produced by the binding source.
        //
        //   targetType:
        //     The type of the binding target property.
        //
        //   parameter:
        //     The converter parameter to use.
        //
        //   culture:
        //     The culture to use in the converter.
        //
        // Returns:
        //     A converted value. If the method returns null, the valid null value is used.
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                string result = ((Product)value).Name;
                return result;
            }
            catch (Exception ex) {
                return null;
            }
        }

        //
        // Summary:
        //     Converts a value.
        //
        // Parameters:
        //   value:
        //     The value that is produced by the binding target.
        //
        //   targetType:
        //     The type to convert to.
        //
        //   parameter:
        //     The converter parameter to use.
        //
        //   culture:
        //     The culture to use in the converter.
        //
        // Returns:
        //     A converted value. If the method returns null, the valid null value is used.
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return DependencyProperty.UnsetValue;
        }
    }
}
