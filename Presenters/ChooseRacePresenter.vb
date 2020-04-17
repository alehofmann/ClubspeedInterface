Imports System.Collections.ObjectModel
Imports DCS.KioskV15.ClubSpeedInterface.Models
Imports DCS.KioskV15.ClubSpeedInterface.Services
Imports DCS.KioskV15.ClubSpeedInterface.Views

Namespace Presenters

    Public Class ChooseRacePresenter

        'Private WithEvents _view As frmRaces = Nothing
        Private WithEvents _selectRaceView As IPickRaceView

        Private _webSvc As IClubSpeedService = Nothing,
                _availableRaces As IList(Of RaceView),
                _selectedRacers As IList(Of Customer)

        Private _selectedHeat As RaceView = Nothing

        Public Sub New()

        End Sub

        Public Property ClubSpeedService As IClubSpeedService
            Get
                Return (_webSvc)
            End Get
            Set(ByVal newService As IClubSpeedService)
                _webSvc = newService
            End Set
        End Property

        Public Property PickRaceView As IPickRaceView
            Get
                Return (_selectRaceView)
            End Get
            Set(ByVal newView As IPickRaceView)
                _selectRaceView = newView
            End Set
        End Property

        Friend ReadOnly Property SelectedHeat As RaceView
            Get
                Return (_selectedHeat)
            End Get
        End Property

        Public Function ChooseRace(ByVal targetRacers As List(Of Customer)
                                  ) As Boolean

            'Persist required racers
            _selectedRacers = targetRacers

            _selectRaceView.LoadingContents()

            Dim elTask As Task(Of IList(Of RaceView)) = Task.Run(Function() GetTodayRaces())


            'Dim task = GetTodayRaces()
            elTask.Wait()

            _availableRaces = elTask.Result
            'Instance HeatMain selection screen

            _selectRaceView.ShowView(_availableRaces)

            'Trigger selection and return the dialog's result
            'Return IIf(_view.ShowDialog() = DialogResult.OK, True, False)
            Return (_selectedHeat IsNot Nothing)

        End Function
        Private Async Function GetTodayRaces() As Task(Of IList(Of RaceView))
            Dim types As IEnumerable(Of HeatType) = _webSvc.GetRaceTypes.ToList()
            Dim retVal As IList(Of RaceView)

            Dim r As IEnumerable(Of HeatMain) = Await _webSvc.GetTodayRaces.ConfigureAwait(False)

            retVal = r.Select(Function(item)
                                  Return New RaceView With
                                                                                                {
                                                                                                    .Id = item.HeatId,
                                                                                                    .DateScheduled = item.ScheduledTime,
                                                                                                    .Name = types.Where(Function(myType)
                                                                                                                            Return (item.Type = myType.HeatTypesId)
                                                                                                                        End Function
                                                                                                                       ).FirstOrDefault().Name,
                                                                                                    .NumberOfReservation = item.NumberOfReservation,
                                                                                                    .TotalSeats = item.RacersPerHeat,
                                                                                                    .HeatDetails = item.HeatDetails.Select(Function(detail)
                                                                                                                                               Return New HeatDetailView(detail.HeatId, detail.CustomerId)
                                                                                                                                           End Function
                                                                                                                                          ).ToList
                                                                                                }
                              End Function
                                                                                ).ToList

            Return retVal.Where(Function(race)
                                    Return (race.AvailableSeats > _selectedRacers.Count)
                                End Function
                                                                               ).ToList

            'Dim result = Await Task.Run(Function() retVal.Where(Function(race)
            '                                                        Return (race.AvailableSeats > _selectedRacers.Count)
            '                                                    End Function
            '                                                                   ).ToList
            '                                             )
            'Return result
        End Function
        Private Sub _selectRaceView_GetRaces(ByRef races As IList(Of RaceView)) Handles _selectRaceView.GetRaces

            'The view is now on screen and HeatMain retrieval may proceed.



            _availableRaces = GetTodayRaces()

            'Filter races by seat availability
            _availableRaces = New List(Of RaceView)(_availableRaces.Where(Function(race)
                                                                              Return (race.AvailableSeats > _selectedRacers.Count)
                                                                          End Function
                                                                               ).ToList
                                                         )

            races = Me._availableRaces

        End Sub

        Private Sub _selectRaceView_CanSelectRace(ByVal race As RaceView,
                                                  ByRef raceIsSelectable As Boolean
                                                 ) Handles _selectRaceView.CanSelectRace

            raceIsSelectable = True

            'Make sure to warn user if the race already contains one of the racers.
            For Each cust As Customer In _selectedRacers
                If (race.HasRacerID(cust.CustomerId)) Then
                    raceIsSelectable = False
                    Exit For
                End If
            Next

        End Sub

        Private Sub _selectRaceView_RaceSelected(ByVal race As RaceView) Handles _selectRaceView.RaceSelected

            _selectedHeat = race

        End Sub

        Private Sub _selectRaceView_SelectionCanceled() Handles _selectRaceView.SelectionCanceled

            _selectedHeat = Nothing

        End Sub

        Private Sub _selectRaceView_RaceSelectedById(raceId As Integer) Handles _selectRaceView.RaceSelectedById
            _selectedHeat = _availableRaces.Where(Function(x) x.Id = raceId).FirstOrDefault
        End Sub
    End Class

End Namespace