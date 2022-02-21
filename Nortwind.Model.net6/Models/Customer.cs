using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    /// <summary>
    /// Customers&apos; names, addresses, and phone numbers.
    /// </summary>
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        /// <summary>
        /// !NEW
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Unique five-character code based on customer name.
        /// </summary>
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        /// <summary>
        /// Street or post-office box.
        /// </summary>
        public string? Address { get; set; }
        public string? City { get; set; }
        /// <summary>
        /// State or province.
        /// </summary>
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        /// <summary>
        /// Phone number includes country code or area code.
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// Phone number includes country code or area code.
        /// </summary>
        public string? Fax { get; set; }
        public byte[]? RowTimeStamp { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
