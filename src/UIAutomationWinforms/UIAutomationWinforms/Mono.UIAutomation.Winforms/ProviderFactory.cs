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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Automation.Provider;
using Mono.UIAutomation.Winforms.Navigation;
using SWFErrorProvider = System.Windows.Forms.ErrorProvider;
using SWFHelpProvider = System.Windows.Forms.HelpProvider;
using System.Linq;

namespace Mono.UIAutomation.Winforms
{

	public sealed class ProviderFactory
	{
		#region Private constructor
		
		private ProviderFactory ()
		{
		}
		
		#endregion
		
		#region Static Fields
		
		// NOTE: This may not be the best place to track this...however
		//       I forsee this factory class evolving into a builder
		//       class that takes raw providers and attaches provider
		//       behaviors depending on the control type, and maybe
		//       it makes sense for the builder to keep track of
		//       this mapping?
		private static Dictionary<Component, IRawElementProviderFragment>
			componentProviders;
		//Read comment in ReleaseProvider method for detailed information.
		private static Dictionary<Component, int> sharedComponents;
		
		private static Dictionary<SWFErrorProvider, List<ErrorProvider>> errorProviders;
		
		static ProviderFactory ()
		{
			componentProviders =
				new Dictionary<Component,IRawElementProviderFragment> ();
			sharedComponents = new Dictionary<Component, int> ();
			
			errorProviders = new Dictionary<SWFErrorProvider, List <ErrorProvider>>();
		}
		
		#endregion
		
		#region Static Public Methods
		
		public static IRawElementProviderFragment GetProvider (Component component)
		{
			return GetProvider (component, true, false);
		}
		
		public static IRawElementProviderFragment GetProvider (Component component,
		                                                       bool initializeEvents)
		{
			return GetProvider (component, true, false);
		}

