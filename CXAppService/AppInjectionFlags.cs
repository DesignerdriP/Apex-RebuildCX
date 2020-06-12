using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace loader.CXAppService
{
	[DataContract(Name="AppInjectionFlags", Namespace="http://schemas.datacontract.org/2004/07/CxAppService.Contracts")]
	[Flags]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum AppInjectionFlags
	{
		[EnumMember]
		WaitForInterval = 1,
		[EnumMember]
		WaitForMainThread = 2,
		[EnumMember]
		WaitForThreadAddress = 4,
		[EnumMember]
		Target64Bit = 8
	}
}