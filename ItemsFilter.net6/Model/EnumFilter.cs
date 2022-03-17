// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;

namespace BolapanControl.ItemsFilter.Model {
    /// <summary>
    /// Define the logic for Enum values filter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumFilter<T> : EqualFilter, IMultiValueFilter
        where T : Enum/*, IEquatable<T>*/ {
        private static readonly Lazy<IEnumerable> lz_enumValues = new(() => {
            Type enumType = typeof(T);
            return enumType.GetEnumValues();
        });
        /// <summary>
        /// Create new instance of EnumFilter.
        /// </summary>
        /// <param name="propertyInfo">propertyInfo, used to access a property of the collection item</param>
        public EnumFilter(ItemPropertyInfo propertyInfo)
            : base(((PropertyDescriptor)(propertyInfo.Descriptor)).GetValue) {
        }
        internal EnumFilter(Func<object?, object?> getter) : base(getter) { }
        /// <summary>
        /// TryGet list of defined enum values.
        /// Throw <exception cref="NotImplementedException"/> if attempt to set value.
        /// </summary>
        public override IEnumerable AvailableValues {
            get {
                return lz_enumValues.Value;
            }
            set {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        public override void IsMatch(FilterPresenter? sender, FilterEventArgs e) {
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
