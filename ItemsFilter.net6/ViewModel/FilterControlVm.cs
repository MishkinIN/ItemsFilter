// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BolapanControl.ItemsFilter.ViewModel {
    public delegate void FilterControlStateChangedEventHandler(FilterControlVm sender, FilterControl.State newValue);
    /// <summary>
    /// View model for <c>FilterControl</c>.
    /// FilterControlVm is IEnumerable&lt;Filter&gt;. 
    /// An instance of FilterControlVm not created directly. Instead, the procedure FilterPresenter.TryGetFilterControlVm(string viewKey, IEnumerable&lt;FilterInitializer&gt; filterInitializers)
    /// prepares a model for FilterControl, as IEnumerable&lt;Filter&gt;, where each Filter instance bound to FilterControlVm for the transmission of changes.
    /// Usually, instance of FilterControlVm created by FilterControl when it need this and disposed on raise Unload event.
    /// </summary>
    public class FilterControlVm : ObservableCollection<Filter>, IDisposable {
        private static readonly Lazy<FilterControlVm> lzEmpty = new(() => new EmptyFilterControlVm());
        public static FilterControlVm Empty { get => lzEmpty.Value; }
        private readonly object lockFlag = new();
        private bool isDisposed = false;
        private bool isActive = false;
        private bool isOpen = false;
        private bool isEnable = false;
        private FilterControl.State state = FilterControl.State.Enable;
        internal FilterControlVm() { }
        /// <summary>
        /// Get FilterControl state.
        /// </summary>
        public FilterControl.State State {
            get { return state; }
            private set {
                if (state != value) {
                    state = value;
                    var handler = StateChanged;
                    if (handler != null) {
                        lock (handler) {
                            handler(this, state);
                        }
                    };
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(State)));
                }
            }
        }
        /// <summary>
        /// Provede FilterControlStateChanged event.
        /// </summary>
        public event FilterControlStateChangedEventHandler? StateChanged;
        /// <summary>
        /// Get or set FilterControl.IsEnabled.
        /// </summary>
        public bool IsEnable {
            get { return isEnable; }
            set {
                if (isEnable != value) {
                    isEnable = value;
                    if (value)
                        State |= FilterControl.State.Enable;
                    else
                        State &= ~FilterControl.State.Enable;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsEnable)));
                }
            }
        }
        /// <summary>
        /// Gets or sets whether the popup is currently displaying on the screen.
        /// </summary>
        public bool IsOpen {
            get { return isOpen; }
            set {
                if (isOpen != value) {
                    isOpen = value;
                    if (value)
                        State |= FilterControl.State.Open;
                    else
                        State &= ~FilterControl.State.Open;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsOpen)));
                }
            }
        }
        /// <summary>
        /// Get or set whether filter is active.
        /// </summary>
        public bool IsActive {
            get { return isActive; }
            set {
                if (isActive != value) {
                    isActive = value;
                    if (value)
                        State |= FilterControl.State.Active;
                    else
                        State &= ~FilterControl.State.Active;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsActive)));
                }
            }
        }
        /// <summary>
        /// Gets whether Dispose has been called.
        /// </summary>
        public bool IsDisposed {
            get { return isDisposed; }

        }

        //
        // Summary:
        //     Inserts a filter into the collection at the specified index and attach filter to FilterControlVm.
        //
        // Parameters:
        //   index:
        //     The zero-based index at which item should be inserted.
        //
        //   item:
        //     The object to insert.
        protected override void InsertItem(int index, Filter filter) {
            if (!isDisposed) {
                base.InsertItem(index, filter);
                filter.PropertyChanged += Filter_PropertyChanged;
                RaiseFilterChanged();
            }
        }

        //
        // Summary:
        //    Detach FilterControlVm from all filters and remove all filters from collection.
        protected override void ClearItems() {
            foreach (var filter in base.Items) {
                filter.PropertyChanged -= Filter_PropertyChanged;
            }
            RaiseFilterChanged();
            base.ClearItems();
        }
        //
        // Summary:
        //     Replaces the filter at the specified index. 
        //     Detach FilterControlVm from removed filter and attach FilterControlVm to new filter.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to replace.
        //
        //   item:
        //     The new value for the element at the specified index.
        protected override void SetItem(int index, Filter filter) {
            if (!isDisposed) {
                base[index].PropertyChanged -= Filter_PropertyChanged;
                base.SetItem(index, filter);
                filter.PropertyChanged += Filter_PropertyChanged;
                RaiseFilterChanged();
            }
        }
        //
        // Summary:
        //     Moves the filter at the specified index to a new location in the collection.
        //
        // Parameters:
        //   oldIndex:
        //     The zero-based index specifying the location of the item to be moved.
        //
        //   newIndex:
        //     The zero-based index specifying the new location of the item.
        protected override void MoveItem(int oldIndex, int newIndex) {
            if (!isDisposed) {
                base.MoveItem(oldIndex, newIndex);
            }
        }
        //
        // Summary:
        //     Detach FilterControlVm from filter at the specified index of the collection and removes filter.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to remove.
        protected override void RemoveItem(int index) {
            if (!isDisposed) {
                base[index].PropertyChanged -= Filter_PropertyChanged;
                base.RemoveItem(index);
                RaiseFilterChanged();
            }
        }

        /// <summary>
        /// Detach FilterControlVm from all filters and remove all filters from collection.
        /// </summary>
        public void Dispose() {
            lock (lockFlag) {
                if (!isDisposed) {
                    Clear();
                    isDisposed = true;
                    GC.SuppressFinalize(this);
                }
            }
        }
        internal void RaiseFilterChanged() {
            bool active = false;
            foreach (var item in base.Items) {
                active |= item.IsActive;
            }
            IsActive = active;
        }
        private void Filter_PropertyChanged(object? sender, PropertyChangedEventArgs e) {
            if (e.PropertyName==nameof(Filter.IsActive)) {
                RaiseFilterChanged(); 
            }
        }
    }
}
