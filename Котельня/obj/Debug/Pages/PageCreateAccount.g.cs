﻿#pragma checksum "..\..\..\Pages\PageCreateAccount.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "493F368EB7A0EA6B0C4A03C3BCBA81191BE329547B9F3563D983BF7AAF0C6F3E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
using Котельня.Pages;


namespace Котельня.Pages {
    
    
    /// <summary>
    /// PageCreateAccount
    /// </summary>
    public partial class PageCreateAccount : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\Pages\PageCreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbLogin;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Pages\PageCreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox psbPass1;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Pages\PageCreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox psbPass2;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Pages\PageCreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chbAdmin;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Pages\PageCreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel SPKod;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Pages\PageCreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txbkod;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Pages\PageCreateAccount.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnreg;
        
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
            System.Uri resourceLocater = new System.Uri("/Котельня;component/pages/pagecreateaccount.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\PageCreateAccount.xaml"
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
            this.txbLogin = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.psbPass1 = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.psbPass2 = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 4:
            this.chbAdmin = ((System.Windows.Controls.CheckBox)(target));
            
            #line 29 "..\..\..\Pages\PageCreateAccount.xaml"
            this.chbAdmin.Checked += new System.Windows.RoutedEventHandler(this.chkbox_admin_Checked);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\Pages\PageCreateAccount.xaml"
            this.chbAdmin.Unchecked += new System.Windows.RoutedEventHandler(this.chkbox_admin_Unchecked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SPKod = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            this.txbkod = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 45 "..\..\..\Pages\PageCreateAccount.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.chkbox_Checked);
            
            #line default
            #line hidden
            
            #line 45 "..\..\..\Pages\PageCreateAccount.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.chkbox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnreg = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\Pages\PageCreateAccount.xaml"
            this.btnreg.Click += new System.Windows.RoutedEventHandler(this.btnCreate_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

