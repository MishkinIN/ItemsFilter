using Northwind.NET.EF6Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.NET.Sample.ViewModel {
    public class CustomersTreeVm:IEnumerable<CountryCustomersTreeItem> {
        private IEnumerable<CountryCustomersTreeItem> query;
        public int Count {
            get {
                if (query is CountryCustomersTreeItem[])
                    return ((CountryCustomersTreeItem[])query).Length;
                else
                    return
                        query.Count();
            }
        }
        internal CustomersTreeVm(IEnumerable<CountryCustomersTreeItem> query) {
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
        public string Country { get; set; }
        public int Count { get; set; }
        public IEnumerable<CityCustomersTreeItem> Cities { get; set; }
    }
    public class CityCustomersTreeItem {
        public string City { get; set; }
        public int Count { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}
