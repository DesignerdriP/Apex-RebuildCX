using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace loader.CXAppService
{
	[DataContract(Name="AppRunFlags", Namespace="http://schemas.datacontract.org/2004/07/CxAppService.Contracts")]
	[Flags]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum AppRunFlags
	{
		[EnumMember]
		Normal = 1,
		[EnumMember]
		DLL = 2
	}
}