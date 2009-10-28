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
//      Sandy Armstrong <sanfordarmstrong@gmail.com>
// 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;

using AEIds = System.Windows.Automation.AutomationElementIdentifiers;

using NUnit.Framework;

namespace MonoTests.System.Windows.Automation
{
	public class BaseTest
	{
		#region Setup/Teardown

		protected Process p;
		protected Dictionary<AutomationPattern, AutomationProperty> patternProperties;
		protected AutomationElement testFormElement;
		protected AutomationElement groupBoxElement;
		protected AutomationElement groupBox1Element;
		protected AutomationElement groupBox2Element;
		protected AutomationElement groupBox3Element;
		protected AutomationElement button1Element;
		protected AutomationElement button2Element;
		protected AutomationElement button3Element;
		protected AutomationElement button4Element;
		protected AutomationElement button5Element;
		protected AutomationElement button6Element;
		protected AutomationElement button7Element;
		protected AutomationElement checkBox1Element;
		protected AutomationElement label1Element;
		protected AutomationElement numericUpDown1Element;
		protected AutomationElement numericUpDown2Element;
		protected AutomationElement textbox1Element;
		protected AutomationElement textbox2Element;
		protected AutomationElement textbox3Element;
		protected AutomationElement tb3horizontalScrollBarElement;
		protected AutomationElement tb3verticalScrollBarElement;
		//protected AutomationElement horizontalMenuStripElement;
		//protected AutomationElement verticalMenuStripElement;
		protected AutomationElement checkbox1Element;
		protected AutomationElement checkbox2Element;
		protected AutomationElement panel1Element;
		protected AutomationElement btnAddTextboxElement;
		protected AutomationElement btnRemoveTextboxElement;
		protected AutomationElement txtCommandElement;
		protected AutomationElement btnRunElement;
		protected AutomationElement treeView1Element;
		protected AutomationElement table1Element;

		public static Process StartApplication (string name, string arguments)
		{
			Process p = new Process ();
			p.StartInfo.FileName = name;
			p.StartInfo.Arguments = arguments;
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.CreateNoWindow = true;
			p.Start ();
			return p;
		}

		[TestFixtureSetUp]
		public virtual void FixtureSetUp ()
		{
			patternProperties = new Dictionary<AutomationPattern, AutomationProperty> ();
			patternProperties.Add (DockPatternIdentifiers.Pattern, AEIds.IsDockPatternAvailableProperty);
			patternProperties.Add (ExpandCollapsePatternIdentifiers.Pattern, AEIds.IsExpandCollapsePatternAvailableProperty);
			patternProperties.Add (GridItemPatternIdentifiers.Pattern, AEIds.IsGridItemPatternAvailableProperty);
			patternProperties.Add (GridPatternIdentifiers.Pattern, AEIds.IsGridPatternAvailableProperty);
			patternProperties.Add (InvokePatternIdentifiers.Pattern, AEIds.IsInvokePatternAvailableProperty);
			patternProperties.Add (MultipleViewPatternIdentifiers.Pattern, AEIds.IsMultipleViewPatternAvailableProperty);
			patternProperties.Add (RangeValuePatternIdentifiers.Pattern, AEIds.IsRangeValuePatternAvailableProperty);
			patternProperties.Add (ScrollItemPatternIdentifiers.Pattern, AEIds.IsScrollItemPatternAvailableProperty);
			patternProperties.Add (ScrollPatternIdentifiers.Pattern, AEIds.IsScrollPatternAvailableProperty);
			patternProperties.Add (SelectionItemPatternIdentifiers.Pattern, AEIds.IsSelectionItemPatternAvailableProperty);
			patternProperties.Add (SelectionPatternIdentifiers.Pattern, AEIds.IsSelectionPatternAvailableProperty);
			patternProperties.Add (TableItemPatternIdentifiers.Pattern, AEIds.IsTableItemPatternAvailableProperty);
			patternProperties.Add (TablePatternIdentifiers.Pattern, AEIds.IsTablePatternAvailableProperty);
			patternProperties.Add (TextPatternIdentifiers.Pattern, AEIds.IsTextPatternAvailableProperty);
			patternProperties.Add (TogglePatternIdentifiers.Pattern, AEIds.IsTogglePatternAvailableProperty);
			patternProperties.Add (TransformPatternIdentifiers.Pattern, AEIds.IsTransformPatternAvailableProperty);
			patternProperties.Add (ValuePatternIdentifiers.Pattern, AEIds.IsValuePatternAvailableProperty);
			patternProperties.Add (WindowPatternIdentifiers.Pattern, AEIds.IsWindowPatternAvailableProperty);

			if (Atspi)
				AtspiSetup ();
			else
				SWFSetup ();
			Assert.IsNotNull (testFormElement);
			Assert.IsNotNull (groupBox1Element);
			Assert.IsNotNull (button1Element);
			Assert.IsNotNull (button2Element);
			Assert.IsNotNull (button3Element);
			Assert.IsNotNull (button4Element);
			Assert.IsNotNull (button5Element);
			Assert.IsNotNull (button6Element);
			Assert.IsNotNull (button7Element);
			Assert.IsNotNull (label1Element);
			Assert.IsNotNull (textbox1Element);
			Assert.IsNotNull (textbox2Element);
			Assert.IsNotNull (textbox3Element);
			if (!Atspi) {
				Assert.IsNotNull (groupBox2Element);
				Assert.IsNotNull (groupBox3Element);
				Assert.IsNotNull (tb3horizontalScrollBarElement);
				Assert.IsNotNull (tb3verticalScrollBarElement);
			}
			Assert.IsNotNull (checkbox1Element);
			Assert.IsNotNull (panel1Element);
			Assert.IsNotNull (btnAddTextboxElement);
			Assert.IsNotNull (btnRemoveTextboxElement);

			Assert.IsNotNull (txtCommandElement);
			Assert.IsNotNull (btnRunElement);
			Assert.IsNotNull (numericUpDown1Element);
			Assert.IsNotNull (treeView1Element);
			Assert.IsNotNull (table1Element);
			/* Todo, why selection pattern will fail on a DataGridView?
			 Assert.IsTrue ((bool) table1Element.GetCurrentPropertyValue
			               (AutomationElementIdentifiers.IsSelectionPatternAvailableProperty),
			               "Selection Pattern should be available");*/
			Assert.IsTrue ((bool) table1Element.GetCurrentPropertyValue
			               (AutomationElementIdentifiers.IsGridPatternAvailableProperty),
			               "Grid Pattern should be available");
			Assert.IsTrue ((bool) table1Element.GetCurrentPropertyValue
			               (AutomationElementIdentifiers.IsTablePatternAvailableProperty),
			               "Table Pattern should be available");
			//Assert.IsNotNull (horizontalMenuStripElement);
			//Assert.IsNotNull (verticalMenuStripElement);
		}

