using System;

namespace System.Net.Http.Headers
{
	public class StringWithQualityHeaderValue : ICloneable
	{
		public double? Quality
		{
			get
			{
			}
		}

		public string Value
		{
			get
			{
			}
		}

		public StringWithQualityHeaderValue(string value)
		{
		}

		public StringWithQualityHeaderValue(string value, double quality)
		{
		}

		private StringWithQualityHeaderValue()
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetStringWithQualityLength(string input, int startIndex, out object parsedValue)
		{
		}

		public static StringWithQualityHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out StringWithQualityHeaderValue parsedValue)
		{
		}
	}
}