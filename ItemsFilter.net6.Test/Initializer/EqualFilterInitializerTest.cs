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
    
    public class EqualFilterInitializerTest {
        
        [Test]
       public void TryGetFilter() {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var items = StateItem.All;
            ListCollectionView view = new(items);
            Assert.IsTrue(view.CanFilter);
            Assert.IsNotNull(view.ItemProperties);
            FilterPresenter? filterPresenter = FilterPresenter.TryGet(view);
            Assert.IsNotNull(filterPresenter);
            EqualFilterInitializer initializer = new();
            EqualFilter? filter = initializer.TryGetFilter(filterPresenter, nameof(StateItem.StateId)) as EqualFilter;
            Assert.IsNotNull(filter);
            Assert.IsFalse(filter.IsActive);
            Assert.AreSame(filterPresenter, filter.FilterPresenter);
            Assert.AreEqual(view.Count, items.Count);
            List<int> selected = new(new int[] { (int)StateEnum.State1, (int)StateEnum.State4 });
            List<int> unselected = new();
            filter.SelectedValuesChanged(addedItems: selected, removedItems: unselected);
            Assert.IsTrue(filter.IsActive);
            var filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(filtered.Count, selected.Count);
            Assert.AreEqual(selected[0], ((StateItem)filtered[0]).StateId);
            Assert.AreEqual(selected[1], ((StateItem)filtered[1]).StateId);
            filterPresenter.IsFilterActive = false;
            filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(filtered.Count, items.Count);

#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
        }

    }
}
