using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.NET.Sample.SampleData {
    class Employees : List<Northwind.NET.EF6Model.Employee>, IList<Northwind.NET.EF6Model.Employee> {
    }
}
