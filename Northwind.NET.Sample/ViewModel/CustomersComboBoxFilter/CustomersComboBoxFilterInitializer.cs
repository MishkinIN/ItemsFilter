// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Initializer;
using Northwind.NET.EF6Model;
using System.Collections.Generic;

namespace Northwind.NET.Sample.ViewModel {
    public class CustomersComboBoxFilterInitializer:FilterInitializer {
        public override BolapanControl.ItemsFilter.Model.Filter? TryGetFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            if (key != null  && filterPresenter.CollectionView.SourceCollection is IEnumerable<Customer>) {
                return new CustomersComboBoxFilter();
            }
            return null;
        }
    }
}
