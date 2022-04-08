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
    
    public class StringFilterInitializerTest {
        
        [Test]
       public void TryGetFilter() {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var items = StateItem.All;
            var compareTo = StateEnum.State3;
            
            ListCollectionView view = new(items);
            Assert.IsTrue(view.CanFilter);
            Assert.IsNotNull(view.ItemProperties);
            FilterPresenter? filterPresenter = FilterPresenter.Get(view);
            Assert.IsNotNull(filterPresenter);

            StringFilterInitializer initializer = new();
            StringFilter? filter = initializer.TryCreateFilter(filterPresenter, nameof(StateItem.StateText)) as StringFilter;
            Assert.IsNotNull(filter);
            Assert.IsFalse(filter.IsActive);
            Assert.AreSame(filterPresenter, filter.FilterPresenter);
            Assert.AreEqual(view.Count, items.Count);
            
            filter.Value=compareTo.ToString();
            filter.Mode = StringFilterMode.Equals;
            var expected = Enum.GetValues<StateEnum>()
                .Where(st => st== compareTo)
                .Select(st => new StateItem(st))
                .ToList();
            Assert.IsTrue(filter.IsActive);
            var filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(1, filtered.Count);
            Assert.AreEqual(expected[0].StateId, ((StateItem)filtered[0]).StateId);
            
            filter.Value = "4";
            filter.Mode = StringFilterMode.EndsWith;
            compareTo = StateEnum.State4;
            expected = Enum.GetValues<StateEnum>()
                .Where(st => st == compareTo)
                .Select(st => new StateItem(st))
                .ToList();
            Assert.IsTrue(filter.IsActive);
            filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(1, filtered.Count);
            Assert.AreEqual(expected[0].StateId, ((StateItem)filtered[0]).StateId);

            filter.Value = "St";
            filter.Mode = StringFilterMode.StartsWith;
            expected = Enum.GetValues<StateEnum>()
                .Select(st => new StateItem(st))
                .ToList();
            Assert.IsTrue(filter.IsActive);
            filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(expected.Count, filtered.Count);
            Assert.AreEqual(expected[0].StateId, ((StateItem)filtered[0]).StateId);
            Assert.AreEqual(expected[^1].StateId, ((StateItem)filtered[^1]).StateId);
            
            filter.Value = "tat";
            filter.Mode = StringFilterMode.Contains;
            expected = Enum.GetValues<StateEnum>()
                .Select(st => new StateItem(st))
                .ToList();
            Assert.IsTrue(filter.IsActive);
            filtered = FilterPresenterTest.GetCollection(view);
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
