﻿Module pnp
    Dim info_64 As Boolean
    Public Function ProductNameParser(productname As String)
        If productname = "iPad1,1" Then
            info_64 = False
        ElseIf productname = "iPad2,1" Then
            info_64 = False
        ElseIf productname = "iPad2,2" Then
            info_64 = False
        ElseIf productname = "iPad2,3" Then
            info_64 = False
        ElseIf productname = "iPad2,4" Then
            info_64 = False
        ElseIf productname = "iPad2,5" Then
            info_64 = False
        ElseIf productname = "iPad2,6" Then
            info_64 = False
        ElseIf productname = "iPad2,7" Then
            info_64 = False
        ElseIf productname = "iPad3,1" Then
            info_64 = False
        ElseIf productname = "iPad3,2" Then
            info_64 = False
        ElseIf productname = "iPad3,3" Then
            info_64 = False
        ElseIf productname = "iPad3,4" Then
            info_64 = False
        ElseIf productname = "iPad3,5" Then
            info_64 = False
        ElseIf productname = "iPad3,6" Then
            info_64 = False
        ElseIf productname = "iPad4,1" Then
            info_64 = True
        ElseIf productname = "iPad4,2" Then
            info_64 = True
        ElseIf productname = "iPad4,3" Then
            info_64 = True
        ElseIf productname = "iPad4,4" Then
            info_64 = False
        ElseIf productname = "iPad4,5" Then
            info_64 = False
        ElseIf productname = "iPad4,6" Then
            info_64 = False
        ElseIf productname = "iPad4,7" Then
            info_64 = True
        ElseIf productname = "iPad4,8" Then
            info_64 = True
        ElseIf productname = "iPad4,9" Then
            info_64 = True
        ElseIf productname = "iPad5,1" Then
            info_64 = True
        ElseIf productname = "iPad5,2" Then
            info_64 = True
        ElseIf productname = "iPad5,3" Then
            info_64 = True
        ElseIf productname = "iPad5,4" Then
            info_64 = True
        ElseIf productname = "iPad6,7" Then
            info_64 = True
        ElseIf productname = "iPad6,8" Then
            info_64 = True
        ElseIf productname = "iPhone1,1" Then
            info_64 = False
        ElseIf productname = "iPhone1,2" Then
            info_64 = False
        ElseIf productname = "iPhone2,1" Then
            info_64 = False
        ElseIf productname = "iPhone3,1" Then
            info_64 = False
        ElseIf productname = "iPhone3,2" Then
            info_64 = False
        ElseIf productname = "iPhone3,3" Then
            info_64 = False
        ElseIf productname = "iPhone4,1" Then
            info_64 = False
        ElseIf productname = "iPhone5,1" Then
            info_64 = False
        ElseIf productname = "iPhone5,2" Then
            info_64 = False
        ElseIf productname = "iPhone5,3" Then
            info_64 = False
        ElseIf productname = "iPhone5,4" Then
            info_64 = False
        ElseIf productname = "iPhone6,1" Then
            info_64 = True
        ElseIf productname = "iPhone6,2" Then
            info_64 = True
        ElseIf productname = "iPhone7,1" Then
            info_64 = True
        ElseIf productname = "iPhone7,2" Then
            info_64 = True
        ElseIf productname = "iPhone8,1" Then
            info_64 = True
        ElseIf productname = "iPhone8,2" Then
            info_64 = True
        ElseIf productname = "iPod1,1" Then
            info_64 = False
        ElseIf productname = "iPod2,1" Then
            info_64 = False
        ElseIf productname = "iPod3,1" Then
            info_64 = False
        ElseIf productname = "iPod4,1" Then
            info_64 = False
        ElseIf productname = "iPod5,1" Then
            info_64 = False
        ElseIf productname = "iPod7,1" Then
            info_64 = True
        ElseIf productname = "AppleTV5,3" Then
            info_64 = True
        Else
            info_64 = True
        End If
        Return info_64
    End Function
End Module
