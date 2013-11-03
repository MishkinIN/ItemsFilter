using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.Initializer;
using System.Collections;
using Northwind.NET.EF6Model;

namespace Northwind.NET.Sample.ViewModel {
    public class UnitPriceEqualFilter:EqualFilter<decimal?> {
        public UnitPriceEqualFilter() : base(
            value => ((Product)value).UnitPrice
            ) { }
        protected override void OnAttachPresenter(BolapanControl.ItemsFilter.FilterPresenter presenter) {
            base.OnAttachPresenter(presenter);
            base.AvailableValues = presenter.CollectionView.SourceCollection.OfType<Product>().Select(item => item.UnitPrice).OrderBy(i => i);
        }
    }
    public class UnitPriceRangeFilter : RangeFilter<decimal> {
        public UnitPriceRangeFilter()
            : base((value) => ((Product)value).UnitPrice.HasValue?((Product)value).UnitPrice.Value:Decimal.Zero) {
        }
    }
    public class UnitPriceFilterInitializer : FilterInitializer {
        public override Filter NewFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            return new UnitPriceEqualFilter();
        }
    }
    public class UnitPriceRangeFilterInitializer : FilterInitializer {
        public override Filter NewFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            return new UnitPriceRangeFilter();
        }
    }
}
