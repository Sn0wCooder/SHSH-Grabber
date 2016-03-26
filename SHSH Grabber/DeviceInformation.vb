Public Class DeviceInformation

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            If System.IO.File.Exists(SaveFileDialog1.FileName) Then
                Delete(False, SaveFileDialog1.FileName)
            End If
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, RichTextBox1.Text, True)
        End If
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        RichTextBox1.Text = GetDeviceInfos(False)
    End Sub

    Private Sub DeviceInformation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshToolStripMenuItem.PerformClick()
    End Sub
End Class