#Region "#usings_form_cs"
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.XtraScheduler
#End Region ' #usings_form_cs
#Region "#templatecontainer"
Public Class UserAppointmentFormTemplateContainer
    Inherits AppointmentFormTemplateContainer

        Public Sub New(ByVal control As ASPxScheduler)
            MyBase.New(control)
        End Sub
        Public ReadOnly Property Field1() As Double
            Get
                Dim val As Object = Appointment.CustomFields("Field1")
                Return If(val Is System.DBNull.Value, 0, System.Convert.ToDouble(val))
            End Get
        End Property
        Public ReadOnly Property Field2() As String
            Get
                Return System.Convert.ToString(Appointment.CustomFields("Field2"))
            End Get
        End Property
End Class
#End Region ' #templatecontainer

#Region "#formcontroller"
Public Class UserAppointmentFormController
    Inherits AppointmentFormController

    Public Sub New(ByVal control As ASPxScheduler, ByVal apt As Appointment)
        MyBase.New(control, apt)
    End Sub
    Public Property Field1() As Double
        Get
            Return CDbl(EditedAppointmentCopy.CustomFields("Field1"))
        End Get
        Set(ByVal value As Double)
            EditedAppointmentCopy.CustomFields("Field1") = value
        End Set
    End Property
    Public Property Field2() As String
        Get
            Return CStr(EditedAppointmentCopy.CustomFields("Field2"))
        End Get
        Set(ByVal value As String)
            EditedAppointmentCopy.CustomFields("Field2") = value
        End Set
    End Property
    Private Property SourceField1() As Double
        Get
            Return CDbl(SourceAppointment.CustomFields("Field1"))
        End Get
        Set(ByVal value As Double)
            SourceAppointment.CustomFields("Field1") = value
        End Set
    End Property
    Private Property SourceField2() As String
        Get
            Return CStr(SourceAppointment.CustomFields("Field2"))
        End Get
        Set(ByVal value As String)
            SourceAppointment.CustomFields("Field2") = value
        End Set
    End Property
End Class
#End Region ' #formcontroller