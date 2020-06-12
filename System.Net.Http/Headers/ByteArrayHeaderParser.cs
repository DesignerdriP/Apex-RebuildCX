using System;

namespace System.Net.Http.Headers
{
	internal class ByteArrayHeaderParser : HttpHeaderParser
	{
		internal readonly static ByteArrayHeaderParser Parser;

		static ByteArrayHeaderParser()
		{
		}

		private ByteArrayHeaderParser()
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