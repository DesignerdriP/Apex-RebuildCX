using System;

namespace System.Net.Http.Headers
{
	internal class CacheControlHeaderParser : BaseHeaderParser
	{
		internal readonly static CacheControlHeaderParser Parser;

		static CacheControlHeaderParser()
		{
		}

		private CacheControlHeaderParser()
		{
		}

		protected override int GetParsedValueLength(string value, int startIndex, object storeValue, out object parsedValue)
		{
		}
	}
}