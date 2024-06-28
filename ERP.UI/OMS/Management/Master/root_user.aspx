<%--******************************************************************************************************
 * Rev 1.0      Sanchita            07/02/2023      V2.0.36     FSM Employee & User Master - To implement Show button. refer: 25641
   Rev 2.0      Pallab              09/02/2023      V2.0.36     Master module design modification. refer: 25656
   Rev 3.0      Sanchita/Pallab     15/02/2023      V2.0.39     A setting required for Employee and User Master module in FSM Portal.
   Rev 4.0      Sanchita            05-05-2023      V2.0.40     In Portal -> Master -> Organization -> User  -> Logedin user in app is shows green which is ok but after 
                                                                pressing refresh button from action it is not turning red . Refer: 25947
   Rev 5.0      Sanchita            04-11-2024      V2.0.46     Company Add/Edit column is showing in User master which is irrelevant to FSM. Mantis: 27363
   Rev 6.0      Sanchita            03/06/2024      V2.0.47     27500: Attendance/ Leave Clear tab need to add in security Role of "Users"    
*******************************************************************************************************--%>

<%@ Page Title="Users" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master" Inherits="ERP.OMS.Management.Master.management_master_root_user" CodeBehind="root_user.aspx.cs" %>
<%--Rev 1.0--%>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<%--End of Rev 1.0--%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Rev 3.0--%>
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>

    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <link href="../../../assests/css/custom/SearchPopup.css" rel="stylesheet" />
    <script src="../../../Scripts/SearchPopup.js"></script>
    <script src="../../../Scripts/SearchMultiPopup.js"></script>
    <%--End of Rev 3.0--%>

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

         /*Rev 3.0*/
       .for-cust-padding
       {
           padding: 0 0 0 10px;
       }

       .for-cust-padding label
       {
           margin-right: 5px;
       }

       .dis-flex
       {
            display: flex;
            align-items: center;
       }

       .btn-show
       {
           margin-left: 10px;
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

        .close:hover
        {
            color: #fff;
            opacity: 1;
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

            .btn-show
    {
        background: #2379d1;
        border-color: #2379d1;
            color: #fff;
    }

    .btn-show:hover , .btn-show:focus
    {
        color: #fff;
    }

    .pmsModal.w80 .modal-dialog {
    width: 50% !important;
}

       /*Rev end 3.0*/
       
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

        /*Rev 2.0*/

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
            top: 41px;
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
        padding: 6px 10px !important;
    }
    .btn
    {
        height: 34px;
    }

    #UserTable
    {
        margin-top: 10px;
    }
    #PartyTable
    {
        margin-top: 10px;
    }

    /*Rev 3.0*/
        .fullMulti .multiselect-native-select, .fullMulti .multiselect-native-select .btn-group {
            width: 100%;
        }

            .fullMulti .multiselect-native-select .multiselect {
                width: 100%;
                text-align: left;
                border-radius: 4px !important;
            }

                .fullMulti .multiselect-native-select .multiselect .caret {
                    float: right;
                    margin: 9px 5px;
                }

        .hideScndTd > table > tbody > tr > td:last-child {
            display: none;
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

        .dynamicPopupTbl {
        font-family: 'Poppins', sans-serif !important;
        }

            .dynamicPopupTbl > tbody > tr > td,
            #EmployeeTable table tr th {
                font-family: 'Poppins', sans-serif !important;
                font-size: 12px;
            }
        /*End of Rev 3.0*/

        #userGrid a img
        {
            transition: all .3s;
        }

        #userGrid a:hover img
        {
            transform: scale(1.1, 1.1);
        }
    /*Rev end 2.0*/

    @media only screen and (max-width: 768px) {
        .breadCumb {
            padding: 0 28%;
        }

        .breadCumb > span {
            padding: 9px 40px;
        }

        .FilterSide
        {
            width: 100% !important;
        }

        #marketsGrid_DXPEForm_PW-1
        {
            width: 330px !important;
        }

        #ShowFilter, #divUser, #show-btn
        {
                display: block;
                margin-bottom: 10px;
        }

        .overflow-x-auto
        {
            overflow-x: auto;
            width: 290px !important;
        }

    }

    .pmsModal .modal-content
    {
        border-radius: 15px !important;
    }

    .pmsModal .modal-footer
    {
            border-radius: 0 0 15px 15px !important;
    }

    .modal .modal-header
    {
            border-radius: 15px 15px 0 0 !important;
    }
    </style>
    <link href="/assests/pluggins/Transfer/icon_font/css/icon_font.css" rel="stylesheet" />
    
    <link href="/assests/css/custom/PMSStyles.css" rel="stylesheet" />
    <script src="/Scripts/SearchPopup.js"></script>
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <link href="/assests/css/custom/SearchPopup.css" rel="stylesheet" />
    <%--Mantis Issue 24362--%>
    <%--<script src="/Scripts/SearchPopup.js"></script>--%>
   <%-- Rev Work 08.04.2022
         Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked--%>
    <%--<script src="/Scripts/SearchMultiPopup.js></script>--%>
    <script src="/Scripts/SearchMultiPopup.js?v1.0"></script>
   <%-- Rev Work close 08.04.2022
         Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked--%>
    <link href="/assests/pluggins/Transfer/css/jquery.transfer.css" rel="stylesheet" />

    <script src="/assests/pluggins/Transfer/jquery.transfer.js"></script>
    <%--End of Mantis Issue 24362--%>

    <script language="javascript" type="text/javascript">

        function AddUserDetails() {
            // document.location.href="RootUserDetails.aspx?id=Add"
            var url = 'RootUserDetails.aspx?id=Add';
            //OnMoreInfoClick(url, "Add User Details", '940px', '450px', "Y");
            location.href = url;
        }
        function EditUserDetails(keyValue) {

            //document.location.href="RootUserDetails.aspx?id="+keyValue;
            var url = 'RootUserDetails.aspx?id=' + keyValue;
            //OnMoreInfoClick(url, "Edit User Details", '940px', '450px', "Y");
            location.href = url;
        }
        //Rev work start 26.04.2022 Mantise ID:0024856: Copy feature add in User master
        function CopyUserDetails(keyValue) {
            var url = 'RootUserDetails.aspx?id=' + keyValue+'&Mode=Copy';
            location.href = url;
        }
        //Rev work close 26.04.2022 Mantise ID:0024856: Copy feature add in User master
        function ChangePassword(keyValue) {
            var url = '../ToolsUtilities/frmchangeuserspassword.aspx?uid=' + keyValue;
            location.href = url;
        }
        //Mantis Issue 25116
        function AttendanceLeaveClear(keyValue) {            
            jConfirm('Clear Attendance/Leave?', 'Alert', function (r) {
                if (r) {
                    $.ajax({
                        type: "POST",
                        url: "root_user.aspx/AttendanceLeaveClear",
                        data: JSON.stringify({ User_Id: keyValue }),
                        async: false,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            //console.log(response);
                            if (response.d) {
                                jAlert(response.d, "Alert", function () {
                                });
                                //if (response.d == "true") {
                                //    jAlert("Attendance/Leave Clear Successfully.", "Alert", function () {
                                //    });
                                //}
                                //else {
                                //    jAlert(response.d);
                                //    return
                                //}
                            }
                        },
                        error: function (response) {
                            console.log(response);
                        }
                    });
                }
            });
        }
        
        //End of Mantis Issue 25116
        function RefreshLoggoff(keyValue) {

            //document.location.href="RootUserDetails.aspx?id="+keyValue;




            $.ajax({
                type: "POST",
                url: "root_user.aspx/Resetloggedin",
                data: JSON.stringify({ Userid: keyValue }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {

                    if (msg.d == true) {
                        // Rev 4.0
                        //grid.Refresh();
                        $("#hfIsFilter").val("Y");
                        grid.PerformCallback("Show");
                        // End of Rev 4.0
                    }
                    else {

                    }
                }
            });
        }


        function AddCompany(keyValue) {

            //document.location.href="RootUserDetails.aspx?id="+keyValue;
            var url = 'Root_AddUserCompany.aspx?id=' + keyValue;
            //OnMoreInfoClick(url, "Add Company Details for User", '940px', '450px', "Y");
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

        function AddAssignParty() {
            $("#SelectPartyMapUsersModal").modal('show');
        }

        function ShowParty() {
            cPartyGrid.PerformCallback('BindPartyList');
            cPartySelectPopup.Show();
        }

        function PartyGridEndCallBack() {
            if (cPartyGrid.cpReceviedString) {
                if (cPartyGrid.cpReceviedString == 'SetAllRecordToDataTable') {
                    cPartySelectPopup.Hide();
                }
            }

            if (cPartyGrid.cpPartyselected) {

                if (cPartyGrid.cpPartyselected == '1') {
                    //jAlert("Individual branch selection not allowed when all branch option is checked.");
                    cPartyGrid.cpBrselected = null;
                    // cPartySelectPopup.Hide();
                }
                else {
                    cPartyGrid.cpPartyselected = null;
                    jAlert("Please select Party.");
                    cPartySelectPopup.Show();
                }
            }

            if (cPartyGrid.cpBrChecked) {
                if (cPartyGrid.cpBrChecked == '1') {
                 <%--   $('#<%= lblBranch.ClientID %>').attr('style', 'display:inline');
                    $('#<%=chkAllBranch.ClientID %>').prop('checked', true)--%>
                    cPartyGrid.cpBrChecked = null;
               <%--     $('#<%= hdnBranchAllSelected.ClientID %>').val('0');--%>
                }
                else {
                 <%--   $('#<%= lblBranch.ClientID %>').attr('style', 'display:none');
                    $('#<%=chkAllBranch.ClientID %>').prop('checked', false)--%>
                    cPartyGrid.cpBrChecked = null;
                   <%-- $('#<%= hdnBranchAllSelected.ClientID %>').val('1');--%>
                }
            }
        }

        function MultiBranchClick() {
            cPartyGrid.PerformCallback('SetAllSelectedRecord');
            cPartySelectPopup.Show();
        }

        function SaveSelectedParty() {
            cPartyGrid.PerformCallback('SetAllRecordToDataTable');
        }

        function selectAll() {

            cPartyGrid.PerformCallback('SelectAllPartyFromList');
            cPartySelectPopup.Show();
        }
        function unselectAll() {
            cPartyGrid.PerformCallback('ClearSelectedParty');
            cPartySelectPopup.Show();
        }

        // Rev 3.0
        function UserButnClick(s, e) {
            $('#UserModel').modal('show');
            $("#txtUserSearch").focus();
        }

        function UserbtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#UserModel').modal('show');
                $("#txtUserSearch").focus();
            }
        }

        function Userkeydown(e) {

            var OtherDetails = {}
            OtherDetails.SearchKey = $("#txtUserSearch").val();
            if ($.trim($("#txtUserSearch").val()) == "" || $.trim($("#txtUserSearch").val()) == null) {
                return false;
            }
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("User Name");
                HeaderCaption.push("Login ID");
                HeaderCaption.push("Employee ID");
                if ($("#txtUserSearch").val() != null && $("#txtUserSearch").val() != "") {
                    callonServerM("root_user.aspx/GetOnDemandUser", OtherDetails, "UserTable", HeaderCaption, "dPropertyIndex", "SetSelectedValues", "UserSource");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[UserIndex=0]"))
                    $("input[UserIndex=0]").focus();
            }
        }
        // End of Rev 3.0


    </script>

    <%--select user bind--%>
    <script>

        // Rev 3.0
        var UserArr = new Array();
        $(document).ready(function () {
            var UserObj = new Object();
            UserObj.Name = "UserSource";
            UserObj.ArraySource = UserArr;
            arrMultiPopup.push(UserObj);
        })
        // End of Rev 3.0

        function MapUser() {
            cUsersGrid.PerformCallback('BindUsersList');
            cUserSelectPopup.Show();
        }

        function UsersGridEndCallBack() {
            if (cUsersGrid.cpReceviedString) {
                if (cUsersGrid.cpReceviedString == 'SetAllRecordToDataTable') {
                    cUserSelectPopup.Hide();
                }
            }

            if (cUsersGrid.cpPartyselected) {
                if (cUsersGrid.cpPartyselected == '1') {
                    jAlert("Saved Successfully.");
                    cUsersGrid.cpPartyselected = null;
                    //  cUserSelectPopup.Hide();
                }
                else if (cUsersGrid.cpPartyselected == '2') {
                    cUsersGrid.cpPartyselected = null;
                    jAlert("Please select Party.");
                    cUserSelectPopup.Hide();
                    cPartySelectPopup.Show();
                }
                else if (cUsersGrid.cpPartyselected == '5') {
                    jAlert("Mapping user already exists.");
                    cUserSelectPopup.Show();
                }
                else {
                    cUsersGrid.cpPartyselected = null;
                    jAlert("Please select User.");
                    cUserSelectPopup.Show();
                }
            }

            if (cUsersGrid.cpBrChecked) {
                if (cUsersGrid.cpBrChecked == '1') {
                 <%--   $('#<%= lblBranch.ClientID %>').attr('style', 'display:inline');
                    $('#<%=chkAllBranch.ClientID %>').prop('checked', true)--%>
                    cUsersGrid.cpBrChecked = null;
               <%--     $('#<%= hdnBranchAllSelected.ClientID %>').val('0');--%>
                }
                else {
                 <%--   $('#<%= lblBranch.ClientID %>').attr('style', 'display:none');
                    $('#<%=chkAllBranch.ClientID %>').prop('checked', false)--%>
                    cUsersGrid.cpBrChecked = null;
                   <%-- $('#<%= hdnBranchAllSelected.ClientID %>').val('1');--%>
                }
            }
        }

        function MultiBranchClick() {
            cUsersGrid.PerformCallback('SetAllSelectedRecord');
            cUserSelectPopup.Show();
        }

        function SaveSelectedUser() {
            cUsersGrid.PerformCallback('SetAllRecordToDataTable');
        }

        function selectAll() {

            cUsersGrid.PerformCallback('SelectAllUsersFromList');
            cUserSelectPopup.Show();
        }
        function unselectAll() {
            cUsersGrid.PerformCallback('ClearSelectedUsers');
            cUserSelectPopup.Show();
        }

        function CloseUserpopUp() {
            cUserSelectPopup.Hide();
        }
    </script>
    <%--select user bind--%>

    <%--select UnAssign Party bind--%>
    <script>

        function UnAssignParty() {
            cUnAssignPartyGrid.PerformCallback('BindAssignPartyList');
            cUnAssignPartyPopup.Show();
        }

        function UnAssignPartyEndCallBack() {
            if (cUnAssignPartyGrid.cpReceviedString) {
                if (cUnAssignPartyGrid.cpReceviedString == 'SetAllRecordToDataTable') {
                    cUnAssignPartyPopup.Hide();
                }
            }

            if (cUnAssignPartyGrid.cpPartyselected) {
                if (cUnAssignPartyGrid.cpPartyselected == '1') {
                    jAlert("Update Successfully.");
                    cUnAssignPartyGrid.cpPartyselected = null;
                    //  cUnAssignPartyPopup.Hide();
                }
                else if (cUnAssignPartyGrid.cpPartyselected == '2') {
                    cUnAssignPartyGrid.cpPartyselected = null;
                    jAlert("Please select User.");

                    cUnAssignPartyPopup.Show();
                }
                else {
                    cUnAssignPartyGrid.cpPartyselected = null;
                    jAlert("Please select User.");
                    cUnAssignPartyPopup.Show();
                }
            }

            if (cUnAssignPartyGrid.cpBrChecked) {
                if (cUnAssignPartyGrid.cpBrChecked == '1') {
                 <%--   $('#<%= lblBranch.ClientID %>').attr('style', 'display:inline');
                    $('#<%=chkAllBranch.ClientID %>').prop('checked', true)--%>
                    cUnAssignPartyGrid.cpBrChecked = null;
               <%--     $('#<%= hdnBranchAllSelected.ClientID %>').val('0');--%>
                }
                else {
                 <%--   $('#<%= lblBranch.ClientID %>').attr('style', 'display:none');
                    $('#<%=chkAllBranch.ClientID %>').prop('checked', false)--%>
                    cUnAssignPartyGrid.cpBrChecked = null;
                   <%-- $('#<%= hdnBranchAllSelected.ClientID %>').val('1');--%>
                }
            }
        }

        function MultiBranchClick() {
            cUnAssignPartyGrid.PerformCallback('SetAllSelectedRecord');
            cUnAssignPartyPopup.Show();
        }

        function UnAssignUser() {
            cUnAssignPartyGrid.PerformCallback('SetAllRecordToDataTable');
        }

        function selectAll() {

            cUnAssignPartyGrid.PerformCallback('SelectAllUsersFromList');
            cUnAssignPartyPopup.Show();
        }
        function unselectAll() {
            cUnAssignPartyGrid.PerformCallback('ClearSelectedUsers');
            cUnAssignPartyPopup.Show();
        }

        function CloseUserpopUp() {
            cUnAssignPartyPopup.Hide();
        }
    </script>
    <%--select UnAssign Party bind--%>

    <%-- Show details Party Assign --%>
    <script>
        function ShowAssignParty() {
            cgrdAssignPartyList.PerformCallback();
            //cAssignPartyListPopUp.Show();
            $("#HeaderPartyMapUsersModal").modal('show');
        }

        function AddParty() {
            // Rev 24363
            $("#hdnHeaderId").val('');
            // End of Rev 24363
            $("#txtName").val('');
            ctxtPartys.SetText('');
            $("#txtParty_hidden").val('');
            $("#ddlPartyTypes").val(1);
            $("#transfer1").html('');
            $("#PartyMapUsersModal").modal('show');
        }

        // Rev 1.0
        function ShowData() {
            $("#hfIsFilter").val("Y");
            grid.PerformCallback("Show");
        }
        // End of Rev 1.0
    </script>

    <script>
        var PartyArr = new Array();
        $(document).ready(function () {
            var PartyObj = new Object();
            PartyObj.Name = "PartySource";
            PartyObj.ArraySource = PartyArr;
            arrMultiPopup.push(PartyObj);
        })
        function PartyButnClick(s, e) {
            $('#PartyModel').modal('show');
            $("#txtPartySearch").focus();
        }

        function PartybtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#PartyModel').modal('show');
                $("#txtPartySearch").focus();
            }
        }

        function partyskeydown(e) {

            var OtherDetails = {}
            OtherDetails.SearchKey = $("#txtPartySearch").val();
            OtherDetails.PartyType = $("#ddlPartyTypes").val();
            if ($.trim($("#txtPartySearch").val()) == "" || $.trim($("#txtPartySearch").val()) == null) {
                return false;
            }
            /*Rev Work 08.04.2022
            Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/
            var Mode = $("#ModeType").val();
            if (Mode == "Edit")
            {
                arrMultiPopup = [];
                var ProdObjEdit = new Object();
                var ProdArrEdit = new Object();
                ProdArrEdit = JSON.parse($("#jsonProducts").text());
                ProdObjEdit.Name = "PartySource";
                ProdObjEdit.ArraySource = ProdArrEdit;
                arrMultiPopup.push(ProdObjEdit);
            }
            /*Rev Work close 08.04.2022
            Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/

            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                //HeaderCaption.push("Party Code");
                //HeaderCaption.push("Location");
                if ($("#txtPartySearch").val() != null && $("#txtPartySearch").val() != "") {
                    // Mantis Issue 24362
                    //callonServer("root_user.aspx/GetParty", OtherDetails, "PartyTable", HeaderCaption, "PartyIndex", "SetParty");
                    callonServerM("root_user.aspx/GetParty", OtherDetails, "PartyTable", HeaderCaption, "dPropertyIndex", "SetSelectedValues", "PartySource");
                    // End of Mantis Issue 24362
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[PartyIndex=0]"))
                    $("input[PartyIndex=0]").focus();
            }
        }

        // Mantis Issue 24363
        function SetSelectedValues(Id, Name, ArrName) {
            if (ArrName == 'PartySource') {
                var key = Id;
                if (key != null && key != '') {
                    $("#txtParty_hidden").val(Id);
                    ctxtPartys.SetText(Name);
                    $('#PartyModel').modal('hide');
                    
                }
                else {
                    $("#txtParty_hidden").val('');
                    ctxtPartys.SetText('');
                    $('#PartyModel').modal('hide');
                   
                }
            }
            // Rev 3.0
            if (ArrName == 'UserSource') {
                var key = Id;
                if (key != null && key != '') {
                    $("#txtUser_hidden").val(Id);
                    ctxtUser.SetText(Name);
                    $('#UserModel').modal('hide');
                }
                else {
                    $("#txtUser_hidden").val('');
                    ctxtUser.SetText('');
                    $('#UserModel').modal('hide');

                }
            }
            // End of Rev 3.0
        }
        // End of Mantis Issue 24363

        function SetParty(Id, Name) {
            $("#txtParty_hidden").val(Id);
            ctxtPartys.SetText(Name);
            $('#PartyModel').modal('hide');

            // Mantis Issue 24362
            //ddsstatechange();
            // End of Mantis Issue 24362
        }

        var selectedUser;
        function ddsstatechange() {
            selectedUser = '';
            var objmultiselectto = [];

            $.ajax({
                //type: "POST",
                //url: @Url.Action('root_user.aspx/GetParty'),
                //data: { designationid: designationid, notificationId: $("#hdnnotid").val(), stateid: $("#ddstate").val() },
                // Mantis Issue 24362  [ parameter branch added in ajax]
                type: "POST",
                url: "root_user.aspx/GetUserList",
                data: JSON.stringify({ shop_code: $("#txtParty_hidden").val(), branch: $("#ddlBranch").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response)
                    var temArr2 = [];
                    var data = response.d;
                    for (var a = 0; a < data.length; a++) {
                        if (data[a].selected) {
                            temArr2.push(data[a].UserID);
                        }
                    }
                    selectedUser = temArr2.join(",");
                    $("#transfer1").html('');
                    var settings1 = {
                        "dataArray": data,
                        "itemName": "username",
                        "valueName": "UserID",
                        "tabNameText": "Users",
                        "rightTabNameText": "Selected users",
                        "searchPlaceholderText": "search in users",
                        "callable": function (items) {
                            //console.dir(items)
                            var temArr = [];
                            for (var i = 0; i < items.length; i++) {
                                temArr.push(items[i].UserID);
                            }
                            selectedUser = temArr.join(",");
                        }
                    };
                    $("#transfer1").transfer(settings1);
                }
            });
        }

        function SaveAssignParty() {
            if ($("#txtParty_hidden").val()=="") {
                jAlert("Please select party.", "Alert", function () {
                    // Mantis Issue 24363
                    //ctxtPartys.focus();
                    ctxtPartys.SetFocus();
                    // End of Mantis Issue 24363
                    return
                });
                return
            }

            if (selectedUser == '') {
                jAlert("Please select user.", "Alert", function () {
                    return
                });
                return
            }
            // Mantis Issue 24362 [ parametre "branch" added in ajax ]
            $.ajax({
                type: "POST",
                url: "root_user.aspx/SaveAssignParty",
                data: JSON.stringify({
                    shop_code: $("#txtParty_hidden").val(), Shop_type: $("#ddlPartyTypes").val(), Users: selectedUser, Headr_name: $("#txtName").val(),
                    header_id: $("#hdnHeaderId").val(), branch: $("#ddlBranch").val()
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d="OK") {
                        jAlert("Save successfully");
                        $('#PartyMapUsersModal').modal('hide');
                        cgrdAssignPartyList.PerformCallback();
                    }
                    else {
                        jAlert("Mapping already exists.");
                    }
                }
            });
        }

        function EditAssignDetails(val) {
            $("#hdnHeaderId").val(val);
            selectedUser = '';
            var objmultiselectto = [];
            // Mantis Issue 24363
            selectedShopCode = '';
            selectedShopName = '';
            // End of Mantis Issue 24363
            
            $.ajax({
                type: "POST",
                url: "root_user.aspx/EditGetUserList",
                data: JSON.stringify({ Header_id: val }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                   
                    console.log(response)
                    $("#txtName").val(response.d.Name);
                    // Mantis Issue 24363
                    //ctxtPartys.SetText(response.d.Shop_Name);
                    //$("#txtParty_hidden").val(response.d.Shop_code);
                    var temArrShopCode = [];
                    var temArrShopName = [];
                    var dataShop = response.d.PartyList;
                    for (var a = 0; a < dataShop.length; a++) {
                        if (dataShop[a].selected) {
                            temArrShopCode.push(dataShop[a].Shop_Code);
                            temArrShopName.push(dataShop[a].Shop_Name);
                        }
                    }
                    selectedShopCode = temArrShopCode.join(",");
                    selectedShopName = temArrShopName.join(",");

                    ctxtPartys.SetText(selectedShopName);
                   
                    $("#txtParty_hidden").val(selectedShopCode)

                    /*Rev Work 08.04.2022 
                    Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/
                    const extndedArr = response.d.PartySelectID.map(function (item){
                        return {
                            Id : item.id,
                            Name: item.Shop_Name
                        }
                    })
                    //console.log("newArray", extndedArr)                                    
                    $("#jsonProducts").text(JSON.stringify(extndedArr));
                    $("#ModeType").val("Edit");
                    /*Rev Work close 08.04.2022
                    Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked*/

                    var OtherDetails = {}
                    OtherDetails.SearchKey = "";
                    OtherDetails.PartyType = response.d.Shop_type;

                    var HeaderCaption = [];
                    HeaderCaption.push("Name");

                    // End of Mantis Issue 24363

                    $("#ddlPartyTypes").val(response.d.Shop_type);
                    //Rev work start 01.08.2022 mantise no:0025119 branch details not fetch in assign party details edit mode in user master
                    $("#ddlBranch").val(response.d.BranchCode);
                    //Rev work close 01.08.2022 mantise no:0025119 branch details not fetch in assign party details edit mode in user master
                    var temArr2 = [];
                    var data = response.d.UserList;
                    for (var a = 0; a < data.length; a++) {
                        if (data[a].selected) {
                            temArr2.push(data[a].UserID);
                        }
                    }
                    selectedUser = temArr2.join(",");
                    $("#transfer1").html('');
                    var settings1 = {
                        "dataArray": data,
                        "itemName": "username",
                        "valueName": "UserID",
                        "tabNameText": "Users",
                        "rightTabNameText": "Selected users",
                        "searchPlaceholderText": "search in users",
                        "callable": function (items) {
                            //console.dir(items)
                            var temArr = [];
                            for (var i = 0; i < items.length; i++) {
                                temArr.push(items[i].UserID);
                            }
                            selectedUser = temArr.join(",");
                        }
                    };
                    $("#transfer1").transfer(settings1);
                    $('#PartyMapUsersModal').modal('show');
                   
                }
            });
        }

        function DeleteAssignDetails(val) {
            $("#hdnHeaderId").val(val);
            jConfirm("Do you want to delete?", "Confirm", function (r) {
                if (r==true) {
                    $.ajax({
                        type: "POST",
                        url: "root_user.aspx/DeleteAssignParty",
                        data: JSON.stringify({
                            header_id: $("#hdnHeaderId").val()
                        }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d = "OK") {
                                jAlert("Delete successfully");
                                cgrdAssignPartyList.PerformCallback();
                            }
                            else {
                                jAlert("Failed.");
                            }
                        }
                    });
                }
            })
        }

        /*Rev 3.0*/
        $(document).ready(function () {
            $('#UserModel').on('shown.bs.modal', function () {
                $('#txtUserSearch').focus();
            })
        })
        /*Rev end 3.0*/
    </script>
    <%-- Show details Party Assign --%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="breadCumb">
        <span>Users</span>
    </div>

    <div class="container mt-4">
        <div class="backBox p-3">
            <table class="TableMain100">
                <%--<tr>
                <td class="EHEADER" style="text-align: center;">
                    <strong><span style="color: #000099">All User List</span></strong>
                </td>
            </tr>--%>
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
                                                <%--<a href="javascript:ShowHideFilter('s');" class="btn btn-primary"><span>Show Filter</span></a>--%>
                                                <% if (IsFaceDetectionOn)
                                                   { %>
                                                <a href="javascript:void(0);" onclick="AddAssignParty()" class="btn btn-warning hide"><span>Assign Party</span> </a>

                                                <a href="javascript:void(0);" onclick="UnAssignParty()" class="btn btn-warning hide"><span>Un-Assign Party</span> </a>

                                                <a href="javascript:void(0);" onclick="ShowAssignParty()" class="btn btn-warning"><span>Assign Party</span> </a>
                                                <%} %>

                                                <%--Rev 3.0--%>
                                                <%--Rev 1.0--%>
                                               <%-- <% if (rights.CanView)
                                                    { %>
                                                <a href="javascript:void(0);" onclick="ShowData()" class="btn btn-warning"><span>Show Data</span> </a>
                                                <% } %> --%>
                                                <%--End of Rev 1.0--%>
                                                <%--End of Rev 3.0--%>
                                            </td>
                                            <%--Rev 3.0--%>
                                            <td class="for-cust-padding" id="divUser" runat="server" >
                                                <div class="dis-flex" >
                                                    <label>User(s)</label>
                                                    <div style="position: relative">
                                                        <dxe:ASPxButtonEdit ID="txtUser" runat="server" ReadOnly="true" ClientInstanceName="ctxtUser" >
                                                            <Buttons>
                                                                <dxe:EditButton>
                                                                </dxe:EditButton>
                                                            </Buttons>
                                                            <ClientSideEvents ButtonClick="function(s,e){UserButnClick();}" KeyDown="UserbtnKeyDown" />
                                                        </dxe:ASPxButtonEdit>
                                                        <asp:HiddenField ID="txtUser_hidden" runat="server" />

                                                    </div>
                                                </div>
                                            </td>
                                            <td id="show-btn">
                                                 <% if (rights.CanView)
                                                   { %>
                                                    <a href="javascript:void(0);" onclick="ShowData()" class="btn btn-show"><span>Show Data</span> </a>
                                                <% } %>
                                            </td>
                                            <%--End of Rev 3.0--%>
                                            <%--<td id="Td1">
                                            <a href="javascript:ShowHideFilter('All');" class="btn btn-primary"><span>All Records</span></a>
                                        </td>--%>
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
                            <%--Rev 1.0 [ DataSourceID="EntityServerlogModeDataSource"  added ]--%>
                            <dxe:ASPxGridView ID="userGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" DataSourceID="EntityServerlogModeDataSource"
                                KeyFieldName="user_id" Width="100%" OnCustomCallback="userGrid_CustomCallback" OnCustomJSProperties="userGrid_CustomJSProperties" SettingsBehavior-AllowFocusedRow="true"
                                SettingsCookies-Enabled="true" SettingsCookies-StorePaging="true" SettingsCookies-StoreFiltering="true" SettingsCookies-StoreGroupingAndSorting="true" Settings-HorizontalScrollBarMode="Auto">
                                <%--DataSourceID="RootUserDataSource"--%>
                                <Columns>
                                    <%--Rev 1.0 [SortOrder="Descending"  added]   --%>
                                    <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="0" FieldName="user_id" SortOrder="Descending"
                                        Visible="False">
                                        <EditFormSettings Visible="False"></EditFormSettings>
                                    </dxe:GridViewDataTextColumn>
                                    <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="1" FieldName="user_name"
                                        Caption="Name" Width="200px">
                                        <PropertiesTextEdit>
                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                <RequiredField ErrorText="Please Enter user Name" IsRequired="True" />
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <EditFormSettings Caption="User Name:" Visible="True" />
                                    </dxe:GridViewDataTextColumn>
                                    <dxe:GridViewDataTextColumn VisibleIndex="2" Caption="Designation" FieldName="designation"  Width="200px">
                                    </dxe:GridViewDataTextColumn>
                                    <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="3" FieldName="user_loginId"
                                        Caption="Login ID" Width="120px">
                                        <PropertiesTextEdit>
                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                <RequiredField ErrorText="Please Enter Login Id" IsRequired="True" />
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <EditFormSettings Caption="Login Id:" Visible="True" />
                                    </dxe:GridViewDataTextColumn>
                                    <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="4" FieldName="AssignedUser"
                                        Caption="Associated User" Width="150px">
                                        <PropertiesTextEdit>
                                        </PropertiesTextEdit>
                                        <EditFormSettings Visible="false" />
                                    </dxe:GridViewDataTextColumn>
                                    <%--Mantis Issue 24740 --%>
                                    <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="4" FieldName="AssignedUserID"
                                        Caption="Associated ID"  Width="150px">
                                        <PropertiesTextEdit>
                                        </PropertiesTextEdit>
                                        <EditFormSettings Visible="false" />
                                    </dxe:GridViewDataTextColumn>
                                    <%--End of Mantis Issue 24740 --%>
                                    <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="5" FieldName="BranchName"
                                        Caption="Branch" Width="220px" >
                                        <PropertiesTextEdit>
                                        </PropertiesTextEdit>
                                        <EditFormSettings Visible="false" />
                                    </dxe:GridViewDataTextColumn>
                                    <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="6" FieldName="grp_name"
                                        Caption="Group" Width="220px">
                                        <PropertiesTextEdit>
                                        </PropertiesTextEdit>
                                        <EditFormSettings Visible="false" />
                                    </dxe:GridViewDataTextColumn>
                                    <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="7" FieldName="Status"
                                        Caption="User Status"  Width="100px">
                                        <PropertiesTextEdit>
                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                                <RequiredField ErrorText="Please Enter Login Id" IsRequired="True" />
                                            </ValidationSettings>
                                        </PropertiesTextEdit>
                                        <EditFormSettings Caption="Login Id:" Visible="True" />
                                    </dxe:GridViewDataTextColumn>


                                    <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="8" FieldName="StatusMac"
                                        Caption="Mac Lock">
                                    </dxe:GridViewDataTextColumn>



                                    <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="9" FieldName="Status" Settings-AllowAutoFilter="False"
                                        Caption="Online Status" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">

                                        <DataItemTemplate>


                                            <%# Eval("Onlinestatus").ToString()=="1" ? "<img title='logged in' src='../../../assests/images/activeState.png' />" : "<img title='logged off' src='../../../assests/images/inactiveState.png' />" %>
                                        </DataItemTemplate>
                                        <HeaderTemplate>Online Status</HeaderTemplate>
                                        <EditFormSettings Visible="False" />

                                    </dxe:GridViewDataTextColumn>

                                    <%--Rev 5.0--%>
                                    <%--<dxe:GridViewDataTextColumn Caption="Company Add/Edit" VisibleIndex="10" Width="5%" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>

                                            <a href="javascript:void(0);" onclick="AddCompany('<%# Container.KeyValue %>')" title="Add Company">
                                                <img src="../../../assests/images/Add.png" />
                                            </a>

                                        </DataItemTemplate>
                                        <EditFormSettings Visible="False" />
                                        <HeaderTemplate>
                                            <span>Company Add/Edit</span>
                                        </HeaderTemplate>
                                    </dxe:GridViewDataTextColumn>--%>
                                    <%--End of Rev 5.0--%>


                                    <%--Rev work start 26.04.2022 Mantise ID:0024856: Copy feature add in User master--%>
                                    <%--<dxe:GridViewDataTextColumn VisibleIndex="11" Width="6%" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">--%>
                                    <dxe:GridViewDataTextColumn VisibleIndex="11" Width="200px" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                    <%--Rev work close 26.04.2022 Mantise ID:0024856: Copy feature add in User master--%>
                                        <DataItemTemplate>


                                            <a href="javascript:void(0);" onclick="RefreshLoggoff('<%# Container.KeyValue %>')" title="Reset Online Status" class="pad">
                                                <%--Rev 2.0--%>
                                                <%--<span class="fa fa-refresh"></span>--%>
                                                <img src="../../../assests/images/reset-status.png" /></a>

                                            <% if (rights.CanEdit)
                                               { %>
                                            <a href="javascript:void(0);" onclick="EditUserDetails('<%# Container.KeyValue %>')" title="Edit" class="pad">
                                                <img src="../../../assests/images/Edit.png" /></a>
                                            <% } %>
                                            <%--Rev work start 26.04.2022 Mantise ID:0024856: Copy feature add in User master--%>
                                            <% if (rights.CanAdd)
                                               { %>
                                            <a href="javascript:void(0);" onclick="ChangePassword('<%# Container.KeyValue %>')" title="Change Password">
                                                <%--Rev 2.0--%>
                                                <%--<img src="../../../assests/images/change-dark.png" />--%>
                                                <img src="../../../assests/images/change-password.png" /></a>
                                            <%--Rev end 2.0--%>
                                            <a href="javascript:void(0);" onclick="CopyUserDetails('<%# Container.KeyValue %>')" title="Copy" class="pad">
                                                <%--Rev 2.0--%>
                                                <%--<img src="../../../assests/images/copy.png" /></a>--%>
                                                <img src="../../../assests/images/copy2.png" /></a>
                                            <%--Rev end 2.0--%>
                                            <% } %>
                                         
                                        
                                            <%--Mantis Issue 25116--%>
                                            <%--Rev 6.0--%>
                                            <% if (rights.CanAttendanceLeaveClear){ %>
                                            <%--End of Rev 6.0--%>
                                                <a href="javascript:void(0);" onclick="AttendanceLeaveClear('<%# Container.KeyValue %>')" title="Attendance/Leave Clear">
                                                    <%--Rev 2.0--%>
                                                    <%--<img src="../../../assests/images/clear.png" / style="width:16px">--%>
                                                    <img src="../../../assests/images/clear2.png" / style=""></a>
                                            <% } %>
                                            <%--Rev end 2.0--%>
                                            <%--End of Mantis Issue 25116--%>

                                        </DataItemTemplate>
                                        <HeaderTemplate>Actions</HeaderTemplate>
                                        <EditFormSettings Visible="False" />

                                    </dxe:GridViewDataTextColumn>
                                </Columns>
                                <%--<Styles>
                                <LoadingPanel ImageSpacing="10px">
                                </LoadingPanel>
                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                </Header>
                                <Cell CssClass="gridcellleft">
                                </Cell>
                            </Styles>--%>
                                <SettingsSearchPanel Visible="True" />
                                <Settings ShowStatusBar="Hidden" ShowFilterRow="true" ShowGroupPanel="True" ShowFilterRowMenu="true" />
                                <%--<SettingsPager NumericButtonCount="20" PageSize="20" AlwaysShowPager="True" ShowSeparators="True">
                                <FirstPageButton Visible="True">
                                </FirstPageButton>
                                <LastPageButton Visible="True">
                                </LastPageButton>
                            </SettingsPager>--%>
                                <SettingsBehavior ConfirmDelete="True" />
                                <ClientSideEvents EndCallback="function(s, e) {
	    EndCall(s.cpHeight);
    }" />
                            </dxe:ASPxGridView>
                            <%--Rev 1.0--%>
                            <dx:linqservermodedatasource id="EntityServerlogModeDataSource" runat="server" onselecting="EntityServerModelogDataSource_Selecting"
                                        contexttypename="ERPDataClassesDataContext" tablename="FSMUser_Master_List" />
                            <%--End of Rev 1.0--%>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        
        <asp:SqlDataSource ID="RootUserDataSource" runat="server" ConflictDetection="CompareAllValues"
            DeleteCommand="DELETE FROM [tbl_master_user] WHERE [user_id] = @original_user_id"
            OldValuesParameterFormatString="original_{0}" SelectCommand="">
            <DeleteParameters>
                <asp:Parameter Name="original_user_id" Type="Decimal" />
            </DeleteParameters>
            <SelectParameters>
                <asp:SessionParameter Name="branch" SessionField="userbranchHierarchy" Type="string" />
            </SelectParameters>
        </asp:SqlDataSource>
        <dxe:ASPxGridViewExporter ID="exporter" runat="server">
        </dxe:ASPxGridViewExporter>
    </div>


    <div id="SelectPartyMapUsersModal" class="modal fade pmsModal w50" data-backdrop="static" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" onclick="close()">&times;</button>
                    <h4 class="modal-title">Select Party & Map Users</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-5">
                            <label>Select Party Type </label>
                            <div>
                                <asp:DropDownList ID="ddlPartyType" runat="server" class="demo" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" style="padding-top: 25px;">
                            <button type="button" class="btn btn-success" onclick="ShowParty();">Show Party</button>
                        </div>
                        <div class="col-md-5 hide">
                            <label>Select Party</label>
                            <div>
                                <asp:DropDownList ID="ddlpartys" runat="server" class="demo" multiple="multiple" Width="100%">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-success" onclick="MapUser();">Map User(s)</button>
                </div>
            </div>

        </div>
    </div>


    <dxe:ASPxPopupControl ID="PartySelectPopup" runat="server" Width="700"
        CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="cPartySelectPopup"
        HeaderText="Select Party" AllowResize="false" ResizingMode="Postponed" Modal="true">
        <ContentCollection>
            <dxe:PopupControlContentControl runat="server">

                <%--  <div style="margin-bottom: 10px; margin-top: 10px;" class="hide">
                    Apply for All Branch &nbsp; 
                     <asp:CheckBox ID="chkAllBranch" runat="server" OnClick="SelectAllBranches(this);" />
                    <asp:Label ID="lblBranch" runat="server" Text="All Branch Selected, No need to select individual Branch" CssClass="vehiclecls"></asp:Label>
                </div>--%>

                <dxe:ASPxGridView ID="PartyGrid" runat="server" KeyFieldName="Shop_Code" AutoGenerateColumns="False" OnDataBinding="PartyGrid_DataBinding"
                    Width="100%" ClientInstanceName="cPartyGrid" OnCustomCallback="PartyGrid_CustomCallback"
                    SelectionMode="Multiple" SettingsBehavior-AllowFocusedRow="true">
                    <Columns>
                        <dxe:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" Width="60" Caption="Select" />
                        <dxe:GridViewDataTextColumn Caption="Party Name" FieldName="Shop_Name"
                            VisibleIndex="1" FixedStyle="Left">
                            <CellStyle CssClass="gridcellleft" Wrap="true">
                            </CellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dxe:GridViewDataTextColumn>

                        <dxe:GridViewDataTextColumn Caption="Party Owner Contact" FieldName="Shop_Owner_Contact"
                            VisibleIndex="1" FixedStyle="Left">
                            <CellStyle CssClass="gridcellleft" Wrap="true">
                            </CellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dxe:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager NumericButtonCount="20" PageSize="10" ShowSeparators="True" Mode="ShowPager">
                        <FirstPageButton Visible="True">
                        </FirstPageButton>
                        <LastPageButton Visible="True">
                        </LastPageButton>
                    </SettingsPager>
                    <SettingsSearchPanel Visible="True" />
                    <Settings ShowGroupPanel="False" ShowStatusBar="Hidden" ShowHorizontalScrollBar="False" ShowFilterRow="true" ShowFilterRowMenu="true" />
                    <SettingsLoadingPanel Text="Please Wait..." />
                    <ClientSideEvents EndCallback="PartyGridEndCallBack" />
                </dxe:ASPxGridView>
                <br />
                <input type="button" value="Ok" class="btn btn-primary" onclick="SaveSelectedParty()" />
                <%-- <div style="float: right;">
                    <input type="button" runat="server" value="Select All" onclick="selectAll()" />
                    <input type="button" runat="server" value="Deselect All" onclick="unselectAll()" />
                </div>--%>
            </dxe:PopupControlContentControl>
        </ContentCollection>
    </dxe:ASPxPopupControl>




    <dxe:ASPxPopupControl ID="UserSelectPopup" runat="server" Width="700"
        CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="cUserSelectPopup"
        HeaderText="Select User" AllowResize="false" ResizingMode="Postponed" Modal="true">
        <ContentCollection>
            <dxe:PopupControlContentControl runat="server">

                <%--  <div style="margin-bottom: 10px; margin-top: 10px;" class="hide">
                    Apply for All Branch &nbsp; 
                     <asp:CheckBox ID="chkAllBranch" runat="server" OnClick="SelectAllBranches(this);" />
                    <asp:Label ID="lblBranch" runat="server" Text="All Branch Selected, No need to select individual Branch" CssClass="vehiclecls"></asp:Label>
                </div>--%>

                <dxe:ASPxGridView ID="UsersGrid" runat="server" KeyFieldName="user_id" AutoGenerateColumns="False" OnDataBinding="UsersGrid_DataBinding"
                    Width="100%" ClientInstanceName="cUsersGrid" OnCustomCallback="UsersGrid_CustomCallback"
                    SelectionMode="Multiple" SettingsBehavior-AllowFocusedRow="true">
                    <Columns>
                        <dxe:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" Width="60" Caption="Select" />
                        <dxe:GridViewDataTextColumn Caption="User Name" FieldName="user_name"
                            VisibleIndex="1" FixedStyle="Left">
                            <CellStyle CssClass="gridcellleft" Wrap="true">
                            </CellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dxe:GridViewDataTextColumn>

                        <dxe:GridViewDataTextColumn Caption="User Login Id" FieldName="user_loginId"
                            VisibleIndex="1" FixedStyle="Left">
                            <CellStyle CssClass="gridcellleft" Wrap="true">
                            </CellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dxe:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager NumericButtonCount="20" PageSize="10" ShowSeparators="True" Mode="ShowPager">
                        <FirstPageButton Visible="True">
                        </FirstPageButton>
                        <LastPageButton Visible="True">
                        </LastPageButton>
                    </SettingsPager>
                    <SettingsSearchPanel Visible="True" />
                    <Settings ShowGroupPanel="False" ShowStatusBar="Hidden" ShowHorizontalScrollBar="False" ShowFilterRow="true" ShowFilterRowMenu="true" />
                    <SettingsLoadingPanel Text="Please Wait..." />
                    <ClientSideEvents EndCallback="UsersGridEndCallBack" />
                </dxe:ASPxGridView>
                <br />
                <input type="button" value="Ok" class="btn btn-primary" onclick="SaveSelectedUser()" />
                <input type="button" value="Close" class="btn btn-primary" onclick="CloseUserpopUp()" />
                <%-- <div style="float: right;">
                    <input type="button" runat="server" value="Select All" onclick="selectAll()" />
                    <input type="button" runat="server" value="Deselect All" onclick="unselectAll()" />
                </div>--%>
            </dxe:PopupControlContentControl>
        </ContentCollection>
    </dxe:ASPxPopupControl>


    <dxe:ASPxPopupControl ID="UnAssignPartyPopup" runat="server" Width="700"
        CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="cUnAssignPartyPopup"
        HeaderText="Select User" AllowResize="false" ResizingMode="Postponed" Modal="true">
        <ContentCollection>
            <dxe:PopupControlContentControl runat="server">
                <dxe:ASPxGridView ID="UnAssignPartyGrid" runat="server" KeyFieldName="ID" AutoGenerateColumns="False" OnDataBinding="UnAssignPartyGrid_DataBinding"
                    Width="100%" ClientInstanceName="cUnAssignPartyGrid" OnCustomCallback="UnAssignPartyGrid_CustomCallback"
                    SelectionMode="Multiple" SettingsBehavior-AllowFocusedRow="true">
                    <Columns>
                        <dxe:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="1" Width="60" Caption="Select" />
                        <dxe:GridViewDataTextColumn Caption="Party Name" FieldName="Shop_Name"
                            VisibleIndex="2" FixedStyle="Left">
                            <CellStyle CssClass="gridcellleft" Wrap="true">
                            </CellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dxe:GridViewDataTextColumn>

                        <dxe:GridViewDataTextColumn Caption="Party Contact" FieldName="Party_Contact"
                            VisibleIndex="3" FixedStyle="Left">
                            <CellStyle CssClass="gridcellleft" Wrap="true">
                            </CellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dxe:GridViewDataTextColumn>

                        <dxe:GridViewDataTextColumn Caption="User name" FieldName="User_Name"
                            VisibleIndex="4" FixedStyle="Left">
                            <CellStyle CssClass="gridcellleft" Wrap="true">
                            </CellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dxe:GridViewDataTextColumn>

                        <dxe:GridViewDataTextColumn Caption="Login ID" FieldName="Login_ID"
                            VisibleIndex="5" FixedStyle="Left">
                            <CellStyle CssClass="gridcellleft" Wrap="true">
                            </CellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dxe:GridViewDataTextColumn>

                        <dxe:GridViewDataDateColumn Caption="Creation Date/Time" FieldName="CreationDate" PropertiesDateEdit-DisplayFormatString="dd-MM-yyyy HH:mm:ss"
                            VisibleIndex="6" Width="100px" ReadOnly="True">
                        </dxe:GridViewDataDateColumn>


                    </Columns>
                    <SettingsPager NumericButtonCount="20" PageSize="10" ShowSeparators="True" Mode="ShowPager">
                        <FirstPageButton Visible="True">
                        </FirstPageButton>
                        <LastPageButton Visible="True">
                        </LastPageButton>
                    </SettingsPager>
                    <SettingsSearchPanel Visible="True" />
                    <Settings ShowGroupPanel="False" ShowStatusBar="Hidden" ShowHorizontalScrollBar="False" ShowFilterRow="true" ShowFilterRowMenu="true" />
                    <SettingsLoadingPanel Text="Please Wait..." />
                    <ClientSideEvents EndCallback="UnAssignPartyEndCallBack" />
                </dxe:ASPxGridView>
                <br />
                <input type="button" value="Ok" class="btn btn-primary" onclick="UnAssignUser()" />
            </dxe:PopupControlContentControl>
        </ContentCollection>
    </dxe:ASPxPopupControl>
    <asp:HiddenField ID="hdnIsFaceDetectionOn" runat="server" />
    <asp:HiddenField ID="hdnEmployeeHierarchy" runat="server" />
    <%--Rev 1.0--%>
    <asp:HiddenField ID="hfIsFilter" runat="server" />
    <%--End of Rev 1.0--%>

    <%--Rev Work 08.04.2022
        Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked--%>
    <div runat="server" id="jsonProducts" class="hide"></div>
    <div runat="server" id="ModeType" class="hide"></div>
    <%--Rev work close 08.04.2022
        Mantise No:0024819 In user master in Assign Party entry section in edit mode selected party not coming as checked--%>
    <%--<dxe:ASPxPopupControl ID="AssignPartyListPopUp" runat="server" Width="700"
        CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="cAssignPartyListPopUp"
        HeaderText="Assign Party List" AllowResize="false" ResizingMode="Postponed" Modal="true">
        <ContentCollection>           
            <dxe:PopupControlContentControl runat="server">--%>
    <div id="HeaderPartyMapUsersModal" class="modal fade pmsModal w80" data-backdrop="static" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" onclick="close()">&times;</button>
                    <h4 class="modal-title">Assign Party List</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <input type="button" value="Add" class="btn btn-success " style="margin-bottom: 5px" onclick="AddParty()" />

                    </div>
                    <div class="row">

                        <dxe:ASPxGridView ID="grdAssignPartyList" runat="server" KeyFieldName="ID" AutoGenerateColumns="False" OnDataBinding="grdAssignPartyList_DataBinding"
                            Width="100%" ClientInstanceName="cgrdAssignPartyList" OnCustomCallback="grdAssignPartyList_CustomCallback"
                            SettingsBehavior-AllowFocusedRow="true">
                            <Columns>
                                <dxe:GridViewDataTextColumn Caption="Name" FieldName="NAME"
                                    VisibleIndex="1" FixedStyle="Left">
                                    <CellStyle CssClass="gridcellleft" Wrap="true">
                                    </CellStyle>
                                    <Settings AutoFilterCondition="Contains" />
                                </dxe:GridViewDataTextColumn>

                                <dxe:GridViewDataTextColumn Caption="Party" FieldName="Party"
                                    VisibleIndex="2" FixedStyle="Left">
                                    <CellStyle CssClass="gridcellleft" Wrap="true">
                                    </CellStyle>
                                    <Settings AutoFilterCondition="Contains" />
                                </dxe:GridViewDataTextColumn>

                                <dxe:GridViewDataDateColumn Caption="Creation Date/Time" FieldName="CREATED_ON" PropertiesDateEdit-DisplayFormatString="dd-MM-yyyy HH:mm:ss"
                                    VisibleIndex="3" Width="100px" ReadOnly="True">
                                </dxe:GridViewDataDateColumn>

                                <dxe:GridViewDataDateColumn Caption="Update Date/Time" FieldName="UPDATED_ON" PropertiesDateEdit-DisplayFormatString="dd-MM-yyyy HH:mm:ss"
                                    VisibleIndex="4" Width="100px" ReadOnly="True">
                                </dxe:GridViewDataDateColumn>

                                <dxe:GridViewDataTextColumn VisibleIndex="11" Width="6%" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>

                                        <% if (rights.CanEdit)
                                           { %>
                                        <a href="javascript:void(0);" onclick="EditAssignDetails('<%# Container.KeyValue %>')" title="Edit" class="pad">
                                            <img src="../../../assests/images/Edit.png" /></a>
                                        <% } %>
                                        <% if (rights.CanDelete)
                                           { %>
                                        <a href="javascript:void(0);" onclick="DeleteAssignDetails('<%# Container.KeyValue %>')" title="Delete" class="pad">
                                            <img src="../../../assests/images/Delete.png" /></a>
                                        <% } %>
                                    </DataItemTemplate>
                                    <HeaderTemplate>Actions</HeaderTemplate>
                                    <EditFormSettings Visible="False" />

                                </dxe:GridViewDataTextColumn>

                            </Columns>
                            <SettingsPager NumericButtonCount="20" PageSize="10" ShowSeparators="True" Mode="ShowPager">
                                <FirstPageButton Visible="True">
                                </FirstPageButton>
                                <LastPageButton Visible="True">
                                </LastPageButton>
                            </SettingsPager>
                            <SettingsSearchPanel Visible="True" />
                            <Settings ShowGroupPanel="False" ShowStatusBar="Hidden" ShowHorizontalScrollBar="False" ShowFilterRow="true" ShowFilterRowMenu="true" />
                            <SettingsLoadingPanel Text="Please Wait..." />
                            <%--<ClientSideEvents EndCallback="UnAssignPartyEndCallBack" />--%>
                        </dxe:ASPxGridView>
                        <br />
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    <%--<button type="button" class="btn btn-success" onclick="MapUser();">Map User(s)</button>--%>
                </div>
            </div>

        </div>
    </div>
    <%--            </dxe:PopupControlContentControl>
        </ContentCollection>
    </dxe:ASPxPopupControl>--%>

    <div id="PartyMapUsersModal" class="modal fade pmsModal w80" data-backdrop="static" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" onclick="close()">&times;</button>
                    <h4 class="modal-title">Select Party & Map Users</h4>
                </div>
                <div class="modal-body">

                    <div class="clearfix">
                        <div class="col-lg-4">
                            <label><b>Name</b><span style="color: red">*</span></label>
                            <input type="text" id="txtName" class="form-control" />
                            <asp:HiddenField ID="hdnHeaderId" runat="server" />
                        </div>
                        <div class="col-lg-4">
                            <label><b>Select Party Type</b></label>
                            <%-- <select name="to" id="ddlPartyTypes" class="form-control" >
                            </select>--%>
                            <asp:DropDownList ID="ddlPartyTypes" runat="server" class="demo" Width="100%">
                            </asp:DropDownList>
                        </div>

                        <div class="col-lg-4">
                            <label><b>Select Party</b></label>
                            <div>
                                <dxe:ASPxButtonEdit ID="txtPartys" runat="server" ReadOnly="true" ClientInstanceName="ctxtPartys" TabIndex="8" Width="100%">
                                    <Buttons>
                                        <dxe:EditButton>
                                        </dxe:EditButton>
                                    </Buttons>
                                    <ClientSideEvents ButtonClick="function(s,e){PartyButnClick();}" KeyDown="PartybtnKeyDown" />
                                </dxe:ASPxButtonEdit>
                                <asp:HiddenField ID="txtParty_hidden" runat="server" />
                            </div>
                        </div>
                    </div>

                    <%--Mantis Issue 24362--%>
                    <div class="clearfix"></div>
                    <div class="clearfix">
                        <div class="col-lg-4">
                            <label><b>Branch</b></label>
                            <asp:DropDownList ID="ddlBranch" runat="server" class="demo" Width="100%">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-4" style="padding-top:26px">
                            <button type="button" class="btn btn-success" onclick="ddsstatechange();">Show</button>
                        </div>
                    </div>
                        <%--End of Mantis Issue 24362--%>

                    <div class="clearfix">
                        <div class="col-md-8">
                            <label>Select users</label>
                            <div>
                                <div id="transfer1" class="transfer-demo"></div>
                            </div>
                        </div>
                    </div>



                    <%--  <div class="col-md-12 text-center">
                        <button type="button" class="btn btn-success" onclick="DashboardSettingSave()">Create Setting</button>
                        <button type="button" class="btn btn-danger" onclick="DashboardSettingCancel()">Cancel</button>
                    </div>--%>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-success" onclick="SaveAssignParty();">Save</button>
                </div>
            </div>

        </div>
    </div>



    <div class="modal fade pmsModal w80" id="PartyModel" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Party Search</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="partyskeydown(event)" class="form-control" id="txtPartySearch" autofocus style="width: 100%" placeholder="Search By Party Name or Party Code" />
                    <div id="PartyTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>                                
                                <th>Name</th>
                                <%-- <th>Code</th>
                            <th>Location</th>--%>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--Mantis Issue 24363--%>
                    <button type="button" class="btnOkformultiselection btn-default  btn btn-success" data-dismiss="modal" onclick="OKPopup('PartySource')">OK</button>
                    <%--End of Mantis Issue 24363--%>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <%--Rev 3.0--%>
    <div class="modal fade pmsModal w80 " id="UserModel" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">User Search</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="Userkeydown(event)" id="txtUserSearch" class="form-control" autofocus width="100%" placeholder="Search By User Code" />

                    <div id="UserTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>User Name</th>
                                <th>Login ID</th>
                                <th>Employee ID</th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSaveUser" class="btnOkformultiselection btn-default  btn btn-success" data-dismiss="modal" onclick="OKPopup('UserSource')">OK</button>
                    <button type="button" id="btnCloseUser" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>
    <%--End of Rev 3.0--%>
</asp:Content>
