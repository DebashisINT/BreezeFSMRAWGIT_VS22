<%--------------------------------------------------Revision History ------------------------------------------------------------%>
<%--1.0     V2.0.40     Pallab      25/04/2023     25913: Salesman/Agents - Industry Map (NEW ADMIN ) design modification and issue fix--%>
<%-----------------------------------------------End of Revision History----------------------------------------------------------%>

<%@ Page Title="Industry Map" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" CodeBehind="AssignIndustry.aspx.cs" Inherits="ERP.OMS.Management.Master.AssignIndustry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        <%--        $(document).ready(function () {
            alert('sss');
            
                var x = document.getElementById("<%=lbAvailable.ClientID %>");
                for (var i = 0; i < x.options.length; i++) {
                    if (x.options[i].selected == true) {
                        alert(x.options[i].selected);
                    }
                }
           
        });--%>
    </script>



    <script type="text/javascript">

        function GetChar(event) {
            var chCode = ('charCode' in event) ? event.charCode : event.keyCode;

            if (event.keyCode) {
                __doPostBack('<%=btnCancel.UniqueID%>', "");
            }
        }
        function ShowAvailable(name) {

            // alert('aaa')
            var a = $('#txtAvailable').val()

            if (a.length > 3) {
                __doPostBack("txtAvailable", "TextChanged");
            }
        }

        function AddSelectedItems() {
            MoveSelectedItems(lbAvailable, lbChoosen);
            UpdateButtonState();
        }
        function AddAllItems() {
            MoveAllItems(lbAvailable, lbChoosen);
            UpdateButtonState();
        }
        function RemoveSelectedItems() {
            MoveSelectedItems(lbChoosen, lbAvailable);
            UpdateButtonState();
        }
        function RemoveAllItems() {
            MoveAllItems(lbChoosen, lbAvailable);
            UpdateButtonState();
        }
        function MoveSelectedItems(srcListBox, dstListBox) {
            srcListBox.BeginUpdate();
            dstListBox.BeginUpdate();
            var items = srcListBox.GetSelectedItems();
            for (var i = items.length - 1; i >= 0; i = i - 1) {
                dstListBox.AddItem(items[i].text, items[i].value);
                srcListBox.RemoveItem(items[i].index);
            }
            srcListBox.EndUpdate();
            dstListBox.EndUpdate();
        }
        function MoveAllItems(srcListBox, dstListBox) {
            srcListBox.BeginUpdate();
            var count = srcListBox.GetItemCount();
            for (var i = 0; i < count; i++) {
                var item = srcListBox.GetItem(i);
                dstListBox.AddItem(item.text, item.value);
            }
            srcListBox.EndUpdate();
            srcListBox.ClearItems();
        }
        function UpdateButtonState() {

            //btnMoveAllItemsToRight.SetEnabled(lbAvailable.GetItemCount() > 0);
            //btnMoveAllItemsToLeft.SetEnabled(lbChoosen.GetItemCount() > 0);
            //btnMoveSelectedItemsToRight.SetEnabled(lbAvailable.GetSelectedItems().length > 0);
            //btnMoveSelectedItemsToLeft.SetEnabled(lbChoosen.GetSelectedItems().length > 0);
        }

        function CheckBoxListEdu_Init(s, e) {
            document.getElementById('divqualilist').innerHTML = '';
            //alert(document.getElementById('divqualilist').innerHTML);
            var s9 = '';
            var s1 = s.GetSelectedItems();
            var s2 = s1.length;
            var s3 = "";
            if (s2 != 0) {
                for (var i = 0; i < s2; i++) {
                    s9 = s1[i].text;
                    s3 += s9.replace(/-/g, ' ') + ", ";
                }
            }

            if (s3 != null || s3 != ', ') {
                s3 = s3.slice(0, s3.lastIndexOf(", "));
                document.getElementById('divqualilist').innerHTML = s3;
            }

        }
    </script>
    <style>
        #lbAvailable {
            min-width:352px;
        }

        /*Rev 1.0*/

        body, .dxtcLite_PlasticBlue {
            font-family: 'Poppins', sans-serif !important;
        }

        #txtAvailable  {
            min-height: 34px;
            border-radius: 5px;
            background-color: #fff;
    border: 1px solid #ccc;
        }

        .dxeListBox_PlasticBlue
        {
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
        bottom: 8px;
        right: 20px;
        z-index: 0;
        cursor: pointer;
    }

    .date-select .form-control {
        position: relative;
        z-index: 1;
        background: transparent;
    }

    #ddlState, #ddlPartyType, #divoutletStatus, #slmonth, #slyear , #txtCINVdate {
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
            top: 37px;
            right: 12px;
            font-size: 18px;
            transform: rotate(269deg);
            font-weight: 500;

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
            bottom: 8px;
            right: 20px;
            z-index: 0;
            cursor: pointer;
        }

        .date-select .form-control {
            position: relative;
            z-index: 1;
            background: transparent;
        }

        #ddlState, #ddlPartyType, #divoutletStatus, #slmonth, #slyear, #txtCINVdate {
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
                top: 37px;
                right: 12px;
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
                line-height: 18px;
                z-index: 0;
            }

        select:not(.btn):focus {
            border-color: #094e8c;
        }

        select:not(.btn):focus-visible {
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
        .dxeEditArea_PlasticBlue {
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

        /*.pmsModal .modal-header {
            background: #094e8c !important;
            background-image: none !important;
            padding: 11px 20px;
            border: none;
            border-radius: 5px 5px 0 0;
            color: #fff;
            border-radius: 10px 10px 0 0;
        }*/

       /* .pmsModal .modal-content {
            border: none;
            border-radius: 10px;
        }

        .pmsModal .modal-header .modal-title {
            font-size: 14px;
        }
*/
        .pmsModal .close {
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

        #dtFrom_B-1, #dtTo_B-1, #txtCINVdate_B-1 {
            background: transparent !important;
            border: none;
            width: 30px;
            padding: 10px !important;
        }

            #dtFrom_B-1 #dtFrom_B-1Img,
            #dtTo_B-1 #dtTo_B-1Img,
            #txtCINVdate_B-1 #txtCINVdate_B-1Img {
                display: none;
            }

        #dtFrom_I, #dtTo_I {
            background: transparent;
        }

        .for-cust-icon {
            position: relative;
            z-index: 1;
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

        .dxeTextBox_PlasticBlue {
            height: 34px;
            border-radius: 4px;
        }

        .container {
            width: 88% !important;
        }

        /*Rev end 1.0*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadCumb">
       
            <span>
                <asp:Label ID="lblEntityType" runat="server"></asp:Label>
                - Industry Map
                <asp:Label ID="lblEntityUserName" runat="server"></asp:Label></span>

            <div class="crossBtnN">
                <asp:LinkButton ID="goBackCrossBtn" runat="server" OnClick="goBackCrossBtn_Click"><i class="fa fa-times"></i></asp:LinkButton>
                <%--<a href="frmContactMain.aspx" id="goBackCrossBtn"><i class="fa fa-times"></i></a>--%>
                <asp:HiddenField ID="hidbackPagerequesttype" runat="server" />
            </div>

       
    </div>
    <div class="container">
        <div class="backBox mt-5 p-3 ">
            <table class="TableMain100">
            <tr>
                <td style="width: 4%">
                   <%-- <span style="font-size: 14px; display: inline-block; margin-bottom: 8px">Industry Name</span>--%>
                    <div id="divqualilist" runat="server" clientidmode="Static" style="width: 353px; font-size: xx-small; font-family: Arial; color: #0000FF; font-weight: bold;">
                       
                    </div>
                  
                     <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <div class="backBox p-4">
                        <asp:TextBox ID="txtAvailable" runat="server" AutoPostBack="true" autocomplete="off" OnTextChanged="txtAvailable_TextChanged" onkeyup="ShowAvailable(this)" Width="353px"  se placeholder="Type Industry name to search"></asp:TextBox>
                          <span  style="font-weight:600;color:brown"> Industry (Min. 4 Char)</span>  
                        <div class="mt-4"></div>
                        <dxe:ASPxListBox ID="lbAvailable" runat="server" ClientInstanceName="lbAvailable"
                            Height="240px" SelectionMode="CheckColumn" Caption="" >
                           <%-- <ClientSideEvents SelectedIndexChanged="CheckBoxListEdu_Init" />--%>
                        </dxe:ASPxListBox>
                    </div>
                </td>

                <td style="padding: 100px 0px; display: none" align="center" width="10%">
                    <div>
                        <dxe:ASPxButton ID="btnMoveSelectedItemsToRight" runat="server" ClientInstanceName="btnMoveSelectedItemsToRight"
                            AutoPostBack="False" Text="Add >" CssClass="btn btn-primary btn-xs" Width="150px" ClientEnabled="False"
                            ToolTip="Add selected items">
                            <ClientSideEvents Click="function(s, e) { AddSelectedItems(); }" />
                        </dxe:ASPxButton>
                    </div>
                    <div class="TopPadding">
                        <dxe:ASPxButton ID="btnMoveAllItemsToRight" runat="server" ClientInstanceName="btnMoveAllItemsToRight"
                            AutoPostBack="False" Text="Add All >>" CssClass="btn btn-primary btn-xs" Width="150px" ToolTip="Add all items">
                            <ClientSideEvents Click="function(s, e) { AddAllItems(); }" />
                        </dxe:ASPxButton>
                    </div>
                    <div style="height: 32px">
                    </div>
                    <div>
                        <dxe:ASPxButton ID="btnMoveSelectedItemsToLeft" runat="server" ClientInstanceName="btnMoveSelectedItemsToLeft"
                            AutoPostBack="False" Text="< Remove" CssClass="btn btn-danger btn-xs" Width="150px" ClientEnabled="False"
                            ToolTip="Remove selected items">
                            <ClientSideEvents Click="function(s, e) { RemoveSelectedItems(); }" />
                        </dxe:ASPxButton>
                    </div>
                    <div class="TopPadding">
                        <dxe:ASPxButton ID="btnMoveAllItemsToLeft" runat="server" ClientInstanceName="btnMoveAllItemsToLeft"
                            AutoPostBack="False" Text="<< Remove All" CssClass="btn btn-danger btn-xs" Width="150px" ClientEnabled="False"
                            ToolTip="Remove all items">
                            <ClientSideEvents Click="function(s, e) { RemoveAllItems(); }" />
                        </dxe:ASPxButton>
                    </div>
                </td>
                <td style="width: 35%; display: none">
                    <dxe:ASPxListBox ID="lbChoosen" runat="server" ClientInstanceName="lbChoosen" Width="350px"
                        Height="240px" SelectionMode="CheckColumn" Caption="Selected Industry">
                        <CaptionSettings Position="Top" />
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }"></ClientSideEvents>
                    </dxe:ASPxListBox>
                </td>
            </tr>
            <tr>
                <td class="pt-4">
                    <asp:Button ID="btnsubmit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnsubmit_click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="goBackCrossBtn_Click" />






                </td>
            </tr>


        </table>
        </div>
    </div>
</asp:Content>
