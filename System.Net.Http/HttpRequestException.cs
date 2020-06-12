using System;

namespace System.Net.Http
{
	[Serializable]
	public class HttpRequestException : Exception
	{
		static HttpRequestException()
		{
		}

		public HttpRequestException()
		{
		}

		public HttpRequestException(string message)
		{
		}

		public HttpRequestException(string message, Exception inner)
		{
		}
	}
}