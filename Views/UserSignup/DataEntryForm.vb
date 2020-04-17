Imports DCS.KioskV15.ClubSpeedInterface.Services

Public Class DataEntryForm

    Implements IUserSignupDataForm

    Private _result As IUserSignupDataForm.ResultCodesEnum,
            _configReader As Services.IConfigReader

    Public Property ConfigReader As IConfigReader Implements IUserSignupDataForm.ConfigReader
        Get
            Return (Me._configReader)
        End Get
        Set(ByVal newReader As IConfigReader)
            Me._configReader = newReader
        End Set
    End Property

    Public Function GetUserDataItem(ByVal description As String,
                                    ByVal isFirstItem As Boolean,
                                    ByVal isLastItem As Boolean,
                                    ByRef dataEntered As String,
                                    ByVal entryType As IUserSignupView.EntryType
                                   ) As IUserSignupDataForm.ResultCodesEnum Implements IUserSignupDataForm.GetUserDataItem

        If (isFirstItem) Then
            btnPrev.Text = "Cancel"
        Else
            btnPrev.Text = "Previous"
        End If

        If (isLastItem) Then
            btnNext.Text = "Finish"
        Else
            btnNext.Text = "Next"
        End If

        Select Case entryType

            Case IUserSignupView.EntryType.TypeFreeText,
                 IUserSignupView.EntryType.TypeEmail
                mtbData.Mask = String.Empty

            Case IUserSignupView.EntryType.TypeDate
                mtbData.Mask = "00/00/0000"

            Case IUserSignupView.EntryType.TypeTime
                mtbData.Mask = "90:00"

            Case IUserSignupView.EntryType.TypePhone
                mtbData.Mask = "(999)000-0000"

        End Select

        mtbData.Text = dataEntered
        lblDescription.Text = description

        'Make sure to fetch just the data, not formatting, while editing
        mtbData.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals

        ShowDialog()

        'Make sure to fetch data and formatting on return
        mtbData.TextMaskFormat = MaskFormat.IncludePromptAndLiterals
        dataEntered = mtbData.Text

        Return (_result)

    End Function

    Private Sub btnPrev_Click(ByVal sender As Object,
                              ByVal e As EventArgs
                             ) Handles btnPrev.Click

        _result = IUserSignupDataForm.ResultCodesEnum.PreviousPressed
        Close()

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object,
                              ByVal e As EventArgs
                             ) Handles btnNext.Click

        _result = IUserSignupDataForm.ResultCodesEnum.NextPressed
        Close()

    End Sub

    Private Sub vkKeyboard_Load(ByVal sender As Object,
                                ByVal e As EventArgs
                               ) Handles vkKeyboard.Load

        'Please do not remove these commented lines!
        'vkKeyboard.BackColor = Color.Transparent
        'vkKeyboard.BackgroundImage = Nothing
        'vkKeyboard.BackgroundImage.Save("C:\SARKeyboard.Png")

    End Sub

    Private Sub vkKeyboard_SendKey(ByVal newKey As String) Handles vkKeyboard.SendKey

        If (newKey = "-1") Then

            'Remove rightmost char, if there's any text
            If (Me.mtbData.Text.Length > 0) Then
                mtbData.Text = mtbData.Text.Substring(0, mtbData.Text.Length - 1)
            End If

        Else

            'Add the string sent
            mtbData.Text = StrConv(mtbData.Text, VbStrConv.ProperCase) & newKey

        End If

    End Sub

    Private Sub mtbData_KeyUp(ByVal sender As Object,
                              ByVal e As KeyEventArgs
                             ) Handles mtbData.KeyUp

        If (e.KeyCode = Keys.Return) Then
            e.Handled = True
            btnNext.PerformClick()
        End If

        If (e.KeyCode = Keys.Escape) Then
            e.Handled = True
            btnPrev.PerformClick()
        End If

    End Sub

End Class