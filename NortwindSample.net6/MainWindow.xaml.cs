// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
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

        private void ShowEmployeesView(object sender, RoutedEventArgs e) {
            mainFrame.Content = new EmployeesView();
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
