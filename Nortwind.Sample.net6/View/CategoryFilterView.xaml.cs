using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.View;
using Northwind.NET.EF6Model;
using Northwind.NET.Sample.ViewModel;
using System;
using System.Windows.Controls;

namespace Northwind.NET.Sample.View {
    /// <summary>
    /// EqualFilter view for Product.Category item property in default view of the Workspace.This.Products DataCollection.
    /// </summary>
    public partial class CategoryFilterView : MultiValueFilterView {
        public CategoryFilterView() {
            InitializeComponent();
            // Define Filter that must be use.
            EqualFilterInitializer initializer = new ();
            // Get FilterPresenter that connected to default collection view for Workspace.This.Products collection.
            FilterPresenter? fp = FilterPresenter.TryGet(Workspace.This.Products?? Array.Empty<Product>());
            // Get EqualFilter that use Category item property.
            if (fp!=null && fp.TryGetFilter("Category", initializer) is EqualFilter filter) {
                // Use instance of EqualFilter as Model.
                Model = filter; 
            }
        }
    }
}
