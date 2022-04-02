// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace BolapanControl.ItemsFilter.Model {
    /// <summary>
    /// Specify the [ModelView] class that present model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewAttribute : Attribute {
        private readonly Type viewType;

        public Type ViewType {
            get { return viewType; }
        }

        public ViewAttribute(Type ViewType) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(typeof(FrameworkElement).IsAssignableFrom(ViewType));
#endif
            viewType = ViewType;
        }
        public ViewAttribute(string typeName) {
            Assembly? assembly = Assembly.GetEntryAssembly()?? Assembly.GetEntryAssembly();
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(assembly);
#endif
            var type= assembly?.GetType(typeName);
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(type);
#endif
            viewType = type;
        }
    }
}
