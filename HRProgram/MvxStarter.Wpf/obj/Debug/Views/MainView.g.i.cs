﻿#pragma checksum "..\..\..\Views\MainView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F8DB24E71F095D3DB30745219CF715E5AE13732874837D5A76D93E416B7EDD28"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using HRProgram.Core.ViewModels;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using MvvmCross.Platforms.Wpf.Binding;
using MvvmCross.Platforms.Wpf.Views;
using MvxStarter.Wpf.Service;
using MvxStarter.Wpf.Views;
using ShowMeTheXAML;
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
using System.Windows.Interactivity;
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


namespace MvxStarter.Wpf.Views {
    
    
    /// <summary>
    /// MainView
    /// </summary>
    public partial class MainView : MvvmCross.Platforms.Wpf.Views.MvxWpfView, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 75 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView EmployeesListView;
        
        #line default
        #line hidden
        
        
        #line 241 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView SummaryListView;
        
        #line default
        #line hidden
        
        
        #line 363 "..\..\..\Views\MainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView PositionsListView;
        
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
            System.Uri resourceLocater = new System.Uri("/MvxStarter.Wpf;component/views/mainview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\MainView.xaml"
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
            this.EmployeesListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 6:
            this.SummaryListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            this.PositionsListView = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 2:
            
            #line 114 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ViewSummaryEmployeeClick);
            
            #line default
            #line hidden
            break;
            case 3:
            
            #line 143 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DetailedEmployeeClick);
            
            #line default
            #line hidden
            break;
            case 4:
            
            #line 145 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditEmployeeClick);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 147 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteEmployeeClick);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 314 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SummaryEditClick);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 316 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SummaryDeleteClick);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 319 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExportSummaryClick);
            
            #line default
            #line hidden
            break;
            case 11:
            
            #line 420 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PositionDeleteButtonClicked);
            
            #line default
            #line hidden
            break;
            case 12:
            
            #line 425 "..\..\..\Views\MainView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PositionEditButtonClicked);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

