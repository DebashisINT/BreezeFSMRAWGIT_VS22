$(document).ready(function () {

    $('#cmbStateFV').multiselect({
        includeSelectAllOption: true,
        enableFiltering: true,
        maxHeight: 400,
        enableCaseInsensitiveFiltering: true,
        onDropdownHide: function (event) {
            GetSateValueFV();
        }
    }).multiselect('selectAll', false).multiselect('updateButtonText');
    $('#cmbBranchFV').multiselect({
        includeSelectAllOption: true,
        enableFiltering: true,
        maxHeight: 400,
        enableCaseInsensitiveFiltering: true,
        onDropdownHide: function (event) {
            GetBranchValueFV();
        }
    }).multiselect('selectAll', false).multiselect('updateButtonText');

    branchidsFV = $('#cmbBranchFV').val();

    

  
    // End of Mantis Issue 24729
    stateidsFV = $('#cmbStateFV').val();
    var statelistcountFV = $('#hdnStateListCountFV').val();
    if (statelistcountFV > 0) {
        var GroupBy = $('#hdnGridStatewiseSummaryGroupByFV').val();
        for (var i = objsettings.length - 1; i >= 0; i--) {
            if (objsettings[i].ID == settingsid) {
                objsettings.splice(i, 1);
            }
        }

        // Mantis Issue 25455
        //if (settingsid == "1") {
        //    var obj = {};
        //    obj.ID = "1";
        //    obj.action = "AT_WORK";
        //    obj.rptype = "Summary";
        //    obj.empid = "";
        //    obj.stateid = stateids.join(',');// cmbState.GetValue();

        //    var isObject = typeof branchidsFV
        //    if (isObject == "object") {
        //        if (branchidsFV != null && branchidsFV.length > 0) {
        //            obj.branchid = branchidsFV.join(',');
        //        } else {
        //            obj.branchid = "";

        //        }
        //    } else {
        //        if (branchidsFV != null && branchidsFV.length > 0) {
        //            obj.branchid = branchidsFV;
        //        }
        //        else {
        //            obj.branchid = "";
        //        }
        //    }
        //    obj.designid = "";
        //    objsettings.push(obj);
        //}

        //WindowSize = $(window).width();


        //$("#lblAtWorkFV").html("<img src='/assests/images/Spinner.gif' />");
        //$("#lblOnLeaveFV").html("<img src='/assests/images/Spinner.gif' />");
        //$("#lblNotLoggedInFV").html("<img src='/assests/images/Spinner.gif' />");
        //$("#lblTotalFV").html("<img src='/assests/images/Spinner.gif' />");
        //stateid = stateidsFV.join(',');// cmbState.GetValue();
        //$("#salesmanheader").html("State wise Summary");
        //$("#salesmanheaderTeam").html("State wise Summary");
        //stateid = stateidsFV.join(',');// cmbState.GetValue();
        //// Mantis Issue 24729
        //var isObject = typeof branchidsFV
        //if (isObject == "object") {
        //    if (branchidsFV != null && branchidsFV.length > 0) {
        //        branchid = branchidsFV.join(',');
        //    } else {
        //        branchid = "";

        //    }
        //} else {
        //    if (branchidsFV != null && branchidsFV.length > 0) {
        //        branchid = branchidsFV;
        //    }
        //    else {
        //        branchid = "";
        //    }
        //}

        //objData = {};

        //var hdnTotalEmployeesFV = $('#hdnTotalEmployeesFV').val();
        //var hdnAtWorkFV = $('#hdnAtWorkFV').val();
        //var hdnOnLeaveFV = $('#hdnOnLeaveFV').val();
        //var hdnNotLoggedInFV = $('#hdnNotLoggedInFV').val();
        //var hdnStatewiseSummaryFV = $('#hdnStatewiseSummaryFV').val();
        //var hdnStatewiseSummaryTeamFV = $('#hdnStatewiseSummaryTeamFV').val();

        //var obj = {};
        //obj.stateid = stateid;
        //// Mantis Issue 24729
        //obj.branchid = branchid;
        //// End of Mantis Issue 24729

        //if (hdnTotalEmployeesFV > 0 || hdnAtWorkFV > 0 || hdnOnLeaveFV > 0 || hdnNotLoggedInFV > 0) {
        //    $.ajax({
        //        type: "POST",
        //        url: "/DashboardMenu/GetDashboardData",
        //        data: JSON.stringify(obj),
        //        async: true,
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (response) {
        //            if (hdnAtWorkFV > 0) {
        //                $("#lblAtWorkFV").html(response.lblAtWork);
        //            }
        //            if (hdnOnLeaveFV > 0) {
        //                $("#lblOnLeaveFV").html(response.lblOnLeave);
        //            }
        //            if (hdnNotLoggedInFV > 0) {
        //                $("#lblNotLoggedInFV").html(response.lblNotLoggedIn);
        //            }
        //            if (hdnTotalEmployeesFV > 0) {
        //                $("#lblTotalFV").html(response.lblTotal);
        //            }
        //        },
        //        error: function (response) {
        //            jAlert("Please try again later");
        //        }
        //    });
        //    // ajax Team visit
        //    if ($("#hdnStatewiseSummaryTeamTabFV").val() == 1) {
        //        TeamVisitData(obj)
        //    }
        //}
        
        //if (hdnStatewiseSummaryFV > 0) {
        //    //gridsalesman.ClearFilter();
        //    //gridsalesman.Refresh();
        //}
        // End of Mantis Issue 25455
    }
    else {
        $('.bodymain_areastatewise').hide();
    }

});


$(document).ready(function () {

    $('#mapdeatsilsFV').hide();
    $('.closeAcFV').click(function () {
        $('#dashDetails1FV').addClass('hide');
        $('#dashDetails2FV').addClass('hide');
        $('.boxWidgetFV').removeClass('activeW');
    });
    $('.closeAcDetailsFV').click(function () {
        $('#dashDetails2FV').removeClass('hide');
        $('#dashDetails1FV').addClass('hide');
        $('#gridsummaryFV').show('hide');
    });
    $('.closeAcDetails2FV').click(function () {

        $('#dashDetails1FV').removeClass('hide');
        $('#gridthirdlevelFV').addClass('hide');
        $('#gridsummaryFV').show('hide');
    });
    $('#close1FV').click(function () {
        $('#mapdeatsilsFV').hide();
        $('#mapFV').show();
    });
    // AtWorkClick(this);
})





// new script


var stateidsFV = [];
// Mantis Issue 24729
var branchidsFV = [];
   

function GetSateValueFV() {
    stateidsFV = [];

    var datalist = $('#cmbStateFV').val();
    if (datalist != null) {
        stateidsFV = datalist;
    }
    else {
        stateidsFV.push('999999');
    }
    cmbStatechangeFV();
}

// Mantis Issue 24729
function GetBranchValueFV() {
    branchidsFV = [];

    var datalist = $('#cmbBranchFV').val();
    if (datalist != null) {
        branchidsFV = datalist;
    }
    else {
        branchidsFV.push('999999');
    }
    cmbBranchChangeFV();
}
    
function cmbBranchChangeFV() {
    let newBranch = $('#cmbBranchFV').val();

    var isObject = typeof newBranch
    if (isObject == "object") {
        if (newBranch != null && newBranch.length > 0) {
            branchidsFV = newBranch.join(',');
        } else {
            branchidsFV = "";

        }
    } else {
        if (newBranch != null && newBranch.length > 0) {
            branchidsFV = newBranch;
        }
        else {
            branchidsFV = "";
        }
    }
    reloadBoxDataFV(branchidsFV);
}

// End of Mantis Issue 24729
// Mantis Issue 25455
function ShowFieldVisitData() {
    if (settingsid == "1") {
        var obj = {};
        obj.ID = "1";
        obj.action = "AT_WORK";
        obj.rptype = "Summary";
        obj.empid = "";
        obj.stateid = stateids.join(',');// cmbState.GetValue();

        var isObject = typeof branchidsFV
        if (isObject == "object") {
            if (branchidsFV != null && branchidsFV.length > 0) {
                obj.branchid = branchidsFV.join(',');
            } else {
                obj.branchid = "";

            }
        } else {
            if (branchidsFV != null && branchidsFV.length > 0) {
                obj.branchid = branchidsFV;
            }
            else {
                obj.branchid = "";
            }
        }
        obj.designid = "";
        objsettings.push(obj);
    }

    WindowSize = $(window).width();

    $(".fieldvisit-tb-hand").hide();
    $("#lblAtWorkFV").html("<img src='/assests/images/Spinner.gif' />");
    $("#lblOnLeaveFV").html("<img src='/assests/images/Spinner.gif' />");
    $("#lblNotLoggedInFV").html("<img src='/assests/images/Spinner.gif' />");
    $("#lblTotalFV").html("<img src='/assests/images/Spinner.gif' />");
    stateid = stateidsFV.join(',');// cmbState.GetValue();
    $("#salesmanheader").html("State wise Summary");
    $("#salesmanheaderTeam").html("State wise Summary");
    stateid = stateidsFV.join(',');// cmbState.GetValue();
    // Mantis Issue 24729
    var isObject = typeof branchidsFV
    if (isObject == "object") {
        if (branchidsFV != null && branchidsFV.length > 0) {
            branchid = branchidsFV.join(',');
        } else {
            branchid = "";

        }
    } else {
        if (branchidsFV != null && branchidsFV.length > 0) {
            branchid = branchidsFV;
        }
        else {
            branchid = "";
        }
    }

    objData = {};

    var hdnTotalEmployeesFV = $('#hdnTotalEmployeesFV').val();
    var hdnAtWorkFV = $('#hdnAtWorkFV').val();
    var hdnOnLeaveFV = $('#hdnOnLeaveFV').val();
    var hdnNotLoggedInFV = $('#hdnNotLoggedInFV').val();
    var hdnStatewiseSummaryFV = $('#hdnStatewiseSummaryFV').val();
    var hdnStatewiseSummaryTeamFV = $('#hdnStatewiseSummaryTeamFV').val();

    var obj = {};
    obj.stateid = stateid;
    // Mantis Issue 24729
    obj.branchid = branchid;
    // End of Mantis Issue 24729

    if (hdnTotalEmployeesFV > 0 || hdnAtWorkFV > 0 || hdnOnLeaveFV > 0 || hdnNotLoggedInFV > 0) {
        $.ajax({
            type: "POST",
            url: "/DashboardMenu/GetDashboardData",
            data: JSON.stringify(obj),
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (hdnAtWorkFV > 0) {
                    $("#lblAtWorkFV").html(response.lblAtWork);
                }
                if (hdnOnLeaveFV > 0) {
                    $("#lblOnLeaveFV").html(response.lblOnLeave);
                }
                if (hdnNotLoggedInFV > 0) {
                    $("#lblNotLoggedInFV").html(response.lblNotLoggedIn);
                }
                if (hdnTotalEmployeesFV > 0) {
                    $("#lblTotalFV").html(response.lblTotal);
                }
            },
            error: function (response) {
                jAlert("Please try again later");
            }
        });
        // ajax Team visit
        if ($("#hdnStatewiseSummaryTeamTabFV").val() == 1) {
            TeamVisitData(obj)
        }
    }
}
// End of Mantis Issue 25455
// Susanta

