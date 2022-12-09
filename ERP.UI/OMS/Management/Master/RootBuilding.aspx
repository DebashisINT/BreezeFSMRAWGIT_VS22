<%@ Page Title="Building/Warehouses" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master" Inherits="ERP.OMS.Management.Master.management_master_RootBuilding" CodeBehind="RootBuilding.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">
        function showhistory(obj) {

            //var URL = 'Contact_Document.aspx?idbldng=' + obj;
            var URL = 'Contact_Document.aspx?idbldng=' + obj;
            //editwin = dhtmlmodal.open("Editbox", "iframe", URL, "Document", "width=1000px,height=400px,center=0,resize=1,top=-1", "recal");
            window.location.href = URL;
            editwin.onclose = function () {
                grid.PerformCallback();
            }

        }
        function Show() {
            var url = "RootBuildingInsertUpdate.aspx?id=ADD";
            // popup.SetContentUrl(url);
            //OnMoreInfoClick(url, "Modify Building Details", '940px', '450px', "Y");
            // popup.Show();
            window.location.href = url;
        }
        function ClickOnMoreInfo(keyValue) {
            var url = 'RootBuildingInsertUpdate.aspx?id=' + keyValue;
            //OnMoreInfoClick(url, "Modify Building Details", '940px', '450px', "Y");
            window.location.href = url;
        }
        function callback() {
            // alert('1');
            buildingGrid.PerformCallback('All');
        }
    </script>

    <script language="javascript" type="text/javascript">

        function frmOpenNewWindow1(location, v_height, v_weight) {
            var x = (screen.availHeight - v_height) / 2;
            var y = (screen.availWidth - v_weight) / 2
            window.open(location, "Search_Conformation_Box", "height=" + v_height + ",width=" + v_weight + ",top=" + x + ",left=" + y + ",location=no,directories=no,menubar=no,toolbar=no,status=yes,scrollbars=yes,resizable=no,dependent=no'");
        }

        function ShowHideFilter(obj) {
            buildingGrid.PerformCallback(obj);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel-heading">
        <div class="panel-title">
            <h3>Building/Warehouses</h3>
        </div>
    </div>
    <div class="form_main">
        <table class="TableMain100" width="100%">
            <%--<tr>
                <td class="EHEADER" colspan="2" style="text-align: center;">
                    <strong><span style="color: #000099">Building Details</span></strong>
                </td>
            </tr>--%>
            <tr>
                <td style="text-align: left; vertical-align: top">
                    <table>
                        <tr>
                            <td id="ShowFilter">
                                 <% if (rights.CanAdd)
                                        { %>
                                <a href="javascript:void(0);" onclick="Show();" class="btn btn-primary"><span>Add New</span></a><% } %>
                                <%--<a href="javascript:ShowHideFilter('s');" class="btn btn-primary"><span>Show Filter</span></a>--%>
                            </td>
                            <td id="Td1">
                               <%-- <a href="javascript:ShowHideFilter('All');" class="btn btn-primary"><span>All Records</span></a>--%>
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
                        </tr>
                    </table>
                </td>
                <td class="gridcellright" style="float: right; vertical-align: top">&nbsp;
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
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left" colspan="2">
                    <dxe:ASPxGridView ID="RootGrid" ClientInstanceName="buildingGrid" runat="server"
                        AutoGenerateColumns="False" KeyFieldName="Id" Width="100%"
                        OnHtmlRowCreated="RootGrid_HtmlRowCreated" OnCustomCallback="RootGrid_CustomCallback" 
                        OnRowDeleting="RootGrid_RowDeleting" OnRowCommand="RootGrid_RowCommand">
                        <Columns>
                            <dxe:GridViewDataTextColumn Caption="Building/Warehouse  Name" FieldName="Building" VisibleIndex="0"
                                Width="25%">
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="Caretaker" FieldName="CareTaker" ReadOnly="True" VisibleIndex="1"
                                Width="25%">
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn FieldName="Address" ReadOnly="True" VisibleIndex="2"
                                Width="25%">
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn FieldName="Id" ReadOnly="True" Visible="False" VisibleIndex="1">
                                <EditFormSettings Visible="False" />
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="Details" VisibleIndex="3" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="7%">
                                <DataItemTemplate>
                                    <%--  <a href="javascript:void(0);" onclick="OnMoreInfoClick('<%# Container.KeyValue %>')">Update</a>--%>
                                      <% if (rights.CanEdit)
                                        { %>
                                    <a href="javascript:void(0);" onclick="ClickOnMoreInfo('<%# Container.KeyValue %>')" title="Edit" class="pad">
                                        <img src="../../../assests/images/Edit.png" /></a><% } %>
                                      <% if (rights.CanDelete)
                                        { %>
                                    <asp:LinkButton ID="btn_delete" runat="server" OnClientClick="return confirm('Confirm delete?');" CommandArgument='<%# Container.KeyValue %>' CommandName="delete" ToolTip="Delete" CssClass="pad" Font-Underline="false">
                                        <img src="../../../assests/images/Delete.png" />
                                    </asp:LinkButton><% } %>
                                     <% if (rights.CanView)
                                        { %>
                                    <a href="javascript:void(0);" onclick="showhistory('<%# Container.KeyValue %>')" title="Add/Update Document(s)">
                                        <img src="../../../assests/images/document.png" /></a><% } %>

                                </DataItemTemplate>
                                <HeaderTemplate>
                                    <span>Actions</span>
                                </HeaderTemplate>
                                <EditFormSettings Visible="False" />
                            </dxe:GridViewDataTextColumn>
                            <%-- <dxe:GridViewCommandColumn VisibleIndex="4" ShowDeleteButton="false" Visible="false">
                                <DeleteButton Visible="True">
                                </DeleteButton>
                                <HeaderTemplate>
                                </HeaderTemplate>
                            </dxe:GridViewCommandColumn>--%>
                            <%--<dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center"
                                Caption="Document">
                            </dxe:GridViewDataTextColumn>--%>
                        </Columns>
                        <%--  <SettingsCommandButton>
                            <DeleteButton ButtonType="Image" Image-Url="/assests/images/Delete.png"></DeleteButton>
                        </SettingsCommandButton>--%>
                        <SettingsSearchPanel Visible="True" />
                        <Settings ShowGroupPanel="True" ShowStatusBar="Hidden" ShowFilterRow="true" ShowFilterRowMenu ="true" />
                        <SettingsEditing Mode="PopupEditForm" PopupEditFormHeight="200px" PopupEditFormHorizontalAlign="Center"
                            PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" PopupEditFormWidth="600px"
                            EditFormColumnCount="1" />
                        <%--<Styles>
                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                            </Header>
                            <LoadingPanel ImageSpacing="10px">
                            </LoadingPanel>
                        </Styles>--%>
                        <SettingsText PopupEditFormCaption="Add/Modify CallDisposition" ConfirmDelete="Confirm delete?" />
                        <%--<SettingsPager NumericButtonCount="20" PageSize="20" AlwaysShowPager="True" ShowSeparators="True">
                            <FirstPageButton Visible="True">
                            </FirstPageButton>
                            <LastPageButton Visible="True">
                            </LastPageButton>
                        </SettingsPager>--%>
                        <SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" />
                        <Templates>
                            <EditForm>
                            </EditForm>
                        </Templates>
                    </dxe:ASPxGridView>
                    <dxe:ASPxPopupControl runat="server" ClientInstanceName="popup" CloseAction="CloseButton"
                        ContentUrl="RootBuilding.aspx.cs" HeaderText="Building Master" Left="150" Top="10"
                        Width="700px" Height="400px" ID="ASPXPopupControl">
                        <ContentCollection>
                            <dxe:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            </dxe:PopupControlContentControl>
                        </ContentCollection>
                    </dxe:ASPxPopupControl>
                </td>
            </tr>
        </table>
        <%--<asp:SqlDataSource ID="RootSource" ConflictDetection="CompareAllValues" runat="server"
            ConnectionString="<%$ ConnectionStrings:crmConnectionString %>" SelectCommand=""
            DeleteCommand="delete from tbl_master_building where bui_id=@Id">
            <DeleteParameters>
                <asp:Parameter Name="Id" Type="decimal" />
            </DeleteParameters>
        </asp:SqlDataSource>--%>
        <dxe:ASPxGridViewExporter ID="exporter" runat="server">
        </dxe:ASPxGridViewExporter>
    </div>
</asp:Content>
