using Shazzam.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace Northwind.NET.Sample.View {
    public class BoolToEffectConverter : IMultiValueConverter {
        static MonochromeEffect effect = new MonochromeEffect();
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (values.Length >= 2 && values[0] is bool && values[1] is bool) {
                bool isSelected = (bool)values[0];
                bool isFilterActive = (bool)values[1];
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
