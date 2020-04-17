Imports System.Threading
Imports DCS.KioskV15.ClubSpeedInterface.Services
Imports DCS.KioskV15.ClubSpeedInterface.Views.UserSignup

Public Class UserSignupView

    Implements IUserSignupView

    Property Email As String Implements IUserSignupView.Email

    Property FirstName As String Implements IUserSignupView.FirstName

    Property LastName As String Implements IUserSignupView.LastName

    Property RacerName As String Implements IUserSignupView.RacerName

    Property PhoneNumberFormatted As String Implements IUserSignupView.PhoneNumberFormatted

    Property BirthDate As String Implements IUserSignupView.BirthDate

    Property PhotoBase64 As String Implements IUserSignupView.PhotoBase64

    Private _signupResult As IUserSignupView.SignupResultEnum = IUserSignupView.SignupResultEnum.Aborted,
            _configReader As Services.IConfigReader = Nothing

    Public ReadOnly Property SignupResult As IUserSignupView.SignupResultEnum Implements IUserSignupView.SignupResult
        Get
            Return (_signupResult)
        End Get
    End Property

    Public Property ConfigReader As IConfigReader Implements IUserSignupView.ConfigReader
        Get
            Return (Me._configReader)
        End Get
        Set(ByVal newReader As IConfigReader)
            Me._configReader = newReader
        End Set
    End Property

    Public Event ValidateData(ByVal dataitem As IUserSignupView.DataItemsEnum,
                              ByVal text As String
                             ) Implements IUserSignupView.ValidateData

    'Private _dataEnterForm As New DataEntryForm
    Private _dataEntryForm As IUserSignupDataForm
    Private _notifier As Services.IPopupNotifier
    Private _currentIndex As IUserSignupView.DataItemsEnum
    Private _signupFinished As Boolean = False
    Private _signupFinishedResetEvent As New ManualResetEvent(False)

    Public Sub New(ByVal notifier As Services.IPopupNotifier)

        _notifier = notifier

    End Sub

    Public Function SignupUser() As IUserSignupView.SignupResultEnum Implements IUserSignupView.SignupUser

        'Must init the properties; otherwise subsequent users 
        'will have to erase the previous user data.
        Email = String.Empty
        FirstName = String.Empty
        LastName = String.Empty
        RacerName = String.Empty
        PhoneNumberFormatted = String.Empty
        BirthDate = String.Empty
        PhotoBase64 = String.Empty

        'Also must init the sequence. This makes sure to use the first enum value even if it changes.
        _currentIndex = [Enum].
            GetValues(GetType(IUserSignupView.DataItemsEnum)).
            Cast(Of IUserSignupView.DataItemsEnum).
            Min()

        'Trigger data retrieval...
        GetData()

        '... and hold until the process is over.
        _signupFinishedResetEvent.WaitOne()

        'Once hold is released, return the result.
        Return (_signupResult)

    End Function

    Public Sub GetData()

        Dim itemName As String = "No Item",
            data As String = "No Data",
            dataType As IUserSignupView.EntryType = IUserSignupView.EntryType.TypeFreeText

        Select Case _currentIndex

            Case IUserSignupView.DataItemsEnum.FirstName
                Me._dataEntryForm = New DataEntryForm
                itemName = "First Name"
                data = FirstName

            Case IUserSignupView.DataItemsEnum.LastName
                Me._dataEntryForm = New DataEntryForm
                itemName = "Last Name"
                data = LastName

            Case IUserSignupView.DataItemsEnum.RacerName
                Me._dataEntryForm = New DataEntryForm
                itemName = "Racer Name"
                data = RacerName

            Case IUserSignupView.DataItemsEnum.Email
                Me._dataEntryForm = New DataEntryForm
                itemName = "Email"
                data = Email

            Case IUserSignupView.DataItemsEnum.PhoneNumber
                Me._dataEntryForm = New DataEntryForm
                itemName = "Phone Number"
                dataType = IUserSignupView.EntryType.TypePhone
                data = PhoneNumberFormatted

            Case IUserSignupView.DataItemsEnum.BirthDate
                Me._dataEntryForm = New DataEntryForm
                itemName = "Birth Date"
                dataType = IUserSignupView.EntryType.TypeDate
                Dim dummyDate As Date
                If (Date.TryParse(BirthDate, dummyDate)) Then
                    data = BirthDate.ToString()
                Else
                    data = String.Empty
                End If

            Case IUserSignupView.DataItemsEnum.Photo
                Me._dataEntryForm = New PhotoBoothView
                Me._dataEntryForm.ConfigReader = _configReader
                itemName = "Photo"
                dataType = IUserSignupView.EntryType.TypePicture
                data = PhotoBase64

        End Select

        If (_dataEntryForm.GetUserDataItem(itemName,
                                           OnFirstItem,
                                           OnLastItem,
                                           data,
                                           dataType
                                          ) = IUserSignupDataForm.ResultCodesEnum.NextPressed
           ) Then

            RaiseEvent ValidateData(_currentIndex, data)

        Else

            If (OnFirstItem()) Then
                _signupResult = IUserSignupView.SignupResultEnum.Aborted
                SignalSignupAborted()
            Else
                _currentIndex = _currentIndex - 1
                GetData()
            End If

        End If

    End Sub

    Private Function OnFirstItem() As Boolean

        Return (_currentIndex = [Enum].GetValues(GetType(IUserSignupView.DataItemsEnum)).GetLowerBound(0) + 1)

    End Function

    Private Function OnLastItem() As Boolean

        Return (_currentIndex = [Enum].GetValues(GetType(IUserSignupView.DataItemsEnum)).GetUpperBound(0) + 1)

    End Function

    Private Sub SignalSignupAborted()

        _signupResult = IUserSignupView.SignupResultEnum.Aborted
        _signupFinishedResetEvent.Set()

    End Sub

    Private Sub SignalSignupFinished()

        'RaiseEvent SignupFinished()
        _signupResult = IUserSignupView.SignupResultEnum.Finished
        _signupFinishedResetEvent.Set()

    End Sub

    Public Sub DataValidationSuccess(ByVal data As String) Implements IUserSignupView.DataValidationSuccess

        Select Case _currentIndex

            Case IUserSignupView.DataItemsEnum.FirstName
                FirstName = data

            Case IUserSignupView.DataItemsEnum.LastName
                LastName = data

            Case IUserSignupView.DataItemsEnum.RacerName
                RacerName = data

            Case IUserSignupView.DataItemsEnum.Email
                Email = data

            Case IUserSignupView.DataItemsEnum.PhoneNumber
                PhoneNumberFormatted = data

            Case IUserSignupView.DataItemsEnum.BirthDate
                BirthDate = data

            Case IUserSignupView.DataItemsEnum.Photo
                PhotoBase64 = data

        End Select

        If (Not (OnLastItem())) Then
            _currentIndex += 1
            GetData()
        Else
            SignalSignupFinished()
        End If

    End Sub

    Public Sub DataValidationFailed() Implements IUserSignupView.DataValidationFailed

        _notifier.Notify("Invalid data")
        GetData()

    End Sub

End Class
