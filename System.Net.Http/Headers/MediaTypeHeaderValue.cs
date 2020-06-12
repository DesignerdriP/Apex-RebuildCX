using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	public class MediaTypeHeaderValue : ICloneable
	{
		public string CharSet
		{
			get
			{
			}
			set
			{
			}
		}

		public string MediaType
		{
			get
			{
			}
			set
			{
			}
		}

		public ICollection<NameValueHeaderValue> Parameters
		{
			get
			{
			}
		}

		internal MediaTypeHeaderValue()
		{
		}

		protected MediaTypeHeaderValue(MediaTypeHeaderValue source)
		{
		}

		public MediaTypeHeaderValue(string mediaType)
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetMediaTypeLength(string input, int startIndex, Func<MediaTypeHeaderValue> mediaTypeCreator, out MediaTypeHeaderValue parsedValue)
		{
		}

		public static MediaTypeHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out MediaTypeHeaderValue parsedValue)
		{
		}
	}
}