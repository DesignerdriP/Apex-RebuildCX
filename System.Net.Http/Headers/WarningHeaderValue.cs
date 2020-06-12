using System;

namespace System.Net.Http.Headers
{
	public class WarningHeaderValue : ICloneable
	{
		public string Agent
		{
			get
			{
			}
		}

		public int Code
		{
			get
			{
			}
		}

		public DateTimeOffset? Date
		{
			get
			{
			}
		}

		public string Text
		{
			get
			{
			}
		}

		public WarningHeaderValue(int code, string agent, string text)
		{
		}

		public WarningHeaderValue(int code, string agent, string text, DateTimeOffset date)
		{
		}

		private WarningHeaderValue()
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetWarningLength(string input, int startIndex, out object parsedValue)
		{
		}

		public static WarningHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out WarningHeaderValue parsedValue)
		{
		}
	}
}