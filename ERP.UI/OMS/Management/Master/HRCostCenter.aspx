<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                08-02-2023        2.0.39           Pallab              25656 : Master module design modification 
2.0                27-06-2023        2.0.41           Pallab              26443: Cost Centers/Departments module responsive issue fix and make mobile friendly
====================================================== Revision History ==========================================================--%>

<%@ Page Title="Cost Centers/Departments" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master"
    Inherits="ERP.OMS.Management.Master.management_master_HRCostCenter" CodeBehind="HRCostCenter.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    


    <script language="javascript" type="text/javascript">
        //function SignOff() {
        //    window.parent.SignOff();
        //}
        //function height() {
        //    if (document.body.scrollHeight >= 500)
        //        window.frameElement.height = document.body.scrollHeight;
        //    else
        //        window.frameElement.height = '500px';
        //    window.frameElement.Width = document.body.scrollWidth;
        //}
        function ClickOnMoreInfo(keyValue) {
            var url = 'HRCost.aspx?id=' + keyValue;
            //OnMoreInfoClick(url, "Modify Cost Center", '940px', '450px', "Y");
            window.location.href = url;

        }
        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        function AddNewClick() {
            var url = "HRAddNewCostDept.aspx"
            //OnMoreInfoClick(url, "Modify Cost Center", '940px', '450px', "Y");
            window.location.href = url;
        }
    </script>

    <style>
        .btn-pd {
            /*padding: 7px;*/
            padding: 6px 10px !important;
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
            top: 37px;
            right: 12px;
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

    .btn
    {
        height: 34px;
    }

    /*Rev end 1.0*/

    /*Rev 2.0*/

    @media only screen and (max-width: 768px)
    {
        .breadCumb {
            padding: 0 25px;
        }

        .breadCumb > span
        {
            padding: 9px 8px;
        }
    }
    /*Rev end 2.0*/

    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--rev 25249--%>
    <%--<div class="panel-heading">
        <div class="panel-title">
            <h3>Cost Centers/Departments</h3>
        </div>
    </div>--%>
    <div class="breadCumb">
            <span>Cost Centers/Departments</span>
        </div>
    <%--rev end 25249--%>
    <div class="container">
        <div class="backBox mt-5 p-3 ">
    <div class="form_main">
        <table class="TableMain100" width="100%">
            <%--<tr>
                <td class="EHEADER" style="text-align: center" colspan="2">
                    <strong><span style="color: #000099">Cost Centers/Departments</span></strong>
                </td>
            </tr>--%>
            <tr>
                <td style="text-align: left; vertical-align: top; width: 536px;">
                    <table style="margin-bottom: 15px !important;">
                        <tr>
                            <td id="ShowFilter">
                                <%-- <%if (Session["PageAccess"].ToString().Trim() == "All" || Session["PageAccess"].ToString().Trim() == "Add" || Session["PageAccess"].ToString().Trim() == "DelAdd")
                                  { %>--%>
                                <% if (rights.CanAdd)
                                   { %>
                                <a href="javascript:void(0);" onclick="AddNewClick();" class="btn btn-success"><span>Add New</span> </a>
                                <%} %>
                                <%-- <a href="javascript:ShowHideFilter('s');" class="btn btn-primary"><span>Show Filter</span></a>--%>
                              <% if (rights.CanExport)
                                               { %>
                                  <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-primary btn-pd" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true"  >
                                    <asp:ListItem Value="0">Export to</asp:ListItem>
                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                        <asp:ListItem Value="2">XLS</asp:ListItem>
                                        <asp:ListItem Value="3">RTF</asp:ListItem>
                                        <asp:ListItem Value="4">CSV</asp:ListItem>
                                </asp:DropDownList>
                                <% } %>
                            </td>
                            <td id="Td1">
                                <%--<a href="javascript:ShowHideFilter('All');" class="btn btn-primary"><span>All Records</span></a>--%> <%--ag18102016 ---0011370--%>
                            </td>
                        </tr>
                    </table>
                </td>
                <%--<td class="gridcellright" style="float: right">
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
                        <Border BorderColor="black" />
                        <DropDownButton Text="Export">
                        </DropDownButton>
                    </dxe:ASPxComboBox>
                </td>--%>
            </tr>
            <tr>
                <%--ag18102016 ---0011370--%>
                <td colspan="2">
                    <dxe:ASPxGridView ID="CostDepartmentGrid" runat="server" ClientInstanceName="grid"
                        AutoGenerateColumns="False" DataSourceID="DepartmentSource" KeyFieldName="cost_id"
                        Width="100%" OnCustomCallback="CostDepartmentGrid_CustomCallback" OnRowUpdated="CostDepartmentGrid_RowUpdated" OnRowCommand="CostDepartmentGrid_RowCommand" SettingsBehavior-AllowFixedGroups="true">
                        <Columns>
                            <dxe:GridViewDataTextColumn FieldName="cost_id" ReadOnly="True" VisibleIndex="0"
                                Visible="False">
                                <EditFormSettings Visible="False" />
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataComboBoxColumn Caption="Cost Center/Department Type" FieldName="cost_costCenterType"
                                VisibleIndex="1" Width="40%">
                                <PropertiesComboBox ValueType="System.String" EnableIncrementalFiltering="True">
                                    <Items>
                                        <dxe:ListEditItem Text="Department" Value="Department"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Employee" Value="Employee"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Branch" Value="Branch"></dxe:ListEditItem>
                                    </Items>
                                </PropertiesComboBox>
                                <EditFormSettings Visible="True" />
                                <CellStyle CssClass="gridcellleft" HorizontalAlign="Left">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right" Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataComboBoxColumn>
                            <dxe:GridViewDataTextColumn Caption="Cost Centers/Departments" FieldName="cost_description" VisibleIndex="0"
                                Width="40%">
                                <EditFormSettings Visible="True" />
                                <CellStyle CssClass="gridcellleft" HorizontalAlign="Left">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right" Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataComboBoxColumn FieldName="cost_costCenterHead" Visible="False"
                                VisibleIndex="2">
                                <PropertiesComboBox DataSourceID="HeadSource" TextField="cnt_firstName" ValueField="cnt_internalId"
                                    ValueType="System.String" EnableIncrementalFiltering="True">
                                </PropertiesComboBox>
                                <EditFormSettings Visible="True" Caption="Head Of department" />
                                <EditFormCaptionStyle HorizontalAlign="Right" Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataComboBoxColumn>
                            <dxe:GridViewDataComboBoxColumn FieldName="cost_principlalDepartment" Visible="False"
                                VisibleIndex="3">
                                <PropertiesComboBox DataSourceID="ParentSource" TextField="cost_description" ValueField="cost_id"
                                    ValueType="System.String" EnableIncrementalFiltering="True">
                                </PropertiesComboBox>
                                <EditFormSettings Visible="True" Caption="Parent Cost  Center/Dept." />
                                <EditFormCaptionStyle HorizontalAlign="Right" Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataComboBoxColumn>
                            <dxe:GridViewDataCheckColumn FieldName="cost_operationIn" Visible="False" VisibleIndex="5">
                                <EditFormSettings Visible="True" Caption="Mutual Fund" />
                                <EditFormCaptionStyle HorizontalAlign="Right" Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataCheckColumn>
                            <dxe:GridViewDataCheckColumn FieldName="cost_operationIn1" Visible="False" VisibleIndex="6">
                                <EditFormSettings Visible="True" Caption="Broking" />
                                <EditFormCaptionStyle HorizontalAlign="Right" Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataCheckColumn>
                            <dxe:GridViewDataCheckColumn FieldName="cost_operationIn2" Visible="False" VisibleIndex="7">
                                <EditFormSettings Visible="True" Caption="Insurance" />
                                <EditFormCaptionStyle HorizontalAlign="Right" Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataCheckColumn>
                            <dxe:GridViewDataCheckColumn FieldName="cost_operationIn3" Visible="False" VisibleIndex="8">
                                <EditFormSettings Visible="True" Caption="Depository" />
                                <EditFormCaptionStyle HorizontalAlign="Right" Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataCheckColumn>
                            <dxe:GridViewDataTextColumn FieldName="cost_email" Visible="False" VisibleIndex="4">
                                <EditFormSettings Visible="True" Caption="Email-Id" />
                                <EditFormCaptionStyle HorizontalAlign="Right" Wrap="False">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn Caption="Details" VisibleIndex="2" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="10%">
                                <DataItemTemplate>
                                    <% if (rights.CanEdit)
                                               { %>
                                    <a href="javascript:void(0);" onclick="ClickOnMoreInfo('<%# Container.KeyValue %>')" title="More Info" class="pad" style="text-decoration: none;">
                                        <img src="../../../assests/images/info.png" />
                                    </a>
                                     <% } %>
                                      <% if (rights.CanDelete)
                                               { %>
                                    <asp:LinkButton ID="btn_delete" runat="server" OnClientClick="return confirm('Confirm Delete?');" CommandArgument='<%# Container.KeyValue %>' CommandName="delete" ToolTip="Delete" Font-Underline="false">
                                        <img src="../../../assests/images/Delete.png" />
                                    </asp:LinkButton>
                                     <% } %>
                                </DataItemTemplate>
                                <EditFormSettings Visible="False" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <span>Actions</span>
                                </HeaderTemplate>
                            </dxe:GridViewDataTextColumn>
                            <%--<dxe:GridViewCommandColumn VisibleIndex="3" ShowDeleteButton="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                <HeaderTemplate>
                                    <span>Actions</span>
                                </HeaderTemplate>
                            </dxe:GridViewCommandColumn>--%>
                        </Columns>
                        <Templates>
                            <EditForm>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%"></td>
                                        <td style="width: 50%">

                                            <dxe:ASPxGridViewTemplateReplacement runat="server" ReplacementType="EditFormEditors" ColumnID="" ID="Editors"></dxe:ASPxGridViewTemplateReplacement>

                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                <dxe:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                    runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                                <dxe:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                    runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                            </div>
                                        </td>
                                        <td style="width: 25%"></td>
                                    </tr>
                                </table>
                            </EditForm>
                        </Templates>
                        <%--<Styles>
                            <LoadingPanel ImageSpacing="10px">
                            </LoadingPanel>
                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                            </Header>
                        </Styles>--%>
                        <SettingsSearchPanel Visible="True" />
                        <Settings ShowTitlePanel="false" ShowFooter="false" ShowStatusBar="Hidden" ShowFilterRow="true" ShowGroupPanel="true" ShowFilterRowMenu ="true" />
                        <%--<SettingsCommandButton>
                            <EditButton Text="Edit"></EditButton>
                            <DeleteButton ButtonType="Image" Image-Url="../../../assests/images/Delete.png"></DeleteButton>

                        </SettingsCommandButton>--%>
                        <SettingsText PopupEditFormCaption="Add/ Modify CostCenter" />
                        <SettingsEditing Mode="PopupEditForm" PopupEditFormHorizontalAlign="Center" PopupEditFormModal="True"
                            PopupEditFormVerticalAlign="TopSides" PopupEditFormWidth="900px" EditFormColumnCount="1" />
                        <SettingsBehavior AllowFocusedRow="false" ColumnResizeMode="NextColumn" ConfirmDelete="True" />
                        <%--<SettingsPager AlwaysShowPager="True" PageSize="20" ShowSeparators="True">
                            <FirstPageButton Visible="True">
                            </FirstPageButton>
                            <LastPageButton Visible="True">
                            </LastPageButton>
                        </SettingsPager>--%>
                    </dxe:ASPxGridView>
                </td>
            </tr>
        </table>
        <dxe:ASPxGridViewExporter ID="exporter" runat="server">
        </dxe:ASPxGridViewExporter>
        <asp:SqlDataSource ID="DepartmentSource" runat="server"
            SelectCommand="HrCostCenterSelect" DeleteCommand="delete from tbl_master_costCenter where cost_id=@cost_id"
            SelectCommandType="StoredProcedure" InsertCommand="HrCostCenterInsert" InsertCommandType="StoredProcedure"
            UpdateCommand="HrCostCenterUpdate" UpdateCommandType="StoredProcedure">
            <InsertParameters>
                <asp:Parameter Name="cost_costCenterType" Type="String" />
                <asp:Parameter Name="cost_description" Type="String" />
                <asp:Parameter Name="cost_costCenterHead" Type="String" />
                <asp:Parameter Name="cost_principlalDepartment" Type="String" />
                <asp:Parameter Name="cost_operationIn" Type="String" />
                <asp:Parameter Name="cost_operationIn1" Type="String" />
                <asp:Parameter Name="cost_operationIn2" Type="String" />
                <asp:Parameter Name="cost_operationIn3" Type="String" />
                <asp:Parameter Name="cost_email" Type="String" />
                <asp:SessionParameter Name="CreateUser" SessionField="userid" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="cost_id" Type="String" />
                <asp:Parameter Name="cost_costCenterType" Type="String" />
                <asp:Parameter Name="cost_description" Type="String" />
                <asp:Parameter Name="cost_costCenterHead" Type="String" />
                <asp:Parameter Name="cost_principlalDepartment" Type="String" />
                <asp:Parameter Name="cost_operationIn" Type="String" />
                <asp:Parameter Name="cost_operationIn1" Type="String" />
                <asp:Parameter Name="cost_operationIn2" Type="String" />
                <asp:Parameter Name="cost_operationIn3" Type="String" />
                <asp:Parameter Name="cost_email" Type="String" />
                <asp:SessionParameter Name="CreateUser" SessionField="userid" Type="String" />
            </UpdateParameters>
            <DeleteParameters>
                <asp:Parameter Name="cost_id" Type="String" />
            </DeleteParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="ParentSource" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="select cost_description,cost_id from tbl_master_costCenter where cost_costCenterType='Department' order by cost_description"></asp:SqlDataSource>
        <asp:SqlDataSource ID="HeadSource" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="select cnt_firstName+' '+isnull(cnt_middleName,'')+' '+isnull(cnt_lastName,'')+' ['+isnull(rtrim(cnt_shortName),'')+']' as cnt_firstName, cnt_internalId from tbl_master_contact where cnt_internalId LIKE '%em_%' order by cnt_firstName"></asp:SqlDataSource>
        <br />
    </div>
   </div>
 </div>
</asp:Content>
