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
//	Sandy Armstrong <sanfordarmstrong@gmail.com>
//	Mario Carrion <mcarrion@novell.com>
// 

using System;
using SD = System.Drawing;
using SWF = System.Windows.Forms;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using Mono.UIAutomation.Bridge;
using Mono.UIAutomation.Winforms;
using Mono.UIAutomation.Winforms.Events;
using Mono.UIAutomation.Winforms.Events.Button;

namespace Mono.UIAutomation.Winforms.Behaviors.Button
{
	
	internal class InvokeProviderBehavior 
		: ProviderBehavior, IInvokeProvider, IEmbeddedImage
	{
		
		#region Constructor
		
		public InvokeProviderBehavior (FragmentControlProvider provider)
			: base (provider)
		{
		}
		
		#endregion
		
		#region IEmbeddedImage Interface
		
		public System.Windows.Rect BoundingRectangle {
			get {
				//Implementation highly based in ThemeWin32Classic.ButtonBase_DrawImage method
				
				SWF.Button button = (SWF.Button) Provider.Control;
				SD.Image image;
				int	imageX;
				int	imageY;
				int	imageWidth;
				int	imageHeight;
				int width = button.Width;
				int height = button.Height;				

				if (button.ImageIndex != -1)
					image = button.ImageList.Images [button.ImageIndex];
				else
					image = button.Image;
				
				if (image == null)
					return System.Windows.Rect.Empty;

				imageWidth = image.Width;
				imageHeight = image.Height;
			
				switch (button.ImageAlign) {
					case SD.ContentAlignment.TopLeft: {
						imageX = 5;
						imageY = 5;
						break;
					}
					
					case SD.ContentAlignment.TopCenter: {
						imageX = (width - imageWidth) / 2;
						imageY = 5;
						break;
					}
					
					case SD.ContentAlignment.TopRight: {
						imageX = width - imageWidth - 5;
						imageY = 5;
						break;
					}
					
					case SD.ContentAlignment.MiddleLeft: {
						imageX = 5;
						imageY = (height - imageHeight) / 2;
						break;
					}
					
					case SD.ContentAlignment.MiddleCenter: {
						imageX = (width - imageWidth) / 2;
						imageY = (height - imageHeight) / 2;
						break;
					}
					
					case SD.ContentAlignment.MiddleRight: {
						imageX = width - imageWidth - 4;
						imageY = (height - imageHeight) / 2;
						break;
					}
					
					case SD.ContentAlignment.BottomLeft: {
						imageX = 5;
						imageY = height - imageHeight - 4;
						break;
					}
					
					case SD.ContentAlignment.BottomCenter: {
						imageX = (width - imageWidth) / 2;
						imageY = height - imageHeight - 4;
						break;
					}
					
					case SD.ContentAlignment.BottomRight: {
						imageX = width - imageWidth - 4;
						imageY = height - imageHeight - 4;
						break;
					}
					
					default: {
						imageX = 5;
						imageY = 5;
						break;
					}
				}
				
				return new System.Windows.Rect (imageX, imageY, imageWidth, imageHeight);
			}
		}
		
		#endregion
		
		#region IProviderBehavior Interface

		public override void Connect (SWF.Control control)
		{
			Provider.SetEvent (ProviderEventType.InvokePatternInvokedEvent, 
			                   new InvokePatternInvokedEvent (Provider));
		}
		
		public override void Disconnect (SWF.Control control)
		{
			Provider.SetEvent (ProviderEventType.InvokePatternInvokedEvent, 
			                   null);
		}

		public override object GetPropertyValue (int propertyId)
		{
			if (propertyId == AutomationElementIdentifiers.AcceleratorKeyProperty.Id)
				return null; // TODO
			else if (propertyId == AutomationElementIdentifiers.LocalizedControlTypeProperty.Id)
				return "button";
			else
				return null;
		}
		
		public override AutomationPattern ProviderPattern { 
			get { return InvokePatternIdentifiers.Pattern; }
		}
		
		#endregion
		
		#region IInvokeProvider Members
		
		public virtual void Invoke ()
		{
			if (Provider.Control.Enabled == false)
				throw new ElementNotEnabledException ();

			PerformClick ();
		}
		
		#endregion	
		
		#region Private Methods
		
		private void PerformClick ()
		{
	        if (Provider.Control.InvokeRequired == true) {
	            Provider.Control.BeginInvoke (new SWF.MethodInvoker (PerformClick));
	            return;
	        }
			((SWF.Button) Provider.Control).PerformClick ();
		}
		
		#endregion
		
	}

}
