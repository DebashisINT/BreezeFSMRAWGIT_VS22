<%--====================================================== Revision History ===============================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                16-02-2023        V2.0.39          Pallab              Settings/Options module design modification 
====================================================== Revision History ===================================================--%>

<%@ Page Title="Home Location User" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true"
    Inherits="ERP.OMS.Management.Master.Salesman_Address" CodeBehind="Salesman-Address.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Scripts/Geocomplete/styles.css" rel="stylesheet" />


   
        <script src="https://maps.googleapis.com/maps/api/js?libraries=places&key=AIzaSyBif3telvlrJn61kkLXDQA0ViQdDVJWifk"></script>

    <script src="../../../Scripts/Geocomplete/jquery.geocomplete.js"></script>

    <%--<script src="../../../Scripts/Geocomplete/logger.js"></script>--%>

     <script  type="text/javascript">


         $(function () {

             //var options = {
             //    map: ".map_canvas",
             //    details: "form ",
             //    markerOptions: {
             //        draggable: true
             //    }
             //};


             //$("#txtaddress").geocomplete(options);

             $("#txtaddress").geocomplete({
                 map: ".map_canvas",
                 details: "form ",
                 markerOptions: {
                     draggable: true
                 }
             });

             $("#txtaddress").bind("geocode:dragged", function (event, latLng) {
                 $("#txtlat").val(latLng.lat());
                 $("#txtlong").val(latLng.lng());
               
             });

             $("#txtaddress").trigger("geocode");

             //$("#find").click(function () {
             //    $("#txtaddress").trigger("geocode");
             //}).click();

         });

        FieldName = null;


        function GetBranches(obj1, obj2, obj3) {

            var strQuery_Table = "tbl_master_branch";
            var strQuery_FieldName = "top 10 (isnull(branch_description,\'\')+ \'-[\'+ isnull(branch_code,\'\') + \']\') as Branch,branch_id";
            var strQuery_WhereClause = " (branch_description Like (\'%RequestLetter%\') or branch_code Like (\'%RequestLetter%\')) and branch_id not in (select BranchGroupMembers_BranchID from trans_branchgroupmembers)";
            var strQuery_OrderBy = '';
            var strQuery_GroupBy = '';
            var CombinedQuery = new String(strQuery_Table + "$" + strQuery_FieldName + "$" + strQuery_WhereClause + "$" + strQuery_OrderBy + "$" + strQuery_GroupBy);
            if (obj1.value == "") {
                obj1.value = "%";
            }
            ajax_showOptions(obj1, obj2, obj3, replaceChars(CombinedQuery));
            if (obj1.value == "%") {
                obj1.value = "";
            }
        }

        function replaceChars(entry) {
            out = "+"; // replace this
            add = "--"; // with this
            temp = "" + entry; // temporary holder

            while (temp.indexOf(out) > -1) {
                pos = temp.indexOf(out);
                temp = "" + (temp.substring(0, pos) + add +
                temp.substring((pos + out.length), temp.length));
            }

            return temp;

        }

        function btnAddBranch() {
            var userid = document.getElementById('txtBranch');
            if (userid.value != '') {
                var ids = document.getElementById('txtBranch_hidden');
                var listBox = document.getElementById('lstBranches');
                var tLength = listBox.length;
                //
                var i;
                if (tLength > 0) {
                    for (i = 0; i < tLength; i++) {

                        if (listBox[i].value == ids.value) {
                            alert('This Branch is Already Added !');

                            return false;
                        }

                    }

                }
                //
                var no = new Option();
                no.value = ids.value;
                no.text = userid.value;
                listBox[tLength] = no;
                var recipient = document.getElementById('txtBranch');
                recipient.value = '';

                var listIDs = '';
                if (listBox.length > 0) {
                    for (i = 0; i < listBox.length; i++) {
                        if (listIDs == '')
                            listIDs = listBox.options[i].value + ';' + listBox.options[i].text;
                        else
                            listIDs += ',' + listBox.options[i].value + ';' + listBox.options[i].text;
                    }

                    // var sendData = cmb.value + '~' + listIDs;
                    CallServer(listIDs, "");

                }

            }
            else
                alert('Please search name and then Add!')
            var s = document.getElementById('txtBranch');
            s.focus();
            s.select();
        }

        function Branchselectionfinal() {

            var listBoxSubs = document.getElementById('lstBranches');
            // var cmb=document.getElementById('cmbsearchOption');
            var listIDs = '';
            var i;

            if (listBoxSubs.length > 0) {
                for (i = 0; i < listBoxSubs.length; i++) {
                    if (listIDs == '')
                        listIDs = listBoxSubs.options[i].value + ';' + listBoxSubs.options[i].text;
                    else
                        listIDs += ',' + listBoxSubs.options[i].value + ';' + listBoxSubs.options[i].text;
                }

                // var sendData = cmb.value + '~' + listIDs;
                CallServer(listIDs, "");

            }
            //	        var i;
            for (i = listBoxSubs.options.length - 1; i >= 0; i--) {
                listBoxSubs.remove(i);
            }

            // document.getElementById('TdFilter').style.visibility='hidden';
            // document.getElementById('TdFilter1').style.visibility='hidden';


        }

        function btnRemoveBranch() {

            var listBox = document.getElementById('lstBranches');
            var tLength = listBox.length;

            var arrTbox = new Array();
            var arrLookup = new Array();
            var i;
            var j = 0;
            for (i = 0; i < listBox.options.length; i++) {
                if (listBox.options[i].selected && listBox.options[i].value != "") {

                }
                else {
                    arrLookup[listBox.options[i].text] = listBox.options[i].value;
                    arrTbox[j] = listBox.options[i].text;
                    j++;
                }
            }
            listBox.length = 0;
            for (i = 0; i < j; i++) {
                var no = new Option();
                no.value = arrLookup[arrTbox[i]];
                no.text = arrTbox[i];
                listBox[i] = no;
            }

            var listIDs = '';
            var k;
            if (listBox.length > 0) {
                for (k = 0; k < listBox.length; k++) {
                    if (listIDs == '')
                        listIDs = listBox.options[k].value + ';' + listBox.options[k].text;
                    else
                        listIDs += ',' + listBox.options[k].value + ';' + listBox.options[k].text;
                }

                // var sendData = cmb.value + '~' + listIDs;
                CallServer(listIDs, "");

            }

        }


        function ReceiveServerData(rValue) {

            var Data = rValue.split('~');
            var NoItems = Data[0].split(';');
            var a = '';
            if (NoItems.length > 1) {

                var NoItemsDis = Data[1].split(',');
                for (i = 0; i < NoItemsDis.length; i++) {
                    if (i == 0) {
                        //                        var a=NoItemsDis[i];
                        var Dis = NoItemsDis[i].split(';');
                        a = Dis[1];
                    }
                    else {
                        var Dis = NoItemsDis[i].split(';');
                        a = a + ',' + Dis[1];
                    }

                }
            }

        }


        function CheckValid() {

            var Nameval = document.getElementById('txtName').value;
            var newNameval = /^ *$/.test(Nameval);
            var Codeval = document.getElementById('txtCode').value;
            var newCodeval = /^ *$/.test(Codeval);
            var lstBranches = document.getElementById('lstBranches');
            // var newlstBranches = /^ *$/.test(lstBranches);

            if (!newNameval)
                if (!newCodeval)
                    if (lstBranches.length > 0) {
                        return true;
                    }
                    else {
                        alert('Branch name required');
                        return false;
                    }
                else {
                    alert('Short name required');
                    return false;
                }
            else {
                alert('Branch Group name required');
                return false;
            }
        }


        function Check() {
            /// debugger;
            //var txtBranch = document.getElementById('txtBranch').value;
            var lstBranches = $("#lstBranches").val();
            var user = $("#drduser").val();
            var addr = $("#txtaddress").val();
            var lat = $("#txtlat").val();
            var long = $("#txtlong").val();
            //  alert(lstBranches + ' ' + user + ' ' + Imei)

            if (lstBranches == 'Select Branch') {


                jAlert('Branch is mandatory');
                return false;
            }

            else if (user == null) {

                jAlert('User is mandatory');
                return false;
            }

            else if (addr == '') {

                jAlert('Address is mandatory');
                return false;
            }


            else if (lat == '') {

                jAlert('Latitude is mandatory');
                return false;
            }


            else if (long == '') {

                jAlert('Longitude is mandatory');
                return false;
            }

        }


        function EditBranch(branchtext, branchvalue) {
            //alert(branchtext);
            var listBox = document.getElementById('lstBranches');
            var tLength = listBox.length;
            var no = new Option();
            no.value = branchvalue;
            no.text = branchtext;
            listBox[tLength] = no;

        }
        function ClosePage() {
            parent.editwin.close();

        }
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 85px;
        }

        .mb0 {
            margin-bottom: 0px !important;
        }
    </style>
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>



    <script type="text/javascript">
        $(document).ready(function () {
            //$('#lstBranches').chosen();
            var config = {
                '#lstBranches': {},
                '#lstBranches-deselect': { allow_single_deselect: true },
                '#lstBranches-no-single': { disable_search_threshold: 10 },
                '#lstBranches-no-results': { no_results_text: 'Oops, nothing found!' },
                '#lstBranches-width': { width: "95%" }
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);
            }
        });

        $(function () {
            if ($("#hdncommaaccept").val() == '1') {
                $("#spancomma").attr('style', 'display:inline-block;');
                $("#txtimei").keypress(function () {
                    return RestrictSpaceSpecial(event);
                });
            }

        });

        function RestrictSpaceSpecial(e) {
            var k;
            document.all ? k = e.keyCode : k = e.which;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
        }


    </script>

    <%--Rev 1.0--%>
    <style>
        
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
            top: 8px;
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

    #dtFrom_B-1, #dtTo_B-1 , #cmbDOJ_B-1, #cmbLeaveEff_B-1, #FormDate_B-1, #toDate_B-1 {
        background: transparent !important;
        border: none;
        width: 30px;
        padding: 10px !important;
    }

        #dtFrom_B-1 #dtFrom_B-1Img,
        #dtTo_B-1 #dtTo_B-1Img , #cmbDOJ_B-1 #cmbDOJ_B-1Img, #cmbLeaveEff_B-1 #cmbLeaveEff_B-1Img, #FormDate_B-1 #FormDate_B-1Img, #toDate_B-1 #toDate_B-1Img {
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

    .btn-sm, .btn-xs, .btn
    {
        padding: 7px 10px !important;
        height: 34px;
    }

    #productAttributePopUp_PWH-1 span, #ASPxPopupControl2_PWH-1 span
    {
        color: #fff !important;
    }

    #marketsGrid_DXPEForm_tcefnew, .dxgvPopupEditForm_PlasticBlue
    {
        height: 135px !important;
    }
    
    </style>
    <%--Rev end 1.0--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--rev 25249--%>
    <%--<div class="panel-heading">
        <div class="panel-title">
            <h3>SalesMan Home Address</h3>
            <div class="crossBtn"><a href="Salesman-AddressList.aspx"><i class="fa fa-times"></i></a></div>
        </div>

    </div>--%>
    <div class="breadCumb">
        <span>SalesMan Home Address</span>
        <div class="crossBtnN"><a href="Salesman-AddressList.aspx"><i class="fa fa-times"></i></a></div>
    </div>
    <%--rev end 25249--%>
    <div class="container">
        <div class="backBox mt-5 p-4 ">
    <div class="form_main" style="border: 1px solid #ccc;">
        <div class="col-md-6">
            <table class="pdtble">
                        <tr>
                            <td align="left" style="">Branch<span style="color: red">*</span>
                            </td>
                            <td align="left" style="position: relative">
                                <asp:DropDownList ID="lstBranches" runat="server" Width="253px" MaxLength="100" ValidationGroup="A" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                <span id="MandatoryName" class="pullleftClass fa fa-exclamation-circle iconRed " style="color: red; position: absolute; right: -12px; top: 10px; display: none" title="Mandatory"></span>

                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td align="left">User Name<span style="color: red">*</span>
                            </td>
                            <%--Rev 1.0--%>
                            <%--<td align="left" style="position: relative">--%>
                            <td align="left" style="position: relative" class="h-branch-select">
                                <%--Rev end 1.0--%>
                                <asp:DropDownList ID="drduser" runat="server" Width="253px" MaxLength="100" ValidationGroup="A"></asp:DropDownList>
                                <span id="MandatoryShortname" class="pullleftClass fa fa-exclamation-circle iconRed" style="color: red; position: absolute; right: -12px; top: 10px; display: none" title="Mandatory"></span>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>

                        <tr>
                            <td align="left">State<span style="color: red">*</span>
                            </td>

                            <%--Rev 1.0--%>
                            <%--<td align="left" style="position: relative">--%>
                            <td align="left" style="position: relative" class="h-branch-select">
                                <%--Rev end 1.0--%>

                                <asp:DropDownList ID="ddlstate" runat="server" Width="253px" MaxLength="100" ValidationGroup="A"></asp:DropDownList>

                            </td>
                            <td></td>

                        </tr>

                        <tr>
                            <td align="left">Home Address<span style="color: red">*</span>
                            </td>
                            <td align="left" style="position: relative">

                                <asp:TextBox ID="txtaddress"  Text="India" runat="server" Width="253px" MaxLength="50" required="required" CssClass="form-control"></asp:TextBox>


                                <span id="MandatoryShortname" class="pullleftClass fa fa-exclamation-circle iconRed" style="color: red; position: absolute; right: -12px; top: 10px; display: none" title="Mandatory"></span>

                                     <%-- <input id="find" type="button" value="find" />--%>

