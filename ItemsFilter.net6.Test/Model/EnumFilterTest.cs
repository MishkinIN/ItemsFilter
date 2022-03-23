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
    public class EnumFilterTest {
        private List<string> enumNames=new();
        [SetUp]
        public void Setup() {
            enumNames = new List<string>( typeof(StateEnum).GetEnumNames());
        }
        [Test]
        public void CTOR() {
            EnumFilter<StateEnum> filter = GetEnumFilter<StateEnum>();
            var values = filter.AvailableValues as Array;
            Assert.IsNotNull(values);
            Assert.AreEqual(enumNames.Count, values?.Length);
        }
        [Test]
        public void TestAvailableValues() {
            EnumFilter<StateEnum> filter = GetEnumFilter<StateEnum>();
            Assert.IsFalse(filter.IsActive);
            CollectionViewSource cvs = new();
            cvs.Source = filter.AvailableValues;
            ICollectionView view = cvs.View;
            var currentView = GetCollection(view);
            Assert.AreEqual(enumNames.Count, currentView.Count);
        }
        [Test]
        public void TestFilterIsMatch() {
            EnumFilter<StateEnum> filter = GetEnumFilter<StateEnum>();
            CollectionViewSource cvs = new();
            cvs.Source = filter.AvailableValues;
            ICollectionView view = cvs.View;
            using (IDisposable defer = view.DeferRefresh()) {
                view.Filter = el => {
                    BolapanControl.ItemsFilter.FilterEventArgs e = new(el);
                    filter.IsMatch(null, e);
                    return e.Accepted;
                };
            }
            var filtered = GetCollection(view);
            Assert.AreEqual(enumNames.Count, filtered.Count);
            List<StateEnum> selected = new(new StateEnum[] { StateEnum.State1, StateEnum.State4 });
            List<StateEnum> unselected = new();
            filter.SelectedValuesChanged(addedItems: selected, removedItems: unselected);
            view.Refresh();
            filtered = GetCollection(view);
            Assert.AreEqual(selected.Count, filtered.Count);
            Assert.AreEqual(selected[0], filtered[0]);
            Assert.AreEqual(selected[1], filtered[1]);
        }
        [Test]
        public void TestAttach() {
            EnumFilter<StateEnum> filter = GetEnumFilter<StateEnum>();
            var filterPresenter = FilterPresenter.Get(filter.AvailableValues);
            Assert.IsNotNull(filterPresenter);
            Assert.IsFalse(filter.IsActive);
#pragma warning disable CS8604 // Possible null reference argument.
            filter.Attach(filterPresenter);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var view = filterPresenter.CollectionView;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            var filtered = GetCollection(view);
            Assert.AreEqual(enumNames.Count, filtered.Count);
            List<StateEnum> selected = new(new StateEnum[] { StateEnum.State1, StateEnum.State4 });
            List<StateEnum> unselected = new();
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

        private static EnumFilter<T> GetEnumFilter<T>()
            where T:Enum{
            EnumFilter<T> filter = new(o=>o);
            return filter;
        }
    }
}
