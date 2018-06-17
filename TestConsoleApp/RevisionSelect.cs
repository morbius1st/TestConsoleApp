using System;
using System.CodeDom;
using System.Collections.Generic;
using static TestConsoleApp.RevisionSelect.ESelVisibility;

using static TestConsoleApp.RevisionUtil;
using static TestConsoleApp.RevisionFilters;
using static TestConsoleApp.RevisionFilters.CompareOps;




namespace TestConsoleApp
{

	public static class RevisionSelect
	{
		public enum ESelVisibility
		{
			VISIBILITY_ALL = -1,
			VISIBILITY_HIDDEN = 0,
			VISIBILITY_CLOUDANDTAG = 1,
			VISIBILITY_TAGONLY = 2,
			VISIBILITY_COUNT = VISIBILITY_TAGONLY + 2
		}

		private static RevisionVisibility[] _visCrossRef =
			new RevisionVisibility[(int) VISIBILITY_COUNT];

		static RevisionSelect()
		{
			DefineVisibility();
		}

		#region + Comparisons

		public static bool Verify(bool a, Criteria c)
		{
			bool result = false;
			ICompRoot opr = c.CompareOpr;

			if (c.CompareOpr == ANY)
			{
				result = true;
			}
			else if (opr == TRUE ||
				opr == FALSE)
			{
				result = (a == true) == (opr == TRUE);
			}

			return result;
		}

		public static bool Verify(RevisionVisibility a, Criteria c)
		{
			bool result = false;
			ICompRoot opr = c.CompareOpr;

			if (c.CompareOpr == ANY)
			{
				result = true;
			}
			else if (opr == EQUAL ||
				opr == NOT_EQUAL)
			{
				result = (a == c.TestValue.AsVisibility) == (opr == EQUAL);
			}

			return result;
		}

		public static bool Verify(ElementId a, Criteria c)
		{
			bool result = false;
			ICompRoot opr = c.CompareOpr;

			ElementId b = c.TestValue?.AsElementId;

			if (c.CompareOpr == ANY)
			{
				result = true;
			}
			else if (a == null || b == null)
			{
				result = false;
			}
			else if (opr == EQUAL || opr == NOT_EQUAL)
			{
				result = (a.Value == b.Value) == (opr == EQUAL);
			}

			return result;
		}

		public static bool Verify(int a, Criteria c)
		{
			bool result = false;
			ICompRoot opr = c.CompareOpr;
			int b = c.TestValue?.AsInt ?? 0;

			switch (c.CompareOpr.Type)
			{
			case CompareType.ANY:
				{
					result = true;
					break;
				}
			case CompareType.EQUAL:
			case CompareType.NOT_EQUAL:
				{
					result = (a == b) == (opr == EQUAL);
					break;
				}
			case CompareType.LESS_THEN:
			case CompareType.LESS_THEN_OR_EQUAL:
				{
					result = a < b ||
						((a == b) && (opr == LESS_THEN_OR_EQUAL));
					break;
				}
			case CompareType.GREATER_THEN:
			case CompareType.GREATER_THEN_OR_EQUAL:
				{
					result = a > b ||
						((a == b) && (opr == GREATER_THEN_OR_EQUAL));
					break;
				}
			}

			return result;
		}

		public static bool Verify(string a, Criteria c)
		{
			bool result = false;
			ICompRoot opr = c.CompareOpr;
			StringComparison ignorecase = StringComparison.OrdinalIgnoreCase;

			if (!c.IgnoreCase)
			{
				ignorecase = StringComparison.Ordinal;
			}

			string b = c.TestValue?.AsString;

			if (c.CompareOpr.Type == CompareType.ANY)
			{
				result = true;
			}
			else if (a == null)
			{
				result = false;
			}
			else if (opr == IS_EMPTY ||
				opr == IS_NOT_EMPTY)
			{
				bool ans = string.IsNullOrEmpty(a);
				result = (opr == IS_EMPTY) == ans;
			}
			else if (b == null)
			{
				result = false;
			}
			else
			{
				switch (c.CompareOpr.Type)
				{
				case CompareType.EQUAL:
				case CompareType.NOT_EQUAL:
					{
						result = a.Equals(b) == (opr == EQUAL);
						break;
					}
				case CompareType.LESS_THEN:
				case CompareType.LESS_THEN_OR_EQUAL:
					{
						int ans = String.Compare(a, b, ignorecase);
						result = ans < 0 ||
							((a.Equals(b)) && (opr == LESS_THEN_OR_EQUAL));
						break;
					}
				case CompareType.GREATER_THEN:
				case CompareType.GREATER_THEN_OR_EQUAL:
					{
						int ans = String.Compare(a, b, ignorecase);
						result = ans > 0 ||
							((a.Equals(b)) && (opr == GREATER_THEN_OR_EQUAL));
						break;
					}
				case CompareType.STARTS_WITH:
				case CompareType.DOES_NOT_START_WITH:
					{
						bool ans = a.StartsWith(b, ignorecase);
						result = (opr == STARTS_WITH) == ans;
						break;
					}
				case CompareType.CONTAINS:
				case CompareType.DOES_NOT_CONTAIN:
					{
						bool ans = a.IndexOf(b, ignorecase) > 0;
						result = (opr == CONTAINS) == ans;
						break;
					}
				}
			}

			return result;
		}

