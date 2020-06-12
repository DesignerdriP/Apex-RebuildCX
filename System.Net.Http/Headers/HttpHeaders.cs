using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	public abstract class HttpHeaders : IEnumerable<KeyValuePair<string, IEnumerable<string>>>, IEnumerable
	{
		protected HttpHeaders()
		{
		}

		public void Add(string name, string value)
		{
		}

		public void Add(string name, IEnumerable<string> values)
		{
		}

		internal virtual void AddHeaders(HttpHeaders sourceHeaders)
		{
		}

		internal void AddParsedValue(string name, object value)
		{
		}

		public void Clear()
		{
		}

		public bool Contains(string name)
		{
		}

		internal bool ContainsParsedValue(string name, object value)
		{
		}

		public IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetEnumerator()
		{
		}

		internal string GetHeaderString(string headerName)
		{
		}

		internal string GetHeaderString(string headerName, object exclude)
		{
		}

		internal IEnumerable<KeyValuePair<string, string>> GetHeaderStrings()
		{
		}

		internal object GetParsedValues(string name)
		{
		}

		public IEnumerable<string> GetValues(string name)
		{
		}

		public bool Remove(string name)
		{
		}

		internal bool RemoveParsedValue(string name, object value)
		{
		}

		internal void SetConfiguration(Dictionary<string, HttpHeaderParser> parserStore, HashSet<string> invalidHeaders)
		{
		}

		internal void SetOrRemoveParsedValue(string name, object value)
		{
		}

		internal void SetParsedValue(string name, object value)
		{
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
		}

		public override string ToString()
		{
		}

		public bool TryAddWithoutValidation(string name, string value)
		{
		}

		public bool TryAddWithoutValidation(string name, IEnumerable<string> values)
		{
		}

		public bool TryGetValues(string name, out IEnumerable<string> values)
		{
		}

		internal bool TryParseAndAddValue(string name, string value)
		{
		}
	}
}