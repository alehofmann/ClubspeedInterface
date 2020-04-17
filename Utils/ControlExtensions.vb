Imports System.Reflection
Imports System.Runtime.CompilerServices

Namespace Utils
    Module ControlExtensions
        <Extension>
        Public Function Clone(Of T As Control)(controlToClone As T) As T
            Dim controlProperties() As PropertyInfo = GetType(T).GetProperties(BindingFlags.Public Or BindingFlags.CreateInstance)

            Dim instance As T = Activator.CreateInstance(Of T)()

            For Each propInfo In controlProperties
                If propInfo.CanWrite Then
                    If propInfo.Name <> "WindowTarget" Then propInfo.SetValue(instance, propInfo.GetValue(controlToClone, Nothing), Nothing)
                End If
            Next
            Return instance

        End Function


    End Module
End Namespace