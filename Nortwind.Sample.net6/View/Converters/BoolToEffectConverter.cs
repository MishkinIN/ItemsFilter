// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using Shazzam.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace Northwind.NET.Sample.View {
    public class BoolToEffectConverter : IMultiValueConverter {
        static readonly MonochromeEffect effect = new MonochromeEffect();
        public object? Convert(object[]? values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (values!=null && values.Length >= 2 && values[0] is bool isSelected && values[1] is bool isFilterActive) {
                if (isFilterActive & (!isSelected))
                    return effect;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            return new object[]{false,false};
        }
    }
}
