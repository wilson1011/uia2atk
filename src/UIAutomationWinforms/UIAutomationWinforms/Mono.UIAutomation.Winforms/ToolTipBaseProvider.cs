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
using System.Windows.Forms;

namespace Mono.UIAutomation.Winforms
{
	internal abstract class ToolTipBaseProvider : FragmentControlProvider 
	{
		
		#region Constructors 

		protected ToolTipBaseProvider (Component component) : base (component)
		{
			message = string.Empty;
		}
		
		#endregion 

		#region SimpleControlProvider: Specializations
		
		public override Component Container {
			get { return null; }
		}
		
		public override object GetPropertyValue (int propertyId)
		{
			if (propertyId == AutomationElementIdentifiers.ControlTypeProperty.Id)
				return ControlType.ToolTip.Id;
			else if (propertyId == AutomationElementIdentifiers.NameProperty.Id)
				return message;
			else if (propertyId == AutomationElementIdentifiers.LabeledByProperty.Id)
				return null;
			else if (propertyId == AutomationElementIdentifiers.LocalizedControlTypeProperty.Id)
				return "tool tip";
			else if (propertyId == AutomationElementIdentifiers.IsContentElementProperty.Id)
				return false;
			else if (propertyId == AutomationElementIdentifiers.HelpTextProperty.Id)
				return null;
			else if (propertyId == AutomationElementIdentifiers.IsKeyboardFocusableProperty.Id)
				return false;
			else if (propertyId == AutomationElementIdentifiers.ClickablePointProperty.Id)
				return null;
			else
				return base.GetPropertyValue (propertyId);
		}
		
		#endregion
		
		#region Protected Methods

		protected abstract string GetTextFromControl (Control control);
		
		protected void OnToolTipShown (object sender, ControlEventArgs args)
		{
			if (AutomationInteropProvider.ClientsAreListening == true) {					
				message = GetTextFromControl (args.Control);
				
				//TODO: We need deeper tests in Vista because MS is generating both events
				Helper.RaiseStructureChangedEvent (StructureChangeType.ChildAdded,
				                                   this);
				
				Console.WriteLine ("{0}: ErrorProvider.ToolTipOpenedEvent", GetType ());

				AutomationEventArgs eventArgs 
					= new AutomationEventArgs (AutomationElementIdentifiers.ToolTipOpenedEvent);
				AutomationInteropProvider.RaiseAutomationEvent (AutomationElementIdentifiers.ToolTipOpenedEvent,
				                                                this, 
				                                                eventArgs);
			}
		}

		protected void OnToolTipHidden (object sender, ControlEventArgs args)
		{
			if (AutomationInteropProvider.ClientsAreListening == true) {
				message = GetTextFromControl (args.Control);
				
				//TODO: We need deeper tests in Vista because MS is generating both events
				Helper.RaiseStructureChangedEvent (StructureChangeType.ChildRemoved,
				                                   this);
				
				Console.WriteLine ("{0}: ErrorProvider.ToolTipClosedEvent", GetType ());				
				
				AutomationEventArgs eventArgs 
					= new AutomationEventArgs (AutomationElementIdentifiers.ToolTipClosedEvent);
				AutomationInteropProvider.RaiseAutomationEvent (AutomationElementIdentifiers.ToolTipClosedEvent,
				                                                this, 
				                                                eventArgs);
			}
		}
		
		#endregion
		
		#region Private Fields
		
		private string message;
		
		#endregion
	}
}

