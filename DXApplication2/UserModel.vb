Imports System.Collections.ObjectModel

Public Class UserModel
    Public Property Id As Long
    Public Property FirstName As String
    Public Property LastName As String
    Public Property Email As String
    Public Property Phone As String
    Public Property Token As String
    Public Property AccountType As Integer
End Class

Public Class ViewModel
    Public Property Users As New ObservableCollection(Of UserModel)()
End Class
