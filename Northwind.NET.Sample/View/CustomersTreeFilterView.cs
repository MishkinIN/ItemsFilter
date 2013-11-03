using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.View;
using Northwind.NET.Sample.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Northwind.NET.Sample.View {
    [ModelView]
    public class CustomersTreeFilterView : FilterViewBase<CustomersTreeFilter> {
        static CustomersTreeFilterView() {
            ControlTemplate defaultTemplate = new CustomersTreeFilterViewTemplate();
            TemplateProperty.OverrideMetadata(typeof(CustomersTreeFilterView), new System.Windows.FrameworkPropertyMetadata(defaultTemplate));
        }
        public CustomersTreeFilterView(CustomersTreeFilter model)
            : base() {
            base.DataContext= base.Model = model;
        }
    }
}
