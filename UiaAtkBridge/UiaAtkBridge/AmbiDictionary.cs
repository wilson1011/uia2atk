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
//      Andres G. Aragoneses <aaragoneses@novell.com>
// 

using System;
using System.Collections.Generic;

namespace UiaAtkBridge
{
	
	
	public class AmbiDictionary <K, V>
	{

		object locking = new object ();
		
		public AmbiDictionary ()
		{
			if (typeof (K) == typeof (V))
				throw new NotSupportedException ();
			
			normalDict = new Dictionary <K, V> ();
			reverseDict = new Dictionary <V, K> ();
		}

		public bool ContainsKey (K key)
		{
			lock (locking) {
				if (key == null)
					return false;
				return normalDict.ContainsKey (key);
			}
		}

		public V this [K key] {
			get { 
				lock (locking) 
					return normalDict [key]; 
			}
			set {
				lock (locking) {
					V oldValue = default (V);
					normalDict.TryGetValue (key, out oldValue);
					if (oldValue != null)
						reverseDict.Remove (oldValue);
					normalDict [key] = value;
					reverseDict [value] = key;
				}
			}
		}

		public void Remove (K key)
		{
			lock (locking) {
				V value = default (V);
				normalDict.TryGetValue (key, out value);
				if (value != null) {
					reverseDict.Remove (value);
					normalDict.Remove (key);
				}
			}
		}

		public void Remove (V value)
		{
			lock (locking) {
				K key = default (K);
				reverseDict.TryGetValue (value, out key);
				if (key != null) {
					normalDict.Remove (key);
					reverseDict.Remove (value);
				}
			}
		}

		public bool TryGetValue (K key, out V value) {
			value = default (V);
			lock (locking) {
				if (normalDict.ContainsKey (key)) {
					value = normalDict [key];
					return true;
				}
				return false;
			}
		}

		public bool TryGetKey (V value, out K key) {
			key = default (K);
			lock (locking) {
				if (reverseDict.ContainsKey (value)) {
					key = reverseDict [value];
					return true;
				}
				return false;
			}
		}

		public Dictionary<K, V>.KeyCollection Keys {
			get { return normalDict.Keys; }
		}

		public int Count {
			get { return normalDict.Count; }
		}
		
		Dictionary <K, V> normalDict;
		Dictionary <V, K> reverseDict;
	}
}
