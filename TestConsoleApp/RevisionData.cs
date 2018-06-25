using System;
using System.Collections;
using System.Collections.Generic;



namespace TestConsoleApp 
{
//	public class RevisionData : IEnumerable<RevisionDataFields>, ICloneable<RevisionData>
//	{
//		// this is the data read from the revit database
//		private List<RevisionDataFields> _revisionData = 
//			new List<RevisionDataFields>(10);
//
//		public void Add(RevisionDataFields data)
//		{
//			_revisionData.Add(data);
//		}
//
//		public RevisionDataFields this[int idx]
//		{
//			get => _revisionData[idx];
//			set => _revisionData[idx] = value;
//		}
////
////		public RevisionDataFields this[int idx]
////		{
////			get => _revisionData[_revisionData.Keys[idx]];
////			set => _revisionData[_revisionData.Keys[idx]] = value;
////		}
//
//		public int Count => _revisionData.Count;
//
//		public IEnumerator<RevisionDataFields> GetEnumerator()
//		{
//			return _revisionData.GetEnumerator();
//		}
//
//		IEnumerator IEnumerable.GetEnumerator()
//		{
//			return GetEnumerator();
//		}
//
//		public IEnumerable<RevisionDataFields> GetEnumerable()
//		{
//			foreach (RevisionDataFields rdf in _revisionData)
//			{
//				yield return rdf;
//			}
//		}
//
//		public RevisionData Clone()
//		{
//			RevisionData rd = new RevisionData();
//
//			foreach (RevisionDataFields kvp in _revisionData)
//			{
//				rd.Add(kvp.Clone());
//			}
//
//			return rd;
//		}
//
//		object ICloneable.Clone()
//		{
//			return Clone();
//		}
//	}

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

	public class RevisionData : IEnumerable<KeyValuePair<string, RevisionDataFields>>, ICloneable<RevisionData>
	{
		// this is the data read from the revit database
		private SortedList<string, RevisionDataFields> _revisionData = 
			new SortedList<string, RevisionDataFields>(10);

		public void Add(string key, RevisionDataFields data)
		{
			_revisionData.Add(key, data);
		}

		public RevisionDataFields this[string idx]
		{
			get => _revisionData[idx];
			set => _revisionData[idx] = value;
		}

		public RevisionDataFields this[int idx]
		{
			get => _revisionData[_revisionData.Keys[idx]];
			set => _revisionData[_revisionData.Keys[idx]] = value;
		}

		public int Count => _revisionData.Count;

		public IEnumerator<KeyValuePair<string, 
			RevisionDataFields>> GetEnumerator()
		{
			return _revisionData.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<KeyValuePair<string, 
			RevisionDataFields>> GetEnumerable()
		{
			foreach (KeyValuePair<string, 
				RevisionDataFields> kvp in _revisionData)
			{
				yield return kvp;
			}
		}

		public RevisionData Clone()
		{
			RevisionData rd = new RevisionData();

			foreach (KeyValuePair<string, RevisionDataFields> kvp in _revisionData)
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
}
