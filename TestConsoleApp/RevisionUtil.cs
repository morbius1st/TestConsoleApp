using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static TestConsoleApp.DataItems;
using static TestConsoleApp.DataItems.EDataFields;


using static TestConsoleApp.RevisionFilters.ECompareOps;
using static TestConsoleApp.RevisionFilters;
using static TestConsoleApp.RevisionSelect;

using static TestConsoleApp.RevisionVisibility;

namespace TestConsoleApp
{
		public interface ICloneable<T> : ICloneable where T : ICloneable<T>
	{
		new T Clone();
	}

	public class RevisionUtil
	{
		public static int Switch = 8;


		public static string nl = Environment.NewLine;

		// psudo class to just simulate revit

		public static void Process()
		{
			// setup the revision meta data
			RevisionMetaData.Init();

			// read the revisions from the revit file
			RevisionDataMgr.GetRevisions();

			switch (Switch)
			{
			case 0:
				{
					Process0();
					break;
				}
			case 1:
				{
					Process1();
					break;
				}
			case 2:
				{
					Process2();
					break;
				}
			case 3:
				{
					Process3();
					break;
				}
			case 4:
				{
					Process4();
					break;
				}
			case 5:
				{
					Process5();
					break;
				}			
			case 6:
				{
					Process6();
					break;
				}
			case 7:
				{
					Process7();
					break;
				}
			case 8:
				{
					Process8();
					break;
				}
			}
		}
		
		// list enum and meta data
		public static void Process0()
		{
			// list the enum information
			ListEnum();

			// list the meta data
			ListMetaData();
		}		

		// list the raw data
		public static void Process1()
		{
			Console.WriteLine("master data");
			// how the header
			ListColumnHeadersInColumnOrder();
			// list the data
			ListDataInColumnOrder(RevisionDataMgr.IterateRevisionData(), false, 3);
			Console.Write(nl);

			ListPreSelect();
			Console.Write(nl);

			Console.WriteLine("selected data");
			// how the header
			ListColumnHeadersInColumnOrder();
			// list the data
			ListDataInColumnOrder(RevisionDataMgr.IterateSelected(), false, 3);
			Console.Write(nl);
		}

		// change the which columns to show
		// change the order to show the columns
		public static void Process2()
		{
			// list based on raw data order
			// this is a sample to compare to
			// use as a comparison
			ListColumnHeadersInColumnOrder();
			ListDataInColumnOrder(RevisionDataMgr.IterateRevisionData(), false, 3);

			// change the column order and which columns
			// to show
			ModifyColumnOrder();

			// list based on raw data order
			ListColumnHeadersInColumnOrder();
			ListDataInColumnOrder(RevisionDataMgr.IterateRevisionData(), false, 3);

		}
		
		// create compare filters
		// list the compare filters
		public static void Process3()
		{
			// create a revision filter list
			RevisionFilters rf = TestSelect3();

			// show the filters
			ListFilters(rf);
		}

		// create and use compare filters
		// select using the compare filters
		// list the data
		public static void Process4()
		{
			// create a revision filter list
			RevisionFilters rf = TestSelect4();

			// show the filters
			ListFilters(rf);

							// how the header
			ListColumnHeadersInColumnOrder();
			// list the data
			ListDataInColumnOrder(RevisionDataMgr.IterateRevisionData(), false);

			Console.WriteLine(nl);

			RevisionDataMgr.ResetSelected();

			bool result = RevisionDataMgr.Select(rf);

			if (!result)
			{
				Console.WriteLine("no data selected");
			}
			else
			{
				// how the header
			ListColumnHeadersInColumnOrder();
			// list the data
			ListDataInColumnOrder(RevisionDataMgr.IterateSelected(), false);
			}
		}
	
		// test all comparison methods
		// list results of each test
		public static void Process5()
		{
			string nullString = null;
			ElementId nullElementId = null;

			// ANY
			Console.WriteLine("testing ALL");
			Eval(true,          new Criteria(REV_SELECTED, ANY)                                   , true);
			Eval(5,             new Criteria(REV_SEQ, ANY)                                        , true);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, ANY)                                 , true);
			Eval(Hidden,        new Criteria(REV_ITEM_VISIBLE, ANY)                               , true);
			Eval(               new ElementId(1), new Criteria(REV_TAG_ELEM_ID, ANY)              , true);
			Console.Write(nl);

			// test using bool
			Console.WriteLine("testing bool");
			Eval(true,          new Criteria(REV_SELECTED, TRUE)                                  , true);
			Eval(true,          new Criteria(REV_SELECTED, FALSE)                                 , false);
			Eval(false,         new Criteria(REV_SELECTED, TRUE)                                  , false);
			Eval(false,         new Criteria(REV_SELECTED, FALSE)                                 , true);
			Console.Write(nl);

