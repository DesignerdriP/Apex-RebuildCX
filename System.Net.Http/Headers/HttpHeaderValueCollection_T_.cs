using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	public sealed class HttpHeaderValueCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	where T : class
	{
		public int Count
		{
			get
			{
			}
		}

		public bool IsReadOnly
		{
			get
			{
			}
		}

		internal bool IsSpecialValueSet
		{
			get
			{
			}
		}

		internal HttpHeaderValueCollection(string headerName, HttpHeaders store)
		{
		}

		internal HttpHeaderValueCollection(string headerName, HttpHeaders store, Action<HttpHeaderValueCollection<T>, T> validator)
		{
		}

		internal HttpHeaderValueCollection(string headerName, HttpHeaders store, T specialValue)
		{
		}

		internal HttpHeaderValueCollection(string headerName, HttpHeaders store, T specialValue, Action<HttpHeaderValueCollection<T>, T> validator)
		{
		}

		public void Add(T item)
		{
		}

		public void Clear()
		{
		}

		public bool Contains(T item)
		{
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
		}

		public IEnumerator<T> GetEnumerator()
		{
		}

		internal string GetHeaderStringWithoutSpecial()
		{
		}

		public void ParseAdd(string input)
		{
		}

		public bool Remove(T item)
		{
		}

		internal void RemoveSpecialValue()
		{
		}

		internal void SetSpecialValue()
		{
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
		}

		public override string ToString()
		{
		}

		public bool TryParseAdd(string input)
		{
		}
	}
}