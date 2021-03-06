﻿// Permission is hereby granted, free of charge, to any person obtaining 
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
//  Mario Carrion <mcarrion@novell.com>
//
using System;
using System.Windows.Automation;
using NUnit.Framework;

namespace MonoTests.System.Windows.Automation
{
    [TestFixture]
	public class TableItemPatternIdentifiersTest
    {

		[Test]
		public void PatternTest ()
		{
			AutomationPattern pattern = TableItemPatternIdentifiers.Pattern;
			Assert.IsNotNull (pattern, "Pattern field must not be null");
			Assert.AreEqual (10013, pattern.Id, "Id");
			Assert.AreEqual ("TableItemPatternIdentifiers.Pattern", pattern.ProgrammaticName, "ProgrammaticName");
			Assert.AreEqual (pattern, AutomationPattern.LookupById (pattern.Id), "LookupById");
		}

		[Test]
		public void ColumnHeaderItemsPropertyTest ()
		{
			AutomationProperty property = TableItemPatternIdentifiers.ColumnHeaderItemsProperty;
			Assert.IsNotNull (property, "Property field must not be null");
			Assert.AreEqual (30085, property.Id, "Id");
			Assert.AreEqual ("TableItemPatternIdentifiers.ColumnHeaderItemsProperty", property.ProgrammaticName, "ProgrammaticName");
			Assert.AreEqual (property, AutomationProperty.LookupById (property.Id), "LookupById");
		}

		[Test]
		public void RowHeaderItemsPropertyTest ()
		{
			AutomationProperty property = TableItemPatternIdentifiers.RowHeaderItemsProperty;
			Assert.IsNotNull (property, "Property field must not be null");
			Assert.AreEqual (30084, property.Id, "Id");
			Assert.AreEqual ("TableItemPatternIdentifiers.RowHeaderItemsProperty", property.ProgrammaticName, "ProgrammaticName");
			Assert.AreEqual (property, AutomationProperty.LookupById (property.Id), "LookupById");
		}

    }
}