var stateidFV = "";
var designationFV = "";
var statefilteridFV = "";
var EMPCODEFV = "";
var WindowSize = 0;


var objsettingsFV = [];
var objFV = {};
objFV.ID = "1";
objFV.action = "AT_WORK";
objFV.rptype = "Summary";
objFV.empid = "";
objFV.stateid = "15";
objFV.designid = "";

objsettingsFV.push(obj);

var objFV = {};
objFV.ID = "2";
objFV.action = "";
objFV.rptype = "";
objFV.empid = "bbb";
objFV.stateid = "bbb";
objFV.designid = "bbb";
objsettingsFV.push(obj);


var objFV = {};
objFV.ID = "3";
objFV.action = "ccc";
objFV.rptype = "ccc";
objFV.empid = "ccc";
objFV.stateid = "ccc";
objFV.designid = "ccc";
objsettingsFV.push(obj);

var settingsidFV = "1";



function salesbackclick() {
    var GroupBy = $('#hdnGridStatewiseSummaryGroupByFV').val();
    settingsid = parseInt(settingsid) - 1;

    if (settingsid == "1") {
        $("#salesmanheader, #salesmanheaderTeam, #salesmanheaderFV").html("State wise Summary");
        $('.salesExport').addClass('mr0');
        //setTimeout(function () { gridsalesman.GroupBy(GroupBy); }, 2000);
    }
    else if (settingsid == "2") {
        $("#salesmanheader, #salesmanheaderTeam, #salesmanheaderFV").html("Employee wise Summary");
        //setTimeout(function () { gridsalesman.GroupBy(GroupBy); }, 2000);
    }
    else if (settingsid == "3") {
        $("#salesmanheader, #salesmanheaderTeam, #salesmanheaderFV").html("Details Of: " + Salesmanname);
    }
   
}

