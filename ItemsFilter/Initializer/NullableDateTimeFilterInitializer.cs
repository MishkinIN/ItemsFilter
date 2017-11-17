// ****************************************************************************
// <author>Jonas Tampier</author>
// <email>jonas@tampier.de</email>
// <date>02.10.2017</date>
// <project>ItemsFilter</project>
// <license> GNU Lesser General Public License version 3 (LGPLv3) </license>
// based on code from Ivan Mishkin
// ****************************************************************************
using System;
using System.ComponentModel;
using BolapanControl.ItemsFilter.Model;
using System.Diagnostics;

namespace BolapanControl.ItemsFilter.Initializer {
    /// <summary>
    /// Define DateTimeFilter initializer.
    /// </summary>
    public class NullableDateTimeFilterInitializer : PropertyFilterInitializer {
        #region IPropertyFilterInitializer Members

        protected override PropertyFilter NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo) {
            Debug.Assert(filterPresenter != null);
            Debug.Assert(propertyInfo != null);

            Type propertyType = propertyInfo.PropertyType;
            Type genericArgument = null;
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                genericArgument = propertyType.GetGenericArguments()[0];

            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && genericArgument == typeof(DateTime)
                )
            {
                return (PropertyFilter)Activator.CreateInstance(typeof(NullableDateTimeFilter), propertyInfo);
            }
            return null;
        }

        #endregion
    }
}
