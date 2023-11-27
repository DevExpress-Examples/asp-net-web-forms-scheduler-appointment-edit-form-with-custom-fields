<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128546049/16.1.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2924)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# Scheduler for ASP.NET Web Forms - How to customize the appointment edit form for working with custom fields
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/128546049/)**
<!-- run online end -->

This example provides a step-by-step tutorial for simple appointment form customization. You'll learn how to replace a text box used for editing an appointment's subject with a combo box and add an additional text box for editing an appointment's custom field.

To follow this tutorial, create a project in which the [ASPxScheduler](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxScheduler.ASPxScheduler) control is bound to a collection of custom objects: [How to: Bind ASPxScheduler Appointment to ObjectDataSource](https://docs.devexpress.com/AspNet/15675/components/scheduler/examples/data-binding/how-to-bind-aspxscheduler-appointment-to-objectdatasource).

Follow the steps below to customize the appointment edit form.

## Step 1 - Update Data Source
Add a new field named "CustomInfo" to your data source. The CustomInfo field will contain text, thus, depending on your database and data provider, it could be of the **string:NVARCHAR(MAX)** type or one similar to it. Modify your select/insert/update queries to include those fields.

## Step 2 – Specify Custom Field Mappings
Add custom field mapping for the previously created CustomInfo field. You can do this in the Visual Studio Designer, in the markup or using the property grid (ASPxScheduler control ->Storage->Appointments->CustomFieldMappings).

```aspx
<CustomFieldMappings>
    <dx:ASPxAppointmentCustomFieldMapping Member="CustomInfo" Name="ApptCustomInfo" ValueType="String" />
</CustomFieldMappings>
```

Depending on your requirements, you may need to add mappings at runtime instead. In this case, use the following code in the code-behind file for the page containing the ASPxScheduler control (**Default.aspx.cs** or **Default.aspx.vb** if you use Visual Basic).

```csharp
ASPxScheduler1.Storage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("ApptCustomInfo", "CustomInfo"));
```

## Step 3 – Prepare a Custom Appointment Editing Form
Create a new folder named "CustomForms". Copy default ASPxScheduler forms and templates to your project site using the ASPxScheduler control's Smart Tag, as described in the [Dialog Forms](https://docs.devexpress.com/AspNet/15069/components/scheduler/visual-elements/aspxscheduler/dialog-forms) article. Locate the **AppointmentForm.ascx** form and the corresponding code&#0045;behind file **AppointmentForm.ascx.cs** (or **AppointmentForm.ascx.vb** if you use Visual Basic) and copy them to the newly created CustomForms folder.

## Step 4 – Register the Custom Form
After copying default ASPxScheduler forms and templates into your project, the corresponding paths will be automatically registered using the **OptionsForms** and **OptionsToolTips** properties. Change the [ASPxSchedulerOptionsForms.AppointmentFormTemplateUrl](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxScheduler.ASPxSchedulerOptionsForms.AppointmentFormTemplateUrl) property value to the value corresponding to the new location of the form's template &#0045; **~/CustomForms/AppointmentForm.ascx**. You can clear the other paths or keep them for further customization of default templates.

```aspx
<OptionsForms AppointmentFormTemplateUrl="CustomForms/AppointmentForm.ascx" />
```

## Step 5  - Add Custom Editors
In the form's markup, replace the predefined text box used for editing an appointment's subject with a combo box and add an additional text box for editing the CustomInfo field.

```aspx
<td class="dxscSingleCell">
    <table class="dxscLabelControlPair" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
        <tr>
            <td class="dxscLabelCell">
                <dx:ASPxLabel ID="lblSubject" runat="server">
                </dx:ASPxLabel>
            </td>
            <td class="dxscControlCell">
                <dx:ASPxComboBox ID="cbSubject" runat="server" ValueType="System.String"></dx:ASPxComboBox>
            </td>
        </tr>
    </table>
</td>
<td class="dxscSingleCell">
    <table class="dxscLabelControlPair" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
        <tr>
            <td class="dxscLabelCell">
                <dx:ASPxLabel ID="lblCustomInfo" runat="server" Text="Custom info:">
                </dx:ASPxLabel>
            </td>
            <td class="dxscControlCell">
                <dx:ASPxTextBox ID="tbCustomInfo" runat="server" Width="100%" />
            </td>
        </tr>
    </table>
</td>
```

> [!IMPORTANT]
> Note that the **AssociatedControlID** property of the **lblSubject** label points to the **tbSubject** text box, which has been removed from the form. This may cause unwanted re-creation of the form's controls hierarchy. As a result, the form editors will return empty values when you save an appointment. To prevent incorrect behavior, remove this property or change its value to the **cbSubject** combo box ID.

After you add custom editors to the form, you have to register them. Add IDs of the newly added editors to the array returned by the form's **GetChildEditors** method. This step ensures that the editors will be accessible by client&#0045;side scripts. After that, the method will look as follows.

```csharp
protected override ASPxEditBase[] GetChildEditors() {
    ASPxEditBase[] edits = new ASPxEditBase[] {
        lblSubject, cbSubject, tbCustomInfo,
        lblLocation, tbLocation,
        lblLabel, edtLabel,
        lblStartDate, edtStartDate,
        lblEndDate, edtEndDate,
        lblStatus, edtStatus,
        lblAllDay, chkAllDay,
        lblResource, edtResource,
        tbDescription, cbReminder, lblReminder,
        ddResource, chkReminder, GetMultiResourceEditor(),
        edtStartTime, edtEndTime, cbTimeZone
    };        
    return edits;
}
```

## Step 6 – Bind the Custom Editors to Data
In the **DataBind** method of the Edit Appointment form, you can provide values for the new editors based on the properties of the currently edited appointment. Since you use a combo box for editing an appointment's subject, you need to assign a corresponding data source to this combo box as shown below.

```csharp
private void BindSubjectCombobox() {
    List<string> subjectList = new List<string>();
    subjectList.Add("Meeting");
    subjectList.Add("Business travel");
    subjectList.Add("Phone call");
    cbSubject.DataSource = subjectList;
    cbSubject.DataBind();
}

public override void DataBind() {
    base.DataBind();

    // ...
    BindSubjectCombobox();
    cbSubject.Value = container.Subject;
    tbCustomInfo.Text = container.Appointment.CustomFields["ApptCustomInfo"] != null ? container.Appointment.CustomFields["ApptCustomInfo"].ToString() : "";
    // ...
}
```

## Step 7 – Save Editor Values
To save values of the new editors as corresponding appointment properties, override the default **AppointmentFormSaveCallbackCommand**. Create this command's descendant in the CustomForms folder or in the **App_Code** folder (for websites).

```csharp
public class CustomAppointmentSaveCallbackCommand : AppointmentFormSaveCallbackCommand {
    public CustomAppointmentSaveCallbackCommand(ASPxScheduler control) : base(control) { }
}
```

In the overridden **AssignControllerValues** method, assign the values of the new editors to the corresponding properties of the **AppointmentFormController**. Note that your custom code should be placed after you execute the base implementation of the AssignControllerValues method. Otherwise, changes made to standard appointment fields (for example, the **Controller.Subject** field) will be reset with the base implementation.

```csharp
protected override void AssignControllerValues() {
    base.AssignControllerValues();

    ASPxComboBox cbSubject = FindControlByID("cbSubject") as ASPxComboBox;
    ASPxTextBox tbCustomInfo = FindControlByID("tbCustomInfo") as ASPxTextBox;

    Controller.Subject = cbSubject.Text;
    Controller.CustomInfoField = tbCustomInfo.Text;
}
```

Make a note that the default **FindControlByID** method searches the editor located in the AppointmentFormTemplateContainer's controls collection without taking into account the complex hierarchy of the controls with multiple nesting levels. It is recommended that you also override this method as shown below.

```csharp
protected override System.Web.UI.Control FindControlByID(string id) {
    return FindTemplateControl(TemplateContainer, id);
}

System.Web.UI.Control FindTemplateControl(System.Web.UI.Control RootControl, string id) {
    System.Web.UI.Control foundedControl = RootControl.FindControl(id);
    if(foundedControl == null) {
        foreach(System.Web.UI.Control item in RootControl.Controls) {
            foundedControl = FindTemplateControl(item, id);
                if(foundedControl != null) break;
        }
    }
    return foundedControl;
}
```

## Step 8 – Create a Custom Appointment Form Controller
In the previous step, the  tbCustomInfo  editor value is saved as an appointment's custom field using the **Controller.CustomInfoField**  property. The default AppointmentFormController class does not contain such a property so you need to manually implement this property in a custom AppointmentFormController class descendant.

The code sample below demonstrates the implementation of a custom appointment form controller.

```csharp
public class CustomAppointmentFormController : AppointmentFormController {
    public CustomAppointmentFormController(ASPxScheduler control, Appointment apt)
        : base(control, apt) {
    }
   
    // Provides access to a user-specified value of the custom field.
    public string CustomInfoField {
        get { return (string)EditedAppointmentCopy.CustomFields["ApptCustomInfo"]; }
        set { EditedAppointmentCopy.CustomFields["ApptCustomInfo"] = value; }
    }

    // Provides access to an initial value of the custom field.
    string SourceCustomInfoField {
        get { return (string)SourceAppointment.CustomFields["ApptCustomInfo"]; }
        set { SourceAppointment.CustomFields["ApptCustomInfo"] = value; }
    }
    
    // Checks whether or not an appointment has been modified taking a custom field value into account.
    public override bool IsAppointmentChanged() {
        bool isChanged = base.IsAppointmentChanged();
        return isChanged || SourceCustomInfoField != CustomInfoField;
    }
}

public class CustomAppointmentSaveCallbackCommand : AppointmentFormSaveCallbackCommand {
    public CustomAppointmentSaveCallbackCommand(ASPxScheduler control) : base(control) { }
    protected internal new CustomAppointmentFormController Controller {
        get { return (CustomAppointmentFormController)base.Controller; }
    }    protected override AppointmentFormController CreateAppointmentFormController(DevExpress.XtraScheduler.Appointment apt) {
        return new CustomAppointmentFormController(Control, apt);
    }
}
```

## Step 9 – Execute the Custom Save Command
Your newly created **CustomAppointmentSaveCallbackCommand** should be executed instead of the default save command whenever a user issues the **Save** command. To accomplish this, handle the [ASPxScheduler.BeforeExecuteCallbackCommand](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxScheduler.ASPxScheduler.BeforeExecuteCallbackCommand) event as illustrated below.

```csharp
protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, DevExpress.Web.ASPxScheduler.SchedulerCallbackCommandEventArgs e) {
    if(e.CommandId == SchedulerCallbackCommandId.AppointmentSave) {
        e.Command = new CustomAppointmentSaveCallbackCommand((ASPxScheduler)sender);
    }
}
```

## Step 10 – Get the Result
Run the project. Check to see whether or not your custom form is shown when you open an appointment for editing. Fill in the custom fields and see whether or not new values can be saved when clicking **OK**, and then restored with the next opening of the form.

## Files to Review

* [CustomAppointmentFormController.cs](./CS/WebSite/App_Code/CustomAppointmentFormController.cs) (VB: [CustomAppointmentFormController.vb](./VB/WebSite/App_Code/CustomAppointmentFormController.vb))
* [CustomAppointmentSaveCallbackCommand.cs](./CS/WebSite/App_Code/CustomAppointmentSaveCallbackCommand.cs) (VB: [CustomAppointmentSaveCallbackCommand.vb](./VB/WebSite/App_Code/CustomAppointmentSaveCallbackCommand.vb))
* [AppointmentForm.ascx](./CS/WebSite/CustomForms/AppointmentForm.ascx) (VB: [AppointmentForm.ascx](./VB/WebSite/CustomForms/AppointmentForm.ascx))
* [AppointmentForm.ascx.cs](./CS/WebSite/CustomForms/AppointmentForm.ascx.cs) (VB: [AppointmentForm.ascx.vb](./VB/WebSite/CustomForms/AppointmentForm.ascx.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
