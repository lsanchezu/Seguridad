<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptEmpresa.aspx.cs" Inherits="Komatsu.Core.Seguridad.Mvc.Report.RptEmpresa" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 300px">
    <form id="form1" runat="server">
    <div style="height: 292px">
    
        <asp:ScriptManager ID="smRptEmpresa" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="rvRptEmpresa" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="482px" 
            Width="100%" ViewStateMode="Enabled" ShowPrintButton="False" 
            ShowZoomControl="False">
            <%--<ServerReport ReportServerUrl="http://certsqlr2/Reportserver" />--%>
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
