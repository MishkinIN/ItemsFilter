using BolapanControl.ItemsFilter.Model;
using Northwind.NET.EF6Model;
using System;
using System.Windows.Data;

namespace Northwind.NET.Sample.View {
    public class CustomerItemFilter : Filter, IFilter {
        private readonly string key;
        private string? contactCompareTo;
        private string? nameCompareTo;
        private bool isNameCompareActive = false;
        private bool isContactCompareActive = false;
        protected CollectionView? collectionView;
        internal protected CustomerItemFilter(string key) : base(o => o) {
            this.key = key;
        }
        public string Key {
            get { return key; }
        }
        public string? ContactCompareTo {
            get { return contactCompareTo; }
            set {
                if (contactCompareTo != value) {
                    contactCompareTo = value;
                    isContactCompareActive = !String.IsNullOrEmpty(contactCompareTo);
                    IDisposable? defer = this.FilterPresenter?.DeferRefresh();
                    SendChangesToChild();
                    IsActive = CheckIsActive();
                    //RaiseFilterChanged();
                    RaisePropertyChanged(nameof(ContactCompareTo));
                    if (defer != null)
                        defer.Dispose();
                }
            }
        }
        public int Count {
            get { return collectionView == null ? 0 : collectionView.Count; }
        }

        public string? NameCompareTo {
            get { return nameCompareTo; }
            set {
                if (nameCompareTo != value) {
                    nameCompareTo = value;
                    isNameCompareActive = !String.IsNullOrEmpty(nameCompareTo);
                    IDisposable? defer = this.FilterPresenter?.DeferRefresh();
                    SendChangesToChild();
                    IsActive = CheckIsActive();
                    //RaiseFilterChanged();
                    RaisePropertyChanged(nameof(NameCompareTo));
                    if (defer != null)
                        defer.Dispose();
                }
            }
        }
        public override void IsMatch(BolapanControl.ItemsFilter.FilterPresenter sender, BolapanControl.ItemsFilter.FilterEventArgs e) {
            if (IsActive & e.Accepted) {
                if (e.Item is Customer customer) {
                    if (isNameCompareActive && customer.Name != null)
#pragma warning disable CS8604 // Possible null reference argument.
                        e.Accepted = customer.Name.Contains(nameCompareTo);
#pragma warning restore CS8604 // Possible null reference argument.
                    if (isContactCompareActive && customer.ContactName != null)
#pragma warning disable CS8604 // Possible null reference argument.
                        e.Accepted &= customer.ContactName.Contains(contactCompareTo);
#pragma warning restore CS8604 // Possible null reference argument.

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
