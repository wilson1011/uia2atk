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
using Mono.UIAutomation.Winforms;
using Mono.UIAutomation.Winforms.Events;

namespace Mono.UIAutomation.Winforms.Events.ComboBox
{

	internal class ButtonInvokePatternInvokedEvent 
		: BaseAutomationEvent
	{
		
		#region Constructors
		
		public ButtonInvokePatternInvokedEvent (FragmentControlProvider provider) 
			: base (provider, InvokePatternIdentifiers.InvokedEvent)
		{
		}
				
		#endregion

		#region IConnectable Overriders

		public override void Connect ()
		{
			((SWF.ComboBox) Provider.Control).DropDown += new EventHandler (OnDropDown);
			((SWF.ComboBox) Provider.Control).DropDownClosed += new EventHandler (OnDropDown);
		}

		public override void Disconnect ()
		{
			((SWF.ComboBox) Provider.Control).DropDown -= new EventHandler (OnDropDown);
			((SWF.ComboBox) Provider.Control).DropDownClosed -= new EventHandler (OnDropDown);
		}

		#endregion

		#region Private Methods		
		
		private void OnDropDown (object sender, EventArgs e)
		{
			 RaiseAutomationEvent ();
		}
		
		#endregion
	}
}
