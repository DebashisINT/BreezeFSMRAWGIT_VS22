<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                13-02-2023        2.0.39           Pallab              25656 : Master module design modification 
====================================================== Revision History ==========================================================--%>

<%@ Page Title="Product Class/Category" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master" Inherits="ERP.OMS.Management.Store.Master.management_sProductClass" CodeBehind="sProductClass.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style  type="text/css">
        #marketsGrid_DXMainTable .dx-nowrap, span.dx-nowrap
        {
            white-space:normal !important;
        }

    </style>
    <script type="text/javascript">
      
        //$(document).ready(function () {
        //    document.getElementsByClassName("dxgvPopupEditForm").style.padding = "5px;";
        //});
        //marketsGrid_DXPEForm_tcefnew
        //        function SetDropdownValue() {
        //            document.getElementById('marketsGrid_DXPEForm_efnew_DXEditor4_I').value = '0';
        //        }
        function OnHsnChange(s, e) {
           
        }

        function LastCall() {
            if (grid.cpErrorMsg != null) {
                jAlert(grid.cpErrorMsg);
                grid.cpErrorMsg = null;
            }
        }
        function OnAddBusinessClick(keyValue) {
            var url = '../../master/AssignIndustry.aspx?id1=' + keyValue+'&EntType=productclass' ;
            window.location.href = url;
        }
        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        function UniqueCodeCheck() {
            var proclassid = '0';
            var id = '<%= Convert.ToString(Session["id"]) %>'; 
            //var ProductClassCode = document.getElementById('marketsGrid_DXPEForm_efnew_DXEditor1_I').value;
            var ProductClassCode = grid.GetEditor('ProductClass_Code').GetValue(); 
            if ((id != null) && (id != ''))
            {
                proclassid = id;
               '<%=Session["id"]=null %>'
            }
            
            var CheckUniqueCode = false;

            $.ajax({
                type: "POST",
                url: "sProductClass.aspx/CheckUniqueCode",
                //data: "{'ProductClassCode':'" + ProductClassCode + "'}",
                data: JSON.stringify({ ProductClassCode: ProductClassCode, proclassid: proclassid }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    CheckUniqueCode = msg.d;
                    if (CheckUniqueCode == true) {
                        jAlert('Please enter unique short name');
                        grid.GetEditor('ProductClass_Code').SetValue('');
                        grid.GetEditor('ProductClass_Code').Focus();
                        //document.getElementById('marketsGrid_DXPEForm_efnew_DXEditor1_I').value = '';
                        //document.getElementById('marketsGrid_DXPEForm_efnew_DXEditor1_I').Focus();
                    }
                }

            });
        }
    </script>

     

    <style>
        .dxbButton a {
            color: #000;
        }

        .dxbButton {
            padding: 3px !important;
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
        padding: 5px 10px !important;
    }

    #drdExport
    {
        height: 32px;
    }

    #marketsGrid
    {
        max-width: 100% !important;
    }

    .GridViewArea
    {
        overflow: hidden;
    overflow-x: auto;
    }
    /*Rev end 1.0*/

    @media only screen and (max-width: 768px) {
        .breadCumb {
            padding: 0 11%;
        }

        .breadCumb > span {
            padding: 9px 10px;
        }

        .FilterSide
        {
            width: 100% !important;
        }

        #marketsGrid_DXPEForm_PW-1
        {
            width: 330px !important;
        }
    }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="breadCumb">
        <span>Product Class/Category</span>
    </div>

    <div class="container">
        <div class="backBox mt-5 p-3 ">
        <table class="TableMain100">
            <tr>
                <td>
                    <%-- <table width="100%">
                    <tr>
                        <td style="text-align: left; vertical-align: top">
                            <table>
                                <tr>
                                    <td id="ShowFilter">
                                        <a href="javascript:ShowHideFilter('s');"><span style="color: #000099; text-decoration: underline">
                                            Show Filter</span></a>
                                    </td>
                                    <td id="Td1">
                                        <a href="javascript:ShowHideFilter('All');"><span style="color: #000099; text-decoration: underline">
                                            All Records</span></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                        <td class="gridcellright">
                            <dxe:ASPxComboBox ID="cmbExport" runat="server" AutoPostBack="true" BackColor="Navy"
                                Font-Bold="False" ForeColor="White" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged"
                                ValueType="System.Int32" Width="130px">
                                <Items>
                                    <dxe:ListEditItem Text="Select" Value="0" />
                                    <dxe:ListEditItem Text="PDF" Value="1" />
                                    <dxe:ListEditItem Text="XLS" Value="2" />
                                    <dxe:ListEditItem Text="RTF" Value="3" />
                                    <dxe:ListEditItem Text="CSV" Value="4" />
                                </Items>
                                <ButtonStyle BackColor="#C0C0FF" ForeColor="Black">
                                </ButtonStyle>
                                <ItemStyle BackColor="Navy" ForeColor="White">
                                    <HoverStyle BackColor="#8080FF" ForeColor="White">
                                    </HoverStyle>
                                </ItemStyle>
                                <Border BorderColor="White" />
                                <DropDownButton Text="Export">
                                </DropDownButton>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                </table>--%>
                    <div class="SearchArea mb-4">
                        <div class="FilterSide mb-4" style=" width: 500px">
                            <div style="padding-right: 5px;">
                                <% if (rights.CanAdd)
                                { %>
                                <a class="btn btn-success mr-2" href="javascript:void(0);" onclick="grid.AddNewRow()"><span>Add New</span> </a>
                                <%} %>
                                <% if (rights.CanExport)
                                               { %>
                                <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true" OnChange="if(!AvailableExportOption()){return false;}" >
                                    <asp:ListItem Value="0">Export to</asp:ListItem>
                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                        <asp:ListItem Value="2">XLS</asp:ListItem>
                                        <asp:ListItem Value="3">RTF</asp:ListItem>
                                        <asp:ListItem Value="4">CSV</asp:ListItem>
                                </asp:DropDownList>
                                 <%} %>
                            </div>
                            <%--<div>
                                <a class="btn btn-primary" href="javascript:ShowHideFilter('All');"><span>All Records</span></a>
                            </div>--%>
                        </div>
                        <%--<div class="ExportSide" style="float: right">
                            <div>
                                <dxe:ASPxComboBox ID="cmbExport" runat="server" AutoPostBack="true"
                                    Font-Bold="False" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged"
                                    ValueType="System.Int32" Width="130px">
                                    <Items>
                                        <dxe:ListEditItem Text="Select" Value="0" />
                                        <dxe:ListEditItem Text="PDF" Value="1" />
                                        <dxe:ListEditItem Text="XLS" Value="2" />
                                        <dxe:ListEditItem Text="RTF" Value="3" />
                                        <dxe:ListEditItem Text="CSV" Value="4" />
                                    </Items>
                                    <Border BorderColor="Black" />
                                    <DropDownButton Text="Export">
                                    </DropDownButton>
                                </dxe:ASPxComboBox>
                            </div>
                        </div>--%>
                    </div>
                </td>
            </tr>
            </table>
            <%--<tr>
                <td>--%>
                    <div class="GridViewArea">
                    <dxe:ASPxGridView ID="marketsGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="markets" KeyFieldName="ProductClass_ID" Width="100%" OnHtmlRowCreated="marketsGrid_HtmlRowCreated" OnRowDeleting="marketsGrid_RowDeleting"
                          
                        OnHtmlEditFormCreated="marketsGrid_HtmlEditFormCreated" OnCustomCallback="marketsGrid_CustomCallback"
                        OnCustomErrorText="marketsGrid_CustomErrorText" OnStartRowEditing="marketsGrid_StartRowEditing" SettingsBehavior-AllowFocusedRow="true" OnCellEditorInitialize="marketsGrid_CellEditorInitialize" OnCommandButtonInitialize="marketsGrid_CommandButtonInitialize" Settings-HorizontalScrollBarMode="Auto">
                        <SettingsSearchPanel Visible="True" />
                        <Settings ShowGroupPanel="True" ShowStatusBar="Hidden" ShowFilterRow="true" ShowFilterRowMenu="true" />
                        <Columns>
                            <dxe:GridViewDataTextColumn Visible="False" ReadOnly="True" VisibleIndex="0" FieldName="_ID">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="0" FieldName="ProductClass_Code" Caption="Short Name" Width="25%">
                                <PropertiesTextEdit Width="200px" MaxLength="50"  Style-Wrap="True" >

                                    <ClientSideEvents TextChanged="function(s, e) {UniqueCodeCheck();}" Init="function (s,e) {s.Focus(); }" />
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True" >
                                        <RequiredField IsRequired="True" ErrorText="Mandatory"/>
                                      
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                                <EditCellStyle HorizontalAlign="Left" Wrap="False">
                                    <Paddings PaddingTop="15px" />
                                </EditCellStyle>
                                <CellStyle Wrap="False">
                                </CellStyle>
                                <EditFormCaptionStyle Wrap="False" HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataTextColumn>
                            
                            <dxe:GridViewDataTextColumn VisibleIndex="0" FieldName="ProductClass_Name" Caption=" Name" Width="25%">
                                <PropertiesTextEdit Width="200px" MaxLength="100"  EncodeHtml="false">
                                   
                                    
                                    <%-- <ValidationSettings SetFocusOnError="True"  Display="None" ErrorImage-AlternateText="">
                                        <RequiredField IsRequired="True"  />
                                    </ValidationSettings>--%>

                                    
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                        <RequiredField  IsRequired="True" ErrorText="Mandatory"/>
                                    </ValidationSettings>
                                

                                </PropertiesTextEdit>
                                <EditCellStyle HorizontalAlign="Left"  >
                                    <Paddings PaddingTop="15px" />
                                </EditCellStyle>
                                <CellStyle Wrap="False">
                                </CellStyle>
                                <EditFormCaptionStyle Wrap="False" HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="0" FieldName="ProductClass_Description"
                                Caption="Description" Width="25%"> 
                                <EditItemTemplate>
                                    <dxe:ASPxMemo ID="ASPxMemo1" runat="server" Width="198px" Height="60px" MaxLength="300" Text='<%# Bind("ProductClass_Description") %>'>
                                    </dxe:ASPxMemo>
                                </EditItemTemplate>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataComboBoxColumn FieldName="ProductClass_ParentID" VisibleIndex="1" Visible="false"
                                Caption="Parent Class">
                                <PropertiesComboBox Width="200px" DataSourceID="SqlSourceProductClass_ParentID" EnableIncrementalFiltering="True" ValueField="ProductClass_ID"
                                    TextField="ProductClass_Name" EnableSynchronization="Default" ValueType="System.String">
                                </PropertiesComboBox>
                                <EditFormSettings Visible="False" Caption="Parent Class" />
                                <EditCellStyle HorizontalAlign="Left" Wrap="False">
                                    <Paddings PaddingTop="25px" />
                                </EditCellStyle>
                                <CellStyle Wrap="False">
                                </CellStyle>
                                <EditFormCaptionStyle Wrap="False" HorizontalAlign="Right" VerticalAlign="Bottom">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataComboBoxColumn>


                            <dxe:GridViewDataTextColumn VisibleIndex="0"   Caption="HSN Code"   FieldName="ProductClass_HSNCode" Visible="false"> 
                                <EditItemTemplate>
                                   <dxe:ASPxGridLookup ID="HsnLookUp" runat="server"  DataSourceID="HsnDataSource" ClientInstanceName="cHsnLookUp" 
                                                                        KeyFieldName="ProductClass_HSNCode" Width="200px" TextFormatString="{0}"  Value='<%# Bind("ProductClass_HSNCode") %>'>
                                                                        <Columns> 
                                                                            <dxe:GridViewDataColumn FieldName="ProductClass_HSNCode" Caption="Code" Width="50"/>
                                                                            <dxe:GridViewDataColumn FieldName="Description" Caption="Description" Width="350"/>
                                                                        </Columns>
                                                                        <GridViewProperties  Settings-VerticalScrollBarMode="Auto"    >
                                                                             
                                                                            <Templates>
                                                                                <StatusBar>
                                                                                    <table class="OptionsTable" style="float: right">
                                                                                        <tr>
                                                                                            <td>
                                                                                              <%--  <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Text="Close" ClientSideEvents-Click="CloseGridLookup" />--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </StatusBar>
                                                                            </Templates>
                                                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowStatusBar="Visible" UseFixedTableLayout="true"/>
                                                                        </GridViewProperties>
                                                                    <ClientSideEvents TextChanged="OnHsnChange"></ClientSideEvents>
                                                                    </dxe:ASPxGridLookup>
                                </EditItemTemplate>
                            </dxe:GridViewDataTextColumn>

                          <%--         <dxe:GridViewDataTextColumn Visible="True" VisibleIndex="0"   FieldName="FullServiceTax" Caption="Service Accounting Codes" >
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>--%>


                            <%--Service tax section--%>
                             <dxe:GridViewDataTextColumn VisibleIndex="0"   Caption="SAC"   FieldName="TAX_ID"  Visible="False"> 
                                  <EditFormSettings Visible="False" />
                                <EditItemTemplate>
                                   <dxe:ASPxGridLookup ID="serviceTaxLookup" runat="server"  DataSourceID="servicetaxDataSource" ClientInstanceName="cserviceTaxLookup" 
                                                                        KeyFieldName="TAX_ID" Width="200px" TextFormatString="{1}"  Value='<%# Bind("TAX_ID") %>'>
                                                                        <Columns> 
                                                                            <dxe:GridViewDataColumn FieldName="TAX_ID" Caption="Code" Width="0"/>
                                                                            <dxe:GridViewDataColumn FieldName="SERVICE_CATEGORY_CODE" Caption="Description" Width="350"/>
                                                                            <dxe:GridViewDataColumn FieldName="SERVICE_TAX_NAME" Caption="Description" Width="350"/>
                                                                        </Columns>
                                                                        <GridViewProperties  Settings-VerticalScrollBarMode="Auto"    >
                                                                             
                                                                            <Templates>
                                                                                <StatusBar>
                                                                                    <table class="OptionsTable" style="float: right">
                                                                                        <tr>
                                                                                            <td>
                                                                                              <%--  <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Text="Close" ClientSideEvents-Click="CloseGridLookup" />--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </StatusBar>
                                                                            </Templates>
                                                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowStatusBar="Visible" UseFixedTableLayout="true"/>
                                                                        </GridViewProperties>
                                                                    </dxe:ASPxGridLookup>
                                </EditItemTemplate>
                            </dxe:GridViewDataTextColumn>

                      

                            <dxe:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="CreateDate">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="CreateUser">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="LastModifyDate">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="LastModifyUser">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewCommandColumn ShowDeleteButton="true" Width="22%" ShowEditButton="true">
                                
                                <%-- <DeleteButton Visible="True">
                            </DeleteButton>
                            <EditButton Visible="True">
                            </EditButton>--%>

                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <%--    <%if (Session["PageAccess"].ToString().Trim() == "All" || Session["PageAccess"].ToString().Trim() == "Add" || Session["PageAccess"].ToString().Trim() == "DelAdd")
                                  { %>--%>
                                Actions
                               <%-- <%} %>--%>
                                </HeaderTemplate>                                
                            </dxe:GridViewCommandColumn>

                             <dxe:GridViewDataTextColumn VisibleIndex="2" Width="6%" CellStyle-HorizontalAlign="Center" Visible="false">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                  <EditFormSettings Visible="False"></EditFormSettings>
                                  <DataItemTemplate>
                                      <% if (rights.CanIndustry)
                                        { %>
                                          <a href="javascript:void(0);" onclick="OnAddBusinessClick('<%#Eval("ProductClass_Code") %>')" title="Add Industry" class="pad" style="text-decoration: none;"> 
                                            <img src="../../../../assests/images/icoaccts.gif" />
                                          </a>
                                      <%} %>
                                      </DataItemTemplate>
                              
                                <HeaderTemplate>
                                    Map
                                </HeaderTemplate>

                              
                            </dxe:GridViewDataTextColumn>
                        </Columns>

                        <SettingsCommandButton>
                            <DeleteButton Image-Url="../../../../assests/images/Delete.png" ButtonType="Image" Image-AlternateText="Delete" Styles-Style-CssClass="pad">
                            </DeleteButton>
                            <EditButton Image-Url="../../../../assests/images/Edit.png" ButtonType="Image" Image-AlternateText="Edit">
                            </EditButton>
                            <UpdateButton Text="Update" ButtonType="Button" Styles-Style-CssClass="btn btn-primary"></UpdateButton>
                            <CancelButton Text="Cancel" ButtonType="Button" Styles-Style-CssClass="btn btn-danger"></CancelButton>


                        </SettingsCommandButton>
                        
                         <SettingsText PopupEditFormCaption="Add/Modify Products Class/Category" />

                        <SettingsPager NumericButtonCount="20" PageSize="20" AlwaysShowPager="True" ShowSeparators="True">
                            <FirstPageButton Visible="True">
                            </FirstPageButton>
                            <LastPageButton Visible="True">
                            </LastPageButton>
                        </SettingsPager>

                        <SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" />

                        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm"  PopupEditFormHorizontalAlign="WindowCenter"
                            PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" PopupEditFormWidth="400px"  />

                        <Templates>
                            <EditForm>
                                <div style="padding: 5px; padding-bottom: 0px;">
                                    <table>
                                        <tr>
                                            <td style="width: 5%"></td>
                                            <td style="width: 90%">
                                                <%--<controls>--%>
                                <dxe:ASPxGridViewTemplateReplacement runat="server"  ReplacementType="EditFormEditors" ColumnID="" ID="Editors">
                                </dxe:ASPxGridViewTemplateReplacement>                                                           
                            <%--</controls>--%>
                                                <div style="padding: 2px 2px 5px 92px">
                                                    <div class="dxbButton" style="display: inline-block; color: Black;">
                                                        <dxe:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                            runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                                    </div>
                                                    <div class="dxbButton" style="display: inline-block; color: Black;">
                                                        <dxe:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                            runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="width: 5%"></td>
                                        </tr>
                                    </table>
                                </div>
                            </EditForm>
                        </Templates>
                        <clientsideevents endcallback="function(s, e) {	LastCall( );}" />
                    </dxe:ASPxGridView>
                    </div>
                <%--</td>
            </tr>
        </table>--%>
        <asp:SqlDataSource ID="SqlSourceProductClass_ParentID" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="select ProductClass_ID,ProductClass_Name from Master_ProductClass order by ProductClass_Name"></asp:SqlDataSource>
        <asp:SqlDataSource ID="markets" runat="server" ConflictDetection="CompareAllValues"
            ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>" DeleteCommand="DELETE FROM [Master_ProductClass] where ProductClass_ID=@original_ProductClass_ID;
          INSERT INTO [dbo].[Master_ProductClass_Log] ([ProductClass_ID],[ProductClass_Code],[ProductClass_Name],[ProductClass_Description],[ProductClass_ParentID],[ProductClass_CreateUser]
      ,[ProductClass_CreateTime],[ProductClass_ModifyUser],[ProductClass_ModifyTime],[ProductClass_LogType],[ProductClass_LogUser],[ProductClass_LogTime])
	  SELECT [ProductClass_ID],[ProductClass_Code],[ProductClass_Name],[ProductClass_Description],[ProductClass_ParentID],[ProductClass_CreateUser],[ProductClass_CreateTime]
      ,[ProductClass_ModifyUser],[ProductClass_ModifyTime],'D',1,GETDATE() FROM [dbo].[Master_ProductClass] WHERE [ProductClass_ID] = @original_ProductClass_ID"
            InsertCommand="INSERT INTO  [dbo].[Master_ProductClass] 
        ([ProductClass_Code],[ProductClass_Name],[ProductClass_Description],[ProductClass_ParentID]  ,[ProductClass_CreateUser],[ProductClass_CreateTime],ProductClass_HSNCode,ProductClass_SERVICE_CATEGORY_CODE) 
  values(@ProductClass_Code,@ProductClass_Name,@ProductClass_Description,@ProductClass_ParentID,@CreateUser,getdate(),@ProductClass_HSNCode,@TAX_ID)"
            OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT B.[ProductClass_ID],B.[ProductClass_Code],B.[ProductClass_Name],B.[ProductClass_Description], (select A.ProductClass_Name from Master_ProductClass A where A.ProductClass_ID=B.ProductClass_ID ) as ProductClass_Names,  B.[ProductClass_ParentID],B.[ProductClass_CreateUser],B.[ProductClass_CreateTime] 
        ,B.[ProductClass_ModifyUser],B.[ProductClass_ModifyTime],B.ProductClass_HSNCode as ProductClass_HSNCode,B.ProductClass_SERVICE_CATEGORY_CODE as TAX_ID,
            (select SERVICE_CATEGORY_CODE from TBL_MASTER_SERVICE_TAX where TAX_ID=ProductClass_SERVICE_CATEGORY_CODE) FullServiceTax FROM [dbo].[Master_ProductClass] B "
            UpdateCommand="update [dbo].[Master_ProductClass] set [ProductClass_Code]=@ProductClass_Code,[ProductClass_Name]=@ProductClass_Name,
