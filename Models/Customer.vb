Imports DCS.KioskV15.ClubSpeedInterface.Views

Namespace Models

    Public Class Customer

        Implements IEquatable(Of Customer)

        <Newtonsoft.Json.JsonProperty(PropertyName:="customerId")>
        Public Property CustomerId As Integer? = Nothing

        <Newtonsoft.Json.JsonProperty(PropertyName:="firstname")>
        Public Property FirstName As String = ""

        <Newtonsoft.Json.JsonProperty(PropertyName:="lastname")>
        Public Property LastName As String = ""

        <Newtonsoft.Json.JsonProperty(PropertyName:="racername")>
        Public Property RacerName As String = ""

        <Newtonsoft.Json.JsonProperty(PropertyName:="birthdate")>
        Public Property BirthDate As Date = New Date(1900, 1, 1)

        <Newtonsoft.Json.JsonProperty(PropertyName:="mobilephone")>
        Public Property MobilePhone As String = ""

        <Newtonsoft.Json.JsonProperty(PropertyName:="email")>
        Public Property EMail As String = ""

        <Newtonsoft.Json.JsonProperty(PropertyName:="profilephoto")>
        Public Property PhotoBase64String As String = ""

        Public Overrides Function ToString() As String
            Return ($"{FirstName} {LastName}")
        End Function

        Public Overloads Function Equals(ByVal Other As Customer) As Boolean Implements IEquatable(Of Customer).Equals
            Return (CustomerId = Other.CustomerId)
        End Function

        Public Shared Narrowing Operator CType(ByVal customer As Customer) As RacerView

            Return (New RacerView(customer.CustomerId,
                                  customer.FirstName,
                                  customer.LastName
                                 )
                   )

        End Operator

    End Class

End Namespace