// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BolapanControl.ItemsFilter.View {
    /// <summary>
    /// Provide IValueConverter for common ValueTypes.
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(String))]
    [ValueConversion(typeof(String), typeof(String))]
    [ValueConversion(typeof(int), typeof(String))]
    [ValueConversion(typeof(Int64), typeof(String))]
    [ValueConversion(typeof(long), typeof(String))]
    [ValueConversion(typeof(double), typeof(String))]
    [ValueConversion(typeof(Enum), typeof(String))]
    [ValueConversion(typeof(bool), typeof(String))]
    public class SimplePropertyConverter : IValueConverter {
        private static readonly Lazy<SimplePropertyConverter> lzDefault = new(() => new SimplePropertyConverter());
        public static SimplePropertyConverter Default { get => lzDefault.Value; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            TypeConverter converter = TypeDescriptor.GetConverter(targetType);
            try {
                if (converter.CanConvertTo(null, typeof(string))) {
                    //return converter.ConvertTo(value, typeof(string));
                    return converter.ConvertTo(null, culture, value, typeof(string))?? String.Empty;
                }
                else {
                    return value.ToString()?? String.Empty;
                }
            }
            catch (Exception) {
                return value;
            }
        }

        public object? ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var valueType = value?.GetType();
            if (valueType==null) {
                return value;
            }
            if (valueType==targetType) {
                return value;
            }
            TypeConverter converter = TypeDescriptor.GetConverter(targetType);
            try {

                if (converter.CanConvertFrom(null, valueType)) {
                    return converter.ConvertFrom(null, culture, value) ?? String.Empty;
                }
                else {
                    return converter.ConvertFrom(value.ToString()?? String.Empty) ?? String.Empty;
                }
            }
            catch (Exception) {
                return value;
            }
        }
    }
}
