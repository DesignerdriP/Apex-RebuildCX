using System;
using System.Collections;

namespace System.Net.Http.Headers
{
	internal sealed class GenericHeaderParser : BaseHeaderParser
	{
		internal readonly static HttpHeaderParser HostParser;

		internal readonly static HttpHeaderParser TokenListParser;

		internal readonly static HttpHeaderParser SingleValueNameValueWithParametersParser;

		internal readonly static HttpHeaderParser MultipleValueNameValueWithParametersParser;

		internal readonly static HttpHeaderParser SingleValueNameValueParser;

		internal readonly static HttpHeaderParser MultipleValueNameValueParser;

		internal readonly static HttpHeaderParser MailAddressParser;

		internal readonly static HttpHeaderParser SingleValueProductParser;

		internal readonly static HttpHeaderParser MultipleValueProductParser;

		internal readonly static HttpHeaderParser RangeConditionParser;

		internal readonly static HttpHeaderParser SingleValueAuthenticationParser;

		internal readonly static HttpHeaderParser MultipleValueAuthenticationParser;

		internal readonly static HttpHeaderParser RangeParser;

		internal readonly static HttpHeaderParser RetryConditionParser;

		internal readonly static HttpHeaderParser ContentRangeParser;

		internal readonly static HttpHeaderParser ContentDispositionParser;

		internal readonly static HttpHeaderParser SingleValueStringWithQualityParser;

		internal readonly static HttpHeaderParser MultipleValueStringWithQualityParser;

		internal readonly static HttpHeaderParser SingleValueEntityTagParser;

		internal readonly static HttpHeaderParser MultipleValueEntityTagParser;

		internal readonly static HttpHeaderParser SingleValueViaParser;

		internal readonly static HttpHeaderParser MultipleValueViaParser;

		internal readonly static HttpHeaderParser SingleValueWarningParser;

		internal readonly static HttpHeaderParser MultipleValueWarningParser;

		public override IEqualityComparer Comparer
		{
			get
			{
			}
		}

		static GenericHeaderParser()
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