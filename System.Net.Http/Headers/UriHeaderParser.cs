using System;

namespace System.Net.Http.Headers
{
	internal class UriHeaderParser : HttpHeaderParser
	{
		internal readonly static UriHeaderParser RelativeOrAbsoluteUriParser;

		static UriHeaderParser()
		{
		}

		private new void .ctor()
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