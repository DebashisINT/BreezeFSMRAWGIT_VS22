<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                09-02-2023        2.0.39           Pallab              25656 : Master module design modification 
====================================================== Revision History ==========================================================--%>

<%@ Page Title="State" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" Inherits="ERP.OMS.Management.Master.management_master_empState" CodeBehind="empState.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
       

        .dxpc-mainDiv {
            position: relative;
            left: 0px;
            z-index: 100000;
        }

        .dxpc-headerContent {
            color: white;
        }
        #txtStateName_EC.dxeErrorFrameSys {
            position:absolute;
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

    /*Rev end 1.0*/

    @media only screen and (max-width: 768px)
    {
        .breadCumb {
            padding: 0 30%;
        }

    }

    </style>

    <script type="text/javascript">
        //function is called on changing country
        //function OnCountryChanged(cmbCountry) {
        //    grid.GetEditor("cou_country").PerformCallback(cmbCountry.GetValue().toString());
        //}
        //function ShowHideFilter(obj) {
        //    grid.PerformCallback(obj);
        //}
    </script>

    <%-- added by krishnendu--%>

    <script type="text/javascript">
        function fn_PopOpen() {
            document.getElementById('<%=hiddenedit.ClientID%>').value = '';
            ctxtStateName.SetText('');
            cPopup_EmpStates.Show();
        }
        //function btnSave_States1() {
        //    if(document.getElementById('ctxtStateName').)
        //}

        function btnSave_States() {
            var statenm = ctxtStateName.GetText();
            if (statenm.trim() == '')
                //if (trim(ctxtStateName.GetText()) == '')
             { 
                //alert('Please Enter State Name');
                //ctxtStateName.Focus();
            }
            else {
                if (document.getElementById('<%=hiddenedit.ClientID%>').value == '')
                    //grid.PerformCallback('savestate~' + ctxtStateName.GetText() + '~' + cCmbCountryName.GetText() + '~' + ctxtNseCode.GetText() + '~' + ctxtBseCode.GetText() + '~' + ctxtMcxCode.GetText() + '~' + ctxtMcsxCode.GetText() + '~' + ctxtNcdexCode.GetText() + '~' + ctxtCdslCode.GetText() + '~' + ctxtNsdlCode.GetText() + '~' + ctxtNdmlCode.GetText() + '~' + ctxtDotexidCode.GetText() + '~' + ctxtCvlidCode.GetText());
                    grid.PerformCallback('savestate~' + ctxtStateName.GetText());
                else
                    grid.PerformCallback('updatestate~' + GetObjectID('<%=hiddenedit.ClientID%>').value);
                //                 grid.PerformCallback('updatestate~'+ctxtStateName.GetText()+'~'+ cCmbCountryName.GetText()+'~'+GetObjectID('hiddenedit').value);
            }
        }
        function fn_btnCancel() {
            cPopup_EmpStates.Hide();
            $("#txtStateName_EC").hide();
        }
        function fn_EditState(keyValue) {
            grid.PerformCallback('Edit~' + keyValue);
        }
        function fn_DeleteState(keyValue) {

            //var result = confirm('Confirm delete?');
            //if (result) {
            //    grid.PerformCallback('Delete~' + keyValue);
            //}
            //else {
            //    return false;
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
        function grid_EndCallBack() {
            
            if (grid.cpinsert != null) {
                if (grid.cpinsert == 'Success') {
                    jAlert('Saved successfully');
                    cPopup_EmpStates.Hide();
                    grid.cpinsert = null;
                }
                else {
                    jAlert("Error On Insertion\n'Please Try Again!!'");
                }
            }

            if (grid.cpEdit != null) {

                ctxtStateName.SetText(grid.cpEdit.split('~')[0]);
                cCmbCountryName.SetValue(grid.cpEdit.split('~')[1]); 
                GetObjectID('<%=hiddenedit.ClientID%>').value = grid.cpEdit.split('~')[12];
                cPopup_EmpStates.Show();
                grid.cpEdit = null;
            }

            if (grid.cpUpdate != null) {
                if (grid.cpUpdate == 'Success') {
                    jAlert('Updated successfully');
                    cPopup_EmpStates.Hide();
                    grid.cpUpdate = null;
                }
                else
                    jAlert("Error on Updation\n'Please Try again!!'")
                grid.cpUpdate = null;
            }

            if (grid.cpDelete != null) {
                if (grid.cpDelete == 'Success') {
                    jAlert(grid.cpDelete);
                    grid.cpDelete = null;
                    grid.PerformCallback();
                }
                else
                    jAlert(grid.cpDelete)
                grid.PerformCallback();
            }

            if (grid.cpExists != null) {
                if (grid.cpExists == "Exists") {
                    jAlert('Duplicate value.');
                    cPopup_EmpStates.Hide();
                    grid.cpExists = null;
                }
                else
                    jAlert("Error on operatio\n'Please Try again!!'")
            }

        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
        <div class="breadCumb">
            <span>State</span>
        </div>
    
    <div class="container">
        <div class="backBox mt-3 p-3 ">
        <div class="Main">
            <%--<div class="TitleArea">
                <strong><span style="color: #000099">State List</span></strong>
            </div>--%>
            <div class="SearchArea clearfix">
                <div class="FilterSide mb-4">
                    <div style=" padding-right: 5px;">
                        <% if (rights.CanAdd)
                               { %>
                        <a href="javascript:void(0);" onclick="fn_PopOpen()" class="btn btn-success mr-2"><span>Add New</span> </a><%} %>

                        <% if (rights.CanExport)
                                               { %>
                         <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true"  OnChange="if(!AvailableExportOption()){return false;}">
                            <asp:ListItem Value="0">Export to</asp:ListItem>
                            <asp:ListItem Value="1">PDF</asp:ListItem>
                                <asp:ListItem Value="2">XLS</asp:ListItem>
                                <asp:ListItem Value="3">RTF</asp:ListItem>
                                <asp:ListItem Value="4">CSV</asp:ListItem>
                        </asp:DropDownList>
                         <% } %>
                        <%-- <a href="javascript:ShowHideFilter('s');" class="btn btn-primary"><span>Show Filter</span></a>--%>
                    </div>

                    <%-- ...........Code Commented By Sam on 28092016.................................--%>
                    <%--<div class="pull-left">
                        <a href="javascript:ShowHideFilter('All');" class="btn btn-primary"><span>All Records</span></a>
                    </div>--%>
                    <%-- ...........Code Above Commented By Sam on 28092016.................................--%>
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
                                <Border BorderColor="black" />
                                <DropDownButton Text="Export">
                                </DropDownButton>
                            </dxe:ASPxComboBox>
                        </div>
                    </div>--%>
                </div>

            </div>

            <div class="GridViewArea">
                <dxe:ASPxGridView ID="StateGrid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
                    KeyFieldName="id" Width="100%" OnHtmlRowCreated="StateGrid_HtmlRowCreated" OnHtmlEditFormCreated="StateGrid_HtmlEditFormCreated"
                    OnCustomCallback="StateGrid_CustomCallback" SettingsBehavior-AllowFocusedRow="true">
                    <Columns>
                        <dxe:GridViewDataTextColumn Caption="ID" FieldName="id" ReadOnly="True" Visible="False"
                            FixedStyle="Left" VisibleIndex="0">
                            <EditFormSettings Visible="False" />
                        </dxe:GridViewDataTextColumn>
                          <dxe:GridViewDataTextColumn Caption="Code" FieldName="StateCode" ReadOnly="True" Visible="true"
                            FixedStyle="Left" VisibleIndex="0">
                            <EditFormSettings Visible="False" />
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="State" FieldName="state" Width="50%" FixedStyle="Left"
                            Visible="True" VisibleIndex="1">
                            <EditFormSettings Visible="True" />
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="Country Name" FieldName="countryId" Visible="False"
                            VisibleIndex="2">
                            <CellStyle CssClass="gridcellleft" Wrap="False">
                            </CellStyle>
                        </dxe:GridViewDataTextColumn>


                        <dxe:GridViewDataTextColumn ReadOnly="True" Width="6%" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <span>Actions</span>
                            </HeaderTemplate>
                            <DataItemTemplate>
                                <% if (rights.CanEdit)
                               { %>
                                <a href="javascript:void(0);" onclick="fn_EditState('<%# Container.KeyValue %>')" class="pad" title="Edit">
                                    <img src="../../../assests/images/Edit.png" /></a><%} %>
                                <% if (rights.CanDelete)
                               { %>
                                <a href="javascript:void(0);" onclick="fn_DeleteState('<%# Container.KeyValue %>')" title="Delete">
                                    <img src="../../../assests/images/Delete.png" /></a><%} %>
                            </DataItemTemplate>
                        </dxe:GridViewDataTextColumn>
                    </Columns>
                    <SettingsSearchPanel Visible="True" />
                    <Settings ShowFilterRow="true" ShowGroupPanel="true" ShowStatusBar="Visible" ShowFilterRowMenu="true" />
                    <ClientSideEvents EndCallback="function (s, e) {grid_EndCallBack();}" />
                </dxe:ASPxGridView>
            </div>
            <%--added by krishnendu--%>
            <div class="PopUpArea">
                <dxe:ASPxPopupControl ID="Popup_EmpStates" runat="server" ClientInstanceName="cPopup_EmpStates"
                    Width="400px" HeaderText="Add States Details" PopupHorizontalAlign="WindowCenter"
                    Height="100px" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" Modal="True">
                    <ContentCollection>
                        <dxe:PopupControlContentControl ID="SRPopupControlContentControl" runat="server">
                            <div class="Top clearfix" style="width: 100%">
                                <div style="margin-top: 5px;" class="col-md-10 col-md-offset-1">
                                    <div class="stateDiv" style="padding-top: 5px; width: 95%">
                                        Country Name
                                    </div>
                                    <div style="padding-top: 5px;">
                                        <dxe:ASPxComboBox ID="CmbCountryName" ClientInstanceName="cCmbCountryName" runat="server"
                                            Width="100%" Height="25px" ValueType="System.String" AutoPostBack="false" EnableSynchronization="False"
                                            SelectedIndex="0">
                                        </dxe:ASPxComboBox>
                                    </div>
                                </div>
                                <br style="clear: both;" />
                                <div style="padding-top: 5px;" class="col-md-10 col-md-offset-1">
                                    <div class="stateDiv" style="padding-top: 5px">
                                        State<span style="color: red">*</span>
                                    </div>
                                    <div style="padding-top: 5px; width: 100%">
                                        <dxe:ASPxTextBox ID="txtStateName" MaxLength="50" ClientInstanceName="ctxtStateName" ClientEnabled="true"
                                            runat="server" Width="100%">
                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" ErrorImage-ToolTip="Mandatory" SetFocusOnError="True">
                                                <RequiredField IsRequired="True" ErrorText="Mandatory" />
                                            </ValidationSettings>
                                        </dxe:ASPxTextBox>
                                    </div>
                                </div>


                                <br style="clear: both;" />
                            </div>

                            <div class="ContentDiv">
                                <div class="ScrollDiv"></div>
                                <br style="clear: both;" />
                                <div class="Footer">
                                    <div style="text-align: left; padding-left: 47px;">
                                        <dxe:ASPxButton ID="btnSave_States" CssClass="btn btn-primary" ClientInstanceName="cbtnSave_States" runat="server"
                                            AutoPostBack="False" Text="Save">
                                            <ClientSideEvents Click="function (s, e) {btnSave_States();}" />
                                        </dxe:ASPxButton>
                                        <dxe:ASPxButton ID="btnCancel_States" CssClass="btn btn-danger" runat="server" AutoPostBack="False" Text="Cancel">
                                            <ClientSideEvents Click="function (s, e) {fn_btnCancel();}" />
                                        </dxe:ASPxButton>
                                    </div>
                                    <div style="">
                                    </div>
                                </div>
                            </div>
                        </dxe:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle BackColor="LightGray" ForeColor="Black" />
                </dxe:ASPxPopupControl>
                <dxe:ASPxGridViewExporter ID="exporter" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
        </dxe:ASPxGridViewExporter>
            </div>
            <div class="HiddenFieldArea" style="display: none;">
                <asp:HiddenField runat="server" ID="hiddenedit" />
            </div>
        </div>
    </div>
        </div>
</asp:Content>
