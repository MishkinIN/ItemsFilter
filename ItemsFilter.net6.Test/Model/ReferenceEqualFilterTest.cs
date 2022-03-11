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
    public class ReferenceEqualFilterTest {
        private List<int?> source = new();
        [SetUp]
        public void Setup() {
            var intSource = Enum.GetValues<StateEnum>()
                .Cast<int>()
                .ToArray();
            source.AddRange(intSource.Cast<int?>());
            source.Add((int?)null);
        }
        [Test]
        public void CTOR() {
            ReferenceEqualFilter filter = GetEqualFilter();
            Assert.IsNotNull(filter);
            
            Assert.AreEqual(0, filter.SelectedValues.Count);
            Assert.IsFalse(filter.IsActive);
        }
        [Test]
        public void TestAvailableValues() {
            ReferenceEqualFilter filter = GetEqualFilter();
            filter.AvailableValues = source;
            CollectionViewSource cvs = new();
            cvs.Source = filter.AvailableValues;
            ICollectionView view = cvs.View;
            var currentView = GetCollection(view);
            Assert.AreEqual(source.Count, currentView.Count);
        }
        [Test]
        public void TestEnumFilterIsMatch() {
            ReferenceEqualFilter filter = GetEqualFilter();
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
            var filtered = GetCollection(view);
            Assert.AreEqual(0, filtered.Count);
            List<int?> selected = new(new int?[] { source[0], source[1] });
            List<int?> unselected = new();
            filter.SelectedValuesChanged(addedItems: selected, removedItems: unselected);
            view.Refresh();
            filtered = GetCollection(view);
            Assert.AreEqual(selected.Count, filtered.Count);
            Assert.AreEqual(selected[0], filtered[0]);
            Assert.AreEqual(selected[1], filtered[1]);
        }
        [Test]
        public void TestAttach() {
            ReferenceEqualFilter filter = GetEqualFilter();
            var filterPresenter = FilterPresenter.TryGet(source);
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
            List<int> selected = new(new int[] { (int)StateEnum.State1, (int)StateEnum.State4 });
            List<int> unselected = new();
            filter.SelectedValuesChanged(addedItems: selected, removedItems: unselected);
            Assert.IsTrue(filter.IsActive);
            filtered = GetCollection(view);
            Assert.AreEqual(selected.Count, filtered.Count);
            Assert.AreEqual(selected[0], filtered[0]);
            Assert.AreEqual(selected[1], filtered[1]);
        }



        private static List<object> GetCollection(ICollectionView view) {
            List<object> currentView = new();
            foreach (var item in view) {
                currentView.Add(item);
            }

            return currentView;
        }

        private static ReferenceEqualFilter GetEqualFilter() {
            ReferenceEqualFilter filter = new(o=>o);
            return filter;
        }
    }
}
