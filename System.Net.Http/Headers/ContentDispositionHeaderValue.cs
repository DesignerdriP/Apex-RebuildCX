using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	public class ContentDispositionHeaderValue : ICloneable
	{
		public DateTimeOffset? CreationDate
		{
			get
			{
			}
			set
			{
			}
		}

		public string DispositionType
		{
			get
			{
			}
			set
			{
			}
		}

		public string FileName
		{
			get
			{
			}
			set
			{
			}
		}

		public string FileNameStar
		{
			get
			{
			}
			set
			{
			}
		}

		public DateTimeOffset? ModificationDate
		{
			get
			{
			}
			set
			{
			}
		}

		public string Name
		{
			get
			{
			}
			set
			{
			}
		}

		public ICollection<NameValueHeaderValue> Parameters
		{
			get
			{
			}
		}

		public DateTimeOffset? ReadDate
		{
			get
			{
			}
			set
			{
			}
		}

		public long? Size
		{
			get
			{
			}
			set
			{
			}
		}

		internal ContentDispositionHeaderValue()
		{
		}

		protected ContentDispositionHeaderValue(ContentDispositionHeaderValue source)
		{
		}

		public ContentDispositionHeaderValue(string dispositionType)
		{
		}

		public override bool Equals(object obj)
		{
		}

		internal static int GetDispositionTypeLength(string input, int startIndex, out object parsedValue)
		{
		}

		public override int GetHashCode()
		{
		}

		public static ContentDispositionHeaderValue Parse(string input)
		{
		}

		object System.ICloneable.Clone()
		{
		}

		public override string ToString()
		{
		}

		public static bool TryParse(string input, out ContentDispositionHeaderValue parsedValue)
		{
		}
	}
}