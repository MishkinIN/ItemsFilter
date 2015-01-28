// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace BolapanControl.ItemsFilter.Model {
    /// <summary>
    /// Specify the [ModelView] class that present model.
    /// </summary>
    public class ViewAttribute:Attribute
    {
        private readonly Type viewType;

        public Type ViewType {
            get { return viewType; }
        } 

        public ViewAttribute(Type ViewType = null) {
            Debug.Assert(ViewType == null || typeof(FrameworkElement).IsAssignableFrom(ViewType));
            viewType = ViewType;
        }

    }
}
