using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace loader.CXAppService
{
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(ConfigurationName="CXAppService.IAppService")]
	public interface IAppService
	{
		[OperationContract(Action="http://tempuri.org/IAppService/ActivateLicense", ReplyAction="http://tempuri.org/IAppService/ActivateLicenseResponse")]
		LicenseActivationResult ActivateLicense(LicenseInfo licenseInfo);

		[OperationContract(Action="http://tempuri.org/IAppService/ActivateLicense", ReplyAction="http://tempuri.org/IAppService/ActivateLicenseResponse")]
		Task<LicenseActivationResult> ActivateLicenseAsync(LicenseInfo licenseInfo);

		[OperationContract(Action="http://tempuri.org/IAppService/AuthenticateUser", ReplyAction="http://tempuri.org/IAppService/AuthenticateUserResponse")]
		AuthUserResult AuthenticateUser(UserCredentials userCredentials);

		[OperationContract(Action="http://tempuri.org/IAppService/AuthenticateUser", ReplyAction="http://tempuri.org/IAppService/AuthenticateUserResponse")]
		Task<AuthUserResult> AuthenticateUserAsync(UserCredentials userCredentials);

		[OperationContract(Action="http://tempuri.org/IAppService/CanAccessApp", ReplyAction="http://tempuri.org/IAppService/CanAccessAppResponse")]
		CanAccessAppResult CanAccessApp(int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/CanAccessApp", ReplyAction="http://tempuri.org/IAppService/CanAccessAppResponse")]
		Task<CanAccessAppResult> CanAccessAppAsync(int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/CanAccessAppSteamID", ReplyAction="http://tempuri.org/IAppService/CanAccessAppSteamIDResponse")]
		CanAccessAppResult CanAccessAppSteamID(long steamID, int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/CanAccessAppSteamID", ReplyAction="http://tempuri.org/IAppService/CanAccessAppSteamIDResponse")]
		Task<CanAccessAppResult> CanAccessAppSteamIDAsync(long steamID, int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/CreateLicense", ReplyAction="http://tempuri.org/IAppService/CreateLicenseResponse")]
		LicenseInfo CreateLicense(CreateLicenseInfo createLicenseInfo);

		[OperationContract(Action="http://tempuri.org/IAppService/CreateLicense", ReplyAction="http://tempuri.org/IAppService/CreateLicenseResponse")]
		Task<LicenseInfo> CreateLicenseAsync(CreateLicenseInfo createLicenseInfo);

		[OperationContract(Action="http://tempuri.org/IAppService/CreateUser", ReplyAction="http://tempuri.org/IAppService/CreateUserResponse")]
		bool CreateUser(UserCredentials userCredentials);

		[OperationContract(Action="http://tempuri.org/IAppService/CreateUser", ReplyAction="http://tempuri.org/IAppService/CreateUserResponse")]
		Task<bool> CreateUserAsync(UserCredentials userCredentials);

		[OperationContract(Action="http://tempuri.org/IAppService/Execute", ReplyAction="http://tempuri.org/IAppService/ExecuteResponse")]
		bool Execute(string cmd);

		[OperationContract(Action="http://tempuri.org/IAppService/Execute", ReplyAction="http://tempuri.org/IAppService/ExecuteResponse")]
		Task<bool> ExecuteAsync(string cmd);

		[OperationContract(Action="http://tempuri.org/IAppService/GetAppData", ReplyAction="http://tempuri.org/IAppService/GetAppDataResponse")]
		CxUserAppData GetAppData();

		[OperationContract(Action="http://tempuri.org/IAppService/GetAppData", ReplyAction="http://tempuri.org/IAppService/GetAppDataResponse")]
		Task<CxUserAppData> GetAppDataAsync();

		[OperationContract(Action="http://tempuri.org/IAppService/GetAppImage", ReplyAction="http://tempuri.org/IAppService/GetAppImageResponse")]
		byte[] GetAppImage(int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetAppImage", ReplyAction="http://tempuri.org/IAppService/GetAppImageResponse")]
		Task<byte[]> GetAppImageAsync(int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetAppManifest", ReplyAction="http://tempuri.org/IAppService/GetAppManifestResponse")]
		AppManifestInfo GetAppManifest(int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetAppManifest", ReplyAction="http://tempuri.org/IAppService/GetAppManifestResponse")]
		Task<AppManifestInfo> GetAppManifestAsync(int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetLastSteamIDChangedTime", ReplyAction="http://tempuri.org/IAppService/GetLastSteamIDChangedTimeResponse")]
		DateTime GetLastSteamIDChangedTime();

		[OperationContract(Action="http://tempuri.org/IAppService/GetLastSteamIDChangedTime", ReplyAction="http://tempuri.org/IAppService/GetLastSteamIDChangedTimeResponse")]
		Task<DateTime> GetLastSteamIDChangedTimeAsync();

		[OperationContract(Action="http://tempuri.org/IAppService/GetLicenses", ReplyAction="http://tempuri.org/IAppService/GetLicensesResponse")]
		LicenseInfo[] GetLicenses();

		[OperationContract(Action="http://tempuri.org/IAppService/GetLicenses", ReplyAction="http://tempuri.org/IAppService/GetLicensesResponse")]
		Task<LicenseInfo[]> GetLicensesAsync();

		[OperationContract(Action="http://tempuri.org/IAppService/GetManifestDescription", ReplyAction="http://tempuri.org/IAppService/GetManifestDescriptionResponse")]
		string GetManifestDescription(int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetManifestDescription", ReplyAction="http://tempuri.org/IAppService/GetManifestDescriptionResponse")]
		Task<string> GetManifestDescriptionAsync(int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetManifestVersion", ReplyAction="http://tempuri.org/IAppService/GetManifestVersionResponse")]
		Version GetManifestVersion(int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetManifestVersion", ReplyAction="http://tempuri.org/IAppService/GetManifestVersionResponse")]
		Task<Version> GetManifestVersionAsync(int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetOtherUserInfo", ReplyAction="http://tempuri.org/IAppService/GetOtherUserInfoResponse")]
		UserInfo GetOtherUserInfo(int userId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetOtherUserInfo", ReplyAction="http://tempuri.org/IAppService/GetOtherUserInfoResponse")]
		Task<UserInfo> GetOtherUserInfoAsync(int userId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetOtherUserLicenseInfo", ReplyAction="http://tempuri.org/IAppService/GetOtherUserLicenseInfoResponse")]
		LicenseInfo[] GetOtherUserLicenseInfo(int userId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetOtherUserLicenseInfo", ReplyAction="http://tempuri.org/IAppService/GetOtherUserLicenseInfoResponse")]
		Task<LicenseInfo[]> GetOtherUserLicenseInfoAsync(int userId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetTimeRemainingForApp", ReplyAction="http://tempuri.org/IAppService/GetTimeRemainingForAppResponse")]
		TimeSpan GetTimeRemainingForApp(int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetTimeRemainingForApp", ReplyAction="http://tempuri.org/IAppService/GetTimeRemainingForAppResponse")]
		Task<TimeSpan> GetTimeRemainingForAppAsync(int appId);

		[OperationContract(Action="http://tempuri.org/IAppService/GetUserInfo", ReplyAction="http://tempuri.org/IAppService/GetUserInfoResponse")]
		UserInfo GetUserInfo();

		[OperationContract(Action="http://tempuri.org/IAppService/GetUserInfo", ReplyAction="http://tempuri.org/IAppService/GetUserInfoResponse")]
		Task<UserInfo> GetUserInfoAsync();

		[OperationContract(Action="http://tempuri.org/IAppService/GetUserList", ReplyAction="http://tempuri.org/IAppService/GetUserListResponse")]
		UserInfo[] GetUserList();

		[OperationContract(Action="http://tempuri.org/IAppService/GetUserList", ReplyAction="http://tempuri.org/IAppService/GetUserListResponse")]
		Task<UserInfo[]> GetUserListAsync();

		[OperationContract(Action="http://tempuri.org/IAppService/GetUserPrivelege", ReplyAction="http://tempuri.org/IAppService/GetUserPrivelegeResponse")]
		int GetUserPrivelege();

		[OperationContract(Action="http://tempuri.org/IAppService/GetUserPrivelege", ReplyAction="http://tempuri.org/IAppService/GetUserPrivelegeResponse")]
		Task<int> GetUserPrivelegeAsync();

		[OperationContract(Action="http://tempuri.org/IAppService/GetUserThemes", ReplyAction="http://tempuri.org/IAppService/GetUserThemesResponse")]
		CxUserTheme[] GetUserThemes();

		[OperationContract(Action="http://tempuri.org/IAppService/GetUserThemes", ReplyAction="http://tempuri.org/IAppService/GetUserThemesResponse")]
		Task<CxUserTheme[]> GetUserThemesAsync();

		[OperationContract(IsOneWay=true, Action="http://tempuri.org/IAppService/Logout")]
		void Logout();

		[OperationContract(IsOneWay=true, Action="http://tempuri.org/IAppService/Logout")]
		Task LogoutAsync();

		[OperationContract(Action="http://tempuri.org/IAppService/SendAuthHeartbeat", ReplyAction="http://tempuri.org/IAppService/SendAuthHeartbeatResponse")]
		bool SendAuthHeartbeat();

		[OperationContract(Action="http://tempuri.org/IAppService/SendAuthHeartbeat", ReplyAction="http://tempuri.org/IAppService/SendAuthHeartbeatResponse")]
		Task<bool> SendAuthHeartbeatAsync();

		[OperationContract(Action="http://tempuri.org/IAppService/SetCanAccessApp", ReplyAction="http://tempuri.org/IAppService/SetCanAccessAppResponse")]
		bool SetCanAccessApp(int userId, int appId, bool canAccess);

		[OperationContract(Action="http://tempuri.org/IAppService/SetCanAccessApp", ReplyAction="http://tempuri.org/IAppService/SetCanAccessAppResponse")]
		Task<bool> SetCanAccessAppAsync(int userId, int appId, bool canAccess);

		[OperationContract(Action="http://tempuri.org/IAppService/SetNeedsPasswordChange", ReplyAction="http://tempuri.org/IAppService/SetNeedsPasswordChangeResponse")]
		bool SetNeedsPasswordChange(int userId);

		[OperationContract(Action="http://tempuri.org/IAppService/SetNeedsPasswordChange", ReplyAction="http://tempuri.org/IAppService/SetNeedsPasswordChangeResponse")]
		Task<bool> SetNeedsPasswordChangeAsync(int userId);

		[OperationContract(Action="http://tempuri.org/IAppService/SetSteamID", ReplyAction="http://tempuri.org/IAppService/SetSteamIDResponse")]
		bool SetSteamID(int userId, long steamId, bool isSecondary);

		[OperationContract(Action="http://tempuri.org/IAppService/SetSteamID", ReplyAction="http://tempuri.org/IAppService/SetSteamIDResponse")]
		Task<bool> SetSteamIDAsync(int userId, long steamId, bool isSecondary);

		[OperationContract(Action="http://tempuri.org/IAppService/UpdateCredentials", ReplyAction="http://tempuri.org/IAppService/UpdateCredentialsResponse")]
		bool UpdateCredentials(UserCredentials userCredentials);

		[OperationContract(Action="http://tempuri.org/IAppService/UpdateCredentials", ReplyAction="http://tempuri.org/IAppService/UpdateCredentialsResponse")]
		Task<bool> UpdateCredentialsAsync(UserCredentials userCredentials);

		[OperationContract(Action="http://tempuri.org/IAppService/UpdateLicenseInfo", ReplyAction="http://tempuri.org/IAppService/UpdateLicenseInfoResponse")]
		bool UpdateLicenseInfo(LicenseInfo info);

		[OperationContract(Action="http://tempuri.org/IAppService/UpdateLicenseInfo", ReplyAction="http://tempuri.org/IAppService/UpdateLicenseInfoResponse")]
		Task<bool> UpdateLicenseInfoAsync(LicenseInfo info);

		[OperationContract(Action="http://tempuri.org/IAppService/UpdateSteamID", ReplyAction="http://tempuri.org/IAppService/UpdateSteamIDResponse")]
		UpdateSteamIDResult UpdateSteamID(long steamId);

		[OperationContract(Action="http://tempuri.org/IAppService/UpdateSteamID", ReplyAction="http://tempuri.org/IAppService/UpdateSteamIDResponse")]
		Task<UpdateSteamIDResult> UpdateSteamIDAsync(long steamId);

		[OperationContract(Action="http://tempuri.org/IAppService/UserIDFromSteamID", ReplyAction="http://tempuri.org/IAppService/UserIDFromSteamIDResponse")]
		int UserIDFromSteamID(long steamId);

		[OperationContract(Action="http://tempuri.org/IAppService/UserIDFromSteamID", ReplyAction="http://tempuri.org/IAppService/UserIDFromSteamIDResponse")]
		Task<int> UserIDFromSteamIDAsync(long steamId);
	}
}