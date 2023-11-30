Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json

Class MainWindow
    Public Sub New()
        InitializeComponent()
    End Sub

    Dim connectionString As String = "Data Source=DESKTOP-R04PVQ3\SQLEXPRESS;Initial Catalog=wpfCrudDb; Integrated Security=True"
    Dim connection As New SqlConnection(connectionString)


    Private Sub updateButton_Click(sender As Object, e As RoutedEventArgs) Handles updateButton.Click
        If dataGrid.SelectedItem IsNot Nothing Then
            Dim selectedPerson As DataRowView = CType(dataGrid.SelectedItem, DataRowView)

            Dim result As MessageBoxResult = MessageBox.Show("Satırı güncellemek istiyor musun?", "Update Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)
            If result = MessageBoxResult.Yes Then
                Try
                    Using connection As New SqlConnection("Data Source=DESKTOP-R04PVQ3\SQLEXPRESS;Initial Catalog=wpfCrudDb; Integrated Security=True")
                        Dim updateCmd As New SqlCommand("UPDATE Cruds SET Name = @Name, LastName = @LastName, Age = @Age, Gender = @Gender WHERE Id = @Id", connection)
                        updateCmd.Parameters.AddWithValue("@Name", name_txt.Text)
                        updateCmd.Parameters.AddWithValue("@LastName", lastName_txt.Text)
                        updateCmd.Parameters.AddWithValue("@Age", age_txt.Text)
                        updateCmd.Parameters.AddWithValue("@Gender", gender_txt.Text)
                        updateCmd.Parameters.AddWithValue("@Id", selectedPerson("Id"))
                        connection.Open()
                        updateCmd.ExecuteNonQuery()
                        MessageBox.Show("Güncelleme işlemi başarılı.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
                        LoadDataGrid()
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                Finally
                    connection.Close()

                End Try
            End If
        Else
            MessageBox.Show("bir satır seç.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information)
        End If
    End Sub

    Private Sub deleteButton_Click(sender As Object, e As RoutedEventArgs) Handles deleteButton.Click

        Try
            Using connection As New SqlConnection("Data Source=DESKTOP-R04PVQ3\SQLEXPRESS;Initial Catalog=wpfCrudDb; Integrated Security=True")
                connection.Open()
                If dataGrid.SelectedItem IsNot Nothing Then
                    Dim selectedRow As DataRowView = dataGrid.SelectedItem
                    If selectedRow IsNot Nothing Then
                        Dim idToDelete As Integer = CInt(selectedRow("Id"))
                        Using cmd As New SqlCommand("DELETE FROM Cruds WHERE Id = @Id", connection)
                            cmd.Parameters.AddWithValue("@Id", idToDelete)
                            cmd.ExecuteNonQuery()
                        End Using
                        MessageBox.Show("Silindi", MessageBoxButton.OK)
                        LoadDataGrid()
                    Else
                        MessageBox.Show("seçilen satır geçerli değil.")
                    End If
                Else
                    MessageBox.Show("Silinecek satırı seç.")
                End If
            End Using ' This will automatically close the connection when exiting the Using block
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub clearButton_Click(sender As Object, e As RoutedEventArgs) Handles clearButton.Click
        name_txt.Clear()
        lastName_txt.Clear()
        age_txt.Clear()
    End Sub



    Private Sub addButton_Click(sender As Object, e As RoutedEventArgs) Handles addButton.Click

        If String.IsNullOrWhiteSpace(name_txt.Text) OrElse
       String.IsNullOrWhiteSpace(lastName_txt.Text) OrElse
       String.IsNullOrWhiteSpace(age_txt.Text) Then
            MessageBox.Show("Lütfen tüm alanları doldurun.", MessageBoxButton.OK)
            Return
        End If


        Dim existingRecord As Boolean = CheckExistingRecord(name_txt.Text, lastName_txt.Text)

        If existingRecord Then
            MessageBox.Show("Bu isim ve soyisimde bir kayıt zaten mevcut!", MessageBoxButton.OK)
        Else
            Dim cmd As New SqlCommand("INSERT INTO Cruds VALUES (@Name, @LastName, @Age, @Gender)", connection)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@Name", name_txt.Text)
            cmd.Parameters.AddWithValue("@LastName", lastName_txt.Text)
            cmd.Parameters.AddWithValue("@Age", age_txt.Text)

            Dim selectedGender As String = CType(gender_txt.SelectedItem, ComboBoxItem).Content.ToString()
            cmd.Parameters.AddWithValue("@Gender", selectedGender)

            connection.Open()
            cmd.ExecuteNonQuery()
            connection.Close()

            LoadDataGrid()

            MessageBox.Show("Başarıyla eklendi", MessageBoxButton.OK)
        End If

    End Sub

    Private Function CheckExistingRecord(name As String, lastName As String) As Boolean
        Dim cmd As New SqlCommand("SELECT COUNT(*) FROM Cruds WHERE Name = @Name AND LastName = @LastName", connection)
        cmd.CommandType = CommandType.Text
        cmd.Parameters.AddWithValue("@Name", name)
        cmd.Parameters.AddWithValue("@LastName", lastName)

        connection.Open()
        Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
        connection.Close()

        Return count > 0
    End Function

    'datagridte verileri listeliyor
    Private Sub LoadDataGrid()
        Try
            Dim cmd As New SqlCommand("select * from Cruds", connection)
            Dim dt As New DataTable()

            connection.Open()
            Dim sdr As SqlDataReader = cmd.ExecuteReader()
            dt.Load(sdr)
            connection.Close()

            dataGrid.ItemsSource = dt.DefaultView
        Catch ex As Exception
            MessageBox.Show("Hata oluştu: " & ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub


    'pencere güncellediğinde grid penceresi yenileniyor
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        LoadDataGrid()
    End Sub


    Private Sub TextBox_TextChanged_2(sender As Object, e As TextChangedEventArgs)

    End Sub


    'dataGrid satırı seçtiğimde textboxlara değeri atıyor
    Private Sub DataGrid_SelectionChanged_1(sender As Object, e As SelectionChangedEventArgs)
        If dataGrid.SelectedItem IsNot Nothing Then
            ' Get the selected DataRowView
            Dim selectedPerson As DataRowView = CType(dataGrid.SelectedItem, DataRowView)

            ' Verify that selectedPerson is not Nothing and contains the expected values
            If selectedPerson IsNot Nothing Then
                ' Get the values from the selected DataRowView
                Dim currentName As String = selectedPerson("Name").ToString()
                Dim currentLastName As String = selectedPerson("LastName").ToString()
                Dim currentAge As String = selectedPerson("Age").ToString()
                Dim currentGender As String = selectedPerson("Gender").ToString()

                ' Fill the textboxes with the values from the selected row
                name_txt.Text = currentName
                lastName_txt.Text = currentLastName
                age_txt.Text = currentAge
                gender_txt.Text = currentGender

            End If
        End If
    End Sub

End Class
