
$(document).ready(function () {

    if ($("#markWork").val() == "0") {
        $("#leaveTyp").hide();
        $("#workTyp").hide();
    }


    $('#ddlWorkType').multiselect({
        numberDisplayed: 1
    });
    $("#markWork").change(function () {
        var val = $(this).val();
        if (val == 2) {
            $("#leaveTyp").show();
            $("#workTyp").hide();
        } else if (val == 1) {
            $("#leaveTyp").hide();
            $("#workTyp").show();
        } else {
            $("#leaveTyp").hide();
            $("#workTyp").hide();
        }
    })

    if ($("#hdnisGivenAttendance").val() == "1") {
        $("#BtnSubmitRequest").hide();
        $("#BtnExitRequest").show();
    }
    else {
        $("#BtnSubmitRequest").show();
        $("#BtnExitRequest").hide();
    }
});





var weekday = new Array(7);
weekday[0] = "Sunday";
weekday[1] = "Monday";
weekday[2] = "Tuesday";
weekday[3] = "Wednesday";
weekday[4] = "Thursday";
weekday[5] = "Friday";
weekday[6] = "Saturday";


var MonthDay = new Array(12);
MonthDay[0] = "January";
MonthDay[1] = "February";
MonthDay[2] = "March";
MonthDay[3] = "April";
MonthDay[4] = "May";
MonthDay[5] = "June";
MonthDay[6] = "July";
MonthDay[7] = "Auguest";
MonthDay[8] = "September";
MonthDay[9] = "October";
MonthDay[10] = "November";
MonthDay[11] = "December";




function updateTime() {
    var currentTime = new Date()
    var hours = currentTime.getHours()
    var minutes = currentTime.getMinutes()
    if (minutes < 10) {
        minutes = "0" + minutes
    }
    var t_str;

    if (hours > 11) {
        hours = hours - 12;
        t_str = hours + ":" + minutes + ":" + currentTime.getSeconds() + " PM";

    } else {
        t_str = hours + ":" + minutes + ":" + currentTime.getSeconds() + " AM";

    }
    document.getElementById('Day_span').innerHTML = weekday[currentTime.getDay()] + " , " + MonthDay[currentTime.getMonth()] + " " + currentTime.getDate() + " , " + currentTime.getFullYear() + "<span style='color: red;font-size: 13px;font-size: 13px;'> *</span>";
    document.getElementById('time_span').innerHTML = t_str;
}


setInterval(updateTime, 1000);




function Employeekeydown(e) {

    var OtherDetails = {}
    OtherDetails.SerarchKey = $("#txtEmpSearch").val();

    if (e.code == "Enter" || e.code == "NumpadEnter") {
        var HeaderCaption = [];
        HeaderCaption.push("Employee Name");
        HeaderCaption.push("Employee Id");
        if ($("#txtEmpSearch").val() != '') {
            callonServer("Service/AttdendanceService.asmx/GetEmployee", OtherDetails, "EmployeeTable", HeaderCaption, "EmployeeIndex", "SetEmployee");

            e.preventDefault();
            return false;
        }
    }
    else if (e.code == "ArrowDown") {
        if ($("input[EmployeeIndex=0]"))
            $("input[EmployeeIndex=0]").focus();
    }

}

