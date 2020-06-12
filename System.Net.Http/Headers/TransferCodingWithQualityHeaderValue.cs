using System;

namespace System.Net.Http.Headers
{
	public sealed class TransferCodingWithQualityHeaderValue : TransferCodingHeaderValue, ICloneable
	{
		public double? Quality
		{
			get
			{
			}
			set
			{
			}
		}

		internal TransferCodingWithQualityHeaderValue()
		{
		}

		public TransferCodingWithQualityHeaderValue(string value)
		{
		}

		public TransferCodingWithQualityHeaderValue(string value, double quality)
		{
		}

		public static new TransferCodingWithQualityHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public static bool TryParse(string input, out TransferCodingWithQualityHeaderValue parsedValue)
		{
		}
	}
}