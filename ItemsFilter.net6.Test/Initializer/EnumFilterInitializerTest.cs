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
    
    public class EnumFilterInitializerTest {
        
        [Test]
       public void TryGetFilter() {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var items = StateItem.All;
            ListCollectionView view = new(items);
            FilterPresenter? filterPresenter = FilterPresenter.Get(view);
            Assert.IsNotNull(filterPresenter);
            EnumFilterInitializer initializer = new();
            EnumFilter<StateEnum>? filter = initializer.TryCreateFilter(filterPresenter, nameof(StateItem.State)) as EnumFilter<StateEnum>;
            
            Assert.IsNotNull(filter);
            Assert.IsFalse(filter.IsActive);
            Assert.AreSame(filterPresenter, filter.FilterPresenter);
            Assert.AreEqual(items.Count, view.Count);
            
            List<StateEnum> selected = new(new StateEnum[] { StateEnum.State1, StateEnum.State4 });
            List<StateEnum> unselected = new();
            
            filter.SelectedValuesChanged(addedItems: selected, removedItems: unselected);
            
            Assert.IsTrue(filter.IsActive);
            var filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(selected.Count, filtered.Count);
            Assert.AreEqual(selected[0], ((StateItem)filtered[0]).State);
            Assert.AreEqual(selected[1], ((StateItem)filtered[1]).State);

            //filterPresenter.IsFilterActive = false;
            
            filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(items.Count, filtered.Count);

#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
        }

    }
}
