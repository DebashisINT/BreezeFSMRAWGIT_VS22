using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using DataAccessLayer;
using DevExpress.Web;
using System.Net.Mail;
using System.Drawing;
using DevExpress.XtraPrinting.Drawing;
using System.Web.Services;
using DevExpress.DataAccess.ConnectionParameters;

namespace Reports.Reports.REPXReports
{
    public partial class RepxReportViewer : System.Web.UI.Page
    {
        BusinessLogicLayer.ReportLayout rpLayout = new BusinessLogicLayer.ReportLayout();
        BusinessLogicLayer.ReportData rptData = new BusinessLogicLayer.ReportData();
        DBEngine odbeng = new DBEngine();
        public string redirectReportKey = "";
        string DocidforWaterMark = "";
        DataTable dtWatermark = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string tempFile = HttpContext.Current.Request.QueryString["Previewrpt"];
                StartDate.Value = HttpContext.Current.Request.QueryString["StartDate"];
                EndDate.Value = HttpContext.Current.Request.QueryString["EndDate"];
                string PrintType = HttpContext.Current.Request.QueryString["PrintOption"];
                String RptModuleName = HttpContext.Current.Request.QueryString["reportname"];
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                string rptName = tempFile;
                string filePath = "";
                string ExportFileName = "";
                string filePathtoPDF = "";
                string ReportType = "";
                Session["ReportGenerationType"] = null;

                if (RptModuleName == "EXAMPLE")
                {
                    if (string.IsNullOrEmpty(Page.Title))
                    {
                        Page.Title = "Example_" + rptName.Split('~')[0];
                    }
                    filePath = Server.MapPath("/Reports/RepxReportDesign/Example/DocDesign/Designes/" + rptName + ".repx");
                }
                //Mantis Issue 24944
                if (RptModuleName == "OrderSummary")
                {
                    if (string.IsNullOrEmpty(Page.Title))
                    {
                        Page.Title = "OrderSummary_" + rptName.Split('~')[0];
                    }
                    filePath = Server.MapPath("/Reports/RepxReportDesign/OrderSummary/DocDesign/Designes/" + rptName + ".repx");
                }
                //End of Mantis Issue 24944
                ExportFileName = Page.Title;
                DevExpress.DataAccess.Sql.SqlDataSource sql = GenerateSqlDataSource(RptModuleName);
                XtraReport newXtraReport = XtraReport.FromFile(filePath, true);
                newXtraReport.LoadLayout(filePath);
                newXtraReport.DataSource = sql;
                filePathtoPDF = filePath;
                filePathtoPDF = filePathtoPDF.Split('~')[0];
                
                newXtraReport.DisplayName = ExportFileName;
                ASPxDocumentViewer1.Report = newXtraReport;
                //newXtraReport.DisplayName = ExportFileName;

                //// Create a new memory stream and export the report into it as PDF.
                //MemoryStream mem = new MemoryStream();
                //newXtraReport.ExportToPdf(mem);

                //// Create a new attachment and put the PDF report into it.
                //mem.Seek(0, System.IO.SeekOrigin.Begin);
                //Attachment att = new Attachment(mem, "aa.pdf", "application/pdf");

                //// Create a new message and attach the PDF report to it.
                //MailMessage mail = new MailMessage();
                //mail.Attachments.Add(att);

                //// Specify sender and recipient options for the e-mail message.
                //mail.From = new MailAddress("debashis.talukder@indusnet.co.in", "Debashis");
                ////mail.To.Add(new MailAddress(newXtraReport.ExportOptions.Email.RecipientAddress,newXtraReport.ExportOptions.Email.RecipientName));
                ////mail.To.Add(new MailAddress(newXtraReport.ExportOptions.Email.AddRecipient, newXtraReport.ExportOptions.Email.AddRecipient));
                //mail.To.Add(new MailAddress("debashis.talukder@indusnet.co.in", "Debashis"));

