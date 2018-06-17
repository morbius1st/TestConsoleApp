using System;

using static TestConsoleApp.RevisionUtil;

namespace TestConsoleApp
{
	class RevisionSampleData
	{
		#region + Sample Revision Data

		// has two functions 
		// translate the data read to the correct format
		// basically nothing for most
		// put the data in the correct order
		private static RevisionDataFields MakeRevDataItem(dynamic[] items)
		{
			RevisionDataFields di = new RevisionDataFields();

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

		private static int[] xlate =
		{
			0, // 0
			1, // 1
			3, // 2
			4, // 3
			5, // 4
			6, // 5
			7, // 6
			8, // 7
			2, // 8
			9, // 9
			10, //10
			11, //11
			12, //12
			13, //13
			14, //14

		};

		public static RevisionData Init()
		{
			RevisionData data = new RevisionData();

			dynamic[] Items;

			Items = new dynamic[]
			{
				// order and translation order
				"False",		// 0 =>  0 // selected
				"1",			// 1 =>  1 // seq
				"1",			// 2 =>  3 // alt id
				".00",			// 3 =>  4 // type code
				".00.00",		// 4 =>  5 // disc code
				"BULLETIN 001",	// 5 =>  6 // delta title
				"CS000",		// 6 =>  7 // sht num
				"Hidden",		// 7 =>  8 // visibility
				"1",			// 8 =>  2 // rev id
				"BULLETIN 001",	// 9 =>  9 // block title
				"1/1/2018",		//10 => 10 // date
				"pcc",			//11 => 11 // basis
				"rev desc 1-cs000-000-1", //12 => 12 // desc
				"-1",			//13 => 13 // tag elem id
				"209594"		//14 => 14 // cloud elem id
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

		#endregion
	}
}
