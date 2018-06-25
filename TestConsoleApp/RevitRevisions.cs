using System;

using static TestConsoleApp.DataItems.EDataFields;

using static TestConsoleApp.RevisionUtil;

namespace TestConsoleApp
{
	class RevitRevisions
	{
		#region + Sample Revision Data

		private static int count = 0;

		public static RevisionData Read()
		{
			return Scan();
		}

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
				"rev desc 1-cs000-000-1", //12 => 10 // desc
				"-1",			//13 => 11 // tag elem id
				"209594"		//14 => 12 // cloud elem id
			};

			data.Add("> >1.00.00.00<>CS000 <>BULLETIN 001 <>0002<<",
				MakeRevDataItem(Items));

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

			data.Add("> >1.00.00.00<>CS000 <>BULLETIN 001 <>0004<<",
				MakeRevDataItem(Items));

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

			data.Add("> >1.00.00.00<>CS000 <>BULLETIN 001 <>0006<<",
				MakeRevDataItem(Items));

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

			data.Add("> >1.00.00.00<>CS100 <>BULLETIN 001 <>0000<<",
				MakeRevDataItem(Items));

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

			data.Add("> >1.10.00.00<>CS000 <>ASI 007 <>0013<<",
				MakeRevDataItem(Items));

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

			data.Add("> >1.10.00.00<>CS000 <>ASI 007 <>0014<<",
				MakeRevDataItem(Items));

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

			data.Add("> >1.10.00.00<>CS000 <>ASI 007 <>0015<<",
				MakeRevDataItem(Items));

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

			data.Add("> >1.10.00.00<>CS000 <>ASI 008 <>0065<<",
				MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"12",
				"6",
				".20",
				".07.00",
				"RFI 601",
				"1A A201",
				"TagVisible",
				"",
				"RFI 601 (BULLETIN 006)",
				"6/3/2018",
				"owner rev",
				"rev desc 1A A201 -006-301-1",
				"-1",
				"226383",
			};
			data.Add("> >6.20.07.00<>1A A201             <>RFI 601         <>0121<<",
				MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"12",
				"6",
				".20",
				".07.00",
				"RFI 601",
				"1A A202",
				"TagVisible",
				"",
				"RFI 601 (BULLETIN 006)",
				"6/3/2018",
				"pcc",
				"rev desc 1A A202 -006-301-1",
				"-1",
				"226383",
			};
			data.Add("> >6.20.07.00<>1A A202             <>RFI 601         <>0127<<",
				MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"12",
				"6",
				".20",
				".07.00",
				"RFI 601",
				"1A A202",
				"TagVisible",
				"",
				"RFI 601 (BULLETIN 006)",
				"6/3/2018",
				"rfi",
				"rev desc 1A A202 -006-301-1",
				"-1",
				"226383",
			};
			data.Add("> >6.20.07.00<>1A A202             <>RFI 601         <>0128<<",
				MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"12",
				"6",
				".20",
				".07.00",
				"RFI 601",
				"1B A201",
				"TagVisible",
				"",
				"RFI 601 (BULLETIN 006)",
				"6/3/2018",
				"rfi",
				"rev desc 1B A201 -006-301-1",
				"-1",
				"226383",
			};
			data.Add("> >6.20.07.00<>1B A201             <>RFI 601         <>0131<<",
				MakeRevDataItem(Items));

			Items = new dynamic[]
			{
				"False",
				"12",
				"6",
				".20",
				".07.00",
				"RFI 601",
				"AA A2.20-201.10",
				"TagVisible",
				"",
				"RFI 601 (BULLETIN 006)",
				"6/3/2018",
				"rfi",
				"",
				"-1",
				"226383",
			};
			data.Add("> >6.20.07.00<>AA A2.20-201.10     <>RFI 601         <>0141<<",
				MakeRevDataItem(Items));

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
			data.Add("> >6.10.07.00<>1A A201             <>ASI 013         <>0120<<",
				MakeRevDataItem(Items));

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
			data.Add("> >6.10.07.00<>1A A201             <>ASI 013         <>0123<<",
				MakeRevDataItem(Items));

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
			data.Add("> >6.10.07.00<>1A A202             <>ASI 013         <>0125<<",
				MakeRevDataItem(Items));

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
			data.Add("> >6.10.07.00<>1A A202             <>ASI 013         <>0126<<",
				MakeRevDataItem(Items));

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
			data.Add("> >6.10.07.00<>1B A201             <>ASI 013         <>0130<<",
				MakeRevDataItem(Items));

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
			data.Add("> >6.10.07.00<>1B A201             <>ASI 013         <>0133<<",
				MakeRevDataItem(Items));

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
			data.Add("> >6.10.07.00<>AA A2.20-201.10     <>ASI 013         <>0140<<",
				MakeRevDataItem(Items));

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
			data.Add("> >6.10.07.00<>AA A2.20-201.10     <>ASI 013         <>0143<<",
				MakeRevDataItem(Items));


			return data;
		}

		// translate the date read to the
		// correct data index
		// this the index to put the data into
		// that is, data item read first (the 0 slot)
		// gets placed into the "REV_SELECTED.DataIdx"
		// position in the array
		private static int[] xlate =
		{
			REV_SELECTED.DataIdx,					//  0
			REV_SEQ.DescItemIdx,					//  1
			REV_KEY_ORDER_CODE.DescItemIdx,			//  2 - ignore-+
			REV_SUB_TYPE_CODE.DescItemIdx,			//  3 - ignore +--> put into a RevOrderCode
			REV_SUB_DISCIPLINE_CODE.DescItemIdx,	//  4 - ignore-+
			REV_KEY_DELTA_TITLE.DescItemIdx,		//  5
			REV_KEY_SHEETNUM.DescItemIdx,			//  6
			REV_ITEM_VISIBLE.DescItemIdx,			//  7
			REV_ITEM_REVID.DescItemIdx,				//  8
			REV_ITEM_BLOCK_TITLE.DescItemIdx,		//  9
			REV_ITEM_DATE.DescItemIdx,				// 10
			REV_ITEM_BASIS.DescItemIdx,				// 11
			REV_ITEM_DESC.DescItemIdx,				// 12
			REV_TAG_ELEM_ID.DescItemIdx,			// 13
			REV_CLOUD_ELEM_ID.DescItemIdx,			// 14
		};

		// has two purposes: 
		// translate the data read to the correct format
		// basically nothing for most
		// put the data in the correct order
		private static RevisionDataFields MakeRevDataItem(dynamic[] items)
		{
			RevisionDataFields di = new RevisionDataFields();

			di.Order = count++;

			int len = items.Length;
			int last = len - 1;
			int nextToLast = len - 2;

			for (int i = 0; i < items.Length; i++)
			{
				int j = xlate[i];

				switch (i)
				{
				case 0:		// selected
					{
						di[j] = bool.Parse(items[i]);
						break;
					}
				case 2:
					{
						RevOrderCode rc = new RevOrderCode();
						rc.AltId = items[i++];
						rc.TypeCode = items[i++];
						rc.DisciplineCode = items[i];

						di[j] = rc;

						break;
					}
				case 1:		// sequence
				case 13:	// tag elem id
				case 14:	// cloud elem id
					{
						di[j] = Int32.Parse(items[i]);
						break;
					}
				case 7:		// visibility
					{
						di[j] = Enum.Parse(typeof(RevisionVisibility), items[i]);
						break;
					}
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
			}

			return di;
		}

		#endregion
	}
}
