﻿#pragma checksum "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5B6F721B9297F039EEF4E14667C0349629B2AB1A55CA6164B05C15C6C05F750F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
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
using TTracker.BaseDataModules;
using TTracker.GUI.Converters;
using TTracker.GUI.Views.TicketManagementSubViews;


namespace TTracker.GUI.Views.TicketManagementSubViews {
    
    
    /// <summary>
    /// AllTicketsFrameView
    /// </summary>
    public partial class AllTicketsFrameView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchBox;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SortForName;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SortForProjects;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SortForPriority;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SortForStatus;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SortForProgress;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl taskTicketControl;
        
        #line default
        #line hidden
        
        
        #line 184 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button FilterButton;
        
        #line default
        #line hidden
        
        
        #line 198 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
        #line default
        #line hidden
        
        
        #line 203 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NewTicketButton;
        
        #line default
        #line hidden
        
        
        #line 208 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button FinishedTicketsButton;
        
        #line default
        #line hidden
        
        
        #line 214 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ImportSingleTaskTicketButton;
        
        #line default
        #line hidden
        
        
        #line 218 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ImportFromDirectoryTicketsButton;
        
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
            System.Uri resourceLocater = new System.Uri("/TTracker;component/gui/views/ticketmanagementsubviews/allticketsframeview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
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
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.SearchButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SearchBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.SortForName = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.SortForProjects = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.SortForPriority = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.SortForStatus = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.SortForProgress = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.taskTicketControl = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 9:
            this.FilterButton = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.NewTicketButton = ((System.Windows.Controls.Button)(target));
            return;
            case 12:
            this.FinishedTicketsButton = ((System.Windows.Controls.Button)(target));
            return;
            case 13:
            this.ImportSingleTaskTicketButton = ((System.Windows.Controls.Button)(target));
            return;
            case 14:
            this.ImportFromDirectoryTicketsButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

