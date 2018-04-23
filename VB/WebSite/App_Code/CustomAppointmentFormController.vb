Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.XtraScheduler


Public Class CustomAppointmentFormController
    Inherits AppointmentFormController

    Public Sub New(ByVal control As ASPxScheduler, ByVal apt As Appointment)
        MyBase.New(control, apt)
    End Sub

    Public Property CustomInfoField() As String
        Get
            Return CStr(EditedAppointmentCopy.CustomFields("ApptCustomInfo"))
        End Get
        Set(ByVal value As String)
            EditedAppointmentCopy.CustomFields("ApptCustomInfo") = value
        End Set
    End Property

    Private Property SourceCustomInfoField() As String
        Get
            Return CStr(SourceAppointment.CustomFields("ApptCustomInfo"))
        End Get
        Set(ByVal value As String)
            SourceAppointment.CustomFields("ApptCustomInfo") = value
        End Set
    End Property

    Public Overrides Function IsAppointmentChanged() As Boolean
        Dim isChanged As Boolean = MyBase.IsAppointmentChanged()
        Return isChanged OrElse SourceCustomInfoField <> CustomInfoField
    End Function
End Class
