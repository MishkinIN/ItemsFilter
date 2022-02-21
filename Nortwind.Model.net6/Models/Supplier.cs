using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    /// <summary>
    /// Suppliers&apos; names, addresses, phone numbers, and hyperlinks to home pages.
    /// </summary>
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        /// <summary>
        /// Number automatically assigned to new supplier.
        /// </summary>
        public int Id { get; set; }
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
        /// <summary>
        /// Supplier&apos;s home page on World Wide Web.
        /// </summary>
        public string? HomePage { get; set; }
        public byte[]? RowTimeStamp { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
