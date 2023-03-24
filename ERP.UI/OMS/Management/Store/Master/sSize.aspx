﻿<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                13-02-2023        2.0.39           Pallab              25656 : Master module design modification 
====================================================== Revision History ==========================================================--%>

<%@ Page Title="Strength Schemes" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master" Inherits="ERP.OMS.Management.Store.Master.Management_Store_Master_sSize" Codebehind="sSize.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        

        .dxpc-headerContent {
            color: white;
        }
        .cityDiv {
        width:70px;
        }
    </style>
  <%--  <link type="text/css" href="../../../CSS/style.css" rel="Stylesheet" />
    <link href="../../../CentralData/CSS/GenericCss.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../CentralData/JSScript/GenericJScript.js"></script>

    <script type="text/javascript" src="/assests/js/jquery-1.3.2.min.js"></script>

    <script type="text/javascript" src="/assests/js/init.js"></script>

    <script type="text/javascript" src="/assests/js/loaddata1.js"></script>--%>
    <script type="text/javascript" src="../../../CentralData/JSScript/GenericJScript.js"></script>
    <script type="text/javascript">

        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        //function height() { 
        //    if (document.body.scrollHeight >= 500)
        //        window.frameElement.height = document.body.scrollHeight;
        //    else
        //        window.frameElement.height = '500px';
        //    window.frameElement.Width = document.body.scrollWidth;
        //}

        function PopupOpen(obj) {
            var URL = '../../../Management/Master/Contact_Document.aspx?idbldng=' + obj;
            OnMoreInfoClick(URL, "Products Document Details", '1000px', '400px', "Y");
            //            editwin = dhtmlmodal.open("Editbox", "iframe", URL, "Document", "width=1000px,height=400px,center=0,resize=1,top=-1", "recal");
            //            alert(editwin)
            //            editwin.onclose = function() {
            //                grid.PerformCallback();
            //            }
        }
    </script>

    <style type="text/css">
        .dxgvCustomization, .dxgvPopupEditForm {
            height: 400px !important;
            margin: 0;
            overflow: auto;
            padding: 0;
            width: 100%;
        }

        .dxgvEditFormTable {
            background-color: #ccc !important;
        }

        .dxgvEditFormCaption {
            text-align: left !important;
        }

        td.dxeControlsCell table.dxeButtonEdit, #marketsGrid_DXPEForm_efnew_DXEditor4 {
            width: 300px !important;
        }

        #marketsGrid_DXPEForm_efnew_DXEditor5 {
            width: 300px !important;
        }





        #Popup_Empcitys_PW-1 {
            top: 110px !important;
        }

        #Popup_Empcitys_ASPxPopupControl1_PW-1 {
            top: 80px !important;
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

    .btn-sm, .btn-xs
    {
        padding: 7px 10px !important;
    }

    #productAttributePopUp_PWH-1 span, #ASPxPopupControl2_PWH-1 span
    {
        color: #fff !important;
    }

    #marketsGrid_DXPEForm_tcefnew, .dxgvPopupEditForm_PlasticBlue
    {
        height: 220px !important;
    }
/*Rev end 1.0*/
    </style>

