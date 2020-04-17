Namespace Models

    'Reference: JSON response for /heatMain call.
    '  [
    '    {
    '      "HeatId": 3,
    '      "track": 1,
    '      "scheduledTime": "2016-02-06T13:30:00.00",
    '      "type": 1,
    '      "lapsOrMinutes": 14,
    '      "StatusEnum": 0,
    '      "eventRound": null,
    '      "beginning": null,
    '      "finish": null,
    '      "winBy": 0,
    '      "raceBy": 1,
    '      "scheduleDuration": 10,
    '      "pointsNeeded": 0,
    '      "speedLevel": 1,
    '      "heatColor": -2302756,
    '      "numberOfReservation": 0,
    '      "memberOnly": false,
    '      "notes": "",
    '      "scoreId": 0,
    '      "racersPerHeat": 12,
    '      "numberOfCadetReservation": 0,
    '      "cadetsPerHeat": 0
    '    }
    '  ]

    Public Class HeatMain

        Public Enum HeatStatusEnum As Integer
            '0=Open, 1=Racing, 2=Finished, 3=Aborted, 4=Closed
            RsUndefined = -1
            RsOpen = 0
            RsRacing = 1
            RsFinished = 2
            RsAborted = 3
            RsClosed = 4
        End Enum

        Property HeatDetails As IList(Of HeatDetail) = New List(Of HeatDetail)

        'HeatMain Id in ClubSpeed database
        <Newtonsoft.Json.JsonProperty(PropertyName:="heatId")>
        Public Property HeatId As Integer = -1

        'Track Id in ClubSpeed database
        <Newtonsoft.Json.JsonProperty(PropertyName:="track")>
        Public Property Track As Integer = -1

        'When the HeatMain is scheduled to begin
        <Newtonsoft.Json.JsonProperty(PropertyName:="scheduledTime")>
        Public Property ScheduledTime As Date = New DateTime(1900, 1, 1)

        'Total number of racers available for the HeatMain, defaults to a lookup from HeatType
        <Newtonsoft.Json.JsonProperty(PropertyName:="racersPerHeat")>
        Public Property RacersPerHeat As Integer = 0

        'Number of (direct) purchases. This number plus the count of records in HeatDetail for this HeatMain is the total racer count.
        <Newtonsoft.Json.JsonProperty(PropertyName:="numberOfReservation")>
        Public Property NumberOfReservation As Integer = 0

        'Freetext field - unknown maxlen.
        <Newtonsoft.Json.JsonProperty(PropertyName:="notes")>
        Public Property Notes As String = System.String.Empty

        'HeatMain StatusEnum: 0=Open, 1=Racing, 2=Finished, 3=Aborted, 4=Closed
        <Newtonsoft.Json.JsonProperty(PropertyName:="StatusEnum")>
        Public Property StatusEnum As HeatStatusEnum = Models.HeatMain.HeatStatusEnum.RsUndefined

        '"Price" of the HeatMain, in ClubSpeed points.
        <Newtonsoft.Json.JsonProperty(PropertyName:="pointsNeeded")>
        Public Property PointsNeeded As Integer = 0

        'The HeatMain type Id from HeatTypes
        <Newtonsoft.Json.JsonProperty(PropertyName:="type")>
        Public Property Type As Integer = -1

    End Class

End Namespace