using System;

namespace System.Net.Http.Headers
{
	internal class DateHeaderParser : HttpHeaderParser
	{
		internal readonly static DateHeaderParser Parser;

		static DateHeaderParser()
		{
		}

		private DateHeaderParser()
		{
		}

		public override string ToString(object value)
		{
		}

		public override bool TryParseValue(string value, object storeValue, ref int index, out object parsedValue)
		{
		}
	}
}