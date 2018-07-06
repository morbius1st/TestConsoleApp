using System;
using System.Collections.Generic;
using static TestConsoleApp.DataItems.EDataFields;

using static TestConsoleApp.DataItems.EDataFields;
using static TestConsoleApp.DataItems;

namespace TestConsoleApp
{
	class RevitRevisions
	{
		#region + Sample Revision Data

		private static int count = 0;

		public static int MaxRevision = -1;

		public static List<string[]> RevitRevisionInfo;

		public static RevisionData Read()
		{
			RevitRevInfo();

			MaxRevision = ScanForMaxInListOfStringArray(RevitRevisionInfo, 
				(int) ERevitRevInfo.REV_ID);

			return Scan();
		}

		// has two purposes: 
		// translate the data read to the correct format
		// basically nothing for most
		// put the data in the correct order
		// [i] = is the order from the above
		// [j] = the correct order in the array
		private static RevisionDataFields MakeRevDataItem(dynamic[] items)
		{
			RevisionDataFields di = new RevisionDataFields();

			di.Order = count++;

			for (int i = 0; i < items.Length; i++)
			{
				DataEnum d = xlate[i];
				int j = d.DataIdx;

				switch (i)
				{
				// selected
				case 0:		
					{
						di[j] = bool.Parse(items[i]);
						break;
					}
				// rev order code
				case 2:
					{
						RevOrderCode rc = new RevOrderCode();
						rc.AltId = items[i++];
						REV_SUB_ALTID.Display.DataWidth = rc.AltId.Length;

						rc.TypeCode = items[i++];
						REV_SUB_TYPE_CODE.Display.DataWidth = rc.TypeCode.Length;

						rc.DisciplineCode = items[i];
						REV_SUB_DISCIPLINE_CODE.Display.DataWidth = rc.DisciplineCode.Length;

						di[j] = rc;

						items[i] = rc;

						break;
					}
				// sequence
				case 1:
				// tag elem id
				case 13:	
				// cloud elem id
				case 14:	
					{
						di[j] = Int32.Parse(items[i]);
						break;
					}
				// visibility
				case 7:		
					{
						di[j] = Enum.Parse(typeof(RevisionVisibility), items[i]);
						break;
					}
				// date
				case 10:
					{
						di[j] = DateTime.Parse(items[i]);
						break;
					}
				default:	// all else - string
					{
						di[j] = items[i];
						break;
					}
				}

				d.Display.DataWidth = (items[i]?.ToString() ?? "").Length;

			}

			return di;
		}

		// scan the revit revisions to determine the 
		// largest revision (bulletin or addenda)
		private static int ScanForMaxInListOfStringArray(List<string[]> list, int idx)
		{
			int max = -1;

			foreach (string[] s in list)
			{
				if (s[idx].Length == 0)
				{
					continue;
				}

				int test = Int32.Parse(s[idx]);

				if (test > max) max = test;
			}

			return max;
		}

		private static DataEnum[] xlate =
		{
			//  0
			REV_SELECTED,
			//  1
			REV_SEQ,
			//  2
			REV_SORT_ORDER_CODE,
			//  3 -> put into a RevOrderCode
			REV_SORT_ORDER_CODE,
			//  4 -> put into a RevOrderCode
			REV_SORT_ORDER_CODE,
			//  5
			REV_SORT_DELTA_TITLE,
			//  6
			REV_SORT_SHEETNUM,
			//  7
			REV_ITEM_VISIBLE,
			//  8
			REV_SORT_ITEM_REVID,
			//  9
			REV_ITEM_BLOCK_TITLE,
			// 10
			REV_ITEM_DATE,
			// 11
			REV_SORT_ITEM_BASIS,
			// 12
			REV_SORT_ITEM_DESC,
			// 13
			REV_TAG_ELEM_ID,
			// 14
			REV_CLOUD_ELEM_ID,
		};

