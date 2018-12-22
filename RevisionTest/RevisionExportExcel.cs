#region + Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using X = Microsoft.Office.Interop.Excel;
using static Microsoft.Office.Interop.Excel.XlHAlign;

using static RevisionTest.DataItems;
using static RevisionTest.RevisionTest;
using static RevisionTest.Settings;

using static RevisionTest.DataItems.EDataFields;

#endregion


// projname: TestConsoleApp
// itemname: RevisionExportExcel
// username: jeffs
// created:  6/30/2018 1:43:01 PM


namespace RevisionTest
{
	public static class RevisionExportExcel
	{
		private const int TITLE_ROW = 1;

		public static bool ExportToExcel(RevisionData selected, 
			RevOrderMgr om)
		{
			List<List<string>> data = 
				AggregateData(selected, om, REV_SORT_ITEM_DESC);
			
			int row = TITLE_ROW;

			string outFile = SetUpOutputFile(Setg.TemplatePathAndFileName, 
				Setg.ExcelPathAndFileName);

			X.Application excel = new X.Application();
			if (excel == null) return false;

			X.Workbook wb = excel.Workbooks.Open(outFile);
			X.Worksheet wsData = 
				wb.Sheets[Setg.ExcelDataWorksheetName] as X.Worksheet;

			if (wsData == null) return false;

			excel.Visible = false;

			ExportColumnTitles(wsData, row, om);

			row++;

			//                           startRow         row count
			X.Range range = GetRange(wsData, row, data.Count, 
			// startCol   colCount
				1, om.ColumnOrder.Count);

			FormatDataCells(range);

			ExportToExcel(data, row, wsData, om);

			AdjustColumnWidthInRange(range.Columns, 1.5);

			X.Worksheet wsPivot = 
				wb.Sheets[Setg.ExcelPivotWorksheetName] as X.Worksheet;

			X.PivotTable pivotTable = 
				(X.PivotTable) wsPivot.PivotTables(Setg.ExcelPivotTableName);

			pivotTable.RefreshTable();

			excel.Visible = true;

			return true;
		}

		private static List<List<string>> AggregateData(RevisionData selected,
			RevOrderMgr om, DataItems.DataEnum field)
		{
			// rows x columns
			List<List<string>> rawTableData = FormatTableData(selected, om);
			List<List<string>> finalTableData = new List<List<string>>();

			int newRow = 0;

			int maxRawRows = rawTableData.Count;

			finalTableData.Add(rawTableData[0].Clone());

			for (int i = 1; i < rawTableData.Count; i++)
			{
				bool result = true;

				foreach (DataItems.DataEnum item in om.SortOrder.Columns)
				{
					if (item.Equals(field)) continue;

					if (!rawTableData[i-1][item.DataIdx].
						Equals(rawTableData[i][item.DataIdx]))
					{
						finalTableData.Add(rawTableData[i].Clone());
						newRow++;
						result = false;
						break;
					}
				}

				if (result)
				{
					if (!finalTableData[newRow][field.DataIdx]
						.Equals(rawTableData[i][field.DataIdx]))
					{
						finalTableData[newRow][field.DataIdx] +=
							nl + rawTableData[i][field.DataIdx];
					}
				}
			}

			Console.WriteLine("raw table data");
			ListTableData(rawTableData);
			Console.Write(nl);

			Console.WriteLine("final table data");
			ListTableData(finalTableData);


			return finalTableData;
		}

		private static void ListTableData(List<List<string>> tableData)
		{
			foreach (List<string> list in tableData)
			{
				foreach (string s in list)
				{
					Console.Write(s + "  ");
				}

				Console.Write(nl);
			}
		}


		private static List<List<string>> FormatTableData(RevisionData selected,
			RevOrderMgr om)
		{
			// rows x columns
			List<List<string>> tableData = new List<List<string>>();

			foreach (RevisionDataFields rdf in selected.GetEnumerable())
			{
				List<string> rowData = new List<string>(new string[selected.Count]);

				foreach (DataItems.DataEnum d in om.ColumnOrder.itemize())
				{
					rowData[d.DataIdx] = (string.Format(d.Display.FormatString,
						rdf[d.DataIdx]?? ""));
				}
				tableData.Add(rowData);
			}
			return tableData;
		}


		private static void ExportColumnTitles(X.Worksheet ws, int row, RevOrderMgr om)
		{
			int col = 1;

			foreach (DataItems.DataEnum item in om.ColumnOrder.Iterate())
			{
				ExportARowTitle(item, row, col++, ws);
			}

			X.Range range = ws.Range[ws.Cells[row, 1], 
				ws.Cells[row, om.ColumnOrder.Count]];

			range.Font.Bold = true;
			range.Cells.HorizontalAlignment = xlHAlignCenter;
			range.Cells.WrapText = true;
		}

		private static void ExportARowTitle(DataItems.DataEnum item, int row, int col, 
			X.Worksheet ws)
		{
			string title = item.FullTitle;

			ws.Cells[row, col] = title;
		}

		private static void ExportToExcel(RevisionData selected, 
			int startRow, X.Worksheet ws, RevOrderMgr om)
		{
			int row = startRow;

			foreach (RevisionDataFields rdf in selected.GetEnumerable())
			{
				int col = 1;
				foreach (DataItems.DataEnum d in om.ColumnOrder.Iterate())
				{
					ExportAnItem(rdf[d.DataIdx], d, row, col++, ws);
				}
				row++;
			}
		}

		private static void ExportToExcel(List<List<string>> data, 
			int startRow, X.Worksheet ws, RevOrderMgr om)
		{
			int row = startRow;

			foreach (List<string> field in data)
			{
				int col = 1;
				foreach (DataItems.DataEnum d in om.ColumnOrder.Iterate())
				{
					ws.Cells[row, col++] = field[d.DataIdx];
				}
				row++;
			}
		}

		private static void ExportAnItem(dynamic data, DataItems.DescEnum descEnum, 
			int row, int col, X.Worksheet ws)
		{
			string formatted = string.Format(descEnum.Display.FormatString,
					data?? "");

			ws.Cells[row, col] = RevisionFormat.Format(data, descEnum.Display);
		}

		private static X.Range GetRange(X.Worksheet ws,
			int startRow, int rowCount,
			int startColumn, int colCount)
		{
			return ws.Range[ws.Cells[startRow, startColumn], 
				ws.Cells[startRow + rowCount, startColumn + colCount]];
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

		private static string SetUpOutputFile(string source, string destinition)
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
			range.Cells.VerticalAlignment = X.XlVAlign.xlVAlignTop;
		}

	}
}
