#region + Using Directives


using static TestConsoleApp.RevisionMetaData;


#endregion


// projname: TestConsoleApp
// itemname: RevisionFormat
// username: jeffs
// created:  6/30/2018 2:26:13 PM



// take the raw data and format depending on the usage

namespace TestConsoleApp
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
		public static string FormatTitle(string[] titles, 
			RevisionDataDisplay dd, int row, char filler = (char) 0)
		{
			if (string.IsNullOrWhiteSpace(titles[row])) return "";

			string formatted = Abbreviate(titles[row], 
				dd.ColWidth, '…');

			formatted = Justify(formatted, dd.JustifyColumn, dd.ColWidth);

			formatted = ApplyMargin(formatted, dd.MarginLeft, dd.MarginRight, filler);

			return formatted;
		}

		public static string FormatTitle(string[] titles)
		{
			if (titles == null) return "";

			string formatted = "";

			for (int i = titles.Length - 1; i >= 0; i--)
			{
				formatted = titles[i] + formatted;

				if (i == 0 || string.IsNullOrWhiteSpace(titles[i - 1])) break;

				formatted = " " + formatted;
			}
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
			Justification justifyColumn, int width)
		{
			switch (justifyColumn)
			{
			case Justification.LEFT:
				{
					return formatted.PadRight(width);
				}
			case Justification.CENTER:
				{
					return formatted.PadCenter(width);
				}
			case Justification.RIGHT:
				{
					return formatted.PadLeft(width);
				}
			}
			return formatted;
		}

		private static string ApplyMargin(string value,
			int marginLeft, int marginRight, char filler = ' ')
		{
			return value.PadLeft(marginLeft,filler).PadRight(marginRight, filler);
		}



	}
}
