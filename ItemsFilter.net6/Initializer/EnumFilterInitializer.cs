﻿// ****************************************************************************
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
    /// Filter initializer for EnumFilter.
    /// </summary>
    public class EnumFilterInitializer : PropertyFilterInitializer {
        /// <summary>
        /// Generate new instance of EnumFilter class, if it is possible for a pair of filterPresenter and propertyInfo.
        /// </summary>
        /// <param name="filterPresenter">FilterPresenter, which can be attached Filter</param>
        /// <param name="key">Key, used as the name for binding property in filterPresenter.Parent collection.</param>
        /// <returns>Instance of EnumFilter class or null.</returns>
        protected override Filter? NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(filterPresenter);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(propertyInfo);
#endif
            Type propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && propertyType.IsEnum
                ) {
                var filter =  Activator.CreateInstance(typeof(EnumFilter<>).MakeGenericType(propertyInfo.PropertyType), propertyInfo) as EqualFilter;
                if (filter!=null) {
                    filter.Attach(filterPresenter);
                }
                return filter;
            }
            return null;
        }
    }
}