		public static IRawElementProviderFragment GetProvider (Component component,
		                                                       bool initializeEvents,
		                                                       bool forceInitializeChildren)
		{
			Label l;
			Button b;
			RadioButton r;
			CheckBox c;
			TextBox t;
			LinkLabel ll;
			NumericUpDown ud;
			FragmentControlProvider provider = null;
			Form f;
			GroupBox gb;
			StatusBar sb;
			ComboBox cb;
			ListBox lb;
			ScrollBar scb;
			PictureBox pb;
			ToolTip tt;
			Control ctrl;
			ProgressBar pgb;
			SWFHelpProvider hlp;
			
			bool isComponentBased = false;
			
			if (component == null)
				return null;

			provider = (FragmentControlProvider) FindProvider (component);
			if (provider != null)
				return provider;

			if ((f = component as Form) != null)
				provider = new WindowProvider (f);
			else if ((gb = component as GroupBox) != null)
				provider = new GroupBoxProvider (gb);
			else if ((b = component as Button) != null)
				provider = new ButtonProvider (b);
			else if ((r = component as RadioButton) != null)
				provider = new RadioButtonProvider (r);
			else if ((c = component as CheckBox) != null)
				provider = new CheckBoxProvider (c);
			else if ((t = component as TextBox) != null)
				provider = new TextBoxProvider (t);
			else if ((ll = component as LinkLabel) != null)
				provider = new LinkLabelProvider (ll);
			else if ((l = component as Label) != null)
				provider = new LabelProvider (l);
			else if ((ud = component as NumericUpDown) != null)
				provider = new NumericUpDownProvider (ud);
			else if ((sb = component as StatusBar) != null)
				provider = new StatusBarProvider (sb);
			else if ((cb = component as ComboBox) != null)
				provider = new ComboBoxProvider (cb);
			else if ((lb = component as ListBox) != null)
				provider = new ListBoxProvider (lb);
			else if ((pgb = component as ProgressBar) != null)
				provider = new ProgressBarProvider (pgb);
			else if ((scb = component as ScrollBar) != null) {
				if ((lb = scb.Parent as ListBox) != null)
					provider = new ListBoxProvider.ListBoxScrollBarProvider (scb);
				else {
					//TODO:
					//   We need to add here a ScrollableControlProvider and then verify
					//   if the internal scrollbar instances are matching this one,
					//   if so, then we return a scrollbar, otherwise we return a pane.
					ScrollableControl scrollable;
					//ScrollableControlProvider scrollableProvider;
					if ((scrollable = scb.Parent as ScrollableControl) != null
					    || scb.Parent == null) {
					//	scrollableProvider = (ScrollableControlProvider) GetProvider (scrollable);
					//	if (scrollableProvider.ScrollBarExists (scb) == true)
							provider = new ScrollBarProvider (scb);
					//	else 
					//		provider = new PaneProvider (scb);
					} else
						provider = new PaneProvider (scb);
				}
			} else if ((pb = component as PictureBox) != null)
				provider = new PaneProvider (pb);
			else if ((ctrl = component as Control) != null) {
				//Notice that we don't update the local variable "provider"
				if (ErrorProvider.InstancesTracker.IsControlFromErrorProvider (ctrl) == true) {
					SWFErrorProvider swfError = ErrorProvider.InstancesTracker.GetErrorProviderFromControl (ctrl);
					Control parent = ErrorProvider.InstancesTracker.GetParentFromControl (ctrl);
					ErrorProvider errorProvider = null;

					if (ErrorProvider.InstancesTracker.IsFirstControlFromErrorProvider (ctrl) == true) {
						Console.WriteLine ("Is first control!!!");
						errorProvider = new ErrorProvider (ctrl, swfError);
						
						List<ErrorProvider> providers = new List<ErrorProvider> ();
						providers.Add ((ErrorProvider) errorProvider);
						errorProviders [swfError] = providers;
					} else {
						Console.WriteLine ("Is other control!!");
						List<ErrorProvider> providers = errorProviders [swfError];
						
						errorProvider = (from p in providers
						                 where p.Parent == parent
						                 select p).First ();
						errorProvider.AddControl (ctrl);
					}
				}
			}
			//NOTE: The following providers are Component-based meaning that
			//can be shared.
			else if ((tt = component as ToolTip) != null) {
				provider = new ToolTipProvider (tt);
				isComponentBased = true;
			} else if ((hlp = component as SWFHelpProvider) != null) {
				provider = new HelpProvider (hlp);
				Console.WriteLine ("---------------- Helprpororrorori!!");
				isComponentBased = true;
			} else //TODO: We have to solve the problem when there's a Custom control
				throw new NotImplementedException ("Provider not implemented for control");
			
			if (provider != null) {
				// TODO: Make tracking in dictionary optional
				componentProviders [component] = provider;
				if (initializeEvents)
					provider.InitializeEvents ();
				
				//Read comment in ReleaseProvider method for detailed information.
				if (isComponentBased == true)
					sharedComponents.Add (component, 1);
				
				FragmentRootControlProvider root;
				if (forceInitializeChildren == true
				    && (root = provider as FragmentRootControlProvider) != null)
					root.InitializeChildControlStructure ();
			}
			
			return provider;
		}
		
		//Control-based Providers aren't shared, meaning that you can't
		//associate the Control with more than one Control/Component, so
		//releasing it doesn't impact in other providers, however Component-based
		//Providers are shared, meaning that you can associate the Component with 
		//one or more Controls, for example the ToolTip and HelpProvider.
		//We are releasing the Component-based when the number of sharedComponents
		//reaches 1, that way we aren't creating the provider over and over.
		public static void ReleaseProvider (Component component)
		{
			IRawElementProviderFragment provider;
			if (componentProviders.TryGetValue (component, 
			                                    out provider) == true) {
				if (sharedComponents.ContainsKey (component) == true) {
					if (sharedComponents [component] == 1) {
						componentProviders.Remove (component);
						((FragmentControlProvider) provider).Terminate ();
						sharedComponents.Remove (component);
					} else
						sharedComponents [component] = sharedComponents [component] - 1;
				} else {
					componentProviders.Remove (component);
					((FragmentControlProvider) provider).Terminate ();
				}
			}
		}
		
		private static IRawElementProviderFragment FindProvider (Component component)
		{
			IRawElementProviderFragment provider;
			
			if (component == null)
				return null;
			else if (componentProviders.TryGetValue (component, out provider)) {
				if (sharedComponents.ContainsKey (component) == true)
					sharedComponents [component] = sharedComponents [component] + 1;

				return provider;
			}

			return null;
		}
		
		#endregion
	}
}
