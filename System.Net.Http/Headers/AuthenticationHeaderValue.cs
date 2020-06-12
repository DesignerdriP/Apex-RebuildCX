using System;

namespace System.Net.Http.Headers
{
	public class AuthenticationHeaderValue : ICloneable
	{
		public string Parameter
		{
			get
			{
			}
		}

		public string Scheme
		{
			get
			{
			}
		}

		public AuthenticationHeaderValue(string scheme)
		{
		}

		public AuthenticationHeaderValue(string scheme, string parameter)
		{
		}

		private AuthenticationHeaderValue()
		{
		}

		public override bool Equals(object obj)
		{
		}

		internal static int GetAuthenticationLength(string input, int startIndex, out object parsedValue)
		{
		}

		public override int GetHashCode()
		{
		}

		public static AuthenticationHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out AuthenticationHeaderValue parsedValue)
		{
		}
	}
}