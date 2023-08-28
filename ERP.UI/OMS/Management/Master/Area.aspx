<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                08-02-2023        2.0.39           Pallab              25656 : Master module design modification 
2.0                27-06-2023        2.0.41           Pallab              26444: Area module responsive issue fix and make mobile friendly
====================================================== Revision History ==========================================================--%>

<%@ Page Title="Area" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" Inherits="ERP.OMS.Management.Master.management_master_Area" CodeBehind="Area.aspx.cs" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<%--<%@ Register Assembly="DevExpress.Web.v10.2.Export, Version=10.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Export" TagPrefix="dxe" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe000001" %>
<%@ Register assembly="DevExpress.Web.v10.2" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2" namespace="DevExpress.Web" tagprefix="dx" %>--%>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- 
    --%>
    <style>
        .dxgvControl_PlasticBlue a {
            color: #5A83D0;
        }

    </style>

    <script type="text/javascript">
        //function is called on changing country
        //function OnCountryChanged(cmbCountry) {
        //    grid.GetEditor("cou_country").PerformCallback(cmbCountry.GetValue().toString());
        //}
        //Rev Rajdip
        function fn_import() {
            //grid.Refresh();
            popup_excelupload.Show();
        }
        function downloadformat()
        { }
        function uploadformat()
        { }
        function hideimportpopup() {
            jAlert("Area Imported Succesfully!", 'Alert Dialog: [Area Import]', function (r) {
                if (r == true) {
                    popup_excelupload.Hide();
                }
            });

        }
        function hideimportfailurepopup() {
            jAlert("Please Select Valid data", 'Alert Dialog: [Area Import]', function (r) {
                if (r == true) {
                    popup_excelupload.Hide();
                }
            });

        }
        //End Rev Rajdip
        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        function LastCall(obj) {
            if (grid.cpErrorMsg) {
                if (grid.cpErrorMsg.trim != "") {
                    jAlert(grid.cpErrorMsg);
                    grid.cpErrorMsg = '';
                    return;
                }
            }
            if (grid.cpDelmsg != null) {
                if (grid.cpDelmsg.trim() != '') {
                    jAlert(grid.cpDelmsg);
                    grid.cpDelmsg = '';

                }
            }


        }
        function DeleteRow(keyValue) {

            jConfirm('Confirm delete?', 'Confirmation Dialog', function (r) {
                if (r == true) {
                    grid.PerformCallback('Delete~' + keyValue);
                }
            });
        }
        function openviewlog() {
            
            cLogPopUp.Show();
        }
        function updateGridByDate() {
            //debugger;
            if (cFormDate.GetDate() == null) {
                jAlert('Please select from date.', 'Alert', function () { cFormDate.Focus(); });
            }
            else if (ctoDate.GetDate() == null) {
                jAlert('Please select to date.', 'Alert', function () { ctoDate.Focus(); });
            }
            else {
                localStorage.setItem("FromDateSalesOrder", cFormDate.GetDate().format('yyyy-MM-dd'));
                localStorage.setItem("ToDateSalesOrder", ctoDate.GetDate().format('yyyy-MM-dd'));
                $("#hfFromDate").val(cFormDate.GetDate().format('yyyy-MM-dd'));
                $("#hfToDate").val(ctoDate.GetDate().format('yyyy-MM-dd'));

                cgridlog.Refresh();


            }
        }
    </script>

    <style>
        .btn-pd {
            /*Rev 1.0*/
            /*padding: 5px;*/
            padding: 7px;
            /*Rev end 1.0*/
            margin-right: 1px;
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

    .btn-sm, .btn-xs
    {
        padding: 6px 10px !important;
    }
    .btn
    {
        height: 34px;
    }
/*Rev end 1.0*/
/*Rev 2.0*/
    @media only screen and (max-width: 768px)
    {
        .breadCumb {
            padding: 0 32%;
        }
    }
    /*Rev end 2.0*/

    
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--rev end 25249--%>
    <%--<div class="panel-heading">
        <div class="panel-title">
            <h3>Area</h3>
        </div>
    </div>--%>
    <div class="breadCumb">
        <span>Area</span>
       
    </div>
    <%--rev end 25249--%>
    <div class="container">
        <div class="backBox mt-5 p-3 ">
    <div class="form_main">
        <table class="TableMain100">
            <%--            <tr>
                <td class="EHEADER" style="text-align: center;">
                    <strong><span style="color: #000099">Area List</span></strong>
                </td>
            </tr>--%>
            <tr>
                <td>

                    <table width="100%">
                        <tr>
                            <td style="text-align: left; vertical-align: top">
                                <table style="margin-bottom: 15px !important;">
                                    <tr>
                                        <td id="ShowFilter">
                                             <%if (rights.CanAdd)
                                              { %>
                                            <a href="javascript:void(0);" onclick="grid.AddNewRow()" class="btn btn-success">
                                                <span>Add New</span>
                                            </a>
                                              <% } %>
                                            <%--<a href="javascript:ShowHideFilter('s');" class="btn btn-primary"><span>Show Filter</span></a>--%>
                                            <% if (rights.CanExport)
                                               { %>
                                             <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-primary btn-pd" OnChange="if(!AvailableExportOption()){return false;}" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true"  >
                                                <asp:ListItem Value="0">Export to</asp:ListItem>
                                                <asp:ListItem Value="1">PDF</asp:ListItem>
                                                 <asp:ListItem Value="2">XLS</asp:ListItem>
                                                 <asp:ListItem Value="3">RTF</asp:ListItem>
                                                 <asp:ListItem Value="4">CSV</asp:ListItem>
                                            </asp:DropDownList>
                                             <% } %>
                                                  <%if (rights.CanAdd)
                                              { %>
                                           <asp:Button ID="btndownloadexcel"  OnClick="btndownload_click"   CssClass="btn btn-info btn-radius" runat="server" Text="Download" /><%--OnClientClick="btndownload_clientclick()"--%>
                                               <% } %>
                                                   <%if (rights.CanAdd)
                                              { %>
                                                <input type="button" value="Import(Add/Update)" onclick="fn_import()" class="btn btn-danger btn-radius"  />
                                            <% } %>
                                                <%if (rights.CanAdd)
                                              { %>
                                                  <input type="button" value="View Log" class="btn btn-warning  typeNotificationBtn btn-radius"  onclick="openviewlog()" />
                                            <% } %>

                                           
                                        </td>
                                        <td id="Td1">
                                           <%-- <a href="javascript:ShowHideFilter('All');" class="btn btn-primary"><span>All Records</span></a>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                           
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <%--OnCommandButtonInitialize="AreaGrid_CommandButtonInitialize"--%>
                <td>
                    <dxe:ASPxGridView ID="AreaGrid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
                        DataSourceID="insertupdate" KeyFieldName="areaid" Width="100%"  OnInitNewRow="AreaGrid_InitNewRow"
                        OnHtmlEditFormCreated="AreaGrid_HtmlEditFormCreated" OnStartRowEditing="AreaGrid_StartRowEditing"
                        OnHtmlRowCreated="AreaGrid_HtmlRowCreated" OnRowValidating="AreaGrid_RowValidating" 
                        OnCustomCallback="AreaGrid_CustomCallback"  OnCommandButtonInitialize="AreaGrid_CommandButtonInitialize" SettingsBehavior-AllowFocusedRow="true">
                            <clientsideevents endcallback="function(s, e) {
	LastCall(s.cpHeight);
}" />
                        <Columns>
                            <dxe:GridViewDataTextColumn FieldName="areaid" ReadOnly="True" Visible="False"
                                VisibleIndex="0">
                                <EditFormSettings Visible="False" />
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="Area Name" FieldName="name" VisibleIndex="0" Width="30%">
                                <EditFormSettings Visible="True" Caption="Area Name" />
                                <PropertiesTextEdit MaxLength="50">
                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right">
                                        <RequiredField IsRequired="True" ErrorText="Mandatory" />

                                    </ValidationSettings>
                                </PropertiesTextEdit>
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataTextColumn>
                           
                            <dxe:GridViewDataComboBoxColumn FieldName="SId" Visible="False" VisibleIndex="1">
                                <PropertiesComboBox DataSourceID="SelectState" ValueType="System.String" ValueField="city_id" TextField="city_name" EnableIncrementalFiltering="True" EnableSynchronization="False">
                                    <%--<ClientSideEvents SelectedIndexChanged="function(s,e){OnCountryChanged(s);}" />--%>
                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right">
                                        <RequiredField IsRequired="True" ErrorText="Mandatory" />

                                    </ValidationSettings>
                                </PropertiesComboBox>
                                <EditFormSettings Visible="True" Caption="City Name" />
                                <EditFormCaptionStyle HorizontalAlign="Left" Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataComboBoxColumn>
                            <dxe:GridViewDataTextColumn Caption="City Name" FieldName="city" VisibleIndex="1" Width="30%">
                                <EditFormSettings Visible="False" Caption="City Name" />
                                <EditFormCaptionStyle Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataTextColumn>
                             <%--added by debjyoti 30-11-2016--%>
                            <%--<dxe:GridViewDataTextColumn Caption="Pin Code" FieldName="pin" VisibleIndex="2" Width="30%">
                                <EditFormSettings Visible="True" Caption="Pin Code" />
                                <PropertiesTextEdit MaxLength="10">
                                     </PropertiesTextEdit>
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataTextColumn>--%>
                            <%--End by debjyoti 30-11-2016--%>

                            <dxe:GridViewCommandColumn VisibleIndex="2" ShowDeleteButton="false" ShowEditButton="true" ShowClearFilterButton="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="6%">

                                <%--<EditButton Visible="True">
                            </EditButton>
                            <DeleteButton Visible="True">
                            </DeleteButton>
                            <ClearFilterButton Visible="True"></ClearFilterButton>--%>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <span>Actions</span>
                                </HeaderTemplate>
                                 <CustomButtons>
                                    <dxe:GridViewCommandColumnCustomButton Image-Url="../../../assests/images/Delete.png" Image-ToolTip="Delete">
                                       
                                    </dxe:GridViewCommandColumnCustomButton>

                                </CustomButtons>
                            </dxe:GridViewCommandColumn>

                        </Columns>
                         <ClientSideEvents CustomButtonClick="function(s, e) {
                             var key = s.GetRowKey(e.visibleIndex);
                             DeleteRow(key);
                            
                            }" />
                        <SettingsCommandButton>

                            <EditButton Image-Url="../../../assests/images/Edit.png" ButtonType="Image" Image-AlternateText="Edit" Styles-Style-CssClass="pad">
                            </EditButton>
                            <DeleteButton Image-Url="../../../assests/images/Delete.png" ButtonType="Image" Image-AlternateText="Delete">
                            </DeleteButton>
                            <UpdateButton Text="Save" ButtonType="Button" Styles-Style-CssClass="btn btn-primary"></UpdateButton>
                            <CancelButton Text="Cancel" ButtonType="Button" Styles-Style-CssClass="btn btn-danger"></CancelButton>
                            <ClearFilterButton Text="Clear Filter"></ClearFilterButton>
                        </SettingsCommandButton>
                        <SettingsSearchPanel Visible="True" />
                       <Settings ShowGroupPanel="True" ShowStatusBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="true" />
                        <SettingsEditing Mode="PopupEditForm" PopupEditFormHorizontalAlign="Center"
                            PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" PopupEditFormWidth="400px" EditFormColumnCount="1" />
                        <%-- <Styles>
                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                            </Header>
                            <LoadingPanel ImageSpacing="10px">
                            </LoadingPanel>
                        </Styles>--%>
                        <SettingsText PopupEditFormCaption="Add/Modify Area" ConfirmDelete="Confirm delete?" />
                        <%--<SettingsPager NumericButtonCount="20" PageSize="20" ShowSeparators="True" AlwaysShowPager="True">
                            <FirstPageButton Visible="True">
                            </FirstPageButton>
                            <LastPageButton Visible="True">
                            </LastPageButton>
                        </SettingsPager>--%>
                        <SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" />
                        <Templates>
                            <EditForm>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 5%"></td>
                                        <td style="width: 90%">

                                            <dxe:ASPxGridViewTemplateReplacement runat="server" ReplacementType="EditFormEditors" ColumnID="" ID="Editors"></dxe:ASPxGridViewTemplateReplacement>

                                            <div style="padding: 2px 2px 2px 92px">
                                                <dxe:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                                <dxe:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                            </div>
                                        </td>
                                        <td style="width: 5%"></td>
                                    </tr>
                                </table>
                            </EditForm>

                        </Templates>

                    </dxe:ASPxGridView>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="insertupdate" runat="server" ConflictDetection="CompareAllValues"
            DeleteCommand="DELETE FROM [tbl_master_area] WHERE [area_id] = @original_areaid"
            InsertCommand="INSERT INTO [tbl_master_area] ([area_name], [city_id], [CreateDate], [CreateUser] ) VALUES (@name, @SId, getdate(), @CreateUser )"
            OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT a.area_id AS areaid, a.area_name AS name, s.city_name AS city, s.city_id AS SId  FROM tbl_master_area AS a INNER JOIN tbl_master_city AS s ON a.city_id = s.city_id"
            UpdateCommand="UPDATE [tbl_master_area] SET [area_name] = @name, [city_id] = @SId, [CreateDate] = getdate(), [CreateUser] = @CreateUser  WHERE [area_id] = @areaid">

            <UpdateParameters>
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="SId" Type="Int32" />
                <asp:Parameter Name="CreateDate" Type="String" />
                <asp:SessionParameter Name="CreateUser" SessionField="userid" Type="Decimal" />
                <asp:Parameter Name="areaid" Type="Int32" /> 

            </UpdateParameters>
            <InsertParameters>
                <%--<asp:Parameter Name="area_name" Type="String" />
                <asp:Parameter Name="city_id" Type="Decimal" />
                <asp:Parameter Name="CreateDate" Type="String" />
                <asp:SessionParameter Name="CreateUser" SessionField="userid" Type="Decimal" />--%>

                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="SId" Type="Int32" />
                <asp:Parameter Name="CreateDate" Type="String" />
                <asp:SessionParameter Name="CreateUser" SessionField="userid" Type="Decimal" /> 
            </InsertParameters>
            <DeleteParameters>
                <asp:Parameter Name="original_areaid" Type="Int32" />

            </DeleteParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SelectArea" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="SELECT a.area_id AS areaid, a.area_name AS name, s.city_name AS city, s.city_id AS SId  FROM tbl_master_area AS a INNER JOIN tbl_master_city AS s ON a.city_id = s.city_id"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SelectState" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="SELECT [city_id], [city_name] FROM [tbl_master_city] order by city_name"></asp:SqlDataSource>
        <dxe:ASPxGridViewExporter ID="exporter" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
        </dxe:ASPxGridViewExporter>
    </div>
