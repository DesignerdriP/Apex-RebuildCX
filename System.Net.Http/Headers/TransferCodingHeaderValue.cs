using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	public class TransferCodingHeaderValue : ICloneable
	{
		public ICollection<NameValueHeaderValue> Parameters
		{
			get
			{
			}
		}

		public string Value
		{
			get
			{
			}
		}

		internal TransferCodingHeaderValue()
		{
		}

		protected TransferCodingHeaderValue(TransferCodingHeaderValue source)
		{
		}

		public TransferCodingHeaderValue(string value)
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetTransferCodingLength(string input, int startIndex, Func<TransferCodingHeaderValue> transferCodingCreator, out TransferCodingHeaderValue parsedValue)
		{
		}

		public static TransferCodingHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out TransferCodingHeaderValue parsedValue)
		{
		}
	}
}