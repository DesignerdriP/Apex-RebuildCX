using System;

namespace System.Net.Http.Headers
{
	internal class ProductInfoHeaderParser : HttpHeaderParser
	{
		internal readonly static ProductInfoHeaderParser SingleValueParser;

		internal readonly static ProductInfoHeaderParser MultipleValueParser;

		static ProductInfoHeaderParser()
		{
		}

		private new void .ctor()
		{
		}

		public override bool TryParseValue(string value, object storeValue, ref int index, out object parsedValue)
		{
		}
	}
}