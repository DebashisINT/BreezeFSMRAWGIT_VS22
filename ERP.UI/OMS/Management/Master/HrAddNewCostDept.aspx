<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                08-02-2023        2.0.39           Pallab              25656 : Master module design modification 
====================================================== Revision History ==========================================================--%>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master"
    Inherits="ERP.OMS.Management.Master.management_master_HrAddNewCostDept" CodeBehind="HrAddNewCostDept.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--JS Inline Method-->
    <script language="javascript" type="text/javascript">
        FieldName = "DontKnow";
        //function SignOff()
        //{
        //    window.parent.SignOff();
        //}
        //function height()
        //{        
        //   if(document.body.scrollHeight>=350)
        //    window.frameElement.height = document.body.scrollHeight;
        //   else
        //    window.frameElement.height = '350px';
        //    window.frameElement.Width = document.body.scrollWidth;
        //}
        function Message(obj) {
            if (obj == 1) {
                alert("Successfully Inserted");
                editwin.close();
            }
            else
                alert("There is Any Problem To Save Data!!!\n Please Try Again");
        }
        function Cancel() {
            alert('111')
            window.location.href = "HRCostCenter.aspx";

        }
    </script>
    <style>
        #RequiredFieldValidator1, #revEmailID {
            position:absolute;
            right: -5px;
            top: 7px;
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

    .h-branch-select {
        position: relative;
    }

        .h-branch-select::after {
            /*content: '<';*/
            content: url(../../../assests/images/left-arw.png);
            position: absolute;
            top: 8px;
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

    .pmsModal .modal-header {
        background: #094e8c !important;
        background-image: none !important;
        padding: 11px 20px;
        border: none;
        border-radius: 5px 5px 0 0;
        color: #fff;
        border-radius: 10px 10px 0 0;
    }

    .pmsModal .modal-content {
        border: none;
        border-radius: 10px;
    }

    .pmsModal .modal-header .modal-title {
        font-size: 14px;
    }

    .pmsModal .close {
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

    #dtFrom_B-1, #dtTo_B-1 {
        background: transparent !important;
        border: none;
        width: 30px;
        padding: 10px !important;
    }

        #dtFrom_B-1 #dtFrom_B-1Img,
        #dtTo_B-1 #dtTo_B-1Img {
            display: none;
        }

    #dtFrom_I, #dtTo_I {
        background: transparent;
    }

    .for-cust-icon {
        position: relative;
        z-index: 1;
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

    /*Rev end 1.0*/
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--rev 25249--%>
    <%--<div class="panel-heading">
        <div class="panel-title">
            <h3>Add Cost Centers/Departments</h3>
            <div class="crossBtn"><a href="HRCostCenter.aspx"><i class="fa fa-times"></i></a></div>
        </div>

    </div>--%>
    <div class="breadCumb">
        <span>Add Cost Centers/Departments</span>
        <div class="crossBtnN"><a href="HRCostCenter.aspx"><i class="fa fa-times"></i></a></div>
    </div>
    <%--rev end 25249--%>

    <div class="container">
        <div class="backBox mt-5 p-4 ">
    <div class="form_main">

        <div class="form_main" style=" background: #fff; border-radius: 15px; padding: 15px;">
            <table>
                <tr>
                    <td style="height: 277px">
                        <table class="pdtble" width="" style="" border="0">
                            <tr>
                                <td style="text-align: left; vertical-align: top; height: 11px;width:245px">Cost Center/Department Name<span style="color: red">*</span>
                                </td>
                                <td  style="position:relative;width:260px">
                                    <asp:TextBox ID="TxtCenter" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" ToolTip="Mandatory" ForeColor="Red" CssClass="pullleftClass fa fa-exclamation-circle spl" ControlToValidate="TxtCenter" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>


                            </tr>
                            <tr>
                                <td style="text-align: left; height: 11px; vertical-align: top; width: 190px">Cost Center/Department Type</td>
                                <%--Rev 1.0--%>
                                <%--<td style="height: 24px; ">--%>
                                <td style="height: 24px; " class=" h-branch-select">
                                    <%--Rev end 1.0--%>
                                    <asp:DropDownList ID="DDLType" runat="server" Width="250px">
                                        <asp:ListItem>Department</asp:ListItem>
                                        <asp:ListItem>Employee</asp:ListItem>
                                        <asp:ListItem>Branch</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: left; vertical-align: top; height: 11px;">Parent Cost Center/Department</td>
                                <%--Rev 1.0--%>
                                <%--<td style="">--%>
                                 <td style="" class=" h-branch-select">
                                      <%--Rev end 1.0--%>
                                    <asp:DropDownList ID="DDLCostDept" runat="server" Width="250px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: left; vertical-align: top; height: 11px;">Head Of Department</td>
                                <%--Rev 1.0--%>
                                <%--<td style="">--%>
                                 <td style="" class=" h-branch-select">
                                      <%--Rev end 1.0--%>
                                    <asp:DropDownList ID="DDLHeadDept" runat="server" Width="250px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtDptHead" runat="server" Width="250px" Style="display: none"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: left; vertical-align: top; height: 11px;">Email ID</td>
                                <td style="position:relative;width:260px">
                                    <asp:TextBox ID="TxtEmail" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>

                                    <asp:RegularExpressionValidator ID="revEmailID" runat="server"
                                        ControlToValidate="TxtEmail"  ToolTip="Invalid Email" ForeColor="Red" CssClass="pullleftClass fa fa-exclamation-circle spl"  Display="Dynamic"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td style="vertical-align: top; height: 11px; text-align: left"></td>
                                <td style=" padding: 0">
                                    <table style="width: 250px">
                                        <tr>
                                            <td style="width: 100px">
                                                <asp:CheckBox ID="ChkFund" runat="server" Text="Mutual Funds" Width="124px" />
                                            </td>
                                            <td style="width: 100px">
                                                <asp:CheckBox ID="ChkBrok" runat="server" Text="Broking " />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="ChkInsu" runat="server" Text="Insurance" /></td>
                                            <td>
                                                <asp:CheckBox ID="ChkDepos" runat="server" Text="Depository" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; vertical-align: top; height: 11px;">&nbsp;</td>
                                <td style="">
                                    <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; height: 11px; text-align: left">&nbsp;</td>
                                <td style="">&nbsp;<asp:Button ID="BtnSave" runat="server" CssClass="btn btn-success" Text="Save" CausesValidation="true" OnClick="BtnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
   </div>
  </div>
    <asp:HiddenField ID="txtDptHead_hidden" runat="Server" />
</asp:Content>
