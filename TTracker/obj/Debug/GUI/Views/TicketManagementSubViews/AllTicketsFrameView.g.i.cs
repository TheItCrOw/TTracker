﻿#pragma checksum "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "35600EACA6AABF0C9C736DCC040FB275DB7B18D6F829B260C8365C3EB4D74665"
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
        
        
        #line 43 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SortForName;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SortForProjects;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SortForPriority;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SortForStatus;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SortForProgress;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl taskTicketControl;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button FilterButton;
        
        #line default
        #line hidden
        
        
        #line 179 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
        #line default
        #line hidden
        
        
        #line 183 "..\..\..\..\..\GUI\Views\TicketManagementSubViews\AllTicketsFrameView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NewTicketButton;
        
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
            this.SortForName = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.SortForProjects = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.SortForPriority = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.SortForStatus = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.SortForProgress = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.taskTicketControl = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 7:
            this.FilterButton = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.NewTicketButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

