<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                02-02-2023        2.0.38           Pallab              breadcumb issue fix
2.0                23-02-2023        2.0.39           Pallab              25656 : Master module design modification
====================================================== Revision History ==========================================================--%>

<%@ Page Title="Employee" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master"
    Inherits="ERP.OMS.Management.Master.management_master_HRCostEmployee" CodeBehind="HRCostEmployee.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">




    <script language="javascript" type="text/javascript">
        function disp_prompt(name) {
            if (name == "tab0") {
                //alert(name);
                document.location.href = "HrCost.aspx";
            }
            if (name == "tab1") {
                //alert(name);
                document.location.href = "HRCostBranch.aspx";
            }
            else if (name == "tab2") {
                //alert(name);
                //document.location.href="HRCostEmployee.aspx"; 
            }
        }
    </script>

    <style>
        /*Rev 2.0*/

        body , .dxtcLite_PlasticBlue
        {
            font-family: 'Poppins', sans-serif !important;
        }

    #BranchGridLookup {
        min-height: 34px;
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
            top: 10px;
            right: 4px;
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

        select:not(.btn):focus
        {
            border-color: #094e8c;
        }

        select:not(.btn):focus-visible
        {
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

    .pmsModal .modal-header {
        background: #094e8c !important;
        background-image: none !important;
        padding: 11px 20px;
        border: none;
        border-radius: 5px 5px 0 0;
        color: #fff;
        border-radius: 10px 10px 0 0;
    }

    .pmsModal .modal-content {
        border: none;
        border-radius: 10px;
    }

    .pmsModal .modal-header .modal-title {
        font-size: 14px;
    }

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

    #dtFrom_B-1, #dtTo_B-1 , #txtCINVdate_B-1 {
        background: transparent !important;
        border: none;
        width: 30px;
        padding: 10px !important;
    }

        #dtFrom_B-1 #dtFrom_B-1Img,
        #dtTo_B-1 #dtTo_B-1Img,
        #txtCINVdate_B-1 #txtCINVdate_B-1Img{
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

    .dxeTextBox_PlasticBlue
    {
            height: 32px;
            border-radius: 4px;
    }

    #Label1 , #Label2 , #Label3 , #Label4 , #Label5
    {
         font-size: 14px;
    }

    /*Rev end 2.0*/
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--rev 1.0--%>
    <%--<div class="panel-heading">
        <div class="panel-title">
            <h3>Cost Centers/Departments Information</h3>
            <div class="crossBtn"><a href="HRCostCenter.aspx"><i class="fa fa-times"></i></a></div>
        </div>

    </div>--%>
    <div class="breadCumb">
        <span>Cost Centers/Departments Information</span>
        <div class="crossBtnN"><a href="HRCostCenter.aspx"><i class="fa fa-times"></i></a></div>
    </div>
    <%--rev end 1.0--%>
    <%--rev 1.0 : container and white bg add--%>
    <div class="container">
        <div class="backBox mt-5 p-3 ">
            <%--rev end 1.0--%>
    <div class="form_main">
        <table class="TableMain100">
            <tr>
                <td>
                    <dxe:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="2" Width="100%"
                        ClientInstanceName="page" Font-Size="12px">
                        <TabPages>
                            <dxe:TabPage Text="Update" Name="Update">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                            <dxe:TabPage Text="Branch" Name="Branch">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                            <dxe:TabPage Name="Employee" Text="Employee">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                        <dxe:ASPxGridView ID="EmployeeGrid" runat="server" AutoGenerateColumns="False"
                                            DataSourceID="EmployeeSource" Width="100%">
                                            <Columns>
                                                <dxe:GridViewDataTextColumn FieldName="id" Visible="False" VisibleIndex="0">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="Costcenter" VisibleIndex="0" Width="50%">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="CostCenterType" VisibleIndex="1" Width="50%">
                                                </dxe:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsSearchPanel Visible="True" />
                                            <Settings ShowFilterRow="true" ShowFilterRowMenu ="true" />
                                            <Styles>
                                                <LoadingPanel ImageSpacing="10px">
                                                </LoadingPanel>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>

                                        </dxe:ASPxGridView>
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                        </TabPages>
                        <ClientSideEvents ActiveTabChanged="function(s, e) {
	                                            var activeTab   = page.GetActiveTab();
	                                            var Tab0 = page.GetTab(0);
	                                            var Tab1 = page.GetTab(1);
	                                            var Tab2 = page.GetTab(2);	                                            
	                                            if(activeTab == Tab0)
	                                            {
	                                                disp_prompt('tab0');
	                                            }
	                                            if(activeTab == Tab1)
	                                            {
	                                                disp_prompt('tab1');
	                                            }
	                                            else if(activeTab == Tab2)
	                                            {
	                                                disp_prompt('tab2');
	                                            }
	                                            
	                                            }"></ClientSideEvents>
                        <ContentStyle>
                            <Border BorderColor="#002D96" BorderStyle="Solid" BorderWidth="1px" />
                        </ContentStyle>
                        <LoadingPanelStyle ImageSpacing="6px">
                        </LoadingPanelStyle>
                    </dxe:ASPxPageControl>
                </td>
            </tr>
            <tr>
                <td style="height: 8px">
                    <table style="width: 100%;">
                        <tr>
                            <td align="right" style="width: 843px"></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 843px"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="EmployeeSource" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="select cnt_internalId as id,cnt_firstName  as Costcenter,cost_costCenterType as CostCenterType from tbl_master_contact ,tbl_master_costCenter where cnt_internalId=cost_description"></asp:SqlDataSource>
    </div>
            <%--rev 1.0--%>
    </div>
</div>
    <%--rev end 1.0--%>
</asp:Content>
