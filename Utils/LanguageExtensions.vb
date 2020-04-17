Imports DCS.KioskV15.ClubSpeedInterface.Views

Namespace Utils
    Module LanguageExtensions




        <System.Runtime.CompilerServices.Extension()>
        Public Function Between(ByVal value As System.Int16,
                                ByVal lowerBound As System.Int16,
                                ByVal upperBound As System.Int16,
                                Optional ByVal includeBounds As Boolean = True
                                ) As Boolean

            If (includeBounds) Then
                Return ((value >= lowerBound) And (value <= upperBound))
            Else
                Return ((value > lowerBound) And (value < upperBound))
            End If
        End Function

        <System.Runtime.CompilerServices.Extension()>
        Public Function Between(ByVal value As System.Int32,
                                ByVal lowerBound As System.Int32,
                                ByVal upperBound As System.Int32,
                                Optional ByVal includeBounds As Boolean = True
                                ) As Boolean

            If (includeBounds) Then
                Return ((value >= lowerBound) And (value <= upperBound))
            Else
                Return ((value > lowerBound) And (value < upperBound))
            End If

        End Function

        <System.Runtime.CompilerServices.Extension()>
        Public Function Between(ByVal value As System.Int64,
                                ByVal lowerBound As System.Int64,
                                ByVal upperBound As System.Int64,
                                Optional ByVal includeBounds As Boolean = True
                                ) As Boolean

            If (includeBounds) Then
                Return ((value >= lowerBound) And (value <= upperBound))
            Else
                Return ((value > lowerBound) And (value < upperBound))
            End If

        End Function

        <System.Runtime.CompilerServices.Extension()>
        Public Function Between(ByVal value As System.Single,
                                ByVal lowerBound As System.Single,
                                ByVal upperBound As System.Single,
                                Optional ByVal includeBounds As Boolean = True
                                ) As Boolean

            If (includeBounds) Then
                Return ((value >= lowerBound) And (value <= upperBound))
            Else
                Return ((value > lowerBound) And (value < upperBound))
            End If

        End Function

        <System.Runtime.CompilerServices.Extension()>
        Public Function Between(ByVal value As System.Double,
                                ByVal lowerBound As System.Double,
                                ByVal upperBound As System.Double,
                                Optional ByVal includeBounds As Boolean = True
                                ) As Boolean

            If (includeBounds) Then
                Return ((value >= lowerBound) And (value <= upperBound))
            Else
                Return ((value > lowerBound) And (value < upperBound))
            End If

        End Function

        <System.Runtime.CompilerServices.Extension()>
        Public Function Between(ByVal value As System.Decimal,
                                ByVal lowerBound As System.Decimal,
                                ByVal upperBound As System.Decimal,
                                Optional ByVal includeBounds As Boolean = True
                                ) As Boolean

            If (includeBounds) Then
                Return ((value >= lowerBound) And (value <= upperBound))
            Else
                Return ((value > lowerBound) And (value < upperBound))
            End If

        End Function

        <System.Runtime.CompilerServices.Extension()>
        Public Function Between(ByVal value As System.Char,
                                ByVal lowerBound As System.Char,
                                ByVal upperBound As System.Char,
                                Optional ByVal includeBounds As Boolean = True
                                ) As Boolean

            If (includeBounds) Then
                Return ((value >= lowerBound) And (value <= upperBound))
            Else
                Return ((value > lowerBound) And (value < upperBound))
            End If

        End Function

        <System.Runtime.CompilerServices.Extension()>
        Public Function Between(ByVal value As System.String,
                                ByVal lowerBound As System.String,
                                ByVal upperBound As System.String,
                                Optional ByVal includeBounds As Boolean = True
                                ) As Boolean

            If (includeBounds) Then
                Return ((value >= lowerBound) And (value <= upperBound))
            Else
                Return ((value > lowerBound) And (value < upperBound))
            End If

        End Function

        <System.Runtime.CompilerServices.Extension()>
        Friend Sub [Reset](ByRef value As Boolean)
            value = (False)
        End Sub

        <System.Runtime.CompilerServices.Extension()>
        Friend Sub [Set](ByRef value As Boolean)
            value = (True)
        End Sub

        <System.Runtime.CompilerServices.Extension()>
        Friend Sub Toggle(ByRef value As Boolean)
            value = (Not (value))
        End Sub

        <System.Runtime.CompilerServices.Extension>
        Friend Function Split(ByVal stringWithDelimiters As System.String,
                              ByVal splitDelimiter As System.String
                              ) As System.String()

            'Return (System.Text.RegularExpressions.Regex.Split(stringWithDelimiters, splitDelimiter))
            Return (stringWithDelimiters.Split(New System.String() {splitDelimiter}, System.StringSplitOptions.None))
        End Function
    End Module
End Namespace