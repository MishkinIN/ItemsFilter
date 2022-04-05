// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;
using System;
using System.Diagnostics;

namespace BolapanControl.ItemsFilter.Initializer {
    /// <summary>
    /// Define ValueFilter initializer.
    /// </summary>
    public class ObjectFilterInitializer : FilterInitializer {

        /// <summary>
        /// Generate new instance of Filter class, if it is possible for filterPresenter and key.
        /// </summary>
        /// <param name="filterPresenter">FilterPresenter, which can be attached Filter</param>
        /// <param name="key">Key for generated Filter. For PropertyFilter, key used as the name for binding property in filterPresenter.Parent collection.</param>
        /// <returns>Instance of Filter class or null.</returns>
        public override Model.Filter TryCreateFilter(FilterPresenter filterPresenter, object key) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(filterPresenter);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(key); 
#endif
            ObjectEqualFilter filter = new(item => item, filterPresenter.CollectionView.SourceCollection);
            filter.Attach(filterPresenter);
            return filter;
        }
        /// <summary>
        /// Generate new instance of Filter class, if it is possible for filterPresenter and key.
        /// </summary>
        /// <param name="filterPresenter">FilterPresenter, which can be attached Filter</param>
        /// <param name="key">Key for generated Filter. For PropertyFilter, key used as the name for binding property in filterPresenter.Parent collection.</param>
        /// <returns>Instance of Filter class or null.</returns>
#pragma warning disable CA1822 // Mark members as static
        public Model.Filter TrygetFilter<T>(FilterPresenter filterPresenter, T key)
#pragma warning restore CA1822 // Mark members as static
            where T : class {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(filterPresenter);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(key);
#endif
            ObjectEqualFilter filter = new(item => item, filterPresenter.CollectionView.SourceCollection);
            filter.Attach(filterPresenter);
            return filter;
        }
    }
}
