Imports System.IO
Imports MySql.Data.MySqlClient

Public Class UserForm
    Dim db As New DatabaseConnection()
    Dim connection As MySqlConnection
    Dim destination As String = Path.Combine(Application.StartupPath, "Images")

    Private Sub UserForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProducts()
    End Sub

    Private Sub LoadProducts()
        Try
            connection = db.OpenConnection
            If connection IsNot Nothing Then
                Dim query As String = "SELECT * FROM products"
                Dim command As New MySqlCommand(query, connection)
                Dim reader As MySqlDataReader = command.ExecuteReader()

                Dim yPosition As Integer = 10
                Dim hasData As Boolean = False

                While reader.Read()
                    hasData = True

                    Dim productName As String = reader("name").ToString()
                    Dim productPrice As Decimal = Convert.ToDecimal(reader("price"))
                    Dim productStock As Integer = Convert.ToInt32(reader("stock"))
                    Dim imagePath As String = Path.Combine(destination, Path.GetFileName(reader("img").ToString()))

                    Dim productImage As New PictureBox()
                    productImage.Size = New Size(100, 100)
                    productImage.Location = New Point(10, yPosition)
                    productImage.SizeMode = PictureBoxSizeMode.StretchImage
                    If File.Exists(imagePath) Then
                        productImage.Image = Image.FromFile(imagePath)
                    End If
                    Me.Controls.Add(productImage)

                    Dim nameLabel As New Label()
                    nameLabel.Text = "Nama: " & productName
                    nameLabel.ForeColor = Color.White
                    nameLabel.Location = New Point(120, yPosition)
                    nameLabel.AutoSize = True
                    Me.Controls.Add(nameLabel)

                    Dim priceLabel As New Label()
                    priceLabel.Text = "Harga: Rp " & productPrice.ToString("N0")
                    priceLabel.ForeColor = Color.White
                    priceLabel.Location = New Point(120, yPosition + 20)
                    priceLabel.AutoSize = True
                    Me.Controls.Add(priceLabel)

                    Dim quantityInput As New NumericUpDown()
                    quantityInput.Minimum = 1

                    If productStock > 0 Then
                        quantityInput.Maximum = productStock
                        quantityInput.Value = 1
                    Else
                        quantityInput.Maximum = 0
                        quantityInput.Minimum = 0
                        quantityInput.Value = 0
                        quantityInput.Enabled = False
                    End If

                    quantityInput.Location = New Point(120, yPosition + 40)
                    quantityInput.Width = 80
                    Me.Controls.Add(quantityInput)

                    Dim buyButton As New Button()
                    buyButton.Text = "Beli"
                    buyButton.BackColor = Color.White
                    buyButton.Location = New Point(120, yPosition + 70)
                    buyButton.Size = New Size(80, 30)

                    If productStock = 0 Then
                        buyButton.Enabled = False
                        buyButton.Text = "Habis"
                    Else
                        AddHandler buyButton.Click, Sub(sender As Object, e As EventArgs)
                                                        HandlePurchase(productName, productPrice, quantityInput.Value, productStock)
                                                    End Sub
                    End If
                    Me.Controls.Add(buyButton)

                    yPosition += 120
                End While

                reader.Close()

                If Not hasData Then
                    TextMessage.Visible = True
                    TextMessage.Text = "Tidak ada data produk tersedia."
                    TextMessage.ForeColor = Color.Red
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Gagal memuat data produk: " & ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            db.CloseConnection()
        End Try
    End Sub

    Private Sub HandlePurchase(productName As String, productPrice As Decimal, quantity As Decimal, productStock As Integer)
        If quantity <= productStock Then
            Dim totalPrice As Decimal = productPrice * quantity

            Try
                connection = db.OpenConnection()
                If connection IsNot Nothing Then
                    Dim newStock As Integer = productStock - quantity
                    Dim query As String = "UPDATE products SET stock = @newStock WHERE name = @productName"
                    Dim command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@newStock", newStock)
                    command.Parameters.AddWithValue("@productName", productName)

                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show($"Anda membeli {quantity} {productName} dengan total harga Rp {totalPrice.ToString("N0")}.", "Pembelian Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadProducts()
                    Else
                        MessageBox.Show("Gagal memperbarui stok produk.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Terjadi kesalahan saat memperbarui stok: " & ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                db.CloseConnection()
            End Try
        Else
            MessageBox.Show($"Stok {productName} tidak mencukupi untuk jumlah yang dipilih.", "Pembelian Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

End Class