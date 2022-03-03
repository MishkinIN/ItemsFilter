// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using Northwind.NET.EF6Model;
using Northwind.NET.Sample.ViewModel;
using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;

namespace Northwind.NET.Sample.ViewModel {
    public partial class WorkspaceInstance{
        #region fields
        private IList<Employee>? _Employees;
        private IList<Order>? _Orders;
        private CustomersTreeVm? _CustomersTreeList;
        private IList<Supplier>? _Suppliers;
        private IList<Customer>? _Customers;
        private IList<Category>? _Categories;
        private IList<Product>? _Products;
        private IList<OrderDetail>? _OrderDetails;
        #endregion
        public WorkspaceInstance() {
            Workspace.LoadInstance(this);
        }
        #region public properties
        public IList<OrderDetail>? OrderDetails {
            get { return _OrderDetails; }
            set {
                if (_OrderDetails != value) {
                    _OrderDetails = value;
                   // RaisePropertyChanged("OrderDetails");
                }
            }
        }
        public IList<Employee>? Employees {
            get { return _Employees; }
            set {
                if (_Employees != value) {
                    _Employees = value;
                    //RaisePropertyChanged("Employees");
                }
            }
        }
        public IList<Order>? Orders {
            get { return _Orders; }
            set {
                if (_Orders != value) {
                    _Orders = value;
                    //RaisePropertyChanged("Orders");
                }
            }
        }
        public CustomersTreeVm? CustomersTreeList {
            get { return _CustomersTreeList; }
            set {
                if (_CustomersTreeList != value) {
                    _CustomersTreeList = value;
                    //RaisePropertyChanged("CustomersTreeList");
                }
            }
        }
        public IList<Supplier>? Suppliers {
            get { return _Suppliers; }
            set {
                if (_Suppliers != value) {
                    _Suppliers = value;
                    //RaisePropertyChanged("Suppliers");
                }
            }
        }
        public IList<Customer>? Customers {
            get { return _Customers; }
            set {
                if (_Customers != value) {
                    _Customers = value;
                    //RaisePropertyChanged("Customers");
                }
            }
        }
        public IList<Category>? Categories {
            get { return _Categories; }
            set {
                if (_Categories != value) {
                    _Categories = value;
                    //RaisePropertyChanged("Categories");
                }
            }
        }
        public IList<Product>? Products {
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
