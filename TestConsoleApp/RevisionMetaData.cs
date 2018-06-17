using System.Drawing;
using static TestConsoleApp.DataItems;
using static TestConsoleApp.DataItems.EDataFields;

using static TestConsoleApp.EDataType;
using static TestConsoleApp.EItemSource;
using static TestConsoleApp.RevisionMetaData.Justification;

namespace TestConsoleApp
{
	public static class RevisionMetaData
	{
		public static void Init()
		{
			int i = 0;

			const string fmt_id = "0:D";
			const string fmt_str = "0:G";
			const string fmt_eid = "0:D";
			const string fmt_date = "0:MM/dd/yyyy";

			// items - in no specific order
			ConfigItem(REV_SELECTED           , "", "Selected"           , 
				REV_SOURCE_DERIVED, new DataDisplay(10, LEFT,fmt_str, null));
			ConfigItem(REV_SEQ                , "", "Seq"                , 
				REV_SOURCE_DERIVED, new DataDisplay(6, RIGHT,fmt_id, null));
			ConfigItem(REV_KEY_ALTID          , "Alt", "Id"              , 
				REV_SOURCE_CLOUD  , new DataDisplay(6, RIGHT,fmt_str, null));
			ConfigItem(REV_KEY_TYPE_CODE      , "Type", "Code"           , 
				REV_SOURCE_DERIVED, new DataDisplay(6, LEFT,fmt_str, null));
			ConfigItem(REV_KEY_DISCIPLINE_CODE, "Disc", "Code"	         , 
				REV_SOURCE_DERIVED, new DataDisplay(8, LEFT,fmt_str, null));
			ConfigItem(REV_KEY_DELTA_TITLE    , "Delta", "Title"         , 
				REV_SOURCE_CLOUD  , new DataDisplay(20, LEFT,fmt_str, null));
			ConfigItem(REV_KEY_SHEETNUM       , "Sheet", "Number"        , 
				REV_SOURCE_DERIVED, new DataDisplay(20, LEFT,fmt_str, null));
			ConfigItem(REV_ITEM_VISIBLE       , "", "Visibility"         , 
				REV_SOURCE_CLOUD  , new DataDisplay(22, CENTER,fmt_str, null));
			ConfigItem(REV_ITEM_REVID         , "Rev", "Id"              , 
				REV_SOURCE_CLOUD  , new DataDisplay(6, RIGHT,fmt_str, null));
			ConfigItem(REV_ITEM_BLOCK_TITLE   , "Revision", "Block Title", 
				REV_SOURCE_CLOUD  , new DataDisplay(32, LEFT,fmt_str, null));
			ConfigItem(REV_ITEM_DATE          , "", "Date"               , 
				REV_SOURCE_CLOUD  , new DataDisplay(12, RIGHT,fmt_date, null));
			ConfigItem(REV_ITEM_BASIS         , "", "Basis"              , 
				REV_SOURCE_CLOUD  , new DataDisplay(20, LEFT,fmt_str, null));
			ConfigItem(REV_ITEM_DESC          , "", "Description"        , 
				REV_SOURCE_CLOUD  , new DataDisplay(36, LEFT,fmt_str, null));
			ConfigItem(REV_TAG_ELEM_ID        , "Tag", "Elem Id"         , 
				REV_SOURCE_DERIVED, new DataDisplay(10, RIGHT,fmt_eid, null));
			ConfigItem(REV_CLOUD_ELEM_ID      , "Cloud", "Elem Id"       , 
				REV_SOURCE_DERIVED, new DataDisplay(10, RIGHT,fmt_eid, null));

			// mgmt
			ConfigItem(REV_MGMT_COLUMN        , "", "Column");
			ConfigItem(REV_KEY                , "", "Key");

			foreach (Desc d in DescList)
			{
				foreach (string s in d.Title)
				{
					d.Display.MaxTitleWidth =
						d.Display.MaxTitleWidth > s.Length ? 
							d.Display.MaxTitleWidth : s.Length;
				}
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

		private static void ConfigItem( DataEnum i,
			string title1, string title2,
			EItemSource source, DataDisplay display)
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

		public class DataDisplay
		{
			public int ColumnWidth { get; set; }       // the size of the column in which to place the data
			public int MaxTitleWidth { get; set; }	   // the maximum width of a title
			public Justification Justify { get; set; } // data justification in the column
			public string FormatString  { get; set; }  // the format string in which to format the data
			public Font Font  { get; set; }            // the font in which to format the data (not used)

			public DataDisplay()
			{
				ColumnWidth = 5;
				FormatString = "";
				Font = null;
			}

			public DataDisplay(int colWidth, Justification justify, string formatString, Font font)
			{
				ColumnWidth = colWidth;
				if (string.IsNullOrEmpty(formatString))
				{
					FormatString = " {0:G} ";
				} 
				else
				{
					FormatString = " {" + formatString + "} ";
				}
				Justify = justify;
				Font = font;
			}
		}

	}
}
