Imports MySql.Data.MySqlClient
Public Class LoginForm
    Dim db As New DatabaseConnection
    Dim connection = db.OpenConnection()
    Public Function ValidateLogin(username As String, password As String) As (Boolean, String)
        Try
            Dim query As String = "SELECT role FROM users WHERE username = @username AND password = SHA2(@password, 256)"
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@username", username)
            command.Parameters.AddWithValue("@password", password)

            Dim role As String = Convert.ToString(command.ExecuteScalar())

            If Not String.IsNullOrEmpty(role) Then
                Return (True, role) ' Login berhasil, kembalikan role
            End If

            Return (False, Nothing)
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat login: " & ex.Message)
            Return (False, Nothing)
        End Try
    End Function
    Private Sub SignInButton_Click(sender As Object, e As EventArgs) Handles SignInButton.Click
        Dim username As String = UsernameBox.Text
        Dim password As String = PasswordBox.Text

        Dim LoginResult = ValidateLogin(username, password)

        Dim isLoginValid As Boolean = LoginResult.Item1
        Dim role As String = LoginResult.Item2

        If isLoginValid Then
            Dim mainForm As New Main()
            mainForm.userRole = role
            mainForm.Show()
            Me.Hide()
        Else
            MessageBox.Show("Login gagal. Username atau password salah.")
        End If
    End Sub
End Class