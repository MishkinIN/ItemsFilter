using BolapanControl.ItemsFilter.Model;
using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Linq;

namespace Northwind.NET.Sample.ViewModel {
    public class CitiesTreeFilter : CustomersTreeFilter, IFilter {
        private bool isCityCompareActive;
        private string? cityCompareTo;
        private readonly Dictionary<CityCustomersTreeItem, CustomersTreeFilter> customerFilters = new();
        private readonly CustomerFilterInitializer customerFilterInitializer = new();

        internal protected CitiesTreeFilter(string key)
            : base(key) {
        }
        public string? CityCompareTo {
            get { return cityCompareTo; }
            set {
                if (cityCompareTo != value) {
                    cityCompareTo = value;
                    isCityCompareActive = !String.IsNullOrEmpty(value);
                    IDisposable? defer = this.FilterPresenter?.DeferRefresh();
                    SendChangesToChild();
                    IsActive = CheckIsActive();
                    RaiseFilterChanged();
                    if (defer != null)
                        defer.Dispose();
                    RaisePropertyChanged(nameof(CityCompareTo));
                }
            }
        }
        public override void IsMatch(BolapanControl.ItemsFilter.FilterPresenter sender, BolapanControl.ItemsFilter.FilterEventArgs e) {
            if (IsActive && e.Accepted) {
                if (e.Item is CityCustomersTreeItem item) {
                    if (isCityCompareActive)
                        e.Accepted = item.City != null
#pragma warning disable CS8604 // Possible null reference argument.
                            && item.City.Contains(cityCompareTo)
#pragma warning restore CS8604 // Possible null reference argument.
                            && customerFilters[item].Count > 0;
                }
                else {
                    e.Accepted = false;
                }
            }
        }
        protected override void OnAttachPresenter(BolapanControl.ItemsFilter.FilterPresenter presenter) {
            base.OnAttachPresenter(presenter);
            foreach (CityCustomersTreeItem item in presenter.CollectionView.SourceCollection) {
                if (item.Customers is not null) {
                    BolapanControl.ItemsFilter.FilterPresenter? customersPresenter = BolapanControl.ItemsFilter.FilterPresenter.Get(item.Customers);
                    if (customersPresenter?.TryGetFilter(Key, customerFilterInitializer) is CustomersTreeFilter customerFilter) {
                        customerFilter.NameCompareTo = NameCompareTo;
                        customerFilter.ContactCompareTo = ContactCompareTo;
                        customerFilters[item] = customerFilter;
                    }
                }
            }

        }
        protected override void OnDetachPresenter(BolapanControl.ItemsFilter.FilterPresenter presenter) {
            customerFilters.Clear();
            base.OnDetachPresenter(presenter);
        }
        protected override void OnIsActiveChanged() {
            base.OnIsActiveChanged();
            if (!IsActive) {
                CityCompareTo = null;
            }
        }
        protected override void SendChangesToChild() {
            foreach (var entry in customerFilters) {
                entry.Value.NameCompareTo = NameCompareTo;
                entry.Value.ContactCompareTo = ContactCompareTo;
            }
        }
        protected override bool CheckIsActive() {
            return isCityCompareActive | base.CheckIsActive();
        }
    }
}
