using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ItemsFilter.net6.Test.Model;
using System.Windows.Data;
using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;

namespace ItemsFilter.net6.Test.Initializer {
    
    public class LessOrEqualFilterInitializerTest {
        
        [Test]
       public void TryGetFilter() {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var items = StateItem.All;
            var compareTo = StateEnum.State3;
            var expected = Enum.GetValues<StateEnum>()
                .Where(st => st <= compareTo)
                .Select(st => new StateItem(st))
                .ToList();
            ListCollectionView view = new(items);
            Assert.IsTrue(view.CanFilter);
            Assert.IsNotNull(view.ItemProperties);
            FilterPresenter? filterPresenter = FilterPresenter.Get(view);
            Assert.IsNotNull(filterPresenter);

            LessOrEqualFilterInitializer initializer = new();
            LessOrEqualFilter<int>? filter = initializer.TryCreateFilter(filterPresenter, nameof(StateItem.StateId)) as LessOrEqualFilter<int>;
            Assert.IsNotNull(filter);
            Assert.IsFalse(filter.IsActive);
            Assert.AreSame(filterPresenter, filter.FilterPresenter);
            Assert.AreEqual(view.Count, items.Count);
            
            filter.CompareTo=(int)compareTo;
            Assert.IsTrue(filter.IsActive);
            var filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(expected.Count, filtered.Count);
            Assert.AreEqual(expected[0].StateId, ((StateItem)filtered[0]).StateId);
            Assert.AreEqual(expected[^1].StateId, ((StateItem)filtered[^1]).StateId);
           
            //filterPresenter.IsFilterActive = false;
            filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(items.Count, filtered.Count);

#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
        }

    }
}
