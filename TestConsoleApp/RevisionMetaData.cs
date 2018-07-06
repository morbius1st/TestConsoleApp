using static TestConsoleApp.DataItems;
using static TestConsoleApp.DataItems.EDataFields;

using static TestConsoleApp.EDataType;
using static TestConsoleApp.EFieldSource;
using static TestConsoleApp.RevisionMetaData.Justification;

namespace TestConsoleApp
{
	public class RevisionMetaData
	{
		public static void Init()
		{
			int i = 0;

			const string fmt_num = "{0:D}";
			const string fmt_str = "{0:G}";
			const string fmt_eid = "{0:D}";
			const string fmt_date = "{0:MM/dd/yyyy}";

			// items - in no specific order
			ConfigItem(REV_SELECTED           , "", "Selected"           , 
				REV_SOURCE_DERIVED, new RevisionDataDisplay(  5, null, fmt_str, LEFT, RIGHT));
			ConfigItem(REV_SEQ                , "", "Seq"                , 
				REV_SOURCE_DERIVED, new RevisionDataDisplay(  4, null, fmt_num, RIGHT, RIGHT));
			ConfigItem(REV_SORT_DELTA_TITLE   , "Delta", "Title"         , 
				REV_SOURCE_CLOUD  , new RevisionDataDisplay(  20, null, fmt_str, LEFT, RIGHT));
			ConfigItem(REV_SORT_SHEETNUM      , "Sheet", "Number"        , 
				REV_SOURCE_DERIVED, new RevisionDataDisplay(  20, null, fmt_str, LEFT, RIGHT));
			ConfigItem(REV_ITEM_VISIBLE       , "", "Visibility"         , 
				REV_SOURCE_CLOUD  , new RevisionDataDisplay(  10, null, fmt_str, CENTER, RIGHT));
			ConfigItem(REV_SORT_ITEM_REVID    , "Rev", "Id"              , 
				REV_SOURCE_CLOUD  , new RevisionDataDisplay(  4, null, fmt_str, RIGHT, RIGHT));
			ConfigItem(REV_SORT_ORDER_CODE    , "Order", "Code"              , 
				REV_SOURCE_DERIVED, new RevisionDataDisplay(  14, null, fmt_str, RIGHT, RIGHT));
			ConfigItem(REV_ITEM_BLOCK_TITLE   , "Revision", "Block Title", 
				REV_SOURCE_CLOUD  , new RevisionDataDisplay(  16, null, fmt_str, LEFT, RIGHT));
			ConfigItem(REV_ITEM_DATE          , "", "Date"               , 
				REV_SOURCE_CLOUD  , new RevisionDataDisplay(  12, null, fmt_date, RIGHT, RIGHT));
			ConfigItem(REV_SORT_ITEM_BASIS    , "", "Basis"              , 
				REV_SOURCE_CLOUD  , new RevisionDataDisplay(  12, null, fmt_str, LEFT, RIGHT));
			ConfigItem(REV_SORT_ITEM_DESC     , "", "Description"        , 
				REV_SOURCE_CLOUD  , new RevisionDataDisplay(  36, null, fmt_str, LEFT, RIGHT));
			ConfigItem(REV_TAG_ELEM_ID        , "Tag", "Elem Id"         , 
				REV_SOURCE_DERIVED, new RevisionDataDisplay(  10, null, fmt_eid, RIGHT, RIGHT));
			ConfigItem(REV_CLOUD_ELEM_ID      , "Cloud", "Elem Id"       , 
				REV_SOURCE_DERIVED, new RevisionDataDisplay(  10, null, fmt_eid, RIGHT, RIGHT));
			ConfigItem(REV_MGMT_RECORD_ID     , "Record", "Id"       , 
				REV_SOURCE_DERIVED, new RevisionDataDisplay(  4, null, fmt_num, RIGHT, RIGHT));
			ConfigItem(REV_SORT_KEY           , "", "Sort Key"       , 
				REV_SOURCE_DERIVED, new RevisionDataDisplay(  100, null, fmt_str, LEFT, LEFT));

			// mgmt
			ConfigItem(REV_MGMT_COLUMN        , "", "Column");

			// sub-data fields
			ConfigItem(REV_SUB_ALTID          , "Alt", "Id"    , 
				new RevisionDataDisplay(  4, null, fmt_str, RIGHT, RIGHT));	   
			ConfigItem(REV_SUB_TYPE_CODE      , "Type", "Code" , 
				new RevisionDataDisplay(  4, null, fmt_str, LEFT, RIGHT));	   
			ConfigItem(REV_SUB_DISCIPLINE_CODE, "Disc", "Code" , 
				new RevisionDataDisplay(  6, null, fmt_str, LEFT, RIGHT));	   

			foreach (DescEnum de in DescList)
			{
				de.Display.SetWidthByTitle(de);
			}
		}

		private static void ConfigItem(RootEnum r,
			string title1, string title2)
		{
			r.Title[0] = title1;
			r.Title[1] = title2;
		}

		private static void ConfigItem(MgmtEnum m,
			string title1, string title2)
		{
			ConfigItem((RootEnum) m, title1, title2);
		}

		private static void ConfigItem(DescEnum d,
			string title1, string title2,
			RevisionDataDisplay display)
		{
			d.Display = display;
			ConfigItem((RootEnum) d, title1, title2);
		}

		private static void ConfigItem(DataEnum i,
			string title1, string title2,
			EFieldSource source, RevisionDataDisplay display)
		{
			i.Source = source;
			i.Display = display;
			ConfigItem((RootEnum) i, title1, title2);
		}

		public enum Justification
		{
			LEFT = -1,
			CENTER = 0,
			RIGHT = 1
		}
	}
}
