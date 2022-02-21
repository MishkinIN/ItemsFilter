using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    public partial class VwCategoryTable
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }
        public byte[]? RowTimeStamp { get; set; }
    }
}
