using System;
using System.Collections;

namespace System.Net.Http.Headers
{
	internal abstract class HttpHeaderParser
	{
		internal const string DefaultSeparator = ", ";

		public virtual IEqualityComparer Comparer
		{
			get
			{
			}
		}

		public string Separator
		{
			get
			{
			}
		}

		public bool SupportsMultipleValues
		{
			get
			{
			}
		}

		protected HttpHeaderParser(bool supportsMultipleValues)
		{
		}

		protected HttpHeaderParser(bool supportsMultipleValues, string separator)
		{
		}

		public object ParseValue(string value, object storeValue, ref int index)
		{
		}

		public virtual string ToString(object value)
		{
		}

		public abstract bool TryParseValue(string value, object storeValue, ref int index, out object parsedValue);
	}
}