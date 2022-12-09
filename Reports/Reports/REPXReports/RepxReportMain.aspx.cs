using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicLayer;
using System.Configuration;
using System.IO;
using DevExpress.Web;

namespace Reports.Reports.REPXReports
{
    public partial class RepxReportMain : ERP.OMS.ViewState_class.VSPage //System.Web.UI.Page
    {
        public DBEngine oDBEngine = new DBEngine();
        BusinessLogicLayer.ReportLayout rpLayout = new BusinessLogicLayer.ReportLayout();
        BusinessLogicLayer.Converter objConverter = new BusinessLogicLayer.Converter();
        BusinessLogicLayer.DBEngine oDbEngine = new BusinessLogicLayer.DBEngine();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'
                string sPath = HttpContext.Current.Request.Url.ToString();
                oDBEngine.Call_CheckPageaccessebility(sPath);
            }
        }

        protected void Page_Init(object sender,EventArgs e)
        {
            //SqlStructure.ConnectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] filePaths = new string[] { };
                TxtStartDate.EditFormatString = objConverter.GetDateFormat("Date");
                TxtEndDate.EditFormatString = objConverter.GetDateFormat("Date");
                string fDate = null;
                string tDate = null;
                fDate = Session["FinYearStart"].ToString();
                tDate = Session["FinYearEnd"].ToString();
                TxtStartDate.Value = Convert.ToDateTime(fDate); //oDBEngine.GetDate();
                TxtEndDate.Value = Convert.ToDateTime(tDate); //oDBEngine.GetDate();
                if (ConfigurationManager.AppSettings["IsDevelopedZone"] != null || ConfigurationManager.AppSettings["IsActiveDesignMode"] !=null) 
                {
                    btnNewDesign.Visible = true;
                    btnLoadDesign.Visible = true;
                }
                else
                {
                    btnNewDesign.Visible = false;
                    btnLoadDesign.Visible = false;
                }

                if (ConfigurationManager.AppSettings["IsActiveDesignMode"] != null) //For Peekay
                {
                    btnPreview.Visible = false;
                }
                else
                {
                    btnPreview.Visible = true;
                }
                
                String RptModuleName = HttpContext.Current.Request.QueryString["reportname"];
                Session["NewRptModuleName"] = RptModuleName;

                string RptModuleType = "";
                string Module_name = "";
                switch (RptModuleName)
                {
                    //case "StockTrialSumm":
                    //    lblReportTitle.Text = "Report Parameters(Stock Trial Summary)";
                    //    RptModuleType = "StockTrialSumm";
                    //    Module_name = "STOCKTRIALSUMM";
                    //    btnBranch.ClientVisible = true;
                    //    btnProduct.ClientVisible = true;
                    //    btnWarehouse.ClientVisible = false;
                    //    break;                    
                    case "EXAMPLE":
                        lblReportTitle.Text = "Report Parameters(Example)";
                        RptModuleType = "EXAMPLE";
                        Module_name = "EXAMPLE";
                        break;
                    //Mantis Issue 24944
                    case "OrderSummary":
                        lblReportTitle.Text = "Report Parameters(OrderSummary)";
                        RptModuleType = "OrderSummary";
                        Module_name = "OrderSummary";
                        break;
                    //End of Mantis Issue 24944
                }
                //Mantis Issue 24944
                //if (ConfigurationManager.AppSettings["IsActiveDesignMode"] != null && (RptModuleName == "EXAMPLE"))
                if (ConfigurationManager.AppSettings["IsActiveDesignMode"] != null && (RptModuleName == "EXAMPLE" || RptModuleName == "OrderSummary"))
                //End of Mantis Issue 24944
                {
                    btnNewDesign.Visible = true;
                    btnLoadDesign.Visible = true;
                    btnPreview.Visible = true;
                }
                else
                {
                    btnNewDesign.Visible = false;
                    btnLoadDesign.Visible = false;
                    if (RptModuleName == "TRIALONNETBALSUMARY")
                    {
                        btnPreview.Visible = false;
                    }
                    else
                    {
                        btnPreview.Visible = true;
                    }
                }

                Session["Module_Name"] = Module_name;

                if (RptModuleType == "EXAMPLE")
                {
                    filePaths = System.IO.Directory.GetFiles(Server.MapPath("/Reports/RepxReportDesign/Example/DocDesign/Designes"), "*.repx");
                }
                //Mantis Issue 24944
                if (RptModuleType == "OrderSummary")
                {
                    filePaths = System.IO.Directory.GetFiles(Server.MapPath("/Reports/RepxReportDesign/OrderSummary/DocDesign/Designes"), "*.repx");
                }
                //End of Mantis Issue 24944
                //ddReportName.Items.Add("--Select--");
                foreach (string filename in filePaths)
                {
                    string reportname = Path.GetFileNameWithoutExtension(filename);
                    string name = "";
                    if (reportname.Split('~').Length > 1)
                    {
                        name = reportname.Split('~')[0];
                    }
                    else
                    {
                        name = reportname;
                    }
                    string reportValue = reportname;
                    ddReportName.Items.Add(name, reportValue);
                }
                ddReportName.SelectedIndex = 0;

                HDReportModuleName.Value = Convert.ToString(Request.QueryString["reportname"]);
            }
        }

        protected void btnNewDesign_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("~/Reports/REPXReports/ReportDesignerRepx.aspx");
        }
        protected void btnLoadDesign_Click(object sender, EventArgs e)
        {
            string reportName = Convert.ToString(ddReportName.Value);
            RptName.Value = Convert.ToString(ddReportName.Value);
            string ReportModuleNM = Convert.ToString(Request.QueryString["reportname"]);
            string Module_Name = Convert.ToString(Request.QueryString["Module_Name"]);

            if (reportName == "--Select--")
            {
                return;
            }
            HttpContext.Current.Response.Redirect("~/Reports/REPXReports/ReportDesignerRepx.aspx?LoadrptName=" + reportName + "&&reportname=" + Convert.ToString(Request.QueryString["reportname"]));
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            string StartDate = string.Empty;
            string EndDate = string.Empty;
            string reportName = Convert.ToString(ddReportName.Value);
            string ReportModuleNM = Convert.ToString(Request.QueryString["reportname"]);
            if (reportName == "--Select--")
            {
                return;
            }
            //string SelectedBranchList = "";
            //string SelectedUserList = "";
            //string SelectedPartyList = "";
            //Session["ButtonBranch"] = "Branch";
            //Session["ButtonUser"] = "User";
            //Session["ButtonBank"] = "Bank";
            //Session["ButtonCash"] = "Cash";
            //string ZeroBalProduct = "";
            //if (chkZeroBal.Checked == true)
            //{
            //    ZeroBalProduct = "Y";
            //}
            //else
            //{
            //    ZeroBalProduct = "N";
            //}
            //Session["chkZeroBalProduct"] = ZeroBalProduct;

            //if ((ReportModuleNM == "StockTrialSumm" || ReportModuleNM == "StockTrialDet" || ReportModuleNM == "StockTrialProd" || ReportModuleNM == "StockTrial1" ||
            //   ReportModuleNM == "LedgerPost" || ReportModuleNM == "BankBook" || ReportModuleNM == "CashBook" || ReportModuleNM == "BRSSTATEMENT") && Convert.ToString(Session["ButtonBranch"]) == "Branch")
            //{
            //    List<object> docList = grid_Branch.GetSelectedFieldValues("ID");
            //    foreach (object Dobj in docList)
            //    {
            //        SelectedBranchList += "," + Dobj;
            //    }
            //    SelectedBranchList = SelectedBranchList.TrimStart(',');
            //    if (SelectedBranchList.Trim() == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), UniqueID, "jAlert('Please Select Some Document(s)')", true);
            //        Session["SelectedBranchList"] = Convert.ToString(Session["userbranchHierarchy"]);
            //    }
            //    else
            //    {
            //        Session["SelectedBranchList"] = SelectedBranchList;
            //    }
            //}
            //if ((ReportModuleNM == "BankBook" || ReportModuleNM == "CashBook") && Convert.ToString(Session["ButtonUser"]) == "User")
            //{
            //    Session["SelectedTagPartyList"] = "";
            //    Session["SelectedTagLedgerList"] = "";
            //    Session["SelectedTagEmployeeList"] = "";
            //    Session["SelectedTagProductList"] = "";
            //    Session["SelectedTagWarehouseList"] = "";
            //    List<object> docList = grid_User.GetSelectedFieldValues("ID");
            //    foreach (object Dobj in docList)
            //    {
            //        SelectedUserList += "," + Dobj;
            //    }
            //    SelectedUserList = SelectedUserList.TrimStart(',');
            //    if (SelectedUserList.Trim() == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), UniqueID, "jAlert('Please Select Some Document(s)')", true);
            //        Session["SelectedUserList"] = "";
            //    }
            //    else
            //    {
            //        Session["SelectedUserList"] = SelectedUserList;
            //    }
            //}
            //if ((ReportModuleNM == "BankBook" || ReportModuleNM == "BRSSTATEMENT" || ReportModuleNM == "BRSCONSSTATEMENT") && Convert.ToString(Session["ButtonBank"]) == "Bank")
            //{
            //    Session["SelectedTagPartyList"] = "";
            //    Session["SelectedTagLedgerList"] = "";
            //    Session["SelectedTagEmployeeList"] = "";
            //    Session["SelectedTagProductList"] = "";
            //    Session["SelectedTagWarehouseList"] = "";
            //    string SelectedCashBankList = "";
            //    if (string.IsNullOrEmpty(hdnBankValue.Value))
            //    {
            //        List<object> docList = grid_Bank.GetSelectedFieldValues("ID");
            //        foreach (object Dobj in docList)
            //        {
            //            SelectedCashBankList += "," + Dobj;
            //        }
            //    }
            //    else
            //    {
            //        SelectedCashBankList = hdnBankValue.Value;
            //    }
            //    SelectedCashBankList = SelectedCashBankList.TrimStart(',');
            //    if (SelectedCashBankList.Trim() == "")
            //    {
            //        grid_Bank.JSProperties["cpBlankBankSelection"] = "BlankBankSelection";
            //    }
            //    else
            //    {
            //        Session["SelectedCashBankList"] = SelectedCashBankList;
            //    }
            //}
            //if (ReportModuleNM == "CashBook" && Convert.ToString(Session["ButtonCash"]) == "Cash")
            //{
            //    Session["SelectedTagPartyList"] = "";
            //    Session["SelectedTagLedgerList"] = "";
            //    Session["SelectedTagEmployeeList"] = "";
            //    Session["SelectedTagProductList"] = "";
            //    Session["SelectedTagWarehouseList"] = "";
            //    string SelectedCashBankList = "";
            //    List<object> docList = grid_Cash.GetSelectedFieldValues("ID");
            //    foreach (object Dobj in docList)
            //    {
            //        SelectedCashBankList += "," + Dobj;
            //    }
            //    SelectedCashBankList = SelectedCashBankList.TrimStart(',');
            //    if (SelectedCashBankList.Trim() == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), UniqueID, "jAlert('Please Select Some Document(s)')", true);
            //        Session["SelectedCashBankList"] = "";
            //    }
            //    else
            //    {
            //        Session["SelectedCashBankList"] = SelectedCashBankList;
            //    }
            //}            
            //else if (ReportModuleNM == "Proforma1")
            //{
            //    List<object> docList = grid_Documents.GetSelectedFieldValues("ID");
            //    foreach (object Dobj in docList)
            //    {
            //        SelectedBranchList += "," + Dobj;
            //    }
            //}
            //else if (ReportModuleNM == "PAYSLIP")
            //{
            //    Session["StructureID_Report"] = CmbStructure.Value;
            //}
            StartDate = Convert.ToDateTime(TxtStartDate.Value).ToString("yyyy-MM-dd");
            EndDate = Convert.ToDateTime(TxtEndDate.Value).ToString("yyyy-MM-dd");
            HttpContext.Current.Response.Redirect("~/Reports/REPXReports/RepxReportViewer.aspx?Previewrpt=" + reportName + "&&StartDate=" + StartDate + "&&EndDate=" + EndDate + "&&reportname=" + Convert.ToString(Request.QueryString["reportname"]));
        }

        protected void btnNewFileSave_Click(object sender, EventArgs e)
        {
            string reportName = txtFileName.Text;
            HttpContext.Current.Response.Redirect("~/Reports/REPXReports/ReportDesignerRepx.aspx?NewReport=" + reportName + "&&reportname=" + Convert.ToString(Request.QueryString["reportname"]));
        }

        //protected void cgridDocuments_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string strSplitCommand = e.Parameters.Split('~')[0];
        //    if (strSplitCommand == "BindDocumentsDetails")
        //    {
        //        DataTable dtdata = GetDocumentListGridData();
        //        if (dtdata != null && dtdata.Rows.Count > 0)
        //        {
        //            Session["ReportMain_DocumentList"] = dtdata;
        //            grid_Documents.DataBind();
        //        }
        //        else
        //        {
        //            grid_Documents.DataSource = null;
        //            grid_Documents.DataBind();
        //        }
        //    }
        //    else if (strSplitCommand == "SelectAndDeSelectDocuments")
        //    {
        //        ASPxGridView gv = sender as ASPxGridView;
        //        string command = e.Parameters.ToString();
        //        string State = Convert.ToString(e.Parameters.Split('~')[1]);
        //        if (State == "SelectAll")
        //        {
        //            gv.Selection.SelectAll();
        //        }
        //        if (State == "UnSelectAll")
        //        {
        //            gv.Selection.UnselectAll();
        //        }
        //        if (State == "Revart")
        //        {
        //            for (int i = 0; i < gv.VisibleRowCount; i++)
        //            {
        //                if (gv.Selection.IsRowSelected(i))
        //                    gv.Selection.UnselectAll();
        //                else
        //                    gv.Selection.SelectAll();
        //            }

        //        }
        //    }
        //    else
        //    {
        //        string SelectedDocList = "";

        //        List<object> docList = grid_Documents.GetSelectedFieldValues("ID");
        //        foreach (object Dobj in docList)
        //        {
        //            SelectedDocList += "," + Dobj;
        //        }
        //        SelectedDocList = SelectedDocList.TrimStart(',');
        //        if (SelectedDocList.Trim() == "")
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), SelectedDocList, "jAlert('Please Select Some Document(s)')", true);
        //        }
        //        else
        //        {
        //            Session["SelectedDocumentList"] = SelectedDocList;
        //        }
        //    }
        //}

        //protected void cgridBranch_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string userbranch = "";
        //    Session["ButtonName"] = "Branch";
        //    if (Session["userbranchHierarchy"] != null)
        //    {
        //        userbranch = Convert.ToString(Session["userbranchHierarchy"]);
        //    }
        //    string strSplitCommand = e.Parameters.Split('~')[0];
        //    if (strSplitCommand == "BindDocumentsDetails")
        //    {
        //        DataTable dtdata = GetDocumentListGridData();
        //        if (dtdata != null && dtdata.Rows.Count > 0)
        //        {
        //            Session["ReportMain_BranchList"] = dtdata;
        //            grid_Branch.DataBind();
        //        }
        //        else
        //        {
        //            grid_Branch.DataSource = null;
        //            grid_Branch.DataBind();
        //        }
        //    }
        //    else if (strSplitCommand == "SelectAndDeSelectDocuments")
        //    {
        //        ASPxGridView gv = sender as ASPxGridView;
        //        string command = e.Parameters.ToString();
        //        string State = Convert.ToString(e.Parameters.Split('~')[1]);
        //        if (State == "SelectAll")
        //        {
        //            gv.Selection.SelectAll();
        //        }
        //        if (State == "UnSelectAll")
        //        {
        //            gv.Selection.UnselectAll();
        //        }
        //        if (State == "Revart")
        //        {
        //            for (int i = 0; i < gv.VisibleRowCount; i++)
        //            {
        //                if (gv.Selection.IsRowSelected(i))
        //                    gv.Selection.UnselectAll();
        //                else
        //                    gv.Selection.SelectAll();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        string SelectedTagBranchList = "";

        //        List<object> docList = grid_Branch.GetSelectedFieldValues("ID");
        //        foreach (object Dobj in docList)
        //        {
        //            SelectedTagBranchList += "," + Dobj;
        //        }
        //        SelectedTagBranchList = SelectedTagBranchList.TrimStart(',');
        //        if (SelectedTagBranchList.Trim() == "")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Abc", "Alert('Please Select Some Document(s)')", true);
        //        }
        //        else
        //        {
        //            Session["SelectedTagBranchList"] = SelectedTagBranchList;
        //        }
        //    }
        //}

        //protected void cgridUser_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string userbranch = "";
        //    Session["ButtonName"] = "User";
        //    if (Session["userbranchHierarchy"] != null)
        //    {
        //        userbranch = Convert.ToString(Session["userbranchHierarchy"]);
        //    }
        //    string strSplitCommand = e.Parameters.Split('~')[0];
        //    if (strSplitCommand == "BindDocumentsDetails")
        //    {
        //        DataTable dtdata = GetDocumentListGridData();
        //        if (dtdata != null && dtdata.Rows.Count > 0)
        //        {
        //            Session["ReportMain_UserList"] = dtdata;
        //            grid_User.DataBind();
        //        }
        //        else
        //        {
        //            grid_User.DataSource = null;
        //            grid_User.DataBind();
        //        }
        //    }
        //    else if (strSplitCommand == "SelectAndDeSelectDocuments")
        //    {
        //        ASPxGridView gv = sender as ASPxGridView;
        //        string command = e.Parameters.ToString();
        //        string State = Convert.ToString(e.Parameters.Split('~')[1]);
        //        if (State == "SelectAll")
        //        {
        //            gv.Selection.SelectAll();
        //        }
        //        if (State == "UnSelectAll")
        //        {
        //            gv.Selection.SelectAll();
        //        }
        //        if (State == "Revart")
        //        {
        //            for (int i = 0; i < gv.VisibleRowCount; i++)
        //            {
        //                if (gv.Selection.IsRowSelected(i))
        //                    gv.Selection.UnselectRow(i);
        //                else
        //                    gv.Selection.SelectRow(i);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        string SelectedUserList = "";

        //        List<object> docList = grid_User.GetSelectedFieldValues("ID");
        //        foreach (object Dobj in docList)
        //        {
        //            SelectedUserList += "," + Dobj;
        //        }
        //        SelectedUserList = SelectedUserList.TrimStart(',');
        //        if (SelectedUserList.Trim() == "")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Abc", "Alert('Please Select Some Document(s)')", true);
        //        }
        //        else
        //        {
        //            Session["SelectedUserList"] = SelectedUserList;
        //        }
        //    }
        //}

        //protected void cgridBank_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string userbranch = "";
        //    Session["ButtonName"] = "Bank";
        //    if (Session["userbranchHierarchy"] != null)
        //    {
        //        userbranch = Convert.ToString(Session["userbranchHierarchy"]);
        //    }
        //    string strSplitCommand = e.Parameters.Split('~')[0];
        //    if (strSplitCommand == "BindDocumentsDetails")
        //    {
        //        DataTable dtdata = GetDocumentListGridData();
        //        if (dtdata != null && dtdata.Rows.Count > 0)
        //        {
        //            Session["ReportMain_BankList"] = dtdata;
        //            grid_Bank.DataBind();
        //        }
        //        else
        //        {
        //            grid_Bank.DataSource = null;
        //            grid_Bank.DataBind();
        //        }
        //    }
        //    else if (strSplitCommand == "SelectAndDeSelectDocuments")
        //    {
        //        ASPxGridView gv = sender as ASPxGridView;
        //        string command = e.Parameters.ToString();
        //        string State = Convert.ToString(e.Parameters.Split('~')[1]);
        //        if (State == "SelectAll")
        //        {
        //            gv.Selection.SelectAll();
        //        }
        //        if (State == "UnSelectAll")
        //        {
        //            gv.Selection.SelectAll();
        //        }
        //        if (State == "Revart")
        //        {
        //            for (int i = 0; i < gv.VisibleRowCount; i++)
        //            {
        //                if (gv.Selection.IsRowSelected(i))
        //                    gv.Selection.UnselectRow(i);
        //                else
        //                    gv.Selection.SelectRow(i);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        string SelectedBankList = "";

        //        List<object> docList = grid_Bank.GetSelectedFieldValues("ID");
        //        foreach (object Dobj in docList)
        //        {
        //            SelectedBankList += "," + Dobj;
        //        }
        //        SelectedBankList = SelectedBankList.TrimStart(',');
        //        if (SelectedBankList.Trim() == "")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Abc", "Alert('Please Select Some Document(s)')", true);
        //        }
        //        else
        //        {
        //            Session["SelectedBankList"] = SelectedBankList;
        //        }
        //    }
        //}

        //protected void cgridCash_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string userbranch = "";
        //    Session["ButtonName"] = "Cash";
        //    if (Session["userbranchHierarchy"] != null)
        //    {
        //        userbranch = Convert.ToString(Session["userbranchHierarchy"]);
        //    }
        //    string strSplitCommand = e.Parameters.Split('~')[0];
        //    if (strSplitCommand == "BindDocumentsDetails")
        //    {
        //        DataTable dtdata = GetDocumentListGridData();
        //        if (dtdata != null && dtdata.Rows.Count > 0)
        //        {
        //            Session["ReportMain_CashList"] = dtdata;
        //            grid_Cash.DataBind();
        //        }
        //        else
        //        {
        //            grid_Cash.DataSource = null;
        //            grid_Cash.DataBind();
        //        }
        //    }
        //    else if (strSplitCommand == "SelectAndDeSelectDocuments")
        //    {
        //        ASPxGridView gv = sender as ASPxGridView;
        //        string command = e.Parameters.ToString();
        //        string State = Convert.ToString(e.Parameters.Split('~')[1]);
        //        if (State == "SelectAll")
        //        {
        //            gv.Selection.SelectAll();
        //        }
        //        if (State == "UnSelectAll")
        //        {
        //            gv.Selection.SelectAll();
        //        }
        //        if (State == "Revart")
        //        {
        //            for (int i = 0; i < gv.VisibleRowCount; i++)
        //            {
        //                if (gv.Selection.IsRowSelected(i))
        //                    gv.Selection.UnselectRow(i);
        //                else
        //                    gv.Selection.SelectRow(i);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        string SelectedCashList = "";

        //        List<object> docList = grid_Cash.GetSelectedFieldValues("ID");
        //        foreach (object Dobj in docList)
        //        {
        //            SelectedCashList += "," + Dobj;
        //        }
        //        SelectedCashList = SelectedCashList.TrimStart(',');
        //        if (SelectedCashList.Trim() == "")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Abc", "Alert('Please Select Some Document(s)')", true);
        //        }
        //        else
        //        {
        //            Session["SelectedCashList"] = SelectedCashList;
        //        }
        //    }
        //}

        //protected void cgridDocDesc_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string userbranch = "";
        //    if (Session["userbranchHierarchy"] != null)
        //    {
        //        userbranch = Convert.ToString(Session["userbranchHierarchy"]);
        //    }
        //    string strSplitCommand = e.Parameters.Split('~')[0];
        //    if (strSplitCommand == "BindDocumentsDetails")
        //    {
        //        string DocumentType = Convert.ToString(e.Parameters.Split('~')[1]);
        //        Session["DocumentType"] = DocumentType;
        //        if (DocumentType == "Party")
        //        {
        //            Session["ButtonName"] = "Party";
        //            grid_DocDesc.Columns[3].Visible = false;
        //        }
        //        else if (DocumentType == "Ledger")
        //        {
        //            Session["ButtonName"] = "Ledger";
        //        }
        //        else if (DocumentType == "Employee")
        //        {
        //            Session["ButtonName"] = "Employee";
        //            grid_DocDesc.Columns[3].Visible = false;
        //        }
        //        else if (DocumentType == "Product")
        //        {
        //            Session["ButtonName"] = "Product";
        //        }
        //        else
        //        {
        //            Session["ButtonName"] = "Warehouse";
        //        }
        //        DataTable dtdata = GetDocumentListGridData();
        //        Session["ReportMain_DocDescList"] = dtdata;
        //        grid_DocDesc.DataSource = dtdata;
        //        grid_DocDesc.DataBind();
        //    }
        //    else if (strSplitCommand == "SelectAndDeSelectDocuments")
        //    {
        //        ASPxGridView gv = sender as ASPxGridView;
        //        string command = e.Parameters.ToString();
        //        string State = Convert.ToString(e.Parameters.Split('~')[1]);
        //        if (State == "SelectAll")
        //        {
        //            gv.Selection.SelectAll();
        //            if (Convert.ToString(Session["DocumentType"]) == "Party")
        //            {
        //                gv.Columns[3].Visible = false;
        //                gv.Columns[5].Visible = false;
        //                gv.Columns[3].Caption = "Party Code";
        //                gv.Columns[4].Caption = "Party Name";
        //            }
        //            else if (Convert.ToString(Session["DocumentType"]) == "Ledger")
        //            {
        //                gv.Columns[3].Caption = "Ledger Code";
        //                gv.Columns[4].Caption = "Description";
        //                gv.Columns[5].Visible = false;
        //            }
        //            else if (Convert.ToString(Session["DocumentType"]) == "Employee")
        //            {
        //                gv.Columns[3].Caption = "Code";
        //                gv.Columns[3].Visible = false;
        //                gv.Columns[4].Caption = "SubLedger";
        //                gv.Columns[5].Caption = "Type";
        //            }
        //            else if (Convert.ToString(Session["DocumentType"]) == "Product")
        //            {
        //                gv.Columns[3].Caption = "Product Name";
        //                gv.Columns[4].Caption = "Description";
        //                gv.Columns[5].Visible = false;
        //            }
        //            else
        //            {
        //                gv.Columns[3].Visible = false;
        //                gv.Columns[4].Caption = "Warehouse";
        //                gv.Columns[5].Visible = false;
        //            }

        //        }
        //        if (State == "UnSelectAll")
        //        {
        //            gv.Selection.UnselectAll();
        //            if (Convert.ToString(Session["DocumentType"]) == "Party")
        //            {
        //                gv.Columns[3].Visible = false;
        //                gv.Columns[5].Visible = false;
        //                gv.Columns[3].Caption = "Party Code";
        //                gv.Columns[4].Caption = "Party Name";
        //            }
        //            else if (Convert.ToString(Session["DocumentType"]) == "Ledger")
        //            {
        //                gv.Columns[3].Caption = "Ledger Code";
        //                gv.Columns[4].Caption = "Description";
        //                gv.Columns[5].Visible = false;
        //            }
        //            else if (Convert.ToString(Session["DocumentType"]) == "Employee")
        //            {
        //                gv.Columns[3].Caption = "Code";
        //                gv.Columns[3].Visible = false;
        //                gv.Columns[4].Caption = "SubLedger";
        //                gv.Columns[5].Caption = "Type";
        //            }
        //            else if (Convert.ToString(Session["DocumentType"]) == "Product")
        //            {
        //                gv.Columns[3].Caption = "Product Name";
        //                gv.Columns[4].Caption = "Description";
        //                gv.Columns[5].Visible = false;
        //            }
        //            else
        //            {
        //                gv.Columns[3].Visible = false;
        //                gv.Columns[4].Caption = "Warehouse";
        //                gv.Columns[5].Visible = false;
        //            }
        //        }
        //        if (State == "Revart")
        //        {
        //            for (int i = 0; i < gv.VisibleRowCount; i++)
        //            {
        //                if (gv.Selection.IsRowSelected(i))
        //                    gv.Selection.UnselectRow(i);
        //                else
        //                    gv.Selection.SelectRow(i);
        //            }
        //            if (Convert.ToString(Session["DocumentType"]) == "Party")
        //            {
        //                gv.Columns[3].Visible = false;
        //                gv.Columns[5].Visible = false;
        //                gv.Columns[3].Caption = "Party Code";
        //                gv.Columns[4].Caption = "Party Name";
        //            }
        //            else if (Convert.ToString(Session["DocumentType"]) == "Ledger")
        //            {
        //                gv.Columns[3].Caption = "Ledger Code";
        //                gv.Columns[4].Caption = "Description";
        //                gv.Columns[5].Visible = false;
        //            }
        //            else if (Convert.ToString(Session["DocumentType"]) == "Employee")
        //            {
        //                gv.Columns[3].Caption = "Code";
        //                gv.Columns[3].Visible = false;
        //                gv.Columns[4].Caption = "SubLedger";
        //                gv.Columns[5].Caption = "Type";
        //            }
        //            else if (Convert.ToString(Session["DocumentType"]) == "Product")
        //            {
        //                gv.Columns[3].Caption = "Product Name";
        //                gv.Columns[4].Caption = "Description";
        //                gv.Columns[5].Visible = false;
        //            }
        //            else
        //            {
        //                gv.Columns[3].Visible = false;
        //                gv.Columns[4].Caption = "Warehouse";
        //                gv.Columns[5].Visible = false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (Convert.ToString(Session["DocumentType"]) == "Party")
        //        {
        //            string SelectedTagPartyList = "";
        //            List<object> docList = grid_DocDesc.GetSelectedFieldValues("Doc Code");
        //            foreach (object Dobj in docList)
        //            {
        //                SelectedTagPartyList += "," + Dobj;
        //            }
        //            SelectedTagPartyList = SelectedTagPartyList.TrimStart(',');
        //            if (SelectedTagPartyList.Trim() == "")
        //            {
        //                Session["SelectedTagPartyList"] = "";
        //                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Abc", "Alert('Please Select Some Document(s)')", true);
        //            }
        //            else
        //            {
        //                Session["SelectedTagPartyList"] = SelectedTagPartyList;
        //            }
        //        }
        //        else if (Convert.ToString(Session["DocumentType"]) == "Ledger")
        //        {
        //            string SelectedTagLedgerList = "";
        //            List<object> docList = grid_DocDesc.GetSelectedFieldValues("ID");
        //            foreach (object Dobj in docList)
        //            {
        //                SelectedTagLedgerList += "," + Dobj;
        //            }
        //            SelectedTagLedgerList = SelectedTagLedgerList.TrimStart(',');
        //            if (SelectedTagLedgerList.Trim() == "")
        //            {
        //                Session["SelectedTagLedgerList"] = "";
        //                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Abc", "Alert('Please Select Some Document(s)')", true);
        //            }
        //            else
        //            {
        //                Session["SelectedTagLedgerList"] = SelectedTagLedgerList;
        //            }
        //        }
        //        else if (Convert.ToString(Session["DocumentType"]) == "Employee")
        //        {
        //            string SelectedTagemployeeList = "";
        //            List<object> docList = grid_DocDesc.GetSelectedFieldValues("Doc Code");
        //            foreach (object Dobj in docList)
        //            {
        //                SelectedTagemployeeList += "," + Dobj;
        //            }
        //            SelectedTagemployeeList = SelectedTagemployeeList.TrimStart(',');
        //            if (SelectedTagemployeeList.Trim() == "")
        //            {
        //                Session["SelectedTagemployeeList"] = "";
        //                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Abc", "Alert('Please Select Some Document(s)')", true);
        //            }
        //            else
        //            {
        //                Session["SelectedTagemployeeList"] = SelectedTagemployeeList;
        //            }
        //        }
        //        else if (Convert.ToString(Session["DocumentType"]) == "Product")
        //        {
        //            string SelectedTagProductList = "";
        //            List<object> docList = grid_DocDesc.GetSelectedFieldValues("ID");
        //            foreach (object Dobj in docList)
        //            {
        //                SelectedTagProductList += "," + Dobj;
        //            }
        //            SelectedTagProductList = SelectedTagProductList.TrimStart(',');
        //            if (SelectedTagProductList.Trim() == "")
        //            {
        //                Session["SelectedTagProductList"] = "";
        //                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Abc", "Alert('Please Select Some Document(s)')", true);
        //            }
        //            else
        //            {
        //                Session["SelectedTagProductList"] = SelectedTagProductList;
        //            }
        //        }
        //        else
        //        {
        //            string SelectedTagWarehouseList = "";
        //            List<object> docList = grid_DocDesc.GetSelectedFieldValues("ID");
        //            foreach (object Dobj in docList)
        //            {
        //                SelectedTagWarehouseList += "," + Dobj;
        //            }
        //            SelectedTagWarehouseList = SelectedTagWarehouseList.TrimStart(',');
        //            if (SelectedTagWarehouseList.Trim() == "")
        //            {
        //                Session["SelectedTagWarehouseList"] = "";
        //                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Abc", "Alert('Please Select Some Document(s)')", true);
        //            }
        //            else
        //            {
        //                Session["SelectedTagWarehouseList"] = SelectedTagWarehouseList;
        //            }
        //        }
        //    }
        //}

        //protected void gridDocDesc_DataBound(object sender, EventArgs e)
        //{
        //    if (Session["ReportMain_DocDescList"] != null)
        //    {
        //        grid_DocDesc.DataSource = Session["ReportMain_DocDescList"];
        //        if (Convert.ToString(Session["DocumentType"]) == "Party")
        //        {
        //            grid_DocDesc.Columns[3].Visible = false;
        //            grid_DocDesc.Columns[5].Visible = false;
        //            grid_DocDesc.Columns[3].Caption = "Party Code";
        //            grid_DocDesc.Columns[4].Caption = "Party Name";
        //        }
        //        else if (Convert.ToString(Session["DocumentType"]) == "Ledger")
        //        {
        //            grid_DocDesc.Columns[3].Caption = "Ledger Code";
        //            grid_DocDesc.Columns[4].Caption = "Description";
        //            grid_DocDesc.Columns[5].Visible = false;
        //        }
        //        else if (Convert.ToString(Session["DocumentType"]) == "Employee")
        //        {
        //            grid_DocDesc.Columns[3].Caption = "Code";
        //            grid_DocDesc.Columns[3].Visible = false;
        //            grid_DocDesc.Columns[4].Caption = "SubLedger";
        //            grid_DocDesc.Columns[5].Caption = "Type";
        //        }
        //        else if (Convert.ToString(Session["DocumentType"]) == "Product")
        //        {
        //            grid_DocDesc.Columns[3].Caption = "Product Name";
        //            grid_DocDesc.Columns[4].Caption = "Description";
        //            grid_DocDesc.Columns[5].Visible = false;
        //        }
        //        else
        //        {
        //            grid_DocDesc.Columns[3].Visible = false;
        //            grid_DocDesc.Columns[4].Caption = "Warehouse";
        //            grid_DocDesc.Columns[5].Visible = false;
        //        }
        //    }
        //}
