// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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

        public ViewAttribute(Type ViewType) {
#if DEBUG
            Assert.IsTrue(typeof(FrameworkElement).IsAssignableFrom(ViewType)); 
#endif
            viewType = ViewType;
        }
        public ViewAttribute(string typeName) {
            Assembly assembly = Assembly.GetCallingAssembly();
            viewType = assembly.GetType(typeName)?? typeof(Control);
        }
    }
}
