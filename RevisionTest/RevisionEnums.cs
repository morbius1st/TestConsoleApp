using System;
using System.Reflection;
using System.Collections.Generic;
//using static TestConsoleApp.DataItems.ColumnHelpers;

using static RevisionTest.DataItems.EDataFields;

namespace RevisionTest
{
	public enum EDataType
	{
		BOOL,
		INT,
		STRING,
		ELEMENTID,
		VISIBILITY,
		ORDER
	}

	public enum EFieldSource
	{
		REV_SOURCE_DERIVED,
		REV_SOURCE_TAG,
		REV_SOURCE_CLOUD
	}

	public static class DataItems
	{
		public static int PropertyCount { get; private set; }

		public static List<RootEnum>   RootList ;
		public static List<DescEnum>   DescList;
		public static List<DataEnum>   DataList ;
		public static List<FilterEnum> FilterList ;

//		public static List<DataEnum> ColumnList;

		public static class EDataFields
		{
			// *** add no other properties here ***
			// create the data enums here - the order below defines the
			// default column order
			//
            public static DataEnum              REV_MGMT_RECORD_ID { get; } =      // => n/a	// (derived) the sequence number the data was 	   
                new DataEnum();                                                    //	        // read from the revit file
                                                                                   //																
            public static CompareBoolEnum       REV_SELECTED { get; } =            // =>  0	// (derived) flag that this data item has 
                new CompareBoolEnum();                                             //			// been selected
                                                                                   //																
            public static CompareIntEnum        REV_SEQ { get; } =                 // =>  1	// (from sequence) revision sequence number
                new CompareIntEnum();                                              //			// for ordering only
                                                                                   // ***
            public static CompareStrSortable    REV_SORT_ITEM_REVID { get; } =     // =>  2	// (from revision) revision id number or 		   
                new CompareStrSortable();                                          //			// alphanumeric									
                                                                                   // ***
            public static CompareOrderEnum      REV_SORT_ORDER_CODE { get; } =     // =>  3	// (derived) combnation of AltId, 
                new CompareOrderEnum();                                            //			// Type_code, Discipline code (a structure)
                                                                                   // ***
            public static CompareStrSortable    REV_SORT_DELTA_TITLE { get; } =    // =>  4	// (from issued to) (part of item key) 		   
                new CompareStrSortable();                                          //			// simple name for this issuance (goes below the delta)			
                                                                                   // ***
            public static CompareStrSortable    REV_SORT_SHEETNUM { get; } =       // =>  5	// (calculated) (part of item key) sheet 		   
                new CompareStrSortable();                                          //			// number of this tag											
                                                                                   //																			   
            public static CompareVisEnum        REV_ITEM_VISIBLE { get; } =        // =>  6	// (from visibility)(calculated)) item 
                new CompareVisEnum();                                              //			// visibility													
                                                                                   //																			   
            public static CompareStrEnum        REV_ITEM_BLOCK_TITLE { get; } =    // =>  7	// (from revision description) title for 		   
                new CompareStrEnum();                                              //			// this issuance												
                                                                                   //																			   
            public static DataEnum              REV_ITEM_DATE { get; } =           // =>  8	// (from revision date) the date assigned 
                new DataEnum();                                                    //			// to the revision												
                                                                                   // ***
            public static CompareStrSortable    REV_SORT_ITEM_BASIS { get; } =     // =>  9	// (from comment) the reason for the 			   
                new CompareStrSortable();                                          //			// revision														
                                                                                   // ***
            public static CompareStrSortable    REV_SORT_ITEM_DESC { get; } =      // => 10	// (from mark) the description of the 
                new CompareStrSortable();                                          //			// revision														
                                                                                   //																		   
            public static CompareEIdEnum        REV_TAG_ELEM_ID { get; } =         // => 11	// the element id of the tag for this 
                new CompareEIdEnum();                                              //			// data item													
                                                                                   //																		   
            public static CompareEIdEnum        REV_CLOUD_ELEM_ID { get; } =       // => 12	// the element id of of the cloud for 
                new CompareEIdEnum();                                              //			// this data item
                                                                                   //																
            public static MgmtEnum              REV_MGMT_COLUMN { get; } =         // => n/a	// (derived) the title for the column field the	   
                new MgmtEnum();                                                    //			// column is only stored with the data description				
                                                                                   //																			   
            public static SubRevOrderCodeEnum   REV_SUB_ALTID { get; } =           // =>sub(0) // (from issued by) (part of REV_SORT_ORDER_CODE) 
                new SubRevOrderCodeEnum();                                         //			// a cross-reference to the REV_REVID associated with this item
                                                                                   //																
            public static SubRevOrderCodeEnum   REV_SUB_TYPE_CODE { get; } =       // =>sub(1) // (derived) (part of REV_SORT_ORDER_CODE) code based  
                new SubRevOrderCodeEnum();                                         //			// on the document type
                                                                                   //
            public static SubRevOrderCodeEnum   REV_SUB_DISCIPLINE_CODE { get; } = // =>sub(2)	// (derived) (part of REV_SORT_ORDER_CODE) code based
                new SubRevOrderCodeEnum();                                         //			// on the discipline
                                                                                   //
            public static DataEnum              REV_SORT_KEY { get; } =            //			// (derived) the key used to sort the date
                new DataEnum();                                                    //
		}

