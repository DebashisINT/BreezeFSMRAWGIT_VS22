﻿using BusinessLogicLayer;
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
using Reports.Reports.REPXReports;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.Web;
using System.Net.Mail;
using System.Drawing;
using DevExpress.XtraPrinting.Drawing;
using DataAccessLayer;
using System.Web.Services;
using DevExpress.DataAccess.ConnectionParameters;

namespace Reports.Reports.REPXReports
{
    public partial class RepxDocumentViewer : System.Web.UI.Page
    {
        BusinessLogicLayer.ReportLayout rpLayout = new BusinessLogicLayer.ReportLayout();
        BusinessLogicLayer.ReportData rptData = new BusinessLogicLayer.ReportData();

        string Module_name = "";
        DataTable dtWatermark = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string tempFile = HttpContext.Current.Request.QueryString["Previewrpt"];
                string RptModuleName = HttpContext.Current.Request.QueryString["modulename"]; //Convert.ToString(Session["NewRptModuleName"]);   
                string PrintType = HttpContext.Current.Request.QueryString["PrintOption"];
                string DocumentID = HttpContext.Current.Request.QueryString["id"];
                string Doctype = HttpContext.Current.Request.QueryString["doctype"];

                DBEngine odbeng = new DBEngine();
                //DataTable WatermarkDt = odbeng.GetDataTable("select Variable_Value from Config_SystemSettings where Variable_Name='ShowWatermarkinDocumentPrint'");

                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                string rptName = tempFile;
                string filePath = "";
                string filePathtoPDF = "";
                string[] filePaths = new string[] { };
                string DesignPath = "";
                string PDFFilePath = "";
                string ExportFileName = "";
                
                if (RptModuleName == "EXAMPLE")
                {
                    if (string.IsNullOrEmpty(Page.Title) && rptName != null)
                    {
                        Page.Title = "Example_" + rptName.Split('~')[0];
                    }
                    Module_name = "EXAMPLE";
                    if (ConfigurationManager.AppSettings["IsDevelopedZone"] != null)
                    {
                        DesignPath = @"Reports\RepxReportDesign\Example\DocDesign\Designes\";
                        PDFFilePath = @"Reports\RepxReportDesign\Example\DocDesign\PDFFiles\";
                    }
                    else
                    {
                        DesignPath = @"Reports\RepxReportDesign\Example\DocDesign\Designes\";
                        PDFFilePath = @"Reports\RepxReportDesign\Example\DocDesign\PDFFiles\";
                    }
                }
                //Mantis Issue 24944
                if (RptModuleName == "OrderSummary")
                {
                    if (string.IsNullOrEmpty(Page.Title) && rptName != null)
                    {
                        Page.Title = "OrderSummary_" + rptName.Split('~')[0];
                    }
                    Module_name = "OrderSummary";
                    if (ConfigurationManager.AppSettings["IsDevelopedZone"] != null)
                    {
                        DesignPath = @"Reports\RepxReportDesign\OrderSummary\DocDesign\Designes\";
                        PDFFilePath = @"Reports\RepxReportDesign\OrderSummary\DocDesign\PDFFiles\";
                    }
                    else
                    {
                        DesignPath = @"Reports\RepxReportDesign\OrderSummary\DocDesign\Designes\";
                        PDFFilePath = @"Reports\RepxReportDesign\OrderSummary\DocDesign\PDFFiles\";
                    }
                }
                //End of Mantis Issue 24944
                ExportFileName = Page.Title;
                Session["Module_Name"] = Module_name;
                string fullpath = Server.MapPath("~");
                fullpath = fullpath.Replace("ERP.UI\\", "");
                string DesignFullPath = fullpath + DesignPath;
                string PDFFullPath = fullpath + PDFFilePath;
                filePath = System.IO.Path.GetDirectoryName(DesignFullPath);
                filePath = filePath + "\\" + rptName + ".repx";
                DevExpress.DataAccess.Sql.SqlDataSource sql = GenerateSqlDataSource(RptModuleName);

                XtraReport newXtraReport = XtraReport.FromFile(filePath, true);
                //newXtraReport.ReportPrintOptions.DetailCount = 1;
                newXtraReport.LoadLayout(filePath);
                newXtraReport.DataSource = sql;
                filePathtoPDF = filePath;
                filePathtoPDF = filePathtoPDF.Split('~')[0];
                //filePathtoPDF=filePath.Replace(".repx",".pdf");

                newXtraReport.DisplayName = ExportFileName;
                ASPxDocumentDesignViewer1.Report = newXtraReport;

                //PDF file generation has been blocked as per requirment.
                //Reportname = Path.GetFileNameWithoutExtension(filePathtoPDF);
                //PDF file generation has been blocked as per requirment.

                //// Create a new attachment and put the PDF report into it.            
                //Attachment att = new Attachment(filePathtoPDF, "application/pdf");            

                //// Create a new message and attach the PDF report to it.
                //MailMessage mail = new MailMessage();
                //mail.Attachments.Add(att);

                //// Specify sender and recipient options for the e-mail message.
                //mail.From = new MailAddress("debashis.talukder@indusnet.co.in", "Debashis");
                //mail.To.Add(new MailAddress("debashis.talukder@indusnet.co.in", "Debashis"));

