using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    /// <summary>
    /// Shippers&apos; names and phone numbers.
    /// </summary>
    public partial class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        /// <summary>
        /// Number automatically assigned to new shipper.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of shipping company.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Phone number includes country code or area code.
        /// </summary>
        public string? Phone { get; set; }
        public byte[]? RowTimeStamp { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
