// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace BolapanControl.ItemsFilter.Initializer {
    /// <summary>
    /// Define EqualFilter initializer.
    /// </summary>
    public class EqualFilterInitializer : FilterInitializer {
#pragma warning disable IDE0051 // Remove unused private members
        private const string _filterName = "Equality";
#pragma warning restore IDE0051 // Remove unused private members

        public override Filter? TryCreateFilter(FilterPresenter filterPresenter, object key) {
            if (key is ItemPropertyInfo info)
                return NewFilter(filterPresenter, info);
            if (key is string str
                && filterPresenter.ItemProperties.FirstOrDefault(item => item.Name == str) is ItemPropertyInfo propertyInfo) {
                return NewFilter(filterPresenter, propertyInfo);
            }
            return null;
        }
        protected static Filter? NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(filterPresenter);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(propertyInfo);
#endif

            Type propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && !propertyType.IsEnum
                ) {
                Type generic = typeof(IEquatable<>);
                Type[] tArgs = new Type[] { propertyType };
                Type iquatableType = generic.MakeGenericType(tArgs);
                Filter? filter = null;
                if (iquatableType.IsAssignableFrom(propertyType)) {
                    filter = Activator.CreateInstance(
                                        typeof(EqualFilter<>).MakeGenericType(propertyType),
                                        propertyInfo,
                                        GetAvailableValuesQuery(filterPresenter, propertyInfo)
                                    ) as Filter;
                }
                else {
                    filter = new ObjectEqualFilter(propertyInfo, GetAvailableValuesQuery(filterPresenter, propertyInfo));
                }
                if (filter != null) {
                    filter.Attach(filterPresenter);
                }
                return filter;
            }
            return null;
        }
        /// <summary>
        /// Returns a query that returns the unique item property values in the ItemsSource collection..
        /// </summary>
        public static IEnumerable GetAvailableValuesQuery(FilterPresenter filterPresenter, ItemPropertyInfo propInfo) {
            IEnumerable source = filterPresenter.CollectionView.SourceCollection;
            if (source == null)
                return Array.Empty<object>();
            var propertyDescriptor = (PropertyDescriptor)(propInfo.Descriptor);
            var sourceQuery = source.OfType<object>().Select(item => propertyDescriptor.GetValue(item));
            var propType = propertyDescriptor.PropertyType;
            if (typeof(IComparable).IsAssignableFrom(propType)) {
                sourceQuery = sourceQuery.OrderBy(item => item);
            }
            else
                sourceQuery = sourceQuery.OrderBy(item => item == null ? string.Empty : item.ToString());
            sourceQuery = sourceQuery.Distinct();
            return sourceQuery;
        }

      
    }
}
