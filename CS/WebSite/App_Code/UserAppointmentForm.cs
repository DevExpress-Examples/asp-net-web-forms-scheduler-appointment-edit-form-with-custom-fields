#region #usings_form_cs
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;
#endregion #usings_form_cs
#region #templatecontainer
public class UserAppointmentFormTemplateContainer :
             AppointmentFormTemplateContainer{
        public UserAppointmentFormTemplateContainer(ASPxScheduler control)
            : base(control){}
        public double Field1{
            get{
                object val = Appointment.CustomFields["Field1"];
                return (val == System.DBNull.Value) ? 0 : System.Convert.ToDouble(val);
            }
        }
        public string Field2{
            get{
                return System.Convert.ToString(Appointment.CustomFields["Field2"]);
            }
        }
    }
#endregion #templatecontainer

#region #formcontroller
public class UserAppointmentFormController : AppointmentFormController
{
    public UserAppointmentFormController(ASPxScheduler control, Appointment apt)
        : base(control, apt) { }
    public double Field1
    {
        get { return (double)EditedAppointmentCopy.CustomFields["Field1"]; }
        set { EditedAppointmentCopy.CustomFields["Field1"] = value; }
    }
    public string Field2
    {
        get { return (string)EditedAppointmentCopy.CustomFields["Field2"]; }
        set { EditedAppointmentCopy.CustomFields["Field2"] = value; }
    }
    double SourceField1
    {
        get { return (double)SourceAppointment.CustomFields["Field1"]; }
        set { SourceAppointment.CustomFields["Field1"] = value; }
    }
    string SourceField2
    {
        get { return (string)SourceAppointment.CustomFields["Field2"]; }
        set { SourceAppointment.CustomFields["Field2"] = value; }
    }
}
#endregion #formcontroller