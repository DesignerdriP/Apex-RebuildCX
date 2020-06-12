using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	public class CacheControlHeaderValue : ICloneable
	{
		public ICollection<NameValueHeaderValue> Extensions
		{
			get
			{
			}
		}

		public TimeSpan? MaxAge
		{
			get
			{
			}
			set
			{
			}
		}

		public bool MaxStale
		{
			get
			{
			}
			set
			{
			}
		}

		public TimeSpan? MaxStaleLimit
		{
			get
			{
			}
			set
			{
			}
		}

		public TimeSpan? MinFresh
		{
			get
			{
			}
			set
			{
			}
		}

		public bool MustRevalidate
		{
			get
			{
			}
			set
			{
			}
		}

		public bool NoCache
		{
			get
			{
			}
			set
			{
			}
		}

		public ICollection<string> NoCacheHeaders
		{
			get
			{
			}
		}

		public bool NoStore
		{
			get
			{
			}
			set
			{
			}
		}

		public bool NoTransform
		{
			get
			{
			}
			set
			{
			}
		}

		public bool OnlyIfCached
		{
			get
			{
			}
			set
			{
			}
		}

		public bool Private
		{
			get
			{
			}
			set
			{
			}
		}

		public ICollection<string> PrivateHeaders
		{
			get
			{
			}
		}

		public bool ProxyRevalidate
		{
			get
			{
			}
			set
			{
			}
		}

		public bool Public
		{
			get
			{
			}
			set
			{
			}
		}

		public TimeSpan? SharedMaxAge
		{
			get
			{
			}
			set
			{
			}
		}

		static CacheControlHeaderValue()
		{
		}

		public CacheControlHeaderValue()
		{
		}

		public override bool Equals(object obj)
		{
		}

		internal static int GetCacheControlLength(string input, int startIndex, CacheControlHeaderValue storeValue, out CacheControlHeaderValue parsedValue)
		{
		}

		public override int GetHashCode()
		{
		}

		public static CacheControlHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out CacheControlHeaderValue parsedValue)
		{
		}
	}
}