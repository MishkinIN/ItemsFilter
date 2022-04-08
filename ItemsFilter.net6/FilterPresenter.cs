// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace BolapanControl.ItemsFilter {
    // <summary>
    // FilterPresenter performs the role of a manager that manages the instantiation of filters and their connection to the CollectionView.
    // </summary>
    public sealed class FilterPresenter {
        private static readonly Dictionary<ICollectionView, WeakReference> filterPresenters = new();
        private readonly ReadOnlyCollection<ItemPropertyInfo> itemProperties;
        private int itemsDeferRefreshCount = 0;
        private IDisposable? itemsDeferRefresh = null;
        private Predicate<object>? filterFunction;
        //private bool isFilterActive;
        private readonly ICollectionView collectionView;
        private readonly Dictionary<string, Dictionary<Type, Filter>> filters;
        private event FilterEventHandler? FilterAction;
        //private readonly FilteredEventArgs filteredEventArgs;
        // <summary>
        // Returns FilterPresenter, connected to a pass source .
        // If pass instance of ICollectionView, FilterPresenter connected to passed instance, otherwise, filterPresenter connected to default view for passed collection.
        // </summary>
        // <param name="source">ICollectionView for source or source</param>
        // <returns>FilterPresenter, connected to source, or null if source is null.</returns>
        public static FilterPresenter Get(IEnumerable source) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(source);
#endif
            ICollectionView sourceCollectionView = (source as ICollectionView) ?? CollectionViewSource.GetDefaultView(source);
            //FilterPresenter? instance = null;
            //GC.Collect();
            foreach (var entry in filterPresenters.Where(kvp => !kvp.Value.IsAlive).ToArray()) {
                filterPresenters.Remove(entry.Key);
            }
            //WeakReference wr;
            if (filterPresenters.TryGetValue(sourceCollectionView, out var wr)
                && wr.IsAlive
                && wr.Target is FilterPresenter instance) {
                return instance;
            }
            else {
                instance = new FilterPresenter(sourceCollectionView);
                filterPresenters[sourceCollectionView] = new WeakReference(instance);
                return instance;
            }
        }

        private FilterPresenter(ICollectionView source) {
            collectionView = source;
            itemProperties = (source as IItemProperties)?.ItemProperties ?? new ReadOnlyCollection<ItemPropertyInfo>(Array.Empty<ItemPropertyInfo>());
            filters = new Dictionary<string, Dictionary<Type, Filter>>();
        }

        // <summary>
        // Returns view of the source collection that have the functionalities of custom filtering.
        // </summary>
        public ICollectionView CollectionView {
            get { return collectionView; }
        }

        /// <summary>
        /// Get or set a value that indicates whether the defined filter set to attached ItemsControl.Items.PropertyFilter.
        /// </summary>
        //public bool IsFilterActive {
        //    get {
        //        return isFilterActive;
        //    }
        //    set {
        //        if (isFilterActive != value) {
        //            using (var defer = DeferRefresh()) {
        //                isFilterActive = value;
        //                if (!isFilterActive) {
        //                    foreach (var filter in filters) {

        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        // <summary>
        // Initializes and configures the ViewModel for FilterControl.
        // </summary>
        // <param name="viewKey">A string representing the key for a set of filters.</param>
        // <param name="filterInitializers"> Filter initialisers to determine permissible set of the filters in the FilterControlVm.</param>
        // <returns>Instance of FilterControlVm that was bind to view.</returns>
        public FilterControlVm TryGetFilterControlVm(string viewKey, IEnumerable<FilterInitializer>? filterInitializers) {
            if (viewKey is null) {
                return FilterControlVm.Empty;
            }
            FilterControlVm viewModel = new();
            var filtersEntry = GetFiltersEntry(viewKey);
            filterInitializers ??= FilterInitializersManager.Default;

            foreach (FilterInitializer initializer in filterInitializers) {
                if (TryGetFilter(filtersEntry, initializer, viewKey, out Filter? filter)) {
#pragma warning disable CS8604 // Possible null reference argument.
                    viewModel.Add(filter);
#pragma warning restore CS8604 // Possible null reference argument.
                };
            }
            //view.ItemsSource = viewModel; 
            return viewModel;
        }
        /// <summary>
        /// Retrieves  or tries to create the filter model, using as a key pair {viewKey, initializer}.
        /// </summary>
        /// <param name="viewKey">A string representing a key of the set of filters.</param>
        /// <param name="initializer">Initialiser filter that defines filter in the collection of filters.</param>
        /// <param name="filter">When this method returns, contains the instatce of filter for couples viewKey and initializer</param>
        /// <returns>True, if Filter instance are created. Otherwise, false.</returns>
        public bool TryGetFilter(string viewKey, FilterInitializer initializer, out Filter? filter) {
#if DEBUG
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(initializer);
#endif   
            if (viewKey != null) {
                // Get registered collection by key.
                var filtersEntry = GetFiltersEntry(viewKey);
                Type filterKey = initializer.GetType();
                return TryGetFilter(filtersEntry, initializer, viewKey, out filter);
            }
            filter = null;
            return false;
        }
        private bool TryGetFilter(Dictionary<Type, Filter> filtersEntry, FilterInitializer initializer, string viewKey, out Filter? filter) {
            Type filterKey = initializer.GetType();
            if (!filtersEntry.TryGetValue(filterKey, out filter)) {
                filter = initializer.TryCreateFilter(this, viewKey);
                if (filter != null) {
                    filtersEntry[filterKey] = filter;
                }
            }
            return filter is not null;
        }
        private Dictionary<Type, Filter> GetFiltersEntry(string viewKey) {
            if (!filters.TryGetValue(viewKey, out Dictionary<Type, Filter>? entry)) {
                entry = new Dictionary<Type, Filter>();
                filters.Add(viewKey, entry);
            }
            return entry;
        }
        /// <summary>
        ///  Enters a defer cycle that you can use to change filter of the view and delay automatic refresh.
        /// </summary>
        /// <returns> An System.IDisposable object that you can use to dispose of the calling object. </returns>
        public IDisposable DeferRefresh() {
            return new DisposeItemsDeferRefresh(this);
        }
        /// <summary>
        /// Gets a collection that contains information about the properties that are
        /// available on the items in a collection.
        /// </summary>
        public ReadOnlyCollection<ItemPropertyInfo> ItemProperties {
            get { return itemProperties; }
        }
        // Represent a set of Predicate<Object> that used to generate filter function.
        internal event FilterEventHandler? Filter {
            add {
                if (filterFunction == null)
                    filterFunction = FilterFunction;
                using var deferRefresh = DeferRefresh();
                FilterAction += value;
                //IsFilterActive = true;
            }
            remove {
                using var deferRefresh = DeferRefresh();
                FilterAction -= value;
                //IsFilterActive = FilterAction != null;
                if (FilterAction == null)
                    filterFunction = null;
            }
        }
        // Сообщает FilterPresenter об изменении состояния фильтра.
        // Для экземпляра фильтра в активном состоянии, производится включение фильтра в условие фильтрации представления коллекции.
        // Для экземпляра фильтра в пассивном состоянии, производится исключение фильтра из условия фильтрации коллекции.
        /// <summary>
        /// Receives notice of the change filter conditions and IsActive property.
        /// </summary>
        /// <param name="filter"></param>
        internal void ReceiveFilterChanged(IFilter filter) {
            using (var defer = DeferRefresh()) {
                Filter -= filter.IsMatch;
                if (filter.IsActive)
                    Filter += filter.IsMatch;
            }
        }
        private bool FilterFunction(object obj) {
            if (FilterAction != null) {
                FilterEventArgs args = new(obj);
                FilterAction(this, args);
                return args.Accepted;
            }
            else
                return true;
        }
        private class DisposeItemsDeferRefresh : IDisposable {
            private readonly FilterPresenter filterPr;
            private bool isDisposed = false;
            internal DisposeItemsDeferRefresh(FilterPresenter filterpr) {
#if DEBUG
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(filterpr);
#endif
                this.filterPr = filterpr;
                if (filterPr.CollectionView is IEditableCollectionView cv) {
                    if (cv.IsAddingNew)
                        cv.CommitNew();
                    if (cv.IsEditingItem)
                        cv.CommitEdit();
                }
                if (filterPr.itemsDeferRefreshCount == 0)
                    filterPr.itemsDeferRefresh = filterPr.CollectionView.DeferRefresh();
                filterPr.itemsDeferRefreshCount++;
            }
            public void Dispose() {
                if (!isDisposed) {
                    filterPr.itemsDeferRefreshCount--;
                    if (filterPr.itemsDeferRefreshCount <= 0) {
                        filterPr.itemsDeferRefreshCount = 0;
                        if (filterPr.CollectionView is IEditableCollectionView cv) {
                            if (cv.IsAddingNew)
                                cv.CancelNew();
                            if (cv.IsEditingItem)
                                cv.CancelEdit();
                        }
                        filterPr.CollectionView.Filter = filterPr.filterFunction;
                        //if (filterPr.isFilterActive) {
                        //}
                        //else //if (filterVm.items.PropertyFilter==null)
                        //    filterPr.CollectionView.Filter = null;
                        if (filterPr.itemsDeferRefresh != null) {
                            filterPr.itemsDeferRefresh.Dispose();
                        }
                        filterPr.itemsDeferRefresh = null;
                    }
                    isDisposed = true;
                }
                else
                    throw new ObjectDisposedException(nameof(FilterPresenter.DisposeItemsDeferRefresh));
            }
        }

    }
}