<%--                                  <input id="geocomplete" type="text" placeholder="Type in an address" size="90" />--%>

                            </td>
                            <td>

                            </td>

                        </tr>

                        <tr>
                            <td align="left">Home Latitude<span style="color: red">*</span>
                            </td>

                            <td align="left" style="position: relative">

                                <asp:TextBox ID="txtlat"  runat="server" Width="253px"  required="required" CssClass="form-control"></asp:TextBox>

                            </td>
                            <td></td>

                        </tr>

                        <tr>
                            <td align="left">Home Longitude<span style="color: red">*</span>
                            </td>
                            <td align="left" style="position: relative">

                                <asp:TextBox ID="txtlong" runat="server" Width="253px" required="required" CssClass="form-control"></asp:TextBox>

                            </td>
                            <td></td>

                        </tr>




                        <tr>
                            <td></td>
                            <td align="left" colspan="3">
                                <asp:Button ID="btnSubmit" runat="Server" CssClass="btn btn-primary" Text="Submit"
                                    OnClientClick="return Check()" OnClick="btnSubmit_Click" ValidationGroup="A" />
                            </td>
                        </tr>
                    </table>
        </div>
        <div class="col-md-6">
               <div class="map_canvas" style="height:350px"></div>
        </div>
    </div>
    </div>
        </div>
    <div>

        <asp:HiddenField ID="hdncommaaccept" runat="server" />

    </div>
     
<%--     <script src="http://maps.googleapis.com/maps/api/js?sensor=false&amp;libraries=places"></script>--%>
   


</asp:Content>

