using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.NET.Sample.SampleData {
    class Products : List<Northwind.NET.EF6Model.Product>, IList<Northwind.NET.EF6Model.Product> {
    }
}
