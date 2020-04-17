Imports System.Net.Http
Imports System.Collections.ObjectModel
Imports DCS.KioskV15.ClubSpeedInterface.Models
Imports Newtonsoft.Json

Namespace Services

    Public Class ClubSpeedService

        Implements IClubSpeedService

        Private ReadOnly _iniFilename As String = Environment.ExpandEnvironmentVariables("%NW%") & "\DCS\Config\Clubspeed\Config.Ini"
        Private ReadOnly _baseAddress As String     '"https://vm-122.clubspeedtiming.com/api/index.php/"
        Private ReadOnly _apiKey As String          '"key=REF2J9qM3sAKRgNz4ugF"
        Private ReadOnly _log As log4net.ILog = log4net.LogManager.GetLogger(Reflection.MethodBase.GetCurrentMethod().DeclaringType)

        Public Sub New()

            'Here we read the INI to get the values to connect to ClubSpeed.
            _baseAddress = IniGet("WebService", "Base URL", String.Empty, _iniFilename).Trim
            _apiKey = IniGet("WebService", "API Key", String.Empty, _iniFilename).Trim

            If ((_baseAddress = String.Empty) Or
                (_apiKey = String.Empty)
               ) Then
                Throw New ApplicationException($"Configuration file {Me._iniFilename} lack keys [WebService] - 'Base URL' and/or 'API Key'; cannot continue.")
            End If

        End Sub

        Public Function WebServiceAvailable(Optional ByRef errorReason As String = "") As Boolean Implements IClubSpeedService.WebServiceAvailable

            Dim client As New System.Net.Http.HttpClient,
                response As HttpResponseMessage = Nothing

            _log.Info("Retrieving Clubspeed API version (" & _baseAddress & "version/api?" & _apiKey & ")")
            Try

                response = client.GetAsync(_baseAddress & "version/api?" & _apiKey, HttpCompletionOption.ResponseContentRead).Result

            Catch e As HttpRequestException
                errorReason = "Network error connecting to Clubspeed web service: " & e.Message
                Return False

            Catch e As AggregateException
                For Each ex In e.InnerExceptions
                    If ex.GetType = GetType(HttpRequestException) Then
                        errorReason = "Network error connecting to Clubspeed web service: " & ex.Message
                        Return False
                    Else
                        Throw ex
                    End If
                Next

            End Try

            If response.IsSuccessStatusCode Then
                _log.Info($"Clubspeed API Version is {response.Content}")
                Return True
            Else
                errorReason = $"Clubspeed web service returned status code {response.StatusCode}: '{response.ReasonPhrase()}')"
                Return False
            End If

        End Function

        Public Function GetCustomers() As IEnumerable(Of Customer) Implements IClubSpeedService.GetCustomers
            Return (GetCustomersByFirstLastOrEmail(String.Empty))
        End Function

        Public Function GetCustomerById(ByVal customerId As String) As Customer Implements IClubSpeedService.GetCustomerById

            Dim client As New HttpClient,
                response As HttpResponseMessage = client.GetAsync(_baseAddress & "customers?where={""customerId"": " & customerId & "}&" & _apiKey).Result

            'To have ".ReadAsAsync" one has to NuGet a package called "Microsoft.ASPNet.WebApi.Client"

            Dim result As IList(Of Models.Customer) = response.Content.ReadAsAsync(Of IList(Of Models.Customer)).Result

            If result.Count > 0 Then
                Return result.Item(0)
            Else
                Return Nothing
            End If

        End Function

        Public Function GetCustomersByFirstLastOrEmail(ByVal target As String) As IEnumerable(Of Customer) Implements IClubSpeedService.GetCustomersByFirstLastOrEmail

            'Conditions used:
            '  First Name can have the target string as a substring anywhere, and is caseless ("alE", "J" and "jan" will all match "Alejandro")
            '  Same goes for Last Name ("OP", "lop" and "Z" will all match "Lopez")
            '  Email, though, is different. It must BEGIN with the target string sought ("aho" will match "ahofmann@sacoa.com", but "@sacoa" will return no matches).

            Dim client As New HttpClient,
                filter As String = "{""$or"": [" &
                                                "{""firstname"": {""$has"": """ & target & """}}" &
                                                ", {""lastname"": {""$has"": """ & target & """}}" &
                                                ", {""email"": {""$lk"": """ & target & "%""}}" &
                                              "]" &
                                   "}",
                response As HttpResponseMessage = client.GetAsync(_baseAddress & "customers?where=" & filter & "&" & _apiKey).Result

            'To have ".ReadAsAsync" one has to NuGet a package called "Microsoft.ASPNet.WebApi.Client"
            Return (response.Content.ReadAsAsync(Of ObservableCollection(Of Models.Customer)).Result)

        End Function

        Public Async Function GetTodayRacesOld() As Task(Of IEnumerable(Of HeatMain)) 'Implements IClubSpeedService.GetTodayRaces

            'The filter will return all races scheduled between this moment and
            'today @ 23:59:59, whose StatusEnum is 0-Open. I'm just guessing that 
            'future races are "open" when created by default.

            Dim client As New HttpClient,
                from As Date = Now,
                fromText As String = from.ToString("yyyy\-MM\-dd\THH\:mm\:ss\.\0\0"),
                upto As Date = from.Date.AddDays(1).AddSeconds(-1),
                upToText As String = upto.ToString("yyyy\-MM\-dd\THH\:mm\:ss\.\0\0"),
                filter As String = "{""$and"": [" &
                                                 "{""scheduledTime"": {""$gte"": """ & fromText & """}}" &
                                                 ", {""scheduledTime"": {""$lte"": """ & upToText & """}}" &
                                                 ", {""status"": 0}" &
                                              "]" &
                                   "}",
                response As HttpResponseMessage = client.GetAsync($"{_baseAddress}heatMain?where={filter}&order=scheduledTime&{_apiKey}").Result

            Dim todayRaces = response.Content.ReadAsAsync(Of ObservableCollection(Of HeatMain)).Result,
                racesWithDetail As New ObservableCollection(Of HeatMain)

            Dim mark As Date = Now
            For Each race In todayRaces
                race.HeatDetails = Await GetHeatDetailsByRaceId(race.HeatId).ConfigureAwait(False)
                racesWithDetail.Add(race)
            Next
            'MsgBox(Now.Subtract(mark).TotalMilliseconds)

            Return (racesWithDetail)

        End Function

        Public Async Function GetTodayRaces() As Task(Of IEnumerable(Of HeatMain)) Implements IClubSpeedService.GetTodayRaces

            'The filter will return all races scheduled between this moment and
            'today @ 23:59:59, whose StatusEnum is 0-Open. I'm just guessing that 
            'future races are "open" when created by default.

            Dim client As New HttpClient,
                from As Date = Now,
                fromText As String = from.ToString("yyyy\-MM\-dd\THH\:mm\:ss\.\0\0"),
                upto As Date = from.Date.AddDays(1).AddSeconds(-1),
                upToText As String = upto.ToString("yyyy\-MM\-dd\THH\:mm\:ss\.\0\0"),
                filter As String = "{""$and"": [" &
                                                 "{""scheduledTime"": {""$gte"": """ & fromText & """}}" &
                                                 ", {""scheduledTime"": {""$lte"": """ & upToText & """}}" &
                                                 ", {""status"": 0}" &
                                              "]" &
                                   "}",
                response As HttpResponseMessage = client.GetAsync($"{_baseAddress}heatMain?where={filter}&order=scheduledTime&{_apiKey}").Result


            Dim todayRaces = response.Content.ReadAsAsync(Of ObservableCollection(Of Models.HeatMain)).Result
            Dim racesWithDetail As New ObservableCollection(Of HeatMain)


            Dim requestTasks = todayRaces.Select(Function(race) client.GetAsync(_baseAddress & "HeatDetails?where={""heatId"": " & race.HeatId.ToString() & "}&" & _apiKey))
            Dim mark As Date = Now

            'Parallel alternative (marginally faster)
            Dim responses = Await Task.WhenAll(requestTasks).ConfigureAwait(False)
            '*****************************
            'Secuential alternative (slower)
            'Dim responses As New List(Of HttpResponseMessage)
            'For Each task In requestTasks
            'responses.Add(Await task)
            'Next
            '*******************************
            For index = 0 To requestTasks.Count - 1
                todayRaces.Item(index).HeatDetails = responses(index).Content.ReadAsAsync(Of IList(Of HeatDetail)).Result
            Next

            Return todayRaces

        End Function

        Public Function GetHeatMainById(ByVal heatId As Integer,
                                        ByVal includeDetail As Boolean
                                       ) As HeatMain Implements IClubSpeedService.GetHeatMainById

            Dim client As New HttpClient,
                response As HttpResponseMessage = client.GetAsync(_baseAddress & "heatMain?where={""heatId"": " & heatId & "}&" & _apiKey).Result,
                results As IList(Of HeatMain) = response.Content.ReadAsAsync(Of IList(Of HeatMain)).Result

            If (results.Count > 0) Then
                'throw ex
            ElseIf (results.Count = 0) Then
                Return Nothing
            End If

            Dim heatMain As HeatMain = results.Item(0)

            If (includeDetail) Then
                heatMain.HeatDetails = GetHeatDetailsByRaceId(heatMain.HeatId)
            End If

            Return heatMain

        End Function

        Public Function GetRaceTypes() As IEnumerable(Of HeatType) Implements IClubSpeedService.GetRaceTypes

            Dim client As New HttpClient,
                response As HttpResponseMessage = client.GetAsync(_baseAddress & "heatTypes?" & _apiKey).Result

            Return (response.Content.ReadAsAsync(Of ObservableCollection(Of HeatType)).Result)

        End Function

        Public Function GetHeatDetailsCountByRaceId(ByVal raceId As Integer) As Integer Implements IClubSpeedService.GetHeatDetailsCountByRaceId

            Dim client As New HttpClient,
                response As HttpResponseMessage = client.GetAsync(_baseAddress & "HeatDetails/Count?where={""heatId"": " & raceId.ToString() & "}&" & _apiKey).Result,
                count = response.Content.ReadAsAsync(Of Integer).Result

            Return (count)

        End Function

        Public Async Function GetHeatDetailsByRaceId(ByVal raceId As Integer) As Task(Of IList(Of HeatDetail)) Implements IClubSpeedService.GetHeatDetailsByRaceId

            Dim client As New HttpClient,
                response As HttpResponseMessage = Await client.GetAsync(_baseAddress & "HeatDetails?where={""heatId"": " & raceId.ToString() & "}&" & _apiKey).ConfigureAwait(False)

            Return Await response.Content.ReadAsAsync(Of IList(Of HeatDetail)).ConfigureAwait(False)

        End Function

        Public Sub AddHeatDetailRecord(ByVal heatId As Integer,
                                       ByVal customerId As Integer
                                      ) Implements IClubSpeedService.AddHeatDetailRecord

            Dim heatDetail As New HeatDetail(heatId, customerId),
                client As New HttpClient,
                postBody As String = JsonConvert.SerializeObject(heatDetail,
                                                                 Formatting.None,
                                                                 New JsonSerializerSettings() With
                                                                     {
                                                                         .NullValueHandling = NullValueHandling.Ignore
                                                                     }
                                                                ),
                request As New StringContent(postBody, Text.Encoding.UTF8, "application/json"),
                response As HttpResponseMessage

            Try
                response = client.PostAsync(Me._baseAddress & "heatDetails?" & Me._apiKey, request).Result
            Catch ex As HttpRequestException
                Throw New ApplicationException("Network error while trying to post to Clubspeed Web Service", ex)
            End Try

            If (Not (response.IsSuccessStatusCode)) Then
                Throw New ApplicationException("Clubspeed returned a failure StatusEnum code: " & response.StatusCode & " (" & response.ReasonPhrase & ")")
            End If

        End Sub

        Public Function DeleteHeatDetailRecord(ByVal heatId As Integer,
                                               ByVal customerId As Integer
                                              ) As Boolean Implements IClubSpeedService.DeleteHeatDetailRecord

            Dim client As New HttpClient,
                deleteURL As String = Me._baseAddress & $"heatDetails/{heatId.ToString()}/{customerId.ToString()}?" & Me._apiKey,
                response As HttpResponseMessage

            Try
                response = client.DeleteAsync(deleteURL).Result
            Catch ex As HttpRequestException
                Throw New ApplicationException("Network error while trying to delete from Clubspeed Web Service", ex)
            End Try

            If (Not (response.IsSuccessStatusCode)) Then
                Throw New ApplicationException("Clubspeed returned a failure StatusEnum code: " & response.StatusCode & " (" & response.ReasonPhrase & ")")
            End If

            Return (response.IsSuccessStatusCode)

        End Function

        Public Function GetCustomersByPhoneNumber(ByVal target As String) As IEnumerable(Of Customer) Implements IClubSpeedService.GetCustomersByPhoneNumber

            Return New ObservableCollection(Of Customer)

        End Function

        Public Sub RegisterCustomer(ByVal newCustomer As Customer) Implements IClubSpeedService.AddCustomer

            '{    
            '     "email": "gloat@clubspeed.com",
            '     "firstname": "Bart",
            '     "lastname": "Simpson",
            '     "birthdate": "1982-04-23",                'Will also accept time part appended as "T00:00:00.00"
            '     "mobilephone": "123-456-7890"             'is OK, but API accepts "(123)456-7890"
            '     "profilephoto": "data:image/jpg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/..."
            '}

            Dim client As New HttpClient,
                postBody As String = JsonConvert.SerializeObject(newCustomer,
                                                                 Formatting.None,
                                                                 New JsonSerializerSettings() With
                                                                     {
                                                                         .NullValueHandling = NullValueHandling.Ignore,
                                                                         .MissingMemberHandling = MissingMemberHandling.Ignore
                                                                     }
                                                                ),
                request As New StringContent(postBody,
                                             Text.Encoding.UTF8,
                                             "application/json"
                                            ),
                response As HttpResponseMessage

            Try
                response = client.PostAsync(_baseAddress & "customers?" & _apiKey, request).Result
            Catch ex As HttpRequestException
                Throw New ApplicationException("Network error while trying to post to Clubspeed Web Service", ex)
            End Try

            If (response.IsSuccessStatusCode) Then
                newCustomer.CustomerId = CInt(response.Content.ReadAsStringAsync().Result)
            Else
                Throw New ApplicationException("Clubspeed returned a failure StatusEnum code: " & response.StatusCode & " (" & response.ReasonPhrase & ")")
            End If

        End Sub

    End Class

End Namespace