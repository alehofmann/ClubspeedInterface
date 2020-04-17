Public Class frmProgress

    Private _newThread As Threading.Thread,
            _taskDescription As String

    Private Sub ShowForm()

        lbTitle.Text = _taskDescription
        Me.TopMost = True
        Me.Cursor = Cursors.WaitCursor
        Me.Show()

        Application.Run(Me)

    End Sub

    Public Sub TaskStarted(ByVal taskDescription As String)

        _taskDescription = taskDescription
        _newThread = New Threading.Thread(AddressOf ShowForm)
        _newThread.Start()

    End Sub

    Public Sub TaskFinished()

        Me.Invoke(New Action(AddressOf Me.Close))

    End Sub

End Class