﻿#pragma checksum "..\..\CompanyRegistration.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "33C8D544EC99530AB7BA97D82638988F670AAEC2AF7183F9FDAD4E046263CF8C"
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
    /// CompanyRegistration
    /// </summary>
    public partial class CompanyRegistration : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\CompanyRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\CompanyRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Type;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\CompanyRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Number;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\CompanyRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Email;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\CompanyRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Address;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\CompanyRegistration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CompanyId;
        
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
            System.Uri resourceLocater = new System.Uri("/DashBoard;component/companyregistration.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CompanyRegistration.xaml"
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
            this.Name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Type = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.Number = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Email = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.Address = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.CompanyId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 47 "..\..\CompanyRegistration.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.submit);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 48 "..\..\CompanyRegistration.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.clear);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

