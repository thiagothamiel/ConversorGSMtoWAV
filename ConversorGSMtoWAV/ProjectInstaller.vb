Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.ServiceProcess
Public Class ProjectInstaller

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add initialization code after the call to InitializeComponent

    End Sub

    Private Sub ServiceInstaller1_AfterInstall(ByVal sender As System.Object, ByVal e As System.Configuration.Install.InstallEventArgs) Handles ServiceInstaller1.AfterInstall
        Dim sc As New ServiceController("ConversorGSMtoWAV")
        Try
            sc.Start()
            sc.WaitForStatus(ServiceControllerStatus.Running)

            MsgBox("Serviço iniciado com sucesso!", MsgBoxStyle.Information, "Atenção")

        Catch ex As Exception
            MsgBox("Não foi possível inicializar o serviço. Esse procedimento deverá ser feito manualmente!", MsgBoxStyle.Critical, "Atenção")
        End Try
    End Sub
End Class
