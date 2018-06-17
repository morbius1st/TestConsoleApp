﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Reflection;
using static TestConsoleApp.DataItems;
using static TestConsoleApp.RevisionSelect.ESelVisibility;
using static TestConsoleApp.RevisionSelect;


using static TestConsoleApp.RevisionFilters;
using static TestConsoleApp.RevisionUtil;

namespace TestConsoleApp
{
	// selection method:
	//	this item
	// visible or not visible
	//	and / or
	//	one of these items
	// revision id (==, >, >=, <, <=, !=)#
	// revision alt id == ## (will get master and childern
	// block title == ""
	// delta title == ""
	//	and / or this item
	// basis


	// this class holds the list of filters


	public class RevisionFilters : IEnumerable<KeyValuePair<FilterEnum, Filters>>
	{
		#region + Compare Ops

		public enum CompareType
		{
			ANY ,
			TRUE ,
			FALSE ,
			NOT_EQUAL ,
			EQUAL ,
			GREATER_THEN ,
			LESS_THEN ,
			GREATER_THEN_OR_EQUAL,
			LESS_THEN_OR_EQUAL ,
			IS_EMPTY ,
			IS_NOT_EMPTY ,
			STARTS_WITH ,
			DOES_NOT_START_WITH ,
			CONTAINS ,
			DOES_NOT_CONTAIN ,
		}


		public static class CompareOps 
		{
			public static CompareOpAny       ANY                    { get; } = new CompareOpAny      ();

			public static CompareOpBool      TRUE                   { get; } = new CompareOpBool     ();
			public static CompareOpBool      FALSE                  { get; } = new CompareOpBool     ();

			public static CompareOpBasic     NOT_EQUAL              { get; } = new CompareOpBasic    ();
			public static CompareOpBasic     EQUAL                  { get; } = new CompareOpBasic    ();

			public static CompareOpExtended  GREATER_THEN           { get; } = new CompareOpExtended ();
			public static CompareOpExtended  LESS_THEN              { get; } = new CompareOpExtended ();
			public static CompareOpExtended  GREATER_THEN_OR_EQUAL  { get; } = new CompareOpExtended ();
			public static CompareOpExtended  LESS_THEN_OR_EQUAL     { get; } = new CompareOpExtended ();

			public static CompareOpStrUnary  IS_EMPTY               { get; } = new CompareOpStrUnary ();
			public static CompareOpStrUnary  IS_NOT_EMPTY           { get; } = new CompareOpStrUnary ();

			public static CompareOpStrBinary STARTS_WITH            { get; } = new CompareOpStrBinary();
			public static CompareOpStrBinary DOES_NOT_START_WITH    { get; } = new CompareOpStrBinary();

			public static CompareOpStrBinary CONTAINS               { get; } = new CompareOpStrBinary();
			public static CompareOpStrBinary DOES_NOT_CONTAIN       { get; } = new CompareOpStrBinary();


