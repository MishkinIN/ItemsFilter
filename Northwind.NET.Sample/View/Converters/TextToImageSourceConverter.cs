using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Northwind.NET.Sample.View {
    class TextToImageSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                string picName = (((string)value).Split('.'))[0];
                return new BitmapImage(
                    new System.Uri("/Northwind.NET.Sample;component/Resources/" + picName + ".gif", System.UriKind.Relative));
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
