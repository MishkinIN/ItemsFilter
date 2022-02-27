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
        protected new readonly Func<object?, T?> getter;
        protected T? _compareTo = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="getter">Func that return from item IComparable value to compare.</param>
        protected LessOrEqualFilter(Func<object?, T?> getter) : base(o => getter(o)) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(getter);
#endif
            this.getter = getter;
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
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(propertyInfo != null, "propertyInfo is null.");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(typeof(IComparable<T>).IsAssignableFrom(propertyInfo.PropertyType));
#endif 
            //base.PropertyInfo = propertyInfo;
            getter = t => base.getter(t) as T?;
            base.Name = "Less or equal:";
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LessOrEqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="compareTo">The compare to.</param>
        public LessOrEqualFilter(ItemPropertyInfo propertyInfo, T compareTo)
            : this(propertyInfo) {
            _compareTo = compareTo;
            RefreshIsActive();

        }
        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        public override void IsMatch(FilterPresenter sender, FilterEventArgs e) {
            if (e.Accepted) {
                if (e.Item == null || !_compareTo.HasValue)
                    e.Accepted = false;
                else {
                    T? value = getter(e.Item);
                    if (!value.HasValue)
                        e.Accepted = false;
                    else
                        e.Accepted = _compareTo.Value.CompareTo(value.Value) <= 0;
                }
            }
        }
        /// <summary>
        /// Get or set the value used in the comparison. If assign null, filter deactivated.
        /// </summary>
        public T? CompareTo {
            get {
                return _compareTo;
            }
            set {
                if (value.HasValue ^ _compareTo.HasValue
#pragma warning disable CS8629 // Nullable value type may be null.
                    || (value.HasValue & _compareTo.HasValue && _compareTo.Value.CompareTo(value.Value) != 0)) {
#pragma warning restore CS8629 // Nullable value type may be null.
                    _compareTo = value;
                    RefreshIsActive();
                    RaiseFilterChanged();
                    RaisePropertyChanged(nameof(CompareTo));
                }
            }
        }
        #region IComparableFilter Members

        T? IComparableFilter<T>.CompareTo {
            get {
                return CompareTo;
            }
            set {
                CompareTo = value;
            }
        }
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
