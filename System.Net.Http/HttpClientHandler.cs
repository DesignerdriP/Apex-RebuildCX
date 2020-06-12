using System;
using System.Net;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	public class HttpClientHandler : HttpMessageHandler
	{
		public bool AllowAutoRedirect
		{
			get
			{
			}
			set
			{
			}
		}

		public DecompressionMethods AutomaticDecompression
		{
			get
			{
			}
			set
			{
			}
		}

		public ClientCertificateOption ClientCertificateOptions
		{
			get
			{
			}
			set
			{
			}
		}

		public System.Net.CookieContainer CookieContainer
		{
			get
			{
			}
			set
			{
			}
		}

		public ICredentials Credentials
		{
			get
			{
			}
			set
			{
			}
		}

		public int MaxAutomaticRedirections
		{
			get
			{
			}
			set
			{
			}
		}

		public long MaxRequestContentBufferSize
		{
			get
			{
			}
			set
			{
			}
		}

		public bool PreAuthenticate
		{
			get
			{
			}
			set
			{
			}
		}

		public IWebProxy Proxy
		{
			get
			{
			}
			[SecuritySafeCritical]
			set
			{
			}
		}

		public virtual bool SupportsAutomaticDecompression
		{
			get
			{
			}
		}

		public virtual bool SupportsProxy
		{
			get
			{
			}
		}

		public virtual bool SupportsRedirectConfiguration
		{
			get
			{
			}
		}

		public bool UseCookies
		{
			get
			{
			}
			set
			{
			}
		}

		public bool UseDefaultCredentials
		{
			get
			{
			}
			set
			{
			}
		}

		public bool UseProxy
		{
			get
			{
			}
			set
			{
			}
		}

		static HttpClientHandler()
		{
		}

		public HttpClientHandler()
		{
		}

		internal void CheckDisposedOrStarted()
		{
		}

		protected override void Dispose(bool disposing)
		{
		}

		internal virtual void InitializeWebRequest(HttpRequestMessage request, HttpWebRequest webRequest)
		{
		}

		protected internal override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
		}
	}
}