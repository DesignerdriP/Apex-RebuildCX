using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	internal sealed class HttpGeneralHeaders
	{
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

		public HttpHeaderValueCollection<NameValueHeaderValue> Pragma
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

		internal HttpGeneralHeaders(HttpHeaders parent)
		{
		}

		internal static void AddKnownHeaders(HashSet<string> headerSet)
		{
		}

		internal static void AddParsers(Dictionary<string, HttpHeaderParser> parserStore)
		{
		}

		internal void AddSpecialsFrom(HttpGeneralHeaders sourceHeaders)
		{
		}
	}
}