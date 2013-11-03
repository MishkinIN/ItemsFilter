//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Northwind.NET.EF6Model
{
    using System;
    using System.Collections.Generic;
    
    using System.ComponentModel;
    using System.Collections.ObjectModel;
        
    public partial class Order: INotifyPropertyChanged
    {
        public Order()
        {
            this.OrderDetails = new ObservableCollection<OrderDetail>();
        }
    
        
    			private int _ID;
    			public int ID { 
    				get { return _ID;}
    				set { 
    					if (_ID!=value) {
    						_ID = value;
    						RaisePropertyChanged("ID");
    					}
    				}
               }
        
    			private Nullable<int> _CustomerId;
    			public Nullable<int> CustomerId { 
    				get { return _CustomerId;}
    				set { 
    					if (_CustomerId!=value) {
    						_CustomerId = value;
    						RaisePropertyChanged("CustomerId");
    					}
    				}
               }
        
    			private Nullable<int> _EmployeeID;
    			public Nullable<int> EmployeeID { 
    				get { return _EmployeeID;}
    				set { 
    					if (_EmployeeID!=value) {
    						_EmployeeID = value;
    						RaisePropertyChanged("EmployeeID");
    					}
    				}
               }
        
    			private Nullable<System.DateTime> _OrderDate;
    			public Nullable<System.DateTime> OrderDate { 
    				get { return _OrderDate;}
    				set { 
    					if (_OrderDate!=value) {
    						_OrderDate = value;
    						RaisePropertyChanged("OrderDate");
    					}
    				}
               }
        
    			private Nullable<System.DateTime> _RequiredDate;
    			public Nullable<System.DateTime> RequiredDate { 
    				get { return _RequiredDate;}
    				set { 
    					if (_RequiredDate!=value) {
    						_RequiredDate = value;
    						RaisePropertyChanged("RequiredDate");
    					}
    				}
               }
        
    			private Nullable<System.DateTime> _ShippedDate;
    			public Nullable<System.DateTime> ShippedDate { 
    				get { return _ShippedDate;}
    				set { 
    					if (_ShippedDate!=value) {
    						_ShippedDate = value;
    						RaisePropertyChanged("ShippedDate");
    					}
    				}
               }
        
    			private Nullable<int> _ShipperId;
    			public Nullable<int> ShipperId { 
    				get { return _ShipperId;}
    				set { 
    					if (_ShipperId!=value) {
    						_ShipperId = value;
    						RaisePropertyChanged("ShipperId");
    					}
    				}
               }
        
    			private Nullable<decimal> _Freight;
    			public Nullable<decimal> Freight { 
    				get { return _Freight;}
    				set { 
    					if (_Freight!=value) {
    						_Freight = value;
    						RaisePropertyChanged("Freight");
    					}
    				}
               }
        
    			private string _ShipName;
    			public string ShipName { 
    				get { return _ShipName;}
    				set { 
    					if (_ShipName!=value) {
    						_ShipName = value;
    						RaisePropertyChanged("ShipName");
    					}
    				}
               }
        
    			private string _ShipAddress;
    			public string ShipAddress { 
    				get { return _ShipAddress;}
    				set { 
    					if (_ShipAddress!=value) {
    						_ShipAddress = value;
    						RaisePropertyChanged("ShipAddress");
    					}
    				}
               }
        
    			private string _ShipCity;
    			public string ShipCity { 
    				get { return _ShipCity;}
    				set { 
    					if (_ShipCity!=value) {
    						_ShipCity = value;
    						RaisePropertyChanged("ShipCity");
    					}
    				}
               }
        
    			private string _ShipRegion;
    			public string ShipRegion { 
    				get { return _ShipRegion;}
    				set { 
    					if (_ShipRegion!=value) {
    						_ShipRegion = value;
    						RaisePropertyChanged("ShipRegion");
    					}
    				}
               }
        
    			private string _ShipPostalCode;
    			public string ShipPostalCode { 
    				get { return _ShipPostalCode;}
    				set { 
    					if (_ShipPostalCode!=value) {
    						_ShipPostalCode = value;
    						RaisePropertyChanged("ShipPostalCode");
    					}
    				}
               }
        
    			private string _ShipCountry;
    			public string ShipCountry { 
    				get { return _ShipCountry;}
    				set { 
    					if (_ShipCountry!=value) {
    						_ShipCountry = value;
    						RaisePropertyChanged("ShipCountry");
    					}
    				}
               }
        
    			private byte[] _RowTimeStamp;
    			public byte[] RowTimeStamp { 
    				get { return _RowTimeStamp;}
    				set { 
    					if (_RowTimeStamp!=value) {
    						_RowTimeStamp = value;
    						RaisePropertyChanged("RowTimeStamp");
    					}
    				}
               }
    
        
    			private Customer _Customer;
    			public virtual Customer Customer { 
    				get { return _Customer;}
    				set { 
    					if (_Customer!=value) {
    						_Customer = value;
    						RaisePropertyChanged("Customer");
    					}
    				}
               }
        
    			private Employee _Employee;
    			public virtual Employee Employee { 
    				get { return _Employee;}
    				set { 
    					if (_Employee!=value) {
    						_Employee = value;
    						RaisePropertyChanged("Employee");
    					}
    				}
               }
        
    			private ObservableCollection<OrderDetail> _OrderDetails;
    			public virtual ObservableCollection<OrderDetail> OrderDetails { 
    				get { return _OrderDetails;}
    				set { 
    					if (_OrderDetails!=value) {
    						_OrderDetails = value;
    						RaisePropertyChanged("OrderDetails");
    					}
    				}
               }
        
    			private Shipper _Shipper;
    			public virtual Shipper Shipper { 
    				get { return _Shipper;}
    				set { 
    					if (_Shipper!=value) {
    						_Shipper = value;
    						RaisePropertyChanged("Shipper");
    					}
    				}
               }
    	public event PropertyChangedEventHandler PropertyChanged;
    	protected virtual void RaisePropertyChanged(string propertyName) {
            	var handler = PropertyChanged;
    			if (handler != null) {
    	            handler(this, new PropertyChangedEventArgs(propertyName));
    			}
        }
    }
}
