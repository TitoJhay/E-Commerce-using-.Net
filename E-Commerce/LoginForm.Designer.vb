<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        UsernameBox = New TextBox()
        PasswordBox = New TextBox()
        SignInButton = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(175, 143)
        Label1.Name = "Label1"
        Label1.Size = New Size(63, 25)
        Label1.TabIndex = 0
        Label1.Text = "LOGIN"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(43, 191)
        Label2.Name = "Label2"
        Label2.Size = New Size(91, 25)
        Label2.TabIndex = 1
        Label2.Text = "Username"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(43, 269)
        Label3.Name = "Label3"
        Label3.Size = New Size(87, 25)
        Label3.TabIndex = 2
        Label3.Text = "Password"
        ' 
        ' UsernameBox
        ' 
        UsernameBox.Location = New Point(47, 219)
        UsernameBox.Name = "UsernameBox"
        UsernameBox.PlaceholderText = "Username"
        UsernameBox.Size = New Size(324, 31)
        UsernameBox.TabIndex = 3
        ' 
        ' PasswordBox
        ' 
        PasswordBox.Location = New Point(47, 297)
        PasswordBox.Name = "PasswordBox"
        PasswordBox.PasswordChar = "*"c
        PasswordBox.PlaceholderText = "Password"
        PasswordBox.Size = New Size(324, 31)
        PasswordBox.TabIndex = 4
        PasswordBox.UseSystemPasswordChar = True
        ' 
        ' SignInButton
        ' 
        SignInButton.Location = New Point(47, 347)
        SignInButton.Name = "SignInButton"
        SignInButton.Size = New Size(324, 47)
        SignInButton.TabIndex = 5
        SignInButton.Text = "SIGN IN"
        SignInButton.UseVisualStyleBackColor = True
        ' 
        ' LoginForm
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(428, 422)
        Controls.Add(SignInButton)
        Controls.Add(PasswordBox)
        Controls.Add(UsernameBox)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Name = "LoginForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "LoginForm"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents UsernameBox As TextBox
    Friend WithEvents PasswordBox As TextBox
    Friend WithEvents SignInButton As Button
End Class
