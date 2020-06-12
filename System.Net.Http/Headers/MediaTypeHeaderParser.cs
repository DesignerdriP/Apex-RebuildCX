using System;

namespace System.Net.Http.Headers
{
	internal class MediaTypeHeaderParser : BaseHeaderParser
	{
		internal readonly static MediaTypeHeaderParser SingleValueParser;

		internal readonly static MediaTypeHeaderParser SingleValueWithQualityParser;

		internal readonly static MediaTypeHeaderParser MultipleValuesParser;

		static MediaTypeHeaderParser()
		{
		}

		private new void .ctor()
		{
		}

		protected override int GetParsedValueLength(string value, int startIndex, object storeValue, out object parsedValue)
		{
		}
	}
}