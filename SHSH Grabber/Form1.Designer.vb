<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GrabECIDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator()
        Me.SendACertificateToOurServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadABlobFromOurServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenDatasFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenSourceOnGitHubToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ReportBugsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator()
        Me.CreditsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator()
        Me.RestartApplicationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SHSHProgressBar = New System.Windows.Forms.ProgressBar()
        Me.SaveSHSHButton = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(503, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.GrabECIDToolStripMenuItem, Me.ToolStripMenuItem7, Me.SendACertificateToOurServerToolStripMenuItem, Me.DownloadABlobFromOurServerToolStripMenuItem, Me.ToolStripMenuItem4, Me.OpenDatasFolderToolStripMenuItem, Me.ToolStripMenuItem3, Me.OpenSourceOnGitHubToolStripMenuItem, Me.ToolStripMenuItem2, Me.ReportBugsToolStripMenuItem, Me.ToolStripMenuItem5, Me.CreditsToolStripMenuItem, Me.ToolStripMenuItem6, Me.RestartApplicationToolStripMenuItem, Me.QuitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem
        '
        Me.AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem.Checked = True
        Me.AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem.CheckOnClick = True
        Me.AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem.Name = "AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem"
        Me.AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem.Size = New System.Drawing.Size(380, 22)
        Me.AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem.Text = "Always show signed firmware for selected device"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(380, 22)
        Me.OptionsToolStripMenuItem.Text = "Device information"
        '
        'GrabECIDToolStripMenuItem
        '
        Me.GrabECIDToolStripMenuItem.Name = "GrabECIDToolStripMenuItem"
        Me.GrabECIDToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.GrabECIDToolStripMenuItem.Size = New System.Drawing.Size(380, 22)
        Me.GrabECIDToolStripMenuItem.Text = "ECID Grabber"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(377, 6)
        '
        'SendACertificateToOurServerToolStripMenuItem
        '
        Me.SendACertificateToOurServerToolStripMenuItem.Name = "SendACertificateToOurServerToolStripMenuItem"
        Me.SendACertificateToOurServerToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.SendACertificateToOurServerToolStripMenuItem.Size = New System.Drawing.Size(380, 22)
        Me.SendACertificateToOurServerToolStripMenuItem.Text = "Send a certificate to our server"
        '
        'DownloadABlobFromOurServerToolStripMenuItem
        '
        Me.DownloadABlobFromOurServerToolStripMenuItem.Name = "DownloadABlobFromOurServerToolStripMenuItem"
        Me.DownloadABlobFromOurServerToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.DownloadABlobFromOurServerToolStripMenuItem.Size = New System.Drawing.Size(380, 22)
        Me.DownloadABlobFromOurServerToolStripMenuItem.Text = "Download a blob from our server"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(377, 6)
        '
        'OpenDatasFolderToolStripMenuItem
        '
        Me.OpenDatasFolderToolStripMenuItem.Name = "OpenDatasFolderToolStripMenuItem"
        Me.OpenDatasFolderToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.OpenDatasFolderToolStripMenuItem.Size = New System.Drawing.Size(380, 22)
        Me.OpenDatasFolderToolStripMenuItem.Text = "Open data's folder"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(377, 6)
        '
        'OpenSourceOnGitHubToolStripMenuItem
        '
        Me.OpenSourceOnGitHubToolStripMenuItem.Name = "OpenSourceOnGitHubToolStripMenuItem"
        Me.OpenSourceOnGitHubToolStripMenuItem.Size = New System.Drawing.Size(380, 22)
        Me.OpenSourceOnGitHubToolStripMenuItem.Text = "Open Source on GitHub"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(377, 6)
        '
        'ReportBugsToolStripMenuItem
        '
        Me.ReportBugsToolStripMenuItem.Name = "ReportBugsToolStripMenuItem"
        Me.ReportBugsToolStripMenuItem.Size = New System.Drawing.Size(380, 22)
        Me.ReportBugsToolStripMenuItem.Text = "Report bugs/improvements"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(377, 6)
        '
        'CreditsToolStripMenuItem
        '
        Me.CreditsToolStripMenuItem.Name = "CreditsToolStripMenuItem"
        Me.CreditsToolStripMenuItem.Size = New System.Drawing.Size(380, 22)
        Me.CreditsToolStripMenuItem.Text = "Credits"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(377, 6)
        '
        'RestartApplicationToolStripMenuItem
        '
        Me.RestartApplicationToolStripMenuItem.Name = "RestartApplicationToolStripMenuItem"
        Me.RestartApplicationToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.RestartApplicationToolStripMenuItem.Size = New System.Drawing.Size(380, 22)
        Me.RestartApplicationToolStripMenuItem.Text = "Restart application"
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.ShortcutKeyDisplayString = ""
        Me.QuitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(380, 22)
        Me.QuitToolStripMenuItem.Text = "Quit"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(44, 20)
        Me.ToolStripMenuItem1.Text = "Help"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button5)
        Me.GroupBox1.Controls.Add(Me.ComboBox1)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(260, 76)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select device"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(192, 17)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(62, 23)
        Me.Button5.TabIndex = 3
        Me.Button5.Text = "Refresh"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(6, 19)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(180, 21)
        Me.ComboBox1.TabIndex = 2
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(133, 46)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(121, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Delete device"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(6, 46)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(121, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Add device"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'SHSHProgressBar
        '
        Me.SHSHProgressBar.Location = New System.Drawing.Point(12, 192)
        Me.SHSHProgressBar.Name = "SHSHProgressBar"
        Me.SHSHProgressBar.Size = New System.Drawing.Size(398, 23)
        Me.SHSHProgressBar.TabIndex = 2
        '
        'SaveSHSHButton
        '
        Me.SaveSHSHButton.Location = New System.Drawing.Point(416, 192)
        Me.SaveSHSHButton.Name = "SaveSHSHButton"
        Me.SaveSHSHButton.Size = New System.Drawing.Size(75, 23)
        Me.SaveSHSHButton.TabIndex = 3
        Me.SaveSHSHButton.Text = "GRAB"
        Me.SaveSHSHButton.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.Button4)
        Me.GroupBox2.Location = New System.Drawing.Point(278, 27)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(213, 76)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Saving path:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(6, 46)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(201, 20)
        Me.TextBox1.TabIndex = 11
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(6, 17)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(201, 23)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Select directory"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 176)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Device ECID:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(78, 176)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "n/a"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 218)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Status: idle"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(6, 19)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(156, 17)
        Me.CheckBox1.TabIndex = 11
        Me.CheckBox1.Text = "Request SHSHs from Cydia"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(6, 41)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(260, 17)
        Me.CheckBox2.TabIndex = 12
        Me.CheckBox2.Text = "Also send SHSH(s) to our server (reccommended)"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CheckBox3)
        Me.GroupBox3.Controls.Add(Me.CheckBox2)
        Me.GroupBox3.Controls.Add(Me.CheckBox1)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 109)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(479, 64)
        Me.GroupBox3.TabIndex = 13
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Options"
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(272, 19)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(116, 17)
        Me.CheckBox3.TabIndex = 0
        Me.CheckBox3.Text = "Fetch OTA SHSHs"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 236)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.SaveSHSHButton)
        Me.Controls.Add(Me.SHSHProgressBar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SHSH Grabber v1.0.1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents SHSHProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents SaveSHSHButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenDatasFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenSourceOnGitHubToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreditsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RestartApplicationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QuitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ReportBugsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents SendACertificateToOurServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DownloadABlobFromOurServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GrabECIDToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
