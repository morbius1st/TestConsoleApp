namespace TestConsoleApp
{
	public class ItemValue
	{
		public dynamic Value { get; private set; }

		public ItemValue(dynamic x)
		{
			Value = x;
		}

		public EDataType Type { get; private set; }

		public bool? AsBool
		{
			get => Value;
			set
			{
				Type = EDataType.BOOL;
				Value = value;
			}
		}

		public int AsInt
		{
			get => Value;
			set
			{
				Type = EDataType.INT;
				Value = value;
			}
		}

		public ElementId AsElementId
		{
			get => Value;
			set
			{
				Type = EDataType.ELEMENTID;
				Value = value;
			}
		}

		public RevisionVisibility AsVisibility
		{
			get => Value;
			set
			{
				Type = EDataType.VISIBILITY;
				Value = value;
			}
		}

		public string AsString
		{
			get => Value;
			set
			{
				Type = EDataType.STRING;
				Value = value;
			}
		}
	
		public string AsOrderCode
		{
			get => Value;
			set
			{
				Type = EDataType.ORDER;
				Value = value;
			}
		}
	
	}
}