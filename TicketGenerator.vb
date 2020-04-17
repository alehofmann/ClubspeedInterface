Imports System.IO
Imports DCS.KioskV15.ClubSpeedInterface.Model

Public Class TicketGenerator

    Public Function GetTicketsToPrint(customers As IList(Of Customer), heat As HeatMain) As IList(Of String)

        Dim ticketsToPrint As New List(Of String),
            template = GetTemplate()

        For Each customer In customers

            Dim ticket = template.Replace("%FIRST_NAME%", customer.FirstName)

            ticket = ticket.Replace("%CUSTOMER_ID%", customer.CustomerId)
            ticket = ticket.Replace("%LAST_NAME%", customer.LastName)
            ticket = ticket.Replace("%RACE_ID%", heat.HeatId.ToString("###,###,##0"))
            ticket = ticket.Replace("%SCHED_DATE%", heat.ScheduledTime.ToShortDateString)
            ticket = ticket.Replace("%SCHED_TIME%", heat.ScheduledTime.ToShortTimeString)
            ticket = ticket.Replace("%TRACK_ID%", heat.Track.ToString())

            ticketsToPrint.Add(ticket)

        Next

        Return ticketsToPrint

    End Function

    Private Function GetTemplate() As String

        Dim nw = Environment.GetEnvironmentVariable("NW"),
            templatePath = Path.Combine(nw, "DCS\Config\ClubSpeed\Receipt.Txt")

        If Not File.Exists(templatePath) Then
            Throw New ApplicationException("Clubspeed receipt template not found: [" & templatePath & "]")
        End If

        Return File.ReadAllText(templatePath)

    End Function
End Class
