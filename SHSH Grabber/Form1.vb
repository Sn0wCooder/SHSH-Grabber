Imports System.IO
Imports System.IO.Compression
Imports System.Security.Cryptography

Public Class Form1

    Public da As String = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\"
    Public savepath As String = Nothing
    Public ECID As String = Nothing
    Public DeviceName As String = Nothing
    Public info64 As Boolean
    Public info_product As String
    Public ShowSignedIPSWWhenChangeCB As Boolean = True
    Public LatestInteger As Integer = 1

    
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not SHSHProgressBar.Value = 0 Then
            ' If MessageBox.Show("SHSHs aren't saved yet. Do you really want to exit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
            'Else
            '   End
            'nd If
        Else
            My.Settings.Save()
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CheckForInternetConnection() = False Then
            MsgBox("An internet connection is required to launch this app!", MsgBoxStyle.Critical, "Error")
            End
        End If
        CheckForUpdates()
        Button5.PerformClick()
        TextBox1.Text = My.Settings.SavePath
        If My.Settings.ShowSignedIPSWs = True Then
            AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem.Checked = True
        Else
            AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem.Checked = False
        End If
        If My.Settings.SendSHSHsToMyServer = True Then
            CheckBox2.Checked = True
        Else
            CheckBox2.Checked = False
        End If
        If My.Settings.RequestCydiaSHSHs = True Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
        If My.Settings.RequestOTASHSHs = True Then
            CheckBox3.Checked = True
        Else
            CheckBox3.Checked = False
        End If
        If Not My.Computer.FileSystem.DirectoryExists(da & "libimobiledevice") Then
            If System.IO.File.Exists(da & "libimobiledevice.zip") Then
                Delete(False, da & "libimobiledevice.zip")
            End If
            My.Computer.FileSystem.WriteAllBytes(da & "libimobiledevice.zip", My.Resources.libimobiledevice, True)
            Dim sc As New Shell32.Shell
            Dim output As Shell32.Folder = sc.NameSpace(da)
            Dim input As Shell32.Folder = sc.NameSpace(da & "libimobiledevice.zip")
            output.CopyHere(input.Items, 4)
            Delete(False, da & "libimobiledevice.zip")
        End If

        If Not File.Exists(da & "xml2bplist.exe") Then
            My.Computer.FileSystem.WriteAllBytes(da & "xml2bplist.exe", My.Resources.xml2bplist, True)
        End If

        If Not File.Exists(da & "TSSQuery.exe") Then
            My.Computer.FileSystem.WriteAllBytes(da & "TSSQuery.exe", My.Resources.TSSQuery, True)
        End If
        If Not File.Exists(da & "pzip.exe") Then
            My.Computer.FileSystem.WriteAllBytes(da & "pzip.exe", My.Resources.pzip, True)
        End If
        If Not File.Exists(da & "ICSharpCode.SharpZipLib.dll") Then
            My.Computer.FileSystem.WriteAllBytes(da & "ICSharpCode.SharpZipLib.dll", My.Resources.ICSharpCode_SharpZipLib, True)
        End If
        If Not File.Exists(da & "hostshandler.exe") Then
            My.Computer.FileSystem.WriteAllBytes(da & "hostshandler.exe", My.Resources.hostshandler, True)
        End If

    End Sub

    Private Sub SaveSHSHButton_Click(sender As Object, e As EventArgs) Handles SaveSHSHButton.Click
        Timer1.Stop()
        SaveSHSHButton.Enabled = False
        GroupBox1.Enabled = False
        GroupBox2.Enabled = False
        GroupBox3.Enabled = False
        MenuStrip1.Enabled = False
        Dim info_ecid As String
        Dim SHSHOutputDir As String = TextBox1.Text
        Dim WebClient1 As New Net.WebClient
        Dim pf As String = GetPasswordServer()
        If ComboBox1.SelectedItem = "ALL DEVICES" Then
            ComboBox1.Items.Remove("ALL DEVICES")
            ComboBox1.Text = "ALL DEVICES"
            'Dim x As Integer = 0
            Dim x As Integer = ComboBox1.Items.Count
            For Each device In ComboBox1.Items
                info_ecid = My.Computer.FileSystem.ReadAllText(da & "devices\" & device & ".txt")
                info_product = System.IO.File.ReadAllText(da & "devices\" & info_ecid & "\type.txt")
                Dim bb As Boolean = False
                Dim apticket As Boolean = False

                UpdateStatus(SHSHProgressBar.Value + 0, "Getting APIs from IPSW.ME...")

                Delay(1)
                Dim signedbuilds As String() = GetSignedSHSHs(info_product)

                If CheckBox1.Checked = True Then
                    RequestSHSHsFromCydia(info_ecid, signedbuilds, info_product, signedios)
                End If

                For Each build In signedbuilds
                    Dim ios = signedios(Array.IndexOf(signedbuilds, build))
                    Dim url = signeduris(Array.IndexOf(signedbuilds, build))

                    If build.Contains("cydia") Then
                        hostshandler("add", "74.208.10.249", "gs.apple.com")
                        build = build.Replace("cydia", "")
                    Else
                        hostshandler("remove", "74.208.10.249", "gs.apple.com")
                    End If

                    UpdateStatus(SHSHProgressBar.Value + ((SHSHProgressBar.Maximum - 5) / (5 * signedbuilds.Length) / x), "Getting BuildManifest for iOS " + ios + ", " + build + " (" + info_product + ", " + info_ecid + ")...")

                    Delay(1)

                    If Not My.Computer.FileSystem.DirectoryExists(da & "shsh") Then
                        My.Computer.FileSystem.CreateDirectory(da & "shsh")
                    End If

                    Dim shshdir As String = da & "shsh\"

                    partialzip(url, "BuildManifest.plist", shshdir + info_product + "_" + build + "_" + "BuildManifest.plist")

                    If ExistsInBuildManifest(shshdir + info_product + "_" + build + "_" + "BuildManifest.plist", "BasebandFirmware", "key") Then
                        bb = True
                    Else
                        bb = False
                    End If

                    If Not info_product = "iPhone1,1" Or info_product = "iPhone1,2" Or info_product = "iPhone1,1" Or info_product = "iPod1,1" Or info_product = "iPod2,1" Then
                        apticket = True
                    Else
                        apticket = False
                    End If

                    UpdateStatus(SHSHProgressBar.Value + ((SHSHProgressBar.Maximum - 5) / (5 * signedbuilds.Length) / x), "Writing request for iOS " + ios + ", " + build + " (" + info_product + ", " + info_ecid + ")...")

                    Dim iinfo64 = pnp.ProductNameParser(My.Computer.FileSystem.ReadAllText(da & "devices\" & info_ecid & "\type.txt"))
                    Delay(1)
                    WriteTSSRequest(shshdir + info_product + "_" + build + "_" + "BuildManifest.plist", shshdir + info_product + "_" + build + "_" + "tss-request.plist", info_ecid, apticket, bb, iinfo64, GetDeviceInfos(True, "ApNonce"), "", GetDeviceInfos(True, "BasebandSerialNumber"), GetDeviceInfos(True, "BasebandCertId"), GetDeviceInfos(True, "SEPNonce"))

                    UpdateStatus(SHSHProgressBar.Value + ((SHSHProgressBar.Maximum - 5) / (5 * signedbuilds.Length) / x), "Sending TSS request (" + info_product + ", " + info_ecid + ")...")

                    SendTSSRequest(shshdir + info_product + "_" + build + "_" + "tss-request.plist", shshdir + info_product + "_" + build + "_" + "tss-response.plist")

                    UpdateStatus(SHSHProgressBar.Value + ((SHSHProgressBar.Maximum - 5) / (5 * signedbuilds.Length) / x), "Compressing SHSH (bplist format) (" + info_product + ", " + info_ecid + ")...")

                    Delay(1)
                    If Not File.Exists(SHSHOutputDir + "\" + info_ecid + "_" + info_product + "_" + ios + "_" + build + ".shsh") Then
                        XmlToBplist(shshdir + info_product + "_" + build + "_" + "tss-response.plist", SHSHOutputDir + "\" + info_ecid + "_" + info_product + "_" + ios + "_" + build + ".shsh")
                    End If
                    Delete(False, shshdir + info_product + "_" + build + "_" + "BuildManifest.plist")
                    Delete(False, shshdir + info_product + "_" + build + "_" + "tss-request.plist")
                    Delete(False, shshdir + info_product + "_" + build + "_" + "tss-response.plist")

                    UpdateStatus(SHSHProgressBar.Value, "Uploading SHSH to our server...")
                    If CheckBox2.Checked = True Then
                        UploadSHSH(SHSHOutputDir + "\" + info_ecid + "_" + info_product + "_" + ios + "_" + build + ".shsh", info_ecid, "ftp://shshgrabber.altervista.org/", "shshuserspace@shshgrabber", pf)
                    End If

                    UpdateStatus(SHSHProgressBar.Value + ((SHSHProgressBar.Maximum - 5) / (5 * signedbuilds.Length) / x), "Done!")

                    Delay(1)
                Next
            Next
            Button5.PerformClick()
            ComboBox1.SelectedItem = "ALL DEVICES"
        Else
            info_ecid = Label3.Text
            info_product = System.IO.File.ReadAllText(da & "devices\" & info_ecid & "\type.txt")
            Dim bb As Boolean = False
            Dim apticket As Boolean = False
            UpdateStatus(5, "Getting APIs from IPSW.ME...")
            Delay(1)
            Dim signedbuilds As String() = GetSignedSHSHs(info_product)

            If CheckBox1.Checked = True Then
                RequestSHSHsFromCydia(info_ecid, signedbuilds, info_product, signedios)
            End If

            For Each build In signedbuilds
                Dim ios = signedios(Array.IndexOf(signedbuilds, build))
                Dim url = signeduris(Array.IndexOf(signedbuilds, build))

                If build.Contains("cydia") Then
                    hostshandler("add", "74.208.10.249", "gs.apple.com")
                    build = build.Replace("cydia", "")
                Else
                    hostshandler("remove", "74.208.10.249", "gs.apple.com")
                End If

                UpdateStatus(SHSHProgressBar.Value + ((SHSHProgressBar.Maximum - 5) / (5 * signedbuilds.Length)), "Getting BuildManifest for iOS " + ios + ", " + build + "...")
                Delay(1)

                If Not My.Computer.FileSystem.DirectoryExists(da & "shsh") Then
                    My.Computer.FileSystem.CreateDirectory(da & "shsh")
                End If

                Dim shshdir As String = da & "shsh\"

                partialzip(url, "BuildManifest.plist", shshdir + info_product + "_" + build + "_" + "BuildManifest.plist")

                If ExistsInBuildManifest(shshdir + info_product + "_" + build + "_" + "BuildManifest.plist", "BasebandFirmware", "key") Then
                    bb = True
                Else
                    bb = False
                End If

                If Not info_product = "iPhone1,1" Or info_product = "iPhone1,2" Or info_product = "iPhone1,1" Or info_product = "iPod1,1" Or info_product = "iPod2,1" Then
                    apticket = True
                Else
                    apticket = False
                End If

                UpdateStatus(SHSHProgressBar.Value + ((SHSHProgressBar.Maximum - 5) / (5 * signedbuilds.Length)), "Writing request for iOS " + ios + ", " + build + "...")
                Dim iinfo64 = pnp.ProductNameParser(My.Computer.FileSystem.ReadAllText(da & "devices\" & info_ecid & "\type.txt"))
                Delay(1)
                WriteTSSRequest(shshdir + info_product + "_" + build + "_" + "BuildManifest.plist", shshdir + info_product + "_" + build + "_" + "tss-request.plist", info_ecid, apticket, bb, iinfo64, GetDeviceInfos(True, "ApNonce"), "", GetDeviceInfos(True, "BasebandSerialNumber"), GetDeviceInfos(True, "BasebandCertId"), GetDeviceInfos(True, "SEPNonce"))
                UpdateStatus(SHSHProgressBar.Value + ((SHSHProgressBar.Maximum - 5) / (5 * signedbuilds.Length)), "Sending TSS request...")
                SendTSSRequest(shshdir + info_product + "_" + build + "_" + "tss-request.plist", shshdir + info_product + "_" + build + "_" + "tss-response.plist")
                UpdateStatus(SHSHProgressBar.Value + ((SHSHProgressBar.Maximum - 5) / (5 * signedbuilds.Length)), "Compressing SHSH (bplist format)...")
                Delay(1)
                If Not File.Exists(SHSHOutputDir + "\" + info_ecid + "_" + info_product + "_" + ios + "_" + build + ".shsh") Then
                    XmlToBplist(shshdir + info_product + "_" + build + "_" + "tss-response.plist", SHSHOutputDir + "\" + info_ecid + "_" + info_product + "_" + ios + "_" + build + ".shsh")
                End If
                Delete(False, shshdir + info_product + "_" + build + "_" + "BuildManifest.plist")
                Delete(False, shshdir + info_product + "_" + build + "_" + "tss-request.plist")
                Delete(False, shshdir + info_product + "_" + build + "_" + "tss-response.plist")
                If CheckBox2.Checked = True Then
                    UploadSHSH(SHSHOutputDir + "\" + info_ecid + "_" + info_product + "_" + ios + "_" + build + ".shsh", info_ecid, "ftp://shshgrabber.altervista.org/", "shshuserspace@shshgrabber", pf)
                End If
                UpdateStatus(SHSHProgressBar.Value + ((SHSHProgressBar.Maximum - 5) / (5 * signedbuilds.Length)), "Done!")
                Delay(1)
            Next
        End If
        hostshandler("remove", "74.208.10.249", "gs.apple.com")
        SaveSHSHButton.Enabled = True
        GroupBox1.Enabled = True
        GroupBox2.Enabled = True
        GroupBox3.Enabled = True
        MenuStrip1.Enabled = True
        'Button5.PerformClick()
        MsgBox("Done! All SHSHs are saved in " & TextBox1.Text & "!", MsgBoxStyle.Information, "Finish!")
        SHSHProgressBar.Value = 0
        Label5.Text = "Status: idle"
        Timer1.Start()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CreateDevice.Show()
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If My.Computer.FileSystem.DirectoryExists(TextBox1.Text) And Not ComboBox1.SelectedItem = Nothing Then
            SaveSHSHButton.Enabled = True
        Else
            SaveSHSHButton.Enabled = False
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ComboBox1.Text = ""
        Dim di As New DirectoryInfo(da & "devices\")
        If Not My.Computer.FileSystem.DirectoryExists(di.ToString) Then
            My.Computer.FileSystem.CreateDirectory(di.ToString)
        End If
        Dim diar1 As DirectoryInfo() = di.GetDirectories()
        Dim dra As DirectoryInfo
        Dim dra2 As Integer = 0
        For Each dra In diar1
            dra2 += 1
        Next
        If dra2 = 0 Then
            Button2.Enabled = False
            SaveSHSHButton.Enabled = False
            ComboBox1.Enabled = False
            ComboBox1.Text = Nothing
        Else
            ComboBox1.Enabled = True
            ComboBox1.Items.Clear()
            For Each dra In diar1
                ComboBox1.Items.Add(File.ReadAllText(da & "devices\" & dra.ToString & "\devicename.txt"))
            Next
            ComboBox1.SelectedIndex = 0
            If Not ComboBox1.Items.Count = 1 Then
                ComboBox1.Items.Add("ALL DEVICES")
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "ALL DEVICES" Then
            Label3.Text = "n/a"
            'OPS
            'MsgBox("ERROR: function not implemented yet!", MsgBoxStyle.Critical, "Error")
            'ComboBox1.SelectedIndex = 0
            If My.Settings.ShowSignedIPSWs = True And ShowSignedIPSWWhenChangeCB = True Then
                SIgnedIPSWs.Close()
                SIgnedIPSWs.Show()
            ElseIf My.Settings.ShowSignedIPSWs = True And ShowSignedIPSWWhenChangeCB = False Then
                ShowSignedIPSWWhenChangeCB = True
            End If
            'End If
        ElseIf ComboBox1.Text = "" Then
        Else
            Label3.Text = My.Computer.FileSystem.ReadAllText(da & "devices\" & ComboBox1.SelectedItem & ".txt")
            ProductNameParser(da & "devices\" & Label3.Text & "\type.txt")
            info_product = System.IO.File.ReadAllText(da & "devices\" & Label3.Text & "\type.txt")
            If My.Settings.ShowSignedIPSWs = True And ShowSignedIPSWWhenChangeCB = True Then
                SIgnedIPSWs.Close()
                SIgnedIPSWs.Show()
            ElseIf My.Settings.ShowSignedIPSWs = True And ShowSignedIPSWWhenChangeCB = False Then
                ShowSignedIPSWWhenChangeCB = True
            End If
        End If
        If Not ComboBox1.SelectedItem = Nothing Then
            Button2.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox1.SelectedItem = "ALL DEVICES" Then
            If MessageBox.Show("Are you sure that you want to delete all devices?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                'My.Computer.FileSystem.DeleteDirectory(da & "devices", FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.DeletePermanently)
                Delete(True, da & "devices")
            End If
        Else
            If MessageBox.Show("Are you sure that you want to delete this device?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                Dim ecidpe As String = File.ReadAllText(da & "devices\" & ComboBox1.SelectedItem & ".txt")
                Delete(False, da & "devices\" & ComboBox1.SelectedItem & ".txt")
                Delete(True, da & "devices\" & ecidpe)
                ' My.Computer.FileSystem.DeleteDirectory(da & "devices\" & ecidpe, FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.DeletePermanently)
            End If
        End If

        SIgnedIPSWs.Close()
        Button5.PerformClick()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            savepath = FolderBrowserDialog1.SelectedPath
            TextBox1.Text = savepath
        End If
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        DeviceInformation.Show()
    End Sub

    Public Sub UpdateStatus(ByVal PC As Integer, ByVal L As String)
        If PC > 100 Then
            SHSHProgressBar.Value = 100
        Else
            SHSHProgressBar.Value = PC
        End If
        Label5.Text = "Status: " & PC.ToString & "% - " & L
    End Sub

    Private Sub AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem.Click
        If AlwaysShowSignedFirmwareForSelectedDeviceToolStripMenuItem.Checked = True Then
            My.Settings.ShowSignedIPSWs = True
            My.Settings.Save()
            Button5.PerformClick()
        Else
            My.Settings.ShowSignedIPSWs = False
            My.Settings.Save()
            SIgnedIPSWs.Close()
            Button5.PerformClick()
        End If
    End Sub


    Private Sub OpenDatasFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenDatasFolderToolStripMenuItem.Click
        Process.Start(da)
    End Sub

    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub RestartApplicationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartApplicationToolStripMenuItem.Click
        Application.Restart()
    End Sub

    Private Sub OpenSourceOnGitHubToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenSourceOnGitHubToolStripMenuItem.Click
        Process.Start("http://github.com/Sn0wCooder/SHSH-Grabber")
    End Sub

    Private Sub CreditsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreditsToolStripMenuItem.Click
        MsgBox("THANKS TO:" & vbCrLf & vbCrLf & "@sn0wcooder for this application" & vbCrLf & "@blackgeektuto for some codes (Beehind)" & vbCrLf & "@icj_ for the json of IPSW.ME" & vbCrLf & "@libimobiledevice guys for ideviceinfo" & vbCrLf & "@geek_break for testing", MsgBoxStyle.Information, "Credits")
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Process.Start("http://shshgrabber.altervista.org/faq")
    End Sub

    Private Sub ReportBugsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportBugsToolStripMenuItem.Click
        MsgBox("All bugs and/or improvements can be reported/submitted to leoalfreducci@gmail.com.", MsgBoxStyle.Information, "Warning")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        My.Settings.SavePath = TextBox1.Text
        My.Settings.Save()
    End Sub

    Private Sub SendACertificateToOurServerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendACertificateToOurServerToolStripMenuItem.Click
        Send.Show()
        Me.Enabled = False
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = False And My.Settings.SendSHSHsToMyServer = True Then

            MsgBox("WARNING: It is strongly recommended to send the SHSHs to our server. In this way you have a very low probability of losing them (there is always a possibility, although it is very low).", MsgBoxStyle.Exclamation, "Warning!")
        End If
        If CheckBox2.Checked = True Then
            My.Settings.SendSHSHsToMyServer = True
        Else
            My.Settings.SendSHSHsToMyServer = False
        End If
        'My.Settings.Save()
    End Sub

    Private Sub GrabECIDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GrabECIDToolStripMenuItem.Click
        ECIDGrabber.Show()
    End Sub

    Private Sub DownloadABlobFromOurServerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownloadABlobFromOurServerToolStripMenuItem.Click
        DownloadSHSHs.Show()
        Me.Enabled = False
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            My.Settings.RequestCydiaSHSHs = True
        Else
            My.Settings.RequestCydiaSHSHs = False
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            My.Settings.RequestOTASHSHs = True
            MsgBox("ERROR: function not implemented yet!", MsgBoxStyle.Critical, "ERROR")
            CheckBox3.Checked = False
        Else
            My.Settings.RequestOTASHSHs = False
        End If
    End Sub
End Class
