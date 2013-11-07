using BolapanControl.ItemsFilter.Model;
using Northwind.NET.EF6Model;
using System;
using System.Windows.Data;

namespace Northwind.NET.Sample.ViewModel {
    public class CustomersTreeFilter : Filter, IFilter {
        private string key;
        private string contactCompareTo;
        private string nameCompareTo;
        private bool isNameCompareActive;
        private bool isContactCompareActive;
        CollectionView collectionView;
        internal protected CustomersTreeFilter(string key) {
            this.key = key;
        }
        public string Key {
            get { return key; }
        }
         public string ContactCompareTo {
            get { return contactCompareTo; }
            set {
                if (contactCompareTo != value) {
                    contactCompareTo = value;
                    isContactCompareActive = !String.IsNullOrEmpty(value);
                    IDisposable defer = this.FilterPresenter == null ? null : this.FilterPresenter.DeferRefresh();
                    SendChangesToChild();
                    IsActive= CheckIsActive();
                    RaiseFilterChanged();
                     if (defer != null)
                        defer.Dispose();
                    RaisePropertyChanged("ContactCompareTo");
                }
            }
        }
         public int Count {
             get { return collectionView == null ? 0 : collectionView.Count; }
         }

        public string NameCompareTo {
            get { return nameCompareTo; }
            set {
                if (nameCompareTo != value) {
                    nameCompareTo = value;
                    isNameCompareActive = !String.IsNullOrEmpty(value);
                    IDisposable defer = this.FilterPresenter == null ? null : this.FilterPresenter.DeferRefresh();
                    SendChangesToChild();
                    IsActive = CheckIsActive();
                    RaiseFilterChanged();
                    if (defer != null)
                        defer.Dispose();
                    RaisePropertyChanged("NameCompareTo");
                }
            }
        }
        public override void IsMatch(BolapanControl.ItemsFilter.FilterPresenter sender, BolapanControl.ItemsFilter.FilterEventArgs e) {
            if (IsActive & e.Accepted) {
                if (e.Item == null)
                    e.Accepted = false;
                else {
                    Customer customer = ((Customer)e.Item);
                    if (isNameCompareActive)
                        e.Accepted = customer.Name != null & customer.Name.Contains(nameCompareTo);
                    if (isContactCompareActive)
                        e.Accepted &= customer.ContactName != null && customer.ContactName.Contains(contactCompareTo);
               }
            }
        }
        protected override void OnAttachPresenter(BolapanControl.ItemsFilter.FilterPresenter presenter) {
            collectionView = presenter.CollectionView as CollectionView;
        }
        protected override void OnDetachPresenter(BolapanControl.ItemsFilter.FilterPresenter presenter) {
            collectionView = null;
        }
        protected override void OnIsActiveChanged() {
            base.OnIsActiveChanged();
            if (!IsActive) {
                NameCompareTo = null;
                ContactCompareTo = null;
            }
        }
         protected virtual void SendChangesToChild() {
           
        }
       protected virtual bool CheckIsActive() {
            return isNameCompareActive | isContactCompareActive;
            
        }
    }
}
