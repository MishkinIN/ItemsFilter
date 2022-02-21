using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Northwind.NET.EF6Model
{
    public partial class NorthwindNETEntities : DbContext
    {
        public NorthwindNETEntities()
        {
        }

        public NorthwindNETEntities(DbContextOptions<NorthwindNETEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Shipper> Shippers { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<VwAlphabeticalListOfProductsReport> VwAlphabeticalListOfProductsReports { get; set; } = null!;
        public virtual DbSet<VwCategoryTable> VwCategoryTables { get; set; } = null!;
        public virtual DbSet<VwCustomerCountry> VwCustomerCountries { get; set; } = null!;
        public virtual DbSet<VwCustomerOrderTotal> VwCustomerOrderTotals { get; set; } = null!;
        public virtual DbSet<VwCustomerOrdersProduct> VwCustomerOrdersProducts { get; set; } = null!;
        public virtual DbSet<VwCustomerTable> VwCustomerTables { get; set; } = null!;
        public virtual DbSet<VwEmployeeTable> VwEmployeeTables { get; set; } = null!;
        public virtual DbSet<VwOrderDetailTable> VwOrderDetailTables { get; set; } = null!;
        public virtual DbSet<VwOrderTable> VwOrderTables { get; set; } = null!;
        public virtual DbSet<VwProductTable> VwProductTables { get; set; } = null!;
        public virtual DbSet<VwShipperTable> VwShipperTables { get; set; } = null!;
        public virtual DbSet<VwSupplierTable> VwSupplierTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=NORTHWIND;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaCategory_PK")
                    .IsClustered(false);

                entity.ToTable("Category");

                entity.HasComment("Categories of Northwind products.");

                entity.HasIndex(e => e.Name, "CategoryName")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("Number automatically assigned to a new category.");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Name)
                    .HasMaxLength(15)
                    .HasComment("Name of food category.");

                entity.Property(e => e.Picture)
                    .HasColumnType("image")
                    .HasComment("A picture representing the food category.");

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaCustomer_PK")
                    .IsClustered(false);

                entity.ToTable("Customer");

                entity.HasComment("Customers' names, addresses, and phone numbers.");

                entity.HasIndex(e => e.City, "City");

                entity.HasIndex(e => e.Name, "CompanyName");

                entity.HasIndex(e => e.Id, "CustomerId");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.HasIndex(e => e.Region, "Region");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("!NEW");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasComment("Street or post-office box.");

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .HasComment("Unique five-character code based on customer name.");

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Fax)
                    .HasMaxLength(24)
                    .HasComment("Phone number includes country code or area code.");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .HasComment("Phone number includes country code or area code.");

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .HasComment("State or province.");

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaEmployee_PK")
                    .IsClustered(false);

                entity.ToTable("Employee");

                entity.HasComment("Employees' names, titles, and personal information.");

                entity.HasIndex(e => e.LastName, "LastName");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("Number automatically assigned to new employee.");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasComment("Street or post-office box.");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Extension)
                    .HasMaxLength(4)
                    .HasComment("Internal telephone extension number.");

                entity.Property(e => e.FirstName).HasMaxLength(10);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(24)
                    .HasComment("Phone number includes country code or area code.");

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.Notes)
                    .HasColumnType("ntext")
                    .HasComment("General information about employee's background.");

                entity.Property(e => e.Photo)
                    .HasMaxLength(255)
                    .HasComment("Picture of employee.");

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .HasComment("State or province.");

                entity.Property(e => e.ReportsTo).HasComment("Employee's supervisor.");

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .HasComment("Employee's title.");

                entity.Property(e => e.TitleOfCourtesy)
                    .HasMaxLength(25)
                    .HasComment("Title used in salutations.");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaOrder_PK")
                    .IsClustered(false);

                entity.ToTable("Order");

                entity.HasComment("Customer name, order date, and freight charge for each order.");

                entity.HasIndex(e => e.CustomerId, "CustomerId1");

                entity.HasIndex(e => e.CustomerId, "CustomerOrder");

                entity.HasIndex(e => e.EmployeeId, "EmployeeID");

                entity.HasIndex(e => e.OrderDate, "OrderDate");

                entity.HasIndex(e => e.ShipPostalCode, "ShipPostalCode");

                entity.HasIndex(e => e.ShippedDate, "ShippedDate");

                entity.HasIndex(e => e.EmployeeId, "{9B9FC10C-681E-4F2D-84F2-2736368FE925}");

                entity.HasIndex(e => e.ShipperId, "{B06018A6-1EDE-4A8F-9A0E-53D679E60CA3}");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("Unique order number.");

                entity.Property(e => e.CustomerId).HasComment("!New");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("EmployeeID")
                    .HasComment("Same entry as in Employees table.");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.ShipAddress)
                    .HasMaxLength(60)
                    .HasComment("Street address only -- no post-office box allowed.");

                entity.Property(e => e.ShipCity).HasMaxLength(15);

                entity.Property(e => e.ShipCountry).HasMaxLength(15);

                entity.Property(e => e.ShipName)
                    .HasMaxLength(40)
                    .HasComment("Name of person or company to receive the shipment.");

                entity.Property(e => e.ShipPostalCode).HasMaxLength(10);

                entity.Property(e => e.ShipRegion)
                    .HasMaxLength(15)
                    .HasComment("State or province.");

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.Property(e => e.ShipperId).HasComment("Same as Shipper ID in Shippers table.");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("CustomerOrders");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("EmployeeOrders");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("ShipperOrders");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaOrderDetail_PK")
                    .IsClustered(false);

                entity.ToTable("OrderDetail");

                entity.HasComment("Details on products, quantities, and prices for each order in the Orders table.");

                entity.HasIndex(e => e.Id, "OrderDetailId");

                entity.HasIndex(e => e.OrderId, "OrderID");

                entity.HasIndex(e => e.ProductId, "ProductID");

                entity.HasIndex(e => e.OrderId, "{08438758-FFD3-438D-9FA0-30F2DAA20241}");

                entity.HasIndex(e => e.ProductId, "{929178D4-BDC9-4A67-AEEA-B2292324D0FB}");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("!NEW");

                entity.Property(e => e.Discount).HasDefaultValueSql("((0))");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasComment("Same as Order ID in Orders table.");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasComment("Same as Product ID in Products table.");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("OrderDetails");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("ProductOrders");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaProduct_PK")
                    .IsClustered(false);

                entity.ToTable("Product");

                entity.HasComment("Product names, suppliers, prices, and units in stock.");

                entity.HasIndex(e => e.CategoryId, "CategoryID");

                entity.HasIndex(e => e.Name, "ProductName");

                entity.HasIndex(e => e.SupplierId, "SupplierID");

                entity.HasIndex(e => e.CategoryId, "{1D914383-2CC0-4E61-8FCE-8720068188CF}");

                entity.HasIndex(e => e.SupplierId, "{A022239F-4099-4E97-97EF-AF5A49A4C4D8}");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("Number automatically assigned to new product.");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasComment("Same entry as in Categories table.");

                entity.Property(e => e.Discontinued)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Yes means item is no longer available.");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.QuantityPerUnit)
                    .HasMaxLength(20)
                    .HasComment("(e.g., 24-count case, 1-liter bottle).");

                entity.Property(e => e.ReorderLevel)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Minimum units to maintain in stock.");

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.SupplierId)
                    .HasColumnName("SupplierID")
                    .HasComment("Same entry as in Suppliers table.");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("CategoryProducts");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("SupplierProducts");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaShipper_PK")
                    .IsClustered(false);

                entity.ToTable("Shipper");

                entity.HasComment("Shippers' names and phone numbers.");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("Number automatically assigned to new shipper.");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .HasComment("Name of shipping company.");

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .HasComment("Phone number includes country code or area code.");

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaSupplier_PK")
                    .IsClustered(false);

                entity.ToTable("Supplier");

                entity.HasComment("Suppliers' names, addresses, phone numbers, and hyperlinks to home pages.");

                entity.HasIndex(e => e.Name, "CompanyName");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("Number automatically assigned to new supplier.");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .HasComment("Street or post-office box.");

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Fax)
                    .HasMaxLength(24)
                    .HasComment("Phone number includes country code or area code.");

                entity.Property(e => e.HomePage)
                    .HasColumnType("ntext")
                    .HasComment("Supplier's home page on World Wide Web.");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .HasComment("Phone number includes country code or area code.");

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .HasComment("State or province.");

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<VwAlphabeticalListOfProductsReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_AlphabeticalListOfProductsReport");

                entity.Property(e => e.CategoryName).HasMaxLength(15);

                entity.Property(e => e.ProductName).HasMaxLength(40);

                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

                entity.Property(e => e.SortGroupLetter).HasMaxLength(1);
            });

            modelBuilder.Entity<VwCategoryTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_CategoryTable");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(15);

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Picture).HasColumnType("image");

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<VwCustomerCountry>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_CustomerCountries");

                entity.Property(e => e.Country).HasMaxLength(15);
            });

            modelBuilder.Entity<VwCustomerOrderTotal>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_CustomerOrderTotals");

                entity.Property(e => e.Freight).HasColumnType("money");
            });

            modelBuilder.Entity<VwCustomerOrdersProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_CustomerOrdersProducts");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<VwCustomerTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_CustomerTable");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName).HasMaxLength(40);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.CustomerCode).HasMaxLength(5);

                entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<VwEmployeeTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_EmployeeTable");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Extension).HasMaxLength(4);

                entity.Property(e => e.FirstName).HasMaxLength(10);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.HomePhone).HasMaxLength(24);

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.Notes).HasColumnType("ntext");

                entity.Property(e => e.Photo).HasMaxLength(255);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Title).HasMaxLength(30);

                entity.Property(e => e.TitleOfCourtesy).HasMaxLength(25);
            });

            modelBuilder.Entity<VwOrderDetailTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_OrderDetailTable");

                entity.Property(e => e.OrderDetailId).ValueGeneratedOnAdd();

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<VwOrderTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_OrderTable");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Freight).HasColumnType("money");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OrderID");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.ShipAddress).HasMaxLength(60);

                entity.Property(e => e.ShipCity).HasMaxLength(15);

                entity.Property(e => e.ShipCountry).HasMaxLength(15);

                entity.Property(e => e.ShipName).HasMaxLength(40);

                entity.Property(e => e.ShipPostalCode).HasMaxLength(10);

                entity.Property(e => e.ShipRegion).HasMaxLength(15);

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwProductTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_ProductTable");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ProductID");

                entity.Property(e => e.ProductName).HasMaxLength(40);

                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<VwShipperTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_ShipperTable");

                entity.Property(e => e.CompanyName).HasMaxLength(40);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.ShipperId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ShipperID");
            });

            modelBuilder.Entity<VwSupplierTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_SupplierTable");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName).HasMaxLength(40);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.HomePage).HasColumnType("ntext");

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.RowTimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.SupplierId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SupplierID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
