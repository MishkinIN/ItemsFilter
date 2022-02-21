using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    public partial class VwOrderDetailTable
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? Quantity { get; set; }
        public float? Discount { get; set; }
        public byte[]? RowTimeStamp { get; set; }
    }
}
