using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    /// <summary>
    /// Customer name, order date, and freight charge for each order.
    /// </summary>
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }

        /// <summary>
        /// Unique order number.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// !New
        /// </summary>
        public int? CustomerId { get; set; }
        /// <summary>
        /// Same entry as in Employees table.
        /// </summary>
        public int? EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        /// <summary>
        /// Same as Shipper ID in Shippers table.
        /// </summary>
        public int? ShipperId { get; set; }
        public decimal? Freight { get; set; }
        /// <summary>
        /// Name of person or company to receive the shipment.
        /// </summary>
        public string? ShipName { get; set; }
        /// <summary>
        /// Street address only -- no post-office box allowed.
        /// </summary>
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        /// <summary>
        /// State or province.
        /// </summary>
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }
        public byte[]? RowTimeStamp { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Shipper? Shipper { get; set; }
        public virtual IList<OrderDetail> OrderDetails { get; set; }
    }
}
