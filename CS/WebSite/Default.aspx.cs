using System;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxScheduler;


public partial class Default : System.Web.UI.Page {
    Object lastInsertedAppointmentId;

    protected void Page_Load(object sender, EventArgs e) {
        ASPxScheduler1.Storage.Appointments.AutoRetrieveId = true;
    }

    #region #setappointment
    CustomEventDataSource objectInstance;
    // Obtain the ID of the last inserted appointment from the object data source and assign it to the appointment in the ASPxScheduler storage.
    protected void ASPxScheduler1_AppointmentRowInserted(object sender, DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertedEventArgs e) {
        e.KeyFieldValue = this.objectInstance.ObtainLastInsertedId();
    }

    protected void appointmentsDataSource_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        this.objectInstance = new CustomEventDataSource(GetCustomEvents());
        e.ObjectInstance = this.objectInstance;
    }
    CustomEventList GetCustomEvents() {
        CustomEventList events = Session["CustomEventListData"] as CustomEventList;
        if (events == null) {
            events = new CustomEventList();
            Session["CustomEventListData"] = events;
        }
        return events;
    }
    #endregion #setappointment


    #region #appointmentid
    // The following code is unnecessary if the ASPxScheduler.Storage.Appointments.AutoRetrieveId option is TRUE.
    protected void ASPxScheduler1_AppointmentInserted(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e) {
        SetAppointmentId(sender, e);
    }
    protected void appointmentsDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        this.lastInsertedAppointmentId = e.ReturnValue;
    }
    void SetAppointmentId(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e) {
        DevExpress.Web.ASPxScheduler.ASPxSchedulerStorage storage = (DevExpress.Web.ASPxScheduler.ASPxSchedulerStorage)sender;
        DevExpress.XtraScheduler.Appointment apt = (DevExpress.XtraScheduler.Appointment)e.Objects[0];
        storage.SetAppointmentId(apt, this.lastInsertedAppointmentId);
    }
    #endregion #appointmentid

    #region #beforeexecutecallbackcommand
    protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, DevExpress.Web.ASPxScheduler.SchedulerCallbackCommandEventArgs e) {
        if (e.CommandId == SchedulerCallbackCommandId.AppointmentSave) {
            e.Command = new CustomAppointmentSaveCallbackCommand((ASPxScheduler)sender);
        }
    }
    #endregion #beforeexecutecallbackcommand
}