function SetEmployee(id, name) {


    $.ajax({
        type: "POST",
        url: "Frm_Attendance.aspx/EmpAttendance",
        data: JSON.stringify({ "EmpID": id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            $('#hdnisGivenAttendance').val(msg.d.isGivenAttendance);
            $('#hdnIsLeaveonApprovval').val(msg.d.IsLeaveonApprovval);
            $('#hdnUserID').val(msg.d.user_id);

            if ($("#hdnisGivenAttendance").val() == "1") {
                $("#BtnSubmitRequest").hide();
                $("#BtnExitRequest").show();
            }
            else {
                $("#BtnSubmitRequest").show();
                $("#BtnExitRequest").hide();
            }

            $('#EmployeeModel').modal('hide');
            document.getElementById('EmployeeNameSpan').innerText = name;
            $('#EmpId').val(id);
        }
    });

}

function EmployeeSelect() {
    $('#EmployeeModel').modal('show');
}



function ValueSelected(e, indexName) {
    if (e.code == "Enter" || e.code == "NumpadEnter") {
        var Id = e.target.parentElement.parentElement.cells[0].innerText;
        var name = e.target.parentElement.parentElement.cells[1].children[0].value;
        if (Id) {
            SetEmployee(Id, name);
        }

    }

    else if (e.code == "ArrowDown") {
        thisindex = parseFloat(e.target.getAttribute(indexName));
        thisindex++;
        if (thisindex < 10)
            $("input[" + indexName + "=" + thisindex + "]").focus();
    }
    else if (e.code == "ArrowUp") {
        thisindex = parseFloat(e.target.getAttribute(indexName));
        thisindex--;
        if (thisindex > -1)
            $("input[" + indexName + "=" + thisindex + "]").focus();
        else {

            $('#txtEmpSearch').focus();
        }
    }

}

function AttendanceSubmit() {

    $("#inOutModal").modal("show");

    $("#markWork").val(0);
    $("#leaveTyp").hide();
    $("#workTyp").hide();


    //if ($('#EmpId').val().trim() == "") {
    //    jAlert('Please Select an Employee.', "Alert", function () {
    //            $('#BtnShowEmployee').click(); 
    //    });
    //}
    //else {
    //    var OtherDetails = {}
    //    OtherDetails.EmpId = $("#EmpId").val();
    //    $.ajax({ 
    //        type: "POST",
    //        url: "Service/AttdendanceService.asmx/SaveAttendance",
    //        data: JSON.stringify(OtherDetails),
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (msg) {
    //            jAlert(msg.d.Msg, "Alert", function (a) {
    //                setTimeout(function () {
    //                    $('#BtnShowEmployee').focus();
    //                },200);
    //            });

    //            if (msg.d.status == "Ok") {
    //                $('#EmpId').val('');
    //                document.getElementById('EmployeeNameSpan').innerText = '';
    //            }
    //        }

    //    });


    //}
}

function clearPopup() {

    var rowsArr = $('.dynamicPopupTbl')[0].rows;
    var len = rowsArr.length;
    while (rowsArr.length > 1) {
        rowsArr[rowsArr.length - 1].remove();
    }

    $('#txtEmpSearch').val('');

}


$(document).ready(function () {
    $('#EmployeeModel').on('shown.bs.modal', function () {
        clearPopup();
        $('#txtEmpSearch').focus();
    })
    $('#EmployeeModel').on('hidden.bs.modal', function () {
        if ($('#EmpId').val().trim() == "") {
            $('#BtnShowEmployee').focus();
        } else {
            $('#BtnSubmitRequest').focus();
        }
    })

    document.getElementById('EmployeeNameSpan').innerText = $('#hdEmpName').val();


    $('#BtnSubmitRequest').focus();
});

function WorkValueBind() {

    var dt = {};
    dt.userid = $('#hdnUserID').val();
    $.ajax({
        type: "POST",
        url: "Service/AttdendanceService.asmx/WorkValueBind",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dt),
        dataType: "json",
        success: function (data) {

            var status = "<select id='ddlWorkType' runat='server' class='demo' multiple='multiple' Width='100%'>";
            for (var i = 0; i < data.d.length; i++) {
                status = status + '<option value=' + data.d[i].ID + '>' + data.d[i].Descrpton + '</option>';
            }
            status = status + '</select>';
            $('#Workdiv').html(status);

            $('#ddlWorkType').multiselect("refresh");
        }
    });


}

function LeaveValueBind() {

    var dt = {};
    dt.userid = $('#hdnUserID').val();
    $.ajax({
        type: "POST",
        //url: "http://3.7.30.86:82/API/Leave/Types",
        url: "Service/AttdendanceService.asmx/LeaveValueBind",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dt),
        dataType: "json",
        success: function (data) {

            var status = "<select id='ddlLeaveType' class='demo' Width='100%'>";
            for (var i = 0; i < data.d.length; i++) {
                status = status + '<option value=' + data.d[i].id + '>' + data.d[i].name + '</option>';
            }
            status = status + '</select>';
            $('#Leavediv').html(status);
        }
    });
}

function markWork_change() {
    if ($("#markWork").val() == "1") {
        WorkValueBind();
    }
    else if ($("#markWork").val() == "2") {
        LeaveValueBind();
    }
}

function Attendance() {
    if ($("#markWork").val() == "1") {
        WorkSubmit();
    }
    else if ($("#markWork").val() == "2") {
        if ($('#hdnIsLeaveonApprovval').val() == "1") {
            LeaveOnApproval();
        }
        else {
            LeaveSubmit();
        }
    }
}

function WorkSubmit() {
    var worktyps = "";
    var datas = $("#ddlWorkType").val();
    if ($("#ddlWorkType").val() == null) {
        jAlert("Please select work type.")
        $("#ddlWorkType").focus();
        return;
    }
    for (var i = 0; i < datas.length; i++) {
        if (worktyps == "") {
            worktyps = datas[i];
        }
        else {
            worktyps += "," + datas[i];
        }
    }

    if (worktyps == "") {
        jAlert("Please select work type.")
        $("#ddlWorkType").focus();
        return;
    }

    var time = new Date();
    var add_attendence_time = time.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })

    var work_date_time = time.getFullYear() + "-" + (time.getMonth() + 1) + "-" + time.getDate() + "T" + time.getHours() + ":" + time.getMinutes() + ":" + time.getSeconds();

    var dt = {};
    dt.session_token = "sdkjfhsdjfkjhaskfjsdg";
    dt.user_id = $('#hdnUserID').val();
    dt.work_type = worktyps;
    dt.work_desc = $("#txtWorkRemarks").val(),
    dt.work_lat = "0";
    dt.work_long = "0";
    dt.work_address = "Unknown";
    dt.work_date_time = work_date_time;
    dt.is_on_leave = "false";
    dt.add_attendence_time = add_attendence_time;
    dt.route = "";
    dt.leave_from_date = "",
    dt.leave_to_date = "",
    dt.leave_type = "",
    dt.Distributor_Name = "",
    dt.Market_Worked = "",
    dt.order_taken = "0",
    dt.collection_taken = "0",
    dt.new_shop_visit = "0",
    dt.revisit_shop = "0",
    dt.state_id = "15",
    dt.shop_list = [],
    dt.primary_value_list = [],
    dt.Update_Plan_List = [],
    dt.leave_reason = "",
    dt.from_id = "",
    dt.to_id = "",
    dt.distance = ""

    $.ajax({
        type: "POST",
        url: "Frm_Attendance.aspx/AttendanceShop",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "model": dt }),
        dataType: "json",
        success: function (data) {

            if (data.d == "OK") {
                $('#hdnisGivenAttendance').val('1');
                $("#BtnSubmitRequest").hide();
                $("#BtnExitRequest").show();
                $("#inOutModal").modal("hide");
            }
        }
    });
}

