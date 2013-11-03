using BolapanControl.ItemsFilter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BolapanControl.ItemsFilter.Initializer
{
    //TODO: Translate: Инициализатор фильтра для свойства типа Enum.
    
    public class EnumFilterInitializer:PropertyFilterInitializer
    {
        //TODO: Fill xml comment from base type.
        protected override PropertyFilter NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo)
        {
            Debug.Assert(filterPresenter != null);
            Debug.Assert(propertyInfo != null);
            Type propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && propertyType.IsEnum
                )
            {
                return (PropertyFilter)Activator.CreateInstance(typeof(EnumFilter<>).MakeGenericType(propertyInfo.PropertyType), propertyInfo);
            }
            return null;
        }
    }
}
