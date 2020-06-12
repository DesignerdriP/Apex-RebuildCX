using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	public class NameValueWithParametersHeaderValue : NameValueHeaderValue, ICloneable
	{
		public ICollection<NameValueHeaderValue> Parameters
		{
			get
			{
			}
		}

		static NameValueWithParametersHeaderValue()
		{
		}

		public NameValueWithParametersHeaderValue(string name)
		{
		}

		public NameValueWithParametersHeaderValue(string name, string value)
		{
		}

		internal NameValueWithParametersHeaderValue()
		{
		}

		protected NameValueWithParametersHeaderValue(NameValueWithParametersHeaderValue source)
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetNameValueWithParametersLength(string input, int startIndex, out object parsedValue)
		{
		}

		public static new NameValueWithParametersHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out NameValueWithParametersHeaderValue parsedValue)
		{
		}
	}
}