function gridsalesmanclick(degsi) {
    if (settingsid == "1") {

        settingsid = parseInt(settingsid) + 1;


        for (var i = objsettings.length - 1; i >= 0; i--) {
            if (objsettings[i].ID == settingsid) {
                objsettings.splice(i, 1);
            }
        }
        degid = degsi;
       
        $("#salesmanheader, #salesmanheaderTeam").html("Employee wise Summary");

        if (settingsid == "2") {
            var obj = {};
            obj.ID = "2";
            obj.action = "AT_WORK";
            obj.rptype = "Detail";
            obj.empid = "";
            obj.stateid = stateids.join(','); //cmbState.GetValue();
            // Mantis Issue 24729

            var isObject = typeof branchids
            if (isObject == "object") {
                if (branchids != null && branchids.length > 0) {
                    obj.branchid = branchids.join(',');
                } else {
                    obj.branchid = "";

                }
            } else {
                if (branchids != null && branchids.length > 0) {
                    obj.branchid = branchids;
                }
                else {
                    obj.branchid = "";
                }
            }
            // End of Mantis Issue 24729
            obj.designid = degid;
            objsettings.push(obj);
        }
        //  gridsalesman.ClearFilter();
        $('.salesExport').removeClass('mr0');

    }
}

function assignedclick(id, EMPNAME) {

    Salesmanname = EMPNAME;
    if (settingsid == "3") {
        return;
    }
    $("#salesmanheader, #salesmanheaderTeam").html("Details Of: " + Salesmanname);
    settingsid = parseInt(settingsid) + 1;


    for (var i = objsettings.length - 1; i >= 0; i--) {
        if (objsettings[i].ID == settingsid) {
            objsettings.splice(i, 1);
        }
    }

    if (settingsid == "3") {
        var obj = {};
        obj.ID = "3";
        obj.action = "VISITTODAY";
        obj.rptype = "Detail";
        obj.empid = id;
        obj.stateid = stateids.join(',');//cmbState.GetValue();

        var isObject = typeof branchids
        if (isObject == "object") {
            if (branchids != null && branchids.length > 0) {
                obj.branchid = branchids.join(',');
            } else {
                obj.branchid = "";

            }
        } else {
            if (branchids != null && branchids.length > 0) {
                obj.branchid = branchids;
            }
            else {
                obj.branchid = "";
            }
        }

        //if (branchids != null && branchids.length > 0) {
        //    obj.branchid = branchids.join(',');
        //}
        //else {
        //    obj.branchid = "";
        //}
        obj.designid = "";
        objsettings.push(obj);
    }
}
function cmbStatechangeFV() {
    settingsid = "1";

    for (var i = objsettings.length - 1; i >= 0; i--) {
        if (objsettings[i].ID == settingsid) {
            objsettings.splice(i, 1);
        }
    }
    if (settingsid == "1") {
        var obj = {};
        obj.ID = "1";
        obj.action = "AT_WORK";
        obj.rptype = "Summary";
        obj.empid = "";
        obj.stateid = stateidsFV.join(',');//cmbState.GetValue();
        obj.designid = "";
        // Mantis Issue 24729
        var isObject = typeof branchidsFV
        if (isObject == "object") {
            if (branchidsFV != null && branchidsFV.length > 0) {
                obj.branchid = branchidsFV.join(',');
            } else {
                obj.branchid = "";

            }
        } else {
            if (branchidsFV != null && branchidsFV.length > 0) {
                obj.branchid = branchidsFV;
            }
            else {
                obj.branchid = "";
            }
        }


        // End of Mantis Issue 24729
        objsettings.push(obj);
    }
    //  gridsalesman.ClearFilter();
    if (!$('#dashDetails1').hasClass('hide')) {
        $('#dashDetails1').addClass('hide');
    }
    if (!$('#dashDetails2').hasClass('hide')) {
        $('#dashDetails2').addClass('hide');
    }
    if (!$('#gridthirdlevel').hasClass('hide')) {
        $('#gridthirdlevel').addClass('hide');
    }
    $('.boxWidget').removeClass('activeW');

    stateidFV = stateidsFV.join(','); //;cmbState.GetValue();
    // Mantis Issue 24729
    var obj1 = {};
    obj1.stateid = stateidFV;

    $.ajax({
        type: "POST",
        url: "/DashboardMenu/DashboardBranchCombobox",
        data: JSON.stringify(obj1),
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //console.log("res branch", response);
            const mainData = response.map(function (item) {
                return {
                    label: item.name,
                    title: item.name,
                    value: item.BranchID,
                    selected: true
                }
                // arr.push(obj)
            })

            $("#cmbBranchFV").multiselect('dataprovider', mainData);
            var newBranch = $('#cmbBranchFV').val();

            var isObject = typeof newBranch
            if (isObject == "object") {
                if (newBranch != null && newBranch.length > 0) {
                    branchidsFV = newBranch.join(',');
                } else {
                    branchidsFV = "";

                }
            } else {
                if (newBranch != null && newBranch.length > 0) {
                    branchidsFV = newBranch;
                }
                else {
                    branchidsFV = "";
                }
            }

            reloadBoxDataFV(branchidsFV);
        },
        error: function (response) {
            jAlert("Please try again later");
        }
    });


}

