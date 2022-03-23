using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.View;
using Northwind.NET.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Northwind.NET.Sample.View {
   public partial class CustomersTreeFilterViewTemplate : ControlTemplate {
        private bool _contentLoaded;
        public CustomersTreeFilterViewTemplate() {
            InitializeComponent();
        }
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            //var thisAssembly = typeof(CustomersTreeFilterViewTemplate).Assembly.GetName().Name;
            //System.Uri resourceLocater = new System.Uri($"{thisAssembly};component/View/CustomersTreeFilterViewTemplate.xaml", System.UriKind.Relative);
            System.Uri resourceLocater = new System.Uri($"View/CustomersTreeFilterViewTemplate.xaml", System.UriKind.Relative);
            System.Windows.Application.LoadComponent(this, resourceLocater);
        }
    }
}
