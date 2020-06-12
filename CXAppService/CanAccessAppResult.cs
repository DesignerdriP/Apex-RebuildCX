using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace loader.CXAppService
{
	[DataContract(Name="CanAccessAppResult", Namespace="http://schemas.datacontract.org/2004/07/CxAppService.Contracts")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum CanAccessAppResult
	{
		[EnumMember]
		Success,
		[EnumMember]
		SubscriptionExpired,
		[EnumMember]
		BannedTemp,
		[EnumMember]
		Banned,
		[EnumMember]
		NoAccess
	}
}