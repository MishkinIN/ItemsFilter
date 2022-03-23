// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;
using System;
using System.ComponentModel;

namespace BolapanControl.ItemsFilter.Initializer {
    /// <summary>
    /// Define GreaterOrEqualFilter Initializer.
    /// </summary>
    public class GreaterOrEqualFilterInitializer : LessOrEqualFilterInitializer {

#pragma warning disable IDE0051 // Remove unused private members
        private const string _filterName = "Greater or Equal";
#pragma warning restore IDE0051 // Remove unused private members
        /// <summary>
        /// Create PropertyFilter for instance of FilterPresenter, if it is possible.
        /// </summary>
        /// <param name="filterPresenter">filterPresenter for that filter will be created.</param>
        /// <param name="key">ItemPropertyInfo cpecified property that PropertyFilter will be use.</param>
        /// <returns>Instance of GreaterOrEqualFilter if:
        ///  filterPresenter.ItemProperties.Contains(propertyInfo)
        ///           && typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType)
        ///           && propertyInfo.PropertyType!=typeof(String)
        ///           && propertyInfo.PropertyType != typeof(bool)
        ///           && !propertyType.IsEnum
        ///  otherwise, null.</returns>
        protected override Filter? NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(filterPresenter);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(propertyInfo);
#endif
            Type propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType)
                && propertyInfo.PropertyType != typeof(String)
                && propertyInfo.PropertyType != typeof(bool)
                && !propertyType.IsEnum
               ) {
                var filter = Activator.CreateInstance(typeof(GreaterOrEqualFilter<>).MakeGenericType(propertyInfo.PropertyType), propertyInfo) as Filter;
                if (filter != null) {
                    filter.Attach(filterPresenter);
                }
                return filter;
            }
            return null;
        }

    }
}
