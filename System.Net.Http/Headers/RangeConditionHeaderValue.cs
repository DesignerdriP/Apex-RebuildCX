using System;

namespace System.Net.Http.Headers
{
	public class RangeConditionHeaderValue : ICloneable
	{
		public DateTimeOffset? Date
		{
			get
			{
			}
		}

		public EntityTagHeaderValue EntityTag
		{
			get
			{
			}
		}

		public RangeConditionHeaderValue(DateTimeOffset date)
		{
		}

		public RangeConditionHeaderValue(EntityTagHeaderValue entityTag)
		{
		}

		public RangeConditionHeaderValue(string entityTag)
		{
		}

		private RangeConditionHeaderValue()
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetRangeConditionLength(string input, int startIndex, out object parsedValue)
		{
		}

		public static RangeConditionHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out RangeConditionHeaderValue parsedValue)
		{
		}
	}
}