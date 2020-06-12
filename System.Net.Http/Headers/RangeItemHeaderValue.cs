using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	public class RangeItemHeaderValue : ICloneable
	{
		public long? From
		{
			get
			{
			}
		}

		public long? To
		{
			get
			{
			}
		}

		public RangeItemHeaderValue(long? from, long? to)
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetRangeItemLength(string input, int startIndex, out RangeItemHeaderValue parsedValue)
		{
		}

		internal static int GetRangeItemListLength(string input, int startIndex, ICollection<RangeItemHeaderValue> rangeCollection)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}
	}
}