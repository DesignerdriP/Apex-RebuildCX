using System;

namespace System.Net.Http.Headers
{
	internal class TransferCodingHeaderParser : BaseHeaderParser
	{
		internal readonly static TransferCodingHeaderParser SingleValueParser;

		internal readonly static TransferCodingHeaderParser MultipleValueParser;

		internal readonly static TransferCodingHeaderParser SingleValueWithQualityParser;

		internal readonly static TransferCodingHeaderParser MultipleValueWithQualityParser;

		static TransferCodingHeaderParser()
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