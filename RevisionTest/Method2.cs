using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
	public static class Method2
	{

		public static Item Rev_Selected { get; } = new Item();
		public static Filter Rev_Seq { get; } = new Filter();
		public static Compare Rev_AltId { get; } = new Compare();
		public static Item Rev_TypeCode { get; } = new Item();

		public static Mgmt Rev_Column { get; } = new Mgmt();


		public static Compare[] Comparer = new Compare[]
		{
			Rev_AltId,
		};

		static Method2()
		{
			Rev_Selected.Name = "abc";

			Rev_Seq.Name = "seq";

			Rev_TypeCode.Name = "typecode";

		}


//		public class Mgmt : Desc
//		{
//			public Mgmt() : base()
//			{
//			}
//		}
//
//		public class Item : Item
//		{
//			public Item() : base()
//			{
//			}
//		}
//
//		public class Filter : Filter
//		{
//			public Filter() : base()
//			{
//			}
//		}
//
//		public class Compare : Compare
//		{
//			public Compare() : base()
//			{
//			}
//		}

		public class Desc
		{
			private static int a = 0;

			public string Name { get; set; }

			public int DescIdx { get; }

			public Desc()
			{
				DescIdx = a++;
			}
		}

		public class Mgmt : Desc
		{
		}

		public class Item : Desc
		{
			private static int a = 0;

			public int DataIdx { get; }

			public Item()
			{
				DataIdx = a++;
			}
		}

		public class Filter : Item
		{
			private static int a = 0;

			public int FilterIdx { get; }

			public Filter()
			{
				FilterIdx = a++;
			}
		}

		public class Compare : Filter
		{
			private static int a = 0;

			public int CompareIdx { get; }

			public Compare()
			{
				CompareIdx = a++;
			}
		}


	}
}