                //// Specify other e-mail options.
                //mail.Subject = newXtraReport.ExportOptions.Email.Subject;
                //mail.Body = "This is a test e-mail message sent by an application.";

                //// Send the e-mail message via the specified SMTP server.
                //SmtpClient smtp = new SmtpClient("smtp.gmail.com",25);
                //smtp.Send(mail);

                //// Close the memory stream.
                //mem.Close();
                //}
            }
            if (!IsPostBack)
            {
                HDRepornName.Value = Convert.ToString(Request.QueryString["reportname"]);
            }
        }

        private DevExpress.DataAccess.Sql.SqlDataSource GenerateSqlDataSource(String RptModuleName)
        {
            CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
            DevExpress.DataAccess.Sql.SqlDataSource result = new DevExpress.DataAccess.Sql.SqlDataSource(connectionParameters);
            BusinessLogicLayer.DBEngine oDbEngine = new BusinessLogicLayer.DBEngine();
            string Module_Name = "";
            Module_Name = Convert.ToString(Session["Module_Name"]);
            DataTable dtRptTables = new DataTable();
            string query = "";

            query = @"Select Query_Table_name from tbl_trans_ReportSql where Module_name = '" + Module_Name + "' order by Query_ID ";
            dtRptTables = oDbEngine.GetDataTable(query);
            string DocumentID = "3";//"101";
           
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
                    result.Queries.Add(new CustomSqlQuery(Convert.ToString(row[0]), "EXEC PRC_FTSEXAMPLEPRINT_REPORT '" + Convert.ToString(Session["LastCompany"]) + "','" + fullpath + "','" + Convert.ToString(row[0]) + "','" + DocumentID + "','" + "P" + "'"));
                }
            }
            //Mantis Issue 24944
            if (RptModuleName == "OrderSummary")
            {
                foreach (DataRow row in dtRptTables.Rows)
                {
                    result.Queries.Add(new CustomSqlQuery(Convert.ToString(row[0]), "EXEC PRC_FTSORDERSUMMARYPRINT_REPORT '" + Convert.ToString(Session["LastCompany"]) + "','" + fullpath + "','" + Convert.ToString(row[0]) + "','" + DocumentID + "','" + "P" + "'"));
                }
            }
            //End of Mantis Issue 24944
            DataTable dtRptRelation = new DataTable();
            string RelationQuery = "";

            RelationQuery = @"Select Parent_Query_name,Child_Query_name, Parent_Column_name,Child_Column_name from tbl_trans_ReportTableRelation where Module_name = '" + Module_Name + "' order by Query_ID ";
            dtRptRelation = oDbEngine.GetDataTable(RelationQuery);
            if (dtRptRelation.Rows.Count > 0)
            {
                foreach (DataRow row in dtRptRelation.Rows)
                {
                    result.Relations.Add(Convert.ToString(row[0]), Convert.ToString(row[1]), Convert.ToString(row[2]), Convert.ToString(row[3]));
                }
            }

            result.RebuildResultSchema();
            return result;
        }
        [WebMethod]
        public static List<string> GetRecipients()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

            DataTable dtEmailTable = new DataTable();
            string EmailQuery = "";
            EmailQuery = @"select add_id as ID,add_Email as Email from tbl_master_address where add_Email<>'' or add_Email is not null union all select eml_id as ID,eml_ccEmail as Email from tbl_master_email where eml_ccEmail<>'' ";
            dtEmailTable = oDBEngine.GetDataTable(EmailQuery);

            List<string> obj = new List<string>();
            foreach (DataRow dr in dtEmailTable.Rows)
            {
                obj.Add(Convert.ToString(dr["Email"]) + "|" + Convert.ToString(dr["ID"]));
            }

            return obj;
        }

        [WebMethod]
        public static string GetFromEmail()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
            DataTable dtFromEmail = new DataTable();
            string FromEmailDesc="";
            dtFromEmail = oDBEngine.GetDataTable("select top(1) EmailAccounts_EmailID from Config_EmailAccounts where EmailAccounts_InUse='Y'");
            FromEmailDesc = dtFromEmail.Rows[0][0].ToString();
            return FromEmailDesc;
        }

        protected void CallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                DataTable dtFromEmailDet = new DataTable();
                dtFromEmailDet = oDBEngine.GetDataTable("select top(1) EmailAccounts_Password,EmailAccounts_FromName,LTRIM(RTRIM(EmailAccounts_SMTP)) AS EmailAccounts_SMTP,LTRIM(RTRIM(EmailAccounts_SMTPPort)) AS EmailAccounts_SMTPPort from Config_EmailAccounts where EmailAccounts_InUse='Y'");
                var Password = dtFromEmailDet.Rows[0][0].ToString();
                var FromWhere = dtFromEmailDet.Rows[0][1].ToString();
                var OutgoingSMTPHost = dtFromEmailDet.Rows[0][2].ToString();
                var OutgoingPort = dtFromEmailDet.Rows[0][3].ToString();
                var Rpt = ASPxDocumentViewer1.Report;
                // Create a new memory stream and export the report into it as PDF.
                MemoryStream mem = new MemoryStream();
                Rpt.ExportToPdf(mem);

                // Create a new attachment and put the PDF report into it.
                mem.Seek(0, System.IO.SeekOrigin.Begin);
                Attachment att = new Attachment(mem, Rpt.DisplayName + ".pdf", "application/pdf");

                // Create a new message and attach the PDF report to it.
                MailMessage mail = new MailMessage();
                //SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                SmtpClient smtp = new SmtpClient(OutgoingSMTPHost);
                mail.Attachments.Add(att);
               
                var FromAdd = txtFrom.Text;
                //var ToAdd = txtTo.Text;
                var ToAdd = hndSelectRecipients.Value;
                var CcAdd = txtCc.Text;
                var Body = txtMailBody.Text;
                var Subject = txtSubject.Text;

                // Specify sender and recipient options for the e-mail message.
                //mail.From = new MailAddress("bcool4u@gmail.com","Debashis");
                //mail.To.Add("debashis.talukder@indusnet.co.in");
                //mail.CC.Add("subhra.mukherjee@indusnet.co.in");
                //mail.Subject = "This is a Test Mail";
                mail.From = new MailAddress(FromAdd, FromWhere);
                mail.To.Add(ToAdd);
                if (CcAdd != "")
                {
                    mail.CC.Add(CcAdd);
                }
                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                //mail.Body = "This is a test e-mail message sent by an application.";
                mail.Body = Body;
                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                smtp.Host = OutgoingSMTPHost.Trim();
                smtp.Port = Convert.ToInt32(OutgoingPort);
                //smtp.Credentials = new System.Net.NetworkCredential("bcool4u@gmail.com", "*********");
                smtp.Credentials = new System.Net.NetworkCredential(FromAdd, Password);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                att.Dispose();
                smtp.Dispose();
                mail.Dispose();
                //Response.Write("Email Send successfully");
                // Close the memory stream.
                mem.Close();
                //Response.End();
                //Rpt.Dispose();

                //mail.To.Add(new MailAddress(newXtraReport.ExportOptions.Email.RecipientAddress,newXtraReport.ExportOptions.Email.RecipientName));
                //mail.To.Add(new MailAddress(newXtraReport.ExportOptions.Email.AddRecipient, newXtraReport.ExportOptions.Email.AddRecipient));
                //mail.To.Add(new MailAddress("bcool4u@gmail.com", "Debashis"));
                // Specify other e-mail options.
                //mail.Subject = newXtraReport.ExportOptions.Email.Subject;
                // Send the e-mail message via the specified SMTP server.
                //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25);
            }
            catch (Exception ex)
            {
                ASPxDocumentViewer1.JSProperties["cpErrorResult"] = ex.Message;
            }
        }
    }
}