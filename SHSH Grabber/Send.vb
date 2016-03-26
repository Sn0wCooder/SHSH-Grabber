Public Class Send

    Private Sub Send_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Form1.Enabled = True
    End Sub

    Private Sub Send_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SHSHPathTextBox.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UploadSHSH(SHSHPathTextBox.Text, ECIDTextBox.Text, "ftp://shshgrabber.altervista.org/", "shshuserspace@shshgrabber", GetPasswordServer)
        MsgBox("SHSH(s) received!", MsgBoxStyle.Information, "Success!")
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim file As New System.IO.FileInfo(OpenFileDialog1.FileName)
            Dim SizeOfFile As Long = file.Length
            'MsgBox(SizeOfFile / 1024 / 1024)
            If SizeOfFile / 1024 / 1024 > 1 Then
                MsgBox("The file is too big!", MsgBoxStyle.Critical, "Error")
            Else
                SHSHPathTextBox.Text = OpenFileDialog1.FileName
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Not System.IO.File.Exists(SHSHPathTextBox.Text) Or ECIDTextBox.Text = Nothing Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
        End If
    End Sub

    Private Sub OpenECIDGrabberToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenECIDGrabberToolStripMenuItem.Click
        ECIDGrabber.Show()
    End Sub
End Class