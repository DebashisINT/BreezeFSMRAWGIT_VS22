<%--******************************************************************************************************
 * Rev 1.0      Sanchita/Pallab    07/02/2023      V2.0.36     FSM Employee & User Master - To implement Show button. refer: 25641
   Rev 2.0      Pallab             08/02/2023      V2.0.36     Master module design modification. refer: 25656 
   Rev 3.0      Sanchita/Pallab    15/02/2023      V2.0.39     A setting required for Employee and User Master module in FSM Portal. 
   Rev 4.0      Priti              20/02/2023      V2.0.39     0025676: Employee Import Facility. Refer: 25668 
   Rev 5.0      Pallab             20/02/2023      V2.0.39     parameters and grid issue fix for small screen
   Rev 6.0      Pallab             17/04/2023      V2.0.39     25840: Employee master module employee search popup auto focus add and "cancel" button color change
   Rev 7.0      Pallab             27/06/2023      V2.0.41     26442: Employees module responsive issue fix and make mobile friendly
   Rev 8.0      Pallab             11/07/2023      V2.0.42     26551: Employees module parameter break issue fix for small device
   Rev 9.0      Pallab             02/08/2023      V2.0.42     26656: "View Log" popup loader and page size options showing outside in the popup issue fix
   Rev 10.0     Sanchita           08/08/2023      V2.0.42     FSM - Masters - Organization - Employees - Change Supervisor should be On Demand Search. Mantis: 26700  
   Rev 11.0     Sanchita           09/08/2023      V2.0.42     FSM Portal - Enhance the Export to excel in Employee Master. Mantis : 26708 
   Rev 12.0     Sanchita           25/10/2024      V2.0.49     In employee master a new filed is required as Target Level.. Mantis : 27773    
 *******************************************************************************************************--%>

