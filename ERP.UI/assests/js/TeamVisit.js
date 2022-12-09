$(document).ready(function () {

    $('#cmbStateTV').multiselect({
        includeSelectAllOption: true,
        enableFiltering: true,
        maxHeight: 400,
        enableCaseInsensitiveFiltering: true,
        onDropdownHide: function (event) {
            GetSateValue();
        }
    }).multiselect('selectAll', false).multiselect('updateButtonText');
    $('#cmbBranchTV').multiselect({
        includeSelectAllOption: true,
        enableFiltering: true,
        maxHeight: 400,
        enableCaseInsensitiveFiltering: true,
        onDropdownHide: function (event) {
            GetBranchValueTV();
        }
    }).multiselect('selectAll', false).multiselect('updateButtonText');

    branchidsTV = $('#cmbBranchTV').val();

    // End of Mantis Issue 24729
    stateidsTV = $('#cmbStateTV').val();
    var statelistcountTV = $('#hdnStateListCountTV').val();
    if (statelistcountTV > 0) {
        var GroupBy = $('#hdnGridStatewiseSummaryGroupByTV').val();
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
            obj.stateid = stateids.join(',');// cmbState.GetValue();

            var isObject = typeof branchidsTV
            if (isObject == "object") {
                if (branchidsTV != null && branchidsTV.length > 0) {
                    obj.branchid = branchidsTV.join(',');
                } else {
                    obj.branchid = "";

                }
            } else {
                if (branchidsTV != null && branchidsTV.length > 0) {
                    obj.branchid = branchidsTV;
                }
                else {
                    obj.branchid = "";
                }
            }
            obj.designid = "";
            objsettings.push(obj);
        }

        WindowSize = $(window).width();


        $("#lblAtWorkTV").html("<img src='/assests/images/Spinner.gif' />");
        $("#lblOnLeaveTV").html("<img src='/assests/images/Spinner.gif' />");
        $("#lblNotLoggedInTV").html("<img src='/assests/images/Spinner.gif' />");
        $("#lblTotalTV").html("<img src='/assests/images/Spinner.gif' />");
        stateid = stateidsTV.join(',');// cmbState.GetValue();
        $("#salesmanheader").html("State wise Summary");
        $("#salesmanheaderTeam").html("State wise Summary");
        stateid = stateidsTV.join(',');// cmbState.GetValue();
        // Mantis Issue 24729
        var isObject = typeof branchidsTV
        if (isObject == "object") {
            if (branchidsTV != null && branchidsTV.length > 0) {
                branchid = branchidsTV.join(',');
            } else {
                branchid = "";

            }
        } else {
            if (branchidsTV != null && branchidsTV.length > 0) {
                branchid = branchidsTV;
            }
            else {
                branchid = "";
            }
        }

        objData = {};

        var hdnTotalEmployeesTV = $('#hdnTotalEmployeesTV').val();
        var hdnAtWorkTV = $('#hdnAtWorkTV').val();
        var hdnOnLeaveTV = $('#hdnOnLeaveTV').val();
        var hdnNotLoggedInTV = $('#hdnNotLoggedInTV').val();
        var hdnStatewiseSummaryTV = $('#hdnStatewiseSummaryTV').val();
        var hdnStatewiseSummaryTeamTV = $('#hdnStatewiseSummaryTeamTV').val();

        var obj = {};
        obj.stateid = stateid;
        // Mantis Issue 24729
        obj.branchid = branchid;
        // End of Mantis Issue 24729

        if (hdnTotalEmployeesTV > 0 || hdnAtWorkTV > 0 || hdnOnLeaveTV > 0 || hdnNotLoggedInTV > 0) {
            $.ajax({
                type: "POST",
                url: "/DashboardMenu/GetDashboardDataVisit",
                data: JSON.stringify(obj),
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (hdnAtWorkTV > 0) {
                        $("#EAWTV").html(response.lblAtWork);
                    }
                    if (hdnOnLeaveTV > 0) {
                        $("#EOLTV").html(response.lblOnLeave);
                    }
                    if (hdnNotLoggedInTV > 0) {
                        $("#NLITV").html(response.lblNotLoggedIn);
                    }
                    if (hdnTotalEmployeesTV > 0) {
                        $("#EMPTV").html(response.lblTotal);
                    }
                },
                error: function (response) {
                    jAlert("Please try again later");
                }
            });
            // ajax Team visit
            if ($("#hdnStatewiseSummaryTeamTabTV").val() == 1) {
                TeamVisitData(obj)
            }
        }
        if (hdnStatewiseSummaryTV > 0) {
            //gridsalesman.ClearFilter();
            //gridsalesman.Refresh();
        }
    }
    else {
        $('.bodymain_areastatewise').hide();
    }

});


