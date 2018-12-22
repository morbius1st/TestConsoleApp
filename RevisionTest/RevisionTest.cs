using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static RevisionTest.DataItems;
using static RevisionTest.DataItems.EDataFields;

using static RevisionTest.RevisionFilters.ECompareOps;
using static RevisionTest.RevisionFilters;
using static RevisionTest.RevisionSelect;
using static RevisionTest.RevisionVisibility;
using static RevisionTest.RevisionFormat;
using static RevisionTest.RevisionMetaData;

namespace RevisionTest
{
	public class RevisionTest
	{
		// test list rev order = 0
		// test list filters (list criteria) = 3
		// create excel = 50
		public static int Switch = 50;

		public static string nl = Environment.NewLine;

		public static void Process()
		{
			Console.WindowWidth = Console.LargestWindowWidth;


			int x = DataList?.Count ?? 0;

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
			case 9:
				{
					Process9();
					break;
				}			
			case 10:
				{
					Process10();
					break;
				}
			case 11:
				{
					Process11();
					break;
				}			
			case 12:
				{
					Process12();
					break;
				}			
			case 50:
				{
					Process50();
					break;
				}
			}
		}
		
		// list enum and meta data and column order
		public static void Process0()
		{
			// list the enum information
			ListEnum();

			// list the meta data
			ListMetaData();

			RevOrderMgr om = new RevOrderMgr();

			 // list the default column order info
			ListRevOrder(om);

			om.ColumnOrder.Start(REV_SEQ, REV_SELECTED, 
				REV_ITEM_BLOCK_TITLE, REV_SORT_DELTA_TITLE, REV_SORT_SHEETNUM,
				REV_SORT_ORDER_CODE);

			om.SortOrder.Start(REV_SORT_DELTA_TITLE, REV_SORT_ITEM_REVID,
				REV_SORT_ITEM_DESC);

			 // list the column order info
			ListRevOrder(om);

			om.ColumnOrder.Start();

			om.SortOrder.Start();

			 // list the column order info
			ListRevOrder(om);
		}		

		// list the raw data
		public static void Process1()
		{
			RevOrderMgr ro = new RevOrderMgr();

			Console.WriteLine("master data");
			// how the header
			ListColumnHeadersInColumnOrder(ro);
			// list the data
			ListDataInColumnOrder(RevisionDataMgr.IterateRevisionData(), ro, false);
			Console.Write(nl);

			ListPreSelected();
			Console.Write(nl);

			ListSelected();
			Console.Write(nl);
		}

		// change the order and which columns to show
		public static void Process2()
		{
			RevOrderMgr ro = new RevOrderMgr();
			// list based on raw data order
			// this is a sample to compare to
			// use as a comparison
			ListColumnHeadersInColumnOrder(ro);
			ListDataInColumnOrder(RevisionDataMgr.IterateRevisionData(), ro, false, 3);

			// change the column order and which columns
			// to show
			ro = ModifyColumnOrder();

			// list based on raw data order
			ListColumnHeadersInColumnOrder(ro);
			ListDataInColumnOrder(RevisionDataMgr.IterateRevisionData(), ro, false, 3);
		}

		// uses listfilters		
		// create list compare filters
		public static void Process3()
		{
			// create a revision filter list
			RevisionFilters rf = TestSelect3();

			// show the filters
			ListFilters(rf);
		}

		// create filters and select using the compare filters
		public static void Process4()
		{
			// create a revision filter list
			RevisionFilters rf = TestSelect4();

			RevOrderMgr ro = new RevOrderMgr();

			// show the filters
			ListFilters(rf);

							// how the header
			ListColumnHeadersInColumnOrder(ro);
			// list the data
			ListDataInColumnOrder(RevisionDataMgr.IterateRevisionData(), ro, false);

			Console.WriteLine(nl);

			RevisionDataMgr.ResetPreSelected();

			bool result = RevisionDataMgr.Select(rf);

			if (!result)
			{
				Console.WriteLine("no data selected");
				Console.Write(nl);
			}
			else
			{
				// how the header
			ListColumnHeadersInColumnOrder(ro);
			// list the data
			ListDataInColumnOrder(RevisionDataMgr.IterateSelected(), ro, false);
			}
		}
	
