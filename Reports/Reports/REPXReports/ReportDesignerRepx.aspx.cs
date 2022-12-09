using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using DataAccessLayer;
using System.Data.SqlClient;
using DevExpress.DataAccess.ConnectionParameters;

namespace Reports.Reports.REPXReports
{
    public partial class ReportDesignerRepx : System.Web.UI.Page
    {
        BusinessLogicLayer.ReportLayout rpLayout = new BusinessLogicLayer.ReportLayout();
        BusinessLogicLayer.ReportData rptData = new BusinessLogicLayer.ReportData();

        protected void Page_Load(object sender, EventArgs e)
        {
            // The name for a file to save a report.
            if (!IsPostBack && !IsCallback)
            {
                StartDate.Value = HttpContext.Current.Request.QueryString["StartDate"];
                EndDate.Value = HttpContext.Current.Request.QueryString["EndDate"];
                if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["LoadrptName"]))
                {
                    // Run the Wizard to create a new report.
                    RptName.Value = HttpContext.Current.Request.QueryString["NewReport"];
                    string tempFile = RptName.Value;
                    CreateReport(tempFile);
                }
                else if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["LoadrptName"]))
                {
                    // Load report.
                    RptName.Value = HttpContext.Current.Request.QueryString["LoadrptName"];
                    string tempFile = RptName.Value;
                    LoadReport(tempFile);
                }
            }

            if (!IsPostBack)
            {
                HDERepornName.Value = Convert.ToString(Request.QueryString["reportname"]);
            }
        }

        private void CreateReport(string fileName)
        {
            DevExpress.DataAccess.Sql.SqlDataSource sql = GenerateSqlDataSource();
            string RptModuleName = Convert.ToString(Session["NewRptModuleName"]);
            var rpt = new DevExpress.XtraReports.UI.XtraReport();
            string rptName = fileName;
            if (string.IsNullOrEmpty(Page.Title))
            {
                Page.Title = rptName.Split('~')[0]; //RptModuleName;//ConfigurationManager.AppSettings[RptModuleName];
            }
            XtraReport newXtraReport = new XtraReport();
            newXtraReport.DataSource = sql;
            ASPxReportDesigner1.OpenReport(newXtraReport);
        }

        private void LoadReport(string fileName)
        {
            string rptName = fileName;
            string filePath = "";
            string RptModuleName = Convert.ToString(Session["NewRptModuleName"]);
            if (string.IsNullOrEmpty(Page.Title))
            {
                Page.Title = rptName.Split('~')[0]; //RptModuleName;//ConfigurationManager.AppSettings[RptModuleName];
            }
            if (RptModuleName == "EXAMPLE")
            {
                filePath = Server.MapPath("/Reports/RepxReportDesign/Example/DocDesign/Designes/" + rptName + ".repx");
            }
            //Mantis Issue 24944
            if (RptModuleName == "OrderSummary")
            {
                filePath = Server.MapPath("/Reports/RepxReportDesign/OrderSummary/DocDesign/Designes/" + rptName + ".repx");
            }
            //End of Mantis Issue 24944
            DevExpress.DataAccess.Sql.SqlDataSource sql = GenerateSqlDataSource();
            XtraReport newXtraReport = XtraReport.FromFile(filePath, true);
            newXtraReport.LoadLayout(filePath);
            newXtraReport.DataSource = sql;
            ASPxReportDesigner1.OpenReport(newXtraReport);
        }

        private DevExpress.DataAccess.Sql.SqlDataSource GenerateSqlDataSource()
        {
            CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
            DevExpress.DataAccess.Sql.SqlDataSource result = new DevExpress.DataAccess.Sql.SqlDataSource(connectionParameters);
            BusinessLogicLayer.DBEngine oDbEngine = new BusinessLogicLayer.DBEngine();

            string RptModuleName = Convert.ToString(Session["NewRptModuleName"]);
            string Module_Name = Convert.ToString(Session["Module_Name"]);
            DataTable dtRptTables = new DataTable();
            string query = "";

            query = @"Select Query_Table_name from tbl_trans_ReportSql where Module_name = '" + Module_Name + "' order by Query_ID ";
            dtRptTables = oDbEngine.GetDataTable(query);
            string CustVendType = "";
            string SalesPurchaseType = "";
            #region  for logo image
            string[] filePaths = new string[] { };
            string path = System.Web.HttpContext.Current.Server.MapPath("~");
            string path1 = path.Replace("Reports\\", "ERP.UI");
            string fullpath = path1.Replace("\\", "/");
            #endregion            

            if (RptModuleName == "EXAMPLE")
            {
                foreach (DataRow row in dtRptTables.Rows)
                {
                    result.Queries.Add(new CustomSqlQuery(Convert.ToString(row[0]), "EXEC PRC_FTSEXAMPLEPRINT_REPORT '" + Convert.ToString(Session["LastCompany"]) + "','" + fullpath + "','" + Convert.ToString(row[0]) + "','" + "" + "','" + "L" + "'"));
                }
            }
            //Mantis Issue 24944
            if (RptModuleName == "OrderSummary")
            {
                foreach (DataRow row in dtRptTables.Rows)
                {
                    result.Queries.Add(new CustomSqlQuery(Convert.ToString(row[0]), "EXEC PRC_FTSORDERSUMMARYPRINT_REPORT '" + Convert.ToString(Session["LastCompany"]) + "','" + "" + "','" + fullpath + "','" + Convert.ToString(row[0]) + "','" + "" + "','" + "L" + "'"));
                }
            }
            //End of Mantis Issue 24944
            DataTable dtRptRelation = new DataTable();
            string RelationQuery = "";

            RelationQuery = @"Select Parent_Query_name,Child_Query_name, Parent_Column_name,Child_Column_name from tbl_trans_ReportTableRelation where Module_name = '" + Module_Name + "' order by Query_ID ";
            dtRptRelation = oDbEngine.GetDataTable(RelationQuery);
            foreach (DataRow row in dtRptRelation.Rows)
            {
                result.Relations.Add(Convert.ToString(row[0]), Convert.ToString(row[1]), Convert.ToString(row[2]), Convert.ToString(row[3]));
            }

            result.RebuildResultSchema();
            return result;
        }

        // Save a report to a file.
        protected void ASPxReportDesigner1_SaveReportLayout(object sender, DevExpress.XtraReports.Web.SaveReportLayoutEventArgs e)
        {
            string FileName = "";
            string filePath = "";
            //String ReportModule = Convert.ToString(Session["NewRptModuleName"]);
            string ReportModule = HttpContext.Current.Request.QueryString["reportname"];
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["NewReport"]))
            {
                FileName = HttpContext.Current.Request.QueryString["NewReport"] + "~N";
            }
            else if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["LoadrptName"]))
            {
                FileName = HttpContext.Current.Request.QueryString["LoadrptName"];
            }
            XtraReport newXtraReport = new XtraReport();

            if (ReportModule == "EXAMPLE")
            {
                filePath = Server.MapPath("/Reports/RepxReportDesign/Example/DocDesign/Designes/" + FileName + ".repx");
            }
            //Mantis Issue 24944
            if (ReportModule == "OrderSummary")
            {
                filePath = Server.MapPath("/Reports/RepxReportDesign/OrderSummary/DocDesign/Designes/" + FileName + ".repx");
            }
            //End of Mantis Issue 24944
            var bytarr = e.ReportLayout;
            Stream stream = new MemoryStream(bytarr);
            newXtraReport.LoadLayout(stream);
            newXtraReport.SaveLayout(filePath);
            ASPxReportDesigner1.JSProperties["cpSaveResult"] = "Design saved successfully.";
        }
    }
}