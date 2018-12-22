using System;
using System.Collections;
using System.Collections.Generic;
using static RevisionTest.DataItems;
using static RevisionTest.DataItems.EDataFields;


namespace RevisionTest
{
	public interface SubData
	{
		string this[int idx] { get; set; }
	}

	public class RevOrderCode : IEquatable<RevOrderCode>, 
		IComparable<RevOrderCode>, ICloneable<RevOrderCode>, SubData
	{
		private string[] _revOrderCode = new string[3];

		public string AltId
		{
			get => _revOrderCode[REV_SUB_ALTID.SubDataIdx];
			set => _revOrderCode[REV_SUB_ALTID.SubDataIdx] = value;
		}
		public string TypeCode
		{
			get => _revOrderCode[REV_SUB_TYPE_CODE.SubDataIdx];
			set => _revOrderCode[REV_SUB_TYPE_CODE.SubDataIdx] = value;
		}
		public string DisciplineCode
		{
			get => _revOrderCode[REV_SUB_DISCIPLINE_CODE.SubDataIdx];
			set => _revOrderCode[REV_SUB_DISCIPLINE_CODE.SubDataIdx] = value;
		}

		public bool Equals(RevOrderCode other) => 
			ToString().Equals(other.ToString());

		public int CompareTo(RevOrderCode other) =>
			String.Compare(this.ToString(), other.ToString(), 
				StringComparison.Ordinal);

		public string this[int idx]
		{
			get => _revOrderCode[idx];
			set => _revOrderCode[idx] = value;
		}

		public RevOrderCode Clone()
		{
			RevOrderCode copy = new RevOrderCode();

			for (var i = 0; i < _revOrderCode.Length; i++)
			{
				copy[i] = _revOrderCode[i];
			}

			return copy;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		public override string ToString()
		{
			return AltId + TypeCode + DisciplineCode;
		}
	}

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
				if (_revDataFields[i] is RevOrderCode)
				{
					copy[i] = ((RevOrderCode) _revDataFields[i]).Clone();
				} 
				else
				{ 
					copy[i] = _revDataFields[i];
				}
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

		public dynamic this[DataItems.DataEnum item]
		{
			get => _revDataFields[item.DataIdx];
			set => _revDataFields[item.DataIdx] = value;
		}
		public dynamic this[DataItems.FilterEnum filter]
		{
			get => _revDataFields[filter.DataIdx];
			set => _revDataFields[filter.DataIdx] = value;
		}

		#endregion

		#region + Properties

		public int Count => _revDataFields.Length;

		public bool Selected
		{
			get => _revDataFields[REV_SELECTED.DataIdx];
			set => _revDataFields[REV_SELECTED.DataIdx] = value;
		}
		// read only
		public int Sequence
		{
			get => _revDataFields[REV_SEQ.DataIdx];
			set => _revDataFields[REV_SEQ.DataIdx] = value;
		}

		public RevOrderCode OrderCode
		{
			get =>  _revDataFields[REV_SORT_ORDER_CODE.DataIdx];
			set =>  _revDataFields[REV_SORT_ORDER_CODE.DataIdx] = value;
		}

		public string AltId
		{
			get => ((RevOrderCode) _revDataFields[REV_SORT_ORDER_CODE.DataIdx]).AltId;
			set => _revDataFields[REV_SORT_ORDER_CODE.DataIdx].AltId = value;
		}

		public string TypeCode
		{
			get => ((RevOrderCode) _revDataFields[REV_SORT_ORDER_CODE.DataIdx]).TypeCode;
			set => _revDataFields[REV_SORT_ORDER_CODE.DataIdx].TypeCode = value;
		}

		public string DisciplineCode
		{
			get => ((RevOrderCode) _revDataFields[REV_SORT_ORDER_CODE.DataIdx]).DisciplineCode;
			set => _revDataFields[REV_SORT_ORDER_CODE.DataIdx].DisciplineCode = value;
		}

		public string DeltaTitle
		{
			get => _revDataFields[ REV_SORT_DELTA_TITLE.DataIdx];
			set => _revDataFields[ REV_SORT_DELTA_TITLE.DataIdx] = value;
		}

		public string ShtNum
		{
			get => _revDataFields[ REV_SORT_SHEETNUM.DataIdx];
			set => _revDataFields[ REV_SORT_SHEETNUM.DataIdx] = value;
		}

		public RevisionVisibility Visibility
		{
			get => _revDataFields[ REV_ITEM_VISIBLE.DataIdx];
			set => _revDataFields[ REV_ITEM_VISIBLE.DataIdx] = value;
		}

		public string RevisionId
		{
			get => _revDataFields[ REV_SORT_ITEM_REVID.DataIdx];
			set => _revDataFields[ REV_SORT_ITEM_REVID.DataIdx] = value;
		}

		public string BlockTitle
		{
			get => _revDataFields[ REV_ITEM_BLOCK_TITLE.DataIdx];
			set => _revDataFields[ REV_ITEM_BLOCK_TITLE.DataIdx] = value;
		}

		public string RevisionDate
		{
			get => _revDataFields[ REV_ITEM_DATE.DataIdx];
			set => _revDataFields[ REV_ITEM_DATE.DataIdx] = value;
		}

		public string Basis
		{
			get => _revDataFields[ REV_SORT_ITEM_BASIS.DataIdx];
			set => _revDataFields[ REV_SORT_ITEM_BASIS.DataIdx] = value;
		}

		public ElementId TagElemId
		{
			get => _revDataFields[ REV_TAG_ELEM_ID.DataIdx];
			set => _revDataFields[ REV_TAG_ELEM_ID.DataIdx] = value;
		}

		public ElementId CloudElemId
		{
			get => _revDataFields[ REV_CLOUD_ELEM_ID.DataIdx];
			set => _revDataFields[ REV_CLOUD_ELEM_ID.DataIdx] = value;
		}
		
		public string Description
		{
			get => _revDataFields[ REV_SORT_ITEM_DESC.DataIdx];
			set => _revDataFields[ REV_SORT_ITEM_DESC.DataIdx] = value;
		}

		public int Order
		{
			get => _revDataFields[ REV_MGMT_RECORD_ID.DataIdx];
			set => _revDataFields[ REV_MGMT_RECORD_ID.DataIdx] = value;
		}

		public string SortKey
		{
			get => _revDataFields[ REV_SORT_KEY.DataIdx];
			set => _revDataFields[ REV_SORT_KEY.DataIdx] = value;
		}

		#endregion

	}
}
