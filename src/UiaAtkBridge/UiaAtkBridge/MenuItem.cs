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
//      Andres G. Aragoneses <aaragoneses@novell.com>
// 

using System;

using System.Windows.Automation;
using System.Windows.Automation.Provider;

namespace UiaAtkBridge
{
	
	public class MenuItem : ComponentParentAdapter, Atk.SelectionImplementor, Atk.ActionImplementor
	{
		bool? comboBoxStructure = null;
		IRawElementProviderSimple provider;
		
		public MenuItem (IRawElementProviderSimple provider)
		{
			if (provider == null)
				throw new ArgumentNullException ("provider");

			this.provider = provider;
			
			if ((provider as IRawElementProviderFragment) == null)
				throw new ArgumentException ("Provider for ParentMenu should be IRawElementProviderFragment");
			
			Name = (string) provider.GetPropertyValue (AutomationElementIdentifiers.NameProperty.Id);

			comboBoxStructure = ((int) provider.GetPropertyValue (AutomationElementIdentifiers.ControlTypeProperty.Id) 
			  == ControlType.List.Id);
			
			IRawElementProviderFragment child = ((IRawElementProviderFragment)provider).Navigate (NavigateDirection.FirstChild);
			while (child != null) {
				children.Add (new MenuItem (child));
				child = child.Navigate (NavigateDirection.NextSibling);
			}

			Role = (children.Count > 0 || comboBoxStructure.Value) ? Atk.Role.Menu : Atk.Role.MenuItem;
		}

		protected override Atk.StateSet OnRefStateSet ()
		{
			Atk.StateSet states = base.OnRefStateSet ();
			//FIXME: figure out why MenuItem elements in Gail don't like this state
			// (maybe because they replace it with Selectable?)
			if (states.ContainsState (Atk.StateType.Focusable)) {
				states.RemoveState (Atk.StateType.Focusable);
				states.AddState (Atk.StateType.Selectable);
			}
			return states;
		}

		public override Atk.Layer Layer {
			get { return Atk.Layer.Popup; }
		}
		
		public override IRawElementProviderSimple Provider {
			get { return provider; }
		}
		
		public override void RaiseAutomationEvent (AutomationEvent eventId, AutomationEventArgs e)
		{
			throw new NotImplementedException ();
		}
		
		public int SelectionCount {
			get {
				throw new NotImplementedException ();
			}
		}
		
		public bool AddSelection (int i)
		{
			throw new NotImplementedException ();
		}

		public bool ClearSelection ()
		{
			throw new NotImplementedException ();
		}

		public Atk.Object RefSelection (int i)
		{
			throw new NotImplementedException ();
		}

		public bool IsChildSelected (int i)
		{
			throw new NotImplementedException ();
		}

		public bool RemoveSelection (int i)
		{
			throw new NotImplementedException ();
		}

		public bool SelectAllSelection ()
		{
			throw new NotImplementedException ();
		}
		
		public override void RaiseStructureChangedEvent (object provider, StructureChangedEventArgs e)
		{
			throw new NotImplementedException ();
		}

		#region Action implementation 
		
		public bool DoAction (int i)
		{
			throw new System.NotImplementedException ();
		}
		
		public string GetName (int i)
		{
			throw new System.NotImplementedException ();
		}
		
		public string GetKeybinding (int i)
		{
			throw new System.NotImplementedException ();
		}
		
		public string GetLocalizedName (int i)
		{
			throw new System.NotImplementedException ();
		}
		
		public bool SetDescription (int i, string desc)
		{
			throw new System.NotImplementedException ();
		}
		
		public string GetDescription (int i)
		{
			throw new System.NotImplementedException ();
		}
		
		public int NActions {
			get {
				throw new System.NotImplementedException ();
			}
		}
		
		#endregion 
		
		
	}
}
