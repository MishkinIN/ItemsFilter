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
using System.Text;
using BolapanControl.ItemsFilter.Initializer;
using Northwind.NET.Sample.ViewModel;

namespace Northwind.NET.Sample.View {
    public class CustomersComboBoxFIM:BolapanControl.ItemsFilter.Initializer.FilterInitializersManager {
        public CustomersComboBoxFIM() {
            FilterInitializer fi = new CustomersComboBoxFilterInitializer();
            Add(fi);
        }
    }
}
