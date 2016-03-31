Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Linq
Imports System
Imports System.Management

Public Class Form1
    Dim loc As String = My.Computer.FileSystem.SpecialDirectories.Temp
    Dim Temp As String = loc + "/iconsole_imo"
    Dim installerPath As String = Temp + "/idevicesyslog.exe"
    Dim syslog As New Process

    Public Sub ResetAll()
        RichTextBox1.WordWrap = False
        Label1.Text = "Connect Device"
        Button1.Text = "Refresh USB"
        If My.Computer.FileSystem.DirectoryExists(Temp) Then
            My.Computer.FileSystem.WriteAllBytes(Temp + "/idevicesyslog.exe", My.Resources.idevicesyslog, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libxml2-2.dll", My.Resources.libxml2_2, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libcurl-4.dll", My.Resources.libcurl_4, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libgcc_s_dw2-1.dll", My.Resources.libgcc_s_dw2_1, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libiconv-2.dll", My.Resources.libiconv_2, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libimobiledevice.dll", My.Resources.libimobiledevice, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libplist.dll", My.Resources.libplist, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libusbmuxd.dll", My.Resources.libusbmuxd, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libzip-2.dll", My.Resources.libzip_2, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/readline5.dll", My.Resources.readline5, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/zlib1.dll", My.Resources.zlib1, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libeay32.dll", My.Resources.libeay32, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/ssleay32.dll", My.Resources.ssleay32, False)
        Else
            System.IO.Directory.CreateDirectory(Temp)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/idevicesyslog.exe", My.Resources.idevicesyslog, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libxml2-2.dll", My.Resources.libxml2_2, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libcurl-4.dll", My.Resources.libcurl_4, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libgcc_s_dw2-1.dll", My.Resources.libgcc_s_dw2_1, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libiconv-2.dll", My.Resources.libiconv_2, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libimobiledevice.dll", My.Resources.libimobiledevice, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libplist.dll", My.Resources.libplist, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libusbmuxd.dll", My.Resources.libusbmuxd, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libzip-2.dll", My.Resources.libzip_2, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/readline5.dll", My.Resources.readline5, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/zlib1.dll", My.Resources.zlib1, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/libeay32.dll", My.Resources.libeay32, False)
            My.Computer.FileSystem.WriteAllBytes(Temp + "/ssleay32.dll", My.Resources.ssleay32, False)

        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Button1.Text = "Syslog" Then
            Button1.Enabled = False
            Dim installerPath As String = Temp + "/idevicesyslog.exe"
            StartProcess(installerPath, " -d")


        Else
            If IsUserlandConnected() = True Then
                Label1.Text = "iOS device detected!"
                Button1.Enabled = True
                Button1.Text = "Syslog"
            Else
                ResetAll()
            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ResetAll()

    End Sub

    Private Sub StartProcess(path As String, sArgs As String)

        Dim Proc = New Process()

        Proc.StartInfo.FileName = path
        Proc.StartInfo.Arguments = sArgs

        Proc.StartInfo.RedirectStandardOutput = True
        Proc.StartInfo.RedirectStandardError = True
        Proc.EnableRaisingEvents = True
        Application.DoEvents()
        Proc.StartInfo.CreateNoWindow = True
        Proc.StartInfo.UseShellExecute = False

        AddHandler Proc.ErrorDataReceived, AddressOf proc_OutputDataReceived
        AddHandler Proc.OutputDataReceived, AddressOf proc_OutputDataReceived
        Proc.Start()
        Proc.BeginErrorReadLine()
        Proc.BeginOutputReadLine()

    End Sub

    Delegate Sub UpdateTextBoxDelg(ByVal text As String)
    Public myDelegate As UpdateTextBoxDelg = New UpdateTextBoxDelg(AddressOf UpdateTextBox)


    Public Sub proc_OutputDataReceived(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        On Error Resume Next
        If Me.InvokeRequired = True Then
            Me.Invoke(myDelegate, e.Data)
        Else
            UpdateTextBox(e.Data)
        End If

    End Sub

    Public Sub UpdateTextBox(ByVal text As String)

        RichTextBox1.AppendText(text & " " & vbNewLine)

        ' Coloring option ( need some modifications and improvements )

        'Dim noticestring As String = " <notice>:"
        'Dim warningstring As String = " <warning>:"
        'Dim errorstring As String = " <error>:"
        'Dim debugstring As String = " <debug>:"

        'Dim wordslist As New List(Of String)
        'wordslist.Add(noticestring)
        'wordslist.Add(warningstring)
        'wordslist.Add(errorstring)
        'wordslist.Add(debugstring)

        'Dim len As Integer = text.Length

        'For Each word As String In wordslist

        '    If word = noticestring Then
        '        Dim lastindex = text.LastIndexOf(word)
        '        Dim index As Integer = 0

        '        While index < lastindex
        '            If Not RichTextBox1.SelectionColor = Color.Green Then
        '                RichTextBox1.Find(word, index, len, RichTextBoxFinds.None)
        '                RichTextBox1.SelectionColor = Color.Green
        '                index = text.IndexOf(word, index) + 1

        '            Else
        '                Exit While
        '            End If


        '        End While
        '    End If

        '    If word = warningstring Then
        '        Dim lastindex = RichTextBox1.Text.LastIndexOf(word)
        '        Dim index As Integer = 0

        '        While index < lastindex

        '            If Not RichTextBox1.SelectionColor = Color.Yellow Then
        '                RichTextBox1.Find(word, index, len, RichTextBoxFinds.None)
        '                RichTextBox1.SelectionColor = Color.Yellow
        '                index = RichTextBox1.Text.IndexOf(word, index) + 1

        '            Else
        '                Exit While
        '            End If
        '        End While
        '    End If

        '    If word = errorstring Then
        '        Dim lastindex = RichTextBox1.Text.LastIndexOf(word)
        '        Dim index As Integer = 0

        '        While index < lastindex

        '            If Not RichTextBox1.SelectionColor = Color.Red Then
        '                RichTextBox1.Find(word, index, len, RichTextBoxFinds.None)
        '                RichTextBox1.SelectionColor = Color.Red
        '                index = RichTextBox1.Text.IndexOf(word, index) + 1

        '            Else
        '                Exit While
        '            End If

        '        End While
        '    End If

        '    If word = debugstring Then
        '        Dim lastindex = RichTextBox1.Text.LastIndexOf(word)
        '        Dim index As Integer = 0

        '        While index < lastindex
        '            If Not RichTextBox1.SelectionColor = Color.Magenta Then
        '                RichTextBox1.Find(word, index, len, RichTextBoxFinds.None)
        '                RichTextBox1.SelectionColor = Color.Magenta
        '                index = RichTextBox1.Text.IndexOf(word, index) + 1

        '            Else
        '                Exit While
        '            End If
        '        End While
        '    End If


        'Next


        'Dim noticestring As String = " <notice>:"
        'Dim warningstring As String = " <warning>:"
        'Dim errorstring As String = " <error>:"
        'Dim debugstring As String = " <debug>:"

        'Dim index0 As Integer = RichTextBox1.Find(noticestring)
        'Dim index1 As Integer = RichTextBox1.Find(warningstring)
        'Dim index2 As Integer = RichTextBox1.Find(errorstring)
        'Dim index3 As Integer = RichTextBox1.Find(debugstring)

        'RichTextBox1.Find(noticestring)
        'RichTextBox1.SelectionColor = Color.Green
        'RichTextBox1.DeselectAll()

        'RichTextBox1.Find(warningstring)
        'RichTextBox1.SelectionColor = Color.Yellow
        'RichTextBox1.DeselectAll()

        'RichTextBox1.Find(errorstring)
        'RichTextBox1.SelectionColor = Color.Red
        'RichTextBox1.DeselectAll()

        'RichTextBox1.Find(debugstring)
        'RichTextBox1.SelectionColor = Color.DarkRed
        'RichTextBox1.DeselectAll()

        'RichTextBox1.SelectionStart = index0 + noticestring.Length
        'RichTextBox1.SelectionStart = index1 + warningstring.Length
        'RichTextBox1.SelectionStart = index2 + errorstring.Length
        'RichTextBox1.SelectionStart = index3 + debugstring.Length
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub
End Class
