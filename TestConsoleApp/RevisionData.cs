using System;
using System.Collections;
using System.Collections.Generic;



namespace TestConsoleApp 
{
	public class RevisionData : IEnumerable<KeyValuePair<string, RevDataItems2>>, ICloneable<RevisionData>
	{
		// this is the data read from the revit database
		private SortedList<string, RevDataItems2> _revisionData = 
			new SortedList<string, RevDataItems2>(10);


		public void Add(string key, RevDataItems2 data)
		{
			_revisionData.Add(key, data);
		}

		public RevDataItems2 this[string idx]
		{
			get => _revisionData[idx];
			set => _revisionData[idx] = value;
		}

		public RevDataItems2 this[int idx]
		{
			get => _revisionData[_revisionData.Keys[idx]];
			set => _revisionData[_revisionData.Keys[idx]] = value;
		}

		public int Count => _revisionData.Count;

		public IEnumerator<KeyValuePair<string, RevDataItems2>> GetEnumerator()
		{
			return _revisionData.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public RevisionData Clone()
		{
			RevisionData rd = new RevisionData();

			foreach (KeyValuePair<string, RevDataItems2> kvp in _revisionData)
			{
				rd.Add(kvp.Key, kvp.Value.Clone());
			}

			return rd;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}
	}

	public static class SortedListExt 
	{
		public static TVal Search<TVal>(this SortedList<string, TVal> sortedList, string partialKey) where TVal : new()
		{
			TVal result = new TVal();

			foreach (KeyValuePair<string, TVal> kvp in sortedList)
			{
				if (kvp.Key.Contains(partialKey))
				{
					result = kvp.Value;
					break;
				}
			}
			return result;
		}

	}
}
