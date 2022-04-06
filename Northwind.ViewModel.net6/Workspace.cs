// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using Microsoft.EntityFrameworkCore;
using Northwind.NET.EF6Model;
using Northwind.NET.Sample;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.Windows.Resources;
//using Northwind.NET.Sample.ViewModel;

namespace Northwind.NET.Sample.ViewModel {
    public static class Workspace {
        private static NorthwindNETEntities? northwindModel;
        private static WorkspaceInstance? thisInstance;
        public static WorkspaceInstance This {
            get {
                if (thisInstance == null) {
                    thisInstance = new WorkspaceInstance();
                }
                return thisInstance;
            }
        }
        public static readonly string CallingAssemblyShortName;
        static Workspace() {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            CallingAssemblyShortName = Assembly.GetCallingAssembly().FullName.Split(',')[0];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        public static string ViewModelAssemblyName => Assembly.GetAssembly(typeof(WorkspaceInstance)).FullName.Split(',')[0];
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        public static NorthwindNETEntities NorthwindModel {
            get {
                if (northwindModel == null)
                    northwindModel = new NorthwindNETEntities();
                return Workspace.northwindModel;
            }
        }

        private static ICollection<Product>? products;
        public static ICollection<Product> Products {
            get {
                if (products == null) {
                    try {
                        NorthwindModel.Products
                            .Include(p => p.Category)
                            .Load();
                        products = NorthwindModel.Products.Local;
                    }
                    catch (Exception ex) {
                        //App.LogException(ex);
                        //if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(gestureRecognizer))
                        //    products = new Expression.Blend.SampleData.ProductsSampleDataSource.Products().ProductCollection;
                        //else
                        products = new ObservableCollection<Product>();
                    }
                }
                return products;
            }
        }
        private static ICollection<Category>? categories;
        public static ICollection<Category> Categories {
            get {
                if (categories == null) {
                    //IEnumerable categories;
                    try {
                        NorthwindModel.Categories.Load();
                        categories = NorthwindModel.Categories.Local;
                    }
                    catch (Exception ex) {
                        //App.LogException(ex);
                        //if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(gestureRecognizer))
                        //    categories = new Expression.Blend.SampleData.CategoriesSampleDataSource.Categories().CategoryCollection;
                        //else
                        categories = new ObservableCollection<Category>();
                    }
                }
                return categories;
            }
        }
        private static ICollection<Customer>? customers;
        public static ICollection<Customer> Customers {
            get {
                if (customers == null) {
                    //IEnumerable customers;
                    try {
                        NorthwindModel.Customers.Load();
                        customers = NorthwindModel.Customers.Local;

                    }
                    catch (Exception ex) {
                        //App.LogException(ex);
                        //if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(gestureRecognizer))
                        //    customers = new Expression.Blend.SampleData.CustomersSampleDataSource.Customers().CustomerCollection;
                        //else
                        customers = new ObservableCollection<Customer>();
                    }
                }
                return customers;
            }
        }
        private static ICollection<Employee>? employes;
        public static ICollection<Employee> Employees {
            get {
                if (employes == null) {
                    try {
                        NorthwindModel.Employees.Load();
                        employes = NorthwindModel.Employees.Local;
                    }
                    catch (Exception ex) {
                        //App.LogException(ex);
                        employes = new ObservableCollection<Employee>();
                    }
                }
                return employes;
            }
        }
        private static CustomersTreeVm? customersTreeList;
        public static CustomersTreeVm CustomersTreeList {
            get {

                try {
                    if (customersTreeList == null) {
                        var listCustomersQuery = from cust in NorthwindModel.Customers
                                                 orderby cust.Country
                                                 group cust by cust.Country into countryGroup
                                                 select new CountryCustomersTreeItem {
                                                     Country = countryGroup.Key,
                                                     Count = countryGroup.Count(),
                                                     Cities = (
                                                     from country in countryGroup
                                                     group country by country.City into sityGroup
                                                     select new CityCustomersTreeItem {
                                                         City = sityGroup.Key,
                                                         Count = sityGroup.Count(),
                                                         Customers = (from sity in sityGroup
                                                                      select sity).ToList()
                                                     }
                                                     ).ToList()
                                                 };
                        customersTreeList = new CustomersTreeVm(listCustomersQuery.ToList());
                    }
                }
                catch (Exception ex) {
                    //App.LogException(ex);
                    customersTreeList = new CustomersTreeVm(Array.Empty<CountryCustomersTreeItem>());
                }

                return customersTreeList;
            }
        }

        private static List<Supplier>? suppliers;
        public static List<Supplier> Suppliers {
            get {
                if (suppliers == null) {
                    try {
                        suppliers = NorthwindModel.Suppliers.ToList();
                    }
                    catch (Exception ex) {
                        //App.LogException(ex);
                        suppliers = new List<Supplier>();
                    }
                }
                return suppliers;
            }
        }

        private static ICollection<Order>? orders;
        public static ICollection<Order> Orders {
            get {
                if (orders == null) {
                    try {
                        NorthwindModel.Orders.Include(nameof(WorkspaceInstance.OrderDetails)).Load();
                        orders = NorthwindModel.Orders.Local;
                    }
                    catch (Exception ex) {
                        //App.LogException(ex);
                        orders = new ObservableCollection<Order>();
                    }
                }
                return orders;
            }
        }


        internal static WorkspaceInstance LoadInstance(WorkspaceInstance instance) {
            try {
                System.Uri resourceUri = new($"/{Workspace.ViewModelAssemblyName};component/WorkspaceInstance.xaml", System.UriKind.Relative);
                //System.Uri resourceUri = new System.Uri($"ms-appx:///SampleData/Workspace.xaml");
                if (System.Windows.Application.GetResourceStream(resourceUri) is StreamResourceInfo sri) {
                    System.Windows.Application.LoadComponent(instance, resourceUri);
                    if (instance.Products != null && instance.Categories != null && instance.Suppliers != null) {
                        foreach (Product product in instance.Products) {
                            Category? category = instance.Categories.Where(cat => cat.Id == product.CategoryId).FirstOrDefault();
                            if (category != null) {
                                product.Category = category;
                                category.Products.Add(product);
                            }
                            Supplier? supplier = instance.Suppliers.Where(suppl => suppl.Id == product.SupplierId).FirstOrDefault();
                            if (supplier != null) {
                                supplier.Products.Add(product);
                                product.Supplier = supplier;
                            }
                        }
                    }
                    if (instance.Products != null && instance.OrderDetails != null && instance.Orders != null) {
                        foreach (OrderDetail orderDetail in instance.OrderDetails) {
                            Product? product = instance.Products.Where(prod => prod.Id == orderDetail.ProductId).FirstOrDefault();
                            if (product != null) {
                                product.OrderDetails.Add(orderDetail);
                                orderDetail.Product = product;
                            }
                            Order? order = instance.Orders.Where(ord => ord.Id == orderDetail.OrderId).FirstOrDefault();
                            if (order != null) {
                                order.OrderDetails.Add(orderDetail);
                                orderDetail.Order = order;
                            }
                        }
                    }
                    if (instance.Employees != null && instance.Customers != null && instance.Orders != null) {
                        foreach (Order order in instance.Orders) {
                            Customer? customer = instance.Customers.Where(cust => cust.Id == order.CustomerId).FirstOrDefault();
                            if (customer != null) {
                                customer.Orders.Add(order);
                                order.Customer = customer;
                            }
                            Employee? employee = instance.Employees.Where(empl => empl.Id == order.EmployeeId).FirstOrDefault();
                            if (employee != null) {
                                employee.Orders.Add(order);
                                order.Employee = employee;
                            }
                        }
                    }
                    CustomersTreeVm customersTreeList;

                    var listCustomersQuery = from cust in instance.Customers
                                             orderby cust.Country
                                             group cust by cust.Country into countryGroup
                                             select new CountryCustomersTreeItem {
                                                 Country = countryGroup.Key,
                                                 Count = countryGroup.Count(),
                                                 Cities = (
                                                 from country in countryGroup
                                                 group country by country.City into sityGroup
                                                 select new CityCustomersTreeItem {
                                                     City = sityGroup.Key,
                                                     Count = sityGroup.Count(),
                                                     Customers = (from sity in sityGroup
                                                                  select sity).ToList()
                                                 }
                                                 ).ToList()
                                             };
                    customersTreeList = new CustomersTreeVm(listCustomersQuery.ToList());

                    instance.CustomersTreeList = customersTreeList;

                }
            }
            catch (System.Exception ex) {
                //App.LogException(ex);
            }
            return instance;
        }
    }
}
