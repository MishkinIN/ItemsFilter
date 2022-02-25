// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using Northwind.NET.EF6Model;
using System;
using System.Linq;

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
        public override Filter TrygetFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            return new UnitPriceEqualFilter();
        }
    }
    public class UnitPriceRangeFilterInitializer : FilterInitializer {
        public override Filter TrygetFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            return new UnitPriceRangeFilter();
        }
    }
}
