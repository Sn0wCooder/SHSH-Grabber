Public Class CreateDevice
    'Public info_64 As Boolean
    Private Sub CreateDevice_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Form1.Enabled = True
    End Sub

    Private Sub CreateDevice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form1.Enabled = False
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Try
            If RadioButton1.Checked = True Then
                If IsUserlandConnected() = True Then
                    TextBox2.ReadOnly = True
                    ComboBox1.Enabled = False
                    Dim rawdeviceinfos As String = GetDeviceInfos(False)
                    Dim info_ecid As String = Nothing
                    Dim info_itunesname As String = Nothing
                    Dim info_product As String = Nothing
                    Dim info_color As String = Nothing

                    info_ecid = (rawdeviceinfos.Substring(rawdeviceinfos.IndexOf("UniqueChipID") + 14)).Substring(0, (rawdeviceinfos.Substring(rawdeviceinfos.IndexOf("UniqueChipID") + 14)).IndexOf(Environment.NewLine)).Trim()
                    info_itunesname = (rawdeviceinfos.Substring(rawdeviceinfos.IndexOf("DeviceName") + 12)).Substring(0, (rawdeviceinfos.Substring(rawdeviceinfos.IndexOf("DeviceName") + 12)).IndexOf(Environment.NewLine)).Trim()
                    info_product = (rawdeviceinfos.Substring(rawdeviceinfos.IndexOf("ProductType") + 13)).Substring(0, (rawdeviceinfos.Substring(rawdeviceinfos.IndexOf("ProductType") + 13)).IndexOf(Environment.NewLine)).Trim()
                    info_color = (rawdeviceinfos.Substring(rawdeviceinfos.IndexOf("DeviceColor") + 13)).Substring(0, (rawdeviceinfos.Substring(rawdeviceinfos.IndexOf("DeviceColor") + 13)).IndexOf(Environment.NewLine)).Trim()
                    ' ProductNameParser(info_product, info_color)

                    TextBox1.Text = info_itunesname
                    TextBox2.Text = info_ecid
                    ComboBox1.SelectedItem = info_product

                Else
                    MsgBox("ERROR: NO DEVICES DETECTED!", MsgBoxStyle.Critical, "Error")
                    RadioButton2.Checked = True
                End If
            Else
                ComboBox1.Enabled = True
                TextBox2.ReadOnly = False
            End If
        Catch ex As Exception

            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim da As String = Form1.da
        If My.Computer.FileSystem.DirectoryExists(da & "devices\" & TextBox2.Text) Then
            MsgBox("ERROR: device already created!", MsgBoxStyle.Critical, "Error")
        ElseIf TextBox1.Text = "ALL DEVICES" Then
            MsgBox("ERROR: choose another name.", MsgBoxStyle.Critical, "Error")
        ElseIf My.Computer.FileSystem.FileExists(da & "devices\" & TextBox1.Text & ".txt") Then
            MsgBox("ERROR: choose another name.", MsgBoxStyle.Critical, "Error")
        Else
            If Not My.Computer.FileSystem.DirectoryExists(da & "devices") Then
                My.Computer.FileSystem.CreateDirectory(da & "devices")
            End If
            My.Computer.FileSystem.CreateDirectory(da & "devices\" & TextBox2.Text)
            My.Computer.FileSystem.WriteAllText(da & "devices\" & TextBox2.Text & "\devicename.txt", TextBox1.Text, True)
            If My.Computer.FileSystem.FileExists(da & "devices\" & TextBox1.Text & ".txt") Then
                My.Computer.FileSystem.DeleteFile(da & "devices\" & TextBox1.Text & ".txt")
            End If
            My.Computer.FileSystem.WriteAllText(da & "devices\" & TextBox1.Text & ".txt", TextBox2.Text, False)
            My.Computer.FileSystem.WriteAllText(da & "devices\" & TextBox2.Text & "\type.txt", ComboBox1.SelectedItem, False)
            MsgBox("Device created!", MsgBoxStyle.Information, "Device created!")
            Form1.Enabled = True
            Form1.Button5.PerformClick()
            Me.Close()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If TextBox1.Text = Nothing Or TextBox2.Text.Length > 60 Or ComboBox1.SelectedIndex = 0 Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class