<%--    <script language="javascript" type="text/javascript">
        function SignOff() {
            window.parent.SignOff();
        }
        function height() {
            if (document.body.scrollHeight >= 500)
                window.frameElement.height = document.body.scrollHeight;
            else
                window.frameElement.height = '500px';
            window.frameElement.Width = document.body.scrollWidth;
        }
    </script>--%>

    <script type="text/javascript">
        function fn_PopOpen() {

            ctxtSize_Name.Focus();
            document.getElementById('hiddenedit').value = "";
            ctxtSize_Name.SetText('');
            ctxtSize_Description.SetText('');
            document.getElementById("Popup_AddNew").style.display = "none";
            //  grid2.Refresh();

            //            if (ctxtSize_Name.GetText() == "") {

            //                alert("Refresh Grid2");

            //                grid2.Refresh();
            //                document.getElementById("Popup_AddNew").style.display = "none";

            //  Popup_Empcitys_cityGrid2
            //}

            cPopup_Empcitys.Show();



        }



        function btnSave_citys() {

            if (ctxtSize_Description.GetText() != '' && ctxtSize_Name.GetText() != '') {

                if (document.getElementById('hiddenedit').value == '') {
                    grid.PerformCallback('savecity~');

                }
                else
                    grid.PerformCallback('updatecity~' + GetObjectID('hiddenedit').value);
            }
            //else if (ctxtSize_Name.GetText() == '') {
            //    alert('Please Enter Size Name');
            //    ctxtSize_Name.Focus();
            //}
            //else if (ctxtSize_Description.GetText() == '') {
            //    alert('Please enter size description');
            //    ctxtSize_Description.Focus();
            //}

        }

        function fn_btnCancel() {
            cPopup_Empcitys.Hide();
            //  alert("ss");
        }
        function fn_Editcity(keyValue) {
            grid.PerformCallback('Edit~' + keyValue);

        }
        function fn_Deletecity(keyValue) {
            //if (confirm("Confirm Delete?")) {
            //    grid.PerformCallback('Delete~' + keyValue);
            //    grid.Refresh();
            //}
            jConfirm('Confirm delete ?', 'Confirmation Dialog', function (r) {
                if (r == true) {
                    grid.PerformCallback('Delete~' + keyValue);
                }
            });

        }
        function grid_EndCallBack() {
            debugger;
            if (grid.cpinsert != null) {


                var vSuccessFail = grid.cpinsert.split('~')[0];

                //    alert(vSuccessFail);

                //                if (grid.cpinsert == vSuccessFail) {
                if (vSuccessFail == 'Success') {
                    jAlert('Inserted Successfully');
                    document.getElementById("Popup_AddNew").style.display = "block";
                    GetObjectID('hiddenedit').value = grid.cpinsert.split('~')[1];

                    grid2.Refresh();
                    grid.cpinsert = null;
                    //  cPopup_Empcitys.Hide();

                }
                else {
                    jAlert("Error On Insertion \n 'Please Try Again!!'");
                    cPopup_Empcitys.Hide();
                }
            }
            if (grid.cpEdit != null) {
                ctxtSize_Name.SetText(grid.cpEdit.split('~')[0]);
                if (document.getElementById("Popup_Empcitys_txtSize_Name_I") != null) {
                    document.getElementById("Popup_Empcitys_txtSize_Name_I").readOnly = true;
                }
                ctxtSize_Description.SetText(grid.cpEdit.split('~')[1]);

                GetObjectID('hiddenedit').value = grid.cpEdit.split('~')[2];


                cPopup_Empcitys.Show();

                grid2.Refresh();
                grid.cpEdit = null

            }
            if (grid.cpUpdate != null) {
                if (grid.cpUpdate == 'Success') {
                    jAlert('Update Successfully');
                    cPopup_Empcitys.Hide();
                    grid.cpUpdate = null;
                }
                else {
                    jAlert("Error on Updation\n'Please Try again!!'")
                    cPopup_Empcitys.Hide();
                }
            }
            if (grid.cpUpdateValid != null) {
                if (grid.cpUpdateValid == "StateInvalid") {
                    jAlert("Please select proper country state and city");

                }
            }
            if (grid.cpDelmsg != null) {
                if (grid.cpDelmsg.trim() != '') {
                    jAlert(grid.cpDelmsg);
                    grid.cpDelmsg = '';
                    grid.Refresh();
                }
            }
            if (grid.cpExists != null) {
                if (grid.cpExists == "Exists") {
                    jAlert('Record already Exists');
                    cPopup_Empcitys.Hide();
                    grid.cpExists = null;
                }
                else {
                    jAlert("Error on operation \n 'Please Try again!!'")
                    cPopup_Empcitys.Hide();
                }
            }
        }


    </script>
<script type="text/javascript">
    function fn_ctxtSize_Name_TextChanged() {
        var SizeName = ctxtSize_Name.GetText();
        var sizeid = 0;
        if (GetObjectID('hiddenedit').value != '') {
            sizeid = GetObjectID('hiddenedit').value;
        }
        $.ajax({
            type: "POST",
            url: "sSize.aspx/CheckUniqueName",
            //data: "{'SizeName':'" + SizeName + "'}",
            data: JSON.stringify({ SizeName: SizeName, sizeid: sizeid }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var data = msg.d;
                if (data == true) {
                    jAlert("Please enter unique name");
                    document.getElementById("txtSize_Name_I").value = "";
                    document.getElementById("txtSize_Name_I").focus();
                    //document.getElementById("txtSize_Name_I").value = "";
                    //document.getElementById("txtSize_Name_I").focus();
                    return false;
                }
            }

        });
    }
