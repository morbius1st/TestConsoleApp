﻿#region + Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Xml;

using static RevisionTest.DataItems;
using static RevisionTest.RevisionUtil;


#endregion


// projname: TestConsoleApp
// itemname: RevisionOrder
// username: jeffs
// created:  7/1/2018 6:48:02 PM


// defines which columns to present and 
// which columns on which to sort the data
// 

namespace RevisionTest
{
	public class RevOrderMgr
	{
		public RevColumns<DataItems.DataEnum, RevColumnOrder> 
			DefaultColumnOrder => RevColumnOrder.Default;

		public RevColumns<DataItems.ISortable, RevSortOrder> 
			DefaultSortOrder => RevSortOrder.Default;

		public RevColumnOrder ColumnOrder { get; set; } 
			= new RevColumnOrder();

		public RevSortOrder SortOrder { get; set; } 
			= new RevSortOrder();

	}

	public interface IRevOrder
	{
		int Count { get; }

		IEnumerable itemize();
	}

	public abstract class RevColumns<T, U> : ICloneable<U>, IRevOrder
		where U : RevColumns<T, U>, new()
	{
		protected static U _default;

		public List<T> Columns { get; protected set; }

		public int Count => Columns.Count;

		public virtual U Default { get; set; }

		protected abstract void SetToDefault();
//		{
//			Columns = Default.Columns.Clone();
//		}

		public void Reset()
		{
			Columns = new List<T>(DataList.Count);
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
				if (Columns.Contains(col)) continue;
				
				Columns.Add(col);
			}
		}

		public abstract IEnumerable<T> Iterate();

		public IEnumerable itemize()
		{
			return Iterate();
		}

		public U Clone()
		{
			U copy = new U();

			copy.Columns = Columns.Clone();

			return copy;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}
	} 

	public class RevColumnOrder : 
		RevColumns<DataItems.DataEnum, RevColumnOrder>
	{
		public RevColumnOrder()
		{
			Columns = new List<DataItems.DataEnum>(DataList.Count);
		}

		public new static RevColumnOrder Default {
			get
			{
				if (_default == null) _default = Settings.Setg.DefaultColumnOrder;
				return _default;
			}
			set => _default = value;
		}

		protected override void SetToDefault()
		{
			Columns = Default.Columns.Clone();
		}

		public override IEnumerable<DataItems.DataEnum> Iterate()
		{
			if (Columns == null || Columns.Count == 0) SetToDefault();

			foreach (DataItems.DataEnum de in Columns)
			{
				yield return de;
			}
		}
	}

	public class RevSortOrder : 
		RevColumns<DataItems.ISortable, RevSortOrder>
	{
		public RevSortOrder()
		{
			Columns = new List<DataItems.ISortable>(DataList.Count);
		}

		public new static RevSortOrder Default {
			get
			{
				if (_default == null) _default = Settings.Setg.DefaultSortOrder;
				return _default;
			}
			set => _default = value;
		}
		
		protected override void SetToDefault()
		{
			Columns = Default.Columns.Clone();
		}

		public override IEnumerable<DataItems.ISortable> Iterate()
		{
			if (Columns == null || Columns.Count == 0) SetToDefault();

			foreach (DataItems.ISortable so in Columns)
			{
				yield return so;
			}
		}
	}

}
