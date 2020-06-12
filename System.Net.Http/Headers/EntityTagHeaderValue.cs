using System;

namespace System.Net.Http.Headers
{
	public class EntityTagHeaderValue : ICloneable
	{
		public static EntityTagHeaderValue Any
		{
			get
			{
			}
		}

		public bool IsWeak
		{
			get
			{
			}
		}

		public string Tag
		{
			get
			{
			}
		}

		public EntityTagHeaderValue(string tag)
		{
		}

		public EntityTagHeaderValue(string tag, bool isWeak)
		{
		}

		private EntityTagHeaderValue()
		{
		}

		public override bool Equals(object obj)
		{
		}

		internal static int GetEntityTagLength(string input, int startIndex, out EntityTagHeaderValue parsedValue)
		{
		}

		public override int GetHashCode()
		{
		}

		public static EntityTagHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out EntityTagHeaderValue parsedValue)
		{
		}
	}
}