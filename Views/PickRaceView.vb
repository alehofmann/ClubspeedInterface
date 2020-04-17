Imports System.Collections.ObjectModel
Imports DCS.KioskV15.ClubSpeedInterface.Presenters
Imports DCS.KioskV15.ClubSpeedInterface.Services

Namespace Views

    Public Class PickRaceView

        Implements IPickRaceView

        Public Event CanSelectRace(race As RaceView, ByRef raceIsSelectable As Boolean) Implements IPickRaceView.CanSelectRace
        Public Event GetRaces(ByRef races As IList(Of RaceView)) Implements IPickRaceView.GetRaces
        Public Event RaceSelected(race As RaceView) Implements IPickRaceView.RaceSelected
        Public Event SelectionCanceled() Implements IPickRaceView.SelectionCanceled
        Public Event RaceSelectedById(raceId As Integer) Implements IPickRaceView.RaceSelectedById
        Private _raceList As IList(Of RaceView),
                _raceIsOK As Boolean = False,
                _chosenRace As RaceView = Nothing

        Public Property Notifier As IPopupNotifier = Nothing

        Public Sub ShowView(raceList As IList(Of RaceView)) Implements IPickRaceView.ShowView

            ButtonCancel.Enabled = False
            ButtonOK.Enabled = False
            ShowDialog()

        End Sub

        Private Sub PickRaceView_Shown(ByVal sender As Object,
                                       ByVal e As EventArgs
                                      ) Handles Me.Shown

            Dim index As Integer = 0

            RaiseEvent GetRaces(_raceList)

            For Each race As RaceView In _raceList

                Dim itmRace As New ListViewItem("HeatMain #" & race.Id.ToString &
                                                ": '" & race.Name &
                                                "' (" & race.AvailableSeats.ToString() & " free seats), " &
                                                "starts at " & race.DateScheduled.ToString("MMM\ dd\,\ HH\:mm\.")
                                               )

                itmRace.Tag = index
                index += 1

                lvwRaces.Items.Add(itmRace)

            Next

            ButtonCancel.Enabled = True

        End Sub

        Private Sub ButtonOK_Click(ByVal sender As Object,
                                   ByVal e As EventArgs
                                  ) Handles ButtonOK.Click

            If (_chosenRace IsNot Nothing) Then

                RaiseEvent RaceSelected(_chosenRace)
                lvwRaces.Items.Clear()
                Me.Close()

            Else

                Notifier.Notify("One of the chosen racers is already inscript in this race.")

            End If

        End Sub

        Private Sub cmdUp_Click(ByVal sender As Object,
                                ByVal e As EventArgs
                               ) Handles cmdUp.Click

            If (lvwRaces.SelectedIndices.Count = 0) Then
                'Select first
                lvwRaces.SelectedIndices.Clear()
                lvwRaces.SelectedIndices.Add(lvwRaces.Items.Count - 1)
            Else
                'Scroll one up if possible
                Dim currIndex As Integer = lvwRaces.SelectedIndices(0)
                If (currIndex > 0) Then
                    lvwRaces.SelectedIndices.Clear()
                    lvwRaces.SelectedIndices.Add(currIndex - 1)
                End If
            End If

            lvwRaces.Focus()

        End Sub

        Private Sub cmdDown_Click(ByVal sender As Object,
                                  ByVal e As EventArgs
                                 ) Handles cmdDown.Click

            If (lvwRaces.SelectedIndices.Count = 0) Then
                'Select first
                lvwRaces.SelectedIndices.Clear()
                lvwRaces.SelectedIndices.Add(0)
            Else
                'Scroll one up if possible
                Dim currIndex As Integer = lvwRaces.SelectedIndices(0)
                If (currIndex < (lvwRaces.Items.Count - 1)) Then
                    lvwRaces.SelectedIndices.Clear()
                    lvwRaces.SelectedIndices.Add(currIndex + 1)
                End If
            End If

            lvwRaces.Focus()

        End Sub

        Private Sub ListRaces_SelectedIndexChanged(ByVal sender As Object,
                                                   ByVal e As EventArgs
                                                  ) Handles lvwRaces.SelectedIndexChanged

            If (lvwRaces.SelectedItems.Count = 1) Then

                Dim race As RaceView = _raceList(lvwRaces.SelectedItems(0).Tag)

                RaiseEvent CanSelectRace(race, _raceIsOK)
                If (_raceIsOK) Then
                    _chosenRace = race
                Else
                    _chosenRace = Nothing
                End If

            End If

            ButtonOK.Enabled = True

        End Sub

        Private Sub ButtonCancel_Click(ByVal sender As Object,
                                       ByVal e As EventArgs
                                      ) Handles ButtonCancel.Click

            RaiseEvent SelectionCanceled()
            lvwRaces.Items.Clear()
            Me.Close()

        End Sub

        Public Sub LoadingContents() Implements IPickRaceView.LoadingContents
            Throw New NotImplementedException()
        End Sub
    End Class

End Namespace