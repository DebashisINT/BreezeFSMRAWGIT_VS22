<%@ Page Title="" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" CodeBehind="ProductsBranchMapList.aspx.cs" Inherits="ERP.OMS.Management.Activities.ProductsBranchMapList" %>

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
            margin-bottom: 0;
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
            margin-right: 15px;
        }

            #divImportButton .btn {
                margin-right: 8px;
            }

        #txtProduct {
            margin-right: 8px;
            /*margin-left: 5px;*/
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

        .justify-content-between
        {
            justify-content: space-between;
        }

        .right-parameters label
        {
            margin-right: 5px;
        }
        .show-btn-div
{
    margin-left: 10px;
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
        $(function () {
            cBranchComponentPanel.PerformCallback('BindComponentGrid');
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
        //***Product***//
        var ProductArr = new Array();
        $(document).ready(function () {
            var ProductObj = new Object();
            ProductObj.Name = "ProductSource";
            ProductObj.ArraySource = ProductArr;
            arrMultiPopup.push(ProductObj);
        })
        function ProductButnClick(s, e) {
            $("#txtProduct_hidden").val("");
            $("#ProductTable").empty();
            var html = "<table border='1' width='100%' class='dynamicPopupTbl'><tr class='HeaderStyle'><th style='display:none'>id</th><th>Product Name</th><th>Product Description</th></tr></table>";
            $("#ProductTable").html(html);
            setTimeout(function () { $("#txtProdSearch").focus(); }, 500);
            $('#txtProdSearch').val('');

            $('#ProductModel').modal('show');

        }
        function ProductKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.htmlEvent.key == "NumpadEnter") {
                s.OnButtonClick(0);
            }
        }
        function prodkeydown(e) {
            var OtherDetails = {}
            if ($.trim($("#txtProdSearch").val()) == "" || $.trim($("#txtProdSearch").val()) == null) {
                return false;
            }

            OtherDetails.SearchKey = $("#txtProdSearch").val();

            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Product Name");
                HeaderCaption.push("Product Description");

                if ($("#txtProdSearch").val() != '') {
                    callonServerM("/OMS/Management/Activities/SpecialPriceUpload.aspx/GetProduct", OtherDetails, "ProductTable", HeaderCaption, "ProdIndex", "SetSelectedValues", "ProductSource");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[ProdIndex=0]"))
                    $("input[ProdIndex=0]").focus();
            }

        }
        function SetSelectedValues(Id, Name, ArrName) {
            if (ArrName == 'ProductSource') {
                var key = Id;
                if (key != null && key != '') {
                    $("#txtProduct_hidden").val(Id);
                    ctxtProductName.SetText(Name);
                    $('#ProductModel').modal('hide');
                }
                else {
                    $("#txtProduct_hidden").val('');
                    ctxtProductName.SetText('');
                    $('#ProductModel').modal('hide');

                }
            }
        }


        function ShowData() {
            $("#hfIsFilter").val("Y");
            $("#hFilterType").val("All");
            cCallbackPanel.PerformCallback("");
        }
        function CallbackPanelEndCall(s, e) {
            cGridProductBranchMap.Refresh();
        }
        function OnAddButtonClick() {
            var url = 'ProductsBranchMap.aspx?key=' + 'ADD';
            window.location.href = url;
        }

        function GridProductBranchCustomButtonClick(s, e) {
            var id = s.GetRowKey(e.visibleIndex);
            if (e.buttonID == 'CustomBtnEdit') {
                if (id != "") {
                    var url = 'ProductsBranchMap.aspx?key=' + id;
                    window.location.href = url;

                    //document.getElementById('HiddenSPECIALPRICEID').value = id;
                    //$.ajax({
                    //    type: "POST",
                    //    url: "/OMS/Management/Activities/ProductsBranchMapList.aspx/GetSpecialPrice",
                    //    data: JSON.stringify({ "MAPID": id }),
                    //    dataType: "json",
                    //    contentType: "application/json; charset=utf-8",
                    //    dataType: "json",
                    //    global: false,
                    //    async: false,
                    //    success: OnSuccess
                    //});
                }
            }
            if (e.buttonID == 'CustomBtnDelete') {
                if (id != "") {
                    if (confirm("Are you sure to delete")) {
                        $.ajax({
                            type: "POST",
                            url: "/OMS/Management/Activities/ProductsBranchMapList.aspx/DeleteProductsBranchMap",
                            data: JSON.stringify({ "MAPID": id }),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            global: false,
                            async: false,
                            success: function (msg) {
                                if (msg.d) {
                                    if (msg.d == "-998") {
                                        jAlert("Already is in used.Unable to delete");
                                        return false;
                                    }
                                    if (msg.d == "1") {
                                        jAlert("Deleted Successfuly");
                                        cGridProductBranchMap.Refresh();

                                        return false;
                                    }
                                }
                            }
                        });
                    }
                }

            }
        }

        function gridRowclick(s, e) {
            $('#cGridProductBranchMap').find('tr').removeClass('rowActive');
            $('.floatedBtnArea').removeClass('insideGrid');
            $(s.GetRow(e.visibleIndex)).find('.floatedBtnArea').addClass('insideGrid');
            $(s.GetRow(e.visibleIndex)).addClass('rowActive');
            setTimeout(function () {
                var lists = $(s.GetRow(e.visibleIndex)).find('.floatedBtnArea a');
                $.each(lists, function (index, value) {
                    setTimeout(function () {
                        console.log(value);
                        $(value).css({ 'opacity': '1' });
                    }, 100);
                });
            }, 200);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadCumb">
        <span>
            <label id="lblheading">Branch Wise Product Mapping</label>
        </span>
    </div>
    <div class="container clearfix backBox mt-5 mb-4 py-3">
        <div class="py-3">
            <div class="clearfix">
                <div class="dis-flex ">
                    <div class="left-btns">
                        <div id="divAddButton">
                            <a href="javascript:void(0);" onclick="OnAddButtonClick()" class="btn btn-success btn-radius"><span><u>A</u>dd New</span> </a>
                        </div>
                    </div>

                    <div class="dis-flex right-parameters">
                        <div class="dis-flex">
                            <label>Product(s)</label>
                            <dxe:ASPxButtonEdit ID="txtProduct" runat="server" ReadOnly="true" ClientInstanceName="ctxtProductName">
                                <Buttons>
                                    <dxe:EditButton>
                                    </dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s,e){ProductButnClick();}" KeyDown="ProductKeyDown" />
                            </dxe:ASPxButtonEdit>
                            <asp:HiddenField ID="txtProduct_hidden" runat="server" />

                        </div>
                        <div class="dis-flex">
                            <label>Branch</label>
                            <div>

                                <asp:HiddenField ID="hdnSelectedBranches" runat="server" />
                                <dxe:ASPxCallbackPanel runat="server" ID="BranchComponentPanel" ClientInstanceName="cBranchComponentPanel" OnCallback="Componentbranch_Callback" Width="100%">
                                    <PanelCollection>
                                        <dxe:PanelContent runat="server">
                                            <dxe:ASPxGridLookup ID="lookup_branch" SelectionMode="Multiple" runat="server" ClientInstanceName="gridbranchLookup"
                                                OnDataBinding="lookup_branch_DataBinding"
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
                        <%--<div class="clear"></div>--%>
                        <div class="show-btn-div">
                            <a href="javascript:void(0);" onclick="ShowData()" class="btn btn-show"><span>Show Data</span> </a>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="clear"></div>
        <div id="view" class="relative">
            <dxe:ASPxGridView ID="GridProductBranchMap" runat="server" AutoGenerateColumns="False" SettingsBehavior-AllowSort="true"
                ClientInstanceName="cGridProductBranchMap" KeyFieldName="PRODUCTBRANCHMAP_ID" Width="100%" Settings-HorizontalScrollBarMode="Auto"
                DataSourceID="EntityServerModeDataSource" SettingsDataSecurity-AllowEdit="false" SettingsDataSecurity-AllowInsert="false" SettingsDataSecurity-AllowDelete="false">

                <SettingsSearchPanel Visible="True" Delay="5000" />
                <ClientSideEvents CustomButtonClick="GridProductBranchCustomButtonClick" RowClick="gridRowclick" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxe:GridViewDataTextColumn Visible="False" FieldName="PRODUCTBRANCHMAP_ID" SortOrder="Descending">
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>

                    <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="BRANCH" Caption="Branch" Width="200px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="PARENTEMP" Caption="PARENT EMPLOYEE" Width="300px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="CHILDEMP" Caption="CHILD EMPLOYEE" Width="300px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="2" FieldName="PRODUCTCODE" Caption="ITEM CODE" Width="300px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="3" FieldName="PRODUCTNAME" Caption="ITEM NAME" Width="300px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="4" FieldName="CREATED_BY" Caption="CREATED BY" Width="200px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="4" FieldName="CREATED_ON" Caption="CREATED ON" Width="200px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <%--  <dxe:GridViewDataTextColumn VisibleIndex="4" FieldName="SPECIALPRICE" Caption="Spcial Price" Width="200px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="4" FieldName="SPECIALPRICE" Caption="Spcial Price" Width="200px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>--%>
                    <dxe:GridViewCommandColumn VisibleIndex="5" Width="130px" ButtonType="Image" Caption="Actions" HeaderStyle-HorizontalAlign="Center">
                        <CustomButtons>
                            <dxe:GridViewCommandColumnCustomButton ID="CustomBtnEdit" meta:resourcekey="GridViewCommandColumnCustomButtonResource1" Image-ToolTip="Edit" Styles-Style-CssClass="pad">
                                <Image Url="/assests/images/Edit.png"></Image>
                            </dxe:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                        <CustomButtons>
                            <dxe:GridViewCommandColumnCustomButton ID="CustomBtnDelete" meta:resourcekey="GridViewCommandColumnCustomButtonResource1" Image-ToolTip="Delete" Styles-Style-CssClass="pad">
                                <Image Url="/assests/images/Delete.png" ToolTip="Delete"></Image>
                            </dxe:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dxe:GridViewCommandColumn>
                </Columns>
                <SettingsContextMenu Enabled="true"></SettingsContextMenu>

                <Settings ShowGroupPanel="True" ShowStatusBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="true" HorizontalScrollBarMode="Visible" ShowFooter="true" />
                <SettingsPager NumericButtonCount="10" PageSize="10" ShowSeparators="True" Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" ShowAllItem="false" Items="10,50,100,150,200" />
                    <FirstPageButton Visible="True">
                    </FirstPageButton>
                    <LastPageButton Visible="True">
                    </LastPageButton>
                </SettingsPager>
            </dxe:ASPxGridView>
            <dx:LinqServerModeDataSource ID="EntityServerModeDataSource" runat="server" OnSelecting="EntityServerModeDataSource_Selecting"
                ContextTypeName="ERPDataClassesDataContext" TableName="PRODUCTBRANCHMAPLIST" />
            <asp:HiddenField ID="hfIsFilter" runat="server" />
            <asp:HiddenField ID="hFilterType" runat="server" />
        </div>
        <dxe:ASPxCallbackPanel runat="server" ID="CallbackPanel" ClientInstanceName="cCallbackPanel" OnCallback="CallbackPanel_Callback">
            <PanelCollection>
                <dxe:PanelContent runat="server">
                </dxe:PanelContent>
            </PanelCollection>
            <ClientSideEvents EndCallback="CallbackPanelEndCall" />
        </dxe:ASPxCallbackPanel>
        <!--Product Modal -->
        <div class="modal fade" id="ProductModel" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Product Search</h4>
                    </div>
                    <div class="modal-body">
                        <input type="text" onkeydown="prodkeydown(event)" id="txtProdSearch" class="form-control" autofocus width="100%" placeholder="Search By Product Name or Description" />

                        <div id="ProductTable">
                            <table border='1' width="100%" class="dynamicPopupTbl">
                                <tr class="HeaderStyle">
                                    <th class="hide">id</th>
                                    <th>Product Name</th>
                                    <th>Product Description</th>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnSaveProduct" class="btnOkformultiselection btn btn-success" data-dismiss="modal" onclick="OKPopup('ProductSource')">OK</button>

                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
