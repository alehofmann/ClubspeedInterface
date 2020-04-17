Imports System.Collections.ObjectModel
Imports DCS.KioskV15.ClubSpeedInterface.Presenters

Namespace Views

    Public Interface IPickRaceView

        Sub ShowView(raceList As IList(Of RaceView))
        Sub LoadingContents()

        Event GetRaces(ByRef races As IList(Of RaceView))

        Event CanSelectRace(ByVal race As RaceView,
                            ByRef raceIsSelectable As Boolean
                           )

        Event RaceSelected(ByVal race As RaceView)
        Event RaceSelectedById(ByVal raceId As Integer)

        Event SelectionCanceled()


    End Interface

End Namespace