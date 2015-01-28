// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BolapanControl.ItemsFilter.Model
{
    /// <summary>
    /// Base class for filter that use property of item.
    /// </summary>
    public abstract class PropertyFilter : Filter, IPropertyFilter
    {
        private ItemPropertyInfo _propertyInfo;
        
        /// <summary>
        /// Gets the property info whose property name is filtered.
        /// </summary>
        /// <value>The property info.</value>
        public ItemPropertyInfo PropertyInfo {
            get { return _propertyInfo; }
            protected set {
                _propertyInfo = value;
            }
        }
    }
}
