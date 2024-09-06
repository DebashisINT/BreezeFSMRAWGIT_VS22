#region======================================Revision History===============================================================================================
//
#endregion===================================End of Revision History========================================================================================
using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLogicLayer.SalesTrackerReports;
using System.Data;
using UtilityLayer;
using System.Collections;
using System.Configuration;
using MyShop.Models;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using DocumentFormat.OpenXml.Drawing;
using BusinessLogicLayer;
using DataAccessLayer;
using System.Net;
using System.Web.UI.WebControls;


namespace MyShop.Areas.MYSHOP.Controllers
{
    public class DailyCallReportController : Controller
    {
        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();


        // GET: MYSHOP/DailyCallReport
        public ActionResult Index()
        {
            try
            {

                SalesSummaryReport omodel = new SalesSummaryReport();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                DataTable dt = objgps.GetStateMandatory(Session["userid"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }
                // Rev 3.0
                DBEngine obj1 = new DBEngine();
                ViewBag.BranchMandatory = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsActivateEmployeeBranchHierarchy'").Rows[0][0]);
                // End of Rev 3.0
                // Rev 2.0
                DataTable dtbranch = lstuser.GetHeadBranchList(Convert.ToString(Session["userbranchHierarchy"]), "HO");
                DataTable dtBranchChild = new DataTable();
                if (dtbranch.Rows.Count > 0)
                {
                    dtBranchChild = lstuser.GetChildBranch(Convert.ToString(Session["userbranchHierarchy"]));
                    if (dtBranchChild.Rows.Count > 0)
                    {
                        DataRow dr;
                        dr = dtbranch.NewRow();
                        dr[0] = 0;
                        dr[1] = "All";
                        dtbranch.Rows.Add(dr);
                        dtbranch.DefaultView.Sort = "BRANCH_ID ASC";
                        dtbranch = dtbranch.DefaultView.ToTable();
                    }
                }
                omodel.modelbranch = APIHelperMethods.ToModelList<GetBranch>(dtbranch);
                string h_id = omodel.modelbranch.First().BRANCH_ID.ToString();
                ViewBag.h_id = h_id;
                // End of Rev 2.0

                return View(omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetSalesSummaryList1()
        {
            return PartialView("PartialGetSalesSummaryList", GetSalesSummary("0"));
        }
        public ActionResult GetSalesParformanceList(SalesSummaryReport model)
        {
            try
            {

                DataTable dt = new DataTable();
                string frmdate = string.Empty;

                if (model.is_pageload == "0")
                {
                    frmdate = "Ispageload";
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


                //string state = "";
                //int i = 1;

                //if (model.StateId != null && model.StateId.Count > 0)
                //{
                //    foreach (string item in model.StateId)
                //    {
                //        if (i > 1)
                //            state = state + "," + item;
                //        else
                //            state = item;
                //        i++;
                //    }

                //}

                //string desig = "";
                //int j = 1;

                //if (model.desgid != null && model.desgid.Count > 0)
                //{
                //    foreach (string item in model.desgid)
                //    {
                //        if (j > 1)
                //            desig = desig + "," + item;
                //        else
                //            desig = item;
                //        j++;
                //    }
                //}

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
                //Rev 1.0 Mantis:0025586
                //if (model.IsRevisitContactDetails != null)
                //{
                //    TempData["IsRevisitContactDetails"] = model.IsRevisitContactDetails;
                //    TempData.Keep();
                //}
                //End of Rev 1.0 Mantis:0025586
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;

                // Rev 2.0
                string Branch_Id = "";
                int b = 1;
                if (model.BranchId != null && model.BranchId.Count > 0)
                {
                    foreach (string item in model.BranchId)
                    {
                        if (b > 1)
                            Branch_Id = Branch_Id + "," + item;
                        else
                            Branch_Id = item;
                        b++;
                    }
                }
                // End of Rev 2.0

                //if (days <= 7)
                //Mantis Issue 24728
                //if (days <= 30)
                if (days <= 35)
                //End of Mantis Issue 24728
                {
                    //  dt = objgps.GetSalesPerformanceReport(Branch_Id, datfrmat, dattoat, Userid, state, desig, empcode, model.IsRevisitContactDetails);

                    var pathA = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"] + "ShopRevisitAudio/";

                    DataTable ds = new DataTable();
                    ProcedureExecute proc = new ProcedureExecute("PRC_DAILYCALLREPORT");

                    proc.AddPara("@FROMDATE", datfrmat);
                    proc.AddPara("@TODATE", dattoat);
                    proc.AddPara("@EMPID", empcode);
                    proc.AddPara("@ISREVISITCONTACTDETAILS", 0);
                    proc.AddPara("@USERID", Userid);
                    proc.AddPara("@BRANCHID", Branch_Id);
                    proc.AddPara("@WEBURL", pathA);
                    ds = proc.GetTable();

                }

                return PartialView("PartialDailyCallReport", GetSalesSummary(frmdate));


            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
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

        public IEnumerable GetSalesSummary(string frmdate)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Daily Call Report");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDAILYCALL_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDAILYCALL_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }


        public ActionResult ExporPerformanceList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetSalesSummary(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetSalesSummary(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetSalesSummary(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetSalesSummary(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetSalesSummary(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Daily Call Report");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            var settings = new GridViewSettings();
            settings.Name = "Daily Call Report";
            settings.CallbackRouteValues = new { Controller = "DailyCallReport", Action = "GetSalesParformanceList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Daily Call Report";
            //Rev 5.0 Mantis: "x.Width" replace with "x.ExportWidth" in all columns and column width adjustment for export excel
            //Rev 1.0 Mantis: 0025586


            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                x.Caption = "Employee Name";
                x.VisibleIndex = 1;
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
                x.FieldName = "EMPID";
                x.Caption = "Employee Login ID";
                x.VisibleIndex = 2;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPID'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
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
                x.FieldName = "BRANCHDESC";
                x.Caption = "Branch";
                x.VisibleIndex = 3;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BRANCHDESC'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
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
                x.FieldName = "SHOP_NAME";
                x.Caption = "Shop Name";
                x.VisibleIndex = 4;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_NAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
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
                x.FieldName = "SHOP_TYPE";
                x.Caption = "Shop Type";
                x.VisibleIndex = 5;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_TYPE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.ExportWidth = 80;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.ExportWidth = 80;
                }

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "PARTYSTATUSTYPE";
                x.Caption = "Shop Status";
                x.VisibleIndex = 6;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PARTYSTATUSTYPE'");
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
                x.FieldName = "CONTACT_PERSON_NAME";
                x.Caption = "Owner Name";
                x.VisibleIndex = 7;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_PERSON_NAME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
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
                x.FieldName = "CONTACT_NUMBER";
                x.Caption = "Owner Contact";
                x.VisibleIndex = 8;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NUMBER'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
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
                x.FieldName = "SHOPADDR";
                x.Caption = "Shop Address";
                x.VisibleIndex = 9;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPADDR'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
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
                x.FieldName = "VISITED_TIME";
                x.Caption = "Visited Time";
                x.VisibleIndex = 10;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='VISITED_TIME'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
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
                x.FieldName = "SPENT_DURATION";
                x.Caption = "Duration Spend";
                x.VisibleIndex = 11;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SPENT_DURATION'");
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


            //End of Rev 1.0 Mantis: 0025586
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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "Daily Call Report");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult DownloadAudio(string audiofile)
        {

            //string FileName = "PartyList.xlsx";
            //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            //response.ClearContent();
            //response.Clear();
            //response.ContentType = "audio/wav";
            //response.AddHeader("Content-Disposition", "attachment; filename=" + "DailyCallReport.wav" + ";");
            //response.TransmitFile(audiofile);
            //response.Flush();
            //response.End();

            // string fileUrl = "http://3.7.30.86:8072/CommonFolder/ShopRevisitAudio/ifgenwzhsycopxqaaozxiktx_11984_1724745331108.wav";

            string filename = System.IO.Path.GetFileName(audiofile);

            WebClient client = new WebClient();
            byte[] fileData = client.DownloadData(audiofile);

            return File(fileData, "audio/mp3", filename);

            //return null;
        }
    }
}