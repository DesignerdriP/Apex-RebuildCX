using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	public sealed class HttpRequestHeaders : HttpHeaders
	{
		public HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> Accept
		{
			get
			{
			}
		}

		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptCharset
		{
			get
			{
			}
		}

		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptEncoding
		{
			get
			{
			}
		}

		public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptLanguage
		{
			get
			{
			}
		}

		public AuthenticationHeaderValue Authorization
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

		public HttpHeaderValueCollection<NameValueWithParametersHeaderValue> Expect
		{
			get
			{
			}
		}

		public bool? ExpectContinue
		{
			get
			{
			}
			set
			{
			}
		}

		public string From
		{
			get
			{
			}
			set
			{
			}
		}

		public string Host
		{
			get
			{
			}
			set
			{
			}
		}

		public HttpHeaderValueCollection<EntityTagHeaderValue> IfMatch
		{
			get
			{
			}
		}

		public DateTimeOffset? IfModifiedSince
		{
			get
			{
			}
			set
			{
			}
		}

		public HttpHeaderValueCollection<EntityTagHeaderValue> IfNoneMatch
		{
			get
			{
			}
		}

		public RangeConditionHeaderValue IfRange
		{
			get
			{
			}
			set
			{
			}
		}

		public DateTimeOffset? IfUnmodifiedSince
		{
			get
			{
			}
			set
			{
			}
		}

		public int? MaxForwards
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

		public AuthenticationHeaderValue ProxyAuthorization
		{
			get
			{
			}
			set
			{
			}
		}

		public RangeHeaderValue Range
		{
			get
			{
			}
			set
			{
			}
		}

		public Uri Referrer
		{
			get
			{
			}
			set
			{
			}
		}

		public HttpHeaderValueCollection<TransferCodingWithQualityHeaderValue> TE
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

		public HttpHeaderValueCollection<ProductInfoHeaderValue> UserAgent
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

		static HttpRequestHeaders()
		{
		}

		internal HttpRequestHeaders()
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