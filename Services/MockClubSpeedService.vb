Imports DCS.KioskV15.ClubSpeedInterface.Models

Namespace Services

    Public Class MockClubSpeedService

        Implements IClubSpeedService

        Private ReadOnly _customers As IList(Of Customer) =
            {
                 (New Customer With
                    {
                        .CustomerId = 1,
                        .FirstName = "Marcelo",
                        .LastName = "Lopez"
                    }
                 ),
                 (New Customer With
                    {
                        .CustomerId = 2,
                        .FirstName = "Alejandro",
                        .LastName = "Pomeraniec"
                    }
                 ),
                 (New Customer With
                    {
                        .CustomerId = 3,
                        .FirstName = "Braian",
                        .LastName = "Humbel"
                    }
                 )
            }

        Private ReadOnly _heatTypes As IList(Of HeatType) =
            {
                (New HeatType With
                    {
                        .HeatTypesId = 1,
                        .Name = "10 minute heat",
                        .Deleted = False
                    }
                ),
                (New HeatType With
                    {
                        .HeatTypesId = 2,
                        .Name = "5 laps heat",
                        .Deleted = False
                    }
                )
            }

        Private ReadOnly _todayRaces As IList(Of HeatMain) =
            (From i In Enumerable.Range(1, 23)
             Select New HeatMain With
                {
                    .Type = 1,
                    .HeatId = i,
                    .ScheduledTime = DateTime.Today.AddHours(i),
                    .NumberOfReservation = 0,
                    .RacersPerHeat = 12
                }).ToList

        Protected Overrides Sub Finalize()

            MyBase.Finalize()

        End Sub

        Public Sub AddHeatDetailRecord(ByVal heatId As Integer,
                                       ByVal customerId As Integer
                                      ) Implements IClubSpeedService.AddHeatDetailRecord
        End Sub

        Public Function GetCustomerById(ByVal customerId As String
                                       ) As Customer Implements IClubSpeedService.GetCustomerById

            Return (_customers.Where(Function(x) x.CustomerId = customerId).ToList.FirstOrDefault)

        End Function

        Public Function GetCustomers() As IEnumerable(Of Customer) Implements IClubSpeedService.GetCustomers

            Return (_customers)

        End Function

        Public Function GetCustomersByFirstLastOrEmail(ByVal target As String
                                                      ) As IEnumerable(Of Customer) Implements IClubSpeedService.GetCustomersByFirstLastOrEmail

            Return (_customers.Where(Function(x) (x.FirstName.ToLower & " " & x.LastName.ToLower).Contains(target.ToLower)).ToList)

        End Function

        Public Function GetHeatDetailsCountByRaceId(ByVal raceId As Integer
                                                   ) As Integer Implements IClubSpeedService.GetHeatDetailsCountByRaceId

            Return (0)

        End Function

        Public Function GetHeatMainById(ByVal heatId As Integer,
                                        ByVal includeDetail As Boolean
                                       ) As HeatMain Implements IClubSpeedService.GetHeatMainById

            Return (_todayRaces.Where(Function(x) x.HeatId = heatId).ToList.FirstOrDefault)

        End Function

        Public Function GetRaceTypes() As IEnumerable(Of HeatType) Implements IClubSpeedService.GetRaceTypes
            Return _heatTypes
        End Function

        Public Async Function GetTodayRaces() As Task(Of IEnumerable(Of HeatMain)) Implements IClubSpeedService.GetTodayRaces

            Dim todayRaces As IList(Of HeatMain) = _todayRaces.Where(Function(x) DateTime.Compare(Now, x.ScheduledTime) <= 0).ToList
            Dim racesWithDetail As New List(Of HeatMain)

            For Each race In todayRaces
                race.HeatDetails = Await GetHeatDetailsByRaceId(race.HeatId).ConfigureAwait(False)
                racesWithDetail.Add(race)
            Next

            Return (racesWithDetail)

        End Function

        Public Function WebServiceAvailable(ByRef Optional errorReason As String = ""
                                           ) As Boolean Implements IClubSpeedService.WebServiceAvailable

            Return True

        End Function

        Public Function GetCustomersByPhoneNumber(ByVal target As String
                                                 ) As IEnumerable(Of Customer) Implements IClubSpeedService.GetCustomersByPhoneNumber

            Return (_customers)

        End Function

        Public Sub AddCustomer(ByVal customer As Customer
                              ) Implements IClubSpeedService.AddCustomer

            customer.CustomerId = 976

        End Sub

        Public Function GetHeatDetailsByRaceId(ByVal raceId As Integer
                                              ) As Task(Of IList(Of HeatDetail)) Implements IClubSpeedService.GetHeatDetailsByRaceId

            Throw New NotImplementedException()

        End Function

        Public Function DeleteHeatDetailRecord(ByVal heatId As Integer,
                                               ByVal customerId As Integer
                                              ) As Boolean Implements IClubSpeedService.DeleteHeatDetailRecord

            'Return TRUE on even heatIDs, FALSE on odd ones.
            Return ((heatId Mod 2) = 0)

        End Function

    End Class

End Namespace