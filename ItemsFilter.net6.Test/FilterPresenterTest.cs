using BolapanControl.ItemsFilter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsFilter.net6.Test {
    public class FilterPresenterTest {
        [Test]
        public void CTOR() {
            int[] source = { 1, 2, 3 };
            var fp_empty = FilterPresenter.TryGet(null);
            Assert.IsNull(fp_empty);
            var fp1 = FilterPresenter.TryGet(source);
            Assert.IsNotNull(fp1);
            var fp2 = FilterPresenter.TryGet(source);
            Assert.AreSame(fp1, fp2);
        }
    }
}
