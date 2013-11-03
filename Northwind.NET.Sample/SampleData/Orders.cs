using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.NET.Sample.SampleData {
    class Orders : List<Northwind.NET.EF6Model.Order>, IList<Northwind.NET.EF6Model.Order> {
    }
}
