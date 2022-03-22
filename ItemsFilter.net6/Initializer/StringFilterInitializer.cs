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
using System.Diagnostics;

namespace BolapanControl.ItemsFilter.Initializer {
    /// <summary>
    /// Represent initializer for StringFilter.
    /// </summary>
    public class StringFilterInitializer : PropertyFilterInitializer {
        /// <summary>
        /// Create new instance of StringFilter, if it is possible for filterPresenter in current state and key.
        /// </summary>
        protected override Filter? NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(filterPresenter);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(propertyInfo);
#endif
            Type propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && typeof(String).IsAssignableFrom(propertyInfo.PropertyType)
                && !propertyType.IsEnum
                ) {
                var filter =  new StringFilter(propertyInfo);
                if (filter != null) {
                    filter.Attach(filterPresenter);
                }
                return filter;
            }
            return null;
        }
    }
}
