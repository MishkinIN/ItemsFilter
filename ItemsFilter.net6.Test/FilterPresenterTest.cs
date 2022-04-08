using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using ItemsFilter.net6.Test.Model;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ItemsFilter.net6.Test {
    public class FilterPresenterTest {
        [Test]
        public void CTOR() {
            int[] source = { 1, 2, 3 };
            //var fp_empty = FilterPresenter.Get(null);
            //Assert.IsNull(fp_empty);
            var fp1 = FilterPresenter.Get(source);
            Assert.IsNotNull(fp1);
            var fp2 = FilterPresenter.Get(source);
            Assert.AreSame(fp1, fp2);
        }
        [Test]
        public void Filter() {
            var items = (List<StateItem>)StateItem.All;
            ListCollectionView view = new(items);
            IEnumerable<FilterInitializer> initializers = FilterInitializersManager.Default;
            FilterPresenter? filterPresenter = FilterPresenter.Get(view);
            var vmId = filterPresenter.TryGetFilterControlVm(nameof(StateItem.Id), initializers);
            {
                var filterTypes = vmId.Select(v => v.GetType()).ToList();
                Assert.Contains(typeof(ObjectEqualFilter), filterTypes);
            }
            var vmStateId = filterPresenter.TryGetFilterControlVm(nameof(StateItem.StateId), initializers);
            {
                var filterTypes = vmStateId.Select(v => v.GetType()).ToList();
                Assert.Contains(typeof(EqualFilter<int>), filterTypes);
                Assert.Contains(typeof(LessOrEqualFilter<int>), filterTypes);
                Assert.Contains(typeof(GreaterOrEqualFilter<int>), filterTypes);
                Assert.Contains(typeof(RangeFilter<int>), filterTypes);
            }
            var vmState = filterPresenter.TryGetFilterControlVm(nameof(StateItem.State), initializers);
            {
                var filterTypes = vmState.Select(v => v.GetType()).ToList();
                Assert.Contains(typeof(EnumFilter<StateEnum>), filterTypes);
            }
            //Assert.IsFalse(filterPresenter.IsFilterActive);
            var filtered = GetCollection(view);
            Assert.AreEqual(items.Count, filtered.Count);
            var idLesssFilter = vmStateId.OfType<LessOrEqualFilter<int>>().First();
            var compareId = (int)StateEnum.State5;
            idLesssFilter.CompareTo = compareId;
            //Assert.IsTrue(filterPresenter.IsFilterActive);
            filtered = GetCollection(view);
            Assert.AreEqual(items.Where(v=>v.StateId<= compareId).Count(), filtered.Count);
            
            var stateEnumFilter = vmState.OfType<EnumFilter<StateEnum>>().First();
            List<StateEnum> selected = new(new StateEnum[] { StateEnum.State1, StateEnum.State4, StateEnum.State6 });
            List<StateEnum> unselected = new();

            stateEnumFilter.SelectedValuesChanged(addedItems: selected, removedItems: unselected);
            filtered = GetCollection(view);
            Assert.AreEqual(2, filtered.Count);

        }

        public static List<object?> GetCollection(IEnumerable view) {
            List<object?> currentView = new();
            foreach (var item in view) {
                currentView.Add(item);
            }

            return currentView;
        }

    }
}
