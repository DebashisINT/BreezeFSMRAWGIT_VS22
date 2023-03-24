<%--====================================================== Revision History ===============================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                16-02-2023        V2.0.39          Pallab              Settings/Options module design modification 
====================================================== Revision History ===================================================--%>

<%@ Page Title="User Home Location" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" 
    Inherits="ERP.OMS.Management.Master.Salesman_AddressList" CodeBehind="Salesman-AddressList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   

    <script language="javascript" type="text/javascript">
        FieldName = null;

        function EditBranch(bgid) {
            var url = 'Salesman-Address.aspx?Addid=' + bgid;
            //window.open(url,'aa'); 
            //OnMoreInfoClick(url, "Edit BranchGroup", '940px', '450px', 'Y');
            window.location.href = url;

        }
        function AddBranch() {
            var url = 'Salesman-Address.aspx?Addid=add'
            //window.open(url,'aa'); 
            //OnMoreInfoClick(url, "Add BranchGroup", '940px', '450px', 'Y');
            window.location.href = url;

        }
        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }


        function DeleteBranch(bgid) {

            //  var QuoteNo = ctxt_replcno.GetText();

            var CheckUniqueCode = false;
            $.ajax({
                type: "POST",
                url: "Salesman-AddressList.aspx/DeleteImei",
                data: JSON.stringify({ Addid: bgid }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    CheckUniqueCode = msg.d;
                    if (CheckUniqueCode == true) {

                        grid.Refresh();
                    }
                    else {

                    }
                }
            });

        }
    </script>

    <%--Rev 1.0--%>
    <style>
        
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
        right: 14px;
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

    #dtFrom, #dtTo, #FormDate, #toDate {
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

    #dtFrom_B-1, #dtTo_B-1 , #cmbDOJ_B-1, #cmbLeaveEff_B-1, #FormDate_B-1, #toDate_B-1 {
        background: transparent !important;
        border: none;
        width: 30px;
        padding: 10px !important;
    }

        #dtFrom_B-1 #dtFrom_B-1Img,
        #dtTo_B-1 #dtTo_B-1Img , #cmbDOJ_B-1 #cmbDOJ_B-1Img, #cmbLeaveEff_B-1 #cmbLeaveEff_B-1Img, #FormDate_B-1 #FormDate_B-1Img, #toDate_B-1 #toDate_B-1Img {
            display: none;
        }

    #FormDate_I, #toDate_I {
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

    .btn-sm, .btn-xs, .btn
    {
        padding: 7px 10px !important;
        height: 34px;
    }

    #productAttributePopUp_PWH-1 span, #ASPxPopupControl2_PWH-1 span
    {
        color: #fff !important;
    }

    #marketsGrid_DXPEForm_tcefnew, .dxgvPopupEditForm_PlasticBlue
    {
        height: 135px !important;
    }
    
    </style>
    <%--Rev end 1.0--%>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div class="breadCumb">
            <span>Salesman Address</span>
        </div>

   
    <div class="container">
        <div class="backBox mt-5 p-3 ">
         <div class="SearchArea clearfix">
                <div class="FilterSide mb-4">
                    <div style="padding-right: 5px;">
                        <% if (rights.CanAdd)
                           { %>
                      <a href="javascript:void(0);" onclick="AddBranch();" class="btn btn-success mr-2"><span>Add New</span> </a>
                        <% } %>
                      

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
                    </div></div>
        <table class="TableMain100">
           
            <tr>
                <td>
                    <dxe:ASPxGridView ID="gridtabusraddress" KeyFieldName="Id" runat="Server" ClientInstanceName="grid"  SettingsBehavior-AllowFocusedRow="true">
                         <SettingsSearchPanel Visible="True" />
                        <Settings ShowGroupPanel="True" ShowFilterRow="true" ShowFilterRowMenu ="true" />
                        <Columns>
                            <dxe:GridViewDataTextColumn Caption="Employee Code" FieldName="Empcode" VisibleIndex="0">
                            </dxe:GridViewDataTextColumn>

                             <dxe:GridViewDataTextColumn Caption="Employee Name" FieldName="Empname" VisibleIndex="1">
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn Caption="User" FieldName="UserId" VisibleIndex="2">
                            </dxe:GridViewDataTextColumn>
                            
                            <dxe:GridViewDataTextColumn Caption="Designation" FieldName="deg_designation" VisibleIndex="3">
                            </dxe:GridViewDataTextColumn>
                            
                            <dxe:GridViewDataTextColumn Caption="State" FieldName="StateName" VisibleIndex="4">
                            </dxe:GridViewDataTextColumn>

                               <dxe:GridViewDataTextColumn Caption="Address" FieldName="Address" VisibleIndex="5">
                            </dxe:GridViewDataTextColumn>
                              <dxe:GridViewDataTextColumn Caption="Latitude" FieldName="Latitude" VisibleIndex="6">
                            </dxe:GridViewDataTextColumn>
                              <dxe:GridViewDataTextColumn Caption="Longitude" FieldName="longitude" VisibleIndex="7">
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn  VisibleIndex="8" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" Width="5%">
                                <Settings AllowAutoFilter="False"></Settings>
                                <DataItemTemplate>
                                      <% if (rights.CanEdit)
                                        { %>
                                    <a href="javascript:void(0);" onclick="EditBranch('<%# Container.KeyValue %>')" title="Status"><img src="../../../assests/images/Edit.png" />                                                              
                                    </a> <% } %>


                                         <% if (rights.CanDelete)
                                        { %>
                                    <a href="javascript:void(0);" onclick="DeleteBranch('<%# Container.KeyValue %>')" title="Status"><img src="../../../assests/images/delete.png" />                                                              
                                    </a> <% } %>



                                </DataItemTemplate>
                                <EditFormSettings Visible="False" />
                                <CellStyle Wrap="False">
                                </CellStyle>
                                <HeaderStyle Wrap="False" />
                            </dxe:GridViewDataTextColumn>
                        </Columns>
                        <SettingsSearchPanel Visible="True" />
                        <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                        <SettingsBehavior AllowFocusedRow="true" ColumnResizeMode="NextColumn" />
                      
                    </dxe:ASPxGridView>
                </td>
            </tr>

        </table>
    </div>
        </div>
    </div>
      <dxe:ASPxGridViewExporter ID="exporter" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
        </dxe:ASPxGridViewExporter>
</asp:Content>

