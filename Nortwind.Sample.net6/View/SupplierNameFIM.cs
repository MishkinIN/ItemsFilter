// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;

namespace Northwind.NET.Sample.View {
    public class SupplierNameFIM:BolapanControl.ItemsFilter.Initializer.FilterInitializersManager {
        public SupplierNameFIM() {
            Add(new SupplierNameStringFilterInitializer());
        }
    }
    public class SupplierNameStringFilter : StringFilter {
        public SupplierNameStringFilter()
            : base(NameGetter) {

    }
        private static string NameGetter(object val) {
            return ((Northwind.NET.EF6Model.Product)val).Supplier.Name;
        }
    }
    class SupplierNameStringFilterInitializer : FilterInitializer {
        public override Filter TryGetFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            if (key is string && (string)key == "Supplier.Name") {
                return new SupplierNameStringFilter();
            }
            return null;
        }
    }
}
