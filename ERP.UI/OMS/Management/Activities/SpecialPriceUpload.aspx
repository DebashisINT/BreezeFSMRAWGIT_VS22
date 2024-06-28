<%--====================================================== Revision History ================================================================
Rev Number DATE               VERSION          DEVELOPER           CHANGES
//1.0        03-06-2024        2.0.47          Priti               	0027493: Modification in ITC special price upload module.
====================================================== Revision History ================================================================--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" CodeBehind="SpecialPriceUpload.aspx.cs" Inherits="ERP.OMS.Management.Activities.SpecialPriceUpload" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Activities/CSS/SearchPopup.css" rel="stylesheet" />
   <%-- <script src="../Activities/JS/SearchPopup.js"></script>--%>
    <script src="../../../Scripts/SearchMultiPopup.js"></script>
      <link href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" rel="stylesheet" />
  <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
  <script src="https://cdn.datatables.net/fixedcolumns/3.3.0/js/dataTables.fixedColumns.min.js"></script>

     <script src="../Activities/JS/SearchPopupDatatable.js"></script>
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

        #EmployeeTableTbl_length label, #AddProductTableTbl_length label
        {
                display: flex;
                align-items: center;
        }

        #EmployeeTableTbl_filter input, #AddProductTableTbl_filter input
        {
            height: 30px;
            box-shadow: none;
            outline: none;
            border-radius: 4px;
            border: 1px solid #ccc;
            padding: 0 5px;
        }

        #EmployeeTable {
            margin-top: 10px !important;
        }

        #EmployeeTableTbl_length label select, #AddProductTableTbl_length label select
        {
                margin: 0 5px;
        }

        #EmployeeModel .modal-dialog
        {
            width: 50% !important;
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
        // Rev 3.0
        function EmployeeButnClick(s, e) {
            $('#EmployeeModel').modal('show');
            $("#txtEmployeeSearch").focus();
        }

        function EmployeebtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#EmployeeModel').modal('show');
                $("#txtEmployeeSearch").focus();
            }
        }

        function Employeekeydown(e) {

            var OtherDetails = {}
            OtherDetails.SearchKey = $("#txtEmployeeSearch").val();
            OtherDetails.DesignationId = document.getElementById("cmbDesg").value;
            if ($.trim($("#txtEmployeeSearch").val()) == "" || $.trim($("#txtEmployeeSearch").val()) == null) {
                return false;
            }
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Employee Name");
                HeaderCaption.push("Employee Code");
                if ($("#txtEmployeeSearch").val() != null && $("#txtEmployeeSearch").val() != "") {
                    callonServer("/OMS/Management/Activities/SpecialPriceUpload.aspx/GetEmployee", OtherDetails, "EmployeeTable", HeaderCaption, "EmployeeIndex", "SetEmployee");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[EmployeeIndex=0]"))
                    $("input[EmployeeIndex=0]").focus();
            }
        }

        function SetEmployee(Id, Name) {
            var key = Id;
            if (key != null && key != '') {
                $("#txtEmployee_hidden").val(Id);
                ctxtEmployee.SetText(Name);
                $('#EmployeeModel').modal('hide');
            }
        }

        // End of Rev 3.0
    </script>
    <script>
        function AddProductButnClick(s, e) {
            $("#AddProductTable").empty();
            var html = "<table border='1' width='100%' class='dynamicPopupTbl'><tr class='HeaderStyle'><th style='display:none'>id</th><th>Product Code</th><th>Product Name</th></tr></table>";
            $("#AddProductTable").html(html);
            setTimeout(function () { $("#txtAddProdSearch").focus(); }, 500);
            $('#txtAddProdSearch').val('');
            $('#AddProductModel').modal('show');
        }
        function AddProductKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.htmlEvent.key == "NumpadEnter") {
                //shouldCheck = 0;
                s.OnButtonClick(0);
            }
        }
        function Addprodkeydown(e) {
            var OtherDetails = {}
            if ($.trim($("#txtAddProdSearch").val()) == "" || $.trim($("#txtAddProdSearch").val()) == null) {
                return false;
            }
            OtherDetails.SearchKey = $("#txtAddProdSearch").val();
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Product Code");
                HeaderCaption.push("Product Name");

                if ($("#txtAddProdSearch").val() != '') {
                    callonServer("/OMS/Management/Activities/SpecialPriceUpload.aspx/GetProduct", OtherDetails, "AddProductTable", HeaderCaption, "AddProdIndex", "SetProduct");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[AddProdIndex=0]"))
                    $("input[AddProdIndex=0]").focus();
            }
        }
        function SetProduct(Id, Name) {
            if (Id) {
                $('#AddProductModel').modal('hide');
                SetProdNametxt(Id, Name);
            }
        }
        function SetProdNametxt(id, name) {
            ctxtProductNameAdd.SetText(name);
            var varproductid = id.split("||@||")[0];
            var minsaleprice = id.split("||@||")[1];
            document.getElementById('HiddenProductID').value = varproductid;

        }
    </script>
    <script>
        /***Short cut key handling***/
        document.onkeydown = function (e) {
            if (event.keyCode == 83 && event.altKey == true) { //run code for Ctrl+S -- ie, Save & New  
                StopDefaultAction(e);
                document.getElementById('btnSaveRecords').click();
                return false;
            }
            else if (event.keyCode == 65 && event.altKey == true) {
                StopDefaultAction(e);
                OnAddButtonClick();
            }
            else if (event.keyCode == 67 && event.altKey == true) {
                StopDefaultAction(e);
                cancel()
            }
        }
        function StopDefaultAction(e) {
            if (e.preventDefault) { e.preventDefault() }
            else { e.stop() };

            e.returnValue = false;
            e.stopPropagation();
        }
    </script>
    <script>
        //***Customer***//
        function CustomerButnClick(s, e) {
            $("#CustomerTable").empty();
            var html = "<table border='1' width='100%' class='dynamicPopupTbl'><tr class='HeaderStyle'><th style='display:none'>id</th><th>Customer Name</th><th>Unique Id</th><th>Address</th><th>Type</th></tr></table>";
            $("#CustomerTable").html(html);
            setTimeout(function () { $("#txtCustSearch").focus(); }, 500);
            $('#txtCustSearch').val('');
            //shouldCheck = 1;
            //$('#mainActMsg').hide();
            $('#CustModel').modal('show');

        }
        function CustomerKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.htmlEvent.key == "NumpadEnter") {
                //shouldCheck = 0;
                s.OnButtonClick(0);
            }
        }
        function Customerkeydown(e) {
            var OtherDetails = {}
            if ($.trim($("#txtCustSearch").val()) == "" || $.trim($("#txtCustSearch").val()) == null) {
                return false;
            }

            OtherDetails.SearchKey = $("#txtCustSearch").val();

            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Customer Name");
                HeaderCaption.push("Unique Id");
                HeaderCaption.push("Address");
                HeaderCaption.push("Type");
                if ($("#txtCustSearch").val() != '') {
                    callonServer("/OMS/Management/Activities/CustSaleRateLock.aspx/GetCustomer", OtherDetails, "CustomerTable", HeaderCaption, "customerIndex", "SetCustomer");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[customerindex=0]"))
                    $("input[customerindex=0]").focus();
            }

        }
        function SetCustomer(Id, Name) {
            if (Id) {
                $('#CustModel').modal('hide');
                SetCustNametxt(Id, Name)
            }
        }
        function ValueSelected(e, indexName) {
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var Id = e.target.parentElement.parentElement.cells[0].innerText;
                var name = e.target.parentElement.parentElement.cells[1].children[0].value;
                if (Id) {
                    if (indexName == "customerIndex") {
                        $('#CustModel').modal('hide');
                        SetCustomer(Id, name);
                    }
                    else if (indexName == "ProdIndex") {
                        $('#ProductModel').modal('hide');
                        SetProduct(Id, name);
                    }
                }

            }
            else if (e.code == "ArrowDown") {
                thisindex = parseFloat(e.target.getAttribute(indexName));
                thisindex++;
                if (thisindex < 10)
                    $("input[" + indexName + "=" + thisindex + "]").focus();
            }
            else if (e.code == "ArrowUp") {
                thisindex = parseFloat(e.target.getAttribute(indexName));
                thisindex--;
                if (thisindex > -1)
                    $("input[" + indexName + "=" + thisindex + "]").focus();
                else {
                    if (indexName == "customerIndex") {
                        $('#txtCustSearch').focus();
                    }
                    else if (indexName == "ProdIndex") {
                        $('#txtProdSearch').focus();
                    }
                }
            }
            else if (e.code == "Escape") {
                if (indexName == "customerIndex") {
                    $('#CustModel').modal('hide');
                    ctxtCustName.Focus();
                }
                else if (indexName == "ProdIndex") {
                    $('#ProductModel').modal('hide');
                    ctxtProductName.Focus();
                }

            }
        }
        function SetCustNametxt(id, name) {

            ctxtCustName.SetText(name);
            document.getElementById('hdnCustId').value = id;

            ctxtProductName.Focus();
            $("#MandatorysCustName").hide();


        }
    </script>
    <script>
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
            var html = "<table border='1' width='100%' class='dynamicPopupTbl'><tr class='HeaderStyle'><th style='display:none'>id</th><th>Product Code</th><th>Product Name</th></tr></table>";
            $("#ProductTable").html(html);
            setTimeout(function () { $("#txtProdSearch").focus(); }, 500);
            $('#txtProdSearch').val('');

            $('#ProductModel').modal('show');

        }
        function ProductKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.htmlEvent.key == "NumpadEnter") {
                //shouldCheck = 0;
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
                HeaderCaption.push("Product Code");
                HeaderCaption.push("Product Name");

                if ($("#txtProdSearch").val() != '') {
                    //callonServer("/OMS/Management/Activities/SpecialPriceUpload.aspx/GetProduct", OtherDetails, "ProductTable", HeaderCaption, "ProdIndex", "SetSelectedValues");
                    callonServerM("/OMS/Management/Activities/SpecialPriceUpload.aspx/GetProduct", OtherDetails, "ProductTable", HeaderCaption, "ProdIndex", "SetSelectedValues", "ProductSource");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[ProdIndex=0]"))
                    $("input[ProdIndex=0]").focus();
            }

        }
        //function SetProduct(Id, Name) {
        //    if (Id) {
        //        $('#ProductModel').modal('hide');
        //        SetProdNametxt(Id, Name);
        //    }
        //}
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

            //localStorage.setItem("OrderBranch", ccmbBranchfilter.GetValue());
            //$("#hfBranchID").val(ccmbBranchfilter.GetValue());
            $("#hfIsFilter").val("Y");
            $("#hFilterType").val("All");
            cCallbackPanel.PerformCallback("");
        }
        function CallbackPanelEndCall(s, e) {
            cGridSpecialPriceUpload.Refresh();
        }
        function ShowLogData(haslog) {
            $('#btnViewLog').click();
        }
        function ViewLogData() {
            cGvImportDetailsSearch.Refresh();
        }
    </script>
    <script>
        /***************Calculation************/
        function AmountCalculate() {
            var minsaleprice = ctxtMinSalePrice.GetValue();
            var percentage = ctxtDiscount.GetValue();
            if (percentage == "0.00") {
                ctxtAmount.SetValue(minsaleprice);
            }
            else {
                var calcPrice = (minsaleprice - (minsaleprice * percentage / 100)).toFixed(2);
                ctxtAmount.SetValue(calcPrice);
            }
        }
        function PercentageCalculate() {
            var Amount = ctxtAmount.GetValue();
            var minsaleprice = ctxtMinSalePrice.GetValue();
            if (minsaleprice == "0.00") {

            }
            else {
                var calcDis = (((minsaleprice - Amount) * 100) / minsaleprice).toFixed(2);
                if (calcDis == "NaN" || calcDis == "") {
                }
                else {
                    ctxtDiscount.SetValue(calcDis);
                }
            }
        }

    </script>
    <script type="text/javascript">
        function AddSaveButtonClick(flag) {           

            var ProductID = $("#HiddenProductID").val();
            var BRANCH = $("#ddlBRANCH").val();

            if (BRANCH == "0" || BRANCH == "" || BRANCH == null) {
                $("#AddMandatorysBRANCH").show();
                $("#ddlBRANCH").Focus();
                return false;
            }
            else {
                $("#AddMandatorysBRANCH").hide();
            }

            if (ProductID == "0" || ProductID == "") {
                $("#AddMandatorysProduct").show();
                ctxtProductNameAdd.Focus();
                return false;
            }
            else {
                $("#AddMandatorysProduct").hide();
            }

           
            if (ctxtSPECIALPRICEAdd.GetValue() == "0.00") {
                $("#AddMandatorysSPECIALPRICE").show();
                ctxtSPECIALPRICEAdd.Focus();
                return false;
            }
            else {
                $("#AddMandatorysSPECIALPRICE").hide();
            }


            var des = document.getElementById("cmbDesg").value;
            if (des == "") {               
                des = 0;
            }

            $.ajax({
                type: "POST",
                url: "/OMS/Management/Activities/SpecialPriceUpload.aspx/InsertSpecialPrice",
                data: JSON.stringify({
                    "ProductID": ProductID,
                    "BRANCH": BRANCH,
                    "SPECIALPRICE": ctxtSPECIALPRICEAdd.GetValue(),
                    <%--  Rev 1.0 --%>
                    "DesignationId": des,
                    "EMPINTERNALID": $("#txtEmployee_hidden").val(),
                    "SPECIALPRICEID": $.trim($("#HiddenSPECIALPRICEID").val())
                    <%--  Rev 1.0 End--%>

                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                global: false,
                async: false,
                success: function (msg) {
                    if (msg.d) {
                        if (msg.d == "1") {
                            jAlert("Save Successfully");
                            $("#entry").hide();
                            $("#view").show();
                            $("#TblSearch").show();
                            $("#lblheading").html("Special Price Upload");
                            $("#divAddButton").show();
                            $("#divcross").hide();
                            $("#AddSPECIALPRICE").hide();
                            $("#divImportButton").show();
                            clear();
                            //cGridSpecialPriceUpload.Refresh();
                            ShowData();
                            return false;
                        }

                    }
                },
                error: function (response) {
                    console.log(response);
                }
            });

        }

        function SaveButtonClick(flag) {

            if (ctxtSPECIALPRICE.GetValue() == "0.00") {
                $("#MandatorysSPECIALPRICE").show();
                ctxtSPECIALPRICE.Focus();
                return false;
            }

            $.ajax({
                type: "POST",
                url: "/OMS/Management/Activities/SpecialPriceUpload.aspx/UpdateSpecialPrice",
                data: JSON.stringify({
                    "SPECIALPRICEID": $.trim($("#HiddenSPECIALPRICEID").val()),
                    "SPECIALPRICE": ctxtSPECIALPRICE.GetValue()
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                global: false,
                async: false,
                success: function (msg) {
                    if (msg.d) {
                        if (msg.d == "1") {
                            jAlert("Updated Successfully");
                            $("#entry").hide();
                            $("#view").show();
                            $("#TblSearch").show();
                            $("#divImportButton").show();
                            $("#lblheading").html("Special Price Upload");
                            $("#divAddButton").show();
                            $("#divcross").hide();
                            clear();
                            cGridSpecialPriceUpload.Refresh();
                            return false;
                        }

                    }
                },
                error: function (response) {
                    console.log(response);
                }
            });

        }

        function SpecialPriceCustomButtonClick(s, e) {
            var id = s.GetRowKey(e.visibleIndex);
            if (e.buttonID == 'CustomBtnEdit') {
                if (id != "") {
                    document.getElementById('HiddenSPECIALPRICEID').value = id;
                    $.ajax({
                        type: "POST",
                        url: "/OMS/Management/Activities/SpecialPriceUpload.aspx/GetSpecialPrice",
                        data: JSON.stringify({ "SPECIALPRICEID": id }),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        global: false,
                        async: false,
                        success: OnSuccess
                    });
                }
            }
            if (e.buttonID == 'CustomBtnDelete') {
                if (id != "") {
                    if (confirm("Are you sure to delete")) {
                        $.ajax({
                            type: "POST",
                            url: "/OMS/Management/Activities/SpecialPriceUpload.aspx/DeleteSpecialPrice",
                            data: JSON.stringify({ "SPECIALPRICEID": id }),
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
                                        cGridSpecialPriceUpload.Refresh();

                                        return false;
                                    }
                                }
                            }
                        });
                    }
                }

            }
        }

        function OnSuccess(data) {
            //alert(data);
            for (var i = 0; i < data.d.length; i++) {
                document.getElementById('hdnProdId').value = data.d[i].ProductID;
                //ctxtBRANCH.SetValue(data.d[i].branch_description);
                //ctxtPRODUCTCODE.SetValue(data.d[i].PRODUCT_CODE);
                //ctxtPRODUCTNAME.SetValue(data.d[i].Products_Name);
                //ctxtSPECIALPRICE.SetValue(data.d[i].SPECIAL_PRICE);
                //ctxtDesignation.SetValue(data.d[i].deg_designation);
                //ctxtEditEmployee.SetValue(data.d[i].Employee_Name);

                $("#ddlBRANCH").val(data.d[i].BRANCH_ID);
                $("#cmbDesg").val(data.d[i].DesignationID);                
                ctxtEmployee.SetValue(data.d[i].Employee_Name);
                $("#txtEmployee_hidden").val(data.d[i].EMPINTERNALID);               

                ctxtProductNameAdd.SetText(data.d[i].Products_Name);
                $("#HiddenProductID").val(data.d[i].ProductID);    
                
                ctxtSPECIALPRICEAdd.SetValue(data.d[i].SPECIAL_PRICE);


            }
            //$("#entry").show();
            $("#view").hide();
            $("#divAddButton").hide();
            $("#divcross").show();
            $("#lblheading").html("Modify Special Price Upload");
            $("#TblSearch").hide();

            //$("#AddSPECIALPRICE").hide();
            $("#AddSPECIALPRICE").show();

            $("#divImportButton").hide();
        }

        function OnAddButtonClick() {
            $("#divAddButton").hide();
            $("#entry").hide();
            $("#view").hide();
            $("#lblheading").html("Add Special Price Upload");
            $("#divcross").show();
            $("#AddSPECIALPRICE").show();
            $("#TblSearch").hide();
            $("#divImportButton").hide();
            clear();
        }

        function cancel() {
            clear();
            $("#entry").hide();
            $("#view").show();
            $("#TblSearch").show();
            $("#divImportButton").show();
            $("#lblheading").html("Special Price Upload");
            $("#divAddButton").show();
            $("#divcross").hide();
            $("#AddSPECIALPRICE").hide();

        }
        function cancelAdd() {
            clear();
            $("#entry").hide();
            $("#view").show();
            $("#TblSearch").show();
            $("#divImportButton").show();
            $("#lblheading").html("Special Price Upload");
            $("#divAddButton").show();
            $("#divcross").hide();
            $("#AddSPECIALPRICE").hide();

        }

        function clear() {
            ctxtBRANCH.SetValue("");
            ctxtPRODUCTCODE.SetValue("");
            ctxtPRODUCTNAME.SetValue("");
            ctxtSPECIALPRICE.SetValue("0.00");
            ctxtProductNameAdd.SetValue("");
            ctxtSPECIALPRICEAdd.SetValue("0.00");
            ctxtDesignation.SetValue("");
            ctxtEditEmployee.SetValue("");
            $("#ddlBRANCH").val("");
            $("#cmbDesg").val("");
            ctxtEmployee.SetValue("");
            $("#txtEmployee_hidden").val("");

            ctxtProductNameAdd.SetText("");
            $("#HiddenProductID").val("");

            ctxtSPECIALPRICEAdd.SetValue("");
        }

        $(document).ready(function () {
            console.log('ready');
            $('.navbar-minimalize').click(function () {
                console.log('clicked');
                // cGridSpecialPriceUpload.Refresh();
                cgridProductRate.Refresh();
            });
        });

        function gridRowclick(s, e) {
            $('#GridSpecialPriceUpload').find('tr').removeClass('rowActive');
            $('.floatedBtnArea').removeClass('insideGrid');
            //$('.floatedBtnArea a .ico').css({ 'opacity': '0' });
            $(s.GetRow(e.visibleIndex)).find('.floatedBtnArea').addClass('insideGrid');
            $(s.GetRow(e.visibleIndex)).addClass('rowActive');
            setTimeout(function () {
                //alert('delay');
                var lists = $(s.GetRow(e.visibleIndex)).find('.floatedBtnArea a');
                //$(s.GetRow(e.visibleIndex)).find('.floatedBtnArea a .ico').css({'opacity': '1'});
                //$(s.GetRow(e.visibleIndex)).find('.floatedBtnArea a').each(function (e) {
                //    setTimeout(function () {
                //        $(this).fadeIn();
                //    }, 100);
                //});    
                $.each(lists, function (index, value) {
                    //console.log(index);
                    //console.log(value);
                    setTimeout(function () {
                        console.log(value);
                        $(value).css({ 'opacity': '1' });
                    }, 100);
                });
            }, 200);
        }

        function AllCustCheck() {
            if (document.getElementById("chkAll").checked == true) {
                clookup_Entity.SetEnabled(false);
            }
            else {
                clookup_Entity.SetEnabled(true);
            }
        }

        function AllProdCheck() {
            if (document.getElementById("chkAllProduct").checked == true) {
                clookup_Product.SetEnabled(false);
            }
            else {
                clookup_Product.SetEnabled(true);
            }
        }


        function ImportUpdatePopOpenProductStock() {
            $("#myImportModal").modal('show');
        }

        //function getDownloadTemplateSettings() {
        //    $("#myModal").modal('show');

        //}

        function getTemplateByStaten() {
            var StateId = cCmbState.GetValue();

            if (StateId != "") {


                var StateName = cCmbState.GetText();

                // alert($("#ddlYear").val());
                $.ajax({
                    type: "POST",
                    url: "/OMS/Management/Activities/CustSaleRateLock.aspx/DeleteSaleRateLock",
                    data: JSON.stringify({ "MonthID": StateId, "MonthName": StateName }),
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
                            if (msg.d == "-999") {
                                jAlert("Deleted Successfuly");
                                cGridSpecialPriceUpload.Refresh();
                                return false;
                            }
                        }
                    }
                });

            }
            else {
                jAlert("Please select State");

            }
        }

        function ChekprodUpload() {
            var filename = $("#fileprod").val();
            if ($('#fileprod').get(0).files.length === 0) {
                jAlert("No files selected.");
                return false;
            }
            else {
                var extension = filename.replace(/^.*\./, '');
                switch (extension) {
                    case 'xls':
                    case 'xlsx':
                        return true;
                    default:
                        // Cancel the form submission
                        jAlert('Only excel file require.');
                        return false;
                }
            }
        }

        function DownLoadFormat() {
            debugger;
            window.location.href = "/Ajax/getExportProductRate?State=" + cCmbState.GetValue();
            //$.ajax({
            //    type: "post",
            //    url: "/OMS/Management/Activities/CustSaleRateLock.aspx/download",
            //    data: JSON.stringify({ "State": cCmbState.GetValue() }),
            //    dataType: "json",
            //    contentType: "application/json; charset=utf-8",
            //    global: false,
            //    async: false,
            //    success: function (msg) {
            //        if (msg.d) {
            //            //var url = "@Url.Action("getExportPJP", "PJPDetailsList")";
            //            //window.location.href = url;
            //        }
            //    },
            //    error:function (er) {

            //    }
            //});
        }

    </script>

    <script>
        function OpenProductRateLogModal() {
            cgridproducts.PerformCallback('');
            $("#RateImportLogModal").modal('show');
        }

        function PopupproductHide() {

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="breadCumb">
        <span>
            <label id="lblheading">Special Price Upload</label>
        </span>
    </div>
    <div id="divcross" runat="server" class="crossBtn hide" style="display: none; margin-left: 50px;"><a href="#" onclick="cancel()"><i class="fa fa-times"></i></a></div>

    <div class="container clearfix backBox mt-5 mb-4 py-3">
        <div class="py-3">
            <div class="clearfix">
                <div class="dis-flex">
                    <div id="divAddButton">
                        <a href="javascript:void(0);" onclick="OnAddButtonClick()" class="btn btn-success btn-radius"><span><u>A</u>dd New</span> </a>
                    </div>
                    <div id="divImportButton">
                        <asp:Button ID="btndownload" runat="server" CssClass="btn btn-info" OnClick="btndownload_Click" Text="Download Format" UseSubmitBehavior="False" />
                        <button type="button" onclick="ImportUpdatePopOpenProductStock();" class="btn btn-warning">Import (Add/Update)</button>
                        <button type="button" class="btn btn-view-log btn-radius " data-toggle="modal" data-target="#modalSS" id="btnViewLog" onclick="ViewLogData();" >View Log</button>

                    </div>
                    <div id="TblSearch" class="dis-flex">
                        <label>Product(s)</label>
                        <dxe:ASPxButtonEdit ID="txtProduct" runat="server" ReadOnly="true" ClientInstanceName="ctxtProductName">
                            <Buttons>
                                <dxe:EditButton>
                                </dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s,e){ProductButnClick();}" KeyDown="ProductKeyDown" />
                        </dxe:ASPxButtonEdit>
                        <asp:HiddenField ID="txtProduct_hidden" runat="server" />
                        <a href="javascript:void(0);" onclick="ShowData()" class="btn btn-show"><span>Show Data</span> </a>
                    </div>


                </div>
            </div>
        </div>
        <div class="clear"></div>
        <div id="view" class="relative">
            <dxe:ASPxGridView ID="GridSpecialPriceUpload" runat="server" AutoGenerateColumns="False" SettingsBehavior-AllowSort="true"
                ClientInstanceName="cGridSpecialPriceUpload" KeyFieldName="SPECIALPRICEID" Width="100%" Settings-HorizontalScrollBarMode="Auto"
                DataSourceID="EntityServerModeDataSource" SettingsDataSecurity-AllowEdit="false" SettingsDataSecurity-AllowInsert="false" SettingsDataSecurity-AllowDelete="false" OnCustomCallback="GridSaleRate_CustomCallback">

                <SettingsSearchPanel Visible="True" Delay="5000" />
                <ClientSideEvents CustomButtonClick="SpecialPriceCustomButtonClick" RowClick="gridRowclick" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxe:GridViewDataTextColumn Visible="False" FieldName="SPECIALPRICEID" SortOrder="Descending">
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>

                    <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="BRANCH" Caption="Branch" Width="20%">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                   <%-- Rev 1.0--%>
                    <dxe:GridViewDataTextColumn VisibleIndex="2" FieldName="Employee_Name" Caption="Employee" Width="30%">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                   <%--  Rev 1.0 End--%>
                    <dxe:GridViewDataTextColumn VisibleIndex="3" FieldName="PRODUCTCODE" Caption="Item Code" Width="25%">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="4" FieldName="PRODUCTNAME" Caption="Item Name" Width="30%">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="5" FieldName="SPECIALPRICE" Caption="Spcial Price" Width="15%">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>

                    <dxe:GridViewCommandColumn VisibleIndex="6" Width="10%" ButtonType="Image" Caption="Actions" HeaderStyle-HorizontalAlign="Center">
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
                ContextTypeName="ERPDataClassesDataContext" TableName="PRODUCTSPECIALPRICELIST" />
        </div>
        <div id="entry" style="display: none">
            <div style="background: #f5f4f3; padding: 17px 0; margin-bottom: 0px; border-radius: 4px; border: 1px solid #ccc;" class="clearfix">

                <div class="col-md-2">
                    <label>BRANCH</label>
                    <dxe:ASPxTextBox ID="txtBRANCH" ClientInstanceName="ctxtBRANCH" runat="server" ReadOnly="true">
                    </dxe:ASPxTextBox>
                </div>
                <%--  Rev 1.0 --%>
                <div class="col-md-2">
                    <label>DESIGNATION</label>
                    <div style="position: relative">
                        <dxe:ASPxTextBox ID="txtDesignation" ClientInstanceName="ctxtDesignation" runat="server" ReadOnly="true">
                        </dxe:ASPxTextBox>
                    </div>
                </div>
                 
                <div class="col-md-2">
                    <label>EMPLOYEE</label>
                    <div style="position: relative">
                        <dxe:ASPxTextBox ID="txtEditEmployee" ClientInstanceName="ctxtEditEmployee" runat="server" ReadOnly="true">
                        </dxe:ASPxTextBox>
                    </div>
                </div>
                 <%--  Rev 1.0 End--%>
                <div class="col-md-2">
                    <label>PRODUCT CODE</label>
                    <dxe:ASPxTextBox ID="txtPRODUCTCODE" ClientInstanceName="ctxtPRODUCTCODE" runat="server" ReadOnly="true">
                    </dxe:ASPxTextBox>

                </div>
                <div class="col-md-3">
                    <label>PRODUCT NAME</label>
                    <dxe:ASPxTextBox ID="txtPRODUCTNAME" ClientInstanceName="ctxtPRODUCTNAME" runat="server" ReadOnly="true">
                    </dxe:ASPxTextBox>
                </div>
                <div class="col-md-2">
                    <label>SPECIAL PRICE</label>
                    <dxe:ASPxTextBox ID="txtSPECIALPRICE" ClientInstanceName="ctxtSPECIALPRICE" runat="server" TabIndex="5">
                        <MaskSettings Mask="&lt;0..999999999&gt;.&lt;00..99&gt;" AllowMouseWheel="false" />
                    </dxe:ASPxTextBox>
                    <span id="MandatorysSPECIALPRICE" class="fa fa-exclamation-circle iconRed" style="color: red; position: absolute; display: none; right: -11px; top: 24px;"
                        title="Mandatory"></span>
                </div>
                <div class="clearfix"></div>

            </div>
            <div class="clearfix"></div>
            <div style="padding: 15px 10px 10px 0px;">
                <dxe:ASPxButton ID="btnSaveRecords" TabIndex="7" ClientInstanceName="cbtnSaveRecords" runat="server" AutoPostBack="False" Text="S&#818;ave" CssClass="btn btn-primary" UseSubmitBehavior="False">
                    <ClientSideEvents Click="function(s, e) {SaveButtonClick('Insert');}" />
                </dxe:ASPxButton>
                <dxe:ASPxButton ID="btncancel" TabIndex="8" ClientInstanceName="cbtncancel" runat="server" AutoPostBack="False" Text="C&#818;ancel" CssClass="btn btn-danger" UseSubmitBehavior="False">
                    <ClientSideEvents Click="function(s, e) {cancel();}" />
                </dxe:ASPxButton>

            </div>
        </div>

        <div id="AddSPECIALPRICE" style="display: none">
            <div style="background: #f5f4f3; padding: 17px 0; margin-bottom: 0px; border-radius: 4px; border: 1px solid #ccc;" class="clearfix">

                <div class="col-md-2 h-branch-select">
                    <label>BRANCH</label>
                    <asp:DropDownList ID="ddlBRANCH" runat="server" CssClass="sml" Width="100%" DataTextField="branch_description" DataValueField="branch_id"></asp:DropDownList>
                    <span id="AddMandatorysBRANCH" class="fa fa-exclamation-circle iconRed" style="color: red; position: absolute; display: none; right: -11px; top: 24px;"
                        title="Mandatory"></span>
                </div>
                <%--  Rev 1.0 --%>
                <div class="col-md-3  h-branch-select">
                    <label>Designation</label>
                    <div style="position: relative">
                        <asp:DropDownList ID="cmbDesg" runat="server" Width="100%" DataTextField="deg_designation" DataValueField="deg_id">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <label>Employee(s)</label>
                    <div style="position: relative">
                        <dxe:ASPxButtonEdit ID="txtEmployee" runat="server" ReadOnly="true" ClientInstanceName="ctxtEmployee">
                            <Buttons>
                                <dxe:EditButton>
                                </dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s,e){EmployeeButnClick();}" KeyDown="EmployeebtnKeyDown" />
                        </dxe:ASPxButtonEdit>
                        <asp:HiddenField ID="txtEmployee_hidden" runat="server" />
                    </div>
                </div>
                <%--  Rev 1.0 End--%>
                <div class="col-md-2">
                    <label>Product(s)</label>
                    <div style="position: relative">
                        <dxe:ASPxButtonEdit ID="txtProductNameAdd" runat="server" ReadOnly="true" ClientInstanceName="ctxtProductNameAdd">
                            <Buttons>
                                <dxe:EditButton>
                                </dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s,e){AddProductButnClick();}" KeyDown="AddProductKeyDown" />
                        </dxe:ASPxButtonEdit>
                        <span id="AddMandatorysProduct" class="fa fa-exclamation-circle iconRed" style="color: red; position: absolute; display: none; right: -11px; top: 24px;"
                            title="Mandatory"></span>
                    </div>

                </div>
                <div class="col-md-2">
                    <label>SPECIAL PRICE</label>
                    <dxe:ASPxTextBox ID="txtSPECIALPRICEAdd" ClientInstanceName="ctxtSPECIALPRICEAdd" runat="server">
                        <MaskSettings Mask="&lt;0..999999999&gt;.&lt;00..99&gt;" AllowMouseWheel="false" />
                    </dxe:ASPxTextBox>
                    <span id="AddMandatorysSPECIALPRICE" class="fa fa-exclamation-circle iconRed" style="color: red; position: absolute; display: none; right: -11px; top: 24px;"
                        title="Mandatory"></span>
                </div>
                <div class="clearfix"></div>

            </div>
            <div class="clearfix"></div>
            <div style="padding: 15px 10px 10px 0px;">
                <dxe:ASPxButton ID="btnSaveRecordsAdd" ClientInstanceName="cbtnSaveRecordsAdd" runat="server" AutoPostBack="False" Text="S&#818;ave" CssClass="btn btn-primary" UseSubmitBehavior="False">
                    <ClientSideEvents Click="function(s, e) {AddSaveButtonClick('Insert');}" />
                </dxe:ASPxButton>
                <dxe:ASPxButton ID="btncancelAdd" ClientInstanceName="cbtncancelAdd" runat="server" AutoPostBack="False" Text="C&#818;ancel" CssClass="btn btn-danger" UseSubmitBehavior="False">
                    <ClientSideEvents Click="function(s, e) {cancelAdd();}" />
                </dxe:ASPxButton>

            </div>
        </div>
    </div>

    <dxe:ASPxCallbackPanel runat="server" ID="CallbackPanel" ClientInstanceName="cCallbackPanel" OnCallback="CallbackPanel_Callback">
        <PanelCollection>
            <dxe:PanelContent runat="server">
            </dxe:PanelContent>
        </PanelCollection>
        <ClientSideEvents EndCallback="CallbackPanelEndCall" />
    </dxe:ASPxCallbackPanel>
    <asp:HiddenField ID="hfIsFilter" runat="server" />
    <asp:HiddenField ID="hFilterType" runat="server" />
    <asp:HiddenField ID="hdnCustId" runat="server" />
    <asp:HiddenField ID="hdnProdId" runat="server" />
    <asp:HiddenField ID="HiddenSPECIALPRICEID" runat="server" />
    <asp:HiddenField ID="HiddenProductID" runat="server" />
    <asp:HiddenField ID="Hiddenvalidfrom" runat="server" />
    <asp:HiddenField ID="Hiddenvalidupto" runat="server" />

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
                                <th>Product Code</th>
                                <th>Product Name</th>
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

    <div class="modal fade" id="AddProductModel" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Product Search</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="Addprodkeydown(event)" id="txtAddProdSearch" class="form-control" autofocus width="100%" placeholder="Search By Product Name or Description" />

                    <div id="AddProductTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Product Code</th>
                                <th>Product Name</th>

                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" id="btnAddSaveProduct" class="btnOkformultiselection btn btn-success" data-dismiss="modal" onclick="OKPopup('ProductSource')">OK</button>--%>

                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <div class="modal fade" id="modalSS" role="dialog">
        <div class="modal-dialog fullWidth">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Special Price Upload Log</h4>
                </div>
                <div class="modal-body">
                    <dxe:ASPxGridView ID="GvImportDetailsSearch" runat="server" AutoGenerateColumns="False" SettingsBehavior-AllowSort="true"
                        ClientInstanceName="cGvImportDetailsSearch" KeyFieldName="LOGID" Width="100%" OnDataBinding="GvImportDetailsSearch_DataBinding" Settings-VerticalScrollBarMode="Auto" Settings-VerticalScrollableHeight="400">

                        <SettingsBehavior ConfirmDelete="false" ColumnResizeMode="NextColumn" />
                        <Styles>
                            <Header SortingImageSpacing="5px" ImageSpacing="5px"></Header>
                            <FocusedRow HorizontalAlign="Left" VerticalAlign="Top" CssClass="gridselectrow"></FocusedRow>
                            <LoadingPanel ImageSpacing="10px"></LoadingPanel>
                            <FocusedGroupRow CssClass="gridselectrow"></FocusedGroupRow>
                            <Footer CssClass="gridfooter"></Footer>
                        </Styles>
                        <Columns>
                            <dxe:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="LOGID" Caption="LogID" SortOrder="Descending">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="CREATED_ON" Caption="Date" Width="10%">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="sProducts_Code" Caption="Product Code" Width="10%">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="2" FieldName="LOOPCOUNTER" Caption="Row Number" Width="13%">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="3" FieldName="sProducts_Name" Width="8%" Caption="Product Name">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="4" FieldName="FILENAME" Width="14%" Caption="File Name">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="5" FieldName="DESCRIPTION" Caption="Description" Width="10%" Settings-AllowAutoFilter="False">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="5" FieldName="STATUS" Caption="Status" Width="14%" Settings-AllowAutoFilter="False">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                            </dxe:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowGroupPanel="True" ShowStatusBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="true" />
                        <SettingsSearchPanel Visible="false" />
                        <SettingsPager NumericButtonCount="200" PageSize="200" ShowSeparators="True" Mode="ShowPager">
                            <PageSizeItemSettings Visible="true" ShowAllItem="false" Items="200,400,600" />
                            <FirstPageButton Visible="True">
                            </FirstPageButton>
                            <LastPageButton Visible="True">
                            </LastPageButton>
                        </SettingsPager>
                    </dxe:ASPxGridView>

                </div>

            </div>
        </div>
    </div>


    <div id="myImportModal" class="modal fade pmsModal w30" data-backdrop="static" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" onclick="close()">&times;</button>
                    <h4 class="modal-title">Import Excel Template For Special Price Upload</h4>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="">

                            <div class="row">
                                <div class="col-md-12">
                                    <div id="divproduct">
                                        <label class="uplabel">Select File to Import (Add/Update)</label>
                                        <div>
                                            <%--<input type="file" id="fileprod" accept=".xls,.xlsx">--%>
                                            <asp:FileUpload runat="server" ID="fileprod" accept=".xls,.xlsx" />
                                        </div>
                                        <div class="pTop10  mTop5" style="margin-top: 10px;">
                                            <%--<input type="submit" value="Import (Add/Update)" onclick="return ChekEmpSettingsUpload();" id="btnimportxls" class="btn btn-primary">OnClientClick="return ChekprodUpload();"--%>
                                        </div>
                                        <asp:Button runat="server" ID="btnimportxls" OnClick="ImportExcel" Text="Import (Add/Update)"  CssClass="btn btn-primary" UseSubmitBehavior="false" />
                                    
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
    </div>

    <div class="modal fade pmsModal w80 " id="EmployeeModel" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Employee Search</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="Employeekeydown(event)" id="txtEmployeeSearch" class="form-control" autofocus width="100%" placeholder="Search By Employee Code" />

                    <div id="EmployeeTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Employee Name</th>
                                <th>Employee Code</th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSaveEmployee" class="btnOkformultiselection btn btn-success" data-dismiss="modal" onclick="OKPopup('EmployeeSource')">OK</button>
                    <button type="button" id="btnCloseEmployee" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>




</asp:Content>
