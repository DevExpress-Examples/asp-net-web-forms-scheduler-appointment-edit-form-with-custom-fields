Imports System
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxScheduler


Partial Public Class [Default]
	Inherits System.Web.UI.Page

	Private lastInsertedAppointmentId As Object

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		ASPxScheduler1.Storage.Appointments.AutoRetrieveId = True
	End Sub

	#Region "#setappointment"
	Private objectInstance As CustomEventDataSource
	' Obtain the ID of the last inserted appointment from the object data source and assign it to the appointment in the ASPxScheduler storage.
	Protected Sub ASPxScheduler1_AppointmentRowInserted(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertedEventArgs)
		e.KeyFieldValue = Me.objectInstance.ObtainLastInsertedId()
	End Sub

	Protected Sub appointmentsDataSource_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
		Me.objectInstance = New CustomEventDataSource(GetCustomEvents())
		e.ObjectInstance = Me.objectInstance
	End Sub
	Private Function GetCustomEvents() As CustomEventList
		Dim events As CustomEventList = TryCast(Session("CustomEventListData"), CustomEventList)
		If events Is Nothing Then
			events = New CustomEventList()
			Session("CustomEventListData") = events
		End If
		Return events
	End Function
	#End Region ' #setappointment


	#Region "#appointmentid"
	' The following code is unnecessary if the ASPxScheduler.Storage.Appointments.AutoRetrieveId option is TRUE.
	Protected Sub ASPxScheduler1_AppointmentInserted(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.PersistentObjectsEventArgs)
		SetAppointmentId(sender, e)
	End Sub
	Protected Sub appointmentsDataSource_Inserted(ByVal sender As Object, ByVal e As ObjectDataSourceStatusEventArgs)
		Me.lastInsertedAppointmentId = e.ReturnValue
	End Sub
	Private Sub SetAppointmentId(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.PersistentObjectsEventArgs)
		Dim storage As DevExpress.Web.ASPxScheduler.ASPxSchedulerStorage = DirectCast(sender, DevExpress.Web.ASPxScheduler.ASPxSchedulerStorage)
		Dim apt As DevExpress.XtraScheduler.Appointment = CType(e.Objects(0), DevExpress.XtraScheduler.Appointment)
		storage.SetAppointmentId(apt, Me.lastInsertedAppointmentId)
	End Sub
	#End Region ' #appointmentid

	#Region "#beforeexecutecallbackcommand"
	Protected Sub ASPxScheduler1_BeforeExecuteCallbackCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.SchedulerCallbackCommandEventArgs)
		If e.CommandId = SchedulerCallbackCommandId.AppointmentSave Then
			e.Command = New CustomAppointmentSaveCallbackCommand(DirectCast(sender, ASPxScheduler))
		End If
	End Sub
	#End Region ' #beforeexecutecallbackcommand
End Class
