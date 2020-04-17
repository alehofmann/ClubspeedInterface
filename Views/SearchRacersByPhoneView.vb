Namespace Views

    Public Class SearchRacersByPhoneView

        Implements ISearchRacersView

        Public Event SearchForCustomer(searchText As String) Implements ISearchRacersView.SearchForCustomer
        Public Event SearchForCustomerByPhone(phoneNumber As String) Implements ISearchRacersView.SearchForCustomerByPhone
        Public Event UserSignupPressed() Implements ISearchRacersView.UserSignupPressed
        Private _phoneNumber As String

        Public Sub New()

            'This call is required by the designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call.

        End Sub

        Private Sub cmdSearch_Click(ByVal sender As Object,
                                    ByVal e As EventArgs
                                    )

            RaiseEvent SearchForCustomerByPhone(txtSearch.Text)
            txtSearch.Clear()

        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object,
                                    ByVal e As EventArgs
                                    ) Handles cmdCancel.Click

            Me.DialogResult = DialogResult.Cancel
            lstRacers.Items.Clear()
            Me.Close()

        End Sub

        Private Sub cmdOK_Click(ByVal sender As Object,
                                ByVal e As EventArgs
                                ) Handles cmdOK.Click

            DialogResult = DialogResult.OK
            Close()

        End Sub

        Private Sub txtSearch_TextChanged(ByVal sender As Object,
                                          ByVal e As EventArgs
                                         )

            'cmdSearch.Enabled = (txtSearch.Text.Length > 2)

        End Sub

        Public Function ContainsRacer(racer As RacerView) As Boolean Implements ISearchRacersView.ContainsRacer

            Return (lstRacers.Items.Contains(racer))

        End Function

        Public Sub AddRacer(racer As RacerView) Implements ISearchRacersView.AddRacer

            If (lstRacers.Items.Contains(racer)) Then
                Throw New ArgumentException("Racer id " & racer.RacerId & " is already in lstRacers", "racer")
            End If

            lstRacers.Items.Add(racer.FullName & "(id " & racer.RacerId & ")")

        End Sub

        Public Function ChooseRacers() As Boolean Implements ISearchRacersView.ChooseRacers

            ShowDialog()
            Return (DialogResult = DialogResult.OK)

        End Function

        Public Sub UpdateRacerList(newList As IList(Of RacerView)) Implements ISearchRacersView.UpdateRacerList

            lstRacers.Items.Clear()

            For Each racer In newList
                lstRacers.Items.Add(racer.FullName & " (id " & racer.RacerId & ")")
            Next

        End Sub

        Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)

            If (e.KeyCode = Keys.Enter) Then
                cmdSearch_Click(sender, e)
                e.Handled = True
            End If

        End Sub

        Private _typedPhoneNumber As String = ""

        Private Sub KeyTyped(key As String)
            If _typedPhoneNumber.Length < 10 And IsNumeric(key) Then
                _typedPhoneNumber = _typedPhoneNumber & key
                txtSearch.Text = _typedPhoneNumber
            End If

        End Sub

        Private Sub DeleteKeyPressed()
            If _typedPhoneNumber <> "" Then
                _typedPhoneNumber = _typedPhoneNumber.Remove(_typedPhoneNumber.Length - 1, 1)
                txtSearch.Text = _typedPhoneNumber
            End If
        End Sub


        Private Sub VirtualKeyboard_SendKey(newKey As String) Handles VirtualKeyboard.SendKey

            If (newKey = "-1") Then
                DeleteKeyPressed()
            Else
                KeyTyped(newKey)
            End If

        End Sub


        Private Sub cmdSearch_Click_1(sender As Object, e As EventArgs) Handles cmdSearch.Click
            RaiseEvent SearchForCustomerByPhone(_typedPhoneNumber)
        End Sub

        Public Sub NotifyNoRacersFound() Implements ISearchRacersView.NotifyNoRacersFound
            '_popupNotifier.Notify("No Racers Found")
        End Sub
    End Class
End Namespace