<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                13-02-2023        2.0.39           Pallab              25656 : Master module design modification 
====================================================== Revision History ==========================================================--%>

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

            /*Rev 1.0*/

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
        bottom: 9px;
        right: 14px;
        z-index: 0;
        cursor: pointer;
    }

    .date-select .form-control {
        position: relative;
        z-index: 1;
        background: transparent;
    }

    #ddlState, #ddlPartyType, #divoutletStatus, #slmonth, #slyear {
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
            top: 33px;
            right: 13px;
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
            line-height: 19px;
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
    .dxeEditArea_PlasticBlue,
    .dxgvControl_PlasticBlue, .dxgvDisabled_PlasticBlue{
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

    #dtFrom, #dtTo, #FormDate, #toDate {
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

    .modal-header {
        background: #094e8c !important;
        background-image: none !important;
        padding: 11px 20px;
        border: none;
        border-radius: 5px 5px 0 0;
        color: #fff;
        border-radius: 10px 10px 0 0;
    }

    .modal-content {
        border: none;
        border-radius: 10px;
    }

    .modal-header .modal-title {
        font-size: 14px;
    }

    .close {
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

    #dtFrom_B-1, #dtTo_B-1 , #cmbDOJ_B-1, #cmbLeaveEff_B-1, #FormDate_B-1, #toDate_B-1 {
        background: transparent !important;
        border: none;
        width: 30px;
        padding: 10px !important;
    }

        #dtFrom_B-1 #dtFrom_B-1Img,
        #dtTo_B-1 #dtTo_B-1Img , #cmbDOJ_B-1 #cmbDOJ_B-1Img, #cmbLeaveEff_B-1 #cmbLeaveEff_B-1Img, #FormDate_B-1 #FormDate_B-1Img, #toDate_B-1 #toDate_B-1Img {
            display: none;
        }

    #FormDate_I, #toDate_I {
        background: transparent;
    }

    .for-cust-icon {
        position: relative;
        /*z-index: 1;*/
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
            height: 34px;
            border-radius: 4px;
    }

    #cmbDOJ_DDD_PW-1
    {
        z-index: 9999 !important;
    }

    #cmbDOJ, #cmbLeaveEff
    {
        position: relative;
    z-index: 1;
    background: transparent;
    }

    .btn-sm, .btn-xs
    {
        padding: 7px 10px !important;
    }

    #productAttributePopUp_PWH-1 span, #ASPxPopupControl2_PWH-1 span
    {
        color: #fff !important;
    }

    #marketsGrid_DXPEForm_tcefnew, .dxgvPopupEditForm_PlasticBlue
    {
        height: 220px !important;
    }
/*Rev end 1.0*/
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

