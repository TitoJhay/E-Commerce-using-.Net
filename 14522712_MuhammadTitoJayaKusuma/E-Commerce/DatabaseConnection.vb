Imports MySql.Data.MySqlClient
Public Class DatabaseConnection
    Private ConnectionString As String = "Server=localhost;Database=ecommerce;Uid=root;Pwd=;"
    Private Connection As MySqlConnection

    Public Function OpenConnection() As MySqlConnection
        Try
            Connection = New MySqlConnection(ConnectionString)
            Connection.Open()
            Return Connection
        Catch ex As Exception
            MessageBox.Show("Gagal terhubung ke database: " & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Sub CloseConnection()
        Try
            If Connection IsNot Nothing AndAlso Connection.State = ConnectionState.Open Then
                Connection.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Gagal menutup koneksi: " & ex.Message)
        End Try
    End Sub
End Class
