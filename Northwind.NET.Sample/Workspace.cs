// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using Northwind.NET.EF6Model;
using Northwind.NET.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;

namespace Northwind.NET.Sample {
    public static class Workspace {
        private static NorthwindNETEntities northwindModel;
        private static WorkspaceInstance thisInstance;
        public static WorkspaceInstance This {
            get {
                if (thisInstance == null)
                    thisInstance = Application.Current.Resources["Workspace"] as WorkspaceInstance;
                return thisInstance;
            }
        }

        internal static NorthwindNETEntities NorthwindModel {
            get {
                if (northwindModel == null)
                    northwindModel = new NorthwindNETEntities();
                return Workspace.northwindModel;
            }
        }

        private static ObservableCollection<Product> products;
        internal static ObservableCollection<Product> Products {
            get {
                if (products == null) {
                    try {
                        NorthwindModel.Products.Include("Category").Load();
                        products = NorthwindModel.Products.Local;
                    }
                    catch (Exception ex) {
                        App.LogException(ex);
                        //if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(gestureRecognizer))
                        //    products = new Expression.Blend.SampleData.ProductsSampleDataSource.Products().ProductCollection;
                        //else
                        products = new ObservableCollection<Product>();
                    }
                }
                return products;
            }
        }
        private static ObservableCollection<Category> categories;
        internal static ObservableCollection<Category> Categories {
            get {
                if (categories == null) {
                    //IEnumerable categories;
                    try {
                        NorthwindModel.Categories.Load();
                        categories = NorthwindModel.Categories.Local;
                    }
                    catch (Exception ex) {
                        App.LogException(ex);
                        //if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(gestureRecognizer))
                        //    categories = new Expression.Blend.SampleData.CategoriesSampleDataSource.Categories().CategoryCollection;
                        //else
                        categories = new ObservableCollection<Category>();
                    }
                }
                return categories;
            }
        }
        private static ObservableCollection<Customer> customers;
        internal static ObservableCollection<Customer> Customers {
            get {
                if (customers == null) {
                    //IEnumerable customers;
                    try {
                        NorthwindModel.Customers.Load();
                        customers = NorthwindModel.Customers.Local;

                    }
                    catch (Exception ex) {
                        App.LogException(ex);
                        //if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(gestureRecognizer))
                        //    customers = new Expression.Blend.SampleData.CustomersSampleDataSource.Customers().CustomerCollection;
                        //else
                        customers = new ObservableCollection<Customer>();
                    }
                }
                return customers;
            }
        }
        private static ObservableCollection<Employee> employes;
        internal static ObservableCollection<Employee> Employees {
            get {
                if (employes == null) {
                    try {
                        NorthwindModel.Employees.Load();
                        employes = NorthwindModel.Employees.Local;
                    }
                    catch (Exception ex) {
                        App.LogException(ex);
                        employes = new ObservableCollection<Employee>();
                    }
                }
                return employes;
            }
        }
        private static CustomersTreeVm customersTreeList;
        internal static CustomersTreeVm CustomersTreeList {
            get {

                try {
                    if (customersTreeList == null) {
                        var listCustomersQuery = from cust in NorthwindModel.Customers
                                                 orderby cust.Country
                                                 group cust by cust.Country into countryGroup
                                                 select new CountryCustomersTreeItem
                                                 {
                                                     Country = countryGroup.Key,
                                                     Count = countryGroup.Count(),
                                                     Cities = (
                                                     from country in countryGroup
                                                     group country by country.City into sityGroup
                                                     select new CityCustomersTreeItem
                                                     {
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
                    App.LogException(ex);
                    customersTreeList = new CustomersTreeVm(new CountryCustomersTreeItem[0]);
                }

                return customersTreeList;
            }
        }

        private static List<Supplier> suppliers;
        internal static List<Supplier> Suppliers {
            get {
                if (suppliers == null) {
                    try {
                        suppliers = NorthwindModel.Suppliers.ToList();
                    }
                    catch (Exception ex) {
                        App.LogException(ex);
                        suppliers = new List<Supplier>();
                    }
                }
                return suppliers;
            }
        }

        private static ObservableCollection<Order> orders;
        internal static ObservableCollection<Order> Orders {
            get {
                if (orders == null) {
                    try {
                        NorthwindModel.Orders.Include("OrderDetails").Load();
                        orders = NorthwindModel.Orders.Local;
                    }
                    catch (Exception ex) {
                        App.LogException(ex);
                        orders = new ObservableCollection<Order>();
                    }
                }
                return orders;
            }
        }
       

        internal static WorkspaceInstance LoadInstance(WorkspaceInstance instance) {
            try {
                System.Uri resourceUri = new System.Uri("/Northwind.NET.Sample;component/SampleData/Workspace.xaml", System.UriKind.Relative);
                if (System.Windows.Application.GetResourceStream(resourceUri) != null) {
                    System.Windows.Application.LoadComponent(instance, resourceUri);
                    foreach (Product product in instance.Products) {
                        Category category = instance.Categories.Where(cat => cat.Id == product.CategoryId).FirstOrDefault();
                        if (category != null) {
                            product.Category = category;
                            category.Products.Add(product);
                        }
                        Supplier supplier = instance.Suppliers.Where(suppl => suppl.Id == product.SupplierId).FirstOrDefault();
                        if (supplier != null) {
                            supplier.Products.Add(product);
                            product.Supplier = supplier;
                        }
                    }
                    foreach (OrderDetail orderDetail in instance.OrderDetails) {
                        Product product = instance.Products.Where(prod => prod.Id == orderDetail.ProductId).FirstOrDefault();
                        if (product != null) {
                            product.OrderDetails.Add(orderDetail);
                            orderDetail.Product = product;
                        }
                        Order order = instance.Orders.Where(ord => ord.Id == orderDetail.OrderId).FirstOrDefault();
                        if (order != null) {
                            order.OrderDetails.Add(orderDetail);
                            orderDetail.Order = order;
                        }
                    }
                    foreach (Order order in instance.Orders) {
                        Customer customer = instance.Customers.Where(cust => cust.Id == order.CustomerId).FirstOrDefault();
                        if (customer != null) {
                            customer.Orders.Add(order);
                            order.Customer = customer;
                        }
                        Employee employee = instance.Employees.Where(empl => empl.Id == order.EmployeeId).FirstOrDefault();
                        if (employee != null) {
                            employee.Orders.Add(order);
                            order.Employee = employee;
                        }
                    }
                    CustomersTreeVm customersTreeList;

                    var listCustomersQuery = from cust in instance.Customers
                                             orderby cust.Country
                                             group cust by cust.Country into countryGroup
                                             select new CountryCustomersTreeItem
                                             {
                                                 Country = countryGroup.Key,
                                                 Count = countryGroup.Count(),
                                                 Cities = (
                                                 from country in countryGroup
                                                 group country by country.City into sityGroup
                                                 select new CityCustomersTreeItem
                                                 {
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
                App.LogException(ex);
            }
            return instance;
        }

    }
    public class WorkspaceInstance{
        #region fields
        private IList<Employee> _Employees;
        private IList<Order> _Orders;
        private CustomersTreeVm _CustomersTreeList;
        private IList<Supplier> _Suppliers;
        private IList<Customer> _Customers;
        private IList<Category> _Categories;
        private IList<Product> _Products;
        private IList<OrderDetail> _OrderDetails;
        #endregion
        public WorkspaceInstance() {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject())) {
                Workspace.LoadInstance(this);
            }
            else {
                System.Data.Common.DbConnection connection = null;
                try {
                    connection = ((IObjectContextAdapter)Workspace.NorthwindModel).ObjectContext.Connection;
                    connection.Open();
                    if (connection.State.HasFlag(System.Data.ConnectionState.Open)) {
                        _Employees = Workspace.Employees;
                        _Orders = Workspace.Orders;
                        _CustomersTreeList = Workspace.CustomersTreeList;
                        _Suppliers = Workspace.Suppliers;
                        _Customers = Workspace.Customers;
                        _Categories = Workspace.Categories;
                        _Products = Workspace.Products;
                    }
                }
                catch (Exception ex) {
                    App.LogException(ex);
                    Workspace.LoadInstance(this);
                }
                finally {
                    if (connection != null && connection.State.HasFlag(System.Data.ConnectionState.Open)) {
                        connection.Close();
                    }
                }
            }
        }
        #region public properties
        public IList<OrderDetail> OrderDetails {
            get { return _OrderDetails; }
            set {
                if (_OrderDetails != value) {
                    _OrderDetails = value;
                   // RaisePropertyChanged("OrderDetails");
                }
            }
        }
        public IList<Employee> Employees {
            get { return _Employees; }
            set {
                if (_Employees != value) {
                    _Employees = value;
                    //RaisePropertyChanged("Employees");
                }
            }
        }
        public IList<Order> Orders {
            get { return _Orders; }
            set {
                if (_Orders != value) {
                    _Orders = value;
                    //RaisePropertyChanged("Orders");
                }
            }
        }
        public CustomersTreeVm CustomersTreeList {
            get { return _CustomersTreeList; }
            set {
                if (_CustomersTreeList != value) {
                    _CustomersTreeList = value;
                    //RaisePropertyChanged("CustomersTreeList");
                }
            }
        }
        public IList<Supplier> Suppliers {
            get { return _Suppliers; }
            set {
                if (_Suppliers != value) {
                    _Suppliers = value;
                    //RaisePropertyChanged("Suppliers");
                }
            }
        }
        public IList<Customer> Customers {
            get { return _Customers; }
            set {
                if (_Customers != value) {
                    _Customers = value;
                    //RaisePropertyChanged("Customers");
                }
            }
        }
        public IList<Category> Categories {
            get { return _Categories; }
            set {
                if (_Categories != value) {
                    _Categories = value;
                    //RaisePropertyChanged("Categories");
                }
            }
        }
        public IList<Product> Products {
            get { return _Products; }
            set {
                if (_Products != value) {
                    _Products = value;
                    //RaisePropertyChanged("Products");
                }
            }
        } 
        
        #endregion

      
    }
}
