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
    /// Defines a string filter 
    /// </summary>
    [View(typeof(StringFilterView))]
    public class StringFilter : Filter, IStringFilter {
        //private readonly Func<object?, string?> getter;
        private StringFilterMode _filterMode = StringFilterMode.Contains;
        private string? _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="getter">Func that return from item string value to compare.</param>
        protected StringFilter(Func<object?, string?> getter) : base(getter) {
            //this.getter = getter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="filterMode">The filter mode.</param>
        public StringFilter(ItemPropertyInfo propertyInfo, StringFilterMode filterMode = StringFilterMode.Contains)
            : base(((PropertyDescriptor)(propertyInfo.Descriptor)).GetValue) {
            //if (!typeof(string).IsAssignableFrom(propertyInfo.PropertyType))
            //    throw new ArgumentOutOfRangeException("propertyInfo", "typeof(string).IsAssignableFrom(propertyInfo.PropertyType) return False.");
            //Debug.Assert(propertyInfo != null, "propertyInfo is null.");
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType));
#endif
            //base.PropertyInfo = propertyInfo;
            _filterMode = filterMode;
            //Func<object, object> getterItem = ((PropertyDescriptor)(PropertyInfo.Descriptor)).GetValue;
            //this.getter = t => ((string)(getterItem(t)));
            base.name = "String";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="filterMode">The filter mode.</param>
        /// <param name="value">The value.</param>
        public StringFilter(ItemPropertyInfo propertyInfo, StringFilterMode filterMode, string value)
            : this(propertyInfo, filterMode) {
            _value = value;
            base.IsActive = !String.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Gets or sets the string filter mode.
        /// </summary>
        /// <value>The mode.</value>
        public StringFilterMode Mode {
            get {
                return _filterMode;
            }
            set {
                if (_filterMode != value) {
                    _filterMode = value;
                    //OnIsActiveChanged();
                    RaisePropertyChanged(nameof(Mode));
                }
            }
        }

        /// <summary>
        /// Gets or sets the value to look for.
        /// </summary>
        /// <value>The value.</value>
        public string? Value {
            get {
                return _value;
            }
            set {
                if (_value != value) {
                    _value = value;
                    IDisposable? defer = this.FilterPresenter?.DeferRefresh();
                    base.IsActive = !string.IsNullOrEmpty(value);
                    //OnIsActiveChanged();
                    RaisePropertyChanged(nameof(Value));
                    defer?.Dispose();
                }
            }
        }
        /// <summary>
        /// Provide derived clases IsActiveChanged event.
        /// </summary>
        protected override void OnIsActiveChanged() {
            if (!IsActive)
                Value = string.Empty;
            base.OnIsActiveChanged();
        }

        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        public override void IsMatch(FilterPresenter sender, FilterEventArgs e) {
            if (IsActive && e.Accepted) {
                string? toCompare = getter(e.Item)?.ToString();
                if (!string.IsNullOrEmpty(toCompare))
#pragma warning disable CS8604 // Possible null reference argument.
                    e.Accepted = _filterMode switch {
                        StringFilterMode.Contains => toCompare.Contains(_value),
                        StringFilterMode.StartsWith => toCompare.StartsWith(_value),
                        StringFilterMode.EndsWith => toCompare.EndsWith(_value),
                        _ => toCompare.Equals(_value),
                    };
#pragma warning restore CS8604 // Possible null reference argument.
                else {
                    e.Accepted = false;
                }
            }
        }
    }
}
