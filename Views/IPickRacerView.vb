Namespace Views

    Public Interface IPickRacerView

        Function SelectRacer(sourceList As IList(Of RacerView)) As Boolean

        ReadOnly Property SelectedRacerId As Long

    End Interface

End Namespace
