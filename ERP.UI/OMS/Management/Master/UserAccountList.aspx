<%@ Page Title="Users" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master" Inherits="ERP.OMS.Management.Master.management_master_UserAccountList" CodeBehind="UserAccountList.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #divDashboardHeaderList .panel:last-child {
            margin-bottom: 0;
        }
        ul.inline-list {
            padding-left: 0;
            margin-bottom: 0;
        }
        .inline-list > li {
            display: inline-block;
            list-style-type: none;
            margin-right: 20px;
            margin-bottom: 8px;
        }
        .inline-list > li > input {
                -webkit-transform: translateY(3px);
                -moz-transform: translateY(3px);
                transform: translateY(3px);
                margin-right: 4px;
            }

        .panel-title > a {
            font-size: 13px !important;
            display: inline-block;
            padding-left: 10px;
        }

        #list2 option,
        #list1 option {
            padding: 5px 3px;
        }

        .padTbl > tbody > tr > td {
            padding-right: 20px;
            vertical-align: central;
        }

            .padTbl > tbody > tr > td > label {
                margin-bottom: 0px !important;
            }

        #divDashboardHeaderList .panel-title {
            position: relative;
        }

            #divDashboardHeaderList .panel-title > a:focus {
                text-decoration: none;
            }

            #divDashboardHeaderList .panel-title > a:after {
                content: '\f056';
                font-family: FontAwesome;
                font-size: 18px;
                position: absolute;
                right: 10px;
                top: 6px;
            }

            #divDashboardHeaderList .panel-title > a + input[type="checkbox"] {
                -webkit-transform: translateY(2px);
                -moz-transform: translateY(2px);
                transform: translateY(2px);
            }

            #divDashboardHeaderList .panel-title > a.collapsed:after {
                content: '\f055';
            }

        .errorField {
            position: absolute;
            right: -17px;
            top: 3px;
        }

        #multiselect_to option, #multiselect option {
            padding: 5px 3px;
        }

        .min3 {
            min-height: 150px;
        }

        .pad28 {
            padding-top: 26px;
        }
    </style>

    <style>
        .transfer-demo {
            width: 640px;
            height: 351px;
        }

            .transfer-demo .transfer-double-header {
                display: none;
            }

        .transfer-double-selected-list-main .transfer-double-selected-list-ul .transfer-double-selected-list-li .checkbox-group {
            width: 85%;
        }

        .red {
            color: red;
        }

        input:focus, textarea:focus, select:focus {
            outline: none;
        }

        .transfer-double-content-param {
            border-bottom: 1px solid #4236f5;
            background: #4236f5;
            color: #e8e8e8;
        }
    </style>
    <link href="/assests/pluggins/Transfer/icon_font/css/icon_font.css" rel="stylesheet" />
    
    <link href="/assests/css/custom/PMSStyles.css" rel="stylesheet" />
    <script src="/Scripts/SearchPopup.js"></script>
    <script src="/assests/pluggins/choosen/choosen.min.js"></script>
    <link href="/assests/css/custom/SearchPopup.css" rel="stylesheet" />    
    <script src="/Scripts/SearchMultiPopup.js?v1.0"></script>  
    <link href="/assests/pluggins/Transfer/css/jquery.transfer.css" rel="stylesheet" />
    <script src="/assests/pluggins/Transfer/jquery.transfer.js"></script>   

    <script language="javascript" type="text/javascript">

        function AddUserDetails() {           
            var url = 'UserAccountAdd.aspx?id=Add';
            location.href = url;
        } 
        FieldName = 'Headermain1_cmbSegment';
        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        function callback() {
            grid.PerformCallback('All');
            // grid.PerformCallback();
        }
        function EndCall(obj) {
        }
    </script>  
</asp:Content>
<%--Rev work start .Refer: 25046 27.07.2022 New Listing page create for new User Account Page--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadCumb">
        <span>Users Account</span>
    </div>

    <div class="container mt-4">
        <div class="backBox p-3">
            <table class="TableMain100">              
                <tr>
                    <td>
                        <table width="100%" class="mb-3">
                            <tr>
                                <td style="text-align: left; vertical-align: top">
                                    <table>
                                        <tr>
                                            <td id="ShowFilter">
                                                <%if (rights.CanAdd)
                                                  { %>
                                                <a href="javascript:void(0);" onclick="AddUserDetails()" class="btn btn-success"><span>Add New</span> </a>
                                                <% } %>
                                                <% if (rights.CanExport)
                                                   { %>
                                                <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Export to</asp:ListItem>
                                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                                    <asp:ListItem Value="2">XLS</asp:ListItem>
                                                    <asp:ListItem Value="3">RTF</asp:ListItem>
                                                    <asp:ListItem Value="4">CSV</asp:ListItem>
                                                </asp:DropDownList>
                                                <% } %>                                                
                                            </td>                                           
                                        </tr>
                                    </table>
                                </td>
                                <td></td>

                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>                       
                        <dxe:ASPxGridView ID="userGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            KeyFieldName="USER_ID" Width="100%" OnCustomJSProperties="userGrid_CustomJSProperties" SettingsBehavior-AllowFocusedRow="true"
                            Settings-HorizontalScrollBarMode="Auto">                           
                            <Columns>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="0" FieldName="USER_ID"
                                    Caption="User ID" Width="100px" SortOrder="Descending">
                                    <EditFormSettings></EditFormSettings>
                                </dxe:GridViewDataTextColumn>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="1" FieldName="USER_NAME"
                                    Caption="User Name" Width="200px">
                                    <PropertiesTextEdit>
                                        <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                            <RequiredField ErrorText="Please Enter user Name" IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Caption="User Name:" Visible="True" />
                                </dxe:GridViewDataTextColumn>
                                <dxe:GridViewDataTextColumn VisibleIndex="2" Caption="User Type" FieldName="STAGE"  Width="200px">
                                </dxe:GridViewDataTextColumn>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="3" FieldName="BRANCHNAME"
                                    Caption="Branch" Width="220px" >
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>
                                <%--Rev Sanchita [ caption changed from Report To to WD ID--%>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="4" FieldName="REPORTTO"
                                    Caption="WD ID" Width="150px">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>                                
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="5" FieldName="CHANNELNAME"
                                    Caption="Channel"  Width="150px">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>                               
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="6" FieldName="CIRCLENAME"
                                    Caption="Circle" Width="220px" >
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="7" FieldName="SECTIONNAME"
                                    Caption="Section" Width="220px">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>   
                                <%--Rev Sanchita--%>   
                                <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="8" FieldName="deg_designation"
                                    Caption="Designation" Width="120px">
                                    <PropertiesTextEdit>
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="false" />
                                </dxe:GridViewDataTextColumn>   
                                <%--End of Rev Sanchita--%>                         
                            </Columns>                          
                            <SettingsSearchPanel Visible="True" />
                            <Settings ShowStatusBar="Hidden" ShowFilterRow="true" ShowGroupPanel="True" ShowFilterRowMenu="true" />                           
                            <SettingsBehavior ConfirmDelete="True" />
                            <ClientSideEvents EndCallback="function(s, e) {
	                            EndCall(s.cpHeight);
                            }" />
                        </dxe:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>        
        <dxe:ASPxGridViewExporter ID="exporter" runat="server">
        </dxe:ASPxGridViewExporter>
    </div>
</asp:Content>