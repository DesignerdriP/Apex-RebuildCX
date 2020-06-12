using System;

namespace System.Net.Http.Headers
{
	public class ViaHeaderValue : ICloneable
	{
		public string Comment
		{
			get
			{
			}
		}

		public string ProtocolName
		{
			get
			{
			}
		}

		public string ProtocolVersion
		{
			get
			{
			}
		}

		public string ReceivedBy
		{
			get
			{
			}
		}

		public ViaHeaderValue(string protocolVersion, string receivedBy)
		{
		}

		public ViaHeaderValue(string protocolVersion, string receivedBy, string protocolName)
		{
		}

		public ViaHeaderValue(string protocolVersion, string receivedBy, string protocolName, string comment)
		{
		}

		private ViaHeaderValue()
		{
		}

		public override bool Equals(object obj)
		{
		}

		public override int GetHashCode()
		{
		}

		internal static int GetViaLength(string input, int startIndex, out object parsedValue)
		{
		}

		public static ViaHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out ViaHeaderValue parsedValue)
		{
		}
	}
}