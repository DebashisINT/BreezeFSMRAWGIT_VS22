<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                09-02-2023        2.0.39           Pallab              25656 : Master module design modification 
====================================================== Revision History ==========================================================--%>

<%@ Page Title="Countries" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" Inherits="ERP.OMS.Management.Master.management_master_Country" CodeBehind="Country.aspx.cs" %>

<%--<%@ Register Assembly="DevExpress.Web.v10.2.Export, Version=10.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Export" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe000001" %>
<%@ Register Assembly="DevExpress.Web.v10.2" Namespace="DevExpress.Web.ASPxPopupControl"
    TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dxhf" %>--%>
<%--<%@ Register assembly="DevExpress.Web.v10.2" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2" namespace="DevExpress.Web" tagprefix="dx" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style type="text/css">
        .stateDiv {
            height: 25px;
            width: 68px;
            float: left;
        }

        .dxpc-headerContent {
            color: white;
        }
    </style>
    <script type="text/javascript" src="../../CentralData/JSScript/GenericJScript.js"></script>

    <script language="javascript" type="text/javascript">
        //function SignOff() {
        //    window.parent.SignOff()
        //}
        //function height() {
        //    if (document.body.scrollHeight <= 500)
        //        window.frameElement.height = '500px';
        //    else
        //        window.frameElement.height = document.body.scrollHeight;
        //    window.frameElement.widht = document.body.scrollWidht;
        //}
    </script>



    <style>
        .dxgvHeader {
            border: 1px solid #2c4182 !important;
            background-color: #415698 !important;
        }

            .dxgvHeader, .dxgvHeader table {
                color: #fff !important;
            }

            /*Rev 1.0*/

        body , .dxtcLite_PlasticBlue
        {
            font-family: 'Poppins', sans-serif !important;
        }

    #BranchGridLookup {
        min-height: 34px;
        border-radius: 5px;
    }

    .dxeButtonEditButton_PlasticBlue {
        background: #094e8c !important;
        border-radius: 4px !important;
        padding: 0 4px !important;
    }

    .dxeButtonDisabled_PlasticBlue {
        background: #ababab !important;
    }

    .chosen-container-single .chosen-single div {
        background: #094e8c;
        color: #fff;
        border-radius: 4px;
        height: 30px;
        top: 1px;
        right: 1px;
        /*position:relative;*/
    }

        .chosen-container-single .chosen-single div b {
            display: none;
        }

        .chosen-container-single .chosen-single div::after {
            /*content: '<';*/
            content: url(../../../assests/images/left-arw.png);
            position: absolute;
            top: 2px;
            right: 3px;
            font-size: 13px;
            transform: rotate(269deg);
            font-weight: 500;
        }

    .chosen-container-active.chosen-with-drop .chosen-single div {
        background: #094e8c;
        color: #fff;
    }

        .chosen-container-active.chosen-with-drop .chosen-single div::after {
            transform: rotate(90deg);
            right: 7px;
        }

    .calendar-icon {
        position: absolute;
        bottom: 9px;
        right: 5px;
        z-index: 0;
        cursor: pointer;
    }

    .date-select .form-control {
        position: relative;
        z-index: 1;
        background: transparent;
    }

    #ddlState, #ddlPartyType, #divoutletStatus, #slmonth, #slyear {
        -webkit-appearance: none;
        position: relative;
        z-index: 1;
        background-color: transparent;
    }

    .h-branch-select {
        position: relative;
    }

        .h-branch-select::after {
            /*content: '<';*/
            content: url(../../../assests/images/left-arw.png);
            position: absolute;
            top: 33px;
            right: 13px;
            font-size: 18px;
            transform: rotate(269deg);
            font-weight: 500;
            background: #094e8c;
            color: #fff;
            height: 18px;
            display: block;
            width: 28px;
            /* padding: 10px 0; */
            border-radius: 4px;
            text-align: center;
            line-height: 19px;
            z-index: 0;
        }

        select:not(.btn):focus
        {
            border-color: #094e8c;
        }

        select:not(.btn):focus-visible
        {
            box-shadow: none;
            outline: none;
            
        }

    .multiselect.dropdown-toggle {
        text-align: left;
    }

    .multiselect.dropdown-toggle, #ddlMonth, #ddlYear {
        -webkit-appearance: none;
        position: relative;
        z-index: 1;
        background-color: transparent;
    }

    select:not(.btn) {
        padding-right: 30px;
        -webkit-appearance: none;
        position: relative;
        z-index: 1;
        background-color: transparent;
    }

    #ddlShowReport:focus-visible {
        box-shadow: none;
        outline: none;
        border: 1px solid #164f93;
    }

    #ddlShowReport:focus {
        border: 1px solid #164f93;
    }

    .whclass.selectH:focus-visible {
        outline: none;
    }

    .whclass.selectH:focus {
        border: 1px solid #164f93;
    }

    .dxeButtonEdit_PlasticBlue {
        border: 1px Solid #ccc;
    }

    .chosen-container-single .chosen-single {
        border: 1px solid #ccc;
        background: #fff;
        box-shadow: none;
    }

    .daterangepicker td.active, .daterangepicker td.active:hover {
        background-color: #175396;
    }

    label {
        font-weight: 500;
    }

    .dxgvHeader_PlasticBlue {
        background: #164f94;
    }

    .dxgvSelectedRow_PlasticBlue td.dxgv {
        color: #fff;
    }

    .dxeCalendarHeader_PlasticBlue {
        background: #185598;
    }

    .dxgvControl_PlasticBlue, .dxgvDisabled_PlasticBlue,
    .dxbButton_PlasticBlue,
    .dxeCalendar_PlasticBlue,
    .dxeEditArea_PlasticBlue,
    .dxgvControl_PlasticBlue, .dxgvDisabled_PlasticBlue{
        font-family: 'Poppins', sans-serif !important;
    }

    .dxgvEditFormDisplayRow_PlasticBlue td.dxgv, .dxgvDataRow_PlasticBlue td.dxgv, .dxgvDataRowAlt_PlasticBlue td.dxgv, .dxgvSelectedRow_PlasticBlue td.dxgv, .dxgvFocusedRow_PlasticBlue td.dxgv {
        font-weight: 500;
    }

    .btnPadding .btn {
        padding: 7px 14px !important;
        border-radius: 4px;
    }

    .btnPadding {
        padding-top: 24px !important;
    }

    .dxeButtonEdit_PlasticBlue {
        border-radius: 5px;
        height: 34px;
    }

    #dtFrom, #dtTo {
        position: relative;
        z-index: 1;
        background: transparent;
    }

    #tblshoplist_wrapper .dataTables_scrollHeadInner table tr th {
        background: #165092;
        vertical-align: middle;
        font-weight: 500;
    }

    /*#refreshgrid {
        background: #e5e5e5;
        padding: 0 10px;
        margin-top: 15px;
        border-radius: 8px;
    }*/

    .styled-checkbox {
        position: absolute;
        opacity: 0;
        z-index: 1;
    }

        .styled-checkbox + label {
            position: relative;
            /*cursor: pointer;*/
            padding: 0;
            margin-bottom: 0 !important;
        }

            .styled-checkbox + label:before {
                content: "";
                margin-right: 6px;
                display: inline-block;
                vertical-align: text-top;
                width: 16px;
                height: 16px;
                /*background: #d7d7d7;*/
                margin-top: 2px;
                border-radius: 2px;
                border: 1px solid #c5c5c5;
            }

        .styled-checkbox:hover + label:before {
            background: #094e8c;
        }


        .styled-checkbox:checked + label:before {
            background: #094e8c;
        }

        .styled-checkbox:disabled + label {
            color: #b8b8b8;
            cursor: auto;
        }

            .styled-checkbox:disabled + label:before {
                box-shadow: none;
                background: #ddd;
            }

        .styled-checkbox:checked + label:after {
            content: "";
            position: absolute;
            left: 3px;
            top: 9px;
            background: white;
            width: 2px;
            height: 2px;
            box-shadow: 2px 0 0 white, 4px 0 0 white, 4px -2px 0 white, 4px -4px 0 white, 4px -6px 0 white, 4px -8px 0 white;
            transform: rotate(45deg);
        }

    #dtstate {
        padding-right: 8px;
    }

    .modal-header {
        background: #094e8c !important;
        background-image: none !important;
        padding: 11px 20px;
        border: none;
        border-radius: 5px 5px 0 0;
        color: #fff;
        border-radius: 10px 10px 0 0;
    }

    .modal-content {
        border: none;
        border-radius: 10px;
    }

    .modal-header .modal-title {
        font-size: 14px;
    }

    .close {
        font-weight: 400;
        font-size: 25px;
        color: #fff;
        text-shadow: none;
        opacity: .5;
    }

    #EmployeeTable {
        margin-top: 10px;
    }

        #EmployeeTable table tr th {
            padding: 5px 10px;
        }

    .dynamicPopupTbl {
        font-family: 'Poppins', sans-serif !important;
    }

        .dynamicPopupTbl > tbody > tr > td,
        #EmployeeTable table tr th {
            font-family: 'Poppins', sans-serif !important;
            font-size: 12px;
        }

    .w150 {
        width: 160px;
    }

    .eqpadtbl > tbody > tr > td:not(:last-child) {
        padding-right: 20px;
    }

    #dtFrom_B-1, #dtTo_B-1 , #cmbDOJ_B-1, #cmbLeaveEff_B-1 {
        background: transparent !important;
        border: none;
        width: 30px;
        padding: 10px !important;
    }

        #dtFrom_B-1 #dtFrom_B-1Img,
        #dtTo_B-1 #dtTo_B-1Img , #cmbDOJ_B-1 #cmbDOJ_B-1Img, #cmbLeaveEff_B-1 #cmbLeaveEff_B-1Img {
            display: none;
        }

    #dtFrom_I, #dtTo_I {
        background: transparent;
    }

    .for-cust-icon {
        position: relative;
        /*z-index: 1;*/
    }

    .pad-md-18 {
        padding-top: 24px;
    }

    .open .dropdown-toggle.btn-default {
        background: transparent !important;
    }

    .input-group-btn .multiselect-clear-filter {
        height: 32px;
        border-radius: 0 4px 4px 0;
    }

    .btn .caret {
        display: none;
    }

    .iminentSpan button.multiselect.dropdown-toggle {
        height: 34px;
    }

    .col-lg-2 {
        padding-left: 8px;
        padding-right: 8px;
    }

    .dxeCalendarSelected_PlasticBlue {
        color: White;
        background-color: #185598;
    }

    .dxeTextBox_PlasticBlue
    {
            height: 34px;
            border-radius: 4px;
    }

    #cmbDOJ_DDD_PW-1
    {
        z-index: 9999 !important;
    }

    #cmbDOJ, #cmbLeaveEff
    {
        position: relative;
    z-index: 1;
    background: transparent;
    }

    .btn-sm, .btn-xs
    {
        padding: 7px 10px !important;
    }

    #PopupCountry_PW-1
    {
        width: 24% !important;
    left: 40% !important;
    }

    .stateDiv
    {
        margin-bottom: 5px !important;
    }

    /*Rev end 1.0*/
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="breadCumb">
            <span>Countries</span>
        </div>

    <div class="container">
        <div class="backBox mt-3 p-3 ">
        <div class="Main">
            <%--<div class="TitleArea">
                <strong><span style="color: #000099">Country List</span></strong>
            </div>--%>

            <div class="SearchArea clearfix">
                <div class="FilterSide mb-4">
                    <div style=" padding-right: 5px;">
                        <% if (rights.CanAdd)
                           { %>
                        <a href="javascript:void(0);" onclick="fn_PopUpOpen()" class="btn btn-success"><span>Add New</span> </a>
                        <% } %>
                        <%--<a href="javascript:ShowHideFilter('s');" class="btn btn-primary"><span>Show Filter</span></a>--%>
                        <% if (rights.CanExport)
                                               { %>
                        <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true" OnChange="if(!AvailableExportOption()){return false;}">
                            <asp:ListItem Value="0">Export to</asp:ListItem>
                            <asp:ListItem Value="1">PDF</asp:ListItem>
                            <asp:ListItem Value="2">XLS</asp:ListItem>
                            <asp:ListItem Value="3">RTF</asp:ListItem>
                            <asp:ListItem Value="4">CSV</asp:ListItem>
                        </asp:DropDownList>
                        <% } %>
                    </div>
                    <%--...................................Code Commented By Sam on 28092016................................. --%>

                    <%-- <div class="pull-left">
                        <a href="javascript:ShowHideFilter('All');" class="btn btn-primary"><span>All Records</span></a>
                    </div>--%>
                    <%-- ...................................Code Above Commented By Sam on 28092016.................................--%>

                    <%--<div class="ExportSide pull-right">
                        <div>
                            <dxe:ASPxComboBox ID="cmbExport" runat="server" AutoPostBack="true"
                                Font-Bold="False" ForeColor="black" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged"
                                ValueType="System.Int32" Width="130px">
                                <Items>
                                    <dxe:ListEditItem Text="Select" Value="0" />
                                    <dxe:ListEditItem Text="PDF" Value="1" />
                                    <dxe:ListEditItem Text="XLS" Value="2" />
                                    <dxe:ListEditItem Text="RTF" Value="3" />
                                    <dxe:ListEditItem Text="CSV" Value="4" />
                                </Items>
                                <ButtonStyle>
                                </ButtonStyle>
                                <ItemStyle>
                                    <HoverStyle>
                                    </HoverStyle>
                                </ItemStyle>
                                <Border BorderColor="black" />
                                <DropDownButton Text="Export">
                                </DropDownButton>
                            </dxe:ASPxComboBox>
                        </div>
                    </div>--%>
                </div>

            </div>

            <div class="GridViewArea">
                <dxe:ASPxGridView ID="GridCountry" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
                    KeyFieldName="cou_id" Width="100%" OnHtmlEditFormCreated="GridCountry_HtmlEditFormCreated"
                    OnCustomCallback="GridCountry_CustomCallback" SettingsBehavior-AllowFocusedRow="true">
                    <%--OnHtmlRowCreated="GridCountry_HtmlRowCreated"--%>
                    <Columns>
                        <dxe:GridViewDataTextColumn Caption="Country ID" FieldName="cou_id" ReadOnly="True"
                            Visible="False" VisibleIndex="0">
                            <EditCellStyle HorizontalAlign="Left">
                            </EditCellStyle>
                            <EditFormSettings Visible="False" VisibleIndex="1" />
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="Country Name" FieldName="cou_country" VisibleIndex="1"
                            Width="90%">
                            <EditFormSettings Visible="True" />
                        </dxe:GridViewDataTextColumn>


                        <%--                        <dxe:GridViewDataTextColumn Caption="NSE Code" FieldName="Country_NSECode" VisibleIndex="2"
                            Width="8%" Visible="false">
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="BSE Code" FieldName="Country_BSECode" VisibleIndex="3"
                            Width="8%" Visible="false">
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="MCX Code" FieldName="Country_MCXCode" VisibleIndex="4"
                            Width="8%" Visible="false">
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="MCXSX Code" FieldName="Country_MCXSXCode"
                            VisibleIndex="5" Width="8%" Visible="false">
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="NCDEX Code" FieldName="Country_NCDEXCode"
                            VisibleIndex="6" Width="8%" Visible="false">
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="CDSL Code" FieldName="Country_CdslID" VisibleIndex="7"
                            Width="8%" Visible="false">
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="NSDL Code" FieldName="Country_NsdlID" VisibleIndex="8"
                            Width="8%" Visible="false">
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="NDML Code" FieldName="Country_NDMLId" VisibleIndex="9"
                            Width="8%" Visible="false">
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="DOTEX Code" FieldName="Country_DotExID" VisibleIndex="10"
                            Width="8%" Visible="false">
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="CVLID Code" FieldName="Country_CVLID" VisibleIndex="11"
                            Width="8%" Visible="false">
                        </dxe:GridViewDataTextColumn>--%>
                        <dxe:GridViewDataTextColumn CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="6%">
                            <HeaderTemplate>
                                <span>Actions</span>
                            </HeaderTemplate>
                            <DataItemTemplate>
                                <% if (rights.CanEdit)
                                   { %>
                                <a href="javascript:void(0);" onclick="fn_EditCountry('<%#Container.KeyValue %>')" class="pad" title="Edit">
                                    <img src="../../../assests/images/Edit.png" /></a>
                                <% } %>
                                <% if (rights.CanDelete)
                                   { %>
                                <a href="javascript:void(0);" onclick="fn_DeleteCountry('<%#Container.KeyValue %>')" title="Delete">
                                    <img src="../../../assests/images/Delete.png" /></a>
                                <% } %>
                            </DataItemTemplate>
                        </dxe:GridViewDataTextColumn>
                    </Columns>
                    <SettingsSearchPanel Visible="True" />
                    <Settings ShowGroupPanel="True" ShowStatusBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="true" />
                    <%--<SettingsPager NumericButtonCount="20" PageSize="20" ShowSeparators="True" AlwaysShowPager="True">
                        <FirstPageButton Visible="True">
                        </FirstPageButton>
                        <LastPageButton Visible="True">
                        </LastPageButton>
                    </SettingsPager>
                    <SettingsBehavior ColumnResizeMode="NextColumn" />--%>




                    <ClientSideEvents EndCallback="function (s, e) {grid_EndCallBack();}" />
                </dxe:ASPxGridView>
            </div>

            <div class="PopUpArea">
                <dxe:ASPxPopupControl ID="PopupCountry" runat="server" ClientInstanceName="cPopupCountry"
                    Width="400px" Height="100px" HeaderText="Add/Modify Country" PopupHorizontalAlign="Windowcenter"
                    PopupVerticalAlign="WindowCenter" CloseAction="closeButton" Modal="true">
                    <ContentCollection>
                        <dxe:PopupControlContentControl ID="countryPopup" runat="server">
                            <div class="Top clearfix">
                                <div style="padding-top: 5px;" class="col-md-12">
                                    <div class="stateDiv" style="padding-top: 13px;">Country:<span style="color: red;">*</span></div>
                                    <div style="padding-top: 5px;">
                                        <dxe:ASPxTextBox ID="txtCountryName" ClientInstanceName="ctxtCountryName" ClientEnabled="true"
                                            runat="server" Width="236px" MaxLength="50">
                                        </dxe:ASPxTextBox>
                                        <div id="valid" style="display: none; position: absolute; right: -4px; top: 30px;">
                                            <img id="grid_DXPEForm_DXEFL_DXEditor2_EI" title="Mandatory" class="dxEditors_edtError_PlasticBlue" src="/DXR.axd?r=1_36-YRohc" alt="Required" /></div>
                                    </div>
                                </div>
                                <%--<div style="padding-top: 5px; display: none;" class="col-md-4">
                                    <div style="height: 20px; background-color: Gray; text-align: center">
                                        <h5>Static Code</h5>
                                    </div>
                                    <div style="background-color: Gray; overflow: hidden">
                                        <div style="height: 20px; width: 130px; float: left; margin-left: 70px;">Exchange</div>
                                        <div style="height: 20px; width: 200px; text-align: left; margin-left: 50px;">
                                            Value
                                        </div>
                                    </div>
                                    <div class="stateDiv" style="padding-top: 5px;">
                                        NSE Code
                                    </div>
                                    <div style="padding-top: 5px;">
                                        <dxe:ASPxTextBox ID="txtNseCode" ClientInstanceName="ctxtNseCode" ClientEnabled="true"
                                            runat="server" CssClass="StateTextbox">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </div>--%>
                                <%--<div style="padding-top: 5px; display: none" class="col-md-4">
                                    <div class="stateDiv" style="padding-top: 5px;">
                                        BSE Code
                                    </div>
                                    <div style="padding-top: 5px;">
                                        <dxe:ASPxTextBox ID="txtBseCode" ClientInstanceName="ctxtBseCode" ClientEnabled="true"
                                            runat="server" CssClass="StateTextbox">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </div>--%>
                                <%-- <br style="clear: both;" />
                                <div style="padding-top: 5px; display: none" class="col-md-4">
                                    <div class="stateDiv" style="padding-top: 5px;">
                                        MCX Code
                                    </div>
                                    <div style="padding-top: 5px;">
                                        <dxe:ASPxTextBox ID="txtMcxCode" ClientInstanceName="ctxtMcxCode" ClientEnabled="true"
                                            runat="server" CssClass="StateTextbox">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </div>--%>
                                <%--<div style="padding-top: 5px; display: none" class="col-md-4">
                                    <div class="stateDiv" style="padding-top: 5px;">
                                        MCXSX Code
                                    </div>
                                    <div style="padding-top: 5px;">
                                        <dxe:ASPxTextBox ID="txtMcsxCode" ClientInstanceName="ctxtMcsxCode" ClientEnabled="true"
                                            runat="server" CssClass="StateTextbox">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </div>--%>
                                <%--<div style="padding-top: 5px; display: none" class="col-md-4">
                                    <div class="stateDiv" style="padding-top: 5px;">
                                        NCDEX Code
                                    </div>
                                    <div style="padding-top: 5px;">
                                        <dxe:ASPxTextBox ID="txtNcdexCode" ClientInstanceName="ctxtNcdexCode" ClientEnabled="true"
                                            runat="server" CssClass="StateTextbox">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </div>--%>
                                <%-- <br style="clear: both;" />
                                <div style="padding-top: 5px; display: none" class="col-md-4">
                                    <div class="stateDiv" style="padding-top: 5px;">
                                        CDSL Code
                                    </div>
                                    <div style="padding-top: 5px;">
                                        <dxe:ASPxTextBox ID="txtCdslCode" ClientInstanceName="ctxtCdslCode" ClientEnabled="true"
                                            runat="server" CssClass="StateTextbox">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </div>
                                <div style="padding-top: 5px; display: none" class="col-md-4">
                                    <div class="stateDiv" style="padding-top: 5px;">
                                        NSDL Code
                                    </div>
                                    <div style="padding-top: 5px;">
                                        <dxe:ASPxTextBox ID="txtNsdlCode" ClientInstanceName="ctxtNsdlCode" ClientEnabled="true"
                                            runat="server" CssClass="StateTextbox">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </div>
                                <div style="padding-top: 5px; display: none" class="col-md-4">
                                    <div class="stateDiv" style="padding-top: 5px;">
                                        NDML Code
                                    </div>
                                    <div style="padding-top: 5px;">
                                        <dxe:ASPxTextBox ID="txtNdmlCode" ClientInstanceName="ctxtNdmlCode" ClientEnabled="true"
                                            runat="server" CssClass="StateTextbox">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </div>
                                <br style="clear: both;" />
                                <div style="padding-top: 5px; display: none" class="col-md-4">
                                    <div class="stateDiv" style="padding-top: 5px;">
                                        DOTEXID Code
                                    </div>
                                    <div style="padding-top: 5px;">
                                        <dxe:ASPxTextBox ID="txtDotexidCode" ClientInstanceName="ctxtDotexidCode" ClientEnabled="true"
                                            runat="server" CssClass="StateTextbox">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </div>
                                <div style="padding-top: 5px; display: none" class="col-md-4">
                                    <div class="stateDiv" style="padding-top: 5px;">
                                        CVLID Code
                                    </div>
                                    <div style="padding-top: 5px;">
                                        <dxe:ASPxTextBox ID="txtCvlidCode" ClientInstanceName="ctxtCvlidCode" ClientEnabled="true"
                                            runat="server" CssClass="StateTextbox">
                                        </dxe:ASPxTextBox>
                                    </div>
                                </div>
                                <br style="clear: both;" />--%>
                            </div>
                            <div class="ContentDiv">
                                <div class="ScrollDiv"></div>
                                <br style="clear: both;" />
                                <div class="Footer" style="padding-left: 84px;">
                                    <div style="float: left;">
                                        <dxe:ASPxButton ID="btnSave_Country" ClientInstanceName="cbtnSave_States" runat="server"
                                            AutoPostBack="False" Text="Save" CssClass="btn btn-primary">
                                            <ClientSideEvents Click="function (s, e) {btnSave_Country();}" />
                                        </dxe:ASPxButton>
                                    </div>
                                    <div style="">
                                        <dxe:ASPxButton ID="btnCancel_Country" runat="server" AutoPostBack="False" Text="Cancel" CssClass="btn btn-danger">
                                            <ClientSideEvents Click="function (s, e) {fn_btnCancel();}" />
                                        </dxe:ASPxButton>
                                    </div>
                                    <br style="clear: both;" />
                                </div>
                                <br style="clear: both;" />
                            </div>
                        </dxe:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle BackColor="LightGray" ForeColor="Black" />
                </dxe:ASPxPopupControl>
                <dxe:ASPxGridViewExporter ID="exporter" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
        </dxe:ASPxGridViewExporter>
            </div>

            <div class="HiddenFieldArea" style="display: none;">
                <dxe:ASPxHiddenField runat="server" ClientInstanceName="chfID" ID="hfID">
                </dxe:ASPxHiddenField>
            </div>
        </div>
    </div>
        </div>
    <script type="text/javascript">
        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        function fn_PopUpOpen() {
            $('#valid').attr('style', 'display:none;');
            chfID.Set("hfID", '');
            ctxtCountryName.SetText('');
            cPopupCountry.SetHeaderText('Add Country');
            //ctxtNseCode.SetText('');
            //ctxtBseCode.SetText('');
            //ctxtMcxCode.SetText('');
            //ctxtMcsxCode.SetText('');
            //ctxtNcdexCode.SetText('');
            //ctxtCdslCode.SetText('');
            //ctxtNsdlCode.SetText('');
            //ctxtNdmlCode.SetText('');
            //ctxtDotexidCode.SetText('');
            //ctxtCvlidCode.SetText('');
            cPopupCountry.Show();

        }
        function fn_EditCountry(keyValue) {
            grid.PerformCallback('Edit~' + keyValue);
        }
        function fn_DeleteCountry(keyValue) {
            //var result=confirm('Confirm delete?');
            //if(result)
            //{
            //    grid.PerformCallback('Delete~' + keyValue);
            //}
            jConfirm('Confirm delete?', 'Confirmation Dialog', function (r) {
                if (r == true) {
                    grid.PerformCallback('Delete~' + keyValue);
                }
                else {
                    return false;
                }
            });


        }
        function fn_btnCancel() {
            cPopupCountry.Hide();
        }
        function btnSave_Country() {
            var countrynm = ctxtCountryName.GetText();
            if (countrynm.trim() == '')
                //if (ctxtCountryName.GetText() == '')
            {
                $('#valid').attr('style', 'display:block;position: absolute;right: 32px;top: 17px;');
                // alert('Please Enter Country Name');
                ctxtCountryName.Focus();
            }
            else {
                var id = chfID.Get('hfID');
                if (id == '')

                    //grid.PerformCallback('savecountry~' + ctxtCountryName.GetText() + '~' + ctxtNseCode.GetText() + '~' + ctxtBseCode.GetText() + '~' + ctxtMcxCode.GetText() + '~' + ctxtMcsxCode.GetText() + '~' + ctxtNcdexCode.GetText() + '~' + ctxtCdslCode.GetText() + '~' + ctxtNsdlCode.GetText() + '~' + ctxtNdmlCode.GetText() + '~' + ctxtDotexidCode.GetText() + '~' + ctxtCvlidCode.GetText());
                    grid.PerformCallback('savecountry~' + ctxtCountryName.GetText());
                else
                    grid.PerformCallback('updatecountry~' + chfID.Get('hfID'));
            }
        }


        function grid_EndCallBack() {
            if (grid.cpEdit != null) {
                ctxtCountryName.SetText(grid.cpEdit.split('~')[0]);
                //ctxtNseCode.SetValue(grid.cpEdit.split('~')[2]);
                //ctxtBseCode.SetValue(grid.cpEdit.split('~')[3]);
                //ctxtMcxCode.SetValue(grid.cpEdit.split('~')[4]);
                //ctxtMcsxCode.SetValue(grid.cpEdit.split('~')[5]);
                //ctxtNcdexCode.SetValue(grid.cpEdit.split('~')[6]);
                //ctxtCdslCode.SetValue(grid.cpEdit.split('~')[7]);
                //ctxtNsdlCode.SetValue(grid.cpEdit.split('~')[8]);
                //ctxtNdmlCode.SetValue(grid.cpEdit.split('~')[9]);
                //ctxtDotexidCode.SetValue(grid.cpEdit.split('~')[10]);
                //ctxtCvlidCode.SetValue(grid.cpEdit.split('~')[11]);
                var hfid = grid.cpEdit.split('~')[1];
                cPopupCountry.SetHeaderText('Modify Country');
                chfID.Set("hfID", hfid);
                cPopupCountry.Show();
            }

            if (grid.cpinsert != null) {
                if (grid.cpinsert == 'Success') {
                    jAlert('Saved successfully');
                    cPopupCountry.Hide();
                }
                else {
                    jAlert("Error On Insertion\n'Please Try Again!!'");
                }
            }

            if (grid.cpExists != null) {
                if (grid.cpExists == 'Exists') {
                    jAlert('Duplicate value');
                    cPopupCountry.Hide();
                }

            }

            if (grid.cpUpdate != null) {
                if (grid.cpUpdate == 'Success') {
                    jAlert('Updated successfully');
                    grid.cpUpdate = null;
                    cPopupCountry.Hide();
                }
                else{
                    jAlert("Error on Updation\n'Please Try again!!'")
                    grid.cpUpdate = null;
                }
            }


            if (grid.cpDelete != null) {
                if (grid.cpDelete == 'Success') {
                    jAlert(grid.cpDelete);
                    grid.cpDelete = null;
                    grid.PerformCallback();
                }
                else {
                    jAlert(grid.cpDelete)
                    grid.cpDelete = null;
                    grid.PerformCallback();
                }
            }
        }
    </script>
</asp:Content>