$(document).ready(function () {

    $('#mapdeatsilsTV').hide();
    $('.closeAcTV').click(function () {
        $('#dashDetails1TV').addClass('hide');
        $('#dashDetails2TV').addClass('hide');
        $('.boxWidgetTV').removeClass('activeW');
    });
    $('.closeAcDetailsTV').click(function () {
        $('#dashDetails2TV').removeClass('hide');
        $('#dashDetails1TV').addClass('hide');
        $('#gridsummaryTV').show('hide');
    });
    $('.closeAcDetails2TV').click(function () {

        $('#dashDetails1TV').removeClass('hide');
        $('#gridthirdlevelTV').addClass('hide');
        $('#gridsummaryTV').show('hide');
    });
    $('#close1TV').click(function () {
        $('#mapdeatsilsTV').hide();
        $('#mapTV').show();
    });
    // AtWorkClick(this);
})





// new script


var stateidsTV = [];
// Mantis Issue 24729
var branchidsTV = [];
   

function GetSateValueTV() {
    stateidsTV = [];

    var datalist = $('#cmbStateTV').val();
    if (datalist != null) {
        stateidsTV = datalist;
    }
    else {
        stateidsTV.push('999999');
    }
    cmbStatechangeTV();
}

// Mantis Issue 24729
function GetBranchValueTV() {
    branchidsTV = [];

    var datalist = $('#cmbBranchTV').val();
    if (datalist != null) {
        branchidsTV = datalist;
    }
    else {
        branchidsTV.push('999999');
    }
    cmbBranchChangeTV();
}
    
function cmbBranchChangeTV() {
    let newBranch = $('#cmbBranchTV').val();

    var isObject = typeof newBranch
    if (isObject == "object") {
        if (newBranch != null && newBranch.length > 0) {
            branchidsTV = newBranch.join(',');
        } else {
            branchidsTV = "";

        }
    } else {
        if (newBranch != null && newBranch.length > 0) {
            branchidsTV = newBranch;
        }
        else {
            branchidsTV = "";
        }
    }
    reloadBoxDataTV(branchidsTV);
}

// End of Mantis Issue 24729
// Susanta

var stateidTV = "";
var designationTV = "";
var statefilteridTV = "";
var EMPCODETV = "";
var WindowSize = 0;


var objsettingsTV = [];
var objTV = {};
objTV.ID = "1";
objTV.action = "AT_WORK";
objTV.rptype = "Summary";
objTV.empid = "";
objTV.stateid = "15";
objTV.designid = "";

objsettingsTV.push(obj);

var objTV = {};
objTV.ID = "2";
objTV.action = "";
objTV.rptype = "";
objTV.empid = "bbb";
objTV.stateid = "bbb";
objTV.designid = "bbb";
objsettingsTV.push(obj);


var objTV = {};
objTV.ID = "3";
objTV.action = "ccc";
objTV.rptype = "ccc";
objTV.empid = "ccc";
objTV.stateid = "ccc";
objTV.designid = "ccc";
objsettingsTV.push(obj);

var settingsidTV = "1";



