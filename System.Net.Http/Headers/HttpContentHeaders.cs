using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	public sealed class HttpContentHeaders : HttpHeaders
	{
		public ICollection<string> Allow
		{
			get
			{
			}
		}

		public ContentDispositionHeaderValue ContentDisposition
		{
			get
			{
			}
			set
			{
			}
		}

		public ICollection<string> ContentEncoding
		{
			get
			{
			}
		}

		public ICollection<string> ContentLanguage
		{
			get
			{
			}
		}

		public long? ContentLength
		{
			get
			{
			}
			set
			{
			}
		}

		public Uri ContentLocation
		{
			get
			{
			}
			set
			{
			}
		}

		public byte[] ContentMD5
		{
			get
			{
			}
			set
			{
			}
		}

		public ContentRangeHeaderValue ContentRange
		{
			get
			{
			}
			set
			{
			}
		}

		public MediaTypeHeaderValue ContentType
		{
			get
			{
			}
			set
			{
			}
		}

		public DateTimeOffset? Expires
		{
			get
			{
			}
			set
			{
			}
		}

		public DateTimeOffset? LastModified
		{
			get
			{
			}
			set
			{
			}
		}

		static HttpContentHeaders()
		{
		}

		internal HttpContentHeaders(Func<long?> calculateLengthFunc)
		{
		}

		internal static void AddKnownHeaders(HashSet<string> headerSet)
		{
		}
	}
}