<%@ Page Title="Employee" Language="C#" AutoEventWireup="True" Inherits="ERP.OMS.Management.Master.management_master_Employee" CodeBehind="Employee.aspx.cs" MasterPageFile="~/OMS/MasterPage/ERP.Master" %>
 <%--Mantise ID:0024752: Optimize FSM Employee Master
      Rev work Swati Date:-15.03.2022--%>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<%--Mantise ID:0024752: Optimize FSM Employee Master
      Rev work close Swati Date:-15.03.2022--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Rev 3.0--%>
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>

    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <link href="../../../assests/css/custom/SearchPopup.css" rel="stylesheet" />
    <script src="../../../Scripts/SearchPopup.js"></script>
    <script src="../../../Scripts/SearchMultiPopup.js"></script>
    <%--End of Rev 3.0--%>

    <style>
        .branch-list-modal .modal-header
      {
        background: #074270;
            padding: 10px 15px;
      }
      .branch-list-modal .modal-header h4
      {
        color: #fff;
        line-height: 1;
        font-size: 16px;
      }
      .branch-list-modal .modal-header button span
      {
        color: #fff;
            font-size: 28px;
    font-weight: 300;
    line-height: 21px;
      }

      .branch-list-modal .modal-content
      {
        border-radius: 15px;
            border: none;
            box-shadow: 1px 1px 15px #11111145;
      }
      .branch-list-modal .modal-header
      {
        border-top-left-radius: 15px;
    border-top-right-radius: 15px;
      }

      .branch-list-modal .modal-body .input-group
      {
          width: 100%;
      }

      .branch-list-modal .modal-body .input-group-text
      {
        background-color: transparent;
        border: none;
            color: #a5a5a5;
      }
      .branch-list-modal .modal-body .input-group-prepend
      {
        position: absolute;
        z-index: 1;
        min-height: 33px;
        line-height: 33px;
        width: 30px;
        text-align: center;
      }

      .branch-list-modal .modal-body .form-control
      {
            padding-left: 40px;
            background: transparent !important;
            border-radius: 5px !important;
            border-color: #eaeaea;
            transition: all .4s;
            color: #111;
      }

      .branch-list-modal .modal-body .form-control:focus
      {
        box-shadow: none;
        border-color: #0a4f85;
      }

      .custom-checkbox-single {
          display: block;
          position: relative;
          padding-left: 34px;
          margin-bottom: 15px;
          cursor: pointer;
          font-size: 16px;
          -webkit-user-select: none;
          -moz-user-select: none;
          -ms-user-select: none;
          user-select: none;
          line-height: 22px;
              font-weight: 500;
        }

        /* Hide the browser's default checkbox */
        .custom-checkbox-single input {
          position: absolute;
          opacity: 0;
          cursor: pointer;
          height: 0;
          width: 0;
        }

        /* Create a custom checkbox */
        .checkmark {
          position: absolute;
          top: 0;
          left: 0;
          height: 21px;
          width: 21px;
          background-color: #fff;
    border-radius: 4px;
    border: 1px solid #6a6a6a;
        }

        /* On mouse-over, add a grey background color */
        .custom-checkbox-single:hover input ~ .checkmark {
          background-color: #1541a4;
        }

        /* When the checkbox is checked, add a blue background */
        .custom-checkbox-single input:checked ~ .checkmark {
          background-color: #1541a4;
        }

        /* Create the checkmark/indicator (hidden when not checked) */
        .checkmark:after {
          content: "";
          position: absolute;
          display: none;
        }

        /* Show the checkmark when checked */
        .custom-checkbox-single input:checked ~ .checkmark:after {
          display: block;
        }

        /* Style the checkmark/indicator */
        .custom-checkbox-single .checkmark:after {
              left: 6px;
            top: 1px;
            width: 7px;
            height: 12px;
            border: solid white;
            border-width: 0 3px 3px 0;
            -webkit-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            transform: rotate(45deg);
        }

        /*Rev 3.0*/
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

        .multiselect.dropdown-toggle {
        text-align: left;
        }

        .multiselect.dropdown-toggle, #ddlMonth, #ddlYear {
            -webkit-appearance: none;
            position: relative;
            z-index: 1;
            background-color: transparent;
        }

        .dynamicPopupTbl {
        font-family: 'Poppins', sans-serif !important;
        }

            .dynamicPopupTbl > tbody > tr > td,
            #EmployeeTable table tr th {
                font-family: 'Poppins', sans-serif !important;
                font-size: 12px;
            }

            /*Rev 10.0*/
            #EmployeefromsuperTable table tr th {
                font-family: 'Poppins', sans-serif !important;
                font-size: 12px;
            }
            #EmployeetosupervisorTable table tr th {
                font-family: 'Poppins', sans-serif !important;
                font-size: 12px;
            }
            /*End of Rev 10.0*/
        /*End of Rev 3.0*/
    </style>
    <script type="text/javascript">
      //REV 4.0
      function ShowLogData(haslog) {           
            $('#btnViewLog').click();           
        }
        function ViewLogData() {
            cGvImportDetailsSearch.Refresh();
        }
       //REV 4.0 END
    </script>
    <script type="text/javascript">

        function Pageload() {

            document.getElementById('td1').style.display = "inline";
            document.getElementById('td1').style.display = "tablee-cell";
            document.getElementById('td2').style.display = "inline";
            document.getElementById('td2').style.display = "table-cell";
            document.getElementById('td3').style.display = "inline";
            document.getElementById('td3').style.display = "table-cell";
            document.getElementById('td4').style.display = "inline";
            document.getElementById('td4').style.display = "table-cell";
            HideTrTd("Tr_EmployeeName");
            HideTrTd("Tr_EmployeeCode");

            // Rev 1.0
            //cGrdEmployee.PerformCallback("Show~~~");
            $("#hfIsFilter").val("N");
            // End of Rev 1.0
        }
        function PerformCallToGridBind() {
            var EmpId = document.getElementById('hdnContactId').value;
            cSelectPanel.PerformCallback('SaveDetails~' + EmpId);
            cActivationPopup.Hide();
            return false;
        }

        function SelectPanel_EndCallBack(s, e) {
            if (cSelectPanel.cpResult == "Success") {
                jAlert('Employee Updated Sucessfully');
            }
            else if (cSelectPanel.cpResult == "Problem") {
                jAlert('Some Problem Occur. Please Try Again Later');
            }
            else {

            }
        }


        function OnChangeSuperVisor() {

            // Rev 10.0
            $("#txtEmployeefromsuper_hidden").val("");
            ctxtEmployeefromsuper.SetText("");

            $("#txtEmployeetosupervisor_hidden").val("");
            ctxtEmployeetosupervisor.SetText("");
            // End of Rev 10.0

            cActivationPopupsupervisor.Show();

        }
        function OnserverCallSupervisorchanged() {
            var otherDetails = {};
            // Rev 10.0
            //otherDetails.fromsuper = $("#fromsuper").val();
            //otherDetails.tosuper = $("#tosupervisor").val();

            otherDetails.fromsuper = $("#txtEmployeefromsuper_hidden").val();
            otherDetails.tosuper = $("#txtEmployeetosupervisor_hidden").val();
            // End of Rev 10.0

            //{ 'fromsuper': $("#fromsuper").val(), 'tosuper': $("#tosupervisor").val() }

            $.ajax({
                type: "POST",
                url: "Employee.aspx/Submitsupervisor",
                data: JSON.stringify(otherDetails),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    //  alert(responseFromServer.d)
                    // Rev 1.0
                    //cGrdEmployee.PerformCallback("Show~~~");
                    // End of Rev 1.0
                    jAlert('Supervisor Changed Successfully.');
                    cActivationPopupsupervisor.Hide();
                }
            });

        }
        // Rev 1.0
        function ShowData() {
            $("#hfIsFilter").val("Y");
            cGrdEmployee.PerformCallback("Show~~~");
        }
        // End of Rev 1.0


        var GlobalCheckOption = '';
        function ActivateDeactivateTxtReason(s, e) {

            var value = s.GetChecked();





            if (GlobalCheckOption == "") {
                if (value) {
                    GlobalCheckOption = false;
                }
                else {
                    GlobalCheckOption = true;
                }
                alert(value);
            }


            if (GlobalCheckOption != value) {
                $("#btnOK").attr("disabled", false);
            } else {
                $("#btnOK").attr("disabled", true);
            }

        }


        function fn_DeleteEmp(keyValue) {
            //var result=confirm('Confirm delete?');
            //if(result)
            //{
            //    grid.PerformCallback('Delete~' + keyValue);
            //}

            if (keyValue != "EMV0000001") {
                jConfirm('Confirm delete?', 'Confirmation Dialog', function (r) {
                    if (r == true) {
                        cGrdEmployee.PerformCallback('Delete~' + keyValue);
                    }
                    else {
                        return false;
                    }
                });
            } else {
                jAlert("Sorry, you can not delete the Admin.");
            }


        }

        function fn_DeActivateEmp(keyValue) {



            if (keyValue) {

                cSelectPanel.PerformCallback('Bindalldesignes~' + keyValue)
            }
            cActivationPopup.Show();
            document.getElementById('hdnContactId').value = keyValue;

        }


        function ShowTrTd(obj) {
            document.getElementById(obj).style.display = 'inline';
        }
        function HideTrTd(obj) {
            //document.getElementById(obj).style.display = 'none';
        }

        function OnMoreInfoClick(keyValue) {

            if (keyValue != '') {
                var url = 'employee_general.aspx?id=' + keyValue;
                //parent.OnMoreInfoClick(url, "Modify Employee Details", '980px', '500px', "Y");
                window.location.href = url;
            }
        }
        function OnAddButtonClick() {
            var url = 'Employee_AddNew.aspx?id=' + 'ADD';
            //parent.OnMoreInfoClick(url,"Add Employee Details",'980px','400px',"Y");
            window.location.href = url;
        }
        <%--Rev work start 26.04.2022 0024853: Copy feature add in Employee master--%>
        function OnCopyInfoClick(keyValue) {
            if (keyValue != '') {
                var url = 'Employee_AddNew.aspx?id=' + keyValue+'&Mode=Copy';
                window.location.href = url;
            }
        }
        <%--Rev work close 26.04.2022 0024853: Copy feature add in Employee master--%>
        function OnAddBusinessClick(keyValue, CompName) {
            var url = 'AssignIndustry.aspx?id1=' + keyValue + '&EntType=Employee';
            window.location.href = url;
        }
        function OnLeftNav_Click() {
            var i = document.getElementById("A1").innerText;
            document.getElementById("hdn_GridBindOrNotBind").value = "False"; //To Stop Bind On Page Load
            if (parseInt(i) > 1) {
                if (crbDOJ_Specific_All.GetValue() == "S")
                    cGrdEmployee.PerformCallback("SearchByNavigation~" + cDtFrom.GetValue() + '~' + cDtTo.GetValue() + "~" + document.getElementById("A1").innerText + "~LeftNav");
                else
                    cGrdEmployee.PerformCallback("SearchByNavigation~~~" + document.getElementById("A1").innerText + "~LeftNav");
            }
            else {
                alert('No More Pages.');
            }
        }
        function OnRightNav_Click() {
            var TestEnd = document.getElementById("A10").innerText;
            document.getElementById("hdn_GridBindOrNotBind").value = "False"; //To Stop Bind On Page Load
            var TotalPage = document.getElementById("B_TotalPage").innerText;
            if (TestEnd == "" || TestEnd == TotalPage) {
                alert('No More Records.');
                return;
            }
            var i = document.getElementById("A1").innerText;
            if (parseInt(i) < TotalPage) {
                if (crbDOJ_Specific_All.GetValue() == "S")
                    cGrdEmployee.PerformCallback("SearchByNavigation~" + cDtFrom.GetValue() + '~' + cDtTo.GetValue() + "~" + document.getElementById("A1").innerText + "~RightNav");
                else
                    cGrdEmployee.PerformCallback("SearchByNavigation~~~" + document.getElementById("A1").innerText + "~RightNav");
            }
            else {
                alert('You are at the End');
            }
        }
        function OnPageNo_Click(obj) {
            var i = document.getElementById(obj).innerText;
            document.getElementById("hdn_GridBindOrNotBind").value = "False"; //To Stop Bind On Page Load
            if (crbDOJ_Specific_All.GetValue() == "S")
                cGrdEmployee.PerformCallback("SearchByNavigation~" + cDtFrom.GetValue() + '~' + cDtTo.GetValue() + "~" + i + "~PageNav");
            else
                cGrdEmployee.PerformCallback("SearchByNavigation~~~" + i + "~PageNav");

        }
        function BtnShow_Click() {
            document.getElementById("hdn_GridBindOrNotBind").value = "False"; //To Stop Bind On Page Load
            //if (crbDOJ_Specific_All.GetValue() == "S") {

            //    cGrdEmployee.PerformCallback("Show~" + cDtFrom.GetValue() + '~' + cDtTo.GetValue());
            //}
            //else {

            //    cGrdEmployee.PerformCallback("Show~~~");
            //}
        }
        function GrdEmployee1_EndCallBack() {

            if (cGrdEmployee.cpDelete != null) {
                if (cGrdEmployee.cpDelete == 'Success') {
                    jAlert('Record deleted successfully');
                    // Mantis Issue 24752_Rectify
                    cGrdEmployee.Refresh();
                    // End of Mantis Issue 24752_Rectify
                }
                else if (cGrdEmployee.cpDelete == 'Failure')
                    jAlert('Cannot Delete.This employee has been tagged in User Master.')
            }
            cGrdEmployee.cpDelete = null;
            // Mantis Issue 24752_Rectify
            if (cGrdEmployee.cpLoadData != null) {
                if (cGrdEmployee.cpLoadData == 'Success') {
                    cGrdEmployee.Refresh();
                }
            }
            cGrdEmployee.cpLoadData = null;
            // End of Mantis Issue 24752_Rectify
            
        }



        function GrdEmployee_EndCallBack() {
            if (cGrdEmployee.cpExcelExport != undefined) {
                document.getElementById('BtnForExportEvent').click();
            }
            if (cGrdEmployee.cpRefreshNavPanel != undefined) {
                document.getElementById("B_PageNo").innerText = '';
                document.getElementById("B_TotalPage").innerText = '';
                document.getElementById("B_TotalRows").innerText = '';

                var NavDirection = cGrdEmployee.cpRefreshNavPanel.split('~')[0];
                var PageNum = cGrdEmployee.cpRefreshNavPanel.split('~')[1];
                var TotalPage = cGrdEmployee.cpRefreshNavPanel.split('~')[2];
                var TotalRows = cGrdEmployee.cpRefreshNavPanel.split('~')[3];

                if (NavDirection == "RightNav") {
                    PageNum = parseInt(PageNum) + 10;
                    document.getElementById("B_PageNo").innerText = PageNum;
                    document.getElementById("B_TotalPage").innerText = TotalPage;
                    document.getElementById("B_TotalRows").innerText = TotalRows;
                    var n = parseInt(TotalPage) - parseInt(PageNum) > 10 ? parseInt(11) : parseInt(TotalPage) - parseInt(PageNum) + 2;
                    for (r = 1; r < n; r++) {
                        var obj = "A" + r;
                        document.getElementById(obj).innerText = PageNum++;
                    }
                    for (r = n; r < 11; r++) {
                        var obj = "A" + r;
                        document.getElementById(obj).innerText = "";
                    }
                }
                if (NavDirection == "LeftNav") {
                    if (parseInt(PageNum) > 1) {
                        PageNum = parseInt(PageNum) - 10;
                        document.getElementById("B_PageNo").innerText = PageNum;
                        document.getElementById("B_TotalPage").innerText = TotalPage;
                        document.getElementById("B_TotalRows").innerText = TotalRows;
                        for (l = 1; l < 11; l++) {
                            var obj = "A" + l;
                            document.getElementById(obj).innerText = PageNum++;
                        }
                    }
                    else {
                        alert('No More Pages.');
                    }
                }
                if (NavDirection == "PageNav") {
                    document.getElementById("B_PageNo").innerText = PageNum;
                    document.getElementById("B_TotalPage").innerText = TotalPage;
                    document.getElementById("B_TotalRows").innerText = TotalRows;
                }
                if (NavDirection == "ShowBtnClick") {
                    document.getElementById("B_PageNo").innerText = PageNum;
                    document.getElementById("B_TotalPage").innerText = TotalPage;
                    document.getElementById("B_TotalRows").innerText = TotalRows;
                    var n = parseInt(TotalPage) - parseInt(PageNum) > 10 ? parseInt(11) : parseInt(TotalPage) - parseInt(PageNum) + 2;

                    for (r = 1; r < n; r++) {
                        var obj = "A" + r;
                        document.getElementById(obj).innerText = PageNum++;
                    }

                    for (r = n; r < 11; r++) {
                        var obj = "A" + r;
                        document.getElementById(obj).innerText = "";
                    }

                }
            }
            if (cGrdEmployee.cpCallOtherWhichCallCondition != undefined) {
                if (cGrdEmployee.cpCallOtherWhichCallCondition == "Show") {
                    if (crbDOJ_Specific_All.GetValue() == "S")
                        cGrdEmployee.PerformCallback("Show~" + cDtFrom.GetValue() + '~' + cDtTo.GetValue());
                    else
                        cGrdEmployee.PerformCallback("Show~~~");
                }
            }
            //Now Reset GridBindOrNotBind to True for Next Page Load
            document.getElementById("hdn_GridBindOrNotBind").value = "True";
            //height();
            if (cGrdEmployee.cpDelete != null) {
                if (cGrdEmployee.cpDelete == 'Success')
                    jAlert('Record deleted successfully');
                else
                    jAlert('Error on deletio/n Please Try again!!')
            }
        }
        function selecttion() {
            var combo = document.getElementById('cmbExport');
            combo.value = 'Ex';
        }

        function OnContactInfoClick(keyValue, CompName) {
            var url = 'insurance_contactPerson.aspx?id=' + keyValue;
            // OnMoreInfoClick(url, "Employee Name : " + CompName + "", '980px', '550px', "Y");
            window.location.href = url;
        }
        function Callheight() {
            //height();
        }

        function ShowEmployeeFilterForm(obj) {
            if (obj == 'A') {
                document.getElementById('td1').style.display = "none";
                document.getElementById('td2').style.display = "none";
                document.getElementById('td3').style.display = "none";
                document.getElementById('td4').style.display = "none";
            }
            if (obj == 'S') {

                document.getElementById('td1').style.display = "inline";
                document.getElementById('td1').style.display = "table-cell";

                document.getElementById('td2').style.display = "inline";
                document.getElementById('td2').style.display = "table-cell";

                document.getElementById('td3').style.display = "inline";
                document.getElementById('td3').style.display = "table-cell";

                document.getElementById('td4').style.display = "inline";
                document.getElementById('td4').style.display = "table-cell";
            }

        }
        function ShowFindOption() {
            if (cRb_SearchBy.GetValue() == "N") {
                HideTrTd("Tr_EmployeeName")
                HideTrTd("Tr_EmployeeCode")
            }
            else if (cRb_SearchBy.GetValue() == "EN") {
                ShowTrTd("Tr_EmployeeName")
                HideTrTd("Tr_EmployeeCode")
            }
            else if (cRb_SearchBy.GetValue() == "EC") {
                HideTrTd("Tr_EmployeeName")
                ShowTrTd("Tr_EmployeeCode")
            }
        }
      <%--  function ddlExport_OnChange() {
            var ddlExport = document.getElementById("<%=ddlExport.ClientID%>");
            if (crbDOJ_Specific_All.GetValue() == "S")
                cGrdEmployee.PerformCallback("ExcelExport~" + cDtFrom.GetValue() + '~' + cDtTo.GetValue());
            else
                cGrdEmployee.PerformCallback("ExcelExport~~~");
            ddlExport.options[0].selected = true;
        }--%>

        function ShowHideFilter(obj) {
            document.getElementById("hdn_GridBindOrNotBind").value = "False"; //To Stop Bind On Page Load
            cGrdEmployee.PerformCallback("ShowHideFilter~" + cDtFrom.GetValue() + '~' + cDtTo.GetValue() + '~' + obj);
        }
        // Rev 3.0
        function EmployeeButnClick(s, e) {
            $('#EmployeeModel').modal('show');
            $("#txtEmployeeSearch").focus();
        }

        function EmployeebtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#EmployeeModel').modal('show');
                $("#txtEmployeeSearch").focus();
            }
        }

        function Employeekeydown(e) {

            var OtherDetails = {}
            OtherDetails.SearchKey = $("#txtEmployeeSearch").val();
            if ($.trim($("#txtEmployeeSearch").val()) == "" || $.trim($("#txtEmployeeSearch").val()) == null) {
                return false;
            }
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Employee Name");
                HeaderCaption.push("Employee Code");
                if ($("#txtEmployeeSearch").val() != null && $("#txtEmployeeSearch").val() != "") {
                    //callonServerM("Employee.aspx/GetOnDemandEmployee", OtherDetails, "EmployeeTable", HeaderCaption, "EmployeeIndex", "SetEmployee");
                    //callonServerM("Employee.aspx/GetOnDemandEmployee", OtherDetails, "EmployeeTable", HeaderCaption, "EmployeeIndex", "SetEmployee", "EmployeeSource");
                    callonServerM("Employee.aspx/GetOnDemandEmployee", OtherDetails, "EmployeeTable", HeaderCaption, "dPropertyIndex", "SetSelectedValues", "EmployeeSource");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[EmployeeIndex=0]"))
                    $("input[EmployeeIndex=0]").focus();
            }
        }

        function SetSelectedValues(Id, Name, ArrName) {
            if (ArrName == 'EmployeeSource') {
                var key = Id;
                if (key != null && key != '') {
                    $("#txtEmployee_hidden").val(Id);
                    ctxtEmployee.SetText(Name);
                    $('#EmployeeModel').modal('hide');
                }
                else {
                    $("#txtEmployee_hidden").val('');
                    ctxtEmployee.SetText('');
                    $('#EmployeeModel').modal('hide');

                }
            }
        }

        // End of Rev 3.0

        // Rev 10.0

        // ********************  fromsuper ********************** //
        function EmployeefromsuperButnClick(s, e) {
            $('#EmployeefromsuperModel').modal('show');
            $("#txtEmployeefromsuperSearch").focus();
        }

        function EmployeefromsuperbtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#EmployeefromsuperModel').modal('show');
                $("#txtEmployeefromsuperSearch").focus();
            }
        }

        function Employeefromsuperkeydown(e) {

            var OtherDetails = {}
            OtherDetails.SearchKey = $("#txtEmployeefromsuperSearch").val();
            OtherDetails.Action = "Past";
            if ($.trim($("#txtEmployeefromsuperSearch").val()) == "" || $.trim($("#txtEmployeefromsuperSearch").val()) == null) {
                return false;
            }
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Employee Name");
                HeaderCaption.push("Employee Code");
                if ($("#txtEmployeefromsuperSearch").val() != null && $("#txtEmployeefromsuperSearch").val() != "") {
                    //callonServerM("Employee.aspx/GetOnDemandEmployee", OtherDetails, "EmployeeTable", HeaderCaption, "EmployeeIndex", "SetEmployee");
                    //callonServerM("Employee.aspx/GetOnDemandEmployee", OtherDetails, "EmployeeTable", HeaderCaption, "EmployeeIndex", "SetEmployee", "EmployeeSource");
                    //callonServerM("Employee.aspx/GetOnDemandEmployeefromsuper", OtherDetails, "EmployeefromsuperTable", HeaderCaption, "dPropertyIndex", "SetSelectedValues", "EmployeefromsuperSource");
                    callonServer("Employee.aspx/GetOnDemandEmployeefromsuper", OtherDetails, "EmployeefromsuperTable", HeaderCaption, "dPropertyIndex", "SetEmployeefromsuper");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[EmployeeIndex=0]"))
                    $("input[EmployeeIndex=0]").focus();
            }
        }

        function SetEmployeefromsuper(Id, Name) {
            $("#txtEmployeefromsuper_hidden").val(Id);
            ctxtEmployeefromsuper.SetText(Name);
            $('#EmployeefromsuperModel').modal('hide');
           
        }
        // ********************  fromsuper ********************** //

        // ********************  tosupervisor ********************** //
        function EmployeetosupervisorButnClick(s, e) {
            $('#EmployeetosupervisorModel').modal('show');
            $("#txtEmployeetosupervisorSearch").focus();
        }

        function EmployeetosupervisorbtnKeyDown(s, e) {
            if (e.htmlEvent.key == "Enter" || e.code == "NumpadEnter") {
                $('#EmployeetosupervisorModel').modal('show');
                $("#txtEmployeetosupervisorSearch").focus();
            }
        }

        function Employeetosupervisorkeydown(e) {

            var OtherDetails = {}
            OtherDetails.SearchKey = $("#txtEmployeetosupervisorSearch").val();
            OtherDetails.Action = "New";
            if ($.trim($("#txtEmployeetosupervisorSearch").val()) == "" || $.trim($("#txtEmployeetosupervisorSearch").val()) == null) {
                return false;
            }
            if (e.code == "Enter" || e.code == "NumpadEnter") {
                var HeaderCaption = [];
                HeaderCaption.push("Employee Name");
                HeaderCaption.push("Employee Code");
                if ($("#txtEmployeetosupervisorSearch").val() != null && $("#txtEmployeetosupervisorSearch").val() != "") {
                    //callonServerM("Employee.aspx/GetOnDemandEmployee", OtherDetails, "EmployeeTable", HeaderCaption, "EmployeeIndex", "SetEmployee");
                    //callonServerM("Employee.aspx/GetOnDemandEmployee", OtherDetails, "EmployeeTable", HeaderCaption, "EmployeeIndex", "SetEmployee", "EmployeeSource");
                    //callonServerM("Employee.aspx/GetOnDemandEmployeefromsuper", OtherDetails, "EmployeetosupervisorTable", HeaderCaption, "dPropertyIndex", "SetSelectedValues", "EmployeetosupervisorSource");
                    callonServer("Employee.aspx/GetOnDemandEmployeefromsuper", OtherDetails, "EmployeetosupervisorTable", HeaderCaption, "dPropertyIndex", "SetEmployeetosupervisor");
                }
            }
            else if (e.code == "ArrowDown") {
                if ($("input[EmployeeIndex=0]"))
                    $("input[EmployeeIndex=0]").focus();
            }
        }

        function SetEmployeetosupervisor(Id, Name) {
            $("#txtEmployeetosupervisor_hidden").val(Id);
            ctxtEmployeetosupervisor.SetText(Name);
            $('#EmployeetosupervisorModel').modal('hide');
        }
        // ********************  tosupervisor ********************** //

        //}


        // End of Rev 10.0
    </script>


    <script>
        // Mantis Issue 24752_Rectify
        // Rev 1.0
        //$(document).ready(function () {
        //    cGrdEmployee.PerformCallback();
        //});
        // End of Rev 1.0
        // End of Mantis Issue 24752_Rectify

        // Rev 3.0
        var EmployeeArr = new Array();
        $(document).ready(function () {
            var EmployeeObj = new Object();
            EmployeeObj.Name = "EmployeeSource";
            EmployeeObj.ArraySource = EmployeeArr;
            arrMultiPopup.push(EmployeeObj);
        })
        // End of Rev 3.0

        // Rev 10.0
        var EmployeefromsuperArr = new Array();
        $(document).ready(function () {
            var EmployeefromsuperObj = new Object();
            EmployeefromsuperObj.Name = "EmployeefromsuperSource";
            EmployeefromsuperObj.ArraySource = EmployeefromsuperArr;
            arrMultiPopup.push(EmployeefromsuperObj);
        })

        var EmployeetosupervisorArr = new Array();
        $(document).ready(function () {
            var EmployeetosupervisorObj = new Object();
            EmployeetosupervisorObj.Name = "EmployeetosupervisorSource";
            EmployeetosupervisorObj.ArraySource = EmployeetosupervisorArr;
            arrMultiPopup.push(EmployeetosupervisorObj);
        })
        // End of Rev 10.0

        var statelist = []
        function STATEPUSHPOP() {
            var empID = $("#hdnEMPID").val();
            // debugger;
            //$('input:checkbox.statecheck').each(function () {

            //    var ischecked = $(this).is(':checked');
            //    if (ischecked == true) {
            //        alert(ischecked);
            //    }
            //});

            let a = [];

            $(".statecheckall:checked").each(function () {
                a.push(this.value);
            });

            $(".statecheck:checked").each(function () {
                a.push(this.value);
            });
            var str1
            //  alert(a);

            str1 = { EMPID: empID, Statelist: a }
            $.ajax({
                type: "POST",
                url: "Employee.aspx/GetStateListSubmit",
                data: JSON.stringify(str1),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    // alert(responseFromServer.d)
                    $("#myModal").modal('hide');
                    jAlert('State assigned successfully');
                }
            });
        }

        function CheckParticular(v) {
            ///   alert(v);
            //alert($(".statecheck").is(':checked'));
            if (v == false) {
                $(".statecheckall").prop('checked', false);

            }
        }
        function CheckAll(id) {
            //alert(id);

            var ischecked = $(".statecheckall").is(':checked');
            //alert(ischecked);
            if (ischecked == true) {
                $('input:checkbox.statecheck').each(function () {
                    $(this).prop('checked', true);
                });

            }
            else {
                $('input:checkbox.statecheck').each(function () {
                    $(this).prop('checked', false);
                });

            }


        }
        function StateBind(empID) {
            $("#hdnEMPID").val(empID);
            var str
            str = { EMPID: empID }
            var html = "";
            // alert();
            $.ajax({
                type: "POST",
                url: "Employee.aspx/GetStateList",
                data: JSON.stringify(str),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    if (responseFromServer.d.length == 0) {
                        jAlert('You must create a user, and map this employee. After mapping, you can map the State. So, these employees will appear on Dashboard and in reports for the selected state.');

                    }
                    else if (responseFromServer.d[0].status != 'Success') {

                        jAlert('You must create a user, and map this employee. After mapping, you can map the State. So, these employees will appear on Dashboard and in reports for the selected state.');

                    }
                    else {

                        for (i = 0; i < responseFromServer.d.length; i++) {

                            if (responseFromServer.d[i].StateID == "0") {

                                if (responseFromServer.d[i].IsChecked == true) {

                                    html += "<li><input type='checkbox' id=" + responseFromServer.d[i].StateID + "  class='statecheckall' onclick=CheckAll(" + responseFromServer.d[i].StateID + ") value=" + responseFromServer.d[i].StateID + " checked  /><a href='#'><label id='lblstatename' class='lblstate' for=" + responseFromServer.d[i].StateID + " >" + responseFromServer.d[i].StateName + "</label></a></li>";

                                }
                                else {
                                    html += "<li><input type='checkbox' id=" + responseFromServer.d[i].StateID + "  class='statecheckall' onclick=CheckAll(" + responseFromServer.d[i].StateID + ")  value=" + responseFromServer.d[i].StateID + "   /><a href='#'><label id='lblstatename' class='lblstate'  for=" + responseFromServer.d[i].StateID + ">" + responseFromServer.d[i].StateName + "</label></a></li>";


                                }
                            }
                            else {

                                if (responseFromServer.d[i].IsChecked == true) {

                                    html += "<li><input type='checkbox' id=" + responseFromServer.d[i].StateID + "  class='statecheck' onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].StateID + " checked  /><a href='#'><label id='lblstatename' class='lblstate' for=" + responseFromServer.d[i].StateID + " >" + responseFromServer.d[i].StateName + "</label></a></li>";

                                }
                                else {
                                    html += "<li><input type='checkbox' id=" + responseFromServer.d[i].StateID + " class='statecheck'  onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].StateID + "   /><a href='#'><label id='lblstatename' class='lblstate' for=" + responseFromServer.d[i].StateID + ">" + responseFromServer.d[i].StateName + "</label></a></li>";

                                }
                            }
                        }
                        $("#divModalBody").html(html);
                        $("#myModal").modal('show');
                    }
                   
                }
            });

        }
    </script>
    <style>
        .listStyle > li {
            list-style-type: none;
            padding: 5px;
        }

        .listStyle {
            /*height: 450px;*/
            overflow-y: auto;
        }

            .listStyle > li > input[type="checkbox"] {
                -webkit-transform: translateY(3px);
                -moz-transform: translateY(3px);
                transform: translateY(3px);
            }

        #divModalBody li a:hover:not(.header) {
            background-color: none;
        }
        .modal-backdrop{
            z-index:auto !important;
        }