		// compare one row versus the filters
		public static bool Compare(RevDataItems2 items, RevisionFilters filters)
		{
			// scan through the list of filter tests to determine if
			// the provided information passes 
			// return true if this item meets all of the criteria
			// return false if any part of this item does not meet the criteria

			if (filters == null || items == null)
				throw new ArgumentNullException();

			if (filters.Count == 0 || items.Count == 0) return false;

			bool result = false;

//			Console.Write(nl);
//			Console.WriteLine("processing all filters");
//
//			ListColumnHeadersInColumnOrder();
//			ListDataItemInColumnOrder(1, items);

			// compare the data item referenced in the filter to the filter criteria
			// the filters are in a outer (data enum) order
			// each data item field could have multiple criteria filters
			// inner loop is an 'or' loop - that is, if any of the inner
			// filters are true, the whole inner filter is true.  for conflicts
			// that is, filtering the same item in opposite ways (e.g. is true and is false)
			// a true will supersede a false
			//
			// the outer loop is the opposite
			// it is an 'and' loop - a single false and the data row fails

			int i = 0;

			foreach (KeyValuePair<DataItems.FilterEnum, Filters> kvpOuter in filters)
			{
//				Console.WriteLine("data item    name| " + kvpOuter.Key.Name);
//				Console.WriteLine("data item   title| " + kvpOuter.Key.Title[0] + " " + kvpOuter.Key.Title[1]);
//				Console.WriteLine("data item dataidx| " + kvpOuter.Key.DataIdx);
//				Console.WriteLine("data item filtidx| " + kvpOuter.Key.FilterIdx);
//				Console.WriteLine("data item   value| " + items[kvpOuter.Key.DataIdx] + " :: " +
//					items[kvpOuter.Key.DataIdx].GetType());

				foreach (KeyValuePair<int, Criteria> kvpInner in kvpOuter.Value)
				{
					if (kvpInner.Value.CompareOpr == CompareOps.ANY)
					{
						result = true;
					}
					else
					{
						// evaluate the actual revision data versus the test criteria
						result = Verify(items[kvpOuter.Key.DataIdx],  kvpInner.Value);
					}

//					string testval = kvpInner.Value.TestValue?.Value.ToString() ?? "null";
//					string itemval = items[kvpOuter.Key.DataIdx].ToString();
//
//					Console.WriteLine("        idx| " + (i++).ToString().PadRight(6)
//						+ " type| " + kvpInner.Value.CompareOpr.Type.ToString().PadRight(24) 
//						+ " val| " + itemval.PadRight(10)
//						+ " vs test val| " + testval.PadRight(20)
//						+ " result| " + result);
////
//					Console.WriteLine("filter       item| " + i++);
//					Console.WriteLine("filter        idx| " + kvpInner.Value.FilterEnum.FilterIdx);
//					Console.WriteLine("filter       name| " + kvpInner.Value.FilterEnum.Name);
//					Console.WriteLine("filter   opr name| " + kvpInner.Value.CompareOpr.Type.ToString());
//					Console.WriteLine("filter test value| " + kvpInner.Value.TestValue.Value);

					if (result) break;
				}

//				Console.Write(nl);

				if (!result) break;
			}

//			Console.WriteLine("final result| " + result.ToString());
//			Console.WriteLine(nl);

			return result;
		}



		private static void DefineVisibility()
		{
			_visCrossRef[(int) VISIBILITY_HIDDEN] =
				RevisionVisibility.Hidden;
			_visCrossRef[(int) VISIBILITY_CLOUDANDTAG] =
				RevisionVisibility.CloudAndTagVisible;
			_visCrossRef[(int) VISIBILITY_TAGONLY] =
				RevisionVisibility.TagVisible;
		}

	}

	#endregion
}

