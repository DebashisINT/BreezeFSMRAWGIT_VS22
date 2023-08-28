/****************************************************************************************************************************************
    Written by Sanchita  for    V2.0.41      06-07-2023       A new report is required in FSM as "Order Summary(MTD) 
                                                              Mantis: 26429
 ***************************************************************************************************************************************/

using BusinessLogicLayer.SalesTrackerReports;
using DataAccessLayer;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using Models;
using MyShop.Models;
using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class OrderSummaryMTDController : Controller
    {
        UserList lstuser = new UserList();
        PerformanceSummaryMonthWise_List objgps = null;  // to get the Year List

        // GET: MYSHOP/OrderSummaryMTD
        public ActionResult Index()
        {
            OrderSummaryMTDModel omodel = new OrderSummaryMTDModel();
            string userid = Session["userid"].ToString();


            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);
            return View(ds);

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

        public PartialViewResult OrderSummaryMTDGridViewCallback(OrderSummaryMTDModel model)
        {

            string IsPageLoad = string.Empty;

            string month = model.Month;
            string year = model.Year;


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

            DataSet ds = new DataSet();
            if (model.is_pageload != "0" && model.is_pageload != null)
            {
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSORDERSUMMARYMTD_REPORT");
                proc.AddPara("@MONTH", month);
                proc.AddPara("@YEARS", year);
                proc.AddPara("@STATEID", state);
                proc.AddPara("@DESIGNID", desig);
                proc.AddPara("@EMPID", empcode);
                proc.AddPara("@USERID", Userid);
                ds = proc.GetDataSet();
            }
            else
            {
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
            }
            TempData["OrderSummaryMTDGridView"] = ds;
            return PartialView(ds);
        }

        public ActionResult ExporTOrderSummaryMTDList(int type)
        {
            //Rev Tanmoy get data through execute query
            //DataTable dbDashboardData = new DataTable();
            //DBEngine objdb = new DBEngine();
            //String query = TempData["DashboardGridView"].ToString();
            //dbDashboardData = objdb.GetDataTable(query);

            ViewData["OrderSummaryMTDGridView"] = TempData["OrderSummaryMTDGridView"];

            DataSet DS = (DataSet)ViewData["OrderSummaryMTDGridView"];

            //End Rev
            TempData.Keep();

            if (ViewData["OrderSummaryMTDGridView"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridView(ViewData["OrderSummaryMTDGridView"]), DS.Tables[1]);
                    //break;
                    case 2:
                        //return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["OrderSummaryMTDGridView"]), DS.Tables[1]);
                        return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["OrderSummaryMTDGridView"]), DS.Tables[1], new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridView(ViewData["OrderSummaryMTDGridView"]), DS.Tables[1]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridView(ViewData["OrderSummaryMTDGridView"]), DS.Tables[1]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridView(ViewData["OrderSummaryMTDGridView"]), DS.Tables[1]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetDashboardGridView(object dataset)
        {
            var settings = new GridViewSettings();
            settings.Name = "Order Summary(MTD) report";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Order Summary MTD";
            String ID = Convert.ToString(TempData["OrderSummaryMTDGridView"]);
            TempData.Keep();
            DataSet dt = (DataSet)dataset;



            System.Data.DataTable dtColumnTable = new System.Data.DataTable();

            if (dt != null && dt.Tables.Count > 0)
            {

                dtColumnTable = dt.Tables[0];
                if (dtColumnTable != null && dtColumnTable.Rows.Count > 0)
                {
                    System.Data.DataRow[] drr = dtColumnTable.Select("PARRENTID=0");
                    int i = 0;
                    foreach (System.Data.DataRow dr in drr)
                    {
                        i = i + 1;
                        System.Data.DataRow[] drrRow = dtColumnTable.Select("PARRENTID='" + Convert.ToString(dr["HEADID"]) + "'");

                        if (drrRow.Length > 0)
                        {

                            settings.Columns.AddBand(x =>
                            {
                                //x.FieldName = Convert.ToString(dr["HEADNAME"]);
                                x.Caption = Convert.ToString(dr["HEADNAME"]).Trim();
                                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                                x.VisibleIndex = i;
                                //x.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

                                foreach (System.Data.DataRow drrs in drrRow)
                                {
                                    System.Data.DataRow[] drrRows = dtColumnTable.Select("PARRENTID='" + Convert.ToString(drrs["HEADID"]) + "'");

                                    if (drrRows.Length > 0)
                                    {
                                        x.Columns.AddBand(xSecond =>
                                        {
                                            xSecond.Caption = Convert.ToString(drrs["HEADNAME"]).Trim();
                                            xSecond.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                                            foreach (System.Data.DataRow drrss in drrRows)
                                            {
                                                System.Data.DataRow[] drrRowss = dtColumnTable.Select("PARRENTID='" + Convert.ToString(drrss["HEADID"]) + "'");
                                                if (drrRowss.Length > 0)
                                                {

                                                    xSecond.Columns.AddBand(xThird =>
                                                    {
                                                        xThird.Caption = Convert.ToString(drrss["HEADNAME"]).Trim();
                                                        xThird.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                                                        foreach (System.Data.DataRow drrrrs in drrRowss)
                                                        {
                                                            xThird.Columns.Add(xFourth =>
                                                            {
                                                                xFourth.Caption = Convert.ToString(drrrrs["HEADNAME"]).Trim();
                                                                xFourth.FieldName = Convert.ToString(drrrrs["HEADSHRTNAME"]).Trim();
                                                                xFourth.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                                                            });
                                                        }

                                                    });


                                                }
                                                else
                                                {
                                                    xSecond.Columns.Add(xThird =>
                                                    {
                                                        xThird.Caption = Convert.ToString(drrss["HEADNAME"]).Trim();
                                                        xThird.FieldName = Convert.ToString(drrss["HEADSHRTNAME"]).Trim();
                                                        xThird.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                                                    });
                                                }

                                            }


                                        });

                                    }
                                    else
                                    {
                                        x.Columns.Add(xSecond =>
                                        {
                                            xSecond.Caption = Convert.ToString(drrs["HEADNAME"]).Trim();
                                            xSecond.FieldName = Convert.ToString(drrs["HEADSHRTNAME"]).Trim();
                                            xSecond.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

                                        });
                                    }

                                }

                            });



                        }
                        else
                        {
                            settings.Columns.Add(x =>
                            {
                                x.Caption = Convert.ToString(dr["HEADNAME"]).Trim();
                                x.FieldName = Convert.ToString(dr["HEADSHRTNAME"]).Trim();
                                x.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                                x.VisibleIndex = i;

                            });
                        }

                    }
                }

            }



            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
    }
}