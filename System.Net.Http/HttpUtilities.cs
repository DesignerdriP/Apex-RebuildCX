using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Net.Http
{
	internal static class HttpUtilities
	{
		internal readonly static Version DefaultVersion;

		internal readonly static byte[] EmptyByteArray;

		static HttpUtilities()
		{
		}

		internal static Task ContinueWithStandard(this Task task, Action<Task> continuation)
		{
		}

		internal static Task ContinueWithStandard<T>(this Task<T> task, Action<Task<T>> continuation)
		{
		}

		internal static bool HandleFaultsAndCancelation<T>(Task task, TaskCompletionSource<T> tcs)
		{
		}

		internal static bool IsHttpUri(Uri uri)
		{
		}
	}
}