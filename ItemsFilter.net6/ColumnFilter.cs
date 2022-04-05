// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace BolapanControl.ItemsFilter {
    /// <summary>
    /// Represent BolapanControls.PropertyFilter.ColumnFilter control, which show filter View for associated Model.
    /// If defined as part of System.Windows.Controls.Primitives.DataGridColumnHeader template, represent filter View for current column.
    /// </summary>
    [TemplateVisualState(GroupName = "FilterState", Name = "Disable")]
    [TemplateVisualState(GroupName = "FilterState", Name = "Enable")]
    [TemplateVisualState(GroupName = "FilterState", Name = "Active")]
    [TemplateVisualState(GroupName = "FilterState", Name = "Open")]
    [TemplateVisualState(GroupName = "FilterState", Name = "OpenActive")]
    [TemplatePart(Name = ColumnFilter.PART_FiltersView, Type = typeof(Popup))]
    public class ColumnFilter : FilterControl {
        internal const string PART_FiltersView = "PART_FilterView";
        #region BindingPath

        /// <summary>
        /// BindingPath Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty BindingPathProperty =
            DependencyProperty.RegisterAttached("BindingPath", typeof(string), typeof(ColumnFilter),
                new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets the BindingPath property. This dependency property 
        /// indicates path to the property in ParentCollection.
        /// </summary>
        public static string GetBindingPath(DependencyObject d) {
            return (string)d.GetValue(BindingPathProperty);
        }

        /// <summary>
        /// Sets the BindingPath property. This dependency property 
        /// indicates path to the property in ParentCollection.
        /// </summary>
        public static void SetBindingPath(DependencyObject d, string value) {
            d.SetValue(BindingPathProperty, value);
        }

        #endregion
        #region Initializers

        /// <summary>
        /// Initializers Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty InitializersProperty =
            DependencyProperty.RegisterAttached(nameof(Initializers), typeof(FilterInitializersManager), typeof(ColumnFilter),
                new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets the FilterInitializersManager  that used for generate ColumnFilter.Model. 
        /// </summary>
        public static FilterInitializersManager GetInitializers(DependencyObject d) {
            return (FilterInitializersManager)d.GetValue(InitializersProperty);
        }

        /// <summary>
        /// Sets the FilterInitializersManager that used for generate ColumnFilter.Model.
        /// </summary>
        public static void SetInitializers(DependencyObject d, FilterInitializersManager value) {
            d.SetValue(InitializersProperty, value);
        }
        public FilterInitializersManager Initializers {
            get => (FilterInitializersManager)GetValue(InitializersProperty);
            set => SetValue(InitializersProperty, value);
        }
        #endregion
        

        static ColumnFilter() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColumnFilter), new FrameworkPropertyMetadata(typeof(ColumnFilter)));
            CommandManager.RegisterClassCommandBinding(typeof(ColumnFilter),
                new CommandBinding(FilterCommand.Show, DoShowFilter, CanShowFilter));
        }

        private Popup? partFilterView;
        /// <summary>
        /// Create new instance of BolapanControls.PropertyFilter.ColumnFilter class.
        /// </summary>
        public ColumnFilter() {
            //CommandBindings.Add(new CommandBinding(FilterCommand.Show,DoShowFilter,CanShowFilter));
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            partFilterView = GetTemplateChild(PART_FiltersView) as Popup;
        }

        /// <summary>
        /// Initializes the filter view model.
        /// </summary>
        protected override FilterControlVm CreateModel() {
            FilterControlVm vm = FilterControlVm.Empty;
            filterPresenter = ParentCollection == null ? null : FilterPresenter.Get(ParentCollection);
            if (filterPresenter != null) {
                if (Key == null) {
                    DataGridColumnHeader? columnHeader = this.GetParent<DataGridColumnHeader>();
                    if (columnHeader == null)
                        return vm;
                    DataGridColumn? column = columnHeader.Column;
                    if (column == null) {
                        return vm;
                    }
                    DataGrid? dataGrid = columnHeader.GetParent<DataGrid>();
                    if (dataGrid == null) {
                        return vm;
                    }
                    if (column.DisplayIndex >= dataGrid.Columns.Count) {
                        return vm;
                    }
                    IEnumerable<FilterInitializer> initializers = GetInitializers(column) ?? FilterInitializersManager;
                    string key = GetColumnKey(column);
                    vm = filterPresenter.TryGetFilterControlVm(key, initializers);
                }
                else {
                    IEnumerable<FilterInitializer> initializers = FilterInitializersManager;
                    vm = filterPresenter.TryGetFilterControlVm(Key, initializers);
                }
                if (vm != FilterControlVm.Empty)
                    vm.IsEnable = true;
            }
            return vm;
        }
        //
        // Summary:
        //     Invoked when an unhandled System.Windows.UIElement.MouseLeftButtonDown routed
        //     event is raised on this element. Implement this method to add class handling
        //     for this event.
        //
        // Parameters:
        //   e:
        //     The System.Windows.Input.MouseButtonEventArgs that contains the event data.
        //     The event data reports that the left mouse button was pressed.
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            e.Handled = true;
            base.OnMouseLeftButtonDown(e);

        }
        private static void CanShowFilter(object sender, CanExecuteRoutedEventArgs e) {
            ColumnFilter filterControl = (ColumnFilter)sender;
            e.CanExecute = filterControl.Model != null 
                && filterControl.Model.IsEnable 
                && filterControl.partFilterView != null;
        }

        private static void DoShowFilter(object sender, ExecutedRoutedEventArgs e) {
            ((ColumnFilter)sender).Model.IsOpen = true;
        }

        private static string GetColumnKey(DataGridColumn column) {
            string bindingPath = string.Empty;
            if (GetBindingPath(column) is string attachedBinding && !string.IsNullOrWhiteSpace(attachedBinding)) {
                bindingPath = attachedBinding;
            }
            else if (column is DataGridBoundColumn columnBound && columnBound?.Binding is Binding binding) {
                bindingPath = binding.Path.Path;
            }
            else if (column is DataGridTemplateColumn templateColumn
                && templateColumn.Header is string header) {
                bindingPath = header;
            }
            else if (column is DataGridComboBoxColumn comboBoxColumn
                && comboBoxColumn.SelectedItemBinding is Binding _binding) {
                bindingPath = _binding.Path.Path;
            }
            return bindingPath;
        }
    }
}