[ProductClass_Description]=@ProductClass_Description,[ProductClass_ParentID]=@ProductClass_ParentID,[ProductClass_ModifyUser] = @CreateUser,ProductClass_ModifyTime=getdate(),ProductClass_HSNCode=@ProductClass_HSNCode
            ,ProductClass_SERVICE_CATEGORY_CODE=@TAX_ID where ProductClass_ID=@ProductClass_ID;
            update master_sproducts set sProducts_HsnCode =@ProductClass_HSNCode where  ProductClass_Code =@ProductClass_ID;
            update master_sproducts set sProducts_serviceTax=@TAX_ID where ProductClass_Code=@ProductClass_ID
            ">
            <DeleteParameters>
                <asp:Parameter Name="original_ProductClass_ID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="ProductClass_ID" Type="Int32" />
                <asp:Parameter Name="ProductClass_Code" Type="String" />
                <asp:Parameter Name="ProductClass_Name" Type="String" />
                <asp:Parameter Name="ProductClass_Description" Type="String" />
                <asp:Parameter Name="ProductClass_HSNCode" Type="String" />
                 <asp:Parameter Name="TAX_ID" Type="Int32" /> 
                <asp:Parameter Name="ProductClass_ParentID" Type="Int32" />
                <asp:SessionParameter Name="CreateUser" Type="Decimal" SessionField="userid" />
                <%-- <asp:Parameter Name="Markets_Country" Type="Int32" />
            <asp:SessionParameter Name="CreateUser" Type="Decimal" SessionField="userid" />--%>
            </UpdateParameters>
            <InsertParameters>
                <%--<asp:Parameter Name="edu_markets" Type="String" />--%>
                <asp:Parameter Name="ProductClass_Code" Type="String" />
                <asp:Parameter Name="ProductClass_Name" Type="String" />
                <asp:Parameter Name="ProductClass_Description" Type="String" />
                <asp:Parameter Name="ProductClass_ParentID" Type="Int32" />
                <asp:Parameter Name="ProductClass_HSNCode" Type="String" />
                <asp:Parameter Name="TAX_ID" Type="Int32" />
                <asp:SessionParameter Name="CreateUser" Type="Decimal" SessionField="userid" />
            </InsertParameters>
        </asp:SqlDataSource>
        <dxe:ASPxGridViewExporter ID="exporter" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
        </dxe:ASPxGridViewExporter>

         <asp:SqlDataSource ID="HsnDataSource" runat="server"  ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            SelectCommand="select Code ProductClass_HSNCode,Description from tbl_HSN_Master"
            ></asp:SqlDataSource>

           <asp:SqlDataSource ID="servicetaxDataSource" runat="server"  ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>"
            SelectCommand="select TAX_ID,SERVICE_CATEGORY_CODE,SERVICE_TAX_NAME from TBL_MASTER_SERVICE_TAX"
            ></asp:SqlDataSource>

         <div class="HiddenFieldArea" style="display: none;">
            <asp:HiddenField runat="server" ID="hiddenedit" ClientIDMode="Static" />
        </div>
    </div>
    </div>
    <br />
</asp:Content>
