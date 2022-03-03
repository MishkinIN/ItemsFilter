using System;
using System.Collections.Generic;
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

namespace Northwind.NET.Sample.View {
    /// <summary>
    /// Interaction logic for CustomersView.xaml
    /// </summary>
    public partial class CustomersView : UserControl {
        public CustomersView() {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            {
                //Load your data here and assign the result to the CollectionViewSource.
                //DataContext = Workspace.This.CustomersTreeList;
            }
        }
    }
}