using System.Collections.Generic;
using System.Reflection;
using static TestConsoleApp.DataItems.ColumnHelpers;

using static TestConsoleApp.RevisionMetaData;

namespace TestConsoleApp
{
	public enum RevisionVisibility
	{
		Hidden = 0,
		TagVisible = 1,
		CloudAndTagVisible = 2,
	}

	public enum EDataType
	{
		BOOL,
		INT,
		STRING,
		ELEMENTID,
		VISIBILITY
	}

	public enum EItemSource
	{
		REV_SOURCE_DERIVED,
		REV_SOURCE_TAG,
		REV_SOURCE_CLOUD
	}

	public static class DataItems
	{
		public static int PropertyCount { get; private set; }

		public static List<RootEnum> RootList ;
		public static List<Desc> DescList;
		public static List<DataEnum> DataList ;
		public static List<FilterEnum> FilterList ;
		public static List<MgmtEnum> MgmtList ;

		public static List<DataEnum> ColumnList;

		public static class EDataFields
		{
			// *** add no other properties here ***
			// create the data enums here - the order below defines the
			// default column order
			//
			public static CompareBoolEnum	   REV_SELECTED            { get; } = new CompareBoolEnum();	// =>  0	// (derived) flag that this data item has 
			//																								//			// been selected
			public static CompareIntEnum       REV_SEQ                 { get; } = new CompareIntEnum();// =>  1	// (from sequence) revision sequence number
			//																								//			// for ordering only
			public static CompareStrEnum	   REV_ITEM_REVID          { get; } = new CompareStrEnum();		// =>  2	// (from revision) revision id number or 		   
			//																								//			// alphanumeric									
			public static CompareStrEnum	   REV_KEY_ALTID           { get; } = new CompareStrEnum();		// =>  3	// (from issued by) (part of item key) 
			//																								//			// a cross-reference to the REV_REVID associated with this item
			public static DataEnum		       REV_KEY_TYPE_CODE       { get; } = new DataEnum();			// =>  4	// (derived) (part of item key) code based  
			//																								//			// on the document type
			public static DataEnum		       REV_KEY_DISCIPLINE_CODE { get; } = new DataEnum();	        // =>  5	// (derived) (part of item key) code based
			//																								//			// on the discipline														   
			public static CompareStrEnum	   REV_KEY_DELTA_TITLE     { get; } = new CompareStrEnum();		// =>  6	// (from issued to) (part of item key) 		   
			//																								//			// simple name for this issuance (goes below the delta)						   
			public static CompareStrEnum	   REV_KEY_SHEETNUM        { get; } = new CompareStrEnum();		// =>  7	// (calculated) (part of item key) sheet 		   
			//																								//			// number of this tag														   
			public static CompareVisEnum       REV_ITEM_VISIBLE        { get; } = new CompareVisEnum();	// =>  8	// (from visibility)(calculated)) item 
			//																								//			// visibility																   
			public static CompareStrEnum	   REV_ITEM_BLOCK_TITLE    { get; } = new CompareStrEnum();		// =>  9	// (from revision description) title for 		   
			//																								//			// this issuance															   
			public static DataEnum		       REV_ITEM_DATE           { get; } = new DataEnum();	        // => 10	// (from revision date) the date assigned 
			//																								//			// to the revision															   
			public static CompareStrEnum	   REV_ITEM_BASIS          { get; } = new CompareStrEnum();		// => 11	// (from comment) the reason for the 			   
			//																								//			// revision																	   
			public static CompareStrEnum	   REV_ITEM_DESC           { get; } = new CompareStrEnum();     // => 12	// (from mark) the description of the 
			//																								//			// revision																	   
			public static CompareEIdEnum	   REV_TAG_ELEM_ID         { get; } = new CompareEIdEnum();	// => 13	// the element id of the tag for this 
			//																								//			// data item															   
			public static CompareEIdEnum	   REV_CLOUD_ELEM_ID       { get; } = new CompareEIdEnum();	// => 14	// the element id of of the cloud for 
			//																								//			// this data item															   
			public static MgmtEnum             REV_MGMT_COLUMN         { get; } = new MgmtEnum();			// => n/a	// (derived) the title for the column field the	   
			//																								//			// column is only stored with the data description							   
			public static MgmtEnum             REV_KEY                 { get; } = new MgmtEnum();	        // => n/a	// (derived) the list key for the items
		}

