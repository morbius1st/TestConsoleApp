using System;
using System.Collections.Generic;

using static TestConsoleApp.RevisionUtil;

namespace TestConsoleApp
{
	public static class RevisionDataMgr
	{
		// this is the data read from the Revit file
		// this is a kvp of <string, revdataitems2>
		// the key is a sort key
		// revdataitems2 is the dynamic array of revision data
		private static RevisionData _originalData = new RevisionData();

		// this is the selected revision data
		private static RevisionData _preSelected;

		// this is the selected revision data
		private static RevisionData _selected;

		public static void GetRevisions()
		{
			_originalData = RevisionSampleData.Init();

			ResetSelected();
		}

		public static void ResetSelected()
		{
			_preSelected = _originalData.Clone();
			_selected = new RevisionData();
		}

		public static void UpdatePreSelected()
		{
			_preSelected = _selected.Clone();
			_selected = new RevisionData();
		}

		// from data manager's primary list,
		// select those records that match the
		// filter criteria
		public static bool Select(RevisionFilters filters)
		{
			if (_preSelected.Count == 0) return false;

//			int i = 0;

			foreach (KeyValuePair<string, RevDataItems2> kvp in _preSelected)
			{
//				Console.Write("item| " + i);
//				Console.Write(nl);

				if (RevisionSelect.Compare(kvp.Value, filters))
				{
					_selected.Add(kvp.Key, kvp.Value);
				}

//				if (++i == 15) break;
			}

			return _selected.Count != 0;
		}

		public static IEnumerable<KeyValuePair<string, RevDataItems2>>
			IterateRevisionData()
		{
			foreach (KeyValuePair<string, RevDataItems2> kvp in _originalData)
			{
				yield return kvp;
			}
		}

		public static IEnumerable<KeyValuePair<string, RevDataItems2>>
			IteratePreSelected()
		{
			foreach (KeyValuePair<string, RevDataItems2> kvp in _preSelected)
			{
				yield return kvp;
			}
		}

		public static IEnumerable<KeyValuePair<string, RevDataItems2>>
			IterateSelected()
		{
			foreach (KeyValuePair<string, RevDataItems2> kvp in _selected)
			{
				yield return kvp;
			}
		}

	}
}