		static DataItems()
		{
			PropertyInfo[] propertyInfos = typeof(EDataFields).GetProperties();

			PropertyCount = propertyInfos.Length;

			RootList   = new List<RootEnum>(PropertyCount);
			DescList   = new List<DescEnum>(PropertyCount);
			DataList   = new List<DataEnum>(PropertyCount);
			FilterList = new List<FilterEnum>(PropertyCount);

//			ResetColumnList();

			// use reflection to access all of the data items
			// update the data item's name with its variable name
			// categorize the properties into arrays for easy access
			foreach (PropertyInfo p in propertyInfos)
			{
				RootEnum r = (RootEnum) p.GetValue(null);

				r.Name = p.Name;

				RootList.Add(r);

				if (r is DescEnum ds)
				{
					DescList.Insert(ds.DescItemIdx, ds);
//
//					ds.Column = -1;
				}

				if (r is DataEnum)
				{
					DataEnum de = (DataEnum) r;

					DataList.Insert(de.DataIdx, de);
				}

				if (r is FilterEnum)
				{
					FilterEnum f = (FilterEnum) r;

					FilterList.Insert(f.FilterIdx, f);
				}
			}

			REV_SORT_ORDER_CODE.SubDataList = new []
			{
				REV_SUB_ALTID,
				REV_SUB_TYPE_CODE,
				REV_SUB_DISCIPLINE_CODE
			};

			REV_SUB_ALTID.SubDataIdx = 0;
			REV_SUB_TYPE_CODE.SubDataIdx = 1;
			REV_SUB_DISCIPLINE_CODE.SubDataIdx = 2;


			// update the column list and set the column
			// for each field based on the above order
//			ResetColumnOrder();

			// setup the revision meta data
			RevisionMetaData.Init();

//			RevColumns<DataEnum> co = RevColumnOrder.Default;
//
//			RevColumnOrder.Default.Start(REV_SORT_ITEM_REVID, 
//				REV_SORT_ORDER_CODE, REV_SORT_DELTA_TITLE, REV_SORT_SHEETNUM,
//				REV_ITEM_VISIBLE, REV_ITEM_BLOCK_TITLE, REV_ITEM_DATE,
//				REV_SORT_ITEM_BASIS, REV_SORT_ITEM_DESC);
//
//			RevSortOrder.Default.Start(REV_SORT_ORDER_CODE, 
//				REV_SORT_DELTA_TITLE, REV_SORT_SHEETNUM);
		}


#region + Primary