                //// Specify other e-mail options.
                //mail.Subject = newXtraReport.ExportOptions.Email.Subject;
                //mail.Body = "This is a test e-mail message sent by an application.";

                //// Send the e-mail message via the specified SMTP server.
                //SmtpClient smtp = new SmtpClient("smtp.gmail.com",25);
                //smtp.Send(mail);

                //MemoryStream stream = new MemoryStream();
                ////PdfExportOptions opts = new PdfExportOptions();
                //string fileType = "pdf";
                ////opts.ShowPrintDialogOnOpen = true;
                ////newXtraReport.ExportToPdf(stream,opts);
                //newXtraReport.ExportToPdf(stream);
                //Response.ContentType = "application/" + fileType;
                //Response.BinaryWrite(stream.ToArray());
                //Response.End();

                //using (MemoryStream ms = new MemoryStream())
                //{
                //    XtraReport1 r = new XtraReport1();
                //    r.CreateDocument();
                //    PdfExportOptions opts = new PdfExportOptions();
                //    opts.ShowPrintDialogOnOpen = true;
                //    r.ExportToPdf(ms, opts);
                //    ms.Seek(0, SeekOrigin.Begin);
                //    byte[] report = ms.ToArray();
                //    Page.Response.ContentType = "application/pdf";
                //    Page.Response.Clear();
                //    Page.Response.OutputStream.Write(report, 0, report.Length);
                //    Page.Response.End();
                //}


                //ASPxDocumentViewer1.Report.Print();
                //ASPxDocumentViewer1.Report.PrintDialog();    

            }
            if (!IsPostBack)
            {
                HDReportName.Value = Convert.ToString(Request.QueryString["reportname"]);
            }
        }

        private DevExpress.DataAccess.Sql.SqlDataSource GenerateSqlDataSource(String RptModuleName)
        {
            CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
            DevExpress.DataAccess.Sql.SqlDataSource result = new DevExpress.DataAccess.Sql.SqlDataSource(connectionParameters);
            BusinessLogicLayer.DBEngine oDbEngine = new BusinessLogicLayer.DBEngine();

            DataTable dtRptTables = new DataTable();
            string query = "";

            query = @"Select Query_Table_name from tbl_trans_ReportSql where Module_name = '" + Module_name + "' order by Query_ID ";
            dtRptTables = oDbEngine.GetDataTable(query);
            string[] filePaths = new string[] { };
            string path = System.Web.HttpContext.Current.Server.MapPath("~");
            string path1 = path.Replace("Reports\\", "ERP.UI");
            string fullpath = path1.Replace("\\", "/");
            string DocumentID = HttpContext.Current.Request.QueryString["Id"];
            string Doctype = HttpContext.Current.Request.QueryString["doctype"];
            string BranchId = HttpContext.Current.Request.QueryString["Branch"];
            string yymm = HttpContext.Current.Request.QueryString["yymm"];


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
                    result.Queries.Add(new CustomSqlQuery(Convert.ToString(row[0]), "EXEC PRC_FTSORDERSUMMARYPRINT_REPORT '" + Convert.ToString(Session["LastCompany"]) + "','" + "" + "','" + fullpath + "','" + Convert.ToString(row[0]) + "','" + DocumentID + "','" + "P" + "'"));
                }
            }
            //End of Mantis Issue 24944
            DataTable dtRptRelation = new DataTable();
            string RelationQuery = "";
            RelationQuery = @"Select Parent_Query_name,Child_Query_name, Parent_Column_name,Child_Column_name from tbl_trans_ReportTableRelation where Module_name = '" + Module_name + "' order by Query_ID ";
            dtRptRelation = oDbEngine.GetDataTable(RelationQuery);
            if (dtRptRelation.Rows.Count > 0)
            {
                foreach (DataRow row in dtRptRelation.Rows)
                {
                    result.Relations.Add(Convert.ToString(row[0]), Convert.ToString(row[1]), Convert.ToString(row[2]), Convert.ToString(row[3]));
                }
            }

            result.RebuildResultSchema();
            result.Connection.Close();
            return result;
        }

        [WebMethod]
        public static string GetFromEmail()
        {
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
            DataTable dtFromEmail = new DataTable();
            string FromEmailDesc = "";
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

                var Rpt = ASPxDocumentDesignViewer1.Report;
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

                var FromAdd = txtDocPrintFrom.Text;
                var ToAdd = hndSelectDocPrintRecipients.Value;
                var CcAdd = txtDocPrintCc.Text;
                var Body = txtDocPrintMailBody.Text;
                var Subject = txtDocPrintSubject.Text;

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
                mail.Body = Body;
                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                smtp.Host = OutgoingSMTPHost.Trim();
                smtp.Port = Convert.ToInt32(OutgoingPort);
                smtp.Credentials = new System.Net.NetworkCredential(FromAdd, Password);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                att.Dispose();
                smtp.Dispose();
                mail.Dispose();
                // Close the memory stream.
                mem.Close();
            }
            catch (Exception ex)
            {
                ASPxDocumentDesignViewer1.JSProperties["cpErrorResult"] = ex.Message;
            }
        }
    }
}