			static CompareOps()
			{
// just for testing the hierearchy
//				//                       any     bool   basic     extended      string-un   string-bi
//				// only any               Y       N       N           N            N	        N
//				ICompAny[] a1 =         {ANY,    TRUE, NOT_EQUAL, GREATER_THEN, IS_EMPTY,   CONTAINS};
//				// only bool              N       Y       N           N            N	        N
//				ICompBool[] bl1 =       {ANY,    TRUE, NOT_EQUAL, GREATER_THEN, IS_EMPTY,   CONTAINS};
//				// only basic             N       N       Y           N            N	        N
//				ICompBasic[] b1 =       {ANY,    TRUE, NOT_EQUAL, GREATER_THEN, IS_EMPTY,   CONTAINS};
//				// extended, basic        N        N      Y           Y            N	        N
//				ICompExtended[] n1 =    {ANY,    TRUE, NOT_EQUAL, GREATER_THEN, IS_EMPTY,   CONTAINS};
//				// all ex bool & any      N        N      N           N            Y	        N
//				ICompStringUnary[] s1 = {ANY,    TRUE, NOT_EQUAL, GREATER_THEN, IS_EMPTY,   CONTAINS};
//				// all ex bool & any      N        N      Y           Y            N	        Y
//				ICompStringBinary[] s2 ={ANY,    TRUE, NOT_EQUAL, GREATER_THEN, IS_EMPTY,   CONTAINS};
//				// all ex bool & any      N        Y      Y           Y            Y	        Y
//				ICompString[] s3       ={ANY,    TRUE, NOT_EQUAL, GREATER_THEN, IS_EMPTY,   CONTAINS};
//				// all                    Y        Y      Y           Y            Y	        Y
//				ICompRoot[] r1 =        {ANY,    TRUE, NOT_EQUAL, GREATER_THEN, IS_EMPTY,   CONTAINS};

				foreach (PropertyInfo p in typeof(CompareOps).GetProperties())
				{
					CompareOpRoot r = (CompareOpRoot) p.GetValue(null);
//					r.Name = p.Name;
					r.Type = (CompareType) Enum.Parse(typeof(CompareType), p.Name);
				}
			}
//
//			public static IEnumerable<ICompBasic> GetBasicEnumerator()
//			{
//				yield return EQUAL;
//				yield return NOT_EQUAL;
//			}
//			public static IEnumerable<ICompExtended> GetExtendedEnumerator()
//			{
//				yield return EQUAL;
//				yield return NOT_EQUAL;
//				yield return LESS_THEN;
//				yield return LESS_THEN_OR_EQUAL;
//				yield return GREATER_THEN;
//				yield return GREATER_THEN_OR_EQUAL;
//			}
//			public static IEnumerable<ICompStringBinary> GetString2Enumerator()
//			{
//				yield return EQUAL;
//				yield return NOT_EQUAL;
//				yield return LESS_THEN;
//				yield return LESS_THEN_OR_EQUAL;
//				yield return GREATER_THEN;
//				yield return GREATER_THEN_OR_EQUAL;
//				yield return STARTS_WITH;
//				yield return DOES_NOT_START_WITH;
//				yield return CONTAINS;
//				yield return DOES_NOT_CONTAIN;
//			}
//
//			public static IEnumerable<ICompStringUnary> GetString1Enumerator()
//			{
//				yield return IS_EMPTY;
//				yield return IS_NOT_EMPTY;
//			}

		}

		#endregion

		private FilterLists _filterList = new FilterLists();
	
		#region + Selection Criteria Class Elements

		public RevisionFilters() { }

		public int Count => _filterList.Count();

		public void Add(Criteria c)
		{
			_filterList.Add(c.FilterEnum, c);
		}

		#endregion

		#region + Helper Classes & Structs

