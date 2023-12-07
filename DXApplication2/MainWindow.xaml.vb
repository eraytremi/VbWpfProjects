Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports DevExpress.Xpf.Core
Imports Microsoft.VisualBasic.ApplicationServices

''' <summary>
''' Interaction logic for MainWindow.xaml
''' </summary>
Partial Public Class MainWindow
    Inherits ThemedWindow

    Public Sub New()

    End Sub

    Private Async Sub DataGrid_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub

    Private Async Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Await GetAllUsers()
    End Sub

    Private Async Function GetAllUsers() As Task
        Try
            Using client As New HttpClient()
                Dim apiUrl As String = "https://localhost:44304/api/values/GetAll"
                Dim jsonContent As String = Text.Json.JsonSerializer.Serialize(apiUrl)
                Dim content As New StringContent(jsonContent, Encoding.UTF8, "application/json")
                Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)

                If response.IsSuccessStatusCode Then
                    Dim content1 As String = Await response.Content.ReadAsStringAsync()

                    Dim options As New JsonSerializerOptions() With {
                                .PropertyNameCaseInsensitive = True
    }

                    ' JSON'ı belirtilen türdeki nesneye dönüştür
                    Dim userList As List(Of Product) = JsonSerializer.Deserialize(Of List(Of Product))(content1, options)



                    dataGrid.ItemsSource = userList


                Else
                    MessageBox.Show("API isteği başarısız. Hata kodu: " & response.StatusCode)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Bir hata oluştu: " & ex.Message)
        End Try
    End Function

    Private Async Sub Button_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)

    End Sub


    Private Async Sub Add(sender As Object, e As RoutedEventArgs)

        Dim insertPage As New InsertPage()

        mainFrame.Navigate(insertPage)

    End Sub



    'Private Async Sub Add(sender As Object, e As RoutedEventArgs)
    '    Try
    '        Using client As New HttpClient()
    '            Dim apiUrl As String = "https://localhost:7257/api/Users"

    '            Dim userPostData As New UserPost()

    '            Dim jsonContent As String = Text.Json.JsonSerializer.Serialize(userPostData)
    '            Dim content As New StringContent(jsonContent, Encoding.UTF8, "application/json")

    '            Using response As HttpResponseMessage = Await client.PostAsync(apiUrl, content)
    '                If response.IsSuccessStatusCode Then
    '                    Dim content1 As String = Await response.Content.ReadAsStringAsync()

    '                    Dim options As New JsonSerializerOptions() With {
    '                    .PropertyNameCaseInsensitive = True
    '                }

    '                    Dim response1 As ResponseBody(Of UserPost) = JsonSerializer.Deserialize(Of ResponseBody(Of UserPost))(content1, options)
    '                Else
    '                    MessageBox.Show("API isteği başarısız. Hata kodu: " & response.StatusCode)
    '                End If
    '            End Using
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show("Bir hata oluştu: " & ex.Message)
    '    End Try
    'End Sub
End Class

