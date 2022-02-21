using System;
using System.Collections.Generic;

namespace Northwind.NET.EF6Model
{
    /// <summary>
    /// Categories of Northwind products.
    /// </summary>
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        /// <summary>
        /// Number automatically assigned to a new category.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of food category.
        /// </summary>
        public string? Name { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// A picture representing the food category.
        /// </summary>
        public byte[]? Picture { get; set; }
        public byte[]? RowTimeStamp { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
