using System.Collections.Generic;
using System.Reflection;
using System;

namespace TestConsoleApp
{

	public static class DataEnumsB
	{
		public static Item		REV_SELECTED            { get; } = new Item();
		public static Filter	REV_SEQ                 { get; } = new Filter();
		public static Compare	REV_KEY_ALTID           { get; } = new Compare();
		public static Item		REV_KEY_TYPE_CODE       { get; } = new Item();
		public static Item		REV_KEY_DISCIPLINE_CODE { get; } = new Item();
		public static Compare	REV_KEY_DELTA_TITLE     { get; } = new Compare();
		public static Compare	REV_KEY_SHEETNUM        { get; } = new Compare();
		public static Filter	REV_ITEM_VISIBLE        { get; } = new Filter();
		public static Compare	REV_ITEM_REVID          { get; } = new Compare();
		public static Compare	REV_ITEM_BLOCK_TITLE    { get; } = new Compare();
		public static Item		REV_ITEM_DATE           { get; } = new Item();
		public static Compare	REV_ITEM_BASIS          { get; } = new Compare();
		public static Filter	REV_ITEM_DESC           { get; } = new Filter();
		public static Item		REV_TAG_ELEM_ID         { get; } = new Item();
		public static Item		REV_CLOUD_ELEM_ID       { get; } = new Item();
		public static Mgmt		REV_MGMT_COLUMN         { get; } = new Mgmt();
		public static Mgmt		REV_KEY                 { get; } = new Mgmt();

		static DataEnumsB()
		{
//			REV_SELECTED           .Name = nameof(REV_SELECTED);
//			REV_SEQ                .Name = nameof(REV_SEQ);
//			REV_KEY_ALTID          .Name = nameof(REV_KEY_ALTID);
//			REV_KEY_TYPE_CODE      .Name = nameof(REV_KEY_TYPE_CODE);
//			REV_KEY_DISCIPLINE_CODE.Name = nameof(REV_KEY_DISCIPLINE_CODE);
//			REV_KEY_DELTA_TITLE    .Name = nameof(REV_KEY_DELTA_TITLE);
//			REV_KEY_SHEETNUM       .Name = nameof(REV_KEY_SHEETNUM);
//			REV_ITEM_VISIBLE       .Name = nameof(REV_ITEM_VISIBLE);
//			REV_ITEM_REVID         .Name = nameof(REV_ITEM_REVID);
//			REV_ITEM_BLOCK_TITLE   .Name = nameof(REV_ITEM_BLOCK_TITLE);
//			REV_ITEM_DATE          .Name = nameof(REV_ITEM_DATE);
//			REV_ITEM_BASIS         .Name = nameof(REV_ITEM_BASIS);
//			REV_ITEM_DESC          .Name = nameof(REV_ITEM_DESC);
//			REV_TAG_ELEM_ID        .Name = nameof(REV_TAG_ELEM_ID);
//			REV_CLOUD_ELEM_ID      .Name = nameof(REV_CLOUD_ELEM_ID);
//			REV_MGMT_COLUMN        .Name = nameof(REV_MGMT_COLUMN);
//			REV_KEY                .Name = nameof(REV_KEY);


			foreach (PropertyInfo p in typeof(DataItems).GetProperties())
			{
				Root r = (Root) p.GetValue(null);

				r.Name = p.Name;

				RootList.Add(r);

				if (r is Mgmt)
				{
					MgmtList.Add((Mgmt) r);
				}

				if (r is Desc)
				{
					Descriptions.Add((Desc) r);
				}

				if (r is Item)
				{
					Items.Add((Item) r);
				}

				if (r is Filter)
				{
					Filters.Add((Filter) r);
				}

				if (r is Compare)
				{
					Comparisons.Add((Compare) r);
				}

			}

			REV_SELECTED.Column = 1;
			REV_SEQ					  .Column = 1;
			REV_KEY_ALTID			  .Column = 1;
			REV_KEY_TYPE_CODE		  .Column = 1;
			REV_KEY_DISCIPLINE_CODE	  .Column = 1;
			REV_KEY_DELTA_TITLE		  .Column = 1;
			REV_KEY_SHEETNUM		  .Column = 1;
			REV_ITEM_VISIBLE		  .Column = 1;
			REV_ITEM_REVID			  .Column = 1;
			REV_ITEM_BLOCK_TITLE	  .Column = 1;
			REV_ITEM_DATE			  .Column = 1;
			REV_ITEM_BASIS			  .Column = 1;
			REV_ITEM_DESC			  .Column = 1;
			REV_TAG_ELEM_ID			  .Column = 1;
			REV_CLOUD_ELEM_ID		  .Column = 1;
//			REV_MGMT_COLUMN
//			REV_KEY              
		}

