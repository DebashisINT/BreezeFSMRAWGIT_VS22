<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                09-02-2023        2.0.39           Pallab              25656 : Master module design modification 
2.0                20-04-2023        2.0.40           Sanchita            Under City master, Lat long need to be stored manually. Two new fields (Lat and Long) need to be added. 
                                                                          (Non Mandatory and same as Shop Master). refer : 25826
====================================================== Revision History ==========================================================--%>

<%@ Page Title="City" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" Inherits="ERP.OMS.Management.Master.management_master_City" CodeBehind="City.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        
        .dxpc-contentWrapper{
            background: rgb(237,243,244);
        }
        .dxpc-headerContent{
            color: white;
        }
    </style>
    <%--    <link href="../../CentralData/CSS/GenericCss.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="../../CentralData/JSScript/GenericJScript.js"></script>

    <script type="text/javascript">
        //function is called on changing country
        //        function OnCountryChanged(cmbCountry) 
        //        {
        //            grid.GetEditor("cou_country").PerformCallback(cmbCountry.GetValue().toString());
        //        }
        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
    </script>

    <script type="text/javascript">
        //$(document).ready(function () {
        //    alert('1');
        //    $('#valid').attr('style', 'display:none');
        //});

        function fn_PopOpen() {
            $('#valid').attr('style', 'display:none');
            document.getElementById('<%=hiddenedit.ClientID%>').value = '';
            //            alert('HidenEdit-'+GetObjectID('<%=hiddenedit.ClientID%>').value);
            ctxtcityName.SetText('');
            cCmbCountryName.SetValue("1");
            cCmbState.SetSelectedIndex(0);
            // Rev 2.0
            ctxtCityLat.SetValue(0.0);
            ctxtCityLong.SetValue(0.0);
            // End of Rev 2.0
            //ctxtNseCode.SetText('');
            //ctxtBseCode.SetText('');
            //ctxtMcxCode.SetText('');
            //ctxtMcsxCode.SetText('');
            //ctxtNcdexCode.SetText('');
            //ctxtCdslCode.SetText('');
            //ctxtNsdlCode.SetText('');
            //ctxtNdmlCode.SetText('');
            //ctxtDotexCode.SetText('');
            //ctxtCvlCode.SetText('');
            cPopup_Empcitys.Show();
            OnCmbCountryName_ValueChange();
        }
        function btnSave_citys() {
            if (ctxtcityName.GetText() == '') {
                //alert('Please Enter City Name');
                $('#valid').attr('style', 'display:block;position: absolute;right: -4px;top: 30px;');
                ctxtcityName.Focus();
            }
            else {
                if (document.getElementById('<%=hiddenedit.ClientID%>').value == '')
                    grid.PerformCallback('savecity~');
                else
                    grid.PerformCallback('updatecity~' + GetObjectID('<%=hiddenedit.ClientID%>').value);
            }
        }
        function fn_btnCancel() {
            cPopup_Empcitys.Hide();
        }
        function fn_Editcity(keyValue) {
            grid.PerformCallback('Edit~' + keyValue);
        }
        function fn_Deletecity(keyValue) {
            //var result=confirm('Confirm delete?');
            //if(result)
            //{
            //    grid.PerformCallback('Delete~' + keyValue);
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

        var editedState = "";
        function grid_EndCallBack() {
            
            if (grid.cpinsert != null) {
                if (grid.cpinsert == 'Success') {

                    jAlert('Saved successfully');
                    cPopup_Empcitys.Hide();
                   
                }
                else {
                    jAlert("Error On Insertion \n 'Please Try Again!!'");
                    cPopup_Empcitys.Hide();
                }
            }
            if (grid.cpEdit != null) {
                ctxtcityName.SetText(grid.cpEdit.split('~')[0]);
                //cCmbState.SetValue(grid.cpEdit.split('~')[1]);
                editedState = grid.cpEdit.split('~')[1];
                cCmbCountryName.SetValue(grid.cpEdit.split('~')[2]);
                OnCmbCountryName_ValueChange();
                //ctxtNseCode.SetText(grid.cpEdit.split('~')[3]);
                //ctxtBseCode.SetText(grid.cpEdit.split('~')[4]);
                //ctxtMcxCode.SetText(grid.cpEdit.split('~')[5]);
                //ctxtMcsxCode.SetText(grid.cpEdit.split('~')[6]);
                //ctxtNcdexCode.SetText(grid.cpEdit.split('~')[7]);
                //ctxtCdslCode.SetText(grid.cpEdit.split('~')[8]);
                //ctxtNsdlCode.SetText(grid.cpEdit.split('~')[9]);
                //ctxtNdmlCode.SetText(grid.cpEdit.split('~')[10]);
                //ctxtCvlCode.SetText(grid.cpEdit.split('~')[11]);
                //ctxtDotexCode.SetText(grid.cpEdit.split('~')[12]);
                GetObjectID('<%=hiddenedit.ClientID%>').value = grid.cpEdit.split('~')[13];
                // Rev 2.0
                ctxtCityLat.SetValue(grid.cpEdit.split('~')[14]);
                ctxtCityLong.SetValue(grid.cpEdit.split('~')[15]);
                // End of Rev 2.0
                cPopup_Empcitys.Show();
            }
            if (grid.cpUpdate != null) {
                if (grid.cpUpdate == 'Success') {
                    jAlert('Update Successfully');
                    cPopup_Empcitys.Hide();
                }
                else {
                    jAlert("Error on Updation\n'Please Try again!!'")
                    cPopup_Empcitys.Hide();
                }
            }
            if (grid.cpUpdateValid != null) {
                if (grid.cpUpdateValid == "StateInvalid") {
                    jAlert("Please Select State");
                    //cPopup_Empcitys.Show();
                    //cCmbState.Focus();
                    //alert(GetObjectID('<%=hiddenedit.ClientID%>').value);
                    //grid.PerformCallback('Edit~'+GetObjectID('<%=hiddenedit.ClientID%>').value);
                    //grid.cpUpdateValid=null;
                }
            }
            if (grid.cpDelete != null) {
                if (grid.cpDelete == 'Success') {
                    jAlert('Deleted sccessfully');
                    grid.cpDelete = null;
                    grid.PerformCallback();
                }
                else {
                    jAlert(grid.cpDelete);
                    grid.cpDelete = null;
                    grid.PerformCallback();
                }
            }
            if (grid.cpExists != null) {
                if (grid.cpExists == "Exists") {
                    jAlert('Duplicate value.');
                    cPopup_Empcitys.Hide();
                }
                else {
                    jAlert("Error on operation \n 'Please Try again!!'")
                    cPopup_Empcitys.Hide();
                }
            }
            

        }
        function OnCmbCountryName_ValueChange() {
            cCmbState.PerformCallback("BindState~" + cCmbCountryName.GetValue());
        }
        function CmbState_EndCallback() {
            cCmbState.SetSelectedIndex(0);
            if (editedState != "") {
                cCmbState.SetValue(editedState);
                editedState = "";
            }
            cCmbState.Focus();
        }
    </script>
    <style>
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

    /*Rev 2.0*/
    #txtCityLat , #txtCityLong
    {
        min-width: 195px !important;
    }
    /*Rev end 2.0*/

    @media only screen and (max-width: 768px)
    {
        .breadCumb {
            padding: 0 33%;
        }

    }
    </style>
    <script>
        $(document).ready(function () {
            
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="clearfix">

  
        <div class="breadCumb">
            <span>City</span>
        </div>
   
    
        <div class="container">
            <div class="backBox mt-3 p-3 ">
            <div class="">
                <div class="Main">
                    <%--<div class="TitleArea">
                    <strong><span style="color: #000099">City List</span></strong>
                </div>--%>
                    <div class="SearchArea">
                        <div class="FilterSide mb-4">
                            <div style="padding-right: 5px;">
                                <% if (rights.CanAdd)
                                   { %>
                                <a href="javascript:void(0);" onclick="fn_PopOpen()" class="btn btn-success mr-2"><span>Add New</span> </a><%} %>
                                <%--  <a href="javascript:ShowHideFilter('s');" class="btn btn-primary"><span >Show Filter</span></a>--%>
                                <a href="javascript:ShowHideFilter('All');" class="btn btn-primary" style="display: none;"><span>All Records</span></a>
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
                            </div>

                            <%--<div class="ExportSide pull-right">

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

                        </div>--%>
                        </div>

                    </div>
                </div>
                <div class="GridViewArea TableMain100">
                    <dxe:ASPxGridView ID="cityGrid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
                        KeyFieldName="city_id" Width="100%" OnHtmlRowCreated="cityGrid_HtmlRowCreated"
                        OnHtmlEditFormCreated="cityGrid_HtmlEditFormCreated" OnCustomCallback="cityGrid_CustomCallback" SettingsBehavior-AllowFocusedRow="true">
                        <Columns>
                            <dxe:GridViewDataTextColumn Caption="CityID" FieldName="city_id" ReadOnly="True"
                                Visible="False" FixedStyle="Left" VisibleIndex="0">
                                <EditFormSettings Visible="False" />
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="StateID" FieldName="state_id" ReadOnly="True"
                                Visible="False" FixedStyle="Left" VisibleIndex="1">
                                <EditFormSettings Visible="False" />
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="CountryID" FieldName="cou_id" ReadOnly="True"
                                Visible="False" FixedStyle="Left" VisibleIndex="2">
                                <EditFormSettings Visible="False" />
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="City" FieldName="city_name" Width="20%" FixedStyle="Left"
                                Visible="True" VisibleIndex="3" PropertiesTextEdit-MaxLength="50">
                                <EditFormSettings Visible="True" />
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="State" FieldName="state" Width="20%" FixedStyle="Left"
                                Visible="True" VisibleIndex="4">
                                <EditFormSettings Visible="True" />
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="Country" FieldName="cou_country" Visible="True"
                                Width="20%" VisibleIndex="5">
                                <CellStyle CssClass="gridcellleft" Wrap="False">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <%--Rev 2.0--%>
                            <dxe:GridViewDataTextColumn Caption="City Location Lat" FieldName="City_Lat" Visible="True" 
                                Width="17%" VisibleIndex="6">
                                <PropertiesTextEdit DisplayFormatString="d"></PropertiesTextEdit>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="City Location Long" FieldName="City_Long" Visible="True" 
                                Width="17%" VisibleIndex="7">
                                <PropertiesTextEdit DisplayFormatString="d"></PropertiesTextEdit>
                            </dxe:GridViewDataTextColumn>
                            <%--End of Rev 2.0--%>
                            <%-- <dxe:GridViewDataTextColumn Caption="NSECode" FieldName="City_NSECode" Visible="True"
                            Width="6%" VisibleIndex="6">
                            <CellStyle CssClass="gridcellleft" Wrap="False">
                            </CellStyle>
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="BSECode" FieldName="City_BSECode" Visible="True"
                            Width="6%" VisibleIndex="7">
                            <CellStyle CssClass="gridcellleft" Wrap="False">
                            </CellStyle>
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="MCXCode" FieldName="City_MCXCode" Visible="True"
                            Width="6%" VisibleIndex="8">
                            <CellStyle CssClass="gridcellleft" Wrap="False">
                            </CellStyle>
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="MCXSXCode" FieldName="City_MCXSXCode" Visible="True"
                            Width="6%" VisibleIndex="9">
                            <CellStyle CssClass="gridcellleft" Wrap="False">
                            </CellStyle>
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="NCDEXCode" FieldName="City_NCDEXCode" Visible="True"
                            Width="6%" VisibleIndex="10">
                            <CellStyle CssClass="gridcellleft" Wrap="False">
                            </CellStyle>
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="CDSLCode" FieldName="City_CDSLCode" Visible="True"
                            Width="6%" VisibleIndex="11">
                            <CellStyle CssClass="gridcellleft" Wrap="False">
                            </CellStyle>
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="NSDLCode" FieldName="City_NSDLCode" Visible="True"
                            Width="6%" VisibleIndex="12">
                            <CellStyle CssClass="gridcellleft" Wrap="False">
                            </CellStyle>
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="NDMLCode" FieldName="City_NDMLCode" Visible="True"
                            Width="6%" VisibleIndex="13">
                            <CellStyle CssClass="gridcellleft" Wrap="False">
                            </CellStyle>
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="CVLCode" FieldName="City_CVLCode" Visible="True"
                            Width="6%" VisibleIndex="14">
                            <CellStyle CssClass="gridcellleft" Wrap="False">
                            </CellStyle>
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="DotExCode" FieldName="City_DotExCode" Visible="True"
                            Width="6%" VisibleIndex="15">
                            <CellStyle CssClass="gridcellleft" Wrap="False">
                            </CellStyle>
                        </dxe:GridViewDataTextColumn>--%>
                            <dxe:GridViewDataTextColumn ReadOnly="True" Width="6%" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <span>Actions</span>
                                </HeaderTemplate>
                                <DataItemTemplate>
                                    <% if (rights.CanEdit)
                                       { %>
                                    <a href="javascript:void(0);" onclick="fn_Editcity('<%# Container.KeyValue %>')" class="pad" title="Edit">
                                        <img src="../../../assests/images/Edit.png" /></a><%} %>
                                    <% if (rights.CanDelete)
                                       { %>
                                    <a href="javascript:void(0);" onclick="fn_Deletecity('<%# Container.KeyValue %>')" title="Delete">
                                        <img src="../../../assests/images/Delete.png" /></a><%} %>
                                </DataItemTemplate>
                            </dxe:GridViewDataTextColumn>
                        </Columns>
                        <SettingsSearchPanel Visible="True" />
                        <Settings ShowGroupPanel="True" ShowFilterRow="true" ShowFilterRowMenu="true" />
                        <ClientSideEvents EndCallback="function (s, e) {grid_EndCallBack();}" />
                    </dxe:ASPxGridView>
                </div>
                <div class="PopUpArea">
                    <dxe:ASPxPopupControl ID="Popup_Empcitys" runat="server" ClientInstanceName="cPopup_Empcitys"
                        Width="700px" HeaderText="Add/Modify City" PopupHorizontalAlign="WindowCenter"
                        BackColor="white" Height="100px" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
                        Modal="True" ContentStyle-VerticalAlign="Top" EnableHierarchyRecreation="True" ba>
                        <ContentCollection>
                            <dxe:PopupControlContentControl runat="server">
                                <%--<div style="Width:400px;background-color:#FFFFFF;margin:0px;border:1px solid red;">--%>
                                <div class="Top clearfix">
                                    <div style="margin-bottom: 5px;" class="col-md-4">
                                        <div class="cityDiv" style="padding-top: 5px;">
                                            Country
                                        </div>
                                        <div class="Left_Content" style="padding-top: 5px;">
                                            <dxe:ASPxComboBox ID="CmbCountryName" ClientInstanceName="cCmbCountryName" runat="server"
                                                ValueType="System.String" Width="100%" EnableSynchronization="True">
                                                <ClientSideEvents ValueChanged="function(s,e){OnCmbCountryName_ValueChange()}"></ClientSideEvents>
                                            </dxe:ASPxComboBox>
                                        </div>
                                    </div>
                                    <div style="margin-bottom: 5px;" class="col-md-4">
                                        <div class="cityDiv" style="padding-top: 5px;">
                                            State
                                        </div>
                                        <div class="Left_Content" style="padding-top: 5px;">
                                            <dxe:ASPxComboBox ID="CmbState" ClientInstanceName="cCmbState" runat="server" ValueType="System.String"
                                                Width="100%" EnableSynchronization="True" OnCallback="CmbState_Callback">
                                                <ClientSideEvents EndCallback="CmbState_EndCallback"></ClientSideEvents>
                                            </dxe:ASPxComboBox>
                                        </div>
                                    </div>
                                    <div style="margin-bottom: 5px; position: relative" class="col-md-4">
                                        <div class="cityDiv" style="padding-top: 5px;">
                                            City<span style="color: red">*</span>
                                        </div>
                                        <div class="Left_Content" style="padding-top: 5px;">
                                            <dxe:ASPxTextBox ID="txtcityName" ClientInstanceName="ctxtcityName" runat="server" MaxLength="50"
                                                Width="100%">
                                            </dxe:ASPxTextBox>
                                            <div id="valid" style="display: none; position: absolute; right: -4px; top: 30px;">
                                                <img id="grid_DXPEForm_DXEFL_DXEditor2_EI" title="Mandatory" class="dxEditors_edtError_PlasticBlue" src="/DXR.axd?r=1_36-YRohc" alt="Required"></div>
                                            <%--<asp:RequiredFieldValidator ID="rvCity" runat="server"  ControlToValidate="txtcityName" ValidationGroup="cty" ErrorMessage="not Valid" ></asp:RequiredFieldValidator>--%>
                                        </div>

                                    </div>
                                    <%--Rev 2.0--%>
                                     <div style="margin-bottom: 5px; position: relative" class="col-md-4">
                                        <div class="cityDiv" style="padding-top: 5px;">
                                            City Location Lat
                                        </div>
                                        <div class="Left_Content" style="padding-top: 5px;">
                                            <dxe:ASPxTextBox ID="txtCityLat" ClientInstanceName="ctxtCityLat" runat="server" 
                                                Width="100%">
                                                <MaskSettings Mask="&lt;0..9999999999&gt;.&lt;0..999999999999999&gt;" />
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                    <div style="margin-bottom: 5px; position: relative" class="col-md-4">
                                        <div class="cityDiv" style="padding-top: 5px;">
                                            City Location Long
                                        </div>
                                        <div class="Left_Content" style="padding-top: 5px;">
                                            <dxe:ASPxTextBox ID="txtCityLong" ClientInstanceName="ctxtCityLong" runat="server" 
                                                Width="100%">
                                                <MaskSettings Mask="&lt;0..9999999999&gt;.&lt;0..999999999999999&gt;" />
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                    <%--End of Rev 2.0--%>
                                </div>
                                <div class="ContentDiv">
                                    <%--<div style="height: 20px; width: 100%; background-color: Gray; text-align:center;">
                                    <h5>Static Code</h5>
                                </div>
                                <div class="col-md-6 text-center" style="background-color: Gray;">
                                    Exchange
                                </div>
                                <div class="col-md-6 text-center" style="background-color: Gray;">
                                    Value
                                </div>
                                <div style="clear: both"></div>
                                <div class="ScrollDiv">
                                    <div class="col-md-4 mBot10">
                                        <div class="cityDiv">
                                            NSE Code
                                        </div>
                                        <div style="padding-top: 5px;">
                                            <dxe:ASPxTextBox ID="txtNseCode" ClientInstanceName="ctxtNseCode" runat="server"
                                                CssClass="cityTextbox" Width="100%">
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mBot10">
                                        <div class="cityDiv">
                                            BSE Code
                                        </div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtBseCode" ClientInstanceName="ctxtBseCode" runat="server"
                                                CssClass="cityTextbox" Width="100%">
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-4 mBot10">
                                        <div class="cityDiv">
                                            MCX Code
                                        </div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtMcxCode" ClientInstanceName="ctxtMcxCode" runat="server"
                                                CssClass="cityTextbox" Width="100%">
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                    <br style="clear: both;" />
                                    <div class="col-md-4 mBot10">
                                        <div class="cityDiv">
                                            MCXSX Code
                                        </div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtMcsxCode" ClientInstanceName="ctxtMcsxCode" runat="server"
                                                CssClass="cityTextbox" Width="100%">
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mBot10">

                                        <div class="cityDiv ">
                                            NCDEX Code
                                        </div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtNcdexCode" ClientInstanceName="ctxtNcdexCode" runat="server"
                                                CssClass="cityTextbox" Width="100%">
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mBot10">
                                        <div class="cityDiv ">
                                            CDSL Code
                                        </div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtCdslCode" ClientInstanceName="ctxtCdslCode" CssClass="cityTextbox"
                                                runat="server" Width="100%">
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                    <br style="clear: both;" />
                                    <div class="col-md-4 mBot10">
                                        <div class="cityDiv ">
                                            NSDL Code
                                        </div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtNsdlCode" ClientInstanceName="ctxtNsdlCode" CssClass="cityTextbox"
                                                runat="server" Width="100%">
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mBot10">
                                        <div class="cityDiv ">
                                            NDML Code
                                        </div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtNdmlCode" ClientInstanceName="ctxtNdmlCode" runat="server"
                                                CssClass="cityTextbox" Width="100%">
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mBot10">
                                        <div class="cityDiv ">
                                            CVL Code
                                        </div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtCvlCode" ClientInstanceName="ctxtCvlCode" runat="server"
                                                CssClass="cityTextbox" Width="100%">
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                    <br style="clear: both;" />
                                    <div class="col-md-4 mBot10">
                                        <div class="cityDiv ">
                                            DOTEX Code
                                        </div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtDotexCode" ClientInstanceName="ctxtDotexCode" runat="server"
                                                CssClass="cityTextbox" Width="100%">
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                    <br style="clear: both;" />
                                </div>--%>
                                    <br style="clear: both;" />
                                    <div class="Footer">
                                        <div class="col-md-12">
                                            <dxe:ASPxButton ID="btnSave_citys" ClientInstanceName="cbtnSave_citys" runat="server"
                                                AutoPostBack="False" Text="Save" CssClass="btn btn-primary">
                                                <ClientSideEvents Click="function (s, e) {btnSave_citys();}" />
                                            </dxe:ASPxButton>
                                            <dxe:ASPxButton ID="btnCancel_citys" runat="server" AutoPostBack="False" Text="Cancel" CssClass="btn btn-danger">
                                                <ClientSideEvents Click="function (s, e) {fn_btnCancel();}" />
                                            </dxe:ASPxButton>
                                        </div>
                                    </div>
                                </div>
                                <%-- </div>--%>
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
            <%--left conte slide area--%>
        <%--<div class="ltfg">
            <span class="Closer"><i class="fa fa-angle-right" aria-hidden="true"></i></span>
            <div class="innerCont">
                <div class="part">
                    <label>Current Bank Balance</label>
                    <div class="values">
                        12211.22
                    </div>
                </div>
                <div class="part">
                    <label>Account Balance</label>
                    <div class="values">
                        125463.22
                    </div>
                </div>
            </div> 
        </div>--%>
    </div>
    
    
</div>
    </div>

</asp:Content>
