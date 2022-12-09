<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master" Inherits="ERP.OMS.Management.management_ProjectMainPage" CodeBehind="ProjectMainPage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .widgetBox {
        position: relative;
        margin-top: 20px;
    }

    .numCnt {
        padding: 15px 25px;
    }

    .widgetBox .numCnt .number {
        font-size: 26px;
        font-weight: 700;
        color: #fff;
    }

    .widgetBox .numCnt .text {
        font-size: 16px;
        color: #fff;
    }

    .widgetBox h4 {
        padding: 10px 25px;
        background: #398b9e;
        color: #fff;
    }

    .widgetBox.one {
        background: #4bacc3;
    }

    .widgetBox.two {
        background: #4bc3ae;
    }

    .widgetBox.three {
        background: #b6c72f;
    }

    .widgetBox.four {
        background: #e47b5b;
    }

    .widgetBox.two h4 {
        background: #36a995;
    }

    .widgetBox.three h4 {
        background: #9ead26;
    }

    .widgetBox.four h4 {
        background: #ce6646;
    }

    .widgetBox .fa {
        position: absolute;
        font-size: 43px;
        color: rgba(53, 50, 50, 0.5);
        top: 18px;
        right: 18px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div>
    <div class=" col-sm-3">
        <div class="widgetBox one">
            <div class="numCnt">
                <div class="number"><label id="lbltotaluser" runat="server"></label> </div>
                <div class="text"> Total Users</div>
            </div>
            <i class="fa fa-user" aria-hidden="true"></i>
            <h4></h4>
        </div>
    </div>
    <div class=" col-sm-3">
        <div class="widgetBox two">
            <div class="numCnt">
                <div class="number"><label id="lbltotshop" runat="server"></label> </div>
                <div class="text">Total Shop </div>
            </div>
            <i class="fa fa-stop-circle" aria-hidden="true"></i>
            <h4></h4>
        </div>
    </div>
    <div class=" col-sm-3">
        <div class="widgetBox three">
            <div class="numCnt">
                <div class="number"><label id="lblactive" runat="server"></label> </div>
                <div class="text"> Total Active User </div>
            </div>
            <i class="fa fa-sign-in" aria-hidden="true"></i>
            <h4></h4>
        </div>
    </div>
    <div class=" col-sm-3">
        <div class="widgetBox four">
            <div class="numCnt">
                <div class="number"><label id="lblinactive" runat="server"></label> </div>
                <div class="text">Total Inactive User  </div>
            </div>
            <i class="fa fa-sign-out" aria-hidden="true"></i>
            <h4></h4>
        </div>
    </div>
</div>
</asp:Content>