		public static List<Root> RootList = new List<Root>(17);
		public static List<Desc> Descriptions = new List<Desc>(17);
		public static List<Item> Items = new List<Item>(17);
		public static List<Filter> Filters = new List<Filter>(17);
		public static List<Compare> Comparisons = new List<Compare>(17);
		public static List<Mgmt> MgmtList = new List<Mgmt>(17);


		public static void NameCheck()
		{
			Console.WriteLine("name check| REV_SELECTED           | " + REV_SELECTED .Name);
			Console.WriteLine("name check| REV_SEQ                | " + REV_SEQ .Name);
			Console.WriteLine("name check| REV_KEY_ALTID          | " + REV_KEY_ALTID .Name);
			Console.WriteLine("name check| REV_KEY_TYPE_CODE      | " + REV_KEY_TYPE_CODE .Name);
			Console.WriteLine("name check| REV_KEY_DISCIPLINE_CODE| " + REV_KEY_DISCIPLINE_CODE .Name);
			Console.WriteLine("name check| REV_KEY_DELTA_TITLE    | " + REV_KEY_DELTA_TITLE .Name);
			Console.WriteLine("name check| REV_KEY_SHEETNUM       | " + REV_KEY_SHEETNUM .Name);
			Console.WriteLine("name check| REV_ITEM_VISIBLE       | " + REV_ITEM_VISIBLE .Name);
			Console.WriteLine("name check| REV_ITEM_REVID         | " + REV_ITEM_REVID .Name);
			Console.WriteLine("name check| REV_ITEM_BLOCK_TITLE   | " + REV_ITEM_BLOCK_TITLE .Name);
			Console.WriteLine("name check| REV_ITEM_DATE          | " + REV_ITEM_DATE .Name);
			Console.WriteLine("name check| REV_ITEM_BASIS         | " + REV_ITEM_BASIS .Name);
			Console.WriteLine("name check| REV_ITEM_DESC          | " + REV_ITEM_DESC .Name);
			Console.WriteLine("name check| REV_TAG_ELEM_ID        | " + REV_TAG_ELEM_ID .Name);
			Console.WriteLine("name check| REV_CLOUD_ELEM_ID      | " + REV_CLOUD_ELEM_ID .Name);
			Console.WriteLine("name check| REV_MGMT_COLUMN        | " + REV_MGMT_COLUMN .Name);
			Console.WriteLine("name check| REV_KEY                | " + REV_KEY .Name);
		}

		public class Root
		{
			public string Name { get; set; }
		}

		public class Desc : Root
		{
			private static int a = 0;

			public Desc()
			{
				DescItemIdx = a++;
			}

			public int DescItemIdx { get; }

			public int Column { get; set; }
		}

		public class Mgmt : Root
		{
		}


		public class Item : Desc
		{
			private static int b = 0;

			public Item()
			{
				DataItemIdx = b++;
			}

			public int DataItemIdx { get; }
		}

		public class Filter : Item
		{
			private static int c = 0;

			public Filter()
			{
				FilterIdx = c++;
			}

			public int FilterIdx { get; }
		}

		public class Compare : Filter
		{
			private static int d = 0;

