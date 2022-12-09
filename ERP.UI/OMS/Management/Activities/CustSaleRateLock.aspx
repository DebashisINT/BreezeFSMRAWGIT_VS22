<%@ Page Title="" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" CodeBehind="CustSaleRateLock.aspx.cs" Inherits="ERP.OMS.Management.Activities.CustSaleRateLock" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Activities/CSS/SearchPopup.css" rel="stylesheet" />
    <script src="../Activities/JS/SearchPopup.js"></script>
    <style>
        .w40 .modal-dialog {
            width: 40%;
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
    </style>
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
        function ProductButnClick(s, e) {
            $("#ProductTable").empty();
            var html = "<table border='1' width='100%' class='dynamicPopupTbl'><tr class='HeaderStyle'><th style='display:none'>id</th><th>Product Name</th><th>Product Description</th><th>Min Sale Price</th></tr></table>";
            $("#ProductTable").html(html);
            setTimeout(function () { $("#txtProdSearch").focus(); }, 500);
            $('#txtProdSearch').val('');
            //shouldCheck = 1;
            //$('#mainActMsg').hide();
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
                HeaderCaption.push("Product Name");
                HeaderCaption.push("Product Description");
                HeaderCaption.push("Min Sale Price");
                if ($("#txtProdSearch").val() != '') {
                    callonServer("/OMS/Management/Activities/CustSaleRateLock.aspx/GetProduct", OtherDetails, "ProductTable", HeaderCaption, "ProdIndex", "SetProduct");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[ProdIndex=0]"))
                    $("input[ProdIndex=0]").focus();
            }

        }
        function SetProduct(Id, Name) {
            if (Id) {
                $('#ProductModel').modal('hide');
                SetProdNametxt(Id, Name);
            }
        }
        function SetProdNametxt(id, name) {
            ctxtMinSalePrice.SetValue("0.00");
            ctxtDiscount.SetValue("0.00");
            ctxtAmount.SetValue("0.00");
            ctxtProductName.SetText(name);
            var varproductid = id.split("||@||")[0];
            var minsaleprice = id.split("||@||")[1];
            document.getElementById('hdnProdId').value = varproductid;
            ctxtDiscount.Focus();
            //ctxtMinSalePrice.SetText = "90";
            ctxtMinSalePrice.SetValue(minsaleprice);
            $("#MandatorysProductName").hide();
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
        /****************Save, get, update, Other ************/
        $(document).ready(function () {
            cbtnSaveRecords.Focus();
        });

        function SaveButtonClick(flag) {
            $("#MandatorysCustName").hide();
            $("#MandatorysProductName").hide();
            $("#MandatorysDiscount").hide();
            $("#MandatorysFromdt").hide();
            $("#MandatorysTodt").hide();
            $("#MandatorysAmount").hide();

            var product = [];
            var Entity = [];

            if (document.getElementById("chkAll").checked == false) {
                //if (ctxtCustName.GetText() == "") {
                //    $("#MandatorysCustName").show();
                //    ctxtCustName.Focus();
                //    return false;
                //}
                if (clookup_Entity.gridView.GetSelectedKeysOnPage() == null) {
                    clookup_Entity.gridView.Focus();
                    return false;
                }
                else {
                    $("#hdnCustId").val(clookup_Entity.gridView.GetSelectedKeysOnPage());
                    Entity = clookup_Entity.gridView.GetSelectedKeysOnPage();
                }
            }
            else {
                $("#hdnCustId").val(0);
                Entity.push(0);
            }

            if (document.getElementById("chkAllProduct").checked == false) {
                //if (ctxtProductName.GetText() == "") {
                //    $("#MandatorysProductName").show();
                //    ctxtProductName.Focus();
                //    return false;
                //}
                if (clookup_Product.gridView.GetSelectedKeysOnPage() == null) {
                    clookup_Product.gridView.Focus();
                    return false;
                }
                else {
                    product = clookup_Product.gridView.GetSelectedKeysOnPage();
                    $("#hdnProdId").val(clookup_Product.gridView.GetSelectedKeysOnPage());
                }
            }
            else {
                product.push(0);
                $("#hdnProdId").val(0);
            }
            //if (ctxtDiscount.GetValue() == "") {
            //    $("#MandatorysDiscount").show();
            //    ctxtDiscount.Focus();
            //    return false;
            //}
            if (ctxtAmount.GetValue() == "0.00") {
                $("#MandatorysAmount").show();
                ctxtAmount.Focus();
                return false;
            }

            if (cFormDate.GetValue() == "") {
                $("#MandatorysFromdt").show();
                cFormDate.Focus();
                return false;
            }
            if (cToDate.GetValue() == "") {
                $("#MandatorysTodt").show();
                cToDate.Focus();
                return false;
            }
            if (cbtnSaveRecords.GetText() == "Update") {
                flag = "update";
            }

            //"CustID": $.trim($("#hdnCustId").val()), "ProductID": $.trim($("#hdnProdId").val())

            $.ajax({
                type: "POST",
                url: "/OMS/Management/Activities/CustSaleRateLock.aspx/addSaleRateLock",
                data: JSON.stringify({
                    "SaleRateLockID": $.trim($("#HiddenSaleRateLockID").val()), "CustID": Entity, "ProductID": product, "DiscSalesPrice": ctxtAmount.GetValue(),
                    "MinSalePrice": ctxtMinSalePrice.GetValue(), "discount": ctxtDiscount.GetValue(), "fromdt": cFormDate.GetDate(), "todate": cToDate.GetDate(),
                    "action": flag, "FixRate": ctxtFixRate.GetValue(), "SCHEME": ctxtScheme.GetValue()
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                global: false,
                async: false,
                success: function (msg) {
                    if (msg.d) {
                        if (flag == "Insert") {
                            if (msg.d == "-12") {
                                jAlert("From date cannot be greater than to date");
                                $("#MandatorysFromdt").show();
                                return false;
                            }
                            if (msg.d == "-11") {
                                jAlert("Product already is in sale");
                                return false;
                            }
                            if (msg.d == "-13") {
                                jAlert("From date and to date is same");
                                return false;
                            }
                            else {
                                jAlert("Added Successfully");
                                $("#entry").hide();
                                $("#view").show();
                                $("#lblheading").html("Sale Rate Lock");
                                $("#divAddButton").show();
                                $("#divcross").hide();
                                clear();
                                cGridSaleRate.Refresh();
                                return false;
                            }
                        }
                        if (flag == "update") {
                            if (msg.d == "-12") {
                                jAlert("From date cannot be greater than to date");
                                $("#MandatorysFromdt").show();
                                return false;
                            }
                            if (msg.d == "-11") {
                                jAlert("Product already is in sale");
                                return false;
                            }
                            else {
                                $("#entry").hide();
                                $("#view").show();
                                $("#lblheading").html("Sale Rate Lock");
                                $("#divAddButton").show();
                                $("#divcross").hide();
                                cGridSaleRate.Refresh();
                                $("#txtCustName_B0").show();
                                clear();
                                return false;
                            }
                        }
                    }
                },
                error: function (response) {
                    console.log(response);
                }
            });

        }

        function SaleRateCustomButtonClick(s, e) {
            var id = s.GetRowKey(e.visibleIndex);
            if (e.buttonID == 'CustomBtnEdit') {
                if (id != "") {
                    document.getElementById('HiddenSaleRateLockID').value = id;
                    $.ajax({
                        type: "POST",
                        url: "/OMS/Management/Activities/CustSaleRateLock.aspx/GetSaleRateLock",
                        data: JSON.stringify({ "SaleRateLockID": id }),
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
                            url: "/OMS/Management/Activities/CustSaleRateLock.aspx/DeleteSaleRateLock",
                            data: JSON.stringify({ "SaleRateLockID": id }),
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
                                        cGridSaleRate.Refresh();
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
                if (data.d[i].IsInUse == "1") {
                    jAlert("Already is in used.Unable to delete");
                    return false;
                }

                if (data.d[i].CustomerID == "0") {
                    clookup_Entity.SetEnabled(false);
                    document.getElementById("chkAll").checked = true;
                }
                else {
                    clookup_Entity.SetValue(data.d[i].CustomerID);
                    document.getElementById("chkAll").checked = false;
                }

                if (data.d[i].ProductID == "0") {
                    clookup_Product.SetEnabled(false);
                    document.getElementById("chkAllProduct").checked = true;
                }
                else {
                    clookup_Product.SetValue(data.d[i].ProductID);
                    document.getElementById("chkAllProduct").checked = false;
                }

                // ctxtCustName.SetText(data.d[i].CustomerName);
                document.getElementById('hdnCustId').value = data.d[i].CustomerID;
                //ctxtProductName.SetText(data.d[i].Products_Name);
                document.getElementById('hdnProdId').value = data.d[i].ProductID;
                ctxtMinSalePrice.SetValue(data.d[i].MinSalePrice);
                ctxtDiscount.SetValue(data.d[i].Disc);
                ctxtAmount.SetValue(data.d[i].DiscSalesPrice);
                ctxtFixRate.SetValue(data.d[i].FixRate);
                var frmdt = new Date(data.d[i].ValidFrom);
                cFormDate.SetDate(frmdt);
                var todt = new Date(data.d[i].ValidUpto);
                cToDate.SetDate(todt);
                // ctxtCustName.Focus();
                //ctxtCustName.SetButtonVisible(0);
                ctxtScheme.SetText(data.d[i].Scheme);
                $("#txtCustName_B0").hide();


            }
            $("#entry").show();
            $("#view").hide();
            $("#divAddButton").hide();
            $("#divcross").show();
            $("#lblheading").html("Modify Sale Rate Lock");
            cbtnSaveRecords.SetText("Update");
        }

        function OnAddButtonClick() {
            $("#divAddButton").hide();
            $("#entry").show();
            $("#view").hide();
            $("#lblheading").html("Add Sale Rate Lock");
            $("#divcross").show();
            //  ctxtCustName.Focus();
            //ctxtCustName.SetButtonVisible(1);
            $("#txtCustName_B0").show();
            clear();
        }

        function cancel() {
            clear();
            $("#entry").hide();
            $("#view").show();
            $("#lblheading").html("Sale Rate Lock");
            $("#divAddButton").show();
            cbtnSaveRecords.Focus()
            $("#divcross").hide();
            $("#txtCustName_B0").show();
        }

        function clear() {
            //ctxtCustName.SetText("");
            document.getElementById('hdnCustId').value = "";
            // ctxtProductName.SetText("");
            document.getElementById('hdnProdId').value = "";
            ctxtMinSalePrice.SetValue("0.00");
            ctxtDiscount.SetValue("0.00");
            ctxtAmount.SetValue("0.00");
            ctxtFixRate.SetValue("0.00");
            var frmdt = new Date($.trim($("#Hiddenvalidfrom").val()));
            cFormDate.SetDate(frmdt);
            var todt = new Date($.trim($("#Hiddenvalidupto").val()));
            cToDate.SetDate(todt);
            cbtnSaveRecords.SetText("S&#818;ave");
            $("#MandatorysCustName").hide();
            $("#MandatorysProductName").hide();
            $("#MandatorysDiscount").hide();
            $("#MandatorysFromdt").hide();
            $("#MandatorysTodt").hide();
            $("#MandatorysAmount").hide();
            clookup_Entity.SetEnabled(true);
            clookup_Product.SetEnabled(true);
            document.getElementById("chkAll").checked = false;
            document.getElementById("chkAllProduct").checked = false;
            ctxtScheme.SetText("");
            clookup_Entity.gridView.UnselectRows();
            clookup_Product.gridView.UnselectRows()
        }

        $(document).ready(function () {
            console.log('ready');
            $('.navbar-minimalize').click(function () {
                console.log('clicked');
                // cGridSaleRate.Refresh();
                cgridProductRate.Refresh();
            });
        });

        function gridRowclick(s, e) {
            $('#GridSaleRate').find('tr').removeClass('rowActive');
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

        function getDownloadTemplateSettings() {
            $("#myModal").modal('show');

        }

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
                                cGridSaleRate.Refresh();
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
            <label id="lblheading">Sale Rate Lock</label>
        </span>
    </div>
    <div id="divcross" runat="server" class="crossBtn hide" style="display: none; margin-left: 50px;"><a href="#" onclick="cancel()"><i class="fa fa-times"></i></a></div>

    <div class="container clearfix backBox mt-5 mb-4 py-3">
        <div id="TblSearch" class="py-3">
            <div class="clearfix">
                <div style="padding-right: 5px;" class="hide">
                    <span id="divAddButton">
                        <a href="javascript:void(0);" onclick="OnAddButtonClick()" class="btn btn-success btn-radius"><span><u>A</u>dd New</span> </a>
                    </span>
                </div>
                <div style="padding-right: 5px;">
                    <span id="divImportButton">
                        <button type="button" onclick="getDownloadTemplateSettings();" class="btn btn-info">Download Template</button>
                        <button type="button" onclick="ImportUpdatePopOpenProductStock();" class="btn btn-danger">Import (Add/Update)</button>
                        <%--<a href="javascript:void(0);" onclick="" class="btn btn-success btn-radius"><span><u>I</u>mport Data</span> </a>--%>
                    </span>
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <div id="view" class="relative">
            <dxe:ASPxGridView ID="GridSaleRate" runat="server" AutoGenerateColumns="False" SettingsBehavior-AllowSort="true" Visible="false"
                ClientInstanceName="cGridSaleRate" KeyFieldName="SaleRateLockID" Width="100%" Settings-HorizontalScrollBarMode="Auto"
                DataSourceID="EntityServerModeDataSource" SettingsDataSecurity-AllowEdit="false" SettingsDataSecurity-AllowInsert="false" SettingsDataSecurity-AllowDelete="false" OnCustomCallback="GridSaleRate_CustomCallback">

                <SettingsSearchPanel Visible="True" Delay="5000" />
                <ClientSideEvents CustomButtonClick="SaleRateCustomButtonClick" RowClick="gridRowclick" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxe:GridViewDataTextColumn Visible="False" FieldName="SaleRateLockID" SortOrder="Descending">
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="CustName" Caption="Customer Name" Width="150px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="2" FieldName="Products_Name" Caption="Product Name" Width="150px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="3" FieldName="SCHEME" Caption="Scheme" Width="150px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="4" FieldName="MinSalePrice" Caption="Min Sale Price">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="5" FieldName="Disc" Caption="Discount">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="6" FieldName="DiscSalesPrice" Caption="Amount">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <%-- <dxe:GridViewDataTextColumn VisibleIndex="7" FieldName="FixedRate" Caption="Fixed Rate">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>--%>
                    <dxe:GridViewDataTextColumn VisibleIndex="8" FieldName="ValidFrom" Caption="Valid From Date" Width="200px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>

                    <dxe:GridViewDataTextColumn VisibleIndex="9" FieldName="ValidUpto" Caption="Valid upto Date" Width="200px">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewCommandColumn VisibleIndex="10" Width="130px" ButtonType="Image" Caption="Actions" HeaderStyle-HorizontalAlign="Center">
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
                ContextTypeName="ERPDataClassesDataContext" TableName="v_SaleRateLockList" />





            <dxe:ASPxGridView ID="gridProductRate" runat="server" AutoGenerateColumns="False" SettingsBehavior-AllowSort="true"
                ClientInstanceName="cgridProductRate" KeyFieldName="ID" Width="100%" Settings-HorizontalScrollBarMode="Auto"
                DataSourceID="ProductRateServerModeDataSource" SettingsDataSecurity-AllowEdit="false" SettingsDataSecurity-AllowInsert="false" SettingsDataSecurity-AllowDelete="false" OnCustomCallback="gridProductRate_CustomCallback">

                <SettingsSearchPanel Visible="True" Delay="5000" />

                <SettingsBehavior ConfirmDelete="True" />
                <Columns>

                    <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="state" Caption="State" Width="30%">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="2" FieldName="sProducts_Description" Caption="Product Name" Width="40%">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>

                    <dxe:GridViewDataTextColumn VisibleIndex="4" FieldName="DD_PRICE" Caption="Distributor Rate" Width="15%">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn VisibleIndex="5" FieldName="SHOP_PRICE" Caption="Retail Rate" Width="15%">
                        <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                        <PropertiesTextEdit DisplayFormatString="0.00"></PropertiesTextEdit>
                        <Settings AllowAutoFilterTextInputTimer="False" />
                    </dxe:GridViewDataTextColumn>

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
            <dx:LinqServerModeDataSource ID="ProductRateServerModeDataSource" runat="server" OnSelecting="ProductRateServerModeDataSource_Selecting"
                ContextTypeName="ERPDataClassesDataContext" TableName="v_ProductRate" />
        </div>
        <div id="entry" style="display: none">
            <div style="background: #f5f4f3; padding: 17px 0; margin-bottom: 0px; border-radius: 4px; border: 1px solid #ccc;" class="clearfix">
                <div class="col-md-2">
                    <label>Entity </label>
                    &nbsp;
                    <input type="checkbox" id="chkAll" onchange="AllCustCheck()" />
                    All
                   <%-- <dxe:ASPxButtonEdit ID="txtCustName" runat="server" ReadOnly="true" ClientInstanceName="ctxtCustName" TabIndex="1">
                        <Buttons>
                            <dxe:EditButton>
                            </dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s,e){CustomerButnClick();}" KeyDown="function(s,e){CustomerKeyDown(s,e);}" />
                    </dxe:ASPxButtonEdit>

                    <span id="MandatorysCustName" class="fa fa-exclamation-circle iconRed" style="color: red; position: absolute; display: none; right: -11px; top: 24px;"
                        title="Mandatory"></span>--%>
                    <dxe:ASPxGridLookup ID="lookup_Entity" runat="server" ClientInstanceName="clookup_Entity" DataSourceID="EntityServerModeData" SelectionMode="Multiple"
                        KeyFieldName="cnt_internalid" Width="100%" CheckBoxRowSelect="true" TextFormatString="{0}" AutoGenerateColumns="False" MultiTextSeparator=", ">
                        <Columns>
                            <dxe:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="60" Caption=" " />
                            <dxe:GridViewDataColumn FieldName="Name" Visible="true" VisibleIndex="1" Caption="Entity Name" Settings-AutoFilterCondition="Contains" Width="200px">
                            </dxe:GridViewDataColumn>
                            <dxe:GridViewDataColumn FieldName="uniquename" Visible="true" VisibleIndex="2" Caption="Unique Id" Settings-AutoFilterCondition="Contains" Width="200px">
                            </dxe:GridViewDataColumn>
                            <dxe:GridViewDataColumn FieldName="Billing" Visible="true" VisibleIndex="3" Caption="Address" Settings-AutoFilterCondition="Contains" Width="200px">
                            </dxe:GridViewDataColumn>
                            <dxe:GridViewDataColumn FieldName="Type" Visible="true" VisibleIndex="3" Caption="Type" Settings-AutoFilterCondition="Contains" Width="200px">
                            </dxe:GridViewDataColumn>
                        </Columns>
                        <GridViewProperties Settings-VerticalScrollBarMode="Auto">
                            <Templates>
                                <StatusBar>
                                    <table class="OptionsTable" style="float: right">
                                        <tr>
                                            <td>
                                                <dxe:ASPxButton ID="Close" runat="server" AutoPostBack="false" Text="Close" />
                                                <%--ClientSideEvents-Click="CloseGridQuotationLookup"--%>
                                            </td>
                                        </tr>
                                    </table>
                                </StatusBar>
                            </Templates>

                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="false"></SettingsBehavior>
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowStatusBar="Visible" UseFixedTableLayout="true" />
                        </GridViewProperties>
                        <ClientSideEvents GotFocus="function(s,e){clookup_Entity.ShowDropDown();}" />
                        <%--LostFocus="Project_LostFocus" ValueChanged="ProjectValueChange"--%>

                        <ClearButton DisplayMode="Always">
                        </ClearButton>
                    </dxe:ASPxGridLookup>
                    <dx:LinqServerModeDataSource ID="EntityServerModeData" runat="server" OnSelecting="EntityServerModeData_Selecting"
                        ContextTypeName="ERPDataClassesDataContext" TableName="v_SaleRateLock_customerDetails" />
                </div>
                <div class="col-md-2">
                    <label>Product</label>
                    &nbsp;
                    <input type="checkbox" id="chkAllProduct" onchange="AllProdCheck()" />
                    All
                   <%--  <dxe:ASPxButtonEdit ID="txtProductName" runat="server" ReadOnly="true" ClientInstanceName="ctxtProductName" TabIndex="2">
                        <Buttons>
                            <dxe:EditButton>
                            </dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s,e){ProductButnClick();}" KeyDown="function(s,e){ProductKeyDown(s,e);}" />
                    </dxe:ASPxButtonEdit>
                    <span id="MandatorysProductName" class="fa fa-exclamation-circle iconRed" style="color: red; position: absolute; display: none; right: -11px; top: 24px;"
                        title="Mandatory"></span>--%>

                    <dxe:ASPxGridLookup ID="lookup_Product" runat="server" ClientInstanceName="clookup_Product" DataSourceID="EntityServerModeDataProduct" SelectionMode="Multiple"
                        KeyFieldName="Products_ID" Width="100%" CheckBoxRowSelect="true" TextFormatString="{0}" AutoGenerateColumns="False" MultiTextSeparator=", ">
                        <Columns>
                            <dxe:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="60" Caption=" " />
                            <dxe:GridViewDataColumn FieldName="Products_Name" Visible="true" VisibleIndex="1" Caption="Product Name" Settings-AutoFilterCondition="Contains" Width="200px">
                            </dxe:GridViewDataColumn>
                            <dxe:GridViewDataColumn FieldName="Products_Description" Visible="true" VisibleIndex="2" Caption="Product Description" Settings-AutoFilterCondition="Contains" Width="200px">
                            </dxe:GridViewDataColumn>
                            <dxe:GridViewDataColumn FieldName="sProduct_MinSalePrice" Visible="true" VisibleIndex="3" Caption="Min Sale Price" Settings-AutoFilterCondition="Contains" Width="200px">
                            </dxe:GridViewDataColumn>
                        </Columns>
                        <GridViewProperties Settings-VerticalScrollBarMode="Auto">
                            <Templates>
                                <StatusBar>
                                    <table class="OptionsTable" style="float: right">
                                        <tr>
                                            <td>
                                                <dxe:ASPxButton ID="Close" runat="server" AutoPostBack="false" Text="Close" />
                                                <%--ClientSideEvents-Click="CloseGridQuotationLookup"--%>
                                            </td>
                                        </tr>
                                    </table>
                                </StatusBar>
                            </Templates>

                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="false"></SettingsBehavior>
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowStatusBar="Visible" UseFixedTableLayout="true" />
                        </GridViewProperties>
                        <ClientSideEvents GotFocus="function(s,e){clookup_Product.ShowDropDown();}" />
                        <%--LostFocus="Project_LostFocus" ValueChanged="ProjectValueChange"--%>

                        <ClearButton DisplayMode="Always">
                        </ClearButton>
                    </dxe:ASPxGridLookup>
                    <dx:LinqServerModeDataSource ID="EntityServerModeDataProduct" runat="server" OnSelecting="EntityServerModeDataProduct_Selecting"
                        ContextTypeName="ERPDataClassesDataContext" TableName="v_Product_SaleRateLock" />
                </div>
                <div class="col-md-2">
                    <label>Min Sale Price</label>
                    <dxe:ASPxTextBox ID="txtMinSalePrice" ClientInstanceName="ctxtMinSalePrice" runat="server" ReadOnly="true">
                        <MaskSettings Mask="&lt;0..999999999&gt;.&lt;00..99&gt;" AllowMouseWheel="false" />
                    </dxe:ASPxTextBox>
                </div>
                <div class="col-md-2">
                    <label>Discount (%)</label>
                    <dxe:ASPxSpinEdit ID="txtDiscount" ClientInstanceName="ctxtDiscount" runat="server" ShowOutOfRangeWarning="false" SpinButtons-ClientVisible="false"
                        TabIndex="3" MaxValue="100" AllowMouseWheel="false" EditFormatString="0.00" DisplayFormatString="0.00" DecimalPlaces="2" NumberType="Float" MinValue="0.00">

                        <%-- <MaskSettings Mask="&lt;0..999&gt;.&lt;00..99&gt;" AllowMouseWheel="false"  />--%>
                        <ValidationSettings Display="None"></ValidationSettings>
                        <ClientSideEvents LostFocus="AmountCalculate" />
                    </dxe:ASPxSpinEdit>
                    <span id="MandatorysDiscount" class="fa fa-exclamation-circle iconRed" style="color: red; position: absolute; display: none; right: -13px; top: 24px;"
                        title="Mandatory"></span>
                </div>
                <div class="col-md-2">
                    <label>Amount</label>
                    <dxe:ASPxTextBox ID="txtAmount" ClientInstanceName="ctxtAmount" runat="server" TabIndex="4">
                        <MaskSettings Mask="&lt;0..999999999&gt;.&lt;00..99&gt;" AllowMouseWheel="false" />
                        <ClientSideEvents LostFocus="PercentageCalculate" />
                    </dxe:ASPxTextBox>
                    <span id="MandatorysAmount" class="fa fa-exclamation-circle iconRed" style="color: red; position: absolute; display: none; right: -11px; top: 24px;"
                        title="Mandatory"></span>
                </div>
                <div class="col-md-2 hide">
                    <label>Fix Rate</label>
                    <dxe:ASPxTextBox ID="txtFixRate" ClientInstanceName="ctxtFixRate" runat="server" TabIndex="5">
                        <MaskSettings Mask="&lt;0..999999999&gt;.&lt;00..99&gt;" AllowMouseWheel="false" />
                        <%--<ClientSideEvents LostFocus="PercentageCalculate" />--%>
                    </dxe:ASPxTextBox>
                    <span id="MandatorysFixRate" class="fa fa-exclamation-circle iconRed" style="color: red; position: absolute; display: none; right: -11px; top: 24px;"
                        title="Mandatory"></span>
                </div>
                <div class="clearfix"></div>
                <div class="col-md-2 lblmTop8">
                    <label>From Date</label>
                    <dxe:ASPxDateEdit ID="Fromdt" runat="server" EditFormat="Custom" EditFormatString="dd-MM-yyyy HH:mm" ClientInstanceName="cFormDate"
                        Width="100%" TabIndex="5" DisplayFormatString="dd-MM-yyyy HH:mm" UseMaskBehavior="True">
                        <TimeSectionProperties Visible="true"></TimeSectionProperties>
                        <ButtonStyle Width="13px">
                        </ButtonStyle>
                        <ClientSideEvents GotFocus="function(s,e){cFormDate.ShowDropDown();}" />
                    </dxe:ASPxDateEdit>
                    <span id="MandatorysFromdt" class="fa fa-exclamation-circle iconRed" style="color: red; position: absolute; display: none; right: -11px; top: 24px;"
                        title="Mandatory"></span>
                </div>
                <div class="col-md-2 lblmTop8">
                    <label>To Date</label>
                    <dxe:ASPxDateEdit ID="ToDate" runat="server" EditFormat="DateTime" EditFormatString="dd-MM-yyyy HH:mm" ClientInstanceName="cToDate"
                        Width="100%" TabIndex="6" DisplayFormatString="dd-MM-yyyy HH:mm" UseMaskBehavior="True">
                        <TimeSectionProperties Visible="true"></TimeSectionProperties>
                        <ButtonStyle Width="13px">
                        </ButtonStyle>
                        <ClientSideEvents GotFocus="function(s,e){cToDate.ShowDropDown();}" />
                    </dxe:ASPxDateEdit>
                    <span id="MandatorysTodt" class="fa fa-exclamation-circle iconRed" style="color: red; position: absolute; display: none; right: -11px; top: 24px;"
                        title="Mandatory"></span>
                </div>

                <div class="col-md-2 lblmTop8">
                    <label>Scheme</label>
                    <dxe:ASPxTextBox ID="txtScheme" ClientInstanceName="ctxtScheme" runat="server" TabIndex="8">
                    </dxe:ASPxTextBox>
                    <span id="MandatorysScheme" class="fa fa-exclamation-circle iconRed" style="color: red; position: absolute; display: none; right: -11px; top: 24px;"
                        title="Mandatory"></span>
                </div>

            </div>
            <div class="clearfix"></div>
            <div style="padding: 15px 10px 10px 0px;">
                <dxe:ASPxButton ID="btnSaveRecords" TabIndex="7" ClientInstanceName="cbtnSaveRecords" runat="server" AutoPostBack="False" Text="S&#818;ave" CssClass="btn btn-primary" UseSubmitBehavior="False">
                    <ClientSideEvents Click="function(s, e) {SaveButtonClick('Insert');}" />
                </dxe:ASPxButton>
                <dxe:ASPxButton ID="btncancel" TabIndex="8" ClientInstanceName="cbtncancel" runat="server" AutoPostBack="False" Text="C&#818;ancel" CssClass="btn btn-primary" UseSubmitBehavior="False">
                    <ClientSideEvents Click="function(s, e) {cancel();}" />
                </dxe:ASPxButton>

            </div>
        </div>
    </div>



    <asp:HiddenField ID="hdnCustId" runat="server" />
    <asp:HiddenField ID="hdnProdId" runat="server" />
    <asp:HiddenField ID="HiddenSaleRateLockID" runat="server" />
    <asp:HiddenField ID="Hiddenvalidfrom" runat="server" />
    <asp:HiddenField ID="Hiddenvalidupto" runat="server" />
    <!--Customer Modal -->
    <div class="modal fade" id="CustModel" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Customer Search</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="Customerkeydown(event)" id="txtCustSearch" autofocus width="100%" placeholder="Search By Customer Name,Unique Id and Phone No." />

                    <div id="CustomerTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Customer Name</th>
                                <th>Unique Id</th>
                                <th>Address</th>
                                <th>Type</th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>
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
                    <input type="text" onkeydown="prodkeydown(event)" id="txtProdSearch" autofocus width="100%" placeholder="Search By Product Name or Description" />

                    <div id="ProductTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Product Name</th>
                                <th>Product Description</th>
                                <th>Min Sale Price</th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <div id="myImportModal" class="modal fade pmsModal w30" data-backdrop="static" role="dialog">
        <div class="modal-dialog ">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" onclick="close()">&times;</button>
                    <h4 class="modal-title">Import Excel Template For Product Stock</h4>
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
                                            <%--<input type="submit" value="Import (Add/Update)" onclick="return ChekEmpSettingsUpload();" id="btnimportxls" class="btn btn-primary">--%>
                                        </div>
                                        <asp:Button runat="server" ID="btnimportxls" OnClick="ImportExcel" Text="Import (Add/Update)" OnClientClick="return ChekprodUpload();" CssClass="btn btn-primary" />
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


    <div id="myModal" class="modal fade pmsModal w30" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Download Excel Template For Product Rate</h4>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="">
                            <div class="row">
                                <div>
                                    <div class="clearfix ">
                                        <div class="col-lg-12 lblmTop8">
                                            <label><b>State</b></label>
                                            <div class="iminentSpan">
                                                <dxe:ASPxComboBox ID="CmbState" ClientInstanceName="cCmbState" runat="server" ValueType="System.String"
                                                    Width="100%" EnableSynchronization="True" DropDownStyle="DropDown">
                                                    <%--<ClientSideEvents EndCallback="CmbState_EndCallback"></ClientSideEvents>--%>
                                                </dxe:ASPxComboBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 mTop5" style="margin-top: 10px;">
                                            <%--<button type="button" onclick="getTemplateByStaten();" class="btn btn-success mTop5">Download Now</button>     --%>
                                            <asp:Button ID="btndownload" runat="server" CssClass="btn btn-primary" OnClick="btndownload_Click" Text="Download Format" />
                                        </div>
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


    <div id="RateImportLogModal" class="modal fade pmsModal w70" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Product Rate Import Log</h4>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="">
                            <div class="row">
                                <div>
                                    <div class="clearfix ">
                                        <div class="col-lg-12">
                                            <dxe:ASPxGridView runat="server" KeyFieldName="SEQ" ClientInstanceName="cgridproducts" ID="grid_RateLog"
                                                Width="100%" SettingsBehavior-AllowSort="false" SettingsBehavior-AllowDragDrop="false" SettingsPager-Mode="ShowAllRecords"
                                                OnCustomCallback="ProductRateLog_Callback" OnDataBinding="grid_RateLog_DataBinding"
                                                Settings-ShowFooter="false" AutoGenerateColumns="False" Settings-VerticalScrollableHeight="300" Settings-VerticalScrollBarMode="Visible">
                                                <SettingsBehavior AllowDragDrop="False" AllowSort="False"></SettingsBehavior>
                                                <SettingsPager Visible="false"></SettingsPager>
                                                <Columns>
                                                    <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="STATE" Width="100" ReadOnly="true" Caption="State">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dxe:GridViewDataTextColumn>
                                                    <dxe:GridViewDataTextColumn VisibleIndex="2" FieldName="Code" ReadOnly="true" Caption="Product Code" Width="100">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dxe:GridViewDataTextColumn>
                                                    <dxe:GridViewDataTextColumn VisibleIndex="3" FieldName="Description" ReadOnly="true" Width="100" Caption="Product Description">
                                                        <Settings AutoFilterCondition="Contains" />
                                                         
                                                    </dxe:GridViewDataTextColumn>
                                                    <dxe:GridViewDataTextColumn Caption="Price to Distributor" FieldName="DistributorPrice" Width="70" VisibleIndex="4">
                                                        <PropertiesTextEdit DisplayFormatString="0.00">
                                                            <MaskSettings Mask="<0..999999999999>.<0..99>" AllowMouseWheel="false" />
                                                        </PropertiesTextEdit>
                                                    </dxe:GridViewDataTextColumn>
                                                    <dxe:GridViewDataTextColumn Caption="Price to Retailer" FieldName="RetailerPrice" Width="70" VisibleIndex="5">
                                                        <PropertiesTextEdit DisplayFormatString="0.00">
                                                            <MaskSettings Mask="<0..999999999999>.<0..99>" AllowMouseWheel="false" />
                                                        </PropertiesTextEdit>
                                                    </dxe:GridViewDataTextColumn>
                                                      <dxe:GridViewDataTextColumn VisibleIndex="6" FieldName="Status" ReadOnly="true" Width="100" Caption="Status">
                                                        <Settings AutoFilterCondition="Contains" />                                                         
                                                    </dxe:GridViewDataTextColumn>
                                                      <dxe:GridViewDataTextColumn VisibleIndex="7" FieldName="Reason" ReadOnly="true" Width="100" Caption="Reason">
                                                        <Settings AutoFilterCondition="Contains" />                                                         
                                                    </dxe:GridViewDataTextColumn>
                                                </Columns>
                                                <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                                                <SettingsDataSecurity AllowEdit="true" />
                                            </dxe:ASPxGridView>

                                            <dxe:ASPxGridViewExporter ID="exporter" runat="server" GridViewID="grid_RateLog" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
                                            </dxe:ASPxGridViewExporter>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary" OnChange="if(!AvailableExportOption()){return false;}" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="0">Export to</asp:ListItem>
                        <asp:ListItem Value="1">PDF</asp:ListItem>
                        <asp:ListItem Value="2">XLS</asp:ListItem>
                        <asp:ListItem Value="3">RTF</asp:ListItem>
                        <asp:ListItem Value="4">CSV</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
