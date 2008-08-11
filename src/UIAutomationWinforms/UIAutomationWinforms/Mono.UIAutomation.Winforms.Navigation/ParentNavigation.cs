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
using System.Collections.Generic;
using System.Windows.Automation;
using System.Windows.Automation.Provider;

namespace Mono.UIAutomation.Winforms.Navigation
{
	
	internal class ParentNavigation : SimpleNavigation
	{

		#region Constructor
		
		public ParentNavigation (FragmentRootControlProvider rootProvider,
		                         FragmentRootControlProvider rootParentProvider)
			: base (rootProvider)
		{
			chain = new NavigationChain ();
			if (rootParentProvider != null)
				this.parentNavigation = (ParentNavigation) rootParentProvider.Navigation;
		}
		
		public ParentNavigation (FragmentRootControlProvider rootProvider)
			: this (rootProvider, null)
		{
		}
		
		#endregion
		
		#region INavigable Interface
		
		public override IRawElementProviderFragment Navigate (NavigateDirection direction) 
		{
			if (direction == NavigateDirection.Parent) 
				return ProviderFactory.GetProvider (((FragmentRootControlProvider) Provider).Container);
			else if (direction == NavigateDirection.FirstChild)
				return chain.First == null ? null : (IRawElementProviderFragment) chain.First.Value.Provider;
			else if (direction == NavigateDirection.LastChild)
				return chain.Last == null ? null : (IRawElementProviderFragment) chain.Last.Value.Provider;
			else if (direction == NavigateDirection.NextSibling && parentNavigation != null)
				return parentNavigation.GetNextExplicitSiblingProvider (this);
			else if (direction == NavigateDirection.PreviousSibling && parentNavigation != null) 
				return parentNavigation.GetPreviousExplicitSiblingProvider (this);			
			else
				return null;
		}

		public override void Initialize ()
		{
			FragmentRootControlProvider rootProvider = (FragmentRootControlProvider) Provider;
			
			foreach (FragmentControlProvider child in rootProvider)
				AddLast (child.Navigation);
			
			rootProvider.NavigationChildAdded += new NavigationEventHandler (OnNavigationChildAdded);
			rootProvider.NavigationChildRemoved += new NavigationEventHandler (OnNavigationChildRemoved);
			rootProvider.NavigationChildrenClear += new NavigationEventHandler (OnNavigationChildrenClear);
		}

		public override void Terminate ()
		{		
			((FragmentRootControlProvider) Provider).NavigationChildAdded -= new NavigationEventHandler (OnNavigationChildAdded);
			((FragmentRootControlProvider) Provider).NavigationChildRemoved -= new NavigationEventHandler (OnNavigationChildRemoved);
			((FragmentRootControlProvider) Provider).NavigationChildrenClear -= new NavigationEventHandler (OnNavigationChildrenClear);
		}
		
		#endregion

		#region Public Methods
		
		public IRawElementProviderFragment GetNextExplicitSiblingProvider (INavigation navigation)
		{
			if (chain.Contains (navigation) == true) {
				LinkedListNode<INavigation> nextNode = chain.Find (navigation).Next;
				if (nextNode == null)
					return null;
				else
					return (IRawElementProviderFragment) nextNode.Value.Provider;
			} else
				return null;
		}
		
		public IRawElementProviderFragment GetPreviousExplicitSiblingProvider (INavigation navigation)
		{
			if (chain.Contains (navigation) == true) {
				LinkedListNode<INavigation> previousNode = chain.Find (navigation).Previous;
				if (previousNode == null)
					return null;
				else
					return (IRawElementProviderFragment) previousNode.Value.Provider;
			} else
				return null;
		}
		
		public void AddAfter (INavigation after, INavigation navigation)
		{
			chain.AddAfter (chain.Find (after), navigation);
		}
		
		public void AddLast (INavigation navigation)
		{
			chain.AddLast (navigation);
		}
		
		public void AddFirst (INavigation navigation)
		{
			chain.AddFirst (navigation);
		}
		
		public void Remove (INavigation navigation)
		{
			chain.Remove (navigation);
		}

		#endregion
		
		#region Private Fields
		
		private void OnNavigationChildAdded (FragmentControlProvider parentProvider,
		                                     NavigationEventArgs args)
		{
			AddLast (args.ChildProvider.Navigation);

			System.Console.WriteLine ("ParentNavigation.ADDED IN Type: {0} - {1} - '{2}'", 
			                          GetType (),
			                          args.ChildProvider.GetType (),
			                          args.RaiseEvent);			
			if (args.RaiseEvent == true) {
				Helper.RaiseStructureChangedEvent (StructureChangeType.ChildAdded, 
				                                   args.ChildProvider);
				Helper.RaiseStructureChangedEvent (StructureChangeType.ChildrenInvalidated,
				                                   parentProvider);
			}
		}
		
		private void OnNavigationChildRemoved (FragmentControlProvider parentProvider,
		                                       NavigationEventArgs args)
		{
			Remove (args.ChildProvider.Navigation);

			System.Console.WriteLine ("ParentNavigation.REMOVED IN Type: {0} '{1}'", 
			                          GetType (),
			                          args.RaiseEvent);
			if (args.RaiseEvent == true) {
				Helper.RaiseStructureChangedEvent (StructureChangeType.ChildRemoved, 
				                                   args.ChildProvider);
				Helper.RaiseStructureChangedEvent (StructureChangeType.ChildrenInvalidated,
				                                   parentProvider);
			}
		}
		
		private void OnNavigationChildrenClear (FragmentControlProvider parentProvider,
		                                        NavigationEventArgs args)
		{
			chain.Clear ();
			
			//TODO: Is this the event to generate?
			if (args.RaiseEvent == true) {
				Helper.RaiseStructureChangedEvent (StructureChangeType.ChildrenBulkRemoved, 
				                                   parentProvider);
				Helper.RaiseStructureChangedEvent (StructureChangeType.ChildrenInvalidated,
				                                   parentProvider);
			}
		}
		
		#endregion
		
		#region Private Fields
		
		private NavigationChain chain;
		private ParentNavigation parentNavigation;
		
		#endregion
	}
}
