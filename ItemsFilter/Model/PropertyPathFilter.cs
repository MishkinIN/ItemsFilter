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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Web.UI;

namespace BolapanControl.ItemsFilter.Model
{
    /// <summary>
    /// Base class for filter that use property of item.
    /// </summary>
    public abstract class PropertyPathFilter : Filter, IPropertyPathFilter
    {
        private string _propertyPath;
        
        /// <summary>
        /// Gets the property path which is filtered.
        /// </summary>
        /// <value>The property path.</value>
        public string PropertyPath {
            get { return _propertyPath; }
            protected set {
                _propertyPath = value;
            }
        }

        protected object GetValue(object source)
        {
            if (PropertyPath == null)
                return null;
            return BindingEvaluator.Eval(source, _propertyPath);
        }

    }
}
