using System;

namespace System.Net.Http.Headers
{
	public class ProductHeaderValue : ICloneable
	{
		public string Name
		{
			get
			{
			}
		}

		public string Version
		{
			get
			{
			}
		}

		public ProductHeaderValue(string name)
		{
		}

		public ProductHeaderValue(string name, string version)
		{
		}

		private ProductHeaderValue()
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetProductLength(string input, int startIndex, out ProductHeaderValue parsedValue)
		{
		}

		public static ProductHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out ProductHeaderValue parsedValue)
		{
		}
	}
}