		private void SWFSetup ()
		{
			p = StartApplication (@"SampleForm.exe",
				String.Empty);

			Thread.Sleep (4000);

			testFormElement = AutomationElement.RootElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ProcessIdProperty,
					p.Id));
			groupBox1Element = testFormElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Group));
			groupBox2Element = groupBox1Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"groupBox2"));
			groupBox3Element = groupBox1Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"groupBox3"));
			button1Element = testFormElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Button));
			textbox1Element = testFormElement.FindFirst (TreeScope.Children,
				new AndCondition (new PropertyCondition (AEIds.ControlTypeProperty, ControlType.Edit),
					new PropertyCondition (AEIds.IsPasswordProperty, false)));
			textbox2Element = testFormElement.FindFirst (TreeScope.Children,
				new AndCondition (new PropertyCondition (AEIds.ControlTypeProperty, ControlType.Edit),
					new PropertyCondition (AEIds.IsPasswordProperty, true)));
			label1Element = testFormElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Text));
			button2Element = groupBox1Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"button2"));
			button3Element = groupBox1Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"button3"));
			button4Element = groupBox1Element.FindFirst (TreeScope.Descendants,
				new PropertyCondition (AEIds.NameProperty,
					"button4"));
			button5Element = groupBox1Element.FindFirst (TreeScope.Descendants,
				new PropertyCondition (AEIds.NameProperty,
					"button5"));
			button6Element = groupBox1Element.FindFirst (TreeScope.Descendants,
				new PropertyCondition (AEIds.NameProperty,
					"button6"));
			button7Element = groupBox1Element.FindFirst (TreeScope.Descendants,
				new PropertyCondition (AEIds.NameProperty,
					"button7"));
			checkBox1Element = groupBox2Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"checkBox1"));
			textbox3Element = testFormElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Document));
			tb3horizontalScrollBarElement = textbox3Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.OrientationProperty,
					OrientationType.Horizontal));
			tb3verticalScrollBarElement = textbox3Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.OrientationProperty,
					OrientationType.Vertical));
			groupBox2Element = groupBox1Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"groupBox2"));
			checkbox1Element = groupBox2Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.CheckBox));
			panel1Element = testFormElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Pane));
			btnAddTextboxElement = panel1Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"Add"));
			btnRemoveTextboxElement = panel1Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"Remove"));

			txtCommandElement = testFormElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty, "txtCommand"));

			btnRunElement = testFormElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty, "Run"));

			treeView1Element = testFormElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Tree));
			numericUpDown1Element = testFormElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Spinner));
			table1Element = testFormElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"dataGridView1"));
			//horizontalMenuStripElement = testFormElement.FindFirst (TreeScope.Descendants,
			//        new PropertyCondition (AEIds.NameProperty,
			//                "menuStrip1"));
			//verticalMenuStripElement = testFormElement.FindFirst (TreeScope.Descendants,
			//        new PropertyCondition (AEIds.NameProperty,
			//                "menuStrip2"));
		}

		private void AtspiSetup ()
		{
			string name = "GtkForm.exe";
			p = StartApplication (name, String.Empty);

			Thread.Sleep (1000);

				// TODO: Use ProcessId when at-spi supports it
			testFormElement = AutomationElement.RootElement.FindFirst (TreeScope.Children,
					new PropertyCondition (AEIds.NameProperty,
						"Sample GTK form"));
			groupBoxElement = testFormElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Group));
			groupBox1Element = groupBoxElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Group));
			button1Element = groupBoxElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Button));
			textbox1Element = groupBoxElement.FindFirst (TreeScope.Children,
				new AndCondition (new PropertyCondition (AEIds.ControlTypeProperty, ControlType.Edit),
					new PropertyCondition (AEIds.IsPasswordProperty, false)));
			textbox2Element = groupBoxElement.FindFirst (TreeScope.Children,
				new AndCondition (new PropertyCondition (AEIds.ControlTypeProperty, ControlType.Edit),
					new PropertyCondition (AEIds.IsPasswordProperty, true)));
			label1Element = groupBoxElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Text));
			button2Element = groupBox1Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"button2"));
			button3Element = groupBox1Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"button3"));
			button4Element = groupBox1Element.FindFirst (TreeScope.Descendants,
				new PropertyCondition (AEIds.NameProperty,
					"button4"));
			button5Element = groupBox1Element.FindFirst (TreeScope.Descendants,
				new PropertyCondition (AEIds.NameProperty,
					"button5"));
			button6Element = groupBox1Element.FindFirst (TreeScope.Descendants,
				new PropertyCondition (AEIds.NameProperty,
					"button6"));
			button7Element = groupBox1Element.FindFirst (TreeScope.Descendants,
				new PropertyCondition (AEIds.NameProperty,
					"button7"));
			textbox3Element = groupBoxElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Document));
			checkbox1Element = groupBoxElement.FindFirst (TreeScope.Descendants,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.CheckBox));
			checkbox2Element = groupBoxElement.FindFirst (TreeScope.Descendants,
				new PropertyCondition (AEIds.NameProperty,
					"checkbox2"));
			btnAddTextboxElement = groupBoxElement.FindFirst (TreeScope.Descendants,
				new PropertyCondition (AEIds.NameProperty,
					"Add"));
			panel1Element = TreeWalker.RawViewWalker.GetParent (btnAddTextboxElement);
			btnRemoveTextboxElement = panel1Element.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"Remove"));

			txtCommandElement = groupBoxElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty, "txtCommand"));

			btnRunElement = groupBoxElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty, "Run"));

			treeView1Element = groupBoxElement.FindFirst (TreeScope.Descendants,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Tree));
			table1Element = groupBoxElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.NameProperty,
					"dataGridView1"));
			numericUpDown1Element = groupBoxElement.FindFirst (TreeScope.Children,
				new PropertyCondition (AEIds.ControlTypeProperty,
					ControlType.Slider));
		}

		protected void DisableControls ()
		{
			InvokePattern pattern = (InvokePattern) button4Element.GetCurrentPattern (InvokePatternIdentifiers.Pattern);
			pattern.Invoke ();
			if (Atspi)
				Thread.Sleep (2000);
		}

		protected void EnableControls ()
		{
			InvokePattern pattern = (InvokePattern) button4Element.GetCurrentPattern (InvokePatternIdentifiers.Pattern);
			pattern.Invoke ();
			if (Atspi)
				Thread.Sleep (2000);
		}

		[TestFixtureTearDown]
		public void FixtureTearDown ()
		{
			p.Kill ();
			p = null;
		}

		#endregion

		protected void RunCommand (string command)
		{
			ValuePattern cmd = (ValuePattern) txtCommandElement.GetCurrentPattern (ValuePattern.Pattern);
			cmd.SetValue (command);
			Thread.Sleep (500);
			InvokePattern run = (InvokePattern) btnRunElement.GetCurrentPattern (InvokePattern.Pattern);
			run.Invoke ();
			Thread.Sleep (500);
		}

		public virtual bool Atspi {
			get {
				return false;
			}
		}
	}
}
