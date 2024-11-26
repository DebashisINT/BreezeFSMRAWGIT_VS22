<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                09-02-2023        2.0.39           Pallab              25656 : Master module design modification 
2.0                13-03-2023        2.0.39           Pallab              25731 : Check box design issue in User Master 
3.0                26-04-2023       V2.0.40           Sanchita            A checkbox required for performance module,check box name is Show Employee Performance.
                                                                          Refer: 25911
4.0                08-05-2023       V2.0.40           Sanchita            In user table a column exist as IsShowBeatInMenu. 
                                                                          This will show in portal under user settings as"ShowBeatInMenu".
                                                                          Refer: 25947
5.0                05-06-2023       V2.0.41           Sanchita            Master - Organization - Users - Check box caption correction 
                                                                          Refer: 26289  
6.0                07-06-2023       V2.0.41           Sanchita            Required below System settings + user wise settings in portal
                                                                          Refer: 26245  
7.0                31-08-2023       V2.0.43           Sanchita            User wise settings required in Web Portal Front end User Master
                                                                          Show menu for AI Market Assistant
                                                                          USB Debugging Restricted  
                                                                          Refer: 26768
8.0                06-09-2023       V2.0.43           Sanchita            A new user wise settings required named as ShowLatLongInOutletMaster
                                                                          Mantis: 26794
9.0                19-12-2023       V2.0.44           Sanchita            Call log facility is required in the FSM App - IsCallLogHistoryActivated” - 
                                                                          User Account - Add User master settings. Mantis: 27063
10.0                16-04-2024       V2.0.47           Sanchita            0027369: The mentioned settings are required in the User master in FSM
11.0                17-04-2024       V2.0.47           Priti               0027372: ShowPartyWithCreateOrder setting shall be available User wise setting also
                                                                          0027374: ShowPartyWithGeoFence setting shall be available User wise setting also
12.0                16-04-2024       V2.0.47           Sanchita            0027369: The mentioned settings are required in the User master in FSM
13.0                21-05-2024       V2.0.47           Pallab             0027479: Add user settings page checkbox design modification
14.0                22-05-2024       V2.0.47           Priti               0027467: Some changes are required in CRM Modules
15.0                25-05-2024      V2.0.47            Sanchita           New User wise settings required. Mantis: 27474, 27477 
16.0                25-05-2024      V2.0.47            Sanchita           New User wise settings required. Mantis: 27502 
17.0                03-06-2024      V2.0.47            Sanchita           Some global settings are required for CRM Opportunity module. Mantis: 27481   
18.0                18-06-2024      V2.0.47            Sanchita           27436: Please create a global settings IsShowDateWiseOrderInApp   
19.0                04-07-2024      V2.0.48            Sanchita           27575: Two new global and user settings are required as 'IsUserWiseLMSEnable' and 'IsUserWiseLMSFeatureOnly'   
20.0                29-08-2024      V2.0.48            Sanchita           27648: Global and User wise settings isRecordAudioEnableForVisitRevisit shall be available 
                                                                          in both System settings page and in User master. 
21.0                03-09-2024      V2.0.48            Priti              	0027684: Create a new user setting as ShowClearQuiz
22.0                24-09-2024      V2.0.49            Sanchita           0027705: A new Global and user wise settings required as IsAllowProductCurrentStockUpdateFromApp  
23.0                26-11-2024      V2.0.49            Sanchita           0027793: A new user settings required as ShowTargetOnApp
====================================================== Revision History ================================================================--%>

