using BolapanControl.ItemsFilter.Model;
using Northwind.NET.Sample.View;
using Northwind.NET.Sample.ViewModel;
using System;
using System.Collections.Generic;

namespace Northwind.NET.Sample.View {
    [View(typeof(CountriesTreeFilterView))]
    public class CountriesTreeFilter : CitiesTreeFilter, IFilter {
        private static readonly CityItemFilterInitializer citiItemFilterInitializer = new();
        private string? countryCompareTo;
        private bool isCountryCompareActive;
        private readonly Dictionary<CountryCustomersTreeItem, CitiesTreeFilter> cityFilters = new();
        internal CountriesTreeFilter(string key) : base(key) {
            base.name = "Quick filter:";
        }
        public CountriesTreeFilter() : this("Country") { }

        public string? CountryCompareTo {
            get { return countryCompareTo; }
            set {
                if (countryCompareTo != value) {
                    countryCompareTo = value;
                    isCountryCompareActive = !String.IsNullOrEmpty(value);
                    IDisposable? defer = this.FilterPresenter?.DeferRefresh();
                    IsActive = CheckIsActive();
                    //RaiseFilterChanged();
                    RaisePropertyChanged(nameof(CountryCompareTo));
                    if (defer != null)
                        defer.Dispose();
                }
            }
        }
        public override void IsMatch(BolapanControl.ItemsFilter.FilterPresenter sender, BolapanControl.ItemsFilter.FilterEventArgs e) {
            if (IsActive && e.Accepted) {
                if (e.Item is CountryCustomersTreeItem item) {
                    if (isCountryCompareActive)
                        e.Accepted &= item.Country != null
#pragma warning disable CS8604 // Possible null reference argument.
                            && item.Country.Contains(countryCompareTo);
#pragma warning restore CS8604 // Possible null reference argument.
                    if (e.Accepted)
                        e.Accepted = cityFilters[item].Count > 0;
                }
                else {
                    e.Accepted = false;
                }
            }
        }
        protected override void OnAttachPresenter(BolapanControl.ItemsFilter.FilterPresenter presenter) {
            foreach (CountryCustomersTreeItem country in ((CustomersTreeVm)(presenter.CollectionView.SourceCollection))) {
                if (country.Cities is not null) {
                    BolapanControl.ItemsFilter.FilterPresenter citiesPresenter = BolapanControl.ItemsFilter.FilterPresenter.Get(country.Cities);
                    if (citiesPresenter?.TryGetFilter(Key, citiItemFilterInitializer, out var filter)==true
                        && filter is CitiesTreeFilter cityFilter) {
                        cityFilter.Attach(citiesPresenter);
                        cityFilter.CityCompareTo = CityCompareTo;
                        cityFilter.NameCompareTo = NameCompareTo;
                        cityFilter.ContactCompareTo = ContactCompareTo;
                        cityFilters[country] = cityFilter;
                    } 
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
