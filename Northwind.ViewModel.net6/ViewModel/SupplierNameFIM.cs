// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using Northwind.NET.EF6Model;

namespace Northwind.NET.Sample.ViewModel {
    public class SupplierNameFIM:FilterInitializersManager {
        public SupplierNameFIM() {
            Add(new SupplierNameStringFilterInitializer());
        }
    }
    public class SupplierNameStringFilter : StringFilter {
        public SupplierNameStringFilter()
            : base(NameGetter) {

    }
        private static string? NameGetter(object? val) {
            return (val as Product)?.Supplier?.Name;
        }
    }
    class SupplierNameStringFilterInitializer : FilterInitializer {
        public override Filter? TryCreateFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key) {
            if (key is string @string && @string == "Supplier.Name") {
                return new SupplierNameStringFilter();
            }
            return null;
        }
    }
}
