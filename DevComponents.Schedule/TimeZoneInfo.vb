Imports DevComponents.AdvTree
Imports Microsoft.Win32
Imports Microsoft.Win32.SafeHandles
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization
Imports System.Security
Imports System.Security.AccessControl
Imports System.Security.Permissions
Imports System.Text
Imports System.Threading

Namespace DevComponents.Schedule
	<ComVisible(True)>
	<HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort:=True)>
	<Serializable>
	Public NotInheritable Class TimeZoneInfo
		Implements IEquatable(Of DevComponents.Schedule.TimeZoneInfo), ISerializable, IDeserializationCallback
		Private Const c_daylightValue As String = "Dlt"

		Private Const c_disableDST As String = "DisableAutoDaylightTimeSet"

		Private Const c_disableDynamicDST As String = "DynamicDaylightTimeDisabled"

		Private Const c_displayValue As String = "Display"

		Private Const c_firstEntryValue As String = "FirstEntry"

		Private Const c_lastEntryValue As String = "LastEntry"

		Private Const c_localId As String = "Local"

		Private Const c_maxKeyLength As Integer = 255

		Private Const c_muiDaylightValue As String = "MUI_Dlt"

		Private Const c_muiDisplayValue As String = "MUI_Display"

		Private Const c_muiStandardValue As String = "MUI_Std"

		Private Const c_standardValue As String = "Std"

		Private Const c_ticksPerDay As Long = 864000000000L

		Private Const c_ticksPerDayRange As Long = 863999990000L

		Private Const c_ticksPerHour As Long = 36000000000L

		Private Const c_ticksPerMillisecond As Long = 10000L

		Private Const c_ticksPerMinute As Long = 600000000L

		Private Const c_ticksPerSecond As Long = 10000000L

		Private Const c_timeZoneInfoRegistryHive As String = "SYSTEM\CurrentControlSet\Control\TimeZoneInformation"

		Private Const c_timeZoneInfoRegistryHivePermissionList As String = "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\TimeZoneInformation"

		Private Const c_timeZoneInfoValue As String = "TZI"

		Private Const c_timeZonesRegistryHive As String = "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones"

		Private Const c_timeZonesRegistryHivePermissionList As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones"

		Private Const c_utcId As String = "UTC"

		Private m_adjustmentRules As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule()

		Private m_baseUtcOffset As TimeSpan

		Private m_daylightDisplayName As String

		Private m_displayName As String

		Private m_id As String

		Private m_standardDisplayName As String

		Private m_supportsDaylightSavingTime As Boolean

		Private Shared s_allSystemTimeZonesRead As Boolean

		Private Shared s_hiddenInternalSyncObject As Object

		Private Shared s_hiddenSystemTimeZones As Dictionary(Of String, DevComponents.Schedule.TimeZoneInfo)

		Private Shared s_localTimeZone As DevComponents.Schedule.TimeZoneInfo

		Private Shared s_readOnlySystemTimeZones As List(Of DevComponents.Schedule.TimeZoneInfo)

		Private Shared s_utcTimeZone As DevComponents.Schedule.TimeZoneInfo

		Public ReadOnly Property BaseUtcOffset As TimeSpan
			Get
				Return Me.m_baseUtcOffset
			End Get
		End Property

		Public ReadOnly Property DaylightName As String
			Get
				If (Me.m_daylightDisplayName Is Nothing) Then
					Return String.Empty
				End If
				Return Me.m_daylightDisplayName
			End Get
		End Property

		Public ReadOnly Property DisplayName As String
			Get
				If (Me.m_displayName Is Nothing) Then
					Return String.Empty
				End If
				Return Me.m_displayName
			End Get
		End Property

		Public ReadOnly Property Id As String
			Get
				Return Me.m_id
			End Get
		End Property

		Public ReadOnly Shared Property Local As DevComponents.Schedule.TimeZoneInfo
			<SecurityCritical>
			Get
				Dim sLocalTimeZone As DevComponents.Schedule.TimeZoneInfo
				Dim timeZoneInfo As DevComponents.Schedule.TimeZoneInfo = DevComponents.Schedule.TimeZoneInfo.s_localTimeZone
				If (timeZoneInfo IsNot Nothing) Then
					Return timeZoneInfo
				End If
				SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
					If (DevComponents.Schedule.TimeZoneInfo.s_localTimeZone Is Nothing) Then
						Dim localTimeZone As DevComponents.Schedule.TimeZoneInfo = DevComponents.Schedule.TimeZoneInfo.GetLocalTimeZone()
						DevComponents.Schedule.TimeZoneInfo.s_localTimeZone = New DevComponents.Schedule.TimeZoneInfo(localTimeZone.m_id, localTimeZone.m_baseUtcOffset, localTimeZone.m_displayName, localTimeZone.m_standardDisplayName, localTimeZone.m_daylightDisplayName, localTimeZone.m_adjustmentRules, False)
					End If
					sLocalTimeZone = DevComponents.Schedule.TimeZoneInfo.s_localTimeZone
				End SyncLock
				Return sLocalTimeZone
			End Get
		End Property

		Private ReadOnly Shared Property s_internalSyncObject As Object
			Get
				If (DevComponents.Schedule.TimeZoneInfo.s_hiddenInternalSyncObject Is Nothing) Then
					Dim obj As Object = New Object()
					Interlocked.CompareExchange(DevComponents.Schedule.TimeZoneInfo.s_hiddenInternalSyncObject, obj, Nothing)
				End If
				Return DevComponents.Schedule.TimeZoneInfo.s_hiddenInternalSyncObject
			End Get
		End Property

		Private Shared Property s_systemTimeZones As Dictionary(Of String, DevComponents.Schedule.TimeZoneInfo)
			Get
				If (DevComponents.Schedule.TimeZoneInfo.s_hiddenSystemTimeZones Is Nothing) Then
					DevComponents.Schedule.TimeZoneInfo.s_hiddenSystemTimeZones = New Dictionary(Of String, DevComponents.Schedule.TimeZoneInfo)()
				End If
				Return DevComponents.Schedule.TimeZoneInfo.s_hiddenSystemTimeZones
			End Get
			Set(ByVal value As Dictionary(Of String, DevComponents.Schedule.TimeZoneInfo))
				DevComponents.Schedule.TimeZoneInfo.s_hiddenSystemTimeZones = value
			End Set
		End Property

		Public ReadOnly Property StandardName As String
			Get
				If (Me.m_standardDisplayName Is Nothing) Then
					Return String.Empty
				End If
				Return Me.m_standardDisplayName
			End Get
		End Property

		Public ReadOnly Property SupportsDaylightSavingTime As Boolean
			Get
				Return Me.m_supportsDaylightSavingTime
			End Get
		End Property

		Public ReadOnly Shared Property Utc As DevComponents.Schedule.TimeZoneInfo
			Get
				Dim sUtcTimeZone As DevComponents.Schedule.TimeZoneInfo
				Dim timeZoneInfo As DevComponents.Schedule.TimeZoneInfo = DevComponents.Schedule.TimeZoneInfo.s_utcTimeZone
				If (timeZoneInfo IsNot Nothing) Then
					Return timeZoneInfo
				End If
				SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
					If (DevComponents.Schedule.TimeZoneInfo.s_utcTimeZone Is Nothing) Then
						DevComponents.Schedule.TimeZoneInfo.s_utcTimeZone = DevComponents.Schedule.TimeZoneInfo.CreateCustomTimeZone("UTC", TimeSpan.Zero, "UTC", "UTC")
					End If
					sUtcTimeZone = DevComponents.Schedule.TimeZoneInfo.s_utcTimeZone
				End SyncLock
				Return sUtcTimeZone
			End Get
		End Property

		Private Sub New(ByVal zone As ⟥.⟝, ByVal dstDisabled As Boolean)
			MyBase.New()
			If (Not String.IsNullOrEmpty(zone.⟟)) Then
				Me.m_id = zone.⟟
			Else
				Me.m_id = "Local"
			End If
			Me.m_baseUtcOffset = New TimeSpan(0, -zone.⟞, 0)
			If (Not dstDisabled) Then
				Dim _u27f1 As ⟥.⟱ = New ⟥.⟱(zone)
				Dim [date] As DateTime = DateTime.MinValue.[Date]
				Dim maxValue As DateTime = DateTime.MaxValue
				Dim adjustmentRule As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = DevComponents.Schedule.TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(_u27f1, [date], maxValue.[Date])
				If (adjustmentRule IsNot Nothing) Then
					Me.m_adjustmentRules = New DevComponents.Schedule.TimeZoneInfo.AdjustmentRule() { adjustmentRule }
				End If
			End If
			DevComponents.Schedule.TimeZoneInfo.ValidateTimeZoneInfo(Me.m_id, Me.m_baseUtcOffset, Me.m_adjustmentRules, Me.m_supportsDaylightSavingTime)
			Me.m_displayName = zone.⟟
			Me.m_standardDisplayName = zone.⟟
			Me.m_daylightDisplayName = zone.⟢
		End Sub

		Private Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
			MyBase.New()
			If (info Is Nothing) Then
				Throw New ArgumentNullException("info")
			End If
			Me.m_id = CStr(info.GetValue("Id", GetType(String)))
			Me.m_displayName = CStr(info.GetValue("DisplayName", GetType(String)))
			Me.m_standardDisplayName = CStr(info.GetValue("StandardName", GetType(String)))
			Me.m_daylightDisplayName = CStr(info.GetValue("DaylightName", GetType(String)))
			Me.m_baseUtcOffset = DirectCast(info.GetValue("BaseUtcOffset", GetType(TimeSpan)), TimeSpan)
			Me.m_adjustmentRules = DirectCast(info.GetValue("AdjustmentRules", GetType(TimeZoneInfo.AdjustmentRule())), DevComponents.Schedule.TimeZoneInfo.AdjustmentRule())
			Me.m_supportsDaylightSavingTime = CBool(info.GetValue("SupportsDaylightSavingTime", GetType(Boolean)))
		End Sub

		Private Sub New(ByVal id As String, ByVal baseUtcOffset As TimeSpan, ByVal displayName As String, ByVal standardDisplayName As String, ByVal daylightDisplayName As String, ByVal adjustmentRules As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule(), ByVal disableDaylightSavingTime As Boolean)
			MyBase.New()
			Dim flag As Boolean
			Dim str As String
			DevComponents.Schedule.TimeZoneInfo.ValidateTimeZoneInfo(id, baseUtcOffset, adjustmentRules, flag)
			If (Not disableDaylightSavingTime AndAlso adjustmentRules IsNot Nothing AndAlso CInt(adjustmentRules.Length) > 0) Then
				Me.m_adjustmentRules = DirectCast(adjustmentRules.Clone(), DevComponents.Schedule.TimeZoneInfo.AdjustmentRule())
			End If
			Me.m_id = id
			Me.m_baseUtcOffset = baseUtcOffset
			Me.m_displayName = displayName
			Me.m_standardDisplayName = standardDisplayName
			If (disableDaylightSavingTime) Then
				str = Nothing
			Else
				str = daylightDisplayName
			End If
			Me.m_daylightDisplayName = str
			Me.m_supportsDaylightSavingTime = If(Not flag, False, Not disableDaylightSavingTime)
		End Sub

		<SecurityCritical>
		<SecurityTreatAsSafe>
		Private Shared Function CheckDaylightSavingTimeDisabled() As Boolean
			Dim flag As Boolean
			Try
				Dim permissionSets As PermissionSet = New PermissionSet(PermissionState.None)
				permissionSets.AddPermission(New RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\TimeZoneInformation"))
				permissionSets.Assert()
				Using registryKey As Microsoft.Win32.RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\TimeZoneInformation", RegistryKeyPermissionCheck.[Default], RegistryRights.ExecuteKey)
					If (registryKey IsNot Nothing) Then
						Dim value As Integer = 0
						Try
							value = CInt(registryKey.GetValue("DisableAutoDaylightTimeSet", 0, RegistryValueOptions.None))
						Catch invalidCastException As System.InvalidCastException
						End Try
						If (value <> 1) Then
							Try
								value = CInt(registryKey.GetValue("DynamicDaylightTimeDisabled", 0, RegistryValueOptions.None))
							Catch invalidCastException1 As System.InvalidCastException
							End Try
							If (value <> 1) Then
								Return False
							End If
						End If
						flag = True
					Else
						flag = False
					End If
				End Using
			Finally
				PermissionSet.RevertAssert()
			End Try
			Return flag
		End Function

		Private Shared Function CheckDaylightSavingTimeNotSupported(ByVal timeZone As ⟥.⟝) As Boolean
			If (timeZone.⟣.⟧ <> timeZone.⟠.⟧ OrElse timeZone.⟣.⟨ <> timeZone.⟠.⟨ OrElse timeZone.⟣.⟩ <> timeZone.⟠.⟩ OrElse timeZone.⟣.⟪ <> timeZone.⟠.⟪ OrElse timeZone.⟣.⟫ <> timeZone.⟠.⟫ OrElse timeZone.⟣.⟬ <> timeZone.⟠.⟬ OrElse timeZone.⟣.⟭ <> timeZone.⟠.⟭) Then
				Return False
			End If
			Return timeZone.⟣.⟮ = timeZone.⟠.⟮
		End Function

		Private Shared Function CheckIsDst(ByVal startTime As DateTime, ByVal time As DateTime, ByVal endTime As DateTime) As Boolean
			If (startTime.Year <> endTime.Year) Then
				endTime = endTime.AddYears(startTime.Year - endTime.Year)
			End If
			If (startTime.Year <> time.Year) Then
				time = time.AddYears(startTime.Year - time.Year)
			End If
			If (startTime > endTime) Then
				If (time < endTime) Then
					Return True
				End If
				Return time >= startTime
			End If
			If (time < startTime) Then
				Return False
			End If
			Return time < endTime
		End Function

		Public Shared Sub ClearCachedData()
			SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
				DevComponents.Schedule.TimeZoneInfo.s_localTimeZone = Nothing
				DevComponents.Schedule.TimeZoneInfo.s_utcTimeZone = Nothing
				DevComponents.Schedule.TimeZoneInfo.s_systemTimeZones = Nothing
				DevComponents.Schedule.TimeZoneInfo.s_readOnlySystemTimeZones = Nothing
				DevComponents.Schedule.TimeZoneInfo.s_allSystemTimeZonesRead = False
			End SyncLock
		End Sub

		Public Shared Function ConvertTime(ByVal dateTime As System.DateTime, ByVal destinationTimeZone As DevComponents.Schedule.TimeZoneInfo) As System.DateTime
			Dim dateTime1 As System.DateTime
			If (destinationTimeZone Is Nothing) Then
				Throw New ArgumentNullException("destinationTimeZone")
			End If
			If (dateTime.Kind <> DateTimeKind.Utc) Then
				SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
					dateTime1 = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.Local, destinationTimeZone)
				End SyncLock
			Else
				SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
					dateTime1 = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.Utc, destinationTimeZone)
				End SyncLock
			End If
			Return dateTime1
		End Function

		Public Shared Function ConvertTime(ByVal dateTimeOffset As System.DateTimeOffset, ByVal destinationTimeZone As DevComponents.Schedule.TimeZoneInfo) As System.DateTimeOffset
			If (destinationTimeZone Is Nothing) Then
				Throw New ArgumentNullException("destinationTimeZone")
			End If
			Dim utcDateTime As DateTime = dateTimeOffset.UtcDateTime
			Dim utcOffsetFromUtc As TimeSpan = DevComponents.Schedule.TimeZoneInfo.GetUtcOffsetFromUtc(utcDateTime, destinationTimeZone)
			Dim ticks As Long = utcDateTime.Ticks + utcOffsetFromUtc.Ticks
			If (ticks > System.DateTimeOffset.MaxValue.Ticks) Then
				Return System.DateTimeOffset.MaxValue
			End If
			If (ticks < System.DateTimeOffset.MinValue.Ticks) Then
				Return System.DateTimeOffset.MinValue
			End If
			Return New System.DateTimeOffset(ticks, utcOffsetFromUtc)
		End Function

		Public Shared Function ConvertTime(ByVal dateTime As System.DateTime, ByVal sourceTimeZone As DevComponents.Schedule.TimeZoneInfo, ByVal destinationTimeZone As DevComponents.Schedule.TimeZoneInfo) As System.DateTime
			Return DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, ⟲.ᄱ)
		End Function

		Friend Shared Function ConvertTime(ByVal dateTime As System.DateTime, ByVal sourceTimeZone As DevComponents.Schedule.TimeZoneInfo, ByVal destinationTimeZone As DevComponents.Schedule.TimeZoneInfo, ByVal flags As ⟲) As System.DateTime
			If (sourceTimeZone Is Nothing) Then
				Throw New ArgumentNullException("sourceTimeZone")
			End If
			If (destinationTimeZone Is Nothing) Then
				Throw New ArgumentNullException("destinationTimeZone")
			End If
			Dim correspondingKind As System.DateTimeKind = sourceTimeZone.GetCorrespondingKind()
			If (CInt((flags And ⟲.⟳)) = 0 AndAlso dateTime.Kind <> System.DateTimeKind.Unspecified AndAlso dateTime.Kind <> correspondingKind) Then
				Throw New ArgumentException("Argument_ConvertMismatch", "sourceTimeZone")
			End If
			Dim adjustmentRuleForTime As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = sourceTimeZone.GetAdjustmentRuleForTime(dateTime)
			Dim baseUtcOffset As TimeSpan = sourceTimeZone.BaseUtcOffset
			If (adjustmentRuleForTime IsNot Nothing) Then
				Dim isDaylightSavings As Boolean = False
				Dim daylightTime As System.Globalization.DaylightTime = DevComponents.Schedule.TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime)
				If (CInt((flags And ⟲.⟳)) = 0 AndAlso DevComponents.Schedule.TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime)) Then
					Throw New ArgumentException("Argument_DateTimeIsInvalid", "dateTime")
				End If
				isDaylightSavings = DevComponents.Schedule.TimeZoneInfo.GetIsDaylightSavings(dateTime, adjustmentRuleForTime, daylightTime)
				baseUtcOffset = baseUtcOffset + If(isDaylightSavings, adjustmentRuleForTime.DaylightDelta, TimeSpan.Zero)
			End If
			Dim dateTimeKind As System.DateTimeKind = destinationTimeZone.GetCorrespondingKind()
			If (dateTime.Kind <> System.DateTimeKind.Unspecified AndAlso correspondingKind <> System.DateTimeKind.Unspecified AndAlso correspondingKind = dateTimeKind) Then
				Return dateTime
			End If
			Dim ticks As Long = dateTime.Ticks - baseUtcOffset.Ticks
			Dim timeZone As System.DateTime = DevComponents.Schedule.TimeZoneInfo.ConvertUtcToTimeZone(ticks, destinationTimeZone)
			If (dateTimeKind = System.DateTimeKind.Local) Then
				dateTimeKind = System.DateTimeKind.Unspecified
			End If
			Return New System.DateTime(timeZone.Ticks, dateTimeKind)
		End Function

		Public Shared Function ConvertTimeBySystemTimeZoneId(ByVal dateTime As System.DateTime, ByVal destinationTimeZoneId As String) As System.DateTime
			Return DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId))
		End Function

		Public Shared Function ConvertTimeBySystemTimeZoneId(ByVal dateTimeOffset As System.DateTimeOffset, ByVal destinationTimeZoneId As String) As System.DateTimeOffset
			Return DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTimeOffset, DevComponents.Schedule.TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId))
		End Function

		Public Shared Function ConvertTimeBySystemTimeZoneId(ByVal dateTime As System.DateTime, ByVal sourceTimeZoneId As String, ByVal destinationTimeZoneId As String) As System.DateTime
			Dim dateTime1 As System.DateTime
			If (dateTime.Kind <> DateTimeKind.Local OrElse String.Compare(sourceTimeZoneId, DevComponents.Schedule.TimeZoneInfo.Local.Id, StringComparison.OrdinalIgnoreCase) <> 0) Then
				If (dateTime.Kind <> DateTimeKind.Utc OrElse String.Compare(sourceTimeZoneId, DevComponents.Schedule.TimeZoneInfo.Utc.Id, StringComparison.OrdinalIgnoreCase) <> 0) Then
					Return DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), DevComponents.Schedule.TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId))
				End If
				SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
					dateTime1 = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.Utc, DevComponents.Schedule.TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId))
				End SyncLock
			Else
				SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
					dateTime1 = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.Local, DevComponents.Schedule.TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId))
				End SyncLock
			End If
			Return dateTime1
		End Function

		Public Shared Function ConvertTimeFromUtc(ByVal dateTime As System.DateTime, ByVal destinationTimeZone As DevComponents.Schedule.TimeZoneInfo) As System.DateTime
			Dim dateTime1 As System.DateTime
			SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
				dateTime1 = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.Utc, destinationTimeZone)
			End SyncLock
			Return dateTime1
		End Function

		Public Shared Function ConvertTimeToUtc(ByVal dateTime As System.DateTime) As System.DateTime
			Dim dateTime1 As System.DateTime
			If (dateTime.Kind = DateTimeKind.Utc) Then
				Return dateTime
			End If
			SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
				dateTime1 = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.Local, DevComponents.Schedule.TimeZoneInfo.Utc)
			End SyncLock
			Return dateTime1
		End Function

		Public Shared Function ConvertTimeToUtc(ByVal dateTime As System.DateTime, ByVal sourceTimeZone As DevComponents.Schedule.TimeZoneInfo) As System.DateTime
			Dim dateTime1 As System.DateTime
			SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
				dateTime1 = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, DevComponents.Schedule.TimeZoneInfo.Utc)
			End SyncLock
			Return dateTime1
		End Function

		Private Shared Function ConvertUtcToTimeZone(ByVal ticks As Long, ByVal destinationTimeZone As DevComponents.Schedule.TimeZoneInfo) As DateTime
			Dim maxValue As DateTime
			If (ticks <= DateTime.MaxValue.Ticks) Then
				maxValue = If(ticks >= DateTime.MinValue.Ticks, New DateTime(ticks), DateTime.MinValue)
			Else
				maxValue = DateTime.MaxValue
			End If
			ticks += DevComponents.Schedule.TimeZoneInfo.GetUtcOffsetFromUtc(maxValue, destinationTimeZone).Ticks
			If (ticks > DateTime.MaxValue.Ticks) Then
				Return DateTime.MaxValue
			End If
			If (ticks < DateTime.MinValue.Ticks) Then
				Return DateTime.MinValue
			End If
			Return New DateTime(ticks)
		End Function

		Private Shared Function CreateAdjustmentRuleFromTimeZoneInformation(ByVal timeZoneInformation As ⟥.⟱, ByVal startDate As DateTime, ByVal endDate As DateTime) As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule
			If (timeZoneInformation.⟠.⟨ = 0) Then
				Return Nothing
			End If
			Dim nullable As Nullable(Of DevComponents.Schedule.TimeZoneInfo.TransitionTime) = DevComponents.Schedule.TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, True)
			If (Not nullable.HasValue) Then
				Return Nothing
			End If
			Dim nullable1 As Nullable(Of DevComponents.Schedule.TimeZoneInfo.TransitionTime) = DevComponents.Schedule.TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, False)
			If (Not nullable1.HasValue) Then
				Return Nothing
			End If
			If (nullable.Equals(nullable1)) Then
				Return Nothing
			End If
			Return DevComponents.Schedule.TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, New TimeSpan(0, -timeZoneInformation.⟤, 0), nullable.Value, nullable1.Value)
		End Function

		Public Shared Function CreateCustomTimeZone(ByVal id As String, ByVal baseUtcOffset As TimeSpan, ByVal displayName As String, ByVal standardDisplayName As String) As DevComponents.Schedule.TimeZoneInfo
			Return New DevComponents.Schedule.TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, standardDisplayName, Nothing, False)
		End Function

		Public Shared Function CreateCustomTimeZone(ByVal id As String, ByVal baseUtcOffset As TimeSpan, ByVal displayName As String, ByVal standardDisplayName As String, ByVal daylightDisplayName As String, ByVal adjustmentRules As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule()) As DevComponents.Schedule.TimeZoneInfo
			Return New DevComponents.Schedule.TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, False)
		End Function

		Public Shared Function CreateCustomTimeZone(ByVal id As String, ByVal baseUtcOffset As TimeSpan, ByVal displayName As String, ByVal standardDisplayName As String, ByVal daylightDisplayName As String, ByVal adjustmentRules As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule(), ByVal disableDaylightSavingTime As Boolean) As DevComponents.Schedule.TimeZoneInfo
			Return New DevComponents.Schedule.TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, disableDaylightSavingTime)
		End Function

		Public Function Equals(ByVal other As DevComponents.Schedule.TimeZoneInfo) As Boolean Implements IEquatable(Of DevComponents.Schedule.TimeZoneInfo).Equals
			If (other Is Nothing OrElse String.Compare(Me.m_id, other.m_id, StringComparison.OrdinalIgnoreCase) <> 0) Then
				Return False
			End If
			Return Me.HasSameRules(other)
		End Function

		<SecurityCritical>
		<SecurityTreatAsSafe>
		Private Shared Function FindIdFromTimeZoneInformation(ByVal timeZone As ⟥.⟝, <Out> ByRef dstDisabled As Boolean) As String
			Dim str As String
			dstDisabled = False
			Try
				Dim permissionSets As PermissionSet = New PermissionSet(PermissionState.None)
				permissionSets.AddPermission(New RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones"))
				permissionSets.Assert()
				Using registryKey As Microsoft.Win32.RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones", RegistryKeyPermissionCheck.[Default], RegistryRights.ExecuteKey)
					If (registryKey IsNot Nothing) Then
						Dim subKeyNames As String() = registryKey.GetSubKeyNames()
						Dim num As Integer = 0
						While num < CInt(subKeyNames.Length)
							Dim str1 As String = subKeyNames(num)
							If (Not DevComponents.Schedule.TimeZoneInfo.TryCompareTimeZoneInformationToRegistry(timeZone, str1, dstDisabled)) Then
								num = num + 1
							Else
								str = str1
								Return str
							End If
						End While
					Else
						str = Nothing
						Return str
					End If
				End Using
				Return Nothing
			Finally
				PermissionSet.RevertAssert()
			End Try
			Return str
		End Function

		Public Shared Function FindSystemTimeZoneById(ByVal id As String) As DevComponents.Schedule.TimeZoneInfo
			Dim timeZone As DevComponents.Schedule.TimeZoneInfo
			If (String.Compare(id, "UTC", StringComparison.OrdinalIgnoreCase) = 0) Then
				Return DevComponents.Schedule.TimeZoneInfo.Utc
			End If
			SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
				timeZone = DevComponents.Schedule.TimeZoneInfo.GetTimeZone(id)
			End SyncLock
			Return timeZone
		End Function

		Public Shared Function FromSerializedString(ByVal source As String) As DevComponents.Schedule.TimeZoneInfo
			If (source Is Nothing) Then
				Throw New ArgumentNullException("source")
			End If
			If (source.Length = 0) Then
				Throw New ArgumentException("Argument_InvalidSerializedString source")
			End If
			Return DevComponents.Schedule.TimeZoneInfo.⟻.⠎(source)
		End Function

		Private Function GetAdjustmentRuleForTime(ByVal dateTime As System.DateTime) As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule
			If (Me.m_adjustmentRules IsNot Nothing AndAlso CInt(Me.m_adjustmentRules.Length) <> 0) Then
				Dim [date] As System.DateTime = dateTime.[Date]
				For i As Integer = 0 To CInt(Me.m_adjustmentRules.Length)
					If (Me.m_adjustmentRules(i).DateStart <= [date] AndAlso Me.m_adjustmentRules(i).DateEnd >= [date]) Then
						Return Me.m_adjustmentRules(i)
					End If
				Next

			End If
			Return Nothing
		End Function

		Public Function GetAdjustmentRules() As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule()
			If (Me.m_adjustmentRules Is Nothing) Then
				Return New DevComponents.Schedule.TimeZoneInfo.AdjustmentRule(-1) {}
			End If
			Return DirectCast(Me.m_adjustmentRules.Clone(), DevComponents.Schedule.TimeZoneInfo.AdjustmentRule())
		End Function

		Public Function GetAmbiguousTimeOffsets(ByVal dateTime As System.DateTime) As TimeSpan()
			Dim dateTime1 As System.DateTime
			If (Not Me.m_supportsDaylightSavingTime) Then
				Throw New ArgumentException("Argument_DateTimeIsNotAmbiguous", "dateTime")
			End If
			If (dateTime.Kind = DateTimeKind.Local) Then
				SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
					dateTime1 = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.Local, Me, ⟲.⟳)
				End SyncLock
			ElseIf (dateTime.Kind <> DateTimeKind.Utc) Then
				dateTime1 = dateTime
			Else
				SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
					dateTime1 = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.Utc, Me, ⟲.⟳)
				End SyncLock
			End If
			Dim isAmbiguousTime As Boolean = False
			Dim adjustmentRuleForTime As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = Me.GetAdjustmentRuleForTime(dateTime1)
			If (adjustmentRuleForTime IsNot Nothing) Then
				Dim daylightTime As System.Globalization.DaylightTime = DevComponents.Schedule.TimeZoneInfo.GetDaylightTime(dateTime1.Year, adjustmentRuleForTime)
				isAmbiguousTime = DevComponents.Schedule.TimeZoneInfo.GetIsAmbiguousTime(dateTime1, adjustmentRuleForTime, daylightTime)
			End If
			If (Not isAmbiguousTime) Then
				Throw New ArgumentException("Argument_DateTimeIsNotAmbiguous", "dateTime")
			End If
			Dim mBaseUtcOffset(1) As TimeSpan
			If (adjustmentRuleForTime.DaylightDelta > TimeSpan.Zero) Then
				mBaseUtcOffset(0) = Me.m_baseUtcOffset
				mBaseUtcOffset(1) = Me.m_baseUtcOffset + adjustmentRuleForTime.DaylightDelta
				Return mBaseUtcOffset
			End If
			mBaseUtcOffset(0) = Me.m_baseUtcOffset + adjustmentRuleForTime.DaylightDelta
			mBaseUtcOffset(1) = Me.m_baseUtcOffset
			Return mBaseUtcOffset
		End Function

		Public Function GetAmbiguousTimeOffsets(ByVal dateTimeOffset As System.DateTimeOffset) As TimeSpan()
			If (Not Me.m_supportsDaylightSavingTime) Then
				Throw New ArgumentException("Argument_DateTimeOffsetIsNotAmbiguous", "dateTimeOffset")
			End If
			Dim dateTime As System.DateTime = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTimeOffset, Me).DateTime
			Dim isAmbiguousTime As Boolean = False
			Dim adjustmentRuleForTime As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = Me.GetAdjustmentRuleForTime(dateTime)
			If (adjustmentRuleForTime IsNot Nothing) Then
				Dim daylightTime As System.Globalization.DaylightTime = DevComponents.Schedule.TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime)
				isAmbiguousTime = DevComponents.Schedule.TimeZoneInfo.GetIsAmbiguousTime(dateTime, adjustmentRuleForTime, daylightTime)
			End If
			If (Not isAmbiguousTime) Then
				Throw New ArgumentException("Argument_DateTimeOffsetIsNotAmbiguous", "dateTimeOffset")
			End If
			Dim mBaseUtcOffset(1) As TimeSpan
			If (adjustmentRuleForTime.DaylightDelta > TimeSpan.Zero) Then
				mBaseUtcOffset(0) = Me.m_baseUtcOffset
				mBaseUtcOffset(1) = Me.m_baseUtcOffset + adjustmentRuleForTime.DaylightDelta
				Return mBaseUtcOffset
			End If
			mBaseUtcOffset(0) = Me.m_baseUtcOffset + adjustmentRuleForTime.DaylightDelta
			mBaseUtcOffset(1) = Me.m_baseUtcOffset
			Return mBaseUtcOffset
		End Function

		Private Function GetCorrespondingKind() As DateTimeKind
			If (Me = DevComponents.Schedule.TimeZoneInfo.s_utcTimeZone) Then
				Return DateTimeKind.Utc
			End If
			If (Me = DevComponents.Schedule.TimeZoneInfo.s_localTimeZone) Then
				Return DateTimeKind.Local
			End If
			Return DateTimeKind.Unspecified
		End Function

		Private Shared Function GetDaylightTime(ByVal year As Integer, ByVal rule As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule) As DaylightTime
			Dim daylightDelta As TimeSpan = rule.DaylightDelta
			Dim dateTime As System.DateTime = DevComponents.Schedule.TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionStart)
			Return New DaylightTime(dateTime, DevComponents.Schedule.TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionEnd), daylightDelta)
		End Function

		Public Overrides Function GetHashCode() As Integer
			Return Me.m_id.ToUpperInvariant().GetHashCode()
		End Function

		Private Shared Function GetIsAmbiguousTime(ByVal time As System.DateTime, ByVal rule As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule, ByVal daylightTime As System.Globalization.DaylightTime) As Boolean
			Dim start As System.DateTime
			Dim [end] As System.DateTime
			Dim dateTime As System.DateTime
			Dim flag As Boolean = False
			If (rule IsNot Nothing AndAlso rule.DaylightDelta <> TimeSpan.Zero) Then
				If (rule.DaylightDelta <= TimeSpan.Zero) Then
					start = daylightTime.Start
					[end] = daylightTime.Start + rule.DaylightDelta
				Else
					start = daylightTime.[End]
					[end] = daylightTime.[End] - rule.DaylightDelta
				End If
				flag = If(time < [end], False, time < start)
				If (flag OrElse start.Year = [end].Year) Then
					Return flag
				End If
				Try
					dateTime = start.AddYears(1)
					flag = If(time < [end].AddYears(1), False, time < dateTime)
				Catch argumentOutOfRangeException As System.ArgumentOutOfRangeException
				End Try
				If (flag) Then
					Return flag
				End If
				Try
					dateTime = start.AddYears(-1)
					flag = If(time < [end].AddYears(-1), False, time < dateTime)
				Catch argumentOutOfRangeException1 As System.ArgumentOutOfRangeException
				End Try
			End If
			Return flag
		End Function

		Private Shared Function GetIsDaylightSavings(ByVal time As DateTime, ByVal rule As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule, ByVal daylightTime As System.Globalization.DaylightTime) As Boolean
			If (rule Is Nothing) Then
				Return False
			End If
			Dim daylightDelta As Boolean = rule.DaylightDelta > TimeSpan.Zero
			Dim start As DateTime = daylightTime.Start + If(daylightDelta, rule.DaylightDelta, TimeSpan.Zero)
			Return DevComponents.Schedule.TimeZoneInfo.CheckIsDst(start, time, daylightTime.[End] + If(daylightDelta, -rule.DaylightDelta, TimeSpan.Zero))
		End Function

		Private Shared Function GetIsDaylightSavingsFromUtc(ByVal time As DateTime, ByVal Year As Integer, ByVal utc As System.TimeSpan, ByVal rule As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule) As Boolean
			If (rule Is Nothing) Then
				Return False
			End If
			Dim timeSpan As System.TimeSpan = utc
			Dim daylightTime As System.Globalization.DaylightTime = DevComponents.Schedule.TimeZoneInfo.GetDaylightTime(Year, rule)
			Dim start As DateTime = daylightTime.Start - timeSpan
			Dim [end] As DateTime = (daylightTime.[End] - timeSpan) - rule.DaylightDelta
			Return DevComponents.Schedule.TimeZoneInfo.CheckIsDst(start, time, [end])
		End Function

		Private Shared Function GetIsInvalidTime(ByVal time As System.DateTime, ByVal rule As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule, ByVal daylightTime As System.Globalization.DaylightTime) As Boolean
			Dim start As System.DateTime
			Dim [end] As System.DateTime
			Dim dateTime As System.DateTime
			Dim dateTime1 As System.DateTime
			Dim flag As Boolean = False
			If (rule IsNot Nothing AndAlso rule.DaylightDelta <> TimeSpan.Zero) Then
				If (rule.DaylightDelta >= TimeSpan.Zero) Then
					start = daylightTime.Start
					[end] = daylightTime.Start + rule.DaylightDelta
				Else
					start = daylightTime.[End]
					[end] = daylightTime.[End] - rule.DaylightDelta
				End If
				flag = If(time < start, False, time < [end])
				If (flag OrElse start.Year = [end].Year) Then
					Return flag
				End If
				Try
					dateTime = start.AddYears(1)
					dateTime1 = [end].AddYears(1)
					flag = If(time < dateTime, False, time < dateTime1)
				Catch argumentOutOfRangeException As System.ArgumentOutOfRangeException
				End Try
				If (flag) Then
					Return flag
				End If
				Try
					dateTime = start.AddYears(-1)
					dateTime1 = [end].AddYears(-1)
					flag = If(time < dateTime, False, time < dateTime1)
				Catch argumentOutOfRangeException1 As System.ArgumentOutOfRangeException
				End Try
			End If
			Return flag
		End Function

		<SecurityCritical>
		Private Shared Function GetLocalTimeZone() As DevComponents.Schedule.TimeZoneInfo
			Dim timeZoneInfo As DevComponents.Schedule.TimeZoneInfo
			Dim timeZoneInfo1 As DevComponents.Schedule.TimeZoneInfo
			Dim exception As System.Exception
			Dim timeZoneInfo2 As DevComponents.Schedule.TimeZoneInfo
			Dim exception1 As System.Exception
			Dim flag As Boolean
			Dim timeZoneInfo3 As DevComponents.Schedule.TimeZoneInfo
			Dim exception2 As System.Exception
			Dim str As String = Nothing
			Try
				Dim _u27ef As ⟥.⟯ = New ⟥.⟯()
				If (CLng(⟴.GetDynamicTimeZoneInformation(_u27ef)) <> CLng(-1)) Then
					Dim _u27dd As ⟥.⟝ = New ⟥.⟝(_u27ef)
					Dim flag1 As Boolean = DevComponents.Schedule.TimeZoneInfo.CheckDaylightSavingTimeDisabled()
					If (String.IsNullOrEmpty(_u27ef.⟰) OrElse DevComponents.Schedule.TimeZoneInfo.TryGetTimeZone(_u27ef.⟰, flag1, timeZoneInfo1, exception) <> DevComponents.Schedule.TimeZoneInfo.⟶.⟷) Then
						str = DevComponents.Schedule.TimeZoneInfo.FindIdFromTimeZoneInformation(_u27dd, flag1)
						timeZoneInfo = If(str Is Nothing OrElse DevComponents.Schedule.TimeZoneInfo.TryGetTimeZone(str, flag1, timeZoneInfo2, exception1) <> DevComponents.Schedule.TimeZoneInfo.⟶.⟷, DevComponents.Schedule.TimeZoneInfo.GetLocalTimeZoneFromWin32Data(_u27dd, flag1), timeZoneInfo2)
					Else
						timeZoneInfo = timeZoneInfo1
					End If
				Else
					timeZoneInfo = DevComponents.Schedule.TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local")
				End If
			Catch entryPointNotFoundException As System.EntryPointNotFoundException
				Dim _u27dd1 As ⟥.⟝ = New ⟥.⟝()
				If (CLng(⟴.GetTimeZoneInformation(_u27dd1)) <> CLng(-1)) Then
					str = DevComponents.Schedule.TimeZoneInfo.FindIdFromTimeZoneInformation(_u27dd1, flag)
					timeZoneInfo = If(str Is Nothing OrElse DevComponents.Schedule.TimeZoneInfo.TryGetTimeZone(str, flag, timeZoneInfo3, exception2) <> DevComponents.Schedule.TimeZoneInfo.⟶.⟷, DevComponents.Schedule.TimeZoneInfo.GetLocalTimeZoneFromWin32Data(_u27dd1, flag), timeZoneInfo3)
				Else
					timeZoneInfo = DevComponents.Schedule.TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local")
				End If
			End Try
			Return timeZoneInfo
		End Function

		Private Shared Function GetLocalTimeZoneFromWin32Data(ByVal timeZoneInformation As ⟥.⟝, ByVal dstDisabled As Boolean) As DevComponents.Schedule.TimeZoneInfo
			Dim timeZoneInfo As DevComponents.Schedule.TimeZoneInfo = Nothing
			Try
				timeZoneInfo = New DevComponents.Schedule.TimeZoneInfo(timeZoneInformation, dstDisabled)
			Catch argumentException As System.ArgumentException
			Catch invalidTimeZoneException As DevComponents.Schedule.InvalidTimeZoneException
			End Try
			Return timeZoneInfo
		End Function

		<SecurityCritical>
		<SecurityTreatAsSafe>
		Public Shared Function GetSystemTimeZones() As List(Of DevComponents.Schedule.TimeZoneInfo)
			Dim timeZoneInfos As List(Of DevComponents.Schedule.TimeZoneInfo)
			Dim sReadOnlySystemTimeZones As List(Of DevComponents.Schedule.TimeZoneInfo)
			Dim timeZoneInfo As DevComponents.Schedule.TimeZoneInfo
			Dim exception As System.Exception
			SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
				If (Not DevComponents.Schedule.TimeZoneInfo.s_allSystemTimeZonesRead) Then
					Dim permissionSets As PermissionSet = New PermissionSet(PermissionState.None)
					permissionSets.AddPermission(New RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones"))
					permissionSets.Assert()
					Using registryKey As Microsoft.Win32.RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones", RegistryKeyPermissionCheck.[Default], RegistryRights.ExecuteKey)
						If (registryKey IsNot Nothing) Then
							Dim subKeyNames As String() = registryKey.GetSubKeyNames()
							For i As Integer = 0 To CInt(subKeyNames.Length)
								Dim str As String = subKeyNames(i)
								DevComponents.Schedule.TimeZoneInfo.TryGetTimeZone(str, False, timeZoneInfo, exception)
							Next

						Else
							timeZoneInfos = If(DevComponents.Schedule.TimeZoneInfo.s_systemTimeZones Is Nothing, New List(Of DevComponents.Schedule.TimeZoneInfo)(), New List(Of DevComponents.Schedule.TimeZoneInfo)(DevComponents.Schedule.TimeZoneInfo.s_systemTimeZones.Values))
							DevComponents.Schedule.TimeZoneInfo.s_readOnlySystemTimeZones = New List(Of DevComponents.Schedule.TimeZoneInfo)(timeZoneInfos)
							DevComponents.Schedule.TimeZoneInfo.s_allSystemTimeZonesRead = True
							sReadOnlySystemTimeZones = DevComponents.Schedule.TimeZoneInfo.s_readOnlySystemTimeZones
							Return sReadOnlySystemTimeZones
						End If
					End Using
					Dim _u2825 As IComparer(Of DevComponents.Schedule.TimeZoneInfo) = New DevComponents.Schedule.TimeZoneInfo.⠥()
					Dim timeZoneInfos1 As List(Of DevComponents.Schedule.TimeZoneInfo) = New List(Of DevComponents.Schedule.TimeZoneInfo)(DevComponents.Schedule.TimeZoneInfo.s_systemTimeZones.Values)
					timeZoneInfos1.Sort(_u2825)
					DevComponents.Schedule.TimeZoneInfo.s_readOnlySystemTimeZones = New List(Of DevComponents.Schedule.TimeZoneInfo)(timeZoneInfos1)
					DevComponents.Schedule.TimeZoneInfo.s_allSystemTimeZonesRead = True
				End If
				sReadOnlySystemTimeZones = DevComponents.Schedule.TimeZoneInfo.s_readOnlySystemTimeZones
			End SyncLock
			Return sReadOnlySystemTimeZones
		End Function

		Private Shared Function GetTimeZone(ByVal id As String) As DevComponents.Schedule.TimeZoneInfo
			Dim timeZoneInfo As DevComponents.Schedule.TimeZoneInfo
			Dim exception As System.Exception
			If (id Is Nothing) Then
				Throw New ArgumentNullException("id")
			End If
			If (id.Length = 0 OrElse id.Length > 255 OrElse id.Contains(" ")) Then
				Throw New DevComponents.Schedule.TimeZoneNotFoundException("TimeZoneNotFound_MissingRegistryData, id")
			End If
			Select Case DevComponents.Schedule.TimeZoneInfo.TryGetTimeZone(id, False, timeZoneInfo, exception)
				Case DevComponents.Schedule.TimeZoneInfo.⟶.⟷
					Return timeZoneInfo
				Case DevComponents.Schedule.TimeZoneInfo.⟶.⟸
					Throw New DevComponents.Schedule.TimeZoneNotFoundException("TimeZoneNotFound_MissingRegistryData, id", exception)
				Case DevComponents.Schedule.TimeZoneInfo.⟶.⟹
					Throw New DevComponents.Schedule.InvalidTimeZoneException("InvalidTimeZone_InvalidRegistryData, id", exception)
				Case DevComponents.Schedule.TimeZoneInfo.⟶.⟺
					Throw New SecurityException("Security_CannotReadRegistryData, id", exception)
				Case Else
					Throw New DevComponents.Schedule.TimeZoneNotFoundException("TimeZoneNotFound_MissingRegistryData, id", exception)
			End Select
		End Function

		Public Function GetUtcOffset(ByVal dateTime As System.DateTime) As TimeSpan
			Dim utcOffset As TimeSpan
			If (dateTime.Kind <> DateTimeKind.Local) Then
				If (dateTime.Kind <> DateTimeKind.Utc) Then
					Return DevComponents.Schedule.TimeZoneInfo.GetUtcOffset(dateTime, Me)
				End If
				If (Me.GetCorrespondingKind() = DateTimeKind.Utc) Then
					Return Me.m_baseUtcOffset
				End If
				Return DevComponents.Schedule.TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, Me)
			End If
			SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
				If (Me.GetCorrespondingKind() <> DateTimeKind.Local) Then
					Dim dateTime1 As System.DateTime = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.Local, DevComponents.Schedule.TimeZoneInfo.Utc, ⟲.⟳)
					Return DevComponents.Schedule.TimeZoneInfo.GetUtcOffsetFromUtc(dateTime1, Me)
				Else
					utcOffset = DevComponents.Schedule.TimeZoneInfo.GetUtcOffset(dateTime, Me)
				End If
			End SyncLock
			Return utcOffset
		End Function

		Public Function GetUtcOffset(ByVal dateTimeOffset As System.DateTimeOffset) As TimeSpan
			Return DevComponents.Schedule.TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, Me)
		End Function

		Private Shared Function GetUtcOffset(ByVal time As DateTime, ByVal zone As DevComponents.Schedule.TimeZoneInfo) As TimeSpan
			Dim baseUtcOffset As TimeSpan = zone.BaseUtcOffset
			Dim adjustmentRuleForTime As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = zone.GetAdjustmentRuleForTime(time)
			If (adjustmentRuleForTime IsNot Nothing) Then
				Dim daylightTime As System.Globalization.DaylightTime = DevComponents.Schedule.TimeZoneInfo.GetDaylightTime(time.Year, adjustmentRuleForTime)
				Dim isDaylightSavings As Boolean = DevComponents.Schedule.TimeZoneInfo.GetIsDaylightSavings(time, adjustmentRuleForTime, daylightTime)
				baseUtcOffset = baseUtcOffset + If(isDaylightSavings, adjustmentRuleForTime.DaylightDelta, TimeSpan.Zero)
			End If
			Return baseUtcOffset
		End Function

		Private Shared Function GetUtcOffsetFromUtc(ByVal time As DateTime, ByVal zone As DevComponents.Schedule.TimeZoneInfo) As TimeSpan
			Dim flag As Boolean
			Return DevComponents.Schedule.TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, flag)
		End Function

		Private Shared Function GetUtcOffsetFromUtc(ByVal time As System.DateTime, ByVal zone As DevComponents.Schedule.TimeZoneInfo, <Out> ByRef isDaylightSavings As Boolean) As TimeSpan
			Dim adjustmentRuleForTime As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule
			Dim year As Integer
			isDaylightSavings = False
			Dim baseUtcOffset As TimeSpan = zone.BaseUtcOffset
			If (time > New System.DateTime(9999, 12, 31)) Then
				adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(System.DateTime.MaxValue)
				year = 9999
			ElseIf (time >= New System.DateTime(1, 1, 2)) Then
				Dim dateTime As System.DateTime = time + baseUtcOffset
				year = time.Year
				adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(dateTime)
			Else
				adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(System.DateTime.MinValue)
				year = 1
			End If
			If (adjustmentRuleForTime IsNot Nothing) Then
				isDaylightSavings = DevComponents.Schedule.TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, year, zone.m_baseUtcOffset, adjustmentRuleForTime)
				baseUtcOffset = baseUtcOffset + If(isDaylightSavings, adjustmentRuleForTime.DaylightDelta, TimeSpan.Zero)
			End If
			Return baseUtcOffset
		End Function

		Public Function HasSameRules(ByVal other As DevComponents.Schedule.TimeZoneInfo) As Boolean
			Dim flag As Boolean
			If (other Is Nothing) Then
				Throw New ArgumentNullException("other")
			End If
			If (Me.m_baseUtcOffset <> other.m_baseUtcOffset OrElse Me.m_supportsDaylightSavingTime <> other.m_supportsDaylightSavingTime) Then
				Return False
			End If
			Dim mAdjustmentRules As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule() = Me.m_adjustmentRules
			Dim adjustmentRuleArray As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule() = other.m_adjustmentRules
			If (mAdjustmentRules IsNot Nothing OrElse adjustmentRuleArray IsNot Nothing) Then
				flag = If(mAdjustmentRules Is Nothing, False, adjustmentRuleArray IsNot Nothing)
			Else
				flag = True
			End If
			Dim flag1 As Boolean = flag
			If (Not flag1) Then
				Return False
			End If
			If (mAdjustmentRules IsNot Nothing) Then
				If (CInt(mAdjustmentRules.Length) <> CInt(adjustmentRuleArray.Length)) Then
					Return False
				End If
				For i As Integer = 0 To CInt(mAdjustmentRules.Length)
					If (Not mAdjustmentRules(i).Equals(adjustmentRuleArray(i))) Then
						Return False
					End If
				Next

			End If
			Return flag1
		End Function

		Public Function IsAmbiguousTime(ByVal dateTime As System.DateTime) As Boolean
			Dim dateTime1 As System.DateTime
			If (Not Me.m_supportsDaylightSavingTime) Then
				Return False
			End If
			If (dateTime.Kind = DateTimeKind.Local) Then
				SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
					dateTime1 = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.Local, Me, ⟲.⟳)
				End SyncLock
			ElseIf (dateTime.Kind <> DateTimeKind.Utc) Then
				dateTime1 = dateTime
			Else
				SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
					dateTime1 = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.Utc, Me, ⟲.⟳)
				End SyncLock
			End If
			Dim adjustmentRuleForTime As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = Me.GetAdjustmentRuleForTime(dateTime1)
			If (adjustmentRuleForTime Is Nothing) Then
				Return False
			End If
			Dim daylightTime As System.Globalization.DaylightTime = DevComponents.Schedule.TimeZoneInfo.GetDaylightTime(dateTime1.Year, adjustmentRuleForTime)
			Return DevComponents.Schedule.TimeZoneInfo.GetIsAmbiguousTime(dateTime1, adjustmentRuleForTime, daylightTime)
		End Function

		Public Function IsAmbiguousTime(ByVal dateTimeOffset As System.DateTimeOffset) As Boolean
			If (Not Me.m_supportsDaylightSavingTime) Then
				Return False
			End If
			Dim dateTimeOffset1 As System.DateTimeOffset = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTimeOffset, Me)
			Return Me.IsAmbiguousTime(dateTimeOffset1.DateTime)
		End Function

		Public Function IsDaylightSavingTime(ByVal dateTime As System.DateTime) As Boolean
			Dim dateTime1 As System.DateTime
			Dim flag As Boolean
			If (Not Me.m_supportsDaylightSavingTime OrElse Me.m_adjustmentRules Is Nothing) Then
				Return False
			End If
			If (dateTime.Kind <> DateTimeKind.Local) Then
				If (dateTime.Kind = DateTimeKind.Utc) Then
					If (Me.GetCorrespondingKind() = DateTimeKind.Utc) Then
						Return False
					End If
					DevComponents.Schedule.TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, Me, flag)
					Return flag
				End If
				dateTime1 = dateTime
			Else
				SyncLock DevComponents.Schedule.TimeZoneInfo.s_internalSyncObject
					dateTime1 = DevComponents.Schedule.TimeZoneInfo.ConvertTime(dateTime, DevComponents.Schedule.TimeZoneInfo.Local, Me, ⟲.⟳)
				End SyncLock
			End If
			Dim adjustmentRuleForTime As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = Me.GetAdjustmentRuleForTime(dateTime1)
			If (adjustmentRuleForTime Is Nothing) Then
				Return False
			End If
			Dim daylightTime As System.Globalization.DaylightTime = DevComponents.Schedule.TimeZoneInfo.GetDaylightTime(dateTime1.Year, adjustmentRuleForTime)
			Return DevComponents.Schedule.TimeZoneInfo.GetIsDaylightSavings(dateTime1, adjustmentRuleForTime, daylightTime)
		End Function

		Public Function IsDaylightSavingTime(ByVal dateTimeOffset As System.DateTimeOffset) As Boolean
			Dim flag As Boolean
			DevComponents.Schedule.TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, Me, flag)
			Return flag
		End Function

		Public Function IsInvalidTime(ByVal dateTime As System.DateTime) As Boolean
			Dim flag As Boolean = False
			If (dateTime.Kind <> DateTimeKind.Unspecified AndAlso (dateTime.Kind <> DateTimeKind.Local OrElse Me.GetCorrespondingKind() <> DateTimeKind.Local)) Then
				Return flag
			End If
			Dim adjustmentRuleForTime As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = Me.GetAdjustmentRuleForTime(dateTime)
			If (adjustmentRuleForTime Is Nothing) Then
				Return False
			End If
			Dim daylightTime As System.Globalization.DaylightTime = DevComponents.Schedule.TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime)
			Return DevComponents.Schedule.TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime)
		End Function

		Private Sub OnDeserialization(ByVal sender As Object) Implements IDeserializationCallback.OnDeserialization
			Dim flag As Boolean
			Try
				DevComponents.Schedule.TimeZoneInfo.ValidateTimeZoneInfo(Me.m_id, Me.m_baseUtcOffset, Me.m_adjustmentRules, flag)
				If (flag <> Me.m_supportsDaylightSavingTime) Then
					Throw New SerializationException("Serialization_CorruptField, SupportsDaylightSavingTime")
				End If
			Catch argumentException As System.ArgumentException
				Throw New SerializationException("Serialization_InvalidData", argumentException)
			Catch invalidTimeZoneException As DevComponents.Schedule.InvalidTimeZoneException
				Throw New SerializationException("Serialization_InvalidData", invalidTimeZoneException)
			End Try
		End Sub

		<SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.SerializationFormatter)>
		Private Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext) Implements ISerializable.GetObjectData
			If (info Is Nothing) Then
				Throw New ArgumentNullException("info")
			End If
			info.AddValue("Id", Me.m_id)
			info.AddValue("DisplayName", Me.m_displayName)
			info.AddValue("StandardName", Me.m_standardDisplayName)
			info.AddValue("DaylightName", Me.m_daylightDisplayName)
			info.AddValue("BaseUtcOffset", Me.m_baseUtcOffset)
			info.AddValue("AdjustmentRules", Me.m_adjustmentRules)
			info.AddValue("SupportsDaylightSavingTime", Me.m_supportsDaylightSavingTime)
		End Sub

		Public Function ToSerializedString() As String
			Return DevComponents.Schedule.TimeZoneInfo.⟻.⠗(Me)
		End Function

		Public Overrides Function ToString() As String
			Return Me.DisplayName
		End Function

		Private Shared Function TransitionTimeFromTimeZoneInformation(ByVal timeZoneInformation As ⟥.⟱, ByVal readStartDate As Boolean) As Nullable(Of DevComponents.Schedule.TimeZoneInfo.TransitionTime)
			Dim transitionTime As DevComponents.Schedule.TimeZoneInfo.TransitionTime
			If (timeZoneInformation.⟠.⟨ = 0) Then
				Return Nothing
			End If
			If (Not readStartDate) Then
				transitionTime = If(timeZoneInformation.⟠.⟧ <> 0, DevComponents.Schedule.TimeZoneInfo.TransitionTime.CreateFixedDateRule(New DateTime(1, 1, 1, timeZoneInformation.⟠.⟫, timeZoneInformation.⟠.⟬, timeZoneInformation.⟠.⟭, timeZoneInformation.⟠.⟮), timeZoneInformation.⟠.⟨, timeZoneInformation.⟠.⟪), DevComponents.Schedule.TimeZoneInfo.TransitionTime.CreateFloatingDateRule(New DateTime(1, 1, 1, timeZoneInformation.⟠.⟫, timeZoneInformation.⟠.⟬, timeZoneInformation.⟠.⟭, timeZoneInformation.⟠.⟮), timeZoneInformation.⟠.⟨, timeZoneInformation.⟠.⟪, DirectCast(timeZoneInformation.⟠.⟩, DayOfWeek)))
			Else
				transitionTime = If(timeZoneInformation.⟣.⟧ <> 0, DevComponents.Schedule.TimeZoneInfo.TransitionTime.CreateFixedDateRule(New DateTime(1, 1, 1, timeZoneInformation.⟣.⟫, timeZoneInformation.⟣.⟬, timeZoneInformation.⟣.⟭, timeZoneInformation.⟣.⟮), timeZoneInformation.⟣.⟨, timeZoneInformation.⟣.⟪), DevComponents.Schedule.TimeZoneInfo.TransitionTime.CreateFloatingDateRule(New DateTime(1, 1, 1, timeZoneInformation.⟣.⟫, timeZoneInformation.⟣.⟬, timeZoneInformation.⟣.⟭, timeZoneInformation.⟣.⟮), timeZoneInformation.⟣.⟨, timeZoneInformation.⟣.⟪, DirectCast(timeZoneInformation.⟣.⟩, DayOfWeek)))
			End If
			Return New Nullable(Of DevComponents.Schedule.TimeZoneInfo.TransitionTime)(transitionTime)
		End Function

		Private Shared Function TransitionTimeToDateTime(ByVal year As Integer, ByVal transitionTime As DevComponents.Schedule.TimeZoneInfo.TransitionTime) As System.DateTime
			Dim dateTime As System.DateTime
			Dim timeOfDay As System.DateTime = transitionTime.TimeOfDay
			If (transitionTime.IsFixedDateRule) Then
				Dim num As Integer = System.DateTime.DaysInMonth(year, transitionTime.Month)
				Return New System.DateTime(year, transitionTime.Month, If(num < transitionTime.Day, num, transitionTime.Day), timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond)
			End If
			If (transitionTime.Week > 4) Then
				Dim num1 As Integer = System.DateTime.DaysInMonth(year, transitionTime.Month)
				dateTime = New System.DateTime(year, transitionTime.Month, num1, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond)
				Dim dayOfWeek As Integer = CInt(dateTime.DayOfWeek) - CInt(transitionTime.DayOfWeek)
				If (dayOfWeek < 0) Then
					dayOfWeek += 7
				End If
				If (dayOfWeek > 0) Then
					dateTime = dateTime.AddDays(CDbl((-dayOfWeek)))
				End If
				Return dateTime
			End If
			dateTime = New System.DateTime(year, transitionTime.Month, 1, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond)
			Dim dayOfWeek1 As Integer = CInt(dateTime.DayOfWeek)
			Dim week As Integer = CInt(transitionTime.DayOfWeek) - dayOfWeek1
			If (week < 0) Then
				week += 7
			End If
			week = week + 7 * (transitionTime.Week - 1)
			If (week > 0) Then
				dateTime = dateTime.AddDays(CDbl(week))
			End If
			Return dateTime
		End Function

		Private Shared Function TryCompareStandardDate(ByVal timeZone As ⟥.⟝, ByVal registryTimeZoneInfo As ⟥.⟱) As Boolean
			If (timeZone.⟞ <> registryTimeZoneInfo.⟞ OrElse timeZone.⟡ <> registryTimeZoneInfo.⟡ OrElse timeZone.⟠.⟧ <> registryTimeZoneInfo.⟠.⟧ OrElse timeZone.⟠.⟨ <> registryTimeZoneInfo.⟠.⟨ OrElse timeZone.⟠.⟩ <> registryTimeZoneInfo.⟠.⟩ OrElse timeZone.⟠.⟪ <> registryTimeZoneInfo.⟠.⟪ OrElse timeZone.⟠.⟫ <> registryTimeZoneInfo.⟠.⟫ OrElse timeZone.⟠.⟬ <> registryTimeZoneInfo.⟠.⟬ OrElse timeZone.⟠.⟭ <> registryTimeZoneInfo.⟠.⟭) Then
				Return False
			End If
			Return timeZone.⟠.⟮ = registryTimeZoneInfo.⟠.⟮
		End Function

		<SecurityCritical>
		<SecurityTreatAsSafe>
		Private Shared Function TryCompareTimeZoneInformationToRegistry(ByVal timeZone As ⟥.⟝, ByVal id As String, <Out> ByRef dstDisabled As Boolean) As Boolean
			Dim flag As Boolean
			Dim _u27f1 As ⟥.⟱
			Dim flag1 As Boolean
			Dim flag2 As Boolean
			dstDisabled = False
			Try
				Dim permissionSets As PermissionSet = New PermissionSet(PermissionState.None)
				permissionSets.AddPermission(New RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones"))
				permissionSets.Assert()
				Dim localMachine As Microsoft.Win32.RegistryKey = Registry.LocalMachine
				Dim invariantCulture As CultureInfo = CultureInfo.InvariantCulture
				Dim objArray() As Object = { "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones", id }
				Using registryKey As Microsoft.Win32.RegistryKey = localMachine.OpenSubKey(String.Format(invariantCulture, "{0}\{1}", objArray), RegistryKeyPermissionCheck.[Default], RegistryRights.ExecuteKey)
					If (registryKey IsNot Nothing) Then
						Try
							_u27f1 = New ⟥.⟱(DirectCast(registryKey.GetValue("TZI", Nothing, RegistryValueOptions.None), Byte()))
						Catch invalidCastException As System.InvalidCastException
							flag = False
							Return flag
						Catch argumentException As System.ArgumentException
							flag = False
							Return flag
						End Try
						If (DevComponents.Schedule.TimeZoneInfo.TryCompareStandardDate(timeZone, _u27f1)) Then
							dstDisabled = DevComponents.Schedule.TimeZoneInfo.CheckDaylightSavingTimeDisabled()
							If (dstDisabled OrElse DevComponents.Schedule.TimeZoneInfo.CheckDaylightSavingTimeNotSupported(timeZone)) Then
								flag2 = True
							Else
								flag2 = If(timeZone.⟤ <> _u27f1.⟤ OrElse timeZone.⟣.⟧ <> _u27f1.⟣.⟧ OrElse timeZone.⟣.⟨ <> _u27f1.⟣.⟨ OrElse timeZone.⟣.⟩ <> _u27f1.⟣.⟩ OrElse timeZone.⟣.⟪ <> _u27f1.⟣.⟪ OrElse timeZone.⟣.⟫ <> _u27f1.⟣.⟫ OrElse timeZone.⟣.⟬ <> _u27f1.⟣.⟬ OrElse timeZone.⟣.⟭ <> _u27f1.⟣.⟭, False, timeZone.⟣.⟮ = _u27f1.⟣.⟮)
							End If
							Dim flag3 As Boolean = flag2
							If (flag3) Then
								Dim value As String = TryCast(registryKey.GetValue("Std", String.Empty, RegistryValueOptions.None), String)
								flag3 = String.Compare(value, timeZone.⟟, StringComparison.Ordinal) = 0
							End If
							flag1 = flag3
						Else
							flag = False
							Return flag
						End If
					Else
						flag = False
						Return flag
					End If
				End Using
				Return flag1
			Finally
				PermissionSet.RevertAssert()
			End Try
			Return flag
		End Function

		Private Shared Function TryCreateAdjustmentRules(ByVal id As String, ByVal defaultTimeZoneInformation As ⟥.⟱, <Out> ByRef rules As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule(), <Out> ByRef e As Exception) As Boolean
			Dim flag As Boolean
			e = Nothing
			Try
				Dim localMachine As Microsoft.Win32.RegistryKey = Registry.LocalMachine
				Dim invariantCulture As CultureInfo = CultureInfo.InvariantCulture
				Dim objArray() As Object = { "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones", id }
				Using registryKey As Microsoft.Win32.RegistryKey = localMachine.OpenSubKey(String.Format(invariantCulture, "{0}\{1}\Dynamic DST", objArray), RegistryKeyPermissionCheck.[Default], RegistryRights.ExecuteKey)
					If (registryKey IsNot Nothing) Then
						Dim value As Integer = CInt(registryKey.GetValue("FirstEntry", -1, RegistryValueOptions.None))
						Dim num As Integer = CInt(registryKey.GetValue("LastEntry", -1, RegistryValueOptions.None))
						If (value = -1 OrElse num = -1 OrElse value > num) Then
							rules = Nothing
							flag = False
							Return flag
						Else
							Dim _u27f1 As ⟥.⟱ = New ⟥.⟱(DirectCast(registryKey.GetValue(value.ToString(CultureInfo.InvariantCulture), Nothing, RegistryValueOptions.None), Byte()))
							If (value <> num) Then
								Dim adjustmentRules As List(Of DevComponents.Schedule.TimeZoneInfo.AdjustmentRule) = New List(Of DevComponents.Schedule.TimeZoneInfo.AdjustmentRule)(1)
								Dim minValue As System.DateTime = System.DateTime.MinValue
								Dim adjustmentRule As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = DevComponents.Schedule.TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(_u27f1, minValue.[Date], New System.DateTime(value, 12, 31))
								If (adjustmentRule IsNot Nothing) Then
									adjustmentRules.Add(adjustmentRule)
								End If
								Dim num1 As Integer = value + 1
								Do
									_u27f1 = New ⟥.⟱(DirectCast(registryKey.GetValue(num1.ToString(CultureInfo.InvariantCulture), Nothing, RegistryValueOptions.None), Byte()))
									Dim adjustmentRule1 As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = DevComponents.Schedule.TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(_u27f1, New System.DateTime(num1, 1, 1), New System.DateTime(num1, 12, 31))
									If (adjustmentRule1 IsNot Nothing) Then
										adjustmentRules.Add(adjustmentRule1)
									End If
									num1 = num1 + 1
								Loop While num1 < num
								_u27f1 = New ⟥.⟱(DirectCast(registryKey.GetValue(num.ToString(CultureInfo.InvariantCulture), Nothing, RegistryValueOptions.None), Byte()))
								Dim dateTime As System.DateTime = New System.DateTime(num, 1, 1)
								Dim maxValue As System.DateTime = System.DateTime.MaxValue
								Dim adjustmentRule2 As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = DevComponents.Schedule.TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(_u27f1, dateTime, maxValue.[Date])
								If (adjustmentRule2 IsNot Nothing) Then
									adjustmentRules.Add(adjustmentRule2)
								End If
								rules = adjustmentRules.ToArray()
								If (rules IsNot Nothing AndAlso CInt(rules.Length) = 0) Then
									rules = Nothing
								End If
							Else
								Dim [date] As System.DateTime = System.DateTime.MinValue.[Date]
								Dim maxValue1 As System.DateTime = System.DateTime.MaxValue
								Dim adjustmentRule3 As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = DevComponents.Schedule.TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(_u27f1, [date], maxValue1.[Date])
								If (adjustmentRule3 IsNot Nothing) Then
									rules = New DevComponents.Schedule.TimeZoneInfo.AdjustmentRule() { adjustmentRule3 }
								Else
									rules = Nothing
								End If
								flag = True
								Return flag
							End If
						End If
					Else
						Dim date1 As System.DateTime = System.DateTime.MinValue.[Date]
						Dim dateTime1 As System.DateTime = System.DateTime.MaxValue
						Dim adjustmentRule4 As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = DevComponents.Schedule.TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(defaultTimeZoneInformation, date1, dateTime1.[Date])
						If (adjustmentRule4 IsNot Nothing) Then
							rules = New DevComponents.Schedule.TimeZoneInfo.AdjustmentRule() { adjustmentRule4 }
						Else
							rules = Nothing
						End If
						flag = True
						Return flag
					End If
				End Using
				Return True
			Catch invalidCastException1 As System.InvalidCastException
				Dim invalidCastException As System.InvalidCastException = invalidCastException1
				rules = Nothing
				e = invalidCastException
				flag = False
			Catch argumentOutOfRangeException1 As System.ArgumentOutOfRangeException
				Dim argumentOutOfRangeException As System.ArgumentOutOfRangeException = argumentOutOfRangeException1
				rules = Nothing
				e = argumentOutOfRangeException
				flag = False
			Catch argumentException1 As System.ArgumentException
				Dim argumentException As System.ArgumentException = argumentException1
				rules = Nothing
				e = argumentException
				flag = False
			End Try
			Return flag
		End Function

		<FileIOPermission(SecurityAction.Assert, AllLocalFiles:=FileIOPermissionAccess.PathDiscovery)>
		<SecurityCritical>
		<SecurityTreatAsSafe>
		Private Shared Function TryGetLocalizedNameByMuiNativeResource(ByVal resource As String) As String
			Dim str As String
			Dim empty As String
			Dim num As Integer
			If (String.IsNullOrEmpty(resource)) Then
				Return String.Empty
			End If
			Dim chrArray() As Char = { ","C }
			Dim strArrays As String() = resource.Split(chrArray, StringSplitOptions.None)
			If (CInt(strArrays.Length) <> 2) Then
				Return String.Empty
			End If
			Dim folderPath As String = Environment.GetFolderPath(Environment.SpecialFolder.System)
			Dim str1 As String = strArrays(0).TrimStart(New Char() { "@"C })
			Try
				str = Path.Combine(folderPath, str1)
			Catch argumentException As System.ArgumentException
				empty = String.Empty
				Return empty
			End Try
			If (Not Integer.TryParse(strArrays(1), NumberStyles.[Integer], CultureInfo.InvariantCulture, num)) Then
				Return String.Empty
			End If
			num = -num
			Try
				Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder(260) With
				{
					.Length = 260
				}
				Dim num1 As Integer = 260
				Dim num2 As Integer = 0
				Dim num3 As Long = CLng(0)
				empty = If(⟴.GetFileMUIPath(16, str, Nothing, num2, stringBuilder, num1, num3), DevComponents.Schedule.TimeZoneInfo.TryGetLocalizedNameByNativeResource(stringBuilder.ToString(), num), String.Empty)
			Catch entryPointNotFoundException As System.EntryPointNotFoundException
				empty = String.Empty
			End Try
			Return empty
		End Function

		<SecurityCritical>
		Private Shared Function TryGetLocalizedNameByNativeResource(ByVal filePath As String, ByVal resource As Integer) As String
			Dim str As String
			If (File.Exists(filePath)) Then
				Using _u27f5 As ⟵ = ⟴.LoadLibraryEx(filePath, IntPtr.Zero, 2)
					If (Not _u27f5.IsInvalid) Then
						Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder(500) With
						{
							.Length = 500
						}
						If (⟴.LoadString(_u27f5, resource, stringBuilder, stringBuilder.Length) <> 0) Then
							str = stringBuilder.ToString()
							Return str
						End If
					End If
					Return String.Empty
				End Using
				Return str
			End If
			Return String.Empty
		End Function

		Private Shared Function TryGetLocalizedNamesByRegistryKey(ByVal key As RegistryKey, <Out> ByRef displayName As String, <Out> ByRef standardName As String, <Out> ByRef daylightName As String) As Boolean
			displayName = String.Empty
			standardName = String.Empty
			daylightName = String.Empty
			Dim value As String = TryCast(key.GetValue("MUI_Display", String.Empty, RegistryValueOptions.None), String)
			Dim str As String = TryCast(key.GetValue("MUI_Std", String.Empty, RegistryValueOptions.None), String)
			Dim value1 As String = TryCast(key.GetValue("MUI_Dlt", String.Empty, RegistryValueOptions.None), String)
			If (Not String.IsNullOrEmpty(value)) Then
				displayName = DevComponents.Schedule.TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(value)
			End If
			If (Not String.IsNullOrEmpty(str)) Then
				standardName = DevComponents.Schedule.TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(str)
			End If
			If (Not String.IsNullOrEmpty(value1)) Then
				daylightName = DevComponents.Schedule.TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(value1)
			End If
			If (String.IsNullOrEmpty(displayName)) Then
				displayName = TryCast(key.GetValue("Display", String.Empty, RegistryValueOptions.None), String)
			End If
			If (String.IsNullOrEmpty(standardName)) Then
				standardName = TryCast(key.GetValue("Std", String.Empty, RegistryValueOptions.None), String)
			End If
			If (String.IsNullOrEmpty(daylightName)) Then
				daylightName = TryCast(key.GetValue("Dlt", String.Empty, RegistryValueOptions.None), String)
			End If
			Return True
		End Function

		Private Shared Function TryGetTimeZone(ByVal id As String, ByVal dstDisabled As Boolean, <Out> ByRef value As DevComponents.Schedule.TimeZoneInfo, <Out> ByRef e As Exception) As DevComponents.Schedule.TimeZoneInfo.⟶
			Dim _u27f6 As DevComponents.Schedule.TimeZoneInfo.⟶ = DevComponents.Schedule.TimeZoneInfo.⟶.⟷
			e = Nothing
			Dim timeZoneInfo As DevComponents.Schedule.TimeZoneInfo = Nothing
			If (DevComponents.Schedule.TimeZoneInfo.s_systemTimeZones.TryGetValue(id, timeZoneInfo)) Then
				If (dstDisabled AndAlso timeZoneInfo.m_supportsDaylightSavingTime) Then
					value = DevComponents.Schedule.TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName)
					Return _u27f6
				End If
				value = New DevComponents.Schedule.TimeZoneInfo(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName, timeZoneInfo.m_daylightDisplayName, timeZoneInfo.m_adjustmentRules, False)
				Return _u27f6
			End If
			If (DevComponents.Schedule.TimeZoneInfo.s_allSystemTimeZonesRead) Then
				_u27f6 = DevComponents.Schedule.TimeZoneInfo.⟶.⟸
				value = Nothing
				Return _u27f6
			End If
			_u27f6 = DevComponents.Schedule.TimeZoneInfo.TryGetTimeZoneByRegistryKey(id, timeZoneInfo, e)
			If (_u27f6 <> DevComponents.Schedule.TimeZoneInfo.⟶.⟷) Then
				value = Nothing
				Return _u27f6
			End If
			DevComponents.Schedule.TimeZoneInfo.s_systemTimeZones.Add(id, timeZoneInfo)
			If (dstDisabled AndAlso timeZoneInfo.m_supportsDaylightSavingTime) Then
				value = DevComponents.Schedule.TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName)
				Return _u27f6
			End If
			value = New DevComponents.Schedule.TimeZoneInfo(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName, timeZoneInfo.m_daylightDisplayName, timeZoneInfo.m_adjustmentRules, False)
			Return _u27f6
		End Function

		<SecurityCritical>
		<SecurityTreatAsSafe>
		Private Shared Function TryGetTimeZoneByRegistryKey(ByVal id As String, <Out> ByRef value As DevComponents.Schedule.TimeZoneInfo, <Out> ByRef e As Exception) As DevComponents.Schedule.TimeZoneInfo.⟶
			Dim _u27f6 As DevComponents.Schedule.TimeZoneInfo.⟶
			Dim _u27f1 As ⟥.⟱
			Dim adjustmentRuleArray As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule()
			Dim str As String
			Dim str1 As String
			Dim str2 As String
			Dim _u27f61 As DevComponents.Schedule.TimeZoneInfo.⟶
			e = Nothing
			Try
				Dim permissionSets As PermissionSet = New PermissionSet(PermissionState.None)
				permissionSets.AddPermission(New RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones"))
				permissionSets.Assert()
				Dim localMachine As Microsoft.Win32.RegistryKey = Registry.LocalMachine
				Dim invariantCulture As CultureInfo = CultureInfo.InvariantCulture
				Dim objArray() As Object = { "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones", id }
				Using registryKey As Microsoft.Win32.RegistryKey = localMachine.OpenSubKey(String.Format(invariantCulture, "{0}\{1}", objArray), RegistryKeyPermissionCheck.[Default], RegistryRights.ExecuteKey)
					If (registryKey IsNot Nothing) Then
						Try
							_u27f1 = New ⟥.⟱(DirectCast(registryKey.GetValue("TZI", Nothing, RegistryValueOptions.None), Byte()))
						Catch invalidCastException1 As System.InvalidCastException
							Dim invalidCastException As System.InvalidCastException = invalidCastException1
							value = Nothing
							e = invalidCastException
							_u27f6 = DevComponents.Schedule.TimeZoneInfo.⟶.⟹
							Return _u27f6
						Catch argumentException1 As System.ArgumentException
							Dim argumentException As System.ArgumentException = argumentException1
							value = Nothing
							e = argumentException
							_u27f6 = DevComponents.Schedule.TimeZoneInfo.⟶.⟹
							Return _u27f6
						End Try
						If (Not DevComponents.Schedule.TimeZoneInfo.TryCreateAdjustmentRules(id, _u27f1, adjustmentRuleArray, e)) Then
							value = Nothing
							_u27f6 = DevComponents.Schedule.TimeZoneInfo.⟶.⟹
							Return _u27f6
						ElseIf (DevComponents.Schedule.TimeZoneInfo.TryGetLocalizedNamesByRegistryKey(registryKey, str, str1, str2)) Then
							Try
								value = New DevComponents.Schedule.TimeZoneInfo(id, New TimeSpan(0, -_u27f1.⟞, 0), str, str1, str2, adjustmentRuleArray, False)
								_u27f61 = DevComponents.Schedule.TimeZoneInfo.⟶.⟷
							Catch argumentException3 As System.ArgumentException
								Dim argumentException2 As System.ArgumentException = argumentException3
								value = Nothing
								e = argumentException2
								_u27f61 = DevComponents.Schedule.TimeZoneInfo.⟶.⟹
							Catch invalidTimeZoneException1 As DevComponents.Schedule.InvalidTimeZoneException
								Dim invalidTimeZoneException As DevComponents.Schedule.InvalidTimeZoneException = invalidTimeZoneException1
								value = Nothing
								e = invalidTimeZoneException
								_u27f61 = DevComponents.Schedule.TimeZoneInfo.⟶.⟹
							End Try
						Else
							value = Nothing
							_u27f61 = DevComponents.Schedule.TimeZoneInfo.⟶.⟹
						End If
					Else
						value = Nothing
						_u27f6 = DevComponents.Schedule.TimeZoneInfo.⟶.⟸
						Return _u27f6
					End If
				End Using
				Return _u27f61
			Finally
				PermissionSet.RevertAssert()
			End Try
			Return _u27f6
		End Function

		Friend Shared Function UtcOffsetOutOfRange(ByVal offset As TimeSpan) As Boolean
			If (offset.TotalHours < -14) Then
				Return True
			End If
			Return offset.TotalHours > 14
		End Function

		Private Shared Sub ValidateTimeZoneInfo(ByVal id As String, ByVal baseUtcOffset As TimeSpan, ByVal adjustmentRules As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule(), <Out> ByRef adjustmentRulesSupportDst As Boolean)
			adjustmentRulesSupportDst = False
			If (id Is Nothing) Then
				Throw New ArgumentNullException("id")
			End If
			If (id.Length = 0) Then
				Throw New ArgumentException("Argument_InvalidId , id")
			End If
			If (DevComponents.Schedule.TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset)) Then
				Throw New ArgumentOutOfRangeException("baseUtcOffset", "ArgumentOutOfRange_UtcOffset")
			End If
			If (baseUtcOffset.Ticks Mod CLng(600000000) <> CLng(0)) Then
				Throw New ArgumentException("Argument_TimeSpanHasSeconds", "baseUtcOffset")
			End If
			If (adjustmentRules IsNot Nothing AndAlso CInt(adjustmentRules.Length) <> 0) Then
				adjustmentRulesSupportDst = True
				Dim adjustmentRule As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = Nothing
				Dim adjustmentRule1 As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = Nothing
				For i As Integer = 0 To CInt(adjustmentRules.Length)
					adjustmentRule = adjustmentRule1
					adjustmentRule1 = adjustmentRules(i)
					If (adjustmentRule1 Is Nothing) Then
						Throw New DevComponents.Schedule.InvalidTimeZoneException("Argument_AdjustmentRulesNoNulls")
					End If
					If (DevComponents.Schedule.TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset + adjustmentRule1.DaylightDelta)) Then
						Throw New DevComponents.Schedule.InvalidTimeZoneException("ArgumentOutOfRange_UtcOffsetAndDaylightDelta")
					End If
					If (adjustmentRule IsNot Nothing AndAlso adjustmentRule1.DateStart <= adjustmentRule.DateEnd) Then
						Throw New DevComponents.Schedule.InvalidTimeZoneException("Argument_AdjustmentRulesOutOfOrder")
					End If
				Next

			End If
		End Sub

		Private Enum ⟶
			⟷
			⟸
			⟹
			⟺
		End Enum

		Private NotInheritable Class ⟻
			Private Const ⟼ As String = "MM:dd:yyyy"

			Private Const ⟽ As Char = "\"C

			Private Const ⟾ As String = "\\"

			Private Const ⟿ As String = "\["

			Private Const ⠀ As String = "\]"

			Private Const ⠁ As String = "\;"

			Private Const ⠂ As String = "\"

			Private Const ⠃ As Integer = 64

			Private Const ⠄ As Char = "["C

			Private Const ⠅ As String = "["

			Private Const ⠆ As Char = "]"C

			Private Const ⠇ As String = "]"

			Private Const ⠈ As Char = ";"C

			Private Const ⠉ As String = ";"

			Private Const ⠊ As String = "HH:mm:ss.FFF"

			Private ⠋ As Integer

			Private ⠌ As String

			Private ⠍ As DevComponents.Schedule.TimeZoneInfo.⟻.⠠

			Private Sub New(ByVal str As String)
				MyBase.New()
				Me.⠌ = str
				Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠣
			End Sub

			Public Shared Function ⠎(ByVal ዽ As String) As DevComponents.Schedule.TimeZoneInfo
				Dim timeZoneInfo As DevComponents.Schedule.TimeZoneInfo
				Dim _u27fb As DevComponents.Schedule.TimeZoneInfo.⟻ = New DevComponents.Schedule.TimeZoneInfo.⟻(ዽ)
				Dim str As String = _u27fb.⠔(False)
				Dim timeSpan As System.TimeSpan = _u27fb.⠕(False)
				Dim str1 As String = _u27fb.⠔(False)
				Dim str2 As String = _u27fb.⠔(False)
				Dim str3 As String = _u27fb.⠔(False)
				Dim adjustmentRuleArray As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule() = _u27fb.⠏(False)
				Try
					timeZoneInfo = DevComponents.Schedule.TimeZoneInfo.CreateCustomTimeZone(str, timeSpan, str1, str2, str3, adjustmentRuleArray)
				Catch argumentException As System.ArgumentException
					Throw New SerializationException("Serialization_InvalidData", argumentException)
				Catch invalidTimeZoneException As DevComponents.Schedule.InvalidTimeZoneException
					Throw New SerializationException("Serialization_InvalidData", invalidTimeZoneException)
				End Try
				Return timeZoneInfo
			End Function

			Private Function ⠏(ByVal u2810 As Boolean) As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule()
				Dim adjustmentRules As List(Of DevComponents.Schedule.TimeZoneInfo.AdjustmentRule) = New List(Of DevComponents.Schedule.TimeZoneInfo.AdjustmentRule)(1)
				Dim num As Integer = 0
				Dim adjustmentRule As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = Me.⠑(True)
				While adjustmentRule IsNot Nothing
					adjustmentRules.Add(adjustmentRule)
					num = num + 1
					adjustmentRule = Me.⠑(True)
				End While
				If (Not u2810) Then
					If (Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠤) Then
						Throw New SerializationException("Serialization_InvalidData")
					End If
					If (Me.⠋ < 0 OrElse Me.⠋ >= Me.⠌.Length) Then
						Throw New SerializationException("Serialization_InvalidData")
					End If
				End If
				If (num = 0) Then
					Return Nothing
				End If
				Return adjustmentRules.ToArray()
			End Function

			Private Function ⠑(ByVal u2810 As Boolean) As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule
				Dim adjustmentRule As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule
				If (Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠤) Then
					If (Not u2810) Then
						Throw New SerializationException("Serialization_InvalidData")
					End If
					Return Nothing
				End If
				If (Me.⠋ < 0 OrElse Me.⠋ >= Me.⠌.Length) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				If (Me.⠌(Me.⠋) = ";"C) Then
					Return Nothing
				End If
				If (Me.⠌(Me.⠋) <> "["C) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				Me.⠋ = Me.⠋ + 1
				Dim dateTime As System.DateTime = Me.⠒(False, "MM:dd:yyyy")
				Dim dateTime1 As System.DateTime = Me.⠒(False, "MM:dd:yyyy")
				Dim timeSpan As System.TimeSpan = Me.⠕(False)
				Dim transitionTime As DevComponents.Schedule.TimeZoneInfo.TransitionTime = Me.⠖(False)
				Dim transitionTime1 As DevComponents.Schedule.TimeZoneInfo.TransitionTime = Me.⠖(False)
				If (Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠤ OrElse Me.⠋ >= Me.⠌.Length) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				If (Me.⠌(Me.⠋) = "]"C) Then
					Me.⠋ = Me.⠋ + 1
				Else
					Me.⠝(1)
				End If
				Try
					adjustmentRule = DevComponents.Schedule.TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateTime, dateTime1, timeSpan, transitionTime, transitionTime1)
				Catch argumentException As System.ArgumentException
					Throw New SerializationException("Serialization_InvalidData", argumentException)
				End Try
				If (Me.⠋ >= Me.⠌.Length) Then
					Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠤
					Return adjustmentRule
				End If
				Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠣
				Return adjustmentRule
			End Function

			Private Function ⠒(ByVal u2810 As Boolean, ByVal ພ As String) As System.DateTime
				Dim dateTime As System.DateTime
				If (Not System.DateTime.TryParseExact(Me.⠔(u2810), ພ, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, dateTime)) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				Return dateTime
			End Function

			Private Function ⠓(ByVal u2810 As Boolean) As Integer
				Dim num As Integer
				If (Not Integer.TryParse(Me.⠔(u2810), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, num)) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				Return num
			End Function

			Private Function ⠔(ByVal u2810 As Boolean) As String
				If (Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠤) Then
					If (Not u2810) Then
						Throw New SerializationException("Serialization_InvalidData")
					End If
					Return Nothing
				End If
				If (Me.⠋ < 0 OrElse Me.⠋ >= Me.⠌.Length) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				Dim _u2820 As DevComponents.Schedule.TimeZoneInfo.⟻.⠠ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠢
				Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder(64)
				Dim num As Integer = Me.⠋
				Do
					If (_u2820 = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠡) Then
						DevComponents.Schedule.TimeZoneInfo.⟻.⠟(Me.⠌(num))
						stringBuilder.Append(Me.⠌(num))
						_u2820 = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠢
					ElseIf (_u2820 = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠢) Then
						Dim chr As Char = Me.⠌(num)
						If (chr = Strings.ChrW(0)) Then
							Throw New SerializationException("Serialization_InvalidData")
						End If
						If (chr = ";"C) Then
							Me.⠋ = num + 1
							If (Me.⠋ < Me.⠌.Length) Then
								Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠣
							Else
								Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠤
							End If
							Return stringBuilder.ToString()
						End If
						Select Case chr
							Case "["C
								Throw New SerializationException("Serialization_InvalidData")
							Case "\"C
								_u2820 = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠡
								Exit Select
							Case "]"C
								If (Not u2810) Then
									Throw New SerializationException("Serialization_InvalidData")
								End If
								Me.⠋ = num
								Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠣
								Return stringBuilder.ToString()
							Case Else
								stringBuilder.Append(Me.⠌(num))
								Exit Select
						End Select
					End If
					num = num + 1
				Loop While num < Me.⠌.Length
				If (_u2820 = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠡) Then
					Throw New SerializationException("Serialization_InvalidEscapeSequence")
				End If
				If (Not u2810) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				Me.⠋ = Me.⠌.Length
				Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠤
				Return stringBuilder.ToString()
			End Function

			Private Function ⠕(ByVal u2810 As Boolean) As System.TimeSpan
				Dim timeSpan As System.TimeSpan
				Dim num As Integer = Me.⠓(u2810)
				Try
					timeSpan = New System.TimeSpan(0, num, 0)
				Catch argumentOutOfRangeException As System.ArgumentOutOfRangeException
					Throw New SerializationException("Serialization_InvalidData", argumentOutOfRangeException)
				End Try
				Return timeSpan
			End Function

			Private Function ⠖(ByVal u2810 As Boolean) As DevComponents.Schedule.TimeZoneInfo.TransitionTime
				Dim transitionTime As DevComponents.Schedule.TimeZoneInfo.TransitionTime
				If (Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠤ OrElse Me.⠋ < Me.⠌.Length AndAlso Me.⠌(Me.⠋) = "]"C) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				If (Me.⠋ < 0 OrElse Me.⠋ >= Me.⠌.Length) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				If (Me.⠌(Me.⠋) <> "["C) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				Me.⠋ = Me.⠋ + 1
				Dim num As Integer = Me.⠓(False)
				If (num <> 0 AndAlso num <> 1) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				Dim dateTime As System.DateTime = Me.⠒(False, "HH:mm:ss.FFF")
				dateTime = New System.DateTime(1, 1, 1, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond)
				Dim num1 As Integer = Me.⠓(False)
				If (num <> 1) Then
					Dim num2 As Integer = Me.⠓(False)
					Dim num3 As Integer = Me.⠓(False)
					Try
						transitionTime = DevComponents.Schedule.TimeZoneInfo.TransitionTime.CreateFloatingDateRule(dateTime, num1, num2, DirectCast(num3, DayOfWeek))
					Catch argumentException As System.ArgumentException
						Throw New SerializationException("Serialization_InvalidData", argumentException)
					End Try
				Else
					Dim num4 As Integer = Me.⠓(False)
					Try
						transitionTime = DevComponents.Schedule.TimeZoneInfo.TransitionTime.CreateFixedDateRule(dateTime, num1, num4)
					Catch argumentException1 As System.ArgumentException
						Throw New SerializationException("Serialization_InvalidData", argumentException1)
					End Try
				End If
				If (Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠤ OrElse Me.⠋ >= Me.⠌.Length) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				If (Me.⠌(Me.⠋) = "]"C) Then
					Me.⠋ = Me.⠋ + 1
				Else
					Me.⠝(1)
				End If
				Dim flag As Boolean = False
				If (Me.⠋ < Me.⠌.Length AndAlso Me.⠌(Me.⠋) = ";"C) Then
					Me.⠋ = Me.⠋ + 1
					flag = True
				End If
				If (Not flag AndAlso Not u2810) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				If (Me.⠋ >= Me.⠌.Length) Then
					Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠤
					Return transitionTime
				End If
				Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠣
				Return transitionTime
			End Function

			Public Shared Function ⠗(ByVal u2818 As DevComponents.Schedule.TimeZoneInfo) As String
				Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
				stringBuilder.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(u2818.Id))
				stringBuilder.Append(";"C)
				Dim totalMinutes As Double = u2818.BaseUtcOffset.TotalMinutes
				stringBuilder.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(totalMinutes.ToString(CultureInfo.InvariantCulture)))
				stringBuilder.Append(";"C)
				stringBuilder.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(u2818.DisplayName))
				stringBuilder.Append(";"C)
				stringBuilder.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(u2818.StandardName))
				stringBuilder.Append(";"C)
				stringBuilder.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(u2818.DaylightName))
				stringBuilder.Append(";"C)
				Dim adjustmentRules As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule() = u2818.GetAdjustmentRules()
				If (adjustmentRules IsNot Nothing AndAlso CInt(adjustmentRules.Length) > 0) Then
					For i As Integer = 0 To CInt(adjustmentRules.Length)
						Dim adjustmentRule As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = adjustmentRules(i)
						stringBuilder.Append("["C)
						Dim dateStart As DateTime = adjustmentRule.DateStart
						stringBuilder.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(dateStart.ToString("MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo)))
						stringBuilder.Append(";"C)
						Dim dateEnd As DateTime = adjustmentRule.DateEnd
						stringBuilder.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(dateEnd.ToString("MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo)))
						stringBuilder.Append(";"C)
						Dim num As Double = adjustmentRule.DaylightDelta.TotalMinutes
						stringBuilder.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(num.ToString(CultureInfo.InvariantCulture)))
						stringBuilder.Append(";"C)
						DevComponents.Schedule.TimeZoneInfo.⟻.⠚(adjustmentRule.DaylightTransitionStart, stringBuilder)
						stringBuilder.Append(";"C)
						DevComponents.Schedule.TimeZoneInfo.⟻.⠚(adjustmentRule.DaylightTransitionEnd, stringBuilder)
						stringBuilder.Append(";"C)
						stringBuilder.Append("]"C)
					Next

				End If
				stringBuilder.Append(";"C)
				Return stringBuilder.ToString()
			End Function

			Private Shared Function ⠙(ByVal u06dc As String) As String
				u06dc = u06dc.Replace("\", "\\")
				u06dc = u06dc.Replace("[", "\[")
				u06dc = u06dc.Replace("]", "\]")
				Return u06dc.Replace(";", "\;")
			End Function

			Private Shared Sub ⠚(ByVal u281b As DevComponents.Schedule.TimeZoneInfo.TransitionTime, ByVal u281c As System.Text.StringBuilder)
				u281c.Append("["C)
				Dim stringBuilder As System.Text.StringBuilder = u281c
				Dim num As Integer = If(u281b.IsFixedDateRule, 1, 0)
				stringBuilder.Append(num.ToString(CultureInfo.InvariantCulture))
				u281c.Append(";"C)
				If (Not u281b.IsFixedDateRule) Then
					Dim timeOfDay As System.DateTime = u281b.TimeOfDay
					u281c.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(timeOfDay.ToString("HH:mm:ss.FFF", DateTimeFormatInfo.InvariantInfo)))
					u281c.Append(";"C)
					Dim month As Integer = u281b.Month
					u281c.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(month.ToString(CultureInfo.InvariantCulture)))
					u281c.Append(";"C)
					Dim week As Integer = u281b.Week
					u281c.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(week.ToString(CultureInfo.InvariantCulture)))
					u281c.Append(";"C)
					Dim dayOfWeek As Integer = CInt(u281b.DayOfWeek)
					u281c.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(dayOfWeek.ToString(CultureInfo.InvariantCulture)))
					u281c.Append(";"C)
				Else
					Dim dateTime As System.DateTime = u281b.TimeOfDay
					u281c.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(dateTime.ToString("HH:mm:ss.FFF", DateTimeFormatInfo.InvariantInfo)))
					u281c.Append(";"C)
					Dim month1 As Integer = u281b.Month
					u281c.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(month1.ToString(CultureInfo.InvariantCulture)))
					u281c.Append(";"C)
					Dim day As Integer = u281b.Day
					u281c.Append(DevComponents.Schedule.TimeZoneInfo.⟻.⠙(day.ToString(CultureInfo.InvariantCulture)))
					u281c.Append(";"C)
				End If
				u281c.Append("]"C)
			End Sub

			Private Sub ⠝(ByVal u281e As Integer)
				If (Me.⠋ < 0 OrElse Me.⠋ >= Me.⠌.Length) Then
					Throw New SerializationException("Serialization_InvalidData")
				End If
				Dim _u2820 As DevComponents.Schedule.TimeZoneInfo.⟻.⠠ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠢
				Dim num As Integer = Me.⠋
				Do
					Select Case _u2820
						Case DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠡
							DevComponents.Schedule.TimeZoneInfo.⟻.⠟(Me.⠌(num))
							_u2820 = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠢
							Exit Select
						Case DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠢
							Dim chr As Char = Me.⠌(num)
							If (chr = Strings.ChrW(0)) Then
								Throw New SerializationException("Serialization_InvalidData")
							End If
							Select Case chr
								Case "["C
									u281e = u281e + 1

								Case "\"C
									_u2820 = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠡

								Case "]"C
									u281e = u281e - 1
									If (u281e <> 0) Then
										GoTo Label0
									End If
									Me.⠋ = num + 1
									If (Me.⠋ >= Me.⠌.Length) Then
										Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠤
										Return
									End If
									Me.⠍ = DevComponents.Schedule.TimeZoneInfo.⟻.⠠.⠣
									Return
							End Select

					End Select
				Label0:
					num = num + 1
				Loop While num < Me.⠌.Length
				Throw New SerializationException("Serialization_InvalidData")
			End Sub

			Private Shared Sub ⠟(ByVal ய As Char)
				If (ய <> "\"C AndAlso ய <> ";"C AndAlso ய <> "["C AndAlso ய <> "]"C) Then
					Throw New SerializationException(String.Concat("Serialization_InvalidEscapeSequence ", ய.ToString()))
				End If
			End Sub

			Private Enum ⠠
				⠡
				⠢
				⠣
				⠤
			End Enum
		End Class

		<HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort:=True)>
		<HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort:=True)>
		<Serializable>
		Public NotInheritable Class AdjustmentRule
			Implements IEquatable(Of DevComponents.Schedule.TimeZoneInfo.AdjustmentRule), ISerializable, IDeserializationCallback
			Private m_dateEnd As DateTime

			Private m_dateStart As DateTime

			Private m_daylightDelta As TimeSpan

			Private m_daylightTransitionEnd As DevComponents.Schedule.TimeZoneInfo.TransitionTime

			Private m_daylightTransitionStart As DevComponents.Schedule.TimeZoneInfo.TransitionTime

			Public ReadOnly Property DateEnd As DateTime
				Get
					Return Me.m_dateEnd
				End Get
			End Property

			Public ReadOnly Property DateStart As DateTime
				Get
					Return Me.m_dateStart
				End Get
			End Property

			Public ReadOnly Property DaylightDelta As TimeSpan
				Get
					Return Me.m_daylightDelta
				End Get
			End Property

			Public ReadOnly Property DaylightTransitionEnd As DevComponents.Schedule.TimeZoneInfo.TransitionTime
				Get
					Return Me.m_daylightTransitionEnd
				End Get
			End Property

			Public ReadOnly Property DaylightTransitionStart As DevComponents.Schedule.TimeZoneInfo.TransitionTime
				Get
					Return Me.m_daylightTransitionStart
				End Get
			End Property

			Private Sub New()
				MyBase.New()
			End Sub

			Private Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
				MyBase.New()
				If (info Is Nothing) Then
					Throw New ArgumentNullException("info")
				End If
				Me.m_dateStart = DirectCast(info.GetValue("DateStart", GetType(DateTime)), DateTime)
				Me.m_dateEnd = DirectCast(info.GetValue("DateEnd", GetType(DateTime)), DateTime)
				Me.m_daylightDelta = DirectCast(info.GetValue("DaylightDelta", GetType(TimeSpan)), TimeSpan)
				Me.m_daylightTransitionStart = DirectCast(info.GetValue("DaylightTransitionStart", GetType(DevComponents.Schedule.TimeZoneInfo.TransitionTime)), DevComponents.Schedule.TimeZoneInfo.TransitionTime)
				Me.m_daylightTransitionEnd = DirectCast(info.GetValue("DaylightTransitionEnd", GetType(DevComponents.Schedule.TimeZoneInfo.TransitionTime)), DevComponents.Schedule.TimeZoneInfo.TransitionTime)
			End Sub

			Public Shared Function CreateAdjustmentRule(ByVal dateStart As DateTime, ByVal dateEnd As DateTime, ByVal daylightDelta As TimeSpan, ByVal daylightTransitionStart As DevComponents.Schedule.TimeZoneInfo.TransitionTime, ByVal daylightTransitionEnd As DevComponents.Schedule.TimeZoneInfo.TransitionTime) As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule
				DevComponents.Schedule.TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd)
				Dim adjustmentRule As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule = New DevComponents.Schedule.TimeZoneInfo.AdjustmentRule() With
				{
					.m_dateStart = dateStart,
					.m_dateEnd = dateEnd,
					.m_daylightDelta = daylightDelta,
					.m_daylightTransitionStart = daylightTransitionStart,
					.m_daylightTransitionEnd = daylightTransitionEnd
				}
				Return adjustmentRule
			End Function

			Public Function Equals(ByVal other As DevComponents.Schedule.TimeZoneInfo.AdjustmentRule) As Boolean Implements IEquatable(Of DevComponents.Schedule.TimeZoneInfo.AdjustmentRule).Equals
				If (other Is Nothing OrElse Not (Me.m_dateStart = other.m_dateStart) OrElse Not (Me.m_dateEnd = other.m_dateEnd) OrElse Not (Me.m_daylightDelta = other.m_daylightDelta) OrElse Not Me.m_daylightTransitionEnd.Equals(other.m_daylightTransitionEnd)) Then
					Return False
				End If
				Return Me.m_daylightTransitionStart.Equals(other.m_daylightTransitionStart)
			End Function

			Public Overrides Function GetHashCode() As Integer
				Return Me.m_dateStart.GetHashCode()
			End Function

			Private Sub OnDeserialization(ByVal sender As Object) Implements IDeserializationCallback.OnDeserialization
				Try
					DevComponents.Schedule.TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(Me.m_dateStart, Me.m_dateEnd, Me.m_daylightDelta, Me.m_daylightTransitionStart, Me.m_daylightTransitionEnd)
				Catch argumentException As System.ArgumentException
					Throw New SerializationException("Serialization_InvalidData", argumentException)
				End Try
			End Sub

			<SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.SerializationFormatter)>
			Private Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext) Implements ISerializable.GetObjectData
				If (info Is Nothing) Then
					Throw New ArgumentNullException("info")
				End If
				info.AddValue("DateStart", Me.m_dateStart)
				info.AddValue("DateEnd", Me.m_dateEnd)
				info.AddValue("DaylightDelta", Me.m_daylightDelta)
				info.AddValue("DaylightTransitionStart", Me.m_daylightTransitionStart)
				info.AddValue("DaylightTransitionEnd", Me.m_daylightTransitionEnd)
			End Sub

			Private Shared Sub ValidateAdjustmentRule(ByVal dateStart As DateTime, ByVal dateEnd As DateTime, ByVal daylightDelta As TimeSpan, ByVal daylightTransitionStart As DevComponents.Schedule.TimeZoneInfo.TransitionTime, ByVal daylightTransitionEnd As DevComponents.Schedule.TimeZoneInfo.TransitionTime)
				If (dateStart.Kind <> DateTimeKind.Unspecified) Then
					Throw New ArgumentException("Argument_DateTimeKindMustBeUnspecified", "dateStart")
				End If
				If (dateEnd.Kind <> DateTimeKind.Unspecified) Then
					Throw New ArgumentException("Argument_DateTimeKindMustBeUnspecified", "dateEnd")
				End If
				If (daylightTransitionStart.Equals(daylightTransitionEnd)) Then
					Throw New ArgumentException("Argument_TransitionTimesAreIdentical", "daylightTransitionEnd")
				End If
				If (dateStart > dateEnd) Then
					Throw New ArgumentException("Argument_OutOfOrderDateTimes", "dateStart")
				End If
				If (DevComponents.Schedule.TimeZoneInfo.UtcOffsetOutOfRange(daylightDelta)) Then
					Throw New ArgumentOutOfRangeException("daylightDelta", daylightDelta, "ArgumentOutOfRange_UtcOffset")
				End If
				If (daylightDelta.Ticks Mod CLng(600000000) <> CLng(0)) Then
					Throw New ArgumentException("Argument_TimeSpanHasSeconds", "daylightDelta")
				End If
				If (dateStart.TimeOfDay <> TimeSpan.Zero) Then
					Throw New ArgumentException("Argument_DateTimeHasTimeOfDay", "dateStart")
				End If
				If (dateEnd.TimeOfDay <> TimeSpan.Zero) Then
					Throw New ArgumentException("Argument_DateTimeHasTimeOfDay", "dateEnd")
				End If
			End Sub
		End Class

		<HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort:=True)>
		<HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort:=True)>
		<Serializable>
		Public Structure TransitionTime
			Implements IEquatable(Of DevComponents.Schedule.TimeZoneInfo.TransitionTime), ISerializable, IDeserializationCallback
			Private m_timeOfDay As DateTime

			Private m_month As Byte

			Private m_week As Byte

			Private m_day As Byte

			Private m_dayOfWeek As DayOfWeek

			Private m_isFixedDateRule As Boolean

			Public ReadOnly Property Day As Integer
				Get
					Return Me.m_day
				End Get
			End Property

			Public ReadOnly Property DayOfWeek As DayOfWeek
				Get
					Return Me.m_dayOfWeek
				End Get
			End Property

			Public ReadOnly Property IsFixedDateRule As Boolean
				Get
					Return Me.m_isFixedDateRule
				End Get
			End Property

			Public ReadOnly Property Month As Integer
				Get
					Return Me.m_month
				End Get
			End Property

			Public ReadOnly Property TimeOfDay As DateTime
				Get
					Return Me.m_timeOfDay
				End Get
			End Property

			Public ReadOnly Property Week As Integer
				Get
					Return Me.m_week
				End Get
			End Property

			Private Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
				If (info Is Nothing) Then
					Throw New ArgumentNullException("info")
				End If
				Me.m_timeOfDay = DirectCast(info.GetValue("TimeOfDay", GetType(DateTime)), DateTime)
				Me.m_month = CByte(info.GetValue("Month", GetType(Byte)))
				Me.m_week = CByte(info.GetValue("Week", GetType(Byte)))
				Me.m_day = CByte(info.GetValue("Day", GetType(Byte)))
				Me.m_dayOfWeek = DirectCast(info.GetValue("RelativeDayOfWeek", GetType(DayOfWeek)), DayOfWeek)
				Me.m_isFixedDateRule = CBool(info.GetValue("IsFixedDateRule", GetType(Boolean)))
			End Sub

			Public Shared Function CreateFixedDateRule(ByVal timeOfDay As DateTime, ByVal month As Integer, ByVal day As Integer) As DevComponents.Schedule.TimeZoneInfo.TransitionTime
				Return DevComponents.Schedule.TimeZoneInfo.TransitionTime.CreateTransitionTime(timeOfDay, month, 1, day, DayOfWeek.Sunday, True)
			End Function

			Public Shared Function CreateFloatingDateRule(ByVal timeOfDay As DateTime, ByVal month As Integer, ByVal week As Integer, ByVal dayOfWeek As System.DayOfWeek) As DevComponents.Schedule.TimeZoneInfo.TransitionTime
				Return DevComponents.Schedule.TimeZoneInfo.TransitionTime.CreateTransitionTime(timeOfDay, month, week, 1, dayOfWeek, False)
			End Function

			Private Shared Function CreateTransitionTime(ByVal timeOfDay As DateTime, ByVal month As Integer, ByVal week As Integer, ByVal day As Integer, ByVal dayOfWeek As System.DayOfWeek, ByVal isFixedDateRule As Boolean) As DevComponents.Schedule.TimeZoneInfo.TransitionTime
				DevComponents.Schedule.TimeZoneInfo.TransitionTime.ValidateTransitionTime(timeOfDay, month, week, day, dayOfWeek)
				Dim transitionTime As DevComponents.Schedule.TimeZoneInfo.TransitionTime = New DevComponents.Schedule.TimeZoneInfo.TransitionTime() With
				{
					.m_isFixedDateRule = isFixedDateRule,
					.m_timeOfDay = timeOfDay,
					.m_dayOfWeek = dayOfWeek,
					.m_day = CByte(day),
					.m_week = CByte(week),
					.m_month = CByte(month)
				}
				Return transitionTime
			End Function

			Public Overrides Function Equals(ByVal obj As Object) As Boolean Implements IEquatable(Of DevComponents.Schedule.TimeZoneInfo.TransitionTime).Equals
				If (Not TypeOf obj Is DevComponents.Schedule.TimeZoneInfo.TransitionTime) Then
					Return False
				End If
				Return Me.Equals(DirectCast(obj, DevComponents.Schedule.TimeZoneInfo.TransitionTime))
			End Function

			Public Function Equals(ByVal other As DevComponents.Schedule.TimeZoneInfo.TransitionTime) As Boolean Implements IEquatable(Of DevComponents.Schedule.TimeZoneInfo.TransitionTime).Equals
				Dim flag As Boolean = If(Me.m_isFixedDateRule <> other.m_isFixedDateRule OrElse Not (Me.m_timeOfDay = other.m_timeOfDay), False, Me.m_month = other.m_month)
				If (Not flag) Then
					Return flag
				End If
				If (other.m_isFixedDateRule) Then
					Return Me.m_day = other.m_day
				End If
				If (Me.m_week <> other.m_week) Then
					Return False
				End If
				Return Me.m_dayOfWeek = other.m_dayOfWeek
			End Function

			Public Overrides Function GetHashCode() As Integer
				Return Me.m_month Xor Me.m_week << 8
			End Function

			Public Shared Operator =(ByVal left As DevComponents.Schedule.TimeZoneInfo.TransitionTime, ByVal right As DevComponents.Schedule.TimeZoneInfo.TransitionTime) As Boolean
				Return left.Equals(right)
			End Operator

			Public Shared Operator <>(ByVal left As DevComponents.Schedule.TimeZoneInfo.TransitionTime, ByVal right As DevComponents.Schedule.TimeZoneInfo.TransitionTime) As Boolean
				Return Not left.Equals(right)
			End Operator

			Private Sub OnDeserialization(ByVal sender As Object) Implements IDeserializationCallback.OnDeserialization
				Try
					DevComponents.Schedule.TimeZoneInfo.TransitionTime.ValidateTransitionTime(Me.m_timeOfDay, CInt(Me.m_month), CInt(Me.m_week), CInt(Me.m_day), Me.m_dayOfWeek)
				Catch argumentException As System.ArgumentException
					Throw New SerializationException("Serialization_InvalidData", argumentException)
				End Try
			End Sub

			<SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.SerializationFormatter)>
			Private Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext) Implements ISerializable.GetObjectData
				If (info Is Nothing) Then
					Throw New ArgumentNullException("info")
				End If
				info.AddValue("TimeOfDay", Me.m_timeOfDay)
				info.AddValue("Month", Me.m_month)
				info.AddValue("Week", Me.m_week)
				info.AddValue("Day", Me.m_day)
				info.AddValue("RelativeDayOfWeek", Me.m_dayOfWeek)
				info.AddValue("IsFixedDateRule", Me.m_isFixedDateRule)
			End Sub

			Private Shared Sub ValidateTransitionTime(ByVal timeOfDay As DateTime, ByVal month As Integer, ByVal week As Integer, ByVal day As Integer, ByVal dayOfWeek As System.DayOfWeek)
				If (timeOfDay.Kind <> DateTimeKind.Unspecified) Then
					Throw New ArgumentException("Argument_DateTimeKindMustBeUnspecified", "timeOfDay")
				End If
				If (month < 1 OrElse month > 12) Then
					Throw New ArgumentOutOfRangeException("month", "ArgumentOutOfRange_Month")
				End If
				If (day < 1 OrElse day > 31) Then
					Throw New ArgumentOutOfRangeException("day", "ArgumentOutOfRange_Day")
				End If
				If (week < 1 OrElse week > 5) Then
					Throw New ArgumentOutOfRangeException("week", "ArgumentOutOfRange_Week")
				End If
				If (dayOfWeek < System.DayOfWeek.Sunday OrElse dayOfWeek > System.DayOfWeek.Saturday) Then
					Throw New ArgumentOutOfRangeException("dayOfWeek", "ArgumentOutOfRange_DayOfWeek")
				End If
				If (timeOfDay.Year <> 1 OrElse timeOfDay.Month <> 1 OrElse timeOfDay.Day <> 1 OrElse timeOfDay.Ticks Mod CLng(10000) <> CLng(0)) Then
					Throw New ArgumentException("Argument_DateTimeHasTicks", "timeOfDay")
				End If
			End Sub
		End Structure

		Private Class ⠥
			Implements IComparer(Of DevComponents.Schedule.TimeZoneInfo)
			Public Sub New()
				MyBase.New()
			End Sub

			Private Function ⠦(ByVal ـ As DevComponents.Schedule.TimeZoneInfo, ByVal ف As DevComponents.Schedule.TimeZoneInfo) As Integer Implements IComparer(Of DevComponents.Schedule.TimeZoneInfo).Compare
				Return Utilities.▀(ـ.Id, ف.Id)
			End Function
		End Class
	End Class
End Namespace