Public Class ECIDGrabber

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If IsUserlandConnected() = True Then
            Dim rawdeviceinfos As String = GetDeviceInfos(False)
            TextBox1.Text = (rawdeviceinfos.Substring(rawdeviceinfos.IndexOf("UniqueChipID") + 14)).Substring(0, (rawdeviceinfos.Substring(rawdeviceinfos.IndexOf("UniqueChipID") + 14)).IndexOf(Environment.NewLine)).Trim()
        Else
            MsgBox("Device not detected!", MsgBoxStyle.Critical, "Error")
        End If
    End Sub
End Class