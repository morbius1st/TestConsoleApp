﻿#region + Using Directives
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static TestConsoleApp.DataItems;
using static TestConsoleApp.DataItems.EDataFields;

using static TestConsoleApp.RevisionFilters;
using static TestConsoleApp.RevisionFilters;
using static TestConsoleApp.RevisionFilters.ECompareOps;

using static UtilityLibrary.CsUtilities;

#endregion


// projname: TestConsoleApp
// itemname: Settings
// username: jeffs
// created:  7/8/2018 5:47:48 AM


namespace TestConsoleApp
{
	public class Settings
	{
		private const string DEFAULT_PROJECT_NAME = "New Project";
		private const string DEFAULT_PROJECT_NUMBER = "2018-999";

		private const string DEF_SOURCE_FILENAME = @"RevisionPivotTable.xlsx";
		private const string DEF_DESTINITION_FILENAME = @"ProjectPivotTable.xlsx";


		public static Settings Info { get; private set; }

		public string TemplatePathAndFileName { get; set; }
		public string ExcelPathAndFileName { get; set; }

		public string ProjectNumber { get; private set; }
		public string ProjectName { get; private set; }


		// one click Info
		public RevOrderMgr oneClickOrderMgr { get; private set; }
		public RevisionFilters oneClickFilter { get; private set; }

	
		static Settings()
		{
			Info = new Settings();
			Info.ReadSettings();
		}
		
		public bool SaveSettings()
		{
			return true;
		}

		public void ReadSettings()
		{
			ReadOneClick();

			ReadDefaultOrder();

			ReadProjectInfo();

			ReadTemplateLocation();

			ReadExcelFileLocation();
		}

		public RevColumnOrder DefaultColumnOrder
		{
			get => DefColumnOrder();
		}

		public RevSortOrder DefaultSortOrder
		{
			get => DefSortOrder();
		}

		#region + Read Routines

		#region + OneClick

		private void ReadOneClick()
		{
			ReadOneClickOrder();

			ReadOneClickFilter();

			ReadOneClickCriteria();
		}

		private void ReadOneClickOrder()
		{
			oneClickOrderMgr = new RevOrderMgr();

			ReadOneClickColumnOrder();

			ReadOneClickSortOrder();
		}

		private void ReadOneClickColumnOrder()
		{
			bool result = false;

			if (!result)
			{
				oneClickOrderMgr.ColumnOrder = DefaultOneClickColumnOrder().Clone();
			}
		}

		private void ReadOneClickSortOrder()
		{
			bool result = false;

			if (!result)
			{
				oneClickOrderMgr.SortOrder = DefaultOneClickSortOrder().Clone();
			}

		}

		private void ReadOneClickFilter()
		{
			oneClickFilter = new RevisionFilters();

			oneClickFilter.Add(ReadOneClickCriteria());
		}

		private Criteria  ReadOneClickCriteria()
		{
			Criteria c = null;
			
			if (c == null)
			{
				return DefaultOneClickCriteria();
			}

			return c;
		}

		#endregion


		#region + Default Order

		private void ReadDefaultOrder()
		{
			ReadDefaultColumnOrder();

			ReadDefaultSortOrder();
		}

		private void ReadDefaultColumnOrder()
		{
			bool result = false;

			if (!result)
			{
				RevColumnOrder.Default = DefColumnOrder().Clone();
			}
		}

		private void ReadDefaultSortOrder()
		{
			bool result = false;

			if (!result)
			{
				RevSortOrder.Default = DefSortOrder().Clone();
			}
		}

		#endregion

		#region + Misc

		private void ReadProjectInfo()
		{
			ProjectNumber = DEFAULT_PROJECT_NUMBER;
			ProjectName   = DEFAULT_PROJECT_NAME;
		}

		private void ReadTemplateLocation()
		{
			TemplatePathAndFileName = Path.Combine(
				AssemblyDirectory, DEF_SOURCE_FILENAME);
		}

		private void ReadExcelFileLocation()
		{
			ExcelPathAndFileName = Path.Combine(
				AssemblyDirectory, DEF_DESTINITION_FILENAME);
		}

		#endregion

		#endregion

		#region + Defaults

		// for first time usage and
		// for reset
		private RevColumnOrder DefColumnOrder()
		{
			RevColumnOrder co = new RevColumnOrder();

			co.Start(REV_SORT_ITEM_REVID, 
				REV_SORT_ORDER_CODE, REV_SORT_DELTA_TITLE, REV_SORT_SHEETNUM,
				REV_ITEM_VISIBLE, REV_ITEM_BLOCK_TITLE, REV_ITEM_DATE,
				REV_SORT_ITEM_BASIS, REV_SORT_ITEM_DESC);

			return co;
		}

		// for first time usage and
		// for reset
		private RevSortOrder DefSortOrder()
		{
			RevSortOrder so = new RevSortOrder();

			so.Start(REV_SORT_ORDER_CODE, 
				REV_SORT_DELTA_TITLE, REV_SORT_SHEETNUM);

			return so;
		}

		private RevColumnOrder DefaultOneClickColumnOrder()
		{
			RevColumnOrder co = new RevColumnOrder();

			co.Start(REV_SORT_SHEETNUM, REV_SORT_ITEM_REVID, 
				REV_SORT_ORDER_CODE, REV_SORT_DELTA_TITLE, 
				REV_ITEM_VISIBLE, REV_ITEM_BLOCK_TITLE, REV_ITEM_DATE,
				REV_SORT_ITEM_BASIS, REV_SORT_ITEM_DESC);

			return co;


//			return DefColumnOrder();
		}

		private RevSortOrder DefaultOneClickSortOrder()
		{
			RevSortOrder so = new RevSortOrder();

			so.Start(REV_SORT_SHEETNUM, 
				REV_SORT_ORDER_CODE, 
				REV_SORT_DELTA_TITLE);

			return so;



//			return DefSortOrder();
		}

		private Criteria DefaultOneClickCriteria()
		{
			return new Criteria(REV_SORT_ORDER_CODE , EQUAL, 
				RevitRevisions.MaxRevision.ToString(), REV_SUB_ALTID);
		}

		#endregion
	}
}