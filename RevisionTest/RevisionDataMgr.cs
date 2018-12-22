using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using static RevisionTest.DataItems;
using static RevisionTest.RevColumnOrder;

namespace RevisionTest
{
	public static class RevisionDataMgr
	{
		// this is the data read from the Revit file
		// this is a kvp of <string, revdataitems2>
		// the key is a sort key
		// revdataitems2 is the dynamic array of revision data
		private static RevisionData _masterRevData = new RevisionData();
		public static int MasterCount => _masterRevData.Count;

		// this is the selected revision data
		private static RevisionData _preSelected;
		public static int PreSelectedCount => _preSelected.Count;

		// this is the selected revision data
		private static RevisionData _selected;
		public static int SelectedCount => _selected.Count;

		public static void GetRevisions()
		{
			_masterRevData = RevitRevisions.Read();
			ResetPreSelected();
			ClearSelected();
		}

		public static void ResetPreSelected()
		{
			_preSelected = _masterRevData.Clone();
		}

		public static void ResetSelected()
		{
			_selected = _masterRevData.Clone();
		}

		// prep for changing selected 
		public static void SetPreSelectedToSelected()
		{
			_preSelected = _selected.Clone();
		}

		public static void ClearSelected()
		{
			_selected = new RevisionData();
		}

		// from data manager's primary list,
		// select those records that match the
		// filter criteria
		public static bool Select(RevisionFilters filters)
		{
			if (_preSelected.Count == 0) return false;

			// start with empty and add to this
			ClearSelected();

			foreach (RevisionDataFields kvp in _preSelected)
			{
				if (RevisionSelect.Evaluate(kvp, filters))
				{
					_selected.Add(kvp);
				}
			}
			return _selected.Count != 0;
		}

		public static bool ExportToExcel(RevOrderMgr om)
		{
			return RevisionExportExcel.ExportToExcel(_selected, om);
		}
		
		public static IEnumerable<RevisionDataFields> IterateRevisionData()
		{
			return _masterRevData.GetEnumerable();
		}

		public static IEnumerable<RevisionDataFields> IteratePreSelected()
		{
			return _preSelected.GetEnumerable();
		}

		public static IEnumerable<RevisionDataFields> IterateSelected()
		{
			return _selected.GetEnumerable();
		}

		public static bool SortSelected(RevOrderMgr om)
		{
			if (om.SortOrder.Count == 0) return false;

			foreach (RevisionDataFields rdf in _selected)
			{
				string key = GetKey(rdf, om);

				rdf.SortKey = key;
			}

			_selected.Sort();

			return true;

		}


//		public static bool SortSelected(params ISortable[] d)
//		{
//			if (_selected.Count == 0) return false;
//
//			foreach (RevisionDataFields rdf in _selected)
//			{
//				string key = GetKey(rdf, d);
//
//				rdf.SortKey = key;
//			}
//
//			_selected.Sort();
//
//			return true;
//		}

		public static string GetKey(RevisionDataFields items, 
			RevOrderMgr om)
		{
			string key = null;

			int i = 0;
			
			foreach (DataItems.ISortable so in om.SortOrder.Iterate())
			{
				DataItems.DataEnum d = ((DataItems.DataEnum) so);

				key += RevisionFormat.FormatForKey(items[d.DataIdx], d.Display);

				if (++i != om.SortOrder.Count) key += "|";
			}

			return key;
		}
//
//		public static string GetKey(RevisionDataFields items, params ISortable[] sx)
//		{
//			string key = null;
//
//			int i = 0;
//			
//			foreach (ISortable s in sx)
//			{
//				DataEnum d = ((DataEnum) s);
//
//				key += RevisionFormat.FormatForKey(items[d.DataIdx], d.Display);
//
//				if (++i != sx.Length) key += "|";
//			}
//
//			return key;
//		}
	}
}

