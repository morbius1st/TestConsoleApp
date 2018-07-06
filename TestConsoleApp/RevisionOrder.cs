#region + Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Xml;

using static TestConsoleApp.DataItems;
using static TestConsoleApp.RevisionUtil;


#endregion


// projname: TestConsoleApp
// itemname: RevisionOrder
// username: jeffs
// created:  7/1/2018 6:48:02 PM


// defines which columns to present and 
// which columns on which to sort the data
// 
namespace TestConsoleApp
{

	public abstract class RevColumns<T> : ICloneable
	{
		protected static RevColumns<T> _default;

		protected List<T> _columns { get; set; }

		public int Count => _columns.Count;

		protected abstract void SetDefault();

		public void Reset()
		{
			_columns = new List<T>(DataList.Count);
		}

		public void Start(params T[] cols)
		{
			Reset();

			Add(cols);
		}

		public void Add(params T[] cols)
		{
			if (cols == null || cols.Length == 0) return;

			foreach (T col in cols)
			{
				if (_columns.Contains(col)) continue;
				
				_columns.Add(col);
			}
		}

		public abstract IEnumerable<T> Iterate();

		public List<T> Clone()
		{
			List<T> copy = new List<T>();

			foreach (T column in _columns)
			{
				copy.Add(column);
			}

			return copy;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}
	} 

	public class RevColumnOrder : RevColumns<DataEnum>
	{
		public RevColumnOrder()
		{
			_columns = new List<DataEnum>(DataList.Count);
		}

		public static RevColumns<DataEnum> Default {
			get
			{
				if (_default == null) _default = new RevColumnOrder();

				return _default;
			}
			set => _default = value;
		}

		protected override void SetDefault()
		{
			if (Default == null) Default = new RevColumnOrder();

			if (Default.Count > 0)
			{
				_columns = Default.Clone();
			}
			else
			{
				if (_columns.Count == 0)
				{
					foreach (DataEnum de in DataList)
					{
						_columns.Add(de);
					}
				}
			}
		}

		public override IEnumerable<DataEnum> Iterate()
		{
			if (_columns == null || _columns.Count == 0) SetDefault();

			foreach (DataEnum de in _columns)
			{
				yield return de;
			}
		}

	}

	public class RevSortOrder : RevColumns<ISortable>
	{
		public RevSortOrder()
		{
			_columns = new List<ISortable>(DataList.Count);
		}

		public static RevColumns<ISortable> Default {
			get
			{
				if (_default == null) _default = new RevSortOrder();

				return _default;
			}
			set => _default = value;
		}

		protected override void SetDefault()
		{
			if (Default == null) Default = new RevSortOrder();

			if (Default.Count > 0)
			{
				_columns = Default.Clone();
			}
		}

		public override IEnumerable<ISortable> Iterate()
		{
			if (_columns == null || _columns.Count == 0) yield break;

			foreach (ISortable so in _columns)
			{
				yield return so;
			}
		}

	}

	public class RevOrderMgr
	{
		public RevColumnOrder ColumnOrder { get; set; } 
			= new RevColumnOrder();

		public RevSortOrder SortOrder { get; set; } 
			= new RevSortOrder();

	}



//
//	public class RevisionOrder<T> where T : DataEnum
//	{
//		public static RevisionOrder<DataEnum> 
//			DefaultColumns { get; private set; } = new RevisionOrder<DataEnum>();
//
//		private static RevisionOrder<ISortable> sort = new RevisionOrder<ISortable>();
//
//		private List<T> _columns;
//
//		public int Count => _columns.Count;
//
//		public RevisionOrder()
//		{
//			Reset();
//		}
//
//		public void SetDefault(params DataEnum[] de)
//		{
//			if (DefaultColumns.Count > 0) return;
//
//			foreach (DataEnum dataEnum in de)
//			{
//				DefaultColumns.Add(de);
//			}
//		}
//
////		public static void SetStdColumns(params DataEnum[] cols)
////		{
////			_columnsDefault = new List<DataEnum>(DataList.Count);
////
////			foreach (DataEnum de in cols)
////			{
////				if (_columnsDefault.Contains(de)) continue;
////
////				_columnsDefault.Add(de);
////			}
////		}
////
////		private void SetStdColumns()
////		{
////			if (_columnsDefault.Count > 0)
////			{
////				_columns = _columnsDefault.Clone();
////			}
////			else
////			{
////				foreach (DataEnum item in DataList)
////				{
////					_columns.Add(item);
////				}
////			}
////		}
//
//		public void Reset()
//		{
//			_columns = new List<T>(DataList.Count);
//		}
//
//
//		public void Start(params T[] cols)
//		{
//			Reset();
//
//			Add(cols);
//		}
//
//		public void Add(params T[] cols)
//		{
//			if (cols.Length == 0) return;
//
//			foreach (T de in cols)
//			{
//				if (!_columns.Contains(de))
//				{
//					_columns.Add(de);
//				}
//			}
//		}
//
//
//		public IEnumerable<T> IterateColumns()
//		{
//			if (_columns.Count == 0) SetStdColumns();
//
//			foreach (DataEnum de in _columns)
//			{
//				yield return de;
//			}
//		}
//	}
}
