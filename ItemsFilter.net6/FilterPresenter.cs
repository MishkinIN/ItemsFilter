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
    public sealed class FilterPresenter  {
        private static readonly Dictionary<ICollectionView, WeakReference> filterPresenters = new();
        private readonly ReadOnlyCollection<ItemPropertyInfo> itemProperties;
        private int itemsDeferRefreshCount = 0;
        private IDisposable? itemsDeferRefresh = null;
        private Predicate<object>? filterFunction;
        private bool isFilterActive;
        private readonly ICollectionView collectionView;
        private readonly Dictionary<string, FiltersCollection> filters;
        private event FilterEventHandler? FilterAction;
        private readonly FilteredEventArgs filteredEventArgs;
        // <summary>
        // Returns FilterPresenter, connected to a pass source .
        // If pass instance of ICollectionView, FilterPresenter connected to passed instance, otherwise, filterPresenter connected to default view for passed collection.
        // </summary>
        // <param name="source">ICollectionView for source or source</param>
        // <returns>FilterPresenter, connected to source, or null if source is null.</returns>
        public static FilterPresenter? TryGet(IEnumerable? source) {
            if (source == null)
                return null;
            ICollectionView sourceCollectionView = (source as ICollectionView) ?? CollectionViewSource.GetDefaultView(source);
            FilterPresenter? instance = null;
            //GC.Collect();
            foreach (var entry in filterPresenters.Where(kvp=>!kvp.Value.IsAlive).ToArray()) {
                    filterPresenters.Remove(entry.Key);
            }
            if (filterPresenters.ContainsKey(sourceCollectionView)) {
                var wr = filterPresenters[sourceCollectionView];
                instance = wr.Target as FilterPresenter;
            }
            else {
                instance = new FilterPresenter(sourceCollectionView);
                filterPresenters[sourceCollectionView] = new WeakReference(instance);
            }
            return instance;
        }

        private FilterPresenter(ICollectionView source) {
            collectionView = source;
            filteredEventArgs = new FilteredEventArgs(source);
            itemProperties = (source as IItemProperties)?.ItemProperties ?? new ReadOnlyCollection<ItemPropertyInfo>(Array.Empty<ItemPropertyInfo>());
            filterFunction = new Predicate<object>(FilterFunction);
            filters = new Dictionary<string, FiltersCollection>();
        }

        // <summary>
        // Returns the connected  collection.
        // </summary>
        public ICollectionView CollectionView {
            get { return collectionView; }
        }

        /// <summary>
        /// Get or set a value that indicates whether the defined filter set to attached ItemsControl.Items.PropertyFilter.
        /// </summary>
        public bool IsFilterActive {
            get {
                return isFilterActive;
            }
            set {
                if (isFilterActive != value) {
                    using (var defer = DeferRefresh()) {
                        isFilterActive = value; 
                    }
                }
            }
        }
        // <summary>
        // Initializes and configures the ViewModel for FilterControl.
        // </summary>
        // <param name="viewKey">A string representing the key for a set of filters.</param>
        // <param name="filterInitializers"> Filter initialisers to determine permissible set of the filters in the FilterControlVm.</param>
        // <returns>Instance of FilterControlVm that was bind to view.</returns>
        public FilterControlVm TryGetFilterControlVm(string viewKey, IEnumerable<FilterInitializer> filterInitializers) {
            //string viewKey = view.Key;
            FilterControlVm viewModel = FilterControlVm.Empty;
            if (viewKey != null) {
                FiltersCollection filtersEntry;
                // Get registered collection by key.
                if (filters.ContainsKey(viewKey))
                    filtersEntry = filters[viewKey];
                else {
                    filtersEntry = new FiltersCollection(this);
                    filters.Add(viewKey, filtersEntry);
                }
                filterInitializers ??= FilterInitializersManager.Default;

                foreach (FilterInitializer initializer in filterInitializers) {
                    Type filterKey = initializer.GetType();
                    Filter? filter;
                    if (filtersEntry.ContainsKey(filterKey))
                        filter = filtersEntry[filterKey];
                    else {
                        filter = initializer.TryGetFilter(this, viewKey);
                        if (filter != null)
                            filtersEntry[filterKey] = filter;
                    }
                    if (filter != null) {
                        viewModel = viewModel == FilterControlVm.Empty ? new FilterControlVm() : viewModel;
                        viewModel.Add(filter);
                    }
                }
                //view.ItemsSource = viewModel; 
            }
            return viewModel;
        }
        /// <summary>
        /// Retrieves  or tries to create the filter model, using as a key pair {viewKey, initializer}.
        /// </summary>
        /// <param name="viewKey">A string representing a key of the set of filters.</param>
        /// <param name="initializer">Initialiser filter that defines filter in the collection of filters.</param>
        /// <returns>FilterPresenter instance, if it is possible provide for couples viewKey and initializer. Otherwise, null.</returns>
        public Filter? TryGetFilter(string viewKey, FilterInitializer initializer) {
            Filter? filter = null;
            if (viewKey != null) {
                FiltersCollection? filtersEntry;
                // Get registered collection by key.
                if (!filters.TryGetValue(viewKey, out filtersEntry)) {
                    filtersEntry = new FiltersCollection(this);
                    filters.Add(viewKey, filtersEntry);
                }
                Type filterKey = initializer.GetType();
                if (filtersEntry.ContainsKey(filterKey))
                    filter = filtersEntry[filterKey];
                else {
                    filter = initializer.TryGetFilter(this, viewKey);
                    if (filter != null)
                        filtersEntry[filterKey] = filter;
                }
            }
            return filter;
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
        /// <summary>
        /// Occurs after filtration when changing the filter conditions.
        /// </summary>
        public EventHandler<FilteredEventArgs>? Filtered;
        // Represent a set of Predicate<Object> that used to generate filter function.
        internal event FilterEventHandler? Filter {
            add {
                if (filterFunction == null)
                    filterFunction = new Predicate<object>(FilterFunction);
                using var deferRefresh = DeferRefresh();
                FilterAction += value;
                IsFilterActive = true;
            }
            remove {
                using var deferRefresh = DeferRefresh();
                FilterAction -= value;
                IsFilterActive = FilterAction != null;
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
        private void RaiseFiltered() {
            lock (filteredEventArgs) {
                Filtered?.Invoke(this, filteredEventArgs);
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
                        if (filterPr.isFilterActive) {
                            filterPr.CollectionView.Filter = filterPr.filterFunction;
                        }
                        else //if (filterVm.items.PropertyFilter==null)
                            filterPr.CollectionView.Filter = null;
                        filterPr.RaiseFiltered();
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
