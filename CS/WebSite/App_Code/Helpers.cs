using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using System.Collections.Generic;

public class ResourceFiller {
    public static string[] Users = new string[] { "Sarah Brighton", "Ryan Fischer", "Andrew Miller" };
    public static string[] Usernames = new string[] { "sbrighton", "rfischer", "amiller" };

    public static void FillResources(ASPxSchedulerStorage storage, int count) {
        ResourceCollection resources = storage.Resources.Items;
        storage.BeginUpdate();
        try {
            int cnt = Math.Min(count, Users.Length);
            for(int i = 1; i <= cnt; i++) {
                resources.Add(storage.CreateResource(Usernames[i - 1], Users[i - 1]));
            }
        }
        finally {
            storage.EndUpdate();
        }
    }
}
public class ObjectDataSourceRowInsertionProvider {
    List<Object> lastInsertedIdList = new List<object>();

    public void ProvideRowInsertion(ASPxScheduler control, ObjectDataSource dataSource) {
        control.AppointmentsInserted += new PersistentObjectsEventHandler(control_AppointmentsInserted);
        control.AppointmentCollectionCleared += new EventHandler(control_AppointmentCollectionCleared);
        dataSource.Inserted += new ObjectDataSourceStatusEventHandler(dataSource_Inserted);
    }

    void control_AppointmentCollectionCleared(object sender, EventArgs e) {
        this.lastInsertedIdList.Clear();
    }
    void dataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        this.lastInsertedIdList.Add(e.ReturnValue);
    }
    void control_AppointmentsInserted(object sender, PersistentObjectsEventArgs e) {
        ASPxSchedulerStorage storage = (ASPxSchedulerStorage)sender;
        int count = e.Objects.Count;
        System.Diagnostics.Debug.Assert(count == this.lastInsertedIdList.Count);
        for(int i = 0; i < count; i++) { //B184873
            Appointment apt = (Appointment)e.Objects[i];
            storage.SetAppointmentId(apt, this.lastInsertedIdList[i]);
        }
        this.lastInsertedIdList.Clear();
    }
}