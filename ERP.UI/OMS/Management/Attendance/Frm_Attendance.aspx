<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frm_Attendance.aspx.cs" MasterPageFile="~/OMS/MasterPage/ERP.Master"
    Inherits="ERP.OMS.Management.Attendance.Frm_Attendance" EnableEventValidation="false" EnableViewStateMac="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Css/Attendance.css" rel="stylesheet" />
    <link href="../../../Scripts/pluggins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../Scripts/pluggins/multiselect/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Js/Attendance.js?v=2.01"></script>
    <script src="../Activities/JS/SearchPopup.js"></script>
    <link href="../Activities/CSS/SearchPopup.css?v=0.02" rel="stylesheet" />
    <div class="breadCumb">
        <span>
            <label id="lblheading">Attendance</label>
        </span>
    </div>
    <div class="container clearfix backBox mt-5 mb-4 py-3">
        <div class="timeWraper">
            <div class="timeBox">
                <div class="line"></div>
                <div class="dateMnth" id="Day_span"></div>
                <div id="time_span"></div>

                <div class="btnWrap">
                    <button type="button" class="btn btn-success" id="BtnShowEmployee" onclick="EmployeeSelect()"><i class="fa fa-search"></i>Select Employee</button>
                    <button type="button" class="btn btn-primary" id="BtnSubmitRequest" onclick="AttendanceSubmit()"><i class="fa fa-clock-o"></i>In Time</button>
                    <button type="button" class="btn btn-primary" id="BtnExitRequest" onclick="LogOutSubmit();"><i class="fa fa-clock-o"></i>Out Time</button>
                </div>

                <div class="text-center">
                    <label class="label label-default employeeNameClass"><i class="fa fa-user"></i><span id="EmployeeNameSpan"></span></label>
                </div>

            </div>
        </div>
        <div class="text-center p-4">
            <span style="color: red; font-size: 13px;">* Date/Time of the Local PC. Attendance Time In/Out will be according to the Server Date/Time</span>
        </div>
    </div>



    <asp:HiddenField runat="server" ID="EmpId" />
    <asp:HiddenField ID="hdEmpName" runat="server" />

    <asp:HiddenField ID="hdnisGivenAttendance" runat="server" />
    <asp:HiddenField ID="hdnIsLeaveonApprovval" runat="server" />
    <asp:HiddenField runat="server" ID="hdnUserID" />


    <div class="modal fade pmsModal w40" id="inOutModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">In Out Entry</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                            <label>Mark your attendance</label>
                            <div>
                                <select class="form-control" id="markWork" onchange="markWork_change();">
                                    <option value="0">Select</option>
                                    <option value="1">At Work</option>
                                    <option value="2">On Leave</option>
                                </select>
                            </div>
                        </div>

                    </div>
                    <div class="row" id="workTyp">
                        <div class="col-md-6 mt-3">
                            <label title="Shop Type">Select Work Type :</label>
                            <div class="fullMulti">
                                <%-- <asp:DropDownList ID="ddlWorkType" runat="server" class="demo" multiple="multiple" Width="100%">
                            <asp:ListItem Value="0">Home Restriction</asp:ListItem>
                            <asp:ListItem Value="1">No Restriction</asp:ListItem>
                            <asp:ListItem Value="2">Fix Location</asp:ListItem>
                        </asp:DropDownList>--%>
                                <div id="Workdiv">
                                    <select id="ddlWorkType" runat="server" class="demo" multiple="true" width="100%">
                                        <option>Select</option>
                                    </select>
                                </div>

                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="col-md-6 mt-3 hide">
                            <label>Distributor Name :</label>
                            <div class="">
                                <input type="text" class="form-control" />
                            </div>
                        </div>
                        <div class="col-md-12 mt-3">
                            <label>Remarks :</label>
                            <div class="">
                                <input type="text" class="form-control" id="txtWorkRemarks" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="leaveTyp" style="display: none">
                        <div class="col-md-6 mt-3">
                            <label title="Shop Type">Select Leave Type :</label>
                            <div class="fullMulti">
                                <%-- <asp:DropDownList ID="ddlLeaveType" runat="server" class="demo" Width="100%">
                            <asp:ListItem Value="0">Home Restriction</asp:ListItem>
                            <asp:ListItem Value="1">No Restriction</asp:ListItem>
                            <asp:ListItem Value="2">Fix Location</asp:ListItem>
                        </asp:DropDownList>--%>
                                <div id="Leavediv">
                                    <select id="ddlLeaveType" class="demo" width="100%">
                                        <option>Select</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="col-md-3 mt-3">
                            <label>Leave From :</label>
                            <div style="position: relative">
                                   <dxe:ASPxDateEdit Width="100%" ID="cmbDOJ" runat="server">
                                        </dxe:ASPxDateEdit>
                            </div>
                        </div>
                        <div class="col-md-3 mt-3">
                            <label>Leave To :</label>
                            <div style="position: relative">
                                  <dxe:ASPxDateEdit ID="cmbLeaveEff" runat="server" DateOnError="Today" EditFormat="Custom"
                                            Width="100%">
                                        </dxe:ASPxDateEdit>
                            </div>
                        </div>
                        <div class="clear"></div>
                        <div class="col-md-12 mt-3">
                            <label>Write your Reason :</label>
                            <div class="">
                                <input type="text" class="form-control" id="txtLeaveReason" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" onclick="Attendance();">Submit</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>





    <!--Employee Modal -->
    <div class="modal fade" id="EmployeeModel" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Employee Search</h4>
                </div>
                <div class="modal-body">
                    <input type="text" class="form-control mb-3" onkeydown="Employeekeydown(event)" id="txtEmpSearch" autofocus width="100%" placeholder="Search By Employee Name or Unique Id" />

                    <div id="EmployeeTable">
                        <table border='1' width="100%" class="dynamicPopupTbl">
                            <tr class="HeaderStyle">
                                <th class="hide">id</th>
                                <th>Employee Name</th>
                                <th>Employee Id</th>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>


</asp:Content>
