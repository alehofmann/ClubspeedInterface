Imports AutoMapper
Imports DCS.KioskV15.ClubSpeedInterface.Services
Imports DCS.KioskV15.ClubSpeedInterface.Models
Imports DCS.KioskV15.ClubSpeedInterface.Views

Namespace Presenters

    Public Class SearchRacersPresenter

#Region "Injected Properties"

        Private WithEvents _noRacersFound As INoRacersFoundView
        Private WithEvents _searchRacersView As ISearchRacersView
        Private WithEvents _pickRacerView As IPickRacerView

        Property UserSignupPresenter As UserSignupPresenter

        Private _webSvc As IClubSpeedService,
                _racerList As IList(Of Customer) = New List(Of Customer),
                _mapper As IMapper,
                _notifier As IPopupNotifier

        Public Property PickRacerView As IPickRacerView
            Get
                Return _pickRacerView
            End Get
            Set(ByVal newView As IPickRacerView)
                _pickRacerView = newView
            End Set
        End Property

        Public Property WebSvc As IClubSpeedService
            Get
                Return _webSvc
            End Get
            Set(ByVal newService As IClubSpeedService)
                _webSvc = newService
            End Set
        End Property

        Public Property SearchRacersView As ISearchRacersView
            Get
                Return _searchRacersView
            End Get
            Set(ByVal newView As ISearchRacersView)
                _searchRacersView = newView
            End Set
        End Property

        Public Property Mapper As IMapper
            Get
                Return _mapper
            End Get
            Set(ByVal newMapper As IMapper)
                _mapper = newMapper
            End Set
        End Property

        Public Property PopupNotifier As IPopupNotifier
            Get
                Return (_notifier)
            End Get
            Set(ByVal newNotifier As IPopupNotifier)
                _notifier = newNotifier
            End Set
        End Property

#End Region

        Public Sub OnSearchForCustomerByPhone(phoneNumber As String) Handles _searchRacersView.SearchForCustomerByPhone
            If phoneNumber.Length = 10 Then
                Dim resultList = _webSvc.GetCustomersByPhoneNumber(phoneNumber)
                UpdateRacersList(resultList)
            End If
        End Sub

        Private Sub UpdateRacersList(resultList As IEnumerable(Of Customer))

            Dim racerViewList As IList(Of RacerView),
                racerToAdd As Customer = Nothing

            If (resultList.Count = 1) Then

                'One match: Direct add to list.
                racerToAdd = resultList.First

            ElseIf (resultList.Count > 1) Then

                'More than a match: Disambiguate.
                racerViewList = _mapper.Map(Of IList(Of Customer), IList(Of RacerView))(resultList)
                If (_pickRacerView.SelectRacer(racerViewList)) Then
                    racerToAdd = resultList.Single(Function(x) x.CustomerId = _pickRacerView.SelectedRacerId)
                End If

            Else

                'No matches: Put up dialog abt it.
                _searchRacersView.NotifyNoRacersFound()

            End If

            If ((racerToAdd IsNot Nothing) AndAlso
                (Not (_racerList.Contains(racerToAdd)))
               ) Then

                _racerList.Add(racerToAdd)
                racerViewList = _mapper.Map(Of IList(Of Customer), IList(Of RacerView))(_racerList)
                _searchRacersView.UpdateRacerList(racerViewList)

            End If
        End Sub

        Public Sub OnSearchForCustomer(filterText As String) Handles _searchRacersView.SearchForCustomer

            Dim resultList = _webSvc.GetCustomersByFirstLastOrEmail(filterText)
            UpdateRacersList(resultList)

        End Sub

        Public Function ChooseRacers() As IList(Of Customer)

            If (Not (_searchRacersView.ChooseRacers())) Then
                _racerList.Clear()
            End If

            Return _racerList

        End Function

        Private Sub _searchRacersView_UserSignupPressed() Handles _searchRacersView.UserSignupPressed

            Dim newCustomer As Customer

            newCustomer = _UserSignupPresenter.Signup()
            If (newCustomer IsNot Nothing) Then

                _racerList.Add(newCustomer)
                _searchRacersView.UpdateRacerList(_mapper.Map(Of IList(Of Customer), IList(Of RacerView))(_racerList))

            End If

        End Sub

    End Class

End Namespace