<%@ Page Title="Users" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master" EnableEventValidation="false"
    Inherits="ERP.OMS.Management.Master.management_master_RootUserDetails" CodeBehind="RootUserDetails.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Scripts/pluggins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../Scripts/pluggins/multiselect/bootstrap-multiselect.js"></script>

    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <script type="text/javascript">

        FieldName = 'test';
        /*Code  Added  By Priti on 20122016 to use jquery Choosen*/
        $(document).ready(function () {
            ListBind();
            ChangeSource();

            //For show hide settings Tanmoy
            // ShowSettings();
            //For show hide settings Tanmoy
        });


        $.ajaxSetup({
            type: 'POST',
            headers: { "cache-control": "no-cache" }
        });

        //For show hide settings Start Tanmoy
        function ShowSettings() {
            $.ajax({
                type: "POST",
                url: "RootUserDetails.aspx/UserWiseSetings",

                data: JSON.stringify({ reqStr: '' }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,

                success: function (msg) {
                    var list = msg.d;
                    var listItems = [];
                    if (list != null) {
                        if (list.length > 0) {
                            for (var i = 0; i < list.length; i++) {
                                var key = '';
                                var value = '';
                                key = list[i].split('|')[1];
                                value = list[i].split('|')[0];

                                if (key == "EnableLeaveonApproval") {
                                    if (value == "0") {

                                    }
                                    else {

                                    }
                                }
                                else if (key == "ActiveAutomaticRevisit") {
                                    if (value == "0") {

                                    }
                                    else {

                                    }
                                }
                                else if (key == "InputDayPlan") {
                                    if (value == "0") {

                                    }
                                    else {

                                    }
                                }
                                else if (key == "ActiveMoreDetailsMandatory") {
                                    if (value == "0") {

                                    }
                                    else {

                                    }
                                }
                                else if (key == "DisplayMoreDetailsWhileNewVisit") {
                                    if (value == "0") {

                                    }
                                    else {

                                    }
                                }
                                else if (key == "ShowMeetingsOption") {
                                    if (value == "0") {

                                    }
                                    else {

                                    }
                                }
                                else if (key == "ShowProductRateInApp") {
                                    if (value == "0") {

                                    }
                                    else {

                                    }
                                }
                            }
                        }
                        else {
                        }
                    }
                }
            });
        }
        //For show hide settings End Tanmoy

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
        function lstAssociatedEmployee() {

            $('#lstAssociatedEmployee').fadeIn();

        }
        function setvalue() {
            //Mantis Issue 25016
            if ($("#IsShowUserType").val() == "1") {
                if ($("#IsUserTypeMandatory").val() == "1") {
                    if ($("#ddlType").val() == "Select Type") {
                        jAlert("Please select type.", "Alert", function () {
                            $("#ddlType").focus();
                        });
                        return false;
                    }
                }
                else {
                    if ($("#ddlType").val() == "Select Type") {
                        $("#ddlType").val("0");
                    }
                }
            }
            else {
                $("#ddlType").val("0");
            }

            //End of Mantis Issue 25016
            if ($("#txtusername").val().trim() != "") {
                if ($("#txtuserid").val().trim() != "") {
                    if ($("#hdnentrymode").val() == "Add") {
                        // Mantis Issue 24723
                        var IdExist = 0;
                        $.ajax({
                            type: "POST",
                            url: "RootUserDetails.aspx/chkLoginIdExist",

                            data: JSON.stringify({ userLoginId: $("#txtuserid").val().trim(), action: 'ADD', userid: '' }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,

                            success: function (msg) {
                                IdExist = msg.d;
                            }
                        });

                        if (IdExist == 0) {
                            // End of Mantis Issue 24723
                            if ($("#txtpassword").val().trim() != "") {
                                if ($("#lstAssociatedEmployee").val() != null) {
                                    if ($("#ddlGroups").val() != "Select Group") {

                                        document.getElementById("txtReportTo_hidden").value = document.getElementById("lstAssociatedEmployee").value;

                                        var partyid = "";
                                        var Partys = $("#ddlPartyType").val();

                                        for (var i = 0; i < Partys.length; i++) {
                                            if (partyid == "") {
                                                partyid = Partys[i];
                                            }
                                            else {
                                                partyid += ',' + Partys[i];
                                            }
                                        }
                                        $("#hdnPartyType").val(partyid);

                                    }
                                    else {
                                        jAlert("Please select group.", "Alert", function () {
                                            $("#ddlGroups").focus();
                                        });
                                        return false;
                                    }
                                }
                                else {
                                    jAlert("Please select associated employee.", "Alert", function () {
                                        $("#lstAssociatedEmployee").focus();
                                    });
                                    return false;
                                }
                            }
                            else {
                                jAlert("Please enter password.", "Alert", function () {
                                    $("#txtpassword").focus();
                                });
                                return false;
                            }
                            // Mantis Issue 24723
                        }
                        else {
                            jAlert("Duplicate user id found ! Please Talk to Administrator.", "Alert", function () {
                                $("#txtuserid").focus();
                            });
                            return false;
                        }
                        // End of Mantis Issue 24723
                    }
                    else {
                        // Mantis Issue 24723
                        var IdExist = 0;
                        $.ajax({
                            type: "POST",
                            url: "RootUserDetails.aspx/chkLoginIdExist",

                            data: JSON.stringify({ userLoginId: $("#txtuserid").val().trim(), action: 'UPDATE', userid: $("#hdnentrymode").val() }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,

                            success: function (msg) {
                                IdExist = msg.d;
                            }
                        });

                        if (IdExist == 0) {
                            if ($("#lstAssociatedEmployee").val() != null) {
                                if ($("#ddlGroups").val() != "Select Group") {

                                    document.getElementById("txtReportTo_hidden").value = document.getElementById("lstAssociatedEmployee").value;

                                    var partyid = "";
                                    var Partys = $("#ddlPartyType").val();

                                    for (var i = 0; i < Partys.length; i++) {
                                        if (partyid == "") {
                                            partyid = Partys[i];
                                        }
                                        else {
                                            partyid += ',' + Partys[i];
                                        }
                                    }
                                    $("#hdnPartyType").val(partyid);

                                }
                                else {
                                    jAlert("Please select group.", "Alert", function () {
                                        $("#ddlGroups").focus();
                                    });
                                    return false;
                                }
                            }
                            else {
                                jAlert("Please select associated employee.", "Alert", function () {
                                    $("#lstAssociatedEmployee").focus();
                                });
                                return false;
                            }
                            // Mantis Issue 24723
                        }
                        else {
                            jAlert("Duplicate user id found ! Please Talk to Administrator.", "Alert", function () {
                                $("#txtuserid").focus();
                            });
                            return false;
                        }
                        // End of Mantis Issue 24723
                    }
                }
                else {
                    jAlert("Please enter login id (Mobile No).", "Alert", function () {
                        $("#txtuserid").focus();
                    });
                    return false;
                }
            }
            else {
                jAlert("Please enter user name.", "Alert", function () {
                    $("#txtusername").focus();
                });
                return false;
            }

        }


        function Changeselectedvalue() {
            var lstAssociatedEmployee = document.getElementById("lstAssociatedEmployee");
            ///alert(lstAssociatedEmployee);

            if (document.getElementById("txtReportTo_hidden").value != '') {
                for (var i = 0; i < lstAssociatedEmployee.options.length; i++) {
                    if (lstAssociatedEmployee.options[i].value == document.getElementById("txtReportTo_hidden").value) {
                        lstAssociatedEmployee.options[i].selected = true;
                    }
                }
                $('#lstAssociatedEmployee').trigger("chosen:updated");
            }

        }
        function ChangeSource() {
            var fname = "%";
            var lAssociatedEmployee = $('select[id$=lstAssociatedEmployee]');
            lAssociatedEmployee.empty();


            $.ajax({
                type: "POST",
                url: "RootUserDetails.aspx/ALLEmployee",

                data: JSON.stringify({ reqStr: fname }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,

                success: function (msg) {
                    var list = msg.d;
                    var listItems = [];
                    if (list != null) {
                        if (list.length > 0) {
                            for (var i = 0; i < list.length; i++) {
                                var id = '';
                                var name = '';
                                id = list[i].split('|')[1];
                                name = list[i].split('|')[0];

                                //   alert(name);

                                $('#lstAssociatedEmployee').append($('<option>').text(name).val(id));


                            }

                            $(lAssociatedEmployee).append(listItems.join(''));
                            lstAssociatedEmployee();
                            $('#lstAssociatedEmployee').trigger("chosen:updated");

                            Changeselectedvalue();

                        }
                        else {
                            //   alert("No records found");
                            //lstReferedBy();
                            $('#lstAssociatedEmployee').trigger("chosen:updated");

                        }
                    }
                }
            });
            // }
        }
        //....end......
        function CallList(obj1, obj2, obj3) {
            //var obj5 = '';
            //if (obj5 != '18') {
            //    ajax_showOptions(obj1, obj2, obj3, obj5);
            //}
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
        function Close() {
            parent.editwin.close();
        }
        function Cancel_Click() {
            //parent.editwin.close();
            location.href = '/OMS/Management/Master/root_user.aspx';
        }
    </script>

    <script type="text/javascript" language="javascript">
        // WRITE THE VALIDATION SCRIPT IN THE HEAD TAG.
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
        $(function () {
            $('#ddlPartyType').multiselect({
                numberDisplayed: 2
            });

            var partyids = $("#hdnPartyType").val();
            var str_array = partyids.split(',');
            for (var i = 0; i < str_array.length; i++) {
                $("#ddlPartyType").multiselect('select', str_array[i]);
            }
        });
        //Mantis Issue 24408,24364
        function EnableDisableGPSAlertSound() {
            //alert(chkGPSAlert.GetChecked())
            if (chkGPSAlert.GetChecked() == true) {
                //alert(chkGPSAlert.GetChecked())
                //chkGPSAlertwithSound.SetChecked(true)
                //TdIsGPSAlertwithSound.SetVisible(true)
                document.getElementById("TdIsGPSAlertwithSound").style.visibility = "visible";
                document.getElementById("TdIsGPSAlertwithSound").style.display = "table-cell";
            }
            else {
                //alert(chkGPSAlert.GetChecked())
                chkGPSAlertwithSound.SetChecked(false)
                document.getElementById('TdIsGPSAlertwithSound').style.visibility = 'hidden';
                document.getElementById("TdIsGPSAlertwithSound").style.display = "block";
            }
        }
        //End of Mantis Issue 24408,24364
    </script>

    <%--<script>
        $(document).ready(function () {
            $("#searchtextButton").click(function () {
                var searchText = $("#searchtextInput").val().toLowerCase();

                
                $(".panel-collapse").collapse('hide');

                
                $(".panel-title a").each(function () {
                    var panelTitle = $(this).text().toLowerCase();
                    var $panel = $(this).closest('.panel');
                    if (panelTitle.includes(searchText)) {
                        $panel.find('.panel-collapse').collapse('show'); 
                        $(this).html($(this).text().replace(new RegExp(searchText, "gi"), function (matched) {
                            return "<span class='highlight'>" + matched + "</span>"; 
                        }));
                    }

                   
                    $panel.find("input[type='checkbox']").each(function () {
                        var checkboxText = $(this).parent().next().text().toLowerCase();
                        if (checkboxText.includes(searchText)) {
                            $(this).closest('tr').find("label").html($(this).closest('tr').find("label").text().replace(new RegExp(searchText, "gi"), function (matched) {
                                return "<span class='highlight'>" + matched + "</span>"; 
                            }));
                        }
                    });
                });
            });
        });
    </script>--%>


    <style>
        .reltv {
            position: relative;
        }

        .spl {
            position: absolute;
            right: -17px;
            top: 5px;
        }

        .inb {
            display: inline-block !important;
            width: 62px !important;
        }
        /*Code  Added  By Priti on 20122016 to use jquery Choosen*/
        .chosen-container.chosen-container-single {
            width: 100% !important;
        }

        .chosen-choices {
            width: 100% !important;
        }

        #lstAssociatedEmployee {
            width: 100%;
            display: none !important;
        }

        #display:none !important;_chosen {
            width: 100% !important;
        }

        .dxtcLite_PlasticBlue > .dxtc-content {
            overflow: visible !important;
        }

        .tblWhiteS > tbody > tr > td {
            vertical-align: top;
            padding: 0 5px 10px;
        }

            .tblWhiteS > tbody > tr > td > table > tbody > tr > td:first-child {
                vertical-align: top;
                padding-right: 4px;
            }

        select.sml {
            padding: 0px 12px !important;
            height: 34px !important;
        }

        .sml .chosen-container-single .chosen-single {
            /*Rev 2.0*/
            /*height: 30px !important;*/
            height: 34px !important;
            /*Rev end 2.0*/
        }

            .sml .chosen-container-single .chosen-single div b,
            .sml .chosen-container-single .chosen-single > span {
                margin-top: 2px !important;
            }

        .segHeader {
            background: #bedcfb;
            padding: 5px 15px;
            border-radius: 3px;
        }

        .backBox label {
            font-weight: normal;
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
        /*...end....*/

        .fullMulti .multiselect-native-select .multiselect {
            border: 1px solid #ccc;
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
                right: 8px;
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

        .styled-checkbox input {
            position: absolute;
            opacity: 0;
            z-index: 1;
        }

            .styled-checkbox input + label {
                position: relative;
                /*cursor: pointer;*/
                padding: 0;
                margin-bottom: 0 !important;
            }

                .styled-checkbox input + label:before {
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

            .styled-checkbox input:hover + label:before {
                background: #094e8c;
            }


            .styled-checkbox input:checked + label:before {
                background: #094e8c;
            }

            .styled-checkbox input:disabled + label {
                color: #b8b8b8;
                cursor: auto;
            }

                .styled-checkbox input:disabled + label:before {
                    box-shadow: none;
                    background: #ddd;
                }

            .styled-checkbox input:checked + label:after {
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

        #dtFrom_B-1, #dtTo_B-1, #cmbDOJ_B-1, #cmbLeaveEff_B-1 {
            background: transparent !important;
            border: none;
            width: 30px;
            padding: 10px !important;
        }

            #dtFrom_B-1 #dtFrom_B-1Img,
            #dtTo_B-1 #dtTo_B-1Img, #cmbDOJ_B-1 #cmbDOJ_B-1Img, #cmbLeaveEff_B-1 #cmbLeaveEff_B-1Img {
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
            padding: 6px 10px !important;
        }

        .btn {
            height: 34px;
        }

        /*Rev end 1.0*/

        /*Rev 2.0*/
        .col-md-3 {
            padding-left: 10px !important;
            padding-right: 10px !important;
        }

        .dxeTextBox_PlasticBlue.form-control {
            padding: 2px 12px !important;
        }
        /*Rev end 2.0*/

        .dxWeb_edtCheckBoxUnchecked_PlasticBlue {
            background-image: none !important;
            border: 1px solid #236cb9 !important;
            border-radius: 2px;
        }

        .tblWhiteS > tbody > tr > td {
            font-size: 13px;
        }

        /*Rev 13.0*/

        #divDashboardHeaderList .panel-title {
            position: relative;
        }

            #divDashboardHeaderList .panel-title > a:focus {
                text-decoration: none;
            }

            #divDashboardHeaderList .panel-title > a:after {
                content: '\f056';
                font-family: FontAwesome;
                font-size: 18px;
                position: absolute;
                right: 10px;
                top: 1px;
                color: #094e8c;
            }

            #divDashboardHeaderList .panel-title > a + input[type="checkbox"] {
                -webkit-transform: translateY(2px);
                -moz-transform: translateY(2px);
                transform: translateY(2px);
            }

            #divDashboardHeaderList .panel-title > a.collapsed:after {
                content: '\f055';
            }

        .highlight {
            background-color: yellow;
            font-weight: bold;
        }

        .footer-btns {
            position: fixed;
            bottom: 0;
            left: 0;
            width: 100%;
            background-color: #ffffff;
            padding: 10px;
            text-align: center;
            box-shadow: 0 -3px 18px #11111124;
        }
        /*Rev end 13.0*/
        /* Style to make the footer sticky when page size increases beyond screen view size */
        @media (max-height: 70vh) {
            .footer-btns {
                position: static;
            }
        }

        @media only screen and (max-width: 768px) {
            .tblWhiteS > tbody > tr > td {
                display: block !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadCumb">

        <span>Add/Edit User </span>

        <div class="crossBtnN"><a href="root_user.aspx"><i class="fa fa-times"></i></a></div>
    </div>
    <div class="container">
        <div class="backBox mt-5">
            <%--<asp:Button id="brnChangeUsersPassword" runat="server" Text="Change Password" OnClick="lnkChangePassword_Click" CssClass="btn btn-primary" />--%>

            <div class="p-4">
                <div class="col-md-3">
                    <label>User Name&nbsp;<em style="color: red">*</em> :</label>
                    <div class="reltv">
                        <asp:TextBox ID='txtusername' runat="server" CssClass="form-control" Width="100%" ValidationGroup="a" MaxLength="50"></asp:TextBox>
                        <%-- <asp:RequiredFieldValidator ID="rfvuserName" runat="server" ControlToValidate="txtusername" CssClass="spl fa fa-exclamation-circle iconRed"
                            ErrorMessage="" ValidationGroup="a" ToolTip="Mandatory."></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                <div class="col-md-3">
                    <label>Login Id (Mobile No) &nbsp;<em style="color: red">*</em> :</label>
                    <div class="reltv">
                        <asp:TextBox ID='txtuserid' runat="server" Width="100%" CssClass="form-control" ValidationGroup="a" value=" " MaxLength="50" autocomplete="off"></asp:TextBox>
                        <%--   <asp:RequiredFieldValidator ID="rfvLiginId" runat="server" ControlToValidate="txtuserid" CssClass="spl fa fa-exclamation-circle iconRed"
                            ErrorMessage="" ToolTip="Mandatory." ValidationGroup="a"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                <div class="col-md-3" id="user_password" runat="server" visible="true">
                    <label>Password&nbsp;<em style="color: red">*</em> :</label>
                    <div class="reltv">
                        <asp:TextBox ID='txtpassword' runat="server" Width="100%" TextMode="Password" CssClass="form-control" ValidationGroup="a" MaxLength="50"></asp:TextBox>
                        <%--  <asp:RequiredFieldValidator ID="rfvPass" runat="server" ControlToValidate="txtpassword" CssClass="spl fa fa-exclamation-circle iconRed"
                            ErrorMessage="" ToolTip="Mandatory." ValidationGroup="a"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div></div>
                </div>
                <div class="col-md-3">
                    <label>Associated Employee <em style="color: red">*</em> :</label>
                    <div class="reltv sml">
                        <%--  Code  Added and Commented By Priti on 20122016 to use ListBox instead of TextBox--%>
                        <%--  <asp:TextBox ID="txtReportTo" runat="server" Width="200px" autocomplete="off"></asp:TextBox>--%>
                        <asp:ListBox ID="lstAssociatedEmployee" CssClass="chsn" runat="server" Width="100%" data-placeholder="Select..."></asp:ListBox>
                        <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lstAssociatedEmployee" CssClass="spl fa fa-exclamation-circle iconRed"
                            ErrorMessage="" ToolTip="Mandatory." ValidationGroup="a"></asp:RequiredFieldValidator>--%>

                        <asp:HiddenField ID="txtReportTo_hidden" runat="server" />
                    </div>
                </div>
                <div class="clear"></div>
                <div class="col-md-3 mt-3">
                    <label>GPS Accuracy  :</label>
                    <div class="reltv">
                        <asp:TextBox ID="txtgps" runat="server" Text="50" Width="100%" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <asp:HiddenField ID="hdnentrymode" runat="server" />


                <div class="col-md-3" style="display: none">
                    <label>Data Entry Profile :</label>
                    <div>
                        <asp:DropDownList ID="ddDataEntry" runat="server" Width="100%">
                            <asp:ListItem Value="F">Final</asp:ListItem>
                            <asp:ListItem Value="P">Provisional</asp:ListItem>
                            <asp:ListItem Value="M">Maker</asp:ListItem>
                            <asp:ListItem Value="C">Checker</asp:ListItem>
                            <asp:ListItem Value="R">Read Only</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3" style="display: none;">
                    <label>
                        Allow Access From(IP) :
                    </label>
                    <div>
                        <asp:TextBox ID="txtIp1" runat="server" CssClass="inb" MaxLength="3" onkeypress="javascript:return isNumber (event)"></asp:TextBox><font>.</font>
                        <asp:TextBox ID="txtIp2" runat="server" CssClass="inb" MaxLength="3" onkeypress="javascript:return isNumber (event)"></asp:TextBox><font>.</font>
                        <asp:TextBox ID="txtIp3" runat="server" CssClass="inb" MaxLength="3" onkeypress="javascript:return isNumber (event)"></asp:TextBox><font>.</font>
                        <asp:TextBox ID="txtIp4" runat="server" CssClass="inb" MaxLength="3" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3" style="display: none;">
                    <label>&nbsp</label>
                    <div>
                        <dxe:ASPxCheckBox ID="cbSuperUser" runat="server" Text="Super User">
                        </dxe:ASPxCheckBox>
                    </div>
                </div>
                <div class="col-md-3" style="display: none">

                    <asp:GridView ID="grdUserAccess" runat="Server" Visible="false" AutoGenerateColumns="False" BorderColor="CornflowerBlue"
                        BackColor="White" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" Width="100%"
                        OnRowDataBound="grdUserAccess_RowDataBound">
                        <RowStyle BackColor="LightSteelBlue" ForeColor="#330099"></RowStyle>
                        <SelectedRowStyle BackColor="LightSteelBlue" ForeColor="SlateBlue" Font-Bold="True"></SelectedRowStyle>
                        <PagerStyle BackColor="LightSteelBlue" ForeColor="SlateBlue" HorizontalAlign="Center"></PagerStyle>
                        <HeaderStyle BackColor="LightSteelBlue" ForeColor="Black" Font-Bold="True"></HeaderStyle>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSegmentId" runat="server" />
                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id")%>' Visible="false"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                                <HeaderStyle />
                            </asp:TemplateField>
                            <asp:BoundField DataField="SegmentName" HeaderText="Segment">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="User Group">
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle />
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpUserGroup" runat="server" AppendDataBoundItems="True" Width="250px">
                                        <asp:ListItem Value="0">None</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:DropDownList ID='dropdownlistbranch' runat="Server" AppendDataBoundItems="True"
                        Visible="false">
                        <asp:ListItem Value="0">None</asp:ListItem>
                    </asp:DropDownList>

                </div>
                <%--Rev 1.0--%>
                <%--<div class="col-md-3 mt-3">--%>
                <div class="col-md-3 mt-3 h-branch-select">
                    <%--Rev end 1.0--%>
                    <label>Group<em style="color: red">*</em> :</label>
                    <div class="reltv">
                        <asp:DropDownList ID="ddlGroups" runat="server" CssClass="sml" Width="100%"></asp:DropDownList>
                        <%--    <asp:RequiredFieldValidator ID="rfGroup" runat="server" ControlToValidate="ddlGroups" InitialValue="Select Group"
                            ErrorMessage="" ToolTip="Mandatory." ValidationGroup="a" CssClass="spl fa fa-exclamation-circle iconRed"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                <%--Rev 1.0--%>
                <%--<div class="col-md-3 mt-3" runat="server" id="DivisHomeRestrictAttendance">--%>
                <div class="col-md-3 mt-3 h-branch-select" runat="server" id="DivisHomeRestrictAttendance">
                    <%--Rev end 1.0--%>
                    <%--Rev 2.0 : label change--%>
                    <%--<label title="Restriction Attendance from Home Location">Restriction Attendance from Home Location:</label>--%>
                    <label title="Restriction Attendance from Home Location">Restrict Attend. from Home Location:</label>
                    <%--Rev end 2.0--%>
                    <div class="reltv">
                        <asp:DropDownList ID="ddlRestrictionHomeLocation" runat="server" CssClass="sml" Width="100%">
                            <asp:ListItem Value="0">Home Restriction</asp:ListItem>
                            <asp:ListItem Value="1">No Restriction</asp:ListItem>
                            <asp:ListItem Value="2">Fix Location</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <%--  <dxe:ASPxCheckBox ID="chkisHomeRestrictAttendance" runat="server" Text="">
                    </dxe:ASPxCheckBox>--%>
                </div>

                <div class="clear"></div>
                <div class="col-md-3 mt-3" id="DTdshopLocAccuracy" runat="server">

                    <label>Shop Location Accuracy :</label>
                    <div class="reltv hideScndTd">
                        <%--<asp:TextBox ID="txtshopLocAccuracy" CssClass="form-control" runat="server" Text="50" Width="100%" MaxLength="10"></asp:TextBox>--%>
                        <dxe:ASPxTextBox ID="txtshopLocAccuracy" Text="100" ClientInstanceName="ctxtshopLocAccuracy" MaxLength="8" runat="server" Width="100%" CssClass="form-control">
                            <MaskSettings Mask="<0..99999>.<0..99>" />
                        </dxe:ASPxTextBox>
                    </div>

                </div>
                <div class="col-md-3 mt-3" id="DTdhomeLocDistance" runat="server">
                    <label>Home Location Distance :</label>
                    <div class="reltv hideScndTd">
                        <%--<asp:TextBox ID="txthomeLocDistance" CssClass="form-control" runat="server" Text="50" Width="100%" MaxLength="10"></asp:TextBox>--%>
                        <dxe:ASPxTextBox ID="txthomeLocDistance" Text="100" ClientInstanceName="ctxthomeLocDistance" MaxLength="8" runat="server" Width="100%" CssClass="form-control">
                            <MaskSettings Mask="<0..99999>.<0..99>" />
                        </dxe:ASPxTextBox>
                    </div>
                </div>
                <%--Rev 1.0--%>
                <%--<div class="col-md-3 mt-3" runat="server" id="Div1">--%>
                <div class="col-md-3 mt-3 h-branch-select" runat="server" id="Div1">
                    <%--Rev end 1.0--%>
                    <label title="Shop Type">Party Type :</label>
                    <div class="fullMulti">
                        <asp:DropDownList ID="ddlPartyType" runat="server" class="demo" multiple="multiple" Width="100%">
                            <%--  <asp:ListItem Value="0">Home Restriction</asp:ListItem>
                            <asp:ListItem Value="1">No Restriction</asp:ListItem>
                            <asp:ListItem Value="2">Fix Location</asp:ListItem>--%>
                        </asp:DropDownList>
                        <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPartyType" InitialValue="Select Party Type"
                            ErrorMessage="" ToolTip="Mandatory." ValidationGroup="a" CssClass="spl fa fa-exclamation-circle iconRed"></asp:RequiredFieldValidator>--%>
                        <asp:HiddenField ID="hdnPartyType" runat="server" />
                    </div>
                </div>
                <%--Mantis Issue 25015--%>
                <%--Rev 1.0--%>
                <%--<div class="col-md-3 mt-3" id="divType" runat="server">--%>
                <div class="col-md-3 mt-3 h-branch-select" id="divType" runat="server">
                    <%--Rev end 1.0--%>
                    <label>Type<em style="color: red">*</em> :</label>
                    <div class="reltv">
                        <asp:DropDownList ID="ddlType" runat="server" CssClass="sml" Width="100%"></asp:DropDownList>
                        <asp:HiddenField ID="IsShowUserType" runat="server" />
                        <asp:HiddenField ID="IsUserTypeMandatory" runat="server" />
                    </div>
                </div>
                <%--End of Mantis Issue 25015--%>
                <div class="clear"></div>
                <div class="col-md-3 mt-3" id="DivDeviceInfoMin" runat="server">
                    <label>Device information capture every after : </label>
                    <div class="reltv relative hideScndTd">
                        <dxe:ASPxTextBox ID="txtDeviceInfoMin" Text="0" ClientInstanceName="ctxtDeviceInfoMin" MaxLength="2" runat="server" Width="100%" CssClass="form-control">
                            <MaskSettings Mask="<0..99>" />
                        </dxe:ASPxTextBox>
                        <span style="position: absolute; right: 3px; top: 3px; background: #e9e9e9; padding: 4px 6px; border-radius: 3px;">mins</span>

                    </div>
                </div>


                <div class="clear"></div>
                <div class="col-md-12 mt-4"></div>

                <%--Rev Pallab--%>
                <%--<div class="col-md-12 mt-4">
                    <div class="segHeader col-md-12">General</div>
                    <div class="row">
                        <div class="col-md-3">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Text="">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                    <td>Tick to Make Inactive</td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-3" style="display: none">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxCheckBox ID="ASPxCheckBox2" runat="server" Text="">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                    <td>Is Mac Lock?</td>
                                </tr>
                            </table>
                        </div>

                        <div class="col-md-3"  id="TdHierarchywiseTargetSettings" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxCheckBox ID="ASPxCheckBox3" runat="server" Text="">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                    <td>Tick to active Hierarchy-wise Target Settings</td>
                                </tr>
                            </table>
                        </div>

                    </div>
                </div>--%>
                <%--Rev end Pallab--%>


                <%--rev start 13.0: add all checkboxes in accordion panel and button floating--%>

                <div class="" id="divDashboardHeaderList" style="padding: 15px;">



                    <%--<div class="row">
                        <div class="col-lg-5">
                            <div class="input-group">
                                <input id="searchtextInput" type="text" class="form-control" placeholder="Search for..."/>
                                <span class="input-group-btn">
                                    <button id="searchtextButton" class="btn btn-default" type="button">Go!</button>
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class="clear"></div>--%>

                    <div class="panel panel-default clsListheader">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#divDashboardHeaderList" class="" href="#collapse_3" aria-expanded="true">General</a>

                            </h4>
                        </div>
                        <div id="collapse_3" class="panel-collapse collapse in" aria-expanded="true" style="">
                            <div class="panel-body SelectSubList">
                                <table class="tblWhiteS">

                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsActive" runat="server" Text="" CssClass="C-checkbox">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Tick to Make Inactive</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="display: none">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkmac" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Mac Lock?</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdHierarchywiseTargetSettings" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkTargetSettings" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Tick to active Hierarchy-wise Target Settings</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="DivEnableLeaveonApproval" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkLeaveEanbleSettings" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Enable Leave on Approval</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="DivEnableLeaveonApprover" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkLeaveApprover" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Tick to add as Approver</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="DivActiveautomaticRevisit" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkRevisitSettings" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Active automatic Revisit</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="DivInputDayPlan" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkPlanDetailsSettings" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Input Day Plan</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="DivActiveMoreDetailsMandatory" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkMoreDetailsMandatorySettings" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Active More Details Mandatory</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="DivDisplayMoreDetailswhilenewVisit" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowMoreDetailsSettings" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Display More Details while new Visit</td>
                                                </tr>
                                            </table>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td id="DivShowMeetingsOption" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkMeetingsSettings" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Meetings Option</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--Rev 6.0--%>
                                        <%--<td id="DivShowProductRateinApp" runat="server">--%>
                                        <td id="DivRateEditableSettings" runat="server">
                                            <%--End of Rev 6.0--%>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkRateEditableSettings" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <%--Rev 6.0--%>
                                                    <%--<td>Show Product Rate in App</td>--%>
                                                    <td>Order Rate Not Editable</td>
                                                    <%--End of Rev 6.0--%>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="DivShowTeam" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowTeam" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Team</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="DivAllowPJPupdateforTeam" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkAllowPJPupdateforTeam" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Allow PJP update for Team</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="DivwillReportShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkwillReportShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Report</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="DivIsFingerPrintMandatoryForAttendance" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsFingerPrintMandatoryForAttendance" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Attendance - Fingerprint Mandatory</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="DivIsFingerPrintMandatoryForVisit" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsFingerPrintMandatoryForVisit" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Visits - Fingerprint Mandatory</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="DivIsSelfieMandatoryForAttendance" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsSelfieMandatoryForAttendance" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Attendance - Selfie Mandatory</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                    </div>

                    <div class="panel panel-default clsListheader">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#divDashboardHeaderList" class="collapsed" href="#collapse_4">Report</a>

                            </h4>
                        </div>
                        <div id="collapse_4" class="panel-collapse collapse" aria-expanded="true" style="">
                            <div class="panel-body SelectSubList">
                                <table class="tblWhiteS">
                                    <tr>
                                        <td id="TdisAttendanceReportShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisAttendanceReportShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Attendance Report</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisPerformanceReportShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisPerformanceReportShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Performance Report</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisVisitReportShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisVisitReportShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Visit Report</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdwillTimesheetShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkwillTimesheetShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Timesheet</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <%--Rev work start 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting--%>
                                    <tr>
                                        <td id="TdHorizontalPerformanceReportShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisHorizontalReportShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Hierarchy for Horizontal Performance Report</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <%--Rev work close 11.05.2022 Mantise No:0024880 Horizontal Performance Summary & detail report required with hierarchy setting--%>
                                </table>
                            </div>
                        </div>

                    </div>


                    <div class="panel panel-default clsListheader">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#divDashboardHeaderList" class="collapsed" href="#collapse_5">Configuration</a>

                            </h4>
                        </div>
                        <div id="collapse_5" class="panel-collapse collapse" aria-expanded="true" style="">
                            <div class="panel-body SelectSubList">
                                <table class="tblWhiteS">

                                    <tr>
                                        <td id="TdisAttendanceFeatureOnly" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisAttendanceFeatureOnly" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Apply Attendance Feature Only </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisOrderShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisOrderShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Use Order feature in APP</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisVisitShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisVisitShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Visit Show</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdiscollectioninMenuShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkiscollectioninMenuShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Collection feature in APP</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="TdisShopAddEditAvailable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShopAddEditAvailable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Can Add Party?</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisEntityCodeVisible" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisEntityCodeVisible" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Party Code</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisAreaMandatoryInPartyCreation" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisAreaMandatoryInPartyCreation" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is area mandatory in Add Party?</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisShowPartyInAreaWiseTeam" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowPartyInAreaWiseTeam" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Team details Party listing Area wise.</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisChangePasswordAllowed" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisChangePasswordAllowed" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Change Password</td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdisQuotationShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisQuotationShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Quotation Show</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsStateMandatoryinReport" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsStateMandatoryinReport" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>State mandatory in Report</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisQuotationPopupShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisQuotationPopupShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Quotation show in popup</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>

                                        <td id="TdisAchievementEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisAchievementEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Achievement Report</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisTarVsAchvEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisTarVsAchvEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Target Vs Achievement Enable Report</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisOrderReplacedWithTeam" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisOrderReplacedWithTeam" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Order Replaced With Team</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisMultipleAttendanceSelection" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisMultipleAttendanceSelection" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Multiple Attendance Selection</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisOfflineTeam" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisOfflineTeam" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show offline Team</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisDDShowForMeeting" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisDDShowForMeeting" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>DD show for Meeting</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisDDMandatoryForMeeting" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisDDMandatoryForMeeting" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>DD mandatory for Meeting</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisAllTeamAvailable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisAllTeamAvailable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>All Team Available</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisRecordAudioEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisRecordAudioEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Record audio enable </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisNextVisitDateMandatory" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisNextVisitDateMandatory" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Next visit date mandatory</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisShowCurrentLocNotifiaction" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowCurrentLocNotifiaction" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show current location notification</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisUpdateWorkTypeEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisUpdateWorkTypeEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Update work type enable</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisLeaveEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisLeaveEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Leave enable </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisOrderMailVisible" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisOrderMailVisible" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Order mail visible</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdLateVisitSMS" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkLateVisitSMS" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Late visit SMS</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisShopEditEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShopEditEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Shop edit enable</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisTaskEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisTaskEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Task enable </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisAppInfoEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisAppInfoEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Device Info Saved </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdwillDynamicShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkwillDynamicShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Dynamic form creation </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdwillActivityShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkwillActivityShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Activities feature</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisDocumentRepoShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisDocumentRepoShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Document Report Show </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisChatBotShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisChatBotShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Chat Bot Show </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisAttendanceBotShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisAttendanceBotShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Attendance Bot Show </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisVisitBotShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisVisitBotShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Visit Bot Show </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisInstrumentCompulsory" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisInstrumentCompulsory" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Instrument Compulsory </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisBankCompulsory" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisBankCompulsory" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Bank Compulsory </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdisComplementaryUser" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisComplementaryUser" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Complementary User </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisVisitPlanShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisVisitPlanShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Visit Plan Show </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisVisitPlanMandatory" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisVisitPlanMandatory" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Visit Plan Mandatory </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisAttendanceDistanceShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisAttendanceDistanceShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Attendance Distance Show </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdwillTimelineWithFixedLocationShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkwillTimelineWithFixedLocationShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Timeline With Fixed Location Show </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisShowOrderRemarks" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowOrderRemarks" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Order Remarks </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisShowOrderSignature" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowOrderSignature" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Order Signature </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisShowSmsForParty" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowSmsForParty" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Sms For Party </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdisShowTimeline" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowTimeline" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Timeline </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdwillScanVisitingCard" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkwillScanVisitingCard" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Scan Visiting Card </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisCreateQrCode" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisCreateQrCode" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Create QR Code </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisScanQrForRevisit" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisScanQrForRevisit" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Scan QR For Revisit </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdisShowLogoutReason" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowLogoutReason" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Logout Reason </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdwillShowHomeLocReason" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkwillShowHomeLocReason" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Home Location Reason </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdwillShowShopVisitReason" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkwillShowShopVisitReason" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Shop Visit Reason </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdwillShowPartyStatus" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkwillShowPartyStatus" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Party Status </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdwillShowEntityTypeforShop" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkwillShowEntityTypeforShop" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Entity Type for Shop </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisShowRetailerEntity" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowRetailerEntity" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Retailer Entity </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisShowDealerForDD" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowDealerForDD" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Dealer For DD </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdisShowBeatGroup" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowBeatGroup" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Beat Group </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisShowShopBeatWise" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowShopBeatWise" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Shop Beat Wise </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisShowOTPVerificationPopup" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowOTPVerificationPopup" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show OTP Verification Popup </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisShowMicroLearing" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowMicroLearing" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Micro Learing </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdisMultipleVisitEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisMultipleVisitEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Multiple Visit Enable </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisShowVisitRemarks" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowVisitRemarks" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Visit Remarks </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisShowNearbyCustomer" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShowNearbyCustomer" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Near by Customer </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisServiceFeatureEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisServiceFeatureEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Service Feature Enable </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdisPatientDetailsShowInOrder" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisPatientDetailsShowInOrder" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Patient Details Show In Order </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisPatientDetailsShowInCollection" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisPatientDetailsShowInCollection" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Patient Details Show In Collection </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdisAttachmentMandatory" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisAttachmentMandatory" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Attachment Mandatory </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdisShopImageMandatory" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisShopImageMandatory" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Shop Image Mandatory </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdisLogShareinLogin" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisLogShareinLogin" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Log Share in Login </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsCompetitorenable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsCompetitorenable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Competitor Enable </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdIsOrderStatusRequired" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsOrderStatusRequired" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Order Status Required </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsCurrentStockEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsCurrentStockEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Current Stock Enable </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdIsCurrentStockApplicableforAll" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsCurrentStockApplicableforAll" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Current Stock Applicable for All </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIscompetitorStockRequired" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIscompetitorStockRequired" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Competitor Stock Required </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdIsCompetitorStockforParty" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsCompetitorStockforParty" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Competitor Stock for Party </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdShowFaceRegInMenu" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowFaceRegInMenu" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Face Registration menu option in APP </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdIsFaceDetection" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsFaceDetection" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Face Detection While Attendance </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--  <td id="TdisFaceRegistered" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxCheckBox ID="chkisFaceRegistered" runat="server" Text="">
                                        </dxe:ASPxCheckBox>
                                    </td>
                                    <td>Face Registered </td>
                                </tr>
                            </table>
                        </td>--%>
                                    </tr>

                                    <tr>
                                        <td id="TdIsUserwiseDistributer" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsUserwiseDistributer" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Userwise Distributer(DD) </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsPhotoDeleteShow" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsPhotoDeleteShow" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Face Detection - Photo Delete pemission </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdIsAllDataInPortalwithHeirarchy" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsAllDataInPortalwithHeirarchy" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Heirarchy wise data (DMS Feature On) </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsFaceDetectionWithCaptcha" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsFaceDetectionWithCaptcha" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Face Detection With Captcha </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdIsShowMenuAddAttendance" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuAddAttendance" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Add Attendance Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowMenuAttendance" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuAttendance" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>View Attendance Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdIsShowMenuShops" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuShops" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Customer Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowMenuOutstandingDetailsPPDD" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuOutstandingDetailsPPDD" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Outstanding Details PP/DD Menu Show in App  </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdIsShowMenuStockDetailsPPDD" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuStockDetailsPPDD" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Stock Details - PP/DD Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowMenuTA" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuTA" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>TA Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdIsShowMenuMISReport" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuMISReport" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>MIS Report Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowMenuReimbursement" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuReimbursement" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Reimbursement Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdIsShowMenuAchievement" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuAchievement" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Achievement Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowMenuMapView" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuMapView" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Map View Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdIsShowMenuShareLocation" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuShareLocation" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Share Location Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowMenuHomeLocation" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuHomeLocation" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Home Location Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="TdIsShowMenuWeatherDetails" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuWeatherDetails" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Weather Details Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowMenuChat" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuChat" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Chat Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td id="TdIsShowMenuScanQRCode" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuScanQRCode" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Scan QR Code Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowMenuPermissionInfo" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuPermissionInfo" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Permission Info Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdIsShowMenuAnyDesk" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuAnyDesk" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>AnyDesk Menu Show in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsDocRepoFromPortal" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsDocRepoFromPortal" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Document Repository from Organization in APP </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsDocRepShareDownloadAllowed" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsDocRepShareDownloadAllowed" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Allow Document Repository Share and Download   </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsScreenRecorderEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsScreenRecorderEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Screen Recorder Enable in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <%-- Add new Settings--%>
                                    <tr>
                                        <td id="TdIsShowPartyOnAppDashboard" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowPartyOnAppDashboard" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Party On App Dashboard </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowAttendanceOnAppDashboard" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowAttendanceOnAppDashboard" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Attendance On App Dashboard </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowTotalVisitsOnAppDashboard" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowTotalVisitsOnAppDashboard" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Total Visits On App Dashboard   </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowVisitDurationOnAppDashboard" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowVisitDurationOnAppDashboard" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Visit Duration On App Dashboard </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdIsShowDayStart" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowDayStart" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Day Start On App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsshowDayStartSelfie" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsshowDayStartSelfie" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Day Start Selfie </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowDayEnd" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowDayEnd" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Day End in App  </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsshowDayEndSelfie" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsshowDayEndSelfie" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Day End Selfie in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="TdIsShowLeaveInAttendance" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowLeaveInAttendance" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Leave In Attendance in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsLeaveGPSTrack" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsLeaveGPSTrack" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Leave GPS Track </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowActivitiesInTeam" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowActivitiesInTeam" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Activities In Team in App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsShowMarkDistVisitOnDshbrd" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMarkDistVisitOnDshbrd" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Mark Distributor Visit Show in App </td>
                                                </tr>
                                            </table>
                                        </td>


                                    </tr>
                                    <tr>
                                        <%--Mantis Issue 24408,24364--%>
                                        <td id="TdIsRevisitRemarksMandatory" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsRevisitRemarksMandatory" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Revisit Remarks Mandatory? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsGPSAlert" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkGPSAlert" runat="server" Text="">
                                                            <ClientSideEvents CheckedChanged="function (s, e) {EnableDisableGPSAlertSound();}" />
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is GPS Alert On? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdIsGPSAlertwithSound" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkGPSAlertwithSound" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is GPS Alert with Sound? </td>
                                                </tr>
                                            </table>
                                        </td>

                                    </tr>
                                    <tr>
                                        <%--Mantis Issue 24596,24597--%>
                                        <td id="TdFaceRegistrationFrontCamera" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkFaceRegistrationFrontCamera" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Face Registration - Open Front camera </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="TdMRPInOrder" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkMRPInOrder" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>MRP in Order </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Mantis Issue 24596,24597--%>
                                    </tr>
                                    <%--End of Mantis Issue 24408,24364--%>
                                    <%-- Add new Settings--%>
                                    <%--Mantis Issue 25035--%>
                                    <tr>
                                        <td id="TdDistributerwisePartyOrderReport" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkDistributerwisePartyOrderReport" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Distributer wise Party Order Report </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <%--End of Mantis Issue 25035--%>

                                    <%--Mantis Issue 25207--%>
                                    <tr>
                                        <%--Mantis Issue 25116--%>
                                        <td id="TdShowAttednaceClearmenu" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowAttednaceClearmenu" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Attendance Clear Menu </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Mantis Issue 25116--%>
                                        <td id="AllowProfileUpdate" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkAllowProfileUpdate" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Allow Profile Update </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="AutoDDSelect" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkAutoDDSelect" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Auto DDSelect </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="BatterySetting" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkBatterySetting" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Battery Setting </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="CommonAINotification" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkCommonAINotification" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Common AI Notification </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="Custom_Configuration" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkCustom_Configuration" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Custom Configuration </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="isAadharRegistered" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisAadharRegistered" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Aadhar Registered ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsActivateNewOrderScreenwithSize" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsActivateNewOrderScreenwithSize" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Activate New Order Screenwith Size ? </td>
                                                </tr>
                                            </table>
                                        </td>


                                    </tr>

                                    <tr>

                                        <td id="IsAllowBreakageTracking" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsAllowBreakageTracking" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Allow Breakage Tracking ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsAllowBreakageTrackingunderTeam" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsAllowBreakageTrackingunderTeam" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Allow Breakage Trackingunder Team ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsAllowClickForPhotoRegister" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsAllowClickForPhotoRegister" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Allow Click For Photo Register ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsAllowClickForVisit" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsAllowClickForVisit" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Allow Click For Visit ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td id="IsAllowClickForVisitForSpecificUser" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsAllowClickForVisitForSpecificUser" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Allow Click For Visit For Specific User ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsAllowShopStatusUpdate" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsAllowShopStatusUpdate" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Allow Shop Status Update ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsAlternateNoForCustomer" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsAlternateNoForCustomer" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Alternate No For Customer ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsAttendVisitShowInDashboard" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsAttendVisitShowInDashboard" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Attend Visit Show In Dashboard ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td id="IsAutoLeadActivityDateTime" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsAutoLeadActivityDateTime" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Auto Lead Activity DateTime ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsBeatRouteReportAvailableinTeam" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsBeatRouteReportAvailableinTeam" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Beat Route Report Available in Team ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsCollectionOrderWise" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsCollectionOrderWise" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Collection Order Wise ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsFaceRecognitionOnEyeblink" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsFaceRecognitionOnEyeblink" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Face Recognition On Eyeblink ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td id="isFaceRegistered" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkisFaceRegistered" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Face Registered ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsFeedbackAvailableInShop" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsFeedbackAvailableInShop" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Feedback Available In Shop ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsFeedbackHistoryActivated" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsFeedbackHistoryActivated" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Feedback History Activated ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsFromPortal" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsFromPortal" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is From Portal ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td id="IsIMEICheck" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsIMEICheck" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is IMEI Check ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IslandlineforCustomer" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIslandlineforCustomer" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is landline for Customer ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsNewQuotationfeatureOn" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsNewQuotationfeatureOn" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is New Quotation feature On ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsNewQuotationNumberManual" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsNewQuotationNumberManual" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is New Quotation Number Manual </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td id="IsPendingCollectionRequiredUnderTeam" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsPendingCollectionRequiredUnderTeam" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Pending Collection Required Under Team ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsprojectforCustomer" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsprojectforCustomer" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is project for Customer ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsRateEnabledforNewOrderScreenwithSize" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsRateEnabledforNewOrderScreenwithSize" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Rate Enabled for New Order Screen with Size ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsRestrictNearbyGeofence" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsRestrictNearbyGeofence" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Restrict Nearby Geofence ?</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td id="IsReturnEnableforParty" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsReturnEnableforParty" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Return Enable for Party ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsShowHomeLocationMap" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowHomeLocationMap" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Show Home Location Map ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsShowManualPhotoRegnInApp" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowManualPhotoRegnInApp" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Show Manual Photo Regn In App </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsShowMyDetails" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMyDetails" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Show My Details ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td id="IsShowNearByTeam" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowNearByTeam" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Show Near By Team ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsShowRepeatOrderinNotification" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowRepeatOrderinNotification" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <%--Rev 5.0--%>
                                                    <%--<td>Is Show Repeat Orderin Notification ? </td>--%>
                                                    <td>Is Show Repeat Order in Notification ? </td>
                                                    <%--End of Rev 5.0 --%>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsShowRepeatOrdersNotificationinTeam" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowRepeatOrdersNotificationinTeam" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <%--Rev 5.0--%>
                                                    <%--<td>Is Show Repeat Orders Notificationin Team ? </td>--%>
                                                    <td>Is Show Repeat Orders Notification in Team ? </td>
                                                    <%--End of Rev 5.0--%>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsShowRevisitRemarksPopup" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowRevisitRemarksPopup" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Show Revisit Remarks Popup ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td id="IsShowTypeInRegistration" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowTypeInRegistration" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Show Type In Registration ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsTeamAttendance" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsTeamAttendance" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Team Attendance ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsTeamAttenWithoutPhoto" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsTeamAttenWithoutPhoto" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Team Atten Without Photo ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="IsWhatsappNoForCustomer" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsWhatsappNoForCustomer" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Whatsapp No For Customer ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>

                                        <td id="Leaveapprovalfromsupervisor" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkLeaveapprovalfromsupervisor" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Leave approval from supervisor </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="Leaveapprovalfromsupervisorinteam" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkLeaveapprovalfromsupervisorinteam" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Leave approval from supervisor in team </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="LogoutWithLogFile" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkLogoutWithLogFile" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Logout With Log File </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="MarkAttendNotification" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkMarkAttendNotification" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Mark Attend Notification </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="PartyUpdateAddrMandatory" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkPartyUpdateAddrMandatory" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Party Update Addr Mandatory </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="PowerSaverSetting" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkPowerSaverSetting" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Power Saver Setting </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="ShopScreenAftVisitRevisit" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShopScreenAftVisitRevisit" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Shop Screen Aft Visit Revisit </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="Show_App_Logout_Notification" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShow_App_Logout_Notification" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show App Logout Notification </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td id="ShowAmountNewQuotation" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowAmountNewQuotation" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Amount New Quotation </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="ShowAutoRevisitInAppMenu" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowAutoRevisitInAppMenu" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Auto Revisit In App Menu </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="ShowAutoRevisitInDashboard" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowAutoRevisitInDashboard" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Auto Revisit In Dashboard </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="ShowCollectionAlert" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowCollectionAlert" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Collection Alert </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td id="ShowCollectionOnlywithInvoiceDetails" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowCollectionOnlywithInvoiceDetails" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Collection Only with Invoice Details </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="ShowQuantityNewQuotation" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowQuantityNewQuotation" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Quantity New Quotation </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="ShowTotalVisitAppMenu" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowTotalVisitAppMenu" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Total Visit App Menu </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="ShowUserwiseLeadMenu" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowUserwiseLeadMenu" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Userwise Lead Menu </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td id="ShowZeroCollectioninAlert" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowZeroCollectioninAlert" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Zero Collectionin Alert </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="UpdateOtherID" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkUpdateOtherID" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Update Other ID ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="UpdateUserID" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkUpdateUserID" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Update User ID ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="UpdateUserName" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkUpdateUserName" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Update User Name ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td id="WillRoomDBShareinLogin" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkWillRoomDBShareinLogin" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show user Last St Type </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="ShowPurposeInShopVisit" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowPurposeInShopVisit" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Purpose In Shop Visit </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--Rev 3.0--%>
                                        <td id="divShowEmployeePerformance" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowEmployeePerformance" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Employee Performance </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%-- End of Rev 3.0--%>
                                        <%--Rev 4.0--%>
                                        <td id="divShowBeatInMenu" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowBeatInMenu" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Beat In Menu </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 4.0--%>
                                    </tr>
                                    <%--Rev 6.0--%>
                                    <tr>
                                        <td id="divShowWorkType" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowWorkType" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Work Type </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divShowMarketSpendTimer" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowMarketSpendTimer" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Market Spend Timer </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divShowUploadImageInAppProfile" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowUploadImageInAppProfile" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Upload Image In App Profile </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divShowCalendar" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowCalendar" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Calendar </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="divShowCalculator" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowCalculator" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Calculator </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divShowInactiveCustomer" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowInactiveCustomer" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Inactive Customer </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divShowAttendanceSummary" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowAttendanceSummary" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Attendance Summary </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--Rev 7.0--%>
                                        <td id="divMenuShowAIMarketAssistant" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkMenuShowAIMarketAssistant" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show menu for AI Market Assistant </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="divUsbDebuggingRestricted" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkUsbDebuggingRestricted" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>USB Debugging Restricted </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 7.0--%>
                                        <%--Rev 8.0--%>
                                        <td id="divShowLatLongInOutletMaster" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowLatLongInOutletMaster" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Latitude Longitude in Outlet Master </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 8.0--%>
                                        <%--Rev 9.0--%>
                                        <td id="divIsCallLogHistoryActivated" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsCallLogHistoryActivated" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Call Log History Activated </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 9.0--%>
                                        <%--Rev 10.0--%>
                                        <td id="divIsShowMenuCRMContacts" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowMenuCRMContacts" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Menu CRM Contacts </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="divIsCheckBatteryOptimization" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsCheckBatteryOptimization" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Check Battery Optimization </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 10.0--%>
                                        <%--End of Rev 6.0--%>
                                        <%--End of Mantis Issue 25207--%>
                                        <%--Rev 11.0--%>
                                        <td id="divShowPartyWithGeoFence" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowPartyWithGeoFence" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Party With Geo Fence  </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divShowPartyWithCreateOrder" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowPartyWithCreateOrder" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Party With Create Order  </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--Rev 11.0 END--%>
                                        <%--Rev 12.0--%>
                                        <td id="divAdditionalinfoRequiredforContactListing" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkAdditionalinfoRequiredforContactListing" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Additional Information Required for Contact Listing  </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 12.0--%>
                                    </tr>
                                    <%--Rev 12.0--%>
                                    <tr>
                                        <td id="divAdditionalinfoRequiredforContactAdd" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkAdditionalinfoRequiredforContactAdd" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Additional Information Required for Contact Add  </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divContactAddresswithGeofence" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkContactAddresswithGeofence" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Contact Address with Geofence </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 12.0--%>
                                        <%--Rev 14.0--%>
                                        <td id="divIsCRMPhonebookSyncEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsCRMPhonebookSyncEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is CRM Phonebook Sync Enable ?</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divIsCRMSchedulerEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsCRMSchedulerEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is CRM Scheduler Enable ?</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--Rev 14.0 end--%>
                                    </tr>

                                    <tr>
                                        <%--Rev 14.0--%>
                                        <td id="divIsCRMAddEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsCRMAddEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is CRM Add Enable ?</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divIsCRMEditEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsCRMEditEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is CRM Edit Enable ?</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--Rev 14.0 End--%>
                                        <%--Rev 15.0--%>
                                        <td id="divIsShowAddressInParty" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowAddressInParty" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Show Address In Party ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divIsShowUpdateInvoiceDetails" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowUpdateInvoiceDetails" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Show Update Invoice Details ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 15.0--%>
                                        <%--Rev 16.0--%>
                                    </tr>
                                    <tr>
                                        <td id="divIsSpecialPriceWithEmployee" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsSpecialPriceWithEmployee" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Special Price With Employee ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 16.0--%>
                                        <%--Rev 17.0--%>
                                        <td id="divIsShowCRMOpportunity" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowCRMOpportunity" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Show CRM Opportunity ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divIsEditEnableforOpportunity" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsEditEnableforOpportunity" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Edit Enable for Opportunity ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divIsDeleteEnableforOpportunity" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsDeleteEnableforOpportunity" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Delete Enable for Opportunity ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 17.0--%>
                                    </tr>
                                    <%--Rev 18.0--%>
                                    <tr>
                                        <td id="divIsShowDateWiseOrderInApp" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsShowDateWiseOrderInApp" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is Show Date Wise Order In App ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%-- Rev 19.0--%>
                                        <td id="divIsUserWiseLMSEnable" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsUserWiseLMSEnable" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show LMS Menu </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="divIsUserWiseLMSFeatureOnly" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsUserWiseLMSFeatureOnly" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Is LMS Feature Only ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 19.0--%>
                                        <%-- Rev 20.0--%>
                                        <td id="divIsUserWiseRecordAudioEnableForVisitRevisit" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsUserWiseRecordAudioEnableForVisitRevisit" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Record Audio Enable For Visit/Revisit ? </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 20.0--%>
                                    </tr>
                                    <%-- End of Rev 18.0--%>
                                    <tr>
                                        <%-- Rev 21.0--%>
                                        <td id="DivShowClearQuiz" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkDivShowClearQuiz" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Clear Quiz ?</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 21.0--%>
                                        <%-- Rev 22.0--%>
                                        <td id="divIsAllowProductCurrentStockUpdateFromApp" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkIsAllowProductCurrentStockUpdateFromApp" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Allow Product Current Stock Update From App ?</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 22.0--%>
                                        <%-- Rev 23.0--%>
                                        <td id="divShowTargetOnApp" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="chkShowTargetOnApp" runat="server" Text="">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td>Show Target On App ?</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--End of Rev 23.0--%>
                                    </tr>
                                </table>
                            </div>
                        </div>

                    </div>

                </div>




                <%--<table class="tblWhiteS">--%>

                <%--<tr>
                        <td colspan="4">
                            <div class="segHeader">Report</div>
                        </td>
                    </tr>--%>

                <%--<tr>
                        <td colspan="4">
                            <div class="segHeader">Configuration</div>
                        </td>
                    </tr>--%>


                <%--</table>--%>
                <div class="clear"></div>
                <br />
                <div class="footer-btns">
                    <div class="col-md-12">
                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="btnUpdate_Click" OnClientClick="return setvalue()"
                            ValidationGroup="a" />
                        <input id="btnCancel" type="button" class="btn btn-danger" value="Cancel" onclick="Cancel_Click()" />
                    </div>
                </div>

                <%--End of Rev 13.0--%>
            </div>
        </div>
    </div>
</asp:Content>
