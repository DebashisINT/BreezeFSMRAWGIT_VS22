<%@ Page Title="Brand Master" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" Inherits="ERP.OMS.Management.Master.management_frm_Brand" CodeBehind="frm_Brand.aspx.cs" %>

 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style type="text/css">
        .stateDiv {
            height: 25px;
            width: 68px;
            float: left;
        }

        .dxpc-headerContent {
            color: white;
        }
    </style>
    <script type="text/javascript" src="../../CentralData/JSScript/GenericJScript.js"></script>

    <script type="text/javascript"> 
        function fn_ctxtBrandNBame_TextChanged(s, e) {
           
            //var ProductName = ctxtPro_Name.GetText();
            var brandCode = 0;
            if (status == 'updateBrand') {
                brandCode = document.getElementById('hdBrandId').value;
            }
            var BrandName = ctxtBrandNBame.GetText().trim();
            $.ajax({
                type: "POST",
                url: "frm_Brand.aspx/CheckUniqueName",
                //data: "{'ProductName':'" + ProductName + "'}",
                data: JSON.stringify({ BrandName: BrandName, BrandCode: brandCode }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;

                    if (data == true) {
                        jAlert("Please Enter Unique Brand Name");
                        ctxtBrandNBame.SetText("");
                        ctxtBrandNBame.SetFocus();
                        //document.getElementById("Popup_Empcitys_ctxtPro_Code_I").focus();
                        document.getElementById("txtBrandNBame").focus();

                        return false;
                    }
                }

            });
        }



        var status = 'saveBrand';

        function fn_PopUpOpen() {
            status = 'saveBrand';
            $('#valid').attr('style', 'display:none;');
            ctxtBrandNBame.SetText('');
            ctxtContactNo.SetText('');
            ctxtEmail.SetText('');
            cPopupBrand.SetHeaderText('Add Brand'); 
            cPopupBrand.Show();

        }
        function fn_EditCountry(keyValue) {
            document.getElementById('hdBrandId').value = keyValue;
            status = 'updateBrand';
            grid.PerformCallback('Edit~' + keyValue);
        }
        function fn_DeleteCountry(keyValue) {
            jConfirm('Confirm delete?', 'Confirmation Dialog', function (r) {
                if (r == true) {
                    grid.PerformCallback('Delete~' + keyValue);
                }
                else {
                    return false;
                }
            });


        }
        function fn_btnCancel() {
            ctxtBrandNBame.SetText("");
            status = 'saveBrand';
            cPopupBrand.Hide();
        }
        function btnSave_Brand() {
            var countrynm = ctxtBrandNBame.GetText();
            if (countrynm.trim() == '') {
                $('#valid').attr('style', 'display:block;position: absolute;right: 32px;top: 17px;');
               
                ctxtBrandNBame.Focus();
            }
            else {
                grid.PerformCallback(status);
               
            }
        }


        function grid_EndCallBack() {

            if (grid.cpMsg != null) {
                if (grid.cpMsg != '') {
                    jAlert(grid.cpMsg);
                    fn_btnCancel();
                    grid.cpMsg = null;
                }
            }

            if (grid.cpEdit) {
                if (grid.cpEdit != '') {
                    var ReturnData = grid.cpEdit.split('|@|');
                    ctxtBrandNBame.SetText(ReturnData[0]);
                    ctxtContactNo.SetText(ReturnData[1]);
                    ctxtEmail.SetText(ReturnData[2]);
                    cPopupBrand.Show();
                    grid.cpEdit = null;
                }
            }



        }
    </script>



    <style>
        .dxgvHeader {
            border: 1px solid #2c4182 !important;
            background-color: #415698 !important;
        }

            .dxgvHeader, .dxgvHeader table {
                color: #fff !important;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div class="breadCumb">
            <span>Brand</span>
        </div>
    
    <div class="container">
        <div class="backBox mt-5 p-3 ">
        <div class="Main"> 

            <div class="SearchArea clearfix">
                <div class="FilterSide mb-4">
                    <div style="padding-right: 5px;">
                        <% if (rights.CanAdd)
                           { %>
                        <a href="javascript:void(0);" onclick="fn_PopUpOpen()" class="btn btn-success mr-2"><span>Add New</span> </a>
                        <% } %> 
                        <% if (rights.CanExport)
                                               { %>
                        <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true" OnChange="if(!AvailableExportOption()){return false;}">
                            <asp:ListItem Value="0">Export to</asp:ListItem>
                            <asp:ListItem Value="1">PDF</asp:ListItem>
                            <asp:ListItem Value="2">XLS</asp:ListItem>
                            <asp:ListItem Value="3">RTF</asp:ListItem>
                            <asp:ListItem Value="4">CSV</asp:ListItem>
                        </asp:DropDownList>
                        <% } %>
                    </div>
                
                </div>

            </div>

            <div class="GridViewArea">
                <dxe:ASPxGridView ID="GridBrand" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
                    KeyFieldName="Brand_Id" Width="100%" OnHtmlEditFormCreated="GridBrand_HtmlEditFormCreated"
                    OnCustomCallback="GridBrand_CustomCallback" SettingsBehavior-AllowFocusedRow="true"> 
                    <Columns>
                        <dxe:GridViewDataTextColumn Caption="Brand ID" FieldName="Brand_Id" ReadOnly="True"
                            Visible="False" VisibleIndex="0">
                            <EditCellStyle HorizontalAlign="Left">
                            </EditCellStyle>
                            <EditFormSettings Visible="False" VisibleIndex="1" />
                        </dxe:GridViewDataTextColumn>
                        <dxe:GridViewDataTextColumn Caption="Brand Name" FieldName="Brand_Name" VisibleIndex="1"
                            Width="90%">
                            <EditFormSettings Visible="True" />
                        </dxe:GridViewDataTextColumn>
 
                        <dxe:GridViewDataTextColumn CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" Width="6%">
                            <HeaderTemplate>
                                <span>Actions</span>
                            </HeaderTemplate>
                            <DataItemTemplate>
                                <% if (rights.CanEdit)
                                   { %>
                                <a href="javascript:void(0);" onclick="fn_EditCountry('<%#Container.KeyValue %>')" class="pad" title="Edit">
                                    <img src="../../../assests/images/Edit.png" /></a>
                                <% } %>
                                <% if (rights.CanDelete)
                                   { %>
                                <a href="javascript:void(0);" onclick="fn_DeleteCountry('<%#Container.KeyValue %>')" title="Delete">
                                    <img src="../../../assests/images/Delete.png" /></a>
                                <% } %>
                            </DataItemTemplate>
                        </dxe:GridViewDataTextColumn>
                    </Columns>
                    <SettingsSearchPanel Visible="True" />
                    <Settings ShowGroupPanel="True" ShowStatusBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="true" />
                    



                    <ClientSideEvents EndCallback="function (s, e) {grid_EndCallBack();}" />
                </dxe:ASPxGridView>
            </div>

            <div class="PopUpArea">
                <dxe:ASPxPopupControl ID="PopupBrand" runat="server" ClientInstanceName="cPopupBrand"
                    Width="400px" Height="90px" HeaderText="Add/Modify Brand" PopupHorizontalAlign="Windowcenter"
                    PopupVerticalAlign="WindowCenter" CloseAction="closeButton" Modal="true">
                    <ContentCollection>
                        <dxe:PopupControlContentControl ID="countryPopup" runat="server">
                            <div class="Top clearfix">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <div class="" style="padding-top: 5px;">Brand Name:<span style="color: red;">*</span></div>
                                        </td>
                                        <td class="relative">
                                            <div style="padding-top: 5px;">
                                                <dxe:ASPxTextBox ID="txtBrandNBame" ClientInstanceName="ctxtBrandNBame" ClientEnabled="true"
                                                    runat="server" Width="90%" MaxLength="50">
                                                     <ClientSideEvents TextChanged="function(s,e){fn_ctxtBrandNBame_TextChanged(s,e)}" />
                                                </dxe:ASPxTextBox>
                                                <div id="valid" style="display: none; position: absolute; right: 1px;top: 11px;">
                                                    <img id="grid_DXPEForm_DXEFL_DXEditor2_EI" title="Mandatory" class="dxEditors_edtError_PlasticBlue" src="/DXR.axd?r=1_36-YRohc" alt="Required" /></div>
                                            </div>
                                        </td>
                                    </tr>

                                     <tr>
                                        <td>
                                            <div class="" style="padding-top: 5px;">Contact No.</div>
                                        </td>
                                        <td class="relative">
                                            <div style="padding-top: 5px;">
                                                <dxe:ASPxTextBox ID="txtContactNo" ClientInstanceName="ctxtContactNo" ClientEnabled="true"
                                                    runat="server" Width="90%" MaxLength="100"> 
                                                </dxe:ASPxTextBox>
                                                
                                        </td>
                                    </tr>


                                     <tr>
                                        <td>
                                            <div class="" style="padding-top: 5px;">Email:</div>
                                        </td>
                                        <td class="relative">
                                            <div style="padding-top: 5px;">
                                                <dxe:ASPxTextBox ID="txtEmail" ClientInstanceName="ctxtEmail" ClientEnabled="true"
                                                    runat="server" Width="90%" MaxLength="100"> 
                                                </dxe:ASPxTextBox>
                                                
                                        </td>
                                    </tr>


                                    <tr>
                                        <td></td>
                                        <td style="padding-top: 15px;">
                                            <div class="Footer" >
                                            <div style="float: left;">
                                                <dxe:ASPxButton ID="btnSave_Country" ClientInstanceName="cbtnSave_States" runat="server"
                                                    AutoPostBack="False" Text="Save" CssClass="btn btn-primary">
                                                    <ClientSideEvents Click="function (s, e) {btnSave_Brand();}" />
                                                </dxe:ASPxButton>
                                            </div>
                                            <div style="">
                                                <dxe:ASPxButton ID="btnCancel_Country" runat="server" AutoPostBack="False" Text="Cancel" CssClass="btn btn-danger">
                                                    <ClientSideEvents Click="function (s, e) {fn_btnCancel();}" />
                                                </dxe:ASPxButton>
                                            </div>
                                            <br style="clear: both;" />
                                        </div>
                                        </td>
                                    </tr>
                                </table>                               
                            </div>
                            <div class="ContentDiv">
                                <div class="ScrollDiv"></div>
                                <br style="clear: both;" />
                                
                                <br style="clear: both;" />
                            </div>
                        </dxe:PopupControlContentControl>
                    </ContentCollection>
                    <HeaderStyle BackColor="LightGray" ForeColor="Black" />
                </dxe:ASPxPopupControl>
                <dxe:ASPxGridViewExporter ID="exporter" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
        </dxe:ASPxGridViewExporter>
            </div>

            <div class="HiddenFieldArea" style="display: none;">
                <asp:HiddenField runat="server"  ID="hdBrandId"/>
                
            </div>
        </div>
        </div>
    </div>
   
</asp:Content>

