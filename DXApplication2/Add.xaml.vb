Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Windows.Controls
Imports System.Windows.Forms
Imports DevExpress.Xpf.Core
Imports DXApplication2.MainWindow
Imports Microsoft.VisualBasic.ApplicationServices
Imports Newtonsoft.Json

Class InsertPage
    Private ReadOnly _apiService As IApiService

    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(apiService As IApiService)
        _apiService = apiService
        InitializeComponent()
    End Sub
    Private Sub TextBox_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Async Sub Button_Click_1(sender As Object, e As RoutedEventArgs)

        Try
            Dim name As String = urunAdi.Text
            Dim categoryId As String = kategoriId.Text

            Dim apiUrl = "https://localhost:44304/api/values/AddProduct"

            ' Kategori nesnesi oluştur
            Dim product As New Product()
            product.Name = name
            product.CategoryId = categoryId


            ' HttpClient oluştur
            Using client As New HttpClient()
                ' JSON formatına dönüştür
                Dim json = JsonConvert.SerializeObject(product)
                Dim content = New StringContent(json, Encoding.UTF8, "application/json")

                ' POST isteği oluştur ve API'ye gönder
                Dim response = Await client.PostAsync(apiUrl, content)

                ' Yanıtı işle
                If response.IsSuccessStatusCode Then
                    ' Yanıt 200 OK ise
                    Dim responseString = Await response.Content.ReadAsStringAsync()
                    ' Yanıtı işleme
                    MessageBox.Show("İstek başarılı! Yanıt: " & responseString)
                Else
                    ' Hata durumunda
                    MessageBox.Show("İstek başarısız! Durum kodu: " & response.StatusCode.ToString())
                End If
            End Using
        Catch ex As Exception
            ' Hata durumunda
            MessageBox.Show("Hata oluştu: " & ex.Message)
        End Try


    End Sub
End Class
