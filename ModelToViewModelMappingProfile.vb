Imports DCS.KioskV15.ClubSpeedInterface.Models
Imports DCS.KioskV15.ClubSpeedInterface.Views

Public Class ModelToViewModelMappingProfile
    Inherits AutoMapper.Profile

    Public Sub New()
        CreateMap(Of Customer, RacerView).ForMember(Function(dest) dest.RacerId, Sub(opt) opt.MapFrom(Function(src) src.CustomerId))
    End Sub

End Class
