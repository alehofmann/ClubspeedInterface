Public Interface IUserSignupView

    Enum DataItemsEnum As Integer
        Photo = 1
        FirstName = 2
        LastName = 3
        RacerName = 4
        Email = 5
        PhoneNumber = 6
        BirthDate = 7
    End Enum

    Enum SignupResultEnum As Integer
        Finished = 1
        Aborted = 2
    End Enum

    Enum EntryType As Integer
        TypeFreeText = 1
        TypePhone = 2
        TypeEmail = 3   'Cannot be used with masked textbox, but useful to make validity checks.
        TypeDate = 4
        TypeTime = 5    'Not used but present nonetheless.
        TypePicture = 6 'Racer/customer photo and/or signature
    End Enum

    Property ConfigReader As Services.IConfigReader

    ReadOnly Property SignupResult As SignupResultEnum

    Property FirstName As String

    Property LastName As String

    Property RacerName As String

    Property Email As String

    Property PhoneNumberFormatted As String

    Property BirthDate As String

    'Handled as Base64 encoding, not Bitmap. Dunno if correct, 
    'as this depends on the webservice implementation. Problems 
    'are... ValidateData() event validates TEXT only, and 
    'centralized data is passed as strings between view and forms.
    Property PhotoBase64 As String

    Function SignupUser() As SignupResultEnum

    Sub DataValidationSuccess(ByVal data As String)

    Sub DataValidationFailed()

    Event ValidateData(ByVal dataitem As DataItemsEnum,
                       ByVal data As String
                      )

End Interface
