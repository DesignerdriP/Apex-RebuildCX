using System;

namespace System.Net.Http.Headers
{
	public class ProductInfoHeaderValue : ICloneable
	{
		public string Comment
		{
			get
			{
			}
		}

		public ProductHeaderValue Product
		{
			get
			{
			}
		}

		public ProductInfoHeaderValue(string productName, string productVersion)
		{
		}

		public ProductInfoHeaderValue(ProductHeaderValue product)
		{
		}

		public ProductInfoHeaderValue(string comment)
		{
		}

		private ProductInfoHeaderValue()
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetProductInfoLength(string input, int startIndex, out ProductInfoHeaderValue parsedValue)
		{
		}

		public static ProductInfoHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out ProductInfoHeaderValue parsedValue)
		{
		}
	}
}