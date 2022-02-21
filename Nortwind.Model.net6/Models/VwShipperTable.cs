using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    public partial class VwShipperTable
    {
        public int ShipperId { get; set; }
        public string? CompanyName { get; set; }
        public string? Phone { get; set; }
        public byte[]? RowTimeStamp { get; set; }
    }
}
