#region #usings_callback_cs
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
#endregion #usings_callback_cs
#region #callbackcommand
public class UserAppointmentSaveCallbackCommand :
            AppointmentFormSaveCallbackCommand
    {
        public UserAppointmentSaveCallbackCommand(ASPxScheduler control)
            : base(control){}
        protected internal new UserAppointmentFormController Controller{
            get { return (UserAppointmentFormController)base.Controller; }
        }

        protected override void AssignControllerValues()
        {
            ASPxTextBox tbField1 = (ASPxTextBox)FindControlByID("tbField1");
            ASPxMemo memField2 = (ASPxMemo)FindControlByID("memField2");
            Controller.Field1 = System.Convert.ToDouble(tbField1.Text);
            Controller.Field2 = memField2.Text;
            base.AssignControllerValues();
        }
        protected override AppointmentFormController
                CreateAppointmentFormController(Appointment apt)
        {
            return new UserAppointmentFormController(Control, apt);
        }
    }
#endregion #callbackcommand