function salesbackclick() {
    var GroupBy = $('#hdnGridStatewiseSummaryGroupByTV').val();
    settingsid = parseInt(settingsid) - 1;

    if (settingsid == "1") {
        $("#salesmanheader, #salesmanheaderTeam, #salesmanheaderTV").html("State wise Summary");
        $('.salesExport').addClass('mr0');
        //setTimeout(function () { gridsalesman.GroupBy(GroupBy); }, 2000);
    }
    else if (settingsid == "2") {
        $("#salesmanheader, #salesmanheaderTeam, #salesmanheaderTV").html("Employee wise Summary");
        //setTimeout(function () { gridsalesman.GroupBy(GroupBy); }, 2000);
    }
    else if (settingsid == "3") {
        $("#salesmanheader, #salesmanheaderTeam, #salesmanheaderTV").html("Details Of: " + Salesmanname);
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
function cmbStatechangeTV() {
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
        obj.stateid = stateidsTV.join(',');//cmbState.GetValue();
        obj.designid = "";
        // Mantis Issue 24729
        var isObject = typeof branchidsTV
        if (isObject == "object") {
            if (branchidsTV != null && branchidsTV.length > 0) {
                obj.branchid = branchidsTV.join(',');
            } else {
                obj.branchid = "";

            }
        } else {
            if (branchidsTV != null && branchidsTV.length > 0) {
                obj.branchid = branchidsTV;
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

    stateidTV = stateidsTV.join(','); //;cmbState.GetValue();
    // Mantis Issue 24729
    var obj1 = {};
    obj1.stateid = stateidTV;

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

            $("#cmbBranchTV").multiselect('dataprovider', mainData);
            var newBranch = $('#cmbBranchTV').val();

            var isObject = typeof newBranch
            if (isObject == "object") {
                if (newBranch != null && newBranch.length > 0) {
                    branchidsTV = newBranch.join(',');
                } else {
                    branchidsTV = "";

                }
            } else {
                if (newBranch != null && newBranch.length > 0) {
                    branchidsTV = newBranch;
                }
                else {
                    branchidsTV = "";
                }
            }

            reloadBoxDataTV(branchidsTV);
        },
        error: function (response) {
            jAlert("Please try again later");
        }
    });


}

function reloadBoxDataTV(branchidsTV) {
    branchid = branchidsTV;

    objData = {};
    $('#mapdeatsilsTV').hide();
    $('#mapTV').show();

    var obj = {};
    obj.stateid = stateidTV;
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
            $("#lblAtWorkTV").text(response.lblAtWork);
            $("#lblOnLeaveTV").text(response.lblOnLeave);
            $("#lblNotLoggedInTV").text(response.lblNotLoggedIn);
            $("#lblTotalTV").text(response.lblTotal);
            $("#lbltotalshopTV").text(response.TotalVisit);
            $("#lblnewvisitTV").text(response.NewVisit);
            $("#lblrevisitTV").text(response.ReVisit);

            $("#lblavgvisitsTV").text(response.AvgPerDay);
            $("#lblavgdurationTV").text(response.AvgDurationPerShop);
        },
        error: function (response) {
            jAlert("Please try again later");
        }
    });
}

var Type = "";
var FilterName = "";

function AtWorkClickTV(elem) {
    var GroupBy = $('#hdnGridEmployeeAttendanceSummaryGroupByTV').val();
    document.getElementById("griddetailsTV").style.display = "none";
    document.getElementById("gridsummaryTV").style.display = "block";



    $('#dashDetails2TV').removeClass('hide');
    $('.boxWidget').removeClass('activeW');

    //divBGcolor = $(elem).css('background');

    divBGcolor = "transparent";
    $('#a1TV').addClass('activeW');
    $('#collapseTwoTV').addClass('in');
    Type = "Attendance";
    FilterName = "AT_WORK";
    stateid = stateids.join(',');// cmbState.GetValue();

    //$("#gridHeadedtext").text("Employee Attendance Summary - Employees At Work");

    //gridsummarydashboard.Refresh();
    //gridsummarydashboard.Refresh();
    gridsummarydashboardclickTV(null, null);
    $('#backSpnTV').attr("Style", "Display:none;");
    //setTimeout(function () { gridsummarydashboard.GroupBy(GroupBy); }, 1000);
    griddashboardTV.Refresh();
    griddashboardTV.Refresh();
}

function NotLoggedInClickTV(elem) {
    var GroupBy = $('#hdnGridEmployeeAttendanceSummaryGroupByTV').val();
    document.getElementById("griddetailsTV").style.display = "none";
    document.getElementById("gridsummaryTV").style.display = "block";

    //divBGcolor = $(elem).css('background');

    divBGcolor = "transparent";

    $('#dashDetails2TV').removeClass('hide');
    $('.boxWidget').removeClass('activeW');
    $('#a3TV').addClass('activeW');
    $('#collapseTwoTV').addClass('in');
    Type = "Attendance";
    FilterName = "NOT_LOGIN";
    stateid = stateids.join(',');//cmbState.GetValue();

    //$("#gridHeadedtext").text("Employee Attendance Summary - Employees Not Logged In");

    //gridsummarydashboard.Refresh();
    //gridsummarydashboard.Refresh();
    gridsummarydashboardclickTV(null, null);
    $('#backSpnTV').attr("Style", "Display:none;");
    //setTimeout(function () { gridsummarydashboard.GroupBy(GroupBy); }, 1000);
    griddashboardTV.Refresh();
    griddashboardTV.Refresh();
}

