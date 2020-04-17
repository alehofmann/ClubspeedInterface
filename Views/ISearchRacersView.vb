Namespace Views

    Public Interface ISearchRacersView

        Sub UpdateRacerList(ByVal newList As IList(Of RacerView))

        Function ChooseRacers() As Boolean

        Function ContainsRacer(ByVal racer As RacerView) As Boolean

        Sub AddRacer(racer As RacerView)

        Event SearchForCustomer(ByVal searchText As String)

        Event SearchForCustomerByPhone(ByVal phoneNumber As String)

        Event UserSignupPressed()

        Sub NotifyNoRacersFound()

    End Interface

End Namespace