			// test using int
			Console.WriteLine("testing int");
			Eval(5,             new Criteria(REV_SEQ, EQUAL                         , 5)          , true);
			Eval(5,             new Criteria(REV_SEQ, NOT_EQUAL                     , 5)          , false);
			Eval(5,             new Criteria(REV_SEQ, EQUAL                         , 3)          , false);
			Eval(5,             new Criteria(REV_SEQ, NOT_EQUAL                     , 3)          , true);
			Eval(5,             new Criteria(REV_SEQ, GREATER_THEN                  , 3)          , true);
			Eval(5,             new Criteria(REV_SEQ, GREATER_THEN                  , 5)          , false);
			Eval(5,             new Criteria(REV_SEQ, GREATER_THEN                  , 7)          , false);
			Eval(5,             new Criteria(REV_SEQ, GREATER_THEN_OR_EQUAL         , 3)          , true);
			Eval(5,             new Criteria(REV_SEQ, GREATER_THEN_OR_EQUAL         , 5)          , true);
			Eval(5,             new Criteria(REV_SEQ, GREATER_THEN_OR_EQUAL         , 7)          , false);
			Eval(5,             new Criteria(REV_SEQ, LESS_THEN                     , 3)          , false);
			Eval(5,             new Criteria(REV_SEQ, LESS_THEN                     , 5)          , false);
			Eval(5,             new Criteria(REV_SEQ, LESS_THEN                     , 7)          , true);
			Eval(5,             new Criteria(REV_SEQ, LESS_THEN_OR_EQUAL            , 3)          , false);
			Eval(5,             new Criteria(REV_SEQ, LESS_THEN_OR_EQUAL            , 5)          , true);
			Eval(5,             new Criteria(REV_SEQ, LESS_THEN_OR_EQUAL            , 7)          , true);
			Console.Write(nl);

