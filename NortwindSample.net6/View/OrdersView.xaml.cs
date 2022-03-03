using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using Northwind.NET.EF6Model;
using Northwind.NET.Sample.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Northwind.NET.Sample.View
{
	/// <summary>
	/// Interaction logic for NewOrder.xaml
	/// </summary>
    [TemplateVisualState(GroupName = "EmployeeFilterState", Name = "Unfiltered")]
    [TemplateVisualState(GroupName="EmployeeFilterState",Name="Filtered")]
	public partial class OrdersView : UserControl
	{
        private Filter? filter;
        public OrdersView()
		{
			this.InitializeComponent();
            VisualStateManager.GoToState(this, "Unfiltered", false);
   		}

        
        void filter_PropertyChanged(object? sender, PropertyChangedEventArgs e) {
            if (e.PropertyName=="IsActive") {
                if (filter!=null && filter.IsActive) 
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
            if (e.NewValue is OrdersVm vm) {
                FilterPresenter? fpr = FilterPresenter.TryGet(vm.OrdersCollectionView);
                filter = fpr?.TryGetFilter("Employee", new EqualFilterInitializer());
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