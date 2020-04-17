Namespace Models

    'Reference: Example JSON from API
    '  {
    '    "heatTypesId": 1,
    '    "trackId": 1,
    '    "name": "test",
    '    "winBy": 0,
    '    "raceBy": 1,
    '    "lapsOrMinutes": 14,
    '    "cannotBelow": 0,
    '    "cannotExceed": 1000000,
    '    "racersPerHeat": 12,
    '    "scheduleDuration": 10,
    '    "isEventHeatOnly": false,
    '    "speedLevel": 1,
    '    "isPracticeHeat": false,
    '    "enabled": true,
    '    "deleted": true,
    '    "web": false,
    '    "memberOnly": false,
    '    "onHeatStart": 0,
    '    "onHeatFinishAssignLoop": 0,
    '    "onHeatStop": 0,
    '    "cost": 0,
    '    "printResult": 2,
    '    "entitleHeat": false,
    '    "cadetsPerHeat": 0,
    '    "productId": null
    '  }

    Public Class HeatType

        'The primary key for the record
        Public Property HeatTypesId As Integer = -1

        'The default track Id for the HeatMain type (default = NULL)
        Public Property TrackId As Integer = -1

        'The HeatMain name
        Public Property Name As String = ""

        'The indication of whether the HeatMain is won by laps or position: 0=Best Time, 1=Finish Position.
        Public Property WinBy As Integer = -1

        'The indication of whether the HeatMain type should treat .LapsOrMinutes as laps or minutes: 0=Minutes, 1=Laps.
        Public Property RaceBy As Integer = -1

        'Quantity of laps or minutes (depending on .RaceBy) required for the heat type to finish
        Public Property LapsOrMinutes As Integer = -1

        'The minimum cut off for lap times (in milliseconds)
        Public Property CannotBelow As Integer = -1

        'The maximum cut off for lap times (in milliseconds)
        Public Property CannotExceed As Integer = -1

        'Total number of racers available for the HeatMain type
        Public Property RacersPerHeat As Integer = -1

        'The expected duration of the HeatMain type
        Public Property ScheduleDuration As Integer = -1

        'Flag indicating whether this HeatMain type is meant only for events
        Public Property IsEventHeatOnly As Boolean = False

        'The speed level for the HeatMain type
        Public Property SpeedLevel As Integer = -1

        'Flag indicating whether this HeatMain type is meant to be a practice round
        Public Property IsPracticeHeat As Boolean = False

        'Flag indicating whether this HeatMain type is currently enabled
        Public Property Enabled As Boolean = False

        'Flag indicating whether this HeatMain type has been soft deleted
        Public Property Deleted As Boolean = False

        '<undocumented in API>
        Public Property Web As Boolean = False

        'Flag indicating whether a heat type should only allow entrance to members
        Public Property MemberOnly As Boolean = False

        '<undocumented in API>
        Public Property OnHeatStart As Integer = -1

        '<undocumented in API>
        Public Property OnHeatFinishAssignLoop = -1

        '<undocumented in API>
        Public Property onHeatStop As Integer = -1

        'The number of points required for this heat type, where applicable
        Public Property Cost As Integer = -1

        '<undocumented in API>
        Public Property PrintResult As Integer = -1

        '<undocumented in API>
        Public Property EntitleHeat As Boolean = False

        '<undocumented in API>
        Public Property CadetsPerHeat As Integer = -1

        '<undocumented in API>
        Public Property ProductId As Integer? = -1

    End Class

End Namespace