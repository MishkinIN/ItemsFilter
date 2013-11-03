using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using Northwind.NET.EF6Model;
using BolapanControl.ItemsFilter.Model;
using Northwind.NET.Sample.ViewModel;
using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;

namespace Northwind.NET.Sample.View
{
	/// <summary>
	/// Interaction logic for NewOrder.xaml
	/// </summary>
    [TemplateVisualState(GroupName = "EmployeeFilterState", Name = "Unfiltered")]
    [TemplateVisualState(GroupName="EmployeeFilterState",Name="Filtered")]
	public partial class OrdersView : UserControl
	{
        private Filter filter;
        public static RoutedUICommand SetCustomer = new RoutedUICommand("Set new Order.Customer in DataContext", "SetCustomer", typeof(OrdersView));
        static OrdersView() {
            CommandManager.RegisterClassCommandBinding(typeof(OrdersView), new CommandBinding(OrdersView.SetCustomer, ExecuteSetCustomer, CanExecuteSetCustomer));
        }

        private static void CanExecuteSetCustomer(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private static void ExecuteSetCustomer(object sender, ExecutedRoutedEventArgs e) {
            OrdersView ordersView = (OrdersView)sender;
            Customer customer = e.Parameter as Customer;
            Order order = ordersView.partOrderHeader.DataContext as Order;
            if (order != null && customer != null) {
                order.Customer = customer;
            }
        }
        public OrdersView()
		{
			this.InitializeComponent();
            VisualStateManager.GoToState(this, "Unfiltered", false);
   		}

        private void customerComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            SetCustomer.Execute(((ComboBox)sender).SelectedItem, (IInputElement)sender);
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
          
        }

        void filter_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName=="IsActive") {
                if (filter.IsActive) 
                    VisualStateManager.GoToState(this, "Filtered", false);
                else
                    VisualStateManager.GoToState(this, "Unfiltered", false);
            }
        }

        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
        	if(filter!=null)
                filter.PropertyChanged -= filter_PropertyChanged;
            filter = null;
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            OrdersVm vm = e.NewValue as OrdersVm;
            if (vm != null) {
                FilterPresenter fpr = FilterPresenter.TryGet(vm.OrdersView);
                filter = fpr.TryGetFilter("Employee", new EqualFilterInitializer());
                if (filter != null) {
                    if (filter.IsActive)
                        VisualStateManager.GoToState(this, "Filtered", false);
                    else
                        VisualStateManager.GoToState(this, "Unfiltered", false);
                    filter.PropertyChanged += filter_PropertyChanged;
                }
            }
        }
	}
}