<%@ Page Title="Welcome to FSM Reports" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master"  AutoEventWireup="true" CodeBehind="ProjectMainPage.aspx.cs" Inherits="Reports.Reports.ProjectMainPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3 class="pull-left"><span class="">
        <asp:Label ID="lblHeading" runat="server" Text="Welcome to FSM Reports."></asp:Label></span>
    </h3>
     <div class="clear"></div>
    <ul>
        <li style="font:50px"><a style="font-size: small;"" href="/Reports/REPXReports/RepxReportMain.aspx?reportname=OrderSummary">Order Print</a></li>
    </ul>


</asp:Content>
