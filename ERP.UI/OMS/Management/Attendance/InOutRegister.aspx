<%--================================================== Revision History ===================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                21/02/2023        V2.0.39          Pallab              Settings/Options module design modification 
====================================================== Revision History ===================================================--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" CodeBehind="InOutRegister.aspx.cs" Inherits="ERP.OMS.Management.Attendance.InOutRegister" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .topTableri {
            margin-top: 10px;
        }

            .topTableri > tbody > tr > td {
                padding-right: 15px;
                padding-bottom: 10px;
            }


        .paddingright0 {
          padding-right: 2px !important;
        }
             
        .bandedHeader {
            background: #54749D;
            font-size: medium;
        }

        .gridHeader {
            background: #54749D !important;
        }

        .dxgvSelectedRow_PlasticBlue {
            background: #54749D;
        }

        .exprtClass {
            background: #54749D;
            font-size: 16px;
            margin-top: 10px;
            color: white;
        } 
        .rightStaticNav {
                position: fixed;
                top: 0;
                height: 100vh;
                width: 250px;
                right: 0;
                background: #bfe3ec;
                padding: 15px;
                -webkit-transition:all 0.3s ease-in-out;
                transition:all 0.3s ease-in-out;
                z-index:9999;
        }  
        .rightStaticNav.off {
            right:-260px;
            opacity:0;
        }
        .rightStaticNav.on {
            right:0px;
            opacity:1;
        }


        #HierchyDiv table {
        width :100%
        }

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
        bottom: 18px;
        right: 20px;
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

    #dtFrom, #dtTo, #FormDate, #toDate, #proj_start_dt, .calendar-custom-icon {
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

    .exprtClass
    {
        background: #094e8c;
    }

    .dxeListBoxItemSelected_PlasticBlue
    {
        background-color: #094e8c;
    }

    /*Rev end 1.0*/

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <link href="../Activities/CSS/SearchPopup.css" rel="stylesheet" />
    <script src="../Activities/JS/SearchMultiPopup.js"></script>
    <script src="Js/InOutRegister.js?v=0.1"></script>


    <div class="breadCumb">
        <span>Employee(s) In-Out Register</span>
    </div>



    <div class="container">
        <div class="clearfix">
            <div class="backBox mt-5 mb-4 p-3">
                <table class="topTableri">
                    <tr>
                       <%-- <td>Consider <br />Payroll Branch</td>--%>
                       


                        <td style="color: #333" class="paddingright0">Branch
                        </td>
                        <td colspan="2">
                            <dxe:ASPxComboBox ID="cmbBranch" ClientInstanceName="ccmbBranch"
                                runat="server" ValueType="System.String">
                                <ClientSideEvents SelectedIndexChanged="cmbBranchChange" />
                            </dxe:ASPxComboBox>
                        </td>

                         <td style="color: #333; padding-right: 15px !important;" class="paddingright0">Employee</td>
                        <td colspan="2">
                            <dxe:ASPxButtonEdit ID="empButtonEdit" ReadOnly="true" runat="server" ClientInstanceName="cempButtonEdit" ClientEnabled="true">
                                <Buttons>
                                    <dxe:EditButton>
                                    </dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s,e){EmployeeSelect();}" KeyDown="function(s,e){EmployeebtnKeyDown(s,e);}" />
                            </dxe:ASPxButtonEdit>
                        </td>



                        <td style="color: #333" class="paddingright0">All
                        </td>
                        <td colspan="2">
                            <dxe:ASPxCheckBox ID="chkAllEmp" runat="server" ClientInstanceName="chkAllEmp" >
                                <ClientSideEvents CheckedChanged="allEmpChange" />
                            </dxe:ASPxCheckBox>
                        </td>

                       


                        <td style="color: #333; padding-right: 15px !important;" class="paddingright0">Date</td>
                        <%--Rev 2.0: "calendar-custom-icon" class add--%>
                        <td class="calendar-custom-icon">
                            <dxe:ASPxDateEdit ID="FormDate" runat="server" EditFormat="Custom" EditFormatString="dd-MM-yyyy" AllowNull="false"
                                ClientInstanceName="cFormDate" Width="100%" DisplayFormatString="dd-MM-yyyy" UseMaskBehavior="True">
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </dxe:ASPxDateEdit>
                            <%--Rev 2.0--%>
                            <img src="/assests/images/calendar-icon.png" class="calendar-icon" />
                            <%--Rev end 2.0--%>
                        </td>
                   

                    
                         
                           <td style="color: #333" class="paddingright0">Show Inactive</td>
                           <td>
                               <dxe:ASPxCheckBox ID="chkInactive" runat="server" ClientInstanceName="cchkInactive" Checked="true">
                               </dxe:ASPxCheckBox>
                           </td>

                         
                       </tr>
                        <tr>

                           <td class="setWtd" >
                               <button type="button" class="btn btn-success" id="BtnShow" onclick="ShowReport()">Generate</button></td>

                             <td style="padding-bottom: 21px;">
                                  <asp:DropDownList ID="drdExport" runat="server" CssClass="exprtClass btn btn-primary" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Export to</asp:ListItem>
                                        <asp:ListItem Value="2">XLSX</asp:ListItem> 
                                    </asp:DropDownList>
                             </td>

                              <td> 
                                    <dxe:ASPxCheckBox ID="chkPayrollBranch" runat="server" Visible="false"></dxe:ASPxCheckBox>
                                </td>
                        </tr>
                </table>

            


             <dxe:ASPxGridViewExporter ID="exporter" GridViewID="GridDetail" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
            </dxe:ASPxGridViewExporter>


            <dxe:ASPxGridView ID="GridDetail" runat="server" ClientInstanceName="cGridDetail" KeyFieldName="internalId"
                Width="100%" Settings-HorizontalScrollBarMode="Auto" DataSourceID="EntityServerModeDataSource"
                SettingsBehavior-ColumnResizeMode="Control" OnCustomCallback="GridDetail_CustomCallback"
                Settings-VerticalScrollableHeight="275" SettingsBehavior-AllowSelectByRowClick="true"
                Settings-VerticalScrollBarMode="Auto"
                Settings-ShowFilterRow="true" Settings-ShowFilterRowMenu="true">

                <Columns>

                    <dxe:GridViewBandColumn HeaderStyle-CssClass="bandedHeader" Caption="Employee Details" Name="MainBandedHeader" FixedStyle="Left" HeaderStyle-HorizontalAlign="Center">
                        <Columns>

                            <dxe:GridViewDataTextColumn HeaderStyle-CssClass="gridHeader" Caption="Employee code" Width="150" FieldName="Empcode">
                                <Settings AllowAutoFilterTextInputTimer="False" />
                                <Settings AutoFilterCondition="Contains" />
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn HeaderStyle-CssClass="gridHeader" Caption="Employee Name" Width="250" FieldName="EmpName">
                                <Settings AllowAutoFilterTextInputTimer="False" />
                                <Settings AutoFilterCondition="Contains" />
                            </dxe:GridViewDataTextColumn>

                        </Columns>
                    </dxe:GridViewBandColumn>



                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="In-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Intime1" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>
                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="Out-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Outime1" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>


                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="In-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Intime2" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>
                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="Out-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Outime2" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>


                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="In-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Intime3" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>
                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="Out-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Outime3" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>


                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="In-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Intime4" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>
                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="Out-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Outime4" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>


                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="In-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Intime5" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>
                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="Out-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Outime5" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>


                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="In-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Intime6" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>
                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="Out-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Outime6" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>


                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="In-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Intime7" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>
                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="Out-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Outime7" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>


                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="In-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Intime8" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>
                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="Out-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Outime8" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>


                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="In-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Intime9" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>
                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="Out-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Outime9" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>


                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="In-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Intime10" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>
                    <dxe:GridViewDataTimeEditColumn HeaderStyle-CssClass="gridHeader" Caption="Out-Time" Width="70" CellStyle-HorizontalAlign="Center"
                        FieldName="Outime10" PropertiesTimeEdit-DisplayFormatString="h:mm tt" PropertiesTimeEdit-EditFormatString="h:mm tt">
                    </dxe:GridViewDataTimeEditColumn>



                    <dxe:GridViewBandColumn HeaderStyle-CssClass="bandedHeader" Caption="In-Out Count" Name="MainBandedHeader" FixedStyle="Left" HeaderStyle-HorizontalAlign="Center">
                        <Columns>

                            <dxe:GridViewDataTextColumn HeaderStyle-CssClass="gridHeader" Caption="In-Time" Width="70" FieldName="inCount" CellStyle-HorizontalAlign="Center">
                                <Settings AllowAutoFilterTextInputTimer="False" />
                                <Settings AutoFilterCondition="Contains" />
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn HeaderStyle-CssClass="gridHeader" Caption="Out-Time" Width="70" FieldName="outCount" CellStyle-HorizontalAlign="Center">
                                <Settings AllowAutoFilterTextInputTimer="False" />
                                <Settings AutoFilterCondition="Contains" />
                            </dxe:GridViewDataTextColumn>

                        </Columns>
                    </dxe:GridViewBandColumn>

                </Columns>
                <ClientSideEvents BeginCallback="gridBeginCallBack" RowDblClick="rowDbClick" />

            </dxe:ASPxGridView>




            <dx:LinqServerModeDataSource ID="EntityServerModeDataSource" runat="server" OnSelecting="EntityServerModeDataSource_Selecting"
                ContextTypeName="ERPDataClassesDataContext" TableName="tbl_EmpAttendanceRecord_reports" />


        </div>
</div>






        <asp:HiddenField ID="EmpId" runat="server" />


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
                    <input type="text" class="form-control" onkeydown="Employeekeydown(event)" id="txtEmpSearch" autocomplete="off" autofocus width="100%" placeholder="Search By Employee Name or Unique Id" />

                    <div id="EmployeeTable" class="mt-3">
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
                    <button type="button" class="btn btn-danger btn-sm" style="margin-bottom: 0px !important;" onclick="DeSelectAll('EmpSource')">Deselect All</button>
                    <button type="button" class="btn btn-primary btn-sm" data-dismiss="modal" onclick="OKPopup('EmpSource')">OK</button>
                </div>
            </div>

        </div>
    </div>



    <div style="display: none">
        <dxe:ASPxComboBox ID="ErrorCheck" ClientInstanceName="cErrorCheck"
            runat="server" ValueType="System.String">
        </dxe:ASPxComboBox>
    </div>



    <nav class="rightStaticNav off" id="rightNav">
        <span onclick="navOnOff()" style="cursor:pointer"> Close(X)</span>
        <div id="HierchyDiv"> </div>
    </nav>






</asp:Content>
