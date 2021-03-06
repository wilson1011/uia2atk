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
using SWF = System.Windows.Forms;
using System.Windows.Automation.Provider;
using Mono.UIAutomation.Winforms.Events;
using Mono.UIAutomation.Winforms.Events.TextBox;

namespace Mono.UIAutomation.Winforms.Behaviors.TextBox
{
	// NOTE: This class also supports RichTextBox as they share pretty much
	// everything
	internal class ValueProviderBehavior 
		: ProviderBehavior, IValueProvider
	{

		#region Constructor
		
		public ValueProviderBehavior (FragmentControlProvider provider)
			: base (provider)
		{
		}

		#endregion
		
		#region ProviderBehavior: Specialization

		public override AutomationPattern ProviderPattern { 
			get { return ValuePatternIdentifiers.Pattern; }
		}

		public override void Connect ()
		{
			Provider.SetEvent (ProviderEventType.ValuePatternIsReadOnlyProperty,
			                   new ValuePatternValueIsReadOnlyEvent (Provider));
			Provider.SetEvent (ProviderEventType.ValuePatternValueProperty,
			                   new ValuePatternValueValueEvent (Provider));
		}

		public override void Disconnect ()
		{
			Provider.SetEvent (ProviderEventType.ValuePatternIsReadOnlyProperty,
			                   null);
			Provider.SetEvent (ProviderEventType.ValuePatternValueProperty,
			                   null);
		}

		public override object GetPropertyValue (int propertyId)
		{
			if (propertyId == ValuePatternIdentifiers.IsReadOnlyProperty.Id)
				return IsReadOnly;
			else if (propertyId == ValuePatternIdentifiers.ValueProperty.Id)
				return Value;
			else
				return null;
		}
		
		#endregion

		#region IValueProvider: Specialization
		
		public bool IsReadOnly {
			get { return ((SWF.TextBoxBase) Provider.Control).ReadOnly || !Provider.Control.Enabled; }
		}
		
		public string Value {
			get {
				if (Provider.Control is SWF.MaskedTextBox) {
					System.ComponentModel.MaskedTextProvider mtp
						= ((SWF.MaskedTextBox) Provider.Control).MaskedTextProvider;
					if (mtp != null)
						return mtp.ToDisplayString ();
				}

				return Provider.Control.Text;
			}
		}

		public void SetValue (string value)
		{
			if (IsReadOnly)
				throw new ElementNotEnabledException ();

			PerformSetValue (value);
		}
		
		#endregion

		#region Private Members
		
		private void PerformSetValue (string value)
		{
			if (Provider.Control.InvokeRequired) {
				Provider.Control.BeginInvoke (new SetValueDelegate (PerformSetValue),
				                              new object [] {value});
				return;
			}

			int maxLength = ((TextBoxProvider) Provider).MaxLength;
			if (maxLength > 0
			    && value.Length > maxLength) {
				value = value.Substring (0, maxLength);
			}
			
			Provider.Control.Text = value;
		}

		private delegate void SetValueDelegate (string val);
		
		#endregion
	}
}
