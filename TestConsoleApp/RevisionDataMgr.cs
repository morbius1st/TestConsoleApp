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
		private static RevisionData _masterRevData = new RevisionData();

		// this is the selected revision data
		private static RevisionData _preSelected;

		// this is the selected revision data
		private static RevisionData _selected;

		public static void GetRevisions()
		{
			_masterRevData = RevitRevisions.Read();

			ResetSelected();
		}

		public static void ResetSelected()
		{
			_preSelected = _masterRevData.Clone();
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

			foreach (KeyValuePair<string, RevisionDataFields> kvp in _preSelected)
			{
				if (RevisionSelect.Evaluate(kvp.Value, filters))
				{
					_selected.Add(kvp.Key, kvp.Value);
				}
			}
			return _selected.Count != 0;
		}

		public static IEnumerable<KeyValuePair<string, RevisionDataFields>>
			IterateRevisionData()
		{
			return _masterRevData.GetEnumerable();
		}

		public static IEnumerable<KeyValuePair<string, RevisionDataFields>>
			IteratePreSelected()
		{
			return _preSelected.GetEnumerable();
		}

		public static IEnumerable<KeyValuePair<string, RevisionDataFields>>
			IterateSelected()
		{
			return _selected.GetEnumerable();
		}

	}
}
