﻿#pragma checksum "..\..\Dashborad.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "011364556C9307BE93BDC581E14AA16B27FF7B5A5E2684A056F1ED919568D217"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DashBoard;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace DashBoard {
    
    
    /// <summary>
    /// Dashborad
    /// </summary>
    public partial class Dashborad : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\Dashborad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TopBar_Company;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\Dashborad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid TopBar_Bill;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\Dashborad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid logo_and_customer_details;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\Dashborad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid billing_list;
        
        #line default
        #line hidden
        
        
        #line 214 "..\..\Dashborad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid menu;
        
        #line default
        #line hidden
        
        
        #line 238 "..\..\Dashborad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid amount_list_and_payments;
        
        #line default
        #line hidden
        
        
        #line 244 "..\..\Dashborad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid amount_list;
        
        #line default
        #line hidden
        
        
        #line 290 "..\..\Dashborad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid virtual_keyboard;
        
        #line default
        #line hidden
        
        
        #line 329 "..\..\Dashborad.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid payment_types;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DashBoard;component/dashborad.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Dashborad.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TopBar_Company = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.TopBar_Bill = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.logo_and_customer_details = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            
            #line 128 "..\..\Dashborad.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.opendNewSalesMan);
            
            #line default
            #line hidden
            return;
            case 5:
            this.billing_list = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.menu = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.amount_list_and_payments = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.amount_list = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.virtual_keyboard = ((System.Windows.Controls.Grid)(target));
            return;
            case 10:
            this.payment_types = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

