Namespace Models

    Public Class HeatDetail

        'Reference - HeatDetail record fetched from API:
        '  [
        '    {
        '      "HeatId": 3,
        '      "customerId": 1000001,
        '      "autoNo": -1,
        '      "lineUpPosition": 1,
        '      "groupId": 0,
        '      "proskill": 1200,
        '      "pointHistoryId": 0,
        '      "firstTime": true,
        '      "userId": 1,
        '      "finishPosition": null,
        '      "groupFinishPosition": null,
        '      "proskillDiff": null,
        '      "positionEditedDate": null,
        '      "historyAutoNo": 1,
        '      "scores": 0,
        '      "timeAdded": "2016-02-06T13:20:21.61",
        '      "assignedtoEntitleHeat": true
        '    }
        '  ]

        Public Sub New(ByVal heatIdParam As Integer,
                       ByVal customerIdParam As Integer
                      )
            HeatId = heatIdParam
            CustomerId = customerIdParam
        End Sub

        <Newtonsoft.Json.JsonProperty(PropertyName:="heatId")>
        Public Property HeatId As Integer = -1

        <Newtonsoft.Json.JsonProperty(PropertyName:="customerId")>
        Public Property CustomerId As Integer = -1

        <Newtonsoft.Json.JsonProperty(PropertyName:="autoNo")>
        Public Property AutoNo As Integer?

        <Newtonsoft.Json.JsonProperty(PropertyName:="lineUpPosition")>
        Public Property LineUpPosition As Integer?

        <Newtonsoft.Json.JsonProperty(PropertyName:="groupId")>
        Public Property GroupId As Integer?

        <Newtonsoft.Json.JsonProperty(PropertyName:="proskill")>
        Public Property ProSkill As Integer?

        <Newtonsoft.Json.JsonProperty(PropertyName:="pointHistoryId")>
        Public Property PointHistoryId As Integer?

        <Newtonsoft.Json.JsonProperty(PropertyName:="firstTime")>
        Public Property FirstTime As Boolean = False

        <Newtonsoft.Json.JsonProperty(PropertyName:="userId")>
        Public Property UserId As Integer?

        <Newtonsoft.Json.JsonProperty(PropertyName:="finishPosition")>
        Public Property FinishPosition As Integer?

        <Newtonsoft.Json.JsonProperty(PropertyName:="groupFinishPosition")>
        Public Property GroupFinishPosition As Integer?

        <Newtonsoft.Json.JsonProperty(PropertyName:="proskillDiff")>
        Public Property ProSkillDiff As Integer?

        <Newtonsoft.Json.JsonProperty(PropertyName:="positionEditedDate")>
        Public Property PositionEditedDate As Date?

        <Newtonsoft.Json.JsonProperty(PropertyName:="historyAutoNo")>
        Public Property HistoryAutoNo As Integer?

        <Newtonsoft.Json.JsonProperty(PropertyName:="scores")>
        Public Property Scores As Integer?

        <Newtonsoft.Json.JsonProperty(PropertyName:="timeAdded")>
        Public Property TimeAdded As Date? ' = New Date(1900, 1, 1)

        <Newtonsoft.Json.JsonProperty(PropertyName:="assignedtoEntitleHeat")>
        Public Property AssignedToEntitleHeat As Boolean = False

    End Class

End Namespace