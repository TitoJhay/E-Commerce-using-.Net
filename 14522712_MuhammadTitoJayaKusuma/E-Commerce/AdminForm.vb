Imports System.IO
Imports MySql.Data.MySqlClient
Public Class AdminForm
    Dim db As New DatabaseConnection()
    Dim connection As MySqlConnection
    Private selectedFilePath As String

    Private Sub AdminForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProducts()
        ListProducts.ClearSelection()
    End Sub

    Private Sub AdminForm_MouseClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseClick
        If Not ListProducts.ClientRectangle.Contains(ListProducts.PointToClient(Cursor.Position)) Then
            ListProducts.ClearSelection()
            ClearInputs()
        End If
    End Sub

    Private Sub LoadProducts()
        Try
            connection = db.OpenConnection
            If connection IsNot Nothing Then
                Dim query As String = "SELECT id, name, price, stock FROM products"
                Dim command As New MySqlCommand(query, connection)
                Dim reader As MySqlDataReader = command.ExecuteReader()

                ListProducts.Rows.Clear()

                Dim no As Integer = 1
                While reader.Read()
                    ListProducts.Rows.Add(reader("id"), no, reader("name"), reader("price"), reader("stock"))
                    no += 1
                End While

                reader.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Gagal memuat data produk: " & ex.Message)
        Finally
            db.CloseConnection()
        End Try
    End Sub

    Private Sub ListProducts_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles ListProducts.CellClick
        Try
            If e.RowIndex >= 0 Then
                Dim selectedRow As DataGridViewRow = ListProducts.Rows(e.RowIndex)
                Dim countSelected As Integer = ListProducts.SelectedRows.Count

                UpdateButton.Enabled = (countSelected = 1)

                If countSelected = 1 Then
                    ProductNameBox.Text = selectedRow.Cells("product_name").Value.ToString()
                    PriceBox.Text = selectedRow.Cells("price").Value.ToString()
                    StockBox.Text = selectedRow.Cells("stock").Value.ToString()

                    CheckRemoveButton()
                Else
                    ClearInputs()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Produk tidak ditemukan: ", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            db.CloseConnection()
        End Try
    End Sub

    Private Sub ListProducts_SelectionChanged(sender As Object, e As EventArgs) Handles ListProducts.SelectionChanged
        CheckRemoveButton()
        CheckUpdateButton()
    End Sub

    Private Sub ClearInputs()
        ProductNameBox.Clear()
        PriceBox.Clear()
        StockBox.Clear()
    End Sub

    Private Function IsIdExist() As Boolean
        If ListProducts.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = ListProducts.SelectedRows(0)
            Dim idCellValue = selectedRow.Cells(0).Value

            If idCellValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(idCellValue.ToString()) Then
                Return True
            End If
        End If

        Return False
    End Function


    Private Sub CheckInputs()
        Dim IsInputEmpty As Boolean = Not (String.IsNullOrEmpty(ProductNameBox.Text) OrElse
                             String.IsNullOrEmpty(PriceBox.Text) OrElse
                             String.IsNullOrEmpty(StockBox.Text)) OrElse
                             String.IsNullOrEmpty(selectedFilePath)

        AddButton.Enabled = IsInputEmpty And Not IsIdExist()
        UpdateButton.Enabled = IsInputEmpty And IsIdExist()
    End Sub
    Private Sub CheckRemoveButton()
        RemoveButton.Enabled = ListProducts.SelectedRows.Count > 0
    End Sub

    Private Sub CheckUpdateButton()
        UpdateButton.Enabled = ListProducts.SelectedRows.Count > 0
    End Sub

    Private Sub AddButton_Click(sender As Object, e As EventArgs) Handles AddButton.Click
        If String.IsNullOrEmpty(ProductNameBox.Text) OrElse String.IsNullOrEmpty(PriceBox.Text) OrElse String.IsNullOrEmpty(StockBox.Text) Then
            MessageBox.Show("Semua kolom harus diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrEmpty(selectedFilePath) Then
            MessageBox.Show("Pilih file gambar terlebih dahulu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Dim name As String = ProductNameBox.Text
            Dim price As Integer = Convert.ToInt32(PriceBox.Text)
            Dim stock As Integer = Convert.ToInt32(StockBox.Text)

            connection = db.OpenConnection

            If connection IsNot Nothing Then
                Dim destinationFolder As String = Path.Combine(Application.StartupPath, "Images")
                If Not Directory.Exists(destinationFolder) Then
                    Directory.CreateDirectory(destinationFolder)
                End If

                Dim destinationPath As String = Path.Combine(destinationFolder, Path.GetFileName(selectedFilePath))

                File.Copy(selectedFilePath, destinationPath, overwrite:=True)

                Dim query As String = "INSERT INTO products (name, price, img, stock) VALUES (@name, @price, @img, @stock)"
                Dim command As New MySqlCommand(query, connection)

                command.Parameters.AddWithValue("@name", name)
                command.Parameters.AddWithValue("@price", price)
                command.Parameters.AddWithValue("@stock", stock)
                command.Parameters.AddWithValue("@img", Path.GetFileName(selectedFilePath))

                Dim RowsAffected As Integer = command.ExecuteNonQuery()

                If RowsAffected > 0 Then
                    MessageBox.Show("Produk berhasil ditambahkan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadProducts() ' Refresh daftar produk
                    ClearInputs()  ' Bersihkan input
                Else
                    MessageBox.Show("Gagal menambahkan produk.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            db.CloseConnection() ' Tutup koneksi database
        End Try
    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
        If ListProducts.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = ListProducts.SelectedRows(0)

            Dim id As Integer = Convert.ToInt32(selectedRow.Cells("id").Value)
            Dim name As String = ProductNameBox.Text
            Dim price As Decimal
            Dim stock As Integer

            If IsIdExist() And (String.IsNullOrEmpty(name) OrElse Not Decimal.TryParse(PriceBox.Text, price) OrElse Not Integer.TryParse(StockBox.Text, stock)) Then
                MessageBox.Show("Pastikan semua kolom diisi dengan benar.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Try
                connection = db.OpenConnection()
                If connection IsNot Nothing Then
                    Dim query As String = "UPDATE products SET name = @name, price = @price, stock = @stock WHERE id = @id"
                    Dim command As New MySqlCommand(query, connection)

                    command.Parameters.AddWithValue("@id", id)
                    command.Parameters.AddWithValue("@name", name)
                    command.Parameters.AddWithValue("@price", price)
                    command.Parameters.AddWithValue("@stock", stock)

                    Dim rowsAffected As Integer = command.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        MessageBox.Show("Produk berhasil diperbarui.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadProducts()
                        ClearInputs()
                    Else
                        MessageBox.Show("Tidak ada perubahan yang disimpan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Terjadi kesalahan saat menyimpan perubahan: " & ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                db.CloseConnection()
            End Try
        Else
            MessageBox.Show("Pilih produk yang ingin diperbarui.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub RemoveButton_Click(sender As Object, e As EventArgs) Handles RemoveButton.Click
        If ListProducts.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih produk yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim confirmation = MessageBox.Show("Apakah Anda yakin ingin menghapus produk yang dipilih?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.No Then Return

        Try
            connection = db.OpenConnection()
            If connection IsNot Nothing Then
                Dim transaction = connection.BeginTransaction()
                Try
                    For Each selectedRow As DataGridViewRow In ListProducts.SelectedRows
                        Dim id As Object = selectedRow.Cells(0).Value
                        If id IsNot Nothing Then
                            Dim query As String = "DELETE FROM products WHERE id = @id"
                            Dim command As New MySqlCommand(query, connection, transaction)
                            command.Parameters.AddWithValue("@id", id)
                            command.ExecuteNonQuery()
                        End If
                    Next
                    transaction.Commit()
                    MessageBox.Show("Produk yang dipilih berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadProducts()
                    ClearInputs()
                Catch ex As Exception
                    transaction.Rollback()
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat menghapus produk: " & ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            db.CloseConnection()
        End Try
    End Sub


    Private Sub ProductNameBox_TextChanged(sender As Object, e As EventArgs) Handles ProductNameBox.TextChanged
        CheckInputs()
    End Sub

    Private Sub PriceBox_TextChanged(sender As Object, e As EventArgs) Handles PriceBox.TextChanged
        CheckInputs()
    End Sub

    Private Sub StockBox_TextChanged(sender As Object, e As EventArgs) Handles StockBox.TextChanged
        CheckInputs()
    End Sub

    Private Sub SelectImage_Click(sender As Object, e As EventArgs) Handles SelectImage.Click
        OpenFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = OpenFileDialog1.FileName
            selectedFilePath = filePath
            Me.PictureSelected.Text = Path.GetFileName(selectedFilePath)
        End If
    End Sub

End Class
