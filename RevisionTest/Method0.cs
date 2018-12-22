using System;

using static TestConsoleApp.test;


namespace TestConsoleApp
{
	class Method0
	{
		public void Test()
		{
			a.value = 1;
			a.valA = 1;

			b.value = 1;
			b.valA = 1;

			d.valA = 1;
			d.valB = 1;

			z.valA = 1;
			z.valB = 1;

			Console.WriteLine(a.GetType().ToString());
			Console.WriteLine(b.GetType().ToString());
			Console.WriteLine(d.GetType().ToString());
			Console.WriteLine(z.GetType().ToString());

//			Console.WriteLine(me.test1(a));
//			Console.WriteLine(me.test2(z));
//			Console.WriteLine(me.test3(b));


			Console.WriteLine("is z an A  (yes) " + (z is A).ToString());
			Console.WriteLine("is z an AB (yes) " + (z is AB).ToString());
			Console.WriteLine("is z an AC  (no) " + (z is AC).ToString());
			Console.WriteLine("is z an B  (yes) " + (z is B).ToString());
		}


		private string test1(A a)
		{
			return a.valA.ToString();
		}

		private string test2(B b)
		{
			return b.valB.ToString();
		}

		private string test3(AB ab)
		{
			return ab.valA.ToString();
		}
	}

	interface A
	{
		int valA { get; set; }
	}

	interface AB : A
	{

	}

	interface AC : A
	{
	}

	interface B
	{
		int valB { get; set; }
	}

	public static class test
	{

		public static alpha a = new alpha();
		public static beta b = new beta();
		public static delta d = new delta();
		public static zeta z = new zeta();

		public class alpha : AC
		{
			public int value { get; set; }
			public int valA { get; set; }
		}

		public class beta : AB
		{
			public int value { get; set; }
			public int valA { get; set; }
		}

		public class delta : A, B
		{
			public int valA { get; set; }
			public int valB { get; set; }
		}

		public class zeta : A, B, AB
		{
			public int valA { get; set; }
			public int valB { get; set; }
		}
	}
}
