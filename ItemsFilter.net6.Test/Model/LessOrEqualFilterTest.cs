using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Windows;
using System.ComponentModel;
using BolapanControl.ItemsFilter.Model;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;

namespace ItemsFilter.net6.Test.Model {
    public class LessOrEqualFilterTest {
        private readonly IEnumerable<int> enumSource = Enum.GetValues<StateEnum>().Cast<int>();
        [SetUp]
        public void Setup() {
        }
        [Test]
        public void CTOR() {
            LessOrEqualFilter<int> filter = GetLessOrEqualFilter<int>();
            Assert.IsNotNull(filter);

            Assert.AreEqual((int?)null, filter.CompareTo);
            Assert.IsFalse(filter.IsActive);
            filter.CompareTo = 4;
            Assert.IsTrue(filter.IsActive);
            filter.CompareTo = null;
            Assert.IsFalse(filter.IsActive);
        }
        [Test]
        public void TestFilterIsMatch() {
            int compareTo = (int)StateEnum.State3;
            var source = enumSource.ToList();
            var expectedCollection = source.Where(v => v <= compareTo).ToArray();
            LessOrEqualFilter<int> filter = GetLessOrEqualFilter<int>();
            CollectionViewSource cvs = new();
            cvs.Source = source;
            ICollectionView view = cvs.View;
            Assert.IsTrue(view.CanFilter);
            using (IDisposable defer = view.DeferRefresh()) {
                view.Filter = el => {
                    BolapanControl.ItemsFilter.FilterEventArgs e = new(el);
                    filter.IsMatch(null, e);
                    return e.Accepted;
                };
            }
            Assert.IsFalse(filter.IsActive);
            var filtered = GetCollection(view);
            Assert.AreEqual(source.Count, filtered.Count);
            filter.CompareTo = compareTo;
            Assert.IsTrue(filter.IsActive);
            view.Refresh();
            filtered = GetCollection(view);
            Assert.AreEqual(expectedCollection.Length, filtered.Count);
            for (int i = 0; i < expectedCollection.Length; i++) {
                Assert.AreEqual(expectedCollection[i], filtered[i]);
            }
        }
        [Test]
        public void TestAttach() {
            LessOrEqualFilter<int> filter = GetLessOrEqualFilter<int>();
            var source = enumSource.ToList();
            var filterPresenter = FilterPresenter.Get(source);
            Assert.IsNotNull(filterPresenter);
            Assert.IsFalse(filter.IsActive);
            filter.Attach(filterPresenter);
            var view = filterPresenter.CollectionView;
            var filtered = GetCollection(view);
            Assert.AreEqual(source.Count, filtered.Count);
            filter.CompareTo = (int)StateEnum.State3;
            Assert.IsTrue(filter.IsActive);
            filtered = GetCollection(view);
            Assert.AreEqual(4, filtered.Count);
            Assert.AreEqual((int)StateEnum.State0, filtered[0]);
            Assert.AreEqual((int)StateEnum.State1, filtered[1]);
            Assert.AreEqual((int)StateEnum.State2, filtered[2]);
            Assert.AreEqual((int)StateEnum.State3, filtered[3]);
        }



        private static List<object> GetCollection(ICollectionView view) {
            List<object> currentView = new();
            foreach (var item in view) {
                currentView.Add(item);
            }

            return currentView;
        }

        private static LessOrEqualFilter<T> GetLessOrEqualFilter<T>()
            where T : struct, IComparable<T> {
            LessOrEqualFilter<T> filter = new(o => (T?)o);
            return filter;
        }
    }
}
