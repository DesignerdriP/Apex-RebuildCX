using System;

namespace System.Net.Http.Headers
{
	public sealed class MediaTypeWithQualityHeaderValue : MediaTypeHeaderValue, ICloneable
	{
		public double? Quality
		{
			get
			{
			}
			set
			{
			}
		}

		internal MediaTypeWithQualityHeaderValue()
		{
		}

		public MediaTypeWithQualityHeaderValue(string mediaType)
		{
		}

		public MediaTypeWithQualityHeaderValue(string mediaType, double quality)
		{
		}

		public static new MediaTypeWithQualityHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public static bool TryParse(string input, out MediaTypeWithQualityHeaderValue parsedValue)
		{
		}
	}
}