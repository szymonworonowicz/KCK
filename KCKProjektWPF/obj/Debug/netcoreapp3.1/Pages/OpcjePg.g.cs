﻿#pragma checksum "..\..\..\..\Pages\OpcjePg.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "044F76028FD5090E17C4F8B2931ABD3C352A257C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using KCKProjektWPF;
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
using WpfAnimatedGif;


namespace KCKProjektWPF {
    
    
    /// <summary>
    /// OpcjePg
    /// </summary>
    public partial class OpcjePg : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\Pages\OpcjePg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Menu;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Pages\OpcjePg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Start;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Pages\OpcjePg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ScaleTransform ScaleText;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\Pages\OpcjePg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ScaleTransform Scaleb1;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\Pages\OpcjePg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ScaleTransform Scaleb2;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\Pages\OpcjePg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ScaleTransform Scaleb3;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/KCKProjektWPF;component/pages/opcjepg.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\OpcjePg.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Menu = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.Start = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.ScaleText = ((System.Windows.Media.ScaleTransform)(target));
            return;
            case 4:
            
            #line 63 "..\..\..\..\Pages\OpcjePg.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Wybor_Postaci_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Scaleb1 = ((System.Windows.Media.ScaleTransform)(target));
            return;
            case 6:
            
            #line 86 "..\..\..\..\Pages\OpcjePg.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Poziom_Trudnosci_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Scaleb2 = ((System.Windows.Media.ScaleTransform)(target));
            return;
            case 8:
            
            #line 109 "..\..\..\..\Pages\OpcjePg.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Exit);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Scaleb3 = ((System.Windows.Media.ScaleTransform)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

