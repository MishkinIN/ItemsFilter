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
        public object? Convert(object? value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                if (value is string str) {
                    string picName = (str.Split('.'))[0];
                    var uri = new System.Uri($"../Resources/{picName}.gif", UriKind.Relative);
                    var image = new BitmapImage(uri);
                    return uri;
                }
            }
            catch (Exception) {
            }
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
