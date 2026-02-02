<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agenda.aspx.cs" Inherits="SGN.Web.Agenda.Agenda" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v25.2, Version=25.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"   Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../Content/all.css" />
    <link rel="stylesheet" href="../Content/generic/pageMinimalStyle.css" />

    <script src="../Scripts/sweetalert2.all.min.js"></script>
    <link rel="stylesheet" href="../Scripts/sweetalert2.min.css" />
    <script src="../Scripts/mensajes.js"></script>

    <title>SGN - Agenda</title>

    <script type="text/javascript">
        window.onresize = function () { AdjustSize(); };

        function AdjustSize() {
            var topPanelEl = document.getElementById('TopPanel');
            if (!topPanelEl) topPanelEl = document.querySelector('.topPanel');
            if (!topPanelEl && typeof TopPanel !== 'undefined' && TopPanel.GetMainElement) topPanelEl = TopPanel.GetMainElement();

            var topHeight = 0;
            if (topPanelEl) topHeight = (topPanelEl.offsetHeight || topPanelEl.clientHeight || 0);

            var height = document.getElementById('maindiv').clientHeight - topHeight;
            scAgenda.SetHeight(height);
        }

        function OnSchedulerInit(s, e) { AdjustSize(); }
    </script>
</head>

<body>
    <form id="form1" runat="server" class="Principal">
        <section class="CLPageContent" id="maindiv">

 


            <dx:aspxscheduler id="scAgenda" runat="server" activeviewtype="WorkWeek">
                <views>
                    <workweekview enabled="true" />
                    <fullweekview enabled="true" />
                    <weekview enabled="false" />
                </views>


                <storage enablereminders="false">
                    <appointments autoretrieveid="true" />
                </storage>
            </dx:aspxscheduler>

            <asp:ObjectDataSource ID="appointmentDataSource" runat="server"
                DataObjectTypeName="SGN.Web.Agenda.AgendaCita"
                TypeName="SGN.Web.Agenda.AgendaCitaDataSource"
                SelectMethod="SelectMethodHandler"
                InsertMethod="InsertMethodHandler"
                UpdateMethod="UpdateMethodHandler"
                DeleteMethod="DeleteMethodHandler" />

        </section>
    </form>
</body>
</html>
