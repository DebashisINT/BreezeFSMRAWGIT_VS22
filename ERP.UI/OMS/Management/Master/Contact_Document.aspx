<%--====================================================== Revision History ==============================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                23-02-2023        2.0.39           Pallab              25656 : Master module design modification 
====================================================== Revision History ==================================================--%>

<%@ Page Language="C#" AutoEventWireup="true" Title="Document"
    Inherits="ERP.OMS.Management.Master.management_Master_Contact_Document" CodeBehind="Contact_Document.aspx.cs" MasterPageFile="~/OMS/MasterPage/ERP.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">
        //function SignOff() {
        //window.parent.SignOff()
        //}
        function Show() {
            var url = "";
            var isRedirect = document.getElementById("IsredirectedBranch").value;

            if (isRedirect == "1") {
                url = "frmAddDocuments.aspx?id=Contact_Document.aspx&id1=<%=Session["requesttype"]%>&AcType=Add&Page=branch";
            }
            else {
                url = "frmAddDocuments.aspx?id=Contact_Document.aspx&id1=<%=Session["requesttype"]%>&AcType=Add";
            }
           
            popup.SetContentUrl(url);
            //alert (url);
            popup.Show();

        }
        function PopulateGrid(obj) {
            gridDocument.PerformCallback(obj);
        }
        function Changestatus(obj) {
            var URL = "../verify_documentremarks.aspx?id=" + obj;
            window.location.href = URL;
            //editwin = dhtmlmodal.open("Editbox", "iframe", URL, "Verify Remarks", "width=995px,height=300px,center=0,resize=1,top=-1", "recal");
            //editwin.onclose = function () {
            //    gridDocument.PerformCallback();
            //}
        }
        function ShowEditForm(KeyValue) {
            var isRedirect = document.getElementById("IsredirectedBranch").value;
            var url = "";
            if (isRedirect == "1") {               
                url = 'frmAddDocuments.aspx?id=Contact_Document.aspx&id1=<%=Session["requesttype"]%>&AcType=Edit&Page=branch&docid=' + KeyValue;
            }
            else {
                url = 'frmAddDocuments.aspx?id=Contact_Document.aspx&id1=<%=Session["requesttype"]%>&AcType=Edit&docid=' + KeyValue;
            }
            
            popup.SetContentUrl(url);
            
            popup.Show();
        }
        function disp_prompt(name) {
         
            //var ID = document.getElementById(txtID);
            if (name == "tab0") {
                //alert(name);
                //document.location.href = "rootcompany_general.aspx";//--//"Contact_general.aspx"; 
                if ("<%=Session["requesttype"]%>" == "Branches") {
                    document.location.href = "BranchAddEdit.aspx?id=<%=Session["con_branch"]%>";
                }
                else if("<%=Session["requesttype"]%>" == "Building/Warehouses")
                {
                    var qString =window.location.href.split("=")[1];
                    document.location.href = "RootBuildingInsertUpdate.aspx?id=" + qString;
                }
                else
                {
                    document.location.href = "Contact_general.aspx";
                }
            }
            if (name == "tab1") {

                var isRedirect = document.getElementById("IsredirectedBranch").value;
                
                if (isRedirect == "1") {
                    document.location.href = "Branch_Correspondance.aspx?Page=branch";
                }
                else {
                    document.location.href = "Contact_Correspondence.aspx";
                }
            }
            else if (name == "tab2") {
                //alert(name);
                document.location.href = "Contact_BankDetails.aspx";
            }
            else if (name == "tab3") {
                //alert(name);
                document.location.href = "Contact_DPDetails.aspx";
            }
            else if (name == "tab4") {
                //alert(name);
                //document.location.href="Contact_Document.aspx"; 
            }
            else if (name == "tab12") {
                //alert(name);
                document.location.href = "Contact_FamilyMembers.aspx";
            }
            else if (name == "tab5") {
                //alert(name);
                document.location.href = "Contact_Registration.aspx";
            }
            else if (name == "tab7") {
                //alert(name);
                document.location.href = "Contact_GroupMember.aspx";
            }
            else if (name == "tab8") {
                //alert(name);
                document.location.href = "Contact_Deposit.aspx";
            }
            else if (name == "tab9") {
              <% if (Session["requesttype"] == "Branches")
               { %>
                document.location.href = "frm_BranchUdf.aspx";
                <%    }else {%>
                document.location.href = "Contact_Remarks.aspx";
            <% }%>
            
            }
            else if (name == "tab10") {
                //alert(name);
                document.location.href = "Contact_Education.aspx";
            }
            else if (name == "tab11") {
                //alert(name);
                document.location.href = "contact_brokerage.aspx";
            }
            else if (name == "tab6") {
                //alert(name);
                document.location.href = "contact_other.aspx";
            }
            else if (name == "tab13") {
                document.location.href = "contact_Subscription.aspx";
            }

        }
        function OnDocumentView(obj1, obj2) {
            var docid = obj1;
            var filename;
            var chk = obj2.includes("~");
            if (chk) {
                filename = obj2.split('~')[1];
            }
            else {
                filename = obj2.split('/')[2];
            }
            if (filename != '' && filename != null) {
                var d = new Date();
                var n = d.getFullYear();
                var url = '\\OMS\\Management\\Documents\\' + docid + '\\' + n + '\\' + filename;
                //window.open(url, '_blank');
                var seturl = '\\OMS\\Management\\DailyTask\\viewImage.aspx?id=' + url;
                popup.contentUrl = url;
                popup.Show();
            }
            else {
                jAlert('File not found.')
            }

           

        }

        function DeleteRow(keyValue) {
           
            jConfirm('Confirm delete?', 'Confirmation Dialog', function (r) {
                if (r == true) {
                    gridDocument.PerformCallback('Delete~' + keyValue);
                    height();
                }
            });


        }

        function crossbtn_click() {
            if ("<%=Session["requesttype"]%>" == "Building/Warehouses") {
                document.location.href = "RootBuilding.aspx";
            }
            else {
                document.location.href = "Branch.aspx";
            }

        }
    </script>
    <style>
        .mainWraper > div {
            padding-bottom: 22px;
        }
        span.dx-vam, span.dx-vat, span.dx-vab, a.dx-vam, a.dx-vat, a.dx-vab
        {
                font-size: 15px;
        }
        .dxtcLite_PlasticBlue > .dxtc-stripContainer .dxtc-activeTab {
            background: #094e8c;
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
        bottom: 8px;
        right: 20px;
        z-index: 0;
        cursor: pointer;
    }

    .date-select .form-control {
        position: relative;
        z-index: 1;
        background: transparent;
    }

    #ddlState, #ddlPartyType, #divoutletStatus, #slmonth, #slyear , #txtCINVdate {
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
            top: 42px;
            right: 12px;
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
            line-height: 18px;
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
    .dxeEditArea_PlasticBlue {
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

    #dtFrom, #dtTo {
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

    .pmsModal .modal-header {
        background: #094e8c !important;
        background-image: none !important;
        padding: 11px 20px;
        border: none;
        border-radius: 5px 5px 0 0;
        color: #fff;
        border-radius: 10px 10px 0 0;
    }

    .pmsModal .modal-content {
        border: none;
        border-radius: 10px;
    }

    .pmsModal .modal-header .modal-title {
        font-size: 14px;
    }

    .pmsModal .close {
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

    #dtFrom_B-1, #dtTo_B-1 , #txtCINVdate_B-1 {
        background: transparent !important;
        border: none;
        width: 30px;
        padding: 10px !important;
    }

        #dtFrom_B-1 #dtFrom_B-1Img,
        #dtTo_B-1 #dtTo_B-1Img,
        #txtCINVdate_B-1 #txtCINVdate_B-1Img{
            display: none;
        }

    #dtFrom_I, #dtTo_I {
        background: transparent;
    }

    .for-cust-icon {
        position: relative;
        z-index: 1;
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
            height: 32px;
            border-radius: 4px;
    }

    /*Rev end 1.0*/
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="breadCumb">
         
            <span>
                <asp:Label ID="lblHeadTitle" runat="server"></asp:Label>
            </span>
            <%--<div class="crossBtn"><a href="<%=Session["PrePageRedirect"] %>"><i class="fa fa-times"></i></a></div>--%>

            <%--..........................Code Commented and Modified by sam on 03102016............................--%>
            <%-- <div class="crossBtn"><a href="frmContactMain.aspx?requesttype=<%= Session["Contactrequesttype"] %>""><i class="fa fa-times"></i></a></div>--%>

            <%--    <div class="crossBtn"><a href="Branch.aspx"><i class="fa fa-times"></i></a></div>--%>
            <%--   <div class="crossBtn"><a id="crossbtn" onclick="crossbtn_click()"><i class="fa fa-times"></i></a></div>--%>


            <% if (Session["requesttype"] == "Branches")
               { %>
            <div class="crossBtnN"><a href="Branch.aspx?requesttype=Branches"><i class="fa fa-times"></i></a></div>
            <% }
               else if (Session["requesttype"] == "Building/Warehouses")
               { %>
        
             <div class="crossBtnN"><a href="RootBuilding.aspx"><i class="fa fa-times"></i></a></div>
            <% }
               else if (Session["requesttype"] == "Account Heads")
               { %>
          <div class="crossBtnN"><a href="MainAccountHead.aspx"><i class="fa fa-times"></i></a></div>
            <% }
               
               else if (Session["requesttype"] == "Sub Ledger")
               { %>
            
          <div class="crossBtnN"><a href="<%= Convert.ToString(Session["redirct"]) %>"><i class="fa fa-times"></i></a></div>
            <% }
              
               
                else 
               { %>
            <div class="crossBtnN"><a href="frmContactMain.aspx?requesttype=<%= Session["Contactrequesttype"] %>"><i class="fa fa-times"></i></a></div>
             <% }
         %>

        
        </div>
    <div class="container mt-5">
        <div class="backBox p-3 ">
        <table width="100%">
            <tr>
                <td class="EHEADER" style="text-align: center">
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TableMain100">
            <tr>
                <td>
                    <dxe:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="4" ClientInstanceName="page">
                        <TabPages>

                            <dxe:TabPage Text="General" Name="General">
                             
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="CorresPondence" Text="Correspondence">
                               
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="Bank Details" Text="Bank">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="DP Details" Visible="false" Text="DP">

                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Text="Documents" Name="Documents">
                              
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">

                                        <div style="float: left">
                                            <% if (rights.CanAdd)
                                               { %><a href="javascript:void(0);" class="btn btn-primary" onclick="Show();"><span>Add New</span> </a><% } %>
                                        </div>
                                        <dxe:ASPxGridView ID="EmployeeDocumentGrid" runat="server" AutoGenerateColumns="False"
                                            ClientInstanceName="gridDocument" KeyFieldName="Id" Width="100%" Font-Size="12px"
                                            OnCustomCallback="EmployeeDocumentGrid_CustomCallback" OnHtmlRowCreated="EmployeeDocumentGrid_HtmlRowCreated" OnRowCommand="EmployeeDocumentGrid_RowCommand">
                                            <SettingsSearchPanel Visible="True" />
                                            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowGroupPanel="true" ShowStatusBar="Visible" />
                                            <SettingsPager>
                                                <PageSizeItemSettings Visible="true" />
                                            </SettingsPager>
                                            <Columns>
                                                <dxe:GridViewDataTextColumn FieldName="Id" ReadOnly="True" VisibleIndex="0" Visible="False">
                                                    <EditFormSettings Visible="False" />
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="Type" VisibleIndex="0" Caption="Doc. Type">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="FileName" VisibleIndex="1" Caption="Doc. Name">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="Src" VisibleIndex="2" Visible="False">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="Note1" VisibleIndex="3" Visible="true" Caption="Note1">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="Note2" VisibleIndex="4" Visible="true" Caption="Note2">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="Fileno" VisibleIndex="5" Visible="true" Caption="Number">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="Bldng" VisibleIndex="6" Visible="true" Caption="Building">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="FilePath" ReadOnly="True" VisibleIndex="7"
                                                    Caption="Location">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="createuser" ReadOnly="True" VisibleIndex="8"
                                                    Caption="Upload By">
                                                </dxe:GridViewDataTextColumn>                                               
                                                <dxe:GridViewDataTextColumn Caption="Verified By" FieldName="vrfy" ReadOnly="True"
                                                    VisibleIndex="9">
                                                    <EditFormSettings Visible="False" />
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="ReceiveDate" VisibleIndex="10" Visible="true">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="RenewDate" VisibleIndex="11" Visible="true">
                                                </dxe:GridViewDataTextColumn>
                                               
                                                <dxe:GridViewDataTextColumn VisibleIndex="13" Width="8%" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>Action</HeaderTemplate>
                                                    <CellStyle CssClass="gridcellleft">
                                                    </CellStyle>
                                                    <DataItemTemplate>
                                                        <% if (rights.CanView)
                                                           { %>
                                                        <a onclick="OnDocumentView('<%#Eval("doc") %>','<%#Eval("Src") %>')" style="text-decoration: none; cursor: pointer;" title="View" class="pad">
                                                            <img src="../../../assests/images/viewIcon.png" />
                                                        </a><% } %>
                                                       
                                                        <% if (rights.CanEdit)
                                                           { %>
                                                        <a href="javascript:void(0);" onclick="ShowEditForm('<%# Container.KeyValue %>');" style="text-decoration: none;" title="Edit" class="pad">
                                                            <img src="../../../assests/images/Edit.png" />
                                                        </a><% } %>
                                                        <% if (rights.CanDelete)
                                                           { %>
                                                        <a href="javascript:void(0);" onclick="DeleteRow('<%# Container.KeyValue %>')" style="text-decoration: none;" title="Delete">
                                                            <img src="../../../assests/images/Delete.png" />
                                                        </a><% } %>
                                                    </DataItemTemplate>
                                                   
                                                    <EditFormSettings Visible="False"></EditFormSettings>
                                                </dxe:GridViewDataTextColumn>
                                               
                                            </Columns>
                                            <Settings ShowGroupPanel="True" ShowStatusBar="Visible" />
                                            <SettingsEditing Mode="PopupEditForm" PopupEditFormHeight="2" PopupEditFormHorizontalAlign="Center"
                                                PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" PopupEditFormWidth="700px"
                                                EditFormColumnCount="1" />
                                            <Styles>
                                                <LoadingPanel ImageSpacing="10px">
                                                </LoadingPanel>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                            <SettingsText PopupEditFormCaption="Add/Modify Documents" ConfirmDelete="Confirm delete?" />
                                            <SettingsPager NumericButtonCount="20" PageSize="20" ShowSeparators="True">
                                                <FirstPageButton Visible="True">
                                                </FirstPageButton>
                                                <LastPageButton Visible="True">
                                                </LastPageButton>
                                            </SettingsPager>
                                            <SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" />
                                           
                                        </dxe:ASPxGridView>
                                        <dxe:ASPxPopupControl ID="ASPXPopupControl" runat="server" ContentUrl="frmAddDocuments.aspx"
                                            CloseAction="CloseButton" Top="120" Left="300" ClientInstanceName="popup" Height="466px"
                                            Width="900px" HeaderText="Add/Modify Document" AllowResize="true" ResizingMode="Postponed" Modal="true">
                                            <ContentCollection>
                                                <dxe:PopupControlContentControl runat="server">
                                                </dxe:PopupControlContentControl>
                                            </ContentCollection>
                                            <HeaderStyle BackColor="Blue" Font-Bold="True" ForeColor="White" />
                                        </dxe:ASPxPopupControl>
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="Registration" Text="Registration">

                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="Other" Visible="false" Text="Other">
                                <%--  <TabTemplate>
                                    <span style="font-size: x-small">Other</span>&nbsp;<span style="color: Red;">*</span>
                                </TabTemplate>--%>
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="Group Member" Text="Group">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="Deposit" Visible="false" Text="Deposit">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="Remarks" Text="UDF" Visible="false">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="Education" Visible="false" Text="Education">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="Trad. Prof." Visible="false" Text="Trad.Prof">
                                <%--<TabTemplate ><span style="font-size:x-small">Trad.Prof</span>&nbsp;<span style="color:Red;">*</span> </TabTemplate>--%>
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="FamilyMembers" Visible="false" Text="Family">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                            <dxe:TabPage Name="Subscription" Visible="false" Text="Subscription">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>

                        </TabPages>
                        <ClientSideEvents ActiveTabChanged="function(s, e) {
	                                            var activeTab   = page.GetActiveTab();	                                            
	                                            var Tab0 = page.GetTab(0);
	                                            var Tab1 = page.GetTab(1);
	                                            var Tab2 = page.GetTab(2);
	                                            var Tab3 = page.GetTab(3);
	                                            var Tab4 = page.GetTab(4);
	                                            var Tab5 = page.GetTab(5);
	                                            var Tab6 = page.GetTab(6);
	                                            var Tab7 = page.GetTab(7);
	                                            var Tab8 = page.GetTab(8);
	                                            var Tab9 = page.GetTab(9);
	                                            var Tab10 = page.GetTab(10);
	                                            var Tab11 = page.GetTab(11);
	                                            var Tab12 = page.GetTab(12);
	                                            var Tab13=page.GetTab(13);
	                                             
	                                            if(activeTab == Tab0)
	                                            {
	                                                disp_prompt('tab0');
	                                            }
	                                            if(activeTab == Tab1)
	                                            {
	                                                disp_prompt('tab1');
	                                            }
	                                            else if(activeTab == Tab2)
	                                            {
	                                                disp_prompt('tab2');
	                                            }
	                                            else if(activeTab == Tab3)
	                                            {
	                                                disp_prompt('tab3');
	                                            }
	                                            else if(activeTab == Tab4)
	                                            {
	                                                disp_prompt('tab4');
	                                            }
	                                            else if(activeTab == Tab5)
	                                            {
	                                                disp_prompt('tab5');
	                                            }
	                                            else if(activeTab == Tab6)
	                                            {
	                                                disp_prompt('tab6');
	                                            }
	                                            else if(activeTab == Tab7)
	                                            {
	                                                disp_prompt('tab7');
	                                            }
	                                            else if(activeTab == Tab8)
	                                            {
	                                                disp_prompt('tab8');
	                                            }
	                                            else if(activeTab == Tab9)
	                                            {
	                                                disp_prompt('tab9');
	                                            }
	                                            else if(activeTab == Tab10)
	                                            {
	                                                disp_prompt('tab10');
	                                            }
	                                            else if(activeTab == Tab11)
	                                            {
	                                                disp_prompt('tab11');
	                                            }
	                                            else if(activeTab == Tab12)
	                                            {
	                                                disp_prompt('tab12');
	                                            }
	                                            else if(activeTab == Tab13)
	                                            {
	                                               disp_prompt('tab13');
	                                            }
	                                            
	                                            }"></ClientSideEvents>
                        <ContentStyle>
                            <Border BorderColor="#002D96" BorderStyle="Solid" BorderWidth="1px" />
                        </ContentStyle>
                        <LoadingPanelStyle ImageSpacing="6px">
                        </LoadingPanelStyle>
                        <TabStyle Font-Size="12px">
                        </TabStyle>
                    </dxe:ASPxPageControl>
                </td>
                <td>
                    <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
        </table>
        </div>
        <asp:HiddenField ID="IsredirectedBranch" runat="server" />
        <%--<asp:SqlDataSource ID="EmployeeDocumentData" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand=""
            DeleteCommand="delete from tbl_master_document where doc_id=@Id">
          <DeleteParameters>
             <asp:Parameter Name="Id" Type="decimal" />
          </DeleteParameters>  
          <SelectParameters>
            <asp:SessionParameter Name="doc_contactId" SessionField="KeyVal_InternalID" Type="string" />
          </SelectParameters>
        </asp:SqlDataSource>--%>
    </div>
</asp:Content>
