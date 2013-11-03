using Northwind.NET.EF6Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.NET.Sample.SampleData {
    class CategoriesCollection : List<Northwind.NET.EF6Model.Category>, IList<Northwind.NET.EF6Model.Category> {
    }
}
