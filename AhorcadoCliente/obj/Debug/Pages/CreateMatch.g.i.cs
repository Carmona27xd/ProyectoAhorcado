﻿#pragma checksum "..\..\..\Pages\CreateMatch.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B32F1890250DA6DD703DB8FBCCD8910FF34CD7F6989AB11426493D9A6AC6BEBE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AhorcadoCliente.Pages;
using AhorcadoCliente.Properties;
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


namespace AhorcadoCliente.Pages {
    
    
    /// <summary>
    /// CreateMatch
    /// </summary>
    public partial class CreateMatch : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\Pages\CreateMatch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbWordCategories;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Pages\CreateMatch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateMatchButton;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\Pages\CreateMatch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSelectWord;
        
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
            System.Uri resourceLocater = new System.Uri("/AhorcadoCliente;component/pages/creatematch.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\CreateMatch.xaml"
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
            this.cbWordCategories = ((System.Windows.Controls.ComboBox)(target));
            
            #line 18 "..\..\..\Pages\CreateMatch.xaml"
            this.cbWordCategories.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbWordCategories_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 21 "..\..\..\Pages\CreateMatch.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CreateMatchButton = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\Pages\CreateMatch.xaml"
            this.CreateMatchButton.Click += new System.Windows.RoutedEventHandler(this.CreateMatch_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cbSelectWord = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            
            #line 66 "..\..\..\Pages\CreateMatch.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EnglishSelection_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 87 "..\..\..\Pages\CreateMatch.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SpanishSelection_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

