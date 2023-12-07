Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports System.Text.Json

Public Class ApiService
    Implements IApiService

    Public Async Function GetDataAsync(Of T)(endPoint As String, Optional token As String = Nothing) As Task(Of T) Implements IApiService.GetDataAsync
        Using client As New HttpClient()
            Dim requestMessage As New HttpRequestMessage() With {
                .Method = HttpMethod.Get,
                .RequestUri = New Uri($"https://localhost:44304/api/values{endPoint}")
            }
            requestMessage.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            If Not String.IsNullOrEmpty(token) Then
                requestMessage.Headers.Authorization = New AuthenticationHeaderValue("Bearer", token)
            End If

            Dim responseMessage = Await client.SendAsync(requestMessage)
            Dim jsonResponse = Await responseMessage.Content.ReadAsStringAsync()
            Return JsonSerializer.Deserialize(Of T)(jsonResponse, New JsonSerializerOptions() With {.PropertyNameCaseInsensitive = True})
        End Using
    End Function

    Public Async Function PostDataAsync(Of T)(endPoint As String, jsonData As String, Optional token As String = Nothing) As Task(Of T) Implements IApiService.PostDataAsync
        Using client As New HttpClient()
            Dim requestMessage As New HttpRequestMessage() With {
                .Method = HttpMethod.Post,
                .RequestUri = New Uri($"https://localhost:44304/api/values{endPoint}"),
                .Content = New StringContent(jsonData, Encoding.UTF8, "application/js   on")
            }
            requestMessage.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            If Not String.IsNullOrEmpty(token) Then
                requestMessage.Headers.Authorization = New AuthenticationHeaderValue("Bearer", token)
            End If

            Dim responseMessage = Await client.SendAsync(requestMessage)
            Dim jsonResponse = Await responseMessage.Content.ReadAsStringAsync()
            Return JsonSerializer.Deserialize(Of T)(jsonResponse, New JsonSerializerOptions() With {.PropertyNameCaseInsensitive = True})
        End Using
    End Function

    Public Async Function DeleteDataAsync(Of T)(endPoint As String, Optional token As String = Nothing) As Task(Of T) Implements IApiService.DeleteDataAsync
        Using client As New HttpClient()
            Dim requestMessage As New HttpRequestMessage() With {
                .Method = HttpMethod.Delete,
                .RequestUri = New Uri($"https://localhost:44304/api/values{endPoint}")
            }
            requestMessage.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            If Not String.IsNullOrEmpty(token) Then
                requestMessage.Headers.Authorization = New AuthenticationHeaderValue("Bearer", token)
            End If

            Dim responseMessage = Await client.SendAsync(requestMessage)
            Dim jsonResponse = Await responseMessage.Content.ReadAsStringAsync()
            Return JsonSerializer.Deserialize(Of T)(jsonResponse, New JsonSerializerOptions() With {.PropertyNameCaseInsensitive = True})
        End Using
    End Function

    Public Async Function PutDataAsync(Of T)(endPoint As String, jsonData As String, Optional token As String = Nothing) As Task(Of T) Implements IApiService.PutDataAsync
        Using client As New HttpClient()
            Dim requestMessage As New HttpRequestMessage() With {
                .Method = HttpMethod.Put,
                .RequestUri = New Uri($"https://localhost:44304/api/values{endPoint}"),
                .Content = New StringContent(jsonData, Encoding.UTF8, "application/json")
            }
            requestMessage.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            If Not String.IsNullOrEmpty(token) Then
                requestMessage.Headers.Authorization = New AuthenticationHeaderValue("Bearer", token)
            End If

            Dim responseMessage = Await client.SendAsync(requestMessage)
            Dim jsonResponse = Await responseMessage.Content.ReadAsStringAsync()
            Return JsonSerializer.Deserialize(Of T)(jsonResponse, New JsonSerializerOptions() With {.PropertyNameCaseInsensitive = True})
        End Using
    End Function
End Class
