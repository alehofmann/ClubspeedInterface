Imports DCS.KioskV15.ClubSpeedInterface.Services

Public Interface IUserSignupDataForm

    Enum ResultCodesEnum
        NextPressed = 1
        PreviousPressed = 2
    End Enum

    Property ConfigReader As IConfigReader

    Function GetUserDataItem(ByVal description As String,
                             ByVal isFirstItem As Boolean,
                             ByVal isLastItem As Boolean,
                             ByRef dataEntered As String,
                             ByVal entryType As IUserSignupView.EntryType
                            ) As ResultCodesEnum

End Interface