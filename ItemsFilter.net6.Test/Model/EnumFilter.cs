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

namespace ItemsFilter.net6.Test.Model {
    internal class EnumFilter {
        private static EnumContentElement ui_content = new();
        private string[]? enumNames;
        [SetUp]
        public void Setup() {
            enumNames = typeof(StateEnum).GetEnumNames();
        }
        [Test]
        public void TestConstructor() {
            EnumFilter<StateEnum> filter = GetEnumFilter<StateEnum>();
            var values = filter.AvailableValues as Array;
            Assert.IsNotNull(values);
            Assert.AreEqual(enumNames?.Length, values?.Length);
        }
        [Test]
        public void TestAvailableValues() {
            EnumFilter<StateEnum> filter = GetEnumFilter<StateEnum>();
            Assert.IsFalse(filter.IsActive);
            CollectionViewSource cvs = new();
            cvs.Source = filter.AvailableValues;
            ICollectionView view = cvs.View;
            var currentView = GetCollection(view);
            Assert.AreEqual(enumNames?.Length, currentView.Count);
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
            var pd = DependencyPropertyDescriptor
                .FromName(nameof(EnumContentElement.State),
                    typeof(EnumContentElement), typeof(EnumContentElement));
            var pi = new ItemPropertyInfo(nameof(EnumContentElement.StateEnumProp),
                typeof(EnumContentElement), pd);
            EnumFilter<T> filter = new(pi);
            return filter;
        }
    }
}