		/* RootEnum
		   |  +------> string:				Name
		   |  +------> EDataType:			Type
		   |  +------> string[]:			Title
		   |
		   +>DescEnum
		   | |  +----> int:					DescItemIdx	
		   | |  +----> int:					Column
		   | |  +----> RevisionDataDisplay:			Display
		   | |  +----> (RootEnum):			(Name)
		   | |  +----> (RootEnum):			(Type)
		   | |  +----> (RootEnum):			(Title)
		   | |
		   | +>SubDataEnum
		   |   |  +-> int:					SubDataIdx
		   |   |  +-> DataEnum[]:			SubDataList
		   |   |
		   |   +>DataEnum
		   |     |  +-> int:				DataIdx
		   |     |  +-> EFieldSource:		Source
		   |     |  +-> (DescEnum):			(DescItemIdx)
		   |     |  +-> (DescEnum):			(Column)
		   |     |  +-> (DescEnum):			(Display)
		   |     |  +-> (RootEnum):			(Name)
		   |     |  +-> (RootEnum):			(Type)
		   |     |  +-> (RootEnum):			(Title)
		   |     |  +-> (SubDataEnum):		(SubDataIdx)
		   |     |  +-> (SubDataEnum):		(SubDataList)
		   |     |
		   |     +> FilterEnum
		   |          +-> int:				FilterIdx
		   |          +-> (RootEnum):		(Name)
		   |          +-> (RootEnum):		(Type)
		   |          +-> (RootEnum):		(Title)
		   |          +-> (DescEnum):		(DescItemIdx)
		   |          +-> (DescEnum):		(Column)
		   |          +-> (DescEnum):		(Display)
		   |          +-> (SubDataEnum):	(SubDataIdx)
		   |          +-> (SubDataEnum):	(SubDataList)
		   |          +-> (DataEnum):		(DataIdx)
		   |          +-> (DataEnum):		(Source)
		   |
		   +->MgmtEnum
		         +-> [none]
		         +-> (RootEnum):		(Name)
		         +-> (RootEnum):		(Type)
		         +-> (RootEnum):		(Title)

 		*/

		// root class
		// include properties that apply
		// to every sub-class
		public class RootEnum
		{
			public string    Name { get; set; } // the variable's name - do I need this?
			public EDataType Type { get; set; } // the data type

			public string[] Title { get; set; }
				= new string[2]; // the title for this data item (column header)

			public string FullTitle
			{
				get
				{
					string fullTitle = "";

					for (int i = Title.Length - 1; i >= 0; i--)
					{
						fullTitle = Title[i] + fullTitle;

						if (i == 0 || string.IsNullOrWhiteSpace(Title[i - 1])) break;

						fullTitle = " " + fullTitle;
					}

					return fullTitle;
				}
			}

			public RootEnum()
			{
				Type = EDataType.STRING;
			}
		}

		// description class
		// only items that need a description and
		// may be displayed
		public class DescEnum : RootEnum
		{
			private static int a = 0;

			public DescEnum()
			{
				DescItemIdx = a++;
			}

			public DescEnum(int d)
			{
				DescItemIdx = d;
			}

			public int DescItemIdx { get; }

//			public int Column { get; set; } // the column to present this data item

			public RevisionDataDisplay Display { get; set; } = new RevisionDataDisplay(); // data display
			// information - font, format string, etc.
		}

		// SubField class
		// defines sub data items
		// that is, data items that are
		// a class or struct
		public class SubDataEnum : DescEnum
		{
			public SubDataEnum() { }

			public SubDataEnum(int d) : base(d)
			{
				SubDataIdx  = -1;
				SubDataList = null;
			}

			public SubDataEnum[] SubDataList { get; set; }
			public int SubDataIdx  { get; set; }
		}


		// item class
		// these are the data items read from the
		// revision clouds & tags
		public class DataEnum : SubDataEnum
		{
			private static int b = 0;
			public int DataIdx { get; }
			// where the data came from (to allow changes)
			public EFieldSource Source { get; set; }

			public DataEnum()
			{
				DataIdx = b++;
			}

			public DataEnum(int d, int i) : base(d)
			{
				DataIdx = i;
			}
		}

		// management items
		public class MgmtEnum : RootEnum { }

		// filter class
		// these are sub-items that can be
		// filtered for (that is, selected)
		// this is the general class - refer
		// to the specific sub-compare class
		// for the specific type of filter
		public class FilterEnum : DataEnum
		{
			private static int c = 0;

			public FilterEnum() { }

