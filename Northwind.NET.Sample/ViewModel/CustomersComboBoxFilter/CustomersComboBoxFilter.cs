// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.View;
using Northwind.NET.EF6Model;
using System.Text;

namespace Northwind.NET.Sample.ViewModel {
    
    [View(typeof(StringFilterView))]
    // Define specialized filter for CustomersComboBox.
    public sealed class CustomersComboBoxFilter : StringFilter, IFilter {
        private static readonly StringBuilder sb = new();
        internal CustomersComboBoxFilter()
            // To search for combine the values of several properties.
            : base(item => 
            {
                if (item is Customer customer) {
                    sb.Clear();
                    sb.Append(customer.City);
                    sb.Append(',');
                    sb.Append(customer.Code);
                    sb.Append(',');
                    sb.Append(customer.ContactName);
                    sb.Append(',');
                    sb.Append(customer.Country);
                    sb.Append(',');
                    sb.Append(customer.Name);
                    sb.Append(',');
                    sb.Append(customer.Region);
                }
                return sb.ToString();
            }) {

        }
    }

}
