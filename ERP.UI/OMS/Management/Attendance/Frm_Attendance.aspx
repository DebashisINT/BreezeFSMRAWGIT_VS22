<%--====================================================== Revision History ===================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                21/02/2023        V2.0.39          Pallab              Settings/Options module design modification 
====================================================== Revision History ===================================================--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frm_Attendance.aspx.cs" MasterPageFile="~/OMS/MasterPage/ERP.Master"
    Inherits="ERP.OMS.Management.Attendance.Frm_Attendance" EnableEventValidation="false" EnableViewStateMac="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Css/Attendance.css" rel="stylesheet" />
    <link href="../../../Scripts/pluggins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../Scripts/pluggins/multiselect/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Js/Attendance.js?v=2.01"></script>
    <script src="../Activities/JS/SearchPopup.js"></script>
    <link href="../Activities/CSS/SearchPopup.css?v=0.02" rel="stylesheet" />

    <style>
        /*Rev 1.0*/

    body, .dxtcLite_PlasticBlue {
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
        right: 24px;
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

    .h-branch-select, .h-branch-select-new {
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
            line-height: 17px;
            z-index: 0;
        }

        .h-branch-select-new::after {
            content: '<';
            /*content: url(../../../assests/images/left-arw.png);*/
            position: absolute;
            top: 9px;
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

    select:not(.btn):focus {
        border-color: #094e8c;
    }

    select:not(.btn):focus-visible {
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
    .dxgvControl_PlasticBlue, .dxgvDisabled_PlasticBlue {
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

    #dtFrom, #dtTo, #FormDate, #toDate, #proj_start_dt {
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

    #dtFrom_B-1, #dtTo_B-1, #cmbDOJ_B-1, #cmbLeaveEff_B-1, #FormDate_B-1, #toDate_B-1, #proj_start_dt_B-1 {
        background: transparent !important;
        border: none;
        width: 30px;
        padding: 10px !important;
    }

        #dtFrom_B-1 #dtFrom_B-1Img,
        #dtTo_B-1 #dtTo_B-1Img, #cmbDOJ_B-1 #cmbDOJ_B-1Img, #cmbLeaveEff_B-1 #cmbLeaveEff_B-1Img, #FormDate_B-1 #FormDate_B-1Img, #toDate_B-1 #toDate_B-1Img, #proj_start_dt_B-1 #proj_start_dt_B-1Img {
            display: none;
        }

    #FormDate_I, #toDate_I, #proj_start_dt_I {
        background: transparent;
    }

    .for-cust-icon {
        position: relative;
        /*z-index: 1;*/
    }

    .pad-md-18 {
        padding-top: 10px;
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

    .dxeTextBox_PlasticBlue {
        height: 34px;
        border-radius: 4px;
    }

    #cmbDOJ_DDD_PW-1 {
        z-index: 9999 !important;
    }

    #cmbDOJ, #cmbLeaveEff {
        position: relative;
        z-index: 1;
        background: transparent;
    }

    .btn-sm, .btn-xs {
        padding: 7px 10px !important;
    }

    #productAttributePopUp_PWH-1 span, #ASPxPopupControl2_PWH-1 span {
        color: #fff !important;
    }

    #marketsGrid_DXPEForm_tcefnew, .dxgvPopupEditForm_PlasticBlue {
        height: 220px !important;
    }

    .btn {
        height: 34px;
    }

    .dxpcLite_PlasticBlue .dxpc-header, .dxdpLite_PlasticBlue .dxpc-header {
        padding: 6px 4px 6px 10px;
        background: #094e8c;
    }

    #pcModalMode_PW-1 {
        border-radius: 12px;
        overflow: hidden;
    }

    .cke_bottom {
        background: #094e8c;
    }

    .fff > thead > tr > th {
        background: #094e8c;
    }

    .btn i{
        margin-right: 5px;
    }

    .employeeNameClass
    {
        padding: 5px 10px;
    }

    .employeeNameClass i
    {
        margin-right: 5px;
    }

    .timeBox .line
    {
        margin-bottom: 10px;
    }
    .timeBox
    {
        padding-bottom: 10px;
    }

    /*Rev end 1.0*/
    </style>

    <div class="breadCumb">
        <span>
            <label id="lblheading">Attendance</label>
        </span>
    </div>
    <div class="container clearfix backBox mt-5 mb-4 py-3">
        <div class="timeWraper">
            <div class="timeBox">
                <div class="line"></div>
                <div class="dateMnth" id="Day_span"></div>
                <div id="time_span"></div>

                <div class="btnWrap">
                    <button type="button" class="btn btn-success" id="BtnShowEmployee" onclick="EmployeeSelect()"><i class="fa fa-search"></i>Select Employee</button>
                    <button type="button" class="btn btn-primary" id="BtnSubmitRequest" onclick="AttendanceSubmit()"><i class="fa fa-clock-o"></i>In Time</button>
                    <button type="button" class="btn btn-primary" id="BtnExitRequest" onclick="LogOutSubmit();"><i class="fa fa-clock-o"></i>Out Time</button>
                </div>

                <div class="text-center">
                    <label class="label label-default employeeNameClass"><i class="fa fa-user"></i><span id="EmployeeNameSpan"></span></label>
                </div>

            </div>
        </div>
        <div class="text-center p-4">
            <span style="color: red; font-size: 13px;">* Date/Time of the Local PC. Attendance Time In/Out will be according to the Server Date/Time</span>
        </div>
    </div>



    <asp:HiddenField runat="server" ID="EmpId" />
    <asp:HiddenField ID="hdEmpName" runat="server" />

    <asp:HiddenField ID="hdnisGivenAttendance" runat="server" />
    <asp:HiddenField ID="hdnIsLeaveonApprovval" runat="server" />
    <asp:HiddenField runat="server" ID="hdnUserID" />


    <div class="modal fade pmsModal w40" id="inOutModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">In Out Entry</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <%--Rev 1.0--%>
                        <%--<div class="col-md-6">--%>
                        <div class="col-md-6 h-branch-select">
                            <%--Rev end 1.0--%>
                            <label>Mark your attendance</label>
                            <div>
                                <select class="form-control" id="markWork" onchange="markWork_change();">
                                    <option value="0">Select</option>
                                    <option value="1">At Work</option>
                                    <option value="2">On Leave</option>
                                </select>
                            </div>
                        </div>

                    </div>
                    <div class="row" id="workTyp">
                        <div class="col-md-6 mt-3">
                            <label title="Shop Type">Select Work Type :</label>
                            <div class="fullMulti">
                                <%-- <asp:DropDownList ID="ddlWorkType" runat="server" class="demo" multiple="multiple" Width="100%">
                            <asp:ListItem Value="0">Home Restriction</asp:ListItem>
                            <asp:ListItem Value="1">No Restriction</asp:ListItem>
                            <asp:ListItem Value="2">Fix Location</asp:ListItem>
                        </asp:DropDownList>--%>
                                <div id="Workdiv">
                                    <select id="ddlWorkType" runat="server" class="demo" multiple="true" width="100%">
                                        <option>Select</option>
                                    </select>
                                </div>

                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="col-md-6 mt-3 hide">
                            <label>Distributor Name :</label>
                            <div class="">
                                <input type="text" class="form-control" />
                            </div>
                        </div>
                        <div class="col-md-12 mt-3">
                            <label>Remarks :</label>
                            <div class="">
                                <input type="text" class="form-control" id="txtWorkRemarks" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="leaveTyp" style="display: none">
                        <div class="col-md-6 mt-3">
                            <label title="Shop Type">Select Leave Type :</label>
                            <div class="fullMulti">
                                <%-- <asp:DropDownList ID="ddlLeaveType" runat="server" class="demo" Width="100%">
                            <asp:ListItem Value="0">Home Restriction</asp:ListItem>
                            <asp:ListItem Value="1">No Restriction</asp:ListItem>
                            <asp:ListItem Value="2">Fix Location</asp:ListItem>
                        </asp:DropDownList>--%>
                                <div id="Leavediv">
                                    <select id="ddlLeaveType" class="demo" width="100%">
                                        <option>Select</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="col-md-3 mt-3">
                            <label>Leave From :</label>
                            <div style="position: relative">
                                   <dxe:ASPxDateEdit Width="100%" ID="cmbDOJ" runat="server">
                                        </dxe:ASPxDateEdit>
                            </div>
                        </div>
                        <div class="col-md-3 mt-3">
                            <label>Leave To :</label>
                            <div style="position: relative">
                                  <dxe:ASPxDateEdit ID="cmbLeaveEff" runat="server" DateOnError="Today" EditFormat="Custom"
                                            Width="100%">
                                        </dxe:ASPxDateEdit>
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="col-md-12 mt-3">
                            <label>Write your Reason :</label>
                            <div class="">
                                <input type="text" class="form-control" id="txtLeaveReason" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" onclick="Attendance();">Submit</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>





    <!--Employee Modal -->
    <div class="modal fade" id="EmployeeModel" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Employee Search</h4>
                </div>
                <div class="modal-body">
                    <input type="text" class="form-control mb-3" onkeydown="Employeekeydown(event)" id="txtEmpSearch" autofocus width="100%" placeholder="Search By Employee Name or Unique Id" />

                    <div id="EmployeeTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Employee Name</th>
                                <th>Employee Id</th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>


</asp:Content>
