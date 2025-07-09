<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="panelInicial.aspx.cs" Inherits="SGN.Web.Estadisticas.panelInicial" %>

<%@ Register Assembly="DevExpress.XtraCharts.v24.2.Web, Version=24.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v24.2, Version=24.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../Content/all.css" />
    <link rel="stylesheet" href="../Content/generic/pageMinimalStyle.css" />
    <script src="../Scripts/sweetalert.min.js"></script>
    <script src="../Scripts/mensajes.js"></script>

    <title>SGN</title>
    <script type="text/javascript">
        /* Script de funcionalidad de la pagina OJO solo colocar en este bloque */
        window.onresize = function (event) {
            AdjustSize();
        };

        function OnInit(s, e) {
            s.GetWindowElement(-1).className += " popupStyle";
        }

        function AdjustSize() {

            var height = document.getElementById('maindiv').clientHeight - 100;  // I have some buttons below the grid so needed -50

            gvArea.SetHeight(height);

        }





    </script>
</head>
<body>
    <form id="form1" runat="server" class="Principal">

        <div style="display: flex; justify-content: center; align-items: center; height: 100%;">
            <dx:WebChartControl ID="chartEstadisticaActosSimple" runat="server" Height="700px"
                Width="1200px"
                ClientInstanceName="chartEstadisticaActosSimple" OnCustomCallback="chartEstadisticaActosSimple_CustomCallback"
                ToolTipEnabled="True" CrosshairEnabled="True" RenderFormat="Svg">
                <Legend Name="Default Legend" AlignmentVertical="Top"  ></Legend>
                <SeriesSerializable>
                    <dx:Series Name="Estatus"    LegendTextPattern="{A} : {V:F1}">
                        <Points>
                        </Points>
                        <ViewSerializable>
                            <dx:PieSeriesView Rotation="90" RuntimeExploding="True">
                                <Titles>
                                    <dx:SeriesTitle Dock="Bottom" Text="Total: {TV:#} Expedientes" />
                                </Titles>
                            </dx:PieSeriesView>
                        </ViewSerializable>
                        <LabelSerializable>
                            <dx:PieSeriesLabel Position="Radial"  ColumnIndent="50" TextColor="Black" BackColor="Transparent" Font="Tahoma, 8pt, style=Bold" TextPattern="{VP:P0}">
                                <Border Visibility="False"></Border>
                            </dx:PieSeriesLabel>
                        </LabelSerializable>
                    </dx:Series>
                </SeriesSerializable>
                <BorderOptions Visibility="False" />
                <Titles>
                    <dx:ChartTitle Text="Powered by Consultoria-It.Com"></dx:ChartTitle>

                </Titles>
                <DiagramSerializable>
                    <dx:SimpleDiagram></dx:SimpleDiagram>
                </DiagramSerializable>
            </dx:WebChartControl>
        </div>


    </form>
</body>
</html>