function LeaveSubmit() {

    var time = new Date();
    var add_attendence_time = time.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })
    var work_date_time = time.getFullYear() + "-" + (time.getMonth() + 1) + "-" + time.getDate() + "T" + time.getHours() + ":" + time.getMinutes() + ":" + time.getSeconds();

    var dt = {};
    dt.session_token = "sdkjfhsdjfkjhaskfjsdg";
    dt.user_id = $('#hdnUserID').val();
    dt.work_lat = "0";
    dt.work_long = "0";
    dt.work_address = "Unknown";
    dt.work_date_time = work_date_time;
    dt.is_on_leave = "true";
    dt.add_attendence_time = add_attendence_time;
    dt.leave_from_date = cmbDOJ.GetText().split('-')[2] + '-' + cmbDOJ.GetText().split('-')[1] + '-' + cmbDOJ.GetText().split('-')[0],
    dt.leave_to_date = cmbLeaveEff.GetText().split('-')[2] + '-' + cmbLeaveEff.GetText().split('-')[1] + '-' + cmbLeaveEff.GetText().split('-')[0],
    dt.leave_type = $("#ddlLeaveType").val(),
    dt.state_id = "15",
    dt.leave_reason = $("#txtLeaveReason").val(),
    dt.shop_list = [],
    dt.primary_value_list = [],
    dt.Update_Plan_List = []

    $.ajax({
        type: "POST",
        url: "Frm_Attendance.aspx/AttendanceShop",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "model": dt }),
        dataType: "json",
        success: function (data) {

            $("#inOutModal").modal("hide");
        }
    });
}


function LeaveOnApproval() {

    //var time = new Date();
    //var add_attendence_time = time.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })
    //var work_date_time = time.getFullYear() + "-" + (time.getMonth() + 1) + "-" + time.getDate() + "T" + time.getHours() + ":" + time.getMinutes() + ":" + time.getSeconds();

    if ($("#ddlLeaveType").val() == "") {
        jAlert("Please enter Leave reason.");
        return false;
        $("#ddlLeaveType").focus();
    }

    var dt = {};
    dt.user_id = $('#hdnUserID').val();
    dt.session_token = "sdkjfhsdjfkjhaskfjsdg";
    dt.leave_type = $("#ddlLeaveType").val(),
    dt.leave_to_date = cmbLeaveEff.GetText().split('-')[2] + '-' + cmbLeaveEff.GetText().split('-')[1] + '-' + cmbLeaveEff.GetText().split('-')[0],
    dt.leave_reason = $("#txtLeaveReason").val(),
    dt.leave_from_date = cmbDOJ.GetText().split('-')[2] + '-' + cmbDOJ.GetText().split('-')[1] + '-' + cmbDOJ.GetText().split('-')[0],
    dt.leave_long = "0",
    dt.leave_lat = "0";
    dt.leave_add = "Unknown";

    $.ajax({
        type: "POST",
        url: "Frm_Attendance.aspx/LeaveApprovalSubmit",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "model": dt }),
        dataType: "json",
        success: function (data) {
            $("#inOutModal").modal("hide");

        }
    });
}

function LogOutSubmit() {

    var time = new Date();
    var add_attendence_time = time.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })

    var work_date_time = time.getFullYear() + "-" + (time.getMonth() + 1) + "-" + time.getDate() + " " + time.getHours() + ":" + time.getMinutes() + ":" + time.getSeconds();

    var dt = {};
    dt.session_token = "u4r3rmikoqdyazicunegiryv";
    dt.user_id = $('#hdnUserID').val();
    dt.latitude = "0";
    dt.longitude = "0";
    dt.logout_time = work_date_time;
    dt.Autologout = "0";
    dt.distance = "0.0";
    dt.address = "Unknown"

    $.ajax({
        type: "POST",
        url: "Frm_Attendance.aspx/LogOutSubmit",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "model": dt }),
        dataType: "json",
        success: function (data) {


        }
    });
}