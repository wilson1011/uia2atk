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
// Copyright (c) 2009 Novell, Inc. (http://www.novell.com) 
// 
// Authors: 
//	Mike Gorse <mgorse@novell.com>
// 
using System;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using SWF = System.Windows.Forms;
using Mono.UIAutomation.Winforms.Events;

namespace Mono.UIAutomation.Winforms.Events.TrackBar
{
	internal class RangeValuePatternMaximumEvent
		: BaseAutomationPropertyEvent
	{
		
		#region Constructor
		
		public RangeValuePatternMaximumEvent (SimpleControlProvider provider)
			: base (provider, RangeValuePatternIdentifiers.MaximumProperty)
		{
		}
		
		#endregion
		
		#region Public Methods
		
		public override void Connect ()
		{
			((SWF.TrackBar)Provider.Control).UIAValueParamChanged += OnMaximumChanged;
		}

		public override void Disconnect ()
		{
			((SWF.TrackBar)Provider.Control).UIAValueParamChanged -= OnMaximumChanged;
		}
		
		#endregion
		
		#region Private Methods
		
#pragma warning disable 169
		
		private void OnMaximumChanged (object sender, EventArgs args)
		{
			RaiseAutomationPropertyChangedEvent ();
		}
		
#pragma warning restore 169
		
		#endregion
	}
}
