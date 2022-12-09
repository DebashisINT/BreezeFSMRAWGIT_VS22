$(function () {
    var isLeaveList = $("#isLeaveList").val();


    if(isLeaveList==1){
        var d = new Date();
        dtFrom.SetValue(d);
        dtTo.SetValue(d);


        $('#txtfromDate').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: {
                format: 'DD-MM-YYYY'
            }
        });

        $('#txttoDate').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            mask: true,
            locale: {
                format: 'DD-MM-YYYY'
            }
        });


    }


       

  

    


$('#ddlAppIds').on('change', function () {
    if ($("#ddlAppIds option:selected").index() > 0) {
        var selectedValue = $(this).val();
        var desg = $("#drpdesignation").val();
        var usrtype = $("#ddlusertypes").val();
        $('#ddlAppIds').prop("selectedIndex", 0);
        var url = '@Url.Action("ExporRegisterList", "LeaveApplyList", new { type = "_type_" })'
        window.location.href = url.replace("_type_", selectedValue);

    }
});


var StateIdLeaveList = [];
function StateSelectionChanged(s, e) {
    StateGridLookup.gridView.GetSelectedFieldValues("ID", GetSelectedFieldValuesCallback);
}
function GetSelectedFieldValuesCallback(values) {

    try {
        StateIdLeaveList = [];
        for (var i = 0; i < values.length; i++) {
            StateIdLeaveList.push(values[i]);
        }
    } finally {
        console.log(StateIdLeaveList);
    }
}

var empId = [];
function EmpSelectionChanged(s, e) {
    EmpGridLookup.gridView.GetSelectedFieldValues("empcode", GetEmpSelectedFieldValuesCallback);
}
function GetEmpSelectedFieldValuesCallback(values) {
    try {
        empId = [];
        for (var i = 0; i < values.length; i++) {
            empId.push(values[i]);
        }
    } finally {
        console.log(empId);
    }
}

function OnStartCallback(s, e) {
    e.customArgs["Fromdate"] = dtFrom.GetText();
    e.customArgs["Todate"] = dtTo.GetText();
    //e.customArgs["selectedusrid"] = empId
    //e.customArgs["empcode"] = empId;
    //e.customArgs["StateId"] = StateId;
    e.customArgs["Is_PageLoad"] = Is_PageLoad;
}

function LookupValChange() {
    EmpGridLookup.GetGridView().Refresh()
}

function EmpStartCallback(s, e) {
    //e.customArgs["StateId"] = StateId;
    // e.customArgs["desgid"] = desigId;
}

function OnClickApplyGrid(applyuser, approveuser) {
    //var url = "http://10.0.1.154:8088/oms/management/activities/leaveapproval.aspx?key=" + applyuser + "&au=" + approveuser + "";
    var url = "/oms/management/activities/leaveapproval.aspx?key=" + applyuser + "&au=" + approveuser + "";
    window.open(url);
}

