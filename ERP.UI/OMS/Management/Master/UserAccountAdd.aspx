<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                09-02-2023        2.0.39           Pallab              25656 : Master module design modification 
2.0                17/02/2023        2.0.39           Sanchita            A setting required for 'User Account' Master module in FSM Portal
                                                                          Refer: 25669  
3.0                20/04/2023        2.0.40           Pallab              Add User Account module employee search popup auto focus add and "cancel" button color change
                                                                          Refer: 25866
4.0                22-05-2023       v2.0.40           Sanchita            The first name field of the employee master should consider 150 character from the application end. 
                                                                          For this module the First Name is increased up to 50 since both Employee name
                                                                          and User Name saved from the Fist Name data. In TBL_MASTER_USER, the column first_name has has length 50.  
                                                                          Refer: 26187
====================================================== Revision History ==========================================================--%>

<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAccountAdd.aspx.cs" Inherits="ERP.OMS.Management.Master.UserAccountAdd" %>--%>

<%@ Page Language="C#" AutoEventWireup="True"
    Inherits="ERP.OMS.Management.Master.management_master_UserAccountAdd" MasterPageFile="~/OMS/MasterPage/ERP.Master" CodeBehind="UserAccountAdd.aspx.cs" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>

    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <link href="../../../assests/css/custom/SearchPopup.css" rel="stylesheet" />
    <script src="../../../Scripts/SearchPopup.js"></script>
        
    <script src="../../../Scripts/SearchMultiPopup.js"></script>
    

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            ListBind();
            ChangeSource();           
        });
        
        //Mantis Issue 25148
        $(document).ready(function () {
            if ($("#IsChannelCircleSectionMandatory").val() == "1") {
                $("#ChannelMandatory").show();
                $("#CircleMandatory").show();
                $("#SectionMandatory").show();
            }
            else {
                $("#ChannelMandatory").hide();
                $("#CircleMandatory").hide();
                $("#SectionMandatory").hide();
            }
        });
        //End of Mantis Issue 25148

        function ChannelDefault_Checked() {
            if (chkChannelDefault.GetChecked() == true) {
                chkCircleDefault.SetChecked(false);
                chkSectionDefault.SetChecked(false);
            }
            else {
                chkChannelDefault.SetChecked(false);
            }
        }

        function CircleDefault_Checked() {
            if (chkCircleDefault.GetChecked() == true) {
                chkChannelDefault.SetChecked(false);
                chkSectionDefault.SetChecked(false);
            }
            else {
                chkCircleDefault.SetChecked(false);
            }
        }

        function SectionDefault_Checked() {
            if (chkSectionDefault.GetChecked() == true) {
                chkChannelDefault.SetChecked(false);
                chkCircleDefault.SetChecked(false);
            }
            else {
                chkSectionDefault.SetChecked(false);
            }
        }
       
        // Channel
        var ChannelArr = new Array();
        $(document).ready(function () {
            var ChannelObj = new Object();
            ChannelObj.Name = "ChannelSource";
            ChannelObj.ArraySource = ChannelArr;
            arrMultiPopup.push(ChannelObj);
            $("#calledFromChannelLookup_hidden").val("0");
        })

        function ChannelButnClick(s, e) {
            $('#ChannelModel').modal('show');
            $("#txtChannelSearch").focus();
        }

        function ChannelbtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#ChannelModel').modal('show');
                $("#txtChannelSearch").focus();
            }
        }

        function Channelskeydown(e) {

            var OtherDetails = {}
            OtherDetails.SearchKey = $("#txtChannelSearch").val();
            //OtherDetails.ChannelType = $("#ddlChannelTypes").val();
            if ($.trim($("#txtChannelSearch").val()) == "" || $.trim($("#txtChannelSearch").val()) == null) {
                return false;
            }            
            if ($('#hrCopy').val() == 'Copy') {
                arrMultiPopup = [];
                var ChannelObj = new Object();
                ChannelArr = JSON.parse($("#jsonChannel").text());
                ChannelObj.Name = "ChannelSource";
                ChannelObj.ArraySource = ChannelArr;
                arrMultiPopup.push(ChannelObj);
            }            
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                $("#calledFromChannelLookup_hidden").val("1");
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                if ($("#txtChannelSearch").val() != null && $("#txtChannelSearch").val() != "") {
                    callonServerM("UserAccountAdd.aspx/GetChannel", OtherDetails, "ChannelTable", HeaderCaption, "dPropertyIndex", "SetSelectedValues", "ChannelSource");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[ChannelIndex=0]"))
                    $("input[ChannelIndex=0]").focus();
            }
        }

        // End Channel

        // Circle
        var CircleArr = new Array();
        $(document).ready(function () {
            var CircleObj = new Object();
            CircleObj.Name = "CircleSource";
            CircleObj.ArraySource = CircleArr;
            arrMultiPopup.push(CircleObj);
            $("#calledFromCircleLookup_hidden").val("0");
        })

        function CircleButnClick(s, e) {
            $('#CircleModel').modal('show');
            $("#txtCircleSearch").focus();
        }

        function CirclebtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#CircleModel').modal('show');
                $("#txtCircleSearch").focus();
            }
        }

        function Circleskeydown(e) {

            var OtherDetails = {}
            OtherDetails.SearchKey = $("#txtCircleSearch").val();
            //OtherDetails.CircleType = $("#ddlCircleTypes").val();
            if ($.trim($("#txtCircleSearch").val()) == "" || $.trim($("#txtCircleSearch").val()) == null) {
                return false;
            }
            
            if ($('#hrCopy').val() == 'Copy') {
                arrMultiPopup = [];
                var CircleObj = new Object();
                CircleArr = JSON.parse($("#jsonCircle").text());
                CircleObj.Name = "CircleSource";
                CircleObj.ArraySource = CircleArr;
                arrMultiPopup.push(CircleObj);
            }
           
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                $("#calledFromCircleLookup_hidden").val("1");
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                if ($("#txtCircleSearch").val() != null && $("#txtCircleSearch").val() != "") {
                    callonServerM("UserAccountAdd.aspx/GetCircle", OtherDetails, "CircleTable", HeaderCaption, "dPropertyIndex", "SetSelectedValues", "CircleSource");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[CircleIndex=0]"))
                    $("input[CircleIndex=0]").focus();
            }
        }

        // Section
        var SectionArr = new Array();
        $(document).ready(function () {
            var SectionObj = new Object();
            SectionObj.Name = "SectionSource";
            SectionObj.ArraySource = SectionArr;
            arrMultiPopup.push(SectionObj);
            $("#calledFromSectionLookup_hidden").val("0");
        })

        function SectionButnClick(s, e) {
            $('#SectionModel').modal('show');
            $("#txtSectionSearch").focus();
        }

        function SectionbtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#SectionModel').modal('show');
                $("#txtSectionSearch").focus();
            }
        }

        function Sectionskeydown(e) {

            var OtherDetails = {}
            OtherDetails.SearchKey = $("#txtSectionSearch").val();
            //OtherDetails.SectionType = $("#ddlSectionTypes").val();
            if ($.trim($("#txtSectionSearch").val()) == "" || $.trim($("#txtSectionSearch").val()) == null) {
                return false;
            }
           
            if ($('#hrCopy').val() == 'Copy') {
                arrMultiPopup = [];
                var SectionObj = new Object();
                SectionArr = JSON.parse($("#jsonSection").text());
                SectionObj.Name = "SectionSource";
                SectionObj.ArraySource = SectionArr;
                arrMultiPopup.push(SectionObj);
            }
            
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                $("#calledFromSectionLookup_hidden").val("1");
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                if ($("#txtSectionSearch").val() != null && $("#txtSectionSearch").val() != "") {
                    callonServerM("UserAccountAdd.aspx/GetSection", OtherDetails, "SectionTable", HeaderCaption, "dPropertyIndex", "SetSelectedValues", "SectionSource");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[SectionIndex=0]"))
                    $("input[SectionIndex=0]").focus();
            }
        }

        // End Section

        function SetSelectedValues(Id, Name, ArrName) {
            if (ArrName == 'ChannelSource') {
                var key = Id;
                if (key != null && key != '') {
                    $("#txtChannel_hidden").val(Id);
                    ctxtChannels.SetText(Name);
                    $('#ChannelModel').modal('hide');
                }
                else {
                    $("#txtChannel_hidden").val('');
                    ctxtChannels.SetText('');
                    $('#ChannelModel').modal('hide');

                }
            }

            else if (ArrName == 'CircleSource') {
                var key = Id;
                if (key != null && key != '') {
                    $("#txtCircle_hidden").val(Id);
                    ctxtCircles.SetText(Name);
                    $('#CircleModel').modal('hide');
                }
                else {
                    $("#txtCircle_hidden").val('');
                    ctxtCircles.SetText('');
                    $('#CircleModel').modal('hide');
                }
            }


            else if (ArrName == 'SectionSource') {
                var key = Id;
                if (key != null && key != '') {
                    $("#txtSection_hidden").val(Id);
                    ctxtSections.SetText(Name);
                    $('#SectionModel').modal('hide');
                }
                else {
                    $("#txtSection_hidden").val('');
                    ctxtSections.SetText('');
                    $('#SectionModel').modal('hide');

                }
            }

            if (ctxtChannels.GetText() != '') {
                chkChannelDefault.SetEnabled(true);
            }
            else {
                chkChannelDefault.SetChecked(false);
                chkChannelDefault.SetEnabled(false);
            }

            if (ctxtCircles.GetText() != '') {
                chkCircleDefault.SetEnabled(true);
            }
            else {
                chkCircleDefault.SetChecked(false);
                chkCircleDefault.SetEnabled(false);
            }

            if (ctxtSections.GetText() != '') {
                chkSectionDefault.SetEnabled(true);
            }
            else {
                chkSectionDefault.SetChecked(false);
                chkSectionDefault.SetEnabled(false);
            }
        }
        
        FieldName = 'btnSave';
        function CallList(obj1, obj2, obj3) {

            if (obj1.value == "") {
                obj1.value = "%";
            }
            var obj5 = '';
            if (obj5 != '18') {
                //ajax_showOptions(obj1, obj2, obj3, obj5);
                ajax_showOptionsTEST(obj1, obj2, obj3, obj5);
                if (obj1.value == "%") {
                    obj1.value = "";
                }
            }
        }
        function Pageload() {

            //TrGeneral.style.display = "inline";
            //TrJoin.style.display = "none";
            //TrCTC.style.display = "none";
            //TrEmpID.style.display = "none";

            //TrGeneral.style.display = "inline";
            //TrJoin.style.display = "inline";
            //TrCTC.style.display = "inline";
            //btnEmpID.style.display = "none";
            // btnSave.style.display = "none";
            //btnSave.SetVisible(true);
            //btnJoin.style.display = "none";
        }
        function lstReportTo() {

            // $('#lstReferedBy').chosen();
            $('#lstReportTo').fadeIn();
        }
        //Mantis Issue 25148
        function setvalue() {
            //document.getElementById("txtReportTo_hidden").value = document.getElementById("lstReportTo").value;
        }
        //End of Mantis Issue 25148
        function setvalue() {
            //document.getElementById("txtReportTo_hidden").value = document.getElementById("lstReportTo").value;
        }
        function startLoading() {
            LoadingPanel.Show();
        }
        function stopLoading() {
            LoadingPanel.Hide();
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
        function ChangeSource() {

            var InterId = "";
            var fname = "";
            var sname = "";
            var lReferBy = $('select[id$=lstReportTo]');
            lReferBy.empty();
            $.ajax({
                type: "POST",
                url: "Employee_AddNew.aspx/GetreportTo",
                data: JSON.stringify({ firstname: fname, shortname: sname }),
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
                            $('#lstReportTo').append($('<option>').text(name).val(id));

                            //listItems.push('<option value="' +
                            //id + '">' + id
                            //+ '</option>');
                        }

                        $(lReferBy).append(listItems.join(''));

                        lstReportTo();
                        $('#lstReportTo').trigger("chosen:updated");

                    }
                    else {
                        alert("No records found");
                        lstReportTo();
                        $('#lstReportTo').trigger("chosen:updated");
                    }
                }
            });

        }

        function ForJoin() {
            TrGeneral.style.display = "none";
            //TrJoin.style.display = "inline";
            //TrCTC.style.display = "none";
            // TrEmpID.style.display = "none";

        }
        function ForCTC() {
            TrGeneral.style.display = "none";
            //TrJoin.style.display = "none";
            //TrCTC.style.display = "inline";
            //TrEmpID.style.display = "none";

        }
        //function ForEMPID() {
        //    TrGeneral.style.display = "none";
        //    //TrJoin.style.display = "none";
        //    //TrCTC.style.display = "inline";
        //    TrEmpID.style.display = "inline";
        //}

        function ValidateGeneral() {
            //if (document.getElementById("txtFirstNmae").value.trim() == '') {
            //    alert('Employee First Name is Required!..');
            //    return false;
            //}  
            var FirstName = document.getElementById('txtFirstNmae').value;
            if (FirstName.trim().length == 0) {
                $('#MandatoryFirstName').css({ 'display': 'block' });
                return false;
            }

        }
        //function ValidateDOJ() {
        //    //if (cmbDOJ.GetText() == '01-01-0100' || cmbDOJ.GetText() == '01-01-1900' || cmbDOJ.GetText() == '' || cmbDOJ.GetText() == '01010100') {
        //    //    alert('Joining Date is Required!.');
        //    //    return false;
        //    //}

        //    if (cmbDOJ.GetText() == '01-01-0100' || cmbDOJ.GetText() == '01-01-1900' || cmbDOJ.GetText() == '' || cmbDOJ.GetText() == '01010100') {
        //        $('#MandatoryDOJ').css({ 'display': 'block' });
        //        return false;
        //    }
        //    //if (JoiningDate.GetText() == '01-01-0100' || JoiningDate.GetText() == '01-01-1900' || JoiningDate.GetText() == '' || JoiningDate.GetText() == '01010100') {
        //    //    $('#MandatoryDOJ2').css({ 'display': 'block' });
        //    //    return false;
        //    //}
        //    var today = new Date();
        //    var dd = today.getDate();
        //    var mm = today.getMonth() + 1; //January is 0!

        //    var yyyy = today.getFullYear();
        //    if (dd < 10) {
        //        dd = '0' + dd
        //    }
        //    if (mm < 10) {
        //        mm = '0' + mm
        //    }
        //    var today = dd + '-' + mm + '-' + yyyy;

        //    if (cmbDOJ.GetText() > today) {
        //        alert('joining date can not be greater than today date');
        //        return false;
        //    }
        //}
        function ValidateCTC() {
            //Rev work start 26.07.2022 mantise no:25046            
            var FirstName = document.getElementById('txtFirstNmae').value;
            if (FirstName.trim().length == 0) {
                $('#MandatoryFirstName').css({ 'display': 'block' });
                return false;
            }            
            //Rev work close 26.07.2022 mantise no:25046
            if (document.getElementById("cmbBranch").value == "0") {
                $('#MandatoryBranch').css({ 'display': 'block' });
                return false;
            }
            else {
                $('#MandatoryBranch').css({ 'display': 'none' });
            }
            if (document.getElementById("cmbDesg").value == "0") {
                //alert('Please Select Designation.');
                $('#MandatoryDesignation').css({ 'display': 'block' });
                return false;
            }
            else {
                $('#MandatoryDesignation').css({ 'display': 'none' });
            }            
            if (document.getElementById("txtReportTo_hidden").value == '') {
                //alert('Please Select Reporting Head.');
                $('#MandatoryReportTo').css({ 'display': 'block' });
                return false;
            }
            else {
                $('#MandatoryReportTo').css({ 'display': 'none' });
            }

            
            if (document.getElementById("txtuserid").value.trim() == "") {
                $('#MandatoryLoginid').css({ 'display': 'block' });
                return false;
            }
            else {
                $('#MandatoryLoginid').css({ 'display': 'none' });
            }
            if (document.getElementById("ddlGroups").value == "0") {
                $('#MandatoryGroup').css({ 'display': 'block' });
                return false;
            }
            else {
                $('#MandatoryGroup').css({ 'display': 'none' });
            }

            if (document.getElementById("ddlType").value == "0") {
                $('#MandatoryType').css({ 'display': 'block' });
                return false;
            }
            else {
                $('#MandatoryType').css({ 'display': 'none' });
            }
            
            //Mantis Issue 25148
            if ($("#IsChannelCircleSectionMandatory").val() == "1") {
                //if ($("#txtChannels").val() == "") {
                if (ctxtChannels.GetText() == "") {
                    //jAlert("Please select channel type.", "Alert", function () {

                    //});
                    $('#MandatoryChannel').css({ 'display': 'block' });
                    return false;
                }
                else {
                    $('#MandatoryChannel').css({ 'display': 'none' });
                }
                //if ($("#txtCircle").val() == "") {
                if (ctxtCircles.GetText() == "") {
                    //jAlert("Please select circle.", "Alert", function () {

                    //});
                    $('#MandatoryCircle').css({ 'display': 'block' });
                    return false;
                }
                else {
                    $('#MandatoryCircle').css({ 'display': 'none' });
                }
                //if ($("#txtSection").val() == "") {
                if (ctxtSections.GetText() == "") {
                    //jAlert("Please select Section.", "Alert", function () {

                    //});
                    $('#MandatorySection').css({ 'display': 'block' });
                    return false;
                }
                else {
                    $('#MandatorySection').css({ 'display': 'none' });
                }
            }
            //End of Mantis Issue 25148
            var today = new Date();
            startLoading();
        }
        //function ValidateEMPID() {
        //    if (document.getElementById("txtAliasName").value == '') {
        //        alert('Employee Code is Required!..');
        //        return false;
        //    }
        //}

        function Check() {
            //var txtBranch = document.getElementById('txtBranch').value;
            var lstBranches = document.getElementById('lstBranches');
            var Name = document.getElementById('txtName').value;
            var Code = document.getElementById('txtCode').value;
            var tLength = lstBranches.length;
            //var selectedValue = lstBranches.checked;


            if (Name.trim().length == 0) {
                $('#MandatoryName').css({ 'display': 'block' });
                return false;
            }
            else {
                $('#MandatoryName').css({ 'display': 'none' });
            }

            if (Code.trim().length == 0) {
                $('#MandatoryShortname').css({ 'display': 'block' });
                return false;
            }
            else {
                $('#MandatoryShortname').css({ 'display': 'none' });
            }
            //if (tLength > 0) {
            //    $('#MandatoryFileNo').attr('style', 'display:none;color:red; position:absolute;right:736px;top:240px');
            //}
            //else {
            //    $('#MandatoryFileNo').attr('style', 'display:block;color:red; position:absolute;right:736px;top:240px');
            //    return false;
            //}
            var count = 0;
            for (i = 0; i < tLength; i++) {
                if (lstBranches.options[i].selected == true) {
                    count++;
                }

            }
            if (count > 0) {
                $('#MandatoryFileNo').css({ 'display': 'none' });
            }
            else {
                $('#MandatoryFileNo').css({ 'display': 'block' });
                return false;
            }
        }
    </script>

    <style>
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

        .dxtcLite_PlasticBlue > .dxtc-content {
            overflow: visible !important;
        }
        /*#lstReportTo_chosen{
            width:39% !important;
        }*/
        label {
            font-weight: 400 !important;
            margin-top: 8px;
        }
        
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
        .modal-footer
        {
            padding: 10px 20px 10px;
        }
        .for-long-srlbr
        {
            height: 400px;
    overflow: hidden;
    overflow-y: scroll;
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
            top: 41px;
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

    .dxpcLite_PlasticBlue .dxpc-headerText
    {
            color: #fff;
    }

    /*Rev end 1.0*/

    /*Rev 3.0*/
    #txtReportToSearch , #txtAdditionalReportingHeadSearch , #txtColleagueSearch , #txtColleague1Search , #txtColleague2Search , #txtChannelSearch , #txtCircleSearch , #txtSectionSearch
    {
        margin-bottom: 10px;
    }

    .btn-default
    {
            background-color: #e0e0e0;
    }
    /*Rev end 3.0*/

    @media only screen and (max-width: 768px) 
    {
        .breadCumb {
            padding: 0 18%;
        }

        .breadCumb > span
        {
            padding: 9px 15px;
        }
    }
    </style>
    <%--  <link href="../../css/choosen.min.css" rel="stylesheet" />--%>


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

            var OtherDetails = {}
            OtherDetails.reqStr = $("#txtReportToSearch").val();
            if ($.trim($("#txtReportToSearch").val()) == "" || $.trim($("#txtReportToSearch").val()) == null) {
                return false;
            }
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                if ($("#txtReportToSearch").val() != null && $("#txtReportToSearch").val() != "") {
                    callonServer("UserAccountAdd.aspx/GetOnDemandEmpCTC", OtherDetails, "ReportToTable", HeaderCaption, "ReportToIndex", "SetReportTo");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[ReportToIndex=0]"))
                    $("input[ReportToIndex=0]").focus();
            }
        }

        function SetReportTo(Id, Name) {
            $("#txtReportTo_hidden").val(Id);
            ctxtReportTo.SetText(Name);
            $('#ReportToModel').modal('hide');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--   <script src="/assests/pluggins/choosen/choosen.min.js"></script>--%>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //$(".chzn-select").chosen();
            //$(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        });

        /*Rev 3.0*/
        $(document).ready(function () {
            $('#ReportToModel').on('shown.bs.modal', function () {
                $('#txtReportToSearch').focus();
            })
            $('#AdditionalReportingHeadModel').on('shown.bs.modal', function () {
                $('#txtAdditionalReportingHeadSearch').focus();
            })
            
            $('#ChannelModel').on('shown.bs.modal', function () {
                $('#txtChannelSearch').focus();
            })
            $('#CircleModel').on('shown.bs.modal', function () {
                $('#txtCircleSearch').focus();
            })
            $('#SectionModel').on('shown.bs.modal', function () {
                $('#txtSectionSearch').focus();
            })
        });
        /*Rev end 3.0*/
    </script>


    <div class="breadCumb">
        <span>Add User Account</span>
        <div class="crossBtnN">
            <%--Rev work start 27.07.2022 mantise no:0025046: User Account --%>
            <%--<a href="UserAccountAdd.aspx">--%>
            <a href="UserAccountList.aspx">
            <%--Rev work close 27.07.2022 mantise no:0025046: User Account --%>
                <i class="fa fa-times"></i></a></div>
    </div>

    <div class="container" style="padding: 16px 15px; background: #fff; border-radius: 5px;">
        <table class="TableMain100">
            <tr id="TrGeneral" runat="server">
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="">
                                <div class="col-md-3">
                                    <label>First Name<span style="color: red">*</span></label>
                                    <div style="position: relative">
                                        <%--Rev work start 26.07.2022 mantise no:25046--%>
                                        <%--<asp:TextBox ID="txtFirstNmae" runat="server" Width="100%" MaxLength="20" CssClass="form-control"></asp:TextBox>--%>
                                        <%--Rev 4.0 [MaxLength="20" changed to MaxLength="50" ] --%>
                                        <asp:TextBox ID="txtFirstNmae" runat="server" Width="100%" MaxLength="50" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        <%--Rev work close 26.07.2022 mantise no:25046--%>
                                        <span id="MandatoryFirstName" class="pullleftClass fa fa-exclamation-circle iconRed " style="color: red; position: absolute; right: -4px; top: 10px; display: none" title="Mandatory"></span>
                                        <%--  <dxe:ASPxTextBox ID="txtFirstNmae" runat="server" Width="225px" TabIndex="2">
                                                 </dxe:ASPxTextBox>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <label>Middle Name</label>
                                    <div>
                                        <%--Rev work start 26.07.2022 mantise no:25046--%>
                                        <%--<asp:TextBox ID="txtMiddleName" runat="server" Width="100%" MaxLength="20" CssClass="form-control">--%>
                                        <asp:TextBox ID="txtMiddleName" runat="server" Width="100%" MaxLength="20" CssClass="form-control" TabIndex="2">
                                        <%--Rev work close 26.07.2022 mantise no:25046--%>
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <label>Last Name</label>
                                    <div>
                                        <%--Rev work start 26.07.2022 mantise no:25046--%>
                                        <%--<asp:TextBox ID="txtLastName" runat="server" Width="100%" MaxLength="20" CssClass="form-control">--%>
                                        <asp:TextBox ID="txtLastName" runat="server" Width="100%" MaxLength="20" CssClass="form-control" TabIndex="3">
                                        <%--Rev work close 26.07.2022 mantise no:25046--%>
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <%--Rev 1.0--%>
                                <%--<div class="col-md-3">--%>
                                 <div class="col-md-3 h-branch-select">
                                 <%--Rev end 1.0--%>
                                    <label>Branch<span style="color: red">*</span></label>
                                    <div style="position: relative">
                                        <%--Rev work start 26.07.2022 mantise no:25046--%>
                                        <%--<asp:DropDownList ID="cmbBranch" runat="server" Width="100%">--%>
                                        <asp:DropDownList ID="cmbBranch" runat="server" Width="100%" TabIndex="4">
                                            <%--Rev work close 26.07.2022 mantise no:25046--%>
                                        </asp:DropDownList>
                                        <span id="MandatoryBranch" class="pullleftClass fa fa-exclamation-circle iconRed " style="color: red; position: absolute; right: -18px; top: 10px; display: none" title="Mandatory"></span>
                                    </div>
                                </div>
                                <%--Rev 1.0--%>
                                <%--<div class="col-md-3">--%>
                                 <div class="col-md-3 h-branch-select">
                                 <%--Rev end 1.0--%>
                                    <label>Designation<span style="color: red">*</span></label>
                                    <div style="position: relative">
                                        <%--Rev work start 26.07.2022 mantise no:25046--%>
                                        <%--<asp:DropDownList ID="cmbDesg" runat="server" Width="100%" TabIndex="5">--%>
                                        <asp:DropDownList ID="cmbDesg" runat="server" Width="100%" TabIndex="5">
                                            <%--Rev work close 26.07.2022 mantise no:25046--%>
                                        </asp:DropDownList>
                                        <span id="MandatoryDesignation" class="pullleftClass fa fa-exclamation-circle iconRed " style="color: red; position: absolute; right: -18px; top: 10px; display: none" title="Mandatory"></span>
                                    </div>
                                </div>
                                 <div class="col-md-3">
                                    <label>Report To<span style="color: red">*</span></label>
                                    <div style="position: relative">
                                        <%-- <asp:DropDownList data-placeholder="Select or type here" Visible="false" runat="server" ID="ddlReportTo" class="chzn-select" Style="width: 255px;">
                                            </asp:DropDownList>
                                            <asp:ListBox ID="lstReportTo" CssClass="chsn" runat="server" Width="250px" data-placeholder="Select..."></asp:ListBox>--%>
                                        <%--Rev work start 26.07.2022 mantise no:25046--%>
                                        <%--<dxe:ASPxButtonEdit ID="txtReportTo" runat="server" ReadOnly="true" ClientInstanceName="ctxtReportTo" TabIndex="8" Width="100%">--%>
                                        <dxe:ASPxButtonEdit ID="txtReportTo" runat="server" ReadOnly="true" ClientInstanceName="ctxtReportTo" TabIndex="6" Width="100%">
                                            <%--Rev work close 26.07.2022 mantise no:25046--%>
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s,e){ReportToButnClick();}" KeyDown="ReportTobtnKeyDown" />
                                        </dxe:ASPxButtonEdit>
                                        <%--   <asp:TextBox ID="txtReportTo" runat="server" Width="225px" Visible="true" TabIndex="17"></asp:TextBox>--%>
                                        <span id="MandatoryReportTo" class="pullleftClass fa fa-exclamation-circle iconRed " style="color: red; position: absolute; right: -18px; top: 5px; display: none" title="Mandatory"></span>
                                        <asp:HiddenField ID="txtReportTo_hidden" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <label>User Login ID<span style="color: red">*</span></label>
                                    <div style="position: relative">
                                        <%--Rev work start 26.07.2022 mantise no:25046--%>
                                        <%--<asp:TextBox ID='txtuserid' runat="server" Width="100%" CssClass="form-control" ValidationGroup="a" value=" " MaxLength="50" autocomplete="off"></asp:TextBox>--%>
                                        <asp:TextBox ID='txtuserid' runat="server" Width="100%" CssClass="form-control" ValidationGroup="a" value="" MaxLength="50" autocomplete="off" TabIndex="7"></asp:TextBox>
                                          <%--Rev work close 26.07.2022 mantise no:25046--%> 
                                        <span id="MandatoryLoginid" class="pullleftClass fa fa-exclamation-circle iconRed " style="color: red; position: absolute; right: -18px; top: 10px; display: none" title="Mandatory"></span>
                                    </div>
                                </div>
                                <%--Rev 1.0--%>
                                <%--<div class="col-md-3">--%>
                                 <div class="col-md-3 h-branch-select">
                                 <%--Rev end 1.0--%>
                                    <label>User Group<span style="color: red">*</span></label>
                                    <div style="position: relative">
                                         <%--Rev work start 26.07.2022 mantise no:25046--%>
                                        <%--<asp:DropDownList ID="ddlGroups" runat="server" CssClass="sml" Width="100%"></asp:DropDownList>--%>
                                        <asp:DropDownList ID="ddlGroups" runat="server" CssClass="sml" Width="100%" TabIndex="8"></asp:DropDownList>
                                        <%--Rev work close 26.07.2022 mantise no:25046--%> 
                                        <span id="MandatoryGroup" class="pullleftClass fa fa-exclamation-circle iconRed " style="color: red; position: absolute; right: -18px; top: 10px; display: none" title="Mandatory"></span>
                                    </div>
                                </div>
                                <%--Rev 1.0--%>
                                <%--<div class="col-md-3">--%>
                                <%--Rev 2.0 [id="divUserType" runat="server" added] --%>
                                 <div class="col-md-3 h-branch-select" id="divUserType" runat="server">
                                 <%--Rev end 1.0--%>
                                    <label>User Type<span style="color: red">*</span></label>
                                    <div style="position: relative">
                                        <%--Rev work start 26.07.2022 mantise no:25046--%>
                                        <%--<asp:DropDownList ID="ddlType" runat="server" CssClass="sml" Width="100%"></asp:DropDownList>--%>
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="sml" Width="100%" TabIndex="9"></asp:DropDownList>
                                        <%--Rev work close 26.07.2022 mantise no:25046--%> 
                                        <span id="MandatoryType" class="pullleftClass fa fa-exclamation-circle iconRed " style="color: red; position: absolute; right: -18px; top: 10px; display: none" title="Mandatory"></span>
                                    </div>
                                </div>

                                <%--Rev 2.0 [id="divChannel" runat="server" added] --%>
                                 <div class="col-md-3" id="divChannel" runat="server">
                                    <label>Channel Type</label>
                                    <div style="position: relative">
                                        <%--Rev work start 26.07.2022 mantise no:25046--%>
                                        <%--<dxe:ASPxButtonEdit ID="txtChannels" runat="server" ReadOnly="true" ClientInstanceName="ctxtChannels" TabIndex="8" Width="100%">--%>
                                        <dxe:ASPxButtonEdit ID="txtChannels" runat="server" ReadOnly="true" ClientInstanceName="ctxtChannels" TabIndex="10" Width="100%">
                                            <%--Rev work close 26.07.2022 mantise no:25046--%>
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s,e){ChannelButnClick();}" KeyDown="ChannelbtnKeyDown" />
                                        </dxe:ASPxButtonEdit>
                                        <dxe:ASPxCheckBox ID="chkChannelDefault" runat="server" Text="Set as Default">
                                            <ClientSideEvents CheckedChanged="function (s, e) {ChannelDefault_Checked();}" />
                                        </dxe:ASPxCheckBox>
                                         <%--Mantis Issue 25148--%>
                                        <span id="MandatoryChannel" class="pullleftClass fa fa-exclamation-circle iconRed " style="color: red; position: absolute; right: -18px; top: 10px; display: none" title="Mandatory"></span>
                                        <%--End of Mantis Issue 25148--%>

                                        <asp:HiddenField ID="txtChannel_hidden" runat="server" />
                                        <asp:HiddenField ID="calledFromChannelLookup_hidden" runat="server" />
                                        <%--Mantis Issue 25148--%>
                                        <asp:HiddenField ID="IsChannelCircleSectionMandatory" runat="server" />
                                        <%--End of Mantis Issue 25148--%>
                                    </div>
                                </div>

                                <%--Rev 2.0 [id="divCircle" runat="server" added] --%>
                                <div class="col-md-3" id="divCircle" runat="server" >
                                    <label>Circle</label>
                                    <div style="position: relative">
                                        <%--Rev work start 26.07.2022--%>
                                        <%--<dxe:ASPxButtonEdit ID="txtCircle" runat="server" ReadOnly="true" ClientInstanceName="ctxtCircles" TabIndex="8" Width="100%">--%>
                                        <dxe:ASPxButtonEdit ID="txtCircle" runat="server" ReadOnly="true" ClientInstanceName="ctxtCircles" TabIndex="11" Width="100%">
                                            <%--Rev work close 26.07.2022--%>
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s,e){CircleButnClick();}" KeyDown="CirclebtnKeyDown" />
                                        </dxe:ASPxButtonEdit>
                                        <dxe:ASPxCheckBox ID="chkCircleDefault" runat="server" Text="Set as Default">
                                            <ClientSideEvents CheckedChanged="function (s, e) {CircleDefault_Checked();}" />
                                        </dxe:ASPxCheckBox>
                                        <%--Mantis Issue 25148--%>
                                        <span id="MandatoryCircle" class="pullleftClass fa fa-exclamation-circle iconRed " style="color: red; position: absolute; right: -18px; top: 10px; display: none" title="Mandatory"></span>
                                        <%--End of Mantis Issue 25148--%>
                                        <asp:HiddenField ID="txtCircle_hidden" runat="server" />
                                        <asp:HiddenField ID="calledFromCircleLookup_hidden" runat="server" />
                                    </div>
                                </div>

                                <%--Rev 2.0 [id="divSection" runat="server" added] --%>
                                <div class="col-md-3" id="divSection" runat="server">
                                    <label>Section</label>
                                    <div style="position: relative">
                                         <%--Rev work start 26.07.2022--%>
                                        <%--<dxe:ASPxButtonEdit ID="txtSection" runat="server" ReadOnly="true" ClientInstanceName="ctxtSections" TabIndex="8" Width="100%">--%>
                                        <dxe:ASPxButtonEdit ID="txtSection" runat="server" ReadOnly="true" ClientInstanceName="ctxtSections" TabIndex="12" Width="100%">
                                             <%--Rev work close 26.07.2022--%>
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s,e){SectionButnClick();}" KeyDown="SectionbtnKeyDown" />
                                        </dxe:ASPxButtonEdit>
                                        <dxe:ASPxCheckBox ID="chkSectionDefault" runat="server" Text="Set as Default">
                                            <ClientSideEvents CheckedChanged="function (s, e) {SectionDefault_Checked();}" />
                                        </dxe:ASPxCheckBox>
                                        <%--Mantis Issue 25148--%>
                                        <span id="MandatorySection" class="pullleftClass fa fa-exclamation-circle iconRed " style="color: red; position: absolute; right: -18px; top: 10px; display: none" title="Mandatory"></span>
                                        <%--End of Mantis Issue 25148--%>
                                        <asp:HiddenField ID="txtSection_hidden" runat="server" />
                                        <asp:HiddenField ID="calledFromSectionLookup_hidden" runat="server" />
                                    </div>
                                </div>

                                 <div class="col-md-3">
                                    <label>Contact No</label>
                                    <div style="position: relative">
                                         <%--Rev work start 26.07.2022--%>
                                        <%--<asp:TextBox ID="txtPhno" runat="server" Width="100%" MaxLength="20" CssClass="form-control"></asp:TextBox>--%>    
                                        <asp:TextBox ID="txtPhno" runat="server" Width="100%" MaxLength="20" CssClass="form-control" TabIndex="13"></asp:TextBox> 
                                        <%--Rev work close 26.07.2022--%>                                   
                                    </div>
                                </div>          
                            <div style="clear: both"></div>
                            <div class="col-md-12" style="padding-top: 15px;">
                                <asp:Button ID="btnCTC" CssClass="btn btn-primary btnUpdate" Text="Save & Proceed" runat="server" OnClientClick="setvalue()" OnClick="btnCTC_Click" />
                            </div>
                            </div>

                        </ContentTemplate>
                     <%--   <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>--%>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <%-- <tr id="TrJoin" runat="server">
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>

            <%-- </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnJoin" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>--%>
            <tr id="TrCTC" runat="server">
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <table class="TableMain100">

                                <tr>

                                    <td></td>
                                    <td></td>
                                </tr>

                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCTC" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>            
        </table>
        <dxe:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" ContainerElementID="btnCTC"
            Modal="True">
        </dxe:ASPxLoadingPanel>

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

                        <div id="ReportToTable" class="mt-3 for-long-srlbr">
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
        
        <div class="modal fade pmsModal w80" id="ChannelModel" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Channel Search</h4>
                    </div>
                    <div class="modal-body">
                        <%--Rev 1.0--%>
                        <%--<input type="text" onkeydown="Channelskeydown(event)" id="txtChannelSearch" autofocus style="width: 100%" placeholder="Search By Channel Name or Channel Code" />--%>
                        <input class="form-control" type="text" onkeydown="Channelskeydown(event)" id="txtChannelSearch" autofocus style="width: 100%" placeholder="Search By Channel Name or Channel Code" />
                        <%--Rev end 1.0--%>
                        <div id="ChannelTable">
                            <table border='1' width="100%" class="dynamicPopupTbl">
                                <tr class="HeaderStyle">
                                    <th class="hide">id</th>
                                    <th>Name</th>
                                    <%-- <th>Code</th>
                                <th>Location</th>--%>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnSaveChannel" class="btnOkformultiselection  btn btn-success" data-dismiss="modal" onclick="OKPopup('ChannelSource')">OK</button>
                        <button type="button" id="btnCloseChannel" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade pmsModal w80" id="CircleModel" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Circle Search</h4>
                    </div>
                    <div class="modal-body">
                        <%--Rev 1.0--%>
                        <%--<input type="text" onkeydown="Circleskeydown(event)" id="txtCircleSearch" autofocus style="width: 100%" placeholder="Search By Circle Name or Circle Code" />--%>
                        <input class="form-control" type="text" onkeydown="Circleskeydown(event)" id="txtCircleSearch" autofocus style="width: 100%" placeholder="Search By Circle Name or Circle Code" />
                        <%--Rev end 1.0--%>
                        <div id="CircleTable">
                            <table border='1' width="100%" class="dynamicPopupTbl">
                                <tr class="HeaderStyle">
                                    <th class="hide">id</th>
                                    <th>Name</th>
                                    <%-- <th>Code</th>
                                <th>Location</th>--%>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnSaveCircle" class="btnOkformultiselection  btn btn-success" data-dismiss="modal" onclick="OKPopup('CircleSource')">OK</button>
                        <button type="button" id="btnCloseCircle" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade pmsModal w80" id="SectionModel" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Section Search</h4>
                    </div>
                    <div class="modal-body">
                        <%--Rev 1.0--%>
                        <%--<input type="text" onkeydown="Sectionskeydown(event)" id="txtSectionSearch" autofocus style="width: 100%" placeholder="Search By Section Name or Section Code" />--%>
                        <input class="form-control" type="text" onkeydown="Sectionskeydown(event)" id="txtSectionSearch" autofocus style="width: 100%" placeholder="Search By Section Name or Section Code" />
                        <%--Rev end 1.0--%>
                        <div id="SectionTable">
                            <table border='1' width="100%" class="dynamicPopupTbl">
                                <tr class="HeaderStyle">
                                    <th class="hide">id</th>
                                    <th>Name</th>
                                    <%-- <th>Code</th>
                                <th>Location</th>--%>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnSaveSection" class="btnOkformultiselection btn btn-success" data-dismiss="modal" onclick="OKPopup('SectionSource')">OK</button>
                        <button type="button" id="btnCloseSection" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
       
    </div>
    
    <div runat="server" id="jsonChannel" class="hide"></div>
    <div runat="server" id="jsonCircle" class="hide"></div>
    <div runat="server" id="jsonSection" class="hide"></div>
    <asp:HiddenField ID="hrCopy" runat="server" />
    
    <style>
        a img {
            border: none;
        }

        ol li {
            list-style: decimal outside;
        }

        div#container {
            width: 780px;
            margin: 0 auto;
            padding: 1em 0;
        }

        div.side-by-side {
            width: 100%;
            margin-bottom: 1em;
        }

            div.side-by-side > div {
                float: left;
                width: 50%;
            }

                div.side-by-side > div > em {
                    margin-bottom: 10px;
                    display: block;
                }

        .clearfix:after {
            content: "\0020";
            display: block;
            height: 0;
            clear: both;
            overflow: hidden;
            visibility: hidden;
        }

        .chosen-container-active.chosen-with-drop .chosen-single div,
        .chosen-container-single .chosen-single div {
            display: none !important;
        }

        .chosen-container-single .chosen-single {
            border-radius: 0 !important;
            background: transparent !important;
        }
    </style>
</asp:Content>
