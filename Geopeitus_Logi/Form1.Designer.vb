<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.WB_MainAction = New System.Windows.Forms.WebBrowser()
        Me.Btn_Fire = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.LogBox = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'WB_MainAction
        '
        Me.WB_MainAction.Location = New System.Drawing.Point(555, 83)
        Me.WB_MainAction.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WB_MainAction.Name = "WB_MainAction"
        Me.WB_MainAction.ScriptErrorsSuppressed = True
        Me.WB_MainAction.Size = New System.Drawing.Size(377, 506)
        Me.WB_MainAction.TabIndex = 1
        '
        'Btn_Fire
        '
        Me.Btn_Fire.Location = New System.Drawing.Point(682, 12)
        Me.Btn_Fire.Name = "Btn_Fire"
        Me.Btn_Fire.Size = New System.Drawing.Size(250, 65)
        Me.Btn_Fire.TabIndex = 2
        Me.Btn_Fire.Text = "Anna tuld!"
        Me.Btn_Fire.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(426, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(250, 65)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Test"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'LogBox
        '
        Me.LogBox.HideSelection = False
        Me.LogBox.Location = New System.Drawing.Point(12, 83)
        Me.LogBox.Name = "LogBox"
        Me.LogBox.ReadOnly = True
        Me.LogBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.LogBox.Size = New System.Drawing.Size(537, 506)
        Me.LogBox.TabIndex = 4
        Me.LogBox.Text = ""
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(944, 601)
        Me.Controls.Add(Me.LogBox)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Btn_Fire)
        Me.Controls.Add(Me.WB_MainAction)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Main"
        Me.Text = "Geopeituses 1000 aarde logimine võib osutuda üsna tüütuks"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WB_MainAction As WebBrowser
    Friend WithEvents Btn_Fire As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents LogBox As RichTextBox
End Class
