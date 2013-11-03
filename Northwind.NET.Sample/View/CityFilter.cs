using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Northwind.NET.Sample.View {
    class CityFilter:Control,IFilter {
        FilterPresenter filterPresenter = null;
        static CityFilter() {
            VisibilityProperty.OverrideMetadata(typeof(CityFilter), new FrameworkPropertyMetadata(Visibility.Hidden));
        }
        #region IsActive

        /// <summary>
        /// IsActive Dependency Property
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(CityFilter),
                new FrameworkPropertyMetadata((bool)false,
                    new PropertyChangedCallback(OnIsActiveChanged)));

        /// <summary>
        /// Gets or sets the IsActive property. 
        /// </summary>
        public bool IsActive {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        /// <summary>
        /// Handles changes to the IsActive property.
        /// </summary>
        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            CityFilter target = (CityFilter)d;
            if (target.filterPresenter != null)
                target.filterPresenter.ReceiveFilterChanged(target);
            bool oldIsActive = (bool)e.OldValue;
            bool newIsActive = target.IsActive;
            target.OnIsActiveChanged(oldIsActive, newIsActive);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the IsActive property.
        /// </summary>
        protected virtual void OnIsActiveChanged(bool oldIsActive, bool newIsActive) {
        }

        #endregion
        #region Country

        /// <summary>
        /// Country Dependency Property
        /// </summary>
        public static readonly DependencyProperty CountryProperty =
            DependencyProperty.Register("Country", typeof(string), typeof(CityFilter),
                new FrameworkPropertyMetadata((string)null,
                    new PropertyChangedCallback(OnCountryChanged)));
        
        /// <summary>
        /// Gets or sets the filter string. 
        /// </summary>
        public string Country {
            get { return (string)GetValue(CountryProperty); }
            set { SetValue(CountryProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Country property.
        /// </summary>
        private static void OnCountryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            CityFilter target = (CityFilter)d;
            string oldCountry = (string)e.OldValue;
            string newCountry = target.Country;
            if (target.filterPresenter!=null) {
                target.filterPresenter.ReceiveFilterChanged(target);
            }
            target.OnCountryChanged(oldCountry, newCountry);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Country property.
        /// </summary>
        protected virtual void OnCountryChanged(string oldCountry, string newCountry) {
        }

        #endregion
        #region ParentCollection

        /// <summary>
        /// ParentCollection Dependency Property
        /// </summary>
        public static readonly DependencyProperty ParentCollectionProperty =
            DependencyProperty.Register("ParentCollection", typeof(IEnumerable<CityItem>), typeof(CityFilter),
                new FrameworkPropertyMetadata((IEnumerable<CityItem>)null,
                    new PropertyChangedCallback(OnParentCollectionChanged)));

        /// <summary>
        /// Gets or sets the ParentCollection property. 
        /// </summary>
        public IEnumerable<CityItem> ParentCollection {
            get { return (IEnumerable<CityItem>)GetValue(ParentCollectionProperty); }
            set { SetValue(ParentCollectionProperty, value); }
        }

        /// <summary>
        /// Handles changes to the ParentCollection property.
        /// </summary>
        private static void OnParentCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            CityFilter target = (CityFilter)d;
            IEnumerable<CityItem> oldParentCollection = (IEnumerable<CityItem>)e.OldValue;
            if (target.filterPresenter != null) {
                bool isActive = target.IsActive;
                if (isActive) 
                    target.IsActive = false;
                target.filterPresenter.ReceiveFilterChanged(target);
                if (isActive)
                    target.IsActive = true;
            }
            IEnumerable<CityItem> newParentCollection = target.ParentCollection;
            target.filterPresenter = FilterPresenter.TryGet(newParentCollection);
            if (target.filterPresenter != null) {
                target.filterPresenter.ReceiveFilterChanged(target);
            }
            target.OnParentCollectionChanged(oldParentCollection, newParentCollection);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the ParentCollection property.
        /// </summary>
        protected virtual void OnParentCollectionChanged(IEnumerable<CityItem> oldParentCollection, IEnumerable<CityItem> newParentCollection) {
        }

        #endregion

        public void IsMatch(BolapanControl.ItemsFilter.FilterPresenter sender, BolapanControl.ItemsFilter.FilterEventArgs e) {
            CityItem item = (CityItem)e.Item;
            e.Accepted = item.Country == null ||String.IsNullOrEmpty(this.Country) || item.Country == this.Country;
        }
    }
    public class CityItem {
        public string Country { get; set; }
        public string City { get; set; }
    }
}
