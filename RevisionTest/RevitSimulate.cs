using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisionTest
{
	public enum RevisionVisibility
	{
		Hidden = 0,
		TagVisible = 1,
		CloudAndTagVisible = 2,
	}

	public class ElementId
	{
		public int? Value { get; set; }

		public ElementId(int id)
		{
			Value = id;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}

//	class RevitSimulate
//	{
//
//
//	}
}
