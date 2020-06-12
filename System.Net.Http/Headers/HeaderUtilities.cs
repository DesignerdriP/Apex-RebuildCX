using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	internal static class HeaderUtilities
	{
		internal const string ConnectionClose = "close";

		internal readonly static TransferCodingHeaderValue TransferEncodingChunked;

		internal readonly static NameValueWithParametersHeaderValue ExpectContinue;

		internal const string BytesUnit = "bytes";

		internal readonly static Action<HttpHeaderValueCollection<string>, string> TokenValidator;

		static HeaderUtilities()
		{
		}

		internal static bool AreEqualCollections<T>(ICollection<T> x, ICollection<T> y)
		{
		}

		internal static bool AreEqualCollections<T>(ICollection<T> x, ICollection<T> y, IEqualityComparer<T> comparer)
		{
		}

		internal static void CheckValidComment(string value, string parameterName)
		{
		}

		internal static void CheckValidQuotedString(string value, string parameterName)
		{
		}

		internal static void CheckValidToken(string value, string parameterName)
		{
		}

		internal static string DumpHeaders(params HttpHeaders[] headers)
		{
		}

		internal static DateTimeOffset? GetDateTimeOffsetValue(string headerName, HttpHeaders store)
		{
		}

		internal static int GetNextNonEmptyOrWhitespaceIndex(string input, int startIndex, bool skipEmptyValues, out bool separatorFound)
		{
		}

		internal static double? GetQuality(ICollection<NameValueHeaderValue> parameters)
		{
		}

		internal static TimeSpan? GetTimeSpanValue(string headerName, HttpHeaders store)
		{
		}

		internal static bool IsValidEmailAddress(string value)
		{
		}

		internal static void SetQuality(ICollection<NameValueHeaderValue> parameters, double? value)
		{
		}

		internal static bool TryParseInt32(string value, out int result)
		{
		}

		internal static bool TryParseInt64(string value, out long result)
		{
		}
	}
}