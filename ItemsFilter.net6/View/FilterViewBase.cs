﻿// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace BolapanControl.ItemsFilter.View {
    /// <summary>
    /// Provide base class for filter View that include Filter as Model property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [TemplatePart(Name = FilterViewBase<T>.PART_Name, Type = typeof(TextBlock))]
    public abstract class FilterViewBase<T> : Control, IFilterView
        where T : IFilter {
        public const string PART_Name = "PART_Name";

        static FilterViewBase() {
            CommandManager.RegisterClassCommandBinding(typeof(FilterViewBase<T>),
                new CommandBinding(FilterCommand.Clear, ClearFilterExecute, ClearFilterCanExecute));
        }

        private static void ClearFilterCanExecute(object sender, CanExecuteRoutedEventArgs e) {
            var model = ((FilterViewBase<T>)sender).Model;
            e.CanExecute = model != null && model.IsActive;
        }
        private static void ClearFilterExecute(object sender, ExecutedRoutedEventArgs e) {
            var model = ((FilterViewBase<T>)sender).Model;
            if (model != null)
                model.IsActive = false;
        }
        private TextBlock? _txtName;


        /// <summary>
        /// Initializes a new instance of the <see cref="FilterViewBase&lt;T&gt;"/> class.
        /// </summary>
        public FilterViewBase() {
        }

        #region Model

        /// <summary>
        /// Model Dependency Property
        /// </summary>
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(T), typeof(FilterViewBase<T>),
                new FrameworkPropertyMetadata(default(T),
                    new PropertyChangedCallback(OnModelChanged)));

        /// <summary>
        /// Gets or sets the VievModel.
        /// </summary>
        public T? Model {
            get { return (T)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        /// <summary>
        /// Handles changes to the VievModel.
        /// </summary>
        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            FilterViewBase<T> target = (FilterViewBase<T>)d;
            T oldModel = (T)e.OldValue;
            T newModel = (T)e.NewValue;
            target.OnModelChanged(oldModel, newModel);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Model property.
        /// </summary>
        protected virtual void OnModelChanged(T oldModel, T newModel) {
        }

        #endregion


        IFilter? IFilterView.Model {
            get { return Model; }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            _txtName = GetTemplateChild(PART_Name) as TextBlock;
            InitializeBindings();
        }
        /// <summary>
        /// Initializes the bindings.
        /// </summary>
        private void InitializeBindings() {
            if (_txtName != null) {
                _txtName.SetBinding(TextBlock.TextProperty, new Binding("Model.Name") {
                    Mode = BindingMode.OneWay,
                    Source = this,
                });
            }
        }


    }
}
