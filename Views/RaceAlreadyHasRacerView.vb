Public Class RaceAlreadyHasRacerView

    Implements IRaceAlreadyHasRacerView

    Public Sub ShowPopup(Optional ByVal timeoutSeconds As Integer = 0) Implements IRaceAlreadyHasRacerView.ShowPopup

        Me.ShowDialog()

    End Sub

    Private Sub cmdOK_Click(ByVal sender As Object,
                            ByVal e As EventArgs
                           ) Handles cmdOK.Click

        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub

End Class