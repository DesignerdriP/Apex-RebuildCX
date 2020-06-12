using System;

namespace System.Net.Http
{
	public class HttpMethod : IEquatable<HttpMethod>
	{
		public static HttpMethod Delete
		{
			get
			{
			}
		}

		public static HttpMethod Get
		{
			get
			{
			}
		}

		public static HttpMethod Head
		{
			get
			{
			}
		}

		public string Method
		{
			get
			{
			}
		}

		public static HttpMethod Options
		{
			get
			{
			}
		}

		public static HttpMethod Post
		{
			get
			{
			}
		}

		public static HttpMethod Put
		{
			get
			{
			}
		}

		public static HttpMethod Trace
		{
			get
			{
			}
		}

		static HttpMethod()
		{
		}

		public HttpMethod(string method)
		{
		}

		public bool Equals(HttpMethod other)
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		public static bool operator ==(HttpMethod left, HttpMethod right)
		{
		}

		public static bool operator !=(HttpMethod left, HttpMethod right)
		{
		}

		public override string ToString()
		{
		}
	}
}