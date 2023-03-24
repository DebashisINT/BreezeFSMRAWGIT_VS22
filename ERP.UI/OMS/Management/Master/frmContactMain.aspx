<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/OMS/MasterPage/ERP.Master" Inherits="ERP.OMS.Management.Master.management_master_frmContactMain" CodeBehind="frmContactMain.aspx.cs" %>

<%--------------------------------------------------Revision History ------------------------------------------------------------%>
<%--1.0     v2.0.36     Sanchita    10/01/2023      Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True" then 
                                                   data in portal shall be populated based on Hierarchy Only. Refer: 25504--%>
<%--2.0     V2.0.38     Pallab      01/01/2023     Salesman page design modification--%>

<%-----------------------------------------------End of Revision History----------------------------------------------------------%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        
        #EmployeeGrid_DXPagerBottom {
        min-width:100% !important;
        }
        /*Rev 2.0*/
        .btn-sm
        {
           padding: 6px 10px !important;
               font-size: 14px;
        }

        .btn
        {
            height: 34px !important;
        }
        /*Rev end 2.0*/
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".water").each(function () {
                if ($(this).val() == this.title) {
                    $(this).addClass("opaque");
                }
            });

            $(".water").focus(function () {
                if ($(this).val() == this.title) {
                    $(this).val("");
                    $(this).removeClass("opaque");
                }
            });

            $(".water").blur(function () {
                if ($.trim($(this).val()) == "") {
                    $(this).val(this.title);
                    $(this).addClass("opaque");
                }
                else {
                    $(this).removeClass("opaque");
                }
            });
        });

    </script>

    <script language="javascript" type="text/javascript">


        function ShowMissingData(obj, obj2) {
            var url = 'frmContactMissingData.aspx?id=' + obj;
            window.location.href = url;

        }
        function ShowError(obj) {

            if(grid.cpDelete!=null)
            {
                if(grid.cpDelete=='Success')
                {
                    jAlert('Deleted Successfully');
                    grid.cpDelete = null;
                }
                else
                {
                    jAlert('Used in other module.Can not delete');
                    grid.cpDelete = null;
                }

            }
             
            // height()
        }
        function NewPgae(cnt_id) {
            //alert('cnt_id');
        }
        function ClickOnMoreInfo(keyValue) {
            //var url = 'Contact_general.aspx?contact_type=' + '<%=Session["Contactrequesttype"]%>' + '&id=' + keyValue;
            var url = 'Contact_general.aspx?id=' + keyValue;
            window.location.href = url;
        }

        function OnDelete(keyValue) {
            jConfirm('Confirm delete?', 'Confirmation Dialog', function (r) {
                if (r == true)
                {
                    grid.PerformCallback('Delete~' + keyValue);
                }
            }); 
        }

        function OnBudgetopen(Cusid) {
        //    if (document.getElementById('IsUdfpresent').value == '0') {
        //        jAlert("UDF not define.");
        //    }
        //    else {
        //        // var url = '../master/frm_BranchUdfPopUp.aspx?Type=SQO';

        //        var keyVal = document.getElementById('Keyval_internalId').value;
            var url = '/OMS/Management/Master/BudgetAdd.aspx?Cusid=' + Cusid;
                popupbudget.SetContentUrl(url);
                popupbudget.Show();

            //}
            return true;
        }

        function BudgetAfterHide(s, e) {
            popupbudget.Hide();
        }


        function ParentCustomerOnClose(newCustId, CustomerName, CustUniqueName, BillingStateText, BillingStateCode, ShippingStateText, ShippingStateCode) {
            AspxDirectAddCustPopup.Hide();
            var url = 'frmContactMain.aspx?requesttype=customer';
            window.location.href = url;

            //if (newCustId.trim() != '') {
             //   page.SetActiveTabIndex(0);
             //   GetObjectID('hdnCustomerId').value = newCustId;

              //  GetObjectID('lblBillingStateText').value = BillingStateText;
              //  GetObjectID('lblBillingStateValue').value = BillingStateCode;

               // GetObjectID('lblShippingStateText').value = ShippingStateText;
              //  GetObjectID('lblShippingStateValue').value = ShippingStateCode;

                // cCustomerCallBackPanel.PerformCallback('SetCustomer~' + newCustId + '~' + CustomerName);
               // var FullName = new Array(CustUniqueName, CustomerName);
               // cCustomerComboBox.AddItem(FullName, newCustId);
              //  cCustomerComboBox.SetValue(newCustId);
               // $('#DeleteCustomer').val("yes");
               // page.GetTabByName('[B]illing/Shipping').SetEnabled(true);
              //  cddl_SalesAgent.Focus();
              
            //}
        }

        function OnAddButtonClick() {
          

            var isLighterPage = $("#hidIsLigherContactPage").val();
           // alert(isLighterPage);
            if (isLighterPage == 1) {               
                var url = '/OMS/management/Master/customermasterPopup.html?var=1.6';
               // alert(url);
                AspxDirectAddCustPopup.SetContentUrl(url);                
                AspxDirectAddCustPopup.RefreshContentUrl();
                AspxDirectAddCustPopup.Show();
            }
            else
             {
            var url = 'Contact_general.aspx?id=' + 'ADD';        
            window.location.href = url;
            }
        }
        function OnCreateActivityClick(KeyVal, cnt_id,status) {
            //kaushik
            // var url = "Lead_Activity.aspx?id=" + KeyVal + "&cnt_id=" + cnt_id;
            if (status == "Converted")
            {
                jAlert("Your Current Status is: Converted .Cannot Proceed");
                return false;
            }
           else if (status == "Lost")
           {
               jAlert("Your Current Status is: Lost .Cannot Proceed");
               return false;
           }
           else
               {
            var url = "../ActivityManagement/Sales_Activity.aspx?id=" + KeyVal + "&cnt_id=" + cnt_id;
            window.location.href = url;
           }
        }
        function ShowHideFilter(obj) {
            if (document.getElementById('TxtSeg').value == 'N') {
                document.getElementById('TxtTCODE').style.display = "none";
            }
            else {
                document.getElementById('TxtTCODE').style.display = "inline";
            }
            InitialTextVal();
            if (obj == "s")
                //document.getElementById('TrFilter').style.display="inline";
                grid.PerformCallback('ssss');
            else {
                document.getElementById('TrFilter').style.display = "none";
                grid.PerformCallback(obj);
            }
        }
        function callback() {
            grid.PerformCallback();
        }
        function OnContactInfoClick(keyValue, CompName) {

            var url = 'insurance_contactPerson.aspx?id=' + keyValue;
            window.location.href = url;
        }
        function OnHistoryInfoClick(keyValue, CompName) {
            var url = 'ShowHistory_Phonecall.aspx?id1=' + keyValue;
            //OnMoreInfoClick(url, "Lead  History", '940px', '450px', "Y");
            window.location.href = url;
        }
        function OnAddBusinessClick(keyValue, CompName) {
            var url = 'AssignIndustry.aspx?id1=' + keyValue;
            window.location.href = url;
        }
        function btnSearch_click() {
            document.getElementById('TrFilter').style.display = "none";
            grid.PerformCallback('s');
        }
        function InitialTextVal() {


            document.getElementById('txtName').value = "Name";
            document.getElementById('txtBranchName').value = "Branch Name";
            document.getElementById('txtCode').value = "Code";
            document.getElementById('txtRelationManager').value = "R. Manager";
            document.getElementById('txtReferedBy').value = "Email";
            document.getElementById('txtPhNumber').value = "Ph. Number";
            document.getElementById('txtContactStatus').value = "Contact Status";
            //        document.getElementById('txtStatus').value = "Status";

            document.getElementById('TxtTCODE').value = "Trade. Code";
            document.getElementById('txtPAN').value = "PAN No.";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="breadCumb" id="td_contact1" runat="server">
        <span><asp:Label ID="lblHeadTitle" runat="server"></asp:Label></span>
        <span id="td_broker1" runat="server">
            Broker List
        </span>
    </div>
    <div class="container">
        <div class="backBox mt-5 p-3 ">
        <table class="TableMain100 mb-4">
            <%--<tr id="td_contact" runat="server">
                <td style="text-align: center;">
                    <strong><span style="color: #000099">Contact List</span></strong>
                </td>
            </tr>
            <tr id="td_broker" runat="server">
                <td class="EHEADER" style="text-align: center;">
                    <strong><span style="color: #000099">Broker List</span></strong>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <%--  <table width="100%">
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
                            <td class="gridcellright" align="right">
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
                        <div class="FilterSide">
                            <div style="float: left; padding-right: 5px;">
                                  <% if (rights.CanAdd)
                                               { %>

                            
                                <a href="javascript:void(0);" onclick="OnAddButtonClick()" class="btn btn-success"><span>Add New</span> </a>

                                  <% } %>
                            </div>
                            <%--<div style="float: left; padding-right: 5px;">
                        <a href="javascript:ShowHideFilter('s');" class="btn btn-primary"><span>
                            Show Filter</span></a>
                    </div>--%>
                            <div class="pull-left">
                                <%--<a href="javascript:ShowHideFilter('All');" class="btn btn-primary"><span>All Records</span></a>--%>

                                 <% if (rights.CanExport)
                                               { %>
                                 <asp:DropDownList ID="drdExport" runat="server" CssClass="btn btn-sm btn-primary" OnChange="if(!AvailableExportOption()){return false;}" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Export to</asp:ListItem>
                                    <asp:ListItem Value="1">PDF</asp:ListItem>
                                    <asp:ListItem Value="2">XLS</asp:ListItem>
                                    <asp:ListItem Value="3">RTF</asp:ListItem>
                                    <asp:ListItem Value="4">CSV</asp:ListItem>

                                </asp:DropDownList>
                                 <% } %>
                            </div>
                        </div>
                        <div class="ExportSide pull-right">
                            <div>
                               <%-- <dxe:ASPxComboBox ID="cmbExport" runat="server" AutoPostBack="true"
                                    Font-Bold="False" ForeColor="black" OnSelectedIndexChanged="cmbExport_SelectedIndexChanged"
                                    ValueType="System.Int32" Width="130px">
                                    <items>
                                    <dxe:ListEditItem Text="Select" Value="0" />
                                    <dxe:ListEditItem Text="PDF" Value="1" />
                                    <dxe:ListEditItem Text="XLS" Value="2" />
                                    <dxe:ListEditItem Text="RTF" Value="3" />
                                    <dxe:ListEditItem Text="CSV" Value="4" />
                                </items>
                                    <buttonstyle>
                                </buttonstyle>
                                    <itemstyle>
                                    <HoverStyle>
                                    </HoverStyle>
                                </itemstyle>
                                    <border bordercolor="black" />
                                    <dropdownbutton text="Export">
                                </dropdownbutton>
                                </dxe:ASPxComboBox>--%>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="TrFilter" style="display: none">
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" CssClass="water" Text="Name" ToolTip="Name"
                                    Font-Size="12px" Width="119px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBranchName" runat="server" CssClass="water" Text="Branch Name"
                                    ToolTip="Branch Name" Font-Size="12px" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCode" runat="server" CssClass="water" Text="Code" ToolTip="Code"
                                    Font-Size="12px" Width="54px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtTCODE" runat="server" CssClass="water" Text="Trade.Code" ToolTip="Trade.Code"
                                    Font-Size="12px" Width="79px"></asp:TextBox>
                                <asp:HiddenField ID="TxtSeg" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtPAN" runat="server" CssClass="water" Text="PAN No." ToolTip="PAN No."
                                    Font-Size="12px" Width="79px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRelationManager" runat="server" CssClass="water" Text="R. Manager"
                                    ToolTip="R. Manager" Font-Size="12px" Width="85px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtReferedBy" runat="server" CssClass="water" Text="Email" ToolTip="Email"
                                    Font-Size="12px" Width="92px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPhNumber" runat="server" CssClass="water" Text="Ph. Number" ToolTip="Ph. Number"
                                    Font-Size="12px" Width="90px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtContactStatus" runat="server" CssClass="water" Text="Contact Status"
                                    ToolTip="Contact Status" Font-Size="12px" Width="79px"></asp:TextBox>
                            </td>
                            <%--  <td visible="false">
                                    <asp:TextBox ID="txtStatus" runat="server" CssClass="water" Text="Status" ToolTip="Status"
                                        Font-Size="12px" Width="97px"></asp:TextBox>
                                </td>--%>
                            <td>
                                <input id="btnSearch" type="button" value="Search" class="btnUpdate" style="height: 21px"
                                    onclick="btnSearch_click()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="pt-4">
                    <%--Rev 1.0 [DataSourceID="EmployeeDataSource" removed]  --%>
                    <dxe:ASPxGridView ID="EmployeeGrid" runat="server" KeyFieldName="cnt_Id" AutoGenerateColumns="False" OnDataBound="EmployeeGrid_DataBound"
                        Width="100%" ClientInstanceName="grid" OnCustomJSProperties="EmployeeGrid_CustomJSProperties"
                        OnCustomCallback="EmployeeGrid_CustomCallback" OnHtmlRowCreated="EmployeeGrid_HtmlRowCreated"  SettingsBehavior-AllowFocusedRow="true">
                       
                        <clientsideevents endcallback="function(s,e) { ShowError(s.cpInsertError);
                                                                                                 }" />
                        <settingspager numericbuttoncount="10" showseparators="True" alwaysshowpager="True"  pagesize="10">
                             <PageSizeItemSettings Visible="true" ShowAllItem="false" Items="10,50,100,150,200"/>
                              
                            <FirstPageButton Visible="True">
                            </FirstPageButton>
                            <LastPageButton Visible="True">
                            </LastPageButton>
                        </settingspager>

                        <settingsediting mode="PopupEditForm" popupeditformhorizontalalign="Center" popupeditformmodal="True"
                            popupeditformverticalalign="WindowCenter" popupeditformwidth="900px" editformcolumncount="3" />
                        <SettingsSearchPanel Visible="True" />
                        <settings showfilterrow="true" showgrouppanel="true" showfilterrowmenu="true"   />
                        
                        <settingsbehavior confirmdelete="True" columnresizemode="NextColumn" FilterRowMode="Auto" />

                        <settingstext popupeditformcaption="Add/ Modify Employee" ConfirmDelete="Confirm delete?"  />
                        <stylespager>
                            <Summary Width="100%">
                            </Summary>
                        </stylespager>
                        <columns>
                            <dxe:GridViewDataTextColumn Visible="False" ReadOnly="True" VisibleIndex="0" FieldName="Id">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="2" FieldName="Name">
                                <CellStyle CssClass="gridcellleft" wrap="True">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="3" FieldName="BranchName">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="1" FieldName="Code" Caption="Unique ID">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <%-- <dxe:GridViewDataTextColumn VisibleIndex="4" FieldName="CRG_TCODE" Caption="Trade. Code" Visible="false">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>--%>

                       <%--     <dxe:GridViewDataTextColumn VisibleIndex="5" FieldName="PanNumber" Caption="PAN No." >
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>--%>

                            <%-- <dxe:GridViewDataTextColumn VisibleIndex="6" FieldName="RM" Caption="Relationship Manager">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>--%>
                            <%--<dxe:GridViewDataTextColumn VisibleIndex="7" FieldName="eml_email" Caption="Email (Official)">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>--%>

                            <dxe:GridViewDataTextColumn VisibleIndex="4" FieldName="phf_phoneNumber" Caption="Phone Number" Width="120px">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn VisibleIndex="6" FieldName="Status" Caption="Contact Status">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                          <%--  <dxe:GridViewDataTextColumn VisibleIndex="5" FieldName="Status" Caption="Status"
                                Visible="false">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>--%>

                             <dxe:GridViewDataTextColumn VisibleIndex="7" FieldName="Activetype" Caption="Status" Width="10%"
                                >
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                             <dxe:GridViewDataTextColumn VisibleIndex="8" FieldName="EnterBy" Caption="Enter By" Width="10%"
                                >
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                             <dxe:GridViewDataTextColumn VisibleIndex="9" FieldName="ModifyDateTime" Caption="Last Update On" Width="10%"
                                >
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                             <dxe:GridViewDataTextColumn VisibleIndex="10" FieldName="ModifyUser" Caption="Updated By" Width="10%"
                                >
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                             <dxe:GridViewDataTextColumn VisibleIndex="11" FieldName="gstin" Caption="GSTIN" Width="11%"
                                >
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>
                            <dxe:GridViewDataTextColumn ReadOnly="True" VisibleIndex="12" CellStyle-HorizontalAlign="Center" Width="150px">

                                 <CellStyle HorizontalAlign="Center"></CellStyle>
                                <HeaderTemplate>Actions</HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <DataItemTemplate>
                                    
                                    <% if (Convert.ToString(Session["requesttype"]) != "Lead")
                                       { if(rights.CanCreateActivity)
                                          { %>
                                    <a href="javascript:void(0);" onclick="OnCreateActivityClick('<%# Eval("Id") %>','<%# Eval("cnt_id") %>','<%# Eval("Status") %>')" title="Create Activity" class="pad" style="text-decoration: none;">
                                        <img src="../../../assests/images/activity.png" />
                                    </a>
                                        <% } 
                                           if(rights.CanEdit)
                                           {%>
                                    <a href="javascript:void(0);" onclick="ClickOnMoreInfo('<%# Eval("cnt_id") %>')" title=" More Info" class="pad"  style="text-decoration: none;">
                                        <img src="../../../assests/images/info.png" />
                                    </a>
                                        <% } 
                                           if(rights.CanContactPerson)
                                           {%>
                                    <%--........................................Code Added By Sam on 27092016........................................Add Industry...--%>
                                    <a href="javascript:void(0);" onclick="OnContactInfoClick('<%#Eval("Id") %>','<%#Eval("Name") %>')" title="Add Contact Person" class="pad" style="text-decoration: none;">
                                        <img src="../../../assests/images/show.png" />
                                    </a>
                                        <% } 
                                           if(rights.CanIndustry)
                                           { %>
                                      <a href="javascript:void(0);" onclick="OnAddBusinessClick('<%#Eval("Id") %>','<%#Eval("Name") %>')" title="Map Industry" class="pad"  style="text-decoration: none;">
                                        <img src="../../../assests/images/icoaccts.gif" />  </a>
                                     <%--........................................Code Added By Sam on 27092016...........................................--%>
                                    <%     }
                                            if (rights.CanDelete)
                                           { %>
                                    <a href="javascript:void(0);" onclick="OnDelete('<%# Eval("Id") %>')" title="Delete"  class="pad">
                                <img src="/assests/images/Delete.png" /></a>
                                    <%   }%>

                                      <%  if (rights.CanBudget)
                                           { %>

                                   <a href="javascript:void(0);" onclick="OnBudgetopen('<%# Eval("cnt_Id") %>')" title="Budget"  class="pad">
                                <img src="/assests/images/cashbudget.png" width="16" height="16"/></a>
                                         
                                       <%   }%>


                                       <% } %>


                                        <% else

                                       { %>
                                     <%--........................................Code Added By Sam on 07112016...........................................--%>
                                   <%-- <a href="javascript:void(0);" onclick="OnCreateActivityClick('<%# Eval("Id") %>')" title="Create Activity" class="pad" style="text-decoration: none;">--%>
                                        <% if(rights.CanCreateActivity)
                                           { %>
                                      <a href="javascript:void(0);" onclick="OnCreateActivityClick('<%# Eval("Id") %>','<%# Eval("cnt_id") %>','<%# Eval("Status") %>')" title="Create Activity" class="pad" style="text-decoration: none;">

                                         <%--........................................Code Above Added By Sam on 07112016...........................................--%>

                                        <img src="../../../assests/images/activity.png" />
                                    </a>
                                    <% }
                                           if (rights.CanEdit)
                                       {%>
                                    <a href="javascript:void(0);" onclick="ClickOnMoreInfo('<%# Eval("cnt_id") %>')" title="More Info" class="pad" style="text-decoration: none;">
                                        <img src="../../../assests/images/info.png" />
                                    </a>
                                    <% }
                                           if (rights.CanContactPerson)
                                        {%>
                                    <a href="javascript:void(0);" onclick="OnContactInfoClick('<%#Eval("Id") %>','<%#Eval("Name") %>')" title="Show" class="pad" style="text-decoration: none;">
                                        <img src="../../../assests/images/show.png" />
                                    </a>
                                    <% } 
                                       if(rights.CanHistory)
                                         {%>
                                    <a href="javascript:void(0);" onclick="OnHistoryInfoClick('<%#Eval("Id") %>','<%#Eval("Name") %>')" title="History" class="pad" style="text-decoration: none;">
                                        <img src="../../../assests/images/history.png" />
                                        <% }
                                           if (rights.CanIndustry)
                                           { %>
                                         <a href="javascript:void(0);" onclick="OnAddBusinessClick('<%#Eval("Id") %>','<%#Eval("Name") %>')" title="Map Industry" class="pad" style="text-decoration: none;">
                                        <img src="../../../assests/images/icoaccts.gif" />
                                    </a>
                                    <%   } 
                                  
                                    if (rights.CanDelete)
                                           { %>
                                    <a href="javascript:void(0);" onclick="OnDelete('<%# Eval("Id") %>')" title="Delete"  class="pad">
                                <img src="/assests/images/Delete.png" /></a>
                                    <%   }
                                         }%> 
                                </DataItemTemplate>
                            </dxe:GridViewDataTextColumn>

                            <dxe:GridViewDataTextColumn Visible="False" VisibleIndex="14" FieldName="user_name"
                                Caption="Created User">
                                <CellStyle CssClass="gridcellleft">
                                </CellStyle>
                                <EditFormCaptionStyle HorizontalAlign="Right">
                                </EditFormCaptionStyle>
                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dxe:GridViewDataTextColumn>

                        </columns>
                    </dxe:ASPxGridView>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="EmployeeDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"  SelectCommand="">
             <SelectParameters>
                <asp:SessionParameter Name="userlist" SessionField="userchildHierarchy" Type="string" />
            </SelectParameters>


        </asp:SqlDataSource>
        <dxe:ASPxGridViewExporter ID="exporter" runat="server"  Landscape="false" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true" >
        </dxe:ASPxGridViewExporter>


                 <dxe:ASPxPopupControl ID="ASPXPopupControl2" runat="server"
                CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popupbudget" Height="500px"
                Width="1310px" HeaderText="Budget" Modal="true" AllowResize="true" ResizingMode="Postponed">
                <ContentCollection>
                    <dxe:PopupControlContentControl runat="server">
                    </dxe:PopupControlContentControl>
                </ContentCollection>
                
                 <ClientSideEvents CloseUp="BudgetAfterHide" />
            </dxe:ASPxPopupControl>


        <dxe:ASPxPopupControl ID="DirectAddCustPopup" runat="server"
        CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="AspxDirectAddCustPopup" Height="650px"
        Width="1020px" HeaderText="Add New Customer" Modal="true" AllowResize="true" ResizingMode="Postponed">
         
        <ContentCollection>
            <dxe:PopupControlContentControl runat="server">
            </dxe:PopupControlContentControl>
        </ContentCollection>
    </dxe:ASPxPopupControl>

        <asp:HiddenField id="hidIsLigherContactPage" runat="server" />
          <asp:HiddenField id="hdnIsDMSFeatureOn" runat="server" />
    </div>
        </div>
</asp:Content>