function reloadBoxDataFV(branchidsFV) {
    branchid = branchidsFV;

    objData = {};
    $('#mapdeatsilsFV').hide();
    $('#mapFV').show();

    var obj = {};
    obj.stateid = stateid;
    // Mantis Issue 24729
    obj.branchid = branchid;
    // End of Mantis Issue 24729
    $.ajax({
        type: "POST",
        url: "/DashboardMenu/GetDashboardData",
        data: JSON.stringify(obj),
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#lblAtWorkFV").text(response.lblAtWork);
            $("#lblOnLeaveFV").text(response.lblOnLeave);
            $("#lblNotLoggedInFV").text(response.lblNotLoggedIn);
            $("#lblTotalFV").text(response.lblTotal);
            $("#lbltotalshopFV").text(response.TotalVisit);
            $("#lblnewvisitFV").text(response.NewVisit);
            $("#lblrevisitFV").text(response.ReVisit);

            $("#lblavgvisitsFV").text(response.AvgPerDay);
            $("#lblavgdurationFV").text(response.AvgDurationPerShop);
        },
        error: function (response) {
            jAlert("Please try again later");
        }
    });
}

var Type = "";
var FilterName = "";

function AtWorkClickFV(elem) {
    var GroupBy = $('#hdnGridEmployeeAttendanceSummaryGroupByFV').val();
    document.getElementById("griddetailsFV").style.display = "none";
    document.getElementById("gridsummaryFV").style.display = "block";



    $('#dashDetails2FV').removeClass('hide');
    $('.boxWidget').removeClass('activeW');

    //divBGcolor = $(elem).css('background');

    divBGcolor = "transparent";
    $('#a1FV').addClass('activeW');
    $('#collapseTwoFV').addClass('in');
    Type = "Attendance";
    FilterName = "AT_WORK";
    stateid = stateids.join(',');// cmbState.GetValue();

    //$("#gridHeadedtext").text("Employee Attendance Summary - Employees At Work");

    //gridsummarydashboard.Refresh();
    //gridsummarydashboard.Refresh();
    gridsummarydashboardclickFV(null, null);
    $('#backSpnFV').attr("Style", "Display:none;");
    //setTimeout(function () { gridsummarydashboard.GroupBy(GroupBy); }, 1000);
    griddashboardFV.Refresh();
    griddashboardFV.Refresh();
}

