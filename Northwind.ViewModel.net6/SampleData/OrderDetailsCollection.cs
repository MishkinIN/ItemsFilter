using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.NET.Sample.SampleData {
    public class OrderDetailsCollection : List<Northwind.NET.EF6Model.OrderDetail>, IList<Northwind.NET.EF6Model.OrderDetail> {
    }
}
