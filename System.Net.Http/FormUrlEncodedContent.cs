using System;
using System.Collections.Generic;

namespace System.Net.Http
{
	public class FormUrlEncodedContent : ByteArrayContent
	{
		public FormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
		{
		}
	}
}