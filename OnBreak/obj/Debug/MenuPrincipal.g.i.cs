﻿#pragma checksum "..\..\MenuPrincipal.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A2A6A04E400E890F6EE7733D5F001ED6C2BBF220390AF78990ADC1B2B21C7F14"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using OnBreak;
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


namespace OnBreak {
    
    
    /// <summary>
    /// MenuPrincipal
    /// </summary>
    public partial class MenuPrincipal : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAgregarcliente;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnlistado;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnadministrarContrato;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnsalir;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\MenuPrincipal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnlistar;
        
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
            System.Uri resourceLocater = new System.Uri("/OnBreak;component/menuprincipal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MenuPrincipal.xaml"
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
            
            #line 8 "..\..\MenuPrincipal.xaml"
            ((OnBreak.MenuPrincipal)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnAgregarcliente = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\MenuPrincipal.xaml"
            this.btnAgregarcliente.Click += new System.Windows.RoutedEventHandler(this.button_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnlistado = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\MenuPrincipal.xaml"
            this.btnlistado.Click += new System.Windows.RoutedEventHandler(this.btnlistado_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnadministrarContrato = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\MenuPrincipal.xaml"
            this.btnadministrarContrato.Click += new System.Windows.RoutedEventHandler(this.btnadministrarContrato_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnsalir = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\MenuPrincipal.xaml"
            this.btnsalir.Click += new System.Windows.RoutedEventHandler(this.Btnsalir_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnlistar = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\MenuPrincipal.xaml"
            this.btnlistar.Click += new System.Windows.RoutedEventHandler(this.btnlistar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

