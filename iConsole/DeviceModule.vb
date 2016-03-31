Imports System.Management

Module DeviceModule

    Public Async Function GetCMDOutput(path As String, Optional args As String = "") As Task(Of String)
        Dim proc As New Process
        proc.StartInfo.FileName = path
        proc.StartInfo.Arguments = args
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden

        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.RedirectStandardInput = True
        proc.StartInfo.RedirectStandardError = True


        proc.Start()
        'proc.WaitForExit()
        Await Task.Run(Sub()
                           Dim p As Process = Process.Start(proc.StartInfo)
                           p.WaitForExit()
                       End Sub)

        Dim output As String = proc.StandardOutput.ReadToEnd() + proc.StandardError.ReadToEnd()

        Return output
    End Function

    Public Function IsUserlandConnected()
        Dim forever As Boolean = True
        Dim USBName As String = String.Empty
        Dim USBSearcher As New ManagementObjectSearcher(
                  "root\CIMV2",
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

End Module
