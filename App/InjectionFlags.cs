using System;
using System.Xml.Serialization;

namespace loader.App
{
	[Flags]
	[Serializable]
	public enum InjectionFlags
	{
		[XmlEnum(Name="WaitForInterval")]
		WaitForInterval = 1,
		[XmlEnum(Name="WaitForMainThread")]
		WaitForMainThread = 2,
		[XmlEnum(Name="WaitForThreadAddress")]
		WaitForThreadAddress = 4,
		[XmlEnum(Name="Target64Bit")]
		Target64Bit = 8
	}
}