Namespace Services

    Public Interface IConfigReader

        Function GetNumber(ByVal section As String,
                           ByVal key As String,
                           ByVal defaultValue As Integer
                          ) As Integer

        Function GetNumber(ByVal section As String,
                           ByVal key As String,
                           ByVal defaultValue As Decimal
                          ) As Decimal

        Function GetNumber(ByVal section As String,
                           ByVal key As String,
                           ByVal defaultValue As Single
                          ) As Single

        Function GetNumber(ByVal section As String,
                           ByVal key As String,
                           ByVal defaultValue As Double
                          ) As Double

        Function GetString(ByVal section As String,
                           ByVal key As String,
                           ByVal defaultValue As String
                          ) As String

        Function GetToggle(ByVal section As String,
                           ByVal key As String,
                           ByVal defaultValue As Boolean
                          ) As Boolean

        Function GetColor(ByVal section As String,
                          ByVal key As String,
                          ByVal defaultValue As String
                         ) As Color

    End Interface

End Namespace