Imports DCS.KioskV15.ClubSpeedInterface.Models

Namespace Presenters

    Public Class RaceView

        'Implements System.ComponentModel.INotifyPropertyChanged
        Implements System.IEquatable(Of RaceView)

        'Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property Id As Integer = -1

        Public Property DateScheduled As Date = New Date(1900, 1, 1)

        Public Property Name As String = String.Empty

        Public Property Selected As Boolean = False

        Public Property NumberOfReservation As Integer = -1

        Public ReadOnly Property HeatDetailsCount As Integer '= -1
            Get
                If (HeatDetails Is Nothing) Then
                    Return (0)
                End If
                Return (HeatDetails.Count)
            End Get
        End Property

        Public Property TotalSeats As Integer = -1

        Public Property HeatDetails As IList(Of HeatDetailView) = New List(Of HeatDetailView)

        Public ReadOnly Property HasRacerID(ByVal racerID As Integer) As Boolean
            Get
                If (Me.HeatDetails.Count = 0) Then
                    Return (False)
                End If
                'Return (Me.HeatDetails.Where(Function(ByVal racer As Models.HeatDetail)
                '                                 Return (racer.CustomerId = racerID)
                '                             End Function
                '                            ) IsNot Nothing
                '       )
                'Return (Me.HeatDetails.Select(Function(myHeatDetail As Models.HeatDetail)
                '                                  If (myHeatDetail.CustomerId = racerID) Then
                '                                      Return (myHeatDetail)
                '                                  Else
                '                                      Return (Nothing)
                '                                  End If
                '                              End Function
                '                             ).Count > 0
                '       )

                'Ok, either LINQ does not work, or... I forgot how to use it again, 
                'even after googling. Back to the brute-force-looping method.
                Dim retVal As Boolean = False

                For Each racer In HeatDetails
                    If (racer.CustomerId = racerID) Then
                        retVal = True
                        Exit For
                    End If
                Next

                Return (retVal)

            End Get
        End Property

        Friend ReadOnly Property AvailableSeats As Integer
            Get
                If ((Me.TotalSeats <> -1) And (Me.NumberOfReservation <> -1) And (Me.HeatDetailsCount <> -1)) Then
                    Dim availSeats = Me.TotalSeats - Me.NumberOfReservation - Me.HeatDetailsCount
                    Return If(availSeats < 0, 0, availSeats)
                Else
                    Return (0)
                End If
            End Get
        End Property

        Public Shadows Function Equals(ByVal other As RaceView) As Boolean Implements IEquatable(Of RaceView).Equals
            Return (other.Id = Me.Id)
        End Function

        'Public ReadOnly Property BackColor As SolIdColorBrush
        '    Get
        '        Return (New SolIdColorBrush(If(Me.Selected, Colors.Beige, Colors.Transparent)))
        '    End Get
        'End Property

    End Class

End Namespace