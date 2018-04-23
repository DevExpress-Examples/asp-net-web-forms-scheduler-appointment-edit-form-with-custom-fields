using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;


public class CustomAppointmentFormController : AppointmentFormController {
    public CustomAppointmentFormController(ASPxScheduler control, Appointment apt)
        : base(control, apt) { }

    public string CustomInfoField {
        get { return (string)EditedAppointmentCopy.CustomFields["ApptCustomInfo"]; }
        set { EditedAppointmentCopy.CustomFields["ApptCustomInfo"] = value; }
    }

    string SourceCustomInfoField {
        get { return (string)SourceAppointment.CustomFields["ApptCustomInfo"]; }
        set { SourceAppointment.CustomFields["ApptCustomInfo"] = value; }
    }

    public override bool IsAppointmentChanged() {
        bool isChanged = base.IsAppointmentChanged();
        return isChanged || SourceCustomInfoField != CustomInfoField;
    }
}
