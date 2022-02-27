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
    /// Define LessOrEqualFilter initializer.
    /// </summary>
    public class LessOrEqualFilterInitializer : PropertyFilterInitializer {
        private const string _filterName = "Less or Equal";
        /// <summary>
        /// Create LessOrEqualFilter for instance of FilterPresenter, if it is possible.
        /// </summary>
        /// <param name="filterPresenter">FilterPresenter, which can be attached Filter</param>
        /// <param name="key">ItemPropertyInfo for binding to property.</param>
        /// <returns>Instance of LessOrEqualFilter class or null</returns>
        protected override PropertyFilter? NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo) {
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
                return Activator.CreateInstance(typeof(LessOrEqualFilter<>).MakeGenericType(propertyInfo.PropertyType), propertyInfo) as PropertyFilter;
            }
            return null;
        }
    }
}
