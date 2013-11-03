using BolapanControl.ItemsFilter.Model;
using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Linq;

namespace Northwind.NET.Sample.ViewModel {
    public class CityItemFilter : CustomerItemFilter, IFilter {
        private static Func<int> NullCount = () => 0;
        private bool isCityCompareActive;
        private string cityCompareTo;
        private Dictionary<CityCustomersTreeItem, CustomerItemFilter> customerFilters = new Dictionary<CityCustomersTreeItem, CustomerItemFilter>();
        private CustomerFilterInitializer customerFilterInitializer = new CustomerFilterInitializer();
        private CollectionView collectionViev;
        
        internal protected CityItemFilter(string key)
            : base(key) {
        }
        public string CityCompareTo {
            get { return cityCompareTo; }
            set {
                if (cityCompareTo != value) {
                    cityCompareTo = value;
                    isCityCompareActive = !String.IsNullOrEmpty(value);
                    IDisposable defer = this.FilterPresenter == null ? null : this.FilterPresenter.DeferRefresh();
                    SendChangesToChild();
                    IsActive = CheckIsActive();
                    RaiseFilterChanged();
                     if (defer != null)
                        defer.Dispose();
                   RaisePropertyChanged("CityCompareTo");
                }
            }
        }
        public override void IsMatch(BolapanControl.ItemsFilter.FilterPresenter sender, BolapanControl.ItemsFilter.FilterEventArgs e) {
            if (IsActive && e.Accepted) {
                if (e.Item == null)
                    e.Accepted = false;
                else {
                    CityCustomersTreeItem item = (CityCustomersTreeItem)e.Item;
                    if (isCityCompareActive)
                        e.Accepted = item.City != null && item.City.Contains(cityCompareTo);
                    if (e.Accepted)
                        //e.Accepted =((ListCollectionView)( customerFilters[item].FilterPresenter.CollectionView)).Count>0;
                        e.Accepted = customerFilters[item].Count > 0;
                    
                }
            }
        }
        protected override void OnAttachPresenter(BolapanControl.ItemsFilter.FilterPresenter presenter) {
            if (presenter.CollectionView is CollectionView) {
                collectionViev = presenter.CollectionView as CollectionView;
            }
            foreach (CityCustomersTreeItem item in presenter.CollectionView.SourceCollection) {
                BolapanControl.ItemsFilter.FilterPresenter customersPresenter = BolapanControl.ItemsFilter.FilterPresenter.TryGet(item.Customers);
                CustomerItemFilter customerFilter = customersPresenter.TryGetFilter(Key, customerFilterInitializer) as CustomerItemFilter;
                if (customerFilter != null) {
                    customerFilter.NameCompareTo = NameCompareTo;
                    customerFilter.ContactCompareTo = ContactCompareTo;
                    customerFilters[item] = customerFilter;
                }
            }

        }
        public int Count { 
            get { return collectionViev == null ? 0 : collectionViev.Count; } 
        }
        protected override void OnDetachPresenter(BolapanControl.ItemsFilter.FilterPresenter presenter) {
            customerFilters.Clear();
            collectionViev = null;

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
