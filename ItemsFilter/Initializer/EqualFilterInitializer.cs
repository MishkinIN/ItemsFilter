using System;
using System.Linq;
using System.ComponentModel;
using BolapanControl.ItemsFilter.Model;
using System.Collections;
using System.Diagnostics;

namespace BolapanControl.ItemsFilter.Initializer {
    /// <summary>
    /// Define EqualFilter initializer.
    /// </summary>
    public class EqualFilterInitializer : PropertyFilterInitializer {
        private const string _filterName = "Equality";
        
        protected override PropertyFilter NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo) {
            Debug.Assert(filterPresenter != null);
            Debug.Assert(propertyInfo != null);

            Type propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && !propertyType.IsEnum
                )
            {
                PropertyFilter filter= (PropertyFilter)Activator.CreateInstance(
                    typeof(EqualFilter<>).MakeGenericType(propertyInfo.PropertyType),
                    propertyInfo,
                    GetAvailableValuesQuery(filterPresenter, propertyInfo)
                );
                return filter;
            }
            return null;
        }
        //TODO: Translate 
        // Возвращает запрос, возвращающий уникальные значения свойства членов коллекции ItemsSource.
        /// <summary>
        /// Returns query for the available property values in ItemsSource.
        /// </summary>
        public static IEnumerable GetAvailableValuesQuery(FilterPresenter filterPresenter, ItemPropertyInfo propInfo) {
              IEnumerable source = filterPresenter.CollectionView.SourceCollection;
              if (source == null)
                return new object[0];
            var propertyDescriptor = propInfo.Descriptor as PropertyDescriptor;
            var sourceQuery = source.OfType<object>().Select(item => propertyDescriptor.GetValue(item));
            var propType = propertyDescriptor.PropertyType;
            if (typeof(IComparable).IsAssignableFrom(propType)) {
                sourceQuery = sourceQuery.OrderBy(item => item);
            }
            else
                sourceQuery = sourceQuery.OrderBy(item =>item==null?"": item.ToString());
            sourceQuery = sourceQuery.Distinct();
            return sourceQuery;
        }
    }
}
