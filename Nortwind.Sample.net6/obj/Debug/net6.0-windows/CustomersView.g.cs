﻿#pragma checksum "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F1D9F12C850E55CF225CD0CAF9C1B9AC6EB50D0D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.View;
using BolapanControl.ItemsFilter.ViewModel;
using Northwind.NET.EF6Model;
using Northwind.NET.Sample;
using Northwind.NET.Sample.View;
using Northwind.NET.Sample.ViewModel;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Northwind.NET.Sample.View {
    
    
    /// <summary>
    /// CustomersView
    /// </summary>
    public partial class CustomersView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Northwind.NET.Sample.View.CustomersView UserControl;
        
        #line default
        #line hidden
        
        
        #line 189 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 196 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView listCustomersTreeView;
        
        #line default
        #line hidden
        
        
        #line 275 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid1;
        
        #line default
        #line hidden
        
        
        #line 312 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox addressTextBox;
        
        #line default
        #line hidden
        
        
        #line 374 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nameTextBox;
        
        #line default
        #line hidden
        
        
        #line 391 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox regionTextBox;
        
        #line default
        #line hidden
        
        
        #line 415 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox codeTextBox;
        
        #line default
        #line hidden
        
        
        #line 433 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox contactNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 451 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox contactTitleTextBox;
        
        #line default
        #line hidden
        
        
        #line 469 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox faxTextBox;
        
        #line default
        #line hidden
        
        
        #line 487 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox iDTextBox;
        
        #line default
        #line hidden
        
        
        #line 505 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox phoneTextBox;
        
        #line default
        #line hidden
        
        
        #line 523 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox postalCodeTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Nortwind.Sample.net6;component/view/customersview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Northwind.NET.Sample\View\CustomersView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.UserControl = ((Northwind.NET.Sample.View.CustomersView)(target));
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.listCustomersTreeView = ((System.Windows.Controls.TreeView)(target));
            return;
            case 4:
            this.grid1 = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.addressTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.nameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.regionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.codeTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.contactNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.contactTitleTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.faxTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.iDTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.phoneTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 14:
            this.postalCodeTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

