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
using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using Northwind.NET.EF6Model;

namespace Northwind.NET.Sample.View {
    /// <summary>
    /// Interaction logic for CategoryFilter.xaml
    /// </summary>
    public partial class CategoryFilterView : UserControl {
        public CategoryFilterView() {
            InitializeComponent();
            EqualFilterInitializer initializer = new EqualFilterInitializer();
            //if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) {
                EqualFilter filter = ((EqualFilter)(FilterPresenter.TryGet(Workspace.This.Products).TryGetFilter("Category", initializer)));
                DataContext = filter;
            //}
        }
    }
}