function NotLoggedInClickFV(elem) {
    var GroupBy = $('#hdnGridEmployeeAttendanceSummaryGroupByFV').val();
    document.getElementById("griddetailsFV").style.display = "none";
    document.getElementById("gridsummaryFV").style.display = "block";

    //divBGcolor = $(elem).css('background');

    divBGcolor = "transparent";

    $('#dashDetails2FV').removeClass('hide');
    $('.boxWidget').removeClass('activeW');
    $('#a3FV').addClass('activeW');
    $('#collapseTwoFV').addClass('in');
    Type = "Attendance";
    FilterName = "NOT_LOGIN";
    stateid = stateids.join(',');//cmbState.GetValue();

    //$("#gridHeadedtext").text("Employee Attendance Summary - Employees Not Logged In");

    //gridsummarydashboard.Refresh();
    //gridsummarydashboard.Refresh();
    gridsummarydashboardclickFV(null, null);
    $('#backSpnFV').attr("Style", "Display:none;");
    //setTimeout(function () { gridsummarydashboard.GroupBy(GroupBy); }, 1000);
    griddashboardFV.Refresh();
    griddashboardFV.Refresh();
}

function OnLeaveClickFV(elem) {
    var GroupBy = $('#hdnGridEmployeeAttendanceSummaryGroupByFV').val();
    document.getElementById("griddetailsFV").style.display = "none";
    document.getElementById("gridsummaryFV").style.display = "block";

    // divBGcolor = $(elem).css('background');
    divBGcolor = "transparent";


    $('#dashDetails2FV').removeClass('hide');
    $('.boxWidget').removeClass('activeW');
    $('#a2FV').addClass('activeW');
    $('#collapseTwoFV').addClass('in');
    Type = "Attendance";
    FilterName = "ON_LEAVE";
    stateid = stateids.join(',');// cmbState.GetValue();

    //$("#gridHeadedtext").text("Employee Attendance Summary - Employees On Leave");
    //gridsummarydashboard.Refresh();
    //gridsummarydashboard.Refresh();
    gridsummarydashboardclickFV(null, null);
    $('#backSpnFV').attr("Style", "Display:none;");
    //setTimeout(function () { gridsummarydashboard.GroupBy(GroupBy); }, 1000);
    griddashboardFV.Refresh();
    setTimeout(function () {
        griddashboardFV.Refresh();
    }, 100)
}

function OnTotalClickFV(elem) {
    var GroupBy = $('#hdnGridEmployeeAttendanceSummaryGroupByFV').val();
    document.getElementById("griddetailsFV").style.display = "none";
    document.getElementById("gridsummaryFV").style.display = "block";

    //divBGcolor = $(elem).css('background');
    divBGcolor = "transparent";

    $('#dashDetails2FV').removeClass('hide');
    $('.boxWidget').removeClass('activeW');
    $('#a4FV').addClass('activeW');
    $('#collapseTwoFV').addClass('in');
    Type = "Attendance";
    FilterName = "EMP";
    stateid = stateids.join(',');// cmbState.GetValue();

    //$("#gridHeadedtext").text("Employee Attendance Summary - Total Employees");
    //gridsummarydashboard.Refresh();
    //gridsummarydashboard.Refresh();
    gridsummarydashboardclickFV(null, null);
    //setTimeout(function () { gridsummarydashboard.GroupBy(GroupBy); }, 1000);
    $('#backSpnFV').attr("Style", "Display:none;");
}



