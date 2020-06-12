using System;
using System.Net;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	public class HttpResponseMessage : IDisposable
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

		public HttpResponseHeaders Headers
		{
			get
			{
			}
		}

		public bool IsSuccessStatusCode
		{
			get
			{
			}
		}

		public string ReasonPhrase
		{
			get
			{
			}
			set
			{
			}
		}

		public HttpRequestMessage RequestMessage
		{
			get
			{
			}
			set
			{
			}
		}

		public HttpStatusCode StatusCode
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

		public HttpResponseMessage()
		{
		}

		public HttpResponseMessage(HttpStatusCode statusCode)
		{
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		public void Dispose()
		{
		}

		public HttpResponseMessage EnsureSuccessStatusCode()
		{
		}

		public override string ToString()
		{
		}
	}
}