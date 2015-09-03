Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.ServiceProcess
Imports System.Diagnostics
Public Class ConversorGSMtoWAV
    Public fswVerificaPasta As FileSystemWatcher
    Dim sPastaPadrao As String = "C:\GSMtoWAV\"
    Protected Overrides Sub OnStart(ByVal args() As String)
        sIniciarVerificacao()
    End Sub
    Protected Overrides Sub OnStop()
        log("Serviço parado")
    End Sub
    Private Sub sIniciarVerificacao()
        Try
            log("---------------------------------------------------")
            log("Iniciando verificação de pasta")
            fswVerificaPasta = New System.IO.FileSystemWatcher()
            fswVerificaPasta.Path = sPastaPadrao & "GRAVACOES\"
            fswVerificaPasta.NotifyFilter = IO.NotifyFilters.DirectoryName
            fswVerificaPasta.NotifyFilter = fswVerificaPasta.NotifyFilter Or _
                                       IO.NotifyFilters.FileName
            fswVerificaPasta.NotifyFilter = fswVerificaPasta.NotifyFilter Or _
                                       IO.NotifyFilters.Attributes
            AddHandler fswVerificaPasta.Created, AddressOf sArquivoCriado
            fswVerificaPasta.EnableRaisingEvents = True
            log("Verificação de pasta iniciada com sucesso em: " & sPastaPadrao)
            log("---------------------------------------------------")
        Catch ex As Exception
            log("sIniciarVerificacao: " & ex.Message)
            log("---------------------------------------------------")
            evento.WriteEntry("Falha para iniciar a verificação da pasta: " & sPastaPadrao & "GRAVACOES\")
            evento.WriteEntry(ex.Message)
        End Try
    End Sub
    Private Sub sArquivoCriado(ByVal source As Object, ByVal e As  _
                           System.IO.FileSystemEventArgs)
        If e.ChangeType = IO.WatcherChangeTypes.Created Then
            If e.FullPath.ToLower().EndsWith(".gsm") Then
                Dim sComandoShell As String = sPastaPadrao & "SOX\sox.exe " & e.FullPath & " -r 8000 -c1 " & e.FullPath.ToLower().Replace(".gsm", ".wav")
                log("Realizando conversão do arquivo: " & sComandoShell)
                If Not File.Exists(sPastaPadrao & "SOX\sox.exe") Then
                    log("--------- ATENÇÃO--------- Programa de conversão não localizado: " & sPastaPadrao & "SOX\sox.exe")
                    Exit Sub
                End If
                Shell(sComandoShell, AppWinStyle.Hide, True)
            End If
            If e.FullPath.ToLower().EndsWith(".wav") Then
                Try
                    log("-----------deletando arquivo: " & e.FullPath.ToLower().Replace(".wav", ".gsm"))
                    IO.File.Delete(e.FullPath.ToLower().Replace(".wav", ".gsm"))
                Catch ex As Exception
                    log("sArquivoCriado: " & ex.Message)
                End Try
            End If
        End If
        sDeletarArquivosAntigos()
    End Sub
    Private Sub sDeletarArquivosAntigos()
        Try
            For Each arq As String In Directory.GetFiles(sPastaPadrao & "GRAVACOES\", "*.wav")
                Dim dif As TimeSpan = Now.TimeOfDay.Subtract(File.GetCreationTime(arq).TimeOfDay())
                If dif.TotalMinutes > 3 Then
                    log("-----------deletando arquivo antigo: " & arq)
                    Kill(arq)
                End If
            Next
        Catch ex As Exception
            log("sDeletarArquivosAntigos: " & ex.Message)
        End Try
    End Sub
    Private Sub log(ByVal sTexto)
        Try
            If Not Directory.Exists(sPastaPadrao & "LOG\") Then Directory.CreateDirectory(sPastaPadrao & "LOG\")
            IO.File.AppendAllText(sPastaPadrao & "LOG\log_" & Format(Now, "ddMMyyyy") & ".log", Format(Now, "dd/MM/yyyy HH:mm:ss") & " [" & sTexto & "]" & vbCrLf)
        Catch ex As Exception
            evento.WriteEntry(ex.Message)
        End Try
    End Sub
End Class
