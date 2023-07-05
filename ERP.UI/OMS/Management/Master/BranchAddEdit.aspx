<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                08-02-2023        2.0.39           Pallab              0025656 : Master module design modification
2.0                22-03-2023        2.0.39           Priti               0025745 :While click the Add button of Branch Master, it is taking some time to load the page & take the input    
====================================================== Revision History ==========================================================--%>

<%@ Page Title="Branches" Language="C#" AutoEventWireup="true" Inherits="ERP.OMS.Managemnent.Master.management_Master_BranchAddEdit" CodeBehind="BranchAddEdit.aspx.cs" MasterPageFile="~/OMS/MasterPage/ERP.Master" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Rev 2.0--%>
    <link href="../../../assests/css/custom/SearchPopup.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/fixedcolumns/3.3.0/js/dataTables.fixedColumns.min.js"></script>
    <script src="../Activities/JS/SearchPopupDatatable.js"></script>
    <%-- Rev 2.0 End--%>
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <script language="javascript" type="text/javascript">
        //Gstin Changes
        function fn_AllowonlyNumeric(s, e) {
            var theEvent = e.htmlEvent || window.event;
            var key = theEvent.keyCode || theEvent.which;
            var keychar = String.fromCharCode(key);
            if (key == 9 || key == 37 || key == 38 || key == 39 || key == 40 || key == 8) { //tab/ Left / Up / Right / Down Arrow, Backspace, Delete keys
                return;
            }
            var regex = /[0-9]/;

            if (!regex.test(keychar)) {
                theEvent.returnValue = false;
                if (theEvent.preventDefault)
                    theEvent.preventDefault();
            }
        }

        function Gstin2TextChanged(s, e) {

            if (!e.htmlEvent.ctrlKey) {
                if (e.htmlEvent.key != 'Control') {
                    s.SetText(s.GetText().toUpperCase());
                }
            }

        }
        //Code for UDF Control 
        function OpenUdf(s,e) {
            if (document.getElementById('IsUdfpresent').value == '0') {
                jAlert("UDF not define.");
            }
            else {
                var keyVal = document.getElementById('Keyval_internalId').value;
                var url = '/OMS/management/Master/frm_BranchUdfPopUp.aspx?Type=Br&&KeyVal_InternalID=' + keyVal;
                popup.SetContentUrl(url);
                popup.Show();
            }
           // return true;
        }

        function udfError() {
            jAlert('UDF is set as Mandatory. Please enter values.', 'Alert', function () { OpenUdf(s,e); });
        }
        // End Udf Code


        var pinCodeWithAreaId = [];
        $(document).ready(function () {
            ListBind();
            /*Code  Added  By Priti on 06122016 to use jquery Choosen for BranchHead*/
           /* Rev 2.0*/
            //ChangeSourceBranchHead();
            /* Rev 2.0 End*/
            //.............end........
            var cntry = document.getElementById('txtCountry_hidden').value;
            document.getElementById('txtCountry_hidden').value = "";

            //var Statery = document.getElementById('txtState_hidden').value;
            //    document.getElementById('txtState_hidden').value = "";

            // var cityry = document.getElementById('txtCity_hidden').value;
            //    document.getElementById('txtCity_hidden').value = "";
            setCountry(cntry);
            //setState(Statery);
            //setCity(cityry);
            setMainAccount(document.getElementById('hdlstMainAccount').value);

            $('#BranchHeadModel').on('shown.bs.modal', function () {
                setTimeout(function () {
                    $('#txtBranchHeadSearch').focus();
                }, 200);

            });

        });

        function ClientSaveClick(s, e) {
            var returnValue = true;
            document.getElementById('txtCountry_hidden').value = document.getElementById('lstCountry').value;
            document.getElementById('txtState_hidden').value = document.getElementById('lstState').value;
            document.getElementById('txtCity_hidden').value = document.getElementById('lstCity').value;
            document.getElementById('hdLstArea').value = document.getElementById('lstArea').value;
            document.getElementById('HdPin').value = document.getElementById('lstPin').value;
            /*Code  Added  By Priti on 06122016 to use jquery Choosen for BranchHead*/
            //Rev 2.0
           // document.getElementById('txtBranchHead_hidden').value = document.getElementById('lstBranchHead').value;
            //Rev 2.0 End
            returnValue = validateControl();

            return returnValue;

        }


        function validateControl() {
            var isValid = true;
            Page_ClientValidate();
            $('#invalidGst').css({ 'display': 'none' });
            var gst1 = ctxtGSTIN1.GetText().trim();
            var gst2 = ctxtGSTIN2.GetText().trim();
            var gst3 = ctxtGSTIN3.GetText().trim();

            if (gst1.length == 0 && gst2.length == 0 && gst3.length == 0) {
                isValid = true;
            }
            else {
                if (gst1.length != 2 || gst2.length != 10 || gst3.length != 3) {
                    $('#invalidGst').css({ 'display': 'block' });
                    isValid = false;
                }


                var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                var code = /([C,P,H,F,A,T,B,L,J,G])/;
                var code_chk = gst2.substring(3, 4);
                if (gst2.search(panPat) == -1) {
                    $('#invalidGst').css({ 'display': 'block' });
                    isValid = false;
                }
                if (code.test(code_chk) == false) {
                    $('#invalidGst').css({ 'display': 'block' });
                    isValid = false;
                }
            }





            return isValid;
        }


        function setCountry(obj) {
            if (obj) {
                var lstCntry = document.getElementById("lstCountry");

                for (var i = 0; i < lstCntry.options.length; i++) {
                    if (lstCntry.options[i].value == obj) {
                        lstCntry.options[i].selected = true;
                    }
                }
                $('#lstCountry').trigger("chosen:updated");
                onCountryChange();
            }
        }

        function setMainAccount(obj) {
            if (obj) {
                var lstMainAct = document.getElementById("lstMainAccount");

                for (var i = 0; i < lstMainAct.options.length; i++) {
                    if (lstMainAct.options[i].value == obj) {
                        lstMainAct.options[i].selected = true;
                    }
                }
                $('#lstMainAccount').trigger("chosen:updated");
                onlstMainAccountChange();
            }
        }


        function setState(obj) {
            if (obj) {
                var lstStae = document.getElementById("lstState");

                for (var i = 0; i < lstStae.options.length; i++) {
                    if (lstStae.options[i].value == obj) {
                        lstStae.options[i].selected = true;
                    }
                }
                $('#lstState').trigger("chosen:updated");
                onStateChange();
            }
        }
        function setCity(obj) {
            if (obj) {
                var lstCity = document.getElementById("lstCity");

                for (var i = 0; i < lstCity.options.length; i++) {
                    if (lstCity.options[i].value == obj) {
                        lstCity.options[i].selected = true;
                    }
                }
                $('#lstCity').trigger("chosen:updated");
                onCityChange();
            }
        }

        function setArea(obj) {
            if (obj) {
                var lstArea = document.getElementById("lstArea");

                for (var i = 0; i < lstArea.options.length; i++) {
                    if (lstArea.options[i].value == obj) {
                        lstArea.options[i].selected = true;
                    }
                }
                $('#lstArea').trigger("chosen:updated");

            }

        }
        function setPin(obj) {
            if (obj) {
                var lstPin = document.getElementById("lstPin");

                for (var i = 0; i < lstPin.options.length; i++) {
                    if (lstPin.options[i].value == obj) {
                        lstPin.options[i].selected = true;
                    }
                }
                $('#lstPin').trigger("chosen:updated");

            }
        }
        function onAreaChange() {
            if (document.getElementById('lstArea').value) {
                getPinCodeForArea(document.getElementById('lstArea').value);
            }
        }
        function getPinCodeForArea(obj) {

            var pinData = '';
            for (var i = 0; i < pinCodeWithAreaId.length; i++) {
                if (pinCodeWithAreaId[i].split('~')[0] == obj) {
                    console.log("pin code", pinCodeWithAreaId[i].split('~')[1]);
                    document.getElementById('txtPin').value = pinCodeWithAreaId[i].split('~')[1];
                }
            }

        }

        function onlstMainAccountChange() {
            document.getElementById('hdlstMainAccount').value = document.getElementById('lstMainAccount').value;
        }

        function onCountryChange() {
            var CountryId = "";
            if (document.getElementById('lstCountry').value) {
                CountryId = document.getElementById('lstCountry').value;
            } else {
                return;
            }
            var lState = $('select[id$=lstState]');
            var lCity = $('select[id$=lstCity]');
            var lArea = $('select[id$=lstArea]');
            var lPin = $('select[id$=lstPin]');
            lState.empty();
            lCity.empty();
            lArea.empty();
            lPin.empty();
            $('#lstCity').trigger("chosen:updated");
            $('#lstArea').trigger("chosen:updated");
            $('#lstPin').trigger("chosen:updated");
            $.ajax({
                type: "POST",
                url: "BranchAddEdit.aspx/GetStates",
                data: JSON.stringify({ CountryCode: CountryId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var list = msg.d;
                    var listItems = [];
                    if (list.length > 0) {

                        for (var i = 0; i < list.length; i++) {
                            var id = '';
                            var name = '';
                            id = list[i].split('|')[1];
                            name = list[i].split('|')[0];

                            listItems.push('<option value="' +
                                id + '">' + name
                                + '</option>');
                        }

                        $(lState).append(listItems.join(''));

                        $('#lstState').fadeIn();
                        $('#lstState').trigger("chosen:updated");
                        if (document.getElementById('txtState_hidden').value) {
                            var stateVal = document.getElementById('txtState_hidden').value;
                            document.getElementById('txtState_hidden').value = "";
                            setState(stateVal);
                        }
                    }
                    else {
                        $('#lstState').fadeIn();
                        $('#lstState').trigger("chosen:updated");
                    }
                }
            });
        }

        function onStateChange() {
            var StateId = "";
            if (document.getElementById('lstState').value) {
                StateId = document.getElementById('lstState').value;
            }
            else {
                return;
            }
            var lCity = $('select[id$=lstCity]');
            var lArea = $('select[id$=lstArea]');
            var lPin = $('select[id$=lstPin]');
            lArea.empty();
            lCity.empty();
            lPin.empty();
            $('#lstArea').trigger("chosen:updated");
            $('#lstPin').trigger("chosen:updated");
            $.ajax({
                type: "POST",
                url: "BranchAddEdit.aspx/GetCities",
                data: JSON.stringify({ StateCode: StateId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var list = msg.d;
                    var listItems = [];
                    if (list.length > 0) {

                        for (var i = 0; i < list.length; i++) {
                            var id = '';
                            var name = '';
                            id = list[i].split('|')[1];
                            name = list[i].split('|')[0];

                            listItems.push('<option value="' +
                                id + '">' + name
                                + '</option>');
                        }

                        $(lCity).append(listItems.join(''));

                        $('#lstCity').fadeIn();
                        $('#lstCity').trigger("chosen:updated");
                        if (document.getElementById('txtCity_hidden').value) {
                            var cityVal = document.getElementById('txtCity_hidden').value;
                            document.getElementById('txtCity_hidden').value = "";
                            setCity(cityVal);
                        }
                    }
                    else {
                        $('#lstCity').fadeIn();
                        $('#lstCity').trigger("chosen:updated");
                    }
                }
            });
        }

        function getPinList() {
            var CityId = "";
            if (document.getElementById('lstCity').value) {
                CityId = document.getElementById('lstCity').value;
            }
            else {
                return;
            }
            var lPin = $('select[id$=lstPin]');
            lPin.empty();
            $.ajax({
                type: "POST",
                url: "BranchAddEdit.aspx/GetPin",
                data: JSON.stringify({ CityCode: CityId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var list = msg.d;
                    var listItems = [];
                    if (list.length > 0) {

                        for (var i = 0; i < list.length; i++) {
                            var id = '';
                            var name = '';
                            id = list[i].split('|')[1];
                            name = list[i].split('|')[0];

                            listItems.push('<option value="' +
                                id + '">' + name
                                + '</option>');
                        }

                        $(lPin).append(listItems.join(''));

                        $('#lstPin').fadeIn();
                        $('#lstPin').trigger("chosen:updated");
                        if (document.getElementById('HdPin').value) {
                            setPin(document.getElementById('HdPin').value);
                        }

                    }
                    else {
                        $('#lstPin').fadeIn();
                        $('#lstPin').trigger("chosen:updated");
                    }
                }
            });
        }
        function onCityChange() {
            getPinList();

            var CityId = "";
            if (document.getElementById('lstCity').value) {
                CityId = document.getElementById('lstCity').value;
            }
            else {
                return;
            }
            var lArea = $('select[id$=lstArea]');
            lArea.empty();
            pinCodeWithAreaId = [];
            $.ajax({
                type: "POST",
                url: "BranchAddEdit.aspx/GetArea",
                data: JSON.stringify({ CityCode: CityId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var list = msg.d;
                    var listItems = [];
                    if (list.length > 0) {

                        for (var i = 0; i < list.length; i++) {
                            var id = '';
                            var name = '';
                            id = list[i].split('|')[1];
                            name = list[i].split('|')[0];
                            pin = list[i].split('|')[2];
                            listItems.push('<option value="' +
                                id + '">' + name
                                + '</option>');
                            pinCodeWithAreaId[i] = id + '~' + pin;
                        }

                        $(lArea).append(listItems.join(''));

                        $('#lstArea').fadeIn();
                        $('#lstArea').trigger("chosen:updated");
                        if (document.getElementById('hdLstArea').value) {
                            var areaVal = document.getElementById('hdLstArea').value;
                            document.getElementById('hdLstArea').value = "";
                            setArea(areaVal);
                        }
                    }
                    else {
                        $('#lstArea').fadeIn();
                        $('#lstArea').trigger("chosen:updated");
                    }
                }
            });
        }

        function ListBind() {

            var config = {
                '.chsn': {},
                '.chsn-deselect': { allow_single_deselect: true },
                '.chsn-no-single': { disable_search_threshold: 10 },
                '.chsn-no-results': { no_results_text: 'Oops, nothing found!' },
                '.chsn-width': { width: "100%" }
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);
            }

        }
        function lstCountry() {
            $('#lstCountry').fadeIn();
        }
        /*Code  Added  By Priti on 06122016 to use jquery Choosen for BranchHead*/

        function lstBranchHead() {
            $('#lstBranchHead').fadeIn();
        }

        function ChangeselectedvalueBranchHead() {
            var lstBranchHead = document.getElementById("lstBranchHead");
            if (document.getElementById("txtBranchHead_hidden").value != '') {
                for (var i = 0; i < lstBranchHead.options.length; i++) {
                    if (lstBranchHead.options[i].value == document.getElementById("txtBranchHead_hidden").value) {
                        lstBranchHead.options[i].selected = true;
                    }
                }
                $('#lstBranchHead').trigger("chosen:updated");
            }

        }
        function ChangeSourceBranchHead() {
            var fname = "%";
            var lBranchHead = $('select[id$=lstBranchHead]');
            lBranchHead.empty();

            $.ajax({
                type: "POST",
                url: "BranchAddEdit.aspx/GetBranchHead",
                data: JSON.stringify({ reqStr: fname }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var list = msg.d;
                    var listItems = [];
                    if (list.length > 0) {

                        for (var i = 0; i < list.length; i++) {
                            var id = '';
                            var name = '';
                            id = list[i].split('|')[1];
                            name = list[i].split('|')[0];

                            $('#lstBranchHead').append($('<option>').text(name).val(id));

                        }

                        $(lBranchHead).append(listItems.join(''));

                        lstBranchHead();
                        $('#lstBranchHead').trigger("chosen:updated");


                        ChangeselectedvalueBranchHead();

                    }
                    else {
                        //// alert("No records found");
                        //lstReferedBy();
                        $('#lstBranchHead').trigger("chosen:updated");

                    }
                }
            });
            // }
        }
        //..............end...............
        function ValidatePage() {
            var BranchCode = document.getElementById("PageControl1_txtCode").value;
            if (document.getElementById("PageControl1_txtCode").value == '') {
                alert('Branch Code Required!..');
                return false;
            }
            else if (document.getElementById("PageControl1_txtBranchDesc").value == '') {
                alert('Branch Name Required!..');
                return false;
            }

            if (BranchCode.length < 3) {
                alert('Branch Code Should be 3 characters!..');
                return false;
            }


        }
        function disp_prompt(name) {
            //if (name == "tab2") {
            //    document.location.href = "Contact_Document.aspx?Page=branch";

            //}
            if (name == "tab2") {
                document.location.href = "frm_branchUdf.aspx";
            }
            else if (name == "tab3") {
                document.location.href = "Branch_Correspondance.aspx?Page=branch";
            }
            else if (name == "tab4") {
                document.location.href = "Contact_Document.aspx?Page=branch";

            }
        }

        function Close() {
            editwin.close();
        }


        function CallAjaxState(obj1, obj2, obj3) {


            if (obj1.value == "") {
                obj1.value = "%";
            }
            var obj5 = document.getElementById("txtCountry_hidden").value;
            ajax_showOptionsTEST(obj1, obj2, obj3, obj5);
            if (obj5 != '') {
                ajax_showOptions(obj1, obj2, obj3, obj5);
                if (obj1.value == "%") {
                    obj1.value = "";
                }
            }
            else {
                alert("Please Select Country!..")
            }


        }


        function CallAjaxCity(obj1, obj2, obj3) {


            if (obj1.value == "") {
                obj1.value = "%";
            }
            var obj5 = document.getElementById("txtState_hidden").value;
            if (obj5 != '') {
                ajax_showOptionsTEST(obj1, obj2, obj3, obj5);
                if (obj1.value == "%") {
                    obj1.value = "";
                }
            }
            else {
                alert("Please Select State!..")
            }
        }


        function OnEditButtonClick(keyValue) {
            var url = 'BranchAddEdit.aspx?id=' + keyValue;
            //parent.OnMoreInfoClick(url, "Edit Account", '820px', '400px', "Y");
            window.location.href = url;
        }

        function OnAddButtonClick() {
            var url = 'BranchAddEdit.aspx?id=ADD';
            //OnMoreInfoClick(url, "Add New Account", '820px', '400px', "Y");
            window.location.href = url;
        }


        function DeleteRow(keyValue) {
            doIt = confirm('Confirm delete?');
            if (doIt) {
                grid.PerformCallback('Delete~' + keyValue);
                //height();
            }
            else {

            }
        }


        function ShowHideFilter1(obj) {

            gridTerminal.PerformCallback(obj);
        }

        function OnCountryChanged(cmbCountry) {



            drpState.PerformCallback(obj);


        }
        function OnStateChanged(cmbState) {

        }
        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        function setvaluetovariable(obj1) {
            combo1.PerformCallback(obj1);
        }
        function setvaluetovariable1(obj1) {
            combo2.PerformCallback(obj1);
        }
        function CallList(obj1, obj2, obj3) {

            if (obj1.value == "") {
                obj1.value = "%";
            }
            var obj5 = '';
            ajax_showOptionsTEST(obj1, obj2, obj3, obj5);
            if (obj1.value == "%") {
                obj1.value = "";
            }
        }
        function hide_show(obj) {
            if (obj == 'All') {
                document.getElementById("client_pro").style.display = "none";
            }

        }
        function GetClick() {
            btnC.PerformCallback();
        }
        function Page_Load() {
            document.getElementById("TdCombo").style.display = "none";
        }
        function Message(obj) {
            if (obj == "update") {
                alert('Update Successfully');
                gridTerminal.PerformCallback();
            }
            else if (obj == "insert") {
                alert('Insert Successfully');
                gridTerminal.PerformCallback();
            }
            else {
                alert('Terminal Id Already Exists');
            }
        }
        function CheckingTD(obj) {

            var gridstat = gridTerminal.cpCompCombo;
            if (gridstat == 'anew')
                combo.SetFocus();


        }
        FieldName = "cmbExport_DDDWS";
        function LastCall(obj) {

        }
        // Code Added By Priti on 21122016 to check Unique Short Name
        function fn_ctxtPro_Name_TextChanged() {
            var ShortName = document.getElementById("txtCode").value;
            var qString = window.location.href.split("=")[1];
            $.ajax({
                type: "POST",
                url: "BranchAddEdit.aspx/CheckUniqueName",
                data: JSON.stringify({ ShortName: ShortName, qString: qString }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;

                    if (data == true) {
                        jAlert("Please enter unique Short Name");
                        document.getElementById("txtCode").value = '';
                        document.getElementById("txtCode").focus();
                        return false;
                    }
                }
            });
        }
        $(document).ready(function () {

        });
    </script>
    <style type="text/css">
        .abs {
            position: absolute;
            right: -19px;
            top: 4px;
        }

        .abs1 {
            position: absolute;
            right: -19px;
            top: 4px;
        }

        .chosen-container.chosen-container-single {
            width: 100% !important;
        }

        .chosen-choices {
            width: 100% !important;
        }

        #lstCountry, #lstState, #lstCity, #lstArea, #lstPin, #lstBranchHead, #lstMainAccount {
            width: 100%;
        }

        #lstCountry, #lstState, #lstCity, #lstArea, #lstPin, #lstBranchHead, #lstMainAccount {
            display: none !important;
        }

        #lstCountry_chosen, #lstState_chosen, #lstCity_chosen, #lstArea_chosen, #lstPin_chosen, #lstBranchHead_chosen {
            width: 100% !important;
        }

        #PageControl1_CC {
            overflow: visible !important;
        }

        #lstState_chosen, #lstCountry_chosen, #lstCity_chosen, #lstPin_chosen, #lstBranchHead_chosen {
            margin-bottom: 5px;
        }

        .divControlClass > span.controlClass {
            margin-top: 8px;
        }

        .nestedinput {
            padding: 0;
            margin: 0;
        }

            .nestedinput li {
                list-style-type: none;
                display: inline-block;
                float: left;
            }

                .nestedinput li.dash {
                    width: 26px;
                    text-align: center;
                    padding: 6px;
                }

                .nestedinput li .iconRed {
                    position: absolute;
                    right: -10px;
                    top: 5px;
                }
        /*rev 25249*/
        label {
            font-weight: 400 !important;
            margin-top: 8px;
            font-size: 14px;
            font-family: 'Poppins', sans-serif !important;
        }

        span.dx-vam {
            font-size: 15px;
        }

        .dxtcLite_PlasticBlue > .dxtc-stripContainer .dxtc-activeTab {
            background: #094e8c;
        }
        /*rev end 25249*/

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
            font-size: 11px;
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
        bottom: 8px;
        right: 20px;
        z-index: 0;
        cursor: pointer;
    }

    .date-select .form-control {
        position: relative;
        z-index: 1;
        background: transparent;
    }

    #ddlState, #ddlPartyType, #divoutletStatus, #slmonth, #slyear , #txtCINVdate {
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
            bottom: 8px;
            right: 20px;
            z-index: 0;
            cursor: pointer;
        }

        .date-select .form-control {
            position: relative;
            z-index: 1;
            background: transparent;
        }

        #ddlState, #ddlPartyType, #divoutletStatus, #slmonth, #slyear, #txtCINVdate {
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
        .dxeEditArea_PlasticBlue {
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

        /*.pmsModal .modal-header {
            background: #094e8c !important;
            background-image: none !important;
            padding: 11px 20px;
            border: none;
            border-radius: 5px 5px 0 0;
            color: #fff;
            border-radius: 10px 10px 0 0;
        }*/

       /* .pmsModal .modal-content {
            border: none;
            border-radius: 10px;
        }

        .pmsModal .modal-header .modal-title {
            font-size: 14px;
        }
*/
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

        #dtFrom_B-1, #dtTo_B-1, #txtCINVdate_B-1 {
            background: transparent !important;
            border: none;
            width: 30px;
            padding: 10px !important;
        }

            #dtFrom_B-1 #dtFrom_B-1Img,
            #dtTo_B-1 #dtTo_B-1Img,
            #txtCINVdate_B-1 #txtCINVdate_B-1Img {
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

        .dxeTextBox_PlasticBlue {
            height: 34px;
            border-radius: 4px;
        }

        .container {
            width: 88% !important;
        }

        /*Rev end 1.0*/

        .close:hover, .close:focus
        {
            color: #fff;
            opacity: .8;
        }

        #BranchHeadModelTable
        {
            margin-top: 10px !important;
        }

        #BranchHeadModelTable table tr th{
            padding: 4px 5px;
        }

        #BranchHeadModelTableTbl_length label
        {
            display: flex;
            align-items: center;
        }
        #BranchHeadModelTableTbl_length label select{
            margin: 0 2px;
        }

        #BranchHeadModelTableTbl_filter input
        {
            height: 30px;
            box-shadow: none;
            outline: none;
            border-radius: 4px;
            border: 1px solid #ccc;
            padding: 0 5px;
        }

        #BranchHeadModelTableTbl tbody tr td input:focus-visible{
            border: none;
            box-shadow: none;
            outline: none;
        }

        @media only screen and (max-width: 768px)
        {
            .breadCumb > span
            {
                padding: 9px 12px;
            }
        }
    </style>
   <%-- Rev 2.0--%>
    <script>
        
        function BranchHeadButnClick(s, e) {
            $('#BranchHeadModel').modal('show');
            $("#txtBranchHeadSearch").focus();
        }

        function BranchHeadbtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#BranchHeadModel').modal('show');
                $("#txtBranchHeadSearch").focus();
            }
        }
        function BranchHeadModelkeydown(e) {
            var OtherDetails = {}
            OtherDetails.reqStr = $("#txtBranchHeadSearch").val();
            if ($.trim($("#txtBranchHeadSearch").val()) == "" || $.trim($("#txtBranchHeadSearch").val()) == null) {
                return false;
            }
            if (e.code == "Enter" || e.code == "NumpadEnter") {               
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                if ($("#txtBranchHeadSearch").val() != null && $("#txtBranchHeadSearch").val() != "") {
                    callonServer("BranchAddEdit.aspx/GetOnDemandBranchHead", OtherDetails, "BranchHeadModelTable", HeaderCaption, "BranchHeadHeadIndex", "SetBranchHeadHead");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[BranchHeadHeadIndex=0]"))
                    $("input[BranchHeadHeadIndex=0]").focus();
            }
        }

        function SetBranchHeadHead(Id, Name) {
            $("#txtBranchHead_hidden").val(Id);
            ctxtBranchHead.SetText(Name);
            $('#BranchHeadModel').modal('hide');
        }
    </script>
   <%-- Rev 2.0 End--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--rev 25249--%>
    <%--<div class="panel-heading">
        <div class="panel-title clearfix">
            <h3 class="pull-left">Add/Edit Branch</h3>
            
            <div class="crossBtn"><a href=""><i class="fa fa-times"></i></a></div>
        </div>
    </div>--%>
    <div class="breadCumb">
        <span>Add/Edit Branch</span>
        <div class="crossBtnN"><a href="Branch.aspx"><i class="fa fa-times"></i></a></div>
    </div>
    <%--rev 25249--%>
    <%--debjyoti 22-12-2016--%>
    <div class="container">
        <div class="backBox mt-5 p-4 ">
            <dxe:ASPxPopupControl ID="ASPXPopupControl" runat="server"
                CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popup" Height="630px"
                Width="600px" HeaderText="Add/Modify UDF" Modal="true" AllowResize="true" ResizingMode="Postponed">
                <ContentCollection>
                    <dxe:PopupControlContentControl runat="server">
                    </dxe:PopupControlContentControl>
                </ContentCollection>
            </dxe:ASPxPopupControl>

            <asp:HiddenField runat="server" ID="IsUdfpresent" />
            <asp:HiddenField runat="server" ID="Keyval_internalId" />
            <%--End debjyoti 22-12-2016--%>

            <%--rev 25249
        added CssClass="form-control" in input field--%>
            <div class="form_main">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 100%">
                            <dxe:ASPxPageControl ID="PageControl1" runat="server" Width="100%" ActiveTabIndex="0"
                                ClientInstanceName="page">
                                <TabPages>
                                    <dxe:TabPage Text="General">
                                        <ContentCollection>
                                            <dxe:ContentControl runat="server">
                                                <div class="totalWrap">
                                                    <%--Rev 1.0--%>
                                                    <%--<div class="col-md-3">--%>
                                                    <div class="col-md-3 h-branch-select">
                                                        <%--Rev 1.0--%>
                                                        <label>Branch Type </label>
                                                        <div class="relative">
                                                            <asp:DropDownList ID="cmbBranchType" runat="server" Width="100%">
                                                                <asp:ListItem Text="Own Branch" Value="Own Branch"></asp:ListItem>
                                                                <asp:ListItem Text="Franchisee" Value="Franchisee"></asp:ListItem>
                                                                <asp:ListItem Text="Rental" Value="Rental"></asp:ListItem>
                                                                <asp:ListItem Text="Service Center" Value="ServiceCenter"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Short Name <span style="color: red">*</span></label>
                                                        <div class="relative">
                                                            <asp:TextBox ID="txtCode" runat="server" ClientIDMode="Static" Width="100%"
                                                                MaxLength="80" onchange="fn_ctxtPro_Name_TextChanged()" CssClass="form-control">
                                                                   
                                                            </asp:TextBox>

                                                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtCode"
                                                                SetFocusOnError="true" ErrorMessage="" class="pullrightClass fa fa-exclamation-circle abs iconRed" ToolTip="Mandatory" ValidationGroup="branchgrp">                                                        
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <%--Rev 1.0--%>
                                                    <%--<div class="col-md-3">--%>
                                                    <div class="col-md-3 h-branch-select">
                                                        <%--Rev 1.0--%>
                                                        <label>Parent Branch </label>
                                                        <div class="relative">
                                                            <asp:DropDownList ID="cmbParentBranch" runat="server" Width="100%">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Branch Name <span style="color: red">*</span></label>
                                                        <div class="relative">
                                                            <asp:TextBox ID="txtBranchDesc" runat="server" Width="100%" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtBranchDesc"
                                                                SetFocusOnError="true" ErrorMessage="" class="pullrightClass fa fa-exclamation-circle abs1 iconRed" ToolTip="Mandatory" ValidationGroup="branchgrp">                                                        
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <%--Rev 1.0--%>
                                                    <%--<div class="col-md-3">--%>
                                                    <div class="col-md-3 h-branch-select">
                                                        <%--Rev 1.0--%>
                                                        <label>Region </label>
                                                        <div class="relative">
                                                            <asp:DropDownList ID="cmbBranchRegion" runat="server" Width="100%">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Address1</label>
                                                        <div class="relative">
                                                            <asp:TextBox ID="txtAddress1" runat="server" Width="100%" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Address2 </label>
                                                        <div class="relative">
                                                            <asp:TextBox ID="txtAddress2" runat="server" Width="100%" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Address3 </label>
                                                        <div class="relative">
                                                            <asp:TextBox ID="txtAddress3" runat="server" Width="100%" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Country <span style="color: red">*</span> </label>
                                                        <div class="relative">
                                                            <%-- <asp:TextBox ID="txtCountry" runat="server" Width="250px" TabIndex="9"></asp:TextBox>--%>
                                                            <asp:ListBox ID="lstCountry" CssClass="chsn" runat="server" Width="100%" data-placeholder="Select..." onchange="onCountryChange()"></asp:ListBox>
                                                            <asp:HiddenField ID="txtCountry_hidden" runat="server" />
                                                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator5" ControlToValidate="lstCountry"
                                                                SetFocusOnError="true" ErrorMessage="" class="pullrightClass fa fa-exclamation-circle abs1 iconRed" ToolTip="Mandatory" ValidationGroup="branchgrp">                                                        
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>State <span style="color: red">*</span> </label>
                                                        <div class="relative">
                                                            <%--<asp:TextBox ID="txtState" runat="server" Width="250px" TabIndex="10"></asp:TextBox>--%>
                                                            <asp:ListBox ID="lstState" CssClass="chsn" runat="server" Width="100%" data-placeholder="Select State.." onchange="onStateChange()"></asp:ListBox>
                                                            <asp:HiddenField ID="txtState_hidden" runat="server" />
                                                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator4" ControlToValidate="lstState"
                                                                SetFocusOnError="true" ErrorMessage="" class="pullrightClass fa fa-exclamation-circle abs1 iconRed" ToolTip="Mandatory" ValidationGroup="branchgrp">                                                        
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>City / District <span style="color: red">*</span> </label>
                                                        <div class="relative">
                                                            <%--<asp:TextBox ID="txtCity" runat="server" Width="250px" TabIndex="11"></asp:TextBox>--%>
                                                            <asp:ListBox ID="lstCity" CssClass="chsn" runat="server" Width="100%" data-placeholder="Select City.." onchange="onCityChange()"></asp:ListBox>
                                                            <asp:HiddenField ID="txtCity_hidden" runat="server" />

                                                            <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator3" ControlToValidate="lstCity"
                                                                SetFocusOnError="true" ErrorMessage="" class="pullrightClass fa fa-exclamation-circle abs1 iconRed" ToolTip="Mandatory" ValidationGroup="branchgrp">                                                        
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Area </label>
                                                        <div class="relative">
                                                            <%--<asp:TextBox ID="txtCity" runat="server" Width="250px" TabIndex="11"></asp:TextBox>--%>
                                                            <asp:ListBox ID="lstArea" CssClass="chsn" runat="server" Width="100%" data-placeholder="Select area.." onchange="onAreaChange()"></asp:ListBox>
                                                            <asp:HiddenField ID="hdLstArea" runat="server" />

                                                        </div>
                                                    </div>
                                                    <div class="clear"></div>
                                                    <div class="col-md-3">
                                                        <label>PIN </label>
                                                        <div class="relative">
                                                            <%-- <asp:TextBox ID="txtPin" runat="server" Width="250px" TabIndex="12" MaxLength="50"></asp:TextBox>--%>
                                                            <asp:ListBox ID="lstPin" CssClass="chsn" runat="server" Width="100%" data-placeholder="Select..."></asp:ListBox>
                                                            <asp:HiddenField ID="HdPin" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Branch Phone </label>
                                                        <div class="relative">
                                                            <asp:TextBox ID="txtPhone" runat="server" Width="100%" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Fax </label>
                                                        <div class="relative">
                                                            <asp:TextBox ID="txtFax" runat="server" Width="100%" MaxLength="12" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Branch Head</label>
                                                        <div class="relative">
                                                            <dxe:ASPxButtonEdit ID="txtBranchHead" runat="server" ReadOnly="true" ClientInstanceName="ctxtBranchHead"  Width="100%">
                                                                <Buttons>
                                                                    <dxe:EditButton>
                                                                    </dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s,e){BranchHeadButnClick();}" KeyDown="BranchHeadbtnKeyDown" />
                                                            </dxe:ASPxButtonEdit>
                                                            <%--<asp:ListBox ID="lstBranchHead" CssClass="chsn" runat="server" Width="100%" data-placeholder="Select..."></asp:ListBox>--%>
                                                            <%-- <asp:TextBox ID="txtBranchHead" runat="server" Width="250px" TabIndex="15"></asp:TextBox>--%>
                                                            <asp:HiddenField ID="txtBranchHead_hidden" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="clear"></div>
                                                    <div class="col-md-3">
                                                        <label>Contact Person Phone</label>
                                                        <div class="relative">
                                                            <asp:TextBox ID="txtContPhone" runat="server" Width="100%" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Contact Person</label>
                                                        <div class="relative">
                                                            <asp:TextBox ID="txtContPerson" runat="server" Width="100%" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Contact Person Email</label>
                                                        <div class="relative">
                                                            <asp:TextBox ID="txtContEmail" runat="server" Width="100%" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-3">
                                                        <label>Ledger Posting Account </label>
                                                        <div class="relative">
                                                            <asp:ListBox ID="lstMainAccount" CssClass="chsn" runat="server" Width="100%" data-placeholder="Select..." onchange="onlstMainAccountChange()"></asp:ListBox>
                                                            <asp:HiddenField ID="hdlstMainAccount" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="clear"></div>
                                                    <%--for Gstin--%>
                                                    <div class="col-md-3">
                                                        <label class="labelt">GSTIN   </label>
                                                        <div class="relative">
                                                            <ul class="nestedinput">
                                                                <li>
                                                                    <dxe:ASPxTextBox ID="txtGSTIN1" ClientInstanceName="ctxtGSTIN1" MaxLength="2" runat="server" Width="33px">
                                                                        <ClientSideEvents KeyPress="function(s,e){ fn_AllowonlyNumeric(s,e);}" />
                                                                    </dxe:ASPxTextBox>
                                                                </li>
                                                                <li class="dash">- </li>
                                                                <li>
                                                                    <dxe:ASPxTextBox ID="txtGSTIN2" ClientInstanceName="ctxtGSTIN2" MaxLength="10" runat="server" Width="90px">
                                                                        <ClientSideEvents KeyUp="Gstin2TextChanged" />
                                                                    </dxe:ASPxTextBox>
                                                                </li>
                                                                <li class="dash">- </li>
                                                                <li>
                                                                    <dxe:ASPxTextBox ID="txtGSTIN3" ClientInstanceName="ctxtGSTIN3" MaxLength="3" runat="server" Width="50px">
                                                                    </dxe:ASPxTextBox>
                                                                    <span id="invalidGst" class="fa fa-exclamation-circle iconRed " style="color: red; display: none; padding-left: 9px; left: 302px;" title="Invalid GSTIN"></span>
                                                                </li>
                                                            </ul>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>CIN </label>
                                                        <div>
                                                            <asp:TextBox ID="txtCIN" runat="server" Width="100%" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>CIN Validity Date </label>
                                                        <div>
                                                            <dxe:ASPxDateEdit ID="txtCINVdate" runat="server" EditFormat="Custom" UseMaskBehavior="True"
                                                                Width="100%">
                                                                <ButtonStyle Width="13px">
                                                                </ButtonStyle>
                                                            </dxe:ASPxDateEdit>
                                                            <img src="/assests/images/calendar-icon.png" class="calendar-icon" />
                                                        </div>
                                                    </div>


                                                    <div class="clear"></div>
                                                    <div class="col-md-12" style="padding-top: 10px;">
                                                        <%-- <% if (rights.CanAdd || rights.CanEdit)
                                                    { %>--%>
<%--Rev 2.0--%>
<dxe:ASPxButton ID="btnSave" runat="server" Text="Save" ValidationGroup="branchgrp" CssClass="btn btn-success dxbButton" OnClick="btnSave_Click"  UseSubmitBehavior="False" AutoPostBack="False">
<ClientSideEvents Click="function(s, e) {ClientSaveClick(s,e);}" />
</dxe:ASPxButton>
<dxe:ASPxButton ID="Button2" runat="server" Text="Cancel" CssClass="btn btn-danger dxbButton" OnClick="Button2_Click" UseSubmitBehavior="False" AutoPostBack="False">                                                
</dxe:ASPxButton>
<dxe:ASPxButton ID="btnUdf" runat="server" Text="UDF" CssClass="btn btn-primary dxbButton"  UseSubmitBehavior="False" AutoPostBack="False">
<ClientSideEvents Click="function(s, e) {OpenUdf();}" />
</dxe:ASPxButton>
<%--<asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="branchgrp" CssClass="btn btn-success dxbButton" OnClick="btnSave_Click" OnClientClick="return ClientSaveClick()" UseSubmitBehavior="False" />--%>
<%--<asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn btn-danger dxbButton" OnClick="Button2_Click" UseSubmitBehavior="False"/>                                                  --%>

<%--<asp:Button ID="btnUdf" runat="server" Text="UDF" CssClass="btn btn-primary dxbButton" OnClientClick="if(OpenUdf()){ return false;}" UseSubmitBehavior="False" />--%>
<%--Rev 2.0 End--%>
<%--  <% } %>--%>
                                                    </div>
                                                </div>

                                            </dxe:ContentControl>
                                        </ContentCollection>
                                    </dxe:TabPage>
                                    <dxe:TabPage Text="Trading Terminal" Visible="false">
                                        <ContentCollection>
                                            <dxe:ContentControl runat="server">
                                                <table>
                                                    <tr>

                                                        <td>
                                                            <dxe:ASPxGridViewExporter ID="exporter" runat="server">
                                                            </dxe:ASPxGridViewExporter>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td id="Td4">
                                                            <a href="javascript:ShowHideFilter1('s1');"><span style="color: #000099; text-decoration: underline">Show Filter</span></a>
                                                        </td>
                                                        <td id="Td5">
                                                            <a href="javascript:ShowHideFilter1('All1');"><span style="color: #000099; text-decoration: underline">All Records</span></a>
                                                        </td>
                                                        <td class="gridcellright">
                                                            <dxe:ASPxComboBox ID="cmbExport" runat="server" AutoPostBack="true" BackColor="Navy"
                                                                Font-Bold="False" ForeColor="White" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged"
                                                                ValueType="System.Int32" Width="130px">
                                                                <Items>
                                                                    <%--  <dxe:ListEditItem Text="Select" Value="0" />--%>
                                                                    <%-- <dxe:ListEditItem Text="PDF" Value="1" />--%>
                                                                    <dxe:ListEditItem Text="XLS" Value="2" />
                                                                    <%--<dxe:ListEditItem Text="RTF" Value="3" />
                            <dxe:ListEditItem Text="CSV" Value="4" />--%>
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
                                                </table>
                                                <dxe:ASPxGridView ID="gridTerminalId" ClientInstanceName="gridTerminal" KeyFieldName="TradingTerminal_ID"
                                                    runat="server" DataSourceID="TrdTerminal" Width="100%" OnHtmlEditFormCreated="gridTerminalId_HtmlEditFormCreated"
                                                    OnStartRowEditing="gridTerminalId_StartRowEditing" AutoGenerateColumns="False"
                                                    OnCustomCallback="gridTerminalId_CustomCallback" OnCellEditorInitialize="gridTerminalId_CellEditorInitialize"
                                                    OnInitNewRow="gridTerminalId_InitNewRow" OnCustomJSProperties="gridTerminalId_CustomJSProperties">

                                                    <Settings ShowGroupPanel="True" ShowFooter="false" ShowStatusBar="Visible" ShowTitlePanel="false" />
                                                    <SettingsEditing PopupEditFormHeight="300px" PopupEditFormHorizontalAlign="Center"
                                                        PopupEditFormModal="True" PopupEditFormVerticalAlign="BottomSides" PopupEditFormWidth="800px"
                                                        EditFormColumnCount="1" Mode="PopupEditForm" />
                                                    <SettingsText PopupEditFormCaption="Add/Modify Branch" ConfirmDelete="Are you sure to Delete this Record!" />
                                                    <SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" AllowFocusedRow="True" />
                                                    <Columns>
                                                        <dxe:GridViewDataTextColumn FieldName="TradingTerminal_ID" ReadOnly="True" Visible="False"
                                                            VisibleIndex="0">
                                                            <CellStyle CssClass="gridcellleft">
                                                            </CellStyle>
                                                            <EditFormCaptionStyle Wrap="False">
                                                            </EditFormCaptionStyle>
                                                            <EditFormSettings Visible="False" />
                                                        </dxe:GridViewDataTextColumn>
                                                        <dxe:GridViewDataTextColumn FieldName="Exchange" ReadOnly="True" VisibleIndex="0">
                                                            <EditFormCaptionStyle Wrap="False">
                                                            </EditFormCaptionStyle>
                                                            <CellStyle CssClass="gridcellleft">
                                                            </CellStyle>
                                                            <EditFormSettings Visible="False" />
                                                        </dxe:GridViewDataTextColumn>
                                                        <dxe:GridViewDataTextColumn FieldName="TradingTerminal_TerminalID" Caption="TerminalID"
                                                            ReadOnly="True" VisibleIndex="1">
                                                            <EditFormCaptionStyle Wrap="False">
                                                            </EditFormCaptionStyle>
                                                            <CellStyle CssClass="gridcellleft">
                                                            </CellStyle>
                                                            <EditFormSettings Visible="False" />
                                                        </dxe:GridViewDataTextColumn>
                                                        <dxe:GridViewDataTextColumn FieldName="TradingTerminal_ParentTerminalID" Caption="Parent TerminalID"
                                                            ReadOnly="True" VisibleIndex="2">
                                                            <EditFormCaptionStyle Wrap="False">
                                                            </EditFormCaptionStyle>
                                                            <CellStyle CssClass="gridcellleft">
                                                            </CellStyle>
                                                            <EditFormSettings Visible="False" />
                                                        </dxe:GridViewDataTextColumn>
                                                        <dxe:GridViewDataTextColumn FieldName="AllTradeID" Caption="All Trade Name" ReadOnly="True"
                                                            VisibleIndex="3">
                                                            <EditFormCaptionStyle Wrap="False">
                                                            </EditFormCaptionStyle>
                                                            <CellStyle CssClass="gridcellleft">
                                                            </CellStyle>
                                                            <EditFormSettings Visible="False" />
                                                        </dxe:GridViewDataTextColumn>
                                                        <dxe:GridViewDataTextColumn FieldName="CliTradeID" Caption="Client Trade Name"
                                                            ReadOnly="True" VisibleIndex="4">
                                                            <EditFormCaptionStyle Wrap="False">
                                                            </EditFormCaptionStyle>
                                                            <CellStyle CssClass="gridcellleft">
                                                            </CellStyle>
                                                            <EditFormSettings Visible="False" />
                                                        </dxe:GridViewDataTextColumn>
                                                        <dxe:GridViewDataTextColumn FieldName="ProTradeID" Caption="Pro Trade Name" ReadOnly="True"
                                                            VisibleIndex="5">
                                                            <EditFormCaptionStyle Wrap="False">
                                                            </EditFormCaptionStyle>
                                                            <CellStyle CssClass="gridcellleft">
                                                            </CellStyle>
                                                            <EditFormSettings Visible="False" />
                                                        </dxe:GridViewDataTextColumn>
                                                        <dxe:GridViewDataTextColumn FieldName="brokid" Caption="Broker Name" ReadOnly="True"
                                                            VisibleIndex="6">
                                                            <EditFormCaptionStyle Wrap="False">
                                                            </EditFormCaptionStyle>
                                                            <CellStyle CssClass="gridcellleft">
                                                            </CellStyle>
                                                            <EditFormSettings Visible="False" />
                                                        </dxe:GridViewDataTextColumn>
                                                        <dxe:GridViewCommandColumn VisibleIndex="7" ShowClearFilterButton="true" ShowDeleteButton="true" ShowEditButton="true">
                                                            <%--<ClearFilterButton Visible="True">
                                                    </ClearFilterButton>
                                                    <DeleteButton Visible="True">
                                                    </DeleteButton>
                                                    <EditButton Visible="True">
                                                    </EditButton>--%>
                                                            <HeaderTemplate>
                                                                <a href="javascript:void(0);" onclick="gridTerminal.AddNewRow()"><span style="color: #000099; text-decoration: underline">Add New</span></a>
                                                            </HeaderTemplate>
                                                        </dxe:GridViewCommandColumn>
                                                    </Columns>
                                                    <SettingsCommandButton>
                                                        <ClearFilterButton Text="ClearFilter">
                                                        </ClearFilterButton>

                                                        <EditButton Image-Url="/assests/images/Edit.png" ButtonType="Image" Image-AlternateText="Edit">
                                                        </EditButton>
                                                        <DeleteButton Image-Url="/assests/images/Delete.png" ButtonType="Image" Image-AlternateText="Delete">
                                                        </DeleteButton>
                                                    </SettingsCommandButton>
                                                    <Templates>
                                                        <EditForm>
                                                            <table width="100%">
                                                                <tr>
                                                                    <td style="text-align: right">
                                                                        <span class="Ecoheadtxt" style="color: Black">Company Name :</span>
                                                                    </td>
                                                                    <td class="gridcellleft">
                                                                        <dxe:ASPxComboBox ID="comboCompany" runat="server" EnableSynchronization="False"
                                                                            EnableIncrementalFiltering="True" DataSourceID="sqlCompany" TextField="cmp_name"
                                                                            ValueField="cmp_internalId" ClientInstanceName="combo" Width="300px" ValueType="System.String">
                                                                            <ClientSideEvents ValueChanged="function(s,e){
                                                                                                                var indexr = s.GetSelectedIndex();
                                                                                                                setvaluetovariable(indexr)
                                                                                                                }" />
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <span class="Ecoheadtxt" style="color: Black">Exchange :</span>
                                                                    </td>
                                                                    <td class="gridcellleft">
                                                                        <dxe:ASPxComboBox ID="comboExchange" runat="server" DataSourceID="SqlExchange" TextField="Exchange"
                                                                            EnableSynchronization="False" EnableIncrementalFiltering="True" ValueField="exch_internalId"
                                                                            ValueType="System.String" Width="300px" ClientInstanceName="combo1"
                                                                            OnCallback="comboExchange_Callback">
                                                                            <ClientSideEvents ValueChanged="function(s,e){
                                                                                                                var indexr = s.GetSelectedIndex();
                                                                                                                setvaluetovariable1(indexr)
                                                                                                                }" />
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right">
                                                                        <span class="Ecoheadtxt" style="color: Black">CTCL Vender ID:</span>
                                                                    </td>
                                                                    <td class="gridcellleft">
                                                                        <dxe:ASPxComboBox ID="comboVendor" runat="server" DataSourceID="SqlVendor" TextField="CTCLVendor_Name"
                                                                            ValueField="CTCLVendor_ID" ValueType="System.String" Width="300px"
                                                                            ClientInstanceName="combo2" EnableSynchronization="False" EnableIncrementalFiltering="True"
                                                                            OnCallback="comboVendor_Callback">
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <span class="Ecoheadtxt" style="color: Black">Broker:</span>
                                                                    </td>
                                                                    <td class="gridcellleft">
                                                                        <asp:TextBox ID="txtBrokername" runat="server" CssClass="EcoheadCon" Width="300px"></asp:TextBox>
                                                                    </td>
                                                                    <td class="gridcellleft" style="display: none">
                                                                        <asp:TextBox ID="txtMappinID" runat="server" CssClass="EcoheadCon" Width="300px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right">
                                                                        <span class="Ecoheadtxt" style="color: Black">Terminal Id:</span>
                                                                    </td>
                                                                    <td class="gridcellleft">
                                                                        <asp:TextBox ID="txtTerminalId" runat="server" MaxLength="20" CssClass="EcoheadCon"
                                                                            Width="300px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <span class="Ecoheadtxt" style="color: Black">Parent TerminalId:</span>
                                                                    </td>
                                                                    <td class="gridcellleft">
                                                                        <dxe:ASPxComboBox ID="parentTerID" runat="server" DataSourceID="SqlParentTerminal"
                                                                            TextField="TradingTerminal_TerminalID" ValueField="TradingTerminal_TerminalID"
                                                                            EnableSynchronization="False" EnableIncrementalFiltering="True"
                                                                            Width="300px" ValueType="System.String">
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right">
                                                                        <span class="Ecoheadtxt" style="color: Black">Contact Name:</span>
                                                                    </td>
                                                                    <td class="gridcellleft">
                                                                        <asp:TextBox ID="txtContactName" runat="server" CssClass="EcoheadCon" Width="300px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <span class="Ecoheadtxt" style="color: Black">CTCL ID:</span>
                                                                    </td>
                                                                    <td class="gridcellleft">
                                                                        <asp:TextBox ID="txtCTCLID" runat="server" CssClass="EcoheadCon" Width="300px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                                                                    <td style="text-align: right">
                                                                                        <span class="Ecoheadtxt" style="color: Black">Broker:</span>
                                                                                    </td>
                                                                                    <td class="gridcellleft">
                                                                                        <asp:TextBox ID="txtBrokername" runat="server" CssClass="EcoheadCon" Width="300px" ></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="text-align: right">
                                                                                        <span class="Ecoheadtxt" style="color: Black"></span>
                                                                                    </td>
                                                                                    <td class="gridcellleft">
                                                                                        
                                                                                    </td>
                                                                                </tr>--%>
                                                                <tr>
                                                                    <td style="text-align: right">
                                                                        <span class="Ecoheadtxt" style="color: Black">Connection Mode:</span>
                                                                    </td>
                                                                    <td class="gridcellleft">
                                                                        <asp:TextBox ID="txtConnection" runat="server" CssClass="EcoheadCon" Width="300px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <span class="Ecoheadtxt" style="color: Black">Activation Date:</span>
                                                                    </td>
                                                                    <td class="gridcellleft">
                                                                        <dxe:ASPxDateEdit ID="dtActivation" runat="server" EditFormat="Custom" UseMaskBehavior="True"
                                                                            Width="300px">
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </dxe:ASPxDateEdit>
                                                                        <%--  <dxe:ASPxDateEdit ID="dtActivation" runat="server" Font-Size="12px" Width="200px"
                                                                                            EditFormat="Custom" EditFormatString="dd-mm-yyyy" UseMaskBehavior="True">
                                                                                            <ButtonStyle Width="13px">
                                                                                            </ButtonStyle>
                                                                                        </dxe:ASPxDateEdit>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right">
                                                                        <span class="Ecoheadtxt" style="color: Black">Deactivation Date:</span>
                                                                    </td>
                                                                    <td class="gridcellleft">
                                                                        <dxe:ASPxDateEdit ID="dtDeactivation" runat="server" EditFormat="Custom" UseMaskBehavior="True"
                                                                            Width="300px">
                                                                            <ButtonStyle Width="13px">
                                                                            </ButtonStyle>
                                                                        </dxe:ASPxDateEdit>
                                                                        <%--  <dxe:ASPxDateEdit ID="dtDeactivation" runat="server" Font-Size="12px" Width="200px"
                                                                                            EditFormat="Custom" EditFormatString="dd-mm-yyyy" UseMaskBehavior="True">
                                                                                            <ButtonStyle Width="13px">
                                                                                            </ButtonStyle>
                                                                                        </dxe:ASPxDateEdit>--%>
                                                                    </td>
                                                                    <td style="text-align: right" id="all1">
                                                                        <span class="Ecoheadtxt" style="color: Black">All Trade Name:</span>
                                                                    </td>
                                                                    <td class="gridcellleft" id="all2">
                                                                        <asp:TextBox ID="txtAllTrade" runat="server" CssClass="EcoheadCon" Width="300px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                                                                    <td style="text-align: right">
                                                                                        <span class="Ecoheadtxt" style="color: Black">Broker:</span>
                                                                                    </td>
                                                                                    <td class="gridcellleft">
                                                                                        <asp:TextBox ID="txtBrokername" runat="server" CssClass="EcoheadCon" Width="300px" ></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="gridcellleft" style="display:none">
                                                                                        <asp:TextBox ID="txtMappinID" runat="server" CssClass="EcoheadCon" Width="300px" ></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="text-align: right" id="all1">
                                                                                        <span class="Ecoheadtxt" style="color: Black">All Trade Name:</span>
                                                                                    </td>
                                                                                    <td class="gridcellleft" id="all2">
                                                                                        <asp:TextBox ID="txtAllTrade" runat="server" CssClass="EcoheadCon" Width="300px" ></asp:TextBox>
                                                                                    </td>
                                                                                </tr>--%>
                                                                <tr id="client_pro">
                                                                    <td style="text-align: right" id="client1">
                                                                        <span class="Ecoheadtxt" style="color: Black">Client Trade Name:</span>
                                                                    </td>
                                                                    <td class="gridcellleft" id="client2">
                                                                        <asp:TextBox ID="txtClientTrade" runat="server" CssClass="EcoheadCon" Width="300px"></asp:TextBox>
                                                                    </td>
                                                                    <td style="text-align: right" id="pro1">
                                                                        <span class="Ecoheadtxt" style="color: Black">Pro Trade Name:</span>
                                                                    </td>
                                                                    <td class="gridcellleft" id="pro2">
                                                                        <asp:TextBox ID="txtProductTrade" runat="server" CssClass="EcoheadCon" Width="300px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" style="text-align: right">
                                                                        <input id="Button1" type="button" value="Save" onclick="GetClick()" class="btnUpdate"
                                                                            style="width: 88px; height: 28px" />
                                                                    </td>
                                                                    <td style="text-align: left;" colspan="1">
                                                                        <dxe:ASPxButton ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel data"
                                                                            Height="18px" Width="88px" AutoPostBack="False">
                                                                            <ClientSideEvents Click="function(s, e) {gridTerminal.CancelEdit();}" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" style="text-align: right; display: none" id="TdCombo">
                                                                        <dxe:ASPxComboBox ID="ASPxComboBox1" runat="server" ClientInstanceName="btnC" OnCallback="ASPxComboBox1_Callback"
                                                                            ValueType="System.String" BackColor="#C1D7F8" ForeColor="#C1D7F8">
                                                                            <Border BorderColor="#C1D7F8" />
                                                                            <ButtonStyle BackColor="#C1D7F8" ForeColor="#C1D7F8">
                                                                                <BorderBottom BorderColor="#C1D7F8" BorderStyle="None" />
                                                                                <Border BorderColor="#C1D7F8" BorderStyle="None" />
                                                                                <BorderLeft BorderStyle="None" />
                                                                                <DisabledStyle BackColor="#C1D7F8">
                                                                                </DisabledStyle>
                                                                            </ButtonStyle>
                                                                            <DropDownButton Visible="False">
                                                                            </DropDownButton>
                                                                            <ClientSideEvents EndCallback="function(s, e) {Message(btnC.cpDataExists);}" />
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td style="display: none">
                                                                        <asp:TextBox ID="txtProductTrade_hidden" runat="server"></asp:TextBox>
                                                                        <asp:TextBox ID="txtClientTrade_hidden" runat="server"></asp:TextBox>
                                                                        <asp:TextBox ID="txtAllTrade_hidden" runat="server"></asp:TextBox>
                                                                        <asp:TextBox ID="txtContactName_hidden" runat="server"></asp:TextBox>
                                                                        <asp:TextBox ID="txtBrokername_hidden" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditForm>
                                                    </Templates>
                                                    <ClientSideEvents EndCallback="function(s,e){CheckingTD(gridTerminal.cpExist);}" />
                                                </dxe:ASPxGridView>
                                            </dxe:ContentControl>
                                        </ContentCollection>
                                    </dxe:TabPage>


                                    <dxe:TabPage Name="UDF" Text="UDF" Visible="false">
                                        <ContentCollection>
                                            <dxe:ContentControl runat="server">
                                            </dxe:ContentControl>
                                        </ContentCollection>
                                    </dxe:TabPage>
                                    <dxe:TabPage Name="Correspondence" Text="Correspondence">
                                        <ContentCollection>
                                            <dxe:ContentControl runat="server">
                                            </dxe:ContentControl>
                                        </ContentCollection>
                                    </dxe:TabPage>
                                    <dxe:TabPage Name="Documents" Text="Documents">
                                        <ContentCollection>
                                            <dxe:ContentControl runat="server">
                                            </dxe:ContentControl>
                                        </ContentCollection>
                                    </dxe:TabPage>
                                </TabPages>
                                <ClientSideEvents ActiveTabChanged="function(s, e) {
	                                            var activeTab   = page.GetActiveTab();
	                                            
	                                            var Tab2 = page.GetTab(2);
	                                           
	                                           
	                                            
	                                            if(activeTab == Tab2)
	                                            {
	                                                disp_prompt('tab2');
	                                            }
	                                            if(activeTab == page.GetTab(3))
	                                            {
	                                                disp_prompt('tab3');
	                                            }
	                                            if(activeTab == page.GetTab(4))
	                                            {
	                                                disp_prompt('tab4');
	                                            }
	                                            }"></ClientSideEvents>
                            </dxe:ASPxPageControl>

                            <asp:SqlDataSource ID="sqlCompany" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
                                SelectCommand="select cmp_internalId,cmp_name from tbl_master_company"></asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlExchange" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
                                SelectCommand="select exch_internalId,(select exh_shortName from tbl_master_exchange where exh_cntId=tbl_master_companyExchange.exch_exchId)+'-'+ exch_segmentId as Exchange from tbl_master_companyExchange where exch_compId=@CompID">
                                <SelectParameters>
                                    <asp:SessionParameter Name="CompID" SessionField="ID" Type="string" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlParentTerminal" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
                                SelectCommand="select distinct TradingTerminal_TerminalID from Master_TradingTerminal"></asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlVendor" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
                                SelectCommand="select CTCLVendor_ID,CTCLVendor_Name+' ['+CTCLVendor_ProductType+']' as CTCLVendor_Name from Master_CTCLVendor where CTCLVendor_ExchangeSegment=@CompID1">
                                <SelectParameters>
                                    <asp:SessionParameter Name="CompID1" SessionField="ID1" Type="string" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <asp:SqlDataSource ID="TrdTerminal" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
                                SelectCommand="select td.TradingTerminal_ID,e.exh_shortName+'-'+ce.exch_segmentId as Exchange,td.TradingTerminal_TerminalID,td.TradingTerminal_ParentTerminalID,td.TradingTerminal_ProTradeID,td.TradingTerminal_brokerid ,(select isnull(cnt_firstName,'') +' '+isnull(cnt_middleName,'')+' '+isnull(cnt_lastName,'')+ ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' from tbl_master_contact where cnt_internalId=td.TradingTerminal_BrokerID) as brokid,(Select  ISNULL(ltrim(rtrim(cnt_firstName)), '') + ' ' + ISNULL(ltrim(rtrim(cnt_middleName)), '')   + ' ' + ISNULL(ltrim(rtrim(cnt_lastName)), '') + ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' AS cnt_firstName from tbl_master_contact WHERE  cnt_internalid =td.TradingTerminal_ProTradeID) as ProTradeID,(Select  ISNULL(ltrim(rtrim(cnt_firstName)), '') + ' ' + ISNULL(ltrim(rtrim(cnt_middleName)), '')   + ' ' + ISNULL(ltrim(rtrim(cnt_lastName)), '') + ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' AS cnt_firstName from tbl_master_contact WHERE  cnt_internalid =td.TradingTerminal_CliTradeID) as CliTradeID,(Select  ISNULL(ltrim(rtrim(cnt_firstName)), '') + ' ' + ISNULL(ltrim(rtrim(cnt_middleName)), '')   + ' ' + ISNULL(ltrim(rtrim(cnt_lastName)), '') + ' [' + isnull(ltrim(rtrim(cnt_UCC)),'') + '] ' AS cnt_firstName from tbl_master_contact WHERE  cnt_internalid =td.TradingTerminal_AllTradeID) as AllTradeID from  Master_TradingTerminal td, tbl_master_exchange e, tbl_master_companyExchange ce where td.TradingTerminal_CompanyID=ce.exch_compId  and td.TradingTerminal_ExchangeSegmentID=ce.exch_InternalId and  e.exh_cntId=ce.exch_exchId and td.TradingTerminal_BranchID=@BranchID order by TradingTerminal_ID desc"
                                DeleteCommand="delete from Master_TradingTerminal where TradingTerminal_ID=@TradingTerminal_ID">
                                <SelectParameters>
                                    <asp:SessionParameter Name="BranchID" SessionField="KeyVal_InternalID" Type="string" />
                                </SelectParameters>
                                <DeleteParameters>
                                    <asp:Parameter Name="TradingTerminal_ID" Type="Int32" />
                                </DeleteParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

     <%--Branch Head--%>
        <div class="modal fade" id="BranchHeadModel" role="dialog">
            <div class="modal-dialog">               
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Branch Head</h4>
                    </div>
                    <div class="modal-body">
                        <input type="text" onkeydown="BranchHeadModelkeydown(event)" id="txtBranchHeadSearch" class="form-control" autofocus width="100%" placeholder="Search By Employee Name" />

                        <div id="BranchHeadModelTable">
                            <table border='1' width="100%" >
                                <tr class="HeaderStyle">
                                    <th class="hide">id</th>
                                    <th>Name</th>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <%--Branch Head--%>
</asp:Content>
