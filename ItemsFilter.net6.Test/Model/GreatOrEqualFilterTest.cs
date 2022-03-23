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
    public class GreatOrEqualFilterTest {
        private List<int> source = new();
        [SetUp]
        public void Setup() {
            source.AddRange(Enum.GetValues<StateEnum>().Cast<int>());
        }
        [Test]
        public void CTOR() {
            GreaterOrEqualFilter<int> filter = GetGreaterOrEqualFilter<int>();
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
            var expectedCollection = source.Where(v => v >= compareTo).ToArray();
            GreaterOrEqualFilter<int> filter = GetGreaterOrEqualFilter<int>();
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
            GreaterOrEqualFilter<int> filter = GetGreaterOrEqualFilter<int>();
            var filterPresenter = FilterPresenter.Get(source);
            Assert.IsNotNull(filterPresenter);
            Assert.IsFalse(filter.IsActive);
#pragma warning disable CS8604 // Possible null reference argument.
            filter.Attach(filterPresenter);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var view = filterPresenter.CollectionView;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            var filtered = GetCollection(view);
            Assert.AreEqual(source.Count, filtered.Count);
            filter.CompareTo = (int)StateEnum.State3;
            var expectedCollection = source.Where(v => v >= filter.CompareTo).ToArray(); 
            Assert.IsTrue(filter.IsActive);
            filtered = GetCollection(view);
            Assert.AreEqual(expectedCollection.Length, filtered.Count);
            for (int i = 0; i < expectedCollection.Length; i++) {
                Assert.AreEqual(expectedCollection[i], filtered[i]);
            }
        }



        private static List<object> GetCollection(ICollectionView view) {
            List<object> currentView = new();
            foreach (var item in view) {
                currentView.Add(item);
            }

            return currentView;
        }

        private static GreaterOrEqualFilter<T> GetGreaterOrEqualFilter<T>()
            where T : struct, IComparable<T> {
            GreaterOrEqualFilter<T> filter = new(o => (T?)o);
            return filter;
        }
    }
}
