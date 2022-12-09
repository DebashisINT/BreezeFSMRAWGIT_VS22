﻿<%@ Page Title="" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" CodeBehind="root_UserGroupMember.aspx.cs" Inherits="ERP.OMS.Management.Master.root_UserGroupMember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

        function AddUserDetails() {
            // document.location.href="RootUserDetails.aspx?id=Add"
            var url = 'RootUserDetails.aspx?id=Add';
            //OnMoreInfoClick(url, "Add User Details", '940px', '450px', "Y");
            location.href = url;
        }
        function EditUserDetails(keyValue) {

            //document.location.href="RootUserDetails.aspx?id="+keyValue;
            var url = 'RootUserDetails.aspx?id=' + keyValue;
            //OnMoreInfoClick(url, "Edit User Details", '940px', '450px', "Y");
            location.href = url;
        }
       
        function AddCompany(keyValue) {

            //document.location.href="RootUserDetails.aspx?id="+keyValue;
            var url = 'Root_AddUserCompany.aspx?id=' + keyValue;
            //OnMoreInfoClick(url, "Add Company Details for User", '940px', '450px', "Y");
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="breadCumb">
        <span>Group Member(s) of <asp:Label ID="txtgrpname" runat="server" ForeColor="#09608e"></asp:Label></span>
        <div id="divcross" runat="server" class="crossBtnN"><a href="root_UserGroups.aspx"><i class="fa fa-times"></i></a></div>
    </div>
                                         
    <div class="container mt-5">
        <div class="backBox p-5">
        <table class="TableMain100">
           
            <tr>
                <td>
                    <dxe:ASPxGridView ID="userGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                        DataSourceID="RootUserDataSource" KeyFieldName="user_id" Width="100%" OnCustomCallback="userGrid_CustomCallback" OnCustomJSProperties="userGrid_CustomJSProperties" SettingsBehavior-AllowFocusedRow="true"
                        SettingsCookies-Enabled="true" SettingsCookies-StorePaging="true" SettingsCookies-StoreFiltering="true" SettingsCookies-StoreGroupingAndSorting="true" >
                        <Columns>
                            <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="0" FieldName="user_id"
                                Visible="False">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="1" FieldName="user_name"
                                Caption="Name" Width="32%">
                                <PropertiesTextEdit>
                                    <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                        <RequiredField ErrorText="Please Enter user Name" IsRequired="True" />
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                                <EditFormSettings Caption="User Name:" Visible="True" />
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="2" Caption="Designation" FieldName="designation">
                            </dxe:GridViewDataTextColumn>
                           


                            <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="4" FieldName="Status"
                                Caption="User Status">
                                <PropertiesTextEdit>
                                    <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom" SetFocusOnError="True">
                                        <RequiredField ErrorText="Please Enter Login Id" IsRequired="True" />
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                                <EditFormSettings Caption="Login Id:" Visible="True" />
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="5" FieldName="Status" Settings-AllowAutoFilter="False"
                                Caption="Online Status" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">

                                  <DataItemTemplate>
                                  

                                         <%# Eval("Onlinestatus").ToString()=="1" ? "<img title='logged in' src='../../../assests/images/activeState.png' />" : "<img title='logged off' src='../../../assests/images/inactiveState.png' />" %>


                                </DataItemTemplate>
                                <HeaderTemplate>Online Status</HeaderTemplate>
                                <EditFormSettings Visible="False" />

                            </dxe:GridViewDataTextColumn>





                           
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
        <asp:SqlDataSource ID="RootUserDataSource" runat="server" ConflictDetection="CompareAllValues"
            DeleteCommand="DELETE FROM [tbl_master_user] WHERE [user_id] = @original_user_id"
            OldValuesParameterFormatString="original_{0}" SelectCommand="">
            <DeleteParameters>
                <asp:Parameter Name="original_user_id" Type="Decimal" />
            </DeleteParameters>
            <SelectParameters>
                <asp:SessionParameter Name="branch" SessionField="userbranchHierarchy" Type="string" />
            </SelectParameters>
        </asp:SqlDataSource>
        <dxe:ASPxGridViewExporter ID="exporter" runat="server">
        </dxe:ASPxGridViewExporter>
    </div>
</asp:Content>