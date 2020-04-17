Imports DCS.ClubSpeedKioskInterface.Model

Public Class Plugin

    Private _webSvc As New DCS.ClubSpeedKioskInterface.ClubSpeedService

    Private WithEvents _view As DCS.ClubSpeedKioskInterface.frmMain = Nothing

    Private _chosenRaceID As Integer = -1
    Private _availableRaces As System.Collections.ObjectModel.Collection(Of DCS.ClubSpeedKioskInterface.Model.RaceView)
    Private _racers As New System.Collections.ObjectModel.Collection(Of DCS.ClubSpeedKioskInterface.Model.Customer)

    Private _lastErrorText As String

    Private Sub _view_AddRacers() Handles _view.AddRacers

        'Clicked the ADD RACERS button, must invoke the racers class to select racers.

        Dim racersVM As New DCS.ClubSpeedKioskInterface.ViewModel.RacersViewModel

        If ((racersVM.Create(Me._webSvc)) AndAlso (racersVM.Start(Me._racers))) Then
            Me._racers = racersVM.SelectedRacers
            Me._view.UpdateRacers(Me._racers)
        End If

    End Sub

    Private Sub _view_RaceSelected(ByVal raceID As Integer) Handles _view.RaceSelected

        Me._chosenRaceID = raceID

    End Sub

    Private Sub _view_Ready() Handles _view.Ready

        'The form is now on screen and race retrieval may proceed.
        Dim types As IEnumerable(Of Model.RaceType) = _webSvc.GetRaceTypes.ToList(),
            races As IEnumerable(Of Model.RaceView) = _webSvc.GetTodayRaces.Select(Function(item)
                                                                                       Return New Model.RaceView With
                                                                                            {
                                                                                                .ID = item.HeatID,
                                                                                                .DateScheduled = item.ScheduledTime,
                                                                                                .Name = types.Where(Function(myType)
                                                                                                                        Return (item.Type = myType.HeatTypesID)
                                                                                                                    End Function
                                                                                                                   ).FirstOrDefault().Name,
                                                                                                .NumberOfReservation = item.NumberOfReservation,
                                                                                                .TotalSeats = item.RacersPerHeat
                                                                                            }
                                                                                   End Function
                                                                                  ).ToList



        Me._availableRaces = New ObjectModel.Collection(Of DCS.ClubSpeedKioskInterface.Model.RaceView)(races)

        'Fetch available seats for each race.
        For Each race As DCS.ClubSpeedKioskInterface.Model.RaceView In Me._availableRaces
            race.HeatDetailsCount = Me._webSvc.GetHeatDetailsCountByRaceID(race.ID)
        Next race

        Dim filteredRaces As IEnumerable(Of RaceView) = _availableRaces.Where(Function(item) item.AvailableSeats > 8).ToList

        Me._view.ShowRaces(Me._availableRaces)

    End Sub

    Public Function Create() As Boolean

        Dim retVal As Boolean = False,
            failReason As String = System.String.Empty

        retVal = _webSvc.WebServiceAvailable(failReason)

        If (retVal) Then
            Me._view = New DCS.ClubSpeedKioskInterface.frmMain
        Else
            Me._lastErrorText = failReason
        End If

        Return (retVal)

    End Function

    Public Property LastErrorString As String
        Get
            LastErrorString = Me._lastErrorText
        End Get
        Private Set(ByVal newErrorText As String)
            Me._lastErrorText = newErrorText
        End Set
    End Property

    Public Function PurchaseProduct(ByVal parametersIn As System.Collections.Generic.IDictionary(Of String, String),
                                    ByRef parametersOut As System.Collections.Generic.IDictionary(Of String, String)
                                   ) As Boolean

        Dim retVal As Boolean = False

        Me._lastErrorText = System.String.Empty
        Try

            'Create output dictionary
            parametersOut = New System.Collections.Generic.Dictionary(Of String, String)

            'Process the request
            retVal = (Me._view.ShowDialog = DialogResult.OK)

            If (retVal) Then
                'If the dialog was OK'ed, then build the response dictionary
                parametersOut.Add(New System.Collections.Generic.KeyValuePair(Of String, String)("Quantity", Me._racers.Count.ToString()))
                parametersOut.Add(New System.Collections.Generic.KeyValuePair(Of String, String)("HeatID", Me._chosenRaceID.ToString()))
                For ndx As Integer = 0 To Me._racers.Count - 1
                    parametersOut.Add(New System.Collections.Generic.KeyValuePair(Of String, String)("RacerID_" & (ndx + 1).ToString(), Me._racers(ndx).CustomerID.ToString()))
                Next ndx
            End If

        Catch ex As Exception
            Me._lastErrorText = ex.Message
        End Try

        Return (retVal)

    End Function

    Public Function FulfillTransaction(ByVal parametersIn As System.Collections.Generic.IDictionary(Of String, String)) As Boolean

        Dim retVal As Boolean = False,
            raceId As Integer = -1,
            racerQty As Integer = 0

        Try

            'Fetch race id and racer quantity
            raceId = parametersIn.Item("HeatID")
            racerQty = parametersIn.Item("Quantity")

            'Now, for each racer id add a heatDetails record linking both HeatID and CustomerID
            For Each kvp As System.Collections.Generic.KeyValuePair(Of String, String) In parametersIn
                If (kvp.Key.StartsWith("RacerID_")) Then
                    If (Not (Me._webSvc.AddHeatDetailRecord(raceId, kvp.Value))) Then

                        Me._lastErrorText = String.Format("(DCS.Kioskv15.ClubspeedInterface.Plugin.FulfillTransaction()) Could not add customer id '{0}' to race id '{1}'.",
                                                          kvp.Value.ToString(),
                                                          raceId.ToString()
                                                         )

                    End If
                End If
            Next kvp

            retVal = True

        Catch ex As Exception
            Me._lastErrorText = ex.Message()
        End Try

        Return (retVal)

    End Function

End Class
