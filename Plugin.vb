'Imports AutoMapper
Imports DCS.KioskV15.ClubSpeedInterface.Services
Imports DCS.KioskV15.ClubSpeedInterface.Models
Imports DCS.KioskV15.ClubSpeedInterface.Views
Imports DCS.KioskV15.ClubSpeedInterface.Presenters
Imports Autofac

Public Class Plugin

    Private ReadOnly _webSvc As IClubSpeedService
    Private _racersSelector As SearchRacersPresenter
    Private ReadOnly _raceSelector As ChooseRacePresenter

    Private ReadOnly _configReader As INIConfigReader

    Private _selectedHeat As RaceView
    Private _racers As IEnumerable(Of Customer)

    Private _lastErrorText As String
    Private log As log4net.ILog = log4net.LogManager.GetLogger(Reflection.MethodBase.GetCurrentMethod().DeclaringType)

    Public Sub New()

        Dim failReason As String = String.Empty

        log.Info("Initializing Clubspeed Interface Plugin")

        'Dim iniFilename As String = Environment.ExpandEnvironmentVariables("%NW%") & "\DCS\Config\Clubspeed\Config.Ini",
        '    configReader As New INIConfigReader(iniFilename)
        Dim iniFilename As String = Environment.ExpandEnvironmentVariables("%NW%") & "\DCS\Config\Clubspeed\Config.Ini"

        _configReader = New INIConfigReader(iniFilename)
        log.Info($"Reading configuration from ini file name [{iniFilename}]")

        Dim useMockService = IniGetBoolean("WebService", "Use Mock Service", False, iniFilename)
        If useMockService Then
            log.Warn("Using 'mock' Clubspeed webservice client")
            _webSvc = New MockClubSpeedService
        Else
            log.Info("Initializing actual Clubspeed webservice client")
            _webSvc = New ClubSpeedService
        End If

        log.Info("Checking If Clubspeed web service is available")
        If Not _webSvc.WebServiceAvailable(failReason) Then
            log.Error($"Clubspeed web service is not available: {failReason}")
            Throw New ApplicationException($"Clubspeed web service is not available: {failReason}")
        End If
        log.Info("Clubspeed Interface Initialized")

        Dim mapperConfig = New AutoMapper.MapperConfiguration(Sub(cfg)
                                                                  cfg.AddProfile(Of ModelToViewModelMappingProfile)()
                                                              End Sub)

        Dim theMapper As AutoMapper.IMapper = New AutoMapper.Mapper(mapperConfig)
        theMapper.ConfigurationProvider.AssertConfigurationIsValid()

        Dim theNotifier As IPopupNotifier = New DialogNotifier()

        'Initialize Dependency Injection***************
        Dim builder As New Autofac.ContainerBuilder

        builder.RegisterInstance(_configReader).As(Of IConfigReader).PropertiesAutowired
        builder.RegisterInstance(_webSvc).As(Of IClubSpeedService).PropertiesAutowired
        builder.RegisterInstance(theNotifier).As(Of IPopupNotifier).PropertiesAutowired
        builder.RegisterInstance(theMapper).As(Of AutoMapper.IMapper).PropertiesAutowired

        builder.RegisterType(Of SearchRacersPresenter).AsSelf.PropertiesAutowired  '(Of SearchRacersPresenter).PropertiesAutowired
        builder.RegisterType(Of SearchRacersView).As(Of ISearchRacersView).PropertiesAutowired
        builder.RegisterType(Of ChooseRacePresenter).AsSelf.PropertiesAutowired  '(Of ChooseRacePresenter).PropertiesAutowired
        builder.RegisterType(Of PickRace_v2View).As(Of IPickRaceView).PropertiesAutowired()
        builder.RegisterType(Of PickRacerView).As(Of IPickRacerView).PropertiesAutowired
        builder.RegisterType(Of NoRacersFoundView).As(Of INoRacersFoundView).PropertiesAutowired
        builder.RegisterType(Of UserSignupPresenter).AsSelf.PropertiesAutowired
        builder.RegisterType(Of UserSignupView).As(Of IUserSignupView).PropertiesAutowired

        'Seems that Property ConfigReader is never called for PhotoBoothView, so we pass it from 
        'IUserSignupDataForm's implementations at least until we UNDERSTAND WHY, but that is for the 
        'future; now this has to WORK. Maybe the property is "buried" beyond what Autofac/Automap 
        'can "see". Tof shit.
        'builder.RegisterType(Of PhotoBoothView).As(Of IUserSignupDataForm).PropertiesAutowired 

        Dim container = builder.Build
        '***********************************************

        log.Info("Initializing racer selector")

        _racersSelector = container.Resolve(Of SearchRacersPresenter)
        _raceSelector = container.Resolve(Of ChooseRacePresenter)

        log.Info("Clubspeed interface plugin initialization SUCCESS")

    End Sub

    Public ReadOnly Property LastErrorString As String
        Get
            Return _lastErrorText
        End Get
    End Property

    Public Function PurchaseProduct(ByVal parametersIn As IDictionary(Of String, String),
                                    ByRef parametersOut As IDictionary(Of String, String)
                                   ) As Boolean

        log.Info("PurchaseProduct Invoked")

        _lastErrorText = String.Empty
        parametersOut = New Dictionary(Of String, String)

        _racers = _racersSelector.ChooseRacers()

        If (_racers.Count = 0) Then
            _lastErrorText = "No racers selected, purchase aborted."
            log.Warn(_lastErrorText)
            Return (False)
        End If

        If (Not (_raceSelector.ChooseRace(_racers))) Then
            _lastErrorText = "Race selection canceled or no races available, purchase aborted."
            log.Warn(_lastErrorText)
            Return (False)
        End If

        _selectedHeat = _raceSelector.SelectedHeat

        parametersOut = _racers.Select(Function(value, index) New With {
                                                                            .theIndex = index,
                                                                            .theValue = value
                                                                       }
                                      ).ToDictionary(Function(i) $"RacerId_{(i.theIndex + 1).ToString()}",
                                                     Function(x) x.theValue.CustomerId.ToString()
                                                    )

        parametersOut.Add(New KeyValuePair(Of String, String)("Quantity", _racers.Count.ToString()))
        parametersOut.Add(New KeyValuePair(Of String, String)("HeatId", _selectedHeat.Id.ToString()))

        parametersOut.Add(New KeyValuePair(Of String, String)("ImmediateFulfill", True.ToString()))

        Return True

    End Function

    Public Function FulfillTransaction(ByVal parametersIn As IDictionary(Of String, String),
                                       ByRef parametersOut As IDictionary(Of String, Object)
                                      ) As Boolean

        Dim heatId As Integer = -1,
            heatMain As Models.HeatMain,
            customerList As New List(Of Customer),
            customer As Models.Customer,
            ticketGenerator As TicketGenerator

        log.Info("FulfillTransaction invoked")

        If (Not (parametersIn.ContainsKey("HeatId"))) Then
            'Throw New ArgumentException("HeatId not found within input parameters")
            _lastErrorText = "HeatId not found within input parameters, fulfillment aborted."
            log.Error(_lastErrorText)
            Return (False)
        End If

        log.Info("Heat Id is [" & parametersIn.Item("HeatId") & "]")

        If (Not (Integer.TryParse(parametersIn.Item("HeatId"), heatId))) Then
            'Throw New ArgumentException("HeatId parameter is not an integer number")
            _lastErrorText = "HeatId parameter is not an integer number, fulfillment aborted."
            log.Error(_lastErrorText)
            Return (False)
        End If

        heatMain = _webSvc.GetHeatMainById(heatId, False)

        If (heatMain Is Nothing) Then
            'Throw New ArgumentException($"HeatMain id {heatId.ToString()} does not exist")
            _lastErrorText = $"HeatMain id {heatId.ToString()} does not exist, fulfillment aborted."
            log.Error(_lastErrorText)
            Return (False)
        End If

        For Each param As KeyValuePair(Of String, String) In parametersIn
            If (param.Key.StartsWith("RacerId_")) Then
                customer = _webSvc.GetCustomerById(param.Value)
                If (customer Is Nothing) Then
                    'Throw New ArgumentException($"Customer id {param.Value.ToString()} was not found in DB")
                    _lastErrorText = $"Customer id {param.Value.ToString()} was not found in DB, fulfillment aborted."
                    log.Error(_lastErrorText)
                    Return (False)
                End If
                customerList.Add(customer)
            End If
        Next

        For Each customer In customerList
            log.Info($"Adding HeatDetail record for customer Id [{customer.CustomerId.ToString}]")
            Try
                _webSvc.AddHeatDetailRecord(heatId, customer.CustomerId)
            Catch ex As ApplicationException
                _lastErrorText = $"Could not add customer Id [{customer.CustomerId}] to heat Id [{heatId.ToString()}: {ex.Message}]."
                log.Error($"Could not add customer Id [{customer.CustomerId}] to heat Id [{heatId.ToString()}].", ex)
                Return (False)
            End Try
        Next

        log.Info($"Creating ticket(s) for {customerList.Count.ToString()} customers in Heat ID {heatId.ToString()}")
        ticketGenerator = New TicketGenerator
        parametersOut = New Dictionary(Of String, Object) From
            {
                {"Tickets", TicketGenerator.GetTicketsToPrint(customerList, heatMain)}
            }

        log.Info("Done.")
        Return (True)

    End Function

    Public Function RollbackTransaction(ByVal parametersIn As IDictionary(Of String, String),
                                        ByRef parametersOut As IDictionary(Of String, Object)
                                       ) As Boolean

        Dim heatId As Integer = -1,
            heatMain As Models.HeatMain,
            customerList As New List(Of Customer),
            customer As Models.Customer

        log.Info("RollbackTransaction invoked")

        If (Not (parametersIn.ContainsKey("HeatId"))) Then
            'Throw New ArgumentException("HeatId not found within input parameters")
            _lastErrorText = "HeatId not found within input parameters, rollback aborted."
            log.Error(_lastErrorText)
            Return (False)
        End If

        log.Info("Heat Id is [" & parametersIn.Item("HeatId") & "]")

        If (Not (Integer.TryParse(parametersIn.Item("HeatId"), heatId))) Then
            'Throw New ArgumentException("HeatId parameter is not an integer number")
            _lastErrorText = "HeatId parameter is not an integer number, rollback aborted."
            log.Error(_lastErrorText)
            Return (False)
        End If

        heatMain = _webSvc.GetHeatMainById(heatId, False)

        If (heatMain Is Nothing) Then
            'Throw New ArgumentException($"HeatMain id {heatId.ToString()} does not exist")
            _lastErrorText = $"HeatMain id {heatId.ToString()} does not exist, rollback aborted."
            log.Error(_lastErrorText)
            Return (False)
        End If

        For Each param As KeyValuePair(Of String, String) In parametersIn
            If (param.Key.StartsWith("RacerId_")) Then
                customer = _webSvc.GetCustomerById(param.Value)
                If (customer Is Nothing) Then
                    'Throw New ArgumentException($"Customer id {param.Value.ToString()} was not found in DB")
                    _lastErrorText = $"Customer id {param.Value.ToString()} was not found in DB, rollback aborted."
                    log.Error(_lastErrorText)
                    Return (False)
                End If
                customerList.Add(customer)
            End If
        Next

        For Each customer In customerList
            log.Info("Deleting HeatDetail record for customer Id [" & customer.CustomerId & "]")
            Try
                _webSvc.DeleteHeatDetailRecord(heatId, customer.CustomerId)
            Catch ex As ApplicationException
                _lastErrorText = $"Could not remove Customer Id [{customer.CustomerId}] from Heat Id [{heatId.ToString()}]: {ex.Message}."
                log.Error($"Could not remove customer Id [{customer.CustomerId}] from heat Id [{heatId.ToString()}].", ex)
                Return (False)
            End Try
        Next

        parametersOut = New Dictionary(Of String, Object)

        log.Info("Done.")
        Return (True)

    End Function

End Class
