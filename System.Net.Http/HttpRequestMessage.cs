using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	public class HttpRequestMessage : IDisposable
	{
		public HttpContent Content
		{
			get
			{
			}
			set
			{
			}
		}

		public HttpRequestHeaders Headers
		{
			get
			{
			}
		}

		public HttpMethod Method
		{
			get
			{
			}
			set
			{
			}
		}

		public IDictionary<string, object> Properties
		{
			get
			{
			}
		}

		public Uri RequestUri
		{
			get
			{
			}
			set
			{
			}
		}

		public System.Version Version
		{
			get
			{
			}
			set
			{
			}
		}

		public HttpRequestMessage()
		{
		}

		public HttpRequestMessage(HttpMethod method, Uri requestUri)
		{
		}

		public HttpRequestMessage(HttpMethod method, string requestUri)
		{
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		public void Dispose()
		{
		}

		internal bool MarkAsSent()
		{
		}

		public override string ToString()
		{
		}
	}
}