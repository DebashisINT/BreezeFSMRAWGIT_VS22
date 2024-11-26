//====================================================== Revision History ===========================================================
//@* Rev Number         DATE                VERSION          DEVELOPER           CHANGES *@
//@* 1.0                26 - 06 - 2023      2.0.41           Pallab              26413: FSM dashboard tab boxes click event disable, when "show data" button not clicked *@
//@* 2.0                07 - 07 - 2023      2.0.42           Pallab              FSM dashboard tab data not coming in ITC, when "Employees On Leave" box hide.refer: 26529
//@* 3.0                07 - 07 - 2023      V2.0.42          Sanchita            FSM Dashboard - Hierarchy Tab - Console error when clicked on Show Data.Refer: 26530*/
//@* 4.0                03 - 08 - 2023      V2.0.42          Pallab              Clicked state or branch selection without click "show data" button, tab box number data showing, disable click event.Refer: 26663*/
//====================================================== Revision History ===========================================================


$(document).ready(function () {

    $('#cmbStateTVH').multiselect({
        includeSelectAllOption: true,
        enableFiltering: true,
        maxHeight: 400,
        enableCaseInsensitiveFiltering: true,
        onDropdownHide: function (event) {
            GetSateValueTVH();
        }
    }).multiselect('selectAll', false).multiselect('updateButtonText');
    $('#cmbBranchTVH').multiselect({
        includeSelectAllOption: true,
        enableFiltering: true,
        maxHeight: 400,
        enableCaseInsensitiveFiltering: true,
        onDropdownHide: function (event) {
            GetBranchValueTVH();
        }
    }).multiselect('selectAll', false).multiselect('updateButtonText');

    branchidsTVH = $('#cmbBranchTVH').val();

    // End of Mantis Issue 24729
    stateidsTVH = $('#cmbStateTVH').val();
    var statelistcountTVH = $('#hdnStateListCountTVH').val();
    if (statelistcountTVH > 0) {
        var GroupBy = $('#hdnGridStatewiseSummaryGroupByTVH').val();
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
        //    obj.stateid = stateids.join(',');// cmbState.GeTVHalue();

        //    var isObject = typeof branchidsTVH
        //    if (isObject == "object") {
        //        if (branchidsTVH != null && branchidsTVH.length > 0) {
        //            obj.branchid = branchidsTVH.join(',');
        //        } else {
        //            obj.branchid = "";

        //        }
        //    } else {
        //        if (branchidsTVH != null && branchidsTVH.length > 0) {
        //            obj.branchid = branchidsTVH;
        //        }
        //        else {
        //            obj.branchid = "";
        //        }
        //    }
        //    obj.designid = "";
        //    objsettings.push(obj);
        //}

        //WindowSize = $(window).width();


        //$("#lblAtWorkTVH").html("<img src='/assests/images/Spinner.gif' />");
        //$("#lblOnLeaveTVH").html("<img src='/assests/images/Spinner.gif' />");
        //$("#lblNotLoggedInTVH").html("<img src='/assests/images/Spinner.gif' />");
        //$("#lblTotalTVH").html("<img src='/assests/images/Spinner.gif' />");
        //stateid = stateidsTVH.join(',');// cmbState.GeTVHalue();
        //$("#salesmanheader").html("State wise Summary");
        //$("#salesmanheaderTeam").html("State wise Summary");
        //stateid = stateidsTVH.join(',');// cmbState.GeTVHalue();
        //// Mantis Issue 24729
        //var isObject = typeof branchidsTVH
        //if (isObject == "object") {
        //    if (branchidsTVH != null && branchidsTVH.length > 0) {
        //        branchid = branchidsTVH.join(',');
        //    } else {
        //        branchid = "";

        //    }
        //} else {
        //    if (branchidsTVH != null && branchidsTVH.length > 0) {
        //        branchid = branchidsTVH;
        //    }
        //    else {
        //        branchid = "";
        //    }
        //}

        //objData = {};

        //var hdnTotalEmployeesTVH = $('#hdnTotalEmployeesTVH').val();
        //var hdnAtWorkTVH = $('#hdnAtWorkTVH').val();
        //var hdnOnLeaveTVH = $('#hdnOnLeaveTVH').val();
        //var hdnNotLoggedInTVH = $('#hdnNotLoggedInTVH').val();
        //var hdnStatewiseSummaryTVH = $('#hdnStatewiseSummaryTVH').val();
        //var hdnStatewiseSummaryTeamTVH = $('#hdnStatewiseSummaryTeamTVH').val();

        //var obj = {};
        //obj.stateid = stateid;
        //// Mantis Issue 24729
        //obj.branchid = branchid;
        //// End of Mantis Issue 24729

        //if (hdnTotalEmployeesTVH > 0 || hdnAtWorkTVH > 0 || hdnOnLeaveTVH > 0 || hdnNotLoggedInTVH > 0) {
        //    $.ajax({
        //        type: "POST",
        //        url: "/DashboardMenu/GetDashboardDataVisit",
        //        data: JSON.stringify(obj),
        //        async: true,
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (response) {
        //            if (hdnAtWorkTVH > 0) {
        //                $("#EAWTVH").html(response.lblAtWork);
        //            }
        //            if (hdnOnLeaveTVH > 0) {
        //                $("#EOLTVH").html(response.lblOnLeave);
        //            }
        //            if (hdnNotLoggedInTVH > 0) {
        //                $("#NLITVH").html(response.lblNotLoggedIn);
        //            }
        //            if (hdnTotalEmployeesTVH > 0) {
        //                $("#EMPTVH").html(response.lblTotal);
        //            }
        //        },
        //        error: function (response) {
        //            jAlert("Please try again later");
        //        }
        //    });
        //    // ajax Team visit
        //    if ($("#hdnStatewiseSummaryTeamTabTVH").val() == 1) {
        //        TeamVisitData(obj)
        //    }
        //}

        //if (hdnStatewiseSummaryTVH > 0) {
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

    $('#mapdeatsilsTVH').hide();
    $('.closeAcTVH').click(function () {
        $('#dashDetails1TVH').addClass('hide');
        $('#dashDetails2TVH').addClass('hide');
        $('.boxWidgetTVH').removeClass('activeW');
    });
    $('.closeAcDetailsTVH').click(function () {
        $('#dashDetails2TVH').removeClass('hide');
        $('#dashDetails1TVH').addClass('hide');
        $('#gridsummaryTVH').show('hide');
    });
    $('.closeAcDetails2TVH').click(function () {

        $('#dashDetails1TVH').removeClass('hide');
        $('#gridthirdlevelTVH').addClass('hide');
        $('#gridsummaryTVH').show('hide');
    });
    $('#close1TVH').click(function () {
        $('#mapdeatsilsTVH').hide();
        $('#mapTVH').show();
    });
    // AtWorkClick(this);
})





// new script


var stateidsTVH = [];
// Mantis Issue 24729
var branchidsTVH = [];


function GetSateValueTVH() {
    stateidsTVH = [];

    var datalist = $('#cmbStateTVH').val();
    if (datalist != null) {
        stateidsTVH = datalist;
    }
    else {
        stateidsTVH.push('999999');
    }
    cmbStatechangeTVH();
}

// Mantis Issue 24729
function GetBranchValueTVH() {
    branchidsTVH = [];

    var datalist = $('#cmbBranchTVH').val();
    if (datalist != null) {
        branchidsTVH = datalist;
    }
    else {
        branchidsTVH.push('999999');
    }
    cmbBranchChangeTVH();
}

function cmbBranchChangeTVH() {
    /*Rev 4.0*/
    if (!isShowTeamVisitHDataClicked) {

        return; // Disable click event
    }
    /*Rev end 4.0*/
    let newBranch = $('#cmbBranchTVH').val();

    var isObject = typeof newBranch
    if (isObject == "object") {
        if (newBranch != null && newBranch.length > 0) {
            branchidsTVH = newBranch.join(',');
        } else {
            branchidsTVH = "";

        }
    } else {
        if (newBranch != null && newBranch.length > 0) {
            branchidsTVH = newBranch;
        }
        else {
            branchidsTVH = "";
        }
    }
    reloadBoxDataTVH(branchidsTVH);
}

// End of Mantis Issue 24729
//// Mantis Issue 25455
//function ShowTeamVisitHData() {
//    if (settingsid == "1") {
//        var obj = {};
//        obj.ID = "1";
//        obj.action = "AT_WORK";
//        obj.rptype = "Summary";
//        obj.empid = "";
//        obj.stateid = stateids.join(',');// cmbState.GeTVHalue();

//        var isObject = typeof branchidsTVH
//        if (isObject == "object") {
//            if (branchidsTVH != null && branchidsTVH.length > 0) {
//                obj.branchid = branchidsTVH.join(',');
//            } else {
//                obj.branchid = "";

//            }
//        } else {
//            if (branchidsTVH != null && branchidsTVH.length > 0) {
//                obj.branchid = branchidsTVH;
//            }
//            else {
//                obj.branchid = "";
//            }
//        }
//        obj.designid = "";
//        objsettings.push(obj);
//    }

//    WindowSize = $(window).width();

//    // Mantis Issue 25455
//    //$("#lblAtWorkTVH").html("<img src='/assests/images/Spinner.gif' />");
//    //$("#lblOnLeaveTVH").html("<img src='/assests/images/Spinner.gif' />");
//    //$("#lblNotLoggedInTVH").html("<img src='/assests/images/Spinner.gif' />");
//    //$("#lblTotalTVH").html("<img src='/assests/images/Spinner.gif' />");
//    $("#EMPTVH").html("<img src='/assests/images/Spinner.gif' />");
//    $("#EAWTVH").html("<img src='/assests/images/Spinner.gif' />");
//    $("#EOLTVH").html("<img src='/assests/images/Spinner.gif' />");
//    $("#NLITVH").html("<img src='/assests/images/Spinner.gif' />");
//    // End of Mantis Issue 25455
//    stateid = stateidsTVH.join(',');// cmbState.GeTVHalue();
//    $("#salesmanheader").html("State wise Summary");
//    $("#salesmanheaderTeam").html("State wise Summary");
//    stateid = stateidsTVH.join(',');// cmbState.GeTVHalue();
//    // Mantis Issue 24729
//    var isObject = typeof branchidsTVH
//    if (isObject == "object") {
//        if (branchidsTVH != null && branchidsTVH.length > 0) {
//            branchid = branchidsTVH.join(',');
//        } else {
//            branchid = "";

//        }
//    } else {
//        if (branchidsTVH != null && branchidsTVH.length > 0) {
//            branchid = branchidsTVH;
//        }
//        else {
//            branchid = "";
//        }
//    }

//    objData = {};

//    var hdnTotalEmployeesTVH = $('#hdnTotalEmployeesTVH').val();
//    var hdnAtWorkTVH = $('#hdnAtWorkTVH').val();
//    var hdnOnLeaveTVH = $('#hdnOnLeaveTVH').val();
//    var hdnNotLoggedInTVH = $('#hdnNotLoggedInTVH').val();
//    var hdnStatewiseSummaryTVH = $('#hdnStatewiseSummaryTVH').val();
//    var hdnStatewiseSummaryTeamTVH = $('#hdnStatewiseSummaryTeamTVH').val();

//    var obj = {};
//    obj.stateid = stateid;
//    // Mantis Issue 24729
//    obj.branchid = branchid;
//    // End of Mantis Issue 24729

//    if (hdnTotalEmployeesTVH > 0 || hdnAtWorkTVH > 0 || hdnOnLeaveTVH > 0 || hdnNotLoggedInTVH > 0) {
//        $.ajax({
//            type: "POST",
//            url: "/DashboardMenu/GetDashboardDataVisitH",
//            data: JSON.stringify(obj),
//            async: true,
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function (response) {
//                if (hdnAtWorkTVH > 0) {
//                    $("#EAWTVH").html(response.lblAtWork);
//                }
//                if (hdnOnLeaveTVH > 0) {
//                    $("#EOLTVH").html(response.lblOnLeave);
//                }
//                if (hdnNotLoggedInTVH > 0) {
//                    $("#NLITVH").html(response.lblNotLoggedIn);
//                }
//                if (hdnTotalEmployeesTVH > 0) {
//                    $("#EMPTVH").html(response.lblTotal);
//                }
//            },
//            error: function (response) {
//                jAlert("Please try again later");
//            }
//        });
//        // ajax Team visit
//        if ($("#hdnStatewiseSummaryTeamTabTVH").val() == 1) {
//            TeamVisitDataH(obj)
//        }
//    }
//}
//// End of Mantis Issue 25455

// Mantis Issue 25468

/*Rev 1.0*/
var isShowTeamVisitHDataClicked = false;
/*Rev end 1.0*/

function ShowTeamVisitHData() {
    /*Rev 1.0*/
    isShowTeamVisitHDataClicked = true;
    $('#a4tvh').addClass('zoom');
    $('#a1tvh').addClass('zoom');
    $('#a2tvh').addClass('zoom');
    $('#a3tvh').addClass('zoom');
    /*Rev 2.0*/
    //var element4tvh = document.getElementById("a4tvh");
    //var element1tvh = document.getElementById("a1tvh");
    //var element2tvh = document.getElementById("a2tvh");
    //var element3tvh = document.getElementById("a3tvh");
    //element1tvh.removeAttribute('title');
    //element2tvh.removeAttribute('title');
    //element3tvh.removeAttribute('title');
    //element4tvh.removeAttribute('title');
    /*Rev end 2.0*/
    /*Rev end 1.0*/
    if (settingsid == "1") {
        var obj = {};
        obj.ID = "1";
        obj.action = "AT_WORK";
        obj.rptype = "Summary";
        obj.empid = "";
        // Rev 3.0
        //obj.stateid = stateids.join(',');// cmbState.GeTVHalue();
        obj.stateid = stateidsTVH.join(',');// cmbState.GeTVHalue();
        // End of Rev 3.0

        var isObject = typeof branchidsTVH
        if (isObject == "object") {
            if (branchidsTVH != null && branchidsTVH.length > 0) {
                obj.branchid = branchidsTVH.join(',');
            } else {
                obj.branchid = "";

            }
        } else {
            if (branchidsTVH != null && branchidsTVH.length > 0) {
                obj.branchid = branchidsTVH;
            }
            else {
                obj.branchid = "";
            }
        }
        obj.designid = "";
        objsettings.push(obj);
    }

    WindowSize = $(window).width();

    $(".teamVisitH-tb-hand").hide();
    // Mantis Issue 25455
    //$("#lblAtWorkTVH").html("<img src='/assests/images/Spinner.gif' />");
    //$("#lblOnLeaveTVH").html("<img src='/assests/images/Spinner.gif' />");
    //$("#lblNotLoggedInTVH").html("<img src='/assests/images/Spinner.gif' />");
    //$("#lblTotalTVH").html("<img src='/assests/images/Spinner.gif' />");
    $("#EMPTVH").html("<img src='/assests/images/Spinner.gif' />");
    $("#EAWTVH").html("<img src='/assests/images/Spinner.gif' />");
    $("#EOLTVH").html("<img src='/assests/images/Spinner.gif' />");
    $("#NLITVH").html("<img src='/assests/images/Spinner.gif' />");
    // End of Mantis Issue 25455
    stateid = stateidsTVH.join(',');// cmbState.GeTVHalue();
    $("#salesmanheader").html("State wise Summary");
    $("#salesmanheaderTeam").html("State wise Summary");
    stateid = stateidsTVH.join(',');// cmbState.GeTVHalue();
    // Mantis Issue 24729
    var isObject = typeof branchidsTVH
    if (isObject == "object") {
        if (branchidsTVH != null && branchidsTVH.length > 0) {
            branchid = branchidsTVH.join(',');
        } else {
            branchid = "";

        }
    } else {
        if (branchidsTVH != null && branchidsTVH.length > 0) {
            branchid = branchidsTVH;
        }
        else {
            branchid = "";
        }
    }

    objData = {};

    var hdnTotalEmployeesTVH = $('#hdnTotalEmployeesTVH').val();
    var hdnAtWorkTVH = $('#hdnAtWorkTVH').val();
    var hdnOnLeaveTVH = $('#hdnOnLeaveTVH').val();
    var hdnNotLoggedInTVH = $('#hdnNotLoggedInTVH').val();
    var hdnStatewiseSummaryTVH = $('#hdnStatewiseSummaryTVH').val();
    var hdnStatewiseSummaryTeamTVH = $('#hdnStatewiseSummaryTeamTVH').val();

    var obj = {};
    obj.stateid = stateid;
    // Mantis Issue 24729
    obj.branchid = branchid;
    // End of Mantis Issue 24729

    if (hdnTotalEmployeesTVH > 0 || hdnAtWorkTVH > 0 || hdnOnLeaveTVH > 0 || hdnNotLoggedInTVH > 0) {
        $.ajax({
            type: "POST",
            url: "/DashboardMenu/GetDashboardDataVisitH",
            data: JSON.stringify(obj),
            async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (hdnAtWorkTVH > 0) {
                    $("#EAWTVH").html(response.lblAtWork);
                }
                if (hdnOnLeaveTVH > 0) {
                    $("#EOLTVH").html(response.lblOnLeave);
                }
                if (hdnNotLoggedInTVH > 0) {
                    $("#NLITVH").html(response.lblNotLoggedIn);
                }
                if (hdnTotalEmployeesTVH > 0) {
                    $("#EMPTVH").html(response.lblTotal);
                }
            },
            error: function (response) {
                jAlert("Please try again later");
            }
        });
        // ajax Team visit
        if ($("#hdnStatewiseSummaryTeamTabTVH").val() == 1) {
            TeamVisitDataH(obj)
        }
    }
}
// End of Mantis Issue 25468
// Susanta

var stateidTVH = "";
var designationTVH = "";
var statefilteridTVH = "";
var EMPCODETVH = "";
var WindowSize = 0;


var objsettingsTVH = [];
var objTVH = {};
objTVH.ID = "1";
objTVH.action = "AT_WORK";
objTVH.rptype = "Summary";
objTVH.empid = "";
objTVH.stateid = "15";
objTVH.designid = "";

objsettingsTVH.push(obj);

var objTVH = {};
objTVH.ID = "2";
objTVH.action = "";
objTVH.rptype = "";
objTVH.empid = "bbb";
objTVH.stateid = "bbb";
objTVH.designid = "bbb";
objsettingsTVH.push(obj);


var objTVH = {};
objTVH.ID = "3";
objTVH.action = "ccc";
objTVH.rptype = "ccc";
objTVH.empid = "ccc";
objTVH.stateid = "ccc";
objTVH.designid = "ccc";
objsettingsTVH.push(obj);

var settingsidTVH = "1";



function salesbackclick() {
    var GroupBy = $('#hdnGridStatewiseSummaryGroupByTVH').val();
    settingsid = parseInt(settingsid) - 1;

    if (settingsid == "1") {
        $("#salesmanheader, #salesmanheaderTeam, #salesmanheaderTVH").html("State wise Summary");
        $('.salesExport').addClass('mr0');
        //setTimeout(function () { gridsalesman.GroupBy(GroupBy); }, 2000);
    }
    else if (settingsid == "2") {
        $("#salesmanheader, #salesmanheaderTeam, #salesmanheaderTVH").html("Employee wise Summary");
        //setTimeout(function () { gridsalesman.GroupBy(GroupBy); }, 2000);
    }
    else if (settingsid == "3") {
        $("#salesmanheader, #salesmanheaderTeam, #salesmanheaderTVH").html("Details Of: " + Salesmanname);
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
            // Rev 3.0
            //obj.stateid = stateids.join(','); //cmbState.GeTVHalue();
            obj.stateid = stateidsTVH.join(','); //cmbState.GeTVHalue();
            // End of Rev 3.0
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
        // Rev 3.0
        //obj.stateid = stateids.join(',');//cmbState.GeTVHalue();
        obj.stateid = stateidsTVH.join(',');//cmbState.GeTVHalue();
        // End of Rev 3.0

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
function cmbStatechangeTVH() {
    /*Rev 4.0*/
    if (!isShowTeamVisitHDataClicked) {

        return; // Disable click event
    }
    /*Rev end 4.0*/
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
        obj.stateid = stateidsTVH.join(',');//cmbState.GeTVHalue();
        obj.designid = "";
        // Mantis Issue 24729
        var isObject = typeof branchidsTVH
        if (isObject == "object") {
            if (branchidsTVH != null && branchidsTVH.length > 0) {
                obj.branchid = branchidsTVH.join(',');
            } else {
                obj.branchid = "";

            }
        } else {
            if (branchidsTVH != null && branchidsTVH.length > 0) {
                obj.branchid = branchidsTVH;
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

    stateidTVH = stateidsTVH.join(','); //;cmbState.GeTVHalue();
    // Mantis Issue 24729
    var obj1 = {};
    obj1.stateid = stateidTVH;

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

            $("#cmbBranchTVH").multiselect('dataprovider', mainData);
            var newBranch = $('#cmbBranchTVH').val();

            var isObject = typeof newBranch
            if (isObject == "object") {
                if (newBranch != null && newBranch.length > 0) {
                    branchidsTVH = newBranch.join(',');
                } else {
                    branchidsTVH = "";

                }
            } else {
                if (newBranch != null && newBranch.length > 0) {
                    branchidsTVH = newBranch;
                }
                else {
                    branchidsTVH = "";
                }
            }

            reloadBoxDataTVH(branchidsTVH);
        },
        error: function (response) {
            jAlert("Please try again later");
        }
    });


}

function reloadBoxDataTVH(branchidsTVH) {
    branchid = branchidsTVH;

    objData = {};
    $('#mapdeatsilsTVH').hide();
    $('#mapTVH').show();

    var obj = {};
    obj.stateid = stateid;
    // Mantis Issue 24729
    obj.branchid = branchid;
    // End of Mantis Issue 24729
    $.ajax({
        type: "POST",
        url: "/DashboardMenu/GetDashboardDataVisit",
        data: JSON.stringify(obj),
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#EAWTVH").text(response.lblAtWork);
            $("#EOLTVH").text(response.lblOnLeave);
            $("#NLITVH").text(response.lblNotLoggedIn);
            $("#EMPTVH").text(response.lblTotal);
            $("#lbltotalshopTVH").text(response.TotalVisit);
            $("#lblnewvisitTVH").text(response.NewVisit);
            $("#lblrevisitTVH").text(response.ReVisit);

            $("#lblavgvisitsTVH").text(response.AvgPerDay);
            $("#lblavgdurationTVH").text(response.AvgDurationPerShop);
        },
        error: function (response) {
            jAlert("Please try again later");
        }
    });
}

var Type = "";
var FilterName = "";

function AtWorkClickTVH(elem) {
    var GroupBy = $('#hdnGridEmployeeAttendanceSummaryGroupByTVH').val();
    document.getElementById("griddetailsTVH").style.display = "none";
    document.getElementById("gridsummaryTVH").style.display = "block";



    $('#dashDetails2TVH').removeClass('hide');
    $('.boxWidget').removeClass('activeW');

    //divBGcolor = $(elem).css('background');

    divBGcolor = "transparent";
    $('#a1TVH').addClass('activeW');
    $('#collapseTwoTVH').addClass('in');
    Type = "Attendance";
    FilterName = "AT_WORK";
    // Rev 3.0
    //stateid = stateids.join(',');// cmbState.GeTVHalue();
    stateid = stateidsTVH.join(',');// cmbState.GeTVHalue();
    // End of Rev 3.0

    //$("#gridHeadedtext").text("Employee Attendance Summary - Employees At Work");

    //gridsummarydashboard.Refresh();
    //gridsummarydashboard.Refresh();
    gridsummarydashboardclickTVH(null, null);
    $('#backSpnTVH').attr("Style", "Display:none;");
    //setTimeout(function () { gridsummarydashboard.GroupBy(GroupBy); }, 1000);
    griddashboardTVH.Refresh();
    griddashboardTVH.Refresh();
}

function NotLoggedInClickTVH(elem) {
    var GroupBy = $('#hdnGridEmployeeAttendanceSummaryGroupByTVH').val();
    document.getElementById("griddetailsTVH").style.display = "none";
    document.getElementById("gridsummaryTVH").style.display = "block";

    //divBGcolor = $(elem).css('background');

    divBGcolor = "transparent";

    $('#dashDetails2TVH').removeClass('hide');
    $('.boxWidget').removeClass('activeW');
    $('#a3TVH').addClass('activeW');
    $('#collapseTwoTVH').addClass('in');
    Type = "Attendance";
    FilterName = "NOT_LOGIN";
    // Rev 3.0
    //stateid = stateids.join(',');//cmbState.GeTVHalue();
    stateid = stateidsTVH.join(',');//cmbState.GeTVHalue();
    // End of Rev 3.0

    //$("#gridHeadedtext").text("Employee Attendance Summary - Employees Not Logged In");

    //gridsummarydashboard.Refresh();
    //gridsummarydashboard.Refresh();
    gridsummarydashboardclickTVH(null, null);
    $('#backSpnTVH').attr("Style", "Display:none;");
    //setTimeout(function () { gridsummarydashboard.GroupBy(GroupBy); }, 1000);
    griddashboardTVH.Refresh();
    griddashboardTVH.Refresh();
}

function OnLeaveClickTVH(elem) {
    var GroupBy = $('#hdnGridEmployeeAttendanceSummaryGroupByTVH').val();
    document.getElementById("griddetailsTVH").style.display = "none";
    document.getElementById("gridsummaryTVH").style.display = "block";

    // divBGcolor = $(elem).css('background');
    divBGcolor = "transparent";


    $('#dashDetails2TVH').removeClass('hide');
    $('.boxWidget').removeClass('activeW');
    $('#a2TVH').addClass('activeW');
    $('#collapseTwoTVH').addClass('in');
    Type = "Attendance";
    FilterName = "ON_LEAVE";
    // Rev 3.0
    //stateid = stateids.join(',');// cmbState.GeTVHalue();
    stateid = stateidsTVH.join(',');// cmbState.GeTVHalue();
    // End of Rev 3.0

    //$("#gridHeadedtext").text("Employee Attendance Summary - Employees On Leave");
    //gridsummarydashboard.Refresh();
    //gridsummarydashboard.Refresh();
    gridsummarydashboardclickTVH(null, null);
    $('#backSpnTVH').attr("Style", "Display:none;");
    //setTimeout(function () { gridsummarydashboard.GroupBy(GroupBy); }, 1000);
    griddashboardTVH.Refresh();
    setTimeout(function () {
        griddashboardTVH.Refresh();
    }, 100)
}

function OnTotalClickTVH(elem) {
    var GroupBy = $('#hdnGridEmployeeAttendanceSummaryGroupByTVH').val();
    document.getElementById("griddetailsTVH").style.display = "none";
    document.getElementById("gridsummaryTVH").style.display = "block";

    //divBGcolor = $(elem).css('background');
    divBGcolor = "transparent";

    $('#dashDetails2TVH').removeClass('hide');
    $('.boxWidget').removeClass('activeW');
    $('#a4TVH').addClass('activeW');
    $('#collapseTwoTVH').addClass('in');
    Type = "Attendance";
    FilterName = "EMP";
    // Rev 3.0
    //stateid = stateids.join(',');// cmbState.GeTVHalue();
    stateid = stateidsTVH.join(',');// cmbState.GeTVHalue();
    // End of Rev 3.0

    //$("#gridHeadedtext").text("Employee Attendance Summary - Total Employees");
    //gridsummarydashboard.Refresh();
    //gridsummarydashboard.Refresh();
    gridsummarydashboardclickTVH(null, null);
    //setTimeout(function () { gridsummarydashboard.GroupBy(GroupBy); }, 1000);
    $('#backSpnTVH').attr("Style", "Display:none;");
}



function shopclickTeam(id, EMPNAME) {
    Salesman_EMPCODE = id;
    $("#gridshoptextTVH").text("Todays visit of " + EMPNAME);
    $('#dashDetails1TVH').addClass('hide');
    $('#gridthirdlevelTVH').removeClass('hide');
    griddashboarddetailsTVH.Refresh();
    // griddashboarddetails.Refresh();


}


function showMapTVH() {
    // Rev 3.0
    //stateid = stateids.join(',');// cmbState.GeTVHalue();
    stateid = stateidsTVH.join(',');// cmbState.GeTVHalue();
    // End of Rev 3.0
    GetAddressTVH(stateid);
    $('#mapTVH').removeClass('hide');
}
function mapcloseclickTVH() {
    $("#mapTVH").addClass("hide");
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

function gridsummarydashboardclickTVH(desig, state) {

    if (Type == "Tracking") {
        return;
    }

    document.getElementById("griddetailsTVH").style.display = "block";
    document.getElementById("gridsummaryTVH").style.display = "none";
    designation = desig;
    statefilterid = state;

    $('#dashDetails1TVH').removeClass('hide');
    $('#dashDetails2TVH').addClass('hide');
    //$('.boxWidget').removeClass('activeW');


    $('#collapseTwoTVH').addClass('in');

    var summaryof = "";
    if (FilterName == "AT_WORK")
        summaryof = "Employees At Work";
    else if (FilterName == "ON_LEAVE")
        summaryof = "Employees On Leave";
    else if (FilterName == "NOT_LOGIN")
        summaryof = "Employees Not Logged In";
    else if (FilterName == "EMP")
        summaryof = "Total Employees";


    $("#griddetailsHeadedtextTVH").text("Employee Attendance details - " + summaryof + "");
    griddashboardTVH.Refresh();
    griddashboardTVH.Refresh();


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