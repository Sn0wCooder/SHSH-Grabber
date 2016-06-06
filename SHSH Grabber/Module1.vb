Imports System.Management
Imports System.Net
Imports System.IO
Imports SHSH_Grabber.Form1
Imports System.Threading
Imports System.Text
Imports System.Security.Cryptography

Module Module1
    Public ARPS As Boolean = False 'request pss
    Public signedios As String()
    Public signeduris As String()
    Dim da As String = Form1.da
    Public shshHeader As String = "<?xml version=" + """" + "1.0" + """" + " encoding=" + """" + "utf-8" + """" + "?>" + Environment.NewLine + "<!DOCTYPE plist PUBLIC " + """" + "-//Apple Computer//DTD PLIST 1.0//EN" + """" + " " + """" + "http://www.apple.com/DTDs/PropertyList-1.0.dtd" + """" + ">" + Environment.NewLine + "<plist version=" + """" + "1.0" + """" + ">" + Environment.NewLine
    Public Sub partialzip(ZipUrl As String, ZipPath As String, Outfile As String)
REDO:
        Dim partialzip_p As New Process()
        ' Try
        partialzip_p.StartInfo.UseShellExecute = False
        partialzip_p.StartInfo.FileName = Form1.da + "pzip.exe"
        partialzip_p.StartInfo.Arguments = """" + ZipUrl + """" + " " + """" + ZipPath + """" + " " + """" + Outfile + """"
        partialzip_p.StartInfo.CreateNoWindow = True
        partialzip_p.Start()
        ' Catch ex As Exception
        ' End Try

        Dim timeout = 0
        Dim looped As Boolean = False

        Do Until IO.File.Exists(Outfile)
            Delay(1)
        Loop
        Dim newfile As New FileInfo(Outfile)
        Dim size As Long = newfile.Length

        Do Until partialzip_p.HasExited

            If timeout = 5 And size <= 0 Then
                'dunno why, sometimes my remote zip file downloader loops :(
                Kill({"pzip"})
                looped = True
            End If

            Delay(1)
            timeout = timeout + 1
        Loop
        If looped = True Then
            GoTo REDO
        End If
    End Sub

    Public Sub Delay(ByVal dblSecs As Double)
        'iH8Sn0w Delay
        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
        Dim dblWaitTil As Date
        Now.AddSeconds(OneSec)
        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(dblSecs)
        Do Until Now > dblWaitTil
            Application.DoEvents()
        Loop
    End Sub

    Public Sub hostshandler(mode As String, ip As String, hostname As String)
        Dim sr As New StreamReader(Environment.SystemDirectory & "\drivers\etc\hosts")
        Dim hosts As String = sr.ReadToEnd()
        sr.Close()

        If mode = "add" And hosts.Contains(ip + " " + hostname) Then
            Exit Sub
        End If

        If mode = "remove" And hosts.Contains(ip + " " + hostname) = False Then
            Exit Sub
        End If

        Dim hostshandler_p As New Process()
        ' Try
        hostshandler_p.StartInfo.UseShellExecute = True
        hostshandler_p.StartInfo.FileName = Form1.da + "hostshandler.exe"
        hostshandler_p.StartInfo.Arguments = mode + " " + """" + ip + """" + " " + """" + hostname + """"
        hostshandler_p.StartInfo.CreateNoWindow = True
        hostshandler_p.StartInfo.Verb = "runas"
        hostshandler_p.Start()
        ' Catch ex As Exception
        ' End Try
        Do Until hostshandler_p.HasExited
            Delay(1)
        Loop
        Delay(1)
    End Sub

    Public Sub Delete(IsDirectory As Boolean, path As String)
        If IsDirectory = True Then
            If My.Computer.FileSystem.DirectoryExists(path) Then
                IO.Directory.Delete(path, True)
            End If
        Else
            If My.Computer.FileSystem.FileExists(path) Then
                IO.File.Delete(path)
            End If
        End If
    End Sub

    Public Sub Kill(ProcessesList As String())
        For Each ProcessName In ProcessesList
            Dim SubProcesses() As Process = Process.GetProcessesByName(ProcessName)
            For Each SubProcess As Process In SubProcesses
                If IsProcessRunning(SubProcess.ProcessName) = True Then
                    SubProcess.Kill()
                End If
            Next
        Next
    End Sub

    Public Function IsProcessRunning(name As String) As Boolean
        For Each clsProcess As Process In Process.GetProcesses()
            If clsProcess.ProcessName.StartsWith(name) Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function IsUserlandConnected()
        Dim forever As Boolean = True
        Dim USBName As String = String.Empty
        Dim USBSearcher As New ManagementObjectSearcher( _
                      "root\CIMV2", _
                      "SELECT * FROM Win32_PnPEntity WHERE Description = 'Apple Mobile Device USB Driver'")
        For Each queryObj As ManagementObject In USBSearcher.Get()
            USBName += (queryObj("Description"))
        Next
        If USBName = "Apple Mobile Device USB Driver" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ExistsInBuildManifest(Infile As String, value As String, valuetype As String, Optional ByVal subvalue As String = "", Optional ByVal subvaluetype As String = "")
        If subvalue = "" Then
            If File.ReadAllText(Infile).Contains("<" + valuetype + ">" + value + "</" + valuetype + ">") Then
                Return True
            Else
                Return False
            End If
        Else
            Dim BuildManifest() As String = IO.File.ReadAllLines(Infile)

            Dim BuildManifestLines As Integer = BuildManifest.Length
            Dim position As Integer = 0
            Dim rightblob As Boolean = False
            Dim data As String = String.Empty

            Dim open = 0
            Dim closed = 0

            Dim typeopen = 0
            Dim typeclosed = 0

            Do While position < BuildManifestLines

                If BuildManifest(position).Contains("<key>" + value + "</key>") Then
                    If BuildManifest(position + 1).Contains("<" + valuetype + ">") Then
                        rightblob = True
                    End If
                End If

                If BuildManifest(position).Contains("<dict>") And rightblob = True Then
                    open = open + 1
                End If

                If BuildManifest(position).Contains("</dict>") And rightblob = True Then
                    closed = closed + 1
                End If

                If open = closed And open <> 0 Then
                    rightblob = False
                    Exit Do
                    Return False
                End If

                If BuildManifest(position).Contains(subvalue) And rightblob = True Then
                    Return True
                    Exit Do
                End If

                position = position + 1
            Loop
        End If
    End Function

    Public Function IsDFUConnected()
        Dim forever As Boolean = True
        Dim text1 As String = ""
        text1 = " "
        Dim searcher As New ManagementObjectSearcher( _
                  "root\CIMV2", _
                  "SELECT * FROM Win32_PnPEntity WHERE Description = 'Apple Recovery (DFU) USB Driver'")
        For Each queryObj As ManagementObject In searcher.Get()

            text1 += (queryObj("Description"))
        Next
        If text1.Contains("DFU") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IsRecoveryConnected()
        Dim text1 As String = ""
        text1 = " "
        Dim searcher As New ManagementObjectSearcher( _
                  "root\CIMV2", _
                  "SELECT * FROM Win32_PnPEntity WHERE Description = 'Apple Recovery (iBoot) USB Driver'")
        For Each queryObj As ManagementObject In searcher.Get()

            text1 += (queryObj("Description"))
        Next
        If text1.Contains("iBoot") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function CheckForInternetConnection() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://www.google.com")
                    Return True
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Sub WriteTSSRequest(BuildManifest As String, Outfile As String, ecid As String, apticket As Boolean, bbticket As Boolean, img4 As Boolean, Optional ByVal apnonce As String = "", Optional ByVal bbnonce As String = "", Optional ByVal bbsnum As String = "", Optional ByVal bbgoldcertid As String = "", Optional ByVal sepnonce As String = "")
        Dim Cool As String() = {"AppleLogo", "BatteryCharging", "BatteryCharging0", "BatteryCharging1", "BatteryFull", "BatteryLow0", "BatteryLow1", "BatteryPlugin", "DeviceTree", "KernelCache", "LLB", "RecoveryMode", "RestoreDeviceTree", "RestoreKernelCache", "RestoreLogo", "RestoreRamDisk", "iBEC", "iBSS", "iBoot", "RestoreSEP", "SEP", "ftap", "ftsp", "rfta", "rfts"}

        File.Create(Outfile).Dispose()
        Using XMLWriter As StreamWriter = New StreamWriter(Outfile, True)
            'System.Text.Encoding.UTF8
            XMLWriter.Write(shshHeader)
            XMLWriter.WriteLine("<dict>")
            If apticket = True Then
                If Not img4 Then
                    XMLWriter.WriteLine("	<key>@APTicket</key>")
                    XMLWriter.WriteLine("	<true/>")
                Else
                    XMLWriter.WriteLine("	<key>@ApImg4Ticket</key>")
                    XMLWriter.WriteLine("	<true/>")
                End If
            End If
            If bbticket = True Then
                XMLWriter.WriteLine("	<key>@BBTicket</key>")
                XMLWriter.WriteLine("	<true/>")
            End If
            XMLWriter.WriteLine("	<key>@HostPlatformInfo</key>")
            XMLWriter.WriteLine("	<string>windows</string>")
            XMLWriter.WriteLine("	<key>@VersionInfo</key>")
            XMLWriter.WriteLine("	<string>libauthinstall-391.0.0.1.3</string>")
            XMLWriter.WriteLine("	<key>@Locality</key>")
            XMLWriter.WriteLine("	<string>" + Thread.CurrentThread.CurrentCulture.Name + "</string>")
            XMLWriter.WriteLine("	<key>ApBoardID</key>")
            XMLWriter.WriteLine("	<integer>" + (HexToDec(GetFromBuildManifest(BuildManifest, "array", "dict", "ApBoardID", "string", False))).ToString + "</integer>")
            XMLWriter.WriteLine("	<key>ApChipID</key>")
            XMLWriter.WriteLine("	<integer>" + (HexToDec(GetFromBuildManifest(BuildManifest, "array", "dict", "ApChipID", "string", False))).ToString + "</integer>")
            If ecid <> String.Empty Then
                XMLWriter.WriteLine("	<key>ApECID</key>")
                XMLWriter.WriteLine("	<integer>" + ecid + "</integer>")
            End If
            If apticket = True Then
                If apnonce.Length = 28 Then
                    XMLWriter.WriteLine("	<key>ApNonce</key>")
                    XMLWriter.WriteLine("	<data>" + apnonce + "</data>")
                Else
                    XMLWriter.WriteLine("	<key>ApNonce</key>")
                    XMLWriter.WriteLine("	<data>FFrp/uZvF8gUV8Xj9RaXRyOZiO0=</data>")
                End If
            End If

            If img4 = True Then
                If sepnonce.Length = 28 Then
                    XMLWriter.WriteLine("	<key>SepNonce</key>")
                    XMLWriter.WriteLine("	<data>" + sepnonce + "</data>")
                    ' trovato nel BM.plist di iPhone 6 iOS 9.2.1
                    XMLWriter.WriteLine("	<key>ApSecurityMode</key>")
                    XMLWriter.WriteLine("	<true/>")
                Else
                    XMLWriter.WriteLine("	<key>SepNonce</key>")
                    XMLWriter.WriteLine("	<data>nE+WLdr06Ey/9TZu93+BedtRcmQ=</data>")
                End If
            End If
            XMLWriter.WriteLine("	<key>ApProductionMode</key>")
            XMLWriter.WriteLine("	<true/>")
            XMLWriter.WriteLine("	<key>ApSecurityDomain</key>")
            XMLWriter.WriteLine("	<integer>" + (HexToDec(GetFromBuildManifest(BuildManifest, "array", "dict", "ApSecurityDomain", "string", False))).ToString + "</integer>")




            For Each element In Cool
                If ExistsInBuildManifest(BuildManifest, element, "key") Then
                    XMLWriter.WriteLine("	<key>" + element + "</key>")
                    XMLWriter.WriteLine("	<dict>")
                    If element = "LLB" Or element = "iBSS" Or element = "iBEC" Then
                        XMLWriter.WriteLine("			<key>BuildString</key>")
                        XMLWriter.WriteLine("			<string>" + GetFromBuildManifest(BuildManifest, "<key>" + element + "</key>", "dict", "BuildString", "string", False) + "</string>")
                    End If
                    If element <> "ftap" And element <> "ftsp" And element <> "rfta" And element <> "rfts" Then
                        If GetFromBuildManifest(BuildManifest, "<key>" + element + "</key>", "dict", "Digest", "data", False) <> String.Empty Then
                            XMLWriter.WriteLine("		<key>Digest</key>")
                            XMLWriter.WriteLine("		<data>" + GetFromBuildManifest(BuildManifest, "<key>" + element + "</key>", "dict", "Digest", "data", False) + "</data>")
                        End If
                    Else
                        ' quei 4 stronzi vogliono il Digest anche se è vuoto :/
                        XMLWriter.WriteLine("		<key>Digest</key>")
                        XMLWriter.WriteLine("		<data>" + GetFromBuildManifest(BuildManifest, "<key>" + element + "</key>", "dict", "Digest", "data", False) + "</data>")
                    End If
                    If img4 = True Then
                        XMLWriter.WriteLine("		<key>EPRO</key>")
                        XMLWriter.WriteLine("		<true/>")
                        XMLWriter.WriteLine("		<key>ESEC</key>")
                        XMLWriter.WriteLine("		<true/>")
                    End If
                    If GetFromBuildManifest(BuildManifest, "<key>" + element + "</key>", "dict", "PartialDigest", "data", False) <> String.Empty Then
                        XMLWriter.WriteLine("		<key>PartialDigest</key>")
                        XMLWriter.WriteLine("		<data>" + GetFromBuildManifest(BuildManifest, "<key>" + element + "</key>", "dict", "PartialDigest", "data", False) + "</data>")
                    End If
                    If ExistsInBuildManifest(BuildManifest, element, "dict", "Trusted", "") = True Then
                        XMLWriter.WriteLine("		<key>Trusted</key>")
                        XMLWriter.WriteLine("		<true/>")
                    End If
                    XMLWriter.WriteLine("	</dict>")
                End If
            Next

            If bbticket = True Then
                XMLWriter.WriteLine("	<key>BasebandFirmware</key>")
                XMLWriter.Write(GetFromBuildManifest(BuildManifest, "Manifest", "dict", "BasebandFirmware", "dict", True))

                XMLWriter.WriteLine("	<key>BbChipID</key>")
                XMLWriter.WriteLine("	<integer>" + (HexToDec(GetFromBuildManifest(BuildManifest, "array", "dict", "BbChipID", "string", False))).ToString + "</integer>")
                XMLWriter.WriteLine("	<key>BbGoldCertId</key>")
                XMLWriter.WriteLine("	<integer>" + bbgoldcertid + "</integer>")
                If bbnonce.Length = 28 Then
                    XMLWriter.WriteLine("	<key>BbNonce</key>")
                    XMLWriter.WriteLine("	<data>" + bbnonce + "</data>")
                Else
                    XMLWriter.WriteLine("	<key>BbNonce</key>")
                    XMLWriter.WriteLine("	<data>FFrp/uZvF8gUV8Xj9RaXRyOZiO0=</data>")
                End If
                XMLWriter.WriteLine("	<key>BbSNUM</key>")
                XMLWriter.WriteLine("	<data>" + bbsnum + "</data>")

                If ExistsInBuildManifest(BuildManifest, "BbSkeyId", "key") Then
                    XMLWriter.WriteLine("	<key>BbSkeyId</key>")
                    XMLWriter.WriteLine("	<data>" + GetFromBuildManifest(BuildManifest, "array", "dict", "BbSkeyId", "data", False) + "</data>")
                End If

                If ExistsInBuildManifest(BuildManifest, "BbActivationManifestKeyHash", "key") Then
                    XMLWriter.WriteLine("	<key>BbActivationManifestKeyHash</key>")
                    XMLWriter.WriteLine("	<data>" + GetFromBuildManifest(BuildManifest, "array", "dict", "BbActivationManifestKeyHash", "data", False) + "</data>")
                End If

                If ExistsInBuildManifest(BuildManifest, "BbProvisioningManifestKeyHash", "key") Then
                    XMLWriter.WriteLine("	<key>BbProvisioningManifestKeyHash</key>")
                    XMLWriter.WriteLine("	<data>" + GetFromBuildManifest(BuildManifest, "array", "dict", "BbProvisioningManifestKeyHash", "data", False) + "</data>")
                End If

            End If
            XMLWriter.WriteLine("	<key>UniqueBuildID</key>")
            XMLWriter.WriteLine("	<data>" + GetFromBuildManifest(BuildManifest, "array", "dict", "UniqueBuildID", "data", False) + "</data>")
            XMLWriter.WriteLine("</dict>")
            XMLWriter.Write("</plist>")

            XMLWriter.Close()
        End Using
    End Sub

    Public Function HexToDec(Hexnumber As String) As Integer
        If Hexnumber.Contains("0x") Then
            Hexnumber = Hexnumber.Replace("0x", "")
        End If
        Dim number As Integer = Integer.Parse(Hexnumber, System.Globalization.NumberStyles.HexNumber)
        Return number
    End Function

    Public Function GetFromBuildManifest(Infile As String, value As String, valuetype As String, subvalue As String, subvaluetype As String, multiline As Boolean, Optional ByVal subvaluekey As String = "key")
        Dim BuildManifest() As String = IO.File.ReadAllLines(Infile)

        Dim BuildManifestLines As Integer = BuildManifest.Length
        Dim position As Integer = 0
        Dim rightblob As Boolean = False
        Dim data As String = String.Empty

        Dim open = 0
        Dim closed = 0

        Dim typeopen = 0
        Dim typeclosed = 0

        Do While position <> BuildManifestLines
            If BuildManifest(position).Contains("</plist") Then
                Exit Do
                Exit Function
            End If

            If BuildManifest(position).Contains(value) And rightblob = False Then
                If BuildManifest(position + 1).Contains(valuetype) Then
                    rightblob = True
                End If
            End If

            If BuildManifest(position).Contains("<dict>") And rightblob = True Then
                open = open + 1
            End If

            If BuildManifest(position).Contains("</dict>") And rightblob = True Then
                closed = closed + 1
            End If

            If open = closed And open <> 0 Then
                rightblob = False
            End If

            If BuildManifest(position).Contains("<" + subvaluekey + ">" + subvalue + "</" + subvaluekey + ">") And rightblob = True Then
                Do While True

                    If BuildManifest(position).Contains("<" + subvaluetype + ">") Then
                        typeopen = typeopen + 1
                    End If

                    If typeopen <> typeclosed And typeopen <> 0 Then
                        If multiline = False Then
                            data = data + BuildManifest(position)
                        Else
                            data = data + BuildManifest(position) + Environment.NewLine
                        End If
                    End If

                    If BuildManifest(position).Contains("</" + subvaluetype + ">") Then
                        typeclosed = typeclosed + 1
                    End If

                    If typeopen = typeclosed And typeopen <> 0 Then
                        Exit Do
                    End If

                    position = position + 1
                Loop
            End If

            position = position + 1
        Loop

        If multiline = False Then
            Return ((data.Replace("<" + subvaluetype + ">", "").Replace("</" + subvaluetype + ">", "")).Replace(vbTab, "")).Trim()
        Else
            Return data
        End If
    End Function

    Public Sub SendTSSRequest(TSSRequestPlist As String, Outfile As String, Optional ByVal AlterHost As Boolean = False)

        Dim tssquery_p As New Process()
        '  Try
        tssquery_p.StartInfo.UseShellExecute = False
        tssquery_p.StartInfo.FileName = Form1.da + "TSSQuery.exe"
        tssquery_p.StartInfo.Arguments = """" + TSSRequestPlist + """" + " " + """" + Outfile + """"
        tssquery_p.StartInfo.CreateNoWindow = True
        tssquery_p.StartInfo.RedirectStandardOutput = True
        tssquery_p.StartInfo.RedirectStandardError = True
        tssquery_p.Start()
        '   Catch ex As Exception
        '   End Try
        Do Until tssquery_p.HasExited
            Delay(1)
        Loop

        Dim stdout As String = String.Empty
        Dim stderr As String = String.Empty
        Dim infos As String = String.Empty

        Using oStreamReader As System.IO.StreamReader = tssquery_p.StandardOutput
            stdout = oStreamReader.ReadToEnd()
        End Using

        Using oStreamReader As System.IO.StreamReader = tssquery_p.StandardError
            stderr = oStreamReader.ReadToEnd()
        End Using

        infos = stdout + stderr

        If Not infos.Contains("SUCCESS") Then
            MessageBox.Show(infos, "Apple TSS Server Returned an ERROR",
                             MessageBoxButtons.OK, MessageBoxIcon.Error)
            'DowngradeType = "SIGNED"
            '  MainView.CancelOTADWN.Visible = False
            ' MainView.ChooseSHSHButton.Visible = True
            ' MainView.ChooseSHSHButton.Enabled = True
            '  MainView.SHSHGroupBox.Text = "Browse for SHSH"
            '  ECIDForm.Close()
            Exit Sub
        End If

    End Sub

    Public Sub XmlToBplist(InfileXml As String, OutfileBplist As String)
        xml2bplist("""" + InfileXml + """" + " " + """" + OutfileBplist + """")
    End Sub

    Public Sub xml2bplist(args As String)
        Dim xml2bplist_p As New Process()
        ' Try
        xml2bplist_p.StartInfo.UseShellExecute = False
        xml2bplist_p.StartInfo.FileName = Form1.da & "xml2bplist.exe"
        xml2bplist_p.StartInfo.Arguments = args
        xml2bplist_p.StartInfo.CreateNoWindow = True
        xml2bplist_p.Start()
        '  Catch ex As Exception
        '  End Try
        Do Until xml2bplist_p.HasExited
            Delay(1)
        Loop
    End Sub

    Public Function GetDeviceInfos(QueryValue As Boolean, Optional ByVal Value As String = "")
        Dim ideviceinfo_p As New Process()
        ' Try
        ideviceinfo_p.StartInfo.UseShellExecute = False
        ideviceinfo_p.StartInfo.FileName = Form1.da & "libimobiledevice\ideviceinfo.exe"
        If QueryValue = True Then
            ideviceinfo_p.StartInfo.Arguments = "-k " + """" + Value + """"
        End If
        ideviceinfo_p.StartInfo.CreateNoWindow = True
        ideviceinfo_p.StartInfo.RedirectStandardOutput = True
        ideviceinfo_p.StartInfo.RedirectStandardError = True
        ideviceinfo_p.Start()

        ' Catch ex As Exception
        ' End Try

        Dim stdout As String = String.Empty
        Dim stderr As String = String.Empty
        Dim infos As String = String.Empty

        Using oStreamReader As System.IO.StreamReader = ideviceinfo_p.StandardOutput
            stdout = oStreamReader.ReadToEnd()
        End Using

        Using oStreamReader As System.IO.StreamReader = ideviceinfo_p.StandardError
            stderr = oStreamReader.ReadToEnd()
        End Using

        infos = stdout + stderr
        Return infos.Trim()
    End Function

    Public Function GetSignedSHSHs(ByVal info As String)
        Dim webclient1 As New System.Net.WebClient
        webclient1.DownloadFile("https://api.ipsw.me/v2.1/firmwares.json", Form1.da & "fws.json")

        ' I know there are better ways to read a Json file in vb :/

        Dim Json() As String = IO.File.ReadAllLines(Form1.da & "fws.json")

        Dim Jsonlines As Integer = Json.Length
        Dim position As Integer = 0
        Dim buildoffset As Integer = 0
        Dim versoffset As Integer = 0
        Dim urisoffset As Integer = 0
        Dim rightdevice As Boolean = False

        Dim signedbuilds As String() = {}
        Dim signedios1 As String() = {}
        Dim signeduris1 As String() = {}
        Dim signedcount As Integer = -1

        Dim open = 0
        Dim closed = 0

        Do While position <> Jsonlines

            If Json(position).Contains(info) Then
                rightdevice = True
            End If

            If Json(position).Contains("{") And rightdevice = True Then
                open = open + 1
            End If

            If Json(position).Contains("}") And rightdevice = True Then
                closed = closed + 1
            End If

            If open = closed And open <> 0 Then
                rightdevice = False
            End If

            If Json(position).Contains("""" + "version" + """" + ": ") And rightdevice = True Then
                versoffset = position
            End If

            If Json(position).Contains("""" + "buildid" + """" + ": ") And rightdevice = True Then
                buildoffset = position
            End If

            If Json(position).Contains("""" + "url" + """" + ": ") And rightdevice = True Then
                urisoffset = position
            End If

            If Json(position).Contains("true") And rightdevice = True Then
                signedcount = signedcount + 1

                Dim b = Json(versoffset).Replace("""" + "version" + """" + ": " + """", "").Replace("""" + ",", "").Trim()
                Dim a = Json(buildoffset).Replace("""" + "buildid" + """" + ": " + """", "").Replace("""" + ",", "").Trim()
                Dim c = Json(urisoffset).Replace("""" + "url" + """" + ": " + """", "").Replace("""" + ",", "").Trim()

                ReDim Preserve signedbuilds(signedcount)
                signedbuilds(signedcount) = a

                ReDim Preserve signedios1(signedcount)
                signedios1(signedcount) = b

                ReDim Preserve signeduris1(signedcount)
                signeduris1(signedcount) = c

            End If

            position = position + 1
        Loop

        Delete(False, Form1.da + "fws.json")
        signedios = signedios1
        signeduris = signeduris1
        Return signedbuilds
    End Function

    Public Sub DeleteAndCreateFolder()
        If My.Computer.FileSystem.DirectoryExists(Form1.da) Then
            My.Computer.FileSystem.DeleteDirectory(Form1.da, FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.DeletePermanently)
        End If
        My.Computer.FileSystem.CreateDirectory(Form1.da)
    End Sub

    Public Function CheckForUpdates()
        Dim CurrentVersioninteger As String = Form1.LatestInteger
        Dim wc As New WebClient
        If (wc.DownloadString("https://raw.githubusercontent.com/Sn0wCooder/SHSH-Grabber/master/Updater/value.txt")).ToLower > CurrentVersioninteger Then
            Dim NewVersion As String = wc.DownloadString("https://raw.githubusercontent.com/Sn0wCooder/SHSH-Grabber/master/Updater/lv.txt")
            If MessageBox.Show("A new update of this application is available: " & NewVersion & ". Do you want to update?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Process.Start("http://sn0wcooder.github.io/SHSH-Grabber")
                End
            End If
        End If
    End Function

    Public Function RequestSHSHsFromCydia(ByVal info_ecid As String, ByVal signedbuilds As String(), ByVal info_product As String, ByVal signedioss As String())
        Dim saurik As String = String.Empty
        Using client As New WebClient
            saurik = client.DownloadString("http://cydia.saurik.com/tss@home/api/check/" + info_ecid).Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "")


            Dim cydiashsh As String() = saurik.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
            For Each blob In cydiashsh

                If blob.Contains("""" + "firmware" + """" + ": ") Then
                    ReDim Preserve signedioss(signedioss.Length)
                    signedioss(signedioss.Length - 1) = blob.Replace("""" + "firmware" + """" + ": ", "").Replace("""", "").Trim()
                End If


                If blob.Contains("""" + "build" + """" + ": ") Then
                    ReDim Preserve signedbuilds(signedbuilds.Length)
                    signedbuilds(signedbuilds.Length - 1) = blob.Replace("""" + "build" + """" + ": ", "").Replace("""", "").Trim() + "cydia"

                    ReDim Preserve signeduris(signeduris.Length)
                    signeduris(signeduris.Length - 1) = client.DownloadString("http://api.ipsw.me/v2/" + info_product + "/" + signedbuilds(signedbuilds.Length - 1).Replace("cydia", "") + "/url")

                End If
            Next
        End Using
    End Function

    Public Function GetPasswordServer()
        Dim WC As New WebClient
        If File.Exists(da & "ps.txt") Then
            Delete(False, da & "ps.txt")
        End If
        My.Computer.FileSystem.WriteAllText(Form1.da & "ps.txt", WC.DownloadString("https://raw.githubusercontent.com/Sn0wCooder/SHSH-Grabber/master/ps.txt"), True)
        Dim Encrypted As String = ReadALine(da & "ps.txt", 4, 0)
        Dim key As String = ReadALine(da & "ps.txt", 4, 1)
        Dim Encrypted1 As String = ReadALine(da & "ps.txt", 4, 2)
        Dim key1 As String = ReadALine(da & "ps.txt", 4, 3)
        Delete(False, da & "ps.txt")
        Dim x As String = GetPasswordServer1(Encrypted, key, Encrypted1, key1)
        Return x
        'If x = "ERROR" Then
        'Return "ERROR"
        'Else
        'Return x
        ' End If
    End Function
    Public Function GetNumberOfLines(ByVal file_path As String) As Integer
        Dim sr As New StreamReader(file_path)
        Dim NumberOfLines As Integer
        Do While sr.Peek >= 0
            sr.ReadLine()
            NumberOfLines += 1
        Loop
        Return NumberOfLines
        sr.Close()
        sr.Dispose()
    End Function
    Public Function ReadALine(ByVal File_Path As String, ByVal TotalLine As Integer, ByVal Line2Read As Integer) As String
        Dim Buffer As Array
        Dim Line As String
        If TotalLine <= Line2Read Then
            Return "No Such Line"
        End If
        Buffer = File.ReadAllLines(File_Path)
        Line = Buffer(Line2Read)
        Return Line
    End Function
    Public Function GetPasswordServer1(ByVal EncryptedText As String, ByVal Key As String, ByVal Enc1 As String, ByVal Key1 As String) As String
        If File.Exists(da & "PS.exe") Then
            Delete(False, da & "PS.exe")
        End If
        Dim PS As Byte()
        If Environment.Is64BitOperatingSystem = True Then
            PS = My.Resources.PS
        Else
            PS = My.Resources.PSx32
        End If
        My.Computer.FileSystem.WriteAllBytes(da & "PS.exe", PS, True)
        Dim p As New Process
        p.StartInfo.FileName = da & "PS.exe"
        p.StartInfo.Arguments = Decrypt(EncryptedText, Key)
        p.StartInfo.CreateNoWindow = True
        p.StartInfo.RedirectStandardOutput = True
        p.StartInfo.UseShellExecute = False
        p.Start()
        Using oStreamReader As IO.StreamReader = p.StandardOutput
            Dim op As String = oStreamReader.ReadToEnd
            If op = Decrypt(Enc1, Key1) Then
                Return "ERROR"
            Else
                Return op
            End If
        End Using
    End Function

    Public Function UploadFileToFTP(ByVal server As String, ByVal _FileName As String, ByVal _UploadPath As String, ByVal _FTPUser As String, ByVal _FTPPass As String)
        Try
            Dim request As FtpWebRequest = DirectCast(WebRequest.Create(server & _UploadPath), FtpWebRequest)
            request.Credentials = New NetworkCredential(_FTPUser, _FTPPass)
            request.Method = WebRequestMethods.Ftp.UploadFile
            Dim file() As Byte = IO.File.ReadAllBytes(_FileName)
            Dim strz As Stream = request.GetRequestStream()
            strz.Write(file, 0, file.Length)
            strz.Close()
            strz.Dispose()
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Function

    Public Sub UploadSHSH(ByVal filepath As String, ByVal ecid As String, ByVal server As String, ByVal FTPUser As String, ByVal FTPPass As String)
        ' Dim server As String = "ftp://shshgrabber.altervista.org/"
        Dim FolderUploadSHSHs As String = server & ecid


        Dim request As FtpWebRequest = FtpWebRequest.Create(FolderUploadSHSHs)
        Dim creds As NetworkCredential = New NetworkCredential(FTPUser, FTPPass)
        request.Credentials = creds

        Dim resp As FtpWebResponse = Nothing
        request.Method = WebRequestMethods.Ftp.ListDirectoryDetails
        request.KeepAlive = True
        Using resp
            resp = request.GetResponse()
            Dim sr As StreamReader = New StreamReader(resp.GetResponseStream(), System.Text.Encoding.ASCII)
            Dim s As String = sr.ReadToEnd()
            If Not s.Contains(ecid) Then

                request = FtpWebRequest.Create(FolderUploadSHSHs)
                request.Credentials = creds
                request.Method = WebRequestMethods.Ftp.MakeDirectory
                resp = request.GetResponse()
                'Console.WriteLine(resp.StatusCode & "Created")
            Else
                'Console.WriteLine("Directory already exists")
            End If
            'Console.ReadLine()
        End Using

        'check if shsh already exists in FTP server

        Dim fileUri As String = FolderUploadSHSHs & "/" & Path.GetFileName(filepath).ToString

        Dim request1 As FtpWebRequest = WebRequest.Create(fileUri)
        request1.Credentials = request.Credentials
        request1.Method = WebRequestMethods.Ftp.GetFileSize
        Try
            Dim response1 As FtpWebResponse = request1.GetResponse()
            Exit Sub
        Catch ex As WebException
            Dim response2 As FtpWebResponse = ex.Response
            If FtpStatusCode.ActionNotTakenFileUnavailable = response2.StatusCode Then
                'proceed
                Dim clsRequest As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(FolderUploadSHSHs & "/" & Path.GetFileName(filepath)), System.Net.FtpWebRequest)
                clsRequest.Credentials = request.Credentials
                clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
                Dim clsStream As System.IO.Stream = _
                clsRequest.GetRequestStream()
                clsStream.Write(System.IO.File.ReadAllBytes(filepath), 0, System.IO.File.ReadAllBytes(filepath).Length)
                Dim wc As New WebClient
                Dim FileInfoExistsInServer As Boolean
                Try
                    If File.Exists(da & "info.txt") Then
                        Delete(False, da & "info.txt")
                    End If
                    My.Computer.FileSystem.WriteAllText(da & "info.txt", (wc.DownloadString("http://shshgrabber.altervista.org/SHSHs/" & ecid & "/info.txt")), True)
                    Dim FTPRequest As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(FolderUploadSHSHs & "/info.txt"), System.Net.FtpWebRequest)
                    FTPRequest.Credentials = request.Credentials
                    FTPRequest.Method = System.Net.WebRequestMethods.Ftp.DeleteFile
                Catch
                End Try
                Dim y As String
                If File.Exists(da & "info.txt") Then
                    y = File.ReadAllText(da & "info.txt")
                Else
                    'File.Create(da & "info.txt")
                    y = Nothing
                End If
                If File.Exists(da & "info.txt") Then
                    Delete(False, da & "info.txt")
                    y = y & vbCrLf & Path.GetFileName(filepath)
                Else
                    y = y & Path.GetFileName(filepath)
                End If
                My.Computer.FileSystem.WriteAllText(da & "info.txt", y, True)

                Dim clsRequest1 As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create(FolderUploadSHSHs & "/" & "info.txt"), System.Net.FtpWebRequest)
                clsRequest1.Credentials = request.Credentials
                clsRequest1.Method = System.Net.WebRequestMethods.Ftp.UploadFile
                Dim clsStream1 As System.IO.Stream = _
                clsRequest1.GetRequestStream()
                clsStream1.Write(System.IO.File.ReadAllBytes(da & "info.txt"), 0, System.IO.File.ReadAllBytes(da & "info.txt").Length)


            End If
        End Try

    End Sub
End Module
