// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace BolapanControl.ItemsFilter.Model {
    /// <summary>
    /// Base class for a filter.
    /// </summary>
    public abstract class Filter : IFilter, INotifyPropertyChanged {
        protected string name = "Filter:";
        private bool isActive;
        private FilterPresenter? filterPresenter;
        protected readonly Func<object?, object?> getter;
        protected Filter(Func<object?, object?> getter) {
            this.getter = getter;
        }
        /// <summary>
        /// Get attached FilterPresenter.
        /// </summary>
        public FilterPresenter? FilterPresenter {
            get { return filterPresenter; }
        }

        /// <summary>
        /// Represent action that determine is item match filter.
        /// </summary>
        /// <param name="sender">FilterPresenter that contains filter.</param>
        /// <param name="e">FilterEventArgs include Item and Accepted fields.</param>
        public abstract void IsMatch(FilterPresenter sender, FilterEventArgs e);
        /// <summary>
        /// Get or set Name of filter.
        /// </summary>
        public string Name {
            get {
                return name;
            }
        }
        /// <summary>
        /// Get or set value, determines is filter IsMatch action include in parentCollection filter.
        /// </summary>
        public bool IsActive {
            get {
                return isActive;
            }
            set {
                if (isActive != value) {
                    isActive = value;
                    IDisposable? defer = this.FilterPresenter?.DeferRefresh();
                    RaisePropertyChanged(nameof(IsActive));
                    OnIsActiveChanged();
                    defer?.Dispose();
                }
            }
        }
        /// <summary>
        /// Provides class handling for the AttachPresenter event that occurs when FilterPresenter is attached.
        /// </summary>
        protected virtual void OnAttachPresenter(FilterPresenter presenter) {

        }
        /// <summary>
        /// Provides class handling for the DetachPresenter event that occurs when FilterPresenter is detached.
        /// </summary>
        protected virtual void OnDetachPresenter(FilterPresenter presenter) {
        }
        /// <summary>
        /// Provide derived class IsActiveChanged event.
        /// </summary>
        protected virtual void OnIsActiveChanged() {
            RaiseFilterChanged();
        }
        /// <summary>
        /// Report attached listeners that filter changed.
        /// </summary>
        private void RaiseFilterChanged() {
            if (filterPresenter != null)
                filterPresenter.ReceiveFilterChanged(this);
        }

        internal void Detach(FilterPresenter presenter) {
            if (presenter != null) {
                presenter.Filter -= IsMatch;
                if (presenter == filterPresenter)
                    filterPresenter = null;
                OnDetachPresenter(presenter);
            }
        }
        public void Attach(FilterPresenter presenter) {
            filterPresenter = presenter;
            if (filterPresenter != null)
                filterPresenter.ReceiveFilterChanged(this);
            OnAttachPresenter(presenter);
        }
        #region Члены INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName) {
           
            VerifyPropertyName(propertyName);
            if (this.FilterPresenter != null) {
                using (IDisposable defer = this.FilterPresenter.DeferRefresh()) {
                    RaiseFilterChanged();
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        protected void VerifyPropertyName(string propertyName) {
            var myType = this.GetType();

#if NETFX_CORE
            if (!string.IsNullOrEmpty(propertyName)
                && myType.GetTypeInfo().GetDeclaredProperty(propertyName) == null)
            {
                throw new ArgumentException("Property not found", propertyName);
            }
#else
            if (!string.IsNullOrEmpty(propertyName)
                && myType.GetProperty(propertyName) == null) {
#if !SILVERLIGHT
                if (this is ICustomTypeDescriptor descriptor) {
                    if (descriptor.GetProperties()
                        .Cast<PropertyDescriptor>()
                        .Any(property => property.Name == propertyName)) {
                        return;
                    }
                }
#endif

                throw new ArgumentException("Property not found", propertyName);
            }
#endif
        }
        #endregion

    }


}
