using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
#region #usings_default
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
#endregion #usings_default

public partial class _Default : System.Web.UI.Page {
    ASPxSchedulerStorage Storage { get { return ASPxScheduler1.Storage; } }
    ObjectDataSourceRowInsertionProvider insertionProvider = new ObjectDataSourceRowInsertionProvider();

    protected void Page_Load(object sender, EventArgs e) {
        insertionProvider.ProvideRowInsertion(this.ASPxScheduler1, this.appointmentDataSource);

        SetupMappings();
        #region #mappings
        ASPxScheduler1.Storage.Appointments.CustomFieldMappings.Add(
    new ASPxAppointmentCustomFieldMapping("Field1", "Amount"));
        ASPxScheduler1.Storage.Appointments.CustomFieldMappings.Add(
    new ASPxAppointmentCustomFieldMapping("Field2", "Memo"));
        #endregion #mappings
        ResourceFiller.FillResources(this.ASPxScheduler1.Storage, 3);

        ASPxScheduler1.AppointmentDataSource = appointmentDataSource;
        ASPxScheduler1.DataBind();

        ASPxScheduler1.Start = DateTime.Today;
        ASPxScheduler1.GroupType = SchedulerGroupType.Resource;

        ASPxScheduler1.Views.TimelineView.Styles.VerticalResourceHeader.Width = 500;
        ASPxScheduler1.Views.TimelineView.Styles.TimelineCellBody.Height = 500;
    }
    #region #appointmentformshowing
    protected void ASPxScheduler1_AppointmentFormShowing(object sender, AppointmentFormEventArgs e) {
        e.Container =
            new UserAppointmentFormTemplateContainer((ASPxScheduler)sender);
    }
    #endregion #appointmentformshowing
    #region #beforeexecutecallbackcommand
    protected void ASPxScheduler1_BeforeExecuteCallbackCommand(
        object sender, SchedulerCallbackCommandEventArgs e) {
        if(e.CommandId == SchedulerCallbackCommandId.AppointmentSave) {
            e.Command = new UserAppointmentSaveCallbackCommand((ASPxScheduler)sender);
        }
    }
    #endregion #beforeexecutecallbackcommand
    void SetupMappings() {
        ASPxAppointmentMappingInfo mappings = Storage.Appointments.Mappings;
        Storage.BeginUpdate();
        try {
            mappings.AppointmentId = "Id";
            mappings.Start = "StartTime";
            mappings.End = "EndTime";
            mappings.Subject = "Subject";
            mappings.AllDay = "AllDay";
            mappings.Description = "Description";
            mappings.Label = "Label";
            mappings.Location = "Location";
            mappings.RecurrenceInfo = "RecurrenceInfo";
            mappings.ReminderInfo = "ReminderInfo";
            mappings.ResourceId = "OwnerId";
            mappings.Status = "Status";
            mappings.Type = "EventType";
        }
        finally {
            Storage.EndUpdate();
        }
    }
    //Initilazing ObjectDataSource
    protected void appointmentsDataSource_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        e.ObjectInstance = new CustomEventDataSource(GetCustomEvents());
    }
    CustomEventList GetCustomEvents() {
        CustomEventList events = Session["ListBoundModeObjects"] as CustomEventList;
        if(events == null) {
            events = new CustomEventList();
            Session["ListBoundModeObjects"] = events;
        }
        return events;
    }
}
