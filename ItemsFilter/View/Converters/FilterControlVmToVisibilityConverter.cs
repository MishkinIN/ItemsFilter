// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace BolapanControl.ItemsFilter.View {
    [ValueConversion(typeof(System.Windows.Visibility), typeof(BolapanControl.ItemsFilter.ViewModel.FilterControlVm))]
    public class FilterControlVmToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value is FilterControlVm) {
                FilterControlVm vm = (FilterControlVm)value;
                return vm.IsEnable ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }
            else
                return
                    System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
