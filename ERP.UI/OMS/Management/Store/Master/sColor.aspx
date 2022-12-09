<%@ Page Title="Color Master" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master" Inherits="ERP.OMS.Management.Store.Master.management_master_Store_sProductClass" CodeBehind="sColor.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style>
        .dxbButton a {
            color: #000;
        }

        .dxbButton {
            height: 20px;
            line-height: 20px;
            padding: 0 5px;
        }
        #marketsGrid_DXPEForm_efnew_DXEFL_DXEditor1_EC,
        #marketsGrid_DXPEForm_efnew_DXEFL_DXEditor2_EC {
            position:absolute;
        }
    </style>

    <script type="text/javascript">

        //        function SetDropdownValue() { 
        //            document.getElementById('marketsGrid_DXPEForm_efnew_DXEditor4_I').value = '0';
        //        }

      

        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        //function height() {
        //    if (document.body.scrollHeight >= 500)
        //        window.frameElement.height = document.body.scrollHeight;
        //    else
        //        window.frameElement.height = '500px';
        //    window.frameElement.Width = document.body.scrollWidth;
        //}

        //function ShowError(obj) {
           
        //    if (grid.cpMsg != null) {
        //        jAlert(grid.cpMsg);
        //        grid.cpMsg = null;
        //    }
        //}
        function LastCall(obj) {
            if (grid.cpErrorMsg) {
                if (grid.cpErrorMsg.trim != "") {
                    jAlert(grid.cpErrorMsg);
                    grid.cpErrorMsg = '';
                    grid.PerformCallback();
                }
            }
            if (grid.cpDelmsg != null) {
                if (grid.cpDelmsg.trim() != '') {
                    jAlert(grid.cpDelmsg);
                    grid.cpDelmsg = '';
                    grid.PerformCallback();
                }
            }


        }
        function DeleteRow(keyValue) {

            jConfirm('Confirm delete?', 'Confirmation Dialog', function (r) {
                if (r == true) {
                    grid.PerformCallback('Delete~' + keyValue);
                }
            });
        }
        function UniqueCodeCheck() {
            var proclassid = '0';
            var id = '<%= Convert.ToString(Session["id"]) %>';  
            var ProductClassCode = grid.GetEditor('Color_Code').GetValue();
            if ((id != null) && (id != ''))
            {
                proclassid = id;
                '<%=Session["id"]=null %>'
            } 
            var CheckUniqueCode = false; 
            $.ajax({
                type: "POST",
                url: "sColor.aspx/CheckUniqueCode",
                data: JSON.stringify({ ProductClassCode: ProductClassCode, proclassid: proclassid }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    CheckUniqueCode = msg.d;
                    if (CheckUniqueCode == true) {
                        jAlert('Please enter unique short name');
                        grid.GetEditor('Color_Code').SetValue('');
                        grid.GetEditor('Color_Code').Focus();
                    }
                } 
            });
        }
    



        //function UniqueCodeCheck() {

        //    var ColorCode = grid.GetEditor('Color_Code').GetValue();
            
        //    var CheckUniqueCode = false;
        //    $.ajax({
        //        type: "POST",
        //        url: "sColor.aspx/CheckUniqueCode",
        //        data: "{'ColorCode':'" + ColorCode + "'}",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (msg) {
        //            CheckUniqueCode = msg.d;
        //            if (CheckUniqueCode == true) {
        //                alert('Please enter unique short name');
        //                grid.GetEditor('Color_Code').SetValue('');
        //                grid.GetEditor('Color_Code').Focus();
        //                //document.getElementById("marketsGrid_DXPEForm_efnew_DXEditor2_I").focus();
        //            }
        //        }

        //    });
        //}
    </script>
    <%--<script type="text/javascript">
        function fn_ctxtSize_Name_TextChanged() {
            var SizeName = document.getElementById("Popup_Empcitys_txtSize_Name_I").value;
            $.ajax({
                type: "POST",
                url: "sSize.aspx/CheckUniqueName",
                data: "{'SizeName':'" + SizeName + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    if (data == true) {
                        alert("Please enter unique name");
                        document.getElementById("Popup_Empcitys_txtSize_Name_I").value = "";
                        document.getElementById("Popup_Empcitys_txtSize_Name_I").focus();
                        return false;
                    }
                }

            });
        }
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--rev 25249--%>
    <%--<div class="panel-heading">
        <div class="panel-title">
            <h3>Color Master</h3>
        </div>

    </div>--%>
    <div class="breadCumb">
            <span>Color Master</span>
        </div>
    <%--rev end 25249--%>

    <div class="container">
    <div class="backBox mt-5 p-3 ">
    <div class="form_main">
        <table class="TableMain100">
            <%--     <tr>
            <td style="text-align: center">
                <strong><span style="color: #000099">Color Master</span></strong>
            </td>
        </tr>--%>
            <tr>
                <td>
                    <%--    <table width="100%">
                    <tr>
                        <td style="text-align: left; vertical-align: top">
                            <table>
                                <tr>
                                    <td id="ShowFilter">
                                        <a href="javascript:ShowHideFilter('s');"><span style="color: #000099; text-decoration: underline">
                                            Show Filter</span></a>
                                    </td>
                                    <td id="Td1">
                                        <a href="javascript:ShowHideFilter('All');"><span style="color: #000099; text-decoration: underline">
                                            All Records</span></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                        <td class="gridcellright">
                            <dxe:ASPxComboBox ID="cmbExport" runat="server" AutoPostBack="true" BackColor="Navy"
                                Font-Bold="False" ForeColor="White" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged"
                                ValueType="System.Int32" Width="130px">
                                <Items>
                                    <dxe:ListEditItem Text="Select" Value="0" />
                                    <dxe:ListEditItem Text="PDF" Value="1" />
                                    <dxe:ListEditItem Text="XLS" Value="2" />
                                    <dxe:ListEditItem Text="RTF" Value="3" />
                                    <dxe:ListEditItem Text="CSV" Value="4" />
                                </Items>
                                <ButtonStyle BackColor="#C0C0FF" ForeColor="Black">
                                </ButtonStyle>
                                <ItemStyle BackColor="Navy" ForeColor="White">
                                    <HoverStyle BackColor="#8080FF" ForeColor="White">
                                    </HoverStyle>
                                </ItemStyle>
                                <Border BorderColor="White" />
                                <DropDownButton Text="Export">
                                </DropDownButton>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                </table>--%>
                    <div class="SearchArea">
                        <div class="FilterSide mb-3" style="float: left; width: 500px">
                            <div style="float: left; padding-right: 5px;">
                                  <% if (rights.CanAdd)
                                               { %>
                                <a class="btn  btn-success mr-2" href="javascript:void(0);" onclick="grid.AddNewRow()">Add New</a><%} %>
                                <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true"  OnChange="if(!AvailableExportOption()){return false;}">
                                    <asp:ListItem Value="0">Export to</asp:ListItem>
                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                        <asp:ListItem Value="2">XLS</asp:ListItem>
                                        <asp:ListItem Value="3">RTF</asp:ListItem>
                                        <asp:ListItem Value="4">CSV</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                          <%--  <div>
                                <a class="btn btn-primary" href="javascript:ShowHideFilter('All');"><span>All Records</span></a>
                            </div>--%>
                        </div>
                        <%--<div class="ExportSide" style="float: right">
                            <div>
                                <dxe:ASPxComboBox ID="cmbExport" runat="server" AutoPostBack="true"
                                    Font-Bold="False" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged"
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
                                </dxe:ASPxComboBox>
                            </div>
                        </div>--%>
                    </div>
                </td>
            </tr>
            <tr>
                <td> <%--OnCustomErrorText="marketsGrid_CustomErrorText"--%>
                    <dxe:ASPxGridView ID="marketsGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"  OnStartRowEditing="marketsGrid_StartRowEditing"
                        DataSourceID="markets" KeyFieldName="Color_ID" Width="100%" OnHtmlRowCreated="marketsGrid_HtmlRowCreated"
                        OnHtmlEditFormCreated="marketsGrid_HtmlEditFormCreated" OnCustomCallback="marketsGrid_CustomCallback" OnInitNewRow="marketsGrid_InitNewRow" 
                        OnCommandButtonInitialize="marketsGrid_CommandButtonInitialize" OnCustomErrorText="marketsGrid_CustomErrorText" SettingsBehavior-AllowFocusedRow="true">
                       <%-- <ClientSideEvents EndCallback="function(s,e) { ShowError(s.cpInsertError);
                                                                                                 }" />--%>
                        <SettingsSearchPanel Visible="True" />
                        <Settings ShowGroupPanel="True" ShowStatusBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="true" />
                        <Columns>
                            <%--Color ID--%>
                            <dxe:GridViewDataTextColumn Visible="False" ReadOnly="True" VisibleIndex="0" FieldName="Color_ID">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="0" FieldName="Color_Code" Caption="Short Name">
                                <PropertiesTextEdit Width="300px" MaxLength="80">
                                    <ClientSideEvents TextChanged="function(s, e) {UniqueCodeCheck();}" />
                                     <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="right" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Mandatory" />
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                                <EditCellStyle HorizontalAlign="Left" Wrap="False">
                                    <Paddings PaddingTop="15px" />
                                </EditCellStyle>
                                <CellStyle Wrap="False">
                                </CellStyle>
                                <EditFormCaptionStyle Wrap="False" HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataTextColumn>
                            <%--Color Name--%>
                            <dxe:GridViewDataTextColumn VisibleIndex="0" FieldName="Color_Name" Caption="Name">
                                <PropertiesTextEdit Width="300px" MaxLength="50">
                                    <%--<ClientSideEvents TextChanged="function(s, e) {UniqueCodeCheck();}" />--%>
                                    <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="right" Display="Dynamic">
                                        <RequiredField IsRequired="True" ErrorText="Mandatory" />
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                                <EditCellStyle HorizontalAlign="Left" Wrap="False">
                                    <Paddings PaddingTop="15px" />
                                </EditCellStyle>
                                <CellStyle Wrap="False">
                                </CellStyle>
                                <EditFormCaptionStyle Wrap="False" HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataTextColumn>
                            <%--Color Description--%>
                            <dxe:GridViewDataTextColumn VisibleIndex="0" FieldName="Color_Description" Caption="Description">
                                <EditItemTemplate>
                                    <dxe:ASPxMemo ID="ASPxMemo1" runat="server" MaxLength="150" Width="300px" Height="60px" Text='<%# Bind("Color_Description") %>'>
                                    </dxe:ASPxMemo>
                                </EditItemTemplate>
                            </dxe:GridViewDataTextColumn>
                            <%--Color Code--%>
                            
                            <dxe:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="Color_CreateTime">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="Color_CreateUser">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="Color_ModifyUser">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Visible="False" VisibleIndex="1" FieldName="Color_ModifyTime">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewCommandColumn Width="6%" ShowEditButton="true" >
                                <%--  <DeleteButton Visible="True">
                            </DeleteButton>
                            <EditButton Visible="True">
                            </EditButton>--%>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    Actions                                   
                                </HeaderTemplate>
                                <CustomButtons>
                                    <dxe:GridViewCommandColumnCustomButton Image-Url="../../../../assests/images/Delete.png" Image-ToolTip="Delete">
                                       
                                    </dxe:GridViewCommandColumnCustomButton>

                                </CustomButtons>
                            </dxe:GridViewCommandColumn>
                        </Columns>
                        <ClientSideEvents CustomButtonClick="function(s, e) {
                             var key = s.GetRowKey(e.visibleIndex);
                             DeleteRow(key);
                            
                            }" />
                        <SettingsCommandButton>
                          <EditButton Image-Url="../../../../assests/images/Edit.png" ButtonType="Image" Image-AlternateText="Edit" Styles-Style-CssClass="pad">
                                                </EditButton>
                                                <DeleteButton Image-Url="../../../../assests/images/Delete.png" ButtonType="Image" Image-AlternateText="Delete">
                                                </DeleteButton>
                            <UpdateButton Text="Update" ButtonType="Button"  Styles-Style-CssClass="btn btn-primary btn-xs"></UpdateButton>
                            <CancelButton Text="Cancel" ButtonType="Button" Styles-Style-CssClass="btn btn-danger btn-xs"></CancelButton>
                        </SettingsCommandButton>
                        <Settings ShowStatusBar="Visible"></Settings>
                            <clientsideevents endcallback="function(s, e) {
	LastCall(s.cpHeight);
}" />
                        <Styles>
                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                            </Header>
                            <Cell CssClass="gridcellleft">
                            </Cell>
                            <LoadingPanel ImageSpacing="10px">
                            </LoadingPanel>
                        </Styles>
                        <SettingsText PopupEditFormCaption="Add/Modify Color" />
                        <SettingsPager NumericButtonCount="20" PageSize="20" AlwaysShowPager="True" ShowSeparators="True">
                            <FirstPageButton Visible="True">
                            </FirstPageButton>
                            <LastPageButton Visible="True">
                            </LastPageButton>
                        </SettingsPager>
                        <SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" />
                        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" PopupEditFormHorizontalAlign="Center"
                            PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" PopupEditFormWidth="450px" />
                        <Templates>
                            <EditForm>
                                <div style="padding: 5px; padding-bottom: 0px;">
                                    <table>
                                        <tr>
                                            <%--<td style="width: 25%">
                                        </td>--%>
                                            <td>
                                                <dxe:ASPxGridViewTemplateReplacement runat="server" ReplacementType="EditFormEditors" ColumnID="" ID="Editors"></dxe:ASPxGridViewTemplateReplacement>
                                                <div style=" padding: 2px 2px 6px 95px">
                                                    <div class="dxbButton" style="display: inline-block; color: Black;">
                                                        <dxe:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                            runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                                    </div>
                                                    <div class="dxbButton" style="display: inline-block; color: Black;">
                                                        <dxe:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                            runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                                    </div>
                                                </div>
                                            </td>
                                            <%--<td style="width: 25%">
                                        </td>--%>
                                        </tr>
                                    </table>
                                </div>
                            </EditForm>
                        </Templates>
                          
                    </dxe:ASPxGridView>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlSourceProductClass_ParentID" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="select ProductClass_ID,ProductClass_Name from Master_ProductClass "></asp:SqlDataSource>
        <asp:SqlDataSource ID="markets" runat="server" ConflictDetection="CompareAllValues"
            ConnectionString="<%$ ConnectionStrings:CRMConnectionString %>" DeleteCommand="DELETE FROM [dbo].[Master_Color] WHERE Color_ID = @original_Color_ID"
            InsertCommand="INSERT INTO [dbo].[Master_Color] ([Color_Code],[Color_Name],[Color_Description],[Color_CreateUser],[Color_CreateTime])
         VALUES (@Color_Code,@Color_Name,@Color_Description,@CreateUser,GETDATE())"
            OldValuesParameterFormatString="original_{0}"
            SelectCommand="SELECT [Color_ID],[Color_Name],[Color_Code],[Color_Description],[Color_CreateUser],
        [Color_CreateTime],[Color_ModifyUser],[Color_ModifyTime] FROM [dbo].[Master_Color]"
            UpdateCommand="UPDATE [dbo].[Master_Color] SET Color_Code = @Color_Code, Color_Name = @Color_Name, Color_Description = @Color_Description, Color_ModifyUser = @CreateUser, Color_ModifyTime = GETDATE()
        WHERE Color_ID = @original_Color_ID">
            <DeleteParameters>
                <asp:Parameter Name="original_Color_ID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="original_Color_ID" Type="Int32" />
                <asp:Parameter Name="Color_Code" Type="String" />
                <asp:Parameter Name="Color_Name" Type="String" />
                <asp:Parameter Name="Color_Description" Type="String" />
                <asp:SessionParameter Name="CreateUser" Type="Decimal" SessionField="userid" />
                <%-- <asp:Parameter Name="Markets_Country" Type="Int32" />
            <asp:SessionParameter Name="CreateUser" Type="Decimal" SessionField="userid" />--%>
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Color_Code" Type="String" />
                <asp:Parameter Name="Color_Name" Type="String" />
                <asp:Parameter Name="Color_Description" Type="String" />
                <asp:SessionParameter Name="CreateUser" Type="Decimal" SessionField="userid" />
            </InsertParameters>
        </asp:SqlDataSource>
        <dxe:ASPxGridViewExporter ID="exporter" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
        </dxe:ASPxGridViewExporter>
        <br />
    </div>
        </div>
        </div>
</asp:Content>
