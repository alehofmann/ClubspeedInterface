Imports DCS.KioskV15.ClubSpeedInterface.Utils

Module IniManager

    Public Enum enuTaskbarMessages As System.Int32
        tm_abs_AutoHIde = &H1         'This is a response value when using tm_abm_GetState, and a setting when using tm_abm_SetState
        tm_abs_OnTop = &H2            'This is a response value when using tm_abm_GetState, and a setting when using tm_abm_SetState
        tm_abm_New = &H0
        tm_abm_Remove = &H1
        tm_abm_QueryPos = &H2
        tm_abm_SetPos = &H3
        tm_abm_GetState = &H4
        tm_abm_GetTaskbarPos = &H5
        tm_abm_Activate = &H6
        tm_abm_GetAutohIdeBar = &H7
        tm_abm_SetAutohIdeBar = &H8
        tm_abm_WindowPosChanged = &H9
        tm_abm_SetState = &HA
    End Enum

    <System.FlagsAttribute()>
    Public Enum enuExecutionState As UInteger 'Determine System and Display Power State
        es_SystemRequired = &H1
        es_DisplayRequired = &H2
        es_AwayModeRequired = &H40
        es_Continuous = &H80000000UI
        'Legacy flag, should not be used.
        'es_UserPresent = &H4
    End Enum

    'Enables an application to inform the system that it is in use, thereby preventing the system from entering sleep or turning off the display while the application is running.
    <System.Runtime.InteropServices.DllImport("Kernel32.Dll",
     CharSet:=System.Runtime.InteropServices.CharSet.Auto,
     SetLastError:=True)>
    Private Function SetThreadExecutionState(ByVal esFlags As enuExecutionState) As enuExecutionState
    End Function

    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential,
                                                 CharSet:=System.Runtime.InteropServices.CharSet.Ansi,
                                                 Pack:=1)>
    Private Structure uLUId
        Friend UsedPart As System.UInt32
        Friend IgnoredForNowHigh32BitPart As System.UInt32
    End Structure

    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential,
                                                 CharSet:=System.Runtime.InteropServices.CharSet.Ansi,
                                                 Pack:=1)>
    Private Structure uTokenPrivileges
        Friend PrivilegeCount As System.UInt32
        Friend LUId As uLUId
        Friend Attributes As System.UInt32
    End Structure

    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential,
                                                 CharSet:=System.Runtime.InteropServices.CharSet.Ansi,
                                                 Pack:=1)>
    Private Structure uRECT
        Private Left As System.Int32
        Private Top As System.Int32
        Private Right As System.Int32
        Private Bottom As System.Int32
    End Structure

    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential,
                                                 CharSet:=System.Runtime.InteropServices.CharSet.Ansi,
                                                 Pack:=1)>
    Private Structure uAppbarData
        Friend cbSize As System.Int32
        Friend hWnd As System.Int32
        Friend uCallbackMessage As System.Int32
        Friend uEdge As System.Int32
        Friend rc As System.Drawing.Rectangle 'RECT
        Friend lParam As enuTaskbarMessages
    End Structure

    Private Const ENCRYPTION_KEY As String = "PAMPITA"

    Public Const SCARD_INI As String = "\Data\SCard.Ini"

    Private _iniName As String = System.String.Empty,
                _exceptionShowMBox As Boolean = True,
                _exceptionCallback As System.Action = Nothing

    'The API functions below are all used to give the application the proper privilege so the OS will allow the app to Shutdown Windows.    
    <System.Runtime.InteropServices.DllImport("AdvAPI32.Dll",
     CharSet:=System.Runtime.InteropServices.CharSet.Auto,
     SetLastError:=True)>
    Private Function OpenProcessToken(
             ByVal ProcessHandle As System.IntPtr,
             ByVal DesiredAccess As System.UInt32,
             ByRef TokenHandle As System.UInt32
            ) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("AdvAPI32.Dll",
     CharSet:=System.Runtime.InteropServices.CharSet.Auto,
     SetLastError:=True)>
    Private Function LookupPrivilegeValue(
             ByVal lpSystemName As String,
             ByVal lpName As String,
             ByRef lpLuId As uLUId
            ) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("AdvAPI32.Dll",
     CharSet:=System.Runtime.InteropServices.CharSet.Auto,
     SetLastError:=True)>
    Private Function AdjustTokenPrivileges(
             ByVal TokenHandle As Integer,
             ByVal DisableAllPrivileges As Boolean,
             ByRef NewState As uTokenPrivileges,
             ByVal BufferLength As Integer,
             ByRef PreviousState As uTokenPrivileges,
             ByRef ReturnLength As Integer
            ) As Integer
    End Function

    <System.Flags()>
    Private Enum enuExitWindowsExMode As UInteger
        ewx_LogOff = &H0
        ewx_ShutDown = &H1
        ewx_Reboot = &H2
        ewx_Force = &H4
        ewx_PowerOff = &H8
        ewx_ForceIfHung = &H10
        ewx_RestartApps = &H40
        ewx_HybrIdShutdown = &H400000
    End Enum

    <System.Flags()>
    Enum enuShutdownReason As UInteger
        MajorApplication = &H40000
        MajorHardware = &H10000
        MajorLegacyApi = &H70000
        MajorOperatingSystem = &H20000
        MajorOther = &H0
        MajorPower = &H60000
        MajorSoftware = &H30000
        MajorSystem = &H50000

        MinorBlueScreen = &HF
        MinorCordUnplugged = &HB
        MinorDisk = &H7
        MinorEnvironment = &HC
        MinorHardwareDriver = &HD
        MinorHotfix = &H11
        MinorHung = &H5
        MinorInstallation = &H2
        MinorMaintenance = &H1
        MinorMMC = &H19
        MinorNetworkConnectivity = &H14
        MinorNetworkCard = &H9
        MinorOther = &H0
        MinorOtherDriver = &HE
        MinorPowerSupply = &HA
        MinorProcessor = &H8
        MinorReconfig = &H4
        MinorSecurity = &H13
        MinorSecurityFix = &H12
        MinorSecurityFixUninstall = &H18
        MinorServicePack = &H10
        MinorServicePackUninstall = &H16
        MinorTermSrv = &H20
        MinorUnstable = &H6
        MinorUpgrade = &H3
        MinorWMI = &H15

        FlagUserDefined = &H40000000
        FlagPlanned = &H80000000&
    End Enum

    'The function used to actually send the request to shutdown windows. Set the 'shutdownTypes' parameter 
    'to whether you want windows to 'shutdown, reboot, logOff, ect…' You can get those at MSDN or download
    'one of my examples from my VBCodesource.com website.
    <System.Runtime.InteropServices.DllImport("User32.Dll",
     CharSet:=System.Runtime.InteropServices.CharSet.Auto,
     SetLastError:=True)>
    Private Function ExitWindowsEx(
            ByVal shutdownType As enuExitWindowsExMode,
            ByVal dwReserved_passNULL As enuShutdownReason
           ) As System.UInt32
    End Function

    'This sub will do all of the work of setting up your apps process using APIs 
    'to get the proper privileges to shutdown the OS.
    'Originally got this function from MSDN and converted it from VB6 to VB.Net,
    'and dId a tweak here and there.
    Private Function AdjustToken() As Boolean

        Const TOKEN_ADJUST_PRIVILEGES As System.Int32 = &H20,
              TOKEN_QUERY As System.Int32 = &H8,
              SE_PRIVILEGE_ENABLED As System.Int32 = &H2

        Dim retVal As Boolean = False,
            completionCode As System.UInt32 = 0,
            hProcess As System.IntPtr = System.Diagnostics.Process.GetCurrentProcess.Handle,
            hToken As System.Int32,
            tmpLuId As uLUId,
            tokenPrivs As uTokenPrivileges,
            tokenPrivsTemp As uTokenPrivileges,
            bufferNeeded As System.Int32

        completionCode = OpenProcessToken(hProcess, (TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY), hToken)
        If (completionCode <> 0) Then

            'Get the LUId for shutdown privilege, flag it as ENABLED.
            completionCode = LookupPrivilegeValue("", "SeShutdownPrivilege", tmpLuId)
            If (completionCode <> 0) Then

                tokenPrivs.PrivilegeCount = 1 'One privilege to set
                tokenPrivs.LUId = tmpLuId
                tokenPrivs.Attributes = SE_PRIVILEGE_ENABLED

                'Enable the shutdown privilege in the access token of this process.
                completionCode = AdjustTokenPrivileges(hToken,
                                                       False,
                                                       tokenPrivs,
                                                       System.Runtime.InteropServices.Marshal.SizeOf(tokenPrivs),
                                                       tokenPrivsTemp,
                                                       bufferNeeded
                                                      )

                retVal = (completionCode <> 0)

            End If

        End If

        Return (retVal)

    End Function

    Friend Function RestartComputer() As Boolean

        Dim retVal As Boolean = False

        AdjustToken()   'Sometimes this call fails, though dunno why.

        retVal = (ExitWindowsEx((enuExitWindowsExMode.ewx_ForceIfHung Or enuExitWindowsExMode.ewx_Reboot),
                                (enuShutdownReason.MajorOther Or enuShutdownReason.MinorOther)
                               ) <> 0
                 )

        Return (retVal)

    End Function

    <System.Runtime.InteropServices.DllImport("Shell32.Dll",
     EntryPoint:="SHAppBarMessage",
     SetLastError:=True,
     CharSet:=System.Runtime.InteropServices.CharSet.Auto,
     ExactSpelling:=True,
     CallingConvention:=System.Runtime.InteropServices.CallingConvention.StdCall)
    >
    Private Function SHAppBarMessage(ByVal dwMessage As enuTaskbarMessages,
                                     ByRef pData As uAppbarData
                                    ) As System.Int32
    End Function

    <System.Runtime.InteropServices.DllImport("Kernel32.Dll",
     EntryPoint:="GetPrivateProfileSectionW",
     SetLastError:=True,
     CharSet:=System.Runtime.InteropServices.CharSet.Unicode,
     ExactSpelling:=True,
     CallingConvention:=System.Runtime.InteropServices.CallingConvention.StdCall)
    >
    Private Function GetPrivateProfileSectionKeys(ByVal section As String,
                                                  ByVal keysArray As String,
                                                  ByVal keysCount As Integer,
                                                  ByVal iniFileName As String
                                                 ) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("Kernel32.Dll",
     EntryPoint:="GetPrivateProfileStringW",
     SetLastError:=True,
     CharSet:=System.Runtime.InteropServices.CharSet.Unicode,
     ExactSpelling:=True,
     CallingConvention:=System.Runtime.InteropServices.CallingConvention.StdCall)
    >
    Friend Function GetPrivateProfileString(ByVal section As String,
                                            ByVal key As String,
                                            ByVal defaultValue As String,
                                            ByVal valueData As String,
                                            ByVal bytesRead As Integer,
                                            ByVal iniFileName As String
                                           ) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("Kernel32.Dll",
     EntryPoint:="WritePrivateProfileStringW",
     SetLastError:=True,
     CharSet:=System.Runtime.InteropServices.CharSet.Unicode,
     ExactSpelling:=True,
     CallingConvention:=System.Runtime.InteropServices.CallingConvention.StdCall)
    >
    Private Function WritePrivateProfileString(ByVal section As String,
                                               ByVal key As String,
                                               ByVal valueData As String,
                                               ByVal iniFileName As String
                                              ) As Integer
    End Function

    Friend Function BusinessDate(ByVal targetDate As Date) As Date

        'Returns a DATE representing the business date.
        Dim bizDayStartTime As String,
            bizDayStartHour As Integer,
            bizDayStartMinute As Integer,
            retVal As Date = New Date(targetDate.Year, targetDate.Month, targetDate.Day)

        bizDayStartTime = IniGet("General", "BusinessDayStart", "06:00", ExpandTokens("%NW%") & SCARD_INI)
        'bizDayStartHour = CInt(Microsoft.VisualBasic.Left(bizDayStartTime, Microsoft.VisualBasic.InStr(bizDayStartTime, ":") - 1))
        'bizDayStartMinute = CInt(Microsoft.VisualBasic.MId(bizDayStartTime, Microsoft.VisualBasic.InStr(bizDayStartTime, ":") + 1))
        bizDayStartHour = CInt(bizDayStartTime.Substring(0, bizDayStartTime.IndexOf(":")))
        bizDayStartMinute = CInt(bizDayStartTime.Substring(bizDayStartTime.IndexOf(":") + 1))


        If (Microsoft.VisualBasic.TimeSerial(targetDate.Hour, targetDate.Minute, 0) < Microsoft.VisualBasic.TimeSerial(bizDayStartHour, bizDayStartMinute, 0)) Then
            'It's yesterday!
            retVal = targetDate.AddDays(-1)
        End If

        Return retVal

    End Function

    Friend Function InIdeleteKey(ByVal section As String,
                                 ByVal key As String,
                                 ByVal iniFileName As String
                                ) As Boolean

        Dim iI As Integer

        iI = WritePrivateProfileString(section, key, 0&, iniFileName)
        Return (iI <> 0)

    End Function

    Friend Function IniGet(ByVal section As String,
                           ByVal key As String,
                           ByVal valueDefault As String,
                           ByVal iniFileName As String
                          ) As String

        Dim temp As String,
            charsRead As Integer

        temp = Microsoft.VisualBasic.Space(65536)
        charsRead = GetPrivateProfileString(section, key, valueDefault, temp, temp.Length - 1, iniFileName)
        Return (temp.Substring(0, charsRead))

    End Function

    Friend Function IniGetDataGrIdViewGrIdLinesType(ByVal iniSection As String,
                                                    ByVal iniKey As String,
                                                    ByVal defaultType_None As System.Windows.Forms.DataGridViewCellBorderStyle,
                                                    ByVal iniFileName As String
                                                   ) As System.Windows.Forms.DataGridViewCellBorderStyle

        'This routine returns a valId GrIdLinesType from a INI key value as follows:
        '  [<iniSection>]
        '  <iniKey> = [1-10 | Custom (0, not used) | Single (1) | Raised (2) |
        '              Sunken (3) | None (4, default) | SingleVertical (5) | 
        '              RaisedVertical (6) | SunkenVertical (7) | SingleHorizontal (8) |
        '              RaisedHorizontal (9) | SunkenHorizontal (10)]

        Dim retVal As System.Windows.Forms.DataGridViewCellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None,
                aux As String = System.String.Empty,
                auxVal As Integer = -1

        aux = IniGet(iniSection, iniKey, CInt(defaultType_None).ToString, iniFileName)

        If (Microsoft.VisualBasic.IsNumeric(aux)) Then

            If (System.Int32.TryParse(aux, auxVal)) Then
                If (auxVal.Between(System.Windows.Forms.DataGridViewCellBorderStyle.Single,
                                   System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal
                                  )
                   ) Then
                    retVal = auxVal
                End If
            End If

        Else

            'Select Case aux.Trim.ToUpper

            '    Case "SINGLE"
            '        retVal = System.Windows.Forms.DataGrIdViewCellBorderStyle.Single
            '    Case "RAISED"
            '        retVal = System.Windows.Forms.DataGrIdViewCellBorderStyle.Raised
            '    Case "SUNKEN"
            '        retVal = System.Windows.Forms.DataGrIdViewCellBorderStyle.Sunken
            '    Case "NONE"
            '        retVal = System.Windows.Forms.DataGrIdViewCellBorderStyle.None
            '    Case "SINGLEVERTICAL"
            '        retVal = System.Windows.Forms.DataGrIdViewCellBorderStyle.SingleVertical
            '    Case "RAISEDVERTICAL"
            '        retVal = System.Windows.Forms.DataGrIdViewCellBorderStyle.RaisedVertical
            '    Case "SUNKENVERTICAL"
            '        retVal = System.Windows.Forms.DataGrIdViewCellBorderStyle.SunkenVertical
            '    Case "SINGLEHORIZONTAL"
            '        retVal = System.Windows.Forms.DataGrIdViewCellBorderStyle.SingleHorizontal
            '    Case "RAISEDHORIZONTAL"
            '        retVal = System.Windows.Forms.DataGrIdViewCellBorderStyle.RaisedHorizontal
            '    Case "SUNKENHORIZONTAL"
            '        retVal = System.Windows.Forms.DataGrIdViewCellBorderStyle.SunkenHorizontal

            'End Select

            retVal = CType([System].Enum.Parse(GetType(System.Windows.Forms.DataGridViewCellBorderStyle),
                                               aux,
                                               True
                                              ),
                           System.Windows.Forms.DataGridViewCellBorderStyle
                          )

        End If

        Return (retVal)

    End Function

    Friend Function IniGetContentAlign(ByVal iniSection As String,
                                       ByVal iniKey As String,
                                       ByVal defaultAlignment_MIddleCenter As System.Drawing.ContentAlignment,
                                       ByVal iniFileName As String
                                      ) As System.Drawing.ContentAlignment

        'This routine returns a valId content alignment from a INI key value as follows:
        '  [<iniSection>]
        '  <iniKey> = [1-1024 | TopLeft | TopCenter | TopRight | MIddleLeft | MIddleCenter | MIddleRight | BottomLeft | BottomCenter | BottomRight]
        'If numerals are used (shown Microsoft's standard values):
        '     1 |    2 |    4
        ' ------+------+------
        '    16 |   32 |   64
        ' ------+------+------
        '   256 |  512 | 1024

        Dim retVal As System.Drawing.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter,
            aux As String = System.String.Empty

        aux = IniGet(iniSection, iniKey, CInt(defaultAlignment_MIddleCenter).ToString, iniFileName)

        If (Microsoft.VisualBasic.IsNumeric(aux)) Then

            'Use number-grId
            retVal = CInt(aux)

        Else

            'Convert text to alignment
            'Select Case aux.Trim.ToUpper

            '    Case "TOPLEFT"
            '        retVal = System.Drawing.ContentAlignment.TopLeft
            '    Case "TOPCENTER"
            '        retVal = System.Drawing.ContentAlignment.TopCenter
            '    Case "TOPRIGHT"
            '        retVal = System.Drawing.ContentAlignment.TopRight

            '    Case "LEFT", "MIdDLELEFT"
            '        retVal = System.Drawing.ContentAlignment.MIddleLeft
            '    Case "CENTER", "MIdDLECENTER"
            '        retVal = System.Drawing.ContentAlignment.MIddleCenter
            '    Case "RIGHT", "MIdDLERIGHT"
            '        retVal = System.Drawing.ContentAlignment.MIddleRight

            '    Case "BOTTOMLEFT"
            '        retVal = System.Drawing.ContentAlignment.BottomLeft
            '    Case "BOTTOMCENTER"
            '        retVal = System.Drawing.ContentAlignment.BottomCenter
            '    Case "BOTTOMRIGHT"
            '        retVal = System.Drawing.ContentAlignment.BottomRight

            'End Select

            retVal = CType([System].Enum.Parse(GetType(System.Drawing.ContentAlignment),
                                               aux,
                                               True
                                              ),
                           System.Drawing.ContentAlignment
                          )

        End If

        Return (retVal)

    End Function

    Friend Function IniGetColor(ByVal iniSection As String,
                                ByVal iniKey As String,
                                ByVal defaultColorNameOrHexARGB As String,
                                ByVal iniFileName As String
                               ) As System.Drawing.Color

        'This routine returns a valId color from a INI key value as follows (supports ONLY hex numbers and color names):
        '  [<iniSection>]
        '  <iniKey> = &HE08040        'Some color with Red=&HE0, Green=&H80 and Blue=&H40 - Should use Alpha=&HFF
        '  <iniKey> = &HC0E08040      'Same as first, but includes ALPHA channel at value &HC0
        '  <iniKey> = Transparent     'Yup, it also supports standard .Net color names. Amazing.

        Dim retVal As System.Drawing.Color = Nothing,
            aux As String = System.String.Empty,
            auxVal As Integer = 0,
            alpha As Byte = 255

        aux = IniGet(iniSection, iniKey, defaultColorNameOrHexARGB, iniFileName).Trim
        If (Microsoft.VisualBasic.IsNumeric(aux)) Then

            'It's a specific '&HRRGGBB' or '&HAARRGGBB' color
            If (aux.Length > 8) Then
                'Has alpha
                alpha = CByte(aux.Substring(0, 4))
                aux = "&H" & aux.Substring(4)
            End If
            Return (System.Drawing.Color.FromArgb(alpha, System.Drawing.ColorTranslator.FromHtml(aux)))

        Else

            'Should be a color name
            Return (System.Drawing.Color.FromName(aux))

        End If

    End Function

    Friend Function IniGetFont(ByVal iniSection As String,
                               ByVal iniKeyPrefix As String,
                               ByVal defaultFont As System.Drawing.Font,
                               ByVal iniFileName As String
                              ) As System.Drawing.Font

        'This function will return a font object from an INI section filled with the following keys:
        '  [<iniSection>]
        '  <iniKeyPrefix> Font=<font name|string>
        '  <iniKeyPrefix> Font Size=<font size|number>
        '  <iniKeyPrefix> Font Bold=<font bold|Yes/No>
        '  <iniKeyPrefix> Font Italic=<font italic|Yes/No>
        '  <iniKeyPrefix> Font Underline=<font underline|Yes/No>
        '  <iniKeyPrefix> Font Strikethrough=<font Strikethrough|Yes/No>
        'Normally, <iniKeyPrefix> would be set to the control's name or some equivalent, unique tag.
        'Any absent key's value is taken from the passed <defaultFont> property.

        If Not (iniKeyPrefix.EndsWith(Microsoft.VisualBasic.Space(1))) Then
            'Make sure the prefix ends with a space
            iniKeyPrefix &= Microsoft.VisualBasic.Space(1)
        End If

        Dim fName As String = IniGet(iniSection, iniKeyPrefix & "Font", defaultFont.Name, iniFileName),
            fSize As Single = CDec(IniGet(iniSection, iniKeyPrefix & "Font Size", defaultFont.Size.ToString, iniFileName)),
            fBold As Boolean = IniGetBoolean(iniSection, iniKeyPrefix & "Font Bold", defaultFont.Bold, iniFileName),
            fItalic As Boolean = IniGetBoolean(iniSection, iniKeyPrefix & "Font Italic", defaultFont.Italic, iniFileName),
            fUnderline As Boolean = IniGetBoolean(iniSection, iniKeyPrefix & "Font Underline", defaultFont.Underline, iniFileName),
            fStrikeOut As Boolean = IniGetBoolean(iniSection, iniKeyPrefix & "Font Strikethrough", defaultFont.Strikeout, iniFileName),
            fStyle As System.Drawing.FontStyle = If(fBold, System.Drawing.FontStyle.Bold, System.Drawing.FontStyle.Regular) Or
                                                 If(fItalic, System.Drawing.FontStyle.Italic, System.Drawing.FontStyle.Regular) Or
                                                 If(fUnderline, System.Drawing.FontStyle.Underline, System.Drawing.FontStyle.Regular) Or
                                                 If(fStrikeOut, System.Drawing.FontStyle.Strikeout, System.Drawing.FontStyle.Regular),
            retVal As System.Drawing.Font = New System.Drawing.Font(fName, fSize, fStyle)

        Return retVal

    End Function

    Friend Function IniGetName() As String

        Return _iniName

    End Function

    Friend Function IniGetPoint(ByVal section As String,
                                ByVal key As String,
                                ByVal valueDefault As String,
                                ByVal iniFileName As String
                               ) As System.Drawing.Point

        Dim retVal As New System.Drawing.Point(0, 0),
            data As String = IniGet(section, key, valueDefault, iniFileName),
            vals() As String = Nothing

        If (data.IndexOf(",") > 0) Then
            vals = data.Split(",")
        Else
            vals = valueDefault.Split(",")
        End If
        retVal.X = CInt(vals(vals.GetLowerBound(0) + 0))
        retVal.Y = CInt(vals(vals.GetLowerBound(0) + 1))

        Return (retVal)

    End Function

    Friend Sub IniGetSectionKeys(ByVal section As String,
                                 ByRef keyValueArray() As String,
                                 ByVal iniFileName As String
                                )

        Dim retVal As String,
            iI As Integer,
            nullChar As String = Microsoft.VisualBasic.Chr(0),
            terminator As String = nullChar & nullChar

        retVal = Microsoft.VisualBasic.Space(65536)
        iI = GetPrivateProfileSectionKeys(section, retVal, retVal.Length - 1, iniFileName)
        If (retVal.IndexOf(terminator) >= 0) Then
            retVal = retVal.Substring(0, retVal.IndexOf(terminator))
        Else
            retVal = Nothing
        End If

        keyValueArray = retVal.Split(nullChar)

    End Sub

    Friend Function IniGetBoolean(ByVal section As String,
                                  ByVal key As String,
                                  ByVal valueDefault As Boolean,
                                  ByVal iniFileName As String
                                 ) As Boolean

        'If Current INI's Section|Key equals 1 or -1, starts with "Y", or is "ON" or "True" returns True;
        'False otherwise. If Key is NOT FOUND, returns Defa.

        Dim retVal As Boolean = False,
                auxData As String

        auxData = IniGet(section, key, "_!_", iniFileName).Trim.ToUpper
        If (auxData = "_!_") Then
            retVal = valueDefault
        Else
            retVal = ((auxData.StartsWith("Y")) Or
                      (auxData = "ON") Or
                      (auxData = "1") Or
                      (auxData = "-1") Or
                      (auxData = "TRUE")
                     )
        End If

        Return retVal

    End Function

    Friend Sub IniSetName(ByVal sNewFile As String)

        _iniName = sNewFile

    End Sub

    Friend Function IniPut(ByVal iniSection As String,
                           ByVal iniKey As String,
                           ByVal iniValue As String,
                           ByVal iniFileName As String
                          ) As Boolean

        'Returns TRUE if written successfully

        Dim iI As Integer

        iI = WritePrivateProfileString(iniSection, iniKey, iniValue, iniFileName)
        Return (iI <> 0)

    End Function

    Friend Sub IniPutBoolean(ByVal iniSection As String,
                             ByVal iniKey As String,
                             ByVal boolValue As Boolean,
                             ByVal iniFileName As String
                            )

        IniPut(iniSection, iniKey, If(boolValue, "Yes", "No"), iniFileName)

    End Sub

    Friend Sub IniPutFont(ByVal iniSection As String,
                          ByVal iniKeyPrefix As String,
                          ByVal fontData As System.Drawing.Font,
                          ByVal iniFileName As String
                         )

        'This function will save a font object to an INI section using the following keys:
        '  [<iniSection>]
        '  <iniKeyPrefix> Font=<font name|string>
        '  <iniKeyPrefix> Font Size=<font size|number>
        '  <iniKeyPrefix> Font Bold=<font bold|Yes/No>
        '  <iniKeyPrefix> Font Italic=<font italic|Yes/No>
        '  <iniKeyPrefix> Font Underline=<font underline|Yes/No>
        '  <iniKeyPrefix> Font Strikethrough=<font Strikethrough|Yes/No>
        'Normally, <iniKeyPrefix> would be set to the control's name or some equivalent, unique tag.

        If Not (iniKeyPrefix.EndsWith(Microsoft.VisualBasic.Space(1))) Then
            'Make sure the prefix ends with a space
            iniKeyPrefix &= Microsoft.VisualBasic.Space(1)
        End If

        IniPut(iniSection, iniKeyPrefix & "Font", fontData.Name, iniFileName)
        IniPut(iniSection, iniKeyPrefix & "Font Size", fontData.Size.ToString, iniFileName)
        IniPut(iniSection, iniKeyPrefix & "Font Bold", If(fontData.Bold, "Yes", "No"), iniFileName)
        IniPut(iniSection, iniKeyPrefix & "Font Italic", If(fontData.Italic, "Yes", "No"), iniFileName)
        IniPut(iniSection, iniKeyPrefix & "Font Underline", If(fontData.Underline, "Yes", "No"), iniFileName)
        IniPut(iniSection, iniKeyPrefix & "Font Strikethrough", If(fontData.Strikeout, "Yes", "No"), iniFileName)

    End Sub

    Friend Sub IniPutColor(ByVal iniSection As String,
                           ByVal iniKey As String,
                           ByVal colorData As System.Drawing.Color,
                           ByVal iniFileName As String
                          )

        'This sub will save a standard .Net color to an INI file's 
        'section's key in numeric format supporting Alpha.

        If (colorData.A <> &HFF) Then
            IniPut(iniSection, iniKey, "Transparent", iniFileName)
        Else
            IniPut(iniSection, iniKey, "&h" & colorData.ToArgb.ToString("X8"), iniFileName)
        End If

    End Sub

    'Friend Function GetSwitch(ByVal switchName As String,
    '                          ByVal valueDefault As String
    '                         ) As String

    '    'Getswitch returns value of switch passed as parameter. Function is Case Insensitive. Proper syntax for
    '    'switches in command line is /switchname=switchvalue. Only switchname should be passed as parameter, not
    '    'the leading / or the trailing =. There should be no spaces between the slash and switchname nor between
    '    'the switchname and the = sign. Spaces after the = are Ok. Switchvalue may contain spaces, as / acts as
    '    'switch separator. If switchname not found in command line, function returns a Default as passed from
    '    'caller. If default is NULL then looks for switch w/o = sign and returns a blank if switch present,
    '    'null otherwise.

    '    Dim retVal As String = valueDefault,
    '        switchText As String = System.String.Empty,
    '        found As Boolean = False

    '    'Locate switch
    '    switchText = "/" & switchName.ToUpper
    '    For Each param As String In My.Application.CommandLineArgs
    '        If (param.ToUpper.StartsWith(switchText)) Then
    '            switchText = param
    '            found = True
    '            Exit For
    '        End If
    '    Next param

    '    'Process switch
    '    If (found) Then
    '        If (switchText.IndexOf("=") > 0) Then
    '            'Has an equal, return the value
    '            retVal = switchText.Substring(switchText.IndexOf("=") + 1)
    '        Else
    '            'Has no equal, but switch is present, return blank.
    '            retVal = Microsoft.VisualBasic.Space(1)
    '        End If
    '    End If

    '    Return (retVal)

    'End Function

    Friend Function ExpandTokens(ByVal sToExpand As String,
                                 Optional ByVal dDate As Date = Nothing
                                ) As String

        'Return SubstEVars(SubstMacros(sToExpand, dDate))
        Return (System.Environment.ExpandEnvironmentVariables(SubstMacros(sToExpand, dDate)))

    End Function

    Private Function GetMacroValue(ByVal macroName As String,
                                   Optional ByVal separator As String = "|",
                                   Optional ByVal targetDate As Date = #12:00:00 AM#
                                  ) As String

        'Returns the passed macro value as a string
        Dim macroText As String = System.String.Empty,
            macroFormat As String = System.String.Empty,
            myDate As Date = Microsoft.VisualBasic.Now,
            paramSeparator As Char = ",",
            parameter As String = System.String.Empty

        If (targetDate <> System.DateTime.MinValue) Then
            'If dDate = 0 Then
            myDate = targetDate
        End If

        If (macroName.IndexOf(separator) >= 0) Then

            'We have a macro name and a format specifier
            'macroText = Microsoft.VisualBasic.UCase(Microsoft.VisualBasic.Trim(Microsoft.VisualBasic.Left(macroName, Microsoft.VisualBasic.InStr(macroName, separator) - 1)))
            macroText = macroName.Substring(0, macroName.IndexOf(separator)).Trim.ToUpper

            If (macroText.Contains(paramSeparator)) Then
                parameter = macroText.Substring(macroText.IndexOf(paramSeparator) + 1)
                macroText = macroText.Substring(0, macroText.IndexOf(paramSeparator))
            End If
            'macroFormat = MId(macroName, InStr(macroName, separator) + Len(separator))
            macroFormat = macroName.Substring(macroName.IndexOf(separator) + separator.Length)

            Select Case macroText

                Case "BUSINESSDATE"
                    Return BusinessDate(myDate).ToString(macroFormat)

                Case "BUSINESSDATEYESTERDAY"
                    'Return DateAdd(DateInterval.Day, -1, BusinessDate(myDate)).ToString(macroFormat)
                    Return (BusinessDate(myDate).AddDays(-1).ToString(macroFormat))

                Case "CURRENTWEEKSTART"
                    'Returns the first date of the current week, based in the passed "first weekday" number
                    'Syntax is $TOKENNAME,<firstdayofweek[0-7]>|FORMAT$ (0=UseRegionalSettings, 1-7=Sun-Sat respectively)
                    Return (Microsoft.VisualBasic.DateAdd(Microsoft.VisualBasic.DateInterval.Day, -Microsoft.VisualBasic.Weekday(myDate, CInt(parameter)) + 1, myDate).ToString(macroFormat))

                Case "DAYSFROMBUSINESSDATE"
                    'Returns BusinessDate + X days (X can be negative). Token syntax is $TOKENNAME,DAYS|FORMAT$
                    'Return DateAdd(DateInterval.Day, Val(parameter), BusinessDate(myDate)).ToShortDateString(macroFormat)
                    Return (BusinessDate(myDate).AddDays(CInt(parameter)).ToShortDateString(macroFormat))

                Case "DAYSFROMTODAY"
                    'Returns Now + X days (X can be negative). Token syntax is $TOKENNAME,DAYS|FORMAT$
                    'Return DateAdd(DateInterval.Day, Val(parameter), myDate).ToString(macroFormat)
                    Return (myDate.AddDays(CInt(parameter)).ToShortDateString(macroFormat))


                Case "LASTBUSINESSWEEK"
                    'Returns BusinessDate - 7
                    'Return DateAdd(DateInterval.Day, -7, BusinessDate(myDate)).ToString(macroFormat)
                    Return (BusinessDate(myDate).AddDays(-7).ToString(macroFormat))

                Case "LASTMONTH"
                    'Returns the FIRST of LAST month
                    Dim auxDate As Date
                    'dAux = DateSerial(Year(myDate), Month(myDate), 1)
                    'dAux = DateAdd(DateInterval.Day, -1, dAux)
                    'dAux = DateSerial(Year(dAux), Month(dAux), 1)
                    'Return auxDate.ToString(macroFormat)
                    auxDate = New Date(myDate.Year, myDate.Month, 1).AddDays(-1)
                    Return (New Date(auxDate.Year, auxDate.Month, 1).ToShortDateString(macroFormat))

                Case "LASTWEEK"
                    'Returns Now - 7
                    'Return DateAdd(DateInterval.Day, -7, myDate).ToString(macroFormat)
                    Return (myDate.AddDays(-7).ToShortDateString(macroFormat))

                Case "TODAY"
                    Return (myDate.ToString(macroFormat))

                Case "YESTERDAY"
                    'Return DateAdd(DateInterval.Day, -1, myDate).ToString(macroFormat)
                    Return myDate.AddDays(-1).ToShortDateString(macroFormat)

                Case Else
                    Return macroName

            End Select

        Else

            'We have just a macro name, no format specifier
            macroText = macroName.Trim.ToUpper 'UCase(Trim(macroName))
            If macroText.Contains(paramSeparator) Then
                macroText = macroText.Substring(0, macroText.IndexOf(paramSeparator) - 1)
                parameter = macroText.Substring(macroText.IndexOf(paramSeparator) + 1)
            End If

            Select Case macroText

                Case "ANSIdATE6", "HOY6", "TODAY6"
                    Return (myDate.ToString("yyMMdd"))

                Case "ANSIdATE", "HOY", "TODAY", "TODAY8", "ANSIdATE8"
                    Return (myDate.ToString("yyyyMMdd"))

                Case "BUSINESSDATE6"
                    Return (BusinessDate(myDate).ToString("yyMMdd"))

                Case "BUSINESSDATE8", "BUSINESSDATE"
                    Return (BusinessDate(myDate).ToString("yyyyMMdd"))

                Case "BUSINESSDATEYESTERDAY"
                    'Return DateAdd(DateInterval.Day, -1, BusinessDate(myDate)).ToString("yyyyMMdd")
                    Return (BusinessDate(myDate).AddDays(-1).ToString("yyyyMMdd"))

                Case "BUSINESSDAYOFWEEKS"
                    Return Microsoft.VisualBasic.WeekdayName(Microsoft.VisualBasic.Weekday(BusinessDate(myDate)), True, Microsoft.VisualBasic.FirstDayOfWeek.Sunday)

                Case "BUSINESSDAYOFWEEKN"
                    'Return Trim(Str(Weekday(BusinessDate(myDate), FirstDayOfWeek.Sunday)))
                    'Return (Microsoft.VisualBasic.Weekday(BusinessDate(myDate), Microsoft.VisualBasic.FirstDayOfWeek.Sunday)).ToString.Trim)
                    Return (BusinessDate(myDate).DayOfWeek.ToString.Trim)

                Case "CURRENTWEEKSTART"
                    'Returns the first date of the current week, based in the passed "first weekday" number.
                    'Syntax is $TOKENNAME,<firstdayofweek[0-7]>$ (0=UseRegionalSettings, 1-7=Sun-Sat respectively)
                    Return (Microsoft.VisualBasic.DateAdd(Microsoft.VisualBasic.DateInterval.Day, -Microsoft.VisualBasic.Weekday(myDate, CInt(parameter)) + 1, myDate).ToString("yyyyMMdd"))

                Case "DAYOFWEEKS"
                    Return (Microsoft.VisualBasic.WeekdayName(Microsoft.VisualBasic.Weekday(myDate), True, Microsoft.VisualBasic.FirstDayOfWeek.Sunday))

                Case "DAYOFWEEKN"
                    'Return Trim(Str(Weekday((myDate), FirstDayOfWeek.Sunday)))
                    Return (myDate.DayOfWeek.ToString.Trim)

                Case "DAYSFROMBUSINESSDATE"
                    'Returns BusinessDate + X days (days can be negative). Token syntax is $TOKENNAME,DAYS|FORMAT$
                    'Return DateAdd(DateInterval.Day, Val(parameter), BusinessDate(myDate)).ToString("yyyyMMdd")
                    Return (BusinessDate(myDate).AddDays(CInt(parameter)).ToString("yyyyMMdd"))

                Case "DAYSFROMTODAY"
                    'Returns Now + X days (days can be negative). Token syntax is $TOKENNAME,DAYS|FORMAT$
                    'Return DateAdd(DateInterval.Day, Val(parameter), myDate).ToString("yyyyMMdd")
                    Return (myDate.AddDays(CInt(parameter)).ToString("yyyyMMdd"))

                Case "LASTBUSINESSWEEK"
                    'Returns BusinessDate - 7
                    'Return DateAdd(DateInterval.Day, -7, BusinessDate(myDate)).ToString("yyyyMMdd")
                    Return (BusinessDate(myDate).AddDays(-7).ToString("yyyyMMdd"))

                Case "LASTMONTH"
                    'Returns the FIRST of LAST month
                    Dim auxDate As Date
                    'dAux = DateSerial(Year(myDate), Month(myDate), 1)
                    'dAux = DateAdd(DateInterval.Day, -1, dAux)
                    'dAux = DateSerial(Year(dAux), Month(dAux), 1)
                    'Return auxDate.ToString("yyMM")
                    auxDate = New Date(myDate.Year, myDate.Month, 1).AddDays(-1)
                    Return (New Date(auxDate.Year, auxDate.Month, 1).ToShortDateString("yyMM"))

                Case "LASTWEEK"
                    'Returns Now - 7
                    'Return DateAdd(DateInterval.Day, -7, myDate).ToString("yyyyMMdd")
                    Return (myDate.AddDays(-7).ToString("yyyyMMdd"))

                Case "LOCAL", "STORE", "STOREId"
                    Return (IniGet("TaskMan",
                                       "Local",
                                       "__",
                                       My.Application.GetEnvironmentVariable("NW") & SCARD_INI
                                       ).Trim.ToUpper
                                )

                Case "PROGRAM"
                    Return My.Application.Info.ProductName

                Case "SERVER", "SERVERNAME"
                    'Returns just the \\SERVER part of %NW%. Does NOT obey command line alterations!
                    Dim sAux As String = System.Environment.ExpandEnvironmentVariables("%NW%")
                    If (sAux.IndexOf("\\") = 0) Then
                        'We have a URL
                        Return (sAux.Substring(0, sAux.IndexOf("\", 2)))
                    ElseIf (sAux.IndexOf(":") = 1) Then
                        'We have a Drive
                        Return (sAux.Substring(0, sAux.IndexOf("\")))
                    Else
                        Return System.String.Empty
                    End If

                Case "STORENAME"
                    Return (IniGet("TaskMan",
                                       "StoreName",
                                       System.String.Empty,
                                       My.Application.GetEnvironmentVariable("NW") & SCARD_INI
                                      ).Trim
                               )

                Case "VERSION"
                    Return My.Application.Info.Version.ToString

                Case Else
                    Return macroName

            End Select

        End If

    End Function

    Friend Function PrevInstance() As Boolean

        Return System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess.ProcessName).GetUpperBound(0) > 0

    End Function

    'Private Function SubstEVars(ByVal stringToExpand As String) As String
    '    'Replaces EVars in sToExpand

    '    Dim retVal As String = stringToExpand,
    '        ptrFrom As Integer,
    '        ptrUpTo As Integer,
    '        envVar As String

    '    ptrFrom = InStr(retVal, "%")

    '    Do Until ptrFrom = 0

    '        ptrUpTo = InStr(ptrFrom + 1, retVal, "%")

    '        If (ptrUpTo > (ptrFrom + 1)) Then

    '            envVar = retVal.Substring(ptrFrom + 1, ptrUpTo - ptrFrom - 1).Trim.ToUpper 'UCase(Trim(MId(retVal, ptrFrom + 1, ptrUpTo - ptrFrom - 1)))

    '            If (Len(envVar) > 0) Then

    '                Try
    '                    retVal = Left(retVal, ptrFrom - 1) &
    '                             My.Application.GetEnvironmentVariable(envVar) &
    '                             retVal.Substring(ptrUpTo + 1) 'MId(retVal, ptrUpTo + 1)
    '                Catch Ex As Exception
    '                    'Just ignore the error (EVar not found, mostly), and move on
    '                    ptrFrom += 1
    '                End Try

    '            Else
    '                ptrFrom += 1
    '            End If

    '        Else
    '            ptrFrom += 1
    '        End If

    '        ptrFrom = retVal.IndexOf("%", ptrFrom) 'InStr(ptrFrom, retVal, "%")

    '    Loop

    '    Return retVal

    'End Function

    Private Function SubstMacros(ByVal stringToExpand As String,
                                 Optional ByVal targetDate As Date = Nothing
                                ) As String
        'Replaces DCS Macros in sToExpand

        Dim retVal As String = stringToExpand,
            ptrFrom As Integer = retVal.IndexOf("$"),
            ptrUpTo As Integer = 0,
            macroName As String = System.String.Empty,
            macroValue As String = System.String.Empty

        Do Until (ptrFrom = -1)

            ptrUpTo = retVal.IndexOf("$", ptrFrom + 1)

            If (ptrUpTo > ptrFrom + 1) Then

                macroName = retVal.Substring(ptrFrom + 1, ptrUpTo - ptrFrom - 1).Trim

                If (macroName.Length > 0) Then

                    macroValue = GetMacroValue(macroName, , targetDate)

                    If (macroValue <> macroName) Then

                        retVal = retVal.Substring(0, ptrFrom - 1) &
                                 macroValue &
                                 retVal.Substring(ptrUpTo + 1)

                    End If

                Else
                    ptrFrom += 1
                End If

            Else
                ptrFrom += 1
            End If

            ptrFrom = retVal.IndexOf("$", ptrFrom)

        Loop

        Return retVal

    End Function

    Friend Function HardEncrypt(ByVal plainText As String,
                                Optional ByVal keySeed As Long = &H8000,
                                Optional ByVal keyText As String = ENCRYPTION_KEY
                               ) As String

        Dim retVal As String = System.String.Empty,
            keyed() As Integer,
            sw As Long,
            strLen As Long

        Try

            'Prepare crap
            strLen = plainText.Length
            ReDim keyed(strLen)

            'Preset RND seed so we get always the same "random" sequence
            sw = CLng(Microsoft.VisualBasic.Rnd(-1))
            Microsoft.VisualBasic.Randomize(keySeed)

            For sw = 1 To strLen

                'Add key char-by-char
                keyed(sw) = Microsoft.VisualBasic.Asc(Microsoft.VisualBasic.Mid(plainText, sw, 1)) + Microsoft.VisualBasic.Asc(Microsoft.VisualBasic.Mid(keyText, sw Mod keyText.Length + 1, 1))

                'Add randomness to every character, and make sure the result is between 0 and 255
                keyed(sw) = keyed(sw) + CInt(Microsoft.VisualBasic.Rnd() * 256) + 1
                If (keyed(sw) > 255) Then
                    keyed(sw) = keyed(sw) - 256
                End If

                'Convert the result array to HEX values (good to put as string in an INI), but RIGHT TO LEFT!
                retVal = Microsoft.VisualBasic.Hex(keyed(sw)).PadLeft(2, "0") & retVal 'Right("00" & Hex(keyed(sw)), 2) & retVal

            Next sw

        Catch ex As System.Exception

            Throw

        End Try

        Return retVal

    End Function

    Friend Function HardDecrypt(ByVal cypherText As String,
                                Optional ByVal keySeed As Long = &H8000,
                                Optional ByVal keyText As String = ENCRYPTION_KEY
                               ) As String

        Dim retVal As String = System.String.Empty,
            keyed() As Integer,
            sw As Long,
            strLen As Long

        Try

            strLen = cypherText.Length / 2
            ReDim keyed(strLen)

            'Preset RND seed so we get always the same "random" sequence
            sw = CLng(Microsoft.VisualBasic.Rnd(-1))
            Microsoft.VisualBasic.Randomize(keySeed)

            For sw = 1 To strLen

                'Convert hex to int (right to left!)
                keyed(sw) = CInt("&H" & cypherText.Substring(cypherText.Length - sw * 2, 2)) 'MId(cypherText, Len(cypherText) - sw * 2 + 1, 2))

                'Deduct random number, and if result is < 0, add 256
                keyed(sw) = keyed(sw) - CInt(Microsoft.VisualBasic.Rnd() * 256) - 1
                If (keyed(sw) < 0) Then
                    keyed(sw) = keyed(sw) + 256
                End If

                'Deduct the key from the array
                keyed(sw) = keyed(sw) - Microsoft.VisualBasic.Asc(keyText.Substring(sw Mod keyText.Length, 1)) 'Asc(MId(keyText, sw Mod Len(keyText) + 1, 1))
                If (keyed(sw) < 0) Then
                    keyed(sw) = keyed(sw) + 256
                End If

                retVal &= Microsoft.VisualBasic.Chr(keyed(sw))

            Next sw

        Catch ex As System.Exception

            Throw

        End Try

        Return retVal

    End Function

    'Friend Function GetColor(ByVal colorNameOrRGB As String) As System.Drawing.Color

    '    If (IsNumeric(colorNameOrRGB)) Then
    '        'It's a specific '#RRGGBB' color (must take Alpha into consIderation)
    '        Return System.Drawing.Color.FromArgb(255, System.Drawing.ColorTranslator.FromHtml(colorNameOrRGB))
    '    Else
    '        'Should be a color name
    '        Return System.Drawing.Color.FromName(colorNameOrRGB)
    '    End If

    'End Function

    Friend Sub TaskbarAutohIde(Optional ByVal makeItAutoHIde As Boolean = True)

        Dim abd As uAppbarData

        abd.cbSize = 36
        SHAppBarMessage(enuTaskbarMessages.tm_abm_GetState, abd)

        If (makeItAutoHIde) Then
            'Pls note: Win7 & up make AlwaysOnTop obsolete by forcing it to TRUE.
            abd.lParam = (enuTaskbarMessages.tm_abs_AutoHIde Or enuTaskbarMessages.tm_abs_OnTop)
        Else
            'Pls note: Win7 & up make AlwaysOnTop obsolete by forcing it to TRUE.
            abd.lParam = (enuTaskbarMessages.tm_abs_OnTop)
        End If

        SHAppBarMessage(enuTaskbarMessages.tm_abm_SetState, abd)

    End Sub

    Friend Sub ManageDisplayPowerSave(ByVal disablePowerSaving As Boolean)

        If (disablePowerSaving) Then
            Call SetThreadExecutionState(enuExecutionState.es_Continuous Or enuExecutionState.es_DisplayRequired Or enuExecutionState.es_SystemRequired)
        Else
            Call SetThreadExecutionState(enuExecutionState.es_Continuous)
        End If

    End Sub

    Friend Function CheckControlFile(ByVal fqfn As String,
                                     ByVal resetActionFlag As Boolean,
                                     ByRef filePresent As Boolean
                                    ) As Boolean

        Dim retVal As Boolean = False,
            cpuName As String = System.Environment.GetEnvironmentVariable("ComputerName")

        'Check whether the file is present at all - This does not seem to raise 
        'exceptions in immediate pane even with paths purposedly mangled.
        fqfn = ExpandTokens(fqfn)
        filePresent = System.IO.File.Exists(fqfn)

        If (filePresent) Then

            Dim fileContents As String = System.IO.File.ReadAllText(fqfn)

            'Return value means this computer's name in mentioned in the file.
            retVal = (fileContents.IndexOf(cpuName) >= 0)

            'Now check whether we have to reset the action flag by deleting
            'the computer name from the control file.
            If (resetActionFlag) Then
                Dim lockObject As New Object
                fileContents.Replace(cpuName, System.String.Empty)
                SyncLock lockObject
                    System.IO.File.WriteAllText(fqfn, fileContents)
                End SyncLock
            End If

        End If

        'Done!
        Return (retVal)

    End Function

End Module