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
    
    public class ObjectFilterInitializerTest {
        
        [Test]
       public void TryGetFilter() {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var items = FilterPresenterTest.GetCollection(StateItem.All);
            ListCollectionView view = new(items);
            FilterPresenter? filterPresenter = FilterPresenter.Get(view);
            Assert.IsNotNull(filterPresenter);
            EqualFilterInitializer initializer = new();
            ObjectEqualFilter? filter = initializer.TryCreateFilter(filterPresenter, nameof(StateItem.Box)) as ObjectEqualFilter;
            
            Assert.IsNotNull(filter);
            Assert.IsFalse(filter.IsActive);
            Assert.AreSame(filterPresenter, filter.FilterPresenter);
            Assert.AreEqual(view.Count, items.Count);
            
            List<object?> selected = new(new object[] { StateEnum.State0, StateEnum.State4 });
            List<object?> unselected = new();
            
            filter.SelectedValuesChanged(addedItems: selected, removedItems: unselected);
            
            Assert.IsTrue(filter.IsActive);
            var filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(selected.Count, filtered.Count);
            Assert.AreEqual(selected[0], ((StateItem)filtered[0]).Box);
            Assert.AreEqual(selected[1], ((StateItem)filtered[1]).Box);
            
            filter.IsActive = false;
            
            filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(items.Count, filtered.Count);

            //selected.Clear();
            selected.Add(null);
            ((StateItem)items[1]).Box = null;
            filter.SelectedValuesChanged(addedItems: selected, removedItems: unselected);
            Assert.IsTrue(filter.IsActive);
            //Assert.IsTrue(filterPresenter.IsFilterActive);
            filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(3, filtered.Count);
            Assert.AreEqual(((StateItem)items[0]).Box, ((StateItem)filtered[0]).Box);
            Assert.AreEqual(((StateItem)items[1]).Box, ((StateItem)filtered[1]).Box);
            Assert.AreEqual(((StateItem)items[4]).Box, ((StateItem)filtered[2]).Box);

#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
        }

    }
}
