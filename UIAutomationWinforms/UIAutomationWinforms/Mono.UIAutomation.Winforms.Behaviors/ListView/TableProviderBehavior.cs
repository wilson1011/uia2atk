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
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using SWF = System.Windows.Forms;
using Mono.UIAutomation.Winforms;
using Mono.UIAutomation.Winforms.Events;
using Mono.UIAutomation.Winforms.Events.ListView;

namespace Mono.UIAutomation.Winforms.Behaviors.ListView
{

	internal class TableProviderBehavior 
		: GridProviderBehavior, ITableProvider
	{
		
		#region Constructors

		public TableProviderBehavior (ListViewProvider provider)
			: base (provider)
		{
			listViewProvider = provider;
		}

		#endregion
		
		#region IProviderBehavior Interface

		public override AutomationPattern ProviderPattern { 
			get { return TablePatternIdentifiers.Pattern; }
		}
		
		public override void Connect ()
		{
			// NOTE: RowHeadersProperty Property NEVER changes.
			// NOTE: RowOrColumnMajor Property NEVER changes.
			Provider.SetEvent (ProviderEventType.TablePatternColumnHeadersProperty,
			                   new TablePatternColumnHeadersEvent (listViewProvider));
		}
		
		public override void Disconnect ()
		{	
			Provider.SetEvent (ProviderEventType.TablePatternColumnHeadersProperty,
			                   null);
			Provider.SetEvent (ProviderEventType.TablePatternRowHeadersProperty,
			                   null);
			Provider.SetEvent (ProviderEventType.TablePatternRowOrColumnMajorProperty,
			                   null);
		}

		public override object GetPropertyValue (int propertyId)
		{
			if (propertyId == TablePatternIdentifiers.ColumnHeadersProperty.Id)
				return GetColumnHeaders ();
			else if (propertyId == TablePatternIdentifiers.RowHeadersProperty.Id)
				return GetRowHeaders ();
			else if (propertyId == TablePatternIdentifiers.RowOrColumnMajorProperty.Id)
				return RowOrColumnMajor;
			else
				return base.GetPropertyValue (propertyId);
		}

		#endregion

		#region ITableProvider implementation 
		
		public IRawElementProviderSimple[] GetColumnHeaders ()
		{
			if (listViewProvider.HeaderProvider == null)
				return new IRawElementProviderSimple [0];
			else
				return listViewProvider.HeaderProvider.GetHeaderItems ();
		}
		
		public IRawElementProviderSimple[] GetRowHeaders ()
		{
			return new IRawElementProviderSimple [0];
		}

		public RowOrColumnMajor RowOrColumnMajor {
			get { return RowOrColumnMajor.RowMajor; }
		}

		#endregion

		#region Private Fields

		private ListViewProvider listViewProvider;

		#endregion

	}
				                      
}
