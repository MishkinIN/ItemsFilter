// ****************************************************************************
// <author>Jonas Tampier</author>
// <email>jonas@tampier.de</email>
// <date>02.10.2017</date>
// <project>ItemsFilter</project>
// <license> GNU Lesser General Public License version 3 (LGPLv3) </license>
// based on code from Ivan Mishkin
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace BolapanControl.ItemsFilter.View {
    /// <summary>
    /// Define View control for IRangeFilter model.
    /// </summary>
    [ModelView]
    [TemplatePart(Name = PART_From, Type = typeof(DatePicker))]
    [TemplatePart(Name = PART_To, Type = typeof(DatePicker))]
    public class DateTimeFilterView : FilterViewBase<IRangeFilter>
    {
        const string PART_From = "PART_From";
        const string PART_To = "PART_To";
        static DateTimeFilterView() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateTimeFilterView),
                new FrameworkPropertyMetadata(typeof(DateTimeFilterView)));

        }
        private DatePicker datePickerFrom, datePickerTo;
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeFilterView"/> class.
        /// </summary>
        public DateTimeFilterView():base() {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeFilterView"/> class and accept model.
        /// </summary>
        /// <param name="model">IRangeFilter model</param>
        public DateTimeFilterView(object model)
        {
            base.Model = model as IRangeFilter;
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate() {
            if (datePickerFrom != null)
            {
                datePickerFrom.RemoveHandler(DatePicker.KeyDownEvent, new KeyEventHandler(DatePicker_KeyDown));
                datePickerFrom.RemoveHandler(DatePicker.LostFocusEvent, new RoutedEventHandler(DatePicker_LostFocus));
            }
            if (datePickerTo != null)
            {
                datePickerTo.RemoveHandler(DatePicker.KeyDownEvent, new KeyEventHandler(DatePicker_KeyDown));
                datePickerTo.RemoveHandler(DatePicker.LostFocusEvent, new RoutedEventHandler(DatePicker_LostFocus));
            }
            base.OnApplyTemplate();
            datePickerTo = GetTemplateChild(PART_To) as DatePicker;
            datePickerFrom = GetTemplateChild(PART_From) as DatePicker;
            if (datePickerFrom != null)
            {
                datePickerFrom.AddHandler(DatePicker.KeyDownEvent, new KeyEventHandler(DatePicker_KeyDown), true);
                datePickerFrom.AddHandler(DatePicker.LostFocusEvent, new RoutedEventHandler(DatePicker_LostFocus), true);
            }
            if (datePickerTo != null)
            {
                datePickerTo.AddHandler(DatePicker.KeyDownEvent, new KeyEventHandler(DatePicker_KeyDown), true);
                datePickerTo.AddHandler(DatePicker.LostFocusEvent, new RoutedEventHandler(DatePicker_LostFocus), true);
            }
        }
        private void DatePicker_LostFocus(object sender, RoutedEventArgs e) {
            UpdateDatePickerSource((DatePicker)sender);
        }
        private void DatePicker_KeyDown(object sender, KeyEventArgs e) {
            switch (e.Key) {
                case Key.Enter: {
                    UpdateDatePickerSource((DatePicker)sender);
                        e.Handled = true;
                        return;
                    }
            }
        }
        private static void UpdateDatePickerSource(DatePicker dp)
        {
            dp.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
        }

    }
}
