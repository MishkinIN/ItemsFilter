using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    public partial class VwAlphabeticalListOfProductsReport
    {
        public string? SortGroupLetter { get; set; }
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public string? QuantityPerUnit { get; set; }
        public short? UnitsInStock { get; set; }
    }
}
