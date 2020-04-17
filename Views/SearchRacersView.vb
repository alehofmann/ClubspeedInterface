Imports DCS.KioskV15.ClubSpeedInterface.Services

Namespace Views

    Public Class SearchRacersView

        Implements ISearchRacersView

        Public Event SearchForCustomer(ByVal searchText As String) Implements ISearchRacersView.SearchForCustomer
        Public Event SearchForCustomerByPhone(ByVal phoneNumber As String) Implements ISearchRacersView.SearchForCustomerByPhone
        Public Event UserSignupPressed() Implements ISearchRacersView.UserSignupPressed

        Private _popupNotifier As IPopupNotifier

        Public Sub New(ByVal popupNotifier As IPopupNotifier)

            'This call is required by the designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call.
            _popupNotifier = popupNotifier
            With Me
                .SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
                .SetStyle(ControlStyles.UserPaint, True)
                .SetStyle(ControlStyles.AllPaintingInWmPaint, True)
                .UpdateStyles()
            End With
        End Sub

        Private Sub cmdSearch_Click(ByVal sender As Object,
                                    ByVal e As EventArgs
                                   ) Handles cmdSearch.Click

            RaiseEvent SearchForCustomer(txtSearch.Text)
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
                                         ) Handles txtSearch.TextChanged

            cmdSearch.Enabled = (txtSearch.Text.Length > 2)

        End Sub

        Public Function ContainsRacer(ByVal racer As RacerView) As Boolean Implements ISearchRacersView.ContainsRacer

            Return (lstRacers.Items.Contains(racer))

        End Function

        Public Sub AddRacer(ByVal racer As RacerView) Implements ISearchRacersView.AddRacer

            If (lstRacers.Items.Contains(racer)) Then
                Throw New ArgumentException("Racer id " & racer.RacerId & " is already in lstRacers", "racer")
            End If

            lstRacers.Items.Add(racer.FullName & "(id " & racer.RacerId & ")")

        End Sub

        Public Function ChooseRacers() As Boolean Implements ISearchRacersView.ChooseRacers

            ShowDialog()
            Return (DialogResult = DialogResult.OK)

        End Function

        Public Sub UpdateRacerList(ByVal newList As IList(Of RacerView)) Implements ISearchRacersView.UpdateRacerList

            lstRacers.Items.Clear()

            For Each racer In newList
                lstRacers.Items.Add(racer.FullName & " (id " & racer.RacerId & ")")
            Next

        End Sub

        Private Sub txtSearch_KeyDown(ByVal sender As Object,
                                      ByVal e As KeyEventArgs
                                     ) Handles txtSearch.KeyDown

            If (e.KeyCode = Keys.Enter) Then
                cmdSearch_Click(sender, e)
                e.Handled = True
            End If

        End Sub

        Private Sub VirtualKeyboard_SendKey(ByVal newKey As String) Handles VirtualKeyboard.SendKey

            If (newKey = "-1") Then
                If (txtSearch.Text.Length > 0) Then
                    txtSearch.Text = txtSearch.Text.Substring(0, txtSearch.Text.Length - 1)
                End If
            Else
                txtSearch.Text &= newKey
            End If

        End Sub

        Public Sub NotifyNoRacersFound() Implements ISearchRacersView.NotifyNoRacersFound

            _popupNotifier.Notify("No Racers Found")

        End Sub

        Private Sub btnUserSignup_Click(ByVal sender As Object,
                                        ByVal e As EventArgs
                                       ) Handles btnUserSignup.Click

            RaiseEvent UserSignupPressed()

        End Sub

    End Class

End Namespace