			// test using string
			Console.WriteLine("testing string");
			// basic and extended
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, EQUAL                  , "BCDE")     , true);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, NOT_EQUAL              , "BCDE")     , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, EQUAL                  , "ACDE")     , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, NOT_EQUAL              , "ACDE")     , true);
																			   
			Eval(nullString,    new Criteria(REV_ITEM_REVID, EQUAL                  , "ACDE")     , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, EQUAL                  , nullString) , false);
			Eval(nullString,    new Criteria(REV_ITEM_REVID, EQUAL                  , nullString) , false);
																			   
			Eval(nullString,    new Criteria(REV_ITEM_REVID, NOT_EQUAL              , "ACDE")     , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, NOT_EQUAL              , nullString) , false);
			Eval(nullString,    new Criteria(REV_ITEM_REVID, NOT_EQUAL              , nullString) , false);
																			   
																		       
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, GREATER_THEN           , "ACDE")     , true);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, GREATER_THEN           , "BCDE")     , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, GREATER_THEN           , "CCDE")     , false);
																		       
			Eval(nullString,    new Criteria(REV_ITEM_REVID, GREATER_THEN           , "ACDE")     , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, GREATER_THEN           , nullString) , false);
			Eval(nullString,    new Criteria(REV_ITEM_REVID, GREATER_THEN           , nullString) , false);
																			   
																			   
																		       
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, GREATER_THEN_OR_EQUAL  , "ACDE")     , true);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, GREATER_THEN_OR_EQUAL  , "BCDE")     , true);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, GREATER_THEN_OR_EQUAL  , "CCDE")     , false);
																    
			Eval(nullString,    new Criteria(REV_ITEM_REVID, GREATER_THEN_OR_EQUAL  , "ACDE")     , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, GREATER_THEN_OR_EQUAL  , nullString) , false);
			Eval(nullString,    new Criteria(REV_ITEM_REVID, GREATER_THEN_OR_EQUAL  , nullString) , false);

																		    
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, LESS_THEN              , "ACDE")     , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, LESS_THEN              , "BCDE")     , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, LESS_THEN              , "CCDE")     , true);
																		       
			Eval(nullString,    new Criteria(REV_ITEM_REVID, LESS_THEN              , "ACDE")     , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, LESS_THEN              , nullString) , false);
			Eval(nullString,    new Criteria(REV_ITEM_REVID, LESS_THEN              , nullString) , false);
																		       
																		       
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, LESS_THEN_OR_EQUAL     , "ACDE")     , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, LESS_THEN_OR_EQUAL     , "BCDE")     , true);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, LESS_THEN_OR_EQUAL     , "CCDE")     , true);
																		       
			Eval(nullString,    new Criteria(REV_ITEM_REVID, LESS_THEN_OR_EQUAL     , "CCDE")     , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, LESS_THEN_OR_EQUAL     , nullString) , false);
			Eval(nullString,    new Criteria(REV_ITEM_REVID, LESS_THEN_OR_EQUAL     , nullString) , false);


			// string unary
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, IS_EMPTY)                            , false);
			Eval("",            new Criteria(REV_ITEM_REVID, IS_EMPTY)                            , true);
			Eval(nullString,    new Criteria(REV_ITEM_REVID, IS_EMPTY)                            , false);

			Eval("BCDE",        new Criteria(REV_ITEM_REVID, IS_NOT_EMPTY)                        , true);
			Eval("",            new Criteria(REV_ITEM_REVID, IS_NOT_EMPTY)                        , false);
			Eval(nullString,    new Criteria(REV_ITEM_REVID, IS_NOT_EMPTY)                        , false);

			// string binary
			// starts with
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, STARTS_WITH            , "BC")       , true);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, STARTS_WITH            , "AA")       , false);
			Eval("",            new Criteria(REV_ITEM_REVID, STARTS_WITH            , "AA")       , false);

			Eval("BCDE",        new Criteria(REV_ITEM_REVID, STARTS_WITH            , "b"         , false), false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, STARTS_WITH            , "b")        , true);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, STARTS_WITH            , "b"         , true), true);
																		       
			Eval(nullString,    new Criteria(REV_ITEM_REVID, STARTS_WITH            , "AA")       , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, STARTS_WITH            , nullString) , false);
			Eval(nullString,    new Criteria(REV_ITEM_REVID, STARTS_WITH            , nullString) , false);
			
			// does not start with
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, DOES_NOT_START_WITH    , "BC")       , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, DOES_NOT_START_WITH    , "AA")       , true);
			Eval("",            new Criteria(REV_ITEM_REVID, DOES_NOT_START_WITH    , "AA")       , true);

			Eval("BCDE",        new Criteria(REV_ITEM_REVID, DOES_NOT_START_WITH    , "b"         , false), true);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, DOES_NOT_START_WITH    , "b")        , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, DOES_NOT_START_WITH    , "b"         , true), false);

			Eval(nullString,    new Criteria(REV_ITEM_REVID, DOES_NOT_START_WITH    , "AA")       , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, DOES_NOT_START_WITH    , nullString) , false);
			Eval(nullString,    new Criteria(REV_ITEM_REVID, DOES_NOT_START_WITH    , nullString) , false);

			// contains
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, CONTAINS               , "CD")       , true);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, CONTAINS               , "AA")       , false);
			Eval("",            new Criteria(REV_ITEM_REVID, CONTAINS               , "AA")       , false);

			Eval("BCDE",        new Criteria(REV_ITEM_REVID, CONTAINS               , "cd"        , false), false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, CONTAINS               , "cd")       , true);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, CONTAINS               , "cd"        , true), true);
																		       
			Eval(nullString,    new Criteria(REV_ITEM_REVID, CONTAINS               , "AA")       , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, CONTAINS               , nullString) , false);
			Eval(nullString,    new Criteria(REV_ITEM_REVID, CONTAINS               , nullString) , false);
																		       
			// does not contain													
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, DOES_NOT_CONTAIN       , "CD")       , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, DOES_NOT_CONTAIN       , "AA")       , true);
			Eval("",            new Criteria(REV_ITEM_REVID, DOES_NOT_CONTAIN       , "AA")       , true);

			Eval("BCDE",        new Criteria(REV_ITEM_REVID, DOES_NOT_CONTAIN       , "cd"        , false), true);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, DOES_NOT_CONTAIN       , "cd")       , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, DOES_NOT_CONTAIN       , "cd"        , true), false);
																		       
			Eval(nullString,    new Criteria(REV_ITEM_REVID, DOES_NOT_CONTAIN       , "AA")       , false);
			Eval("BCDE",        new Criteria(REV_ITEM_REVID, DOES_NOT_CONTAIN       , nullString) , false);
			Eval(nullString,    new Criteria(REV_ITEM_REVID, DOES_NOT_CONTAIN       , nullString) , false);


			Console.Write(nl);

			// test using visibility
			Console.WriteLine("testing visibility");
			// basic
			Eval(Hidden,        new Criteria(REV_ITEM_VISIBLE, EQUAL                , Hidden)     , true);
			Eval(Hidden,        new Criteria(REV_ITEM_VISIBLE, EQUAL                , Hidden)     , true);
			Eval(Hidden,        new Criteria(REV_ITEM_VISIBLE, NOT_EQUAL            , Hidden)     , false);
			Eval(Hidden,        new Criteria(REV_ITEM_VISIBLE, EQUAL                , TagVisible) , false);
			Eval(Hidden,        new Criteria(REV_ITEM_VISIBLE, NOT_EQUAL            , TagVisible) , true);
			Console.Write(nl);											       
																		       
			// test using string										       
			Console.WriteLine("testing element id");					       
			// basic													       
			Eval(new ElementId(1), new Criteria(REV_TAG_ELEM_ID, EQUAL              , new ElementId(1))  , true);
			Eval(new ElementId(1), new Criteria(REV_TAG_ELEM_ID, EQUAL              , new ElementId(2))  , false);
																			        
			Eval(nullElementId   , new Criteria(REV_TAG_ELEM_ID, EQUAL              , new ElementId(1))  , false);
			Eval(new ElementId(1), new Criteria(REV_TAG_ELEM_ID, EQUAL              , nullElementId)     , false);
			Eval(nullElementId   , new Criteria(REV_TAG_ELEM_ID, EQUAL              , nullElementId)     , false);
																			        
			Eval(new ElementId(1), new Criteria(REV_TAG_ELEM_ID, NOT_EQUAL          , new ElementId(1))  , false);
			Eval(new ElementId(1), new Criteria(REV_TAG_ELEM_ID, NOT_EQUAL          , new ElementId(2))  , true);
																			        
			Eval(nullElementId   , new Criteria(REV_TAG_ELEM_ID, NOT_EQUAL          , new ElementId(1))  , false);
			Eval(new ElementId(1), new Criteria(REV_TAG_ELEM_ID, NOT_EQUAL          , nullElementId)     , false);
			Eval(nullElementId   , new Criteria(REV_TAG_ELEM_ID, NOT_EQUAL          , nullElementId)     , false);
			Console.Write(nl);	

			// test using string										       
			Console.WriteLine("testing rev order code");					       
			// basic
			RevOrderCode rc = new RevOrderCode() { AltId = "1",
				DisciplineCode = ".00", TypeCode = ".00" };
			RevOrderCode rcNull = null;
			RevOrderCode rcEmpty = new RevOrderCode();

			Eval(rc              , new Criteria(REV_KEY_ORDER_CODE, EQUAL           , "1.00.00")        , true);
			Eval(rc              , new Criteria(REV_KEY_ORDER_CODE, EQUAL           , "2.00.00")        , false);

			Eval(rc              , new Criteria(REV_KEY_ORDER_CODE, NOT_EQUAL       , "1.00.00")        , false);
			Eval(rc              , new Criteria(REV_KEY_ORDER_CODE, NOT_EQUAL       , "2.00.00")        , true);
			Eval(rcEmpty         , new Criteria(REV_KEY_ORDER_CODE, NOT_EQUAL       , "2.00.00")        , true);
			Eval(rcNull          , new Criteria(REV_KEY_ORDER_CODE, NOT_EQUAL       , "2.00.00")        , true);

						// string unary
			Eval(rc              , new Criteria(REV_KEY_ORDER_CODE, IS_EMPTY)                           , false);
			Eval(rcEmpty         , new Criteria(REV_KEY_ORDER_CODE, IS_EMPTY)                           , true);
			Eval(rcNull          , new Criteria(REV_KEY_ORDER_CODE, IS_EMPTY)                           , true);

			Eval(rc              , new Criteria(REV_KEY_ORDER_CODE, IS_NOT_EMPTY)                       , true);
			Eval(rcEmpty         , new Criteria(REV_KEY_ORDER_CODE, IS_NOT_EMPTY)                       , false);
			Eval(rcNull          , new Criteria(REV_KEY_ORDER_CODE, IS_NOT_EMPTY)                       , false);
							
			// string binary
			// starts with	
			Eval(rc              , new Criteria(REV_KEY_ORDER_CODE, STARTS_WITH      , "1.")            , true);
			Eval(rc              , new Criteria(REV_KEY_ORDER_CODE, STARTS_WITH      , "2.")            , false);
																								        
			Eval(rc              , new Criteria(REV_KEY_ORDER_CODE, STARTS_WITH      , nullString)      , false);
																								        
			Eval(rcEmpty         , new Criteria(REV_KEY_ORDER_CODE, STARTS_WITH      , "AA")            , false);
			Eval(rcEmpty         , new Criteria(REV_KEY_ORDER_CODE, STARTS_WITH      , nullString)      , false);
																								        
			Eval(rcNull          , new Criteria(REV_KEY_ORDER_CODE, STARTS_WITH      , "AA")            , false);
			Eval(rcNull          , new Criteria(REV_KEY_ORDER_CODE, STARTS_WITH      , nullString)      , false);


			Console.Write(nl);
		}
		
		// select from selected
		// select using the compare filters
		// list the data
		public static void Process6()
		{
			// create a revision filter list
			RevisionFilters rf = TestSelect6A();

			// how the header
			ListColumnHeadersInColumnOrder();
			// list the master date
			ListDataInColumnOrder(RevisionDataMgr.IterateRevisionData(), false);

			RevisionDataMgr.ResetSelected();

			bool result = RevisionDataMgr.Select(rf);

			if (!result)
			{
				Console.Write(nl);
				Console.WriteLine("no data selected");
			}
			else
			{
				// if anything selected
				// how the header
				ListColumnHeadersInColumnOrder();
				// list the data
				ListDataInColumnOrder(RevisionDataMgr.IterateSelected(), false);

				RevisionDataMgr.UpdatePreSelected();

				rf = TestSelect6B();

				result = RevisionDataMgr.Select(rf);

				if (!result)
				{
					Console.Write(nl);
					Console.WriteLine("no data selected");
				}
				else
				{
					// if anything selected
					// how the header
					ListColumnHeadersInColumnOrder();
					// list the data
					ListDataInColumnOrder(RevisionDataMgr.IterateSelected(), false);
				}
			}
		}

		// verify cloning of selection data
		// list each
		// this required adjusting the reset method 
		// and is irrelevant otherwise
		public static void Process7()
		{
			ListColumnHeadersInColumnOrder();
			ListDataInColumnOrder(RevisionDataMgr.IterateRevisionData(), false, 3);
			Console.WriteLine(nl);

			ListColumnHeadersInColumnOrder();
			ListDataInColumnOrder(RevisionDataMgr.IteratePreSelected(), false, 3);
			Console.WriteLine(nl);

			ListColumnHeadersInColumnOrder();
			ListDataInColumnOrder(RevisionDataMgr.IterateSelected(), false, 3);
			Console.WriteLine(nl);
		}

		// test sorting by various fields and
		// by several fields
		public static void Process8()
		{
			ListPreSelect();
			Console.Write(nl);


		}





		private static void Eval(RevOrderCode a, Criteria c, bool expected)
		{
			bool result = Verify(a, c);
			string ax = a?.ToString() ?? "null";

			ListResult(ax, c.TestValue?.AsOrderCode, c, result, expected);
		}
		
		private static void Eval(ElementId a, Criteria c, bool expected)
		{
			bool result = Verify(a, c);

			string ax = a?.Value.ToString() ?? "null";
			string cx = c.TestValue?.AsElementId?.ToString() ?? "null";

			ListResult(ax, cx, c, result, expected);
		}
		
		private static void Eval(RevisionVisibility a, Criteria c, bool expected)
		{
			bool result = Verify(a, c);
			ListResult(a, c.TestValue?.AsVisibility, c, result, expected);
		}

		private static void Eval(int a, Criteria c, bool expected)
		{
			bool result = Verify(a, c);
			ListResult(a, c.TestValue?.AsInt, c, result, expected);
		}

		private static void Eval(bool a, Criteria c, bool expected)
		{
			bool result = Verify(a, c);
			ListResult(a, c.CompareOpr == TRUE, c, result, expected);
		}

		private static void Eval(string a, Criteria c, bool expected)
		{
			bool result = Verify(a, c);

			a = a ?? "null";
			string b = c.TestValue?.AsString ?? "null";

			if (a == "") a = "empty";
			if (b == "") b = "empty";

			ListResult(a, b, c, result, expected);
		}

		private static void ListResult<T>(T a, T b, Criteria c, bool result, bool expected)
		{
			string msg1 = "testing " + a + " " + c.CompareOpr.Type.ToString()
			+ " " + b + " is| ";
			string msg2 = msg1.PadRight(46,'.') + result.ToString().PadRight(6) 
				+ " (" + expected.ToString().PadRight(6) + " expected)";

			if (result == expected)
			{
				Console.Write("__ worked __ | ");
			}
			else
			{
				Console.Write("** failed ** | ");
			}

			Console.WriteLine(msg2);
		}
	
		// create a example filter list
		// included are failures for testing
		private static RevisionFilters TestSelect3()
		{
			RevisionFilters f = new RevisionFilters();

			Criteria c;

			// any - string
			c = new Criteria(REV_ITEM_BASIS, ANY);
			f.Add(c);

			// bool - bool
			c = new Criteria(REV_SELECTED, TRUE);
			f.Add(c);

			// basic - elementid
			c = new Criteria(REV_TAG_ELEM_ID, EQUAL, new ElementId(10101));
			f.Add(c);

			// basic - visibility
			c = new Criteria(REV_ITEM_VISIBLE, EQUAL, Hidden);
			f.Add(c);
			
			// extended - int
			c = new Criteria(REV_SEQ, LESS_THEN, 1);
			f.Add(c);
			
			// string unary
			c = new Criteria(REV_ITEM_REVID, IS_EMPTY);
			f.Add(c);
			
			// string binary
			c = new Criteria(REV_KEY_SHEETNUM, STARTS_WITH, "c", true);
			f.Add(c);
			
			// string unary - RevOrderCode
			c = new Criteria(REV_KEY_ORDER_CODE, IS_EMPTY);
			f.Add(c);

			// string binary - RevOrderCode
			c = new Criteria(REV_KEY_ORDER_CODE, STARTS_WITH, "a", true);
			f.Add(c);

			return f;
		}
		
		// create a example filter list
		// included are failures for testing
		private static RevisionFilters TestSelect4()
		{
			RevisionFilters f = new RevisionFilters();

			Criteria c;

			// basis
			c = new Criteria(REV_ITEM_BASIS, EQUAL, "pcc");
			f.Add(c);
			c = new Criteria(REV_ITEM_BASIS, EQUAL, "rfi");
			f.Add(c);

			// bool - bool
			c = new Criteria(REV_SELECTED, FALSE);
			f.Add(c);

			// basic - visibility
			c = new Criteria(REV_ITEM_VISIBLE, EQUAL, TagVisible);
			f.Add(c);
			
			// string unary
			c = new Criteria(REV_ITEM_REVID, IS_EMPTY);
			f.Add(c);
			
			// string binary
			c = new Criteria(REV_KEY_SHEETNUM, STARTS_WITH, "c");
			f.Add(c);
			c = new Criteria(REV_KEY_SHEETNUM, STARTS_WITH, "a");
			f.Add(c);

			
			// string unary - RevOrderCode
			c = new Criteria(REV_KEY_ORDER_CODE, IS_EMPTY);
			f.Add(c);

			// string binary - RevOrderCode
			c = new Criteria(REV_KEY_ORDER_CODE, STARTS_WITH, "1", true);
			f.Add(c);

			return f;
		}	
		
		// create a filter list
		// included are failures for testing
		private static RevisionFilters TestSelect6A()
		{
			RevisionFilters f = new RevisionFilters();

			f.Add(new Criteria(REV_KEY_DELTA_TITLE, CONTAINS, "007"));
			f.Add(new Criteria(REV_KEY_DELTA_TITLE, CONTAINS, "013"));

			return f;
		}
		
		// create a filter list to sub filter
		// included are failures for testing
		private static RevisionFilters TestSelect6B()
		{
			RevisionFilters f = new RevisionFilters();

			f.Add(new Criteria(REV_ITEM_BASIS, EQUAL, "pcc"));

			return f;
		}
	
		
		// list the pre-select data and header 
		// in column order
		private static void ListPreSelect(int qty = 0)
		{
			Console.WriteLine("pre-selected data");
			// how the header
			ListColumnHeadersInColumnOrder();
			// list the data
			ListDataInColumnOrder(RevisionDataMgr.IteratePreSelected(), false, qty);
		}
		
		private static void ListFilters(RevisionFilters f)
		{
			foreach (KeyValuePair<FilterEnum, Filters> kvpOutter in f)
			{
				
				Console.WriteLine("      op name| " + kvpOutter.Key.Name + " (" + kvpOutter.Key.FilterIdx + ")");

				foreach (KeyValuePair<int, Criteria> kvpInner in kvpOutter.Value)
				{
					
					Console.WriteLine("      op type| " + kvpInner.Value.CompareOpr.Type.ToString());
					Console.WriteLine("           op| " + kvpInner.Value.CompareOpr.GetType().Name);

					switch (kvpOutter.Key.Type)
					{
					case EDataType.BOOL:
						{
							Console.WriteLine("test val bool| " + 
								(kvpInner.Value.TestValue?.AsBool.ToString() ?? "null"));
							break;
						}
					case EDataType.ELEMENTID:
						{
							Console.WriteLine("test val Eid | " +
								(kvpInner.Value.TestValue?.AsElementId.ToString() ?? "null"));
							break;
						}
					case EDataType.INT:
						{
							Console.WriteLine("test val int | " + 
								(kvpInner.Value.TestValue?.AsInt.ToString() ?? "null"));
							break;
						}
					case EDataType.ORDER:
					case EDataType.STRING:
						{
							if (kvpInner.Value.CompareOpr is ICompStringUnary)
							{
								Console.WriteLine("test val str | unary");
							}
							else
							{
								Console.WriteLine("test val str | binary| " +
									(kvpInner.Value.TestValue?.AsString ?? "null"));
							}

							break;
						}
					case EDataType.VISIBILITY:
						{
							Console.WriteLine("test val vis | " + 
								(kvpInner.Value.TestValue?.AsVisibility.ToString() ?? "null"));
							break;
						}
					}
					Console.Write(nl);
				}
				Console.Write(nl);
			}
		}

		public static void ListColumnHeadersInColumnOrder()
		{
			ListColnHeaders(0);
			ListColnHeaders(1);
		}

		private static void ListColnHeaders(int row)
		{
			if (row == 0)
			{
				Console.Write("|     |");
			}
			else
			{
				Console.Write("| -#- |");
			}

			foreach (DataEnum item in ColumnList)
			{
				ListARowTitle(item, row);
			}

			Console.Write(nl);
		}

		private static void ListARowTitle(SubDataEnum item, int row)
		{
			string title;
			char spacer = item.Title[row].Equals("") ? ' ' : '-';

			int width = item.Display.ColumnWidth == 0 ? 10 : item.Display.ColumnWidth;

			title = item.Title[row].PadCenter(item.Display.MaxTitleWidth, spacer);
				title = title.PadCenter(width, spacer);

			Console.Write(title);

			Console.Write("|"); // space between columns

			if (item.SubDataList != null)
			{
				foreach (SubDataEnum subDataEnum in item.SubDataList)
				{
					ListARowTitle(subDataEnum, row);
				}
			}
		}

		// list based on raw read data order
		private static void ListDataInColumnOrder(
			IEnumerable<KeyValuePair<string, RevisionDataFields>> iEnumerable,
			bool header, int qty = 0)
		{
			int i = 1;
			
			foreach (KeyValuePair<string, RevisionDataFields> kvp in iEnumerable)
			{
				if (header)
				{
					Console.Write(nl);
					Console.Write($"{"data item",24} | {i,-4:D}");
					Console.WriteLine($" | {"key",24} | " + kvp.Key);
				}

				ListDataItemInColumnOrder(i, kvp.Value);

				if (i++ == qty) break;
			}
			Console.WriteLine(nl);
		}

		public static void ListDataItemInColumnOrder(int idx, RevisionDataFields items)
		{
			Console.Write($"| {idx,3:D} |");

			foreach (DataEnum dataEnum in ColumnList)
			{
				ListAnItem(items[dataEnum.DataIdx], dataEnum);

				if (dataEnum.SubDataList != null)
				{
					for (int i = 0; i < dataEnum.SubDataList.Length; i++)
					{
						ListAnItem(((SubData) items[dataEnum.DataIdx])[i], 
							dataEnum.SubDataList[i]);
					}
				}
			}

			Console.Write(nl);
		}

		public static void ListAnItem(dynamic data, DescEnum dataEnum)
		{
			string dataFormatted = string.Format(dataEnum.Display.FormatString,
					data);

				int colWidth = dataEnum.Display.ColumnWidth;

				switch (dataEnum.Display.Justify)
				{
				case RevisionMetaData.Justification.LEFT:
					{
						dataFormatted = dataFormatted.PadRight(colWidth);
						break;
					}
				case RevisionMetaData.Justification.CENTER:
					{
						dataFormatted = dataFormatted.PadCenter(colWidth);
						break;
					}
				case RevisionMetaData.Justification.RIGHT:
					{
						dataFormatted = dataFormatted.PadLeft(colWidth);
						break;
					}
				}

				Console.Write(dataFormatted);
				Console.Write("|");
		}

		// modify then list on column property order
		private static void ModifyColumnOrder()
		{
			// change the column order for testing
			// set column to -1 to mean - don't include
			SetColumns(-1);

			REV_SELECTED.Column = 1;
			REV_SEQ.Column = 0;

			REV_KEY_SHEETNUM.Column = 4;
			REV_ITEM_DATE.Column = 6;
			REV_KEY_DELTA_TITLE.Column = 3;
			REV_ITEM_BASIS.Column = 2;
			REV_ITEM_DESC.Column = 10;

			SetColumnOrder();
		}

		private static void ListMetaData()
		{
			string fmtx = String.Format(
				"{0,-3:#0} | name | {1,-26} | type | {2,-15} | "
				+ "title | {3,-22} | column | {4,-4:##0} | "
				+ "source | {5,-26} | disp/fmt | {6,-8}",
				1, "name", "type", "title", 1, "source", "disp/fmt"
			);

			string fmtRoot =
				"{0,-3:#0} | root | name | {1,-26} | type | {2,-15} | "
				+ "title | {3,-22}";
			
			string fmtSubField =
				"{0,-3:#0} | item | name | {1,-26} | type | {2,-15} | "
				+ "title | {3,-22}";

			string fmtData =
				"{0,-3:#0} | item | name | {1,-26} | type | {2,-15} | "
				+ "title | {3,-22} | column | {4,-4:##0} | "
				+ "source | {5,-26} | disp/fmt | {6,-8}";

			string fmtDesc =
				"{0,-3:#0} | item | name | {1,-26} | type | {2,-15} | "
				+ "title | {3,-22} | column | {4,-4:##0} "
				+ "| disp/fmt | {5,-8}";

			int i = 0;

			Console.Write(nl);

			foreach (RootEnum rx in RootList)
			{
				if (rx is DataEnum)
				{
					DataEnum ix = (DataEnum) rx;
					Console.WriteLine(fmtData, i, ix.Name, ix.Type.ToString(), FormatTitle(ix.Title), ix.Column, ix.Source.ToString(), ix.Display.FormatString);

					if (ix.SubDataList != null)
					{
						Console.Write("+-->| sub fields  ");

						for (var j = 0; j < ix.SubDataList.Length; j++)
						{
							Console.Write("| " + j + " | " + ix.SubDataList[j].Name);
						}
						Console.Write(nl);
					}
				}
				else if (rx is DescEnum)
				{
					DescEnum ix = (DescEnum) rx;
					Console.WriteLine(fmtDesc, i, ix.Name, ix.Type.ToString(), FormatTitle(ix.Title), ix.Column, ix.Display.FormatString);
				}
				else
				{
					Console.WriteLine(fmtRoot, i, rx.Name, rx.Type.ToString(), FormatTitle(rx.Title));
				}

				i++;
			}
		}

		private static string FormatTitle(string[] title)
		{
			if (title[0] == null || title[0].Equals(""))
			{
				return title[1];
			}

			return title[0] + "" + title[1];
		}

		private static void ListEnum()
		{
			int i = 0;

			string num = "#0;__";

			string fmt = String.Format(
				"{0,-4:#0} name| {1,-26} | desc idx| {2,-3} | data idx| {3,-3:D} | filt idx| {4,-3:D}", 
				1, "A", 1, 1, 1, "", "", "");

			string fmt2 =
				"{0,4:D}"
				+ " | name| {1,-26}"
				+ " | desc idx| {2,-3:" + num + "}"
				+ " | data idx| {3,-3:"  + num + "}"
				+ " | filt idx| {4,-3:" + num + "}";


			Console.WriteLine("root List");

			foreach (RootEnum rx in RootList)
			{
				Console.WriteLine(fmt2, i++, rx.Name, -1, -1, 0);
			}
						
			i = 0;

			Console.WriteLine(nl + "desc list");
			foreach (DescEnum dx in DescList)
			{
				Console.WriteLine(fmt2, i++, dx.Name, dx.DescItemIdx, -1, -1);
			}
			
			i = 0;

			Console.WriteLine(nl + "data list");
			foreach (DataEnum dx in DataList)
			{
				Console.Write(fmt2, i++, dx.Name, dx.DescItemIdx, dx.DataIdx, -1);

				if (dx.SubDataList != null)
				{
					int j = 0;

					foreach (SubDataEnum sub in dx.SubDataList)
					{
						Console.Write("| " + j++ + "| (" + sub.Name + ")");
					}
				}

				Console.Write(nl);
			}

			i = 0;

			Console.WriteLine(nl + "filter list");
			foreach (FilterEnum fx in FilterList)
			{
				Console.WriteLine(fmt2, i++, fx.Name, fx.DescItemIdx, fx.DataIdx, fx.FilterIdx);
			}
			
			i = 0;
			
			Console.WriteLine(nl + "column list");
			foreach (DataEnum dx in ColumnList)
			{
				Console.WriteLine(fmt2, i++, dx.Name, dx.DescItemIdx, dx.DataIdx, -1);
			}
		}
	}

	public static class extensions
	{
		public static string PadCenter(this string s, int w, char cl = (char) 32, char cr = (char) 0)
		{
			if (s == null || s.Length >= w) return s;

			int len = s.Length;
			int space = (w - len) / 2;
			int padding = len + space;

			if (cr == 0) cr = cl;

			if (char.IsControl(cr)) cr = (char) 32;
			if (char.IsControl(cl)) cl = (char) 32;

			string first = s.PadLeft(padding, cl);
			string final = first.PadRight(w, cr);

			int l = final.Length;

			return final;

		}
	}
}
