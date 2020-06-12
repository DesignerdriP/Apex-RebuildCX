using System;
using System.Text;

namespace System.Net.Http
{
	internal static class HttpRuleParser
	{
		internal const char CR = '\r';

		internal const char LF = '\n';

		internal const int MaxInt64Digits = 19;

		internal const int MaxInt32Digits = 10;

		internal readonly static Encoding DefaultHttpEncoding;

		static HttpRuleParser()
		{
		}

		internal static bool ContainsInvalidNewLine(string value)
		{
		}

		internal static bool ContainsInvalidNewLine(string value, int startIndex)
		{
		}

		internal static string DateToString(DateTimeOffset dateTime)
		{
		}

		internal static HttpParseResult GetCommentLength(string input, int startIndex, out int length)
		{
		}

		internal static int GetHostLength(string input, int startIndex, bool allowToken, out string host)
		{
		}

		internal static int GetNumberLength(string input, int startIndex, bool allowDecimal)
		{
		}

		internal static HttpParseResult GetQuotedPairLength(string input, int startIndex, out int length)
		{
		}

		internal static HttpParseResult GetQuotedStringLength(string input, int startIndex, out int length)
		{
		}

		internal static int GetTokenLength(string input, int startIndex)
		{
		}

		internal static int GetWhitespaceLength(string input, int startIndex)
		{
		}

		internal static bool IsTokenChar(char character)
		{
		}

		internal static bool TryStringToDate(string input, out DateTimeOffset result)
		{
		}
	}
}