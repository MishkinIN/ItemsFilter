// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.View;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace BolapanControl.ItemsFilter.Model {
    /// <summary>
    /// Defines the range filter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [View(typeof(RangeFilterView))]
    public class RangeFilter<T> : Filter, IRangeFilter<T>
        where T : struct, IComparable {
        private new readonly Func<object?, T?> getter;
        private T? _compareTo = null;
        private T? _compareFrom = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="getter">Func that return from item values to compare.</param>
        protected RangeFilter(Func<object?, T?> getter) : base(o => getter(o)) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(getter, "getter is null.");
#endif
            this.getter = getter;
            base.name = "In range:";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public RangeFilter(ItemPropertyInfo propertyInfo)
            : base(((PropertyDescriptor)propertyInfo.Descriptor).GetValue) {
            //if (!typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType))
            //    throw new ArgumentOutOfRangeException("propertyInfo", "typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType) return False.");
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(propertyInfo);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(typeof(T).IsAssignableFrom(propertyInfo.PropertyType), "The typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType) return False.");

#endif            
            //base.PropertyInfo = propertyInfo;
            getter = t => (T?)((PropertyDescriptor)(propertyInfo.Descriptor)).GetValue(t);
            base.name = "In range:";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="CompareFrom">Minimum value.</param>
        /// <param name="CompareTo">Maximum value.</param>
        public RangeFilter(ItemPropertyInfo propertyInfo, T CompareFrom, T CompareTo)
            : this(propertyInfo) {
            _compareTo = CompareTo;
            _compareFrom = CompareFrom;
            RefreshIsActive();
        }

        /// <summary>
        /// Get or set the minimum value used in the comparison. 
        /// If CompareFrom and CompareTo is null, filter deactivated.
        /// </summary>
        public T? CompareFrom {
            get {
                return _compareFrom;
            }
            set {
                if (!Object.Equals(_compareFrom, value)) {
                    _compareFrom = value;
                    IDisposable? defer = this.FilterPresenter?.DeferRefresh();
                    RefreshIsActive();
                    OnIsActiveChanged();
                    RaisePropertyChanged(nameof(CompareFrom));
                    defer?.Dispose();
                }
            }
        }
        /// <summary>
        /// Get or set the maximum value used in the comparison.  
        /// If CompareFrom and CompareTo is null, filter deactivated.
        /// </summary>
        public T? CompareTo {
            get { return _compareTo; }
            set {
                if (!Object.Equals(_compareTo, value)) {
                    _compareTo = value;
                    IDisposable? defer = this.FilterPresenter?.DeferRefresh();
                    RefreshIsActive();
                    OnIsActiveChanged();
                    RaisePropertyChanged(nameof(CompareTo));
                    defer?.Dispose();
                }
            }
        }

        /// <summary>
        /// Provide derived clases IsActiveChanged event.
        /// </summary>
        protected override void OnIsActiveChanged() {
            if (!IsActive) {
                CompareFrom = null;
                CompareTo = null;
            }
            base.OnIsActiveChanged();
        }
        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        public override void IsMatch(FilterPresenter sender, FilterEventArgs e) {
            if (IsActive && e.Accepted) {
                if (e.Item != null && getter(e.Item) is T value)
                    e.Accepted = (!_compareFrom.HasValue || value.CompareTo(_compareFrom) >= 0)
                        && (!_compareTo.HasValue || value.CompareTo(_compareTo) <= 0);
                else {
                    e.Accepted = false;
                }
            }
        }
        private void RefreshIsActive() {
            base.IsActive = _compareFrom.HasValue || _compareTo.HasValue;

        }


        #region IRangeFilter Members

        //object? IRangeFilter.CompareFrom {
        //    get => CompareFrom;
        //    set {
        //        CompareFrom = (T?)value;
        //    }
        //}

        //object? IRangeFilter.CompareTo {
        //    get => CompareFrom;
        //    set {
        //        CompareFrom = (T?)value;
        //    }
        //}

        #endregion

    }
}
