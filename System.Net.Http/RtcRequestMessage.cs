using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Net.Http
{
	[ComVisible(true)]
	internal class RtcRequestMessage : HttpRequestMessage, INetworkTransportSettings, INotificationTransportSync
	{
		internal RtcState state;

		static RtcRequestMessage()
		{
		}

		internal RtcRequestMessage(HttpMethod method, Uri uri)
		{
		}

		[SecuritySafeCritical]
		public void ApplySetting([In] ref TRANSPORT_SETTING_ID settingId, [In] int lengthIn, [In] IntPtr valueIn, out int lengthOut, out IntPtr valueOut)
		{
		}

		public void CompleteDelivery()
		{
		}

		public void Flush()
		{
		}

		[SecuritySafeCritical]
		public void QuerySetting([In] ref TRANSPORT_SETTING_ID settingId, [In] int lengthIn, [In] IntPtr valueIn, out int lengthOut, out IntPtr valueOut)
		{
		}
	}
}