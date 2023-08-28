<%--====================================================Revision History=========================================================================
 1.0  Priti   V2.0.41  12-06-2023   0026291:In EMPLOYEE CTC module Admin can not select any Report to.
====================================================End Revision History===================================================--%>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master" Inherits="ERP.OMS.Management.Master.management_master_frmEmployeeCTC"
    CodeBehind="frmEmployeeCTC.aspx.cs" EnableEventValidation="false" %>

<%--<%@ register assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--___________________These files are for List Items__________________________-->
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <link href="../../../assests/css/custom/SearchPopup.css" rel="stylesheet" />
    <script src="../../../Scripts/SearchPopup.js"></script>

    <script language="javascript" type="text/javascript">
        /*Code  Added  By Priti on 06122016 to use jquery Choosen*/
        $(document).ready(function () {
            ListBind();
            //ChangeSource();

            $("#btnSave").click(function () {
                if (document.getElementById("txtReportTo_hidden").value != '') {
                    $('#redlstReportTo').attr('style', 'display:none');
                    return true;
                }
                else {
                    //Rev 1.0
                    var InternalID = $("#hdnUnqid_empCTC").val();                     
                    if (InternalID != "EMB0000002") {
                        //Rev 1.0 End
                        $('#redlstReportTo').attr('style', 'display:block');
                        return false;
                        //Rev 1.0
                    }
                    else {
                        return true;
                    }
                    //Rev 1.0 End
                }
            });
        });
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

        function lstReportTo() {
            //$('#lstReportTo').fadeIn();
           // $('#lstColleague').fadeIn();
            //$('#lstReportHead').fadeIn();
        }

        function setvalue() {
            //document.getElementById("txtReportTo_hidden").value = document.getElementById("lstReportTo").value;
           // document.getElementById("txtColleague_hidden").value = document.getElementById("lstColleague").value;
            //document.getElementById("txtAReportHead_hidden").value = document.getElementById("lstReportHead").value;
            document.getElementById("txtReportTo_hidden").value = document.getElementById("hdnReportToId").value;
        }

        function Changeselectedvalue() {
            var lstReportTo = document.getElementById("lstReportTo");
            if (document.getElementById("txtReportTo_hidden").value != '') {
                for (var i = 0; i < lstReportTo.options.length; i++) {
                    if (lstReportTo.options[i].value == document.getElementById("txtReportTo_hidden").value) {
                        lstReportTo.options[i].selected = true;
                    }
                }
                $('#lstReportTo').trigger("chosen:updated");
            }
        }

        function ChangeselectedvalueColleague() {
            var lstColleague = document.getElementById("lstColleague");
            if (document.getElementById("txtColleague_hidden").value != '') {
                for (var i = 0; i < lstColleague.options.length; i++) {
                    if (lstColleague.options[i].value == document.getElementById("txtColleague_hidden").value) {
                        lstColleague.options[i].selected = true;
                    }
                }
                $('#lstColleague').trigger("chosen:updated");
            }
        }

        // Mantis Issue 24655
        function ChangeselectedvalueColleague1() {
            var lstColleague1 = document.getElementById("lstColleague1");
            if (document.getElementById("txtColleague1_hidden").value != '') {
                for (var i = 0; i < lstColleague1.options.length; i++) {
                    if (lstColleague1.options[i].value == document.getElementById("txtColleague1_hidden").value) {
                        lstColleague1.options[i].selected = true;
                    }
                }
                $('#lstColleague1').trigger("chosen:updated");
            }
        }

        function ChangeselectedvalueColleague2() {
            var lstColleague2 = document.getElementById("lstColleague2");
            if (document.getElementById("txtColleague2_hidden").value != '') {
                for (var i = 0; i < lstColleague2.options.length; i++) {
                    if (lstColleague2.options[i].value == document.getElementById("txtColleague2_hidden").value) {
                        lstColleague2.options[i].selected = true;
                    }
                }
                $('#lstColleague2').trigger("chosen:updated");
            }
        }
        // End of Mantis Issue 24655
        function ChangeselectedvalueReportHead() {
            var lstReportHead = document.getElementById("lstReportHead");
            if (document.getElementById("txtAReportHead_hidden").value != '') {
                for (var i = 0; i < lstReportHead.options.length; i++) {
                    if (lstReportHead.options[i].value == document.getElementById("txtAReportHead_hidden").value) {
                        lstReportHead.options[i].selected = true;
                    }
                }
                $('#lstReportHead').trigger("chosen:updated");
            }
        }

        function ChangeSource() {
            var fname = "%";
            var lReportTo = $('select[id$=lstReportTo]');
            lReportTo.empty();

            var lColleague = $('select[id$=lstColleague]');
            lColleague.empty();

            // Mantis Issue 24655
            var lColleague1 = $('select[id$=lstColleague1]');
            lColleague1.empty();

            var lColleague2 = $('select[id$=lstColleague2]');
            lColleague2.empty();
            // End of Mantis Issue 24655

            var lReportHead = $('select[id$=lstReportHead]');
            lReportHead.empty();

            $.ajax({
                type: "POST",
                url: "frmEmployeeCTC.aspx/GetEmpCTC",
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

                            //$('#lstReportTo').append($('<option>').text(name).val(id));
                            //$('#lstColleague').append($('<option>').text(name).val(id));
                            //$('#lstReportHead').append($('<option>').text(name).val(id));
                        }

                        $(lReportTo).append(listItems.join(''));
                        $(lColleague).append(listItems.join(''));
                        // Mantis Issue 24655
                        $(lColleague1).append(listItems.join(''));
                        $(lColleague2).append(listItems.join(''));
                        // End of Mantis Issue 24655
                        $(lReportHead).append(listItems.join(''));
                        lstReportTo();
                        //$('#lstReportTo').trigger("chosen:updated");
                       // $('#lstColleague').trigger("chosen:updated");
                        //$('#lstReportHead').trigger("chosen:updated");
                        //Changeselectedvalue();
                       // ChangeselectedvalueColleague();
                        //ChangeselectedvalueReportHead();
                    }
                    else {
                        //   alert("No records found");
                        //lstReferedBy();
                        //$('#lstReportTo').trigger("chosen:updated");
                       // $('#lstColleague').trigger("chosen:updated");
                        //$('#lstReportHead').trigger("chosen:updated");
                    }
                }
            });
            // }
        }

        //.................code end.......
        FieldName = 'btnSave';
        function CallList(obj1, obj2, obj3) {
            if (obj1.value == "") {
                obj1.value = "%";
            }
            var obj5 = '';
            if (obj5 != '18') {
                ajax_showOptionsTEST(obj1, obj2, obj3, obj5);
                if (obj1.value == "%") {
                    obj1.value = "";
                }
            }
        }

        function OnCloseButtonClick(s, e) {
            var parentWindow = window.parent;
            parentWindow.popup.Hide();
        }


        //Validation For CTC
        function ValidateCTC() {
            //  alert('hhhh');

            if (document.getElementById("cmbOrganization").value == "0") {
                alert('Please Select  Organization.');
                return false;
            }

            else if (document.getElementById("cmbJobResponse").value == "0") {
                alert('Please Select Job Responsibility.');
                return false;
            }
            else if (document.getElementById("cmbBranch").value == "0") {
                alert('Please Select Branch.');
                return false;
            }
            else if (document.getElementById("cmbDesg").value == "0") {
                alert('Please Select Designation.');
                return false;
            }
            else if (document.getElementById("EmpType").value == "0") {
                alert('Please Select Employee Type.');
                return false;
            }
            else if (document.getElementById("cmbDept").value == "0") {
                alert('Please Select Employee Dept.');
                return false;
            }
            else if (document.getElementById("txtReportTo_hidden").value == '') {
                alert('Please Select Reporting Head.');
                return false;
            }

            else if (JoiningDate.GetText() == '01-01-0100' || cmbLeaveEff.GetText() == '01-01-1900' || cmbLeaveEff.GetText() == '' || cmbLeaveEff.GetText() == '01010100') {
                alert('Joining Date is Required!.');
                return false;
            }
        }
        //


        function MaskMoney(evt) {
            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');

            if (parts.length > 2) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
            if (parts.length == 2 && parts[1].length >= 2) return false;
        }

        function MaskMoneyDecimal(evt) {
            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57))) return false;
            var parts = evt.srcElement.value.split('.');

            if (parts.length > 2) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 9) return false;
            if (parts.length == 2 && parts[1].length >= 2) return false;
        }
        function OncmbLeaveEffChange() {
            var DOJ = document.getElementById("Hidden_DOJ").value;
            var AlreadyAssignedDate = document.getElementById("Hidden_LEF").value;
            var NewDate = cCmbLeaveEff.GetDate();
            CompareDate(DOJ, NewDate, "LE", "Selected Date Can not Be Less Than DOJ Date!!!", cCmbLeaveEff, AlreadyAssignedDate);
        }
        function OnJoiningDateChange() {
            var DOJ = document.getElementById("Hidden_DOJ").value;
            var AlreadyAssignedDate = document.getElementById("Hidden_CTCAppFrom").value;
            var NewDate = cJoiningDate.GetDate();
            CompareDate(DOJ, NewDate, "LE", "Selected Date Can not Be Less Than DOJ Date!!!", cJoiningDate, AlreadyAssignedDate);
        }

    </script>
    <style>
        .pullrightClass {
            position: absolute;
            right: 47px;
            width: 15px;
            height: 15px;
            top: 64px;
            color: #DF3636;
            font-size: 15px;
        }

        .r59 {
            top: 144px;
            right: 77px;
            font-size: 12px;
        }

        .r591 {
            top: 75px;
            right: 77px;
            font-size: 12px;
        }

        .ctcclass {
            position: absolute;
            right: -7px;
            top: 34px;
        }


        /*Code  Added  By Priti on 06122016 to use jquery Choosen*/
        .chosen-container.chosen-container-single {
            width: 100% !important;
        }

        .chosen-choices {
            width: 100% !important;
        }

        #lstReportTo {
            width: 200px;
        }

        #lstReportTo {
            display: none !important;
        }

        #lstReportTo_chosen {
            width: 100% !important;
        }

        .dxtcLite_PlasticBlue > .dxtc-content {
            overflow: visible !important;
        }

        .chosen-container.chosen-container-single {
            width: 100% !important;
        }

        .chosen-choices {
            width: 100% !important;
        }

        #lstColleague {
            width: 200px;
        }

        #lstColleague {
            display: none !important;
        }

        #lstColleague_chosen {
            width: 100% !important;
        }

        /*Mantis Issue 24655*/
        #lstColleague1 {
            width: 200px;
        }

        #lstColleague1 {
            display: none !important;
        }

        #lstColleague1_chosen {
            width: 100% !important;
        }

        #lstColleague2 {
            width: 200px;
        }

        #lstColleague2 {
            display: none !important;
        }

        #lstColleague2_chosen {
            width: 100% !important;
        }
        /*End of Mantis Issue 24655*/
        .dxtcLite_PlasticBlue > .dxtc-content {
            overflow: visible !important;
        }

        #lstReportHead {
            width: 200px;
        }

        #lstReportHead {
            display: none !important;
        }

        #lstReportHead_chosen {
            width: 100% !important;
        }
        label {
            font-weight:500;
            margin-top:10px;
        }
    </style>

    <script>
        function ReportToButnClick(s, e) {
            $('#ReportToModel').modal('show');
            $("#txtReportToSearch").focus();
        }

        function ReportTobtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#ReportToModel').modal('show');
                $("#txtReportToSearch").focus();
            }
        }

        function ReportTokeydown(e) {
            //REV 1.0
            var InternalID = $("#hdnUnqid_empCTC").val(); 
            if (InternalID != "EMB0000002") {
                //REV 1.0 End
                var OtherDetails = {}
                OtherDetails.reqStr = $("#txtReportToSearch").val();
                if ($.trim($("#txtReportToSearch").val()) == "" || $.trim($("#txtReportToSearch").val()) == null) {
                    return false;
                }
                if (e.code == "Enter" || e.code == "NumpadEnter") {
                    var HeaderCaption = [];
                    HeaderCaption.push("Name");
                    if ($("#txtReportToSearch").val() != null && $("#txtReportToSearch").val() != "") {
                        callonServer("frmEmployeeCTC.aspx/GetOnDemandEmpCTC", OtherDetails, "ReportToTable", HeaderCaption, "ReportToIndex", "SetReportTo");
                    }
                }
                else if (e.code == "ArrowDown") {
                    if ($("input[ReportToIndex=0]"))
                        $("input[ReportToIndex=0]").focus();
                }
            //REV 2.0
            }
            else {
                jAlert("Admin not requird Report To.");
                return false;
            }
             //REV 2.0 End
            
        }

        function SetReportTo(Id, Name) {
            $("#hdnReportToId").val(Id);
            ctxtReportTo.SetText(Name);
            $('#ReportToModel').modal('hide');
        }
    </script>

    <script>
        function ColleagueButnClick(s, e) {
            $('#ColleagueModel').modal('show');
            $("#txtColleagueSearch").focus();
        }

        function ColleaguebtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#ColleagueModel').modal('show');
                $("#txtColleagueSearch").focus();
            }
        }

        function Colleaguekeydown(e) {

            var OtherDetails = {}
            OtherDetails.reqStr = $("#txtColleagueSearch").val();
            if ($.trim($("#txtColleagueSearch").val()) == "" || $.trim($("#txtColleagueSearch").val()) == null) {
                return false;
            }
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                if ($("#txtColleagueSearch").val() != null && $("#txtColleagueSearch").val() != "") {
                    callonServer("frmEmployeeCTC.aspx/GetOnDemandEmpCTC", OtherDetails, "ColleagueTable", HeaderCaption, "ColleagueIndex", "SetColleague");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[ColleagueIndex=0]"))
                    $("input[ColleagueIndex=0]").focus();
            }
        }

        function SetColleague(Id, Name) {
            $("#txtColleague_hidden").val(Id);
            ctxtColleague.SetText(Name);
            $('#ColleagueModel').modal('hide');
        }
    </script>

    <%--Mantis Issue 24655--%>
    <script>
        function Colleague1ButnClick(s, e) {
            $('#Colleague1Model').modal('show');
            $("#txtColleague1Search").focus();
        }

        function Colleague1btnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#Colleague1Model').modal('show');
                $("#txtColleague1Search").focus();
            }
        }

        function Colleague1keydown(e) {

            var OtherDetails = {}
            OtherDetails.reqStr = $("#txtColleague1Search").val();
            if ($.trim($("#txtColleague1Search").val()) == "" || $.trim($("#txtColleague1Search").val()) == null) {
                return false;
            }
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                if ($("#txtColleague1Search").val() != null && $("#txtColleague1Search").val() != "") {
                    callonServer("frmEmployeeCTC.aspx/GetOnDemandEmpCTC", OtherDetails, "Colleague1Table", HeaderCaption, "Colleague1Index", "SetColleague1");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[Colleague1Index=0]"))
                    $("input[Colleague1Index=0]").focus();
            }
        }

        function SetColleague1(Id, Name) {
            $("#txtColleague1_hidden").val(Id);
            ctxtColleague1.SetText(Name);
            $('#Colleague1Model').modal('hide');
        }
    </script>

    <script>
        function Colleague2ButnClick(s, e) {
            $('#Colleague2Model').modal('show');
            $("#txtColleague2Search").focus();
        }

        function Colleague2btnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#Colleague2Model').modal('show');
                $("#txtColleague2Search").focus();
            }
        }

        function Colleague2keydown(e) {

            var OtherDetails = {}
            OtherDetails.reqStr = $("#txtColleague2Search").val();
            if ($.trim($("#txtColleague2Search").val()) == "" || $.trim($("#txtColleague2Search").val()) == null) {
                return false;
            }
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                if ($("#txtColleague2Search").val() != null && $("#txtColleague2Search").val() != "") {
                    callonServer("frmEmployeeCTC.aspx/GetOnDemandEmpCTC", OtherDetails, "Colleague2Table", HeaderCaption, "Colleague2Index", "SetColleague2");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[Colleague2Index=0]"))
                    $("input[Colleague2Index=0]").focus();
            }
        }

        function SetColleague2(Id, Name) {
            $("#txtColleague2_hidden").val(Id);
            ctxtColleague2.SetText(Name);
            $('#Colleague2Model').modal('hide');
        }
    </script>
    <%--End of Mantis Issue 24655--%>

    <script>
        function AdditionalReportingHeadButnClick(s, e) {
            $('#AdditionalReportingHeadModel').modal('show');
            $("#txtAdditionalReportingHeadSearch").focus();
        }

        function AdditionalReportingHeadbtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#AdditionalReportingHeadModel').modal('show');
                $("#txtAdditionalReportingHeadSearch").focus();
            }
        }

        function AdditionalReportingHeadkeydown(e) {

            var OtherDetails = {}
            OtherDetails.reqStr = $("#txtAdditionalReportingHeadSearch").val();
            if ($.trim($("#txtAdditionalReportingHeadSearch").val()) == "" || $.trim($("#txtAdditionalReportingHeadSearch").val()) == null) {
                return false;
            }
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                if ($("#txtAdditionalReportingHeadSearch").val() != null && $("#txtAdditionalReportingHeadSearch").val() != "") {
                    callonServer("frmEmployeeCTC.aspx/GetOnDemandEmpCTC", OtherDetails, "AdditionalReportingHeadTable", HeaderCaption, "AdditionalReportingHeadIndex", "SetAdditionalReportingHead");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[AdditionalReportingHeadIndex=0]"))
                    $("input[AdditionalReportingHeadIndex=0]").focus();
            }
        }

        function SetAdditionalReportingHead(Id, Name) {
            $("#txtAReportHead_hidden").val(Id);
            ctxtAdditionalReportingHead.SetText(Name);
            $('#AdditionalReportingHeadModel').modal('hide');
        }
    </script>
    <style>
        .dynamicPopupTbl>tbody>tr>td input[type="text"] {
            width:100% !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
        <div class="breadCumb">
            <span>Employee CTC</span>
            <div class="crossBtnN"><a href="Employee_EmployeeCTC.aspx"><i class="fa fa-times"></i></a></div>
        </div>
    
    <div class="container mt-5">
        <div class="backBox p-4">
            <div class="col-md-3 relative">
                <label>CTC Applicable From:<span style="color: red">*</span></label>
                <div style="position: relative">
                    <dxe:ASPxDateEdit ID="JoiningDate" ClientInstanceName="cJoiningDate" runat="server" DateOnError="Today" EditFormat="Custom"
                        TabIndex="0" Width="100%">
                        <ClientSideEvents DateChanged="OnJoiningDateChange" />

                    </dxe:ASPxDateEdit>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="a" runat="server" ControlToValidate="JoiningDate"
                        CssClass="fa fa-exclamation-circle ctcclass " ToolTip="Mandatory."
                        ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </div>
            </div>
            <div class="col-md-3 relative">
                <label>Organization<span style="color: red">*</span></label>
                <div>
                    <asp:DropDownList ID="cmbOrganization" runat="server" Width="100%" TabIndex="1" Enabled="false">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" ValidationGroup="a" runat="server" ToolTip="Mandatory."
                        CssClass=" fa fa-exclamation-circle ctcclass" SetFocusOnError="true" ControlToValidate="cmbOrganization" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-3 relative">
                <label>Job Responsibility<span style="color: red">*</span></label>
                <div>
                    <asp:DropDownList ID="cmbJobResponse" runat="server" Width="100%" TabIndex="2">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" ValidationGroup="a" runat="server" SetFocusOnError="true" ToolTip="Mandatory."
                        ControlToValidate="cmbJobResponse" ForeColor="Red" CssClass=" fa fa-exclamation-circle ctcclass "></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-3 relative">
                <label>Branch<span style="color: red">*</span></label>
                <div>
                    <asp:DropDownList ID="cmbBranch" runat="server" Width="100%" TabIndex="3">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="0" ValidationGroup="a" runat="server" SetFocusOnError="true" ToolTip="Mandatory."
                        CssClass=" fa fa-exclamation-circle ctcclass" ControlToValidate="cmbBranch" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="clear"></div>
            <div class="col-md-3 relative">
                <label>Designation<span style="color: red">*</span></label>
                <div>
                    <asp:DropDownList ID="cmbDesg" runat="server" Width="100%" TabIndex="4">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" InitialValue="0" CssClass=" fa fa-exclamation-circle ctcclass" ValidationGroup="a" ToolTip="Mandatory."
                        runat="server" SetFocusOnError="true" ControlToValidate="cmbDesg" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-3 relative">
                <label>Employee Type<span style="color: red">*</span></label>
                <div>
                    <asp:DropDownList ID="EmpType" runat="server" Width="100%" TabIndex="5" Enabled="false">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEType1" CssClass=" fa fa-exclamation-circle ctcclass" ToolTip="Mandatory."
                        InitialValue="0" ValidationGroup="a" runat="server" SetFocusOnError="true" ControlToValidate="EmpType" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div id="divContactType" class="col-md-3 relative" runat="server" visible="false">
                <asp:Label ID="lblContactType" runat="server"></asp:Label>
                <div>
                    <asp:DropDownList ID="ddlContactType" runat="server" Width="100%" TabIndex="5">
                        <asp:ListItem Value="" Text="--Select--"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-3 relative">
                <label>Department<span style="color: red">*</span></label>
                <div>
                    <asp:DropDownList ID="cmbDept" runat="server" Width="100%" TabIndex="6">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" CssClass=" fa fa-exclamation-circle ctcclass" InitialValue="0" ToolTip="Mandatory."
                        ValidationGroup="a" runat="server" SetFocusOnError="true" ControlToValidate="cmbDept" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-3">
                <label>Basic</label>
                <div>
                    <asp:TextBox ID="txtBasic" runat="server" Width="100%" TabIndex="7"></asp:TextBox>
                </div>
            </div>
            <div class="clear"></div>
            <div class="col-md-3 relative">
                <label>Report To<span style="color: red">*</span></label>
                <div>
                    <%--<asp:ListBox ID="lstReportTo" CssClass="chsn" runat="server" Width="100%" TabIndex="8" data-placeholder="Select..."></asp:ListBox>--%>
                     <dxe:ASPxButtonEdit ID="txtReportTo" runat="server" ReadOnly="true" ClientInstanceName="ctxtReportTo" TabIndex="8" Width="100%">
                        <Buttons>
                            <dxe:EditButton>
                            </dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s,e){ReportToButnClick();}" KeyDown="ReportTobtnKeyDown" />
                    </dxe:ASPxButtonEdit>
                    <asp:HiddenField ID="hdnReportToId" runat="server" />
                    <%--<asp:TextBox ID="txtReportTo" runat="server" Width="300px" TabIndex="8"></asp:TextBox>--%>
                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1123" ValidationGroup="a" runat="server" ControlToValidate="lstReportTo"
                        CssClass=" fa fa-exclamation-circle ctcclass" ToolTip="Mandatory."
                        ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                    <span id="redlstReportTo" class="pullleftClass fa fa-exclamation-circle iconRed " style="color: red; position: absolute; top: 118px; left: 400px; display: none" title="Mandatory"></span>

                    <asp:HiddenField ID="txtReportTo_hidden" runat="server" />
                </div>
            </div>
            <div class="col-md-3">
                <label>CCA</label>
                <div>
                    <asp:TextBox ID="txtCCA" runat="server" Width="100%" TabIndex="9"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-3">
                <label>Additional Reporting Head</label>
                <div>
                    <%--<asp:ListBox ID="lstReportHead" CssClass="chsn" runat="server" Width="100%" TabIndex="10" data-placeholder="Select..."></asp:ListBox>--%>
                      <dxe:ASPxButtonEdit ID="txtAdditionalReportingHead" runat="server" ReadOnly="true" ClientInstanceName="ctxtAdditionalReportingHead" TabIndex="8" Width="100%">
                        <Buttons>
                            <dxe:EditButton>
                            </dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s,e){AdditionalReportingHeadButnClick();}" KeyDown="AdditionalReportingHeadbtnKeyDown" />
                    </dxe:ASPxButtonEdit>
                    <%--  <asp:TextBox ID="txtAReportHead" runat="server" Width="300px" TabIndex="10"></asp:TextBox>--%>
                    <asp:HiddenField ID="txtAReportHead_hidden" runat="server" />
                </div>
            </div>
            <div class="col-md-3">
                <label>Colleague</label>
                <div>
                    <%--<asp:ListBox ID="lstColleague" CssClass="chsn" runat="server" Width="100%" TabIndex="11" data-placeholder="Select..."></asp:ListBox>--%>
                    <dxe:ASPxButtonEdit ID="txtColleague" runat="server" ReadOnly="true" ClientInstanceName="ctxtColleague" TabIndex="8" Width="100%">
                        <Buttons>
                            <dxe:EditButton>
                            </dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s,e){ColleagueButnClick();}" KeyDown="ColleaguebtnKeyDown" />
                    </dxe:ASPxButtonEdit>
                    <%--<asp:TextBox ID="txtColleague" runat="server" Width="300px" TabIndex="11"></asp:TextBox>--%>
                    <asp:HiddenField ID="txtColleague_hidden" runat="server" />
                </div>
            </div>
            <%--Mantis Issue 24655--%>
            <div class="clear"></div>
            <div class="col-md-3">
                <label>Colleague1</label>
                <div>
                    <dxe:ASPxButtonEdit ID="txtColleague1" runat="server" ReadOnly="true" ClientInstanceName="ctxtColleague1" TabIndex="8" Width="100%">
                        <Buttons>
                            <dxe:EditButton>
                            </dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s,e){Colleague1ButnClick();}" KeyDown="Colleague1btnKeyDown" />
                    </dxe:ASPxButtonEdit>
                    <asp:HiddenField ID="txtColleague1_hidden" runat="server" />
                </div>
            </div>
            <div class="col-md-3">
                <label>Colleague2</label>
                <div>
                    <dxe:ASPxButtonEdit ID="txtColleague2" runat="server" ReadOnly="true" ClientInstanceName="ctxtColleague2" TabIndex="8" Width="100%">
                        <Buttons>
                            <dxe:EditButton>
                            </dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents ButtonClick="function(s,e){Colleague2ButnClick();}" KeyDown="Colleague2btnKeyDown" />
                    </dxe:ASPxButtonEdit>
                    <asp:HiddenField ID="txtColleague2_hidden" runat="server" />
                </div>
            </div>
            <%--End of Mantis Issue 24655--%>
            <div class="clear"></div>
            <div class="col-md-3">
                <label>Current CTC</label>
                <div>
                    <asp:TextBox ID="txtCTC" runat="server" Width="100%" TabIndex="12"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <label>HRA</label>
                <div>
                    <asp:TextBox ID="txtHRA" runat="server" Width="100%" TabIndex="13"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <label>SP. Allowance</label>
                <div>
                    <asp:TextBox ID="txtSpAl" runat="server" Width="100%" TabIndex="14"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <label>Children Allowance</label>
                <div>
                    <asp:TextBox ID="txtChAL" runat="server" Width="100%" TabIndex="15"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <label>PF</label>
                <div>
                    <asp:TextBox ID="txtPf" runat="server" Width="100%" TabIndex="16"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <label>Medical Allowance</label>
                <div>
                    <asp:TextBox ID="txtMedAl" runat="server" Width="100%" TabIndex="17"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <label>LTA</label>
                <div>
                    <asp:TextBox ID="txtLTA" runat="server" Width="100%" TabIndex="18"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <label>Conveyance</label>
                <div>
                    <asp:TextBox ID="txtConvence" runat="server" Width="100%" TabIndex="19"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-3">
                <label>
                    Mobile Phone Expenses
                </label>
                <div>
                    <asp:TextBox ID="txtMbAl" runat="server" Width="100%" TabIndex="20"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <label>
                    Car Allowance
                </label>
                <div>
                    <asp:TextBox ID="txtCarAl" runat="server" Width="100%" TabIndex="21"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <label>
                    Uniform Allowance
                </label>
                <div>
                    <asp:TextBox ID="txtUniform" runat="server" Width="100%" TabIndex="22"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <label>
                    Books Periodicals Allowance
                </label>
                <div>
                    <asp:TextBox ID="txtBook" runat="server" Width="100%" TabIndex="23"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <label>
                    Seminar Allowance
                </label>
                <div>
                    <asp:TextBox ID="txtSeminar" runat="server" Width="100%" TabIndex="24"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <label>
                    Other Allowance
                </label>
                <div>
                    <asp:TextBox ID="txtOther" runat="server" Width="100%" TabIndex="25"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3 relative">
                <label>
                    Working Hour<span style="color: red">*</span>
                </label>
                <div>
                    <asp:DropDownList ID="cmbWorkingHr" runat="server" Width="100%" TabIndex="26">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass=" fa fa-exclamation-circle ctcclass" ForeColor="Red" runat="server" ToolTip="Mandatory."
                        ErrorMessage="" InitialValue="0" SetFocusOnError="true" ControlToValidate="cmbWorkingHr" ValidationGroup="a"></asp:RequiredFieldValidator>

                </div>
            </div>
            <div class="col-md-3 relative">
                <label>Leave Policy<span style="color: red">*</span></label>
                <div>
                    <asp:DropDownList ID="cmbLeaveP" runat="server" Width="100%" TabIndex="27">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass=" fa fa-exclamation-circle ctcclass" ForeColor="Red" runat="server" ToolTip="Mandatory."
                        ErrorMessage="" InitialValue="0" ControlToValidate="cmbLeaveP" SetFocusOnError="true" ValidationGroup="a"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="clear"></div>
            <div class="col-md-3">
                <label>Leave Effective From</label>
                <div>
                    <dxe:ASPxDateEdit ID="cmbLeaveEff" ClientInstanceName="cCmbLeaveEff" runat="server" DateOnError="Today" EditFormat="Custom"
                        TabIndex="28" Width="100%">
                        <ClientSideEvents DateChanged="OncmbLeaveEffChange" />
                    </dxe:ASPxDateEdit>
                </div>
                <div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbLeaveP"
                        ErrorMessage="Required." Width="100%"></asp:RequiredFieldValidator>
                </div>
            </div>


            <div class="col-md-3">
                <label>Employee Grade</label>
                <div>
                    <asp:DropDownList ID="ddlemployeegrade" runat="server" Width="100%" TabIndex="4">
                    </asp:DropDownList>
                </div>
            </div>


            <div class="col-md-6">
                <label>Remarks</label>
                <div>
                    <asp:TextBox ID="txtRemarks" runat="server" Width="100%" Height="60px" TabIndex="29" MaxLength="4000"></asp:TextBox>
                </div>
            </div>


            <div style="clear: both"></div>
            <div style="padding-left: 15px;">
                <dxe:ASPxButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="a" UseSubmitBehavior="False"
                    TabIndex="30" CssClass="btn btn-primary">
                    <ClientSideEvents Click="function(s,e){
                                                    setvalue()}" />
                </dxe:ASPxButton>
                <dxe:ASPxButton ID="btnCancel" runat="server" AutoPostBack="false" Text="Cancel" VerticalAlign="Bottom" OnClick="btnCancel_Click" UseSubmitBehavior="False"
                    CssClass="btn btn-danger" TabIndex="31">
                    <%--  <ClientSideEvents Click="OnCloseButtonClick" />--%>
                </dxe:ASPxButton>
            </div>
        </div>

        <asp:HiddenField ID="Hidden_DOJ" runat="server" />
        <asp:HiddenField ID="Hidden_LEF" runat="server" />
        <asp:HiddenField ID="Hidden_CTCAppFrom" runat="server" />
        <asp:HiddenField ID="hdnUnqid_empCTC" runat="server" />
        
    </div>

    <%--Report To--%>
    <div class="modal fade" id="ReportToModel" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Report To Search</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="ReportTokeydown(event)" id="txtReportToSearch" class="form-control" autofocus width="100%" placeholder="Search By Employee Name" />

                    <div id="ReportToTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Name</th>
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
    <%--Report To--%>

    <%--Additional Reporting Head--%>
    <div class="modal fade" id="AdditionalReportingHeadModel" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Additional Reporting Head</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="AdditionalReportingHeadkeydown(event)" id="txtAdditionalReportingHeadSearch"  class="form-control" autofocus width="100%" placeholder="Search By Employee Name" />

                    <div id="AdditionalReportingHeadTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Name</th>
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
    <%--Additional Reporting Head--%>

    <%--Colleague--%>
    <div class="modal fade" id="ColleagueModel" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Colleague</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="Colleaguekeydown(event)" id="txtColleagueSearch"  class="form-control" autofocus width="100%" placeholder="Search By Employee Name" />

                    <div id="ColleagueTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Name</th>
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
    <%--Colleague--%>

    <%--Mantis Issue 24655--%>
    <%--Colleague1--%>
    <div class="modal fade" id="Colleague1Model" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Colleague1</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="Colleague1keydown(event)" id="txtColleague1Search"  class="form-control" autofocus width="100%" placeholder="Search By Employee Name" />

                    <div id="Colleague1Table">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Name</th>
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
    <%--Colleague1--%>

    <%--Colleague2--%>
    <div class="modal fade" id="Colleague2Model" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Colleague2</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="Colleague2keydown(event)" id="txtColleague2Search"  class="form-control" autofocus width="100%" placeholder="Search By Employee Name" />

                    <div id="Colleague2Table">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Name</th>
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
    <%--Colleague2--%>
    <%--End of Mantis Issue 24655--%>
</asp:Content>
