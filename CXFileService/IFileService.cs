using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace loader.CXFileService
{
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(ConfigurationName="CXFileService.IFileService")]
	public interface IFileService
	{
		[OperationContract(Action="http://tempuri.org/IFileService/GetRemoteFile", ReplyAction="http://tempuri.org/IFileService/GetRemoteFileResponse")]
		RemoteFileInfo GetRemoteFile(DownloadRequest request);

		[OperationContract(Action="http://tempuri.org/IFileService/GetRemoteFile", ReplyAction="http://tempuri.org/IFileService/GetRemoteFileResponse")]
		Task<RemoteFileInfo> GetRemoteFileAsync(DownloadRequest request);
	}
}