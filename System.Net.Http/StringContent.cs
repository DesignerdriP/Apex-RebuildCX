using System;
using System.Text;

namespace System.Net.Http
{
	public class StringContent : ByteArrayContent
	{
		public StringContent(string content)
		{
		}

		public StringContent(string content, Encoding encoding)
		{
		}

		public StringContent(string content, Encoding encoding, string mediaType)
		{
		}
	}
}