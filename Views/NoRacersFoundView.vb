Public Class NoRacersFoundView

    Implements INoRacersFoundView

    Public Sub ShowPopup(Optional ByVal timeoutSeconds As Integer = 0) Implements INoRacersFoundView.ShowPopup

        Me.ShowDialog()

    End Sub

    Private Sub OKButton_Click(ByVal sender As Object,
                               ByVal e As EventArgs
                              ) Handles OKButton.Click

        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub

End Class