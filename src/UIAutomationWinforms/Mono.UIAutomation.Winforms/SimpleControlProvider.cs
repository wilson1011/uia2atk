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
using System.Windows.Forms;

using System.Windows.Automation;
using System.Windows.Automation.Provider;

namespace Mono.UIAutomation.Winforms
{
	public abstract class SimpleControlProvider : IRawElementProviderSimple
	{
#region Protected Fields
		
		protected Control control;
		
#endregion
		
#region Constructors
		
		public SimpleControlProvider (Control control)
		{
			this.control = control;
			
			control.GotFocus += OnFocusChanged;
			//control.LocationChanged += OnLocationChanged;
		}
		
#endregion
		
#region IRawElementProviderSimple Members
	
		public abstract object GetPatternProvider (int patternId);
		
		public virtual object GetPropertyValue (int propertyId)
		{
			if (propertyId == AutomationElementIdentifiers.IsEnabledProperty.Id)
				return control.Enabled;
			else if (propertyId == AutomationElementIdentifiers.NameProperty.Id)
				return control.Text;
			else if (propertyId == AutomationElementIdentifiers.IsKeyboardFocusableProperty.Id)
				return control.CanFocus;
			else if (propertyId == AutomationElementIdentifiers.IsOffscreenProperty.Id)
				throw new NotImplementedException ();
			else if (propertyId == AutomationElementIdentifiers.HasKeyboardFocusProperty.Id)
				return control.Focused;
			else
				return null;
		}


		public virtual IRawElementProviderSimple HostRawElementProvider {
			get {
				return AutomationInteropProvider.HostProviderFromHandle (control.TopLevelControl.Handle);
			}
		}
		
		public virtual ProviderOptions ProviderOptions {
			get {
				return ProviderOptions.ServerSideProvider;
			}
		}

#endregion
		
#region Event Handlers
	
		private void OnFocusChanged (object sender, EventArgs e)
		{
			if (AutomationInteropProvider.ClientsAreListening) {
				AutomationPropertyChangedEventArgs args =
					new AutomationPropertyChangedEventArgs (AutomationElementIdentifiers.HasKeyboardFocusProperty,
					                                        null, // TODO: Test against MS (UI Spy seems to give very odd results on this property)
					                                        GetPropertyValue (AutomationElementIdentifiers.HasKeyboardFocusProperty.Id));
				AutomationInteropProvider.RaiseAutomationPropertyChangedEvent (this, args);
			}
		}
#endregion
	}
}
