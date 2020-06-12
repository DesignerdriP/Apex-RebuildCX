using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	public class RangeHeaderValue : ICloneable
	{
		public ICollection<RangeItemHeaderValue> Ranges
		{
			get
			{
			}
		}

		public string Unit
		{
			get
			{
			}
			set
			{
			}
		}

		public RangeHeaderValue()
		{
		}

		public RangeHeaderValue(long? from, long? to)
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetRangeLength(string input, int startIndex, out object parsedValue)
		{
		}

		public static RangeHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out RangeHeaderValue parsedValue)
		{
		}
	}
}