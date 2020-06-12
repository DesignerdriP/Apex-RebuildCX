using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	public sealed class HttpResponseHeaders : HttpHeaders
	{
		public HttpHeaderValueCollection<string> AcceptRanges
		{
			get
			{
			}
		}

		public TimeSpan? Age
		{
			get
			{
			}
			set
			{
			}
		}

		public CacheControlHeaderValue CacheControl
		{
			get
			{
			}
			set
			{
			}
		}

		public HttpHeaderValueCollection<string> Connection
		{
			get
			{
			}
		}

		public bool? ConnectionClose
		{
			get
			{
			}
			set
			{
			}
		}

		public DateTimeOffset? Date
		{
			get
			{
			}
			set
			{
			}
		}

		public EntityTagHeaderValue ETag
		{
			get
			{
			}
			set
			{
			}
		}

		public Uri Location
		{
			get
			{
			}
			set
			{
			}
		}

		public HttpHeaderValueCollection<NameValueHeaderValue> Pragma
		{
			get
			{
			}
		}

		public HttpHeaderValueCollection<AuthenticationHeaderValue> ProxyAuthenticate
		{
			get
			{
			}
		}

		public RetryConditionHeaderValue RetryAfter
		{
			get
			{
			}
			set
			{
			}
		}

		public HttpHeaderValueCollection<ProductInfoHeaderValue> Server
		{
			get
			{
			}
		}

		public HttpHeaderValueCollection<string> Trailer
		{
			get
			{
			}
		}

		public HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding
		{
			get
			{
			}
		}

		public bool? TransferEncodingChunked
		{
			get
			{
			}
			set
			{
			}
		}

		public HttpHeaderValueCollection<ProductHeaderValue> Upgrade
		{
			get
			{
			}
		}

		public HttpHeaderValueCollection<string> Vary
		{
			get
			{
			}
		}

		public HttpHeaderValueCollection<ViaHeaderValue> Via
		{
			get
			{
			}
		}

		public HttpHeaderValueCollection<WarningHeaderValue> Warning
		{
			get
			{
			}
		}

		public HttpHeaderValueCollection<AuthenticationHeaderValue> WwwAuthenticate
		{
			get
			{
			}
		}

		static HttpResponseHeaders()
		{
		}

		internal HttpResponseHeaders()
		{
		}

		internal override void AddHeaders(HttpHeaders sourceHeaders)
		{
		}

		internal static void AddKnownHeaders(HashSet<string> headerSet)
		{
		}
	}
}