		static DataItems()
		{
			PropertyInfo[] propertyInfos = typeof(EDataFields).GetProperties();

			PropertyCount = propertyInfos.Length;

			RootList = new List<RootEnum>(PropertyCount);
			DescList = new List<Desc>(PropertyCount);
			DataList = new List<DataEnum>(PropertyCount);
			FilterList = new List<FilterEnum>(PropertyCount);
			MgmtList = new List<MgmtEnum>(PropertyCount);

			ResetColumnList();
			
			// use reflection to access all of the data items
			// update the data item's name with its variable name
			// categorize the properties into arrays for easy access
			foreach (PropertyInfo p in propertyInfos)
			{
				RootEnum r = (RootEnum) p.GetValue(null);

				r.Name = p.Name;

				RootList.Add(r);

				if (r is MgmtEnum mgmt)
				{
					MgmtList.Add(mgmt);
				}

				if (r is Desc d)
				{
					DescList.Insert(d.DescItemIdx, d);
				}

				if (r is DataEnum)
				{
					DataEnum i = (DataEnum) r;

					DataList.Insert(i.DataIdx, i);

					ColumnList.Insert(i.DataIdx, i);
				}

				if (r is FilterEnum)
				{
					FilterEnum f = (FilterEnum) r;

					FilterList.Insert(f.FilterIdx, f);
				}
			}

			// update the column list and set the column
			// for each field based on the above order
			ResetColumnOrder();
		}

		#region + Classes

		#region + Primary

		// root class
		// include properties that apply
		// to every sub-class
		public class RootEnum
		{
			public string		Name { get; set; }		// the variable's name - do I need this?
			public EDataType	Type { get; set; }		// the data type
			public string[]		Title { get; set; } 
				= new string[2];					// the title for this data item (column header)

			public RootEnum()
			{
				Type = EDataType.STRING;
			}
		}

		// description class
		// only items that need a description and
		// may be displayed
		public class Desc : RootEnum
		{
			private static int a = 0;

			public Desc()
			{
				DescItemIdx = a++;
			}

			public Desc(int d)
			{
				DescItemIdx = d;
			}

			public int DescItemIdx { get; }
			
			public DataDisplay Display { get; set; } = new DataDisplay();  // data display
			// information - font, format string, etc.
		}

		// management items
		// only need the name property
		public class MgmtEnum : RootEnum { }

		// item class
		// these are the data items read from the
		// revision clouds & tags
		public class DataEnum : Desc
		{
			private static int b = 0;

			public DataEnum()
			{
				DataIdx = b++;
			}

			public DataEnum(int d, int i) : base(d)
			{
				DataIdx = i;
			}

			public int DataIdx { get; }

			public int Column { get; set; }			// the column to present this data item
			public EItemSource Source { get; set; }	// where the data came from (to allow changes)
		}

		// filter class
		// these are sub-items that can be
		// filtered for (that is, selected)
		// this is the general class - refer
		// to the specific sub-compare class
		// for the specific type of filter
		public class FilterEnum : DataEnum
		{
			private static int c = 0;

			public FilterEnum(EDataType type)
			{
				Type = type;
				FilterIdx = c++;
			}

			public int FilterIdx { get; }
		}
	
		// compare classes
		// these are sub-filter items that can
		// be selected and can be compared
		// using a specific type of comparison
		public class CompareStrEnum : FilterEnum
			{ public CompareStrEnum() : base(EDataType.STRING) { } }

		public class CompareVisEnum : FilterEnum
			{ public CompareVisEnum() : base(EDataType.VISIBILITY) { } }

		public class CompareEIdEnum : FilterEnum
			{ public CompareEIdEnum() : base(EDataType.ELEMENTID) { } }

		public class CompareBoolEnum : FilterEnum
			{ public CompareBoolEnum() : base(EDataType.BOOL) { } }

		public class CompareIntEnum : FilterEnum
		{ public CompareIntEnum() : base(EDataType.INT) { } }

		#endregion

		#region + Utility

		public static DataEnum Clone(this DataEnum item)
		{
			DataEnum copy = new DataEnum(item.DescItemIdx,  item.DataIdx);
			copy.Column = item.Column;
			copy.Source = item.Source;
			copy.Name = item.Name;
			copy.Display.ColumnWidth = item.Display.ColumnWidth;
			copy.Display.Font = item.Display.Font;
			copy.Display.FormatString = item.Display.FormatString;
			copy.Title = item.Title;
			copy.Type = item.Type;

			return copy;
		}

		public static class ColumnHelpers
		{
			public static void ResetColumnList()
			{
				ColumnList = new List<DataEnum>(PropertyCount);
			}

			public static IEnumerable<DataEnum> ItemsInColumnOrder()
			{
				foreach (DataEnum item in ColumnList)
				{
					yield return item;
				}
			}

			public static void SetColumns(int col)
			{
				ResetColumnList();

				foreach (DataEnum item in DataList)
				{
					item.Column = col;
				}
			}

			public static void ResetColumnOrder()
			{
				ResetColumnList();

				foreach (DataEnum item in DataList)
				{
					item.Column = item.DataIdx;
					ColumnList.Insert(item.DataIdx, item);
				}
			}

			public static void SetColumnOrder()
			{
				ResetColumnList();

				foreach (DataEnum item in DataList)
				{
					if (item.Column < 0) continue;
					ColumnList.Add(item);
				}

				ColumnList.Sort(CompareColumns);
			}

			private static int CompareColumns(DataEnum x, DataEnum y)
			{
				return x.Column.CompareTo(y.Column);
			}
		}

		#endregion

		#endregion

	}
}