		// test comparison methods
		public static void Process5()
		{
			string nullString = null;
			ElementId nullElementId = null;

			// ANY
			Console.WriteLine("testing ALL");
			Eval(true,          new RevisionFilters.Criteria(REV_SELECTED, ANY)                                   , true);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, ANY)                                        , true);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, ANY)                                 , true);
			Eval(Hidden,        new RevisionFilters.Criteria(REV_ITEM_VISIBLE, ANY)                               , true);
			Eval(               new ElementId(1), new RevisionFilters.Criteria(REV_TAG_ELEM_ID, ANY)              , true);
			Console.Write(nl);

			// test using bool
			Console.WriteLine("testing bool");
			Eval(true,          new RevisionFilters.Criteria(REV_SELECTED, TRUE)                                  , true);
			Eval(true,          new RevisionFilters.Criteria(REV_SELECTED, FALSE)                                 , false);
			Eval(false,         new RevisionFilters.Criteria(REV_SELECTED, TRUE)                                  , false);
			Eval(false,         new RevisionFilters.Criteria(REV_SELECTED, FALSE)                                 , true);
			Console.Write(nl);

			// test using int
			Console.WriteLine("testing int");
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, EQUAL                         , 5)          , true);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, NOT_EQUAL                     , 5)          , false);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, EQUAL                         , 3)          , false);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, NOT_EQUAL                     , 3)          , true);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, GREATER_THEN                  , 3)          , true);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, GREATER_THEN                  , 5)          , false);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, GREATER_THEN                  , 7)          , false);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, GREATER_THEN_OR_EQUAL         , 3)          , true);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, GREATER_THEN_OR_EQUAL         , 5)          , true);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, GREATER_THEN_OR_EQUAL         , 7)          , false);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, LESS_THEN                     , 3)          , false);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, LESS_THEN                     , 5)          , false);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, LESS_THEN                     , 7)          , true);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, LESS_THEN_OR_EQUAL            , 3)          , false);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, LESS_THEN_OR_EQUAL            , 5)          , true);
			Eval(5,             new RevisionFilters.Criteria(REV_SEQ, LESS_THEN_OR_EQUAL            , 7)          , true);
			Console.Write(nl);

			// test using string
			Console.WriteLine("testing string");
			// basic and extended
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, EQUAL                  , "BCDE")     , true);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, NOT_EQUAL              , "BCDE")     , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, EQUAL                  , "ACDE")     , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, NOT_EQUAL              , "ACDE")     , true);
																			   
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, EQUAL                  , "ACDE")     , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, EQUAL                  , nullString) , false);
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, EQUAL                  , nullString) , false);
																			   
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, NOT_EQUAL              , "ACDE")     , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, NOT_EQUAL              , nullString) , false);
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, NOT_EQUAL              , nullString) , false);
																			   
																		       
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, GREATER_THEN           , "ACDE")     , true);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, GREATER_THEN           , "BCDE")     , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, GREATER_THEN           , "CCDE")     , false);
																		       
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, GREATER_THEN           , "ACDE")     , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, GREATER_THEN           , nullString) , false);
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, GREATER_THEN           , nullString) , false);
																			   
																			   
																		       
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, GREATER_THEN_OR_EQUAL  , "ACDE")     , true);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, GREATER_THEN_OR_EQUAL  , "BCDE")     , true);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, GREATER_THEN_OR_EQUAL  , "CCDE")     , false);
																    
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, GREATER_THEN_OR_EQUAL  , "ACDE")     , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, GREATER_THEN_OR_EQUAL  , nullString) , false);
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, GREATER_THEN_OR_EQUAL  , nullString) , false);

																		    
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, LESS_THEN              , "ACDE")     , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, LESS_THEN              , "BCDE")     , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, LESS_THEN              , "CCDE")     , true);
																		       
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, LESS_THEN              , "ACDE")     , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, LESS_THEN              , nullString) , false);
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, LESS_THEN              , nullString) , false);
																		       
																		       
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, LESS_THEN_OR_EQUAL     , "ACDE")     , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, LESS_THEN_OR_EQUAL     , "BCDE")     , true);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, LESS_THEN_OR_EQUAL     , "CCDE")     , true);
																		       
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, LESS_THEN_OR_EQUAL     , "CCDE")     , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, LESS_THEN_OR_EQUAL     , nullString) , false);
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, LESS_THEN_OR_EQUAL     , nullString) , false);


			// string unary
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, IS_EMPTY)                            , false);
			Eval("",            new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, IS_EMPTY)                            , true);
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, IS_EMPTY)                            , false);

			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, IS_NOT_EMPTY)                        , true);
			Eval("",            new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, IS_NOT_EMPTY)                        , false);
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, IS_NOT_EMPTY)                        , false);

			// string binary
			// starts with
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, STARTS_WITH            , "BC")       , true);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, STARTS_WITH            , "AA")       , false);
			Eval("",            new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, STARTS_WITH            , "AA")       , false);

			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, STARTS_WITH            , "b"         , false), false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, STARTS_WITH            , "b")        , true);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, STARTS_WITH            , "b"         , true), true);
																		       
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, STARTS_WITH            , "AA")       , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, STARTS_WITH            , nullString) , false);
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, STARTS_WITH            , nullString) , false);
			
			// does not start with
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_START_WITH    , "BC")       , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_START_WITH    , "AA")       , true);
			Eval("",            new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_START_WITH    , "AA")       , true);

			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_START_WITH    , "b"         , false), true);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_START_WITH    , "b")        , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_START_WITH    , "b"         , true), false);

			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_START_WITH    , "AA")       , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_START_WITH    , nullString) , false);
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_START_WITH    , nullString) , false);

			// contains
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, CONTAINS               , "CD")       , true);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, CONTAINS               , "AA")       , false);
			Eval("",            new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, CONTAINS               , "AA")       , false);

			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, CONTAINS               , "cd"        , false), false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, CONTAINS               , "cd")       , true);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, CONTAINS               , "cd"        , true), true);
																		       
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, CONTAINS               , "AA")       , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, CONTAINS               , nullString) , false);
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, CONTAINS               , nullString) , false);
																		       
			// does not contain													
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_CONTAIN       , "CD")       , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_CONTAIN       , "AA")       , true);
			Eval("",            new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_CONTAIN       , "AA")       , true);

			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_CONTAIN       , "cd"        , false), true);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_CONTAIN       , "cd")       , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_CONTAIN       , "cd"        , true), false);
																		       
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_CONTAIN       , "AA")       , false);
			Eval("BCDE",        new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_CONTAIN       , nullString) , false);
			Eval(nullString,    new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, DOES_NOT_CONTAIN       , nullString) , false);


			Console.Write(nl);

			// test using visibility
			Console.WriteLine("testing visibility");
			// basic
			Eval(Hidden,        new RevisionFilters.Criteria(REV_ITEM_VISIBLE, EQUAL                , Hidden)     , true);
			Eval(Hidden,        new RevisionFilters.Criteria(REV_ITEM_VISIBLE, EQUAL                , Hidden)     , true);
			Eval(Hidden,        new RevisionFilters.Criteria(REV_ITEM_VISIBLE, NOT_EQUAL            , Hidden)     , false);
			Eval(Hidden,        new RevisionFilters.Criteria(REV_ITEM_VISIBLE, EQUAL                , TagVisible) , false);
			Eval(Hidden,        new RevisionFilters.Criteria(REV_ITEM_VISIBLE, NOT_EQUAL            , TagVisible) , true);
			Console.Write(nl);											       
																		       
			// test using string										       
			Console.WriteLine("testing element id");					       
			// basic													       
			Eval(new ElementId(1), new RevisionFilters.Criteria(REV_TAG_ELEM_ID, EQUAL              , new ElementId(1))  , true);
			Eval(new ElementId(1), new RevisionFilters.Criteria(REV_TAG_ELEM_ID, EQUAL              , new ElementId(2))  , false);
																			        
			Eval(nullElementId   , new RevisionFilters.Criteria(REV_TAG_ELEM_ID, EQUAL              , new ElementId(1))  , false);
			Eval(new ElementId(1), new RevisionFilters.Criteria(REV_TAG_ELEM_ID, EQUAL              , nullElementId)     , false);
			Eval(nullElementId   , new RevisionFilters.Criteria(REV_TAG_ELEM_ID, EQUAL              , nullElementId)     , false);
																			        
			Eval(new ElementId(1), new RevisionFilters.Criteria(REV_TAG_ELEM_ID, NOT_EQUAL          , new ElementId(1))  , false);
			Eval(new ElementId(1), new RevisionFilters.Criteria(REV_TAG_ELEM_ID, NOT_EQUAL          , new ElementId(2))  , true);
																			        
			Eval(nullElementId   , new RevisionFilters.Criteria(REV_TAG_ELEM_ID, NOT_EQUAL          , new ElementId(1))  , false);
			Eval(new ElementId(1), new RevisionFilters.Criteria(REV_TAG_ELEM_ID, NOT_EQUAL          , nullElementId)     , false);
			Eval(nullElementId   , new RevisionFilters.Criteria(REV_TAG_ELEM_ID, NOT_EQUAL          , nullElementId)     , false);
			Console.Write(nl);	

			// test using string										       
			Console.WriteLine("testing rev order code");					       
			// basic
			RevOrderCode rc = new RevOrderCode() { AltId = "1",
				DisciplineCode = ".00", TypeCode = ".00" };
			RevOrderCode rcNull = null;
			RevOrderCode rcEmpty = new RevOrderCode();

			Eval(rc              , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, EQUAL           , "1.00.00")        , true);
			Eval(rc              , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, EQUAL           , "2.00.00")        , false);

			Eval(rc              , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, NOT_EQUAL       , "1.00.00")        , false);
			Eval(rc              , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, NOT_EQUAL       , "2.00.00")        , true);
			Eval(rcEmpty         , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, NOT_EQUAL       , "2.00.00")        , true);
			Eval(rcNull          , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, NOT_EQUAL       , "2.00.00")        , true);

						// string unary
			Eval(rc              , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, IS_EMPTY)                           , false);
			Eval(rcEmpty         , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, IS_EMPTY)                           , true);
			Eval(rcNull          , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, IS_EMPTY)                           , true);

			Eval(rc              , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, IS_NOT_EMPTY)                       , true);
			Eval(rcEmpty         , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, IS_NOT_EMPTY)                       , false);
			Eval(rcNull          , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, IS_NOT_EMPTY)                       , false);
							
			// string binary
			// starts with	
			Eval(rc              , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, STARTS_WITH      , "1.")            , true);
			Eval(rc              , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, STARTS_WITH      , "2.")            , false);
																								        
			Eval(rc              , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, STARTS_WITH      , nullString)      , false);
																								        
			Eval(rcEmpty         , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, STARTS_WITH      , "AA")            , false);
			Eval(rcEmpty         , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, STARTS_WITH      , nullString)      , false);
																								        
			Eval(rcNull          , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, STARTS_WITH      , "AA")            , false);
			Eval(rcNull          , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, STARTS_WITH      , nullString)      , false);

			// using subdataenum
			Eval(rc              , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, EQUAL           , "1", REV_SUB_ALTID)        , true);
			Eval(rc              , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, EQUAL           , "2", REV_SUB_ALTID)        , false);

			Eval(rc              , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, IS_EMPTY, REV_SUB_ALTID)            , false);
			Eval(rcEmpty         , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, IS_EMPTY, REV_SUB_ALTID)            , true);
			Eval(rcNull          , new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, IS_EMPTY, REV_SUB_ALTID)            , true);

			Console.Write(nl);
		}
		
		// select from selected
		// select using the compare filters
		// list the data
		public static void Process6()
		{
			// create a revision filter list
			RevisionFilters rf = TestSelect6A();

			RevOrderMgr ro = new RevOrderMgr();

			// how the header
			ListColumnHeadersInColumnOrder(ro);
			// list the master date
			ListDataInColumnOrder(RevisionDataMgr.IterateRevisionData(), ro, false);

			RevisionDataMgr.ResetPreSelected();

			bool result = RevisionDataMgr.Select(rf);

			if (!result)
			{
				Console.Write(nl);
				Console.WriteLine("no data selected");
			}
			else
			{
				Console.WriteLine("first pass selection");
				// if anything selected
				// how the header
				ListColumnHeadersInColumnOrder(ro);
				// list the data
				ListDataInColumnOrder(RevisionDataMgr.IterateSelected(), ro, false);

				RevisionDataMgr.SetPreSelectedToSelected();

				rf = TestSelect6B();

				result = RevisionDataMgr.Select(rf);

				if (!result)
				{
					Console.Write(nl);
					Console.WriteLine("no data selected");
				}
				else
				{
					Console.WriteLine("second pass selection");
					// if anything selected
					// how the header
					ListColumnHeadersInColumnOrder(ro);
					// list the data
					ListDataInColumnOrder(RevisionDataMgr.IterateSelected(), ro, false);
				}
			}
		}

		// verify cloning of selection data
		// list each
		// this required adjusting the reset method 
		// and is irrelevant otherwise
		public static void Process7()
		{
			RevOrderMgr ro = new RevOrderMgr();

			ListColumnHeadersInColumnOrder(ro);
			ListDataInColumnOrder(RevisionDataMgr.IterateRevisionData(), ro, false, 3);
			Console.WriteLine(nl);

			ListColumnHeadersInColumnOrder(ro);
			ListDataInColumnOrder(RevisionDataMgr.IteratePreSelected(), ro, false, 3);
			Console.WriteLine(nl);

			ListColumnHeadersInColumnOrder(ro);
			ListDataInColumnOrder(RevisionDataMgr.IterateSelected(), ro, false, 3);
			Console.WriteLine(nl);
		}

		// test sorting by various fields and
		// by several fields
		public static void Process8()
		{
			ListSelected();
			Console.Write(nl);

			RevOrderMgr om = new RevOrderMgr();

			om.SortOrder.Start(REV_SORT_ORDER_CODE, REV_SORT_SHEETNUM);

			RevisionDataMgr.SortSelected(om);

			ListSelected();
			Console.Write(nl);

			om.SortOrder.Start(REV_SORT_DELTA_TITLE, REV_SORT_SHEETNUM);

			RevisionDataMgr.SortSelected(om);

			ListSelected();
			Console.Write(nl);
		}

		// test sorting selecting and sorting
		public static void Process9()
		{
			ListSelected();
			Console.Write(nl);

			RevisionDataMgr.ResetPreSelected();

			// create a revision filter list
			RevisionFilters rf = TestSelect9();

			

			if (!RevisionDataMgr.Select(rf))
			{
				Console.WriteLine("no data selected");
			}
			else
			{
				RevOrderMgr om = new RevOrderMgr();

				om.SortOrder.Start(REV_SORT_ORDER_CODE, REV_SORT_SHEETNUM);

				RevisionDataMgr.SortSelected(om);

				ListSelected();
				Console.Write(nl);

				RevisionDataMgr.SetPreSelectedToSelected();

				rf = TestSelect6B();

				if (!RevisionDataMgr.Select(rf))
				{
					Console.WriteLine("no data selected");
				}
				else
				{
					om.SortOrder.Start(REV_SORT_DELTA_TITLE);

					RevisionDataMgr.SortSelected(om);

					ListSelected();
					Console.Write(nl);
				}

			}
		}

		// display revit revision info
		public static void Process10()
		{
			ListRevitRevInfoHeaders();
			ListRevitRevInfo();
		}

		// test oneclick
		public static void Process11()
		{
			bool result = RevisionUtil.OneClick();

			if (!result)
			{
				Console.WriteLine("nothing found");
				return;
			}

			ListSelected();
		}

		// test settings
		public static void Process12()
		{
			RevOrderMgr om = new RevOrderMgr();

			Console.WriteLine("read settings");
			Console.Write(nl);
			Console.WriteLine("project number| " + Settings.Setg.ProjectNumber);
			Console.WriteLine("  project name| " + Settings.Setg.ProjectName);
			Console.WriteLine(" template file| " + Settings.Setg.TemplatePathAndFileName);
			Console.WriteLine("    excel file| " + Settings.Setg.ExcelPathAndFileName);

//			ListRevOrder(Settings.settings.oneClickOrder);
			Console.Write(nl);
			Console.WriteLine("one click column order");
			ListOrder(Settings.Setg.oneClickOrderMgr.ColumnOrder);

// these work	
//			Console.Write(nl);
//			Console.WriteLine("default column order - direct");
//			ListOrder(RevColumnOrder.Default);
//			
//			Console.Write(nl);
//			Console.WriteLine("default column order - via mgr");
//			ListOrder(om.DefaultColumnOrder);

			Console.Write(nl);
			Console.WriteLine("one click sort order");
			ListOrder(Settings.Setg.oneClickOrderMgr.SortOrder);

// these work
//			Console.Write(nl);
//			Console.WriteLine("default sort order - direct");
//			ListOrder(RevSortOrder.Default);
//
//			Console.Write(nl);
//			Console.WriteLine("default sort order - via mgr");
//			ListOrder(om.DefaultSortOrder);

			Console.Write(nl);
			Console.WriteLine("one click critera");
			ListFilters(Settings.Setg.oneClickFilter);

		}
		
		
		// send data to excel
		// using one click settings
		public static void Process50()
		{
			// one-click, get the data
			bool result = RevisionUtil.OneClick();

			// list the selected data
			Console.WriteLine("exporting to excel");
			ListSelected();

			if (!result)
			{
				Console.WriteLine("nothing found");
				return;
			}

			// export the data 
			bool good = RevisionDataMgr.ExportToExcel(Settings.Setg.oneClickOrderMgr);

			if (!good)
			{
				Console.WriteLine("export failed");
				return;
			}
		}
		
		private static void Eval(RevOrderCode a, RevisionFilters.Criteria c, bool expected)
		{
			bool result = Verify(a, c);
			string ax = a?.ToString() ?? "null";

			ListResult(ax, c.TestValue?.AsOrderCode, c, result, expected);
		}
		
		private static void Eval(ElementId a, RevisionFilters.Criteria c, bool expected)
		{
			bool result = Verify(a, c);

			string ax = a?.Value.ToString() ?? "null";
			string cx = c.TestValue?.AsElementId?.ToString() ?? "null";

			ListResult(ax, cx, c, result, expected);
		}
		
		private static void Eval(RevisionVisibility a, RevisionFilters.Criteria c, bool expected)
		{
			bool result = Verify(a, c);
			ListResult(a, c.TestValue?.AsVisibility, c, result, expected);
		}

		private static void Eval(int a, RevisionFilters.Criteria c, bool expected)
		{
			bool result = Verify(a, c);
			ListResult(a, c.TestValue?.AsInt, c, result, expected);
		}

		private static void Eval(bool a, RevisionFilters.Criteria c, bool expected)
		{
			bool result = Verify(a, c);
			ListResult(a, c.CompareOpr == TRUE, c, result, expected);
		}

		private static void Eval(string a, RevisionFilters.Criteria c, bool expected)
		{
			bool result = Verify(a, c);

			a = a ?? "null";
			string b = c.TestValue?.AsString ?? "null";

			if (a == "") a = "empty";
			if (b == "") b = "empty";

			ListResult(a, b, c, result, expected);
		}

		private static void ListResult<T>(T a, T b, RevisionFilters.Criteria c, bool result, bool expected)
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

			RevisionFilters.Criteria c;

			// any - string
			c = new RevisionFilters.Criteria(REV_SORT_ITEM_BASIS, ANY);
			f.Add(c);

			// bool - bool
			c = new RevisionFilters.Criteria(REV_SELECTED, TRUE);
			f.Add(c);

			// basic - elementid
			c = new RevisionFilters.Criteria(REV_TAG_ELEM_ID, EQUAL, new ElementId(10101));
			f.Add(c);

			// basic - visibility
			c = new RevisionFilters.Criteria(REV_ITEM_VISIBLE, EQUAL, Hidden);
			f.Add(c);
			
			// extended - int
			c = new RevisionFilters.Criteria(REV_SEQ, LESS_THEN, 1);
			f.Add(c);
			
			// string unary
			c = new RevisionFilters.Criteria(REV_SORT_ITEM_REVID, IS_EMPTY);
			f.Add(c);
			
			// string binary
			c = new RevisionFilters.Criteria(REV_SORT_SHEETNUM, STARTS_WITH, "c", true);
			f.Add(c);
			
			// string unary - RevOrderCode
			c = new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, IS_EMPTY);
			f.Add(c);

			// string binary - RevOrderCode
			c = new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, STARTS_WITH, "a", true);
			f.Add(c);

			return f;
		}
		
		// create a example filter list
		// included are failures for testing
		private static RevisionFilters TestSelect4()
		{
			RevisionFilters f = new RevisionFilters();

			RevisionFilters.Criteria c;

			// basis
			c = new RevisionFilters.Criteria(REV_SORT_ITEM_BASIS, EQUAL, "pcc");
			f.Add(c);
			c = new RevisionFilters.Criteria(REV_SORT_ITEM_BASIS, EQUAL, "rfi");
			f.Add(c);

			// bool - bool
//			c = new Criteria(REV_SELECTED, FALSE);
//			f.Add(c);

			// basic - visibility
			c = new RevisionFilters.Criteria(REV_ITEM_VISIBLE, EQUAL, TagVisible);
			f.Add(c);
			
//			// string unary
//			c = new Criteria(REV_SORT_ITEM_REVID, EQUAL, "1");
//			f.Add(c);
			
			// string binary
			c = new RevisionFilters.Criteria(REV_SORT_SHEETNUM, STARTS_WITH, "c");
			f.Add(c);
			c = new RevisionFilters.Criteria(REV_SORT_SHEETNUM, STARTS_WITH, "a");
			f.Add(c);

			
//			// string unary - RevOrderCode
//			c = new Criteria(REV_SORT_ORDER_CODE, IS_EMPTY);
//			f.Add(c);

			// string binary - RevOrderCode
			c = new RevisionFilters.Criteria(REV_SORT_ORDER_CODE, STARTS_WITH, "1", true);
			f.Add(c);

			return f;
		}

		// create a example filter list
		// included are failures for testing
		private static RevisionFilters TestSelect9()
		{
			RevisionFilters f = new RevisionFilters();

			RevisionFilters.Criteria c;

			// basis
			c = new RevisionFilters.Criteria(REV_SORT_ITEM_BASIS, EQUAL, "pcc");
			f.Add(c);
			c = new RevisionFilters.Criteria(REV_SORT_ITEM_BASIS, EQUAL, "rfi");
			f.Add(c);

			// string binary
			c = new RevisionFilters.Criteria(REV_SORT_SHEETNUM, STARTS_WITH, "c");
			f.Add(c);
			c = new RevisionFilters.Criteria(REV_SORT_SHEETNUM, STARTS_WITH, "a");
			f.Add(c);

			return f;
		}	
		
		// create a filter list
		// included are failures for testing
		private static RevisionFilters TestSelect6A()
		{
			RevisionFilters f = new RevisionFilters();

			f.Add(new RevisionFilters.Criteria(REV_SORT_DELTA_TITLE, CONTAINS, "007"));
			f.Add(new RevisionFilters.Criteria(REV_SORT_DELTA_TITLE, CONTAINS, "013"));

			return f;
		}
		
		// create a filter list to sub filter
		// included are failures for testing
		private static RevisionFilters TestSelect6B()
		{
			RevisionFilters f = new RevisionFilters();

			f.Add(new RevisionFilters.Criteria(REV_SORT_ITEM_BASIS, EQUAL, "pcc"));

			return f;
		}

		// adjust the column order values
		private static RevOrderMgr ModifyColumnOrder()
		{
			RevOrderMgr om = new RevOrderMgr();

			om.ColumnOrder.Start(REV_SELECTED);								// 0
			om.ColumnOrder.Add(REV_SEQ, REV_SORT_SHEETNUM, REV_ITEM_DATE);	// 1, 2
			om.ColumnOrder.Add(REV_SORT_DELTA_TITLE);						// 3
			om.ColumnOrder.Add(REV_SORT_ITEM_BASIS);						// 4
			om.ColumnOrder.Add(REV_SORT_ITEM_DESC);							// 5

			return om;

//			// change the column order for testing
//			// set column to -1 to mean - don't include
//			SetColumns(-1);
//
//			REV_SELECTED.Column = 1;
//			REV_SEQ.Column      = 0;
//
//			REV_SORT_SHEETNUM.Column    = 4;
//			REV_ITEM_DATE.Column        = 6;
//			REV_SORT_DELTA_TITLE.Column = 3;
//			REV_SORT_ITEM_BASIS.Column  = 2;
//			REV_SORT_ITEM_DESC.Column   = 10;
//
//			SetColumnOrder();
		}

		// list the pre-select data and header 
		// in column order
		private static void ListPreSelected(int qty = 0)
		{
			RevOrderMgr om = new RevOrderMgr();

			Console.WriteLine("pre-selected data");
			// how the header
			ListColumnHeadersInColumnOrder(om);
			// list the data
			ListDataInColumnOrder(RevisionDataMgr.IteratePreSelected(), om, false, qty);
		}

		// list the pre-select data and header 
		// in column order
		private static void ListSelected(int qty = 0)
		{
			RevOrderMgr om = new RevOrderMgr();

			Console.WriteLine("selected data");
			// how the header
			ListColumnHeadersInColumnOrder(om);
			// list the data
			ListDataInColumnOrder(RevisionDataMgr.IterateSelected(), om, false, qty);
		}
		
		private static void ListFilters(RevisionFilters f)
		{
			foreach (KeyValuePair<DataItems.FilterEnum, RevisionFilters.Filters> kvpOutter in f)
			{
				
				Console.WriteLine("      op name| " + kvpOutter.Key.Name + " (" + kvpOutter.Key.FilterIdx + ")");

				foreach (KeyValuePair<int, RevisionFilters.Criteria> kvpInner in kvpOutter.Value)
				{
					ListCriteria(kvpInner.Value);

//					
//					Console.WriteLine("      op type| " + kvpInner.Value.CompareOpr.Type.ToString());
//					Console.WriteLine("           op| " + kvpInner.Value.CompareOpr.GetType().Name);
//
//					switch (kvpOutter.Key.Type)
//					{
//					case EDataType.BOOL:
//						{
//							Console.WriteLine("test val bool| " + 
//								(kvpInner.Value.TestValue?.AsBool.ToString() ?? "null"));
//							break;
//						}
//					case EDataType.ELEMENTID:
//						{
//							Console.WriteLine("test val Eid | " +
//								(kvpInner.Value.TestValue?.AsElementId.ToString() ?? "null"));
//							break;
//						}
//					case EDataType.INT:
//						{
//							Console.WriteLine("test val int | " + 
//								(kvpInner.Value.TestValue?.AsInt.ToString() ?? "null"));
//							break;
//						}
//					case EDataType.ORDER:
//					case EDataType.STRING:
//						{
//							if (kvpInner.Value.CompareOpr is ICompStringUnary)
//							{
//								Console.WriteLine("test val str | unary");
//							}
//							else
//							{
//								Console.WriteLine("test val str | binary| " +
//									(kvpInner.Value.TestValue?.AsString ?? "null"));
//							}
//
//							break;
//						}
//					case EDataType.VISIBILITY:
//						{
//							Console.WriteLine("test val vis | " + 
//								(kvpInner.Value.TestValue?.AsVisibility.ToString() ?? "null"));
//							break;
//						}
//					}
//					Console.Write(nl);
				}
				Console.Write(nl);
			}
		}

		public static void ListColumnHeadersInColumnOrder(RevOrderMgr om, bool subData = false)
		{
			ListColHeaders(0, om, subData);
			ListColHeaders(1, om, subData);
		}

		private static void ListColHeaders(int row, RevOrderMgr om, bool subData)
		{
			if (row == 0)
			{
				Console.Write("|     |");
			}
			else
			{
				Console.Write("| -#- |");
			}

			foreach (DataItems.DataEnum item in om.ColumnOrder.Iterate())
			{
				ListARowTitle(item, row, subData);
			}

			Console.Write(nl);
		}

		private static void ListARowTitle(DataItems.SubDataEnum item, int row, bool subData)
		{
			string title;
			RevisionDataDisplay dd = item.Display;

			char spacer = item.Title[row].Equals("") ? ' ' : '-';

			title = FormatForColumn(item.Title[row], dd, 
				spacer, spacer, RevisionMetaData.Justification.CENTER);

			Console.Write(title);

			Console.Write("|"); // space between columns

			if (subData)
			{
				if (item.SubDataList != null)
				{
					foreach (DataItems.SubDataEnum subDataEnum in item.SubDataList)
					{
						ListARowTitle(subDataEnum, row, false);
					}
				}
			}
		}

		// list based on raw read data order
		private static void ListDataInColumnOrder(
			IEnumerable<RevisionDataFields> iEnumerable,
			RevOrderMgr om, bool header, int qty = 0, 
			bool subData = false)
		{
			int i = 0;
			
			foreach (RevisionDataFields kvp in iEnumerable)
			{
				if (header)
				{
					Console.Write(nl);
					Console.WriteLine($"{"data item",24} | {i,-4:D}");
				}

				ListDataItemInColumnOrder(i, kvp, om, subData);

				if (++i == qty) break;
			}
			Console.WriteLine(nl);
		}

		public static void ListDataItemInColumnOrder(int idx, 
			RevisionDataFields items, RevOrderMgr om,
			bool subData)
		{
			Console.Write($"| {idx,3:D} |");

			StringBuilder col = new StringBuilder("col| ");
			StringBuilder key = new StringBuilder("key| ");

			foreach (DataItems.DataEnum d in om.ColumnOrder.Iterate())
			{
				ListAnItem(items[d.DataIdx], d, subData);

				if (subData)
				{
					if (d.SubDataList != null)
					{
						for (int i = 0; i < d.SubDataList.Length; i++)
						{
							ListAnItem(((SubData) items[d.DataIdx])[i], 
								d.SubDataList[i], false);
						}
					}
				}
			}
			Console.Write(nl);

		}

		public static void ListAnItem(dynamic data, DataItems.DescEnum dataEnum, 
			bool subData)
		{
			RevisionDataDisplay dd = dataEnum.Display;

			string dataFormatted;

			dataFormatted = string.Format(dd.FormatString,
					data?? "");

			dataFormatted = FormatForColumn(dataFormatted, dd);

				Console.Write(dataFormatted);
				Console.Write("|");
		}

		// modify then list on column property order
		private static void ListMetaData()
		{
			string fmtx = String.Format(
				"{0,-3:#0}| name| {1,-26}| type| {2,-15}| "
				+ "title| {3,-22}| column| {4,-4:##0}| "
				+ "source| {5,-26}" ,
				1, "name", "type", "title", 1, "source" 
			);
			string fmty = String.Format(
				"   | display| width| "
				+ "min| {0,-3:D} " 
				+ "max title| {1,-3:D} " 
				+ "col| {2,-3:D} " 
				+ "key| {3,-3:D} " 
				+ "margin| "
				+ "L| {4,-3:D} " 
				+ "R| {5,-3:D} " 
				+ "fmt str {6,-10} " 
				+ "justify| "
				+ "col| {7,-10} " 
				+ "key| {8,-10} " ,
			//  0  1  2  3  4  5   6       7       8
				1, 1, 1, 1, 1, 1, "asdf", "asdf", "asdf"
			);

			string fmtRoot =
				"{0,-3:#0}| root| name| {1,-31}| type| {2,-15}| "
				+ "title| {3,-22}";
			
			string fmtSubField =
				"{0,-3:#0}| item| name| {1,-31}| type| {2,-15}| "
				+ "title| {3,-22}";

			string fmtData =
				"{0,-3:#0}| item| name| {1,-31}| type| {2,-15}| "
				+ "title| {3,-22}| "
//				+ "column| {4,-4:##0}| "
				+ "source| {4,-26}";

			string fmtDesc =
				"{0,-3:#0}| item| name| {1,-31}| type| {2,-15}| "
				+ "title| {3,-22}| "
//				+ "column| {4,-4:##0} "
				+ "| disp/fmt| {4,-8}";

			string fmtDisplay =
			//   +->|           |
				"+->|    display|  "
				+ " margin| "
				+ "L| {0,-3:D} "
				+ "R| {1,-3:D} "
				+ "fmt str| {2,-10}";

			string fmtDisplayCol =
			//   +->|           |
				"+->|  col width| {0,-3:D} "
				+ "justify| "
				+ "col| {1,-12} | "
				+ "sample| col| >{2}<";

			string fmtDisplayKey =
			//   +->|           |
				"+->|  key width| {0,-3:D} "
				+ "justify| "
				+ "key| {1,-12} | "
				+ "sample| key| >{2}<";
			
			string fmtDisplayReadWidth =
			//   +->|           |
				"+->| read width| {0,-3:D} ";
			
			string fmtDisplayTtlStackWidth =
			//   +->|           |
				"+->|  Ttl stack| {0,-3:D} ";
			
			string fmtDisplayTtlFlatWidth =
			//   +->|           |
				"+->|   Ttl flat| {0,-3:D} ";

			int i = 0;

			Console.Write(nl);

			foreach (DataItems.RootEnum rx in RootList)
			{
				if (rx is DataItems.DataEnum)
				{
					DataItems.DataEnum ix = (DataItems.DataEnum) rx;
					Console.WriteLine(fmtData, i, ix.Name, ix.Type.ToString(), 
						ix.FullTitle, ix.Source.ToString());

					RevisionDataDisplay d = ix.Display;

					string sampleColumn = RevisionFormat.FormatForColumn(
						"now is the time for all good men", d);
					string sampleKey = RevisionFormat.FormatForColumn(
						"key", d);

					Console.WriteLine(fmtDisplay, 
						d.MarginLeft, d.MarginRight, d.FormatString);

					Console.WriteLine(fmtDisplayTtlFlatWidth,d.TitleWidthFlat);
					Console.WriteLine(fmtDisplayTtlStackWidth,d.TitleWidthStacked);
					Console.WriteLine(fmtDisplayReadWidth,d.DataWidth);

					Console.WriteLine(fmtDisplayCol,d.ColWidth, d.JustifyColumn, sampleColumn);

					Console.WriteLine(fmtDisplayKey, d.KeyWidth, d.JustifyKey, sampleKey);
					
					if (ix.SubDataList != null)
					{
						Console.Write("+->| sub fields  ");

						for (var j = 0; j < ix.SubDataList.Length; j++)
						{
							Console.Write("| " + j + " | " + ix.SubDataList[j].Name);
						}
						Console.Write(nl);
					}
					
				}
				else if (rx is DataItems.DescEnum)
				{
					DataItems.DescEnum ix = (DataItems.DescEnum) rx;
					Console.WriteLine(fmtDesc, i, ix.Name, ix.Type.ToString(), 
						ix.FullTitle, ix.Display.FormatString);

					RevisionDataDisplay d = ix.Display;

					Console.WriteLine(fmtDisplayTtlFlatWidth,d.TitleWidthFlat);
					Console.WriteLine(fmtDisplayTtlStackWidth,d.TitleWidthStacked);
					Console.WriteLine(fmtDisplayReadWidth,d.DataWidth);
					Console.WriteLine(fmtDisplayCol,d.ColWidth, d.JustifyColumn, "");
					Console.WriteLine(fmtDisplayKey, d.KeyWidth, d.JustifyKey, "");
				}
				else
				{
					Console.WriteLine(fmtRoot, i, rx.Name, rx.Type.ToString(), rx.FullTitle);
				}

				i++;
				Console.Write(nl);
			}
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

			foreach (DataItems.RootEnum rx in RootList)
			{
				Console.WriteLine(fmt2, i++, rx.Name, -1, -1, 0);
			}
						
			i = 0;

			Console.WriteLine(nl + "desc list");
			foreach (DataItems.DescEnum dx in DescList)
			{
				Console.WriteLine(fmt2, i++, dx.Name, dx.DescItemIdx, -1, -1);
			}
			
			i = 0;

			Console.WriteLine(nl + "data list");
			foreach (DataItems.DataEnum dx in DataList)
			{
				Console.Write(fmt2, i++, dx.Name, dx.DescItemIdx, dx.DataIdx, -1);

				if (dx.SubDataList != null)
				{
					int j = 0;

					foreach (DataItems.SubDataEnum sub in dx.SubDataList)
					{
						Console.Write("| " + j++ + "| (" + sub.Name + ")");
					}
				}

				Console.Write(nl);
			}

			i = 0;

			Console.WriteLine(nl + "filter list");
			foreach (DataItems.FilterEnum fx in FilterList)
			{
				Console.WriteLine(fmt2, i++, fx.Name, fx.DescItemIdx, fx.DataIdx, fx.FilterIdx);
			}
			
			i = 0;
			
			RevColumnOrder co = new RevColumnOrder();

			Console.WriteLine(nl + "column list");
			foreach (DataItems.DataEnum dx in co.Iterate()) 
			{
				Console.WriteLine(fmt2, i++, dx.Name, dx.DescItemIdx, dx.DataIdx, -1);
			}
		}

		private static void ListRevitRevInfoHeaders()
		{
			Console.WriteLine("Revit Revision Info");
			Console.WriteLine("max revision| " + RevitRevisions.MaxRevision);


			// top row
			Console.Write($"|{"seq"        ,-5}");
			Console.Write($"|{"rev"        ,-5}");
			Console.Write($"|{"alt"        ,-5}");
			Console.Write($"|{""           ,-8}");
			Console.Write($"|{"block"      ,-25}");
			Console.Write($"|{"delta"      ,-25}");
			Console.Write($"|{"revision"   ,-12}");
			Console.Write($"|{"revision"   ,-16}");
			Console.Write($"|{""           ,-22}");
			Console.WriteLine("|");

			// bottom row
			Console.Write($"|{"num"       ,-5}");
			Console.Write($"|{"id"        ,-5}");
			Console.Write($"|{"id"        ,-5}");
			Console.Write($"|{"issued"    ,-8}");
			Console.Write($"|{"title"     ,-25}");
			Console.Write($"|{"title"     ,-25}");
			Console.Write($"|{"date"      ,-12}");
			Console.Write($"|{"type"      ,-16}");
			Console.Write($"|{"visibility",-22}");
			Console.WriteLine("|");
		}

		private static void ListRevitRevInfo()
		{
			foreach (string[] ri in RevitRevisions.RevitRevisionInfo)
			{
				Console.Write($"|{ri[0]   ,-5}");
				Console.Write($"|{ri[1]   ,-5}");
				Console.Write($"|{ri[2]   ,-5}");
				Console.Write($"|{ri[3]   ,-8}");
				Console.Write($"|{ri[4]   ,-25}");
				Console.Write($"|{ri[5]   ,-25}");
				Console.Write($"|{ri[6]   ,-12}");
				Console.Write($"|{ri[7]   ,-16}");
				Console.Write($"|{ri[8]   ,-22}");
				Console.WriteLine("|");
			}
		}

		private static void ListRevOrder(RevOrderMgr om)
		{
			Console.Write(nl);
			Console.WriteLine(" *** order ***");
			Console.WriteLine("    column count| " + om.ColumnOrder.Count);
			Console.WriteLine("      sort count| " + om.SortOrder.Count);
			Console.WriteLine("def column count| " + RevColumnOrder.Default.Count);
			Console.WriteLine("  def sort count| " + RevSortOrder.Default.Count);
			Console.Write(nl);

			Console.WriteLine(" *** column order ***");
			if (om.ColumnOrder.Count == 0)
			{
				Console.WriteLine("column order not set - listing default");
				Console.Write(nl);

				ListOrder(om.DefaultColumnOrder);
			}
			else
			{
				Console.WriteLine("column order found");
				Console.Write(nl);

				ListOrder(om.ColumnOrder);
			}


			Console.Write(nl);
			Console.WriteLine(" *** sort order ***");
			if (om.SortOrder.Count == 0)
			{
				Console.WriteLine("sort order not set - listing default");
				Console.Write(nl);

				ListOrder(om.DefaultSortOrder);
			} 
			else 
			{
				Console.WriteLine("sort order found");
				Console.Write(nl);

				ListOrder(om.SortOrder);
			}

			Console.Write(nl);
		}

		private static void ListOrder(IRevOrder revOrder)
		{
			Console.WriteLine("        type | " + revOrder.GetType().Name);
			Console.WriteLine("       count | " + revOrder.Count);

			if (revOrder.Count == 0)
			{
				Console.WriteLine("*** no order items ***");
				Console.Write(nl);
				return;
			}

			foreach (DataItems.DataEnum column in revOrder.itemize())
			{
				Console.WriteLine("      column | " + column.FullTitle);
			}
		}

		private static void ListCriteria(RevisionFilters.Criteria c)
		{
			const string isNull = "** is null **"; 

			Console.WriteLine("      op type| " + c.CompareOpr.Type);
			Console.WriteLine("           op| " + c.CompareOpr.GetType().Name);

			Console.WriteLine("     test val| " + (c.TestValue?.Value ?? isNull));
			Console.WriteLine("  ignore case| " + c.IgnoreCase);
			Console.WriteLine("  filter name| " + (c.FilterEnum?.Name ?? isNull));
			Console.WriteLine(" filter title| " + (c.FilterEnum?.FullTitle ?? isNull));
			Console.WriteLine("subdata title| " + (c.SubDataEnum?.FullTitle ?? isNull));
			Console.Write(nl);
		}

	}
}
