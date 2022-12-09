<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveSuccessPage.aspx.cs" Inherits="ERP.OMS.Management.Activities.LeaveSuccessPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Leave Success</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style>
        .leaveBox {
            position:absolute;
            min-width:40%;
            border-radius: 8px;
            max-width: 450px;
            padding: 50px;
            text-align: center;
            box-shadow: 0px 0px 17px rgba(0,0,0,0.14);
            text-transform: initial;
            line-height: 1.8rem;
            top: 50%;
            transform: translate(-50%, -50%);
            left: 50%;
            border-bottom: 4px solid #6172e9;
        }
        .close{
            margin-top:15px;
            background:red;
            color:#fff;
            border:none;
            border-radius:4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="leaveBox">
        <div id="dvSuccessText" runat="server"></div>
    </div>
        <%--<button class="close" onclick="window.close();">Close</button>--%>
    </form>
</body>
</html>
