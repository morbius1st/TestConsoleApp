using System;
using System.Collections.Generic;
using static TestConsoleApp.DataItems;
using static TestConsoleApp.DataItems.EDataFields;
using static TestConsoleApp.RevisionUtil;

namespace TestConsoleApp
{

	public class RevisionDataFields : ICloneable<RevisionDataFields>
	{
		// these fields represent the data items - this represents their
		// array position
		private dynamic[] _revDataFields;

		public RevisionDataFields()
		{
			_revDataFields = new dynamic[DataList.Count];
		}

		public IEnumerable<dynamic> Iterate()
		{
			foreach (dynamic d in _revDataFields)
			{
				yield return d;
			}
		}

		public RevisionDataFields Clone()
		{
			RevisionDataFields copy = new RevisionDataFields();

			for (int i = 0; i < _revDataFields.Length; i++)
			{
				copy[i] = _revDataFields[i];
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
			get => _revDataFields[idx];
			set => _revDataFields[idx] = value;
		}

		public dynamic this[DataEnum item]
		{
			get => _revDataFields[item.DataIdx];
			set => _revDataFields[item.DataIdx] = value;
		}
		public dynamic this[FilterEnum filter]
		{
			get => _revDataFields[filter.DataIdx];
			set => _revDataFields[filter.DataIdx] = value;
		}

		#endregion

		#region + Data Access

		public int? AsInt(DataEnum item)
		{
			if (item.Type != EDataType.INT) return null;

			return (int) _revDataFields[item.DataIdx];
		}

		public bool? AsBool(DataEnum item)
		{
			if (item.Type != EDataType.BOOL) return null;

			return (bool) _revDataFields[item.DataIdx];
		}

		public ElementId AsElementId(DataEnum item)
		{
			if (item.Type != EDataType.ELEMENTID) return null;

			return (ElementId) _revDataFields[item.DataIdx];
		}

		public RevisionVisibility? AsRevVisibility(DataEnum item)
		{
			if (item.Type != EDataType.VISIBILITY) return null;

			return (RevisionVisibility) _revDataFields[item.DataIdx];
		}

		public String AsString(DataEnum item)
		{
			if (item.Type != EDataType.STRING) return null;

			return (string) _revDataFields[item.DataIdx];
		}

		#endregion

		#region + Properties

		public int Count => _revDataFields.Length;

		public bool Selected
		{
			get => (bool) _revDataFields[REV_SELECTED.DataIdx];
			set => _revDataFields[REV_SELECTED.DataIdx] = value;
		}
		// read only
		public int Sequence
		{
			get => (int) _revDataFields[REV_SEQ.DataIdx];
			set => _revDataFields[REV_SEQ.DataIdx] = value;
		}

		public string AltId
		{
			get => (string) _revDataFields[REV_KEY_ALTID.DataIdx];
			set => _revDataFields[REV_KEY_ALTID.DataIdx] = value;
		}

		public string TypeCode
		{
			get => (string) _revDataFields[REV_KEY_TYPE_CODE.DataIdx];
			set => _revDataFields[REV_KEY_TYPE_CODE.DataIdx] = value;
		}

		public string DisciplineCode
		{
			get => (string) _revDataFields[REV_KEY_DISCIPLINE_CODE.DataIdx];
			set => _revDataFields[REV_KEY_DISCIPLINE_CODE.DataIdx] = value;
		}

		public string DeltaTitle
		{
			get => (string) _revDataFields[ REV_KEY_DELTA_TITLE.DataIdx];
			set => _revDataFields[ REV_KEY_DELTA_TITLE.DataIdx] = value;
		}

		public string ShtNum
		{
			get => (string) _revDataFields[ REV_KEY_SHEETNUM.DataIdx];
			set => _revDataFields[ REV_KEY_SHEETNUM.DataIdx] = value;
		}

		public RevisionVisibility Visibility
		{
			get => (RevisionVisibility) _revDataFields[ REV_ITEM_VISIBLE.DataIdx];
			set => _revDataFields[ REV_ITEM_VISIBLE.DataIdx] = value;
		}

		public string RevisionId
		{
			get => (string) _revDataFields[ REV_ITEM_REVID.DataIdx];
			set => _revDataFields[ REV_ITEM_REVID.DataIdx] = value;
		}

		public  string BlockTitle
		{
			get => (string) _revDataFields[ REV_ITEM_BLOCK_TITLE.DataIdx];
			set => _revDataFields[ REV_ITEM_BLOCK_TITLE.DataIdx] = value;
		}

		public  string RevisionDate
		{
			get => (string) _revDataFields[ REV_ITEM_DATE.DataIdx];
			set => _revDataFields[ REV_ITEM_DATE.DataIdx] = value;
		}

		public  string Basis
		{
			get => (string) _revDataFields[ REV_ITEM_BASIS.DataIdx];
			set => _revDataFields[ REV_ITEM_BASIS.DataIdx] = value;
		}

		public  ElementId TagElemId
		{
			get => (ElementId) _revDataFields[ REV_TAG_ELEM_ID.DataIdx];
			set => _revDataFields[ REV_TAG_ELEM_ID.DataIdx] = value;
		}

		public  ElementId CloudElemId
		{
			get => (ElementId) _revDataFields[ REV_CLOUD_ELEM_ID.DataIdx];
			set => _revDataFields[ REV_CLOUD_ELEM_ID.DataIdx] = value;
		}
		public  string Description
		{
			get => (string) _revDataFields[ REV_ITEM_DESC.DataIdx];
			set => _revDataFields[ REV_ITEM_DESC.DataIdx] = value;
		}

		#endregion

	}
}
