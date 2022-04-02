// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.View;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace BolapanControl.ItemsFilter.Model {
    /// <summary>
    /// Base class for filter that using list of values.
    /// </summary>
    [View(typeof(MultiValueFilterView))]
    public abstract class EqualFilter : Filter, IMultiValueFilter, IFilter {
        private readonly ObservableCollection<object> selectedValues;
        private readonly ReadOnlyObservableCollection<object> _selectedValues;

        /// <summary>
        /// Initialize new instance of EqualFilter from deriver class.
        /// </summary>
        protected EqualFilter(Func<object?, object?> getter) : base(getter) {
            selectedValues = new ObservableCollection<object>();
            _selectedValues = new ReadOnlyObservableCollection<object>(selectedValues);
            base.name = "Equal:";
        }

        public ReadOnlyObservableCollection<object> SelectedValues {
            get { return _selectedValues; }
        }


        public abstract IEnumerable AvailableValues {
            get;
            set;
        }

        protected override void OnIsActiveChanged() {
            if (!IsActive)
                selectedValues.Clear();
            base.OnIsActiveChanged();
        }
        public void SelectedValuesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            SelectedValuesChanged(e.AddedItems, e.RemovedItems);
        }
        internal void SelectedValuesChanged(IList? addedItems, IList? removedItems) {
            IDisposable? defer = this.FilterPresenter?.DeferRefresh();
            bool isactive = IsActive;
            if (addedItems != null) {
                foreach (var item in addedItems) {
                    selectedValues.Add(item);
                    isactive = true;
                }
            }
            if (removedItems != null) {
                foreach (var item in removedItems) {
                    selectedValues.Remove(item);
                }
                isactive = selectedValues.Count > 0;
            }
            IsActive = isactive;
            defer?.Dispose();
        }
    }

    /// <summary>
    /// Defines the logic for reference equality filter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectEqualFilter : EqualFilter, IMultiValueFilter {
        private IEnumerable availableValues;


        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="getter">Func that return from item values to compare.</param>
        internal protected ObjectEqualFilter(Func<object?, object?> getter) : base(getter) {
            //this.getter = getter;
            availableValues = Array.Empty<object>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="getter">Func that return values to compare from item.</param>
        /// <param name="availableValues">Predefined set of available values.</param>
        protected internal ObjectEqualFilter(Func<object?, object?> getter, IEnumerable availableValues)
            : this(getter) {
            this.availableValues = availableValues;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public ObjectEqualFilter(ItemPropertyInfo propertyInfo)
            : base(((PropertyDescriptor)(propertyInfo.Descriptor)).GetValue) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(propertyInfo);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(typeof(object).IsAssignableFrom(propertyInfo.PropertyType));

#endif
            availableValues = Array.Empty<object>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="availableValues">Predefined set of available values.</param>
        public ObjectEqualFilter(ItemPropertyInfo propertyInfo, IEnumerable availableValues)
            : this(propertyInfo) {
            this.availableValues = availableValues;
        }
        /// <summary>
        /// Set of available values that can be include in filter.
        /// </summary>
        public override IEnumerable AvailableValues {
            get {
                return availableValues;
            }
            set {
                if (availableValues != value) {
                    availableValues = value;
                }
            }
        }
        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        public override void IsMatch(FilterPresenter sender, FilterEventArgs e) {
            if (IsActive && e.Accepted) {
                object? value = getter(e.Item);
                e.Accepted = SelectedValues.Any<object>(val => Object.Equals(value,val));
            }
        }
    }


    /// <summary>
    /// Defines the logic for equality filter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EqualFilter<T> : EqualFilter, IMultiValueFilter
        where T : IEquatable<T> {
        private IEnumerable availableValues;


        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="getter">Func that return from item values to compare.</param>
        internal protected EqualFilter(Func<object?, object?> getter) : base(getter) {
            //this.getter = getter;
            availableValues = Array.Empty<object>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="getter">Func that return values to compare from item.</param>
        /// <param name="availableValues">Predefined set of available values.</param>
        protected internal EqualFilter(Func<object?, object?> getter, IEnumerable availableValues)
            : this(getter) {
            this.availableValues = availableValues;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public EqualFilter(ItemPropertyInfo propertyInfo)
            : base(((PropertyDescriptor)(propertyInfo.Descriptor)).GetValue) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(propertyInfo);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(typeof(T), propertyInfo.PropertyType);

#endif
            availableValues = Array.Empty<object>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="availableValues">Predefined set of available values.</param>
        public EqualFilter(ItemPropertyInfo propertyInfo, IEnumerable availableValues)
            : this(propertyInfo) {
            this.availableValues = availableValues;
        }
        /// <summary>
        /// Set of available values that can be include in filter.
        /// </summary>
        public override IEnumerable AvailableValues {
            get {
                return availableValues;
            }
            set {
                if (availableValues != value) {
                    availableValues = value;
                }
            }
        }
        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        public override void IsMatch(FilterPresenter sender, FilterEventArgs e) {
            if (IsActive && e.Accepted) {
                object? value = getter(e.Item);
                if (value == null)
                    e.Accepted = false;
                else
                    e.Accepted = SelectedValues.Any(val => val.Equals(value));
            }
        }
    }
}

