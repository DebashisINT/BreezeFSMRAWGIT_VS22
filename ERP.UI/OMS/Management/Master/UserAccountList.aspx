<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                09-02-2023        2.0.39           Pallab              25656 : Master module design modification 
2.0                17/02/2023        2.0.39           Sanchita            A setting required for 'User Account' Master module in FSM Portal
                                                                          Refer: 25669  
====================================================== Revision History ==========================================================--%>

<%@ Page Title="Users" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master" Inherits="ERP.OMS.Management.Master.management_master_UserAccountList" CodeBehind="UserAccountList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #divDashboardHeaderList .panel:last-child {
            margin-bottom: 0;
        }
        ul.inline-list {
            padding-left: 0;
            margin-bottom: 0;
        }
        .inline-list > li {
            display: inline-block;
            list-style-type: none;
            margin-right: 20px;
            margin-bottom: 8px;
        }
        .inline-list > li > input {
                -webkit-transform: translateY(3px);
                -moz-transform: translateY(3px);
                transform: translateY(3px);
                margin-right: 4px;
            }

        .panel-title > a {
            font-size: 13px !important;
            display: inline-block;
            padding-left: 10px;
        }

        #list2 option,
        #list1 option {
            padding: 5px 3px;
        }

        .padTbl > tbody > tr > td {
            padding-right: 20px;
            vertical-align: central;
        }

            .padTbl > tbody > tr > td > label {
                margin-bottom: 0px !important;
            }

        #divDashboardHeaderList .panel-title {
            position: relative;
        }

            #divDashboardHeaderList .panel-title > a:focus {
                text-decoration: none;
            }

            #divDashboardHeaderList .panel-title > a:after {
                content: '\f056';
                font-family: FontAwesome;
                font-size: 18px;
                position: absolute;
                right: 10px;
                top: 6px;
            }

            #divDashboardHeaderList .panel-title > a + input[type="checkbox"] {
                -webkit-transform: translateY(2px);
                -moz-transform: translateY(2px);
                transform: translateY(2px);
            }

            #divDashboardHeaderList .panel-title > a.collapsed:after {
                content: '\f055';
            }

        .errorField {
            position: absolute;
            right: -17px;
            top: 3px;
        }

        #multiselect_to option, #multiselect option {
            padding: 5px 3px;
        }

        .min3 {
            min-height: 150px;
        }

        .pad28 {
            padding-top: 26px;
        }
    </style>

    <style>
        .transfer-demo {
            width: 640px;
            height: 351px;
        }

            .transfer-demo .transfer-double-header {
                display: none;
            }

        .transfer-double-selected-list-main .transfer-double-selected-list-ul .transfer-double-selected-list-li .checkbox-group {
            width: 85%;
        }

        .red {
            color: red;
        }

        input:focus, textarea:focus, select:focus {
            outline: none;
        }

        .transfer-double-content-param {
            border-bottom: 1px solid #4236f5;
            background: #4236f5;
            color: #e8e8e8;
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

    .dxpcLite_PlasticBlue .dxpc-headerText
    {
            color: #fff;
    }

    #drdExport
    {
        max-height: 32px;
        padding: 5px 10px !important;
    }

    @media only screen and (max-width: 768px) 
    {
        .overflow-x-auto {
            overflow-x: auto !important;
                width: 300px;
        }

        .backBox
        {
            overflow: hidden !important;
        }

        .breadCumb > span
        {
            padding: 9px 15px;
        }
    }

    /*Rev end 1.0*/
    </style>
    <link href="/assests/pluggins/Transfer/icon_font/css/icon_font.css" rel="stylesheet" />
    
    <link href="/assests/css/custom/PMSStyles.css" rel="stylesheet" />
    <script src="/Scripts/SearchPopup.js"></script>
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <link href="/assests/css/custom/SearchPopup.css" rel="stylesheet" />    
    <script src="/Scripts/SearchMultiPopup.js?v1.0"></script>  
    <link href="/assests/pluggins/Transfer/css/jquery.transfer.css" rel="stylesheet" />
    <script src="/assests/pluggins/Transfer/jquery.transfer.js"></script>   

    <script language="javascript" type="text/javascript">

        function AddUserDetails() {           
            var url = 'UserAccountAdd.aspx?id=Add';
            location.href = url;
        } 
        FieldName = 'Headermain1_cmbSegment';
        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        function callback() {
            grid.PerformCallback('All');
            // grid.PerformCallback();
        }
        function EndCall(obj) {
        }
    </script>  
</asp:Content>
<%--Rev work start .Refer: 25046 27.07.2022 New Listing page create for new User Account Page--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadCumb">
        <span>Users Account</span>
    </div>

    <div class="container mt-4">
        <div class="backBox p-3">
            <table class="TableMain100">              
                <tr>
                    <td>
                        <table width="100%" class="mb-3">
                            <tr>
                                <td style="text-align: left; vertical-align: top">
                                    <table>
                                        <tr>
                                            <td id="ShowFilter">
                                                <%if (rights.CanAdd)
                                                  { %>
                                                <a href="javascript:void(0);" onclick="AddUserDetails()" class="btn btn-success"><span>Add New</span> </a>
                                                <% } %>
                                                <% if (rights.CanExport)
                                                   { %>
                                                <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Export to</asp:ListItem>
                                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                                    <asp:ListItem Value="2">XLS</asp:ListItem>
                                                    <asp:ListItem Value="3">RTF</asp:ListItem>
                                                    <asp:ListItem Value="4">CSV</asp:ListItem>
                                                </asp:DropDownList>
                                                <% } %>                                                
                                            </td>                                           
                                        </tr>
                                    </table>
                                </td>
                                <td></td>

                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="overflow-x-auto">
                            <%--Rev 2.0: grid column width increase--%>
                            <dxe:ASPxGridView ID="userGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            KeyFieldName="USER_ID" Width="100%" OnCustomJSProperties="userGrid_CustomJSProperties" SettingsBehavior-AllowFocusedRow="true"
                            Settings-HorizontalScrollBarMode="Auto">                           
                            <Columns>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="0" FieldName="UID"
                                    Caption="UID" Width="0" SortOrder="Descending">
                                    <EditFormSettings></EditFormSettings>
                                </dxe:GridViewDataTextColumn>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="0" FieldName="USER_ID"
                                    Caption="User ID" Width="180px" >
                                    <EditFormSettings></EditFormSettings>
                                </dxe:GridViewDataTextColumn>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="1" FieldName="USER_NAME"
                                    Caption="User Name" Width="260px">
                                    <PropertiesTextEdit>
                                        <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                            <RequiredField ErrorText="Please Enter user Name" IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Caption="User Name:" Visible="True" />
                                </dxe:GridViewDataTextColumn>
                                <dxe:GridViewDataTextColumn VisibleIndex="2" Caption="User Type" FieldName="STAGE"  Width="200px">
                                </dxe:GridViewDataTextColumn>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="3" FieldName="BRANCHNAME"
                                    Caption="Branch" Width="270px" >
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>
                                <%--Rev 2.0 [ caption changed from Report To to WD ID--%>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="4" FieldName="REPORTTO"
                                    Caption="WD ID" Width="300px">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>                                
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="5" FieldName="CHANNELNAME"
                                    Caption="Channel"  Width="150px">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>                               
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="6" FieldName="CIRCLENAME"
                                    Caption="Circle" Width="220px" >
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="7" FieldName="SECTIONNAME"
                                    Caption="Section" Width="220px">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>   
                                <%--Rev 2.0--%>   
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="8" FieldName="deg_designation"
                                    Caption="Designation" Width="180px">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>   
                                <%--End of Rev 2.0--%>                         
                            </Columns>                          
                            <SettingsSearchPanel Visible="True" />
                            <Settings ShowStatusBar="Hidden" ShowFilterRow="true" ShowGroupPanel="True" ShowFilterRowMenu="true" />                           
                            <SettingsBehavior ConfirmDelete="True" />
                            <ClientSideEvents EndCallback="function(s, e) {
	                            EndCall(s.cpHeight);
                            }" />
                        </dxe:ASPxGridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>        
        <dxe:ASPxGridViewExporter ID="exporter" runat="server">
        </dxe:ASPxGridViewExporter>
    </div>
</asp:Content>