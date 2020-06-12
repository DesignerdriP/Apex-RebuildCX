using System;

namespace System.Net.Http.Headers
{
	internal class Int32NumberHeaderParser : BaseHeaderParser
	{
		internal readonly static Int32NumberHeaderParser Parser;

		static Int32NumberHeaderParser()
		{
		}

		private Int32NumberHeaderParser()
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