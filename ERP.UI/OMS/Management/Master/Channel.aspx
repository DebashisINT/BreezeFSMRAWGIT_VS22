<%@ Page Title="Channel" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" Inherits="ERP.OMS.Management.Master.Channel" CodeBehind="Channel.aspx.cs" %>

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
            var ProductClassCode = grid.GetEditor('ch_Channel').GetValue();
            if ((id != null) && (id != '')) {
                proclassid = id;
                '<%=Session["Desigid"]=null %>'
            }
            var CheckUniqueCode = false;
            $.ajax({
                type: "POST",
                url: "Channel.aspx/CheckUniqueCode",
                data: JSON.stringify({ ProductClassCode: ProductClassCode, proclassid: proclassid }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    CheckUniqueCode = msg.d;
                    if (CheckUniqueCode == true) {
                        jAlert('Please enter unique Channel');
                        grid.GetEditor('ch_Channel').SetValue('');
                        grid.GetEditor('ch_Channel').Focus();
                    }
                }
            });
        }
    </script>

    <%--Mantis Issue 25017--%>
    <script>
        function fn_DSTypeMap(chnlID) {
            $("#hdnchnlID").val(chnlID);
            var str
            str = { chnlID: chnlID }
            var html = "";
            // alert();
            $.ajax({
                type: "POST",
                url: "Employee.aspx/GetDSTypeList",
                data: JSON.stringify(str),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    for (i = 0; i < responseFromServer.d.length; i++) {
                        if (responseFromServer.d[i].IsChecked == true) {
                            html += "<li><input type='checkbox' id=" + responseFromServer.d[i].StageID + "  class='statecheck' onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].StageID + " checked  /><a href='#'><label id='lblstatename' class='lblstate' for=" + responseFromServer.d[i].StageID + " >" + responseFromServer.d[i].Stage + "</label></a></li>";
                        }
                        else {
                            html += "<li><input type='checkbox' id=" + responseFromServer.d[i].StageID + " class='statecheck'  onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].StageID + "   /><a href='#'><label id='lblstatename' class='lblstate' for=" + responseFromServer.d[i].StageID + ">" + responseFromServer.d[i].Stage + "</label></a></li>";
                        }
                    }
                    $("#divModalBody").html(html);
                    $("#myModal").modal('show');
                }
            });
        }
    </script>

    <script>
        var Stagelist = []
        function StagePushPop() {
            var chnlID = $("#hdnchnlID").val();
            let a = [];

            $(".StageMapcheckall:checked").each(function () {
                a.push(this.value);
            });

            $(".StageMapcheck:checked").each(function () {
                a.push(this.value);
            });
            var str1
            //  alert(a);

            str1 = { chnlID: chnlID, Stagelist: a }
            $.ajax({
                type: "POST",
                url: "Channel.aspx/GetStagelistSubmit",
                data: JSON.stringify(str1),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    // alert(responseFromServer.d)
                    $("#myModalStageMap").modal('hide');
                    jAlert('DS Type assigned successfully');
                }
            });
        }

        function CheckParticular(v) {
            if (v == false) {
                $(".StageMapcheckall").prop('checked', false);
            }
        }

        function CheckAll(id) {
            var ischecked = $(".StageMapcheckall").is(':checked');
            if (ischecked == true) {
                $('input:checkbox.StageMapcheck').each(function () {
                    $(this).prop('checked', true);
                });

            }
            else {
                $('input:checkbox.StageMapcheck').each(function () {
                    $(this).prop('checked', false);
                });

            }


        }

        function fn_DSTypeMap(chnlID) {
            $("#hdnchnlID").val(chnlID);
            var str
            str = { CHNLID: chnlID }
            var html = "";
            // alert();
            $.ajax({
                type: "POST",
                url: "Channel.aspx/GetDSTypeList",
                data: JSON.stringify(str),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (responseFromServer) {
                    for (i = 0; i < responseFromServer.d.length; i++) {
                        if (responseFromServer.d[i].IsChecked == true) {
                            html += "<li><input type='checkbox' id=" + responseFromServer.d[i].StageID + "  class='StageMapcheck' onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].StageID + " checked  /><a href='#'><label id='BranchMapname' class='lblstate' for=" + responseFromServer.d[i].StageID + " >" + responseFromServer.d[i].Stage + "</label></a></li>";
                        }
                        else {
                            html += "<li><input type='checkbox' id=" + responseFromServer.d[i].StageID + " class='StageMapcheck'  onclick=CheckParticular($(this).is(':checked')) value=" + responseFromServer.d[i].StageID + "   /><a href='#'><label id='BranchMapname' class='lblstate' for=" + responseFromServer.d[i].StageID + ">" + responseFromServer.d[i].Stage + "</label></a></li>";
                        }
                    }
                    $("#divModalBodyStageMap").html(html);
                    $("#myModalStageMap").modal('show');
                }
            });
        }
        function ClearData() {
            $("#myModalStageMap").modal('hide');
        }
        function ClearChannelData() {
            $("#modalStageChannel").modal('hide');
        }
    </script>

    <%--Mantis Issue 25001--%>
    <style>
        #myInputStageMap {
            background-image: url('/css/searchicon.png'); /* Add a search icon to input */
            background-position: 10px 12px; /* Position the search icon */
            background-repeat: no-repeat; /* Do not repeat the icon image */
            width: 100%; /* Full-width */
            font-size: 16px; /* Increase font-size */
            padding: 12px 20px 12px 40px; /* Add some padding */
            border: 1px solid #ddd; /* Add a grey border */
            margin-bottom: 12px; /* Add some space below the input */
        }

        #divModalBodyStageMap {
            /* Remove default list styling */
            list-style-type: none;
            padding: 0;
            margin: 0;
            margin-bottom: 8px;
        }

            #divModalBodyStageMap li {
                padding: 5px 10px;
            }

                #divModalBodyStageMap li a {
                    margin-top: -1px; /* Prevent double borders */
                    padding: 0 12px; /* Add some padding */
                    text-decoration: none; /* Remove default text underline */
                    font-size: 14px; /* Increase the font-size */
                    color: black; /* Add a black text color */
                    display: inline-block; /* Make it into a block element to fill the whole list */
                    cursor: pointer;
                }
                .tblView>tbody>tr>td{
                    padding-right:5px;
                    padding-bottom:10px
                }
                .tblView>tbody>tr>td>label{
                    display:block
                }
    </style>

    <script>
        function myFunctionStageMap() {
            // Declare variables
            var input, filter, ul, li, a, i, txtValue;
            input = document.getElementById('myInputStageMap');
            filter = input.value.toUpperCase();
            ul = document.getElementById("divModalBodyStageMap");
            li = ul.getElementsByTagName('li');

            // Loop through all list items, and hide those who don't match the search query
            for (i = 0; i < li.length; i++) {
                a = li[i].getElementsByTagName("a")[0];
                txtValue = a.textContent || a.innerText;

                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    li[i].style.display = "";
                } else {
                    li[i].style.display = "none";
                }

            }
        }
    </script>
    <%--End of Mantis Issue 25017--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="breadCumb">
        <span>Channel</span>
    </div>
   
    <div class="container">
        <div class="backBox mt-5 p-3 ">
        <table class="TableMain100">
            <%--<tr>
                <td class="EHEADER" style="text-align: center">
                    <strong><span style="color: #000099">Channels List</span></strong></td>
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
                                           <%} %>
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
                    <dxe:ASPxGridView ID="DesigGrid" runat="server" AutoGenerateColumns="False" DataSourceID="channel"
                        KeyFieldName="ch_id" ClientInstanceName="grid" Width="100%" OnHtmlEditFormCreated="DesigGrid_HtmlEditFormCreated"
                        OnHtmlRowCreated="DesigGrid_HtmlRowCreated" OnCustomCallback="DesigGrid_CustomCallback"
                        OnStartRowEditing="DesigGrid_StartRowEditing" OnCommandButtonInitialize="DesigGrid_CommandButtonInitialize"
                        OnRowDeleting="DesigGrid_RowDeleting">
                        <ClientSideEvents EndCallback="function(s, e) {EndCall(s.cpEND);}"></ClientSideEvents>
                        <Columns>
                            <dxe:GridViewDataTextColumn Visible="False" ReadOnly="True" VisibleIndex="0" FieldName="ch_id">
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn VisibleIndex="0" FieldName="ch_Channel" Width="80%"
                                Caption="Channel">
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
                            <%--Mantis Issue 25017--%>
                            <dxe:GridViewDataTextColumn HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="center" VisibleIndex="1" Width="110px">
                                    <DataItemTemplate>
                                         <a href="javascript:void(0);" onclick="fn_DSTypeMap('<%# Container.KeyValue %>')" class="pad" title="DS Type Mapping">
                                            <span class='ico deleteColor'><i class='fa fa-sitemap' aria-hidden='true'></i></span></a>
                                     </DataItemTemplate>
                                </dxe:GridViewDataTextColumn>
                            <%--End of Mantis Issue 25017--%>
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
                        <SettingsText PopupEditFormCaption="Add/Modify Channel" ConfirmDelete="Confirm delete?" />
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

        <asp:SqlDataSource ID="channel" runat="server" ConflictDetection="CompareAllValues"
            DeleteCommand="DELETE FROM [Employee_Channel] WHERE [ch_id] = @original_ch_id"
            InsertCommand="IF NOT EXISTS (SELECT 'Y' FROM Employee_Channel WHERE ch_Channel = @ch_Channel) BEGIN INSERT INTO [Employee_Channel] ([ch_Channel], [CreateDate], [CreateUser]) VALUES (@ch_Channel, getdate(), @CreateUser) End"
            OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [ch_id],[ch_Channel] FROM [Employee_Channel]"
            UpdateCommand="IF NOT EXISTS (SELECT 'Y' FROM Employee_Channel WHERE ch_Channel = @ch_Channel AND [ch_id] <> @original_ch_id) BEGIN UPDATE [Employee_Channel] SET [ch_Channel] = @ch_Channel,[LastModifyDate]=getdate(),[LastModifyUser]=@CreateUser WHERE [ch_id] = @original_ch_id END">
            <DeleteParameters>
                <asp:Parameter Name="original_ch_id" Type="Decimal" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="ch_Channel" Type="String" />
                <asp:SessionParameter Name="CreateUser" Type="Decimal" SessionField="userid" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="ch_Channel" Type="String" />
                <asp:SessionParameter Name="CreateUser" Type="Decimal" SessionField="userid" />
            </InsertParameters>
        </asp:SqlDataSource>
        <dxe:ASPxGridViewExporter ID="exporter" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
        </dxe:ASPxGridViewExporter>
        <br />
    </div>
        </div>

    <%--Mantis Issue 25017--%>
    <div id="myModalStageMap" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 450px;">

            <div class="modal-content">
                <div class="modal-header">
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="ClearData();"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">DS Type List</h4>
                </div>
                <div class="modal-body">
                    <div>

                        <input type="text" id="myInputStageMap" onkeyup="myFunctionStageMap()" placeholder="Search for DS Type.">

                        <ul id="divModalBodyStageMap" class="listStyle">
                            <%--<input type="checkbox" id="idstate" class="statecheck" /><label id="lblstatename" class="lblstate"></label>--%>
                        </ul>
                    </div>
                    <input type="button" id="btnStageMapsubmit" title="SUBMIT" value="SUBMIT" class="btn btn-primary" onclick="StagePushPop()" />
                    <input type="hidden" id="hdnchnlID" />
                </div>
            </div>

        </div>
    </div>
    <%--End of Mantis Issue 25017--%>
</asp:Content>
