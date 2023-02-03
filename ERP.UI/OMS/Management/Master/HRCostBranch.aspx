<%--====================================================== Revision History ==========================================================
Rev Number         DATE              VERSION          DEVELOPER           CHANGES
1.0                02-02-2023        2.0.38           Pallab              breadcumb issue fix
====================================================== Revision History ==========================================================--%>

<%@ Page Title="Branch" Language="C#" AutoEventWireup="true" MasterPageFile="~/OMS/MasterPage/ERP.Master"
    Inherits="ERP.OMS.Management.Master.management_master_HRCostBranch" CodeBehind="HRCostBranch.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">




    <script language="javascript" type="text/javascript">
        function disp_prompt(name) {
            if (name == "tab0") {
                //alert(name);
                document.location.href = "HrCost.aspx";
            }
            if (name == "tab1") {
                //alert(name);
                //document.location.href="HRCostBranch.aspx"; 
            }
            else if (name == "tab2") {
                //alert(name);
                document.location.href = "HRCostEmployee.aspx";
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--rev 1.0--%>
    <%--<div class="panel-heading">
        <div class="panel-title">
            <h3>Cost Centers/Departments Information</h3>
            <div class="crossBtn"><a href="HRCostCenter.aspx"><i class="fa fa-times"></i></a></div>
        </div>

    </div>--%>
    <div class="breadCumb">
        <span>Cost Centers/Departments Information</span>
        <div class="crossBtnN"><a href="HRCostCenter.aspx"><i class="fa fa-times"></i></a></div>
    </div>
    <%--rev end 1.0--%>
    <%--rev 1.0 : container and white bg add--%>
    <div class="container">
        <div class="backBox mt-5 p-3 ">
            <%--rev end 1.0--%>
    <div class="form_main">
        <table class="TableMain100">
            <tr>
                <td>
                    <dxe:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="1" Width="100%"
                        ClientInstanceName="page" Font-Size="12px">
                        <TabPages>
                            <dxe:TabPage Text="Update" Name="Update">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                            <dxe:TabPage Text="Branch" Name="Branch">
                                <ContentCollection>
                                    <dxe:ContentControl runat="server">
                                        <dxe:ASPxGridView ID="BranchGrid" runat="server" KeyFieldName="id" AutoGenerateColumns="False"
                                            DataSourceID="BranchSource" Width="100%">
                                            <Columns>
                                                <dxe:GridViewDataTextColumn FieldName="id" VisibleIndex="0" Visible="False">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="Costcenter" VisibleIndex="0" Width="50%">
                                                </dxe:GridViewDataTextColumn>
                                                <dxe:GridViewDataTextColumn FieldName="CostCenterType" VisibleIndex="1" Width="50%">
                                                </dxe:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsSearchPanel Visible="True" />
                                            <Settings ShowFilterRow="true" ShowFilterRowMenu ="true" />
                                            <Styles>
                                                <LoadingPanel ImageSpacing="10px">
                                                </LoadingPanel>
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                            </Styles>
                                        </dxe:ASPxGridView>
                                    </dxe:ContentControl>
                                </ContentCollection>
                            </dxe:TabPage>
                            <dxe:TabPage Name="Employee" Text="Employee">
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
	                                            else if(activeTab == Tab2)
	                                            {
	                                                disp_prompt('tab2');
	                                            }
	                                            
	                                            }"></ClientSideEvents>
                        <ContentStyle>
                            <Border BorderColor="#002D96" BorderStyle="Solid" BorderWidth="1px" />
                        </ContentStyle>
                        <LoadingPanelStyle ImageSpacing="6px">
                        </LoadingPanelStyle>
                    </dxe:ASPxPageControl>
                </td>
            </tr>
            <tr>
                <td style="height: 8px">
                    <table style="width: 100%;">
                        <tr>
                            <td align="right" style="width: 843px"></td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 843px"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="BranchSource" runat="server" ConnectionString="<%$ ConnectionStrings:crmConnectionString %>"
            SelectCommand="select branch_internalId as id,branch_description as Costcenter,cost_costCenterType as CostCenterType&#13;&#10;from tbl_master_branch,tbl_master_costCenter where branch_internalId =cost_description"></asp:SqlDataSource>
    </div>
          <%--rev 1.0--%>
    </div>
</div>
    <%--rev end 1.0--%>
    
</asp:Content>
