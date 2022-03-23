// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using Northwind.NET.EF6Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.NET.Sample.ViewModel {
    public class CustomersTreeVm:IEnumerable<CountryCustomersTreeItem> {
        private readonly IEnumerable<CountryCustomersTreeItem> query;
        public int Count {
            get {
                if (query is CountryCustomersTreeItem[] v)
                    return v.Length;
                else
                    return
                        query.Count();
            }
        }
        public CustomersTreeVm(IEnumerable<CountryCustomersTreeItem> query) {
            this.query = query;
        }
        public IEnumerator<CountryCustomersTreeItem> GetEnumerator() {
            return query.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
    public class CountryCustomersTreeItem {
        public CountryCustomersTreeItem() {
            Cities = Array.Empty<CityCustomersTreeItem>();
        }
        public string? Country { get; set; }
        public int Count { get; set; }
        public IEnumerable<CityCustomersTreeItem> Cities { get; set; }
    }
    public class CityCustomersTreeItem {
        public CityCustomersTreeItem() {
            Customers = Array.Empty<Customer>();
        }
        public string? City { get; set; }
        public int Count { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}
