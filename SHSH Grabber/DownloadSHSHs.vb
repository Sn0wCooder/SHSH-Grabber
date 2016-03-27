Public Class DownloadSHSHs
    Dim client As New System.Net.WebClient
    Dim da = Form1.da
    Dim ecid1 As String
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim x As String = client.DownloadString("http://shshgrabber.altervista.org/SHSHs/" & TextBox1.Text & "/info.txt")
            If System.IO.File.Exists(da & "x.txt") Then
                Delete(False, da & "x.txt")
            End If
            Dim y As Integer = 0
            My.Computer.FileSystem.WriteAllText(da & "x.txt", x, True)
            ComboBox1.Items.Clear()
            For Each line In System.IO.File.ReadLines(da & "x.txt")
                y += 1
            Next
            ComboBox1.Items.AddRange(System.IO.File.ReadAllLines(da & "x.txt"))
            Label3.Text = "Found " & y & " SHSH(s) for device " & TextBox1.Text & "."
            If ComboBox1.Items.Count > 1 Then
                ComboBox1.Items.Add("ALL SHSHs")
            End If
            Delete(False, da & "x.txt")
            GroupBox2.Enabled = True
            ComboBox1.SelectedIndex = 0
            ecid1 = TextBox1.Text
        Catch ex As Exception
            If InStr(ex.Message, "404") Then
                Label3.Text = "ERROR: No SHSH(s) found for device " & TextBox1.Text & " to our server."
                GroupBox2.Enabled = False
                ComboBox1.Items.Clear()
            End If
        End Try

    End Sub

    Private Sub DownloadSHSHs_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Form1.Enabled = True
    End Sub

    Private Sub DownloadSHSHs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Text = Form1.TextBox1.Text
    End Sub

    Private Sub ECIDGrabberToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ECIDGrabberToolStripMenuItem.Click
        ECIDGrabber.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Enabled = False
        GroupBox1.Enabled = False
        GroupBox2.Enabled = False
        GroupBox3.Enabled = False
        ControlBox = False
        If ComboBox1.SelectedItem = "ALL SHSHs" Then
            ComboBox1.Items.Remove("ALL SHSHs")
            Dim numberofshshs As Integer = ComboBox1.Items.Count
            Dim y As Integer = ProgressBar1.Value / numberofshshs
            For Each item In ComboBox1.Items
                ProgressBar1.Value = ProgressBar1.Value / numberofshshs + y
                Label2.Text = "Status: " & ProgressBar1.Value & " - downloading " & item & "..."
                client.DownloadFile("http://shshgrabber.altervista.org/SHSHs/" & ecid1 & "/" & item, TextBox2.Text & "/" & item)
            Next
        Else
            ProgressBar1.Value = 50
            Label2.Text = "Status: 50% - downloading " & ComboBox1.SelectedItem & "..."
            client.DownloadFile("http://shshgrabber.altervista.org/SHSHs/" & ecid1 & "/" & ComboBox1.SelectedItem, TextBox2.Text & "/" & ComboBox1.SelectedItem)
        End If
        ProgressBar1.Value = 100
        Label2.Text = "Status: 100% - Done!"
        MsgBox("DONE! All SHSH(s) are downloaded in " & TextBox2.Text & "!", MsgBoxStyle.Information, "Success!")
        Button1.Enabled = True
        ControlBox = True
        GroupBox1.Enabled = True
        GroupBox2.Enabled = True
        GroupBox3.Enabled = True
        Label2.Text = "Status: idle"
        ProgressBar1.Value = 0
        Button2.PerformClick()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If GroupBox2.Enabled = False Or TextBox2.Text = Nothing Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
        End If
    End Sub
End Class