//        public DataTable GetDocumentListGridData()
//        {
//            string query = "";
//            string ReportModule = string.Empty;
//            ReportModule = Convert.ToString(Request.QueryString["reportname"]);
//            if ((ReportModule == "StockTrialSumm" || ReportModule == "StockTrialDet" || ReportModule == "StockTrialProd" || ReportModule == "StockTrialWH" || ReportModule == "StockTrial1" ||
//                ReportModule == "LedgerPost" || ReportModule == "BankBook" || ReportModule == "CashBook" || ReportModule == "BRSSTATEMENT") && Convert.ToString(Session["ButtonName"]) == "Branch")
//            {
//                Session.Remove("ButtonName");
//                query = @"Select ROW_NUMBER() over(order by branch_id) SrlNo, branch_id AS ID, branch_code as Doc_Code, branch_description as Description from tbl_master_branch WHERE branch_id IN(" + Convert.ToString(Session["userbranchHierarchy"]) + ") ORDER BY branch_description";
//            }
//            else if ((ReportModule == "BankBook" || ReportModule == "CashBook") && Convert.ToString(Session["ButtonName"]) == "User")
//            {
//                Session.Remove("ButtonName");
//                if (Convert.ToString(Session["SelectedTagBranchList"]).Trim() != "")
//                    query = @"Select ROW_NUMBER() over(order by user_id) SrlNo, user_id AS ID, user_name as 'User' from tbl_master_user WHERE user_branchId IN(" + Convert.ToString(Session["SelectedTagBranchList"]) + ") ORDER BY user_name";
//                else
//                    query = "select top 0  null as  'SrlNo',0 as 'ID',0 as 'User' ";
//            }
//            else if ((ReportModule == "BankBook" || ReportModule == "BRSSTATEMENT") && Convert.ToString(Session["ButtonName"]) == "Bank")
//            {
//                Session.Remove("ButtonName");
//                if (Convert.ToString(Session["SelectedTagBranchList"]).Trim() != "")
//                    query = @"Select ROW_NUMBER() over(order by MainAccount_ReferenceID) SrlNo, MainAccount_ReferenceID AS ID, MainAccount_Name as 'Bank Name' 
//                    from Master_MainAccount WHERE MainAccount_BankCashType='Bank' AND MainAccount_branchId IN(" + Convert.ToString(Session["SelectedTagBranchList"]) + ")  " +
//                    "union ALL " +
//                    "Select ROW_NUMBER() over(order by MainAccount_ReferenceID) SrlNo, MainAccount_ReferenceID AS ID, MainAccount_Name as 'Bank Name' " +
//                    "from Master_MainAccount WHERE MainAccount_BankCashType='Bank' AND MainAccount_branchId= 0 and " +
//                    "not exists(select 1 from tbl_master_ledgerBranch_map where MainAccount_id =MainAccount_ReferenceID) " +
//                    "union ALL " +
//                    "Select ROW_NUMBER() over(order by MainAccount_ReferenceID) SrlNo, MainAccount_ReferenceID AS ID, MainAccount_Name as 'Bank Name' " +
//                    "from Master_MainAccount WHERE MainAccount_BankCashType='Bank' AND MainAccount_branchId= 0 and " +
//                    "exists(select 1 from tbl_master_ledgerBranch_map where MainAccount_id =MainAccount_ReferenceID and branch_id IN(" + Convert.ToString(Session["SelectedTagBranchList"]) + "))";
//                else
//                    query = "select top 0 null as 'SrlNo',0 as 'ID',0 as 'Bank Name' ";
//            }
//            else if ((ReportModule == "BRSCONSSTATEMENT") && Convert.ToString(Session["ButtonName"]) == "Bank")
//            {
//                Session.Remove("ButtonName");
//                query = @"Select ROW_NUMBER() over(order by MainAccount_ReferenceID) SrlNo, MainAccount_ReferenceID AS ID, MainAccount_Name as 'Bank Name' 
//                from Master_MainAccount WHERE MainAccount_BankCashType='Bank' " +
//                "union ALL " +
//                "Select ROW_NUMBER() over(order by MainAccount_ReferenceID) SrlNo, MainAccount_ReferenceID AS ID, MainAccount_Name as 'Bank Name' " +
//                "from Master_MainAccount WHERE MainAccount_BankCashType='Bank' AND MainAccount_branchId= 0 and " +
//                "not exists(select 1 from tbl_master_ledgerBranch_map where MainAccount_id =MainAccount_ReferenceID) " +
//                "union ALL " +
//                "Select ROW_NUMBER() over(order by MainAccount_ReferenceID) SrlNo, MainAccount_ReferenceID AS ID, MainAccount_Name as 'Bank Name' " +
//                "from Master_MainAccount WHERE MainAccount_BankCashType='Bank' AND MainAccount_branchId= 0 and " +
//                "exists(select 1 from tbl_master_ledgerBranch_map where MainAccount_id =MainAccount_ReferenceID) ";
//            }
//            else if (ReportModule == "CashBook" && Convert.ToString(Session["ButtonName"]) == "Cash")
//            {
//                Session.Remove("ButtonName");
//                if (Convert.ToString(Session["SelectedTagBranchList"]).Trim() != "")
//                    query = @"Select ROW_NUMBER() over(order by MainAccount_ReferenceID) SrlNo, MainAccount_ReferenceID AS ID, MainAccount_Name as 'Cash' 
//                    from Master_MainAccount WHERE MainAccount_BankCashType='Cash' AND MainAccount_branchId IN(" + Convert.ToString(Session["SelectedTagBranchList"]) + ")  " +
//                    "union ALL " +
//                    "Select ROW_NUMBER() over(order by MainAccount_ReferenceID) SrlNo, MainAccount_ReferenceID AS ID, MainAccount_Name as 'Cash' " +
//                    "from Master_MainAccount WHERE MainAccount_BankCashType='Cash' AND MainAccount_branchId= 0 and " +
//                    "not exists(select 1 from tbl_master_ledgerBranch_map where MainAccount_id =MainAccount_ReferenceID) " +
//                    "union ALL " +
//                    "Select ROW_NUMBER() over(order by MainAccount_ReferenceID) SrlNo, MainAccount_ReferenceID AS ID, MainAccount_Name as 'Cash' " +
//                    "from Master_MainAccount WHERE MainAccount_BankCashType='Cash' AND MainAccount_branchId= 0 and " +
//                    "exists(select 1 from tbl_master_ledgerBranch_map where MainAccount_id =MainAccount_ReferenceID and branch_id IN(" + Convert.ToString(Session["SelectedTagBranchList"]) + "))";
//                else
//                    query = "select top 0 null as 'SrlNo',0 as 'ID',0 as 'Bank Name' ";
//            }
//            else if (ReportModule == "LedgerPost" && Convert.ToString(Session["ButtonName"]) == "Party")
//            {
//                Session.Remove("ButtonName");
//                if (Convert.ToString(Session["SelectedTagBranchList"]).Trim() != "")
//                    query = @"select ROW_NUMBER() over(order by cnt_id) SrlNo, cnt_id AS ID,cnt_internalId as 'Doc Code',ISNULL(cnt_firstName,'')+' '+ISNULL(cnt_middleName,'')+'  '+ISNULL(cnt_lastName,'') as 'Description','' AS Type 
//                    FROM tbl_master_contact WHERE cnt_contactType in('CL','DV') and cnt_branchid IN(" + Convert.ToString(Session["SelectedTagBranchList"]) + ") ORDER BY ISNULL(cnt_firstName,'')+' '+ISNULL(cnt_middleName,'')+' '+ISNULL(cnt_lastName,'') ";
//                else
//                    query = "select top 0 null as 'SrlNo',0 as 'ID',null as 'Doc Code',null as 'Description' ";
//            }
//            else if (ReportModule == "LedgerPost" && Convert.ToString(Session["ButtonName"]) == "Ledger")
//            {
//                Session.Remove("ButtonName");
//                if (Convert.ToString(Session["SelectedTagBranchList"]).Trim() != "")
//                {
//                    query = @"SELECT * FROM ( 
//                    select ROW_NUMBER() over(order by A.MainAccount_ReferenceID) SrlNo, A.MainAccount_ReferenceID AS ID,A.MainAccount_AccountCode AS 'Doc Code',A.MainAccount_Name AS 'Description','' AS Type  
//                    FROM Master_MainAccount A WHERE A.MainAccount_branchId in(0,'')
//                    UNION ALL 
//                    select ROW_NUMBER() over(order by A.MainAccount_ReferenceID) SrlNo, A.MainAccount_ReferenceID AS ID,A.MainAccount_AccountCode AS 'Doc Code',A.MainAccount_Name AS 'Description','' AS Type  
//                    FROM Master_MainAccount A WHERE A.MainAccount_branchId in(" + Convert.ToString(Session["SelectedTagBranchList"]) + ")) AA ORDER BY Description ";
//                }
//                else
//                {
//                    query = @"select ROW_NUMBER() over(order by A.MainAccount_ReferenceID) SrlNo, A.MainAccount_ReferenceID AS ID,A.MainAccount_AccountCode AS 'Doc Code',A.MainAccount_Name AS 'Description','' AS Type  
//                    FROM Master_MainAccount A WHERE A.MainAccount_branchId in(0,'') ORDER BY A.MainAccount_Name ";
//                }
//            }
//            else if (ReportModule == "LedgerPost" && Convert.ToString(Session["ButtonName"]) == "Employee")
//            {
//                Session.Remove("ButtonName");
//                if (Convert.ToString(Session["SelectedTagBranchList"]).Trim() != "")
//                    query = @" select ROW_NUMBER() over(order by cnt_id) SrlNo, cnt_id AS ID,cnt_internalId as 'Doc Code',ISNULL(cnt_firstName,'')+' '+ISNULL(cnt_middleName,'')+'  '+ISNULL(cnt_lastName,'') as 'Description','Employee' Type 
//                    FROM tbl_master_contact WHERE cnt_contactType ='EM' and cnt_branchid IN(" + Convert.ToString(Session["SelectedTagBranchList"]) + ") " +
//                    "union ALL " +
//                    "select ROW_NUMBER() over(order by cnt_id) SrlNo, cnt_id AS ID,cnt_internalId as 'Doc Code',ISNULL(cnt_firstName,'')+' '+ISNULL(cnt_middleName,'')+'  '+ISNULL(cnt_lastName,'') as 'Description','Customer' Type " +
//                    "FROM tbl_master_contact WHERE cnt_contactType = 'CL' and cnt_branchid IN(" + Convert.ToString(Session["SelectedTagBranchList"]) + ") " +
//                    "union ALL " +
//                    "select ROW_NUMBER() over(order by cnt_id) SrlNo, cnt_id AS ID,cnt_internalId as 'Doc Code',ISNULL(cnt_firstName,'')+' '+ISNULL(cnt_middleName,'')+'  '+ISNULL(cnt_lastName,'') as 'Description','Vendor' Type " +
//                    "FROM tbl_master_contact WHERE cnt_contactType ='DV' and cnt_branchid IN(" + Convert.ToString(Session["SelectedTagBranchList"]) + ") " +
//                    "union ALL " +
//                    "select ROW_NUMBER() over(order by cnt_id) SrlNo, cnt_id AS ID,cnt_internalId as 'Doc Code',ISNULL(cnt_firstName,'')+' '+ISNULL(cnt_middleName,'')+'  '+ISNULL(cnt_lastName,'') as 'Description','Customer' Type " +
//                    "FROM tbl_master_contact WHERE cnt_contactType IN('CL','DV','EM') and cnt_branchid = 0 " +
//                    "union ALL " +
//                    "select ROW_NUMBER() over(order by MainAccount_ReferenceID) SrlNo, MainAccount_ReferenceID AS ID,MainAccount_AccountCode as 'Doc Code',MainAccount_Name as 'Description','Ledger' Type FROM Master_MainAccount WHERE MainAccount_SubLedgerType ='Custom' ";
//                else
//                    query = "select top 0 null as 'SrlNo',0 as 'ID',null as 'Doc Code',null as 'Description',NULL AS Type ";
//            }
//            else if ((ReportModule == "StockTrialSumm" || ReportModule == "StockTrialDet" || ReportModule == "StockTrialProd" || ReportModule == "StockTrialWH") && Convert.ToString(Session["ButtonName"]) == "Product")
//            {
//                Session.Remove("ButtonName");
//                query = @"select ROW_NUMBER() over(order by sProducts_ID) SrlNo, sProducts_ID AS ID,sProducts_Code as 'Doc Code',sProducts_Description as 'Description','' AS Type 
//                FROM Master_sProducts WHERE sProduct_IsInventory=1 
//                AND sProducts_ID IN(SELECT Distinct StockBranchWarehouseDetail_ProductId FROM Trans_StockBranchWarehouseDetails) ORDER BY sProducts_Description ";
//            }
//            else if (ReportModule == "StockTrialWH" && Convert.ToString(Session["ButtonName"]) == "Warehouse")
//            {
//                Session.Remove("ButtonName");
//                query = @";WITH CTE AS 
//                (
//                SELECT P.bui_id,P.bui_Name AS WH,P.bui_ParentId,P.bui_address1,P.bui_address2,P.bui_address3,P.bui_landmark,P.bui_city,P.bui_state,P.bui_country,P.bui_contactId,P.bui_pin,P.bui_BranchId,
//                CAST(P.bui_id AS VarChar(Max)) as Level,CAST(P.bui_Name AS VARCHAR(MAX)) AS WH_NAME 
//                FROM tbl_master_building P WHERE P.bui_ParentId IS NULL OR P.bui_ParentId = 0 
//                UNION ALL 
//                SELECT P1.bui_id,P1.bui_Name AS WH,P1.bui_ParentId,P1.bui_address1,P1.bui_address2,P1.bui_address3,P1.bui_landmark,P1.bui_city,P1.bui_state,P1.bui_country,P1.bui_contactId,P1.bui_pin,P1.bui_BranchId,
//                M.Level + ';' + CAST(P1.bui_id AS VARCHAR(MAX)),WH_NAME + ' : ' + CAST(P1.bui_Name AS VARCHAR(MAX)) AS bui_Name 
//                FROM tbl_master_building P1 
//                INNER JOIN CTE M 
//                ON M.bui_id = P1.bui_ParentId 
//                ) 
//                SELECT ROW_NUMBER() OVER(ORDER BY WH) SrlNo,bui_id AS ID,WH AS 'Doc Code',WH_NAME AS 'Description','' AS Type From CTE 
//                where bui_id in (select bui_id from tbl_master_building where bui_id not in (select distinct ISNULL(bui_ParentId,0) from tbl_master_building )) ORDER BY WH_NAME ";
//            }
//            else if (ReportModule == "Proforma1")
//            {
//                query = @"Select ROW_NUMBER() over(order by Quote_Id) SrlNo, Quote_Id AS ID, Quote_Number, CONVERT(VARCHAR(11),Quote_Expiry, 105) as Quote_Date from tbl_trans_Quotation order by Quote_Number ";
//            }
//            BusinessLogicLayer.DBEngine oDbEngine = new BusinessLogicLayer.DBEngine();
//            DataTable dt = oDbEngine.GetDataTable(query);
//            return dt;
//        }

        //protected void grid_Branch_DataBinding(object sender, EventArgs e)
        //{
        //    if (Session["ReportMain_BranchList"] != null)
        //    {
        //        grid_Branch.DataSource = Session["ReportMain_BranchList"];
        //    }
        //}

        //protected void grid_User_DataBinding(object sender, EventArgs e)
        //{
        //    if (Session["ReportMain_UserList"] != null)
        //    {
        //        grid_User.DataSource = Session["ReportMain_UserList"];
        //    }
        //}

        //protected void grid_Bank_DataBinding(object sender, EventArgs e)
        //{
        //    if (Session["ReportMain_BankList"] != null)
        //    {
        //        grid_Bank.DataSource = Session["ReportMain_BankList"];
        //    }
        //}

        //protected void grid_Cash_DataBinding(object sender, EventArgs e)
        //{
        //    if (Session["ReportMain_CashList"] != null)
        //    {
        //        grid_Cash.DataSource = Session["ReportMain_CashList"];
        //    }
        //}

        //protected void grid_DocDesc_DataBinding(object sender, EventArgs e)
        //{
        //    if (Session["ReportMain_DocDescList"] != null)
        //    {
        //        grid_DocDesc.DataSource = Session["ReportMain_DocDescList"];
        //        //grid_DocDesc.Columns[0].Visible = false;
        //    }
        //}

        //protected void grid_Documents_DataBinding(object sender, EventArgs e)
        //{
        //    if (Session["ReportMain_DocunmentList"] != null)
        //    {
        //        grid_Branch.DataSource = Session["ReportMain_DocunmentList"];
        //    }
        //}
    }
}