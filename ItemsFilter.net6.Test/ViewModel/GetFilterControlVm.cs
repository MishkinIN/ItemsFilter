using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using ItemsFilter.net6.Test.Model;
using NUnit.Framework;

namespace ItemsFilter.net6.Test.ViewModel {
    public class FilterControlVm {
        [Test]
        public void GetFilterControlVm() {
            var items = StateItem.All;
            ListCollectionView view = new(items);
            IEnumerable<FilterInitializer> initializers = FilterInitializersManager.Default;
            FilterPresenter? filterPresenter = FilterPresenter.Get(view);
            {
                string key = nameof(StateItem.Id);
                var vm = filterPresenter.TryGetFilterControlVm(key, initializers);
                var filterTypes = vm.Select(v => v.GetType()).ToList();
                Assert.Contains(typeof(ObjectEqualFilter), filterTypes);
            }
            {
                string key = nameof(StateItem.StateId);
                var vm = filterPresenter.TryGetFilterControlVm(key, initializers);
                var filterTypes = vm.Select(v => v.GetType()).ToList();
                Assert.Contains(typeof(EqualFilter<int>), filterTypes);
                Assert.Contains(typeof(LessOrEqualFilter<int>), filterTypes);
                Assert.Contains(typeof(GreaterOrEqualFilter<int>), filterTypes);
                Assert.Contains(typeof(RangeFilter<int>), filterTypes);
            }
            {
                string key = nameof(StateItem.State);
                var vm = filterPresenter.TryGetFilterControlVm(key, initializers);
                var filterTypes = vm.Select(v => v.GetType()).ToList();
                Assert.Contains(typeof(EnumFilter<StateEnum>), filterTypes);
            }
            {
                string key = nameof(StateItem.StateText);
                var vm = filterPresenter.TryGetFilterControlVm(key, initializers);
                var filterTypes = vm.Select(v => v.GetType()).ToList();
                Assert.Contains(typeof(EqualFilter<string>), filterTypes);
                Assert.Contains(typeof(StringFilter), filterTypes);
            }

        }
    }
}
