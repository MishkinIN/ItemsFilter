using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.View;
using Northwind.NET.EF6Model;
using Northwind.NET.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Northwind.NET.Sample.View {
    /// <summary>
    /// EqualFilter view for Product.Category item property in default view of the Workspace.This.Products DataCollection.
    /// </summary>
    public partial class CategoryFilterView : MultiValueFilterView {
        public CategoryFilterView() {
            InitializeComponent();
            DataContextChanged += CategoryFilterView_DataContextChanged;
        }
        private void CategoryFilterView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e) {
            if (DataContext is IList<Product> source) {
                // Define Filter that must be use.
                EqualFilterInitializer initializer = new();
                FilterPresenter fp;
                // Get FilterPresenter that connected to default collection view for Workspace.This.Products collection.
                fp = FilterPresenter.Get(source);
                // Get EqualFilter that use Category item property.
                if (fp?.TryGetFilter("Category", initializer, out var f)==true
                    && f is EqualFilter filter) {
                    // Use instance of EqualFilter as Model.
                    Model = filter;
                }
            }
            else {
                Model = null;
            }
        }
        
    }
}
