#region + Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using X = Microsoft.Office.Interop.Excel;
using static Microsoft.Office.Interop.Excel.XlHAlign;

using static TestConsoleApp.DataItems;
using static TestConsoleApp.RevisionTest;

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

		private const string LOCATION = @"B:\Programming\VisualStudioProjects\TestConsoleApp";
		private const string SOURCE_FILE_NAME = @"RevisionPivotTable.xlsx";
		private const string OUTPUT_FILE_NAME = @"ProjectPivotTable.xlsx";

		private const string WKS_DATA = "RevisionsData";
		private const string WKS_PIVOT = "PivotTable";

		private const string PIVOT_TABLE_NAME = "Revisions";

		public static bool ExportToExcel(RevisionData revisionData, 
			RevOrderMgr om)
		{
			int row = TITLE_ROW;

//			string inFile = Path.Combine(LOCATION, SOURCE_FILE_NAME);
//			string outFile = Path.Combine(LOCATION, OUTPUT_FILE_NAME);
//
//			if (!File.Exists(inFile))
//			{
//				Console.WriteLine("template file does not exist");
//				Console.WriteLine(nl);
//				return false;
//			}
//
//			File.Copy(inFile, outFile, true);
//
//			if (!File.Exists(outFile))
//			{
//				Console.WriteLine("file does not exist");
//				Console.WriteLine(nl);
//				return false;
//			}

			string outFile = GetOutputFile(Settings.Info.TemplatePathAndFileName, 
				Settings.Info.ExcelPathAndFileName);

			X.Application excel = new X.Application();
			if (excel == null) return false;

			X.Workbook wb = excel.Workbooks.Open(outFile);
			X.Worksheet wsData = wb.Sheets[WKS_DATA] as X.Worksheet;

			if (wsData == null) return false;

			wsData.Name = WKS_DATA;

			excel.Visible = false;

			ExportColumnTitles(wsData, row, om);

			row++;

			X.Range range = GetRange(wsData, row,
				RevisionDataMgr.SelectedCount, 1, om.ColumnOrder.Count);

			FormatDataCells(range);

			ExportRevisionData(RevisionDataMgr.IterateSelected(), row, wsData, om);

			AdjustColumnWidthInRange(range.Columns, 1.5);

			X.Worksheet wsPivot = wb.Sheets[WKS_PIVOT] as X.Worksheet;

			X.PivotTable pivotTable = (X.PivotTable) wsPivot.PivotTables(PIVOT_TABLE_NAME);

			pivotTable.RefreshTable();

			excel.Visible = true;

			return true;
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

		private static X.Range GetRange(X.Worksheet ws,
			int startRow, int rowCount,
			int startColumn, int columnsWide)
		{
			return ws.Range[ws.Cells[startRow, startColumn], 
				ws.Cells[startRow + rowCount, startColumn + columnsWide]];
		}

		private static void AdjustColumnWidth(X.Range col, double adjustAmt)
		{
			col.ColumnWidth += adjustAmt;
		}

		private static void AdjustColumnWidthInRange(X.Range rangeOfColumns, 
			double adjustAmt)
		{
			foreach (X.Range col in rangeOfColumns)
			{
				col.EntireColumn.AutoFit();
				AdjustColumnWidth(col, adjustAmt);
			}
		}

		private static string GetOutputFile(string source, string destinition)
		{
			if (!File.Exists(source))
			{
				Console.WriteLine("template file (" + source 
					+ ") does not exist");
				Console.WriteLine(nl);
				return null;
			}

			File.Copy(source, destinition, true);

			if (!File.Exists(destinition))
			{
				Console.WriteLine("could create destinition excel file");
				Console.WriteLine(nl);
				return null;
			}

			return destinition;
		}

		private static void FormatDataCells(X.Range range)
		{
			range.Cells.NumberFormat = "@";
		}

	}
}
