using BolapanControl.ItemsFilter.View;
using Northwind.NET.Sample.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// <summary>
    /// Interaction logic for CountriesTreeFilterView.xaml
    /// </summary>
    [ModelView]
    public partial class CountriesTreeFilterView : CustomersTreeFilterControl {
        public CountriesTreeFilterView() {
            InitializeComponent();
        }
        public CountriesTreeFilterView(CountriesTreeFilter model)
            : this() {
            base.DataContext = base.Model = model;
        }

    }
}
