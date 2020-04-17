Public Class RaceItemButton

    Public Event ButtonClick()

    Property RaceId As Integer

    Private _raceName As String = ""
    Private _raceDate As Date = "jan 1 1900 00:00"
    Private _freeSeats As Integer = 0

    Private _idle As Boolean = True

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        With Me
            .SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            .SetStyle(ControlStyles.UserPaint, True)
            .SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            .UpdateStyles()
        End With

    End Sub

    Overloads Property Enabled As Boolean
        Get
            Return (MyBase.Enabled)
        End Get
        Set(ByVal newState As Boolean)
            'If (newState <> MyBase.Enabled) Then
            '    Me.BackgroundImage = If(newState,
            '                            My.Resources.RaceItemButtonPicture_Enabled,
            '                            My.Resources.RaceItemButtonPicture_Disabled
            '                           )
            'End If
            MyBase.Enabled = newState
        End Set
    End Property

    Property FreeSeats As Integer
        Get
            Return (_freeSeats)
        End Get
        Set(ByVal newSeatQty As Integer)
            _freeSeats = newSeatQty
            RefreshText()
        End Set
    End Property

    Property RaceDate As Date
        Get
            Return (_raceDate)
        End Get
        Set(ByVal newDate As Date)
            _raceDate = newDate
            RefreshText()
        End Set
    End Property

    Friend Property RaceName As String
        Get
            Return (_raceName)
        End Get
        Set(ByVal newName As String)
            _raceName = newName
            RefreshText()
        End Set
    End Property

    Private Sub RefreshText()

        lblRaceDate.Text = Format(RaceDate, "HH:mm")
        lblRaceName.Text = _raceName
        lblAvailSeats.Text = _freeSeats & " free seats"

    End Sub

    'Private Sub Clicked()

    '    If (_idle) Then

    '        _idle = False
    '        RaiseEvent ButtonClick()
    '        _idle = True

    '    End If

    'End Sub

    Private Sub RaceItemButton_Click(ByVal sender As Object,
                                     ByVal e As EventArgs
                                    ) Handles Me.Click

        'Clicked()
        RaiseEvent ButtonClick()

    End Sub

    'Private Sub lblAvailSeats_Click(ByVal sender As Object,
    '                                ByVal e As EventArgs
    '                               ) Handles lblAvailSeats.Click


    '    'Clicked()
    '    RaceItemButton_Click(Me, Nothing)

    'End Sub

    'Private Sub lblRaceDate_Click(ByVal sender As Object,
    '                              ByVal e As EventArgs
    '                             ) Handles lblRaceDate.Click

    '    'Clicked()
    '    RaceItemButton_Click(Me, Nothing)

    'End Sub

    'Private Sub lblRaceName_Click(ByVal sender As Object,
    '                              ByVal e As EventArgs
    '                             ) Handles lblRaceName.Click

    '    'Clicked()
    '    RaceItemButton_Click(Me, Nothing)

    'End Sub

End Class
