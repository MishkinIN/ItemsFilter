using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    /// <summary>
    /// Employees&apos; names, titles, and personal information.
    /// </summary>
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        /// <summary>
        /// Number automatically assigned to new employee.
        /// </summary>
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        /// <summary>
        /// Employee&apos;s title.
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Title used in salutations.
        /// </summary>
        public string? TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
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
        public string? HomePhone { get; set; }
        /// <summary>
        /// Internal telephone extension number.
        /// </summary>
        public string? Extension { get; set; }
        /// <summary>
        /// Picture of employee.
        /// </summary>
        public string? Photo { get; set; }
        /// <summary>
        /// General information about employee&apos;s background.
        /// </summary>
        public string? Notes { get; set; }
        /// <summary>
        /// Employee&apos;s supervisor.
        /// </summary>
        public int? ReportsTo { get; set; }
        public byte[]? RowTimeStamp { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
