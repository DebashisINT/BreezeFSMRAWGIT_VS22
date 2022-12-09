<%@ Page Title="Section" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" Inherits="ERP.OMS.Management.Master.Section" CodeBehind="Section.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">
        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        function EndCall(obj) {
            if (grid.cpDelmsg != null)
                jAlert(grid.cpDelmsg);
            grid.cpDelmsg = null;
        }
    </script>
    <script type="text/javascript">
        function UniqueCodeCheck() {
            var proclassid = '0';
            var id = '<%= Convert.ToString(Session["Desigid"]) %>';
            var ProductClassCode = grid.GetEditor('sec_Section').GetValue();
            if ((id != null) && (id != '')) {
                proclassid = id;
                '<%=Session["Desigid"]=null %>'
            }
            var CheckUniqueCode = false;
            $.ajax({
                type: "POST",
                url: "Section.aspx/CheckUniqueCode",
                data: JSON.stringify({ ProductClassCode: ProductClassCode, proclassid: proclassid }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    CheckUniqueCode = msg.d;
                    if (CheckUniqueCode == true) {
                        jAlert('Please enter unique Section');
                        grid.GetEditor('sec_Section').SetValue('');
                        grid.GetEditor('sec_Section').Focus();
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="breadCumb">
        <span>Section</span>
    </div>
   
    <div class="container">
        <div class="backBox mt-5 p-3 ">
        <table class="TableMain100">
            <%--<tr>
                <td class="EHEADER" style="text-align: center">
                    <strong><span style="color: #000099">Sections List</span></strong></td>
            </tr>--%>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="text-align: left; vertical-align: top">
                                <table class=" mb-4">
                                    <tr>
                                        <td id="ShowFilter">
                                            <% if (rights.CanAdd)
                                               { %>
                                            <a href="javascript:void(0);" onclick="grid.AddNewRow();" class="btn btn-success mr-2"><span>Add New</span> </a>
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
                                            <%--<a href="javascript:ShowHideFilter('s');" class="btn btn-success"><span>Show Filter</span></a>--%>
                                        </td>
                                        <td id="Td1">
                                            <%--<a href="javascript:ShowHideFilter('All');" class="btn btn-primary"><span>All Records</span></a>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <%--<td></td>
                            <td class="gridcellright pull-right">
                                <dxe:ASPxComboBox ID="cmbExport" runat="server" AutoPostBack="true"
                                    Font-Bold="False" ForeColor="black" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged"
                                    ValueType="System.Int32" Width="130px">
                                    <Items>
                                        <dxe:ListEditItem Text="Select" Value="0" />
                                        <dxe:ListEditItem Text="PDF" Value="1" />
                                        <dxe:ListEditItem Text="XLS" Value="2" />
                                        <dxe:ListEditItem Text="RTF" Value="3" />
                                        <dxe:ListEditItem Text="CSV" Value="4" />
                                    </Items>
                                    <ButtonStyle>
                                    </ButtonStyle>
                                    <ItemStyle>
                                        <HoverStyle>
                                        </HoverStyle>
                                    </ItemStyle>
                                    <Border BorderColor="black" />
                                    <DropDownButton Text="Export">
                                    </DropDownButton>
                                </dxe:ASPxComboBox>
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxGridView ID="DesigGrid" runat="server" AutoGenerateColumns="False" DataSourceID="section"
                        KeyFieldName="sec_id" ClientInstanceName="grid" Width="100%" OnHtmlEditFormCreated="DesigGrid_HtmlEditFormCreated"
                        OnHtmlRowCreated="DesigGrid_HtmlRowCreated" OnCustomCallback="DesigGrid_CustomCallback"
                        OnStartRowEditing="DesigGrid_StartRowEditing" OnCommandButtonInitialize="DesigGrid_CommandButtonInitialize"
                        OnRowDeleting="DesigGrid_RowDeleting">
                        <ClientSideEvents EndCallback="function(s, e) {EndCall(s.cpEND);}"></ClientSideEvents>
                        <Columns>
                            <dxe:GridViewDataTextColumn Visible="False" ReadOnly="True" VisibleIndex="0" FieldName="sec_id">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="0" FieldName="sec_Section" Width="80%"
                                Caption="Section">
                                <PropertiesTextEdit Width="300px" MaxLength="50">
                                    <ValidationSettings SetFocusOnError="True" ErrorTextPosition="Right" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField IsRequired="True" ErrorText="Mandatory"></RequiredField>
                                    </ValidationSettings>
                                    <ClientSideEvents TextChanged="function(s, e) {UniqueCodeCheck();}" Init="function (s,e) {s.Focus(); }" />
                                </PropertiesTextEdit>
                                <EditCellStyle HorizontalAlign="Left" Wrap="False">
                                    <Paddings PaddingTop="15px" />
                                </EditCellStyle>
                                <CellStyle Wrap="False">
                                </CellStyle>
                                <EditFormCaptionStyle Wrap="False" HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewCommandColumn VisibleIndex="1" ShowEditButton="true" ShowDeleteButton="true" HeaderStyle-HorizontalAlign="Center" Width="6%">
                                <%-- <DeleteButton Visible="True">
                                </DeleteButton>
                                <EditButton Visible="True">
                                </EditButton>--%>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    Actions
                                    <%--<%if (Session["PageAccess"].ToString().Trim() == "All" || Session["PageAccess"].ToString().Trim() == "Add" || Session["PageAccess"].ToString().Trim() == "DelAdd")
                                      { %>
                                    <a href="javascript:void(0);" onclick="grid.AddNewRow();"><span>Add New</span> </a>
                                    <%} %>--%>
                                </HeaderTemplate>
                            </dxe:GridViewCommandColumn>
                        </Columns>
                        <SettingsCommandButton>

                            <EditButton Image-Url="../../../assests/images/Edit.png" ButtonType="Image" Image-AlternateText="Edit" Styles-Style-CssClass="pad">
                            </EditButton>
                            <DeleteButton Image-Url="../../../assests/images/Delete.png" ButtonType="Image" Image-AlternateText="Delete">
                            </DeleteButton>
                            <UpdateButton Text="Save" ButtonType="Button" Styles-Style-CssClass="btn btn-primary btn-xs"></UpdateButton>
                            <CancelButton Text="Cancel" ButtonType="Button" Styles-Style-CssClass="btn btn-danger btn-xs"></CancelButton>
                        </SettingsCommandButton>
                        <SettingsSearchPanel Visible="True" />
                        <Settings ShowGroupPanel="True" ShowStatusBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="true"></Settings>
                        <Styles>
                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                            </Header>
                            <Cell CssClass="gridcellleft">
                            </Cell>
                            <LoadingPanel ImageSpacing="10px">
                            </LoadingPanel>
                        </Styles>
                        <SettingsText PopupEditFormCaption="Add/Modify Section" ConfirmDelete="Confirm delete?" />
                        <SettingsPager NumericButtonCount="20" PageSize="20" AlwaysShowPager="True" ShowSeparators="True">
                            <FirstPageButton Visible="True">
                            </FirstPageButton>
                            <LastPageButton Visible="True">
                            </LastPageButton>
                        </SettingsPager>




                        <SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" />
                        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm"
                            PopupEditFormHorizontalAlign="Center" PopupEditFormModal="True" PopupEditFormVerticalAlign="Windowcenter"
                            PopupEditFormWidth="400px" />
                        <Templates>
                            <EditForm>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%"></td>
                                        <td style="width: 50%">
                                            <controls>
                                <dxe:ASPxGridViewTemplateReplacement runat="server" ReplacementType="EditFormEditors" ColumnID="" ID="Editors">
                                </dxe:ASPxGridViewTemplateReplacement>                                                           
                            </controls>
                                            <div style="text-align: left; padding: 2px 2px 2px 94px">
                                                <dxe:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                    runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                                <dxe:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                    runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                            </div>
                                        </td>
                                        <td style="width: 25%"></td>
                                    </tr>
                                </table>
                            </EditForm>
                        </Templates>
                    </dxe:ASPxGridView>
                </td>
            </tr>
        </table>

        <asp:SqlDataSource ID="section" runat="server" ConflictDetection="CompareAllValues"
            DeleteCommand="DELETE FROM [Employee_Section] WHERE [sec_id] = @original_sec_id"
            InsertCommand="IF NOT EXISTS (SELECT 'Y' FROM Employee_Section WHERE sec_Section = @sec_Section) BEGIN INSERT INTO [Employee_Section] ([sec_Section], [CreateDate], [CreateUser]) VALUES (@sec_Section, getdate(), @CreateUser) End"
            OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [sec_id],[sec_Section] FROM [Employee_Section]"
            UpdateCommand="IF NOT EXISTS (SELECT 'Y' FROM Employee_Section WHERE sec_Section = @sec_Section AND [sec_id] <> @original_sec_id) BEGIN UPDATE [Employee_Section] SET [sec_Section] = @sec_Section,[LastModifyDate]=getdate(),[LastModifyUser]=@CreateUser WHERE [sec_id] = @original_sec_id END">
            <DeleteParameters>
                <asp:Parameter Name="original_sec_id" Type="Decimal" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="sec_Section" Type="String" />
                <asp:SessionParameter Name="CreateUser" Type="Decimal" SessionField="userid" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="sec_Section" Type="String" />
                <asp:SessionParameter Name="CreateUser" Type="Decimal" SessionField="userid" />
            </InsertParameters>
        </asp:SqlDataSource>
        <dxe:ASPxGridViewExporter ID="exporter" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
        </dxe:ASPxGridViewExporter>
        <br />
    </div>
        </div>
</asp:Content>
