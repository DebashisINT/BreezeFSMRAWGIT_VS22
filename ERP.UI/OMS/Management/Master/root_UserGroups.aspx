﻿<%@ Page Title="User Groups" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master" CodeBehind="root_UserGroups.aspx.cs" Inherits="ERP.OMS.Management.Master.management_master_root_UserGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <%--   <link href="/assests/pluggins/easyui/easyui.css" rel="stylesheet" />
    <link href="/assests/pluggins/easyui/icon.css" rel="stylesheet" />
    <link href="/assests/pluggins/jquery.alert/jquery.alerts.css" rel="stylesheet" />--%>
    <script src="/assests/pluggins/jquery.alert/jquery.ui.draggable.js"></script>
    <script src="/assests/pluggins/jquery.alert/jquery.alerts.js"></script>

    <style type="text/css">
        .tree-title {
            width: 600px;
        }

        .tree-title span {
            float: right;
        }

        .tree-folder {
            display: none !important;
        }

        .tree-folder-open {
            display: none !important;
        }

        .tree-icon {
            display: none !important;
        }

        .tree-file {
            display: none !important;
        }

        .chckRights {
            left: 3px;
            position: relative;
        }

        
    </style>

    <script type="text/javascript" src="/assests/pluggins/easyui/jquery.easyui.min.js"></script>
    <script lang="javascript" type="text/javascript">
        $(function () {

            if ($('#ulMenuTree')) {
                $('#ulMenuTree').tree({
                    checkbox: true,
                    cascadeCheck: true,
                    animate: true,
                    icon: false,
                    onLoadSuccess: function (node, data) {
                        CheckListgenerator();
                        CheckSelectedValues();
                    }
                });
            }

            if ($('#hdnMessage').val()) {
               // jAlert($('#hdnMessage').val());
            }

            $('.chckRights').click(function () {
                GenerateData();
            });

            $('#btnTagAll').click(function (e) {
                e.preventDefault();
                $('.chckRights').prop('checked', true);
                GenerateData();
            });

            $('#btnUnTagAll').click(function (e) {
                e.preventDefault();
                $('.chckRights').prop('checked', false);
                GenerateData();
            });
        });

        function OnMemberInfoClick(keyValue) {
            //alert(keyValue);
            document.location.href = "root_UserGroupMember.aspx?grp=" + keyValue + "";
            //$('#dvUserList').load('/Ajax/_PartialGroupUserListForShow', { GroupId: keyValue }, function () {
            //    $('#dvgrpUserList').modal('show');
            //});
        }

        function CheckSelectedValues() {
            var $rightChecked = $('.chckRights');
            var GroupUserRights = $('#GroupUserRights').val();
            if (GroupUserRights) {
                var SubMenuWithRights = GroupUserRights.split('_');
                for (var i = 0; i < SubMenuWithRights.length; i++) {
                    var SubMenuId = SubMenuWithRights[i].split('^')[0];
                    var rights = SubMenuWithRights[i].split('^')[1].split('|');

                    if (rights && rights.length > 0) {
                        $.each($rightChecked, function (index, value) {
                            var $chck = $(this);
                            var menuid = $chck.attr('data-menuid');
                            if (menuid == SubMenuId) {
                                var role = $chck.attr('data-id');

                                for (var j = 0; j < rights.length; j++) {
                                    if (role == rights[j]) {
                                        $chck.prop('checked', true);
                                    }
                                }
                            }
                        });
                    }
                }
            }
        }

        function GenerateData() {
            var $rightChecked = $('.chckRights:checked');

            var subMenuIds = [];

            $.each($rightChecked, function (index, value) {
                var SubMenuId = $(this).attr('data-menuid');

                if (subMenuIds && subMenuIds.length > 0) {
                    var flagVal = true;
                    for (var i = 0; i < subMenuIds.length; i++) {
                        if (subMenuIds[i] == SubMenuId) {
                            flagVal = false;
                            break;
                        }
                    }
                    if (flagVal) {
                        subMenuIds.push(SubMenuId);
                    }
                }
                else {
                    subMenuIds.push(SubMenuId);
                }
            });

            if (subMenuIds && subMenuIds.length > 0) {
                var MenuWithRole = '';
                for (var i = 0; i < subMenuIds.length; i++) {
                    var roleString = '';
                    $.each($rightChecked, function (index, value) {
                        var SubMenuId = $(this).attr('data-menuid');
                        var role = $(this).attr('data-id');
                        if (SubMenuId == subMenuIds[i]) {
                            if (roleString == '') {
                                roleString = role;
                            }
                            else {
                                roleString += '|' + role;
                            }
                        }
                    });
                    if (roleString != '') {
                        if (MenuWithRole != '') {
                            MenuWithRole += '_' + subMenuIds[i] + '^' + roleString;
                        }
                        else {
                            MenuWithRole = subMenuIds[i] + '^' + roleString;
                        }
                    }
                }
                $('#GroupUserRights').val(MenuWithRole);
            }
            else {
                $('#GroupUserRights').val('');
            }

            CheckListgenerator();
        }

        function CheckListgenerator() {
            var GroupUserRights = $('#GroupUserRights').val();
            var nodes = $('#ulMenuTree').tree('getChecked');
            $.each(nodes, function (index, value) {
                var node = $('#ulMenuTree').tree('find', value.id);
                $('#ulMenuTree').tree('uncheck', node.target);
            });
            if (GroupUserRights) {
                var SubMenuWithRights = GroupUserRights.split('_');
                for (var i = 0; i < SubMenuWithRights.length; i++) {
                    var SId = SubMenuWithRights[i].split('^')[0];
                    var node = $('#ulMenuTree').tree('find', SId);
                    if (node) {
                        $('#ulMenuTree').tree('check', node.target);
                    }
                }
            }
        }

        function ConfirmToDelete() {
            var value = confirm('Are you sure you want to delete this group?');
            return value;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="breadCumb">
        
            <span>User Groups <%--&nbsp;<% ERP.OMS.MVCUtility.RenderAction("Test", "_PartialWebFormToMvcTest", new { }); %>--%></span>
        
    </div>
    <div class="container">
        <div class="backBox p-3">
        <table class="TableMain100" style="width: 100%">
            <tr>
                <td style="text-align: left">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <table width="100%" class="mt-2 mb-3">
                                    <tr>
                                        <td>
                                            <% if (rights.CanAdd)
                                               { %>
                                            <asp:Button ID="btn_Add_New" runat="server" Text="Add New" CssClass="btn btn-success" OnClick="btn_Add_New_Click" />
                                             <% } %>
                                            <% if (rights.CanExport)
                                               { %>
                                            <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true"  >
                                                <asp:ListItem Value="0">Export to</asp:ListItem>
                                                <asp:ListItem Value="1">PDF</asp:ListItem>
                                                 <asp:ListItem Value="2">XLS</asp:ListItem>
                                                 <asp:ListItem Value="3">RTF</asp:ListItem>
                                                 <asp:ListItem Value="4">CSV</asp:ListItem>
                                            </asp:DropDownList>
                                              <% } %>
                                        </td>
                                        <%--<td class="pull-right">
                                            <dxe:ASPxComboBox ID="cmbExport" runat="server" AutoPostBack="true"
                                                ForeColor="Black" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged"
                                                ValueType="System.Int32" Width="130px">
                                                <Items>
                                                    <dxe:ListEditItem Text="Select" Value="0" />
                                                    <dxe:ListEditItem Text="PDF" Value="1" />
                                                    <dxe:ListEditItem Text="XLS" Value="2" />
                                                    <dxe:ListEditItem Text="RTF" Value="3" />
                                                    <dxe:ListEditItem Text="CSV" Value="4" />
                                                </Items>
                                                <Border BorderColor="Black" />
                                                <DropDownButton Text="Export">
                                                </DropDownButton>
                                            </dxe:ASPxComboBox>
                                        </td>--%>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; vertical-align: top;" runat="server">
                                <dxe:ASPxGridView ID="GridUserGroup" runat="server" Width="100%" AutoGenerateColumns="False"
                                    ClientInstanceName="GridUserGroup" KeyFieldName="grp_id" OnRowCommand="GridUserGroup_RowCommand"
                                    OnRowDeleting="GridUserGroup_RowDeleting" 
                                    Border-BorderStyle="NotSet" SettingsBehavior-AllowFocusedRow="true">
                                  
                                    <Columns>

                                        <dxe:GridViewDataTextColumn VisibleIndex="0" FieldName="grp_name"
                                            Caption="Group Name">
                                        </dxe:GridViewDataTextColumn>


                                        <dxe:GridViewDataTextColumn VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" Width="6%">
                                            <DataItemTemplate>
                                                 <% if (rights.CanEdit)
                                                   { %>
                                                <asp:LinkButton ID="btn_show" runat="server" CommandArgument='<%# Container.KeyValue %>' CommandName="edit" Font-Underline="false" CssClass="pad">
                                                     <img src="../../../assests/images/Edit.png" />
                                                </asp:LinkButton>
                                                 <% } %>
                                                <% if (rights.CanDelete)
                                                   { %>
                                                <asp:LinkButton ID="btn_delete" runat="server" OnClientClick="return confirm('Confirm Delete?');" CommandArgument='<%# Container.KeyValue %>' CommandName="delete"> 

                                                      <img src="../../../assests/images/Delete.png" />
                                                </asp:LinkButton>


                                                 <% } %>
                                            </DataItemTemplate>
                                            <HeaderTemplate>Actions</HeaderTemplate>
                                        </dxe:GridViewDataTextColumn>

                                        <dxe:GridViewDataTextColumn VisibleIndex="1" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" Width="6%">
                                            <DataItemTemplate>
                                                 <% if (rights.CanMembers)
                                                   { %>
                                                <a href="javascript:void(0);" onclick="OnMemberInfoClick('<%# Container.KeyValue %>')" title="Members">
                                                    <img src="../../../assests/images/Members.png" />
                                                </a> <% } %>
                                            </DataItemTemplate>
                                            <HeaderTemplate>Members</HeaderTemplate>
                                        </dxe:GridViewDataTextColumn>

                                    </Columns>
                                    <SettingsSearchPanel Visible="True" />
                                     <Settings ShowTitlePanel="false" ShowStatusBar="Hidden" ShowFilterRow="true" ShowGroupPanel="true" ShowFilterRowMenu ="true"/>
                                    <SettingsBehavior AllowFocusedRow="true" ConfirmDelete="True" />
                                    <SettingsText ConfirmDelete="Do you want to delete this?" />

                                 
                                </dxe:ASPxGridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
        <table id="tblCreateModifyForms" runat="server" style="width: 100%" visible="false">
            <tr>
                <td style="width: 100%">
                    <div class="panel-body">
                        <div class="form-horizontal" style="padding: 10px;">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class=" col-sm-4">
                                            <asp:Label ID="lblGroupName" runat="server" Text="Group Name"></asp:Label>
                                        </div>
                                        <div class="col-sm-4 divDown">
                                            <asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 divDown">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Save" />&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" OnClick="btnCancel_Click" Text="Cancel" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">&nbsp;</div>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="row">
                                <div class="form-group">
                                    <div class=" col-sm-12">
                                        <div class="col-sm-3 divDown">
                                            <input type="button" class="btn btn-primary" id="btnTagAll" value="Tag All" />&nbsp;
                                            <input type="button" class="btn btn-primary " id="btnUnTagAll" value="Untag All" />
                                            <asp:HiddenField ID="GroupUserRights" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix">&nbsp;</div>
                            <div class="row">
                                <div class="form-group">
                                    <div class=" col-sm-12" id="dvTreeMenus" runat="server"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnMessage" ClientIDMode="Static" runat="server" />
        <dxe:ASPxGridViewExporter ID="exporter" runat="server" GridViewID="GridUserGroup">
        </dxe:ASPxGridViewExporter>
    </div>
    <div class="modal fade" id="dvgrpUserList">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title">Users</h4>
                </div>
                <div class="modal-body" id="dvUserList"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success btnwidth" data-dismiss="modal">Ok</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
