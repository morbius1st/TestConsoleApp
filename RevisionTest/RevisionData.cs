using System;
using System.Collections;
using System.Collections.Generic;



namespace RevisionTest 
{
	public class RevisionData : IEnumerable<RevisionDataFields>, 
		ICloneable<RevisionData>
	{
		// this is the data read from the revit database
		private List<RevisionDataFields> _revisionData = 
			new List<RevisionDataFields>(10);

		public void Add(RevisionDataFields data)
		{
			_revisionData.Add(data);
		}

		public RevisionDataFields this[int idx]
		{
			get => _revisionData[idx];
			set => _revisionData[idx] = value;
		}

		public int Count => _revisionData.Count;

		public void Sort()
		{
			_revisionData.Sort(new ListSortExt());
		}

		public IEnumerator<RevisionDataFields> GetEnumerator()
		{
			return _revisionData.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<RevisionDataFields> GetEnumerable()
		{
			foreach (RevisionDataFields rdf in _revisionData)
			{
				yield return rdf;
			}
		}

		public RevisionData Clone()
		{
			RevisionData rd = new RevisionData();

			foreach (RevisionDataFields kvp in _revisionData)
			{
				rd.Add(kvp.Clone());
			}

			return rd;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}
	}


}
