<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="leaveapproval.aspx.cs" Inherits="ERP.OMS.Management.Activities.leaveapproval" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Leave Approval</title>
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500&display=swap" rel="stylesheet" />
    
    <style>
        body {
            font-family: 'Poppins', sans-serif;
        }
        .hide
        {
            display:none;
        }
        .leaveBox {
            width: 60%;
            margin: 50px auto;
            border: 1px solid #ccc;
            border-radius: 10px;
            overflow: hidden;
        }
        .leave-header {
            background: #6b59b1;
            color: #fff;
            padding: 20px;
            font-size: 1.4rem;
        }
        .leave-content {
                padding: 15px;
        }
        .leaveinfo-table {
            width:100%;
        }
        .leaveinfo-table>tbody>tr>td {
            padding:10px 0;
            font-size:14px;
        }
        .leaveinfo-table>tbody>tr>td strong {
            font-weight:500;
        }
        .font-big {
            font-size:16px !important;
        }
        .infoTable{
            width:100%;
            font-size:13px;
            margin-bottom:10px;
        }
        .infoTable>thead>tr>th {
            background:#ccc;
            padding: 12px 8px;
            text-align:left;
        }
        .infoTable>tbody>tr>td{
            padding:8px;
        }
        .btn {
            border:none;
            padding:8px 10px;
        }
        .btn-success {
            background:green;
            color:#fff;
        }
        .btn-danger {
            background:red;
            color:#fff;
        }
        .btn:hover {
            opacity:0.8;
        }
        @media screen and (max-width: 768px) {
          .leaveBox {
              width:98%
          }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="text-center">
        <div class="leaveBox">
            <div class="leave-header">Leave application</div>
            <div class="leave-content">
                <table class="leaveinfo-table">
                    <tr>
                        <td class="font-big"><strong>Name</strong></td>
                        <td><strong> : </strong></td>
                        <td class="font-big"><strong id="txtName" runat="server"><%--Susanta Kundu--%></strong></td>
                    </tr>
                    <tr class="hide">
                        <td>Email</td>
                        <td><strong> : </strong></td>
                        <td><%--<a href="mailto:susanta.kundu@indusnet.co.in" target="_blank">susanta.kundu@indusnet.co.in</a>--%></td>
                    </tr>
                    <tr>
                        <td>Contact number</td>
                        <td><strong> : </strong></td>
                        <td><strong id="txtMobile" runat="server"><%--Susanta Kundu--%></strong></td>
                    </tr>
                    <tr >
                        <td>Supervisor</td>
                        <td><strong> : </strong></td>
                        <td><strong  id="txtSupervisor" runat="server"><%--Susanta Kundu--%></strong></td>
                    </tr>
                    <tr>
                        <td>Leave from</td>
                        <td><strong> : </strong></td>
                        <td><strong id="txtFrom" runat="server"><%--Susanta Kundu--%></strong></td>
                    </tr>
                    <tr>
                        <td>Leave to</td>
                        <td><strong> : </strong></td>
                        <td><strong id="txtTo" runat="server"><%--Susanta Kundu--%></strong></td>
                    </tr>
                    <tr>
                        <td>Leave type</td>
                        <td><strong> : </strong></td>
                        <td><strong id="txtType" runat="server"><%--Susanta Kundu--%></strong></td>
                    </tr>
                    <tr>
                        <td>Reason for leave</td>
                        <td><strong> : </strong></td>
                        <td><strong id="txtReason" runat="server"><%--Susanta Kundu--%></strong></td>
                    </tr>
                    <tr>
                        <td>Application status</td>
                        <td><strong> : </strong></td>
                        <td><strong id="txtStatus" runat="server"><%--Susanta Kundu--%></strong></td>
                    </tr>
                    <tr >
                        <td colspan="3">
                            <div id="dvButtons" runat="server">
                            <asp:Button runat="server" class="btn btn-success" Text="Approve" ID="btApprove" OnClick="btApprove_Click"></asp:Button>
                            <asp:Button runat="server" class="btn btn-danger" Text="Reject" ID="btnReject" OnClick="btnReject_Click"></asp:Button>
                            
                            </div>
                            <div>
                                <asp:Button runat="server" class="btn btn-warning hide" Text="View Status" ID="Button1" OnClick="Button1_Click"></asp:Button>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <table class="infoTable hide" cellspacing="0" cellpadding="0">
                    <thead>
                        <tr>
                            <th>Leave type</th>
                            <th>From</th>
                            <th>To</th>
                            <th>Leave Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Sick Leave</td>
                            <td>31-12-2019</td>
                            <td>31-12-2019</td>
                            <td><div class="tagged approved">Approved</div></td>
                        </tr>
                        <tr>
                            <td>Sick Leave</td>
                            <td>31-12-2019</td>
                            <td>31-12-2019</td>
                            <td><div class="tagged pending">Approved</div></td>
                        </tr>
                        <tr>
                            <td>Sick Leave</td>
                            <td>31-12-2019</td>
                            <td>31-12-2019</td>
                            <td><div class="tagged rejected">Approved</div></td>
                        </tr>
                    </tbody>
                </table>


        </div>
    </div>
    </form>
</body>
</html>
