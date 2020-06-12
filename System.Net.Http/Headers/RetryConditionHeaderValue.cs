using System;

namespace System.Net.Http.Headers
{
	public class RetryConditionHeaderValue : ICloneable
	{
		public DateTimeOffset? Date
		{
			get
			{
			}
		}

		public TimeSpan? Delta
		{
			get
			{
			}
		}

		public RetryConditionHeaderValue(DateTimeOffset date)
		{
		}

		public RetryConditionHeaderValue(TimeSpan delta)
		{
		}

		private RetryConditionHeaderValue()
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetRetryConditionLength(string input, int startIndex, out object parsedValue)
		{
		}

		public static RetryConditionHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out RetryConditionHeaderValue parsedValue)
		{
		}
	}
}