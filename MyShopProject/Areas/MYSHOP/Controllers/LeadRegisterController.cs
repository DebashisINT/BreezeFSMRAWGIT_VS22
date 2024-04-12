using BusinessLogicLayer.SalesTrackerReports;
using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Models;
using MyShop.Models;
using SalesmanTrack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class LeadRegisterController : Controller
    {
        //
        // GET: /MYSHOP/LeadRegister/
        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();
        public ActionResult LeadRegister()
        {
            try
            {

                LeadRegisterModel omodel = new LeadRegisterModel();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                DataTable dt = objgps.GetStateMandatory(Session["userid"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }

                return View(omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetLeadRegisterList(LeadRegisterModel model)
        {
            try
            {

                DataTable dt = new DataTable();
                string Ispageload = string.Empty;

                if (model.is_pageload == "0")
                {
                    Ispageload = "Ispageload";
                }

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                ViewData["ModelData"] = model;

                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                string Userid = Convert.ToString(Session["userid"]);


                string state = "";
                int i = 1;

                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (i > 1)
                            state = state + "," + item;
                        else
                            state = item;
                        i++;
                    }

                }

                string desig = "";
                int j = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (j > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
                        j++;
                    }

                }

                string empcode = "";

                int k = 1;

                if (model.empcode != null && model.empcode.Count > 0)
                {
                    foreach (string item in model.empcode)
                    {
                        if (k > 1)
                            empcode = empcode + "," + item;
                        else
                            empcode = item;
                        k++;
                    }

                }
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;

                //if (days <= 7)
                //if (days <= 30)
                //{
                //    dt = GetLeadRegisterReport(datfrmat, dattoat, Userid, state, desig, empcode);
                //}
                dt = GetLeadRegisterReport(datfrmat, dattoat, Userid, state, desig, empcode);

                return PartialView("PartialGetLeadRegisterReport", GetLeadRegister(Ispageload));


            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }
        public DataTable GetLeadRegisterReport(string fromdate, string todate, string userid, string stateID, string desigid, string empid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSLEADREGISTER_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);

            proc.AddPara("@USERID", userid);
            proc.AddPara("@EMPID", empid);
            ds = proc.GetTable();

            return ds;
        }
        public ActionResult GetStateList()
        {
            try
            {
                List<GetUsersStates> modelstate = new List<GetUsersStates>();
                DataTable dtstate = lstuser.GetStateList();
                modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);


                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_StatesPartial.cshtml", modelstate);



            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult GetDesigList()
        {
            try
            {
                DataTable dtdesig = lstuser.Getdesiglist();
                List<GetDesignation> modeldesig = new List<GetDesignation>();
                modeldesig = APIHelperMethods.ToModelList<GetDesignation>(dtdesig);


                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_DesigPartial.cshtml", modeldesig);



            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public ActionResult GetEmpList(SalesSummaryReport model)
        {
            try
            {
                string state = "";
                int i = 1;

                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (i > 1)
                            state = state + "," + item;
                        else
                            state = item;
                        i++;
                    }

                }

                string desig = "";
                int k = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (k > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
                        k++;
                    }

                }



                DataTable dtemp = lstuser.Getemplist(state, desig);
                List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
                modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(dtemp);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmpPartial.cshtml", modelemp);



            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }

        public IEnumerable GetLeadRegister(string Ispageload)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Lead Register");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (Ispageload != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSLEADREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSLEADREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }


        public ActionResult ExporLeadRegisterList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetLeadRegisterGridViewSettings(), GetLeadRegister(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetLeadRegisterGridViewSettings(), GetLeadRegister(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetLeadRegisterGridViewSettings(), GetLeadRegister(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetLeadRegisterGridViewSettings(), GetLeadRegister(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetLeadRegisterGridViewSettings(), GetLeadRegister(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetLeadRegisterGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "LEAD REGISTER");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            var settings = new GridViewSettings();
            settings.Name = "Lead Register Report";
            settings.CallbackRouteValues = new { Controller = "LeadRegister", Action = "GetLeadRegisterList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Lead Register Report";


            settings.Columns.Add(x =>
            {
                x.FieldName = "SEQ";
                x.Caption = "SN";
                x.VisibleIndex = 1;
                //x.Width = 100;
                //x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SEQ'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 50;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 50;
                }

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SHOPDATE";
                x.Caption = "Date";
                x.VisibleIndex = 2;
                //x.Width = 100;
                //x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPDATE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 90;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 90;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                x.Caption = "Employee Name";
                x.VisibleIndex = 3;
                //x.Width = 100;
                //x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPNAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 180;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "LEAD_NAME";
                x.Caption = "Lead Name";
                x.VisibleIndex = 4;
                //x.Width = 100;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LEAD_NAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 150;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 150;
                }

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "LEADCONTACTNO";
                x.Caption = "Lead Contact no";
                x.VisibleIndex = 5;
                //x.Width = 120;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LEADCONTACTNO'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 90;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 90;
                }
            });

            //Rev Debashis
            settings.Columns.Add(x =>
            {
                x.FieldName = "AGENCY_NAME";
                x.Caption = "Agency Name";
                x.VisibleIndex = 6;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='AGENCY_NAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 100;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "LEAD_ADDRESS";
                x.Caption = "Lead Address";
                x.VisibleIndex = 7;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LEAD_ADDRESS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 220;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 220;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "VISITSTATUS";
                x.Caption = "Status";
                x.VisibleIndex = 8;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITSTATUS'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 90;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 90;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SELFIE_PIC";
                x.Caption = "Selfie pic";
                x.VisibleIndex = 9;
                //x.Width = 180;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SELFIE_PIC'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 420;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 420;
                }
            });

            //settings.CustomColumnDisplayText = (sender, e) =>
            //{
            //    if (e.Column.FieldName == "SELFIE_PIC")
            //    {
            //        DevExpress.Web.Rendering.GridViewRenderHelper renderHelper = new DevExpress.Web.Rendering.GridViewRenderHelper(sender as MVCxGridView);
            //        e.DisplayText = renderHelper.TextBuilder.GetRowDisplayText((sender as MVCxGridView).DataColumns["SELFIE_PIC"], e.VisibleIndex);
            //    }
            //};
            //settings.HtmlDataCellPrepared += (s, e) =>
            //{

            //    if (e.DataColumn.FieldName == "SELFIE_PIC")
            //    {
            //        MVCxGridView grid = (MVCxGridView)s;
            //        var IMG_PATH = (string)grid.GetRowValues(e.VisibleIndex, new string[] { "SELFIE_PIC" });
            //        //var Type = (string)grid.GetRowValues(e.VisibleIndex, new string[] { "Type" });

            //        var buttonHtml = "";
            //        var img_folder = System.Configuration.ConfigurationSettings.AppSettings["Path"];
            //        var img_folder_newvisit = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];

            //        if (IMG_PATH != "")
            //        {
            //            //if (Type == "Re-Visit")
            //            //{
            //            //    buttonHtml = string.Format(" <a class='example-image-link' href='{0}' data-lightbox='example-1'><img src='{0}' data-lightbox='{0}' alt='No Image Found' height='42' width='42'></a>", img_folder + IMG_PATH);
            //            //}
            //            //else
            //            //{
            //            //    buttonHtml = string.Format(" <a class='example-image-link' href='{0}' data-lightbox='example-1'><img src='{0}' data-lightbox='{0}' alt='No Image Found' height='42' width='42'></a>", img_folder_newvisit + IMG_PATH);

            //            //}
            //            buttonHtml = string.Format(" <a class='example-image-link' href='{0}' data-lightbox='example-1'><img src='{0}' data-lightbox='{0}' alt='No Image Found' height='42' width='42'></a>", "/Commonfolder/" + IMG_PATH);
            //        }
            //        else
            //        {
            //            buttonHtml = string.Format("<span>No Image Found </span>", IMG_PATH);
            //        }

            //        e.Cell.Text = buttonHtml;
            //    }
            //};

            settings.Columns.Add(x =>
            {
                x.FieldName = "VISITNG_CARD1";
                x.Caption = "Visitng card1";
                x.VisibleIndex = 10;
                //x.Width = 180;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITNG_CARD1'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 180;
                }
            });
            //settings.HtmlDataCellPrepared += (s, e) =>
            //{

            //    if (e.DataColumn.FieldName == "VISITNG_CARD1")
            //    {
            //        MVCxGridView grid = (MVCxGridView)s;
            //        var IMG_PATH = (string)grid.GetRowValues(e.VisibleIndex, new string[] { "VISITNG_CARD1" });
            //        //var Type = (string)grid.GetRowValues(e.VisibleIndex, new string[] { "Type" });

            //        var buttonHtml = "";
            //        var img_folder = System.Configuration.ConfigurationSettings.AppSettings["Path"];
            //        var img_server = System.Configuration.ConfigurationSettings.AppSettings["SPath"];
            //        //var img_folder_local = "http://localhost:5735//Commonfolder/";
            //        if (IMG_PATH != "")
            //        {
            //            //if (Type == "Re-Visit")
            //            //{
            //            //    buttonHtml = string.Format(" <a class='example-image-link' href='{0}' data-lightbox='example-1'><img src='{0}' data-lightbox='{0}' alt='No Image Found' height='42' width='42'></a>", img_folder + IMG_PATH);
            //            //}
            //            //else
            //            //{
            //            //    buttonHtml = string.Format(" <a class='example-image-link' href='{0}' data-lightbox='example-1'><img src='{0}' data-lightbox='{0}' alt='No Image Found' height='42' width='42'></a>", img_folder_newvisit + IMG_PATH);

            //            //}
            //            buttonHtml = string.Format(" <a class='example-image-link' href='{0}' data-lightbox='example-1'><img src='{0}' data-lightbox='{0}' alt='No Image Found' height='42' width='42'></a>", img_server + IMG_PATH);
            //        }
            //        else
            //        {
            //            buttonHtml = string.Format("<span>No Image Found </span>", IMG_PATH);
            //        }

            //        e.Cell.Text = buttonHtml;
            //    }
            //};

            settings.Columns.Add(x =>
            {
                x.FieldName = "VISITNG_CARD2";
                x.Caption = "Visitng card2";
                x.VisibleIndex = 11;
                //x.Width = 100;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITNG_CARD2'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 180;
                }
            });
            //settings.HtmlDataCellPrepared += (s, e) =>
            //{

            //    if (e.DataColumn.FieldName == "VISITNG_CARD2")
            //    {
            //        MVCxGridView grid = (MVCxGridView)s;
            //        var IMG_PATH = (string)grid.GetRowValues(e.VisibleIndex, new string[] { "VISITNG_CARD2" });
            //        //var Type = (string)grid.GetRowValues(e.VisibleIndex, new string[] { "Type" });

            //        var buttonHtml = "";
            //        var img_folder = System.Configuration.ConfigurationSettings.AppSettings["Path"];
            //        var img_server = System.Configuration.ConfigurationSettings.AppSettings["SPath"];
            //        //var img_folder_local = "http://localhost:5735//Commonfolder/";
            //        if (IMG_PATH != "")
            //        {
            //            //if (Type == "Re-Visit")
            //            //{
            //            //    buttonHtml = string.Format(" <a class='example-image-link' href='{0}' data-lightbox='example-1'><img src='{0}' data-lightbox='{0}' alt='No Image Found' height='42' width='42'></a>", img_folder + IMG_PATH);
            //            //}
            //            //else
            //            //{
            //            //    buttonHtml = string.Format(" <a class='example-image-link' href='{0}' data-lightbox='example-1'><img src='{0}' data-lightbox='{0}' alt='No Image Found' height='42' width='42'></a>", img_folder_newvisit + IMG_PATH);

            //            //}
            //            buttonHtml = string.Format(" <a class='example-image-link' href='{0}' data-lightbox='example-1'><img src='{0}' data-lightbox='{0}' alt='No Image Found' height='42' width='42'></a>", img_server + IMG_PATH);
            //        }
            //        else
            //        {
            //            buttonHtml = string.Format("<span>No Image Found </span>", IMG_PATH);
            //        }

            //        e.Cell.Text = buttonHtml;
            //    }
            //};
            settings.Columns.Add(x =>
            {
                x.FieldName = "PROSPECT";
                x.Caption = "Prospect";
                x.VisibleIndex = 12;
                //x.Width = 100;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PROSPECT'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 100;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "APROXMATE_BILLING";
                x.Caption = "Aproxmate Billing";
                x.VisibleIndex = 13;
                //x.Width = 100;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='APROXMATE_BILLING'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 100;
                }
            });

            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Lead Should go and Deliver Products 4-6 Days in week]";
                x.FieldName = "DELIVER_PRODUCTS_4_6_DAYS";
                x.Caption = "Lead Should go and Deliver Products 4-6 Days in week";
                x.VisibleIndex = 14;
                //x.Width = 180;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Lead Should go and Deliver Products 4-6 Days in week]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DELIVER_PRODUCTS_4_6_DAYS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 100;
                }
            });

            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Lead Should go and Collection products 4-6 days in Week]";
                x.FieldName = "COLLECTION_PRODUCTS_4_6_DAYS";
                x.Caption = "Lead Should go and Collection products 4-6 days in Week";
                x.VisibleIndex = 15;
                //x.Width = 100;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Lead Should go and Collection products 4-6 days in Week]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='COLLECTION_PRODUCTS_4_6_DAYS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 100;
                }
            });


            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Monthly investment 75K - 1.25L]";
                x.FieldName = "MONTHLY_INVESTMENT_75K_125L";
                x.Caption = "Monthly investment 75K - 1.25L";
                x.VisibleIndex = 16;
                //x.Width = 120;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Monthly investment 75K - 1.25L]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MONTHLY_INVESTMENT_75K_125L'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 120;
                }
            });

            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Should Deliver Products 100-200 outlets]";
                x.FieldName = "DELIVER_PRODUCTS_100_200_OUTLETS";
                x.Caption = "Should Deliver Products 100-200 outlets";
                x.VisibleIndex = 17;
                //x.Width = 100;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Should Deliver Products 100-200 outlets]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DELIVER_PRODUCTS_100_200_OUTLETS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 100;
                }
            });

            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Should do cash and carry sales for 25% Retails Outlets]";
                x.FieldName = "CASH_CARRY_SALES_FOR_25_RETAILS_OUTLETS";
                x.Caption = "Should do cash and carry sales for 25% Retails Outlets";
                x.VisibleIndex = 18;
                //x.Width = 120;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Should do cash and carry sales for 25% Retails Outlets]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CASH_CARRY_SALES_FOR_25_RETAILS_OUTLETS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 120;
                }
            });

            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Should do Credit sales for 50% Retails Outlets]";
                x.FieldName = "CREDIT_SALES_FOR_50_RETAILS_OUTLETS";
                x.Caption = "Should do Credit sales for 50% Retails Outlets";
                x.VisibleIndex = 19;
                //x.Width = 120;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Should do Credit sales for 50% Retails Outlets]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CREDIT_SALES_FOR_50_RETAILS_OUTLETS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 120;
                }
            });

            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Should do Bill to Bill sales for 25% Retails outlets]";
                x.FieldName = "BILL_SALES_FOR_25_RETAILS_OUTLETS";
                x.Caption = "Should do Bill to Bill sales for 25% Retails outlets";
                x.VisibleIndex = 20;
                //x.Width = 120;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Should do Bill to Bill sales for 25% Retails outlets]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BILL_SALES_FOR_25_RETAILS_OUTLETS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 120;
                }
            });


            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Should have Already be a FMCG Distributors]";
                x.FieldName = "ALREADY_FMCG_DISTRIBUTORS";
                x.Caption = "Should have Already be a FMCG Distributors";
                x.VisibleIndex = 21;
                //x.Width = 120;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Should have Already be a FMCG Distributors]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ALREADY_FMCG_DISTRIBUTORS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 120;
                }
            });

            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Already handled FMCG beverage Products]";
                x.FieldName = "ALREADY_HANDLED_FMCG_BEVERAGE_PRODUCTS";
                x.Caption = "Already handled FMCG beverage Products";
                x.VisibleIndex = 22;
                //x.Width = 120;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Already handled FMCG beverage Products]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ALREADY_HANDLED_FMCG_BEVERAGE_PRODUCTS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 120;
                }
            });

            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Handling other FMCG Products Parallelly]";
                x.FieldName = "HANDLING_OTHER_FMCG_PRODUCTS_PARALLELLY";
                x.Caption = "Handling other FMCG Products Parallelly";
                x.VisibleIndex = 23;
                //x.Width = 120;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Handling other FMCG Products Parallelly]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='HANDLING_OTHER_FMCG_PRODUCTS_PARALLELLY'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 120;
                }
            });


            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Should have a four-wheeler]";
                x.FieldName = "FOUR_WHEELER";
                x.Caption = "Should have a four-wheeler";
                x.VisibleIndex = 24;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='FOUR_WHEELER'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 120;
                }
            });


            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Should have a Two-Wheeler]";
                x.FieldName = "TWO_WHEELER";
                x.Caption = "Should have a Two-Wheeler";
                x.VisibleIndex = 25;
                //x.Width = 100;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Should have a Two-Wheeler]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TWO_WHEELER'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 100;
                }
            });


            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Should have an employed sales Man]";
                x.FieldName = "EMPLOYED_SALESMAN";
                x.Caption = "Should have an employed sales Man";
                x.VisibleIndex = 26;
                //x.Width = 200;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Should have an employed sales Man]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPLOYED_SALESMAN'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 200;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 200;
                }
            });

            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Should have exposure a Net banking and Google Pay]";
                x.FieldName = "NETBANKING_GOOGLEPAY";
                x.Caption = "Should have exposure a Net banking and Google Pay";
                x.VisibleIndex = 27;
                //x.Width = 200;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Should have exposure a Net banking and Google Pay]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='NETBANKING_GOOGLEPAY'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 200;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 200;
                }
            });
            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Should have a lived in an area more than 15 years]";
                x.FieldName = "LIVED_MORETHAN_15YEARS";
                x.Caption = "Should have a lived in an area more than 15 years";
                x.VisibleIndex = 28;
                //x.Width = 200;
                // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LIVED_MORETHAN_15YEARS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 200;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 200;
                }
            });

            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Should have a business in an area more than 10 years]";
                x.FieldName = "BUSINESS_MORETHAN_10YEARS";
                x.Caption = "Should have a business in an area more than 10 years";
                x.VisibleIndex = 29;
                //x.Width = 200;
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Should have a business in an area more than 10 years]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BUSINESS_MORETHAN_10YEARS'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 200;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 200;
                }
            });

            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Should go market and take order from a shop]";
                x.FieldName = "TAKEORDERFROMASHOP";
                x.Caption = "Should go market and take order from a shop";
                x.VisibleIndex = 30;
                //x.Width = 200;
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Should go market and take order from a shop]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TAKEORDERFROMASHOP'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 200;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 200;
                }
            });

            settings.Columns.Add(x =>
            {
                //x.FieldName = "[Age between 35 – 55]";
                x.FieldName = "AGE_BETWEEN_35_55";
                x.Caption = "Age between 35 – 55";
                x.VisibleIndex = 31;
                //x.Width = 200;
                if (ViewBag.RetentionColumn != null)
                {
                    //System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='[Age between 35 – 55]'");
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='AGE_BETWEEN_35_55'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                        //x.Width = 0;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 200;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 200;
                }
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult PageRetention(List<String> Columns)
        {
            try
            {
                String Col = "";
                int i = 1;
                if (Columns != null && Columns.Count > 0)
                {
                    Col = string.Join(",", Columns);
                }
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "LEAD REGISTER");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

	}
}