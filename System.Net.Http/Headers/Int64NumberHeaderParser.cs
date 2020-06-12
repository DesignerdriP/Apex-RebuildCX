using System;

namespace System.Net.Http.Headers
{
	internal class Int64NumberHeaderParser : BaseHeaderParser
	{
		internal readonly static Int64NumberHeaderParser Parser;

		static Int64NumberHeaderParser()
		{
		}

		private Int64NumberHeaderParser()
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