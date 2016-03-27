Public Class SIgnedIPSWs
    Dim da As String = Form1.da
    Dim info1 As String = Nothing
    Private Sub SIgnedIPSWs_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Form1.ComboBox1.SelectedItem IsNot "ALL DEVICES" Then
            ListBox1.Items.Add("    Signed builds for " & Form1.info_product & ":")
            Dim signedbuilds As String() = GetSignedSHSHs(Form1.info_product)
            For Each buils In signedbuilds
                ListBox1.Items.Add(buils)
            Next
        Else
            Form1.ComboBox1.Items.Remove("ALL DEVICES")
            For Each item In Form1.ComboBox1.Items
                ' MsgBox(item)
                Dim it1 As String = My.Computer.FileSystem.ReadAllText(da & "devices\" & item & ".txt")
                Dim typeitm As String = My.Computer.FileSystem.ReadAllText(da & "devices\" & it1 & "\type.txt")
                ' MsgBox(it1)
                ListBox1.Items.Add("    Signed builds for " & typeitm & ":")
                Dim signedbuilds As String() = GetSignedSHSHs(typeitm)
                For Each builds In signedbuilds
                    ListBox1.Items.Add(builds)
                Next
            Next
            Form1.ComboBox1.Items.Add("ALL DEVICES")
            Form1.ShowSignedIPSWWhenChangeCB = False
            Form1.ComboBox1.Text = "ALL DEVICES"
            'Form1.ComboBox1.SelectedItem = "ALL DEVICES"
        End If
    End Sub


    
End Class