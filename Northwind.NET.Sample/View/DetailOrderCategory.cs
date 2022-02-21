using BolapanControl.ItemsFilter;
// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using Northwind.NET.EF6Model;
using System.Collections;
using System.Linq;

namespace Northwind.NET.Sample.View
{
    public class DetailOrderCategoryFIM : BolapanControl.ItemsFilter.Initializer.FilterInitializersManager
    {
        public DetailOrderCategoryFIM()
        {
            Add(new DetailOrderCategoryFilterInitializer());
        }
    }
    public class DetailOrderCategoryFilter : EqualFilter<OrderDetail>
    {
        internal DetailOrderCategoryFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter)
            : base(CategoryNameGetter, GetAvailableValuesQuery(filterPresenter))
        {

        }
       
        private static string CategoryNameGetter(object orderDetail)
        {
            OrderDetail detail =  (OrderDetail)orderDetail;
            if (detail==null) return "";
            Product product = detail.Product;
            if (product == null) return "";
            Category category = product.Category;
            if (category == null) return "";
            string name = category.Name;
            return name == null ? "" : name;
        }
        /// <summary>
        /// Returns a query that returns the unique item property values in the ItemsSource collection..
        /// </summary>
        private static IEnumerable GetAvailableValuesQuery(FilterPresenter filterPresenter)
        {
            IEnumerable source = filterPresenter.CollectionView.SourceCollection;
            if (source == null)
                return new object[0];
            var sourceQuery = source.OfType<object>()
                .Select(item => CategoryNameGetter(item))
                .OrderBy(item=>item)
                .Distinct();
            return sourceQuery;
        }
    }
    class DetailOrderCategoryFilterInitializer : BolapanControl.ItemsFilter.Initializer.FilterInitializer
    {
        public override Filter NewFilter(BolapanControl.ItemsFilter.FilterPresenter filterPresenter, object key)
        {
            if (key is string && (string)key == "Product.Category.Name")
            {
                return new DetailOrderCategoryFilter(filterPresenter);
            }
            return null;
        }
    }

}
