Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
#Region "#usings_default"
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler
#End Region ' #usings_default

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Private ReadOnly Property Storage() As ASPxSchedulerStorage
        Get
            Return ASPxScheduler1.Storage
        End Get
    End Property
    Private insertionProvider As New ObjectDataSourceRowInsertionProvider()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        insertionProvider.ProvideRowInsertion(Me.ASPxScheduler1, Me.appointmentDataSource)

        SetupMappings()
'        #Region "#mappings"
        ASPxScheduler1.Storage.Appointments.CustomFieldMappings.Add(New ASPxAppointmentCustomFieldMapping("Field1", "Amount"))
        ASPxScheduler1.Storage.Appointments.CustomFieldMappings.Add(New ASPxAppointmentCustomFieldMapping("Field2", "Memo"))
'        #End Region ' #mappings
        ResourceFiller.FillResources(Me.ASPxScheduler1.Storage, 3)

        ASPxScheduler1.AppointmentDataSource = appointmentDataSource
        ASPxScheduler1.DataBind()

        ASPxScheduler1.Start = Date.Today
        ASPxScheduler1.GroupType = SchedulerGroupType.Resource

        ASPxScheduler1.Views.TimelineView.Styles.VerticalResourceHeader.Width = 500
        ASPxScheduler1.Views.TimelineView.Styles.TimelineCellBody.Height = 500
    End Sub
    #Region "#appointmentformshowing"
    Protected Sub ASPxScheduler1_AppointmentFormShowing(ByVal sender As Object, ByVal e As AppointmentFormEventArgs)
        e.Container = New UserAppointmentFormTemplateContainer(DirectCast(sender, ASPxScheduler))
    End Sub
    #End Region ' #appointmentformshowing
    #Region "#beforeexecutecallbackcommand"
    Protected Sub ASPxScheduler1_BeforeExecuteCallbackCommand(ByVal sender As Object, ByVal e As SchedulerCallbackCommandEventArgs)
        If e.CommandId = SchedulerCallbackCommandId.AppointmentSave Then
            e.Command = New UserAppointmentSaveCallbackCommand(DirectCast(sender, ASPxScheduler))
        End If
    End Sub
    #End Region ' #beforeexecutecallbackcommand
    Private Sub SetupMappings()
        Dim mappings As ASPxAppointmentMappingInfo = Storage.Appointments.Mappings
        Storage.BeginUpdate()
        Try
            mappings.AppointmentId = "Id"
            mappings.Start = "StartTime"
            mappings.End = "EndTime"
            mappings.Subject = "Subject"
            mappings.AllDay = "AllDay"
            mappings.Description = "Description"
            mappings.Label = "Label"
            mappings.Location = "Location"
            mappings.RecurrenceInfo = "RecurrenceInfo"
            mappings.ReminderInfo = "ReminderInfo"
            mappings.ResourceId = "OwnerId"
            mappings.Status = "Status"
            mappings.Type = "EventType"
        Finally
            Storage.EndUpdate()
        End Try
    End Sub
    'Initilazing ObjectDataSource
    Protected Sub appointmentsDataSource_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
        e.ObjectInstance = New CustomEventDataSource(GetCustomEvents())
    End Sub
    Private Function GetCustomEvents() As CustomEventList

        Dim events_Renamed As CustomEventList = TryCast(Session("ListBoundModeObjects"), CustomEventList)
        If events_Renamed Is Nothing Then
            events_Renamed = New CustomEventList()
            Session("ListBoundModeObjects") = events_Renamed
        End If
        Return events_Renamed
    End Function
End Class
