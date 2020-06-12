using System;

namespace System.Net.Http.Headers
{
	internal class TimeSpanHeaderParser : BaseHeaderParser
	{
		internal readonly static TimeSpanHeaderParser Parser;

		static TimeSpanHeaderParser()
		{
		}

		private TimeSpanHeaderParser()
		{
		}

		protected override int GetParsedValueLength(string value, int startIndex, object storeValue, out object parsedValue)
		{
		}

		public override string ToString(object value)
		{
		}
	}
}