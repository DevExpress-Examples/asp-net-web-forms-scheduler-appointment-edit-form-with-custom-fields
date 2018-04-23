using DevExpress.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using System.Web.UI;


public class CustomAppointmentSaveCallbackCommand : AppointmentFormSaveCallbackCommand {
    public CustomAppointmentSaveCallbackCommand(ASPxScheduler control) : base(control) { }

    protected internal new CustomAppointmentFormController Controller {
        get { return (CustomAppointmentFormController)base.Controller; }
    }

    protected override AppointmentFormController CreateAppointmentFormController(DevExpress.XtraScheduler.Appointment apt) {
        return new CustomAppointmentFormController(Control, apt);
    }

    protected override Control FindControlByID(string id) {
        return FindTemplateControl(TemplateContainer, id);
    }

    System.Web.UI.Control FindTemplateControl(System.Web.UI.Control RootControl, string id) {
        System.Web.UI.Control foundedControl = RootControl.FindControl(id);
        if (foundedControl == null) {
            foreach (System.Web.UI.Control item in RootControl.Controls) {
                foundedControl = FindTemplateControl(item, id);
                if (foundedControl != null) break;
            }
        }
        return foundedControl;
    }

    protected override void AssignControllerValues() {
        base.AssignControllerValues();

        ASPxComboBox cbSubject = FindControlByID("cbSubject") as ASPxComboBox;
        ASPxTextBox tbCustomInfo = FindControlByID("tbCustomInfo") as ASPxTextBox;

        Controller.Subject = cbSubject.Text;
        Controller.CustomInfoField = tbCustomInfo.Text;
    }
}
