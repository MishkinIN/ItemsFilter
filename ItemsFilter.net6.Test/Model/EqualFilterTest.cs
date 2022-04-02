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
    public class EqualFilterTest {
        private readonly IEnumerable<int> enumSource = Enum.GetValues<StateEnum>().Cast<int>();
        [SetUp]
        public void Setup() {
        }
        [Test]
        public void CTOR() {
            EqualFilter<int> filter = GetEqualFilter<int>();
            Assert.IsNotNull(filter);
            
            Assert.AreEqual(0, filter.SelectedValues.Count);
            Assert.IsFalse(filter.IsActive);
        }
        [Test]
        public void TestAvailableValues() {
            EqualFilter<int> filter = GetEqualFilter<int>();
            var source = enumSource.ToList();
            filter.AvailableValues = source;
            CollectionViewSource cvs = new();
            cvs.Source = filter.AvailableValues;
            ICollectionView view = cvs.View;
            var currentView = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(source.Count, currentView.Count);
        }
        [Test]
        public void TestFilterIsMatch() {
            EqualFilter<int> filter = GetEqualFilter<int>();
            CollectionViewSource cvs = new();
            var source = Enum.GetValues<StateEnum>().Cast<int>().ToList();
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
            var filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(source.Count, filtered.Count);
            List<int> selected = new(new int[] { (int)StateEnum.State1, (int)StateEnum.State4 });
            List<int> unselected = new();
            filter.SelectedValuesChanged(addedItems: selected, removedItems: unselected);
            view.Refresh();
            filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(selected.Count, filtered.Count);
            Assert.AreEqual(selected[0], filtered[0]);
            Assert.AreEqual(selected[1], filtered[1]);
        }
        [Test]
        public void TestAttach() {
            EqualFilter<int> filter = GetEqualFilter<int>();
            var source = enumSource.ToList();
            var filterPresenter = FilterPresenter.Get(source);
            Assert.IsNotNull(filterPresenter);
            Assert.IsFalse(filter.IsActive);
            filter.Attach(filterPresenter);
            var view = filterPresenter.CollectionView;
            var filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(source.Count, filtered.Count);
            List<int> selected = new(new int[] { (int)StateEnum.State1, (int)StateEnum.State4 });
            List<int> unselected = new();
            filter.SelectedValuesChanged(addedItems: selected, removedItems: unselected);
            Assert.IsTrue(filter.IsActive);
            filtered = FilterPresenterTest.GetCollection(view);
            Assert.AreEqual(selected.Count, filtered.Count);
            Assert.AreEqual(selected[0], filtered[0]);
            Assert.AreEqual(selected[1], filtered[1]);
        }




        private static EqualFilter<T> GetEqualFilter<T>()
            where T: IEquatable<T> {
            EqualFilter<T> filter = new(o=>o);
            return filter;
        }
    }
}
