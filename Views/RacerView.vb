Namespace Views

    Public Class RacerView

        Implements IEquatable(Of RacerView)

        Public Property RacerId As Long

        Public Property FirstName As String

        Public Property LastName As String

        Private Sub New()

        End Sub

        Public ReadOnly Property FullName As String
            Get

                Dim retVal As String = ""

                If (Not (String.IsNullOrWhiteSpace(FirstName))) Then
                    retVal = FirstName
                End If

                If (Not (String.IsNullOrWhiteSpace(LastName))) Then
                    If (retVal <> "") Then
                        retVal &= " " & LastName
                    Else
                        retVal = LastName
                    End If
                End If

                Return retVal

            End Get
        End Property

        Public Sub New(ByVal racerId As Long,
                       ByVal firstName As String,
                       ByVal lastName As String
                      )

            _RacerId = racerId

        End Sub

        Public Overloads Function Equals(other As RacerView) As Boolean Implements IEquatable(Of RacerView).Equals
            Return Me.RacerId = other.RacerId
        End Function

    End Class

End Namespace
