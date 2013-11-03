using Northwind.NET.Sample.View;
using Northwind.NET.Sample.ViewModel;
using System.Windows;

namespace Northwind.NET.Sample {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void ShowProductsView(object sender, RoutedEventArgs e) {
            mainFrame.Content = new ProductsView();
        }

        private void ShowEmployesView(object sender, RoutedEventArgs e) {
            mainFrame.Content = new EmployesView();
        }
        private void ShowCategoriesView(object sender, RoutedEventArgs e) {
            mainFrame.Content = new CategoriesView();
        }

        private void ShowCustomersView(object sender, System.Windows.RoutedEventArgs e) {
            mainFrame.Content = new CustomersView();
        }

        private void ShowOrdersView(object sender, RoutedEventArgs e) {
            mainFrame.Content = new OrdersVm();
        }
    }
}
