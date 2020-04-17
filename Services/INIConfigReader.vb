Namespace Services

    Public Class INIConfigReader

        Implements IConfigReader

        Private _iniFQFN As String = ""

        Public Sub New(ByVal primaryINIFileName As String,
                       Optional ByVal secondaryIniFileName As String = ""
                      )

            If (secondaryIniFileName.Trim.Length <> 0) Then

                'Check secondary ini is present; if it is, use it.
                If (Not (IO.File.Exists(secondaryIniFileName))) Then
                    Throw New ArgumentException($"Secondary INI file name '{secondaryIniFileName}' is invalid.")
                End If

                _iniFQFN = secondaryIniFileName

            Else

                'Check primary ini is present and valid; if it is, use it. Otherwise, explode with error.
                If (primaryINIFileName.Trim.Length = 0) Then
                    Throw New ArgumentException($"Primary INI file name cannot be null.")
                End If
                If (Not (IO.File.Exists(primaryINIFileName))) Then
                    Throw New ArgumentException($"Primary INI file name '{primaryINIFileName}' is invalid.")
                End If

                _iniFQFN = primaryINIFileName

            End If

        End Sub

        Public Function GetNumber(ByVal section As String,
                                  ByVal key As String,
                                  ByVal defaultValue As Double
                                 ) As Double Implements IConfigReader.GetNumber

            Dim retVal As Double = GetString(section, key, defaultValue.ToString) 'Using CDbl is discouraged by the IDE.

            Return (retVal)

        End Function

        Public Function GetNumber(ByVal section As String,
                                  ByVal key As String,
                                  ByVal defaultValue As Single
                                 ) As Single Implements IConfigReader.GetNumber

            Dim retVal As Single = GetString(section, key, defaultValue.ToString)  'Using CSng is discouraged by the IDE.

            Return (retVal)

        End Function

        Public Function GetNumber(ByVal section As String,
                                  ByVal key As String,
                                  ByVal defaultValue As Decimal
                                 ) As Decimal Implements IConfigReader.GetNumber

            Dim retVal As Integer = CDec(GetString(section, key, defaultValue.ToString))

            Return (retVal)

        End Function

        Public Function GetNumber(ByVal section As String,
                                  ByVal key As String,
                                  ByVal defaultValue As Integer
                                 ) As Integer Implements IConfigReader.GetNumber

            Dim retVal As Integer = CInt(GetString(section, key, defaultValue.ToString))  'Using CInt is discouraged by the IDE.

            Return (retVal)

        End Function

        Public Function GetString(ByVal section As String,
                                  ByVal key As String,
                                  ByVal defaultValue As String
                                 ) As String Implements IConfigReader.GetString

            Dim buffer As String = New String(Chr(0), 65535),
                charsRead As Integer

            charsRead = GetPrivateProfileString(section, key, defaultValue, buffer, buffer.Length - 1, _iniFQFN)

            Return (buffer.Substring(0, charsRead))

        End Function

        Public Function GetToggle(ByVal section As String,
                                  ByVal key As String,
                                  ByVal defaultValue As Boolean
                                 ) As Boolean Implements IConfigReader.GetToggle

            Dim readValue As String = GetString(section, key, defaultValue.ToString)

            Return (readValue.Trim.ToUpper.StartsWith("Y") Or    'Cater for "YES/NO"
                    readValue.Trim.ToUpper.StartsWith("T") Or    'Cater for "TRUE/FALSE"
                    readValue.Trim.ToUpper.Equals("ON") Or       'Cater for "ON/OFF"
                    (Val(readValue.Trim) <> 0)                   'Cater for 0 (false) or any other numeric value (true)
                   )

        End Function

        Public Function GetColor(ByVal section As String,
                                 ByVal key As String,
                                 ByVal defaultValue As String
                                ) As Color Implements IConfigReader.GetColor

            'This routine returns a valId color from a INI key value as follows (supports ONLY hex numbers and color names):
            '  [<iniSection>]
            '  <iniKey> = &HE08040        'Some color with Red=&HE0, Green=&H80 and Blue=&H40 - Should use Alpha=&HFF
            '  <iniKey> = &HC0E08040      'Same as first, but includes ALPHA channel at value &HC0
            '  <iniKey> = Transparent     'Yup, it also supports standard .Net color names. Amazing.

            Dim retVal As System.Drawing.Color = Nothing,
            aux As String = System.String.Empty,
            auxVal As Integer = 0,
            alpha As Byte = &HFF

            aux = GetString(section, key, defaultValue).Trim
            If (IsNumeric(aux)) Then

                'It's a specific '&HRRGGBB' or '&HAARRGGBB' color
                If (aux.Length > 8) Then
                    'Has alpha
                    alpha = CByte(aux.Substring(0, 4))
                    aux = "&H" & aux.Substring(4)
                End If
                Return (Color.FromArgb(alpha, ColorTranslator.FromHtml(aux)))

            Else

                'Should be a color name
                Return (Color.FromName(aux))

            End If

        End Function

    End Class

End Namespace
