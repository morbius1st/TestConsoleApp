using System;
using System.Collections.Generic;
using static TestConsoleApp.DataItems;
using static TestConsoleApp.DataItems.EDataFields;
using static TestConsoleApp.RevisionUtil;

namespace TestConsoleApp
{
	public interface ICloneable<T> : ICloneable where T : ICloneable<T>
	{
		new T Clone();
	}

	public class RevDataItems2 : ICloneable<RevDataItems2>
	{

		// these fields represent the data items - this represents their
		// array position

		private dynamic[] _revDataItems2;

		public RevDataItems2()
		{
			_revDataItems2 = new dynamic[DataList.Count];
		}

		public IEnumerable<dynamic> Iterate()
		{
			foreach (dynamic d in _revDataItems2)
			{
				yield return d;
			}
		}

		public RevDataItems2 Clone()
		{
			RevDataItems2 copy = new RevDataItems2();

			for (int i = 0; i < _revDataItems2.Length; i++)
			{
				copy[i] = _revDataItems2[i];
			}
			return copy;
		}
		
		object ICloneable.Clone()
		{
			return Clone();
		}

		#region + Indexers

		public dynamic this[int idx]
		{
			get => _revDataItems2[idx];
			set => _revDataItems2[idx] = value;
		}

		public dynamic this[DataEnum item]
		{
			get => _revDataItems2[item.DataIdx];
			set => _revDataItems2[item.DataIdx] = value;
		}
		public dynamic this[FilterEnum filter]
		{
			get => _revDataItems2[filter.DataIdx];
			set => _revDataItems2[filter.DataIdx] = value;
		}

		#endregion

		#region + Data Access

		public int? AsInt(DataEnum item)
		{
			if (item.Type != EDataType.INT) return null;

			return (int) _revDataItems2[item.DataIdx];
		}

		public bool? AsBool(DataEnum item)
		{
			if (item.Type != EDataType.BOOL) return null;

			return (bool) _revDataItems2[item.DataIdx];
		}

		public ElementId AsElementId(DataEnum item)
		{
			if (item.Type != EDataType.ELEMENTID) return null;

			return (ElementId) _revDataItems2[item.DataIdx];
		}

		public RevisionVisibility? AsRevVisibility(DataEnum item)
		{
			if (item.Type != EDataType.VISIBILITY) return null;

			return (RevisionVisibility) _revDataItems2[item.DataIdx];
		}

		public String AsString(DataEnum item)
		{
			if (item.Type != EDataType.STRING) return null;

			return (string) _revDataItems2[item.DataIdx];
		}

		#endregion

		#region + Properties

		public int Count => _revDataItems2.Length;

		public bool Selected
		{
			get => (bool) _revDataItems2[REV_SELECTED.DataIdx];
			set => _revDataItems2[REV_SELECTED.DataIdx] = value;
		}
		// read only
		public int Sequence
		{
			get => (int) _revDataItems2[REV_SEQ.DataIdx];
			set => _revDataItems2[REV_SEQ.DataIdx] = value;
		}

		public string AltId
		{
			get => (string) _revDataItems2[REV_KEY_ALTID.DataIdx];
			set => _revDataItems2[REV_KEY_ALTID.DataIdx] = value;
		}

		public string TypeCode
		{
			get => (string) _revDataItems2[REV_KEY_TYPE_CODE.DataIdx];
			set => _revDataItems2[REV_KEY_TYPE_CODE.DataIdx] = value;
		}

		public string DisciplineCode
		{
			get => (string) _revDataItems2[REV_KEY_DISCIPLINE_CODE.DataIdx];
			set => _revDataItems2[REV_KEY_DISCIPLINE_CODE.DataIdx] = value;
		}

		public string DeltaTitle
		{
			get => (string) _revDataItems2[ REV_KEY_DELTA_TITLE.DataIdx];
			set => _revDataItems2[ REV_KEY_DELTA_TITLE.DataIdx] = value;
		}

		public string ShtNum
		{
			get => (string) _revDataItems2[ REV_KEY_SHEETNUM.DataIdx];
			set => _revDataItems2[ REV_KEY_SHEETNUM.DataIdx] = value;
		}

		public RevisionVisibility Visibility
		{
			get => (RevisionVisibility) _revDataItems2[ REV_ITEM_VISIBLE.DataIdx];
			set => _revDataItems2[ REV_ITEM_VISIBLE.DataIdx] = value;
		}

		public string RevisionId
		{
			get => (string) _revDataItems2[ REV_ITEM_REVID.DataIdx];
			set => _revDataItems2[ REV_ITEM_REVID.DataIdx] = value;
		}

		public  string BlockTitle
		{
			get => (string) _revDataItems2[ REV_ITEM_BLOCK_TITLE.DataIdx];
			set => _revDataItems2[ REV_ITEM_BLOCK_TITLE.DataIdx] = value;
		}

		public  string RevisionDate
		{
			get => (string) _revDataItems2[ REV_ITEM_DATE.DataIdx];
			set => _revDataItems2[ REV_ITEM_DATE.DataIdx] = value;
		}

		public  string Basis
		{
			get => (string) _revDataItems2[ REV_ITEM_BASIS.DataIdx];
			set => _revDataItems2[ REV_ITEM_BASIS.DataIdx] = value;
		}

		public  ElementId TagElemId
		{
			get => (ElementId) _revDataItems2[ REV_TAG_ELEM_ID.DataIdx];
			set => _revDataItems2[ REV_TAG_ELEM_ID.DataIdx] = value;
		}

		public  ElementId CloudElemId
		{
			get => (ElementId) _revDataItems2[ REV_CLOUD_ELEM_ID.DataIdx];
			set => _revDataItems2[ REV_CLOUD_ELEM_ID.DataIdx] = value;
		}
		public  string Description
		{
			get => (string) _revDataItems2[ REV_ITEM_DESC.DataIdx];
			set => _revDataItems2[ REV_ITEM_DESC.DataIdx] = value;
		}

		#endregion

	}
}
