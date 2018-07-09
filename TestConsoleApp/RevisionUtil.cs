#region + Using Directives
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using static TestConsoleApp.DataItems.EDataFields;
using static TestConsoleApp.RevisionFilters;
using static TestConsoleApp.RevisionFilters.ECompareOps;

#endregion


// projname: TestConsoleApp
// itemname: RevisionUtil
// username: jeffs
// created:  6/18/2018 10:29:54 PM


namespace TestConsoleApp
{
	public interface ICloneable<U> : ICloneable where U : ICloneable<U>
	{
		new U Clone();
	}

	public static class RevisionUtil
	{
		// one click operation to collect and 
		// display the current data
		public static bool OneClick()
		{
			RevisionDataMgr.ResetPreSelected();

			bool result = RevisionDataMgr.Select(Settings.Info.oneClickFilter);

			if (!result) return false;

			RevisionDataMgr.SortSelected(Settings.Info.oneClickOrderMgr);

			return true;
		}

	}

	public class ListSortExt : IComparer<RevisionDataFields>
	{
		[SuppressMessage("ReSharper", "StringCompareToIsCultureSpecific")]
		public int Compare(RevisionDataFields x, RevisionDataFields y)
		{
			return ((string) x?[REV_SORT_KEY]?? "")
				.CompareTo((string) y?[REV_SORT_KEY]?? "");
		}
	}


	public static class Extensions
	{
		public static string PadCenter(this string s, int  w, 
			char cl = ' ', char cr = (char) 0)
		{
			if (s == null || s.Length >= w) return s;

			int len     = s.Length;
			int space   = (w - len) / 2;
			int padding = len + space;

			if (cr == 0) cr = cl;

			if (char.IsControl(cr)) cr = (char) 32;
			if (char.IsControl(cl)) cl = (char) 32;

			string first = s.PadLeft(padding, cl);
			string final = first.PadRight(w, cr);

			int l = final.Length;

			return final;

		}

		public static List<T> Clone<T>(this List<T> original)
		{
			List<T> copy = new List<T>(10);

			foreach (T t in original)
			{
				copy.Add(t);
			}

			return copy;
		}
	}
}
