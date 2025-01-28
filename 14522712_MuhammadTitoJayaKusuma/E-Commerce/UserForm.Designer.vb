<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserForm
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
        TextMessage = New Label()
        SuspendLayout()
        ' 
        ' TextMessage
        ' 
        TextMessage.AutoSize = True
        TextMessage.Location = New Point(260, 171)
        TextMessage.Name = "TextMessage"
        TextMessage.Size = New Size(63, 25)
        TextMessage.TabIndex = 0
        TextMessage.Text = "Label1"
        TextMessage.Visible = False
        ' 
        ' UserForm
        ' 
        AutoScaleMode = AutoScaleMode.None
        AutoScroll = True
        BackColor = Color.IndianRed
        ClientSize = New Size(800, 450)
        Controls.Add(TextMessage)
        FormBorderStyle = FormBorderStyle.None
        MaximizeBox = False
        MdiChildrenMinimizedAnchorBottom = False
        MinimizeBox = False
        Name = "UserForm"
        Text = "UserForm"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TextMessage As Label
End Class
