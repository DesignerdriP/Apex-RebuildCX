using System;

namespace System.Net.Http.Headers
{
	public class ContentRangeHeaderValue : ICloneable
	{
		public long? From
		{
			get
			{
			}
		}

		public bool HasLength
		{
			get
			{
			}
		}

		public bool HasRange
		{
			get
			{
			}
		}

		public long? Length
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

		public string Unit
		{
			get
			{
			}
			set
			{
			}
		}

		public ContentRangeHeaderValue(long from, long to, long length)
		{
		}

		public ContentRangeHeaderValue(long length)
		{
		}

		public ContentRangeHeaderValue(long from, long to)
		{
		}

		private ContentRangeHeaderValue()
		{
		}

		public override bool Equals(object obj)
		{
		}

		internal static int GetContentRangeLength(string input, int startIndex, out object parsedValue)
		{
		}

		public override int GetHashCode()
		{
		}

		public static ContentRangeHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out ContentRangeHeaderValue parsedValue)
		{
		}
	}
}