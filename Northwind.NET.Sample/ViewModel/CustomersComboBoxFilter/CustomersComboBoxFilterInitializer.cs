using BolapanControl.ItemsFilter.Initializer;
using Northwind.NET.EF6Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.NET.Sample.ViewModel {
    class CustomersComboBoxFilterInitializer:FilterInitializer {
        public override BolapanControl.ItemsFilter.Model.Filter NewFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            if (key != null  && filterPresenter.CollectionView.SourceCollection is IEnumerable<Customer>) {
                return new CustomersComboBoxFilter();
            }
            return null;
        }
    }
}
