using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Http.Headers
{
	public class NameValueHeaderValue : ICloneable
	{
		public string Name
		{
			get
			{
			}
		}

		public string Value
		{
			get
			{
			}
			set
			{
			}
		}

		static NameValueHeaderValue()
		{
		}

		internal NameValueHeaderValue()
		{
		}

		public NameValueHeaderValue(string name)
		{
		}

		public NameValueHeaderValue(string name, string value)
		{
		}

		protected NameValueHeaderValue(NameValueHeaderValue source)
		{
		}

		public override bool Equals(object obj)
		{
		}

		internal static NameValueHeaderValue Find(ICollection<NameValueHeaderValue> values, string name)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetHashCode(ICollection<NameValueHeaderValue> values)
		{
		}

		internal static int GetNameValueLength(string input, int startIndex, out NameValueHeaderValue parsedValue)
		{
		}

		internal static int GetNameValueLength(string input, int startIndex, Func<NameValueHeaderValue> nameValueCreator, out NameValueHeaderValue parsedValue)
		{
		}

		internal static int GetNameValueListLength(string input, int startIndex, char delimiter, ICollection<NameValueHeaderValue> nameValueCollection)
		{
		}

		internal static int GetValueLength(string input, int startIndex)
		{
		}

		public static NameValueHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		internal static void ToString(ICollection<NameValueHeaderValue> values, char separator, bool leadingSeparator, StringBuilder destination)
		{
		}

		internal static string ToString(ICollection<NameValueHeaderValue> values, char separator, bool leadingSeparator)
		{
		}

		public static bool TryParse(string input, out NameValueHeaderValue parsedValue)
		{
		}
	}
}