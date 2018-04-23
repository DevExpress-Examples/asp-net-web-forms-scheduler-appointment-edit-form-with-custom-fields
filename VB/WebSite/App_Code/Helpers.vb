Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler
Imports System.Collections.Generic

Public Class ResourceFiller
    Public Shared Users() As String = { "Sarah Brighton", "Ryan Fischer", "Andrew Miller" }
    Public Shared Usernames() As String = { "sbrighton", "rfischer", "amiller" }

    Public Shared Sub FillResources(ByVal storage As ASPxSchedulerStorage, ByVal count As Integer)
        Dim resources As ResourceCollection = storage.Resources.Items
        storage.BeginUpdate()
        Try
            Dim cnt As Integer = Math.Min(count, Users.Length)
            For i As Integer = 1 To cnt
                resources.Add(storage.CreateResource(Usernames(i - 1), Users(i - 1)))
            Next i
        Finally
            storage.EndUpdate()
        End Try
    End Sub
End Class
Public Class ObjectDataSourceRowInsertionProvider
    Private lastInsertedIdList As New List(Of Object)()

    Public Sub ProvideRowInsertion(ByVal control As ASPxScheduler, ByVal dataSource As ObjectDataSource)
        AddHandler control.AppointmentsInserted, AddressOf control_AppointmentsInserted
        AddHandler control.AppointmentCollectionCleared, AddressOf control_AppointmentCollectionCleared
        AddHandler dataSource.Inserted, AddressOf dataSource_Inserted
    End Sub

    Private Sub control_AppointmentCollectionCleared(ByVal sender As Object, ByVal e As EventArgs)
        Me.lastInsertedIdList.Clear()
    End Sub
    Private Sub dataSource_Inserted(ByVal sender As Object, ByVal e As ObjectDataSourceStatusEventArgs)
        Me.lastInsertedIdList.Add(e.ReturnValue)
    End Sub
    Private Sub control_AppointmentsInserted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
        Dim storage As ASPxSchedulerStorage = DirectCast(sender, ASPxSchedulerStorage)
        Dim count As Integer = e.Objects.Count
        System.Diagnostics.Debug.Assert(count = Me.lastInsertedIdList.Count)
        For i As Integer = 0 To count - 1 'B184873
            Dim apt As Appointment = CType(e.Objects(i), Appointment)
            storage.SetAppointmentId(apt, Me.lastInsertedIdList(i))
        Next i
        Me.lastInsertedIdList.Clear()
    End Sub
End Class