</script>
<%--    <style type="text/css">
        .cityDiv {
            height: 25px;
            width: 130px;
            float: left;
            margin-left: 70px;
        }

        .cityTextbox {
            height: 25px;
            width: 50px;
        }

        .Top {
            height: 90px;
            width: 400px;
            background-color: Silver;
            padding-top: 5px;
            valign: top;
        }

        .Footer {
            height: 30px;
            width: 400px;
            background-color: Silver;
            padding-top: 10px;
        }

        .ScrollDiv {
            height: 250px;
            width: 400px;
            background-color: Silver;
            overflow-x: hidden;
            overflow-y: scroll;
        }

        .ContentDiv {
            width: 400px;
            height: 300px;
            border: 2px;
            background-color: Silver;
        }

        .pad {
            padding: 10px;
        }

        .TitleArea {
            height: 20px;
            padding-left: 10px;
            padding-right: 3px;
            background-image: url( '../images/EHeaderBack.gif' );
            background-repeat: repeat-x;
            background-position: bottom;
            text-align: center;
        }

        .FilterSide {
            float: left;
            padding-left: 15px;
            width: 50%;
        }

        .SearchArea {
            width: 100%;
            height: 30px;
            padding-top: 5px;
        }
        /* Big box with list of options */ #ajax_listOfOptions {
            position: absolute; /* Never change this one */
            width: 50px; /* Width of box */
            height: auto; /* Height of box */
            overflow: auto; /* Scrolling features */
            border: 1px solid Blue; /* Blue border */
            background-color: #FFF; /* White background color */
            text-align: left;
            font-size: 0.9em;
            z-index: 32767;
        }

            #ajax_listOfOptions div {
                /* General rule for both .optionDiv and .optionDivSelected */
                margin: 1px;
                padding: 1px;
                cursor: pointer;
                font-size: 0.9em;
            }

            #ajax_listOfOptions .optionDiv {
                /* Div for each item in list */
            }

            #ajax_listOfOptions .optionDivSelected {
                /* Selected item in the list */
                background-color: #DDECFE;
                color: Blue;
            }

        #ajax_listOfOptions_iframe {
            background-color: #F00;
            position: absolute;
            z-index: 3000;
        }

        form {
            display: inline;
        }
    </style>--%>
    <!-- inner grig -->

    <script type="text/javascript">
        function fn_PopOpen2() {
            document.getElementById('hiddenedit2').value = "";
            ctxtSizeDetail_AttribName.Focus();
            ctxtSizeDetail_AttribName.SetText('');
            ctxtSizeDetail_Value.SetText('');

            cCmbSizeDetail_UOM.SetSelectedIndex(0);
            // grid2.Refresh();
            cPopup_Empcitys2.Show();
        }


        function btnSave_citys2() {

            if (ctxtSizeDetail_AttribName.GetText() != '' && ctxtSizeDetail_Value.GetText() != '' && cCmbSizeDetail_UOM.GetValue() != "0" && GetObjectID('hiddenedit').value != "" && GetObjectID('hiddenedit').value != "0") {
                if (document.getElementById('hiddenedit2').value == '') {
                    grid2.PerformCallback('savecity~');
                    grid2.Refresh();
                }
                else {
                    grid2.PerformCallback('updatecity~' + GetObjectID('hiddenedit2').value);
                    grid2.Refresh();
                }
            }
            //else if (GetObjectID('hiddenedit').value == "" || GetObjectID('hiddenedit').value == "0") {

               
            //    alert('Please enter size first');
            //}
            //else if (ctxtSizeDetail_AttribName.GetText() == '') {
            //    alert('Please enter size AttribName');
            //    ctxtSizeDetail_AttribName.Focus();
            //}
            //else if (ctxtSizeDetail_Value.GetText() == '') {
            //    alert('Please Enter Size Value');
            //    ctxtSizeDetail_Value.Focus();
            //}
            //else if (cCmbSizeDetail_UOM.GetValue() == '0') {
            //    alert('Please Enter UOM');
            //    cCmbSizeDetail_UOM.Focus();
            //}

        }
        function fn_btnCancel2() {
            cPopup_Empcitys2.Hide();
        }
        function fn_Editcity2(keyValue) {

            grid2.PerformCallback('Edit~' + keyValue);
        }
        function fn_Deletecity2(keyValue) {
            grid2.PerformCallback('Delete~' + keyValue);
            //grid2.Refresh();
        }
        function grid_EndCallBack2() {
            if (grid2.cpinsert != null) {

                if (grid2.cpinsert == 'Success') {
                    //  alert('Inserted Successfully');

                    cPopup_Empcitys2.Hide();
                    grid2.Refresh();
                    grid2.cpinsert = null;
                }
                else {

                    jAlert("Error On Insertion \n 'Please Try Again!!'");
                    cPopup_Empcitys2.Hide();
                }


            }
            if (grid2.cpEdit != null) {

                ctxtSizeDetail_AttribName.SetText(grid2.cpEdit.split('~')[0]);
                ctxtSizeDetail_Value.SetText(grid2.cpEdit.split('~')[1]);

                cCmbSizeDetail_UOM.SetValue(grid2.cpEdit.split('~')[2]);


                GetObjectID('hiddenedit2').value = grid2.cpEdit.split('~')[3];
                cPopup_Empcitys2.Show();
                grid2.cpEdit = null

            }
            if (grid2.cpUpdate != null) {
                if (grid2.cpUpdate == 'Success') {
                    //alert('Update Successfully');
                    cPopup_Empcitys2.Hide();
                    grid2.cpUpdate = null;

                }
                else {
                    jAlert("Error on Updation\n'Please Try again!!'")
                    cPopup_Empcitys2.Hide();
                }
            }
            
            if (grid2.cpUpdateValid != null) {
                if (grid2.cpUpdateValid == "StateInvalid") {
                    //  alert("Please Select proper country state and city");
                    //cPopup_Empcitys2.Show();
                    //cCmbState.Focus();
                    //alert(GetObjectID('<%#hiddenedit.ClientID%>').value);
                    //grid2.PerformCallback('Edit~'+GetObjectID('<%#hiddenedit.ClientID%>').value);
                    //grid2.cpUpdateValid=null;
                }
            }
            if (grid2.cpDelete != null) {
                if (grid2.cpDelete == 'Success')
                {
                   //alert('Deleted Successfully');
                   // grid2.Refresh();
                   // grid2.cpDelete = null;
                    jConfirm('Confirm delete?', 'Confirmation Dialog', function (r) {
                        if (r == true) {
                            grid2.Refresh();
                            grid2.cpDelete = null;
                        }
                    });

                }
                     
                else
                    jAlert("Error on deletion\n'Please Try again!!'")

            }
            
            if (grid2.cpExists != null) {
                if (grid2.cpExists == "Exists") {
                    jAlert('Record already Exists');
                    cPopup_Empcitys2.Hide();
                    grid2.cpExists = null;
                }
                else {
                    jAlert("Error on operation \n 'Please Try again!!'")
                    cPopup_Empcitys2.Hide();
                }
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="breadCumb">
        <span>Strength Schemes</span>
    </div>
    
    <div class="container">
    <div class="backBox mt-5 p-3 ">
        <div class="Main">
            <div class="SearchArea clearfix">
                <div class="FilterSide mb-4">
                    <div style=" padding-right: 5px;">
                         <% if (rights.CanAdd)
                                               { %>
                         <a href="javascript:void(0);" onclick="fn_PopOpen()" class="btn btn-success mr-2"><span>Add New</span> </a><%} %>
                        <% if (rights.CanExport)
                                               { %>
                        <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true"  OnChange="if(!AvailableExportOption()){return false;}">
                            <asp:ListItem Value="0">Export to</asp:ListItem>
                            <asp:ListItem Value="1">PDF</asp:ListItem>
                                <asp:ListItem Value="2">XLS</asp:ListItem>
                                <asp:ListItem Value="3">RTF</asp:ListItem>
                                <asp:ListItem Value="4">CSV</asp:ListItem>
                        </asp:DropDownList>
                         <% } %>
                        <%--<a href="javascript:ShowHideFilter('All');" class="btn btn-primary"><span>
                            All Records</span></a>--%>
                    </div>
                    <%--<div class="ExportSide pull-right">
                        <div>
                           <dxe:ASPxComboBox ID="cmbExport" runat="server" AutoPostBack="true"
                            Font-Bold="False" OnSelectedIndexChanged="cmbExport_SelectedIndexChangedcmb"
                            ValueType="System.Int32" Width="130px">
                            <Items>
                                <dxe:ListEditItem Text="Select" Value="0" />
                                <dxe:ListEditItem Text="PDF" Value="1" />
                                <dxe:ListEditItem Text="XLS" Value="2" />
                                <dxe:ListEditItem Text="RTF" Value="3" />
                                <dxe:ListEditItem Text="CSV" Value="4" />
                            </Items>
                           <%-- <ButtonStyle BackColor="#C0C0FF" ForeColor="Black">
                            </ButtonStyle> 
                            <Border BorderColor="Black" />
                            <DropDownButton Text="Export">
                            </DropDownButton>
                        </dxe:ASPxComboBox>
                        </div>
                    </div>--%>
                </div>
            </div>
        <div class="GridViewArea">
            <dxe:ASPxGridView ID="cityGrid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
                KeyFieldName="Size_ID" Width="100%" OnHtmlRowCreated="cityGrid_HtmlRowCreated"
                OnHtmlEditFormCreated="cityGrid_HtmlEditFormCreated" OnCustomCallback="cityGrid_CustomCallback" SettingsBehavior-AllowFocusedRow="true">
                <SettingsSearchPanel Visible="True" />
                <Settings ShowGroupPanel="True" ShowStatusBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="True" />
                <Columns>
                    <dxe:GridViewDataTextColumn Caption="Size_ID" FieldName="Size_ID" ReadOnly="True"
                        Visible="False" FixedStyle="Left" VisibleIndex="0">
                        <EditFormSettings Visible="False" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn Caption="Strength Schemes" Width="40%" FieldName="Size_Name"
                        FixedStyle="Left" Visible="True" VisibleIndex="1">
                        <EditFormSettings Visible="True" />
                    </dxe:GridViewDataTextColumn>
                    <dxe:GridViewDataTextColumn Caption="Description" FieldName="Size_Description"
                        FixedStyle="Left" Visible="True" Width="40%" VisibleIndex="2">
                        <EditFormSettings Visible="True" />
                    </dxe:GridViewDataTextColumn>
                   <%-- <dxe:GridViewDataTextColumn VisibleIndex="3" ReadOnly="True" Width="15%">
                        <HeaderTemplate>
                            <asp:Label ID="lbltxt" runat="server">More Information</asp:Label>
                        </HeaderTemplate>
                        <DataItemTemplate>
                            
                        </DataItemTemplate>
                    </dxe:GridViewDataTextColumn>--%>
                    <dxe:GridViewDataTextColumn VisibleIndex="3" ReadOnly="True" Width="6%" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
                        <HeaderTemplate>
                          <%--  <%if (rights.CanAdd)
                              {%>--%>
                           <%-- <a href="javascript:void(0);" onclick="fn_PopOpen()"><span style="text-decoration: underline;color:white;">Add New</span> </a>--%>
                           <%-- <%}
                              else
                              { %>
                            <span style="text-decoration: underline;color:white;">Action</span>
                            <%} %>--%>
                            <span>Actions</span>
                        </HeaderTemplate>
                        <DataItemTemplate>
                               <% if (rights.CanEdit)
                                               { %>
                            <a href="javascript:void(0);" onclick="fn_Editcity('<%# Container.KeyValue %>')" class="pad" title="Edit">
                                 <img src="../../../../assests/images/Edit.png"  title="Edit"/> <%--<img src="../../../../assests/images/info.png" />--%>
                            </a><%} %>
                            <% if (rights.CanDelete)
                               { %>
                            <a href="javascript:void(0);" onclick="fn_Deletecity('<%# Container.KeyValue %>')" title="Delete">
                                <img src="../../../../assests/images/Delete.png" title="Delete" />
                            </a>
                            <%} %>
                        </DataItemTemplate>
                    </dxe:GridViewDataTextColumn>
                </Columns>
                <ClientSideEvents EndCallback="function (s, e) {grid_EndCallBack();}" />
            </dxe:ASPxGridView>
        </div>
        <div class="PopUpArea">
            <div>
                <%--Rev 1.0 : popup height change--%>
                <dxe:ASPxPopupControl ID="Popup_Empcitys" runat="server" ClientInstanceName="cPopup_Empcitys" Height="220px"
                    Width="450px" HeaderText="Add/Modify Strength Schemes" PopupHorizontalAlign="WindowCenter" BackColor="white"
                    PopupVerticalAlign="TopSides" CloseAction="CloseButton" Modal="True" ContentStyle-VerticalAlign="Top"
                    EnableHierarchyRecreation="True" ContentStyle-CssClass="pad" OnLoad="Popup_Empcitys_Onload" >
                    <ContentStyle VerticalAlign="Top" CssClass="pad" >
                    </ContentStyle>
                    <ContentCollection>
                        <dxe:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <div class="Top" >
                                <table style="margin:0px auto;">
                                    <tr>
                                        <td>
                                          Strength <span style="color:red"> *</span>
                                        </td>
                                        <td>
                                            <dxe:ASPxTextBox ID="txtSize_Name" ClientInstanceName="ctxtSize_Name" runat="server"  MaxLength="30"
                                            Width="290px">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                                <RequiredField IsRequired="true" ErrorText="Mandatory" />
                                            </ValidationSettings>  
                                            <ClientSideEvents TextChanged="function (s, e) {fn_ctxtSize_Name_TextChanged();}" />
                                        </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                             Description<span style="color:red;"> *</span>
                                        </td>
                                        <td>
                                            <div class="Left_Content mt-3" style="display: inline-block">
                                        <dxe:ASPxMemo ID="txtSize_Description" ClientInstanceName="ctxtSize_Description" MaxLength="150"
                                            runat="server" Width="290px" Height="60px" Text='<%# Bind("Size_Description") %>'>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                                <RequiredField IsRequired="true" ErrorText="Mandatory" />
                                            </ValidationSettings>  
                                        </dxe:ASPxMemo>
                                    </div>
                                        </td>
                                    </tr>
                                </table>
                                <%--<div>
                                    <div class="cityDiv" style="display: inline-block; height: auto; margin-left: 40px;">
                                        Name
                                    </div>
                                    <div class="Left_Content" style="display: inline-block">
                                        
                                    </div>
                                </div>
                                <div>
                                    <div class="cityDiv" style="display: inline-block; height: auto; margin-left: 40px;">
                                        Description
                                    </div>
                                    
                                </div>--%>
                            </div>
                            <div class="ContentDiv" style="height: auto">
                                <div style="display: none">
                                    <div style="height: 20px; width: 280px; background-color: Gray; padding-left: 120px;">
                                        <h5>
                                            Static Code</h5>
                                    </div>
                                    <div style="height: 20px; width: 130px; padding-left: 70px; background-color: Gray;
                                        float: left;">
                                        Exchange</div>
                                    <div style="height: 20px; width: 200px; background-color: Gray; text-align: left;">
                                        Value</div>
                                    <div class="ScrollDiv">
                                        <div class="cityDiv" style="padding-top: 5px;">
                                            NSE Code</div>
                                        <div style="padding-top: 5px;">
                                            <dxe:ASPxTextBox ID="txtNseCode" ClientInstanceName="ctxtNseCode" runat="server"
                                                CssClass="cityTextbox">
                                            </dxe:ASPxTextBox>
                                        </div>
                                        <br style="clear: both;" />
                                        <div class="cityDiv">
                                            BSE Code</div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtBseCode" ClientInstanceName="ctxtBseCode" runat="server"
                                                CssClass="cityTextbox">
                                            </dxe:ASPxTextBox>
                                        </div>
                                        <br style="clear: both;" />
                                        <div class="cityDiv">
                                            MCX Code</div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtMcxCode" ClientInstanceName="ctxtMcxCode" runat="server"
                                                CssClass="cityTextbox">
                                            </dxe:ASPxTextBox>
                                        </div>
                                        <br style="clear: both;" />
                                        <div class="cityDiv">
                                            MCXSX Code</div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtMcsxCode" ClientInstanceName="ctxtMcsxCode" runat="server"
                                                CssClass="cityTextbox">
                                            </dxe:ASPxTextBox>
                                        </div>
                                        <br style="clear: both;" />
                                        <div class="cityDiv">
                                            NCDEX Code</div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtNcdexCode" ClientInstanceName="ctxtNcdexCode" runat="server"
                                                CssClass="cityTextbox">
                                            </dxe:ASPxTextBox>
                                        </div>
                                        <br style="clear: both;" />
                                        <div class="cityDiv">
                                            CDSL Code</div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtCdslCode" ClientInstanceName="ctxtCdslCode" CssClass="cityTextbox"
                                                runat="server">
                                            </dxe:ASPxTextBox>
                                        </div>
                                        <br style="clear: both;" />
                                        <div class="cityDiv">
                                            NSDL Code</div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtNsdlCode" ClientInstanceName="ctxtNsdlCode" CssClass="cityTextbox"
                                                runat="server">
                                            </dxe:ASPxTextBox>
                                        </div>
                                        <br style="clear: both;" />
                                        <div class="cityDiv">
                                            NDML Code</div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtNdmlCode" ClientInstanceName="ctxtNdmlCode" runat="server"
                                                CssClass="cityTextbox">
                                            </dxe:ASPxTextBox>
                                        </div>
                                        <br style="clear: both;" />
                                        <div class="cityDiv">
                                            CVL Code</div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtCvlCode" ClientInstanceName="ctxtCvlCode" runat="server"
                                                CssClass="cityTextbox">
                                            </dxe:ASPxTextBox>
                                        </div>
                                        <br style="clear: both;" />
                                        <div class="cityDiv">
                                            DOTEX Code</div>
                                        <div>
                                            <dxe:ASPxTextBox ID="txtDotexCode" ClientInstanceName="ctxtDotexCode" runat="server"
                                                CssClass="cityTextbox">
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </div>
                                </div>
                                <br style="clear: both;" />
                                <div class="Footer">
                                    <div style="margin-left: 101px; width: 70px; float: left;margin-right: 10px;">
                                        <dxe:ASPxButton ID="btnSave_citys" ClientInstanceName="cbtnSave_citys" runat="server"
                                            AutoPostBack="False" Text="Save" CssClass="btn btn-primary">
                                            <ClientSideEvents Click="function (s, e) {btnSave_citys();}" />
                                        </dxe:ASPxButton>
                                    </div>
                                    <div style="">
                                        <dxe:ASPxButton ID="btnCancel_citys" runat="server" AutoPostBack="False" Text="Close" CssClass="btn btn-danger">
                                            <ClientSideEvents Click="function (s, e) {fn_btnCancel();}" />
                                        </dxe:ASPxButton>
                                    </div>
                                    <br style="clear: both;" />
                                </div>
                                <br style="clear: both;" />
                                <div id="innergrid">
                                    <div class="GridViewArea">
                                        <div id="Popup_AddNew">
                                            <%if(rights.CanAdd)
                                              { %>
                                                            <a class="btn btn-primary" href="javascript:void(0);" onclick="fn_PopOpen2()" style="display:none;"><span
                                                                text-decoration: underline">Add New</span> </a>
                                            <% } %>
                                                        </div>
                                        <dxe:ASPxGridView ID="cityGrid2" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid2" Visible="false"
                                            KeyFieldName="SizeDetail_ID" Width="100%" OnHtmlRowCreated="cityGrid_HtmlRowCreated2"
                                            OnHtmlEditFormCreated="cityGrid_HtmlEditFormCreated2" OnCustomCallback="cityGrid_CustomCallback2">
                                            <Settings ShowGroupPanel="True" ShowStatusBar="Visible" />
                                            <Columns>
                                                <dxe:GridViewDataTextColumn Caption="SizeDetail_ID" FieldName="SizeDetail_ID" ReadOnly="True"
                                                    Visible="False" FixedStyle="Left" VisibleIndex="0">
                                                    <EditFormSettings Visible="False" />
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn Caption="SizeDetail_MainID" FieldName="SizeDetail_MainID"
                                                    ReadOnly="True" Visible="False" FixedStyle="Left" VisibleIndex="0">
                                                    <EditFormSettings Visible="False" />
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn Caption="SizeDetail_UOM" FieldName="SizeDetail_UOM"
                                                    ReadOnly="True" Visible="False" FixedStyle="Left" VisibleIndex="1">
                                                    <EditFormSettings Visible="False" />
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn Caption="SizeDetail_CreateUser" FieldName="SizeDetail_CreateUser"
                                                    ReadOnly="True" Visible="False" FixedStyle="Left" VisibleIndex="2">
                                                    <EditFormSettings Visible="False" />
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn Caption="Name" FieldName="SizeDetail_AttribName" Width="12%"
                                                    FixedStyle="Left" Visible="True" VisibleIndex="3">
                                                    <EditFormSettings Visible="True" />
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn Caption="Value" FieldName="SizeDetail_Value" Width="12%"
                                                    FixedStyle="Left" Visible="True" VisibleIndex="4">
                                                    <EditFormSettings Visible="True" />
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn Caption="UOM" FieldName="UOM_Name" Width="12%" FixedStyle="Left"
                                                    Visible="True" VisibleIndex="5">
                                                    <EditFormSettings Visible="True" />
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn ReadOnly="True" Width="9%" Caption="Actions">
                                                    <HeaderTemplate>
                                                        Actions
                                                        <%--<div id="Popup_AddNew">
                                                            <a href="javascript:void(0);" onclick="fn_PopOpen2()"><span
                                                                text-decoration: underline">Add New</span> </a>
                                                        </div>--%>
                                                    </HeaderTemplate>
                                                    <DataItemTemplate>
                                                        <% if(rights.CanEdit)
                                                           { %>
                                                        <a href="javascript:void(0);" onclick="fn_Editcity2('<%# Container.KeyValue %>')" >
                                                             <img src="../../../../assests/images/Edit.png" /></a>
                                                        <% } %>
                                                        <% if(rights.CanDelete)
                                                           { %>
                                                        <a href="javascript:void(0);" onclick="fn_Deletecity2('<%# Container.KeyValue %>')">
                                                           <img src="../../../../assests/images/Delete.png" /></a>
                                                        <% } %>
                                                    </DataItemTemplate>
                                                </dxe:GridViewDataTextColumn>
                                            </Columns>

                                            <ClientSideEvents EndCallback="function (s, e) {grid_EndCallBack2();}" />
                                        </dxe:ASPxGridView>

                                    </div>
                                    <div class="PopUpArea">
                                        <dxe:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ClientInstanceName="cPopup_Empcitys2"
                                            Width="400px" HeaderText="Add/Modify Details" PopupHorizontalAlign="WindowCenter" BackColor="white"
                                            PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" Modal="True" ContentStyle-VerticalAlign="Top"
                                            EnableHierarchyRecreation="True" ContentStyle-CssClass="pad">
                                            <ContentCollection>
                                                <dxe:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                                    <div class="Top">
                                                        <div style="margin-bottom:5px;">
                                                            <div class="cityDiv" style="display: inline-block; height: auto; margin-left: 40px;vertical-align:top; text-align:left">
                                                                Name:
                                                                <%--<span style="color:red;"> *</span>--%>
                                                            </div>
                                                            <div class="Left_Content" style="display: inline-block">
                                                                <dxe:ASPxTextBox ID="txtSizeDetail_AttribName" ClientInstanceName="ctxtSizeDetail_AttribName" MaxLength="30"
                                                                    runat="server" Width="235px">
                                                                     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                        <RequiredField  IsRequired="True" />
                                    </ValidationSettings>
                                                                </dxe:ASPxTextBox>
                                                            </div>
                                                        </div>
                                                        <div style="margin-bottom:5px;">
                                                            <div class="cityDiv" style="display: inline-block; height: auto; margin-left: 40px; vertical-align:top; text-align:left">
                                                                Value:
                                                                <%--<span style="color:red;"> *</span>--%>
                                                            </div>
                                                            <div class="Left_Content" style="display: inline-block;">
                                                                <dxe:ASPxTextBox ID="txtSizeDetail_Value" ClientInstanceName="ctxtSizeDetail_Value" MaxLength="30"
                                                                    runat="server" Width="235px">
                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                        <RequiredField  IsRequired="True" />
                                    </ValidationSettings>
                                                                </dxe:ASPxTextBox>
                                                            </div>
                                                        </div>
                                                        <div style="margin-bottom:5px;">
                                                            <div class="cityDiv" style="display: inline-block; height: auto; margin-left: 40px;vertical-align:top;; text-align:left">
                                                                UOM:
                                                                <%--<span style="color:red;"> *</span>--%>
                                                            </div>
                                                            <div class="Left_Content" style="display: inline-block;">
                                                                <dxe:ASPxComboBox ID="CmbSizeDetail_UOM" ClientInstanceName="cCmbSizeDetail_UOM"
                                                                    runat="server" ValueType="System.String" Width="235px" EnableSynchronization="True"
                                                                    EnableIncrementalFiltering="True">
                                                                     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                        <RequiredField  IsRequired="True" />
                                    </ValidationSettings>
                                                                </dxe:ASPxComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ContentDiv" style="height: auto">
                                                        <div style="display: none">
                                                            <div style="height: 20px; width: 280px; background-color: Gray; padding-left: 120px;">
                                                                <h5>
                                                                    Static Code</h5>
                                                            </div>
                                                            <div style="height: 20px; width: 130px; padding-left: 70px; background-color: Gray;
                                                                float: left;">
                                                                Exchange</div>
                                                            <div style="height: 20px; width: 200px; background-color: Gray; text-align: left;">
                                                                Value</div>
                                                            <div class="ScrollDiv">
                                                                <div class="cityDiv" style="padding-top: 5px;">
                                                                    NSE Code</div>
                                                                <div style="padding-top: 5px;">
                                                                    <dxe:ASPxTextBox ID="ASPxTextBox1" ClientInstanceName="ctxtNseCode" runat="server"
                                                                        CssClass="cityTextbox">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                                <br style="clear: both;" />
                                                                <div class="cityDiv">
                                                                    BSE Code</div>
                                                                <div>
                                                                    <dxe:ASPxTextBox ID="ASPxTextBox2" ClientInstanceName="ctxtBseCode" runat="server"
                                                                        CssClass="cityTextbox">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                                <br style="clear: both;" />
                                                                <div class="cityDiv">
                                                                    MCX Code</div>
                                                                <div>
                                                                    <dxe:ASPxTextBox ID="ASPxTextBox3" ClientInstanceName="ctxtMcxCode" runat="server"
                                                                        CssClass="cityTextbox">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                                <br style="clear: both;" />
                                                                <div class="cityDiv">
                                                                    MCXSX Code</div>
                                                                <div>
                                                                    <dxe:ASPxTextBox ID="ASPxTextBox4" ClientInstanceName="ctxtMcsxCode" runat="server"
                                                                        CssClass="cityTextbox">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                                <br style="clear: both;" />
                                                                <div class="cityDiv">
                                                                    NCDEX Code</div>
                                                                <div>
                                                                    <dxe:ASPxTextBox ID="ASPxTextBox5" ClientInstanceName="ctxtNcdexCode" runat="server"
                                                                        CssClass="cityTextbox">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                                <br style="clear: both;" />
                                                                <div class="cityDiv">
                                                                    CDSL Code</div>
                                                                <div>
                                                                    <dxe:ASPxTextBox ID="ASPxTextBox6" ClientInstanceName="ctxtCdslCode" CssClass="cityTextbox"
                                                                        runat="server">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                                <br style="clear: both;" />
                                                                <div class="cityDiv">
                                                                    NSDL Code</div>
                                                                <div>
                                                                    <dxe:ASPxTextBox ID="ASPxTextBox7" ClientInstanceName="ctxtNsdlCode" CssClass="cityTextbox"
                                                                        runat="server">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                                <br style="clear: both;" />
                                                                <div class="cityDiv">
                                                                    NDML Code</div>
                                                                <div>
                                                                    <dxe:ASPxTextBox ID="ASPxTextBox8" ClientInstanceName="ctxtNdmlCode" runat="server"
                                                                        CssClass="cityTextbox">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                                <br style="clear: both;" />
                                                                <div class="cityDiv">
                                                                    CVL Code</div>
                                                                <div>
                                                                    <dxe:ASPxTextBox ID="ASPxTextBox9" ClientInstanceName="ctxtCvlCode" runat="server"
                                                                        CssClass="cityTextbox">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                                <br style="clear: both;" />
                                                                <div class="cityDiv">
                                                                    DOTEX Code</div>
                                                                <div>
                                                                    <dxe:ASPxTextBox ID="ASPxTextBox10" ClientInstanceName="ctxtDotexCode" runat="server"
                                                                        CssClass="cityTextbox">
                                                                    </dxe:ASPxTextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br style="clear: both;" />
                                                        <div class="Footer">
                                                            <div style="margin-left: 100px; width: 70px; float: left;margin-right:20px;">
                                                                <dxe:ASPxButton ID="ASPxButton1" ClientInstanceName="cbtnSave_citys2" runat="server"
                                                                    AutoPostBack="False" Text="Save" CssClass="btn btn-primary">
                                                                    <ClientSideEvents Click="function (s, e) {btnSave_citys2();}" />
                                                                </dxe:ASPxButton>
                                                            </div>
                                                            <div style="margin-left:10px;">
                                                                <dxe:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" CssClass="btn btn-danger" Text="Cancel">
                                                                    <ClientSideEvents Click="function (s, e) {fn_btnCancel2();}" />
                                                                </dxe:ASPxButton>
                                                            </div>
                                                            <br style="clear: both;" />
                                                        </div>
                                                        <br style="clear: both;" />
                                                    </div>
                                                    <%-- </div>--%>
                                                </dxe:PopupControlContentControl>
                                            </ContentCollection>
                                            <HeaderStyle BackColor="LightGray" ForeColor="Black" />
                                        </dxe:ASPxPopupControl>
                                        <dxe:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
                                        </dxe:ASPxGridViewExporter>
                                    </div>
                                </div>
                            </div>
                            <%-- </div>--%>
                        </dxe:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle BackColor="LightGray" ForeColor="Black" />
                </dxe:ASPxPopupControl>
                <dxe:ASPxGridViewExporter ID="exporter" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true"  >
                </dxe:ASPxGridViewExporter>
            </div>
        </div>
       
        <div class="HiddenFieldArea" style="display: none;">
            <asp:HiddenField runat="server" ID="hiddenedit" ClientIDMode="Static" Value="" />
            <asp:HiddenField runat="server" ID="hiddenedit2" Value="" />
          
        </div>
        </div>
       
    </div>
    </div>
</asp:Content>
