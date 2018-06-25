using System;

// base data collection
//RevisionDataFields
//    v    -> dynamic[]  (_revDataFields)
//    |
//    +---------------------------+------> RevisionDataMgr --> RevisioinSelect
//                                |
//RevisionData                    |
//    v    -> SortedList<string,  v
//    |               RevisionDataFields>         
//    +------------+         (_revisionData)
//                 |
//RevisionDataMgr  v
//        -> RevisionData (_masterRevData)
//        -> RevisionData (_preSelected)
//        -> RevisionData (_selected)
//     
//RevisionFilters
//-> FilterLists _filterLists
//                     ^
//            +--------+
//            ^
//        FilterLists
//        -> SortedList<FilterEnum, Filters> (_filterList)
//                                    ^
//           +------------------------+
//           ^
//        Filters
//        -> SortedList<int, Criteria> (_filters)
//
//RevisionEnums
//        -> List<RootEnum>           (RootList)
//        -> List<DescEnum>               (DescList)
//        -> List<DataEnum>           (DataList)
//        -> List<FilterEnum>         (FilterList)
//        -> List<MgmtEnum>           (MgmtList)
//        -> List<DataEnum>           (ColumnList)


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



