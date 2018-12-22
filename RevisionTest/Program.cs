using System;

// base data collection
//RevisionDataFields
//    v    -> dynamic[]  (_revDataFields)
//    |
//    +--------------------+------> RevisionDataMgr --> RevisioinSelect
//                         |
//RevisionData             v
//    v    -> List<RevisionDataFields>         
//    +------------+         (_revisionData)
//                 |
//RevisionDataMgr  v
//                        // the complete list of revit revision data, etc.
//        -> RevisionData (_masterRevData) 
//                        // a copy of the master date & a reduced set 
//                        // of revision data when selecting from a 
//                        // prior selection set
//        -> RevisionData (_preSelected)   
//                        // the selected data
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
//        -> List<DescEnum>           (DescList)
//        -> List<DataEnum>           (DataList)
//        -> List<FilterEnum>         (FilterList)
//
//        -> List<DataEnum>           (ColumnList)



// Complete. next step is integrate info revit //

!// Todo: Complete. next step is integrate info revit.

namespace RevisionTest
{
	class Program
	{
		static Program me = new Program();

		static void Main(string[] args)
		{
			RevisionTest.Process();

			Console.Write("Waiting:");
			Console.ReadKey();
		}

	}

}



