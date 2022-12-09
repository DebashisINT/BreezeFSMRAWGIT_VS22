﻿<%@ Page Title="" Language="C#" MasterPageFile="~/OMS/MasterPage/ERP.Master" AutoEventWireup="true" CodeBehind="Branch_Correspondance.aspx.cs" Inherits="ERP.OMS.Management.Master.Branch_Correspondance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        
        /*.dxeErrorCell_PlasticBlue  {
            position: absolute;
            margin-top: 6px;
        }*/
        .dxeErrorFrameSys.dxeErrorCellSys, .dxeErrorFrameSys.dxeErrorCellSys {
            position:absolute !important;
        }
    </style>
    <script lang="javascript" type="text/javascript">
        /*function ul() {
            window.opener.document.getElementById('iFrmInformation').setAttribute('src', 'CallUserInformation.aspx')
        }*/

        function OnCountryChanged(cmbCountry) {

            gridAddress.GetEditor("City").PerformCallback('0');
            gridAddress.GetEditor("area").PerformCallback('0');
            gridAddress.GetEditor("PinCode").PerformCallback('0');// change cmdcountry.getvalue() to 0 on 07122016
            gridAddress.GetEditor("State").PerformCallback(cmbCountry.GetValue().toString());

            //alert("asda");

        }
        function OnStateChanged(cmbState) {

            gridAddress.GetEditor("area").PerformCallback('0');
            //gridAddress.GetEditor("City").PerformCallback('0');
            gridAddress.GetEditor("PinCode").PerformCallback('0');
            gridAddress.GetEditor("City").PerformCallback(cmbState.GetValue().toString());

        }
        function OnCityChanged(cmbCity) {
            gridAddress.GetEditor("area").PerformCallback(cmbCity.GetValue().toString());
            gridAddress.GetEditor("PinCode").PerformCallback(cmbCity.GetValue().toString());
        }

        //................ Code Added by Sam on 20102016..................
        function OnEmilTypeChanged(emiltype) {
            var tes = emiltype.GetValue().toString();
            gridEmail.PerformCallback(emiltype.GetValue().toString());
        }

        function OnPhoneTypeChanged(phonetype) {

            //var ph = phf_type.GetValue().toString();

            //alert(tes);

            gridPhone.PerformCallback(phf_type.GetValue().toString());
        }

        function HidePopupAndShowInfo() {
           // popupan.Hide();

        }





        //<dxe:ListEditItem Text="Official (For sending Emails)" Value="Official"></dxe:ListEditItem>
        //                                                                            <dxe:ListEditItem Text="Personal" Value="Personal"></dxe:ListEditItem>
        //                                                                            <dxe:ListEditItem Text="Web Site" Value="Web Site"></dxe:ListEditItem>




        //................ Code Added by Sam on 20102016..................
        function OnChildCall(cmbCity) {
            OnCityChanged(gridAddress.GetEditor("City"));
        }
        function openAreaPage() {
            var left = (screen.width - 300) / 2;
            var top = (screen.height - 250) / 2;
            var cityid = gridAddress.GetEditor("City").GetValue();
            var cityname = gridAddress.GetEditor("City").GetText();

            if (cityid == null) {
                jAlert('Please select City to add an area');
            }
            else {
                var URL = 'AddArea_PopUp.aspx?id=' + cityid + '&name=' + cityname + '';
                popupan.SetContentUrl(URL);
                popupan.Show();
            }


        }
        function disp_prompt(name) {
            if (name == "tab0") {
                document.location.href = "BranchAddEdit.aspx";
            }
            
            if (name == "tab1") {
                document.location.href = "Branch_Correspondance.aspx?Page=branch";
            }
            if (name == "tab2") {
                document.location.href = "Contact_Document.aspx?Page=branch";
            }
        }

        function OnPhoneClick() {

            gridPhone.UpdateEdit();

        }
        function Errorcheckingonendcallback(s, e) {

            if (gridAddress.cperror != null) {
                jAlert(gridAddress.cperror);
                gridAddress.cperror = null
                return false;
            }
        }
        function OnEmailClick() {
            if (gridEmail.GetEditor('eml_type').GetValue() == 'Web Site') {
                if (gridEmail.GetEditor('eml_website').GetValue() == null)
                    jAlert('Url Required');
                else
                    gridEmail.UpdateEdit();
            }
            else {
                if (gridEmail.GetEditor('eml_email').GetValue() == null)
                    jAlert('Email Required');
                else
                    gridEmail.UpdateEdit();
            }
        }
        function Emailcheck(obj) {
            if (obj == 'c') {
                jAlert("This email id has already exists for other contacts.");
            }

        }


        //-----------For Address Status -------------------
        function btnSave_Click() {
            var obj = 'SaveOld~' + RowID;
            popPanel.PerformCallback(obj);

        }

        function OnAddEditClick(e, obj) {
            var data = obj.split('~');
            if (data.length > 1)
                RowID = data[1];
            popup.Show();
            popPanel.PerformCallback(obj);
        }
        function EndCallBack(obj) {
            if (obj == 'Y') {
                popup.Hide();
                jAlert("Successfully update");
                gridAddress.PerformCallback();
            }
            else if (obj == 'Y1') {
                var msg = 'Cannot proceed. Registered/Permanent Address is already exist as "Active".\n You can set only one Registered/Permanent Address as "Active". ';
                alert(msg);//added by sanjib 20122016

            }
            else if (obj == 'Y2') {
                var msg = 'Cannot proceed. Residence Address is already exist as "Active".\n You can set only one Residence Address as "Active".';
                alert(msg);//added by sanjib 20122016
            }
            else if (obj == 'Y3') {
                var msg = 'Cannot proceed. Office Address is already exist as "Active".\n You can set only one Office Address as "Active".';
                alert(msg);//added by sanjib 20122016
            }



        }
        function btnCancel_Click() {
            popup.Hide();
        }

        //-----------For Phone Status -------------------
        function btnSave_ClickP() {
            var obj = 'SaveOld~' + RowIDP;
            popPanelP.PerformCallback(obj);

        }

        function OnAddEditClickP(e, obj) {

            var data = obj.split('~');
            if (data.length > 1)
                RowIDP = data[1];
            popupP.Show();
            popPanelP.PerformCallback(obj);
        }
        function EndCallBackP(obj) {
            if (obj == 'Y') {
                popupP.Hide();
                jAlert("Successfully Updated");
                gridPhone.PerformCallback();
            }


        }
        function btnCancel_ClickP() {
            popupP.Hide();
        }


        //-----------For Email Status -------------------
        function btnSave_ClickE() {
            var obj = 'SaveOld~' + RowIDE;
            popPanelE.PerformCallback(obj);

        }

        function OnAddEditClickE(e, obj) {

            var data = obj.split('~');
            if (data.length > 1)
                RowIDE = data[1];
            popupE.Show();
            popPanelE.PerformCallback(obj);
        }
        function EndCallBackE(obj) {
            if (obj == 'Y') {
                popupE.Hide();
                jAlert("Successfully Updated");
                gridEmail.PerformCallback();
            }


        }
        function btnCancel_ClickE() {
            popupE.Hide();
        }
        function AddressUpdate() {


            gridAddress.UpdateEdit();



        }

        function IsNumeric(strString)
            //  check for valid numeric strings	
        {
            var strValidChars = "0123456789";
            var strChar;
            var blnResult = true;

            if (strString.length == 0) return false;

            //  test strString consists of valid characters listed above
            for (i = 0; i < strString.length && blnResult == true; i++) {
                strChar = strString.charAt(i);
                if (strValidChars.indexOf(strChar) == -1) {
                    blnResult = false;
                }
            }
            return blnResult;
        }

        function fn_AllowonlyNumeric(s, e) {
            var theEvent = e.htmlEvent || window.event;
            var key = theEvent.keyCode || theEvent.which;
            var keychar = String.fromCharCode(key);
            if (key == 9 || key == 37 || key == 38 || key == 39 || key == 40 || key == 8 || key == 46) { //tab/ Left / Up / Right / Down Arrow, Backspace, Delete keys
                return;
            }
            var regex = /[0-9\b]/;

            if (!regex.test(keychar)) {
                theEvent.returnValue = false;
                if (theEvent.preventDefault)
                    theEvent.preventDefault();
            }
        }
        function fn_chekFbtRate(s, e) {

          
        }


    </script>
   
    <style type="text/css">
        .dxeValidStEditorTable td.dxeErrorFrameSys.dxeErrorCellSys {
            position: absolute !important;
        }

        .dxeValidStEditorTable[errorframe="errorFrame"] {
            width: 100% !important;
        }
        /*rev 25249*/
        .panel-title
        {
                position: relative;
        }
        span.dx-vam, span.dx-vat, span.dx-vab, a.dx-vam, a.dx-vat, a.dx-vab
        {
                font-size: 15px;
        }
        .dxtcLite_PlasticBlue > .dxtc-stripContainer .dxtc-activeTab {
            background: #094e8c;
        }
         /*rev end 25249*/
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="panel-heading">
        <div class="panel-title">
          
            <h3>
                <asp:Label ID="lblHeadTitle" runat="server"></asp:Label>
            </h3>
            <div class="crossBtnN"><a href="BranchAddEdit.aspx"><i class="fa fa-times"></i></a></div>
        </div>

    </div>
    <div class="container">
        <div class="backBox mt-5 p-3 ">
    <div class="form_main">
        <div>
            <table width="100%">
                <tr>
                    <td class="EHEADER" style="text-align: center">
                        <asp:Label ID="lblName" runat="server" Font-Size="12px" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="TableMain100">
                <tr>
                    <td>
                        <dxe:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="1" ClientInstanceName="page"
                            OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged">
                            <TabPages>
                                <dxe:TabPage Name="General" Text="General">

                                    <ContentCollection>
                                        <dxe:ContentControl runat="server">
                                        </dxe:ContentControl>
                                    </ContentCollection>
                                </dxe:TabPage>

                             



                                <dxe:TabPage Name="Correspondence" Text="Correspondence">

                                    <ContentCollection>
                                        <dxe:ContentControl runat="server">
                                            <dxe:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" ClientInstanceName="page">
                                                <TabPages>

                                                    <dxe:TabPage Name="Adress" Text="Address">

                                                        <ContentCollection>
                                                            <dxe:ContentControl runat="server">
                                                                <div style="float: left;">
                                                                     <% if (rights.CanAdd)
                                                                           { %>
                                                                    <a href="javascript:void(0);" class="btn btn-primary" onclick="gridAddress.AddNewRow();"><span>Add New</span> </a>
                                                                 <% } %>
                                                                     </div>

                                                                 <div class="pull-left">
                              
                            </div>

                                                                <dxe:ASPxGridView ID="AddressGrid" runat="server" DataSourceID="Address" ClientInstanceName="gridAddress"
                                                                    KeyFieldName="Id" AutoGenerateColumns="False" OnCellEditorInitialize="AddressGrid_CellEditorInitialize"
                                                                    Width="100%" OnCustomCallback="AddressGrid_CustomCallback" OnRowValidating="AddressGrid_RowValidating"
                                                                     OnCommandButtonInitialize="AddressGrid_CommandButtonInitialize" EnableRowsCache="false"  >


                                                                    <Columns>
                                                                          <dxe:GridViewDataComboBoxColumn  FieldName="area"  Visible="false" VisibleIndex="-1" >
                                                                            <PropertiesComboBox ValueType="System.Int32" DataSourceID="SelectArea" EnableSynchronization="False" Width="100%"
                                                                                EnableIncrementalFiltering="True" ValueField="area_id" TextField="areaname">
                                                                            </PropertiesComboBox>
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                            <EditFormSettings VisibleIndex="18" Visible="True" Caption="Area"/>
                                                                        </dxe:GridViewDataComboBoxColumn>


                                                                        <dxe:GridViewDataTextColumn FieldName="Id" Visible="False" VisibleIndex="0" Caption="Id">
                                                                            <EditFormSettings Visible="False" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                        </dxe:GridViewDataTextColumn>
                                                                       <%-- <dxe:GridViewDataTextColumn Caption="Isdefault" visible="false" FieldName="Id">
                                                                            <EditFormSettings  Visible="True"/>
                                                                             <DataItemTemplate>
                                                                                <dxe:ASPxCheckBox runat="server" ClientInstanceName="cisdefault">
                                                                                    <ClientSideEvents CheckedChanged="function(s,e){fn_chekFbtRate(s,e);}" />
                                                                                </dxe:ASPxCheckBox>
                                                                                
                                                                            </DataItemTemplate>
                                                                        </dxe:GridViewDataTextColumn>--%>
                                                                        <dxe:GridViewDataCheckColumn FieldName="Isdefault" Visible="False" VisibleIndex="1" Caption="Default">
                                                                            <EditFormSettings Visible="True" />
                                                                            <PropertiesCheckEdit>
                                                                              <%--  <ClientSideEvents CheckedChanged="function(s,e){fn_chekFbtRate(s,e);}"/>--%>
                                                                            </PropertiesCheckEdit>
                                                                        </dxe:GridViewDataCheckColumn>
                                                                          <dxe:GridViewDataTextColumn FieldName="contactperson" VisibleIndex="2" Caption="Contact Person">
                                                                            <EditFormSettings Visible="true" VisibleIndex="2" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                        </dxe:GridViewDataTextColumn>
                                                                        <dxe:GridViewDataComboBoxColumn Caption="Address Type" FieldName="Type" Visible="False"
                                                                            VisibleIndex="3" Width="100%">
                                                                            <PropertiesComboBox ValueType="System.String">
                                                                                <Items>
                                                                                    <dxe:ListEditItem Text="Billing" Value="Billing" Selected="true"></dxe:ListEditItem>
                                                                                     <dxe:ListEditItem Text="Shipping" Value="Shipping"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="Registered/Permanent Address" Value="Registered"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="Residence" Value="Residence"></dxe:ListEditItem>
                                                                                    <dxe:ListEditItem Text="Office" Value="Office"></dxe:ListEditItem> 
                                                                                    <dxe:ListEditItem Text="Factory/Work/Branch" Value="FactoryWorkBranch"></dxe:ListEditItem>
                                                                                </Items>

                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">

                                                                                    <RequiredField IsRequired="True" ErrorText="Mandatory" />

                                                                                </ValidationSettings>

                                                                            </PropertiesComboBox>
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormSettings Visible="True" VisibleIndex="3" />
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                        </dxe:GridViewDataComboBoxColumn>

                                                                        <dxe:GridViewDataTextColumn FieldName="Type" VisibleIndex="4" Caption="Type">
                                                                            <EditFormSettings Visible="False" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                        </dxe:GridViewDataTextColumn>

                                                                         

                                                                        <dxe:GridViewDataTextColumn FieldName="Address1" VisibleIndex="5" Caption="Address1" PropertiesTextEdit-MaxLength="500">
                                                                            <EditFormSettings Visible="True" VisibleIndex="5" />
                                                                            <CellStyle CssClass="gridcellleft abc">
                                                                            </CellStyle>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                            <PropertiesTextEdit Width="100%"></PropertiesTextEdit>
                                                                            <%-- <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">

                                                                                    <RequiredField IsRequired="True" ErrorText="Mandatory" />

                                                                                </ValidationSettings>--%>
                                                                             <PropertiesTextEdit MaxLength="100">
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="right" SetFocusOnError="True">
                                                                                    <RequiredField IsRequired="True" ErrorText="Mandatory"></RequiredField>                                                                                   
                                                                                </ValidationSettings>                                                                           

                                                                            </PropertiesTextEdit>


                                                                        </dxe:GridViewDataTextColumn>

                                                                        <dxe:GridViewDataTextColumn FieldName="Address2" VisibleIndex="6" Caption="Address2" PropertiesTextEdit-MaxLength="500">
                                                                            <EditFormSettings Visible="True" VisibleIndex="6" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                            <PropertiesTextEdit Width="100%"></PropertiesTextEdit>
                                                                        </dxe:GridViewDataTextColumn>

                                                                       <dxe:GridViewDataTextColumn FieldName="Address3" VisibleIndex="6" Caption="Address3" PropertiesTextEdit-MaxLength="50"  Visible="false">
                                                                            <EditFormSettings Visible="True" VisibleIndex="6" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                            <PropertiesTextEdit Width="100%"></PropertiesTextEdit>
                                                                        </dxe:GridViewDataTextColumn>
                                                                        <dxe:GridViewDataTextColumn FieldName="Address4" VisibleIndex="6" Caption="Address4" PropertiesTextEdit-MaxLength="50"  Visible="false">
                                                                            <EditFormSettings Visible="True" VisibleIndex="6" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                            <PropertiesTextEdit Width="100%"></PropertiesTextEdit>
                                                                        </dxe:GridViewDataTextColumn>

                                                                        <dxe:GridViewDataTextColumn FieldName="LandMark" VisibleIndex="7" Caption="Landmark" PropertiesTextEdit-MaxLength="100"
                                                                            Visible="false">
                                                                            <EditFormSettings Visible="True" VisibleIndex="7" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                            <PropertiesTextEdit Width="100%"></PropertiesTextEdit>
                                                                        </dxe:GridViewDataTextColumn>
                                                                    <dxe:GridViewDataTextColumn FieldName="Phone" VisibleIndex="8" Caption="Phone" Visible="false">
                                                                        <EditFormSettings Visible="True" VisibleIndex="8" />
                                                                        <PropertiesTextEdit MaxLength="100">
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="right" SetFocusOnError="True">
                                                                                    <RequiredField IsRequired="True" ErrorText="Mandatory"></RequiredField>                                                                                   
                                                                                </ValidationSettings>                                                                           

                                                                            </PropertiesTextEdit>

                                                                        <CellStyle CssClass="gridcellleft">
                                                                        </CellStyle>
                                                                        <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                        </EditFormCaptionStyle>
                                                                    </dxe:GridViewDataTextColumn>
                                                                    <dxe:GridViewDataTextColumn FieldName="add_Email"  Caption="Email" Visible="false">
                                                                        <EditFormSettings Visible="True" VisibleIndex="9" />
                                                                        <CellStyle CssClass="gridcellleft">
                                                                        </CellStyle>
                                                                        <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                        </EditFormCaptionStyle>
                                                                        <PropertiesTextEdit MaxLength="200" Width="100%">
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="right" SetFocusOnError="True">                                                                                   
                                                                                    <RegularExpression ErrorText="Enter valid Email ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                                                </ValidationSettings>
                                                                            </PropertiesTextEdit>


                                                                    </dxe:GridViewDataTextColumn>
                                                                    <dxe:GridViewDataTextColumn FieldName="add_Website"  Caption="Website" Visible="false">
                                                                        <EditFormSettings Visible="True" VisibleIndex="10" />
                                                                        <CellStyle CssClass="gridcellleft">
                                                                        </CellStyle>
                                                                        <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                        </EditFormCaptionStyle>
                                                                    </dxe:GridViewDataTextColumn>

                                                                    <dxe:GridViewDataComboBoxColumn Caption="Designation" FieldName="add_Designation" Visible="False"
                                                                            VisibleIndex="9">
                                                                            <PropertiesComboBox DataSourceID="DesignationSelect" TextField="deg_designation" ValueField="deg_id" 
                                                                                EnableSynchronization="False" EnableIncrementalFiltering="True" ValueType="System.String" ClearButton-DisplayMode="Always" ClearButton-ImagePosition="Right">
                                                                               <%-- <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                                                                    <RequiredField IsRequired="True" ErrorText="Mandatory" />
                                                                                </ValidationSettings>       --%>  
                                                                                                                                                                                                                        
                                                                            </PropertiesComboBox>
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormSettings Visible="True" VisibleIndex="11" />
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                        </dxe:GridViewDataComboBoxColumn>


                                                                    <dxe:GridViewDataComboBoxColumn Caption="Country" FieldName="Country" Visible="False"
                                                                            VisibleIndex="9">
                                                                            <PropertiesComboBox DataSourceID="CountrySelect" TextField="Country" ValueField="cou_id"
                                                                                EnableSynchronization="False" EnableIncrementalFiltering="True" ValueType="System.String">
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                                                                    <RequiredField IsRequired="True" ErrorText="Mandatory" />
                                                                                </ValidationSettings>
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnCountryChanged(s); }"></ClientSideEvents>                                                                                
                                                                            </PropertiesComboBox>
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormSettings Visible="True" VisibleIndex="11" />
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                        </dxe:GridViewDataComboBoxColumn>

                                                                    <dxe:GridViewDataComboBoxColumn Caption="State" FieldName="State" Visible="False"
                                                                            VisibleIndex="9">
                                                                            <PropertiesComboBox DataSourceID="StateSelect" TextField="State" ValueField="ID" Width="100%"
                                                                                EnableSynchronization="False" EnableIncrementalFiltering="True" ValueType="System.String">
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                                                                    <RequiredField IsRequired="True" ErrorText="Mandatory" />
                                                                                </ValidationSettings>
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnStateChanged(s); }"></ClientSideEvents>


                                                                            </PropertiesComboBox>
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormSettings Visible="True" VisibleIndex="12" />
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                        </dxe:GridViewDataComboBoxColumn>



                                                                        <dxe:GridViewDataTextColumn FieldName="Country1" VisibleIndex="10" Caption="Country" Visible="False" >
                                                                            <EditFormSettings Visible="False" VisibleIndex="10"/>
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                        </dxe:GridViewDataTextColumn>

                                                                        <dxe:GridViewDataTextColumn FieldName="State1" VisibleIndex="11" Caption="State">
                                                                            <EditFormSettings Visible="False" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                        </dxe:GridViewDataTextColumn>

                                                                        <dxe:GridViewDataTextColumn FieldName="City1" VisibleIndex="12" Caption="City/ District">
                                                                            <EditFormSettings Visible="False" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                        </dxe:GridViewDataTextColumn>

                                                                        <dxe:GridViewDataComboBoxColumn Caption="City/ District" FieldName="City"  Visible="False" Width="0">
                                                                            <PropertiesComboBox DataSourceID="SelectCity" TextField="City" ValueField="CityId" Width="100%"   
                                                                                EnableSynchronization="False" EnableIncrementalFiltering="True" ValueType="System.String">
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                                                                    <RequiredField IsRequired="True" ErrorText="Mandatory" />
                                                                                </ValidationSettings>
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnCityChanged(s); }"></ClientSideEvents>
                                                                            </PropertiesComboBox>
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                            <EditFormSettings Visible="True" VisibleIndex="13" />
                                                                        </dxe:GridViewDataComboBoxColumn>

                                                                       <dxe:GridViewDataTextColumn Caption="Area" FieldName="add_area" VisibleIndex="17" Visible="true">
                                                                            <EditFormSettings Visible="False" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                        </dxe:GridViewDataTextColumn>

                                                                       

                                                                        <dxe:GridViewDataHyperLinkColumn Visible="false">
                                                                            <EditFormSettings  VisibleIndex="19" Visible="true" />
                                                                            <EditItemTemplate>
                                                                                <a href="javascript:void(0);" onclick="openAreaPage();"><span class="Ecoheadtxt">
                                                                                    <strong>Add New Area</strong></span></a>
                                                                            </EditItemTemplate>
                                                                        </dxe:GridViewDataHyperLinkColumn>
                                                                        <%--Debjyoti 02-12-2016--%>
                                                                        <%--<dxe:GridViewDataTextColumn FieldName="PinCode" VisibleIndex="9" Caption="Pincode">
                                                                            <EditFormSettings Visible="True" VisibleIndex="12" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <PropertiesTextEdit>
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </PropertiesTextEdit>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                        </dxe:GridViewDataTextColumn>--%>
                                                                           <dxe:GridViewDataTextColumn FieldName="PinCode1" VisibleIndex="15" Caption="Pincode / Zip" Visible="true" >
                                                                            <EditFormSettings Visible="false" VisibleIndex="15" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <PropertiesTextEdit>
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </PropertiesTextEdit>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                        </dxe:GridViewDataTextColumn>
                                                                        <dxe:GridViewDataComboBoxColumn Caption="Pincode / Zip" FieldName="PinCode" Visible="false" >
                                                                            <PropertiesComboBox DataSourceID="SelectPin" TextField="pin_code" ValueField="pin_id" Width="100%"
                                                                                EnableSynchronization="False" EnableIncrementalFiltering="True" ValueType="System.String" ClearButton-DisplayMode="Always" ClearButton-ImagePosition="Right">

                                                                                <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { OnStateChanged(s); }"></ClientSideEvents>--%>
                                                                            <ClearButton DisplayMode="Always" ImagePosition="Right"></ClearButton>

                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right" SetFocusOnError="True">

                                                                                    <RequiredField ErrorText="Mandatory" IsRequired="True" />

                                                                                </ValidationSettings>
                                                                            </PropertiesComboBox>
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormSettings VisibleIndex="16" Visible="True"  />
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                        </dxe:GridViewDataComboBoxColumn>

                                                                      <%--  <dxe:GridViewDataTextColumn FieldName="PinCode1" VisibleIndex="8" Caption="Pin / Zip">
                                                                            <EditFormSettings Visible="False" />
                                                                            <CellStyle CssClass="gridcellleft">
                                                                            </CellStyle>
                                                                            <EditFormCaptionStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False">
                                                                            </EditFormCaptionStyle>
                                                                        </dxe:GridViewDataTextColumn>--%>

                                                                        <%--End debjyoti02-12-2016--%>
                                                                        <dxe:GridViewDataTextColumn FieldName="status" VisibleIndex="20" Caption="Status">
                                                                            <DataItemTemplate>
                                                                                <a href="javascript:void(0);" onclick="OnAddEditClick(this,'Edit~'+'<%# Container.KeyValue %>')">
                                                                                    <dxe:ASPxLabel ID="ASPxTextBox2" runat="server" Text='<%# Eval("status")%>'
                                                                                        ToolTip="Click to Change Status">
                                                                                    </dxe:ASPxLabel>
                                                                                </a>
                                                                            </DataItemTemplate>
                                                                            <EditFormSettings Visible="False" />
                                                                            <CellStyle Wrap="False">
                                                                            </CellStyle>
                                                                            <HeaderTemplate>
                                                                                Status
                                                                            </HeaderTemplate>
                                                                            <HeaderStyle Wrap="False" />
                                                                        </dxe:GridViewDataTextColumn>

                                                                        <dxe:GridViewCommandColumn VisibleIndex="21" ShowDeleteButton="true" ShowEditButton="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">

                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                                                            <CellStyle HorizontalAlign="Center"></CellStyle>

                                                                            <HeaderTemplate>
                                                                                Actions
                                                                                <%-- <% if (rights.CanAdd)
                                                                                     { %>--%>
                                                                                <%--<a href="javascript:void(0);" onclick="gridAddress.AddNewRow();"><span>Add New</span> </a>--%>
                                                                                <%-- <% } %>--%>
                                                                            </HeaderTemplate>
                                                                        </dxe:GridViewCommandColumn>



                                                                    </Columns>


                                                                    <SettingsCommandButton>

                                                                       
                                                                        <EditButton Image-Url="../../../assests/images/Edit.png" ButtonType="Image" Image-AlternateText="Edit" Styles-Style-CssClass="pad">
                                                                            <Image AlternateText="Edit" Url="../../../assests/images/Edit.png"></Image>
                                                                            
                                                                            <Styles>
                                                                                <Style CssClass="pad"></Style>
                                                                            </Styles>
                                                                        </EditButton>
                                                                       
                                                                        <DeleteButton Image-Url="../../../assests/images/Delete.png" ButtonType="Image" Image-AlternateText="Delete">
                                                                            <Image AlternateText="Delete" Url="../../../assests/images/Delete.png"></Image>
                                                                        </DeleteButton>
                                                                       
                                                                        <UpdateButton Text="Save" ButtonType="Button"></UpdateButton>
                                                                        <CancelButton Text="Cancel" ButtonType="Button" Styles-Style-CssClass="btn btn-danger">
                                                                            <Styles>
                                                                                <Style CssClass="btn btn-danger"></Style>
                                                                            </Styles>
                                                                        </CancelButton>
                                                                    </SettingsCommandButton>

                                                                    <SettingsSearchPanel Visible="True" />
                                                                    <Settings ShowGroupPanel="True" ShowStatusBar="Visible" ShowFilterRow="true" ShowFilterRowMenu="true" />



                                                                    <SettingsEditing Mode="PopupEditForm" PopupEditFormHorizontalAlign="WindowCenter"
                                                                        PopupEditFormModal="True" PopupEditFormVerticalAlign="WindowCenter" PopupEditFormWidth="450px"
                                                                        EditFormColumnCount="1" />

                                                                    <Styles>
                                                                        <LoadingPanel ImageSpacing="10px">
                                                                        </LoadingPanel>
                                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                                        </Header>
                                                                    </Styles>

                                                                    <SettingsText PopupEditFormCaption="Add/Modify Address" ConfirmDelete="Confirm delete?" />
                                                                    <SettingsPager NumericButtonCount="20" PageSize="20" ShowSeparators="True">
                                                                        <FirstPageButton Visible="True">
                                                                        </FirstPageButton>
                                                                        <LastPageButton Visible="True">
                                                                        </LastPageButton>
                                                                    </SettingsPager>
                                                                    <SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" />
                                                                    <%--<ClientSideEvents EndCallback="function(s,e){addgrid_endcallback()}" />--%>

                                                                    <Templates>
                                                                        <TitlePanel>
                                                                            <table style="width: 100%">
                                                                                <tr>
                                                                                    <td align="center" style="width: 50%">
                                                                                        <span class="Ecoheadtxt" style="color: Black">Add/Modify Address.</span>
                                                                                    </td>

                                                                                </tr>
                                                                            </table>
                                                                        </TitlePanel>
                                                                        <EditForm>
                                                                            <div style="color: red; margin-top: 5px; margin-left: 5px;">* Denotes the mandatory field.</div>
                                                                            <div style="margin: 8px 8px 0px 8px">
                                                                                <table style="width: 100%" style="">

                                                                                    <tr>
                                                                                        <td style="width: 5%;"></td>
                                                                                        <td style="width: 90%;">

                                                                                            <dxe:ASPxGridViewTemplateReplacement runat="server" ReplacementType="EditFormEditors" ColumnID="" ID="Editors"></dxe:ASPxGridViewTemplateReplacement>


                                                                                            <div style="text-align: left; padding: 2px 2px 2px 110px">

                                                                                                <a id="update" href="#" onclick="AddressUpdate()" class="btn btn-primary " style="color: white; padding: 6px 18px !important;">Save</a>
                                                                                                <div class="dxbButton" style="display: inline-block; padding: 3px">
                                                                                                    <dxe:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                                                        runat="server"></dxe:ASPxGridViewTemplateReplacement>
                                                                                                </div>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td style="width: 5%;"></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </EditForm>

                                                                    </Templates>
                                                                    <ClientSideEvents EndCallback="Errorcheckingonendcallback" />
                                                                </dxe:ASPxGridView>

                                                                <dxe:ASPxPopupControl ID="ASPXPopupControl" runat="server" ContentUrl="AddArea_PopUp.aspx"
                                                                    CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popupan" Height="250px"
                                                                    Width="300px" HeaderText="Add New Area" AllowResize="true" ResizingMode="Postponed" Modal="true">
                                                                    <ContentCollection>
                                                                        <dxe:PopupControlContentControl runat="server">
                                                                        </dxe:PopupControlContentControl>
                                                                    </ContentCollection>
                                                                    <HeaderStyle BackColor="Blue" Font-Bold="True" ForeColor="White" />
                                                                </dxe:ASPxPopupControl>

                                                                <dxe:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="popup" runat="server"
                                                                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Set Address Status"
                                                                    EnableHotTrack="False" BackColor="#DDECFE" Width="400px" CloseAction="CloseButton">
                                                                    <ContentCollection>
                                                                        <dxe:PopupControlContentControl runat="server">
                                                                            <dxe:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="400px" ClientInstanceName="popPanel"
                                                                                OnCallback="ASPxCallbackPanel1_Callback" OnCustomJSProperties="ASPxCallbackPanel1_CustomJSProperties">
                                                                                <PanelCollection>
                                                                                    <dxe:PanelContent runat="server">
                                                                                        <table style="width: 100%">
                                                                                            <tr>
                                                                                                <td style="width: 25%">Status:
                                                                                                </td>
                                                                                                <td style="width: 50%">
                                                                                                    <asp:DropDownList ID="cmbStatus" runat="server" Width="180px" TabIndex="0">
                                                                                                        <asp:ListItem Text="Active" Value="Y"></asp:ListItem>
                                                                                                        <asp:ListItem Text="Deactive" Value="N"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 25%">Date:
                                                                                                </td>
                                                                                                <td style="width: 50%">
                                                                                                    <dxe:ASPxDateEdit ID="StDate" runat="server" ClientInstanceName="StDate" EditFormat="Custom"
                                                                                                        UseMaskBehavior="True" Width="179px" TabIndex="1">
                                                                                                        <ButtonStyle Width="13px">
                                                                                                        </ButtonStyle>
                                                                                                    </dxe:ASPxDateEdit>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 25%">Reason:
                                                                                                </td>
                                                                                                <td style="width: 75%">
                                                                                                    <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="250px" TabIndex="2" MaxLength="50"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 25%"></td>
                                                                                                <td colspan="2" class="gridcellleft" style="width: 75%">
                                                                                                    <%-- <input id="Button1" type="button" value="Save" class="btnUpdate dxbButton" onclick="btnSave_Click()"
                                                                                                        style="width: 60px" tabindex="41" />--%>
                                                                                                    <input id="Button1" type="button" value="Save" class="btnUpdate btn btn-primary" onclick="btnSave_Click()"
                                                                                                        style="width: 60px" tabindex="3" />

                                                                                                    <%--<input id="Button2" type="button" value="Cancel" class="btnUpdate dxbButton" onclick="btnCancel_Click()"
                                                                                                        style="width: 60px" tabindex="42" />--%>
                                                                                                    <input id="Button2" type="button" value="Cancel" class="btnUpdate btn btn-danger" onclick="btnCancel_Click()"
                                                                                                        style="width: 60px" tabindex="4" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </dxe:PanelContent>
                                                                                </PanelCollection>
                                                                                <ClientSideEvents EndCallback="function(s, e) {
	                                                        EndCallBack(s.cpLast);
                                                        }" />
                                                                            </dxe:ASPxCallbackPanel>
                                                                        </dxe:PopupControlContentControl>
                                                                    </ContentCollection>
                                                                    <HeaderStyle HorizontalAlign="Left">
                                                                        <Paddings PaddingRight="6px" />
                                                                    </HeaderStyle>
                                                                    <SizeGripImage Height="16px" Width="16px" />
                                                                    <CloseButtonImage Height="12px" Width="13px" />
                                                                    <ClientSideEvents CloseButtonClick="function(s, e) {
	     popup.Hide();
    }" />
                                                                </dxe:ASPxPopupControl>
                                                            </dxe:ContentControl>
                                                        </ContentCollection>
                                                    </dxe:TabPage>




                                                  
                                                </TabPages>
                                            </dxe:ASPxPageControl>
                                        </dxe:ContentControl>
                                    </ContentCollection>
                                </dxe:TabPage>
                                 <dxe:TabPage Name="Documents" Text="Documents">
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
	                                               
	                                                if(activeTab == Tab0)
	                                                {
	                                                    disp_prompt('tab0');
	                                                }
                                                    if(activeTab == Tab1)
	                                                {
	                                                    disp_prompt('tab1');
	                                                }
	                                                if(activeTab == Tab2)
	                                                {
	                                                    disp_prompt('tab2');
	                                                }
	                                             
	                                                
	                                                }"></ClientSideEvents>
                            <ContentStyle>
                                <Border BorderColor="#002D96" BorderStyle="Solid" BorderWidth="1px" />
                            </ContentStyle>
                            <LoadingPanelStyle ImageSpacing="6px">
                            </LoadingPanelStyle>
                            <TabStyle>
                            </TabStyle>
                        </dxe:ASPxPageControl>
                        <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
          <dxe:ASPxGridViewExporter ID="exporter" runat="server"  Landscape="true" PaperKind="A4" PageHeader-Font-Size="Larger" PageHeader-Font-Bold="true" >
        </dxe:ASPxGridViewExporter>
       

        <asp:SqlDataSource ID="Address" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="select DISTINCT  tbl_master_address.add_id AS Id,tbl_master_address.Isdefault as Isdefault, tbl_master_address.contactperson as contactperson,tbl_master_address.add_addressType AS Type,
                        tbl_master_address.add_address1 AS Address1,  tbl_master_address.add_address2 AS Address2, 
                        tbl_master_address.add_address3 AS Address3,tbl_master_address.add_landMark AS LandMark, 
                        tbl_master_address.add_country AS Country, 
                        tbl_master_address.add_state AS State,tbl_master_address.add_city AS City,CASE add_pin WHEN '' THEN '' ELSE(SELECT pin_code FROM tbl_master_pinzip WHERE pin_id = add_pin) END AS PinCode1,
                        CASE add_country WHEN '' THEN '' ELSE(SELECT cou_country FROM tbl_master_country WHERE cou_id = add_country) END AS Country1, 
                        CASE add_state WHEN '' THEN '' ELSE(SELECT state FROM tbl_master_state WHERE id = add_state) END AS State1,
                        CASE add_city WHEN '' THEN '' ELSE(SELECT city_name FROM tbl_master_city WHERE city_id = add_city) END AS City1,
                        CASE add_area WHEN '' THEN '' Else(select area_name From tbl_master_area Where area_id = add_area) End AS add_area, area = CAST(add_area as int),
                        tbl_master_address.add_pin AS PinCode, tbl_master_address.add_landMark AS LankMark ,
                            case when add_status='N' then 'Deactive' else 'Active' end as status ,add_Phone as Phone,add_Email,add_Website,add_Designation,add_address4  as  Address4                  
                            from tbl_master_address where add_cntId=@insuId"
            DeleteCommand="contactDelete"
            DeleteCommandType="StoredProcedure" InsertCommand="insert_correspondence" 
            InsertCommandType="StoredProcedure"  UpdateCommand="Update_correspondence"  UpdateCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="insuId" SessionField="KeyVal_InternalID_New" Type="String" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="Id" Type="int32" />
                <asp:SessionParameter Name="CreateUser" SessionField="userid" Type="int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:SessionParameter Name="insuId" SessionField="KeyVal_InternalID_New" Type="String" />                
                <asp:SessionParameter Name="contacttype" SessionField="ContactType" Type="string" />
                 <asp:Parameter Name="Id" Type="int32" />

                <asp:Parameter Name="Type" Type="string" />
                <asp:Parameter Name="Address1" Type="string" />
                <asp:Parameter Name="Address2" Type="string" />
                <asp:Parameter Name="Address3" Type="string" />
                <asp:Parameter Name="City" Type="int32" />
                <asp:Parameter Name="area" Type="int32" />
                <asp:Parameter Name="LandMark" Type="string" />
                <asp:Parameter Name="Country" Type="int32" />
                <asp:Parameter Name="State" Type="int32" />
                <asp:Parameter Name="PinCode" Type="string" />
                <asp:Parameter Name="contactperson" Type="string" />
                <asp:Parameter Name="Isdefault" Type="int32" />
                <asp:SessionParameter Name="CreateUser" SessionField="userid" Type="Decimal" />
                <asp:Parameter Name="Id" Type="decimal" />
                <asp:Parameter Name="Phone" Type="string" />
                <asp:Parameter Name="add_Email" Type="string" />
                <asp:Parameter Name="add_Website" Type="string" />
                <asp:Parameter Name="add_Designation" Type="int32" /> 
                <asp:Parameter Name="Address4" Type="string" />
            </UpdateParameters>
            <InsertParameters>
                <asp:SessionParameter Name="insuId" SessionField="KeyVal_InternalID_New" Type="String" />
                <asp:Parameter Name="Type" Type="string" />
                <asp:SessionParameter Name="contacttype" SessionField="ContactType" Type="string" />
                <asp:Parameter Name="Address1" Type="string" />
                <asp:Parameter Name="Address2" Type="string" />
                <asp:Parameter Name="Address3" Type="string" />
                <asp:Parameter Name="City" Type="int32" />
                <asp:Parameter Name="area" Type="int32" />
                <asp:Parameter Name="LandMark" Type="string" />
                <asp:Parameter Name="Country" Type="int32" />
                <asp:Parameter Name="State" Type="int32" />
                <asp:Parameter Name="PinCode" Type="string" />
                <asp:Parameter Name="contactperson" Type="string" />
                <asp:Parameter Name="Isdefault" Type="int32" />
                <asp:Parameter Name="Phone" Type="string" />
                <asp:SessionParameter Name="CreateUser" SessionField="userid" Type="Decimal" />
                <asp:Parameter Name="add_Email" Type="string" />
                 <asp:Parameter Name="add_Website" Type="string" />
                 <asp:Parameter Name="add_Designation" Type="int32" />
                  <asp:Parameter Name="Address4" Type="string" />
            </InsertParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="DesignationSelect" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="SELECT deg_id, deg_designation FROM tbl_master_Designation order by deg_designation"></asp:SqlDataSource>

        <asp:SqlDataSource ID="CountrySelect" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="SELECT cou_id, cou_country as Country FROM tbl_master_country order by cou_country"></asp:SqlDataSource>
        <asp:SqlDataSource ID="StateSelect" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="SELECT s.id as ID,s.state as State from tbl_master_state s where (s.countryId = @State) ORDER BY s.state">
            <SelectParameters>
                <asp:Parameter Name="State" Type="string" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SelectCity" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="SELECT c.city_id AS CityId, c.city_name AS City FROM tbl_master_city c where c.state_id=@City order by c.city_name">
            
            <SelectParameters>
                <asp:Parameter Name="City" Type="string" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SelectArea" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="SELECT area_id = CAST(area_id as int), area_name as areaname from tbl_master_area where (city_id = @Area) ORDER BY area_name">
            <SelectParameters>
                <asp:Parameter Name="Area" Type="string" />
            </SelectParameters>
        </asp:SqlDataSource>
        &nbsp;
   


        <%--debjyoti 02-12-2016--%>
        <asp:SqlDataSource ID="SelectPin" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="select pin_id,pin_code from tbl_master_pinzip where city_id=@City order by pin_code">
            <SelectParameters>
                <asp:Parameter Name="City" Type="string" />
            </SelectParameters>
        </asp:SqlDataSource>
        <%--End Debjyoti 02-12-2016--%>

       
    </div>
  </div>
</div>
</asp:Content>