.nfc {
padding: 7px;
    margin-right: 1px;
}
/*Rev 1.0*/
#modalimport .modal-header {
        background: #094e8c !important;
        background-image: none !important;
        padding: 11px 20px;
        border: none;
        border-radius: 5px 5px 0 0;
        color: #fff;
        border-radius: 10px 10px 0 0;
    }

    #modalimport .modal-content {
        border: none;
        border-radius: 10px;
    }

    #modalimport .modal-header .modal-title {
        font-size: 14px;
    }

    #modalimport .close {
        font-weight: 400;
        font-size: 25px;
        color: #fff;
        text-shadow: none;
        opacity: .5;
    }
    /*Rev end 1.0*/
    /*Rev 2.0*/
    .btn-show
    {
        background: #2379d1;
        border-color: #2379d1;
            color: #fff;
    }

    .btn-show:hover , .btn-show:focus
    {
        color: #fff;
    }

    .dxgvControl_PlasticBlue a img{
        margin-bottom: 5px;
    }
    /*Rev end 2.0*/

   /*Rev 3.0*/
   .for-cust-padding
   {
       padding: 0 0 0 10px;
   }

   .for-cust-padding label
   {
       margin-right: 5px;
   }

   .dis-flex
   {
        display: flex;
        align-items: center;
   }

   .btn-show
   {
       margin-left: 10px;
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

    .close:hover
    {
        color: #fff;
        opacity: 1;
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

   /*Rev end 3.0*/
   /*Rev 10.0*/
   #EmployeefromsuperTable {
        margin-top: 10px;
    }

        #EmployeefromsuperTable table tr th {
            padding: 5px 10px;
        }

    .dynamicPopupTbl {
        font-family: 'Poppins', sans-serif !important;
    }

        .dynamicPopupTbl > tbody > tr > td,
        #EmployeefromsuperTable table tr th {
            font-family: 'Poppins', sans-serif !important;
            font-size: 12px;
        }

    #EmployeefromsuperTable {
        margin-top: 10px;
    }

        #EmployeefromsuperTable table tr th {
            padding: 5px 10px;
        }

    .dynamicPopupTbl {
        font-family: 'Poppins', sans-serif !important;
    }

        .dynamicPopupTbl > tbody > tr > td,
        #EmployeefromsuperTable table tr th {
            font-family: 'Poppins', sans-serif !important;
            font-size: 12px;
        }
   /*End of Rev 10.0*/

   /*Rev 5.0*/

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

    .nfc
    {
        padding: 5px 12px;
            height: 32px;
    }

    @media only screen and (min-width: 1300px) and (max-width: 1500px)
    {
        .btn
        {
            font-size: 12px;
            padding: 6px 8px;
        }

        #txtEmployee
        {
            width: 145px;
        }

        .backBox
        {
            overflow:hidden;
        }

        #GrdEmployee
        {
            max-width: 98% !important;
            width: 98% !important;
        }

        #GrdEmployee_DXMainTable , #GrdEmployee_DXPagerBottom
        {
            width: 95% !important;
        }

        /*.dxpLite_PlasticBlue .dxp-pageSizeItem
        {
                margin-right: 100px;
        }*/

        .dxp-right {
    float: left !important;
    /*margin-left: 50px !important;*/
}
        /*.dxpLite_PlasticBlue .dxp-pageSizeItem
        {
            padding-right: 65px !important;
        }*/

        .dxpLite_PlasticBlue .dxp-pageSizeItem
        {
            margin-left: 20px;
        }

    }

    .pTop10
    {
        padding-top: 10px;
    }

    #modalSS .modal-dialog
    {
        width: 700px;
    }

    /*Rev end 5.0*/

    .dxgvControl_PlasticBlue a
    {
        margin: 0 2px !important;
    }

    /*Rev 7.0*/
    @media only screen and (max-width: 768px)
    {
        #ShowFilter, #divEmp, #show-btn
        {
             width: 100%;
            display: block;
        }

        #ShowFilter a, #ShowFilter select, #ShowFilter button
        {
            margin-bottom: 10px;
        }

        #divEmp
        {
            margin-bottom: 10px;
        }

        .grid-view
        {
            overflow-x: auto;
            width: 280px;
        }
    }
    /*Rev end 7.0*/
    /*Rev 8.0*/
    @media  only screen and (min-width: 767px) and (max-width: 1320px)
    {
        #ShowFilter , #divEmp
        {
            display: block;
            width: 100%;
            margin-bottom: 10px;
        }
    }
    /*Rev end 8.0*/

    /*Rev 9.0*/
    #GvImportDetailsSearch_LPV
    {
            left: 43% !important;
    }

    #GvImportDetailsSearch_DXPagerBottom_PSP
    {
            right: 41px !important;
            left: auto !important;
    }
    /*Rev end 9.0*/

    /*Rev 10.0*/
    #EmployeefromsuperModel
    {
       z-index: 99999 !important;
    }
     #EmployeetosupervisorModel
    {
       z-index: 99999 !important;
    }
    

    .dxeButtonEdit_PlasticBlue
    {
        width: 100%;
    }

    #txtEmployeefromsuper
    {
        margin-bottom: 15px;
    }

    .jpopup .dxpcLite_PlasticBlue .dxpc-content, .jpopup .dxdpLite_PlasticBlue .dxpc-content
    {
        padding: 12px 10px 12px;
    }

    .btn-default
    {
        background-color: #dadada;
    }
    /*End of Rev 10.0*/

    #divModalBodyBranchMap
    {
            max-height: 350px !important;
    }

    #drdExport
    {
        padding: 7px 10px !important;
    border-radius: 4px !important;
    }

    #btnViewLog.btn-warning
    {
        background: #b6d206 !important;
    }
    </style>

    <script>
        function EMPIDBind(empID) {
            $("#hdnEMPCode").val(empID);
            var str
            str = { EMPID: empID }
           
            $.ajax({
                type: "POST",
                url: "Employee.aspx/GetEmployeeID",
                data: JSON.stringify(str),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                     // alert(responseFromServer.d)
                    //cGrdEmployee.PerformCallback("Show~~~");
                    //jAlert('Supervisor Changed Successfully.');
                    //cActivationPopupsupervisor.Hide();

                    $("#lblOLDEmpIDname").html(responseFromServer.d);
                    $("#myEmpIDModal").modal('show');
                    $("#txtEMPId").focus();
                }
            });           
        }

        function UpdateEmpId() {
            var empID = $("#hdnEMPCode").val();
            var newEmpID = $("#txtEMPId").val();
            if (newEmpID=="") {
                jAlert("Please enter new employee id.");
                $("#txtEMPId").focus();
                return
            }
            var str1
            //  alert(a);

            str1 = { EMPID: empID, newEmpID: newEmpID }
            $.ajax({
                type: "POST",
                url: "Employee.aspx/GetEmployeeIDUpdate",
                data: JSON.stringify(str1),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    // alert(responseFromServer.d)
                    if (responseFromServer.d == "UPDATED") {
                        $("#myEmpIDModal").modal('hide');
                        jAlert('Employee id update successfully.');
                        $("#txtEMPId").val('');
                        $("#hdnEMPCode").val('');
                    }
                    else if (responseFromServer.d == "ALREADY EXISTS") {
                        jAlert('Employee id already exists.');
                        $("#txtEMPId").focus();
                    }
                    else {
                        jAlert('Please try again later.');
                        $("#txtEMPId").focus();
                    }
                }
            });
        }
    </script>
    <script>
        function ImportUpdatePopOpenEmployeesTarget(e) {

            $("#modalimport").modal('show');
        }
    </script>
    <%--Mantis Issue 24982--%>
    <script>
        function OnEmployeeChannelInfoClick(keyValue) {
            var str1 = "";
            //  alert(a);

            str1 = { EMPID: keyValue }
            $.ajax({
                type: "POST",
                url: "Employee.aspx/GetEmployeeChannel",
                data: JSON.stringify(str1),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (data) {
                    //alert(data.d[0].ReportToEmpName)
                    //$.each(data, function (i, data) {

                        
                        //alert(data.ReportToEmpName)
                        $("#txtReportTo").val(data.d[0].ReportToEmpName)
                        $("#txtAddReportingHead").val(data.d[0].DeputyEmpName)
                        $("#txtColleague").val(data.d[0].ColleagueName)
                        $("#txtColleague1").val(data.d[0].Colleague_1Name)
                        $("#txtColleague2").val(data.d[0].Colleague_2Name)

                        $("#txtReportToChannel").val(data.d[0].ReportToChannel)
                        $("#txtAddReportingHeadChannel").val(data.d[0].DeputyChannel)
                        $("#txtColleagueChannel").val(data.d[0].ColleagueChannel)
                        $("#txtColleague1Channel").val(data.d[0].ColleagueChannel_1)
                        $("#txtColleague2Channel").val(data.d[0].ColleagueChannel_2)

                        $("#txtReportToCircle").val(data.d[0].ReportToCircle)
                        $("#txtAddReportingHeadCircle").val(data.d[0].DeputyCircle)
                        $("#txtColleagueCircle").val(data.d[0].ColleagueCircle)
                        $("#txtColleague1Circle").val(data.d[0].ColleagueCircle_1)
                        $("#txtColleague2Circle").val(data.d[0].ColleagueCircle_2)

                        $("#txtReportToSection").val(data.d[0].ReportToSection)
                        $("#txtAddReportingHeadSection").val(data.d[0].DeputySection)
                        $("#txtColleagueSection").val(data.d[0].ColleagueSection)
                        $("#txtColleague1Section").val(data.d[0].ColleagueSection_1)
                        $("#txtColleague2Section").val(data.d[0].ColleagueSection_2)
                    //});
                }
            });
            $("#modalEmployeeChannel").modal('show');
        }
    </script>
    <%--End of Mantis Issue 24982--%>
    <%--Mantis Issue 25001--%>
    <script>
    function fn_BranchMap(empID) {
            $("#hdnEMPID").val(empID);
            var str
            str = { EMPID: empID }
            var html = "";
            // alert();
            $.ajax({
                type: "POST",
                url: "Employee.aspx/GetBranchList",
                data: JSON.stringify(str),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    for (i = 0; i < responseFromServer.d.length; i++) {
                        if (responseFromServer.d[i].IsChecked == true) {
                            html += "<li><input type='checkbox' id=" + responseFromServer.d[i].branch_id + "  class='statecheck' onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].branch_id + " checked  /><a href='#'><label id='lblstatename' class='lblstate' for=" + responseFromServer.d[i].branch_id + " >" + responseFromServer.d[i].branch_description + "</label></a></li>";
                        }
                        else {
                            html += "<li><input type='checkbox' id=" + responseFromServer.d[i].branch_id + " class='statecheck'  onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].branch_id + "   /><a href='#'><label id='lblstatename' class='lblstate' for=" + responseFromServer.d[i].branch_id + ">" + responseFromServer.d[i].branch_description + "</label></a></li>";
                        }
                    }
                    $("#divModalBody").html(html);
                    $("#myModal").modal('show');
                }
            });
        }
    </script>

    <script>
        var Branchlist = []
        function BranchPushPop() {
            var empID = $("#hdnEMPID").val();
            let a = [];

            $(".BranchMapcheckall:checked").each(function () {
                a.push(this.value);
            });

            $(".BranchMapcheck:checked").each(function () {
                a.push(this.value);
            });
            var str1
            //  alert(a);

            str1 = { EMPID: empID, Branchlist: a }
            $.ajax({
                type: "POST",
                url: "Employee.aspx/GetBranchListSubmit",
                data: JSON.stringify(str1),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    // alert(responseFromServer.d)
                    $("#myModalBranchMap").modal('hide');
                    jAlert('Branch assigned successfully');
                }
            });
        }

        function CheckParticular(v) {
            if (v == false) {
                $(".BranchMapcheckall").prop('checked', false);
            }
        }

        function CheckAll(id) {
            var ischecked = $(".BranchMapcheckall").is(':checked');
            if (ischecked == true) {
                $('input:checkbox.BranchMapcheck').each(function () {
                    $(this).prop('checked', true);
                });

            }
            else {
                $('input:checkbox.BranchMapcheck').each(function () {
                    $(this).prop('checked', false);
                });

            }


        }

        function fn_BranchMap(empID) {
            $("#hdnEMPID").val(empID);
            var str
            str = { EMPID: empID }
            var html = "";
            // alert();
            $.ajax({
                type: "POST",
                url: "Employee.aspx/GetBranchList",
                data: JSON.stringify(str),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    for (i = 0; i < responseFromServer.d.length; i++) {
                        //if (responseFromServer.d[i].IsChecked == true) {
                        //    html += "<li><input type='checkbox' id=" + responseFromServer.d[i].branch_id + "  class='BranchMapcheck' onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].branch_id + " checked  /><a href='#'><label id='BranchMapname' class='lblstate' for=" + responseFromServer.d[i].branch_id + " >" + responseFromServer.d[i].branch_description + "</label></a></li>";
                        //}
                        //else {
                        //    html += "<li><input type='checkbox' id=" + responseFromServer.d[i].branch_id + " class='BranchMapcheck'  onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].branch_id + "   /><a href='#'><label id='BranchMapname' class='lblstate' for=" + responseFromServer.d[i].branch_id + ">" + responseFromServer.d[i].branch_description + "</label></a></li>";
                        //}
                        if (responseFromServer.d[i].IsChecked == true) {
                            html += "<label class='custom-checkbox-single'>" + responseFromServer.d[i].branch_description + "<input type='checkbox' id=" + responseFromServer.d[i].branch_id + "  class='BranchMapcheck' onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].branch_id + " checked  > <span class='checkmark'></span></label>";
                        }
                        else {
                            html += "<label class='custom-checkbox-single'>" + responseFromServer.d[i].branch_description + "<input type='checkbox' id=" + responseFromServer.d[i].branch_id + "  class='BranchMapcheck' onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].branch_id + "  > <span class='checkmark'></span></label>";
                        }
                    }
                    $("#divModalBodyBranchMap").html(html);
                    $("#myModalBranchMap").modal('show');
                }
            });
        }
        function ClearData() {
            $("#myModalBranchMap").modal('hide');
        }
        function ClearChannelData() {
            $("#modalEmployeeChannel").modal('hide');
        }
        /*Rev 6.0*/
        $(document).ready(function () {
            $('#EmployeeModel').on('shown.bs.modal', function () {
                $('#txtEmployeeSearch').focus();
            })
        })
        /*Rev end 6.0*/
    </script>
    <%--End of Mantis Issue 25001--%>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
        <div class="breadCumb">
            <span>Employees</span>
        </div>
    
    <div class="container">
        <div class="backBox mt-5 p-3">
        <table class="TableMain100">
            <%--            <tr>
                <td class="EHEADER" style="text-align: center; height: 20px;">
                    <strong><span style="color: #000099">Employee Details</span></strong></td>
            </tr>--%>

            <tr>
                <td style="text-align: left; vertical-align: top">
                    <table class="mb-4">
                        <tr>
                            <td id="ShowFilter">
                                <% if (rights.CanAdd)
                                   { %>
                                <a href="javascript:void(0);" onclick="OnAddButtonClick()" class="btn btn-success"><span>Add New</span> </a>
                                <% } %>

                                <% if (rights.CanExport)
                                   { %>
                               <%--Rev 11.0--%>
                               <%-- <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-primary nfc" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Export to</asp:ListItem>
                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                    <asp:ListItem Value="2">XLS</asp:ListItem>
                                    <asp:ListItem Value="3">RTF</asp:ListItem>
                                    <asp:ListItem Value="4">CSV</asp:ListItem>

                                </asp:DropDownList>--%>

                                <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary"
                                    OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true" OnChange="if(!AvailableExportOption()){return false;}">
                                    <asp:ListItem Value="0">Export to</asp:ListItem>
                                    <asp:ListItem Value="1">EXCEL</asp:ListItem>
                                    <asp:ListItem Value="2">PDF</asp:ListItem>
                                    <asp:ListItem Value="3">CSV</asp:ListItem>
                                </asp:DropDownList>
                                <%--End of Rev 11.0--%>
                                <% } %>

                                <% if (rights.CanExport)
                                   { %>
                                <a href="javascript:void(0);" onclick="OnChangeSuperVisor()" class="btn btn-warning"><span>Change Supervisor</span> </a>
                                <% } %>

                            <asp:LinkButton ID="lnlDownloaderexcel" runat="server" OnClick="lnlDownloaderexcel_Click" CssClass="btn btn-info btn-radius  mBot0">Download Format</asp:LinkButton>
                            <button type="button" onclick="ImportUpdatePopOpenEmployeesTarget();" class="btn btn-danger btn-radius">Import(Add/Update)</button>
                            <button type="button" class="btn btn-warning btn-radius " data-toggle="modal" data-target="#modalSS" id="btnViewLog" onclick="ViewLogData();">View Log</button>
                                <%--Rev 3.0--%>
                                <%--<%--Rev 1.0--%>
                                <%--<% if (rights.CanView)
                                   { %>
                                <a href="javascript:void(0);" onclick="ShowData()" class="btn btn-show"><span>Show Data</span> </a>
                                <% } %>
                                <%--End of Rev 1.0--%>
                                <%--End of Rev 3.0--%>
                            </td>
                            <%--Rev 3.0--%>
                            <td class="for-cust-padding" id="divEmp" runat="server" >
                                <div class="dis-flex" >
                                    <label>Employee(s)</label>
                                    <div style="position: relative">
                                        <dxe:ASPxButtonEdit ID="txtEmployee" runat="server" ReadOnly="true" ClientInstanceName="ctxtEmployee" >
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s,e){EmployeeButnClick();}" KeyDown="EmployeebtnKeyDown" />
                                        </dxe:ASPxButtonEdit>
                                        <asp:HiddenField ID="txtEmployee_hidden" runat="server" />

                                    </div>
                                   <%-- <% if (rights.CanView)
                                   { %>
                                    <a href="javascript:void(0);" onclick="ShowData()" class="btn btn-show"><span>Show Data</span> </a>
                                <% } %>--%>
                                </div>
                                
                            </td>
                            <td id="show-btn">
                                 <% if (rights.CanView)
                                   { %>
                                    <a href="javascript:void(0);" onclick="ShowData()" class="btn btn-show"><span>Show Data</span> </a>
                                <% } %>
                            </td>
                            <%--End of Rev 3.0--%>
                        <td>
                            
                        </td>

                        <td>
                            <label>&nbsp;</label>
                            

                        </td>
                            <td id="Td1"></td>
                        </tr>
                    </table>
                </td>

            </tr>
            <tr style="display: none">
                <td>
                    <div style="padding: 15px; background: #f9f9f9; border-radius: 3px; margin-bottom: 12px;">
                        <table cellpadding="1" cellspacing="1" style="display: none">
                            <tr id="trSpecific">
                                <td class="gridcellleft" style="vertical-align: top">Date Of Joining :</td>
                                <td valign="top" style="vertical-align: top">
                                    <dxe:ASPxRadioButtonList ID="rbDOJ_Specific_All" runat="server" SelectedIndex="0" ItemSpacing="10px"
                                        ClientInstanceName="crbDOJ_Specific_All" RepeatDirection="Horizontal" TextWrap="False">
                                        <Items>

                                            <dxe:ListEditItem Text="Specific" Value="S" />
                                            <dxe:ListEditItem Text="All" Value="A" />
                                        </Items>
                                        <ClientSideEvents ValueChanged="function(s, e) {ShowEmployeeFilterForm(s.GetValue());}" />
                                        <Border BorderWidth="0px" />
                                    </dxe:ASPxRadioButtonList>
                                </td>
                                <td align="right" valign="middle" id="td1" class="gridcellleft" style="vertical-align: top">&nbsp;From :</td>
                                <td valign="middle" class="gridcellleft" id="td2" style="vertical-align: top">
                                    <dxe:ASPxDateEdit ID="DtFrom" ClientInstanceName="cDtFrom" runat="server" EditFormat="Custom" EditFormatString="dd-MM-yyyy" UseMaskBehavior="True">
                                        <ButtonStyle Width="13px"></ButtonStyle>
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td valign="middle" align="right" id="td3" class="gridcellleft" style="vertical-align: top">To:</td>
                                <td valign="middle" class="gridcellleft" id="td4" style="vertical-align: top">
                                    <dxe:ASPxDateEdit ID="DtTo" ClientInstanceName="cDtTo" runat="server" EditFormat="Custom" EditFormatString="dd-MM-yyyy" UseMaskBehavior="True">
                                        <ButtonStyle Width="13px"></ButtonStyle>
                                    </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="gridcellleft" style="vertical-align: top">Search By :</td>
                                <td style="vertical-align: top" valign="top">
                                    <dxe:ASPxRadioButtonList ID="Rb_SearchBy" runat="server" ItemSpacing="10px" RepeatDirection="Horizontal"
                                        TextWrap="False" ClientInstanceName="cRb_SearchBy" SelectedIndex="0">
                                        <Border BorderWidth="0px" />
                                        <ClientSideEvents ValueChanged="function(s, e) {ShowFindOption();}" />
                                        <Items>
                                            <dxe:ListEditItem Text="None" Value="N"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Employee Name" Value="EN"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Employee Code" Value="EC"></dxe:ListEditItem>
                                        </Items>
                                    </dxe:ASPxRadioButtonList>
                                </td>
                                <td align="right" class="gridcellleft" style="vertical-align: top"
                                    valign="middle"></td>
                                <td class="gridcellleft" style="vertical-align: top" valign="middle">
                                    <dxe:ASPxButton ID="BtnShow" runat="server" AutoPostBack="False" Text="Show" CssClass="btn btn-primary">
                                        <ClientSideEvents Click="function (s, e) {BtnShow_Click();}" />
                                    </dxe:ASPxButton>
                                </td>
                                <td align="right" class="gridcellleft" style="vertical-align: top"
                                    valign="middle"></td>
                                <td class="gridcellleft" style="vertical-align: top" valign="middle"></td>
                            </tr>
                            <tr id="tr_EmployeeName">
                                <td class="gridcellleft" style="vertical-align: top">Employee Name :</td>
                                <td style="vertical-align: top" valign="top">
                                    <asp:TextBox ID="txtEmpName" onFocus="this.select()" runat="server"></asp:TextBox></td>
                                <td align="right" class="gridcellleft" style="vertical-align: top"
                                    valign="middle">Find Option</td>
                                <td class="gridcellleft" style="vertical-align: top" valign="middle">
                                    <dxe:ASPxComboBox ID="cmbEmpNameFindOption" runat="server"
                                        ClientInstanceName="exp" Font-Bold="False" ForeColor="black"
                                        SelectedIndex="0" ValueType="System.Int32" Width="170px">
                                        <Items>
                                            <dxe:ListEditItem Value="0" Text="Like"></dxe:ListEditItem>
                                            <dxe:ListEditItem Value="1" Text="Whole Word"></dxe:ListEditItem>
                                        </Items>
                                        <ButtonStyle>
                                        </ButtonStyle>
                                        <ItemStyle>
                                            <HoverStyle>
                                            </HoverStyle>
                                        </ItemStyle>
                                        <Border BorderColor="black"></Border>

                                    </dxe:ASPxComboBox>
                                </td>
                                <td align="right" class="gridcellleft" style="vertical-align: top"
                                    valign="middle"></td>
                                <td class="gridcellleft" style="vertical-align: top" valign="middle"></td>
                            </tr>
                            <tr id="tr_EmployeeCode">
                                <td class="gridcellleft" style="vertical-align: top">Employee Code :</td>
                                <td style="vertical-align: top" valign="top">
                                    <asp:TextBox ID="txtEmpCode" onFocus="this.select()" runat="server"></asp:TextBox></td>
                                <td align="right" class="gridcellleft" style="vertical-align: top"
                                    valign="middle">Find Option</td>
                                <td class="gridcellleft" style="vertical-align: top" valign="middle">
                                    <dxe:ASPxComboBox ID="cmbEmpCodeFindOption" runat="server"
                                        ClientInstanceName="exp" Font-Bold="False" ForeColor="black"
                                        SelectedIndex="0" ValueType="System.Int32" Width="170px">
                                        <Items>
                                            <dxe:ListEditItem Value="0" Text="Like"></dxe:ListEditItem>
                                            <dxe:ListEditItem Value="1" Text="Whole Word"></dxe:ListEditItem>
                                        </Items>
                                        <ButtonStyle>
                                        </ButtonStyle>
                                        <ItemStyle>
                                            <HoverStyle>
                                            </HoverStyle>
                                        </ItemStyle>
                                        <Border BorderColor="black"></Border>

                                    </dxe:ASPxComboBox>
                                </td>
                                <td align="right" class="gridcellleft" style="vertical-align: top"
                                    valign="middle"></td>
                                <td class="gridcellleft" style="vertical-align: top" valign="middle"></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td id="Td5">
                                    <% if (rights.CanAdd)
                                       { %>
                                    <a href="javascript:void(0);" onclick="OnAddButtonClick()" class="btn btn-primary"><span>Add New</span> </a><% } %>
                                    <%-- <a href="javascript:ShowHideFilter('s');"><span style="color: #000099; text-decoration: underline">Show Filter</span></a>--%>
                                </td>
                                <td id="Td6">
                                    <%-- <a href="javascript:ShowHideFilter('All');" class="btn btn-primary"><span>All Records</span></a>--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="display: none">
                <td>


                    <table style="width: 100%" border="0">
                        <tr>
                            <td valign="top" style="vertical-align: top; width: 34px; text-align: left">Page </td>
                            <td valign="top" style="width: 4px">
                                <b style="text-align: right" id="B_PageNo" runat="server"></b>
                            </td>
                            <td valign="top" style="vertical-align: top; text-align: left;">Of
                            </td>
                            <td valign="top">
                                <b style="text-align: right" id="B_TotalPage" runat="server"></b>
                            </td>
                            <td valign="top" style="vertical-align: top; text-align: left">( <b style="text-align: right" id="B_TotalRows" runat="server"></b>&nbsp;items )
                            </td>
                            <td valign="top">
                                <table width="100%">
                                    <tr>
                                        <td valign="top" style="vertical-align: top; text-align: left">
                                            <a id="A_LeftNav" runat="server" href="javascript:void(0);" onclick="OnLeftNav_Click()">
                                                <img src="/assests/images/LeftNav.gif" width="10" />
                                            </a>
                                        </td>
                                        <td valign="top" style="vertical-align: top; text-align: left">
                                            <a id="A1" runat="server" href="javascript:void(0);" onclick="OnPageNo_Click('A1')">1</a>
                                        </td>
                                        <td valign="top" style="vertical-align: top; text-align: left">
                                            <a id="A2" runat="server" href="javascript:void(0);" onclick="OnPageNo_Click('A2')">2</a>
                                        </td>
                                        <td valign="top" style="vertical-align: top; text-align: left">
                                            <a id="A3" runat="server" href="javascript:void(0);" onclick="OnPageNo_Click('A3')">3</a>
                                        </td>
                                        <td valign="top" style="vertical-align: top; text-align: left">
                                            <a id="A4" runat="server" href="javascript:void(0);" onclick="OnPageNo_Click('A4')">4</a>
                                        </td>
                                        <td valign="top" style="vertical-align: top; text-align: left">
                                            <a id="A5" runat="server" href="javascript:void(0);" onclick="OnPageNo_Click('A5')">5</a>
                                        </td>
                                        <td valign="top" style="vertical-align: top; text-align: left">
                                            <a id="A6" runat="server" href="javascript:void(0);" onclick="OnPageNo_Click('A6')">6</a>
                                        </td>
                                        <td valign="top" style="vertical-align: top; text-align: left">
                                            <a id="A7" runat="server" href="javascript:void(0);" onclick="OnPageNo_Click('A7')">7</a>
                                        </td>
                                        <td valign="top" style="vertical-align: top; text-align: left">
                                            <a id="A8" runat="server" href="javascript:void(0);" onclick="OnPageNo_Click('A8')">8</a>
                                        </td>
                                        <td valign="top" style="vertical-align: top; text-align: left">
                                            <a id="A9" runat="server" href="javascript:void(0);" onclick="OnPageNo_Click('A9')">9</a>
                                        </td>
                                        <td valign="top" style="vertical-align: top; text-align: left">
                                            <a id="A10" runat="server" href="javascript:void(0);" onclick="OnPageNo_Click('A10')">10</a>
                                        </td>
                                        <td style="text-align: right; vertical-align: top;" valign="top">
                                            <a id="A_RightNav" runat="server" href="javascript:void(0);" onclick="OnRightNav_Click()">
                                                <img src="../images/RightNav.gif" width="10" />
                                            </a>
                                        </td>
                                        <td style="vertical-align: top; text-align: right" valign="top">
                                            <%--<asp:DropDownList ID="ddlExport" Onchange="ddlExport_OnChange()" runat="server"
                                                Width="100px">
                                                <asp:ListItem Selected="True" Value="Ex">Export</asp:ListItem>
                                                <asp:ListItem Value="1">Excel</asp:ListItem>
                                            </asp:DropDownList>--%></td>

                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="grid-view">

                    <%--Mantise ID:0024752: Optimize FSM Employee Master [ DataSourceID="EntityServerlogModeDataSource"  added]
                     Rev work Swati Date:-15.03.2022--%>
                    <%--Rev 1.0 : Some columns width px to %--%>
                    <dxe:ASPxGridView ID="GrdEmployee" runat="server" KeyFieldName="cnt_id" AutoGenerateColumns="False" DataSourceID="EntityServerlogModeDataSource"
                        Width="100%" ClientInstanceName="cGrdEmployee" OnCustomCallback="GrdEmployee_CustomCallback" SettingsBehavior-AllowFocusedRow="true" Settings-HorizontalScrollBarMode="Auto">
                        <ClientSideEvents EndCallback="function(s, e) {GrdEmployee1_EndCallBack();}" />
                        <%--Mantise ID:0024752: Optimize FSM Employee Master
                             Rev work close Swati Date:-15.03.2022--%>
                        <SettingsBehavior AllowFocusedRow="true" ConfirmDelete="True" ColumnResizeMode="NextColumn" />
                        <Styles>
                            
                            
                           <Header SortingImageSpacing="5px" ImageSpacing="5px">
                            </Header>
                            <LoadingPanel ImageSpacing="10px">
                            </LoadingPanel>
                            <Row Wrap="true">
                            </Row>
                            <%-- <FocusedRow HorizontalAlign="Left" VerticalAlign="Top" ></FocusedRow>
                            <AlternatingRow Enabled="True"></AlternatingRow>--%>
                        </Styles>

                        <Columns>
                            <%--Mantis Issue 24752_Rectify [SortOrder="Descending"  added]   --%>
                            <dxe:GridViewDataTextColumn FieldName="cnt_id" Visible="false" ShowInCustomizationForm="false" SortOrder="Descending" Width="0">
                                <CellStyle Wrap="False" CssClass="gridcellleft"></CellStyle>
                                <Settings AllowAutoFilterTextInputTimer="False" />
                            </dxe:GridViewDataTextColumn>
                            <%--End of Mantis Issue 24752_Rectify--%>

                            <dxe:GridViewDataTextColumn Caption="Code" Visible="False" FieldName="ContactID" Width="10%"
                                VisibleIndex="0" FixedStyle="Left">
                                <PropertiesTextEdit DisplayFormatInEditMode="True">
                                </PropertiesTextEdit>
                                <CellStyle CssClass="gridcellleft" Wrap="False">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn Caption="Name" FieldName="Name" Width="130px"
                                VisibleIndex="1" FixedStyle="Left">
                                <CellStyle CssClass="gridcellleft" Wrap="true">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>


                            <dxe:GridViewDataTextColumn Caption="Grade" FieldName="Employee_Grade" Width="90px"
                                VisibleIndex="2" FixedStyle="Left">
                                <CellStyle CssClass="gridcellleft" Wrap="true">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>

                            <%--Mantis Issue 24736--%>
                            <dxe:GridViewDataTextColumn Caption="Other ID" FieldName="cnt_OtherID" Width="100px"
                                VisibleIndex="3" FixedStyle="Left">
                                <CellStyle CssClass="gridcellleft" Wrap="true">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <%--End of Mantis Issue 24736--%>

                            <dxe:GridViewDataDateColumn Caption="Joining On" FieldName="DOJ"
                                VisibleIndex="8" Width="100px" ReadOnly="True">
                            </dxe:GridViewDataDateColumn>

                            <dxe:GridViewDataTextColumn Caption="Department" FieldName="Department"
                                VisibleIndex="6" Width="120px">
                                <CellStyle CssClass="gridcellleft" Wrap="False">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn Caption="Branch" FieldName="BranchName"
                                VisibleIndex="5" Width="150px">
                                <CellStyle CssClass="gridcellleft" Wrap="False">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn Caption="CTC" FieldName="CTC"
                                VisibleIndex="7" Width="75px" Visible="false">
                                <CellStyle CssClass="gridcellleft" Wrap="False" HorizontalAlign="Left">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn Caption="Report To" FieldName="ReportTo"
                                VisibleIndex="9" Width="200px">
                                <CellStyle CssClass="gridcellleft" Wrap="False">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <%--Mantis Issue Pratik Extra Columns--%>
                            <dxe:GridViewDataTextColumn Caption="Additional Reporting Head" FieldName="AdditionalReportingHead"
                                VisibleIndex="10" Width="200px">
                                <CellStyle CssClass="gridcellleft" Wrap="False">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="Colleague" FieldName="Colleague"
                                VisibleIndex="11" Width="180px">
                                <CellStyle CssClass="gridcellleft" Wrap="False">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="Colleague1" FieldName="Colleague1"
                                VisibleIndex="12" Width="180px">
                                <CellStyle CssClass="gridcellleft" Wrap="False">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="Colleague2" FieldName="Colleague2"
                                VisibleIndex="13" Width="180px">
                                <CellStyle CssClass="gridcellleft" Wrap="False">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <%--End of Mantis Issue Pratik Extra Columns--%>

                            <dxe:GridViewDataTextColumn Caption="Designation" FieldName="Designation"
                                VisibleIndex="7" Width="150px">
                                <CellStyle CssClass="gridcellleft" Wrap="False">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="Company" FieldName="Company"
                                VisibleIndex="4" Width="150px">
                                <CellStyle CssClass="gridcellleft" Wrap="False">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>

                            <%--Rev 12.0--%>
                            <dxe:GridViewDataTextColumn Caption="Target Level" FieldName="EmpTargetLevel" Width="250px"
                                VisibleIndex="16" >
                                <CellStyle CssClass="gridcellleft" Wrap="true">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <%--End of Rev 12.0--%>

                            <dxe:GridViewCommandColumn Visible="False" ShowDeleteButton="true" VisibleIndex="17">
                                <%--<DeleteButton Visible="True" Text="Delete">
                                </DeleteButton>--%>
                            </dxe:GridViewCommandColumn>
                            <%--Rev work start 26.04.2022 0024853: Copy feature add in Employee master--%>
                            <%--<dxe:GridViewDataTextColumn HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="center" VisibleIndex="17" Width="100px">--%>
                            <dxe:GridViewDataTextColumn HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="center" VisibleIndex="18" Width="150px">
                                <%--Rev work close 26.04.2022 0024853: Copy feature add in Employee master--%>
                                <DataItemTemplate>
                                    <% if (rights.CanContactPerson)
                                       { %>
                                    <a href="javascript:void(0);" onclick="OnContactInfoClick('<%#Eval("ContactID") %>','<%#Eval("Name") %>')" title="show contact person" class="pad">
                                        <img src="../../../assests/images/show.png" style="padding-right: 8px" />
                                    </a><% } %>
                                    <% if (rights.CanEdit)
                                       { %>
                                    <a href="javascript:void(0);" onclick="OnMoreInfoClick('<%# Container.KeyValue %>')" class="pad" title="More Info">
                                        <img src="../../../assests/images/Edit.png" /></a><% } %>
                                    <%--Rev work start 26.04.2022 0024853: Copy feature add in Employee master--%>
                                     <% if (rights.CanAdd)
                                       { %>
                                    <a href="javascript:void(0);" onclick="OnCopyInfoClick('<%# Container.KeyValue %>')" class="pad" title="Copy">
                                        <%--Rev 2.0--%>
                                        <%--<img src="../../../assests/images/copy.png" /></a>--%>
                                        <img src="../../../assests/images/copy2.png" /></a><% } %>
                                    <%--Rev end 2.0--%>
                                    <%--Rev work close 26.04.2022 0024853: Copy feature add in Employee master--%>
                                    <% if (rights.CanAdd)
                                       { %>
                                    <a href="javascript:void(0);" onclick="EMPIDBind('<%#Eval("ContactID") %>')" title="Update Employee ID" class="pad" style="text-decoration: none;">
                                        <%--Rev 2.0--%>
                                        <%--<img src="../../../assests/images/exchange.png" width="16px" />--%>
                                        <img src="../../../assests/images/update-id.png" />
                                        <%--Rev end 2.0--%>
                                        <% } %>

                                        <%--<a href="javascript:void(0);" onclick="OnAddBusinessClick('<%#Eval("ContactID") %>','<%#Eval("Name") %>')" title="State Bind" class="pad" style="text-decoration: none;">
                                        <img src="../../../assests/images/icoaccts.gif" />--%>


                                        <a href="javascript:void(0);" onclick="StateBind('<%#Eval("ContactID") %>')" title="State Mapping" class="pad" style="text-decoration: none;">
                                            <img src="../../../assests/images/state-mapping.png" />
                                            <% if (rights.CanDelete)
                                               { %>

                                            <a href="javascript:void(0);" onclick="fn_DeleteEmp('<%#Eval("ContactID") %>')" title="Delete">
                                                <img src="../../../assests/images/Delete.png" /></a>
                                            <% } %>



                                            <% if (rights.CanDelete)
                                               { %>

                                            <%-- <a href="javascript:void(0);" onclick="fn_DeActivateEmp('<%#Eval("ContactID") %>')" title="Activate/Deactivate">
                                            <img src="../../../assests/images/activate_icon.png" /></a>--%>
                                            <% } %>
                                            <%--Mantis Issue 24982--%>
                                            <% if (rights.CanAdd)
                                            { %>
                                        <a href="javascript:void(0);" onclick="OnEmployeeChannelInfoClick('<%# Container.KeyValue %>')" class="pad" title="EmployeeChannel">
                                            <%--Rev 2.0--%>
                                            <%--<img src="../../../assests/images/doc.png" />--%>
                                            <img src="../../../assests/images/employee-channel.png" /></a><% } %>
                                            <%--Rev end 2.0--%>
                                            <%--End of Mantis Issue 24982--%>

                                            <%--Mantis Issue 25001--%>
                                            <% if (!ActivateEmployeeBranchHierarchy){ %>
                                            <%--Mantis Issue 25001--%>
                                                <%--Mantis Issue 25001--%>
                                                <a href="javascript:void(0);" onclick="fn_BranchMap('<%# Container.KeyValue %>')" class="pad" title="Branch Mapping">
                                                    <%--Rev 2.0--%>
                                                    <%--<span class='ico deleteColor'><i class='fa fa-sitemap' aria-hidden='true'></i></span>--%>
                                                    <img src="../../../assests/images/branch-mapping.png"  />
                                                    <%--Rev end 2.0--%>
                                                </a>
                                                <%--End of Mantis Issue 25001--%>
                                            <% }  %>

                                            <%-- <asp:LinkButton ID="btn_delete" runat="server" OnClientClick="return confirm('Confirm delete?');" CommandArgument='<%# Container.KeyValue %>' CommandName="delete" ToolTip="Delete" Font-Underline="false">
                                        <img src="/assests/images/Delete.png" />
                                    </asp:LinkButton>--%>
                                </DataItemTemplate>

                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                <CellStyle HorizontalAlign="Center"></CellStyle>

                                <HeaderTemplate><span>Actions</span></HeaderTemplate>

                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn Caption="Employee Code" FieldName="Code"
                                VisibleIndex="1" FixedStyle="Left" Width="150px">
                                <CellStyle CssClass="gridcellleft" Wrap="False">
                                </CellStyle>
                            </dxe:GridViewDataTextColumn>

                        </Columns>
                        <SettingsPager PageSize="10" ShowSeparators="True">
                            <FirstPageButton Visible="True">
                            </FirstPageButton>
                            <LastPageButton Visible="True">
                            </LastPageButton>
                            <PageSizeItemSettings Visible="true" ShowAllItem="false" Items="10,50,100,150,200" />
                        </SettingsPager>
                        <SettingsCommandButton>
                            <DeleteButton ButtonType="Image" Image-Url="/assests/images/Delete.png">
                            </DeleteButton>
                        </SettingsCommandButton>
                        <SettingsEditing Mode="PopupEditForm" PopupEditFormHorizontalAlign="Center" PopupEditFormModal="True"
                            PopupEditFormVerticalAlign="WindowCenter" PopupEditFormWidth="900px" EditFormColumnCount="3" />
                        <SettingsText PopupEditFormCaption="Add/ Modify Employee" ConfirmDelete="Are you sure to delete?" />
                        <SettingsSearchPanel Visible="True" />
                        <Settings ShowGroupPanel="True" ShowStatusBar="Hidden" ShowHorizontalScrollBar="False" ShowFilterRow="true" ShowFilterRowMenu="true" />
                        <SettingsLoadingPanel Text="Please Wait..." />
                    </dxe:ASPxGridView>
                    
                    </div>
                </td>
            </tr>
        </table>
            </div>
          <%--Mantise ID:0024752: Optimize FSM Employee Masterss
                 Rev work Swati Date:-15.03.2022--%>
       <dx:linqservermodedatasource id="EntityServerlogModeDataSource" runat="server" onselecting="EntityServerModelogDataSource_Selecting"
                    contexttypename="ERPDataClassesDataContext" tablename="FTS_Final_Display" />
        <%--Mantise ID:0024752: Optimize FSM Employee Master
             Rev work close Swati Date:-15.03.2022--%>
        <br />
        <asp:HiddenField ID="hdn_GridBindOrNotBind" runat="server" />
        <%--Rev 1.0--%>
        <asp:HiddenField ID="hfIsFilter" runat="server" />
        <%--End of Rev 1.0--%>
        <asp:Button ID="BtnForExportEvent" runat="server" OnClick="cmbExport_SelectedIndexChanged" BackColor="#DDECFE" BorderStyle="None" Visible="false" />
        <dxe:ASPxGridViewExporter ID="exporter" runat="server" GridViewID="GrdEmployee" Landscape="true" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
        </dxe:ASPxGridViewExporter>

    </div>
    <div class="PopUpArea">
        <dxe:ASPxPopupControl ID="ASPxActivationPopup" runat="server" ClientInstanceName="cActivationPopup"
            Width="350px" HeaderText="Activate/Deactivate" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
            Modal="True" ContentStyle-VerticalAlign="Top" EnableHierarchyRecreation="True">
            <ContentCollection>
                <dxe:PopupControlContentControl runat="server">
                    <dxe:ASPxCallbackPanel runat="server" ID="SelectPanel" ClientInstanceName="cSelectPanel" OnCallback="SelectPanel_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {SelectPanel_EndCallBack();}" />
                        <PanelCollection>
                            <dxe:PanelContent runat="server">
                                <div>De-Activate</div>
                                <div>
                                    <dxe:ASPxCheckBox ID="CmbDesignName" ClientInstanceName="cCmbDesignName" runat="server" ValueType="System.String" Width="100%" EnableSynchronization="True">
                                        <ClientSideEvents CheckedChanged="function(s, e) {return ActivateDeactivateTxtReason(s, e); }" />
                                    </dxe:ASPxCheckBox>
                                </div>
                                <div id="DivVisible">
                                    <div>Reason Of Deactivate</div>

                                    <dxe:ASPxMemo ID="TxtReason" ClientInstanceName="cTxtReason" runat="server" ValueType="System.String" Width="100%" Height="200px">
                                        <ValidationSettings RequiredField-IsRequired="true" Display="None"></ValidationSettings>
                                    </dxe:ASPxMemo>
                                </div>
                                <div class="text-center pTop10">
                                    <dxe:ASPxButton ID="btnOK" ClientInstanceName="cbtnOK" runat="server" AutoPostBack="False" Text="OK" CssClass="btn btn-primary" meta:resourcekey="btnSaveRecordsResource1" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {return PerformCallToGridBind(); }" />

                                    </dxe:ASPxButton>
                                </div>

                            </dxe:PanelContent>
                        </PanelCollection>
                    </dxe:ASPxCallbackPanel>
                </dxe:PopupControlContentControl>
            </ContentCollection>

        </dxe:ASPxPopupControl>
    </div>
    <input type="hidden" runat="server" id="hdnContactId" value="" />


    <div class="PopUpArea">
        <dxe:ASPxPopupControl ID="popupsupervisorchange" runat="server" ClientInstanceName="cActivationPopupsupervisor"
            Width="400px" HeaderText="Supervisor Change" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
            Modal="True" ContentStyle-VerticalAlign="Top" EnableHierarchyRecreation="True">
            <ContentCollection>
                <dxe:PopupControlContentControl runat="server">
                    <dxe:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel1" ClientInstanceName="cSelectPanel" OnCallback="SelectPanel_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {SelectPanel_EndCallBack();}" />
                        <PanelCollection>
                            <dxe:PanelContent runat="server">
                                <div>Past Supervisor</div>
                                <div>
                                    <%--Rev 10.0--%>
                                    <%--<asp:DropDownList ID="fromsuper" runat="server"></asp:DropDownList>--%>
                                    <div style="position: relative">
                                        <dxe:ASPxButtonEdit ID="txtEmployeefromsuper" runat="server" ReadOnly="true" ClientInstanceName="ctxtEmployeefromsuper" >
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s,e){EmployeefromsuperButnClick();}" KeyDown="EmployeefromsuperbtnKeyDown" />
                                        </dxe:ASPxButtonEdit>
                                        <asp:HiddenField ID="txtEmployeefromsuper_hidden" runat="server" />

                                    </div>
                                    <%--End of Rev 10.0--%>
                                </div>

                                <div id="">
                                    <div>New Supervisor</div>
                                    <%--Rev 10.0--%>
                                    <%--<asp:DropDownList ID="tosupervisor" runat="server"></asp:DropDownList>--%>
                                    <div style="position: relative">
                                        <dxe:ASPxButtonEdit ID="txtEmployeetosupervisor" runat="server" ReadOnly="true" ClientInstanceName="ctxtEmployeetosupervisor" >
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s,e){EmployeetosupervisorButnClick();}" KeyDown="EmployeetosupervisorbtnKeyDown" />
                                        </dxe:ASPxButtonEdit>
                                        <asp:HiddenField ID="txtEmployeetosupervisor_hidden" runat="server" />

                                    </div>
                                    <%--End of Rev 10.0--%>

                                </div>
                                <div class="text-center pTop10">
                                    <a href="javascript:void(0);" onclick="OnserverCallSupervisorchanged()" class="btn btn-primary"><span>Change Supervisor</span> </a>
                                </div>

                            </dxe:PanelContent>
                        </PanelCollection>
                    </dxe:ASPxCallbackPanel>
                </dxe:PopupControlContentControl>
            </ContentCollection>

        </dxe:ASPxPopupControl>
    </div>


    <div id="myModal" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 450px;">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">State List</h4>
                </div>
                <div class="modal-body">
                    <div>

                        <input type="text" id="myInput" onkeyup="myFunction()" placeholder="Search for States.">

                        <ul id="divModalBody" class="listStyle">
                            <%--<input type="checkbox" id="idstate" class="statecheck" /><label id="lblstatename" class="lblstate"></label>--%>
                        </ul>
                    </div>
                    <input type="button" id="btnsatesubmit" title="SUBMIT" value="SUBMIT" class="btn btn-primary" onclick="STATEPUSHPOP()" />
                    <input type="hidden" id="hdnstatelist" class="btn btn-primary" />
                    <input type="hidden" id="hdnEMPID" class="btn btn-primary" />
                </div>
            </div>

        </div>
    </div>

    <div id="myEmpIDModal" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 450px;">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Update Employee ID</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-5">
                            <label id="lbloldEmpID" class="lblstate">OLD Employee ID :</label>
                        </div>
                        <div class="col-sm-7">
                            <label id="lblOLDEmpIDname" class="lblstate">9563218466</label>
                        </div>
                    </div>
                    <div class="row mt-4">
                        <div class="col-sm-5">
                            <label id="lblEmpIDname" class="lblstate">New Employee ID :</label>
                        </div>
                        <div class="col-sm-7">
                            <input type="text" id="txtEMPId" class="form-control" />
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="modal-footer" style="padding: 8px 26px 5px;">
                    <input type="button" id="btnEMPidSubmit" title="Update" value="Update" class="btn btn-primary" onclick="UpdateEmpId()" />
                   
                     <input type="hidden" id="hdnEMPCode" class="btn btn-primary" />
                </div>
            </div>
        </div>

    </div>
    <%--Mantis Issue 25001--%>
    <div id="myModalBranchMap" class="modal fade branch-list-modal" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 520px;">

            <div class="modal-content">
                <div class="modal-header">
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="ClearData();"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Branch List</h4>
                </div>
                <div class="modal-body">
                    <div>

                        <%--<input type="text" id="myInputBranchMap" onkeyup="myFunctionBranchMap()" placeholder="Search for Branch.">--%>

                        <div class="input-group flex-nowrap">
                          <div class="input-group-prepend">
                            <span class="input-group-text" id="addon-wrapping"><i class="fa fa-search"></i></span>
                          </div>
                          <input type="text" id="myInputBranchMap" onkeyup="myFunctionBranchMap()" class="form-control" placeholder="Search for Branch" aria-describedby="addon-wrapping">
                        </div>

                        <div id="divModalBodyBranchMap" class="listStyle">
                            <%--<input type="checkbox" id="idstate" class="statecheck" /><label id="lblstatename" class="lblstate"></label>--%>
                        </div>
                    </div>
                    <input type="button" id="btnBranchMapsubmit" title="SUBMIT" value="SUBMIT" class="btn btn-primary" onclick="BranchPushPop()" />
                    <%--<input type="hidden" id="hdnstatelist" class="btn btn-primary" />
                    <input type="hidden" id="hdnEMPID" class="btn btn-primary" />--%>
                </div>
               <%-- <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="ClearData();">Close</button>
            </div>--%>
            </div>

        </div>
    </div>
    <%--End of Mantis Issue 25001--%>

    <style>
        #myInput {
            background-image: url('/css/searchicon.png'); /* Add a search icon to input */
            background-position: 10px 12px; /* Position the search icon */
            background-repeat: no-repeat; /* Do not repeat the icon image */
            width: 100%; /* Full-width */
            font-size: 16px; /* Increase font-size */
            padding: 12px 20px 12px 40px; /* Add some padding */
            border: 1px solid #ddd; /* Add a grey border */
            margin-bottom: 12px; /* Add some space below the input */
        }

        #divModalBody {
            /* Remove default list styling */
            list-style-type: none;
            padding: 0;
            margin: 0;
            margin-bottom: 8px;
        }

            #divModalBody li {
                padding: 5px 10px;
            }

                #divModalBody li a {
                    margin-top: -1px; /* Prevent double borders */
                    padding: 0 12px; /* Add some padding */
                    text-decoration: none; /* Remove default text underline */
                    font-size: 14px; /* Increase the font-size */
                    color: black; /* Add a black text color */
                    display: inline-block; /* Make it into a block element to fill the whole list */
                    cursor: pointer;
                }
                .tblView>tbody>tr>td{
                    padding-right:5px;
                    padding-bottom:10px
                }
                .tblView>tbody>tr>td>label{
                    display:block
                }
    </style>
    <%--Mantis Issue 25001--%>
    <style>
        #myInputBranchMap {
            background-image: url('/css/searchicon.png'); /* Add a search icon to input */
            background-position: 10px 12px; /* Position the search icon */
            background-repeat: no-repeat; /* Do not repeat the icon image */
            width: 100%; /* Full-width */
            font-size: 16px; /* Increase font-size */
            padding: 12px 20px 12px 40px; /* Add some padding */
            border: 1px solid #ddd; /* Add a grey border */
            margin-bottom: 12px; /* Add some space below the input */
        }

        #divModalBodyBranchMap {
            /* Remove default list styling */
            list-style-type: none;
            padding: 0;
            margin: 0;
            margin-bottom: 8px;
        }

            #divModalBodyBranchMap li {
                padding: 5px 10px;
            }

                #divModalBodyBranchMap li a {
                    margin-top: -1px; /* Prevent double borders */
                    padding: 0 12px; /* Add some padding */
                    text-decoration: none; /* Remove default text underline */
                    font-size: 14px; /* Increase the font-size */
                    color: black; /* Add a black text color */
                    display: inline-block; /* Make it into a block element to fill the whole list */
                    cursor: pointer;
                }
                .tblView>tbody>tr>td{
                    padding-right:5px;
                    padding-bottom:10px
                }
                .tblView>tbody>tr>td>label{
                    display:block
                }
    </style>

    <script>
        function myFunctionBranchMap() {
            // Declare variables
            var input, filter, ul, li, a, i, txtValue;
            input = document.getElementById('myInputBranchMap');
            filter = input.value.toUpperCase();
            ul = document.getElementById("divModalBodyBranchMap");
            li = ul.getElementsByTagName('li');

            // Loop through all list items, and hide those who don't match the search query
            for (i = 0; i < li.length; i++) {
                a = li[i].getElementsByTagName("a")[0];
                txtValue = a.textContent || a.innerText;

                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    li[i].style.display = "";
                } else {
                    li[i].style.display = "none";
                }

            }
        }
    </script>
    <%--End of Mantis Issue 25001--%>
    <script>
        function myFunction() {
            // Declare variables
            var input, filter, ul, li, a, i, txtValue;
            input = document.getElementById('myInput');
            filter = input.value.toUpperCase();
            ul = document.getElementById("divModalBody");
            li = ul.getElementsByTagName('li');

            // Loop through all list items, and hide those who don't match the search query
            for (i = 0; i < li.length; i++) {
                a = li[i].getElementsByTagName("a")[0];
                txtValue = a.textContent || a.innerText;

                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    li[i].style.display = "";
                } else {
                    li[i].style.display = "none";
                }

            }
        }
    </script>

    
    <div class="modal fade" id="modalimport" role="dialog">
        <div class="modal-dialog VerySmall">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Select File to Import (Add/Update)</h4>
                </div>
                <div class="modal-body">

                    <div class="col-md-12">
                        <div id="divproduct">

                            <div>
                                <asp:FileUpload ID="OFDBankSelect" accept=".xls,.xlsx" runat="server" Width="100%" />
                                <div class="pTop10  mTop5">
                                    <asp:Button ID="BtnSaveexcel" runat="server" Text="Import(Add/Update)" OnClick="BtnSaveexcel_Click1" CssClass="btn btn-primary" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

    <%--Mantis Issue 24982--%>
    <div class="modal fade pmsModal" id="modalEmployeeChannel" role="dialog">
        <div class="modal-dialog" style="width:100%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="ClearChannelData();"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Employee Channel/Circle/Section</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <table style="width:100%" class="tblView">
                                <tr>
                                    <td>
                                        <label>Report To</label>
                                         <input type="text" class="form-control" id="txtReportTo" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Additional Reporting Head</label>
                                        <input type="text" class="form-control" id="txtAddReportingHead" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Colleague</label>
                                        <input type="text" class="form-control" id="txtColleague" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Colleague1</label>
                                        <input type="text" class="form-control" id="txtColleague1" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Colleague2</label>
                                        <input type="text" class="form-control" id="txtColleague2" readonly="readonly" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <label>Channel</label>
                                        <input type="text" class="form-control" id="txtReportToChannel" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Channel</label>
                                        <input type="text" class="form-control" id="txtAddReportingHeadChannel" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Channel</label>
                                        <input type="text" class="form-control" id="txtColleagueChannel" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Channel</label>
                                        <input type="text" class="form-control" id="txtColleague1Channel" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Channel</label>
                                        <input type="text" class="form-control" id="txtColleague2Channel" readonly="readonly" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <label>Circle</label>
                                        <input type="text" class="form-control" id="txtReportToCircle" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Circle</label>
                                        <input type="text" class="form-control" id="txtAddReportingHeadCircle" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Circle</label>
                                        <input type="text" class="form-control" id="txtColleagueCircle" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Circle</label>
                                        <input type="text" class="form-control" id="txtColleague1Circle" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Circle</label>
                                        <input type="text" class="form-control" id="txtColleague2Circle" readonly="readonly" />
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <label>Section</label>
                                        <input type="text" class="form-control" id="txtReportToSection" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Section</label>
                                        <input type="text" class="form-control" id="txtAddReportingHeadSection" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Section</label>
                                        <input type="text" class="form-control" id="txtColleagueSection" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Section</label>
                                        <input type="text" class="form-control" id="txtColleague1Section" readonly="readonly" />
                                    </td>
                                    <td>
                                        <label>Section</label>
                                        <input type="text" class="form-control" id="txtColleague2Section" readonly="readonly" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                        <div class="col-md-2">
                            
                        </div>
                    </div>--%>
                    <div class="clear"></div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="ClearChannelData();">Close</button>
                </div>
            </div>
        </div>
    </div>
    <%--End of Mantis Issue 24982--%>
    <%--Rev 3.0--%>
    <div class="modal fade pmsModal w80 " id="EmployeeModel" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Employee Search</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="Employeekeydown(event)" id="txtEmployeeSearch" class="form-control" autofocus width="100%" placeholder="Search By Employee Code" />

                    <div id="EmployeeTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Employee Name</th>
                                <th>Employee Code</th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSaveEmployee" class="btnOkformultiselection btn btn-success" data-dismiss="modal" onclick="OKPopup('EmployeeSource')">OK</button>
                    <button type="button" id="btnCloseEmployee" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>
    <%--End of Rev 3.0--%>

   <%-- Rev 4.0--%>
    <div class="modal fade" id="modalSS" role="dialog">
        <div class="modal-dialog fullWidth">           
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Employee Log</h4>
                </div>
                <div class="modal-body">
                    <dxe:ASPxGridView ID="GvImportDetailsSearch" runat="server" AutoGenerateColumns="False" SettingsBehavior-AllowSort="true"
                        ClientInstanceName="cGvImportDetailsSearch" KeyFieldName="EMPLOGID" Width="100%" OnDataBinding="GvImportDetailsSearch_DataBinding" Settings-VerticalScrollBarMode="Auto" Settings-VerticalScrollableHeight="400">

                        <SettingsBehavior ConfirmDelete="false" ColumnResizeMode="NextColumn" />
                        <Styles>
                            <Header SortingImageSpacing="5px" ImageSpacing="5px"></Header>
                            <FocusedRow HorizontalAlign="Left" VerticalAlign="Top" CssClass="gridselectrow"></FocusedRow>
                            <LoadingPanel ImageSpacing="10px"></LoadingPanel>
                            <FocusedGroupRow CssClass="gridselectrow"></FocusedGroupRow>
                            <Footer CssClass="gridfooter"></Footer>
                        </Styles>
                        <Columns>
                            <dxe:GridViewDataTextColumn Visible="False" VisibleIndex="0" FieldName="EMPLOGID" Caption="LogID" SortOrder="Descending">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="CREATEDDATETIME" Caption="Date" Width="10%">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="EMPLOYEECODE" Caption="Employee Code" Width="10%">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="2" FieldName="LOOPNUMBER" Caption="Row Number" Width="13%">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="3" FieldName="EMPNAME" Width="8%" Caption="Employee Name">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="4" FieldName="FILENAME" Width="14%" Caption="File Name">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="5" FieldName="DESCRIPTION" Caption="Description" Width="10%" Settings-AllowAutoFilter="False">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="5" FieldName="STATUS" Caption="Status" Width="14%" Settings-AllowAutoFilter="False">
                                <CellStyle Wrap="True" CssClass="gridcellleft"></CellStyle>
                            </dxe:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowGroupPanel="True" ShowStatusBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="true" />
                        <SettingsSearchPanel Visible="false" />
                        <SettingsPager NumericButtonCount="200" PageSize="200" ShowSeparators="True" Mode="ShowPager">
                            <PageSizeItemSettings Visible="true" ShowAllItem="false" Items="200,400,600" />
                            <FirstPageButton Visible="True">
                            </FirstPageButton>
                            <LastPageButton Visible="True">
                            </LastPageButton>
                        </SettingsPager>
                    </dxe:ASPxGridView>

                </div>

            </div>
        </div>
    </div>
  <%--  Rev 4.0 End--%>

    <%--Rev 10.0--%>
    <div class="modal fade pmsModal w80 " id="EmployeefromsuperModel" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Past Supervisor Search</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="Employeefromsuperkeydown(event)" id="txtEmployeefromsuperSearch" class="form-control" autofocus width="100%" placeholder="Search By Employee Code" />

                    <div id="EmployeefromsuperTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Employee Name</th>
                                <th>Employee Code</th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSaveEmployeefromsuper" class="btnOkformultiselection btn btn-success" data-dismiss="modal" onclick="OKPopup('EmployeefromsuperSource')">OK</button>
                    <button type="button" id="btnCloseEmployeefromsuper" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>


    <div class="modal fade pmsModal w80 " id="EmployeetosupervisorModel" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">New Supervisor Search</h4>
                </div>
                <div class="modal-body">
                    <input type="text" onkeydown="Employeetosupervisorkeydown(event)" id="txtEmployeetosupervisorSearch" class="form-control" autofocus width="100%" placeholder="Search By Employee Code" />

                    <div id="EmployeetosupervisorTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Employee Name</th>
                                <th>Employee Code</th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSaveEmployeetosupervisor" class="btnOkformultiselection btn btn-success" data-dismiss="modal" onclick="OKPopup('EmployeetosupervisorSource')">OK</button>
                    <button type="button" id="btnCloseEmployeetosupervisor" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>
    <%--End of Rev 10.0--%>
</asp:Content>