			public FilterEnum(EDataType type)
			{
				Type      = type;
				FilterIdx = c++;
			}

			public int FilterIdx { get; }
		}

		public interface ISortable { }

		public interface ISubRevOrderCode { }

		public class SubRevOrderCodeEnum : SubDataEnum, ISubRevOrderCode { }

		// compare classes
		// these are sub-filter items that can
		// be selected and can be compared
		// using a specific type of comparison
		public class CompareStrEnum : FilterEnum
		{ public CompareStrEnum() : base(EDataType.STRING) {} }

		public class CompareStrSortable : FilterEnum, ISortable
		{ public CompareStrSortable() : base(EDataType.STRING) {} }

		public class CompareVisEnum : FilterEnum
		{ public CompareVisEnum() : base(EDataType.VISIBILITY) {} }

		public class CompareEIdEnum : FilterEnum
		{ public CompareEIdEnum() : base(EDataType.ELEMENTID) {} }

		public class CompareBoolEnum : FilterEnum
		{public CompareBoolEnum() : base(EDataType.BOOL) {} }

		public class CompareIntEnum : FilterEnum
		{public CompareIntEnum() : base(EDataType.INT) {} }

		public class CompareOrderEnum : FilterEnum, ISortable
		{public CompareOrderEnum() : base(EDataType.ORDER) {} }

//		
//		private static CompareStrSort cSort;
//
//		private static CompareStrEnum[] c1 =
//		{
//			REV_MGMT_RECORD_ID, REV_SELECTED, REV_SEQ, REV_SORT_ITEM_REVID,
//			REV_SORT_ORDER_CODE, REV_SORT_DELTA_TITLE, REV_SORT_SHEETNUM,
//			REV_ITEM_VISIBLE, REV_ITEM_BLOCK_TITLE, REV_ITEM_DATE,
//			REV_SORT_ITEM_BASIS, REV_SORT_ITEM_DESC, REV_TAG_ELEM_ID, REV_CLOUD_ELEM_ID,
//			cSort,
//		};
//
//		private static CompareStrSort[] c2 =
//		{
//			REV_MGMT_RECORD_ID, REV_SELECTED, REV_SEQ, REV_SORT_ITEM_REVID,
//			REV_SORT_ORDER_CODE, REV_SORT_DELTA_TITLE, REV_SORT_SHEETNUM,
//			REV_ITEM_VISIBLE, REV_ITEM_BLOCK_TITLE, REV_ITEM_DATE,
//			REV_SORT_ITEM_BASIS, REV_SORT_ITEM_DESC, REV_TAG_ELEM_ID, REV_CLOUD_ELEM_ID,
//			cSort,
//		};


#endregion

#region + Utility

		public static DataEnum Clone(this DataEnum item)
		{
			DataEnum copy = new DataEnum(item.DescItemIdx, item.DataIdx);

//			copy.Column  = item.Column;
			copy.Source  = item.Source;
			copy.Name    = item.Name;
			copy.Display = item.Display.Clone();
			copy.Title   = item.Title;
			copy.Type    = item.Type;

			return copy;
		}

#endregion

#region + Column Helpers
//
//		public static void ResetColumnList()
//		{
//			ColumnList = new List<DataEnum>(PropertyCount);
//		}
//
//		public static void SetColumns(int col)
//		{
//			ResetColumnList();
//
//			foreach (DataEnum item in DataList)
//			{
//				item.Column = col;
//			}
//		}
//
//		public static void ResetColumnOrder()
//		{
//			ResetColumnList();
//
//			foreach (DataEnum item in DataList)
//			{
//				item.Column = item.DataIdx;
//				ColumnList.Insert(item.DataIdx, item);
//			}
//		}
//
//		public static void SetColumnOrder()
//		{
//			ResetColumnList();
//
//			foreach (DataEnum item in DataList)
//			{
//				if (item.Column < 0) continue;
//				ColumnList.Add(item);
//			}
//
//			ColumnList.Sort(CompareColumns);
//		}
//
//		private static int CompareColumns(DataEnum x, DataEnum y)
//		{
//			return x.Column.CompareTo(y.Column);
//		}

#endregion


	}
}