function shopclickFV(id, EMPNAME) {
    Salesman_EMPCODE = id;
    $("#gridshoptextFV").text("Todays visit of " + EMPNAME);
    $('#dashDetails1FV').addClass('hide');
    $('#gridthirdlevelFV').removeClass('hide');
    griddashboarddetailsFV.Refresh();
    // griddashboarddetails.Refresh();


}


function showMapFV() {

    stateid = stateids.join(',');// cmbState.GetValue();
    GetAddressFV(stateid);
    $('#mapFV').removeClass('hide');
}
function mapcloseclickFV() {
    $("#mapFV").addClass("hide");
}

// Team visit End








///Next portion remove if not required

function gridsummarydashboardEndcallback() {
    $("#gridsummarydashboard_DXMainTable > tbody > tr > td.dxgvHeader_PlasticBlue").css({ 'background': divBGcolor });
    $("#gridsummarydashboard_DXFooterRow").removeClass("dxgvFooter_PlasticBlue");
    $("#gridsummarydashboard_DXFooterRow td:eq(2)").css({ 'text-align': 'center' });

    $("#gridsummarydashboard_DXFooterRow td:eq(2)").css({ 'color': '#2443AA' });
    $("#gridsummarydashboard_DXFooterRow td:eq(2)").css({ 'font-weight': 'bold' });

    $("#gridsummarydashboard_DXFooterRow td:eq(2)").css({ 'padding': '6px 6px 7px' });

    if ($("#hdnExportDashboardSummaryGridListCount").val() == '0') {


        $('#imgexport').hide();
    }
    else {

        $('#imgexport').show();
    }

}

