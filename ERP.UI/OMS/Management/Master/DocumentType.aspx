<%--====================================================== Revision History ===============================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                14-02-2023        V2.0.39          Pallab              25656 : Master module design modification 
====================================================== Revision History ===================================================--%>

<%@ Page Title="Document Types" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" Inherits="ERP.OMS.Management.Master.management_master_DocumentType" CodeBehind="DocumentType.aspx.cs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/assests/css/ftsNewScreen.css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.1.5/css/fileinput.min.css" media="all" rel="stylesheet" type="text/css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.1.5/js/plugins/sortable.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/5.1.5/js/fileinput.min.js"></script>
    <%--<script src="http://malsup.github.com/jquery.form.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.form/4.3.0/jquery.form.js"></script>
    <style>
        .dxeErrorFrameSys.dxeErrorCellSys {
            position: absolute;
        }

        .dxflRequired_PlasticBlue {
            color: red;
            font-style: normal;
        }

        .modal-header {
        padding: 8px;
        background: #094e8c;
        border-radius: 8px 8px 0 0;
        color: #fff;
    }

    button.close {
        color: #fff;
        font-weight: 300;
        opacity: .5;
    }

    .close:hover, .close:focus {
        color: #fff;
        opacity: 1;
    }

    .modal-content {
        border-radius: 10px;
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

    .btn-sm, .btn-xs, .btn
    {
        padding: 7px 10px !important;
        height: 34px;
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

    @media only screen and (max-width: 768px) {
        .breadCumb {
            padding: 0 21%;
        }

        .breadCumb > span
        {
            padding: 9px 16px;
        }
        .form_main {
    overflow-x: hidden !important;
}
        .overflow-x-auto {
    overflow-x: auto !important;
    width: 300px !important;
}

        #DocumentGrid_DXPEForm_PW-1
        {
            width: 280px !important;
        }
        
    }
    </style>
    <script type="text/javascript">
        function ShowHideFilter(obj) {
            grid.PerformCallback(obj);
        }
        function EndCall(obj) {
            if (grid.cpDelmsg != null)
                alert(grid.cpDelmsg);
            grid.cpDelmsg = null;
        }

    </script>

    <script>
        function AddDoc(val, DocType) {
            // Rev Sanchita
            $("#FileDescription").val('');
            document.getElementById('Active').checked = 0;
            $("#fileupload").val('');
            // End of Rev Sanchita
             $("#UploadDocument").modal('show');
            $("#h1DocList").html(DocType);
            //$("#UploadDocumentList").modal('show');
            $("#hdnTypeId").val(val);
        }

        function AddDocument() {
            // Rev Sanchita
            $("#FileDescription").val('');
            document.getElementById('Active').checked = 0;
            $("#fileupload").val('');
            // End of Rev Sanchita
            $("#UploadDocument").modal('show');
        }


        
        function UploadDocument(values) {

            if ($("#FileDescription").val() == "") {
                jAlert("Please File Description.", "Alert", function () {
                    setTimeout(function () {
                        $("#FileDescription").focus();
                        return
                    }, 200);
                });
                return
            }


            var date1 = new Date("1970-01-01");
            var date2 = new Date();
            var Difference_In_Time = date2.getTime() - date1.getTime();
            var middle = (Math.round(Difference_In_Time / 1000) * 1155) + 1;

            var date = date2.getFullYear() + '-' + (date2.getMonth() + 1) + '-' + date2.getDate();
            var time = date2.getHours() + ":" + date2.getMinutes() + ":" + date2.getSeconds();
            var dateTime = date + 'T' + time;


        //    if (window.FormData !== undefined) {
        //        var fileUpload = $("#fileupload").get(0);
        //        var files = fileUpload.files;
        //        var fileData = new FormData();

        //        for (var i = 0; i < files.length; i++) {
        //            fileData.append('documents', files[i]);
        //        }
           
        //        var ATTACHMENT_CODE = "";
        //            if (fileUpload.files.length > 0) {
        //                ATTACHMENT_CODE = $("#hdnUserID").val() + '_Document_' + middle;
        //            }

        //        if (fileUpload.files.length > 0) {
        //            var isprocess = false;
        //            var extension = $("#fileupload").val().replace(/^.*\./, '');
        //            switch (extension.toLowerCase()) {
        //                case 'pdf':
        //                    isprocess = true;
        //                case 'doc':
        //                    isprocess = true;
        //                case 'docx':
        //                    isprocess = true;
        //                case 'ppt':
        //                    isprocess = true;
        //                case 'pptx':
        //                    isprocess = true;
        //                case 'xls':
        //                    isprocess = true;
        //                case 'xlsx':
        //                    isprocess = true;
        //                default:
        //                    // Cancel the form submission
        //                    if (!isprocess) {
        //                        jAlert('Invalid file.');
        //                        return false;
        //                        return
        //                    }
        //            }
        //            if ($("#fileupload").get(0).files[0].size / 2048 / 2048 > 1) {
        //                jAlert('File size upto 2 MB.');
        //                return false;
        //                return
        //            }
        //        }

                
        //        var data = {
        //            DOCUMENT_NAME: $("#FileDescription").val(),
        //            ATTACHMENT_CODE: ATTACHMENT_CODE,
        //            TYPE_ID: $("#hdnTypeId").val()
        //        }


        //        if (fileUpload.files.length > 0) {
        //            var model = {
        //                DOCUMENT_NAME: $("#FileDescription").val(),
        //                ATTACHMENT_CODE: ATTACHMENT_CODE,
        //                TYPE_ID: $("#hdnTypeId").val(),
        //                documents: fileUpload
        //            }
                   
        //        //  var url = "@Url.Action('DocumentType.aspx/UploadDoc')";
                  
        //            var model = new FormData();
        //            var i = 0;//selected file index
        //            //model.append("documents", files[0]);
        //            //model.append("data", JSON.stringify(data));

        //            //$.ajax({
        //            //    // and other parameter is set here
        //            //    url: "DocumentType.aspx/UploadDoc",
        //            //    type: "POST",
        //            //    data: model,
        //            //    dataType: "json",
        //            //    cache: false,
        //            //    contentType: false,
        //            //    processData: false
        //            //}).always(function (result) {
        //            //    if (result == 'OK') {
        //            //        jAlert("Party submitted successfully.");
        //            //    }
        //            //});
        //            var model = new FormData();
        //            model.append("DOCUMENT_NAME", $("#FileDescription").val());
        //            model.append("ATTACHMENT_CODE", ATTACHMENT_CODE);
        //            model.append("TYPE_ID", $("#hdnTypeId").val());
        //            model.append("documents", files[0]);

        //            $.ajax({
        //                type: "POST",
        //                url: "DocumentType.aspx/UploadDoc",
        //                data: model,
        //                contentType: false,
        //                dataType: false,
        //                processData: false,
        //                success: function (msg) {
        //                    var list = msg.d;

        //                }
        //            });
        //        }
        //}
        //else {
        //    jAlert("Please try again later!");
            //}
            var ATTACHMENT_CODE = '_Document_' + middle;

            var data = new FormData();
            var file = $('#fileupload')[0].files[0];
            data.append('documents', file);
            data.append("DOCUMENT_NAME", $("#FileDescription").val());
            data.append("ATTACHMENT_CODE", ATTACHMENT_CODE);
            data.append("TYPE_ID", $("#hdnTypeId").val());

            myfile = $('#fileupload').val();
            var ext = myfile.split('.').pop();
            // Rev Sanchita
            //console.log(file);
            // End of Rev Sanchita
            if ($('#fileupload')[0].files[0].size > 2048000) {
                alert("File is too big!");
                this.value = "";
                return false;
            };
            if (ext == "pdf" || ext == "docx" || ext == "doc" || ext == "ppt" || ext == "pptx" || ext == "xls" || ext == "xlsx") {
                $.ajax({
                    url: '../../../AttachedDocumentUpload/OrganizationDocUpload',
                    processData: false,
                    contentType: false,
                    data: data,
                    type: 'POST'
                }).done(function (result) {
                    // Rev Sanchita
                    // console.log(result);
                    // End of Rev Sanchita
                    if (result == "OK") {
                        $("#UploadDocument").modal('hide');
                    }
                    else {
                        jAlert(result);
                    }

                }).fail(function (a, b, c) { console.log(a, b, c); });
            } else {
               
            }
            return;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--rev 25249--%>
    <%--<div class="panel-heading">
        <div class="panel-title">
            <h3>Document Types</h3>
        </div>

    </div>--%>
    <div class="breadCumb">
        <span>Document Types</span>
    </div>
    <%--rev end 25249--%>
    <div class="container">
    <div class="backBox mt-5 p-3 ">
    <div class="form_main">
        <table class="TableMain100">
            <%-- <tr>
                <td class="EHEADER" style="text-align: center">
                    <strong><span style="color: #000099">Document Type</span></strong></td>
            </tr>--%>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="text-align: left; vertical-align: top">
                                <table style="margin-bottom: 15px !important;">
                                    <tr>
                                        <td id="ShowFilter">
                                            <% if (rights.CanAdd)
                                               { %>
                                            <a href="javascript:void(0);" onclick="grid.AddNewRow()" class="btn btn-success"><span>Add New</span> </a>
                                            <%} %>
                                            <% if (rights.CanExport)
                                               { %>
                                            <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-primary" OnChange="if(!AvailableExportOption()){return false;}" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Export to</asp:ListItem>
                                                <asp:ListItem Value="1">PDF</asp:ListItem>
                                                <asp:ListItem Value="2">XLS</asp:ListItem>
                                                <asp:ListItem Value="3">RTF</asp:ListItem>
                                                <asp:ListItem Value="4">CSV</asp:ListItem>
                                            </asp:DropDownList>
                                            <%} %>
                                            <%--<a href="javascript:ShowHideFilter('s');" class="btn btn-primary"><span>Show Filter</span></a>--%>
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
                    <div class="overflow-x-auto">
                        <dxe:ASPxGridView ID="DocumentGrid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" OnRowDeleting="DocumentGrid_RowDeleting"
                        DataSourceID="DocumentType" KeyFieldName="dty_id" Width="100%" OnHtmlEditFormCreated="DocumentGrid_HtmlEditFormCreated"
                        OnHtmlRowCreated="DocumentGrid_HtmlRowCreated" OnCustomCallback="DocumentGrid_CustomCallback" OnCommandButtonInitialize="DocumentGrid_CommandButtonInitialize">
                        <ClientSideEvents EndCallback="function(s, e) {EndCall(s.cpEND);}"></ClientSideEvents>
                        <Columns>
                            <dxe:GridViewDataTextColumn FieldName="dty_id" ReadOnly="True" Visible="False" Caption="ID" CellStyle-Font-Bold="true"
                                VisibleIndex="0" HeaderStyle-HorizontalAlign="Center" Width="10%">
                                <EditFormSettings Visible="False" />
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn Caption="Document Type" FieldName="dty_documentType"
                                VisibleIndex="0">
                                <EditCellStyle HorizontalAlign="Left" Wrap="False">
                                    <Paddings PaddingTop="15px" />
                                </EditCellStyle>
                                <CellStyle Wrap="False">
                                </CellStyle>
                                <PropertiesTextEdit Width="280px" MaxLength="50">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                        <RequiredField IsRequired="True" ErrorText="Mandatory" />
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                                <EditFormCaptionStyle Wrap="False" HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Caption="Document Type" Visible="True" />
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataComboBoxColumn FieldName="dty_applicableFor" Caption="Applicable For"
                                VisibleIndex="1">
                                <PropertiesComboBox ValueType="System.String" EnableSynchronization="False" EnableIncrementalFiltering="True"
                                    Width="280px">
                                    <Items>
                                        <%--Rev Sanchita--%>
                                        <%--<dxe:ListEditItem Text="For Organization" Value="For Organization"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="For Own" Value="For Own"></dxe:ListEditItem>--%>
                                        <dxe:ListEditItem Text="For Organization" Value="IsForOrganization"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="For Own" Value="IsForOwn"></dxe:ListEditItem>
                                        <%--End of Rev Sanchita--%>
                                        <%--<dxe:ListEditItem Text="APP Only" Value="APP Only"></dxe:ListEditItem>--%>
                                        <%--<dxe:ListEditItem Text="Customer/Client" Value="Customer/Client"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Product" Value="Product"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Employee" Value="Employee"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Franchisees" Value="Franchisees"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Relationship Partner" Value="Relationship Partner"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Business Partner" Value="Partner"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Recruitment Agents" Value="Recruitment Agent"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Branches" Value="Branches"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Companies" Value="Companies"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Building" Value="Building"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Outsourcing Companies" Value="Outsourcing Companies"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Vendors / Service Providers" Value="HRrecruitmentagent"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Candidate" Value="Candidate"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Account Heads" Value="Account Heads"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Other Entity" Value="Other Entity"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Lead" Value="Lead"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Agents" Value="Agents"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Quotation" Value="Quotation"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Sales Order" Value="SalesOrder"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Purchase Invoice" Value="PurchaseInvoice"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Transit Purchase Invoice" Value="TransitPurchaseInvoice"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Sales Return" Value="SalesReturn"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Purchase Return" Value="PurchaseReturn"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Sale Challan" Value="SaleChallan"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Sale Return" Value="SaleReturn"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Sale Invoice" Value="SaleInvoice"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Transit Sale Invoice" Value="TransitSalesInvoice"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Customer Receipt/Payment" Value="CustomerReceiptPayment"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Vendor Payment/Receipt" Value="VendorPaymentReceipt"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Customer Debit/Credit Note" Value="CustomerDebitCreditNote"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Vendor Debit/Credit Note" Value="VendorDebitCreditNote"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Purchase Indent" Value="PurchaseIndent"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Branch Requisition" Value="BranchRequisition"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Purchase Order" Value="PurchaseOrder"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Purchase Challan" Value="PurchaseChallan"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Branch Tranfer Out" Value="BTO"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Branch Tranfer In" Value="BTI"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Service Centre In" Value="ServiceCentreIn"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Customer Return" Value="CustomerReturn"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Issue To Return" Value="IssueToReturn"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Financer" Value="Financer"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Transporter" Value="Transporter"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Manual Sale Return" Value="ManualSaleReturn"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Normal Sale Return" Value="NormalSaleReturn"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Undelivery Return" Value="UndeliveryReturn"></dxe:ListEditItem>
                                        <dxe:ListEditItem Text="Purchase Return Manual" Value="PurchaseReturnManual"></dxe:ListEditItem>--%>
                                    </Items>
                                </PropertiesComboBox>
                                <EditFormSettings Visible="True" Caption="Applicable For"></EditFormSettings>
                                <EditCellStyle HorizontalAlign="Left" Wrap="False">
                                    <Paddings PaddingTop="15px" />
                                </EditCellStyle>
                                <CellStyle Wrap="False">
                                </CellStyle>
                                <EditFormCaptionStyle Wrap="False" HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                            </dxe:GridViewDataComboBoxColumn>

                            <dxe:GridViewDataTextColumn FieldName="Mandatory1" Caption="Show this type?" VisibleIndex="2" Width="120px">
                                <EditFormSettings Visible="False" />
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataCheckColumn Caption="Show this type?" FieldName="Mandatory"
                                VisibleIndex="2" Visible="False">
                                <PropertiesCheckEdit ValueChecked="1" ValueType="System.String" ValueUnchecked="0">
                                    <Style HorizontalAlign="Left"></Style>
                                </PropertiesCheckEdit>
                                <EditFormSettings Visible="True" />
                                <EditFormCaptionStyle Wrap="False" ForeColor="Red">
                                </EditFormCaptionStyle>
                                <CellStyle ForeColor="Red">
                                </CellStyle>
                            </dxe:GridViewDataCheckColumn>


                            <dxe:GridViewCommandColumn VisibleIndex="3" ShowEditButton="true" ShowDeleteButton="true" Width="10%">
                                <%--<DeleteButton Visible="True">
                                </DeleteButton>
                                <EditButton Visible="True">
                                </EditButton>--%>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    Actions
                                    <%--<%if (Session["PageAccess"].ToString().Trim() == "All" || Session["PageAccess"].ToString().Trim() == "Add" || Session["PageAccess"].ToString().Trim() == "DelAdd")
                                      { %>
                                    <a href="javascript:void(0);" onclick="grid.AddNewRow()"><span style="text-decoration: underline">Add New</span> </a>
                                    <%} %>--%>
                                </HeaderTemplate>
                            </dxe:GridViewCommandColumn>

                            <dxe:GridViewDataTextColumn HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="center" VisibleIndex="17" Width="100px">
                                <DataItemTemplate>
                                    <a style='cursor: pointer' data-toggle='tooltip' title='Add Content' class='pad' onclick="AddDoc('<%#Eval("dty_id") %>','<%#Eval("dty_documentType") %>')">
                                        <img src='../../../assests/images/Add.png' style=' margin-right: 3px' /></a>
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                                <HeaderTemplate><span>Actions</span></HeaderTemplate>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                        </Columns>
                        <SettingsCommandButton>
                            <EditButton Image-Url="../../../assests/images/Edit.png" ButtonType="Image" Image-AlternateText="Edit" Styles-Style-CssClass="pad">
                            </EditButton>
                            <DeleteButton Image-Url="../../../assests/images/Delete.png" ButtonType="Image" Image-AlternateText="Delete">
                            </DeleteButton>

                            <UpdateButton Text="Save" ButtonType="Button" Styles-Style-CssClass="btn btn-primary "></UpdateButton>
                            <CancelButton Text="Cancel" ButtonType="Button" Styles-Style-CssClass="btn btn-danger"></CancelButton>
                        </SettingsCommandButton>
                        <Styles>
                            <LoadingPanel ImageSpacing="10px">
                            </LoadingPanel>
                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                            </Header>
                            <Cell CssClass="gridcellleft">
                            </Cell>
                        </Styles>
                        <SettingsSearchPanel Visible="True" />
                        <Settings ShowStatusBar="Visible" ShowGroupPanel="True" ShowFilterRow="true" ShowFilterRowMenu="true" />
                        <SettingsText PopupEditFormCaption="Add/Modify Document" />
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
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 5%"></td>
                                        <td style="width: 75%">
                                            <%--<controls></controls>--%>
                                            <dxe:ASPxGridViewTemplateReplacement ID="Editors" runat="server" ColumnID="" ReplacementType="EditFormEditors"></dxe:ASPxGridViewTemplateReplacement>
                                            <div style="padding: 2px 2px 2px 121px">
                                                <dxe:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                    runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                                <dxe:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                    runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                            </div>
                                        </td>
                                        <td style="width: 20%"></td>
                                    </tr>
                                </table>
                            </EditForm>
                        </Templates>

                    </dxe:ASPxGridView>
                    </div>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="DocumentType" runat="server" ConflictDetection="CompareAllValues"
            DeleteCommand="DELETE FROM [tbl_master_documentType] WHERE [dty_id] = @original_dty_id"
            InsertCommand="INSERT INTO [tbl_master_documentType] ([dty_documentType], [dty_applicableFor],[dty_SearchBy],[dty_mandatory], [CreateDate], [CreateUser]) VALUES (@dty_documentType, @dty_applicableFor,0,@Mandatory,getdate(),@CreateUser)"
            OldValuesParameterFormatString="original_{0}"
            SelectCommand="SELECT [dty_id],[dty_documentType],(case when [dty_applicableFor]='IsForOrganization' then 'For Organization' when [dty_applicableFor]='IsForOwn' then 'For Own' else [dty_applicableFor] end) [dty_applicableFor],dty_mandatory as Mandatory,case dty_mandatory when '1' then 'Yes' else 'No' end as Mandatory1 FROM [tbl_master_documentType]"
            UpdateCommand="UPDATE [tbl_master_documentType] SET [dty_documentType] = @dty_documentType, [dty_applicableFor] = @dty_applicableFor,dty_mandatory=@Mandatory, [LastModifyDate] = getdate(), [LastModifyUser] = @CreateUser WHERE [dty_id] = @original_dty_id">
            <DeleteParameters>
                <asp:Parameter Name="original_dty_id" Type="Decimal" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="dty_documentType" Type="String" />
                <asp:Parameter Name="dty_applicableFor" Type="String" />
                <asp:Parameter Name="Mandatory" Type="String" />
                <asp:SessionParameter Name="CreateUser" Type="Decimal" SessionField="userid" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="dty_documentType" Type="String" />
                <asp:Parameter Name="dty_applicableFor" Type="String" />
                <asp:Parameter Name="Mandatory" Type="String" />
                <asp:SessionParameter Name="CreateUser" Type="Decimal" SessionField="userid" />
            </InsertParameters>
        </asp:SqlDataSource>
        <dxe:ASPxGridViewExporter ID="exporter" runat="server" Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true">
        </dxe:ASPxGridViewExporter>
        <br />
    </div>
    </div>
    </div>



    <div class="modal pmsModal fade w90" id="UploadDocument">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Upload New File</h4>
                </div>
                <div class="modal-body" style="padding: 12px;">

                    <div class="form-group hide">
                        <label>Name:</label>
                        <div class="custom-file">
                            <input type="text" id="FileName" name="FileName" class="form-control" />
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="form-group">
                                <label>Description:</label>
                                <div class="custom-file">
                                    <input type="text" id="FileDescription" name="FileDescription" maxlength="250" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group" style="padding-top: 21px;">
                                <div class="custom-file">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" id="Active" value="true" name="Active">
                                            Tick to mark Active
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Choose File:</label>
                                <div class="clearfix">
                                    <div class="custom-file">
                                        <input type="file" id="fileupload" name="fileupload" class="custom-file-input" accept=".doc,.docx,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document,.pdf,.pptx,.ppt,.xls,xlsx" />

                                    </div>
                                </div>
                            </div>
                            <p class="text-muted red" style="color: red">Supported types : PDF,DOC,DOCX,PPT,PPTX,XLS and XLSX (File size limited to 2MB)</p>
                        </div>

                    </div>

                  <%--  <div class="progress">
                        <div class="bar"></div>
                        <div class="percent">0%</div>
                    </div>--%>

                    <div id="status"></div>
                    <input type="hidden" id="hdnAddEdit" name="hdnAddEdit" />
                    <input type="hidden" id="hdnTypeId" name="hdnTypeId" />


                </div>
                <div class="modal-footer">
                    <input type="button" onclick="UploadDocument()" id="btnUpload" class="btn btn-info" value="Upload" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>




    <div class="modal pmsModal fade w90" id="UploadDocumentList">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Upload New File</h4>
                </div>
                <div class="form_main backBox px-3 clearfix">
                    <h1 class="text-center mHeader" id="h1DocList"></h1>
                    <div class="row pt-3">
                        <button style="margin-left: 27px; margin-bottom: 10px;" type="button" onclick="AddDocument()" class="btn btn-success" data-toggle="modal" data-target="#UploadDocument">
                            <i class="fa fa-plus-circle"></i>Add New
                        </button>
                    </div>

                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-header bg-danger text-white">
                            </div>
                            <div class="card-body">
                                <div class="row">
                                <%--@foreach (var item in Model)
                                {
                                if (item.Filetype.ToUpper() == ".DOC" || item.Filetype.ToUpper() == ".DOCX")
                                {--%>
                                    <div class="col-sm-4 col-md-4 col-xs-12">
                                        <div class="title">@item.Name</div>
                                        <div class="video-frame">
                                            <div class="butHolder">
                                                <span onclick="editNode('@item.ID','@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)','@item.FileDescription','@item.IsActive')">
                                                    <img src="../../../assests/images/edit.png" />
                                                    Edit</span>
                                                <span onclick="deleteNode('@item.ID', '@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)')">
                                                    <img src="../../../assests/images/button.png" />
                                                    Delete</span>
                                                <span onclick="addUserToNode('@item.ID', ' @Url.Content(@item.FilePath) ')">
                                                    <img src="../../../assests/images/share.png" />
                                                    Share</span>

                                            </div>
                                            <a href="@Url.Content(@item.FilePath)">
                                                <img src="../../../assests/images/Word-Logo.png" alt="Download Excel" height="200" width="200" class="center" />
                                            </a>

                                        </div>
                                        <div class="doc-description">@item.FileDescription</div>
                                    </div>
                                    <%-- }
                                else if (item.Filetype.ToUpper() == ".PPT" || item.Filetype.ToUpper() == ".PPTX")
                                {--%>
                                    <div class="col-sm-4 col-md-4 col-xs-12">
                                        <div class="title">@item.Name</div>
                                        <div class="video-frame">
                                            <div class="butHolder">
                                                <span onclick="editNode('@item.ID','@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)','@item.FileDescription','@item.IsActive')">
                                                    <img src="../../../assests/images/2edit.png" />
                                                    Edit</span>
                                                <span onclick="deleteNode('@item.ID', '@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)')">
                                                    <img src="../../../assests/images/3button.png" />
                                                    Delete</span>
                                                <span onclick="addUserToNode('@item.ID', ' @Url.Content(@item.FilePath) ')">
                                                    <img src="../../../assests/images/1share.png" />
                                                    Share</span>

                                            </div>
                                            <a href="@Url.Content(@item.FilePath)">
                                                <img src="../../../assests/images/PowerPoint.png" alt="Download Excel" height="200" width="200" class="center" />
                                            </a>

                                        </div>
                                        <div class="doc-description">@item.FileDescription</div>
                                    </div>
                                    <%-- }
                                else if (item.Filetype.ToUpper() == ".PDF")
                                {--%>
                                    <div class="col-sm-4 col-md-4 col-xs-12">
                                        <div class="title">@item.Name</div>
                                        <div class="video-frame">
                                            <div class="butHolder">
                                                <span onclick="editNode('@item.ID','@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)','@item.FileDescription','@item.IsActive')">
                                                    <img src="../../../assests/images/2edit.png" />
                                                    Edit</span>
                                                <span onclick="deleteNode('@item.ID', '@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)')">
                                                    <img src="../../../assests/images/3button.png" />
                                                    Delete</span>
                                                <span onclick="addUserToNode('@item.ID', ' @Url.Content(@item.FilePath) ')">
                                                    <img src="../../../assests/images/1share.png" />
                                                    Share</span>

                                            </div>
                                            <a href="@Url.Content(@item.FilePath)">
                                                <iframe src="@Url.Content(@item.FilePath)" style="width: 100%"></iframe>
                                            </a>
                                        </div>
                                        <div class="doc-description">@item.FileDescription</div>
                                    </div>
                                    <%--  }
                                else if (item.Filetype.ToUpper() == ".SQL" || item.Filetype.ToUpper() == ".TXT")
                                {--%>
                                    <div class="col-sm-4 col-md-4 col-xs-12">
                                        <div class="title">@item.Name</div>
                                        <div class="video-frame">
                                            <div class="butHolder">
                                                <span onclick="editNode('@item.ID','@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)','@item.FileDescription','@item.IsActive')">
                                                    <img src="../../../assests/images/2edit.png" />
                                                    Edit</span>
                                                <span onclick="deleteNode('@item.ID', '@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)')">
                                                    <img src="../../../assests/images/3button.png" />
                                                    Delete</span>
                                                <span onclick="addUserToNode('@item.ID', ' @Url.Content(@item.FilePath) ')">
                                                    <img src="../../../assests/images/1share.png" />
                                                    Share</span>
                                            </div>
                                            <object data="@Url.Content(@item.FilePath)" width="300" height="200"></object>
                                        </div>
                                        <div class="doc-description">@item.FileDescription</div>
                                    </div>
                                    <%-- }

                                else if (item.Filetype.ToUpper() == ".XLS" || item.Filetype.ToUpper() == ".CSV" || item.Filetype.ToUpper() == ".XLSX")
                                {--%>
                                    <div class="col-sm-4 col-md-4 col-xs-12">
                                        <div class="title">@item.Name</div>
                                        <div class="video-frame">
                                            <div class="butHolder">
                                                <span onclick="editNode('@item.ID','@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)','@item.FileDescription','@item.IsActive')">
                                                    <img src="../../../assests/images/2edit.png" />
                                                    Edit</span>
                                                <span onclick="deleteNode('@item.ID', '@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)')">
                                                    <img src="../../../assests/images/3button.png" />
                                                    Delete</span>
                                                <span onclick="addUserToNode('@item.ID', ' @Url.Content(@item.FilePath) ')">
                                                    <img src="../../../assests/images/1share.png" />
                                                    Share</span>
                                            </div>
                                            <a href="@Url.Content(@item.FilePath)">
                                                <img src="../../../assests/images/excel-logo.png" alt="Download Excel" height="200" width="200" class="center" />
                                            </a>

                                        </div>
                                        <div class="doc-description">@item.FileDescription</div>
                                    </div>
                                    <%-- }
                                else if (item.Filetype.ToUpper() == ".JPG" || item.Filetype.ToUpper() == ".JPEG" || item.Filetype.ToUpper() == ".PNG")
                                {--%>
                                    <div class="col-sm-4 col-md-4 col-xs-12">
                                        <div class="title">@item.Name</div>
                                        <div class="video-frame">
                                            <div class="butHolder">
                                                <span onclick="editNode('@item.ID','@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)','@item.FileDescription','@item.IsActive')">
                                                    <img src="../../../assests/images/2edit.png" />
                                                    Edit</span>
                                                <span onclick="deleteNode('@item.ID', '@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)')">
                                                    <img src="../../../assests/images/3button.png" />
                                                    Delete</span>
                                                <span onclick="addUserToNode('@item.ID', ' @Url.Content(@item.FilePath) ')">
                                                    <img src="../../../assests/images/1share.png" />
                                                    Share</span>
                                            </div>
                                            <a class='example-image-link' href="@Url.Content(@item.FilePath)" data-lightbox='example-1'>
                                                <img src="@Url.Content(@item.FilePath)" alt="Download Excel" height="180" width="250" class="center" />
                                            </a>
                                        </div>
                                        <div class="doc-description">@item.FileDescription</div>
                                    </div>
                                    <%--  }
                                else
                                {--%>
                                    <div class="col-sm-4 col-md-4 col-xs-12">
                                        <div class="title">@item.Name</div>
                                        <div class="video-frame">
                                            <div class="butHolder">
                                                <span onclick="editNode('@item.ID','@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)','@item.FileDescription','@item.IsActive')">
                                                    <img src="../../../assests/images/2edit.png" />
                                                    Edit</span>
                                                <span onclick="deleteNode('@item.ID', '@Url.Content(@item.FilePath)','@Url.Content(@item.FilePathIcon)')">
                                                    <img src="../../../assests/images/3button.png" />
                                                    Delete</span>
                                                <span onclick="addUserToNode('@item.ID', ' @Url.Content(@item.FilePath) ')">
                                                    <img src="../../../assests/images/1share.png" />
                                                    Share</span>

                                            </div>
                                        </div>
                                    </div>
                                    <%--  }
                            }--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

