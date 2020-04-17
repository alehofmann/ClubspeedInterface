Namespace Views

    Public Class PickRacerView

        Implements IPickRacerView

        Private _selectedRacerId As Long = 0,
                _racerList As IList(Of RacerView),
                log As log4net.ILog = log4net.LogManager.GetLogger(Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        Public ReadOnly Property SelectedRacerId As Long Implements IPickRacerView.SelectedRacerId
            Get
                Return (_selectedRacerId)
            End Get
        End Property

        Public Function SelectRacer(sourceList As IList(Of RacerView)) As Boolean Implements IPickRacerView.SelectRacer

            _racerList = sourceList
            _selectedRacerId = 0

            lstRacers.ValueMember = "RacerId"
            lstRacers.DisplayMember = "FullName"
            lstRacers.DataSource = _racerList
            lstRacers.Update()

            'Preselect the first item in the list
            If (lstRacers.Items.Count > 0) Then
                lstRacers.SelectedIndex = 0
            End If

            ShowDialog()

            Return (DialogResult = DialogResult.OK)

        End Function

        Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click

            'Must have a racer chosen to dismiss with OK.
            If ((lstRacers.SelectedIndex >= 0) And
                (lstRacers.SelectedIndex < lstRacers.Items.Count)
               ) Then

                DialogResult = DialogResult.OK
                _racerList.Clear()
                Close()

            End If

        End Sub

        Private Sub ButtonUp_Click(sender As Object, e As EventArgs) Handles ButtonUp.Click

            If (lstRacers.SelectedIndex > 0) Then
                lstRacers.SelectedIndex -= 1
            End If

        End Sub

        Private Sub ButtonDown_Click(sender As Object, e As EventArgs) Handles ButtonDown.Click

            If (lstRacers.SelectedIndex < (lstRacers.Items.Count - 1)) Then
                lstRacers.SelectedIndex += 1
            End If

        End Sub

        Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click

            DialogResult = DialogResult.Cancel
            _racerList.Clear()
            Close()

        End Sub

        Private Sub lstRacers_SelectedIndexChanged(ByVal sender As Object,
                                                   ByVal e As EventArgs
                                                  ) Handles lstRacers.SelectedIndexChanged

            _selectedRacerId = CType(lstRacers.SelectedItem, RacerView).RacerId

        End Sub

    End Class

End Namespace