		public IEnumerator<KeyValuePair<FilterEnum, Filters>> GetEnumerator()
		{
			return _filterList.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#region + Criteria

		public struct Criteria
		{
			public FilterEnum FilterEnum { get;  }
			public CompareOpRoot CompareOpr { get;  }
			public TestValue TestValue { get;  }
			public bool IgnoreCase { get;  }

			// Any
			public Criteria(FilterEnum filterEnum,
				ICompAny compareOprAny)
			{
				FilterEnum = filterEnum;
				CompareOpr = (CompareOpRoot) compareOprAny;
				TestValue = null;
				IgnoreCase = true;
			}

			// basic / RevitVisibility
			public Criteria(CompareVisEnum filterEnum,
				ICompBasic compareOpr, RevisionVisibility testValue)
			{
				FilterEnum = filterEnum;
				CompareOpr = (CompareOpRoot) compareOpr;
				TestValue = new TestValue(testValue);
				IgnoreCase = true;
			}

			// basic / ElementId
			public Criteria(CompareEIdEnum filterEnum,
				ICompBasic compareOpr, ElementId testValue)
			{
				FilterEnum = filterEnum;
				CompareOpr =  (CompareOpRoot) compareOpr;
				TestValue = new TestValue(testValue);
				IgnoreCase = true;
			}

			// bool
			public Criteria(CompareBoolEnum filterEnum,
				CompareOpBool compareOpr)
			{
				FilterEnum = filterEnum;
				CompareOpr = compareOpr;
				TestValue = null;
				IgnoreCase = true;
			}

			// extended
			public Criteria(CompareIntEnum filterEnum,
				ICompExtended compareOpr, int testValue)
			{
				FilterEnum = filterEnum;
				CompareOpr = (CompareOpRoot) compareOpr;
				TestValue = new TestValue(testValue);
				IgnoreCase = true;
			}

			// strings
			public Criteria(CompareStrEnum filterEnum,
				ICompStringUnary compareOpr)
			{
				FilterEnum = filterEnum;
				CompareOpr = (CompareOpRoot) compareOpr;
				TestValue = null;
				IgnoreCase = true;
			}

			public Criteria(CompareStrEnum filterEnum,
				ICompStringBinary compareOpr,
				string testValue,
				bool ignoreCase = true)
			{
				FilterEnum = filterEnum;
				CompareOpr = (CompareOpRoot) compareOpr;
				TestValue = new TestValue(testValue);
				IgnoreCase = ignoreCase;
			}

			
		}

		public class TestValue
		{
			public dynamic Value { get; private set; }

			public bool? AsBool
			{
				get => Value;
				set => Value = value;
			}

			public int AsInt
			{
				get => Value;
				set => Value = value;
			}

			public ElementId AsElementId
			{
				get => Value;
				set => Value = value;
			}

			public RevisionVisibility AsVisibility
			{
				get => Value;
				set => Value = value;
			}

			public string AsString
			{
				get => Value;
				set => Value = value;
			}

			public string Type()
			{
				return Value.GetType().ToString();
			}

			public TestValue(dynamic x)
			{
				Value = x;
			}
		}

		#endregion

		#region + Compare Ops Configuration

		public interface ICompRoot
		{
//			string Name { get; set; }
			CompareType Type { get; }
		}

		public interface ICompAny	//  : ICompStringUnary, ICompExtended, ICompBasic, ICompBool
		{}
		public interface ICompBasic  : ICompExtended, ICompString
		{}
		public interface ICompBool 
		{}
		public interface ICompExtended : ICompStringBinary, ICompString
		{}
		public interface ICompString
		{}
		public interface ICompStringUnary : ICompString
		{}
		public interface ICompStringBinary : ICompString
		{}

		public class CompareOpRoot : ICompRoot
		{
//			public string Name { get; set; }
			public CompareType Type { get; set; }

			public CompareOpRoot()
			{
				Type = 0;
			}
		}
		
		public class CompareOpAny : CompareOpRoot, ICompAny
		{

//			public CompareOpAny(int val) : base(val) {}
		}

		public class CompareOpBool : CompareOpRoot, ICompBool
		{
//			public CompareOpBool(int val) : base(val) {}
		}	
		
		public class CompareOpBasic : CompareOpRoot, ICompBasic
		{
//			public CompareOpBasic(int val) : base(val)
		}

		public class CompareOpExtended : CompareOpRoot, ICompExtended
		{
//			public CompareOpExtended(int val) : base(val) {}
		}
		
		public class CompareOpStrUnary : CompareOpRoot, ICompStringUnary
		{
//			public CompareOpStrUnary(int val) : base(val) {}
		}
		
		public class CompareOpStrBinary : CompareOpRoot, ICompStringBinary
		{
//			public CompareOpStrBinary(int val) : base(val) {}
		}

		#endregion	

		#region + Filter Lists

		// this is a list of filter lists - that is, it holds
		// a lists of criteria lists.  each list is 
		// associated with a specific FilterEnum
		public class FilterLists :
			IEnumerable<KeyValuePair<FilterEnum, Filters>>
		{
			private SortedList<FilterEnum, Filters> _filterList =
				new SortedList<FilterEnum, Filters>(new FilterCompare());

			public void Add(FilterEnum filterEnum, Criteria criteria)
			{
				if (ContainsKey(filterEnum))
				{
					// update existing
					_filterList[filterEnum].Add(criteria);
				}
				else
				{
					// add new
					_filterList.Add(filterEnum, new Filters(criteria));
				}
			}

			public int Count()
			{
				return _filterList.Count;
			}

			public int Count(FilterEnum filterEnum)
			{
				if (!ContainsKey(filterEnum)) return -1;

				return _filterList[filterEnum].Count;
			}

			public bool ContainsKey(FilterEnum filterEnum)
			{
				return _filterList.ContainsKey(filterEnum);
			}

			public IEnumerator<KeyValuePair<FilterEnum, Filters>> GetEnumerator()
			{
				return _filterList.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		// this is a comparer that allows sorting via filterenum
		public class FilterCompare : IComparer<FilterEnum>
		{
			public int Compare(FilterEnum x, FilterEnum y)
			{
				return x.FilterIdx.CompareTo(y.FilterIdx);
			}
		}

		// a list of criteria all of which are the same FilterEnum
		public class Filters :
			IEnumerable<KeyValuePair<int, Criteria>>
		{
			// the int is the unique code which is just
			// a simple accending number
			private SortedList<int, Criteria> _filters =
				new SortedList<int, Criteria>(3);

			private static int uniqueCode = 0;

			public Filters(Criteria criteria)
			{
				Add(criteria);
			}

			public void Add(Criteria criteria)
			{
				_filters.Add(uniqueCode++, criteria);
			}

			public int Count => _filters.Count;

			public IEnumerator<KeyValuePair<int, Criteria>> GetEnumerator()
			{
				return _filters.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		#endregion

		#endregion
	}

}