		public static RevisionData Scan()
		{
			RevisionData data = new RevisionData();

			dynamic[] Items;

			Items = new dynamic[]
			{
				// order and translation order
				"False",		// 0 =>  0 // selected
				"1",			// 1 =>  1 // seq
				"1",			// 2 =>  3a // alt id
				".00",			// 3 =>  3b // type code
				".00.00",		// 4 =>  3c // disc code
				"BULLETIN 001",	// 5 =>  4 // delta title
				"CS000",		// 6 =>  5 // sht num
				"Hidden",		// 7 =>  6 // visibility
				"1",			// 8 =>  2 // rev id
				"BULLETIN 001",	// 9 =>  7 // block title
				"1/1/2018",		//10 =>  8 // date
				"pcc",			//11 =>  9 // basis
				"rev desc 1-cs000-000-1 this description has been made extra "
					+ "long in order to test what happens when a description is "
					+ "very long.  I've made this description into two"
					+ "sentences to test this also", //12 => 10 // desc
				"-1",			//13 => 11 // tag elem id
				"209594"		//14 => 12 // cloud elem id
			};

			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"1",
				"1",
				".00",
				".00.00",
				"BULLETIN 001",
				"CS000",
				"Hidden",
				"1",
				"BULLETIN 001",
				"1/1/2018",
				"pcc",
				"rev desc 1-cs000-000-2",
				"-1",
				"209594",

			};

			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"1",
				"1",
				".00",
				".00.00",
				"BULLETIN 001",
				"CS000",
				"Hidden",
				"1",
				"BULLETIN 001",
				"1/1/2018",
				"rfi",
				"rev desc 1-cs000-000-3",
				"-1",
				"209594",
			};

			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"1",
				"1",
				".00",
				".00.00",
				"BULLETIN 001",
				"CS100",
				"Hidden",
				"1",
				"BULLETIN 001",
				"1/1/2018",
				"owner rev",
				"rev comment 1",
				"-1",
				"209594",
			};

			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"2",
				"1",
				".10",
				".00.00",
				"ASI 007",
				"CS000",
				"TagVisible",
				"",
				"ASI 007 (BULLETIN 001)",
				"1/7/2018",
				"owner rev",
				"rev desc 1-cs000-007-1",
				"-1",
				"213234",
			};

			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"2",
				"1",
				".10",
				".00.00",
				"ASI 007",
				"CS000",
				"TagVisible",
				"",
				"ASI 007 (BULLETIN 001)",
				"1/7/2018",
				"pcc",
				"rev desc 1-cs000-007-2",
				"-1",
				"213234",
			};

			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"2",
				"1",
				".10",
				".00.00",
				"ASI 007",
				"CS000",
				"TagVisible",
				"",
				"ASI 007 (BULLETIN 001)",
				"1/7/2018",
				"owner rev",
				"rev desc 1-cs000-007-1",
				"-1",
				"213234",
			};

			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"3",
				"1",
				".10",
				".00.00",
				"ASI 008",
				"CS000",
				"CloudAndTagVisible",
				"",
				"ASI 008 (BULLETIN 001)",
				"1/8/2018",
				"owner rev",
				"desc 001-008-000",
				"-1",
				"214040",
			};

			data.Add(MakeRevDataItem(Items));

			

			Items = new dynamic[]
			{
				"False",
				"11",
				"6",
				".10",
				".07.00",
				"ASI 013",
				"1A A201",
				"CloudAndTagVisible",
				"",
				"ASI 013 (BULLETIN 006)",
				"6/2/2018",
				"rfi",
				"rev desc 1A A201 -006-013-1",
				"-1",
				"226131",
			};
			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"11",
				"6",
				".10",
				".07.00",
				"ASI 013",
				"1A A201",
				"CloudAndTagVisible",
				"",
				"ASI 013 (BULLETIN 006)",
				"6/2/2018",
				"pcc",
				"rev desc 1A A201 -006-013-1",
				"-1",
				"226131",
			};
			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"11",
				"6",
				".10",
				".07.00",
				"ASI 013",
				"1A A202",
				"CloudAndTagVisible",
				"",
				"ASI 013 (BULLETIN 006)",
				"6/2/2018",
				"rfi",
				"rev desc 1A A202 -006-013-1",
				"-1",
				"226131",
			};
			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"11",
				"6",
				".10",
				".07.00",
				"ASI 013",
				"1A A202",
				"CloudAndTagVisible",
				"",
				"ASI 013 (BULLETIN 006)",
				"6/2/2018",
				"owner rev",
				"rev desc 1A A202 -006-013-2",
				"-1",
				"226131",
			};
			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"11",
				"6",
				".10",
				".07.00",
				"ASI 013",
				"1B A201",
				"CloudAndTagVisible",
				"",
				"ASI 013 (BULLETIN 006)",
				"6/2/2018",
				"pcc",
				"rev desc 1B A201 -006-013-1",
				"-1",
				"226131",
			};
			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"11",
				"6",
				".10",
				".07.00",
				"ASI 013",
				"1B A201",
				"CloudAndTagVisible",
				"",
				"ASI 013 (BULLETIN 006)",
				"6/2/2018",
				"owner rev",
				"rev desc 1B A201 -006-013-2",
				"-1",
				"226131",
			};
			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"11",
				"6",
				".10",
				".07.00",
				"ASI 013",
				"AA A2.20-201.10",
				"CloudAndTagVisible",
				"",
				"ASI 013 (BULLETIN 006)",
				"6/2/2018",
				"pcc",
				"",
				"-1",
				"226131",
			};
			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"11",
				"6",
				".10",
				".07.00",
				"ASI 013",
				"AA A2.20-201.10",
				"CloudAndTagVisible",
				"",
				"ASI 013 (BULLETIN 006)",
				"6/2/2018",
				"owner rev",
				"",
				"-1",
				"226131",
			};
			data.Add(MakeRevDataItem(Items));

			// bulletin 006
			Items = new dynamic[]
			{
				"False",
				"9",
				"6",
				".00",
				".00.00",
				"BULLETIN 006",
				"CS000",
				"CloudAndTagVisible",
				"6",
				"BULLETIN 006",
				"6/1/2018",
				"owner rev",
				"rev desc 1-cs000-006-xx1",
				"-1",
				"225501",

			};

			data.Add(MakeRevDataItem(Items));

			// bulletin 006
			Items = new dynamic[]
			{
				"False",
				"10",
				"7",
				".00",
				".00.00",
				"BULLETIN 007",
				"CS000",
				"CloudAndTagVisible",
				"7",
				"BULLETIN 007",
				"7/1/2018",
				"owner rev",
				"rev desc 1-cs000-006-xx1",
				"-1",
				"225501",

			};

			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"12",
				"7",
				".20",
				".07.00",
				"RFI 701",
				"1A A201",
				"TagVisible",
				"",
				"RFI 701 (BULLETIN 007)",
				"7/3/2018",
				"owner rev",
				"rev desc 1A A201 -007-301-1",
				"-1",
				"227383",
			};
			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"12",
				"7",
				".20",
				".07.00",
				"RFI 701",
				"1A A202",
				"TagVisible",
				"",
				"RFI 701 (BULLETIN 007)",
				"7/3/2018",
				"pcc",
				"rev desc 1A A202 -007-301-1",
				"-1",
				"227383",
			};
			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"12",
				"7",
				".20",
				".07.00",
				"RFI 701",
				"1A A202",
				"TagVisible",
				"",
				"RFI 701 (BULLETIN 007)",
				"7/3/2018",
				"rfi",
				"rev desc 1A A202 -007-301-1",
				"-1",
				"227383",
			};
			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"12",
				"7",
				".20",
				".07.00",
				"RFI 701",
				"1B A201",
				"TagVisible",
				"",
				"RFI 701 (BULLETIN 007)",
				"7/3/2018",
				"rfi",
				"rev desc 1B A201 -007-301-1",
				"-1",
				"226383",
			};
			data.Add(MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"12",
				"7",
				".20",
				".07.00",
				"RFI 701",
				"AA A2.20-201.10",
				"TagVisible",
				"",
				"RFI 701 (BULLETIN 007)",
				"7/3/2018",
				"rfi",
				"",
				"-1",
				"227383",
			};
			data.Add(MakeRevDataItem(Items));


			return data;
		}

		// translate the date read to the
		// correct data index
		// this the index to put the data into
		// that is, data item read first (the 0 slot)
		// gets placed into the "REV_SELECTED.DataIdx"
		// position in the array

		enum ERevitRevInfo
		{
			SEQ_NUM,
			REV_ID,
			ALT_ID,
			ISSUED,
			BLOCK_TITLE,
			DELTA_TITLE,
			REV_DATE,
			NUM_TYPE,
			VISIBILITY
		}

		private static void RevitRevInfo()
		{
			RevitRevisionInfo = new List<string[]>
			{
				new string[]
				{
					"1",
					"1",
					"1",
					"False",
					"BULLETIN 001",
					"BULLETIN 001",
					"1/1/2018",
					"Alphanumeric",
					"Hidden"
				},

				new string[]
				{
					"2",
					"",
					"1",
					"False",
					"ASI 007 (BULLETIN 001)",
					"ASI 007",
					"1/7/2018",
					"None",
					"TagVisible"
				},

				new string[]
				{
					"3",
					"",
					"1",
					"False",
					"ASI 008 (BULLETIN 001)",
					"ASI 008",
					"1/8/2018",
					"None",
					"CloudAndTagVisible"
				},

				new string[]
				{
					"4",
					"",
					"1",
					"False",
					"RFI 101 (BULLETIN 001)",
					"RFI 101",
					"1/9/2018",
					"None",
					"CloudAndTagVisible"
				},

				new string[]
				{
					"5",
					"3",
					"3",
					"False",
					"BULLETIN 003",
					"BULLETIN 003",
					"3/1/2018",
					"Alphanumeric",
					"Hidden"
				},

				new string[]
				{
					"6",
					"",
					"3",
					"False",
					"ASI 010 (BULLETIN 003)",
					"ASI 010",
					"3/10/2018",
					"None",
					"CloudAndTagVisible"
				},

				new string[]
				{
					"7",
					"",
					"3",
					"False",
					"ASI 012 (BULLETIN 003)",
					"ASI 012",
					"3/12/2018",
					"None",
					"CloudAndTagVisible"
				},

				new string[]
				{
					"8",
					"",
					"3",
					"False",
					"RFI 301 (BULLETIN 003)",
					"RFI 301",
					"3/13/2018",
					"None",
					"Hidden"
				},

				new string[]
				{
					"9",
					"5",
					"5",
					"False",
					"BULLETIN 005",
					"BULLETIN 005",
					"5/1/2018",
					"Alphanumeric",
					"CloudAndTagVisible"
				},

				new string[]
				{
					"10",
					"6",
					"6",
					"False",
					"BULLETIN 006",
					"BULLETIN 006",
					"6/1/2018",
					"Alphanumeric",
					"CloudAndTagVisible",
				},

				new string[]
				{
					"11",
					"",
					"6",
					"False",
					"ASI 013 (BULLETIN 006)",
					"ASI 013",
					"6/2/2018",
					"None",
					"CloudAndTagVisible",
				},

				new string[]
				{
					"12",
					"",
					"6",
					"False",
					"RFI 601 (BULLETIN 006)",
					"RFI 601",
					"6/3/2018",
					"None",
					"TagVisible"
				},
			};
		}

		#endregion
	}
}
