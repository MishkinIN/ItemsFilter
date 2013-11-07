using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.View;
using Northwind.NET.EF6Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.NET.Sample.ViewModel {
    
    [View(typeof(StringFilterView))]
    // Define specialized filter for CustomersComboBox.
    public sealed class CustomersComboBoxFilter : StringFilter, IFilter {
        private static StringBuilder sb = new StringBuilder(6);
        internal CustomersComboBoxFilter()
            // To search for combine the values of several properties.
            : base(item => 
            {
                Customer customer = (Customer)item;
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
                return sb.ToString();
            }) {

        }
    }

}
