﻿// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace BolapanControl.ItemsFilter.View {
    /// <summary>
    /// Defile View control for IMultiValueFilter model.
    /// </summary>
    [ModelView]
    [TemplatePart(Name = MultiValueFilterView.PART_ItemsTemplateName, Type = typeof(ListBox))]
    public class MultiValueFilterView : FilterViewBase<IMultiValueFilter> {
        public const string PART_ItemsTemplateName = "PART_Items";
        static MultiValueFilterView() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiValueFilterView),
                new FrameworkPropertyMetadata(typeof(MultiValueFilterView)));
        }
        private bool isModelAttached;
        private ListBox? _itemsCtrl;
        /// <summary>
        /// Create new instance of MultiValueFilterView;
        /// </summary>
        public MultiValueFilterView() {
            this.Unloaded += MultiValueFilterView_Unloaded;
            this.Loaded += MultiValueFilterView_Loaded;
        }
        /// <summary>
        /// Create new instance of MultiValueFilterView and accept model.
        /// </summary>
        /// <param name="model">IMultiValueFilter model</param>
        public MultiValueFilterView(object model) : this() {
            base.Model = model as IMultiValueFilter;
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Model property.
        /// </summary>
        protected override void OnModelChanged(IMultiValueFilter oldModel, IMultiValueFilter newModel) {
            DetachModel( oldModel);
            AttachModel( newModel);
        }
        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate() {
            DetachModel( Model);
            base.OnApplyTemplate();
            _itemsCtrl = (GetTemplateChild(MultiValueFilterView.PART_ItemsTemplateName) as ListBox);
            AttachModel( Model);
        }
        private void MultiValueFilterView_Loaded(object sender, RoutedEventArgs e) {
            AttachModel( Model);
        }
        private void MultiValueFilterView_Unloaded(object sender, RoutedEventArgs e) {
            DetachModel( Model);
        }

        private void AttachModel(IMultiValueFilter? newModel) {
            if (!isModelAttached && _itemsCtrl != null && newModel != null) {
                if (DesignerProperties.GetIsInDesignMode(this)) {
                    var enumerator = newModel.AvailableValues.GetEnumerator();
                    if (enumerator.MoveNext())
                        _itemsCtrl.SelectedItems.Add(enumerator.Current);
                    return;
                }
                IList selectedItems = _itemsCtrl.SelectedItems;
                selectedItems.Clear();
                foreach (var item in newModel.SelectedValues) {
                    selectedItems.Add(item);
                }
                _itemsCtrl.SelectionChanged += newModel.SelectedValuesChanged;
                ((INotifyCollectionChanged)(newModel.SelectedValues)).CollectionChanged += MultiValueFilterView_CollectionChanged;

                isModelAttached = true;
            }
        }
        private void MultiValueFilterView_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
            if (_itemsCtrl!=null) {
                if (Model != null) {
                    _itemsCtrl.SelectionChanged -= Model.SelectedValuesChanged;
                }
                if (e.Action == NotifyCollectionChangedAction.Reset) {
                    _itemsCtrl.SelectedItems.Clear();
                }
                else {
                    if (e.OldItems != null) {
                        foreach (var item in e.OldItems) {
                            int itemIndex = _itemsCtrl.SelectedItems.IndexOf(item);
                            if (itemIndex >= 0)
                                _itemsCtrl.SelectedItems.RemoveAt(itemIndex);
                        }
                    }
                    if (e.NewItems != null) {
                        foreach (var item in e.NewItems) {
                            int itemIndex = _itemsCtrl.SelectedItems.IndexOf(item);
                            if (itemIndex < 0)
                                _itemsCtrl.SelectedItems.Add(item);
                        }
                    }
                }
                if (Model != null) {
                    _itemsCtrl.SelectionChanged += Model.SelectedValuesChanged;
                } 
            }
        }
        private void DetachModel(IMultiValueFilter? oldModel) {
            if (isModelAttached && _itemsCtrl != null && oldModel != null) {
                ((INotifyCollectionChanged)(oldModel.SelectedValues)).CollectionChanged -= MultiValueFilterView_CollectionChanged;
                _itemsCtrl.SelectionChanged -= oldModel.SelectedValuesChanged;
                isModelAttached = false;
            }
        }
    }
}
