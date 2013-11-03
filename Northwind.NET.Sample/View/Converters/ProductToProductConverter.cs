using Northwind.NET.EF6Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Northwind.NET.Sample.View {
    public class ProductToProductConverter:IValueConverter {


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                object result= (Product)value;
                return result;
            }
            catch (Exception ex) {
                App.LogException(new ApplicationException("ProductToIDStringConverter.Convert raise exception.",ex));
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null || !(value is Product))
                return DependencyProperty.UnsetValue;
                return value;
        }
    }
}
