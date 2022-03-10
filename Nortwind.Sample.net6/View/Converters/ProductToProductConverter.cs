// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using Northwind.NET.EF6Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Northwind.NET.Sample.View {
    public class ProductToProductConverter : IValueConverter {


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value is Product product)
                return product;
            return new Product();
        }

        public object ConvertBack(object? value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null || value is not Product)
                return DependencyProperty.UnsetValue;
            return value;
        }
    }
}
