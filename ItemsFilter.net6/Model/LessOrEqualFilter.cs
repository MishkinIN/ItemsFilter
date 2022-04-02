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
//using System.Diagnostics;

namespace BolapanControl.ItemsFilter.Model {
    /// <summary>
    /// Define LessOrEqual filter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [View(typeof(ComparableFilterView))]
    public class LessOrEqualFilter<T> : Filter, IComparableFilter<T>
        where T : struct, IComparable<T> {
        private static readonly Lazy<string> lzName = new(() => "Less or equal:");
        protected new readonly Func<object?, T?> getter;
        protected T? _compareTo = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="getter">Func that return from item IComparable value to compare.</param>
        internal protected LessOrEqualFilter(Func<object?, T?> getter) : base(o => getter(o)) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(getter);
#endif
            this.getter = getter;
            base.name = lzName.Value;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LessOrEqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public LessOrEqualFilter(ItemPropertyInfo propertyInfo)
            : base(((PropertyDescriptor)propertyInfo.Descriptor).GetValue) {
            //if (!typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType))
            //    throw new ArgumentOutOfRangeException("propertyInfo", "typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType) return False.");
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(propertyInfo);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(typeof(IComparable<T>).IsAssignableFrom(propertyInfo.PropertyType));
#endif 
            //base.PropertyInfo = propertyInfo;
            getter = t => (T?)base.getter(t);
            base.name = lzName.Value;
        }
        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        public override void IsMatch(FilterPresenter sender, FilterEventArgs e) {
            if (IsActive && e.Accepted && _compareTo.HasValue) {
                if (e.Item != null && getter(e.Item) is T value)
                    e.Accepted = _compareTo.Value.CompareTo(value) >= 0;
                else {
                    e.Accepted = false;
                }
            }
        }
        #region IComparableFilter Members
        /// <summary>
        /// Get or set the value used in the comparison. If assign null, filter deactivated.
        /// </summary>
        public Nullable<T> CompareTo {
            get {
                return _compareTo;
            }
            set {
                if (!Object.Equals(value, _compareTo)) {
                    _compareTo = value;
                    IDisposable? defer = this.FilterPresenter?.DeferRefresh();
                    RefreshIsActive();
                    //RaiseFilterChanged();
                    RaisePropertyChanged(nameof(CompareTo));
                    if (defer != null)
                        defer.Dispose();
                }
            }
        }

        //T? IComparableFilter<T>.CompareTo {
        //    get {
        //        return CompareTo;
        //    }
        //    set {
        //        CompareTo = value;
        //    }
        //}

        #endregion

        /// <summary>
        /// Provide derived clases IsActiveChanged event.
        /// </summary>
        protected override void OnIsActiveChanged() {
            if (!IsActive)
                CompareTo = default;
            base.OnIsActiveChanged();
        }
        private void RefreshIsActive() {
            base.IsActive = _compareTo.HasValue;
        }
    }
}