function OnLeaveClickTV(elem) {
    var GroupBy = $('#hdnGridEmployeeAttendanceSummaryGroupByTV').val();
    document.getElementById("griddetailsTV").style.display = "none";
    document.getElementById("gridsummaryTV").style.display = "block";

    // divBGcolor = $(elem).css('background');
    divBGcolor = "transparent";


    $('#dashDetails2TV').removeClass('hide');
    $('.boxWidget').removeClass('activeW');
    $('#a2TV').addClass('activeW');
    $('#collapseTwoTV').addClass('in');
    Type = "Attendance";
    FilterName = "ON_LEAVE";
    stateid = stateids.join(',');// cmbState.GetValue();

    //$("#gridHeadedtext").text("Employee Attendance Summary - Employees On Leave");
    //gridsummarydashboard.Refresh();
    //gridsummarydashboard.Refresh();
    gridsummarydashboardclickTV(null, null);
    $('#backSpnTV').attr("Style", "Display:none;");
    //setTimeout(function () { gridsummarydashboard.GroupBy(GroupBy); }, 1000);
    griddashboardTV.Refresh();
    setTimeout(function () {
        griddashboardTV.Refresh();
    }, 100)
}

function OnTotalClickTV(elem) {
    var GroupBy = $('#hdnGridEmployeeAttendanceSummaryGroupByTV').val();
    document.getElementById("griddetailsTV").style.display = "none";
    document.getElementById("gridsummaryTV").style.display = "block";

    //divBGcolor = $(elem).css('background');
    divBGcolor = "transparent";

    $('#dashDetails2TV').removeClass('hide');
    $('.boxWidget').removeClass('activeW');
    $('#a4TV').addClass('activeW');
    $('#collapseTwoTV').addClass('in');
    Type = "Attendance";
    FilterName = "EMP";
    stateid = stateids.join(',');// cmbState.GetValue();

    //$("#gridHeadedtext").text("Employee Attendance Summary - Total Employees");
    //gridsummarydashboard.Refresh();
    //gridsummarydashboard.Refresh();
    gridsummarydashboardclickTV(null, null);
    //setTimeout(function () { gridsummarydashboard.GroupBy(GroupBy); }, 1000);
    $('#backSpnTV').attr("Style", "Display:none;");
}



function shopclickTeam(id, EMPNAME) {
    Salesman_EMPCODE = id;
    $("#gridshoptextTV").text("Todays visit of " + EMPNAME);
    $('#dashDetails1TV').addClass('hide');
    $('#gridthirdlevelTV').removeClass('hide');
    griddashboarddetailsTV.Refresh();
    // griddashboarddetails.Refresh();


}


function showMapTV() {

    stateid = stateids.join(',');// cmbState.GetValue();
    GetAddressTV(stateid);
    $('#mapTV').removeClass('hide');
}
function mapcloseclickTV() {
    $("#mapTV").addClass("hide");
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

function gridsummarydashboardclickTV(desig, state) {

    if (Type == "Tracking") {
        return;
    }

    document.getElementById("griddetailsTV").style.display = "block";
    document.getElementById("gridsummaryTV").style.display = "none";
    designation = desig;
    statefilterid = state;

    $('#dashDetails1TV').removeClass('hide');
    $('#dashDetails2TV').addClass('hide');
    //$('.boxWidget').removeClass('activeW');


    $('#collapseTwoTV').addClass('in');

    var summaryof = "";
    if (FilterName == "AT_WORK")
        summaryof = "Employees At Work";
    else if (FilterName == "ON_LEAVE")
        summaryof = "Employees On Leave";
    else if (FilterName == "NOT_LOGIN")
        summaryof = "Employees Not Logged In";
    else if (FilterName == "EMP")
        summaryof = "Total Employees";


    $("#griddetailsHeadedtextTV").text("Employee Attendance details - " + summaryof + "");
    griddashboardTV.Refresh();
    griddashboardTV.Refresh();


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