function locationclick(userid, username) {

    if (!$('#dashDetails1').hasClass('hide')) {
        $('#dashDetails1').addClass('hide');
    }
    if (!$('#dashDetails2').hasClass('hide')) {
        $('#dashDetails2').addClass('hide');
    }
    if (!$('#gridthirdlevel').hasClass('hide')) {
        $('#gridthirdlevel').addClass('hide');
    }

    $('#mapdeatsils').show();
    $('#map').hide();
    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = d.getFullYear() + '/' +
        (month < 10 ? '0' : '') + month + '/' +
        (day < 10 ? '0' : '') + day;

    var Url = '@Url.Action("GetSalesmanLocation", "DashboardMenu")'

    $.ajax({
        url: Url,
        async: false,
        type: 'POST',
        data: {
            selectedusrid: userid,
            Date: output,
            IsGmap: "true"
        },
        error: function () {
            $('#info').html('<p>An error has occurred</p>');
        },

        success: function (markers) {

            document.getElementById('directions_panel').innerHTML = '';
            $("#directionname").html("<b>Name : " + username + "</b>");
            if (markers != '') {
                var directionsDisplay;
                var directionsService = new google.maps.DirectionsService();
                directionsDisplay = new google.maps.DirectionsRenderer(
                    );
                var infoWindow = new google.maps.InfoWindow();
                //////// 22/01/2018
                var waypts = [];
                var latlng = new google.maps.LatLng(28.535516, 77.391026);
                // var latlng = new google.maps.LatLng(lat, long);
                map = new google.maps.Map(document.getElementById('mapdetailsshow'), {
                    // center: latlng,
                    zoom: 16,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                });
                //////// 22/01/18
                //directionsDisplay.setMap(map);
                directionsDisplay = new google.maps.DirectionsRenderer({ map: map, suppressMarkers: true, suppressInfoWindows: false });
                for (i = 0; i < markers.salesmanarea.length; i++) {
                    var data = markers.salesmanarea[i];
                    waypts.push({
                        location: data.location,
                        stopover: true,
                    });
                    //createMarker(data.location);
                }
                //////// 22/01/18
                var start = markers.latlanLogin;
                var end = markers.latlanLogout;
                var request = {
                    origin: start,
                    destination: end,
                    waypoints: waypts,
                    optimizeWaypoints: true,
                    travelMode: google.maps.TravelMode.DRIVING
                };
                var infowindow_forRoutedMarker = new google.maps.InfoWindow({
                    content: '22'
                });
                if (markers.Fullresponse != null) {
                    directionsDisplay.setDirections($.parseJSON(markers.Fullresponse));
                    directionsDisplay.setDirections($.parseJSON(markers.Fullresponse));
                    var route = $.parseJSON(markers.Fullresponse).routes[0];
                    var summaryPanel = document.getElementById('directions_panel');
                    var infowindow = new google.maps.InfoWindow();
                    var drivingdistancesave = '';
                    var drivingdistance = '';
                    summaryPanel.innerHTML = '';
                    var distance = 0.0000;
                    var distancecount = 0;
                    // For each route, display summary information.
                    $("#lbltotaldistancecovered").html('');
                    for (var i = 0; i < route.legs.length; i++) {
                        var routeSegment = i + 1;
                        var x = route.legs[i].distance.text;
                        // alert(x);
                        if (x.includes('km')) {
                            //alert(parseFloat(x.replace('km', '').trim()));
                            distance = parseFloat(x.replace('km', '').trim());
                        }
                        else {
                            distance = parseFloat(x.replace('m', '').trim()) / 1000.0000;
                        }
                        distancecount = distancecount + distance;
                        if (i == 0) {
                            summaryPanel.innerHTML += '<div class="mainLabel"> Travel Point: ' + routeSegment + '</div>';
                            summaryPanel.innerHTML += '<p style="color:#1f941f;font-family: Montserrat, sans-serif !important;font-size: 11px;font-weight: 600;">' + route.legs[i].start_address + ' </p>  ';

                            summaryPanel.innerHTML += '0 km' + '<br><br>';

                            drivingdistancesave = route.legs[i].start_location + '|' + '0' + '^';

                            var marker = new google.maps.Marker({
                                position: route.legs[i].start_location,
                                label: "" + (i + 1),
                                map: map,
                                icon: '@weburl' + 'markerloginlogout.png'

                            });
                        }
                        else {

                            summaryPanel.innerHTML += '<div class="mainLabel">Travel Point: ' + routeSegment + '</div>';
                            summaryPanel.innerHTML += '<p style="color:#1f941f;font-family: Montserrat, sans-serif !important;font-size: 11px;font-weight: 600;">' + route.legs[i].start_address + ' </p>  ';

                            summaryPanel.innerHTML += route.legs[i - 1].distance.text + '<br><br>';

                            drivingdistancesave = route.legs[i].start_location + '|' + route.legs[i - 1].distance.text + '^';
                            var marker = new google.maps.Marker({
                                position: route.legs[i].start_location,
                                label: "" + (i + 1),
                                map: map

                            });
                        }
                        drivingdistance = drivingdistance + drivingdistancesave;
                    }
                    summaryPanel.innerHTML += '<b>Travel Point: ' + (i + 1) + '</b><br>' +
                       ' <b style="color:green">' + route.legs[i - 1].end_address + ' </b>' + route.legs[i - 1].distance.text + '<br/><br/>  ';

                    summaryPanel.innerHTML += '<div><b>Total Distance covered: </b> ' + distancecount.toFixed(2) + ' km' + '</div>'

                    var marker = new google.maps.Marker({
                        position: route.legs[i - 1].end_location,
                        label: "" + (i + 1),
                        map: map,
                        animation: google.maps.Animation.DROP,
                        icon: '@weburl' + 'markerloginlogout.png'
                    });


                    drivingdistancesave = route.legs[i - 1].end_location + '|' + route.legs[i - 1].distance.text + '^';

                    drivingdistance = drivingdistance + drivingdistancesave;
                    //marker.addListener('click', function () {
                    //    infowindow.open(map, marker);
                    //});
                    drivingdistance = drivingdistance.substring(0, drivingdistance.length - 1);
                    //  SubmitRequestedLocation(drivingdistance, JSON.stringify(response));
                    $("#lbltotaldistancecovered").val(drivingdistance);
                    //$("#lbltotaldistancecovered").html('Distance Covered: ' + distancecount+' km');
                }
                else {
                    directionsService.route(request, function (response, status) {
                        if (status == google.maps.DirectionsStatus.OK) {
                            //alert(JSON.stringify(response));
                            // objToString(response)
                            directionsDisplay.setDirections(response);
                            var route = response.routes[0];
                            var summaryPanel = document.getElementById('directions_panel');
                            var infowindow = new google.maps.InfoWindow();
                            var drivingdistancesave = '';
                            var drivingdistance = '';
                            summaryPanel.innerHTML = '';
                            var distance = 0;
                            var distancecount = 0;
                            // For each route, display summary information.
                            $("#lbltotaldistancecovered").html('');
                            for (var i = 0; i < route.legs.length; i++) {
                                var routeSegment = i + 1;
                                var x = route.legs[i].distance.text;
                                // alert(x);
                                if (x.includes('km')) {
                                    //  alert(parseFloat(x.replace('km', '').trim()));
                                    distance = parseFloat(x.replace('km', '').trim());
                                }
                                else {
                                    // alert(parseFloat(x.replace('m', '').trim()) / 1000.0000);
                                    distance = parseFloat(x.replace('m', '').trim()) / 1000.0000;
                                }
                                distancecount = distancecount + distance;
                                if (i == 0) {
                                    summaryPanel.innerHTML += '<div class="mainLabel">Travel Point: ' + routeSegment + '</div>';
                                    summaryPanel.innerHTML += '<p style="color:#1f941f;font-family: Montserrat, sans-serif !important;font-size: 11px;font-weight: 600;">' + route.legs[i].start_address + ' </p>  ';
                                    summaryPanel.innerHTML += '0 km' + '<br><br>';
                                    drivingdistancesave = route.legs[i].start_location + '|' + '0' + '^';
                                    var marker = new google.maps.Marker({
                                        position: route.legs[i].start_location,
                                        label: "" + (i + 1),
                                        map: map,
                                        icon: '@weburl' + 'markerloginlogout.png'
                                    });
                                }
                                else {

                                    summaryPanel.innerHTML += '<div class="mainLabel">Travel Point: ' + routeSegment + '</div>';
                                    summaryPanel.innerHTML += '<p style="color:#1f941f;font-family: Montserrat, sans-serif !important;font-size: 11px;font-weight: 600;">' + route.legs[i].start_address + ' </p>  ';
                                    summaryPanel.innerHTML += route.legs[i - 1].distance.text + '<br><br>';
                                    drivingdistancesave = route.legs[i].start_location + '|' + route.legs[i - 1].distance.text + '^';
                                    var marker = new google.maps.Marker({
                                        position: route.legs[i].start_location,
                                        label: "" + (i + 1),
                                        map: map

                                    });
                                }
                                drivingdistance = drivingdistance + drivingdistancesave;
                            }
                            summaryPanel.innerHTML += '<div class="mainLabel">Travel Point: ' + (i + 1) + '</div>' +
                               ' <p style="color:#1f941f;font-family: Montserrat, sans-serif !important;font-size: 11px;font-weight: 600;">' + route.legs[i - 1].end_address + ' </p>' + route.legs[i - 1].distance.text + '<br/><br/>  ';

                            summaryPanel.innerHTML += '<div><b>Total Distance covered: </b> ' + distancecount.toFixed(2) + ' km' + '</div>'

                            var marker = new google.maps.Marker({
                                position: route.legs[i - 1].end_location,
                                label: "" + (i + 1),
                                map: map,
                                animation: google.maps.Animation.DROP,
                                icon: '@weburl' + 'markerloginlogout.png'
                            });
                            drivingdistancesave = route.legs[i - 1].end_location + '|' + route.legs[i - 1].distance.text + '^';

                            drivingdistance = drivingdistance + drivingdistancesave;
                            //marker.addListener('click', function () {
                            //    infowindow.open(map, marker);
                            //});
                            drivingdistance = drivingdistance.substring(0, drivingdistance.length - 1);
                            SubmitRequestedLocation(drivingdistance, JSON.stringify(response));
                            $("#lbltotaldistancecovered").val(drivingdistance);
                            //$("#lbltotaldistancecovered").html('Distance Covered: ' + distancecount+' km');
                        }
                    });
                }
            }

            else {
                summaryPanel.innerHTML = '';
                jAlert('No data');
            }

        },

    });



}

