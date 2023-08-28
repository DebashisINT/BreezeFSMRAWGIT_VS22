<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                09-02-2023        2.0.39           Pallab              25656 : Master module design modification 
====================================================== Revision History ==========================================================--%>

<%@ Page Title="Pin / Zip" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" Inherits="ERP.OMS.Management.Master.management_master_pin" CodeBehind="frm_pinMaster.aspx.cs" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            ListBind();
            cityCode = "";
            stateCode = "";
        });
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

        function SetCountryNothing() {

            var lstCntry = document.getElementById("lstCountry");

            for (var i = 0; i < lstCntry.options.length; i++) {
                if (lstCntry.options[i].selected) {
                    lstCntry.options[i].selected = false;
                }
            }
            $('#lstCountry').trigger("chosen:updated");
            onCountryChange();

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

            }
        }



        function onAreaChange() {

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

            lState.empty();
            lCity.empty();

            $('#lstCity').trigger("chosen:updated");

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
                        if (stateCode) {
                            var stCode = stateCode;
                            stateCode = "";
                            setState(stCode);
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

            lCity.empty();
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
                        if (cityCode) {
                            var ctCode = cityCode;
                            cityCode = "";
                            setCity(ctCode);
                        }
                    }
                    else {
                        $('#lstCity').fadeIn();
                        $('#lstCity').trigger("chosen:updated");
                    }
                }
            });
        }

        function onCityChange() {
            var CityId = "";
            if (document.getElementById('lstCity').value) {
                CityId = document.getElementById('lstCity').value;
            }
            else {
                return;
            }

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



        function LastCall(obj) {
            if (grid.cpErrorMsg) {
                if (grid.cpErrorMsg.trim != "") {
                    jAlert(grid.cpErrorMsg);
                    grid.cpErrorMsg = '';
                    return;
                }
            }

            if (grid.cpSave != null) {
                if (grid.cpSave == 'Y') {
                    grid.cpSave = '';
                    jAlert("Saved Successfully");
                    cPopup_Empcitys.Hide();
                }

            }

            if (grid.cpEdit != null) {
                var pin = grid.cpEdit.split('~')[0];
                cityCode = grid.cpEdit.split('~')[1];
                stateCode = grid.cpEdit.split('~')[2];
                var countryCode = grid.cpEdit.split('~')[3];

                document.getElementById('txtpincode').value = pin;
                setCountry(countryCode);
            }

            if (grid.cpDelmsg != null) {
                if (grid.cpDelmsg.trim() != '') {
                    jAlert(grid.cpDelmsg);
                    grid.cpDelmsg = '';
                    cPopup_Empcitys.Hide();
                }
            }

        }

        function NewPin() {
            Status = 'SAVE_NEW';
            var lState = $('select[id$=lstState]');
            var lCity = $('select[id$=lstCity]');

            lState.empty();
            lCity.empty();

            $('#lstState').trigger("chosen:updated");
            $('#lstCity').trigger("chosen:updated");

            SetCountryNothing();
            document.getElementById('txtpincode').value = '';
            cPopup_Empcitys.Show();
        }
        function MakeRowInVisible() {
            $('#Mandatorytxtpincode').css({ 'display': 'none' });
            $('#MandatorylstCountry').css({ 'display': 'none' });
            $('#MandatorylstState').css({ 'display': 'none' });
            $('#MandatorylstCity').css({ 'display': 'none' });
            cPopup_Empcitys.Hide();
        }

        function isValid() {
            var retvalue = true;
            if (document.getElementById('txtpincode').value.trim() == '') {
                $('#Mandatorytxtpincode').css({ 'display': 'block' });
                retvalue = false;
            } else {
                $('#Mandatorytxtpincode').css({ 'display': 'none' });
            }

            if (document.getElementById('lstCountry').value.trim() == '') {
                $('#MandatorylstCountry').css({ 'display': 'block' });
                retvalue = false;
            } else {
                $('#MandatorylstCountry').css({ 'display': 'none' });
            }
            if (document.getElementById('lstState').value.trim() == '') {
                $('#MandatorylstState').css({ 'display': 'block' });
                retvalue = false;
            } else {
                $('#MandatorylstState').css({ 'display': 'none' });
            }
            if (document.getElementById('lstCity').value.trim() == '') {
                $('#MandatorylstCity').css({ 'display': 'block' });
                retvalue = false;
            } else {
                $('#MandatorylstCity').css({ 'display': 'none' });
            }
            return retvalue;
        }
        function Call_save() {
            if (!isValid()) {
                return;
            }
            document.getElementById('HdlstCity').value = document.getElementById('lstCity').value;
            grid.PerformCallback(Status);

        }

        function DeleteRow(keyValue) {

            jConfirm('Confirm delete?', 'Confirmation Dialog', function (r) {
                if (r == true) {
                    grid.PerformCallback('Delete~' + keyValue);
                }
            });
        }

        function OnEdit(obj) {
            Action = 'edit';
            Status = obj;
            grid.PerformCallback('BEFORE_' + obj);
            cPopup_Empcitys.Show();
        }

        function callback() {
            grid.PerformCallback();
        }
    </script>
    <style type="text/css">
        .chosen-container.chosen-container-single {
            width: 100% !important;
        }

        .chosen-choices {
            width: 100% !important;
        }

        #lstCountry, #lstState, #lstCity {
            width: 200px;
        }

        #lstCountry, #lstState, #lstCity {
            display: none !important;
        }

        #lstCountry_chosen, #lstState_chosen, #lstCity_chosen {
            width: 253px !important;
        }

        #PageControl1_CC {
            overflow: visible !important;
        }

        #lstState_chosen, #lstCountry_chosen, #lstCity_chosen {
            margin-bottom: 5px;
        }
        #gridPin_DXPagerBottom {
        max-width:100% !important;
        min-width: 100% !important; 
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
            right: 5px;
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


    @media only screen and (max-width: 768px)
    {
        #Popup_Empcitys_PW-1
        {
           width: 90% !important;
           left: 5% !important;
        }

    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="breadCumb">
            <span>Pin / Zip</span>
        </div>
    
    <div class="PopUpArea">
        <dxe:ASPxPopupControl ID="Popup_Empcitys" runat="server" ClientInstanceName="cPopup_Empcitys"
            Width="400px" HeaderText="Add/Modify Pin / Zip" PopupHorizontalAlign="WindowCenter"
            BackColor="white" Height="100px" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
            Modal="True" ContentStyle-VerticalAlign="Top" EnableHierarchyRecreation="True" >
            <contentcollection>
                    <dxe:PopupControlContentControl runat="server">
                        <%--<div style="Width:400px;background-color:#FFFFFF;margin:0px;border:1px solid red;">--%>
                        <div class="Top clearfix">
                           
                            <table>
                                    <tr>
                                      <td>
                                          Country:
                                     </td>
                                        <td>
                                            <asp:ListBox ID="lstCountry" CssClass="chsn"   runat="server" Font-Size="12px" Width="253px"   data-placeholder="Select..." onchange="onCountryChange()"></asp:ListBox>
                                             <span id="MandatorylstCountry" class="pullleftClass fa fa-exclamation-circle iconRed " style="color:red; position:absolute;right:26px;top:39px;display:none" title="Mandatory"></span>
                                        </td>
                                   </tr>
                                    <tr>
                                   <td> State:</td>
                                        <td>
                                            <asp:ListBox ID="lstState" CssClass="chsn"   runat="server" Font-Size="12px" Width="253px"   data-placeholder="Select State.."  onchange="onStateChange()"></asp:ListBox>
                                            <span id="MandatorylstState" class="pullleftClass fa fa-exclamation-circle iconRed " style="color:red; position:absolute;right:26px;top:74px;display:none" title="Mandatory"></span>
                                        </td>
                                    </tr> 
                                <tr>
                                    <td>City / District:</td>
                                    <td>
                                        <asp:ListBox ID="lstCity" CssClass="chsn"   runat="server" Font-Size="12px" Width="253px"   data-placeholder="Select City.."  ></asp:ListBox>
                                        <span id="MandatorylstCity" class="pullleftClass fa fa-exclamation-circle iconRed " style="color:red; position:absolute;right:26px;top:104px;display:none" title="Mandatory"></span>
                                         <asp:HiddenField ID="HdlstCity" runat="server" />
                                    </td>
                                </tr>
                                  
                                <tr>
                                    <td>Pin / Zip Code:</td>
                                    <td>
                                       <asp:TextBox ID="txtpincode" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                        <span id="Mandatorytxtpincode" class="pullleftClass fa fa-exclamation-circle iconRed " style="color:red; position:absolute;right:26px;top:141px;display:none" title="Mandatory"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding-left:79px;padding-top:15px">
                                            <input id="btnSave" class="btn btn-primary" onclick="Call_save(status)" type="button" value="Save" />
                                    <input id="btnCancel" class="btn btn-danger" onclick="MakeRowInVisible()" type="button" value="Cancel" />
                                        </td>
                                        
                                    </tr>
                                </table>


                        </div>
                         
                    </dxe:PopupControlContentControl>
                </contentcollection>
            <headerstyle backcolor="LightGray" forecolor="Black" />
        </dxe:ASPxPopupControl>

    </div>


    <div class="container">
        <div class="backBox mt-3 p-3 ">
        <table class="TableMain100">
            <tr>
                <td colspan="4">
                    <table class="TableMain100">
                        <tr>
                            <td colspan="4" style="text-align: left; vertical-align: top">
                                <table class=" mb-4">
                                    <tr>
                                        <td id="ShowFilter">
                                            <% if (rights.CanAdd)
                                               { %>
                                            <asp:HyperLink ID="HyperLink2" runat="server"
                                                NavigateUrl="javascript:void(0)" onclick="javascript:NewPin()" class="btn btn-success mr-2">Add New</asp:HyperLink>
                                            <%} %>
                                            
                                        </td>
                                        <td id="Td1">
                                            <% if (rights.CanExport)
                                               { %>
                                            <asp:DropDownList ID="drdExport"  runat="server" CssClass="btn btn-sm btn-primary" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Export to</asp:ListItem>
                                                <asp:ListItem Value="1">PDF</asp:ListItem>
                                                <asp:ListItem Value="2">XLS</asp:ListItem>
                                                <asp:ListItem Value="3">RTF</asp:ListItem>
                                                <asp:ListItem Value="4">CSV</asp:ListItem>
                                            </asp:DropDownList>
                                             <%} %>
                                            <dxe:ASPxGridViewExporter ID="exporter" runat="server">
                                            </dxe:ASPxGridViewExporter>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="gridcellright"></td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td>
                    <dxe:ASPxGridView ID="gridPin" runat="server" ClientInstanceName="grid" AutoGenerateColumns="False"
                        DataSourceID="SqlDataSource1" KeyFieldName="pin_id" Width="100%" OnCustomCallback="gridPin_CustomCallback"
                        OnCustomJSProperties="gridPin_CustomJSProperties" OnHtmlRowCreated="gridPin_HtmlRowCreated">
                        <styles>
                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                            </Header>
                            <LoadingPanel ImageSpacing="10px">
                            </LoadingPanel>
                        </styles>
                        <SettingsSearchPanel Visible="True" />
                        <Settings ShowGroupPanel="True" ShowStatusBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="true"/>

                        <columns>
                            <dxe:GridViewDataTextColumn FieldName="pin_id" ReadOnly="True" Visible="False" VisibleIndex="0">
                                <EditFormSettings Visible="False" />
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn FieldName="pin_code" Caption="Pin / Zip Code" Width="30%"
                                VisibleIndex="0" ShowInCustomizationForm="True">
                                <editcellstyle wrap="True">
                                </editcellstyle>
                                <CellStyle CssClass="gridcellleft" wrap="True">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>
                             
                             
                             <dxe:GridViewDataTextColumn FieldName="city_id" Caption="City / District" Width="30%"
                                VisibleIndex="1" ShowInCustomizationForm="True">
                                <editcellstyle wrap="True">
                                </editcellstyle>
                                <CellStyle CssClass="gridcellleft" wrap="True">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn FieldName="state" Caption="State" Width="30%"
                                VisibleIndex="2" ShowInCustomizationForm="True">
                                <editcellstyle wrap="True">
                                </editcellstyle>
                                <CellStyle CssClass="gridcellleft" wrap="True">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>


                            <dxe:GridViewDataTextColumn Caption="" VisibleIndex="3" Width="10%">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    Actions
                                   
                                </HeaderTemplate>
                                <DataItemTemplate>
                                    <% if (rights.CanEdit)
                                       { %>
                                    <a href="javascript:void(0);" onclick="OnEdit('EDIT~'+'<%# Container.KeyValue %>')" class="pad">
                                        <img src="../../../assests/images/Edit.png" alt="Edit"></a>
                                    <% } %>
                                    <% if (rights.CanDelete)
                                       { %>
                                     <a href="javascript:void(0);" onclick="DeleteRow('<%#Eval("pin_id") %>')"   alt="Delete" class="pad">
                                        <img src="../../../assests/images/Delete.png" /></a>
                                     <% } %>
                                </DataItemTemplate>
                            </dxe:GridViewDataTextColumn>
                      
                        </columns>

                        <settingspager pagesize="50">
                            <PageSizeItemSettings Visible="true" ShowAllItem="false" Items="50,100,150,200"/>
                              </settingspager>
                        <settingscommandbutton>
                           
                            <EditButton Image-Url="../../../assests/images/Edit.png" ButtonType="Image" Image-AlternateText="Edit">
<Image AlternateText="Edit" Url="../../../assests/images/Edit.png"></Image>
                            </EditButton>
                             <DeleteButton Image-Url="../../../assests/images/Delete.png" ButtonType="Image" Image-AlternateText="Delete">
<Image AlternateText="Delete" Url="../../../assests/images/Delete.png"></Image>
                            </DeleteButton>
                            <UpdateButton Text="Save" ButtonType="Button" Styles-Style-CssClass="btn btn-primary" Image-Width>
<Styles>
<Style CssClass="btn btn-primary"></Style>
</Styles>
                            </UpdateButton>
                            <CancelButton Text="Cancel" ButtonType="Button"></CancelButton>
                        </settingscommandbutton>
                        <styleseditors>
                            <ProgressBar Height="25px">
                            </ProgressBar>
                        </styleseditors>
                        <SettingsSearchPanel Visible="True" />
                        <settings showgrouppanel="True" showstatusbar="Visible" showfilterrow="true" ShowFilterRowMenu ="true" />
                        <settingsediting mode="PopupEditForm" popupeditformheight="200px" popupeditformhorizontalalign="Center"
                            popupeditformmodal="True" popupeditformverticalalign="WindowCenter" popupeditformwidth="600px"
                            editformcolumncount="1" />
                        <settingstext popupeditformcaption="Add/Modify Category" confirmdelete="Confirm delete?" />
                        <settingspager numericbuttoncount="20" pagesize="50" showseparators="True" alwaysshowpager="True">
                            <FirstPageButton Visible="True">
                            </FirstPageButton>
                            <LastPageButton Visible="True">
                            </LastPageButton>
                        </settingspager>
                        <settingsbehavior confirmdelete="True" columnresizemode="NextColumn" />

                        <clientsideevents endcallback="function(s, e) {
	LastCall(s.cpHeight);
}" />
                    </dxe:ASPxGridView>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
            DeleteCommand="DELETE FROM [tbl_master_pinzip] WHERE [pin_id] = @id"
            InsertCommand="INSERT INTO [tbl_master_pinzip] ([pin_code],[[city_id]]) VALUES (@cat_description,@cat_applicablefor)"
            SelectCommand="select  pin_id,pin_code,d.city_name as city_id,s.state from tbl_master_pinzip h inner join tbl_master_city d on h.city_id=d.city_id inner join tbl_master_state s on s.id=d.state_id order by pin_id"
            UpdateCommand="UPDATE [tbl_master_pinzip] SET [pin_code] = @cat_description,cat_applicablefor=@cat_applicablefor WHERE [pin_id] = @pin_id">
            <DeleteParameters>
                <asp:Parameter Name="pin_id" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="pin_code" Type="String" />
                <asp:Parameter Name="city_id" Type="String" />
                <asp:Parameter Name="pin_id" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="pin_code" Type="String" />
                <asp:Parameter Name="city_id" Type="String" />
            </InsertParameters>
            <FilterParameters>
                <asp:Parameter Name="pin_code" Type="String" />
                <asp:Parameter Name="city_id" Type="String" />
            </FilterParameters>
        </asp:SqlDataSource>
        <br />
    </div>
        </div>
</asp:Content>

