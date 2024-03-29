﻿// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;

namespace BolapanControl.ItemsFilter.Initializer {
    /// <summary>
    /// Base class for filter initializer.
    /// </summary>
    public abstract class FilterInitializer {
        /// <summary>
        /// Generate new attached instance of Filter class, if it is possible for filterPresenter and key.
        /// </summary>
        /// <param name="filterPresenter">FilterPresenter, which can be attached Filter</param>
        /// <param name="key">Key for generated Filter. For PropertyFilter, key used as the name for binding property in filterPresenter.Parent collection.</param>
        /// <returns>Instance of Filter class, or null.</returns>
         public abstract Filter? TryCreateFilter(FilterPresenter filterPresenter, object key);
    }

}