			public Compare() : base()
			{
				CompareIdx = d++;
			}

			public int CompareIdx { get; }
		}
	}
}

//
//	public static class DataEnums
//	{
//		public static Item		REV_SELECTED            { get; } = new Item();
//		public static Filter	REV_SEQ                 { get; } = new Filter();
//		public static Compare	REV_KEY_ALTID           { get; } = new Compare();
//		public static Item		REV_KEY_TYPE_CODE       { get; } = new Item();
//		public static Item		REV_KEY_DISCIPLINE_CODE { get; } = new Item();
//		public static Compare	REV_KEY_DELTA_TITLE     { get; } = new Compare();
//		public static Compare	REV_KEY_SHEETNUM        { get; } = new Compare();
//		public static Filter	REV_ITEM_VISIBLE        { get; } = new Filter();
//		public static Compare	REV_ITEM_REVID          { get; } = new Compare();
//		public static Compare	REV_ITEM_BLOCK_TITLE    { get; } = new Compare();
//		public static Item		REV_ITEM_DATE           { get; } = new Item();
//		public static Compare	REV_ITEM_BASIS          { get; } = new Compare();
//		public static Filter	REV_ITEM_DESC           { get; } = new Filter();
//		public static Item		REV_TAG_ELEM_ID         { get; } = new Item();
//		public static Item		REV_CLOUD_ELEM_ID       { get; } = new Item();
//		public static Mgmt		REV_MGMT_COLUMN         { get; } = new Mgmt();
//		public static Mgmt		REV_KEY                 { get; } = new Mgmt();
//
//		static DataEnums()
//		{
////			REV_SELECTED           .Name = nameof(REV_SELECTED);
////			REV_SEQ                .Name = nameof(REV_SEQ);
////			REV_KEY_ALTID          .Name = nameof(REV_KEY_ALTID);
////			REV_KEY_TYPE_CODE      .Name = nameof(REV_KEY_TYPE_CODE);
////			REV_KEY_DISCIPLINE_CODE.Name = nameof(REV_KEY_DISCIPLINE_CODE);
////			REV_KEY_DELTA_TITLE    .Name = nameof(REV_KEY_DELTA_TITLE);
////			REV_KEY_SHEETNUM       .Name = nameof(REV_KEY_SHEETNUM);
////			REV_ITEM_VISIBLE       .Name = nameof(REV_ITEM_VISIBLE);
////			REV_ITEM_REVID         .Name = nameof(REV_ITEM_REVID);
////			REV_ITEM_BLOCK_TITLE   .Name = nameof(REV_ITEM_BLOCK_TITLE);
////			REV_ITEM_DATE          .Name = nameof(REV_ITEM_DATE);
////			REV_ITEM_BASIS         .Name = nameof(REV_ITEM_BASIS);
////			REV_ITEM_DESC          .Name = nameof(REV_ITEM_DESC);
////			REV_TAG_ELEM_ID        .Name = nameof(REV_TAG_ELEM_ID);
////			REV_CLOUD_ELEM_ID      .Name = nameof(REV_CLOUD_ELEM_ID);
////			REV_MGMT_COLUMN        .Name = nameof(REV_MGMT_COLUMN);
////			REV_KEY                .Name = nameof(REV_KEY);
//
//
//			foreach (PropertyInfo p in typeof(DataEnums).GetProperties())
//			{
//				DescItem o = (DescItem) p.GetValue(null);
//
//				o.Name = p.Name;
//
////				Console.WriteLine("??| " + o.ToString());
//			}
//		}
//
//
//		public static DescItem[] Descriptions = new DescItem[]
//		{
//			REV_SELECTED ,
//			REV_SEQ ,
//			REV_KEY_ALTID ,
//			REV_KEY_TYPE_CODE ,
//			REV_KEY_DISCIPLINE_CODE ,
//			REV_KEY_DELTA_TITLE ,
//			REV_KEY_SHEETNUM ,
//			REV_ITEM_VISIBLE ,
//			REV_ITEM_REVID ,
//			REV_ITEM_BLOCK_TITLE ,
//			REV_ITEM_DATE ,
//			REV_ITEM_BASIS ,
//			REV_ITEM_DESC ,
//			REV_TAG_ELEM_ID ,
//			REV_CLOUD_ELEM_ID ,
//			REV_MGMT_COLUMN ,
//			REV_KEY ,
//		};
//
//		public static DataItem[] DataItems = new DataItem[]
//		{
//			REV_SELECTED ,
//			REV_SEQ ,
//			REV_KEY_ALTID ,
//			REV_KEY_TYPE_CODE ,
//			REV_KEY_DISCIPLINE_CODE ,
//			REV_KEY_DELTA_TITLE ,
//			REV_KEY_SHEETNUM ,
//			REV_ITEM_VISIBLE ,
//			REV_ITEM_REVID ,
//			REV_ITEM_BLOCK_TITLE ,
//			REV_ITEM_DATE ,
//			REV_ITEM_BASIS ,
//			REV_ITEM_DESC ,
//			REV_TAG_ELEM_ID ,
//			REV_CLOUD_ELEM_ID ,
//
//		};
//
//		public static FilterItem[] Filters = new FilterItem[]
//		{
//			REV_SEQ ,
//			REV_KEY_ALTID ,
//			REV_KEY_DELTA_TITLE ,
//			REV_KEY_SHEETNUM ,
//			REV_ITEM_VISIBLE ,
//			REV_ITEM_REVID ,
//			REV_ITEM_BLOCK_TITLE ,
//			REV_ITEM_BASIS ,
//			REV_ITEM_DESC ,
//		};
//
//		public static CompareItem[] Comparisons = new CompareItem[]
//		{
//			REV_KEY_ALTID ,
//			REV_KEY_DELTA_TITLE ,
//			REV_KEY_SHEETNUM ,
//			REV_ITEM_REVID ,
//			REV_ITEM_BLOCK_TITLE ,
//			REV_ITEM_BASIS ,
//		};
//
//		public class Mgmt : DescItem, mgmt
//		{
//			public Mgmt() : base()
//			{
//
//			}
//		}
//
//		public class Item : DataItem
//		{
//			public Item() : base()
//			{
//			}
//		}
//
//		public class Filter : FilterItem
//		{
//			public Filter() : base()
//			{
//			}
//		}
//
//		public class Compare : CompareItem
//		{
//			public Compare() : base()
//			{
//			}
//		}
//	}
//
//	public interface descItem
//	{
//		int DescItemIdx { get; }
//	}
//
//	public interface dataItem : descItem
//	{
//		int DataItemIdx { get; }
//	}
//
//	public interface item : dataItem
//	{
//	}
//
//	public interface mgmt : descItem
//	{
//	}
//
//	public interface filterItem : dataItem
//	{
//		int FilterIdx { get; }
//	}
//
//	public interface compareItem : filterItem
//	{
//		int CompareIdx { get; }
//	}
//
//	public abstract class DescItem : descItem
//	{
//		private static int a = 0;
//
//		public string Name { get; set; }
//
//		public DescItem()
//		{
//			DescItemIdx = a++;
//		}
//
//		public int DescItemIdx { get; }
//	}
//
//
//	public abstract class DataItem : DescItem, item
//	{
//		private static int b = 0;
//
//		public DataItem()
//		{
//			DataItemIdx = b++;
//		}
//
//		public int DataItemIdx { get; }
//	}
//
//	public abstract class FilterItem : DataItem , filterItem
//	{
//		private static int c = 0;
//
//		public FilterItem()
//		{
//			FilterIdx = c++;
//		}
//
//		public int FilterIdx { get; }
//	}
//
//	public abstract class CompareItem : FilterItem , compareItem
//	{
//		private static int d = 0;
//
//		public CompareItem() : base()
//		{
//			CompareIdx = d++;
//		}
//
//		public int CompareIdx { get; }
//	}
