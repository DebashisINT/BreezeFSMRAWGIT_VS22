<%@ Page Title="Branches" Language="C#" AutoEventWireup="True" MasterPageFile="~/OMS/MasterPage/ERP.Master" Inherits="ERP.OMS.Management.Master.management_master_Branch" CodeBehind="Branch.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .btn-pd {
            padding: 5px;
            margin-right: 1px;
        }
    </style>

    <script language="javascript" type="text/javascript">

        function OnEditButtonClick(keyValue) {
            var url = 'BranchAddEdit.aspx?id=' + keyValue;
            //OnMoreInfoClick(url, "Add New Accout", '920px', '500px', "Y");
            window.location.href = url;
        }
        function EndCall(obj) {
            if (grid.cpDelmsg != null)
                jAlert(grid.cpDelmsg);
        }
        function OnAddButtonClick() {
            var url = 'BranchAddEdit.aspx?id=ADD';
            //OnMoreInfoClick(url, "Add New Accout", '920px', '500px', "Y");
            window.location.href = url;
        }






        function DeleteRow(keyValue) {

            jConfirm('Confirm delete?', 'Confirmation Dialog', function (r) {             
                if (r == true) {                  
                    grid.PerformCallback('Delete~' + keyValue);
                }
            });
          
          
            //doIt = confirm('Confirm delete?');
            //alert(doIt);
            //if (doIt) {
            //    grid.PerformCallback('Delete~' + keyValue);
            //}
        }


        function ShowHideFilter1(obj) {
            gridTerminal.PerformCallback(obj);
        }

        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        function Page_Load() {
            document.getElementById("TdCombo").style.display = "none";
        }
        function callback() {
            grid.PerformCallback();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--rev 25249--%>
    <div class="breadCumb">
            <span>Branches</span>
        </div>
    
    <%--<div class="panel-heading">
        <div class="panel-title">
            <h3>Branches</h3>
        </div>
    </div>--%>
    <%--rev end 25249--%>
    <div class="container">
    <div class="backBox mt-5 p-3 ">
    <div class="form_main">
        <table class="TableMain100" style="width: 100%">
            <tr>
                <td style="text-align: left; vertical-align: top">
                    <table style="margin-bottom: 15px !important;">
                        <tr>
                            <td id="ShowFilter">
                                 <% if (rights.CanAdd)
                                   { %>
                                <a href="javascript:void(0);" onclick="javascript:OnAddButtonClick();" class="btn  btn-success">Add New</a>
                                  <% } %>
                                <% if (rights.CanExport)
                                               { %>
                                <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-primary btn-pd" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true"  >
                                    <asp:ListItem Value="0">Export to</asp:ListItem>
                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                     <asp:ListItem Value="2">XLS</asp:ListItem>
                                     <asp:ListItem Value="3">RTF</asp:ListItem>
                                     <asp:ListItem Value="4">CSV</asp:ListItem>
                        
                                </asp:DropDownList>
                                 <% } %>
                                <%--<a href="javascript:ShowHideFilter('s');" class="btn btn-primary"><span>Show Filter</span></a>--%>
                            </td>
                            <td id="Td1">
                                <%--<a href="javascript:ShowHideFilter('All');" class="btn btn-primary"><span>All Records</span></a>--%>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="gridcellright" style="float: right; vertical-align: top">
                    <%-- <div style="float: right">--%>
                    <%--<dxe:ASPxComboBox ID="cmbExport" runat="server" AutoPostBack="true"
                        Font-Bold="False" ForeColor="black" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged"
                        ValueType="System.Int32" Width="130px">
                        <Items>
                            <dxe:ListEditItem Text="Select" Value="0" />
                            <dxe:ListEditItem Text="PDF" Value="1" />
                            <dxe:ListEditItem Text="XLS" Value="2" />
                            <dxe:ListEditItem Text="RTF" Value="3" />
                            <dxe:ListEditItem Text="CSV" Value="4" />
                        </Items>
                        <Border BorderColor="black" />
                        <DropDownButton Text="Export">
                        </DropDownButton>
                    </dxe:ASPxComboBox>--%>
                    <%-- </div>--%>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left" colspan="2">
                    <dxe:ASPxGridView ID="gridStatus" ClientInstanceName="grid" Width="100%"
                        KeyFieldName="branch_id" DataSourceID="gridStatusDataSource" runat="server"
                        AutoGenerateColumns="False" OnCustomCallback="gridStatus_CustomCallback"  SettingsBehavior-AllowFocusedRow="true">
                                    <clientsideevents endcallback="function(s, e) {
	  EndCall(s.cpEND);
}" />
                        <Columns>
                            <dxe:GridViewDataTextColumn Visible="False" FieldName="branch_id" Caption="ID">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="0" FieldName="branch_code"
                                Caption="Short Name">
                                <CellStyle CssClass="gridcellleft" Wrap="True">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="branch_description"
                                Caption="Branch Name">
                                <CellStyle CssClass="gridcellleft" Wrap="True">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="2" FieldName="ParentBranch"
                                Caption="Parent Branch">
                                <CellStyle CssClass="gridcellleft" Wrap="True">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="3" FieldName="branch_cpPhone"
                                Caption="Branch Phone" Visible="True">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="4" FieldName="Country"
                                Caption="Country" Visible="true" >
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="5" FieldName="State"
                                Caption="State" Visible="True">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="6" FieldName="City"
                                Caption="City" Visible="True">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>


                            <dxe:GridViewDataTextColumn VisibleIndex="10" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="6%">
                                <%--<CellStyle CssClass="gridcellleft">
                                </CellStyle>--%>

                                <DataItemTemplate>
                                    <%-- <% if (rights.CanDelete)
                                       { %>--%>
                                   
                                    <%-- <% } %>--%>
                                     <% if (rights.CanEdit)
                                        { %>
                                    <a href="javascript:void(0);" onclick="OnEditButtonClick('<%# Container.KeyValue %>')" title="More Info" class="pad">
                                        <img src="../../../assests/images/info.png" />
                                    </a>
                                      <% } %>
                                     <% if (rights.CanDelete)
                                       { %>
                                     <a href="javascript:void(0);" onclick="DeleteRow('<%# Container.KeyValue %>')" title="Delete">
                                        <img src="../../../assests/images/Delete.png" /></a>
                                     <% } %>
                                </DataItemTemplate>
                                <HeaderTemplate>Actions</HeaderTemplate>
                                <%--<HeaderTemplate>--%>
                                <%-- <a href="javascript:void(0);" onclick="javascript:OnAddButtonClick();"><span style="color: #000099; text-decoration: underline">Add New</span> </a>--%>
                                <%--</HeaderTemplate>--%>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                        </Columns>

                        <SettingsText ConfirmDelete="Confirm delete?" />
                        <StylesEditors>
                            <ProgressBar Height="25px">
                            </ProgressBar>
                        </StylesEditors>
                        <SettingsSearchPanel Visible="True" />
                        <Settings ShowGroupPanel="True" ShowStatusBar="Hidden" ShowFilterRow="true" ShowFilterRowMenu = "True" />
                        <%--<Styles>
                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                            </Header>
                            <LoadingPanel ImageSpacing="10px">
                            </LoadingPanel>
                        </Styles>--%>
                        <%--<SettingsPager NumericButtonCount="20" PageSize="20" ShowSeparators="True" AlwaysShowPager="True">
                            <FirstPageButton Visible="True">
                            </FirstPageButton>
                            <LastPageButton Visible="True">
                            </LastPageButton>
                        </SettingsPager>--%>
                        <SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" />
                    </dxe:ASPxGridView>
                </td>
            </tr>
        </table>
        <dxe:ASPxGridViewExporter ID="exporter" runat="server">
        </dxe:ASPxGridViewExporter>
        <asp:SqlDataSource ID="gridStatusDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="">
          <%--  <SelectParameters>
                <asp:SessionParameter Name="userlist" SessionField="userchildHierarchy" Type="string" />
            </SelectParameters>--%>
        </asp:SqlDataSource>
    </div>
  </div>
 </div>
</asp:Content>
