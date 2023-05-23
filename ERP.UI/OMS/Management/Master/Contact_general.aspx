<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                01/01/2023        V2.0.38          Pallab              Salesman add/edit page design modification 
2.0                21/02/2023        V2.0.39          Pallab              Master module design modification. refer: 25656
3.0                24/04/2023        V2.0.39          Pallab              Two Console error after loading "Add/Edit Salesman/Agents" module. refer: 25901
====================================================== Revision History ==========================================================--%>

<%@ Page Language="C#" MasterPageFile="~/OMS/MasterPage/Erp.Master" AutoEventWireup="True"
    Inherits="ERP.OMS.Management.Master.management_Master_Contact_general" CodeBehind="Contact_general.aspx.cs" EnableEventValidation="false" %>

<%@ Register Src="~/OMS/Management/Master/UserControls/GSTINSettings.ascx" TagPrefix="GSTIN" TagName="gstinSettings" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/assests/js/init.js"></script>
    <script type="text/javascript" src="/assests/js/ajax-dynamic-list.js"></script>
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <script type="text/javascript">

        function InvalidUDF() {
            jAlert("Udf is mandatory.", "Alert", function () { OpenUdf(); });

        }


        function CheckContactStatus(Cid) {
            var chkid = Cid.GetValue();
            $.ajax({
                type: "POST",
                data: JSON.stringify({ Cid: chkid }),
                url: 'Contact_general.aspx/CheckContactStatus',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var list = msg.d;

                    if (list == 1) {
                        jAlert('To active this customer From Dormant You must enter Billing address<br/> details: Address, Phone, Country, State, City, PIN Code.');
                        return false;
                    }


                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                }
            });
        }

        function registeredCheckChangeEvent() {
            debugger;
            var contype = '<%= Session["Contactrequesttype"] %>';
            if (contype != 'customer') {
                if ($("#radioregistercheck").find(":checked").val() == '1') {



                    $("#divGSTIN").show();
                }
                else {
                    $("#divGSTIN").hide();
                    ctxtGSTIN111.SetText('');
                    ctxtGSTIN222.SetText('');
                    ctxtGSTIN333.SetText('');
                }
            }
        }

        function changeFunc() {
            if (document.getElementById('hdIsMainAccountInUse').value == "IsInUse") {
                jAlert("Transaction exists for the selected Ledger. Cannot proceed.");
                ChangeselectedMainActvalue();
            } else {
                var MainAccount_val2 = document.getElementById("lstTaxRates_MainAccount").value;
                document.getElementById("hndTaxRates_MainAccount_hidden").value = document.getElementById("lstTaxRates_MainAccount").value;
            }
        }

        function ChangeselectedMainActvalue() {
            var lstTaxRates_MainAccount = document.getElementById("lstTaxRates_MainAccount");
            if (document.getElementById("hndTaxRates_MainAccount_hidden").value != '') {
                for (var i = 0; i < lstTaxRates_MainAccount.options.length; i++) {
                    if (lstTaxRates_MainAccount.options[i].value == document.getElementById("hndTaxRates_MainAccount_hidden").value) {
                        lstTaxRates_MainAccount.options[i].selected = true;
                    }
                }
                $('#lstTaxRates_MainAccount').trigger("chosen:updated");
            }
        }
        function ChangeSourceMainAccount() {
            var fname = "%";
            var lReportTo = $('select[id$=lstTaxRates_MainAccount]');
            lReportTo.empty();

            $.ajax({
                type: "POST",
                url: "Contact_general.aspx/GetMainAccountList",
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

                            $('#lstTaxRates_MainAccount').append($('<option>').text(name).val(id));
                        }

                        $(lReportTo).append(listItems.join(''));

                        $('#lstTaxRates_MainAccount').trigger("chosen:updated");
                        ChangeselectedMainActvalue();
                    }
                    else {
                        $('#lstTaxRates_MainAccount').trigger("chosen:updated");
                    }
                }
            });
        }


        $(document).ready(function () {

            var contype = '<%= Session["Contactrequesttype"] %>';
            if (contype == 'customer' || contype == 'Transporter') {

                if ($("#radioregistercheck").find(":checked").val() == '1') {
                    $("#spanmandategstn").attr('style', 'display:inline;color:red');
                    // alert(1);
                    ctxtGSTIN111.SetEnabled(true);
                    ctxtGSTIN222.SetEnabled(true);
                    ctxtGSTIN333.SetEnabled(true);
                }
                else {
                    // alert(2);
                    ctxtGSTIN111.SetText('');
                    ctxtGSTIN222.SetText('');
                    ctxtGSTIN333.SetText('');

                    ctxtGSTIN111.SetEnabled(false);
                    ctxtGSTIN222.SetEnabled(false);
                    ctxtGSTIN333.SetEnabled(false);
                    $("#spanmandategstn").attr('style', 'display:none;');
                }
            }


            ChangeAssociatedEMPSource();



            function ChangeAssociatedEMPSource() {
                var fname = "%";
                var lAssociatedEmployee = $('select[id$=lstAssociatedEmployee]');
                lAssociatedEmployee.empty();

                $.ajax({
                    type: "POST",
                    url: "Contact_general.aspx/ALLEmployee",
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

                                $('#lstAssociatedEmployee').append($('<option>').text(name).val(id));

                            }

                            $(lAssociatedEmployee).append(listItems.join(''));

                            lstAssociatedEmployee();
                            $('#lstAssociatedEmployee').trigger("chosen:updated");

                            ChangeselectedEmployeevalue();

                        }
                        else {
                            $('#lstAssociatedEmployee').trigger("chosen:updated");

                        }
                    }
                });
            }
            function lstAssociatedEmployee() {

                $('#lstAssociatedEmployee').fadeIn();

            }

            function ChangeselectedEmployeevalue() {
                var lstAssociatedEmployee = document.getElementById("lstAssociatedEmployee");

                if (document.getElementById("hidAssociatedEmp").value != '') {
                    for (var i = 0; i < lstAssociatedEmployee.options.length; i++) {
                        if (lstAssociatedEmployee.options[i].value == document.getElementById("hidAssociatedEmp").value) {
                            lstAssociatedEmployee.options[i].selected = true;
                        }
                    }
                    $('#lstAssociatedEmployee').trigger("chosen:updated");
                }

            }


            $("#lstAssociatedEmployee").chosen().change(function () {
                var empcode = $(this).val();
                document.getElementById('hidAssociatedEmp').value = empcode;
                $.ajax({
                    type: "POST",
                    url: "Contact_general.aspx/EmployeeName",
                    data: JSON.stringify({ Empcode: empcode }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var list = msg.d;
                        var listItems = [];
                        if (list.length > 0) {


                            var empcode = '';
                            var name = '';
                            empcode = list[0].split('|')[1];
                            name = list[0].split('|')[0];

                            // txtClentUcc.SetText(empcode);
                            txtFirstNmae.SetText(name);

                        }

                    }
                });



            })

        });



            //Debjyoti GstIN for Customer
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

                if ($("#<%=hddnApplicationMode.ClientID%>").val() === "E") {

                    td_Applicablefrom.style.display = "block";
                }
                else {
                    td_Applicablefrom.style.display = "block";
                }
            }

            function Gstin2TextChanged(s, e) {
                debugger;
                if (!e.htmlEvent.ctrlKey) {
                    if (e.htmlEvent.key != 'Control') {
                        s.SetText(s.GetText().toUpperCase());
                    }
                }

                if ($("#<%=hddnApplicationMode.ClientID%>").val() === "E") {

                    td_Applicablefrom.style.display = "block";
                }
                else {
                    td_Applicablefrom.style.display = "block";
                }



            }
            function Gstin3TextChanged(s, e) {
                debugger;
                if ($("#<%=hddnApplicationMode.ClientID%>").val() === "E") {

                td_Applicablefrom.style.display = "block";
            }
            else {
                    td_Applicablefrom.style.display = "block";
            }
        }

        //End Here
        function AddNewexecutive() {


            var table = document.getElementById("executiveTable");
            var row = table.insertRow(0);
            var cell1 = row.insertCell(0);
            var cell2 = row.insertCell(1);
            cell1.innerHTML = "<input type='text'  maxlength = '18'  />";
            cell2.innerHTML = "<button type='button' value='' onclick='AddNewexecutive()' class='btn btn-primary btn-xs'><i class='fa fa-plus-circle'></i></button> <button type='button' value='' class='btn btn-danger btn-xs' onclick='removeExecutive(this.parentNode.parentNode)'><i class='fa fa-times-circle'></i></button>";

        }
        function removeExecutive(obj) {
            var rowIndex = obj.rowIndex;
            var table = document.getElementById("executiveTable");
            if (table.rows.length > 1) {
                table.deleteRow(rowIndex);
            } else {
                jAlert('Cannot delete all Vechile.');
            }
        }

        function SetVechileNo() {

            var flag = true;
            var table = document.getElementById("executiveTable");
            document.getElementById('VehicleNo_hidden').value = '';
            var data = '';
            if ('<%= Session["Contactrequesttype"] %>' == 'Transporter') {


                    for (var i = 0, row; row = table.rows[i]; i++) {
                        for (var j = 0, col; col = row.cells[j]; j++) {
                            if (col.children[0].type != 'button') {
                                if (data == '') {
                                    if (col.children[0].value == '') {

                                        //jAlert("Vehicle number required");                                    
                                        //flag = false;
                                    }
                                    else {

                                        data = col.children[0].value;
                                    }
                                }
                                else {
                                    if (col.children[0].value == '') {

                                        jAlert("Required");
                                        //  return false;
                                        flag = false;
                                    }
                                    else {
                                        //alert('4');
                                        data = data + '~' + col.children[0].value;
                                    }
                                }
                            }
                        }

                        if (document.getElementById('VehicleNo_hidden').value == '') {
                            document.getElementById('VehicleNo_hidden').value = data;
                            data = '';
                        }
                        else {
                            document.getElementById('VehicleNo_hidden').value = document.getElementById('VehicleNo_hidden').value + ',' + data;
                            data = '';
                        }
                    }


                }
                //  alert(document.getElementById('VehicleNo_hidden').value);

                return flag;
            }

            $(document).ready(function () {
                $('#cmbLegalStatus').change();
                td_Applicablefrom.style.display = "block";
                if ('<%= Session["Contactrequesttype"] %>' == 'Transporter') {
                    loadExecutiveNameFromField();
                }
            });

            function loadExecutiveNameFromField() {


                var table = document.getElementById("executiveTable");
                var exeName = document.getElementById('VehicleNo_hidden').value;
                //  alert(exeName);

                if (exeName != "") {
                    var values = exeName.split(',');
                    for (var i = 0 ; i < values.length; i++) {

                        if (table.rows[0].cells[0].children[0].value.trim() != '') {
                            var row = table.insertRow(0);
                            var cell1 = row.insertCell(0);
                            var cell2 = row.insertCell(1);
                            cell1.innerHTML = "<input type='text'  value='" + values[i] + "'/>";
                            cell2.innerHTML = "<button type='button' value='' onclick='AddNewexecutive()' class='btn btn-primary btn-xs'><i class='fa fa-plus-circle'></i></button> <button type='button' value='' class='btn btn-danger btn-xs' onclick='removeExecutive(this.parentNode.parentNode)' ><i class='fa fa-times-circle'></i></button>";
                        }
                        else {
                            table.rows[0].cells[0].innerHTML = "<input type='text'  value='" + values[i].toString() + "' novalidate />";

                        }
                    }

                }

            }


            //Code for UDF Control 
            function OpenUdf() {
                if (document.getElementById('IsUdfpresent').value == '0') {
                    jAlert("UDF not define.");
                }
                else {
                    var url = 'frm_BranchUdfPopUp.aspx?Type=' + document.getElementById('hdKeyVal').value + '&&KeyVal_InternalID=' + document.getElementById('KeyVal_InternalID').value;
                    popup.SetContentUrl(url);
                    popup.Show();
                }
                return true;
            }
            // End Udf Code



            function fn_ctxtClentUcc_Name_TextChanged() {
                var procode = 0;
                //var ProductName = ctxtPro_Name.GetText();
                var clientName = txtClentUcc.GetText();
                $.ajax({
                    type: "POST",
                    url: "Contact_general.aspx/CheckUniqueName",
                    //data: "{'ProductName':'" + ProductName + "'}",
                    data: JSON.stringify({ clientName: clientName, procode: procode }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = msg.d;

                        if (data.split('~')[0] == "True") {
                            jAlert("Already exists for " + data.split('~')[1]);

                            txtClentUcc.SetText("");
                            //document.getElementById("Popup_Empcitys_ctxtPro_Code_I").focus();
                            document.getElementById("txtClentUcc").focus();

                            return false;
                        }
                    }

                });
            }

            $(document).ready(function () {
                $("#<%=btnCancel.ClientID %>").click(function () {
                var url = 'frmContactMain.aspx?requesttype=<%= Session["Contactrequesttype"] %>';
                window.location.href = url;
                return false;
            });

          <%--  if ('<%= Session["Contactrequesttype"] %>' != 'customer' || '<%= Session["Contactrequesttype"] %>' != 'Transporter') {
               
                $('.forCustomer').hide();
            }--%>

            if ('<%= Session["Contactrequesttype"] %>' == 'customer') {

                $('.forCustomer').show();
                // $('.Trprofessionother').hide();
                // document.getElementById("Trprofessionother").style.display = 'none';
            }
            else if ('<%= Session["Contactrequesttype"] %>' == 'Transporter') {
                $('.forCustomer').show();
            }
            else { $('.forCustomer').hide(); }



        });
        function onlyNumbers(evt) {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;

        }
        function ul() {
            window.opener.document.getElementById('iFrmInformation').setAttribute('src', 'CallUserInformation.aspx')
        }
        function ContactStatus() {
            var comboid = document.getElementById('ASPxPageControl1_cmbContactStatus');
            var comboval = comboid.value;
            if (comboval == '1') {
                document.getElementById("TrContact").style.display = 'none';
            }
            else {
                document.getElementById("TrContact").style.display = 'inline';
            }
        }
        function disp_prompt(name) {
            if (name == "tab0") {
                //alert(name);
                document.location.href = "Contact_general.aspx";
            }
            if (name == "tab1") {
                //alert(name);
                document.location.href = "Contact_Correspondence.aspx";
            }
            else if (name == "tab2") {
                //alert(name);
                document.location.href = "Contact_BankDetails.aspx";
            }
            else if (name == "tab3") {
                //alert(name);
                document.location.href = "Contact_DPDetails.aspx";
            }
            else if (name == "tab4") {
                //alert(name);
                document.location.href = "Contact_Document.aspx";
            }
            else if (name == "tab12") {
                //alert(name);
                document.location.href = "Contact_FamilyMembers.aspx";
            }
            else if (name == "tab5") {
                //alert(name);
                document.location.href = "Contact_Registration.aspx";
            }
            else if (name == "tab7") {
                //alert(name);
                document.location.href = "Contact_GroupMember.aspx";
            }
            else if (name == "tab8") {
                //alert(name);
                document.location.href = "Contact_Deposit.aspx";
            }
            else if (name == "tab9") {
                //alert(name);
                document.location.href = "Contact_Remarks.aspx";
            }
            else if (name == "tab10") {
                //alert(name);
                document.location.href = "Contact_Education.aspx";
            }
            else if (name == "tab11") {
                //alert(name);
                document.location.href = "contact_brokerage.aspx";
            }
            else if (name == "tab6") {
                //alert(name);
                document.location.href = "contact_other.aspx";
            }
            else if (name == "tab13") {
                document.location.href = "contact_Subscription.aspx";
            }

        }
        function CallList(obj1, obj2, obj3) {
            var sourceID = document.getElementById("cmbSource");
            if (sourceID.value == '21' || sourceID.value == '3' || sourceID.value == '4' || sourceID.value == '8' || sourceID.value == '10' || sourceID.value == '19' || sourceID.value == '20' || sourceID.value == '14' || sourceID.value == '24' || sourceID.value == '25')// || sourceID.value=='18')
            {
                //alert(sourceID.value);
                var obj4 = document.getElementById("cmbSource");
                var obj5 = obj4.value;
                //alert(obj5);
                ajax_showOptions(obj1, obj2, obj3, obj5, 'Sub');
            }
        }
        function LDCallList(obj1, obj2, obj3) {
            var sourceID = document.getElementById("LDcmbSource");
            if (sourceID.value == '21' || sourceID.value == '3' || sourceID.value == '4' || sourceID.value == '8' || sourceID.value == '10' || sourceID.value == '19' || sourceID.value == '20' || sourceID.value == '14' || sourceID.value == '24' || sourceID.value == '25')// || sourceID.value=='18')
            {
                //alert(sourceID.value);
                var obj4 = document.getElementById("LDcmbSource");
                var obj5 = obj4.value;
                //alert(obj5);
                ajax_showOptions(obj1, obj2, obj3, obj5, 'Sub');
            }
        }


        function legalStatus() {
            document.getElementById("cmbLegalStatus").focus();
            var elt = document.getElementById("cmbLegalStatus");
            var ss = elt.options[elt.selectedIndex].text;




            var isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
            // Opera 8.0+ (UA detection to detect Blink/v8-powered Opera)
            var isFirefox = typeof InstallTrigger !== 'undefined';   // Firefox 1.0+
            var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
            // At least Safari 3+: "[object HTMLElementConstructor]"
            var isChrome = !!window.chrome && !isOpera;              // Chrome 1+
            var isIE = /*@cc_on!@*/false || !!document.documentMode;   // At least IE6






            if ((ss.indexOf("Individual") <= -1)) {



                if (isIE == true) {


                    document.getElementById("td_lAnniversary").style.display = 'none';
                    document.getElementById("td_tAnniversary").style.display = 'none';
                    document.getElementById("td_lGender").style.display = 'none';
                    document.getElementById("td_dGender").style.display = 'none';
                    document.getElementById("td_lMarital").style.display = 'none';
                    document.getElementById("td_dMarital").style.display = 'none';

                }

                var eggs = document.getElementsByClassName('visF');


                for (var i = 0; i < eggs.length; i++) {
                    eggs[i].style.display = 'none';
                }
            }
            else {

                if (isIE == true) {

                    document.getElementById("td_lAnniversary").style.display = 'inline';
                    document.getElementById("td_tAnniversary").style.display = 'inline';
                    document.getElementById("td_lGender").style.display = 'block';
                    document.getElementById("td_dGender").style.display = 'block';
                    document.getElementById("td_lMarital").style.display = 'block';
                    document.getElementById("td_dMarital").style.display = 'block';


                }

                var eggs = document.getElementsByClassName('visF');
                for (var i = 0; i < eggs.length; i++) {
                    eggs[i].style.display = 'block';
                }
            }

            var SID1 = document.getElementById("cmbLegalStatus");

            if (SID1.value == '21') {

                document.getElementById("td_red").style.display = 'inline';
                document.getElementById("td_green").style.display = 'none';
                document.getElementById("td_one").style.display = 'inline';
                document.getElementById("td_two").style.display = 'none';
                document.getElementById("td_only").style.display = 'none';

            }
            else if (SID1.value == '2' || SID1.value == '3' || SID1.value == '17' || SID1.value == '18' || SID1.value == '28' || SID1.value == '47' || SID1.value == '48') {

                document.getElementById("td_red").style.display = 'inline';
                document.getElementById("td_green").style.display = 'none';
                document.getElementById("td_one").style.display = 'none';
                document.getElementById("td_two").style.display = 'inline';
                document.getElementById("td_only").style.display = 'none';
            }
            else {
                document.getElementById("td_red").style.display = 'none';
                document.getElementById("td_green").style.display = 'inline';
                document.getElementById("td_one").style.display = 'none';
                document.getElementById("td_two").style.display = 'none';
                document.getElementById("td_only").style.display = 'inline';
            }
            if (SID1.value == '1' || SID1.value == '30' || SID1.value == '31' || SID1.value == '32' || SID1.value == '52' || SID1.value == '29' || SID1.value == '33' || SID1.value == '34' || SID1.value == '54') {


                document.getElementById("Trincorporation").style.display = 'none';
            }
            else {

                document.getElementById("Trincorporation").style.display = 'inline';
            }




        }
        function SourceStatus() {
            var sourceID = document.getElementById("cmbSource");
            if (sourceID.value == '21' || sourceID.value == '3' || sourceID.value == '4' || sourceID.value == '8' || sourceID.value == '10' || sourceID.value == '19' || sourceID.value == '20' || sourceID.value == '14' || sourceID.value == '24' || sourceID.value == '25' || sourceID.value == '18') {
                document.getElementById("TdRfferedBy").style.display = 'inline';
                document.getElementById("TdRfferedBy1").style.display = 'inline';
            }
            else {
                document.getElementById("TdRfferedBy").style.display = 'none';
                document.getElementById("TdRfferedBy1").style.display = 'none';
            }
        }

        function showcountry() {
            var countryID = document.getElementById("ddlnational");
            //if (countryID.value == '1')
            //    document.getElementById("td_country").style.display = 'none';
            //else
            //    document.getElementById("td_country").style.display = 'inline';
        }
        function hideotherstatus() {

            document.getElementById("Trprofessionother").style.display = 'none';
        }
        function ProfessionStatus() {
            var professionID = document.getElementById("cmbProfession");
            if (professionID.value == '20')
                document.getElementById("Trprofessionother").style.display = 'inline';
            else
                document.getElementById("Trprofessionother").style.display = 'none';
        }
        function PageLoad() {
            var PID = document.getElementById("cmbProfession");

            if (PID.value == '20')
                document.getElementById("Trprofessionother").style.display = 'inline';
            else
                document.getElementById("Trprofessionother").style.display = 'none';
            var countryID = document.getElementById("ddlnational");
            //if (countryID.value == '1')
            //    document.getElementById("td_country").style.display = 'none';
            //else
            //    document.getElementById("td_country").style.display = 'inline';
            //var ID = document.getElementById("cmbSource");
            ////  alert(ID.value);
            //if (ID.value == '21' || ID.value == '3' || ID.value == '4' || ID.value == '8' || ID.value == '10' || ID.value == '19' || ID.value == '20' || ID.value == '14' || ID.value == '18') {

            //    document.getElementById("TdRfferedBy").style.display = 'inline';
            //    document.getElementById("TdRfferedBy1").style.display = 'inline';

            //}
            //else {

            //    document.getElementById("TdRfferedBy").style.display = 'none';
            //    document.getElementById("TdRfferedBy1").style.display = 'none';



            //}
            var ID1 = document.getElementById("cmbLegalStatus");
            if (ID1.value == '21') {

                document.getElementById("td_red").style.display = 'inline';
                document.getElementById("td_green").style.display = 'none';
                document.getElementById("td_one").style.display = 'inline';
                document.getElementById("td_two").style.display = 'none';
                document.getElementById("td_only").style.display = 'none';
            }
            else if (ID1.value == '2' || ID1.value == '3' || ID1.value == '17' || ID1.value == '18' || ID1.value == '28' || ID1.value == '47' || ID1.value == '48') {

                document.getElementById("td_red").style.display = 'inline';
                document.getElementById("td_green").style.display = 'none';
                document.getElementById("td_one").style.display = 'none';
                document.getElementById("td_two").style.display = 'inline';
                document.getElementById("td_only").style.display = 'none';
            }
            else {
                /*Rev 3.0*/
                //document.getElementById("td_red").style.display = "none";
                //document.getElementById("td_green").style.display = 'inline';
                /*Rev end 3.0*/
                document.getElementById("td_one").style.display = 'none';
                document.getElementById("td_two").style.display = 'none';
                /*Rev 3.0*/
                /*document.getElementById("td_only").style.display = 'inline';*/
                /*Rev end 3.0*/




            }

            if (ID1.value == '1' || ID1.value == '30' || ID1.value == '31' || ID1.value == '32' || ID1.value == '52' || ID1.value == '29' || ID1.value == '33' || ID1.value == '34' || ID1.value == '54') {


                document.getElementById("Trincorporation").style.display = 'none';
            }
            else {

                document.getElementById("Trincorporation").style.display = 'inline';
            }
            var comboid = document.getElementById('cmbContactStatus');
            /*Rev 3.0*/
            /*var comboval = comboid.value;*/
            var comboval = comboid?.value || '';
            /*Rev end 3.0*/
            if (comboval == '1') {
                document.getElementById("TrContact").style.display = 'none';
            }
            else {
                document.getElementById("TrContact").style.display = 'inline';

            }
        }
        function FillValues(chk) {
            var sel = document.getElementById('LitSpokenLanguage');
            sel.value = chk;
        }
        function FillValues1(chk) {
            var sel = document.getElementById('LitWrittenLanguage');
            sel.value = chk;
        }
        function keyVal(obj) {
            var objPhEmail = obj.split('~');
            document.getElementById('txtRPartner_hidden').value = objPhEmail[0];
            document.getElementById('TxtEmail').value = objPhEmail[1];
            document.getElementById('TxtPhone').value = objPhEmail[2];
        }
        function popup() {
            alert("Please type prefix of UCC");
            return false;

        }

        //function dateValidationFormat(e) {
        //    var v = e.target.value; //this.value;
        //    if (v.match(/^\d{2}$/) !== null) {
        //        e.target.value = v + '-';
        //    } else if (v.match(/^\d{2}\-\d{2}$/) !== null) {
        //        e.target.value = v + '-';
        //    }
        //}

        //function isDateKey(evt) {
        //    var charCode = (evt.which) ? evt.which : event.keyCode
        //    console.log(charCode);

        //    if (charCode > 31 && (charCode < 48 || charCode > 57))
        //        return false;
        //    return true;
        //}


        function fn_btnValidate(s, e) {
            debugger;
            //  e.processOnServer = false;
            var ret = true;
            var validateFlag = true;
            var contype = '<%= Session["Contactrequesttype"] %>';

            if (contype == 'customer') {

                var clientUcc = $('#txtClentUcc_I').val();



                if ($('#ddlIdType').val() == 1) {
                    if (isNaN(clientUcc)) {
                        jAlert("Please type valid Phone No.");
                        //.processOnServer=false;
                        ret = false;

                    }
                } else if ($('#ddlIdType').val() == 2) {

                    var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                    var code = /([C,P,H,F,A,T,B,L,J,G])/;
                    var code_chk = clientUcc.substring(3, 4);
                    if (clientUcc.search(panPat) == -1) {
                        jAlert("Please type valid PAN No.");
                        ret = false;
                    }
                    if (code.test(code_chk) == false) {
                        jAlert("Please type valid PAN No.");
                        ret = false;
                    }

                } else if ($('#ddlIdType').val() == 3) {
                    if (isNaN(clientUcc) || clientUcc.length != 12) {
                        jAlert("Please type valid Aadhar No.");
                        ret = false;
                    }
                }


                Page_ClientValidate();
                $('#invalidGst').css({ 'display': 'none' });
                var gst1 = ctxtGSTIN111.GetText().trim();
                var gst2 = ctxtGSTIN222.GetText().trim();
                var gst3 = ctxtGSTIN333.GetText().trim();

                var isregistered = $('#<%=radioregistercheck.ClientID %> input:checked').val();

                if (gst1.length == 0 && gst2.length == 0 && gst3.length == 0) {
                    if (isregistered == 1) {

                        jAlert('GSTIN is mandatory.');
                        ret = false;
                    }


                }
                else {
                    if (gst1.length != 2 || gst2.length != 10 || gst3.length != 3) {
                        $('#invalidGst').css({ 'display': 'block' });
                        ret = false;
                    }


                    var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                    var code = /([C,P,H,F,A,T,B,L,J,G])/;
                    var code_chk = gst2.substring(3, 4);
                    if (gst2.search(panPat) == -1) {
                        $('#invalidGst').css({ 'display': 'block' });
                        ret = false;
                    }
                    if (code.test(code_chk) == false) {
                        $('#invalidGst').css({ 'display': 'block' });
                        ret = false;
                    }
                }

                var isregisteredCheck = $('#<%=radioregistercheck.ClientID %> input:checked').val();
                var finalGST = (gst1 + gst2 + gst3);
                var GSTINOldval = $("#<%=hddnGSTIN2Val.ClientID%>").val();

                 


                if (cApplicableFrom.GetDate() === null && $("#<%=hddnApplicationMode.ClientID%>").val() === "E" && GSTINOldval.trim() !== finalGST.trim()) {
                    if (isregisteredCheck == "1") {

                        jAlert("Please enter Applicable from.");
                        ret = false;
                        validateFlag = false;
                    }
                }
               <%-- else if (cApplicableFrom.GetDate() === null && $("#<%=hddnApplicationMode.ClientID%>").val() === "E")
                {
                    if (isregisteredCheck == "1") {

                        jAlert("Please enter Applicable from.");
                        ret = false;
                        validateFlag = false;
                    }
                }--%>



                if (GSTINOldval.trim() !== finalGST.trim() && isregisteredCheck == "1" && validateFlag == true &&  $("#<%=hddnApplicationMode.ClientID%>").val() === "E") {

                    //var t = jConfirm("You have entered GSTIN");

                    var r = confirm("You have entered GSTIN Applicable date. Based on the applicable date all the transaction will be updated with entered GSTIN. \nAre you sure?");
                    if (r == true) {
                        ret = true;
                    }
                    else  {
                        ret = false;
                    }


                    $("#<%=hddnGSTINFlag.ClientID%>").val("UPDATE");
                }
                else {
                    $("#<%=hddnGSTINFlag.ClientID%>").val("NotUPDATE");
                }




            }

            if (contype != 'Lead') {

                var txtbox = document.getElementById('txtFirstNmae');
                var txt2 = document.getElementById('txtClentUcc');

                if (txtbox.value == '') {
                    alert("Please Insert Full Name.");
                    document.getElementById('txtFirstNmae').Focus();
                    //  return false;
                    ret = false;
                }
                else if (txt2.value == '') {
                    alert("Please Insert Unique Code.");
                    document.getElementById('txtClentUcc').Focus();
                    //  return false;
                    ret = false;

                }
            }

            if (contype == 'Lead') {
                setvaluechosen();
            }

            if (contype != 'Lead') {
                // alert(contype);
                var selectoption = document.getElementById("cmbLegalStatus");
                var optionText = selectoption.options[selectoption.selectedIndex].text;

                //  alert(optionText);
                if (optionText == 'Local') {

                    ret = SetVechileNo();
                }

                var optionText = $('#<%=radioregistercheck.ClientID %> input:checked').val();




                if (optionText == '1' && contype == 'Transporter') {



                    var gst1 = ctxtGSTIN111.GetText().trim();
                    var gst2 = ctxtGSTIN222.GetText().trim();
                    var gst3 = ctxtGSTIN333.GetText().trim();

                    if (gst1.length == 0 && gst2.length == 0 && gst3.length == 0) {
                        ret = false;
                        jAlert("GSTIN is mandatory. ");
                        // $("#spanmandategstn").attr('style', 'display:inline;color:red');
                    }
                    else {
                        if (gst1.length != 2 || gst2.length != 10 || gst3.length != 3) {
                            $('#invalidGst').css({ 'display': 'block' });
                            ret = false;

                            $("#spanmandategstn").attr('style', 'display:inline;color:red');
                        }


                        var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                        var code = /([C,P,H,F,A,T,B,L,J,G])/;
                        var code_chk = gst2.substring(3, 4);
                        if (gst2.search(panPat) == -1) {
                            $('#invalidGst').css({ 'display': 'block' });
                            ret = false;

                            $("#spanmandategstn").attr('style', 'display:inline;color:red');
                        }

                        if (code.test(code_chk) == false) {
                            $('#invalidGst').css({ 'display': 'block' });
                            ret = false;

                            $("#spanmandategstn").attr('style', 'display:inline;color:red');
                        }
                    }
                }




            }

            e.processOnServer = ret;
        }

        function OnContactInfoClick(keyValue, CompName) {
            var keyValue = ('<%=Session["KeyVal_InternalID"] %>');
            var CompName = ('<%=Session["name"] %>');
            var url = 'insurance_contactPerson.aspx?id=' + keyValue;
            window.location.href = url;
        }
        function FillUCCCode(chk) {
            var sel = document.getElementById('txtClentUcc');
            sel.value = chk;
        }
        FieldName = 'cmbLegalStatus';

        //for leads
        function LeadCallList(obj1, obj2, obj3) {
            var obj4 = document.getElementById("<%=LDcmbSource.ClientID%>");
            var obj5 = obj4.value;
            //alert(obj5);
            ajax_showOptions(obj1, obj2, obj3, obj5);
            //alert(obj5);
            FieldName = '<%=LDcmbGender.ClientID%>';
        }
        function AtTheTimePageLoad() {
            FieldName = '<%=LDcmbLegalStatus.ClientID%>';

            document.getElementById("<%=LDtxtReferedBy_hidden.ClientID%>").style.display = 'none';
        }


        $(document).ready(function () {

            //for choosen
            ListBind();
            ChangeSource();
            ChangeSourceMainAccount();

            if ($("#radioregistercheck").find(":checked").val() == '1') {
                cApplicableFrom.SetEnabled(true);
            }
            else {
                cApplicableFrom.SetEnabled(false);
            }

            if ('<%=Convert.ToString(Session["requesttype"])%>' == "Customer/Client" || '<%=Convert.ToString(Session["requesttype"])%>' == "Transporter") {
                $('.mainAccount').show();
            }
            else {
                $('.mainAccount').hide();
            }


            $("#RequiredFieldValidator9").css("display", "none");
            //end choosen

            $('#cmbLegalStatus').change(function (s, e) {
                var legalstatus = $("#cmbLegalStatus").val();


                if ((legalstatus != '1') && (legalstatus != '27') && (legalstatus != '29') && (legalstatus != '55') && (legalstatus != '54')) {
                    $('#cmbMaritalStatus').prop('selectedIndex', 0);
                    $('#txtAnniversary').val('');
                    $('#ASPxLabel18').text("Date of Incorporation");
                    //$('#td_lAnniversary').css('display', 'none');
                    //$('#td_tAnniversary').css('display', 'none');
                    //$('#td_lGender').css('display', 'none');
                    //$('#td_dGender').css('display', 'none');
                    //$('#td_lMarital').css('display', 'none');
                    //$('#td_dMarital').css('display', 'none');
                    //$('.txtAnniversary').attr('readonly', 'readonly');
                    //txtAnniversary.SetEnabled(false);

                    $('#cmbGender').attr("disabled", true);
                    $('#cmbGender').css("color", "lightgray");

                    $('#cmbMaritalStatus').attr("disabled", true);
                    $('#cmbMaritalStatus').css("color", "lightgray");

                }
                else {
                    $('#cmbMaritalStatus').prop('selectedIndex', 0);
                    $('#txtAnniversary').val('');
                    $('#ASPxLabel18').text("Date of Birth");
                    $('#ASPxLabel18').text("Date of Birth");
                    //$('#td_lAnniversary').css('display', 'block');
                    //$('#td_tAnniversary').css('display', 'block');
                    //$('#td_lGender').css('display', 'block');
                    //$('#td_dGender').css('display', 'block');
                    //$('#td_lMarital').css('display', 'block');
                    //$('#td_dMarital').css('display', 'block');

                    // txtAnniversary.SetEnabled(true);

                    $('#cmbGender').attr("disabled", false);
                    $('#cmbGender').css("color", "black");

                    $('#cmbMaritalStatus').attr("disabled", false);
                    $('#cmbMaritalStatus').css("color", "black");
                }

            })

            var lgsts = $('#cmbLegalStatus').val();

            if (lgsts != '') {

                if ((lgsts != '1') && (lgsts != '27') && (lgsts != '29') && (lgsts != '55') && (lgsts != '54')) {
                    $('#ASPxLabel18').text("Date of Incorporation");
                    //$('#td_lAnniversary').css('display', 'none');
                    //$('#td_tAnniversary').css('display', 'none');
                    //$('#td_lGender').css('display', 'none');
                    //$('#td_dGender').css('display', 'none');
                    //$('#td_lMarital').css('display', 'none');
                    //$('#td_dMarital').css('display', 'none');
                    //txtAnniversary.SetEnabled(false);

                    $('#cmbGender').attr("disabled", true);
                    $('#cmbGender').css("color", "lightgray");

                    $('#cmbMaritalStatus').attr("disabled", true);
                    $('#cmbMaritalStatus').css("color", "lightgray");

                    //document.getElementById("td_lAnniversary").style.display = 'none';
                    //document.getElementById("td_tAnniversary").style.display = 'none';
                    //document.getElementById("td_lGender").style.display = 'none';
                    //document.getElementById("td_dGender").style.display = 'none';
                    //document.getElementById("td_lMarital").style.display = 'none';
                    //document.getElementById("td_dMarital").style.display = 'none';

                }
                else {

                    $('#ASPxLabel18').text("Date of Birth");
                    // txtAnniversary.SetEnabled(true);

                    $('#cmbGender').attr("disabled", false);

                    $('#cmbMaritalStatus').attr("disabled", false);
                    $('#cmbMaritalStatus').css("color", "black");
                    $('#cmbGender').css("color", "black");
                    //$('#td_lAnniversary').css('display', 'block');
                    //$('#td_tAnniversary').css('display', 'block');
                    //$('#td_lGender').css('display', 'block');
                    //$('#td_dGender').css('display', 'block');
                    //$('#td_lMarital').css('display', 'block');
                    //$('#td_dMarital').css('display', 'block');

                    //document.getElementById("td_lAnniversary").style.display = 'inline';
                    //document.getElementById("td_tAnniversary").style.display = 'inline';
                    //document.getElementById("td_lGender").style.display = 'inline';
                    //document.getElementById("td_dGender").style.display = 'inline';
                    //document.getElementById("td_lMarital").style.display = 'inline';
                    //document.getElementById("td_dMarital").style.display = 'inline';
                }
            }

        })



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
        function lstconverttounit() {

            $('#lstconverttounit').fadeIn();

        }
        function ChangeSource() {


            var fname = "%";
            var lconverttounit = $('select[id$=lstconverttounit]');
            lconverttounit.empty();
            //var CurrentSegment = document.getElementById('hdn_CurrentSegment').value;
            var CurrentSegment = "0";
            var sql = "select (ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') + ' [' + cnt_shortName +']') AS cnt_firstName, cnt_internalid from tbl_master_contact where (cnt_contactType='LD' OR cnt_contactType='EM') and (cnt_firstName like ('" + fname + "%') or cnt_lastName like ('" + fname + "%') or cnt_shortName like ('%" + fname + "%'))";

            $.ajax({
                type: "POST",
                url: "Contact_general.aspx/GetrefBy",
                data: JSON.stringify({ query: sql }),
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

                            $('#lstconverttounit').append($('<option>').text(name).val(id));

                        }

                        $(lconverttounit).append(listItems.join(''));

                        lstconverttounit();
                        $('#lstconverttounit').trigger("chosen:updated");

                        Changeselectedvalue();


                    }
                    else {
                        //   alert("No records found");
                        //lstReferedBy();
                        $('#lstconverttounit').trigger("chosen:updated");

                    }
                }
            });
            // }
        }

        function Changeselectedvalue() {
            var lstconverttounit = document.getElementById("lstconverttounit");
            if (document.getElementById("LDtxtReferedBy_hidden") != null) {
                if (document.getElementById("LDtxtReferedBy_hidden").value != '') {

                    for (var i = 0; i < lstconverttounit.options.length; i++) {
                        if (lstconverttounit.options[i].value == document.getElementById("LDtxtReferedBy_hidden").value) {
                            lstconverttounit.options[i].selected = true;
                        }
                    }
                }

                $('#lstconverttounit').trigger("chosen:updated");
            }
        }

        function lstconverttounit() {

            $('#lstconverttounit').fadeIn();

        }
        function setvaluechosen() {
            console.log('setval');
            document.getElementById("LDtxtReferedBy_hidden").value = document.getElementById("lstconverttounit").value;
            if (document.getElementById("LDtxtReferedBy_hidden").value != '') {

               <%-- document.getElementById('<%= btnShow.ClientID %>').click();--%>
            } else {

                //jAlert("Select ");
            }
            //alert(document.getElementById("LDtxtReferedBy_hidden").value);

        }

        $(function () {




            $('body').on('change', '#cmbLegalStatus', function () {

                var selectoption = document.getElementById("cmbLegalStatus");
                var optionText = selectoption.options[selectoption.selectedIndex].text;

                //  alert(optionText);
                if (optionText == 'General') {
                    $("#pnlVehicleNo").attr('style', 'display:none;');
                }
                else {

                    $("#pnlVehicleNo").attr('style', 'display:block;');
                }

            });



            $('body').on('click', '#radioregistercheck', function () {


                var optionText = $('#<%=radioregistercheck.ClientID %> input:checked').val();

                if (optionText == '1') {
                    $("#spanmandategstn").attr('style', 'display:inline;color:red');
                    td_Applicablefrom.style.display = "block";

                    //alert(1);
                    ctxtGSTIN111.SetEnabled(true);
                    ctxtGSTIN222.SetEnabled(true);
                    ctxtGSTIN333.SetEnabled(true);
                    cApplicableFrom.SetEnabled(true);
                }
                else {
                    //alert(2);
                    td_Applicablefrom.style.display = "block";
                    ctxtGSTIN111.SetText('');
                    ctxtGSTIN222.SetText('');
                    ctxtGSTIN333.SetText('');

                    ctxtGSTIN111.SetEnabled(false);
                    ctxtGSTIN222.SetEnabled(false);
                    ctxtGSTIN333.SetEnabled(false);
                    cApplicableFrom.SetEnabled(false);
                    $("#spanmandategstn").attr('style', 'display:none;');
                }
            });


        }
            );
    </script>
    <style type="text/css">
        #lstAssociatedEmployee {
            width: 100%;
            display: none !important;
        }

        .chosen-container.chosen-container-single {
            width: 100% !important;
        }

        #lstconverttounit {
            display: none !important;
        }

        .chosen-choices {
            width: 100% !important;
        }

        .inline {
            display: inline !important;
            float: left;
            padding-right: 5px;
        }

        .dxtcLite_PlasticBlue.dxtc-top > .dxtc-content {
            overflow: visible !important;
        }



        .abs {
            position: absolute;
            right: -20px;
            top: 10px;
        }

        .fa.fa-exclamation-circle:before {
            font-family: FontAwesome;
        }

        .tp2 {
            right: -18px;
            top: 3px;
            position: absolute;
        }

        #txtCreditLimit_EC {
            position: absolute;
        }

        #txtClentUcc_I {
            opacity: 1 !important;
        }

        #txtClentUcc {
            opacity: 1 !important;
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

        #executiveTable > tbody > tr > td:first-child {
            padding-right: 15px;
        }

        #executiveTable > tbody > tr > td:last-child {
            width: 105px;
        }

        .tp28 {
            top: 28px;
        }
        .dxtc-strip.dxtc-stripContainer {
            height:auto !important;
        }
        .dxbButton_PlasticBlue {
    background-image: none !important;
}

        /*Rev 1.0*/
        .dxtcLite_PlasticBlue
        {
            font-family: 'Poppins', sans-serif !important;
        }

        .dxtcLite_PlasticBlue > .dxtc-stripContainer .dxtc-link
        {
                font-size: 13px;
        }
        .dxtcLite_PlasticBlue > .dxtc-stripContainer .dxtc-activeTab
        {
                background: #165193 !important;
        }

        .dxeTextBox_PlasticBlue, .dxeButtonEdit_PlasticBlue, .dxeIRadioButton_PlasticBlue, .dxeRadioButtonList_PlasticBlue, .dxeCheckBoxList_PlasticBlue
        {
            height: 34px;
            border-radius: 4px;
        }
        .dxeBase_PlasticBlue
        {
            font-family: 'Poppins', sans-serif !important;
            /*margin-top: 10px !important;
            margin-bottom: 5px !important;*/
            font-size: 14px;
            line-height: 18px;

        }

        .lblMtop10
        {
            margin-top: 10px !important;
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
    /*Rev end 1.0*/

    /*Rev 2.0*/

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
        bottom: 8px;
        right: 13px;
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

    .h-branch-select, .h-branch-select-new {
        position: relative;
    }

        .h-branch-select::after {
            /*content: '<';*/
            content: url(../../../assests/images/left-arw.png);
            position: absolute;
            top: 26px;
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
            line-height: 17px;
            z-index: 0;
        }

        .h-branch-select-new::after {
            content: '<';
            /*content: url(../../../assests/images/left-arw.png);*/
            position: absolute;
            top: 9px;
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
        font-size: 14px;
    line-height: 18px;
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

    #dtFrom, #dtTo, #FormDate, #toDate, #proj_start_dt, .calendar-custom-icon {
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

    #dtFrom_B-1, #dtTo_B-1, #cmbDOJ_B-1, #cmbLeaveEff_B-1, #FormDate_B-1, #toDate_B-1, #proj_start_dt_B-1 {
        background: transparent !important;
        border: none;
        width: 30px;
        padding: 10px !important;
    }

        #dtFrom_B-1 #dtFrom_B-1Img,
        #dtTo_B-1 #dtTo_B-1Img, #cmbDOJ_B-1 #cmbDOJ_B-1Img, #cmbLeaveEff_B-1 #cmbLeaveEff_B-1Img, #FormDate_B-1 #FormDate_B-1Img, #toDate_B-1 #toDate_B-1Img, #proj_start_dt_B-1 #proj_start_dt_B-1Img {
            display: none;
        }

    #FormDate_I, #toDate_I, #proj_start_dt_I {
        background: transparent;
    }

    .for-cust-icon {
        position: relative;
        /*z-index: 1;*/
    }

    .pad-md-18 {
        padding-top: 10px;
        padding-left: 5px;
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

    .btn {
        height: 34px;
    }

    .dxpcLite_PlasticBlue .dxpc-header, .dxdpLite_PlasticBlue .dxpc-header {
        padding: 6px 4px 6px 10px;
        background: #094e8c;
    }

    #pcModalMode_PW-1 {
        border-radius: 12px;
        overflow: hidden;
    }

    .cke_bottom {
        background: #094e8c;
    }

    .fff > thead > tr > th {
        background: #094e8c;
    }

    .btn i {
        margin-right: 5px;
    }

    .employeeNameClass {
        padding: 5px 10px;
    }

        .employeeNameClass i {
            margin-right: 5px;
        }

    .timeBox .line {
        margin-bottom: 10px;
    }

    .timeBox {
        padding-bottom: 10px;
    }

    .exprtClass {
        background: #094e8c;
    }

    .dxeListBoxItemSelected_PlasticBlue {
        background-color: #094e8c;
    }

    /*Rev end 2.0*/
    </style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <div class="crossBtn" style="right: 28px;top: 14px;"><a href=""><i class="fa fa-times"></i></a></div>--%>
        <div class="breadCumb">
            <%-- <h3>Contact List</h3>--%>
            <span>
                <asp:Label ID="lblHeadTitle" runat="server"></asp:Label>
            </span>
            <div class="crossBtnN"><a href="frmContactMain.aspx?requesttype=<%= Session["Contactrequesttype"] %>"><i class="fa fa-times"></i></a></div>
        </div>
        

        <%--debjyoti 22-12-2016--%>
        <dxe:ASPxPopupControl ID="ASPXPopupControl" runat="server"
            CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popup" Height="630px"
            Width="600px" HeaderText="Add/Modify UDF" Modal="true" AllowResize="true" ResizingMode="Postponed">
            <contentcollection>
                <dxe:PopupControlContentControl runat="server">
                </dxe:PopupControlContentControl>
            </contentcollection>
        </dxe:ASPxPopupControl>

        <asp:HiddenField ID="hdKeyVal" runat="server" />
        <asp:HiddenField ID="KeyVal_InternalID" runat="server" />
        <asp:HiddenField runat="server" ID="IsUdfpresent" />
        <asp:HiddenField ID="hidAssociatedEmp" runat="server" />
        <asp:HiddenField ID="hddnApplicationMode" runat="server" />
        <asp:HiddenField ID="hddnGSTIN2Val" runat="server" />
        <asp:HiddenField ID="hddnGSTINFlag" runat="server" />
        <%--End debjyoti 22-12-2016--%>
    
    <div class="container">
        <div class="backBox mt-5 p-3 ">
        <table class="TableMain100">
            <tr>
                <td style="text-align: center;">
                    <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                </td>

            </tr>

            <tr>
                <td>
                    <dxe:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" ClientInstanceName="page"
                        Font-Size="12px" OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged" Width="100%">
                        <tabpages>
                            <dxe:TabPage Name="General" Text="General">
                                <%-- <TabTemplate>
                                <span style="font-size: x-small">General</span>&nbsp;<span style="color: Red;">*</span>
                            </TabTemplate>--%>
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">

                                        <table class="TableMain100" id="table_others" runat="server">
                                            <tr>
                                                <td style="width: 500px">
                                                    <asp:Panel ID="Panel1" runat="server" BorderColor="White" BorderWidth="2px" Width="100%" CssClass="row">

                                                        <div class="clearfix" id="divSelectEmployee" runat="server">
                                                            <div class="col-md-3">
                                                                <label>Select Employee </label>
                                                                <div class="reltv">
                                                                    <asp:ListBox ID="lstAssociatedEmployee" CssClass="chsn" runat="server" Width="100%" data-placeholder="---Select---"></asp:ListBox>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--Rev 2.0--%>
                                                        <%--<div class="col-md-3 lblMtop10">--%>
                                                          <div class="col-md-3 lblMtop10 h-branch-select">
                                                              <%--Rev end 2.0--%>

                                                            <dxe:ASPxLabel ID="ASPxLabel15" Visible="false" runat="server" Text="Rating">
                                                            </dxe:ASPxLabel>
                                                            <dxe:ASPxLabel ID="ASPxLabel20" runat="server" Text="Customer Type" CssClass="inline">
                                                            </dxe:ASPxLabel>
                                                            <%--Rev 1.0--%>
                                                            <%--<span style="text-align: left; float: left; font-size: medium; color: Red; width: 8px;">*</span>--%>
                                                            <span style="text-align: left; float: left; font-size: medium; color: Red; width: 8px;line-height: 1; height: 13px;">*</span>
                                                            <%--Rev end 1.0--%>
                                                            <asp:DropDownList ID="cmbRating" Visible="false" runat="server" Width="160px" TabIndex="26">
                                                            </asp:DropDownList>

                                                            <asp:DropDownList ID="cmbLegalStatus" TabIndex="1" runat="server" Width="100%">
                                                            </asp:DropDownList>
                                                        </div>
                                                   <%--     .................ID TYPE.........................--%>
                                                        <%--<div class="col-md-3 lblMtop10">--%>
                                                        <div class="col-md-3 lblMtop10 h-branch-select">
                                                           
                                                                <dxe:ASPxLabel ID="ASPxLabel19" runat="server" Text="ID Type" CssClass="inline">
                                                                </dxe:ASPxLabel>
                                                           
                                                                    <asp:DropDownList ID="ddlIdType" runat="server" TabIndex="2" Width="100%">
                                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Phone</asp:ListItem>
                                                                        <asp:ListItem Value="2">PAN</asp:ListItem>
                                                                         <asp:ListItem Value="3">Aadhar No.</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                                                            
                                                        </div>
                                                    <%--     .................ID TYPE  End.........................--%>
                                                        <div class="col-md-3 lblMtop10  h-branch-select" id="divClientBranch" runat="server">
                                                            <dxe:ASPxLabel ID="ASPxLabel3" Visible="false" runat="server" Text="Last Name">
                                                            </dxe:ASPxLabel>
                                                            <dxe:ASPxLabel ID="ASPxLabel10" runat="server" Text="Branch" CssClass="inline">
                                                            </dxe:ASPxLabel>
                                                          <%--Rev 1.0--%>
                                                            <%--<span style="text-align: left; float: left; font-size: medium; color: Red; width: 8px;">*</span>--%>
                                                            <span style="text-align: left; float: left; font-size: medium; color: Red; width: 8px;line-height: 1; height: 13px;">*</span>
                                                            <%--Rev end 1.0--%>
                                                            <asp:DropDownList ID="cmbBranch" runat="server" Width="100%" TabIndex="2">
                                                            </asp:DropDownList>
                                                            <dxe:ASPxTextBox ID="txtLastName" Visible="false" runat="server" TabIndex="4" Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </div>

                                                        <div class="col-md-3 lblMtop10">

                                                            <dxe:ASPxLabel ID="ASPxLabel13" Visible="false" runat="server" Text="Source">
                                                            </dxe:ASPxLabel>
                                                            <asp:DropDownList ID="cmbSource" Visible="false" runat="server" TabIndex="7" Width="160px">
                                                            </asp:DropDownList>
                                                            <dxe:ASPxLabel ID="ASPxLabel17" Visible="false" runat="server" Text="Refered By">
                                                            </dxe:ASPxLabel>
                                                            <%--Rev 1.0--%>
                                                            <%--<span style="text-align: left; float: left; font-size: medium; color: Red; width: 8px;" id="td_star"
                                                                runat="server" visible="false">*
                                                            </span>--%>
                                                            <span style="text-align: left; float: left; font-size: medium; color: Red; width: 8px;line-height: 1; height: 13px;" id="td_star"
                                                                runat="server" visible="false">*
                                                            </span>
                                                            <%--Rev end 1.0--%>
                                                            <asp:TextBox ID="txtReferedBy" Visible="false" runat="server" TabIndex="8" Width="160px"></asp:TextBox>


                                                            <dxe:ASPxLabel ID="ASPxLabel2" Visible="false" runat="server" Text="Middle Name" CssClass="inline">
                                                            </dxe:ASPxLabel>
                                                            <dxe:ASPxLabel ID="ASPxLabel12" runat="server" Text="Unique ID" CssClass="inline ">
                                                            </dxe:ASPxLabel>
                                                            <%--Rev 1.0--%>
                                                            <%--<span id="ASPxLabelS12" style="text-align: left; float: left; font-size: medium; color: Red; width: 8px;" runat="server">*</span>--%>
                                                            <span id="ASPxLabelS12" style="text-align: left; float: left; font-size: medium; color: Red; width: 8px;line-height: 1; height: 13px;" runat="server">*</span>
                                                            <%--Rev end 1.0--%>
                                                            <%--txt unique code--%>
                                                            <div style="position: relative">
                                                                <dxe:ASPxTextBox ID="txtMiddleName" Visible="false" runat="server" Width="160px" TabIndex="3" CssClass="upper">
                                                                </dxe:ASPxTextBox>
                                                                <dxe:ASPxTextBox ID="txtClentUcc" runat="server" TabIndex="3" Width="100%" MaxLength="50" CssClass="upper">
                                                                    <ClientSideEvents TextChanged="function(s,e){fn_ctxtClentUcc_Name_TextChanged()}" />
                                                                </dxe:ASPxTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtClentUcc" ValidationGroup="contact" SetFocusOnError="true" ToolTip="Mandatory" class="tp28 pullrightClass fa fa-exclamation-circle abs iconRed" ErrorMessage=""></asp:RequiredFieldValidator>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" OnClick="LinkButton1_Click" Style="color: #cc3300; text-decoration: underline; font-size: 8pt;">Make System Generate UCC</asp:LinkButton>
                                                                <asp:Label ID="lblErr" Text="" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                           
                                                        <div class="col-md-3 hide lblMtop10">
                                                            <div style="display: none" class="labelt">
                                                                <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Unique ID">
                                                                </dxe:ASPxLabel>
                                                            </div>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox ID="txtAliasName" runat="server" TabIndex="4" Width="100%" CssClass="upper">
                                                                </dxe:ASPxTextBox>
                                                            </div>
                                                        </div>
                                                        <div style="clear: both">
                                                        </div>
                                                        <div class="col-md-3 lblMtop10">
                                                            <dxe:ASPxLabel Visible="false" ID="ASPxLabel1" runat="server" Text="Salutation">
                                                            </dxe:ASPxLabel>
                                                            <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Full Name" CssClass="inline">
                                                            </dxe:ASPxLabel>

                                                            <%--Rev 1.0--%>
                                                            <%--<span style="text-align: left; float: left; font-size: medium; color: Red; width: 8px;">*</span>--%>
                                                            <span style="text-align: left; float: left; font-size: medium; color: Red; width: 8px;line-height: 1; height: 13px;">*</span>
                                                            <%--Rev end 1.0--%>

                                                            <div style="position: relative">
                                                                <asp:DropDownList Visible="false" ID="CmbSalutation" runat="server" TabIndex="1" Width="160px">
                                                                </asp:DropDownList>
                                                                <dxe:ASPxTextBox ID="txtFirstNmae" runat="server" TabIndex="4" Width="100%" MaxLength="100" CssClass="upper">
                                                                    <%-- <ValidationSettings ValidationGroup="a">
                                                                        </ValidationSettings>--%>
                                                                </dxe:ASPxTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstNmae" ValidationGroup="contact" SetFocusOnError="true" class="tp28 pullrightClass fa fa-exclamation-circle abs iconRed" ToolTip="Mandatory" ErrorMessage=""></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        

                                                        <div class="col-md-3 lblMtop10">
                                                            <%--lbl D.O.B F--%>
                                                            <div class="labelt">

                                                                <dxe:ASPxLabel ID="ASPxLabel18" runat="server" Text="Date of Birth">
                                                                </dxe:ASPxLabel>

                                                            </div>
                                                            <%--txt D.O.B F--%>
                                                            <div>

                                                                <dxe:ASPxDateEdit ID="txtDOB" runat="server" Width="100%" EditFormat="Custom" EditFormatString="dd-MM-yyyy"
                                                                    UseMaskBehavior="True" TabIndex="5">
                                                                    <ButtonStyle Width="13px">
                                                                    </ButtonStyle>
                                                                </dxe:ASPxDateEdit>

                                                            </div>
                                                        </div> 
                                                        <div class="col-md-3 lblMtop10">
                                                            <%-- lbl Nationality--%>
                                                            <div class="labelt">
                                                                <dxe:ASPxLabel ID="ASPxLabel34" Visible="false" runat="server" Text="Contact Status">
                                                                </dxe:ASPxLabel>
                                                                <dxe:ASPxLabel ID="ASPxLabel35" Width="120px" runat="server" Text="Nationality">
                                                                </dxe:ASPxLabel>
                                                            </div>
                                                            <%-- lbl Nationality--%>
                                                            <div style="position: relative">
                                                                <asp:DropDownList Visible="false" ID="cmbContactStatus" runat="server" Width="100%" TabIndex="6">
                                                                </asp:DropDownList>

                                                                <asp:DropDownList ID="ddlnational" TabIndex="6" runat="server" Width="100%">
                                                                    <%--<asp:ListItem Value="1">Indian</asp:ListItem>
                                                                    <asp:ListItem Value="2">Others</asp:ListItem>--%>
                                                                </asp:DropDownList>



                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 visF lblMtop10" id="td_lAnniversary">
                                                            <div class="labelt">
                                                                <dxe:ASPxLabel ID="ASPxLabel21" runat="server" Text="Anniversary Dates">
                                                                </dxe:ASPxLabel>
                                                            </div>
                                                            <%--txt Anniversary--%>
                                                            <%--<div id="td_tAnniversary">--%>
                                                            <div class="visF">
                                                                <dxe:ASPxDateEdit ID="txtAnniversary" TabIndex="7" runat="server" Width="100%" EditFormat="Custom" ClientInstanceName="txtAnniversary"
                                                                    EditFormatString="dd-MM-yyyy" UseMaskBehavior="True">
                                                                    <ButtonStyle Width="13px">
                                                                    </ButtonStyle>
                                                                </dxe:ASPxDateEdit>
                                                            </div>
                                                            <%--</div>--%>
                                                        </div>
                                                        <div style="clear: both;"></div>
                                                        <div class="col-md-3 visF lblMtop10">
                                                            <div id="td_lGender" class="labelt">
                                                                <div class="visF">
                                                                    <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Text="Gender">
                                                                    </dxe:ASPxLabel>
                                                                </div>
                                                            </div>
                                                            <div id="td_dGender">
                                                                <div class="visF">
                                                                    <asp:DropDownList ID="cmbGender" runat="server" TabIndex="11" Width="100%">
                                                                        <asp:ListItem Value="2">--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="0">Male</asp:ListItem>
                                                                        <asp:ListItem Value="1">Female</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <%--<div class="col-md-3 visF">
                                                            <div id="td_lGender">
                                                                <div class="visF">
                                                                    <dxe:ASPxLabel ID="ASPxLabel14" runat="server" Text="Gender">
                                                                    </dxe:ASPxLabel>
                                                                </div>
                                                            </div>
                                                            <div id="td_dGender">
                                                                <div class="visF">
                                                                    <asp:DropDownList ID="cmbGender" runat="server" TabIndex="7" Width="100%">
                                                                        <asp:ListItem Value="2">--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="0">Male</asp:ListItem>
                                                                        <asp:ListItem Value="1">Female</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 visF">
                                                            <div id="td_lMarital">
                                                                <div class="visF">
                                                                    <dxe:ASPxLabel ID="ASPxLabel16" runat="server" Text="Marital Status">
                                                                    </dxe:ASPxLabel>
                                                                </div>
                                                            </div>
                                                            <div id="td_dMarital">
                                                                <div class="visF">
                                                                    <asp:DropDownList ID="cmbMaritalStatus" runat="server" TabIndex="8" Width="100%">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>--%>


                                                        

                                                        <%-- Code  Added  By sanjib on 18122016 to change 3 fields postion--%>
                                                        <%--<div class="col-md-3">--%>
                                                        <%-- lbl Nationality--%>
                                                        <%--<div>
                                                                <dxe:ASPxLabel ID="ASPxLabel19" Visible="false" runat="server" Text="Contact Status">
                                                                </dxe:ASPxLabel>
                                                                <dxe:ASPxLabel ID="ASPxLabel32" Width="120px" runat="server" Text="Nationality">
                                                                </dxe:ASPxLabel>
                                                            </div>--%>
                                                        <%-- lbl Nationality--%>
                                                        <%-- <div style="position: relative">
                                                                <asp:DropDownList Visible="false" ID="cmbContactStatus" runat="server" Width="100%" TabIndex="27">
                                                                </asp:DropDownList>

                                                                <asp:DropDownList ID="ddlnational" TabIndex="9" runat="server" Width="100%">--%>
                                                        <%--<asp:ListItem Value="1">Indian</asp:ListItem>
                                                                <%--    <asp:ListItem Value="2">Others</asp:ListItem>--%>
                                                        <%-- </asp:DropDownList>



                                                            </div>
                                                        </div>--%>
                                                        <%-- Code  Added  By Priti on 15122016 to add 3 fields--%>
                                                        <asp:Panel ID="pnlCredit" runat="server">
                                                            <div class="clear"></div>
                                                            <div class="col-md-3">
                                                                <div>
                                                                </div>
                                                                <div style="position: relative">
                                                                    <div class="checkbox" style="padding-left: 0px !important;">
                                                                        <label>
                                                                            <dxe:ASPxCheckBox ID="ChkCreditcard" runat="server" TabIndex="8"></dxe:ASPxCheckBox>
                                                                            <dxe:ASPxLabel ID="lblCreditcard" Width="120px" runat="server" Text="Credit Hold">
                                                                            </dxe:ASPxLabel>
                                                                        </label>
                                                                    </div>


                                                                </div>
                                                            </div>
                                                            <div class="col-md-3 lblMtop10">
                                                                <div class="labelt">
                                                                    <dxe:ASPxLabel ID="lblcreditDays" Width="120px" runat="server" Text="Credit Days">
                                                                    </dxe:ASPxLabel>
                                                                </div>
                                                                <div style="position: relative">
                                                                    <dxe:ASPxTextBox ID="txtcreditDays" runat="server" TabIndex="9" Width="100%" MaxLength="4" Text="0" onkeypress="return onlyNumbers();">
                                                                    </dxe:ASPxTextBox>
                                                                    <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="" ValidationExpression="^[0-9]&" controltovalidate="txtcreditDays"></asp:RegularExpressionValidator>--%>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3 lblMtop10">
                                                                <div class="labelt">
                                                                    <dxe:ASPxLabel ID="lblCreditLimit" Width="100%" runat="server" Text="Credit Limit">
                                                                    </dxe:ASPxLabel>
                                                                </div>
                                                                <div style="position: relative">
                                                                    <dxe:ASPxTextBox ID="txtCreditLimit" runat="server" TabIndex="10" Width="100%" MaxLength="40" Text="0">
                                                                        <%--     Code added By Priti on 16122016 to use decimal value--%>
                                                                        <MaskSettings Mask="<0..999999999999g>.<0..99g>" />
                                                                    </dxe:ASPxTextBox>
                                                                    <%--  ...end...--%>
                                                                </div>
                                                            </div>
                                                            <div class="clear"></div>
                                                        </asp:Panel>
                                                        <%-- .......end.....--%>
                                                        <%--  <div id="td_country" class="col-md-3" style="display:none;">--%>

                                                        <%-- <asp:TextBox ID="txtcountry" runat="server" Width="100%"
                                                                TabIndex="29"></asp:TextBox>--%>
                                                        <%-- </div>--%>

                                                        <div class="col-md-3 visF lblMtop10">
                                                            <div id="td_lMarital" class="labelt">
                                                                <div class="visF">
                                                                    <dxe:ASPxLabel ID="ASPxLabel16" runat="server" Text="Marital Status">
                                                                    </dxe:ASPxLabel>
                                                                </div>
                                                            </div>
                                                            <div id="td_dMarital">
                                                                <div class="visF">
                                                                    <asp:DropDownList ID="cmbMaritalStatus" runat="server" TabIndex="12" Width="100%">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--Rev 1.0--%>
                                                        <%--<div class="col-md-3 visF">--%>
                                                            <div class="col-md-3 visF lblMtop10">
                                                            <%--Rev end 1.0--%>
                                                            <div id="td_lMaritals" class="labelt">
                                                                <div class="visF">
                                                                    <dxe:ASPxLabel ID="txtContactStatusclient" runat="server" Text="Status">
                                                                    </dxe:ASPxLabel>
                                                                </div>
                                                            </div>




                                                            <div id="td_dMaritals4">
                                                                <div class="visF">                                                                  
                                                                    <dxe:ASPxComboBox ID="cmbContactStatusclient" ClientInstanceName="cCmbStatus" runat="server" SelectedIndex="0" TabIndex="23"
                                                                        ValueType="System.String" Width="100%" EnableSynchronization="True" EnableIncrementalFiltering="True">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="Active" Value="A" />
                                                                            <dxe:ListEditItem Text="Dormant" Value="D" />
                                                                        </Items>
                                                                        <ClientSideEvents SelectedIndexChanged="function(s,e){CheckContactStatus(s);}" />
                                                                    </dxe:ASPxComboBox>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="clear"></div>
                                                        <div class="col-md-3 mainAccount">
                                                                <div class="padBot5 lblmTop8" style="display: block;">
                                                                    <span>Main Account</span> 
                                                                </div>
                                                                <div class="Left_Content"> 
                                                                      <asp:ListBox ID="lstTaxRates_MainAccount" CssClass="chsn" runat="server" Font-Size="12px" Width="100%" data-placeholder="Select..." TabIndex="10"  onchange="changeFunc();"></asp:ListBox>
                                                                    <asp:HiddenField ID="hndTaxRates_MainAccount_hidden" runat="server" />
                                                                     <asp:HiddenField ID="hdIsMainAccountInUse" runat="server" />
                                                                </div>
                                                        </div>

                                                        <div id="td_registered" class="labelt col-md-3" runat="server">
                                                            <%--Rev 1.0--%>
                                                            <%--<div class="visF">--%>
                                                                <div class="visF lblMtop10">
                                                                    <%--Rev end 1.0--%>

                                                                <label>Registered?</label>
                                                                <asp:RadioButtonList runat="server" ID="radioregistercheck" RepeatDirection="Horizontal" Width="130px">
                                                                    <asp:ListItem Text="Yes" Value="1" ></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>

                                                        <%--Debjyoti GstIN--%>
                                                         

                                                        <div id="divGSTIN" class="col-md-4 forCustomer">
                                                            <label class="labelt">GSTIN   </label>
                                                            <span id="spanmandategstn" style="color: red; display: none;">*</span>
                                                            <div class="relative">
                                                                <ul class="nestedinput">
                                                                    <li>
                                                                        <dxe:ASPxTextBox ID="txtGSTIN1" ClientInstanceName="ctxtGSTIN111" MaxLength="2" runat="server" Width="33px">
                                                                            <ClientSideEvents KeyPress="function(s,e){ fn_AllowonlyNumeric(s,e);}" />
                                                                        </dxe:ASPxTextBox>
                                                                    </li>
                                                                    <li class="dash">- </li>
                                                                    <li>
                                                                        <dxe:ASPxTextBox ID="txtGSTIN2" ClientInstanceName="ctxtGSTIN222" MaxLength="10" runat="server" Width="90px">
                                                                            <ClientSideEvents KeyUp="Gstin2TextChanged" />
                                                                            <%--<ClientSideEvents TextChanged="function(s, e) {Gstin2ValueChanged();}" />--%>
                                                                        </dxe:ASPxTextBox>
                                                                    </li>
                                                                    <li class="dash">- </li>
                                                                    <li>
                                                                        <dxe:ASPxTextBox ID="txtGSTIN3" ClientInstanceName="ctxtGSTIN333" MaxLength="3" runat="server" Width="50px" >
                                                                            <ClientSideEvents KeyUp="Gstin3TextChanged" />
                                                                        </dxe:ASPxTextBox>
                                                                        <span id="invalidGst" class="fa fa-exclamation-circle iconRed " style="color: red; display: none; padding-left: 9px; left: 302px;" title="Invalid GSTIN"></span>
                                                                    </li>
                                                                </ul>
                                                              <a href="https://services.gst.gov.in/services/searchtp" target="_blank"  style="padding-left:10px" >Validate GST</a> 
                                                            </div>
                                                        </div>
                                                        <%--Rev 1.0--%>
                                                        <%--<div class="col-md-3 visF" id="td_Applicablefrom"> --%>
                                                            <div class="col-md-3 visF lblMtop10" id="td_Applicablefrom"> 
                                                            <%--Rev end 1.0--%>
                                                            <div class="labelt">
                                                                <dxe:ASPxLabel ID="lbl_Applicablefrom" runat="server" Text="Applicable From">
                                                                </dxe:ASPxLabel>
                                                            </div>
                                                            <div class="visF">
                                                                <dxe:ASPxDateEdit ID="dt_ApplicableFrom" TabIndex="12" runat="server" Width="100%" EditFormat="Custom" ClientInstanceName="cApplicableFrom"
                                                                    EditFormatString="dd-MM-yyyy" UseMaskBehavior="True">
                                                                    <ButtonStyle Width="13px">
                                                                    </ButtonStyle>
                                                                </dxe:ASPxDateEdit>

                                                                <%--<input type="text"  onkeyup="dateValidationFormat(event)" onkeypress="return isDateKey(event)" maxlength="10" class="flatpickr"/>--%>

                                                            </div>
                                                        </div>
                                                         
                                                        <div class="clear"></div>
                                                        <%--Debjyoti GstIN--%>
                                                        <%-- Start Tranporter Vechile No --%>
                                                        <asp:Panel ID="pnlVehicleNo" runat="server" CssClass="col-md-6">
                                                            <label style="display: block;">Vehicle No.</label>
                                                            <%-- <table style="width: 200px;" class="pdri">
                                                            <tr>
                                                                <td style="width: 180px">
                                                                    <label style="display: block;">Vehicle No.</label>
                                                                </td>
                                                               
                                                            </tr>
                                                        </table>--%>
                                                            <table id="executiveTable" style="width: 320px;" class="nbackBtn" runat="server">
                                                                <tr>
                                                                    <td style="padding-right: 15px">
                                                                        <input class="form-control" type="text" maxlength="18" />
                                                                    </td>

                                                                    <td>
                                                                        <button type="button" class="btn btn-primary btn-xs" onclick="AddNewexecutive()"><i class="fa fa-plus-circle"></i></button>
                                                                        <button type="button" class="btn btn-danger btn-xs" onclick="removeExecutive(this.parentNode.parentNode)"><i class="fa fa-times-circle"></i></button>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                            <asp:HiddenField ID="VehicleNo_hidden" runat="server" />
                                                        </asp:Panel>
                                                        <%-- END Tranporter Vechile No --%>
    </div>

    <table class="TableMain100">

        <tr style="display: none">
            <td style="width: 64px">
                <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Profession">
                </dxe:ASPxLabel>
            </td>
            <td style="text-align: left; width: 163px;">
                <asp:DropDownList ID="cmbProfession" runat="server" TabIndex="13" Width="160px">
                </asp:DropDownList>
            </td>
            <td style="width: 76px">
                <dxe:ASPxLabel ID="ASPxLabel9" runat="server" Text="Designation">
                </dxe:ASPxLabel>
            </td>
            <td style="text-align: left; font-size: medium; color: Red; width: 8px;"></td>
            <td style="text-align: left; width: 164px;">
                <asp:DropDownList ID="cmbDesignation" runat="server" TabIndex="14" Width="160px">
                </asp:DropDownList>
            </td>
            <td style="width: 68px">
                <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="Job Responsibility">
                </dxe:ASPxLabel>
            </td>
            <td style="text-align: left; width: 166px;">
                <asp:DropDownList ID="cmbJobResponsibility" runat="server" TabIndex="15" Width="160px">
                </asp:DropDownList>
            </td>
            <td style="width: 61px">
                <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Organization">
                </dxe:ASPxLabel>
            </td>
            <td style="text-align: left">
                <dxe:ASPxTextBox ID="txtOrganization" runat="server" TabIndex="16" Width="160px">
                </dxe:ASPxTextBox>
            </td>
        </tr>

        <tr id="Trprofessionother" style="display: none">
            <td style="width: 64px">
                <dxe:ASPxLabel ID="ASPxLabel31" runat="server" Text="Other Detail">
                </dxe:ASPxLabel>
            </td>
            <td style="width: 76px">
                <asp:TextBox ID="txtotheroccu" runat="server" Width="160px"></asp:TextBox>
            </td>
            <td style="text-align: left; font-size: medium; color: Green; width: 4px;">*
            </td>
            <td colspan="9"></td>
        </tr>

        <tr style="display: none">
            <td style="width: 64px">
                <dxe:ASPxLabel ID="ASPxLabel22" runat="server" Text="Education">
                </dxe:ASPxLabel>
            </td>
            <td style="text-align: left; width: 163px;">
                <asp:DropDownList ID="cmbEducation" runat="server" TabIndex="17" Width="160px">
                </asp:DropDownList>
            </td>
            <td style="width: 76px">
                <dxe:ASPxLabel ID="ASPxLabel11" runat="server" Text="Industry/Sector">
                </dxe:ASPxLabel>
            </td>
            <td style="text-align: left; font-size: medium; color: Red; width: 8px;"></td>
            <td style="width: 164px">
                <asp:DropDownList ID="cmbIndustry" runat="server" TabIndex="18" Width="160px">
                </asp:DropDownList>
            </td>
            <td style="width: 68px">
                <dxe:ASPxLabel ID="ASPxLabel23" runat="server" Text="Date Of Regn/Intr">
                </dxe:ASPxLabel>
            </td>
            <td style="text-align: left; width: 166px;">
                <dxe:ASPxDateEdit ID="txtDateRegis" runat="server" Width="156px" EditFormat="Custom"
                    EditFormatString="dd MM yyyy" UseMaskBehavior="True" TabIndex="19">
                    <buttonstyle width="13px">
                    </buttonstyle>
                </dxe:ASPxDateEdit>

                <dxe:ASPxLabel ID="ASPxLabel33" Visible="false" runat="server" Text="Anniversary Date">
                </dxe:ASPxLabel>



            </td>
            <td style="width: 61px" valign="top">
                <dxe:ASPxLabel ID="ASPxLabel28" Visible="false" runat="server" Text="Blood Group">
                </dxe:ASPxLabel>
            </td>
            <td>
                <asp:DropDownList ID="cmbBloodgroup" Visible="false" runat="server" Width="85px" TabIndex="20">
                    <asp:ListItem Value="NULL">--Select--</asp:ListItem>
                    <asp:ListItem Value="A+">A+</asp:ListItem>
                    <asp:ListItem Value="A-">A-</asp:ListItem>
                    <asp:ListItem Value="B+">B+</asp:ListItem>
                    <asp:ListItem Value="B-">B-</asp:ListItem>
                    <asp:ListItem Value="AB+">AB+</asp:ListItem>
                    <asp:ListItem Value="AB-">AB-</asp:ListItem>
                    <asp:ListItem Value="O+">O+</asp:ListItem>
                    <asp:ListItem Value="O-">O-</asp:ListItem>
                    <asp:ListItem Value="N/A">N/A</asp:ListItem>
                </asp:DropDownList><br />
                <asp:Label ID="l1" runat="server" Text="Allow web logIn"></asp:Label>
                <asp:CheckBox ID="chkAllow" runat="server" TabIndex="21" />
            </td>
        </tr>
    </table>
    </asp:Panel>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="width: 961px">
                                                    <table class="TableMain100">
                                                        <tr style="display: none">
                                                            <td style="width: 64px">Branch name
                                                            </td>
                                                            <td style="width: 250px">ddl breach lst
                                                            </td>
                                                            <td style="width: 76px">
                                                                <dxe:ASPxLabel ID="ASPxLabel24" runat="server" Text="SPOC">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="text-align: left; font-size: medium; color: Red; width: 4px;"></td>
                                                            <td style="width: 164px">
                                                                <asp:TextBox ID="txtRPartner" runat="server" TabIndex="23" Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 68px">
                                                                <dxe:ASPxLabel ID="ASPxLabel25" runat="server" Text="SPOC Email">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="text-align: left; font-size: medium; color: Red; width: 8px;"></td>
                                                            <td style="width: 166px">
                                                                <asp:TextBox ID="TxtEmail" runat="server" Width="130px" TabIndex="24"></asp:TextBox></td>
                                                            <td style="width: 61px">
                                                                <dxe:ASPxLabel ID="ASPxLabel26" runat="server" Text="SPOC Phone">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtPhone" runat="server" Width="130px" TabIndex="25"></asp:TextBox>
                                                            </td>
                                                        </tr>


                                                        <tr id="TrContact" style="display: none">
                                                            <td>
                                                                <dxe:ASPxLabel ID="ASPxLabel27" runat="server" Text="Reason">
                                                                </dxe:ASPxLabel>
                                                            </td>
                                                            <td style="text-align: left;" colspan="3">
                                                                <asp:TextBox ID="TxtContactStatus" runat="server" TextMode="MultiLine" Width="404px"
                                                                    TabIndex="29"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr id="tr_contactperson">
                                                            <%--<td colspan="6"></td>
                                                        <td id="td_green" style="text-align: left; font-size: medium; color: Green; width: 8px;">*
                                                        </td>
                                                        <td id="td_red" style="text-align: left; font-size: medium; color: Red; width: 8px;">*
                                                        </td>--%>

                                                            <%--..................... Commented By Sam on 27092016........................--%>
                                                            <%-- <td id="td_only" style="padding-left: 19px">
                                                                <a href="javascript:void(0);" class="btn btn-success"
                                                                    onclick="OnContactInfoClick('<%#Eval("InternalId") %>')">Add Contact Person</a>
                                                            </td>--%>

                                                            <%--  ..................... Commented By Sam on 27092016 end........................--%>
                                                            <td id="td_one" style="padding-left: 19px">
                                                                <a href="javascript:void(0);" class="btn btn-success" style="display: none"
                                                                    onclick="OnContactInfoClick('<%#Eval("InternalId") %>')">Add 1 Contact Person</a>
                                                            </td>
                                                            <td id="td_two" style="padding-left: 19px">
                                                                <a href="javascript:void(0);" class="btn btn-success" style="display: none"
                                                                    onclick="OnContactInfoClick('<%#Eval("InternalId") %>')">Add 3 Contact Persons</a>
                                                            </td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
    <tr style="display: none">
        <td>
            <table>
                <tr id="Trincorporation">
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel29" runat="server" Text="Place Of Incorporation">
                        </dxe:ASPxLabel>
                    </td>
                    <td style="text-align: left; font-size: medium; color: Red; width: 8px;">*
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtincorporation" runat="server" Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel30" runat="server" Text="Date of commencement of business">
                        </dxe:ASPxLabel>
                    </td>
                    <td style="text-align: left; font-size: medium; color: Red; width: 8px;">*
                    </td>
                    <%--<td style="text-align: left;" colspan="3">
                                                                <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Width="404px"
                                                                    TabIndex="29"></asp:TextBox>
                                                            </td>--%>
                    <td style="text-align: left; width: 164px;">
                        <dxe:ASPxDateEdit ID="txtFromDate" EditFormatString="dd-MM-yyyy" runat="server" EditFormat="Custom" Width="105px"
                            UseMaskBehavior="True" DateOnError="Today">
                            <buttonstyle width="13px">
                            </buttonstyle>
                            <%--<DropDownButton Text="From">
                                                                    </DropDownButton>--%>
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td></td>
                    <td></td>
                    <td style="text-align: left;"></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="TrLang" runat="server" visible="false">
        <td id="Td1" runat="server" style="width: 961px">
            <asp:Panel ID="Panel2" Visible="false" runat="server" Width="100%" BorderColor="White" BorderWidth="1px">
                <table class="TableMain100">
                    <tr>
                        <td>
                            <table class="TableMain100">
                                <tr>
                                    <td colspan="2" style="text-align: left">
                                        <span style="color: blue; font-size: 10pt;">Language Proficiencies </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; vertical-align: top">
                                        <asp:Panel ID="PnlSpLAng" runat="server" Width="100%" BorderColor="White" BorderWidth="1px">
                                            <table>
                                                <tr>
                                                    <td style="text-align: left; color: Blue; font-size: small">Can Speak
                                                    </td>
                                                    <td style="vertical-align: top" class="gridcellleft">
                                                        <asp:TextBox ID="LitSpokenLanguage" runat="server" ForeColor="Maroon" BackColor="Transparent"
                                                            BorderStyle="None" Width="309px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; vertical-align: top; font-size: x-small; color: Red;">
                                                        <a href="frmLanguages.aspx?id=''&status=speak" onclick="window.open(this.href,'popupwindow','left=120,top=170,height=400,width=200,scrollbars=no,toolbar=no,location=center,menubar=no'); return false;"
                                                            style="font-size: x-small; color: Red;">click to add</a>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td style="width: 50%; vertical-align: top">
                                        <asp:Panel ID="PnlWrLang" runat="server" Width="100%" BorderColor="White" BorderWidth="1px">
                                            <table>
                                                <tr>
                                                    <td style="text-align: left; color: Blue; font-size: small">Can Write
                                                    </td>
                                                    <td style="vertical-align: top" class="gridcellleft">
                                                        <asp:TextBox ID="LitWrittenLanguage" runat="server" ForeColor="Maroon" BackColor="Transparent"
                                                            BorderStyle="None" Width="313px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; vertical-align: top; font-size: x-small; color: Red;">
                                                        <a href="frmLanguages.aspx?id=''&status=write" onclick="window.open(this.href,'popupwindow','left=120,top=170,height=400,width=200,scrollbars=no,toolbar=no,location=center,menubar=no'); return false;"
                                                            style="color: Red; font-size: x-small;">click to add</a>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="display: none;">
            <asp:TextBox ID="txtRPartner_hidden" runat="server" BackColor="White" BorderColor="White"
                BorderStyle="None" ForeColor="White"></asp:TextBox>
            <asp:TextBox ID="txtReferedBy_hidden" runat="server" BackColor="White" BorderColor="White"
                BorderStyle="None" ForeColor="White"></asp:TextBox>
            <asp:TextBox ID="txtcountry_hidden" runat="server" BackColor="silver"></asp:TextBox>
        </td>
    </tr>
    </table>
                                        <div id="table_leads" class="row" runat="server">
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel1" runat="server" Text="Salutation" Width="130px"></dxe:ASPxLabel>
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="LDCmbSalutation" CssClass="form-control" runat="server" TabIndex="1" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel5" runat="server" Text="First Name" CssClass="pdl8"></dxe:ASPxLabel>
                                                    <span style="color: red;">*</span>

                                                </label>
                                                <div style="position: relative;">
                                                    <asp:TextBox ID="LDtxtFirstNmae" runat="server" CssClass="form-control" TabIndex="2" MaxLength="20" Width="100%">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator111" runat="server" ControlToValidate="LDtxtFirstNmae" ValidationGroup="contact"
                                                        SetFocusOnError="true" ToolTip="Mandatory" class="tp2 fa fa-exclamation-circle iconRed" ErrorMessage=""></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel2" runat="server" Text="Middle Name" CssClass="pdl8" Width="130px"></dxe:ASPxLabel>
                                                </label>
                                                <div style="">
                                                    <asp:TextBox ID="LDtxtMiddleName" runat="server" CssClass="form-control" TabIndex="3" MaxLength="20" Width="100%">
                                                    </asp:TextBox>

                                                </div>
                                                <div style="text-align: right; height: 1px">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LDtxtFirstNmae"
                                                        Display="Dynamic" ErrorMessage="Mandatory" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel3" runat="server" Text="Last Name"></dxe:ASPxLabel>
                                                    <span style="color: red;">*</span>


                                                </label>
                                                <div style="position: relative">
                                                    <asp:TextBox ID="LDtxtLastName" runat="server" CssClass="form-control" TabIndex="4" MaxLength="20" Width="100%">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="LDtxtLastName" ValidationGroup="contact" SetFocusOnError="true" class="tp2 fa fa-exclamation-circle iconRed" ToolTip="Mandatory"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="clear"></div>
                                            <div class="col-md-3" style="display: none">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel4" runat="server" Text="Unique ID" CssClass="pdl8"></dxe:ASPxLabel>
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="LDtxtAliasName" runat="server" CssClass="form-control" TabIndex="5" Width="100%" MaxLength="50">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel6" runat="server" Text="Profession" CssClass="pdl8" Width="130px"></dxe:ASPxLabel>
                                                </label>
                                                <div>

                                                    <asp:DropDownList ID="LDcmbProfession" runat="server" CssClass="form-control" TabIndex="6" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel7" runat="server" Text="Organization"></dxe:ASPxLabel>
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="LDtxtOrganization" runat="server" CssClass="form-control" TabIndex="7" MaxLength="20" Width="100%">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel8" runat="server" Text="Job Responsibility" CssClass="pdl8"></dxe:ASPxLabel>
                                                </label>
                                                <div>

                                                    <asp:DropDownList ID="LDcmbJobResponsibility" runat="server" CssClass="form-control" TabIndex="8" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel9" runat="server" Text="Designation" CssClass="pdl8"></dxe:ASPxLabel>
                                                </label>
                                                <div>

                                                    <asp:DropDownList ID="LDcmbDesignation" runat="server" CssClass="form-control" TabIndex="9" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel10" runat="server" Text="Branch"></dxe:ASPxLabel>
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="LDcmbBranch" runat="server" CssClass="form-control" TabIndex="10" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel11" runat="server" Text="Industry/Sector" CssClass="pdl8"></dxe:ASPxLabel>
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="LDcmbIndustry" runat="server" CssClass="form-control" TabIndex="11" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel12" runat="server" Text="Unique ID" CssClass="pdl8"></dxe:ASPxLabel>
                                                    <span style="color: red;">*</span>

                                                </label>
                                                <div style="position: relative">
                                                    <asp:TextBox ID="LDtxtClentUcc" runat="server" CssClass="form-control" MaxLength="50" TabIndex="12" Width="100%">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="LDtxtClentUcc" ValidationGroup="contact" SetFocusOnError="true" class="tp2 fa fa-exclamation-circle iconRed" ToolTip="Mandatory"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel13" runat="server" Text="Source"></dxe:ASPxLabel>
                                                </label>
                                                <div>

                                                    <asp:DropDownList ID="LDcmbSource" runat="server" CssClass="form-control" TabIndex="13" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel17" runat="server" Text="Referred By" CssClass="pdl8"></dxe:ASPxLabel>
                                                </label>
                                                <div>
                                                    <%-- <asp:TextBox ID="LDtxtReferedBy" runat="server" TabIndex="14" CssClass="form-control" MaxLength="20" Width="100%"></asp:TextBox>
                                                    <asp:TextBox ID="LDtxtReferedBy_hidden" runat="server" TabIndex="14"></asp:TextBox>--%>
                                                    <asp:HiddenField ID="LDtxtReferedBy_hidden" runat="server"></asp:HiddenField>
                                                    <asp:ListBox ID="lstconverttounit" CssClass="chsn" runat="server" Width="100%" TabIndex="8" data-placeholder="Select..."></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="a" runat="server" ControlToValidate="lstconverttounit" Display="Dynamic"
                                                        CssClass="fa fa-exclamation-circle ctcclass " ToolTip="Mandatory."
                                                        ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel15" runat="server" Text="Rating" CssClass="pdl8"></dxe:ASPxLabel>
                                                </label>
                                                <div>

                                                    <asp:DropDownList ID="LDcmbRating" runat="server" CssClass="form-control" TabIndex="15" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel16" runat="server" Text="Marital Status"></dxe:ASPxLabel>
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="LDcmbMaritalStatus" runat="server" CssClass="form-control" TabIndex="16" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel14" runat="server" Text="Gender" CssClass="pdl8"></dxe:ASPxLabel>
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="LDcmbGender" runat="server" CssClass="form-control" TabIndex="17" Width="100%">
                                                        <asp:ListItem Value="1">Male</asp:ListItem>
                                                        <asp:ListItem Value="0">Female</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel20" runat="server" Text="Legal Status" CssClass="pdl8"></dxe:ASPxLabel>
                                                </label>
                                                <div>

                                                    <asp:DropDownList ID="LDcmbLegalStatus" runat="server" CssClass="form-control" TabIndex="18" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel19" runat="server" Text="Contact Status"></dxe:ASPxLabel>
                                                </label>
                                                <div>

                                                    <asp:DropDownList ID="LDcmbContactStatus" runat="server" CssClass="form-control" TabIndex="19" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel18" runat="server" Text="D.O.B." CssClass="pdl8"></dxe:ASPxLabel>
                                                </label>
                                                <div>
                                                    <dxe:ASPxDateEdit ID="LDtxtDOB" runat="server" EditFormat="Custom" UseMaskBehavior="True" TabIndex="20" Width="100%">
                                                        <buttonstyle width="13px">
                                                        </buttonstyle>
                                                    </dxe:ASPxDateEdit>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel21" runat="server" Text="Anniversary Date" Width="111px" CssClass="pdl8"></dxe:ASPxLabel>
                                                </label>
                                                <div>
                                                    <dxe:ASPxDateEdit ID="LDtxtAnniversary" runat="server" EditFormat="Custom" UseMaskBehavior="True" TabIndex="21" Width="100%">
                                                        <buttonstyle width="13px">
                                                        </buttonstyle>
                                                    </dxe:ASPxDateEdit>
                                                </div>
                                            </div>
                                            <div class="clear"></div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel22" runat="server" Text="Education">
                                                    </dxe:ASPxLabel>
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="LDcmbEducation" runat="server" CssClass="form-control" TabIndex="22" Width="100%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <label>
                                                    <dxe:ASPxLabel ID="LDASPxLabel28" runat="server" Text="Blood Group" CssClass="pdl8"></dxe:ASPxLabel>
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="LDcmbBloodgroup" runat="server" CssClass="form-control" TabIndex="23" Width="100%">
                                                        <asp:ListItem Value="A+">A+</asp:ListItem>
                                                        <asp:ListItem Value="A-">A-</asp:ListItem>
                                                        <asp:ListItem Value="B+">B+</asp:ListItem>
                                                        <asp:ListItem Value="B-">B-</asp:ListItem>
                                                        <asp:ListItem Value="AB+">AB+</asp:ListItem>
                                                        <asp:ListItem Value="AB-">AB-</asp:ListItem>
                                                        <asp:ListItem Value="O+">O+</asp:ListItem>
                                                        <asp:ListItem Value="O-">O-</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="clear"></div>

                                        </div>
    <div class="" style="padding-top: 15px;">
        <asp:HiddenField ID="hdReferenceBy" runat="server" />
        <dxe:ASPxButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ClientInstanceName="cbtnSave" AutoPostBack="false" OnClick="btnSave_Click"
            ValidationGroup="contact" TabIndex="24">
            <clientsideevents click="fn_btnValidate" />
        </dxe:ASPxButton>
        <dxe:ASPxButton ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click"
            TabIndex="25">
        </dxe:ASPxButton>

        <asp:Button ID="btnUdf" runat="server" Text="UDF" CssClass="btn btn-primary dxbButton" OnClientClick="if(OpenUdf()){ return false;}"
            TabIndex="22" />

        <GSTIN:gstinSettings runat="server" ID="GstinSettingsButton" />
        <%-- <table>
                                                    <tr>
                                                        <td style="padding-right: 20px;">
                                            
                                                        </td>
                                                        <td>
                                           
                                                        </td>
                                                    </tr>
                                                </table>--%>
    </div>
    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                            <dxe:TabPage Name="Correspondence" Text="Correspondence">

                                <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
                            </dxe:TabPage>
    <dxe:TabPage Name="BankDetails" Text="Bank">

        <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
    </dxe:TabPage>
    <dxe:TabPage Name="DPDetails" Visible="false" Text="DP">

        <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
    </dxe:TabPage>
    <dxe:TabPage Name="Documents" Text="Documents">

        <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
    </dxe:TabPage>
    <dxe:TabPage Name="Registration" Text="Registration">

        <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
    </dxe:TabPage>
    <dxe:TabPage Name="Other" Visible="false" Text="Other">

        <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
    </dxe:TabPage>
    <dxe:TabPage Name="GroupMember" Text="Group">
        <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
    </dxe:TabPage>
    <dxe:TabPage Name="Deposit" Visible="false" Text="Deposit">
        <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
    </dxe:TabPage>
    <dxe:TabPage Name="Remarks" Text="UDF" Visible="false">
        <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
    </dxe:TabPage>
    <dxe:TabPage Name="Education" Visible="false" Text="Education">
        <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
    </dxe:TabPage>
    <dxe:TabPage Name="Trad. Prof." Visible="false" Text="Trad.Prof">
        <%--<TabTemplate ><span style="font-size:x-small">Trad.Prof</span>&nbsp;<span style="color:Red;">*</span> </TabTemplate>--%>
        <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
    </dxe:TabPage>
    <dxe:TabPage Name="FamilyMembers" Visible="false" Text="Family">
        <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
    </dxe:TabPage>
    <dxe:TabPage Name="Subscription" Visible="false" Text="Subscription">
        <contentcollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </contentcollection>
    </dxe:TabPage>
    </TabPages>
                        <clientsideevents activetabchanged="function(s, e) {
	                                            var activeTab   = page.GetActiveTab();
	                                            var Tab0 = page.GetTab(0);
	                                            var Tab1 = page.GetTab(1);
	                                            var Tab2 = page.GetTab(2);
	                                            var Tab3 = page.GetTab(3);
	                                            var Tab4 = page.GetTab(4);
	                                            var Tab5 = page.GetTab(5);
	                                            var Tab6 = page.GetTab(6);
	                                            var Tab7 = page.GetTab(7);
	                                            var Tab8 = page.GetTab(8);
	                                            var Tab9 = page.GetTab(9);
	                                            var Tab10 = page.GetTab(10);
	                                            var Tab11 = page.GetTab(11);
	                                            var Tab12 = page.GetTab(12);
	                                             var Tab13=page.GetTab(13);
	                                            
	                                            if(activeTab == Tab0)
	                                            {
	                                                disp_prompt('tab0');
	                                            }
	                                            if(activeTab == Tab1)
	                                            {
	                                                disp_prompt('tab1');
	                                            }
	                                            else if(activeTab == Tab2)
	                                            {
	                                                disp_prompt('tab2');
	                                            }
	                                            else if(activeTab == Tab3)
	                                            {
	                                                disp_prompt('tab3');
	                                            }
	                                            else if(activeTab == Tab4)
	                                            {
	                                                disp_prompt('tab4');
	                                            }
	                                            else if(activeTab == Tab5)
	                                            {
	                                                disp_prompt('tab5');
	                                            }
	                                            else if(activeTab == Tab6)
	                                            {
	                                                disp_prompt('tab6');
	                                            }
	                                            else if(activeTab == Tab7)
	                                            {
	                                                disp_prompt('tab7');
	                                            }
	                                            else if(activeTab == Tab8)
	                                            {
	                                                disp_prompt('tab8');
	                                            }
	                                            else if(activeTab == Tab9)
	                                            {
	                                                disp_prompt('tab9');
	                                            }
	                                            else if(activeTab == Tab10)
	                                            {
	                                                disp_prompt('tab10');
	                                            }
	                                            else if(activeTab == Tab11)
	                                            {
	                                                disp_prompt('tab11');
	                                            }
	                                            else if(activeTab == Tab12)
	                                            {
	                                                disp_prompt('tab12');
	                                            }
	                                            else if(activeTab == Tab13)
	                                            {
	                                               disp_prompt('tab13');
	                                            }
	                                           
	                                            }"></clientsideevents>
    <contentstyle>
                            <Border BorderColor="#002D96" BorderStyle="Solid" BorderWidth="1px" />
                        </contentstyle>
    <loadingpanelstyle imagespacing="6px">
                        </loadingpanelstyle>
    </dxe:ASPxPageControl>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>

                            <%--<td align="center" style="text-align: ">
                                    <asp:HiddenField ID="hdReferenceBy" runat="server" />
                                    <table >
                                        <tr>
                                        
                                            <td style="padding-right:20px;">
                                                <dxe:ASPxButton ID="btnSave" runat="server" Text="Save" width="80px"  OnClick="btnSave_Click" ValidationGroup="a"
                                                    TabIndex="30">
                                                </dxe:ASPxButton>
                                            </td>
                                            
                                            <td >
                                                <dxe:ASPxButton ID="btnCancel" runat="server" Text="Cancel" width="80px" OnClick="btnCancel_Click" 
                                                    TabIndex="24">
                                                </dxe:ASPxButton>
                                            </td>
                                    
                                        </tr>
                                    </table>
                                   
                                </td>--%>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                </td>
            </tr>
    </table>
    </div>
    </div>
</asp:Content>