</div>
</div>
       <dxe:ASPxPopupControl ID="popup_excelupload" runat="server" ClientInstanceName="popup_excelupload"
                    Width="400px" HeaderText="Import Rate" PopupHorizontalAlign="WindowCenter"
                    Height="130px" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" Modal="True">
                    <ContentCollection>
                        <dxe:PopupControlContentControl ID="PopupControlContentControl1" runat="server">                   
                                   <div style="padding-top: 5px;" class="col-md-10 col-md-offset-1" id="divstateunique1">
                            
                                       <br />
                                       <div>
                                      <asp:FileUpload ID="fileprod" CssClass="custom-file-input" runat="server" />  
                                       </div>
                                       <br />
                                       <div>
                                     <asp:Button ID="Button1"  OnClick="btnImport_click" CssClass="btn btn-primary btn-radius" runat="server" Text="Import" />
                                        <%--   OnClientClick="btnsave_clientclick()" --%>
                                       </div>
                                </div>

                             <br style="clear: both;" />
                       
                            
                        </dxe:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle BackColor="LightGray" ForeColor="white" />
                </dxe:ASPxPopupControl>
            <%--Log table--%>
             <dxe:ASPxPopupControl ID="Popup" runat="server" ClientInstanceName="cLogPopUp"
        Width="800px" HeaderText="Select Tax" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
        Modal="True" ContentStyle-VerticalAlign="Top" EnableHierarchyRecreation="True">
        <HeaderTemplate>
            <strong><span style="color: #fff">Area Import Log</span></strong>
            <dxe:ASPxImage ID="ASPxImage3" runat="server" ImageUrl="/assests/images/closePop.png" Cursor="pointer" CssClass="popUpHeader pull-right">
                <ClientSideEvents Click="function(s, e){ 
                                                            cLogPopUp.Hide();
                                                        }" />
            </dxe:ASPxImage>
        </HeaderTemplate>
        <ContentCollection>
            <dxe:PopupControlContentControl runat="server">
                <div style="padding: 7px 0;">
             <%--       <input type="button" value="Select All Products" onclick="ChangeState('SelectAll')" class="btn btn-primary"></input>
                    <input type="button" value="De-select All Products" onclick="ChangeState('UnSelectAll')" class="btn btn-primary"></input>
                    <input type="button" value="Revert" onclick="ChangeState('Revart')" class="btn btn-primary"></input>--%>
                </div>
            <div class="clearfix relative">
                  <div class="stateDiv" style="padding-top: 5px;">
                      <table  class="padTab pull-Left" style="margin-top: 7px"><tr>
                          <td>
                              From
                                 </td>
                          <%--Rev 1.0--%>
                          <%--<td style="width: 150px">--%>
                         <td style="width: 160px;padding-right: 10px; padding-left: 5px" class=" for-cust-icon">
                             <%--Rev end 1.0--%>
                         <dxe:ASPxDateEdit ID="FormDate" runat="server" EditFormat="Custom" EditFormatString="dd-MM-yyyy" ClientInstanceName="cFormDate" Width="100%">
                              <ButtonStyle Width="13px">
                                </ButtonStyle>
                         </dxe:ASPxDateEdit>
                              <%--&nbsp;--%>
                             <%--Rev 1.0--%>
                                    <img src="/assests/images/calendar-icon.png" class="calendar-icon" />
                                    <%--Rev end 1.0--%>
                          </td>
                          <td>
                            TO
                          </td>
                          <%--Rev 1.0--%>
                          <%--<td style="width: 150px">--%>
                         <td style="width: 160px;padding-right: 10px; padding-left: 5px" class=" for-cust-icon">
                             <%--Rev end 1.0--%>
                        <dxe:ASPxDateEdit ID="toDate" runat="server" EditFormat="Custom" EditFormatString="dd-MM-yyyy" ClientInstanceName="ctoDate" Width="100%">
                           <ButtonStyle Width="13px">
                            </ButtonStyle>
                        </dxe:ASPxDateEdit>
                              <%--&nbsp;--%>
                             <%--Rev 1.0--%>
                                    <img src="/assests/images/calendar-icon.png" class="calendar-icon" />
                                    <%--Rev end 1.0--%>
                          </td>
                          <td style="width: 150px">
                              <input type="button" value="Show" class="btn btn-primary"  onclick="updateGridByDate()"/>

                          </td>
                          </tr>
                         </table>
                   </div>
                                    <div style="padding-top: 5px; width: 20%;">
                                       <div> </div>
                               
                                    </div>
                <div>
                 <%--onclick="updateGridByDate()"--%>
                    </div>
                 <div style="padding: 7px 0;">
                 
                </div>
            <dxe:ASPxGridView ID="gridlog" runat="server" KeyFieldName="area_id" AutoGenerateColumns="False"
                        DataSourceID="EntityServerlogModeDataSource" Width="100%" ClientInstanceName="cgridlog" OnCustomCallback="gridlogCustomCallback"

                        SettingsDataSecurity-AllowDelete="false" Settings-HorizontalScrollBarMode="Auto" Settings-VerticalScrollableHeight="280" Settings-VerticalScrollBarMode="Auto" >
                       <%-- <ClientSideEvents EndCallback="function(s,e) { clogendcallback }" />--%>
                        <SettingsSearchPanel Visible="True" Delay="5000" />
                     
                        <SettingsEditing Mode="PopupEditForm" PopupEditFormHorizontalAlign="Center" PopupEditFormModal="True"
                            PopupEditFormVerticalAlign="WindowCenter" PopupEditFormWidth="700px" EditFormColumnCount="3" />

                        <Settings ShowGroupPanel="True" ShowFilterRow="true" ShowFilterRowMenu="true" />
                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" ColumnResizeMode="NextColumn" />
                        <SettingsText PopupEditFormCaption="Import Log" />
                        <Columns>
      <%--                  <dxe:GridViewDataTextColumn Caption="SrlNo"  FieldName="SrlNo"   VisibleIndex="1" Width="0">
                                                        <PropertiesTextEdit>
                                                            <ValidationSettings EnableCustomValidation="false" ErrorDisplayMode="None" ValidateOnLeave="false" RequiredField-IsRequired="false"></ValidationSettings>
                                                        </PropertiesTextEdit>
                                                    </dxe:GridViewDataTextColumn>--%>
                                                   <dxe:GridViewDataTextColumn ReadOnly="true" CellStyle-Wrap="False" PropertiesTextEdit-ValidationSettings-ErrorFrameStyle-Wrap="False" EditFormCaptionStyle-Wrap="False" Caption="Area Name"  FieldName="area_name"  VisibleIndex="1" Width="150">
                                                        <PropertiesTextEdit>
                                                             <ValidationSettings EnableCustomValidation="false" ErrorDisplayMode="None" ValidateOnLeave="false" RequiredField-IsRequired="false"></ValidationSettings>                                                   
                                                               </PropertiesTextEdit>
                                                        <CellStyle Wrap="False"></CellStyle>  
                                                    </dxe:GridViewDataTextColumn>
                                       <dxe:GridViewDataTextColumn ReadOnly="true" CellStyle-Wrap="False" PropertiesTextEdit-ValidationSettings-ErrorFrameStyle-Wrap="False" EditFormCaptionStyle-Wrap="False" Caption="City Name"  FieldName="city_name"  VisibleIndex="2" Width="150">
                                                        <PropertiesTextEdit>
                                                             <ValidationSettings EnableCustomValidation="false" ErrorDisplayMode="None" ValidateOnLeave="false" RequiredField-IsRequired="false"></ValidationSettings>                                                   
                                                               </PropertiesTextEdit>
                                                        <CellStyle Wrap="False"></CellStyle>  
                                                    </dxe:GridViewDataTextColumn>
                              <dxe:GridViewDataTextColumn Caption="Action" CellStyle-HorizontalAlign="Left" FieldName="Action"   VisibleIndex="3" Width="100">
                                                        <PropertiesTextEdit DisplayFormatString="0.00" Style-HorizontalAlign="Right">
                                                       <MaskSettings AllowMouseWheel="False" Mask="&lt;0..999999999&gt;.&lt;00..999&gt;" />
                                                                <ValidationSettings EnableCustomValidation="false" ErrorDisplayMode="None" ValidateOnLeave="false" RequiredField-IsRequired="false"></ValidationSettings>                                                
                                                            <Style HorizontalAlign="Right">
                                                            </Style>
                                                        </PropertiesTextEdit>
                                                        
                                                        <CellStyle CssClass="gridcellleft" Wrap="true">
                                                        </CellStyle>
                                                    </dxe:GridViewDataTextColumn>
                               <dxe:GridViewDataTextColumn Caption="Status" CellStyle-HorizontalAlign="Left" FieldName="Status"   VisibleIndex="4" Width="100">
                                                        <PropertiesTextEdit DisplayFormatString="0.00" Style-HorizontalAlign="Right">
                                                       <MaskSettings AllowMouseWheel="False" Mask="&lt;0..999999999&gt;.&lt;00..999&gt;" />
                                                                <ValidationSettings EnableCustomValidation="false" ErrorDisplayMode="None" ValidateOnLeave="false" RequiredField-IsRequired="false"></ValidationSettings>                                                
                                                            <Style HorizontalAlign="Right">
                                                            </Style>
                                                        </PropertiesTextEdit>
                                                        
                                                       <CellStyle CssClass="gridcellleft" Wrap="true">
                                                        </CellStyle>
                                                    </dxe:GridViewDataTextColumn>
                             <dxe:GridViewDataTextColumn Caption="Remarks" CellStyle-HorizontalAlign="Left" FieldName="Remarks"   VisibleIndex="5" Width="100">
                                                        <PropertiesTextEdit DisplayFormatString="0.00" Style-HorizontalAlign="Right">
                                                       <MaskSettings AllowMouseWheel="False" Mask="&lt;0..999999999&gt;.&lt;00..999&gt;" />
                                                                <ValidationSettings EnableCustomValidation="false" ErrorDisplayMode="None" ValidateOnLeave="false" RequiredField-IsRequired="false"></ValidationSettings>                                                
                                                            <Style HorizontalAlign="Right">
                                                            </Style>
                                                        </PropertiesTextEdit>
                                                        
                                                        <CellStyle CssClass="gridcellleft" Wrap="true">
                                                        </CellStyle>
                                                    </dxe:GridViewDataTextColumn>
                             <dxe:GridViewDataTextColumn Caption="Created By"  FieldName="createdbyuser"
                                             VisibleIndex="6" Width="100px" Settings-ShowFilterRowMenu="True" Settings-AllowAutoFilter="True">
                                             <CellStyle CssClass="gridcellleft" Wrap="true">
                                             </CellStyle>
                                             <Settings AllowAutoFilterTextInputTimer="False" />
                                             <Settings AutoFilterCondition="Contains" />
                                               </dxe:GridViewDataTextColumn>
                                         <dxe:GridViewDataTextColumn Caption="Created On" FieldName="CreateDate" SortOrder="Descending"
                                           VisibleIndex="7" Width="150px">
                                           <CellStyle CssClass="gridcellleft" Wrap="true">
                                           </CellStyle>
                                            <PropertiesTextEdit DisplayFormatString="dd-MM-yyyy"></PropertiesTextEdit>
                                           <Settings AllowAutoFilterTextInputTimer="False" />
                                           <Settings AutoFilterCondition="Contains" />
                                       </dxe:GridViewDataTextColumn>
                             <dxe:GridViewDataTextColumn Caption="Modified By"  FieldName="modifiedbyuser"
                                             VisibleIndex="8" Width="100px" Settings-ShowFilterRowMenu="True" Settings-AllowAutoFilter="True">
                                             <CellStyle CssClass="gridcellleft" Wrap="true">
                                             </CellStyle>
                                             <Settings AllowAutoFilterTextInputTimer="False" />
                                             <Settings AutoFilterCondition="Contains" />
                                               </dxe:GridViewDataTextColumn>
                                         <dxe:GridViewDataTextColumn Caption="Modified On" FieldName="LastModifyDate" SortOrder="Descending"
                                           VisibleIndex="9" Width="150px">
                                           <CellStyle CssClass="gridcellleft" Wrap="true">
                                           </CellStyle>
                                            <PropertiesTextEdit DisplayFormatString="dd-MM-yyyy"></PropertiesTextEdit>
                                           <Settings AllowAutoFilterTextInputTimer="False" />
                                           <Settings AutoFilterCondition="Contains" />
                                       </dxe:GridViewDataTextColumn>
                                      
                        </Columns>
                 
                        <SettingsContextMenu Enabled="true"></SettingsContextMenu>
                        <SettingsPager PageSize="10">
                            <PageSizeItemSettings Visible="true" ShowAllItem="false" Items="10,50,100,150,200" />
                        </SettingsPager>
                       <%-- <ClientSideEvents RowClick="gridRowclick" />--%>
                        <Styles>
                            <LoadingPanel ImageSpacing="10px">
                            </LoadingPanel>
                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                            </Header>
                        </Styles>
                    </dxe:ASPxGridView>
        </div>

                <dx:linqservermodedatasource id="EntityServerlogModeDataSource" runat="server" onselecting="EntityServerModelogDataSource_Selecting"
                    contexttypename="ERPDataClassesDataContext" tablename="v_GetAreaImportLog" />

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand=""></asp:SqlDataSource>
                <br />
                <div class="text-center">
                 <%--   <asp:Button ID="Button3" runat="server" Text="OK" CssClass="btn btn-primary  mLeft mTop" UseSubmitBehavior="false" />--%>
                        <%--<asp:Button ID="Button2"  OnClick="btndownload_click" OnClientClick="btndownload_clientclick()"  CssClass="btn btn-primary btn-radius" runat="server" Text="Download" />--%>
                </div>
                <div style="display: none">
        <dxe:ASPxGridViewExporter ID="ASPxGridViewExporter1" GridViewID="gridlog" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
        </dxe:ASPxGridViewExporter>
    </div>
            </dxe:PopupControlContentControl>

        </ContentCollection>
        <ContentStyle VerticalAlign="Top" CssClass="pad"></ContentStyle>
        <HeaderStyle BackColor="LightGray" ForeColor="Black" />
    </dxe:ASPxPopupControl>
        <%-- End Import log Popup --%>
     <asp:HiddenField ID="hfFromDate" runat="server" />
        <asp:HiddenField ID="hfToDate" runat="server" />
</asp:Content>
