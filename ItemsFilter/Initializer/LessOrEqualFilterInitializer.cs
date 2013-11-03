using System;
using System.Linq;
using System.ComponentModel;
using BolapanControl.ItemsFilter.Model;
using System.Diagnostics;

namespace BolapanControl.ItemsFilter.Initializer {
    /// <summary>
    /// Define LessOrEqualFilter initializer.
    /// </summary>
    public class LessOrEqualFilterInitializer : PropertyFilterInitializer {
        private const string _filterName = "Less or Equal";
        /// <summary>
        /// Create LessOrEqualFilter for instance of FilterPresenter, if it is possible.
        /// </summary>
        //TODO: Translate: экземпляр FilterPresenter, для которого должен быть создан фильтр.
        /// <param name="filterPresenter"></param>
        //TODO: Translate: Экземпляр ItemPropertyInfo, указывающий на (идентифицирующий?) свойство, для которого создается фильтр.
        /// <param name="key"></param>
        //TODO: Translate: Экземпляр LessOrEqualFilter, если его создание возможно для переданных значений filterPresenter и propertyInfo, или null.
        /// <returns></returns>
        protected override PropertyFilter NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo) {
            Debug.Assert(filterPresenter != null);
            Debug.Assert(propertyInfo != null);

            //ItemPropertyInfo propertyInfo = (ItemPropertyInfo)key;
            Type propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType)
                && propertyInfo.PropertyType != typeof(String)
                && propertyInfo.PropertyType!= typeof(bool)
                && !propertyType.IsEnum
                )
            {
                return (PropertyFilter)Activator.CreateInstance(typeof(LessOrEqualFilter<>).MakeGenericType(propertyInfo.PropertyType), propertyInfo);
            }
            return null;
        }
    }
}
