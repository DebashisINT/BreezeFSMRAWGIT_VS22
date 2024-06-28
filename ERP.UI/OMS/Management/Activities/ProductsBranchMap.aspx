<%--====================================================== Revision History ================================================================
Rev Number DATE              VERSION          DEVELOPER           CHANGES
//1.0        07-05-2024        2.0.43          Priti               0027428 :Under Branch Wise Product mapping module , if IsActivateEmployeeBranchHierarchy=0, then the
====================================================== Revision History ================================================================--%>


<%@ Page Title="" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" CodeBehind="ProductsBranchMap.aspx.cs" Inherits="ERP.OMS.Management.Activities.ProductsBranchMap" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Activities/CSS/SearchPopup.css" rel="stylesheet" />
    <script src="../Activities/JS/SearchPopup.js"></script>
    <script src="../../../Scripts/SearchMultiPopup.js"></script>
    <style>
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
            #ProductTable table tr th {
                font-family: 'Poppins', sans-serif !important;
                font-size: 12px;
            }
    </style>
    <style>
        .w40 .modal-dialog {
            width: 40%;
        }

        .dis-flex {
            display: flex;
            align-items: center;
        }

        .btn-show {
            background: #2379d1;
            border-color: #2379d1;
            color: #fff;
        }

            .btn-show:hover, .btn-show:focus {
                color: #fff;
            }

        .pmsModal.w70 .modal-dialog {
            width: 70%;
        }

        .pmsModal.w60 .modal-dialog {
            width: 60%;
        }

        .pmsModal.w80 .modal-dialog {
            width: 80%;
        }

        .pmsModal .modal-header h4 {
            font-size: 16px;
        }

        .iminentSpan div#CmbState_DDD_PW-1.dxpcDropDown_PlasticBlue {
            left: 15px !important;
        }

        .modal-header {
            background-color: #007DB4;
            color: white;
            border-radius: 6px 6px 0 0;
        }

        button.close {
            color: #fff;
            opacity: .5;
            font-weight: 500;
        }

        .close:hover {
            color: #fff;
            opacity: 1;
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
                line-height: 18px;
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

        #AddProductTable, #ProductTable {
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

        #dtFrom_B-1, #dtTo_B-1, #cmbDOJ_B-1, #cmbLeaveEff_B-1, #FormDate_B-1, #toDate_B-1 {
            background: transparent !important;
            border: none;
            width: 30px;
            padding: 10px !important;
        }

            #dtFrom_B-1 #dtFrom_B-1Img,
            #dtTo_B-1 #dtTo_B-1Img, #cmbDOJ_B-1 #cmbDOJ_B-1Img, #cmbLeaveEff_B-1 #cmbLeaveEff_B-1Img, #FormDate_B-1 #FormDate_B-1Img, #toDate_B-1 #toDate_B-1Img {
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
        /*Rev end 1.0*/

        #divAddButton, #divImportButton {
            margin-right: 8px;
        }

            #divImportButton .btn {
                margin-right: 8px;
            }

        #txtProduct {
            margin-right: 8px;
            margin-left: 5px;
        }


        .btn-primary {
            background-color: #09527b;
        }

        .btn-info {
            background-color: #1eadb9;
        }

        .btn-warning {
            background-color: #bf822c;
        }

        .btn-view-log {
            background: #7919a9;
            color: #fff;
        }

            .btn-view-log:hover {
                color: #fff;
                background: #440662;
            }

        .btn-show {
            background: #011a9d;
            color: #fff;
        }

            .btn-show:hover {
                color: #fff;
                background: #031366;
            }

        /*.tblspace > tbody > tr > td {
        padding-right: 10px;
    }*/

        .btn:focus {
            color: #fff;
        }

        .btn-default {
            background-color: #e8e8e8 !important;
        }

            .btn-default:focus {
                color: #111 !important;
            }

        .btn:focus {
            outline: none;
        }

        #modalSS .modal-dialog {
            width: 850px !important;
        }

        #gridProductLookup_DDD_PW-1
        {
            left: -300px !important;
        }

        @media only screen and (max-width: 768px) {
            .breadCumb {
                padding: 0 18%;
            }

                .breadCumb > span {
                    padding: 9px 28px;
                }

            #TblSearch .btn {
                margin-bottom: 10px;
                margin-left: 10px;
            }

            #view {
                overflow-x: auto;
            }
        }
    </style>
    <script>

        /* --------------Branch----------------------*/
        function lookup_branchEndCall(s, e) {
            cParentEmpComponentPanel.PerformCallback('BindParentEmpgrid');
            cChildEmpComponentPanel.PerformCallback('BindChildEmpGrid');
        }

        $(function () {

            //Rev 1.0
            if ($("#hdnActivateEmployeeBranchHierarchy").val() == "0") {
                $("#DivHeadBranch").hide();
            }
            else {
                $("#DivHeadBranch").show();
            }
            $("#ddlbranchHO").change(function () {
                var Ids = $(this).val();
                cBranchComponentPanel.PerformCallback('BindComponentGrid' + '~' + $("#ddlbranchHO").val());
            })
            //Rev 1.0 End
            if ($("#hdAddOrEdit").val() != "Edit") {
                //Rev 1.0
                var hdnActivateEmployeeBranchHierarchy = $("#hdnActivateEmployeeBranchHierarchy").val();
                if (hdnActivateEmployeeBranchHierarchy == "0") {
                    cBranchComponentPanel.PerformCallback('BindComponentGridBrnachMap');
                }
                else
                {
                    cBranchComponentPanel.PerformCallback('BindComponentGrid' + '~' + $("#ddlbranchHO").val()); 
                }
                //Rev 1.0 End

                
            }
            else {
                $("#lblheading").html("Modify Branch Wise Product Mapping");
                gridbranchLookup.SetEnabled(false);
                $("#DivHeadBranch").hide();
            }


        });
        
        function selectAll() {
            gridbranchLookup.gridView.SelectRows();
        }
        function unselectAll() {
            gridbranchLookup.gridView.UnselectRows();
        }
        function CloseLookupbranch() {
            gridbranchLookup.ConfirmCurrentSelection();
            gridbranchLookup.HideDropDown();
            gridbranchLookup.Focus();
        }
        /* --------------Branch End----------------------*/
        /* --------------Product----------------------*/

        $(function () {
            if ($("#hdAddOrEdit").val() != "Edit") {
                cProductComponentPanel.PerformCallback('BindProductGrid');
            }

        });
        function selectAllProduct() {
            cgridProductLookup.gridView.SelectRows();
        }
        function unselectAllProduct() {
            cgridProductLookup.gridView.UnselectRows();
        }
        function CloseLookupProduct() {
            cgridProductLookup.ConfirmCurrentSelection();
            cgridProductLookup.HideDropDown();
            cgridProductLookup.Focus();
        }
        /* --------------Product End----------------------*/
        /* --------------Parent Emp----------------------*/

        function selectAllParentEmp() {
            cgridParentEmpLookup.gridView.SelectRows();
        }
        function unselectAllParentEmp() {
            cgridParentEmpLookup.gridView.UnselectRows();
        }
        function CloseLookupbranchParentEmp() {
            cgridParentEmpLookup.ConfirmCurrentSelection();
            cgridParentEmpLookup.HideDropDown();
            cgridParentEmpLookup.Focus();
        }
        function ParentEmpLookupEndCall(s, e) {
            cChildEmpComponentPanel.PerformCallback('BindChildEmpGrid');
        }
        /* --------------Parent End----------------------*/

        /* -----------Data save-------------------------------*/
        function SaveButtonClick(e) {
            if (gridbranchLookup.GetValue() == null) {
                jAlert('Please select atleast one branch.');
            }
            else if (cgridProductLookup.GetValue() == null) {
                jAlert('Please select atleast one Product.');
            }
            else if (cgridParentEmpLookup.GetValue() != null) {
                if (cChildParentEmpLookup.GetValue() == null) {
                    jAlert('Please select atleast one Child Employee.');
                }
                else {
                    cCallbackPanel.PerformCallback("SaveData");
                }
            }
            else {
                cCallbackPanel.PerformCallback("SaveData");
            }

        }
        function CallbackPanelEndCall(s, e) {
            if (cCallbackPanel.cpSaveSuccessOrFail == "1") {
                cCallbackPanel.cpSaveSuccessOrFail = null;


                jAlert('Save Successfully', 'Alert Dialog: [Branch wise Product mapping]', function (r) {
                    if (r == true) {

                        window.location.assign("ProductsBranchMapList.aspx");
                    }
                });

            }
            else if (cCallbackPanel.cpSaveSuccessOrFail == "-10") {
                CallbackPanel.cpSaveSuccessOrFail = null;
                jAlert('Please try after sometime.');
            }
        }

        /* -----------Data save  End-------------------------------*/

        /* --------------Child Emp----------------------*/



        function selectAllChildEmp() {
            cChildParentEmpLookup.gridView.SelectRows();
        }
        function unselectAllChildEmp() {
            cChildParentEmpLookup.gridView.UnselectRows();
        }
        function CloseLookupbranchChildEmp() {
            cChildParentEmpLookup.ConfirmCurrentSelection();
            cChildParentEmpLookup.HideDropDown();
            cChildParentEmpLookup.Focus();
        }

        /* --------------Child End----------------------*/

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="breadCumb">
        <span>
            <label id="lblheading">Branch Wise Product Mapping</label>
        </span>
        <div class="crossBtnN"><a href="ProductsBranchMapList.aspx"><i class="fa fa-times"></i></a></div>
    </div>


    <div class="container clearfix backBox mt-5 mb-4 py-3">
        <div class="py-3">
            <div class="clearfix">
            </div>
        </div>
        <div class="clear"></div>



        <div id="entry">
            <div style="background: #fff; padding: 17px 0; margin-bottom: 0px; border-radius: 4px; border: 1px solid #ccc;" class="clearfix">
                <%--Rev 1.0--%>
                <div class="col-md-2 h-branch-select" id="DivHeadBranch">
                    <label>Head Branch</label>
                    <div>
                        <asp:DropDownList ID="ddlbranchHO" runat="server" Width="100%" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <%--Rev 1.0 End--%>
                <div class="col-md-2">
                    <label>Branch</label>

                    <div>

                        <asp:HiddenField ID="hdnSelectedBranches" runat="server" />
                        <dxe:ASPxCallbackPanel runat="server" ID="BranchComponentPanel" ClientInstanceName="cBranchComponentPanel" OnCallback="Componentbranch_Callback" Width="100%">
                            <PanelCollection>
                                <dxe:PanelContent runat="server">
                                    <dxe:ASPxGridLookup ID="lookup_branch" SelectionMode="Multiple" runat="server" ClientInstanceName="gridbranchLookup"
                                        OnDataBinding="lookup_branch_DataBinding" ClientSideEvents-EndCallback="lookup_branchEndCall"
                                        KeyFieldName="ID" Width="100%" TextFormatString="{1}" AutoGenerateColumns="False" MultiTextSeparator=", ">
                                        <Columns>
                                            <dxe:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="60" Caption=" " />
                                            <dxe:GridViewDataColumn FieldName="branch_code" Visible="true" VisibleIndex="1" Width="200px" Caption="Branch code" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxe:GridViewDataColumn>
                                            <dxe:GridViewDataColumn FieldName="branch_description" Visible="true" VisibleIndex="1" Width="200px" Caption="Branch Name" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxe:GridViewDataColumn>
                                        </Columns>
                                        <GridViewProperties Settings-VerticalScrollBarMode="Auto" SettingsPager-Mode="ShowAllRecords">
                                            <Templates>
                                                <StatusBar>
                                                    <table class="OptionsTable" style="float: right">
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="false" Text="Select All" ClientSideEvents-Click="selectAll" UseSubmitBehavior="False" />

                                                                <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Text="Deselect All" ClientSideEvents-Click="unselectAll" UseSubmitBehavior="False" />
                                                                <dxe:ASPxButton ID="Close" runat="server" AutoPostBack="false" Text="Close" ClientSideEvents-Click="CloseLookupbranch" UseSubmitBehavior="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </StatusBar>
                                            </Templates>
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                            <SettingsPager Mode="ShowPager">
                                            </SettingsPager>

                                            <SettingsPager PageSize="20">
                                                <PageSizeItemSettings Visible="true" ShowAllItem="false" Items="10,20,50,100,150,200" />
                                            </SettingsPager>

                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowStatusBar="Visible" UseFixedTableLayout="true" />
                                        </GridViewProperties>

                                    </dxe:ASPxGridLookup>
                                </dxe:PanelContent>
                            </PanelCollection>

                        </dxe:ASPxCallbackPanel>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Parent Employee</label>

                    <div>
                        <dxe:ASPxCallbackPanel runat="server" ID="ParentEmpComponentPanel" ClientInstanceName="cParentEmpComponentPanel" OnCallback="ParentEmpComponentPanel_Callback" Width="100%">
                            <PanelCollection>
                                <dxe:PanelContent runat="server">
                                    <dxe:ASPxGridLookup ID="gridParentEmpLookup" SelectionMode="Multiple" runat="server" ClientInstanceName="cgridParentEmpLookup"
                                        OnDataBinding="ParentEmpLookup_DataBinding" ClientSideEvents-EndCallback="ParentEmpLookupEndCall"
                                        KeyFieldName="cnt_internalId" Width="100%" TextFormatString="{1}" AutoGenerateColumns="False" MultiTextSeparator=", ">
                                        <Columns>
                                            <dxe:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="60" Caption=" " />
                                            <dxe:GridViewDataColumn FieldName="cnt_UCC" Visible="true" VisibleIndex="1" Width="200px" Caption="Parent Emp. Code" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxe:GridViewDataColumn>
                                            <dxe:GridViewDataColumn FieldName="EMPLOYEENAME" Visible="true" VisibleIndex="2" Width="200px" Caption="Parent Employee" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxe:GridViewDataColumn>
                                            <dxe:GridViewDataColumn FieldName="deg_designation" Visible="true" VisibleIndex="3" Width="200px" Caption="Designation" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxe:GridViewDataColumn>
                                        </Columns>
                                        <GridViewProperties Settings-VerticalScrollBarMode="Auto" SettingsPager-Mode="ShowAllRecords">
                                            <Templates>
                                                <StatusBar>
                                                    <table class="OptionsTable" style="float: right">
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="false" Text="Select All" ClientSideEvents-Click="selectAllParentEmp" UseSubmitBehavior="False" />

                                                                <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Text="Deselect All" ClientSideEvents-Click="unselectAllParentEmp" UseSubmitBehavior="False" />
                                                                <dxe:ASPxButton ID="Close" runat="server" AutoPostBack="false" Text="Close" ClientSideEvents-Click="CloseLookupbranchParentEmp" UseSubmitBehavior="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </StatusBar>
                                            </Templates>
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                            <SettingsPager Mode="ShowPager">
                                            </SettingsPager>

                                            <SettingsPager PageSize="20">
                                                <PageSizeItemSettings Visible="true" ShowAllItem="false" Items="10,20,50,100,150,200" />
                                            </SettingsPager>

                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowStatusBar="Visible" UseFixedTableLayout="true" />
                                        </GridViewProperties>

                                    </dxe:ASPxGridLookup>
                                </dxe:PanelContent>
                            </PanelCollection>
                        </dxe:ASPxCallbackPanel>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Child Employee</label>

                    <div>
                        <dxe:ASPxCallbackPanel runat="server" ID="ChildEmpComponentPanel" ClientInstanceName="cChildEmpComponentPanel" OnCallback="ChildEmpComponentPanel_Callback" Width="100%">
                            <PanelCollection>
                                <dxe:PanelContent runat="server">
                                    <dxe:ASPxGridLookup ID="ChildParentEmpLookup" SelectionMode="Multiple" runat="server" ClientInstanceName="cChildParentEmpLookup"
                                        OnDataBinding="ChildParentEmpLookup_DataBinding"
                                        KeyFieldName="cnt_internalId" Width="100%" TextFormatString="{1}" AutoGenerateColumns="False" MultiTextSeparator=", ">
                                        <Columns>
                                            <dxe:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="60" Caption=" " />
                                            <dxe:GridViewDataColumn FieldName="cnt_UCC" Visible="true" VisibleIndex="1" Width="200px" Caption="Child Emp. Code" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxe:GridViewDataColumn>
                                            <dxe:GridViewDataColumn FieldName="EMPLOYEENAME" Visible="true" VisibleIndex="2" Width="200px" Caption="Child Employee" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxe:GridViewDataColumn>
                                            <dxe:GridViewDataColumn FieldName="deg_designation" Visible="true" VisibleIndex="3" Width="200px" Caption="Designation" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxe:GridViewDataColumn>
                                        </Columns>
                                        <GridViewProperties Settings-VerticalScrollBarMode="Auto" SettingsPager-Mode="ShowAllRecords">
                                            <Templates>
                                                <StatusBar>
                                                    <table class="OptionsTable" style="float: right">
                                                        <tr>
                                                            <td>
                                                                <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="false" Text="Select All" ClientSideEvents-Click="selectAllChildEmp" UseSubmitBehavior="False" />

                                                                <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Text="Deselect All" ClientSideEvents-Click="unselectAllChildEmp" UseSubmitBehavior="False" />
                                                                <dxe:ASPxButton ID="Close" runat="server" AutoPostBack="false" Text="Close" ClientSideEvents-Click="CloseLookupbranchChildEmp" UseSubmitBehavior="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </StatusBar>
                                            </Templates>
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                            <SettingsPager Mode="ShowPager">
                                            </SettingsPager>

                                            <SettingsPager PageSize="20">
                                                <PageSizeItemSettings Visible="true" ShowAllItem="false" Items="10,20,50,100,150,200" />
                                            </SettingsPager>

                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowStatusBar="Visible" UseFixedTableLayout="true" />
                                        </GridViewProperties>

                                    </dxe:ASPxGridLookup>
                                </dxe:PanelContent>
                            </PanelCollection>
                        </dxe:ASPxCallbackPanel>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>PRODUCT</label>
                    <div>

                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <dxe:ASPxCallbackPanel runat="server" ID="ProductComponentPanel" ClientInstanceName="cProductComponentPanel" OnCallback="ProductComponentPanel_Callback" Width="100%">
                            <PanelCollection>
                                <dxe:PanelContent runat="server">
                                    <dxe:ASPxGridLookup ID="gridProductLookup" SelectionMode="Multiple" runat="server" ClientInstanceName="cgridProductLookup"
                                        OnDataBinding="gridProductLookup_DataBinding"
                                        KeyFieldName="SPRODUCTS_ID" Width="100%" TextFormatString="{1}" AutoGenerateColumns="False" MultiTextSeparator=", ">
                                        <Columns>
                                            <dxe:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="60" Caption=" " />
                                            <dxe:GridViewDataColumn FieldName="PRODUCT_CODE" Visible="true" VisibleIndex="1" Width="200px" Caption="PRODUCT CODE" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxe:GridViewDataColumn>

                                            <dxe:GridViewDataColumn FieldName="PRODUCT_NAME" Visible="true" VisibleIndex="2" Width="200px" Caption="PRODUCT NAME" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxe:GridViewDataColumn>
                                            <dxe:GridViewDataColumn FieldName="BRANDNAME" Visible="true" VisibleIndex="3" Width="200px" Caption="BRAND" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxe:GridViewDataColumn>

                                            <dxe:GridViewDataColumn FieldName="CLASSCODE" Visible="true" VisibleIndex="4" Width="200px" Caption="CLASS/CATEGORY" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxe:GridViewDataColumn>
                                        </Columns>
                                        <GridViewProperties Settings-VerticalScrollBarMode="Auto" SettingsPager-Mode="ShowAllRecords">
                                            <Templates>
                                                <StatusBar>
                                                    <table class="OptionsTable" style="float: right">
                                                        <tr>
                                                            <td>

                                                                <dxe:ASPxButton ID="btnselectAllProduct" runat="server" AutoPostBack="false" Text="Select All" ClientSideEvents-Click="selectAllProduct" UseSubmitBehavior="False" />

                                                                <dxe:ASPxButton ID="btnunselectAllProduct" runat="server" AutoPostBack="false" Text="Deselect All" ClientSideEvents-Click="unselectAllProduct" UseSubmitBehavior="False" />
                                                                <dxe:ASPxButton ID="CloseProduct" runat="server" AutoPostBack="false" Text="Close" ClientSideEvents-Click="CloseLookupProduct" UseSubmitBehavior="False" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </StatusBar>
                                            </Templates>
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                            <SettingsPager Mode="ShowPager">
                                            </SettingsPager>

                                            <SettingsPager PageSize="20">
                                                <PageSizeItemSettings Visible="true" ShowAllItem="false" Items="10,20,50,100,150,200" />
                                            </SettingsPager>

                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowStatusBar="Visible" UseFixedTableLayout="true" />
                                        </GridViewProperties>

                                    </dxe:ASPxGridLookup>
                                </dxe:PanelContent>
                            </PanelCollection>
                        </dxe:ASPxCallbackPanel>
                    </div>

                </div>
                <div class="clearfix"></div>

            </div>
            <div class="clearfix"></div>
            <div style="padding: 15px 10px 10px 0px;">
                <dxe:ASPxButton ID="btnSaveRecords" TabIndex="7" ClientInstanceName="cbtnSaveRecords" runat="server" AutoPostBack="False" Text="S&#818;ave" CssClass="btn btn-primary" UseSubmitBehavior="False">
                    <ClientSideEvents Click="function(s, e) {SaveButtonClick('Insert');}" />
                </dxe:ASPxButton>
                <%--  <dxe:ASPxButton ID="btncancel" TabIndex="8" ClientInstanceName="cbtncancel" runat="server" AutoPostBack="False" Text="C&#818;ancel" CssClass="btn btn-primary" UseSubmitBehavior="False">
                    <ClientSideEvents Click="function(s, e) {cancel();}" />
                </dxe:ASPxButton>--%>
            </div>
        </div>

    </div>

    <dxe:ASPxCallbackPanel runat="server" ID="CallbackPanel" ClientInstanceName="cCallbackPanel" OnCallback="CallbackPanel_Callback">
        <PanelCollection>
            <dxe:PanelContent runat="server">
                <%--   <asp:HiddenField ID="hfIsCustOutDetFilter" runat="server" />--%>
            </dxe:PanelContent>
        </PanelCollection>
        <ClientSideEvents EndCallback="CallbackPanelEndCall" />
    </dxe:ASPxCallbackPanel>


    <asp:HiddenField runat="server" ID="hdAddOrEdit" />
    <asp:HiddenField runat="server" ID="hdnPageEditId" />
    <%--Rev 1.0--%>
    <asp:HiddenField runat="server" ID="hdnActivateEmployeeBranchHierarchy" />
    <%--Rev 1.0 End--%>





</asp:Content>
