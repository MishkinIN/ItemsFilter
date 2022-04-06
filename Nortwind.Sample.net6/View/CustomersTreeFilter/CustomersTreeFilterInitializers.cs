using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using Northwind.NET.EF6Model;
using Northwind.NET.Sample.ViewModel;
using System.Collections.Generic;

namespace Northwind.NET.Sample.View {
    public class CustomersTreeFilterInitializer : FilterInitializer {

        public override Filter? TryCreateFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            if (key != null && key is string @string && filterPresenter.CollectionView.SourceCollection is CustomersTreeVm) {

                var filter = new CountriesTreeFilter(@string);
                filter.Attach(filterPresenter);
                return filter;
            }
            return null;
        }
    }
    public class CityItemFilterInitializer : FilterInitializer {

        public override Filter? TryCreateFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            if (key is string @string && filterPresenter.CollectionView.SourceCollection is IEnumerable<CityCustomersTreeItem>) {
                return new CitiesTreeFilter(@string);
            }
            return null;
        }
    }
    public class CustomerFilterInitializer : FilterInitializer {

        public override Filter? TryCreateFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            if (key is string @string && filterPresenter.CollectionView.SourceCollection is IEnumerable<Customer>) {
                return new CustomersTreeFilter(@string);
            }
            return null;
        }
    }

}
