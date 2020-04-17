Namespace Presenters

    Public Class HeatDetailView

        Public Sub New(heatIdParam As Integer, customerIdParam As Integer)
            HeatId = heatIdParam
            CustomerId = customerIdParam
        End Sub

        Public Property HeatId As Integer = -1

        Public Property CustomerId As Integer = -1

    End Class

End Namespace