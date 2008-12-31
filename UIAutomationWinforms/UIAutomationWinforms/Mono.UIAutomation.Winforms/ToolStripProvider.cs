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
//      Sandy Armstrong <sanfordarmstrong@gmail.com>
// 


using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Windows.Automation;
using System.Windows.Automation.Provider;

using AEIds = System.Windows.Automation.AutomationElementIdentifiers;

using Mono.Unix;

namespace Mono.UIAutomation.Winforms
{
	internal class ToolStripProvider : FragmentRootControlProvider
	{
		private ToolStrip strip;
		private Dictionary<ToolStripItem, FragmentControlProvider>
			itemProviders;
		
		public ToolStripProvider (ToolStrip strip) : base (strip)
		{
			this.strip = strip;
			itemProviders = new Dictionary<ToolStripItem, FragmentControlProvider> ();
		}

		protected override object GetProviderPropertyValue (int propertyId)
		{
			if (propertyId == AEIds.ControlTypeProperty.Id)
				return ControlType.ToolBar.Id;
			else if (propertyId == AEIds.LocalizedControlTypeProperty.Id)
				return Catalog.GetString ("tool bar");
			else if (propertyId == AEIds.LabeledByProperty.Id)
				return null;
			else if (propertyId == AEIds.NameProperty.Id)
				return strip.Text;
			else
				return base.GetProviderPropertyValue (propertyId);
		}
		
		#region FragmentRootControlProvider: Specializations
		
		public override void InitializeChildControlStructure ()
		{
			//base.InitializeChildControlStructure ();
			
			strip.ItemAdded += OnItemAdded;
			strip.ItemRemoved += OnItemRemoved;
		
			foreach (ToolStripItem item in strip.Items) {
				FragmentControlProvider itemProvider = GetItemProvider (item);
				if (itemProvider != null)
					OnNavigationChildAdded (false, itemProvider);
			}
		}
		
		public override void FinalizeChildControlStructure ()
		{
			//base.FinalizeChildControlStructure ();
			
			strip.ItemAdded -= OnItemAdded;
			strip.ItemRemoved -= OnItemRemoved;
			
			foreach (FragmentControlProvider itemProvider in itemProviders.Values)
				OnNavigationChildRemoved (false, itemProvider);
			OnNavigationChildrenCleared (false);
		}

		#endregion

		#region Private Navigation Methods

		private void OnItemAdded (object sender, ToolStripItemEventArgs e)
		{
			FragmentControlProvider itemProvider = GetItemProvider (e.Item);
			if (itemProvider != null)
				OnNavigationChildAdded (true, itemProvider);
		}

		private void OnItemRemoved (object sender, ToolStripItemEventArgs e)
		{
			FragmentControlProvider itemProvider = GetItemProvider (e.Item);
			if (itemProvider != null) {
				itemProviders.Remove (e.Item);
				itemProvider.Terminate ();
				OnNavigationChildRemoved (true, itemProvider);
			}
		}

		private FragmentControlProvider GetItemProvider (ToolStripItem item)
		{
			FragmentControlProvider itemProvider;
			
			if (!itemProviders.TryGetValue (item, out itemProvider)) {
				itemProvider = (FragmentControlProvider) ProviderFactory.GetProvider (item);
				itemProviders [item]  = itemProvider;
			}

			return itemProvider;
		}

		#endregion
	}
}
