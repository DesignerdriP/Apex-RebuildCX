using System;
using System.Collections.ObjectModel;

namespace System.Net.Http.Headers
{
	internal class ObjectCollection<T> : Collection<T>
	where T : class
	{
		static ObjectCollection()
		{
		}

		public ObjectCollection()
		{
		}

		public ObjectCollection(Action<T> validator)
		{
		}

		protected override void InsertItem(int index, T item)
		{
		}

		protected override void SetItem(int index, T item)
		{
		}
	}
}