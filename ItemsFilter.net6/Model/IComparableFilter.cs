// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;

namespace BolapanControl.ItemsFilter.Model {
    /// <summary>
    /// Define the contract for compare property filter.
    /// </summary>
    public interface IComparableFilter : IFilter /*: IPropertyFilter*/ {
        
    }

    /// <summary>
    /// Defines the contract for a compare filter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IComparableFilter<T> : IFilter, IComparableFilter
        where T :struct, IComparable<T> {
        /// <summary>
        /// Gets or sets the value used in the comparison.
        /// </summary>
        /// <value>The compare to.</value>
        T? CompareTo {
            get;
            set;
        }
    }
}
