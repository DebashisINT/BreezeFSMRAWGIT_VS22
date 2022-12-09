<%@ Page Title="Employee" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true"
    Inherits="ERP.OMS.Management.Master.management_master_Employee_general" CodeBehind="Employee_general.aspx.cs" EnableEventValidation="false" %>

<%--<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe000001" %>
<%@ Register assembly="DevExpress.Web.v10.2" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v10.2" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--___________________These files are for List Items__________________________-->


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
    <style>
        .chosen-container.chosen-container-single {
            width: 100% !important;
        }

        .chosen-choices {
            width: 100% !important;
        }

        #lstReferedBy {
            width: 400px;
        }

        #lstReferedBy {
            display: none !important;
        }

        .dxtcLite_PlasticBlue > .dxtc-content {
            overflow: visible !important;
        }

        #lstReferedBy_chosen {
            width: 39% !important;
        }
    </style>
    <link href="../../css/choosen.min.css" rel="stylesheet" />
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <link href="../../../assests/css/custom/SearchPopup.css" rel="stylesheet" />
    <script src="../../../Scripts/SearchPopup.js"></script>
    <%--Mantis Issue 24655--%>
    <script src="../../../Scripts/SearchMultiPopup.js"></script>
    <%--End of Mantis Issue 24655--%>

    <script language="javascript" type="text/javascript">

        //Code for UDF Control 
        function OpenUdf() {
            if (document.getElementById('IsUdfpresent').value == '0') {
                jAlert("UDF not define.");
            }
            else {
                var url = 'frm_BranchUdfPopUp.aspx?Type=Em';
                popup.SetContentUrl(url);
                popup.Show();
            }
        }
        // End Udf Code

        $(document).ready(function () {
            $(".chzn-select").chosen();
            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            ListBind();
            //ChangeSource();
            
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
        // Mantis Issue 24655
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
        // End of Mantis Issue 24655

        // Mantis Issue 24655
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
            /*Rev work 14.04.2022
            Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            debugger;
            arrMultiPopup = [];
            var ChannelObj = new Object();
            ChannelArr = JSON.parse($("#jsonChannel").text());
            ChannelObj.Name = "ChannelSource";
            ChannelObj.ArraySource = ChannelArr;
            arrMultiPopup.push(ChannelObj);
            /*Rev work close 14.04.2022
            Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                $("#calledFromChannelLookup_hidden").val("1");
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                if ($("#txtChannelSearch").val() != null && $("#txtChannelSearch").val() != "") {
                    callonServerM("Employee_AddNew.aspx/GetChannel", OtherDetails, "ChannelTable", HeaderCaption, "dPropertyIndex", "SetSelectedValues", "ChannelSource");
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
            /*Rev work 14.04.2022
            Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            debugger;
            arrMultiPopup = [];
            var CircleObj = new Object();
            CircleArr = JSON.parse($("#jsonCircle").text());
            CircleObj.Name = "CircleSource";
            CircleObj.ArraySource = CircleArr;
            arrMultiPopup.push(CircleObj);
            /*Rev work close 14.04.2022
            Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                $("#calledFromCircleLookup_hidden").val("1");
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                if ($("#txtCircleSearch").val() != null && $("#txtCircleSearch").val() != "") {
                    callonServerM("Employee_AddNew.aspx/GetCircle", OtherDetails, "CircleTable", HeaderCaption, "dPropertyIndex", "SetSelectedValues", "CircleSource");
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
            /*Rev work 14.04.2022
            Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            debugger;
            arrMultiPopup = [];
            var SectionObj = new Object();
            SectionArr = JSON.parse($("#jsonSection").text());
            SectionObj.Name = "SectionSource";
            SectionObj.ArraySource = SectionArr;
            arrMultiPopup.push(SectionObj);           
            /*Rev work close 14.04.2022
            Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked*/
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                $("#calledFromSectionLookup_hidden").val("1");
                var HeaderCaption = [];
                HeaderCaption.push("Name");
                if ($("#txtSectionSearch").val() != null && $("#txtSectionSearch").val() != "") {
                    callonServerM("Employee_AddNew.aspx/GetSection", OtherDetails, "SectionTable", HeaderCaption, "dPropertyIndex", "SetSelectedValues", "SectionSource");
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
                    chkChannelDefault.SetEnabled(true);
                }
                else {
                    $("#txtChannel_hidden").val('');
                    ctxtChannels.SetText('');
                    $('#ChannelModel').modal('hide');
                    chkChannelDefault.SetChecked(false);
                    chkChannelDefault.SetEnabled(false);
                }
            }

            else if (ArrName == 'CircleSource') {
                var key = Id;
                if (key != null && key != '') {
                    $("#txtCircle_hidden").val(Id);
                    ctxtCircles.SetText(Name);
                    $('#CircleModel').modal('hide');
                    chkCircleDefault.SetEnabled(true);
                }
                else {
                    $("#txtCircle_hidden").val('');
                    ctxtCircles.SetText('');
                    $('#CircleModel').modal('hide');
                    chkCircleDefault.SetChecked(false);
                    chkCircleDefault.SetEnabled(false);
                }
            }


            else if (ArrName == 'SectionSource') {
                var key = Id;
                if (key != null && key != '') {
                    $("#txtSection_hidden").val(Id);
                    ctxtSections.SetText(Name);
                    $('#SectionModel').modal('hide');
                    chkSectionDefault.SetEnabled(true);
                }
                else {
                    $("#txtSection_hidden").val('');
                    ctxtSections.SetText('');
                    $('#SectionModel').modal('hide');
                    chkSectionDefault.SetChecked(false);
                    chkSectionDefault.SetEnabled(false);
                }
            }
        }

        // End of Mantis Issue 24655

        function Changeselectedvalue() {
            var lstReferedBy = document.getElementById("lstReferedBy");
            if (document.getElementById("hdReferenceBy").value != '') {
                for (var i = 0; i < lstReferedBy.options.length; i++) {
                    if (lstReferedBy.options[i].value == document.getElementById("hdReferenceBy").value) {
                        lstReferedBy.options[i].selected = true;
                    }
                }
                $('#lstReferedBy').trigger("chosen:updated");
            }

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
        function lstReferedBy() {

            // $('#lstReferedBy').chosen();
            $('#lstReferedBy').fadeIn();
        }
        function setvalue() {
            //document.getElementById("txtReferedBy_hidden").value = document.getElementById("lstReferedBy").value;
            document.getElementById("txtReferedBy_hidden").value = document.getElementById("hdnReferedBy_hidden").value;

            //Mantis issue 25148
                if ($("#IsChannelCircleSectionMandatory").val() == "1") {
                    if (ctxtChannels.GetText() == "") {
                        $('#MandatoryChannel').css({ 'display': 'block' });
                        return false;
                    }
                    else {
                        $('#MandatoryChannel').css({ 'display': 'none' });
                    }
                    //if ($("#txtCircle").val() == "") {
                    if (ctxtCircles.GetText() == "") {
                        $('#MandatoryCircle').css({ 'display': 'block' });
                        return false;
                    }
                    else {
                        $('#MandatoryCircle').css({ 'display': 'none' });
                    }
                    //if ($("#txtSection").val() == "") {
                    if (ctxtSections.GetText() == "") {
                       $('#MandatorySection').css({ 'display': 'block' });
                        return false;
                    }
                    else {
                        $('#MandatorySection').css({ 'display': 'none' });
                    }
                }
            //    //End of Mantis Issue 25148
        }
        function ChangeSource() {

            var InterId = "";
            var fname = "%";
            var lReferBy = $('select[id$=lstReferedBy]');
            lReferBy.empty();
            $.ajax({
                type: "POST",
                url: "Employee_general.aspx/GetReferredBy",
                data: JSON.stringify({ firstname: fname }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var list = msg.d;
                    var listItems = [];
                    if (list.length > 0) {

                        for (var i = 0; i < list.length; i++) {
                            var id = '';
                            var name = '';
                            cnt_internalId = list[i].split('|')[1];
                            cnt_firstName = list[i].split('|')[0];
                            $('#lstReferedBy').append($('<option>').text(cnt_firstName).val(cnt_internalId));
                        }

                        $(lReferBy).append(listItems.join(''));

                        lstReferedBy();
                        $('#lstReferedBy').trigger("chosen:updated");
                        Changeselectedvalue();
                    }
                    else {
                        //alert("No records found");
                        lstReportTo();
                        $('#lstReferedBy').trigger("chosen:updated");
                    }
                }
            });

        }

        function ClickOnSpeak(keyValue) {
            var url = 'frmLanguageProfi.aspx?id=' + keyValue + '&status=speak';
            window.location.href = url;
            //window.open(url, 'popupwindow', 'left=120,top=200,height=500,width=500,scrollbars=no,toolbar=no,location=center,menubar=no');
        }

        function ClickOnWrite(keyValue) {
            var url = 'frmLanguageProfi.aspx?id=' + keyValue + '&status=write';
            window.location.href = url;
            //window.open(url, 'popupwindow', 'left=120,top=200,height=500,width=500,scrollbars=no,toolbar=no,location=center,menubar=no');

        }
        function heightB() {
            //   alert("123");
        }

        function disp_prompt(name) {
            if (name == "tab0") {
                //alert(name);
                document.location.href = "Employee_general.aspx";
            }
            if (name == "tab1") {
                //alert(name);
                document.location.href = "Employee_Correspondence.aspx";
            }
            else if (name == "tab2") {
                //alert(name);
                document.location.href = "Employee_Education.aspx";
            }
            else if (name == "tab3") {
                //alert(name);
                document.location.href = "Employee_Employee.aspx";
            }
            else if (name == "tab4") {
                //alert(name);
                document.location.href = "Employee_Document.aspx";
            }
            else if (name == "tab5") {
                //alert(name);
                document.location.href = "Employee_FamilyMembers.aspx";
            }
            else if (name == "tab6") {
                //alert(name);
                document.location.href = "Employee_GroupMember.aspx";
            }
            else if (name == "tab7") {
                //alert(name);
                document.location.href = "Employee_EmployeeCTC.aspx";
            }
            else if (name == "tab8") {
                //alert(name);
                document.location.href = "Employee_BankDetails.aspx";
            }
            else if (name == "tab9") {
                //alert(name);
                document.location.href = "Employee_Remarks.aspx";
            }
            else if (name == "tab10") {
                //alert(name);
                //document.location.href = "Employee_Remarks.aspx";
            }
            else if (name == "tab11") {
                //alert(name);
                // document.location.href = "Employee_Education.aspx";
            }
            else if (name == "tab12") {
                //alert(name);
                //   document.location.href="Employee_Subscription.aspx"; 
            }

            else if (name == "tab13") {
                //alert(name);
                var keyValue = $("#hdnlanguagespeak").val();
                document.location.href = 'frmLanguageProfi.aspx?id=' + keyValue + '&status=speak';
                //   document.location.href="Employee_Subscription.aspx"; 
            }



        }
        function FillValues(chk) {
            var sel = document.getElementById('ASPxPageControl1_LitSpokenLanguage');
            sel.value = chk;
        }
        function FillValues1(chk) {
            var sel = document.getElementById('ASPxPageControl1_LitWrittenLanguage');
            sel.value = chk;
        }

        function CallList(obj1, obj2, obj3) {
            if (obj1.value == "") {
                obj1.value = "%";
            }
            //alert('rrr');
            FieldName = 'ASPxPageControl1_cmbGender';


            //var obj4 = document.getElementById("ASPxPageControl1_cmbSource");
            var obj4 = document.getElementById('<%=cmbSource.ClientID %>');
            var obj5 = obj4.value;
            //alert(obj5);
            if (obj5 != '18') {
                ajax_showOptionsTEST(obj1, obj2, obj3, obj5);
                if (obj1.value == "%") {
                    obj1.value = "";
                }
            }
        }
        function AtTheTimePageLoad() {
            FieldName = 'ASPxPageControl1_cmbLegalStatus';
            //alert('22');
            document.getElementById("txtReferedBy_hidden").style.display = 'none';
        }
        function FreeHiddenField() {
            var hddn = document.getElementById("txtReferedBy_hidden");
            //  alert(hddn.value);
            hddn.value = '';
            //  alert(hddn.value);
        }
        //Mantis Issue 25148
        function ValidateCTC() {
            
            //Mantis Issue 25148
            if ($("#IsChannelCircleSectionMandatory").val() == "1") {
                //if ($("#txtChannels").val() == "") {
                if (ctxtChannels.GetText() == "") {
                    jAlert("Please select channel type.", "Alert", function () {

                    });
                    $('#MandatoryChannel').css({ 'display': 'block' });
                    return false;
                }
                else {
                    $('#MandatoryChannel').css({ 'display': 'none' });
                }
                //if ($("#txtCircle").val() == "") {
                if (ctxtCircles.GetText() == "") {
                    jAlert("Please select circle.", "Alert", function () {

                    });
                    $('#MandatoryCircle').css({ 'display': 'block' });
                    return false;
                }
                else {
                    $('#MandatoryCircle').css({ 'display': 'none' });
                }
                //if ($("#txtSection").val() == "") {
                if (ctxtSections.GetText() == "") {
                    jAlert("Please select Section.", "Alert", function () {

                    });
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
        function startLoading() {
            LoadingPanel.Show();
        }
        //End of Mantis Issue 25148
        FieldName = 'ASPxPageControl1_cmbLegalStatus';
    </script>


     <script>
         function ReferredByButnClick(s, e) {
             $('#ReferredByModel').modal('show');
             $("#txtReferredBySearch").focus();
         }

         function ReferredBybtnKeyDown(s, e) {
             if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                 $('#ReferredByModel').modal('show');
                 $("#txtReferredBySearch").focus();
             }
         }

         function ReferredBykeydown(e) {

             var OtherDetails = {}
             OtherDetails.reqStr = $("#txtReferredBySearch").val();
             if ($.trim($("#txtReferredBySearch").val()) == "" || $.trim($("#txtReferredBySearch").val()) == null) {
                 return false;
             }
             if (e.code == "Enter" || e.code == "NumpadEnter") {
                 var HeaderCaption = [];
                 HeaderCaption.push("Name");
                 if ($("#txtReferredBySearch").val() != null && $("#txtReferredBySearch").val() != "") {
                     callonServer("Employee_general.aspx/GetOnDemandEmpCTC", OtherDetails, "ReferredByTable", HeaderCaption, "ReferredByIndex", "SetReferredBy");
                 }
             }
             else if (e.code == "ArrowDown") {
                 if ($("input[ReferredByIndex=0]"))
                     $("input[ReferredByIndex=0]").focus();
             }
         }

         function SetReferredBy(Id, Name) {
             $("#hdnReferedBy_hidden").val(Id);
             ctxtReferredBy.SetText(Name);
             $('#ReferredByModel').modal('hide');
         }
         //Mantis Issue 25148
         //function setvalue() {
         //    //document.getElementById("txtReportTo_hidden").value = document.getElementById("lstReportTo").value;
         //    //Mantis issue 25148
         //    if ($("#IsChannelCircleSectionMandatory").val() == "1") {
         //        if (ctxtChannels.GetText() == "") {
         //            jAlert("Please select channel type.", "Alert", function () {
         //                $("#txtChannels").focus();
         //            });
         //            return false;
         //        }
         //        if (ctxtCircles.GetText() == "") {
         //            jAlert("Please select circle.", "Alert", function () {
         //                $("#txtCircle").focus();
         //            });
         //            return false;
         //        }
         //        if (ctxtSections.GetText() == "") {
         //            jAlert("Please select Section.", "Alert", function () {
         //                $("#txtSection").focus();
         //            });
         //            return false;
         //        }
         //    }
         //    //else {
         //    //    if ($("#txtChannels").val() == "Select Type") {
         //    //        $("#txtChannels").val("0");
         //    //    }
         //    //}
         //    //End of Mantis Issue 25148
         //}
         //End of Mantis Issue 25148
    </script>



    <style>
        .noleftpad {
            padding-left: 0 !important;
            margin-left: 0 !important;
        }

        .pos22 {
            position: absolute;
            right: 9px;
            top: 10px;
        }

        #lstReferedBy_chosen {
            width: 170px !important;
        }

        .dxbButton_PlasticBlue div.dxb {
            padding: 0 !important;
        }
        
        /*Mantis Issue 24655*/
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
        /*End of Mantis Issue 24655*/

    </style>

    <div class="breadCumb">
        <span>Employee</span>
        <div class="crossBtnN">
            <a href="Employee.aspx" id="goBackCrossBtn"><i class="fa fa-times"></i></a>
            <%--  <asp:HyperLink
                ID="goBackCrossBtn"
                NavigateUrl="#"
                runat="server">
        <i class="fa fa-times"></i>
            </asp:HyperLink>--%>
        </div>
    </div>
   
    <div class="container mt-5">
        <div class="backBox p-5">
        <table class="TableMain100">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="15px" ForeColor="Navy"
                        Width="819px" Height="18px"></asp:Label>
                    <%--debjyoti 22-12-2016--%>
                    <dxe:ASPxPopupControl ID="ASPXPopupControl" runat="server"
                        CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popup" Height="630px"
                        Width="600px" HeaderText="Add/Modify UDF" Modal="true" AllowResize="true" ResizingMode="Postponed">
                        <ContentCollection>
                            <dxe:PopupControlContentControl runat="server">
                            </dxe:PopupControlContentControl>
                        </ContentCollection>
                    </dxe:ASPxPopupControl>

                    <asp:HiddenField runat="server" ID="IsUdfpresent" />
                    <%--End debjyoti 22-12-2016--%>

                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" ClientInstanceName="page"
                        Width="100%">
                        <TabPages>
                            <dxe:TabPage Text="General" Name="General">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%; z-index: 101">
                                                        <tr>
                                                            <td class="Ecoheadtxt" style="width: 151px">Salutation
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 214px;">
                                                                <asp:DropDownList ID="CmbSalutation" runat="server" Width="170px" TabIndex="1">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="Ecoheadtxt" style="width: 100px">First Name<span style="color: red">*</span>
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 197px; position: relative">
                                                                <dxe:ASPxTextBox ID="txtFirstNmae" runat="server" Width="170px" TabIndex="2" MaxLength="150">
                                                                    <ValidationSettings ValidationGroup="a">
                                                                    </ValidationSettings>
                                                                </dxe:ASPxTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstNmae"
                                                                    Display="Dynamic" ErrorMessage="" SetFocusOnError="True" CssClass="pullleftClass fa fa-exclamation-circle iconRed pos22"
                                                                    ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="Ecoheadtxt" style="width: 137px">Middle Name
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 214px;">
                                                                <dxe:ASPxTextBox ID="txtMiddleName" runat="server" Width="170px" TabIndex="3" MaxLength="50">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="text-align: right; height: 1px"></td>
                                                            <td colspan="2" style="text-align: right; height: 1px"></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Ecoheadtxt" style="width: 151px">Last Name
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 214px;">
                                                                <dxe:ASPxTextBox ID="txtLastName" runat="server" Width="170px" TabIndex="4" MaxLength="50">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td class="Ecoheadtxt" style="width: 100px">Employee ID
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 197px;">



                                                                <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                         <ContentTemplate>--%>
                                                                <dxe:ASPxTextBox ID="txtAliasName" runat="server" Width="170px" TabIndex="5" ReadOnly="true" ForeColor="black">
                                                                </dxe:ASPxTextBox>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Style="color: #cc3300; text-decoration: underline; font-size: 8pt; display: none">Make System Employee Code</asp:LinkButton>
                                                                <br />
                                                                <asp:Label ID="lblErr" Text="" runat="server"></asp:Label>
                                                                <%--  </ContentTemplate>
                                                                    </asp:UpdatePanel>--%>
                                                                <%--  <asp:UpdatePanel ID="UpdatePanelId" runat="server">
                                                                    <ContentTemplate>
                                                                  <dxe:ASPxTextBox ID="txtAliasName" runat="server" Width="170px" TabIndex="5">
                                                                    </dxe:ASPxTextBox>
                                                                      <asp:LinkButton ID="LinkButton1" runat="server"  OnClick="LinkButton1_Click"  style="color: #cc3300; text-decoration: underline; font-size: 8pt;">Make System Employee Code</asp:LinkButton>
                                                                    <br /><asp:Label ID="lblErr" Text="" runat="server"></asp:Label>
                                                                                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>--%>
                                                            </td>
                                                            <%--Mantis Issue 24736--%>
                                                            <td class="Ecoheadtxt" style="width: 100px">Other ID</td>
                                                           <td class="Ecoheadtxt" style="text-align: left; width: 197px;">
                                                                <dxe:ASPxTextBox ID="txtOtherID" runat="server" Width="170px" TabIndex="3" MaxLength="200">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <%--End of Mantis Issue 24736--%>
                                                            <td class="Ecoheadtxt" style="width: 137px">Branch
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 214px;">
                                                                <asp:DropDownList ID="cmbBranch" runat="server" Width="170px" TabIndex="6">
                                                                </asp:DropDownList>
                                                            </td>
                                                        
                                                            <td class="Ecoheadtxt" style="width: 100px">Source
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 214px;">
                                                                <asp:DropDownList ID="cmbSource" runat="server" Width="170px" TabIndex="7">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="Ecoheadtxt" style="width: 143px">Referred By
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 197px;">
                                                               <%-- <asp:DropDownList data-placeholder="Select or type here" runat="server" Visible="false" ID="ddlReferedBy" TabIndex="8" class="chzn-select" Style="width: 169px;">
                                                                    <asp:ListItem Text="select referer" Value=""></asp:ListItem>

                                                                </asp:DropDownList>--%>
                                                                   <dxe:ASPxButtonEdit ID="txtReferredBy" runat="server" ReadOnly="true" ClientInstanceName="ctxtReferredBy" TabIndex="8" Width="74%">
                                                                    <Buttons>
                                                                        <dxe:EditButton>
                                                                        </dxe:EditButton>
                                                                    </Buttons>
                                                                    <ClientSideEvents ButtonClick="function(s,e){ReferredByButnClick();}" KeyDown="ReferredBybtnKeyDown" />
                                                                </dxe:ASPxButtonEdit>
                                                                <asp:HiddenField ID="hdnReferedBy_hidden" runat="server" />
                                                                 <%--<asp:ListBox ID="lstReferedBy" CssClass="chsn" runat="server" Font-Size="12px" Width="150px" data-placeholder="Select..."></asp:ListBox>--%>
                                                                <%--  <asp:TextBox ID="txtReferedBy" runat="server" Width="165px" Visible="true" TabIndex="8"></asp:TextBox>--%>
                                                                <asp:TextBox ID="txtReferedBy_hidden" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Ecoheadtxt" style="width: 137px">Marital Status
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 214px;">
                                                                <asp:DropDownList ID="cmbMaritalStatus" runat="server" Width="170px" TabIndex="9">
                                                                </asp:DropDownList>
                                                            </td>
                                                       
                                                            <td class="Ecoheadtxt" style="width: 100px">Gender
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 214px;">
                                                                <asp:DropDownList ID="cmbGender" runat="server" Width="170px" TabIndex="10">
                                                                    <asp:ListItem Value="1">Male</asp:ListItem>
                                                                    <asp:ListItem Value="0">Female</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="Ecoheadtxt" style="width: 143px">D.O.B.
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 197px;">
                                                                <dxe:ASPxDateEdit ID="DOBDate" runat="server" DateOnError="Today" EditFormat="Custom"
                                                                    TabIndex="11">
                                                                </dxe:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="Ecoheadtxt" style="width: 137px">Anniversary Date
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 214px;">
                                                                <dxe:ASPxDateEdit ID="AnniversaryDate" runat="server" DateOnError="Today" EditFormat="Custom"
                                                                    TabIndex="12">
                                                                </dxe:ASPxDateEdit>
                                                            </td>
                                                       
                                                            <td class="Ecoheadtxt" style="width: 100px">Legal Status
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 214px;">
                                                                <asp:DropDownList ID="cmbLegalStatus" runat="server" Width="170px" TabIndex="13">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 143px" class="Ecoheadtxt">Education
                                                            </td>
                                                            <td class="Ecoheadtxt" style="text-align: left; width: 197px;">
                                                                <asp:DropDownList ID="cmbEducation" runat="server" Width="170px" TabIndex="14">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td id="Td1" class="Ecoheadtxt" runat="server" style="width: 137px">Profession
                                                            </td>
                                                            <td id="Td2" class="Ecoheadtxt" style="text-align: left; width: 214px;" runat="server">
                                                                <asp:DropDownList ID="cmbProfession" runat="server" Width="170px" TabIndex="15">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>--%>
                                                        <tr id="hide1" runat="server" visible="False">
                                                            <td id="Td3" class="Ecoheadtxt" runat="server" style="width: 100px">Organization
                                                            </td>
                                                            <td id="Td4" class="Ecoheadtxt" style="text-align: left; width: 214px;" runat="server">
                                                                <dxe:ASPxTextBox ID="txtOrganization" runat="server" Width="170px" TabIndex="16">
                                                                </dxe:ASPxTextBox>
                                                            </td>
                                                            <td id="Td5" class="Ecoheadtxt" runat="server" style="width: 143px">Job Responsibility
                                                            </td>
                                                            <td id="Td6" class="Ecoheadtxt" style="text-align: left; width: 197px;" runat="server">
                                                                <asp:DropDownList ID="cmbJobResponsibility" runat="server" Width="170px" TabIndex="17">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td id="Td7" class="Ecoheadtxt" runat="server" style="width: 137px">Designation
                                                            </td>
                                                            <td id="Td8" class="Ecoheadtxt" style="text-align: left; width: 214px;" runat="server">
                                                                <asp:DropDownList ID="cmbDesignation" runat="server" Width="169px" TabIndex="18">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr id="hide2" runat="server" visible="False">
                                                            <td id="Td9" class="Ecoheadtxt" runat="server" style="width: 100px">Industry/Sector
                                                            </td>
                                                            <td id="Td10" class="Ecoheadtxt" runat="server" style="width: 214px; text-align: left;">
                                                                <asp:DropDownList ID="cmbIndustry" runat="server" Width="170px" TabIndex="19">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td id="Td11" class="Ecoheadtxt" runat="server" style="width: 143px">Rating
                                                            </td>
                                                            <td id="Td12" class="Ecoheadtxt" style="text-align: left; width: 197px;" runat="server">
                                                                <asp:DropDownList ID="cmbRating" runat="server" Width="170px" TabIndex="20">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr id="hide3" runat="server" visible="False">
                                                            <td id="Td13" class="Ecoheadtxt" runat="server" style="width: 151px">Contact Status
                                                            </td>
                                                            <td id="Td14" class="Ecoheadtxt" style="text-align: left; width: 214px;" runat="server">
                                                                <asp:DropDownList ID="cmbContactStatus" runat="server" Width="170px" TabIndex="21">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td id="Td1" class="Ecoheadtxt" runat="server" style="width: 137px">Profession
                                                            </td>
                                                            <td id="Td2" class="Ecoheadtxt" style="text-align: left; width: 214px;" runat="server">
                                                                <asp:DropDownList ID="cmbProfession" runat="server" Width="170px" TabIndex="15">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td id="Td16" class="Ecoheadtxt" runat="server" style="width: 137px">Blood Group
                                                            </td>
                                                            <td id="Td17" class="Ecoheadtxt" style="text-align: left; width: 214px;" runat="server">
                                                                <asp:DropDownList ID="cmbBloodgroup" runat="server" Width="75px" TabIndex="22">
                                                                    <asp:ListItem Value="N/A">N/A</asp:ListItem>
                                                                    <asp:ListItem Value="A+">A+</asp:ListItem>
                                                                    <asp:ListItem Value="A-">A-</asp:ListItem>
                                                                    <asp:ListItem Value="B+">B+</asp:ListItem>
                                                                    <asp:ListItem Value="B-">B-</asp:ListItem>
                                                                    <asp:ListItem Value="AB+">AB+</asp:ListItem>
                                                                    <asp:ListItem Value="AB-">AB-</asp:ListItem>
                                                                    <asp:ListItem Value="O+">O+</asp:ListItem>
                                                                    <asp:ListItem Value="O-">O-</asp:ListItem>
                                                                </asp:DropDownList>
                                                                &nbsp;&nbsp; <%--Allow Web Login--%>
                                                                <asp:CheckBox ID="chkAllow" Visible="false" TextAlign="Left" runat="server" Width="17px" TabIndex="23" />
                                                            </td>

                                                            <td id="Td18" class="Ecoheadtxt hidden" runat="server" style="width: 137px">Show Plan Details
                                                            </td>
                                                            <td id="Td19" class="Ecoheadtxt hidden" style="text-align: left; width: 214px;" runat="server">
                                                                <asp:DropDownList ID="ddlPlanDetails" runat="server" Width="75px" TabIndex="22">
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td id="Td28" class="Ecoheadtxt" runat="server" style="width: 137px">Date Of Joining
                                                            </td>
                                                            <td id="Td29" class="Ecoheadtxt" style="text-align: left; width: 214px;" runat="server">
                                                              <%--  <dxe:ASPxDateEdit Width="100%" ID="cmbDOJ" runat="server">
                                                                </dxe:ASPxDateEdit>--%>
                                                                  <dxe:ASPxDateEdit ID="DOJDate" runat="server" DateOnError="Today" EditFormat="Custom" TabIndex="23">
                                                                </dxe:ASPxDateEdit>
                                                            </td>
                                                            <td id="Td20" class="Ecoheadtxt hidden" runat="server" style="width: 137px">Show Meeting
                                                            </td>
                                                            <td id="Td21" class="Ecoheadtxt hidden" style="text-align: left; width: 214px;" runat="server">
                                                                <asp:DropDownList ID="ddlMeeting" runat="server" Width="75px" TabIndex="22">
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td id="Td22" class="Ecoheadtxt hidden" runat="server" style="width: 137px">Product Rate Editable
                                                            </td>
                                                            <td id="Td23" class="Ecoheadtxt hidden" style="text-align: left; width: 214px;" runat="server">
                                                                <asp:DropDownList ID="ddlRateEdit" runat="server" Width="75px" TabIndex="22">
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>

                                                            <td id="Td24" class="Ecoheadtxt hidden" runat="server" style="width: 137px">Show More Details Compulsory
                                                            </td>
                                                            <td id="Td25" class="Ecoheadtxt hidden" style="text-align: left; width: 214px;" runat="server">
                                                                <asp:DropDownList ID="ddlMoreDeatilsCompulsory" runat="server" Width="75px" TabIndex="22">
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>

                                                            <td id="Td26" class="Ecoheadtxt hidden" runat="server" style="width: 137px">Show More Details
                                                            </td>
                                                            <td id="Td27" class="Ecoheadtxt hidden" style="text-align: left; width: 214px;" runat="server">
                                                                <asp:DropDownList ID="ddlMoreDetails" runat="server" Width="75px" TabIndex="22">
                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <%--Mantis Issue 24655--%>
                                                        <tr>
                                                            <td id="Td30" class="Ecoheadtxt" runat="server" style="width: 137px">Channel Type
                                                                <%--Mantis Issue 25148--%>
                                                                <span id="ChannelMandatory" style="color: red">*</span>
                                                                <%--End of Mantis Issue 25148--%>
                                                            </td>
                                                            <td id="Td31" class="Ecoheadtxt" style="text-align: left; width: 214px;" runat="server">
                                                                <dxe:ASPxButtonEdit ID="txtChannels" runat="server" ReadOnly="true" ClientInstanceName="ctxtChannels" TabIndex="23" Width="169px">
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
                                                            </td>

                                                            <td id="Td32" class="Ecoheadtxt" runat="server" style="width: 137px">Circle
                                                                <%--Mantis Issue 25148--%>
                                                                <span id="CircleMandatory" style="color: red">*</span>
                                                                <%--End of Mantis Issue 25148--%>
                                                            </td>
                                                            <td id="Td33" class="Ecoheadtxt" style="text-align: left; width: 214px;" runat="server">
                                                                <dxe:ASPxButtonEdit ID="txtCircle" runat="server" ReadOnly="true" ClientInstanceName="ctxtCircles" TabIndex="24" Width="169px">
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
                                                            </td>

                                                            <td id="Td34" class="Ecoheadtxt" runat="server" style="width: 137px">Section
                                                                <%--Mantis Issue 25148--%>
                                                                <span id="SectionMandatory" style="color: red">*</span>
                                                                <%--End of Mantis Issue 25148--%>
                                                            </td>
                                                            <td id="Td35" class="Ecoheadtxt" style="text-align: left; width: 214px;" runat="server">
                                                                <dxe:ASPxButtonEdit ID="txtSection" runat="server" ReadOnly="true" ClientInstanceName="ctxtSections" TabIndex="25" Width="169px">
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
                                                            </td>

                                                        </tr>
                                                        
                                                        <%--End of Mantis Issue 24655--%>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="TrLang" runat="server">
                                                <td id="Td15" runat="server" style="width: 845px">
                                                    <asp:Panel ID="Panel2" runat="server" Width="100%" BorderColor="White" BorderWidth="1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%; display: none;">
                                                                        <tr>
                                                                            <td colspan="2" style="text-align: center; padding: 8px 10px; background: #ccc;">
                                                                                <span>Language Proficiencies </span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 50%; vertical-align: top; display: none;">
                                                                                <asp:Panel ID="PnlSpLAng" runat="server" Width="100%" BorderColor="White" BorderWidth="1px">
                                                                                    <table width="80%">
                                                                                        <tr>
                                                                                            <td style="text-align: left; color: Blue;">Can Speak
                                                                                            </td>

                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="vertical-align: top" class="gridcellleft">
                                                                                                <asp:TextBox ID="LitSpokenLanguage" runat="server" ForeColor="Maroon" BackColor="Transparent"
                                                                                                    BorderStyle="None" Width="100%" ReadOnly="True" CssClass="noleftpad"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: left; vertical-align: top; font-size: x-small; color: Red; display: none;">
                                                                                                <%-- <a href="frmLanguages.aspx?id='<%=SpLanguage %>'&status=speak" onclick="window.open(this.href,'popupwindow','left=120,top=170,height=400,width=200,scrollbars=no,toolbar=no,location=center,menubar=no'); return false;"
                                                                                                        style="font-size: x-small; color: Red;" tabindex="24">click to add</a>--%>
                                                                                                <a href="javascript:void(0);" onclick="ClickOnSpeak('<%# Eval("id") %>')" title="Map" class="btn btn-primary" tabindex="24">Select Language(s)</a>
                                                                                                <asp:HiddenField ID="hdnlanguagespeak" runat="server" Value='<%# Eval("id") %>' />

                                                                                            </td>

                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </td>
                                                                            <td style="width: 50%; vertical-align: top; display: none;">
                                                                                <asp:Panel ID="PnlWrLang" runat="server" Width="100%" BorderColor="White" BorderWidth="1px">
                                                                                    <table width="80%">
                                                                                        <tr>
                                                                                            <td style="text-align: left; color: Blue;">Can Write
                                                                                            </td>

                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="LitWrittenLanguage" runat="server" ForeColor="Maroon" BackColor="Transparent"
                                                                                                    BorderStyle="None" Width="100%" ReadOnly="True" CssClass="noleftpad"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: left; vertical-align: top; font-size: x-small; color: Red;">
                                                                                                <%--<a href="frmLanguages.aspx?id='<%=WLanguage %>'&status=write" onclick="window.open(this.href,'popupwindow','left=120,top=170,height=400,width=200,scrollbars=no,toolbar=no,location=center,menubar=no'); return false;"
                                                                                                        style="color: Red; font-size: x-small;" tabindex="25">click to add</a>--%>
                                                                                                <a href="javascript:void(0);" onclick="ClickOnWrite('<%# Eval("id") %>')" title="Map" class="btn btn-primary" tabindex="24">Select Language(s)</a>
                                                                                                <asp:HiddenField ID="hddnwrite" runat="server" Value='<%# Eval("id") %>' />

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
                                        </table>
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                            <dxe:TabPage Text="Correspondence" Name="CorresPondence">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                            <dxe:TabPage Name="Education" Text="Education">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                            <dxe:TabPage Name="Employee" Text="Employment">
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
                            <dxe:TabPage Name="FamilyMembers" Text="Family">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                            <dxe:TabPage Name="GroupMember" Text="Group">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="EmployeeCTC" Text="CTC">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                            <dxe:TabPage Name="BankDetails" Text="Bank">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                            <dxe:TabPage Name="Remarks" Text="UDF" Visible="false">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                            <dxe:TabPage Name="DPDetails" Text="DP" Visible="false">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>


                            <dxe:TabPage Name="Registration" Text="Registration" Visible="false">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>




                            <dxe:TabPage Name="Subscription" Text="Subscription" Visible="false">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="Language" Text="Language">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                        </TabPages>
                        <ClientSideEvents ActiveTabChanged="function(s, e) {
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
                                                var Tab13 = page.GetTab(13);
                                            

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
                                               

	                                            }"></ClientSideEvents>
                        <ContentStyle>
                            <Border BorderColor="#002D96" BorderStyle="Solid" BorderWidth="1px" />
                        </ContentStyle>
                        <LoadingPanelStyle ImageSpacing="6px">
                        </LoadingPanelStyle>
                    </dxe:ASPxPageControl>
                </td>
            </tr>
            <tr>
                <td style="height: 8px">
                    <table style="width: 100%;">
                        <tr>
                            <td align="left" style="width: 843px">
                                <asp:HiddenField ID="hdReferenceBy" runat="server" />
                                <table style="margin-top: 10px;">
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="a" UseSubmitBehavior="False"
                                                TabIndex="26" CssClass="btn btn-primary">
                                                <ClientSideEvents Click="function(s,e){
                                                    setvalue()}" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <button type="button" value="UDF" class="btn btn-primary dxbButton" onclick="OpenUdf()">UDF</button>
                                            <a href="employee.aspx" class="btn btn-danger">Cancel</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
            </div>
    </div>
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
        .Ecoheadtxt{
            padding-top:10px;
        }
    </style>


     <%--Referred By--%>
    <div class="modal fade" id="ReferredByModel" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Referred By</h4>
                </div>
                <div class="modal-body">
                    <input type="text" class="form-control" onkeydown="ReferredBykeydown(event)" id="txtReferredBySearch" autofocus width="100%" placeholder="Search By Employee Name" />

                    <div id="ReferredByTable">
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
    <%--Referred By--%>

    <%--Mantis Issue 24655--%>
        <div class="modal fade pmsModal w80" id="ChannelModel" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Channel Search</h4>
                    </div>
                    <div class="modal-body">
                        <input type="text" onkeydown="Channelskeydown(event)" id="txtChannelSearch" autofocus style="width: 100%" placeholder="Search By Channel Name or Channel Code" />
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
                        <button type="button" ID="btnSaveChannel" class="btnOkformultiselection btn-default  btn btn-success" data-dismiss="modal" onclick="OKPopup('ChannelSource')">OK</button>
                        <button type="button" ID="btnCloseChannel" class="btn btn-default" data-dismiss="modal">Close</button>
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
                        <input type="text" onkeydown="Circleskeydown(event)" id="txtCircleSearch" autofocus style="width: 100%" placeholder="Search By Circle Name or Circle Code" />
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
                        <button type="button" ID="btnSaveCircle" class="btnOkformultiselection btn-default  btn btn-success" data-dismiss="modal" onclick="OKPopup('CircleSource')">OK</button>
                        <button type="button" ID="btnCloseCircle" class="btn btn-default" data-dismiss="modal">Close</button>
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
                        <input type="text" onkeydown="Sectionskeydown(event)" id="txtSectionSearch" autofocus style="width: 100%" placeholder="Search By Section Name or Section Code" />
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
                        <button type="button" ID="btnSaveSection" class="btnOkformultiselection btn-default  btn btn-success" data-dismiss="modal" onclick="OKPopup('SectionSource')">OK</button>
                        <button type="button" ID="btnCloseSection" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <%--End of Mantis Issue 24655--%>
      <%--Rev Work 14.04.2022
          Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked--%>
    <div runat="server" id="jsonChannel" class="hide"></div>
    <div runat="server" id="jsonCircle" class="hide"></div>
    <div runat="server" id="jsonSection" class="hide"></div>
   <%--Rev work Close 14.04.2022
       Mantise Issue :0024828: In employee master in entry section in edit mode selected channel,circle,section, not coming as checked--%>
</asp:Content>

