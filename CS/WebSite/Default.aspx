<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v11.2.Core, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" OnAppointmentFormShowing="ASPxScheduler1_AppointmentFormShowing"  OnBeforeExecuteCallbackCommand="ASPxScheduler1_BeforeExecuteCallbackCommand">
            <OptionsForms AppointmentFormTemplateUrl="~/MyForms/UserAppointmentForm.ascx" />
            <Views>
                <DayView>
                    <TimeRulers>
                        <cc1:TimeRuler>
                        </cc1:TimeRuler>
                    </TimeRulers>
                </DayView>
                <WorkWeekView>
                    <TimeRulers>
                        <cc1:TimeRuler>
                        </cc1:TimeRuler>
                    </TimeRulers>
                </WorkWeekView>
            </Views>
            <OptionsBehavior ClientTimeZoneId="Pacific" />
            <OptionsMenu EnableMenuScrolling="True" />
            <ClientSideEvents MouseUp="" />
        </dxwschs:ASPxScheduler>
    
    </div>
    <asp:ObjectDataSource ID="appointmentDataSource" runat="server" DataObjectTypeName="CustomEvent"
            TypeName="CustomEventDataSource" DeleteMethod="DeleteMethodHandler" SelectMethod="SelectMethodHandler" InsertMethod="InsertMethodHandler" UpdateMethod="UpdateMethodHandler" OnObjectCreated="appointmentsDataSource_ObjectCreated"></asp:ObjectDataSource> 

    </form>
</body>
</html>
