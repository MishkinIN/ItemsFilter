using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.NET.Sample.SampleData {
    class Customers:List<Northwind.NET.EF6Model.Customer>, IList<Northwind.NET.EF6Model.Customer> {
    }
}
