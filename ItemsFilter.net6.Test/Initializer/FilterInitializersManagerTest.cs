using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BolapanControl.ItemsFilter.Initializer;
using NUnit.Framework;


namespace ItemsFilter.net6.Test.Initializer {
    internal class FilterInitializersManagerTest {
        [Test]
        public void GetDefaults() {
            FilterInitializer[] fim = FilterInitializersManager.Default.ToArray();
            Assert.Greater(fim.Length,1);
            var fimTypes = fim.Select(f => f.GetType()).ToArray();
            Assert.Contains(typeof(EqualFilterInitializer), fimTypes);
            Assert.Contains(typeof(LessOrEqualFilterInitializer), fimTypes);
            Assert.Contains(typeof(GreaterOrEqualFilterInitializer), fimTypes);
            Assert.Contains(typeof(RangeFilterInitializer), fimTypes);
            Assert.Contains(typeof(StringFilterInitializer), fimTypes);
            Assert.Contains(typeof(EnumFilterInitializer), fimTypes);
        }
    }
}
