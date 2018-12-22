using System;
using System.Drawing;

using static RevisionTest.RevisionMetaData;

namespace RevisionTest
{
	public class RevisionDataDisplay : ICloneable<RevisionDataDisplay>
	{
		private const int ColDefMargin = 1;

		// the margin for this item
		public int MarginLeft { get; set; } = ColDefMargin;
		public int MarginRight { get; set; } = ColDefMargin;

		// the maximum width based on the data read
		// updates the column width to be at least
		// the same width
		private int _dataWidth;
		public int DataWidth
		{
			get => _dataWidth;
			set
			{
				if (value > _dataWidth) _dataWidth = value;
				ColWidth = _dataWidth;
				KeyWidth = _dataWidth;
			}
		}


		// backing store
		private int _colWidth;
		// the column width - used to format the data
		// when used in a tabular column
		public int ColWidth
		{
			get => _colWidth;
			set
			{
				if (value > _colWidth) _colWidth = value;
			}
		}

		private int _keyWidth;
		// the width when used as a key - the larger of
		// a set value or the width of the date read
		public int KeyWidth
		{
			get => _keyWidth;
			set
			{
				if (value > _keyWidth) _keyWidth = value;
			}
		}

		// the title width for the titles
		// when on a single line
		public int TitleWidthFlat { get; private set; }
		// the maximum width of the title for
		// the property - this is another minimum
		// value
		public int TitleWidthStacked { get; private set; }
		// determine the maximum width of the 
		// title for this property
		// updates the column width to be at least
		// the same width
		public void SetWidthByTitle(DataItems.DescEnum de)
		{
			foreach (string s in de.Title)
			{
				TitleWidthStacked = 
					TitleWidthStacked > s.Length ? TitleWidthStacked : s.Length;
			}

			ColWidth = TitleWidthStacked;

			TitleWidthFlat = de.FullTitle.Length;
		}

		// the format string in which to format the data
		public string FormatString { get; set; }  

		// data justification in the column
		// left, right, or center
		public RevisionMetaData.Justification JustifyColumn { get; set; }
			
		// data justification when a key member
		// left, right, or center
		public RevisionMetaData.Justification JustifyKey { get; set; }
			
		// the font in which to format the data (not used)
		public Font Font { get; set; }            

		// defaults
		public RevisionDataDisplay()
		{
			DataWidth     = 5;
			FormatString = "";
			Font         = null;
		}

		public RevisionDataDisplay(int keyWidth,
			Font                       font,
			string                     formatString,
			RevisionMetaData.Justification              justifyColumn,
			RevisionMetaData.Justification              justifyKey)
		{
			ColWidth      = keyWidth;
			KeyWidth      = keyWidth;
			JustifyColumn = justifyColumn;
			JustifyKey    = justifyKey;
			FormatString  = formatString;
			Font          = font;
		}
				
		public RevisionDataDisplay Clone()
		{
			RevisionDataDisplay rd = new RevisionDataDisplay();
			rd._dataWidth = _dataWidth;
			rd._colWidth = _colWidth;
			rd._keyWidth = _keyWidth;
			rd.TitleWidthStacked = TitleWidthStacked;
			rd.FormatString = FormatString;
			rd.JustifyColumn = JustifyColumn;
			rd.JustifyKey = JustifyKey;
			rd.Font = new Font(Font, Font.Style);

			return null;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}


	}
}