'Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports DCS.KioskV15.ClubSpeedInterface.Presenters
'Imports DCS.KioskV15.ClubSpeedInterface.Utils
Imports DCS.KioskV15.ClubSpeedInterface.Views

Public Class PickRace_v2View

    Implements Views.IPickRaceView

    Public Event CanSelectRace(ByVal race As RaceView, ByRef raceIsSelectable As Boolean) Implements IPickRaceView.CanSelectRace
    Public Event GetRaces(ByRef races As IList(Of RaceView)) Implements IPickRaceView.GetRaces
    Public Event RaceSelected(ByVal race As RaceView) Implements IPickRaceView.RaceSelected
    Public Event RaceSelectedById(ByVal raceId As Integer) Implements IPickRaceView.RaceSelectedById
    Public Event SelectionCanceled() Implements IPickRaceView.SelectionCanceled

    Private _raceList As IList(Of RaceView),
            _progressDialog As frmProgress,
            _chosenRaceID As Integer

    Private WithEvents _backgroundWorker As New BackgroundWorker

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub _backgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles _backgroundWorker.DoWork

        _progressDialog.TaskStarted("Loading race list...")

    End Sub

    Public Sub LoadingContents() Implements IPickRaceView.LoadingContents

        If ((Me._progressDialog Is Nothing) OrElse
            (Me._progressDialog.IsDisposed())
           ) Then

            Me._progressDialog = New frmProgress

        End If

        _progressDialog.TaskStarted("Loading race list...")

    End Sub

    Public Sub ShowView(raceList As IList(Of RaceView)) Implements IPickRaceView.ShowView

        If (raceList Is Nothing) Then
            Throw New ArgumentNullException("raceList")
        End If

        _raceList = raceList
        _chosenRaceID = 0
        Me.cmdOK.Enabled = False
        Me.lblChosenRace.Text = String.Empty
        pnlRaceButtons.Controls.Clear()

        If (_raceList.Count = 0) Then

            'No real need to throw an ugly exception; we can just put up a "No
            'races found" message along with an "OK" button and quit, right?
            'Throw New ArgumentException("raceList is empty", "raceList")

            'Dim lblNoRaces As New Label With {
            '                                     .AutoSize = False,
            '                                     .Font = lblSelectRace.Font,
            '                                     .Height = lblSelectRace.Height,
            '                                     .Text = "Sorry, No Races Available - Please Try Again Later",
            '                                     .TextAlign = ContentAlignment.MiddleCenter,
            '                                     .Width = pnlRaceButtons.DisplayRectangle.Width - pnlRaceButtons.Margin.Horizontal
            '                                 },
            '    cmdDismiss As New Button With {
            '                                     .AutoSize = True,
            '                                     .Font = lblSelectRace.Font,
            '                                     .Text = "Dismiss",
            '                                     .TextAlign = ContentAlignment.MiddleCenter,
            '                                     .Width = pnlRaceButtons.DisplayRectangle.Width - pnlRaceButtons.Margin.Horizontal
            '                                  }

            'AddHandler cmdDismiss.Click, AddressOf cmdDismiss_Click

            'pnlRaceButtons.FlowDirection = FlowDirection.TopDown

            'pnlRaceButtons.Controls.Add(lblNoRaces)
            'pnlRaceButtons.Controls.Add(cmdDismiss)

            lblChosenRace.Text = "Sorry, No Races Available - Please Try Again Later"

        Else

            pnlRaceButtons.FlowDirection = FlowDirection.LeftToRight

            For Each race In _raceList

                Dim newButton = New RaceItemButton With
                {
                    .RaceId = race.Id,
                    .RaceDate = race.DateScheduled,
                    .RaceName = race.Name,
                    .FreeSeats = race.AvailableSeats
                }

                AddHandler newButton.Click, AddressOf RaceButton_Click

                RaiseEvent CanSelectRace(race, newButton.Enabled)

                pnlRaceButtons.Controls.Add(newButton)

            Next

        End If

        _progressDialog.TaskFinished()

        ShowDialog()

    End Sub

    Private Sub RaceButton_Click(ByVal sender As Object,
                                 ByVal e As EventArgs
                                )

        _chosenRaceID = CType(sender, RaceItemButton).RaceId
        Dim race As RaceView = _raceList.First(Function(x)
                                                   Return (x.Id = _chosenRaceID)
                                               End Function
                                              )

        Me.lblChosenRace.Text = $"Race {race.Name}, starts at {race.DateScheduled.ToString("HH\:mm")}, has {race.AvailableSeats.ToString()} free seats of {race.TotalSeats.ToString()} total."
        Me.cmdOK.Enabled = True

        'RaiseEvent RaceSelectedById(raceID)
        'Close()

    End Sub

    'Private Sub cmdDismiss_Click(ByVal sender As Object,
    '                             ByVal e As EventArgs
    '                            )

    '    RaiseEvent SelectionCanceled()
    '    Close()

    'End Sub

    Private Sub PickRace_v2View_Load(sender As Object, e As EventArgs) Handles Me.Load

        With Me
            .SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            .SetStyle(ControlStyles.UserPaint, True)
            .SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            .UpdateStyles()
        End With

    End Sub

    Private Sub cmdOK_Click(ByVal sender As Object,
                            ByVal e As EventArgs
                           ) Handles cmdOK.Click

        RaiseEvent RaceSelectedById(_chosenRaceID)
        Close()

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object,
                                ByVal e As EventArgs
                               ) Handles cmdCancel.Click

        RaiseEvent SelectionCanceled()
        Close()

    End Sub

End Class