function createMarker(latlng) {

    var marker = new google.maps.Marker({
        position: latlng,
        map: map
    });
}


function SubmitRequestedLocation(datloaction, responseget) {

    var Url = '@Url.Action("InserSalesmanLocationforMonthlyReport", "DashboardMenu")'
    var data = { "locationdetail": datloaction, selectedusrid: $("#drpuser").val(), Date: $('#txtDate').val(), response: responseget };
    $.ajax({
        type: "POST",
        url: Url,
        processData: false,
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (r) {

        }
    });

}
function objToString(obj) {
    var str = '';
    for (var p in obj) {
        if (obj.hasOwnProperty(p)) {
            str += p + '::' + obj[p] + '\n';
        }
    }
    /// return str;

    alert(str);
}

function gridsummarydashboardclickFV(desig, state) {

    if (Type == "Tracking") {
        return;
    }

    document.getElementById("griddetailsFV").style.display = "block";
    document.getElementById("gridsummaryFV").style.display = "none";
    designation = desig;
    statefilterid = state;

    $('#dashDetails1FV').removeClass('hide');
    $('#dashDetails2FV').addClass('hide');
    //$('.boxWidget').removeClass('activeW');


    $('#collapseTwoFV').addClass('in');

    var summaryof = "";
    if (FilterName == "AT_WORK")
        summaryof = "Employees At Work";
    else if (FilterName == "ON_LEAVE")
        summaryof = "Employees On Leave";
    else if (FilterName == "NOT_LOGIN")
        summaryof = "Employees Not Logged In";
    else if (FilterName == "EMP")
        summaryof = "Total Employees";


    $("#griddetailsHeadedtextFV").text("Employee Attendance details - " + summaryof + "");
    griddashboardFV.Refresh();
    griddashboardFV.Refresh();


}


// End of Mantis Issue 24729
///

var divBGcolor = "transparent";

function GetClientDateFormat(today) {
    if (today) {
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();

        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        today = dd + '-' + mm + '-' + yyyy;
    }
    else {
        today = "";
    }

    return today;
}



// Done upto this point






