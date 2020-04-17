Namespace Services
    Public Class MsgboxNotifier
        Implements IPopupNotifier

        Public Sub Notify(text As String) Implements IPopupNotifier.Notify
            MsgBox(text)
        End Sub
    End Class
End Namespace
