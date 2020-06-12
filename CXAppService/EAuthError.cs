using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace loader.CXAppService
{
	[DataContract(Name="EAuthError", Namespace="http://schemas.datacontract.org/2004/07/CxAppService.Contracts")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum EAuthError
	{
		[EnumMember]
		Success,
		[EnumMember]
		Invalid,
		[EnumMember]
		Failed,
		[EnumMember]
		Banned,
		[EnumMember]
		NeedPasswordChange
	}
}