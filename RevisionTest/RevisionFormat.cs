#region + Using Directives


using static RevisionTest.RevisionMetaData;


#endregion


// projname: TestConsoleApp
// itemname: RevisionFormat
// username: jeffs
// created:  6/30/2018 2:26:13 PM



// take the raw data and format depending on the usage

namespace RevisionTest
{
	public static class RevisionFormat
	{


		public static string Format(dynamic data, RevisionDataDisplay dd)
		{
			return Format(data, dd.FormatString);
		}
		
		public static string Format(dynamic data, string formatString)
		{
			return string.Format(formatString, data);
		}

		// this is the formatted item placed into
		// a left / center / right padded space
		// exactly ColWidth in size
		// and then the margins are added to
		// the left / right sides resp.
		public static string FormatForColumn(string value, 
			RevisionDataDisplay dd, 
			char left = ' ', char right = ' ', 
			RevisionMetaData.Justification justification = RevisionMetaData.Justification.UNDEFINED)
		{
			if (string.IsNullOrWhiteSpace(value)) value = "";

			string formatted;

			formatted = Abbreviate(value, dd.ColWidth, '…');

			if (justification == RevisionMetaData.Justification.UNDEFINED)
			{
				formatted = Justify(formatted, dd.JustifyColumn, dd.ColWidth, left, right);
			}
			else
			{
				formatted = Justify(formatted, justification, dd.ColWidth, left, right);
			}

			formatted = ApplyMargin(formatted, dd.MarginLeft, 
				dd.MarginRight, left, right);

			return formatted;
		}

		// this is the formatted item placed into
		// a left / center / right padded space
		// exactly KeyWidth in size
		// this gets no margins
		public static string FormatForKey(dynamic data, RevisionDataDisplay dd)
		{
			if (data == null) return null;

			string formatted = Abbreviate(Format(data, dd.FormatString), 
				dd.KeyWidth, (char) 0);

			formatted = Justify(formatted, dd.JustifyColumn, dd.KeyWidth);

			return formatted;
		}

		private static string Abbreviate(string value, int width, char ellipsis)
		{
			string result = value;
			string termination = ellipsis >= 32 ? " " + ellipsis : "";

			if (result.Length > width)
			{
				if (width > 2)
				{
					result = result.Substring(0, width - 2) + termination;
				}
				else
				{
					result = "?…";
				}
			}
			return result;

		}

		public static string Justify(string formatted, 
			RevisionMetaData.Justification justifyColumn, int width, char left = ' ', char right = ' ')
		{
			string result = formatted;

			switch (justifyColumn)
			{
			case RevisionMetaData.Justification.LEFT:
				{
					result = formatted.PadRight(width, left);
					break;
				}
			case RevisionMetaData.Justification.CENTER:
				{
					result = formatted.PadCenter(width, left, right);
					break;
				}
			case RevisionMetaData.Justification.RIGHT:
				{
					result = formatted.PadLeft(width, right);
					break;
				}
			}
			return result;
		}

		private static string ApplyMargin(string value,
			int marginLeft, int marginRight, char left = ' ', char right = ' ')
		{
			return new string(left, marginLeft) + value + new string(right, marginRight);
		}



	}
}
