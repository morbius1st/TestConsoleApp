#region + Using Directives

using System;
using System.Collections.Generic;
using System.Reflection;
using X = Microsoft.Office.Interop.Excel;
using static Microsoft.Office.Interop.Excel.XlHAlign;

using static TestConsoleApp.DataItems;
using static TestConsoleApp.RevisionFormat;

#endregion


// projname: TestConsoleApp
// itemname: RevisionExportExcel
// username: jeffs
// created:  6/30/2018 1:43:01 PM


namespace TestConsoleApp
{
	public static class RevisionExportExcel
	{
		private const int TITLE_ROW = 1;

		public static bool ExportToExcel(RevisionData revisionData, 
			RevOrderMgr om)
		{
			X.Application excel = new X.Application();

			X.Worksheet ws = GetWorksheet(excel);

			if (ws == null) return false;

			ws.Name = "Revisions";

			excel.Visible = false;

			int row = TITLE_ROW;

			ExportColumnTitles(ws, row, om);

			row++;

			X.Range range = ws.Range[ws.Cells[row, 1], 
				ws.Cells[RevisionDataMgr.SelectedCount + TITLE_ROW, 
					om.ColumnOrder.Count]];

			range.Cells.NumberFormat = "@";

			ExportRevisionData(RevisionDataMgr.IterateSelected(), row, ws, om);

			range.EntireColumn.AutoFit();

			foreach (X.Range col in range.Columns)
			{
				col.ColumnWidth += 1.5;
			}

			excel.Visible = true;

			return true;
		}

		private static X.Worksheet GetWorksheet(X.Application excel)
		{
			if (excel == null) return null;

			X.Workbook wb = excel.Workbooks.Add(Missing.Value);

			return  wb.Sheets.Item[1] as X.Worksheet;
		}

		private static void ExportColumnTitles(X.Worksheet ws, int row, RevOrderMgr om)
		{
			int col = 1;

			foreach (DataEnum item in om.ColumnOrder.Iterate())
			{
				ExportARowTitle(item, row, col++, ws);
			}

			X.Range range = ws.Range[ws.Cells[row, 1], 
				ws.Cells[row, om.ColumnOrder.Count]];

			range.Font.Bold = true;
			range.Cells.HorizontalAlignment = xlHAlignCenter;
			range.Cells.WrapText = true;
		}

		private static void ExportARowTitle(DataEnum item, int row, int col, 
			X.Worksheet ws)
		{
			string title = item.FullTitle;

			ws.Cells[row, col] = title;
//			ws.Columns[col].ColumnWidth = Math.Max(title.Length + 2, item.Display.DataWidth + 2);
//			ws.Columns[col].ColumnWidth = item.Display.ColWidth + 2;
		}

		private static void ExportRevisionData(IEnumerable<RevisionDataFields> iEnumerable, 
			int startRow, X.Worksheet ws, RevOrderMgr om)
		{
			int row = startRow;

			foreach (RevisionDataFields rdf in iEnumerable)
			{
				int col = 1;
				foreach (DataEnum d in om.ColumnOrder.Iterate())
				{
					ExportAnItem(rdf[d.DataIdx], d, row, col++, ws);
				}
				row++;
			}
		}

		private static void ExportAnItem(dynamic data, DescEnum descEnum, 
			int row, int col, X.Worksheet ws)
		{
			string formatted = string.Format(descEnum.Display.FormatString,
					data?? "");

			ws.Cells[row, col] = RevisionFormat.Format(data, descEnum.Display);
		}
	}
}
