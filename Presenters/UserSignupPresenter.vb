Imports System.Net.Mail
Imports DCS.KioskV15.ClubSpeedInterface.Models
Imports DCS.KioskV15.ClubSpeedInterface.Services

Public Class UserSignupPresenter

    Private _webSvc As IClubSpeedService

    Private WithEvents _view As IUserSignupView

    Public Sub New(ByVal webSvc As IClubSpeedService,
                   ByVal view As IUserSignupView
                  )

        _view = view
        _webSvc = webSvc

    End Sub

    Public Function Signup() As Customer

        If (_view.SignupUser = IUserSignupView.SignupResultEnum.Finished) Then

            Dim customer = New Customer() With {
                    .FirstName = _view.FirstName,
                    .LastName = _view.LastName,
                    .RacerName = _view.RacerName,
                    .BirthDate = Date.Parse(_view.BirthDate),
                    .MobilePhone = _view.PhoneNumberFormatted,
                    .EMail = _view.Email,
                    .PhotoBase64String = _view.PhotoBase64
                }

            Try
                _webSvc.AddCustomer(customer)
            Catch ex As Exception
                '_view.ErrorAddingCustomer
            End Try

            Return (customer)

        Else

            Return (Nothing)

        End If

    End Function

    Private Sub _view_ValidateData(ByVal dataItemType As IUserSignupView.DataItemsEnum,
                                   ByVal textToValidate As String
                                  ) Handles _view.ValidateData

        Dim retVal As Boolean

        If (dataItemType = IUserSignupView.DataItemsEnum.BirthDate) Then

            retVal = Date.TryParse(textToValidate, New Date())

        ElseIf (dataItemType = IUserSignupView.DataItemsEnum.PhoneNumber) Then

            retVal = IsValidUSAPhoneNumber(textToValidate)

        ElseIf (dataItemType = IUserSignupView.DataItemsEnum.Email) Then

            Try
                Dim email As New MailAddress(textToValidate)
                retVal = True
            Catch e As Exception
                retVal = False
            End Try

        ElseIf (dataItemType = IUserSignupView.DataItemsEnum.Photo) Then

            retVal = (textToValidate.Trim <> String.Empty)

        Else

            'This validates free text fields.
            retVal = (textToValidate.Trim <> String.Empty)

        End If

        If (retVal) Then
            _view.DataValidationSuccess(textToValidate)
        Else
            _view.DataValidationFailed()
        End If

    End Sub

    Private Function IsValidUSAPhoneNumber(ByVal phoneToValidate As String) As Boolean

        Dim retVal As Boolean = False,
            phoneNumberChecker As New Text.RegularExpressions.Regex("^\([1-9]\d{2}\)[1-9]\d{2}-\d{4}$")

        If (Not (String.IsNullOrEmpty(phoneToValidate))) Then
            retVal = phoneNumberChecker.IsMatch(phoneToValidate)
        End If

        Return retVal

    End Function

End Class
