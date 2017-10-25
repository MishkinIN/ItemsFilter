// ****************************************************************************
// <author>Jonas Tampier</author>
// <email>jonas@tampier.de</email>
// <date>06.10.2017</date>
// <project>ItemsFilter</project>
// <license> GNU Lesser General Public License version 3 (LGPLv3) </license>
// ****************************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace BolapanControl.ItemsFilter.Model
{
    /// <summary>
    /// Defines the contract for PropertyPath filter.
    /// </summary>
    public interface IPropertyPathFilter : IFilter
    {
        /// <summary>
        /// Gets the property path that use to get property value from item.
        /// </summary>
        /// <value>The property path.</value>
        string PropertyPath
        {
            get;
        }
    }
}
