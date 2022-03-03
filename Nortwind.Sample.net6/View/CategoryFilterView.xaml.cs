using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.View;
using Northwind.NET.Sample.ViewModel;
using System.Windows.Controls;

namespace Northwind.NET.Sample.View {
    /// <summary>
    /// EqualFilter view for Product.Category item property in default view of the Workspace.This.Products DataCollection.
    /// </summary>
    public partial class CategoryFilterView : MultiValueFilterView {
        public CategoryFilterView() {
            InitializeComponent();
            // Define Filter that must be use.
            EqualFilterInitializer initializer = new EqualFilterInitializer();
            // Get FilterPresenter that connected to default collection view for Workspace.This.Products collection.
            FilterPresenter productsCollectionViewFilterPresenter = FilterPresenter.TryGet(Workspace.This.Products);
            // Get EqualFilter that use Category item property.
            EqualFilter filter = ((EqualFilter)(productsCollectionViewFilterPresenter.TryGetFilter("Category", initializer)));
            // Use instance of EqualFilter as Model.
            Model = filter;
        }
    }
}
