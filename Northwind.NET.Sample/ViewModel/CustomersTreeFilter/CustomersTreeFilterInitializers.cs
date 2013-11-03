using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using Northwind.NET.EF6Model;
using System.Collections.Generic;

namespace Northwind.NET.Sample.ViewModel {
    public class CustomersTreeFilterInitializer : FilterInitializer {

        public override Filter NewFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            if (key != null && key is string && filterPresenter.CollectionView.SourceCollection is CustomersTreeVm) {
                return new CustomersTreeFilter((string)key);
            }
            return null;
        }
    }
    public class CityItemFilterInitializer : FilterInitializer {

        public override Filter NewFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            if (key != null && key is string && filterPresenter.CollectionView.SourceCollection is IEnumerable<CityCustomersTreeItem>) {
                return new CityItemFilter((string)key);
            }
            return null;
        }
    }
    public class CustomerFilterInitializer : FilterInitializer {

        public override Filter NewFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            if (key != null && key is string && filterPresenter.CollectionView.SourceCollection is IEnumerable<Customer>) {
                return new CustomerItemFilter((string)key);
            }
            return null;
        }
    }

}
