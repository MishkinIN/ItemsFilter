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
using System.Reflection;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Northwind.NET.Sample.View {
    public class TextToImageSourceConverter : IValueConverter {
        private static Lazy<string> lz_callingAssemblyName = new Lazy<string>(() => {
            var assemblyName = Assembly.GetCallingAssembly().FullName;
            if (assemblyName != null) {
                return assemblyName.Split(',')[0];
            }
            else
                return string.Empty;
        });
        public object? Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                string picName = (((string)value).Split('.'))[0];
                return new BitmapImage(
                    new System.Uri($"/{lz_callingAssemblyName.Value};component/Resources/{picName}.gif", System.UriKind.Relative));
            }
            catch (Exception) {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
