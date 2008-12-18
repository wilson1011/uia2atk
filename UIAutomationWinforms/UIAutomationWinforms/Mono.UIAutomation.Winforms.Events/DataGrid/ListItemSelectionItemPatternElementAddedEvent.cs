// Permission is hereby granted, free of charge, to any person obtaining 
// a copy of this software and associated documentation files (the 
// "Software"), to deal in the Software without restriction, including 
// without limitation the rights to use, copy, modify, merge, publish, 
// distribute, sublicense, and/or sell copies of the Software, and to 
// permit persons to whom the Software is furnished to do so, subject to 
// the following conditions: 
//  
// The above copyright notice and this permission notice shall be 
// included in all copies or substantial portions of the Software. 
//  
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE 
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
// 
// Copyright (c) 2008 Novell, Inc. (http://www.novell.com) 
// 
// Authors: 
//	Mario Carrion <mcarrion@novell.com>
// 
using System;
using System.ComponentModel;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using SWF = System.Windows.Forms;
using Mono.UIAutomation.Winforms;
using Mono.UIAutomation.Winforms.Events;

namespace Mono.UIAutomation.Winforms.Events.DataGrid
{
	internal class ListItemSelectionItemPatternElementAddedEvent
		: BaseAutomationEvent
	{
		#region Constructors

		public ListItemSelectionItemPatternElementAddedEvent (ListItemProvider provider)
			: base (provider, 
			        SelectionItemPatternIdentifiers.ElementAddedToSelectionEvent)
		{
			selected = ((SWF.DataGrid) provider.Control).IsSelected (provider.Index);
		}
		
		#endregion
		
		#region ProviderEvent Methods

		public override void Connect ()
		{
			// FIXME: Considerer NavigationBack event
			
			try {
				// Event used add child. Usually happens when DataSource/DataMember are valid
				Helper.AddPrivateEvent (typeof (SWF.DataGrid),
				                        Provider.Control,
				                        "UIASelectionChanged",
				                        this,
				                        "OnElementAddedEvent");
			} catch (NotSupportedException) {}
		}

		public override void Disconnect ()
		{
			try {
				// Event used add child. Usually happens when DataSource/DataMember are valid
				Helper.RemovePrivateEvent (typeof (SWF.DataGrid),
				                           Provider.Control,
				                           "UIASelectionChanged",
				                           this,
				                           "OnElementAddedEvent");
			} catch (NotSupportedException) {}
		}
		
		#endregion 
		
		#region Private methods

#pragma warning disable 169
		
		private void OnElementAddedEvent (object sender, CollectionChangeEventArgs args)
		{
			if (args.Action != CollectionChangeAction.Add)
				return;

			ListItemProvider provider = (ListItemProvider) Provider;
			SWF.DataGrid datagrid = (SWF.DataGrid) sender;

			// FIXME: Remove reflection after committing patch
			int selectedRows = 0;
			try {
				selectedRows 
					= Helper.GetPrivateProperty<SWF.DataGrid, int> (typeof (SWF.DataGrid),
					                                                datagrid,
					                                                "UIASelectedRows");
			} catch (NotSupportedException) {}
			
			if (!selected
			    && selectedRows > 1
			    && datagrid.IsSelected (provider.Index))
				RaiseAutomationEvent ();
			
			selected = datagrid.IsSelected (provider.Index);
		}

#pragma warning restore 169

		#endregion
		
		#region Private Fields
		
		private bool selected;
		
		#endregion
	}
}
