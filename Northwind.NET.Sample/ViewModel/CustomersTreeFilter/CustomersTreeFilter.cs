using BolapanControl.ItemsFilter.Model;
using Northwind.NET.Sample.View;
using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace Northwind.NET.Sample.ViewModel {
    [View(typeof(CustomersTreeFilterView))]
    public class CustomersTreeFilter : CityItemFilter, IFilter {
        private static CityItemFilterInitializer citiItemFilterInitializer = new CityItemFilterInitializer();
        private string countryCompareTo;
        private bool isCountryCompareActive;
        private Dictionary<CountryCustomersTreeItem, CityItemFilter> cityFilters = new Dictionary<CountryCustomersTreeItem, CityItemFilter>();
        internal CustomersTreeFilter(string key):base(key) {
        }


        public string CountryCompareTo {
            get { return countryCompareTo; }
            set {
                if (countryCompareTo != value) {
                    countryCompareTo = value;
                    isCountryCompareActive = !String.IsNullOrEmpty(value);
                    IDisposable defer = this.FilterPresenter == null ? null : this.FilterPresenter.DeferRefresh();
                    IsActive = CheckIsActive();
                    RaiseFilterChanged();
                    if (defer != null)
                        defer.Dispose();
                    RaisePropertyChanged("CountryCompareTo");
                }
            }
        }
        public override void IsMatch(BolapanControl.ItemsFilter.FilterPresenter sender, BolapanControl.ItemsFilter.FilterEventArgs e) {
            if (IsActive && e.Accepted) {
                if (e.Item == null)
                    e.Accepted = false;
                else {
                    CountryCustomersTreeItem item =(CountryCustomersTreeItem)e.Item;
                    if(isCountryCompareActive)
                        e.Accepted &=item.Country!=null && item.Country.Contains(countryCompareTo);
                    if (e.Accepted)
                        //e.Accepted = ((ListCollectionView)(cityFilters[item].FilterPresenter.CollectionView)).Count > 0;
                        e.Accepted = cityFilters[item].Count > 0;
                }
            }
        }
        protected override void OnAttachPresenter(BolapanControl.ItemsFilter.FilterPresenter presenter) {
            foreach (CountryCustomersTreeItem country in ((CustomersTreeVm)(presenter.CollectionView.SourceCollection))) {
                BolapanControl.ItemsFilter.FilterPresenter citiesPresenter = BolapanControl.ItemsFilter.FilterPresenter.TryGet(country.Cities);
                CityItemFilter cityFilter =  citiesPresenter.TryGetFilter(Key, citiItemFilterInitializer) as CityItemFilter;
                if (cityFilter!=null) {
                    cityFilter.CityCompareTo = CityCompareTo;
                    cityFilter.NameCompareTo = NameCompareTo;
                    cityFilter.ContactCompareTo = ContactCompareTo;
                    cityFilters[country] = cityFilter; 
                }
            }
        }
        protected override void OnDetachPresenter(BolapanControl.ItemsFilter.FilterPresenter presenter) {
            cityFilters.Clear();
        }
        protected override void OnIsActiveChanged() {
            base.OnIsActiveChanged();
            if (!IsActive) {
                CountryCompareTo = null;
            }
        }
        protected override bool CheckIsActive() {
            return isCountryCompareActive | base.CheckIsActive();

        }
        protected override void SendChangesToChild() {
            foreach (var entry in cityFilters) {
                entry.Value.CityCompareTo = CityCompareTo;
                entry.Value.ContactCompareTo = ContactCompareTo;
                entry.Value.NameCompareTo = NameCompareTo;

            }

        }
    }
}
