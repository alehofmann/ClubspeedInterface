Imports DCS.KioskV15.ClubSpeedInterface.Models

Namespace Services

    Public Interface IClubSpeedService

        Function WebServiceAvailable(Optional ByRef errorReason As String = "") As Boolean

        Function GetCustomers() As IEnumerable(Of Customer)

        Function GetCustomerById(ByVal customerId As String) As Customer

        Function GetCustomersByFirstLastOrEmail(ByVal target As String) As IEnumerable(Of Customer)

        Function GetTodayRaces() As Task(Of IEnumerable(Of HeatMain))

        Function GetHeatMainById(ByVal heatId As Integer,
                                 ByVal includeDetail As Boolean
                                ) As HeatMain

        Function GetRaceTypes() As IEnumerable(Of HeatType)

        Function GetHeatDetailsCountByRaceId(ByVal raceId As Integer) As Integer

        Function GetHeatDetailsByRaceId(ByVal raceId As Integer) As Task(Of IList(Of HeatDetail))

        Sub AddHeatDetailRecord(ByVal heatId As Integer,
                                ByVal customerId As Integer
                               )

        Function DeleteHeatDetailRecord(ByVal heatId As Integer,
                                        ByVal customerId As Integer
                                       ) As Boolean

        Function GetCustomersByPhoneNumber(ByVal targetNumber As String) As IEnumerable(Of Customer)

        Sub AddCustomer(ByVal newCustomer As Customer)

    End Interface

End Namespace