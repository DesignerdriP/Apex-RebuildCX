using System;
using System.Xml.Serialization;

namespace loader.App
{
	[Serializable]
	public enum ExecutionType
	{
		[XmlEnum(Name="Normal")]
		Normal,
		[XmlEnum(Name="DLL")]
		InjectDLL
	}
}