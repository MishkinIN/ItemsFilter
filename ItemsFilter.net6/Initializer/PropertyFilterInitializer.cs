// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace BolapanControl.ItemsFilter.Initializer {
    /// <summary>
    /// Base class for PropertyFilter initialiser.
    /// </summary>
    public abstract class PropertyFilterInitializer : FilterInitializer {
        /// <summary>
        /// Generate new instance of Filter class, if it is possible for filterPresenter and key.
        /// </summary>
        /// <param name="filterPresenter">FilterPresenter, which can be attached Filter</param>
        /// <param name="key">Key for generated Filter. For PropertyFilter, key used as the name for binding property in filterPresenter.Parent collection.</param>
        /// <returns>Instance of Filter class or null.</returns>
        public sealed override Filter? TryCreateFilter(FilterPresenter filterPresenter, object key) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(filterPresenter);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(key); 
#endif

            if (key is ItemPropertyInfo info)
                return NewFilter(filterPresenter, info);
            if (key is string str 
                && filterPresenter.ItemProperties.FirstOrDefault(item => item.Name == str) is ItemPropertyInfo propertyInfo) {
                    return NewFilter(filterPresenter, propertyInfo);
            }
            return null;
        }
        /// <summary>
        /// Create instance of PropertyFilter for  filterPresenter and key, if it is possible.
        /// </summary>
        protected abstract Filter? NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo key);
    }
}
