using System;


namespace TestConsoleApp
{
	class Program
	{
		static Program me = new Program();

		static void Main(string[] args)
		{
			RevisionUtil.Process();

			Console.Write("Waiting:");
			Console.ReadKey();
		}

	}

}
