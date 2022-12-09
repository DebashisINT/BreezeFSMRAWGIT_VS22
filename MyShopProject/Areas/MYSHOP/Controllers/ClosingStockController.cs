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

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ClosingStockController : Controller
    {
        //
        // GET: /MYSHOP/ClosingStock/
        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();
        public ActionResult ClosingStock()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            ds.Tables.Add(dt);
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            return View(ds);
        }
        public PartialViewResult ClosingStockGridView(ClosingStock model)
        {

            string pageload = string.Empty;

            if (model.is_pageload == "0")
            {
                pageload = "Ispageload";
            }

            if (model.Fromdate == null)
            {
                model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
            }

            if (model.Todate == null)
            {
                model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
            }


            string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
            string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
            string Userid = Convert.ToString(Session["userid"]);

            string state = "";
            string desig = "";
            int j = 1;

            if (model.StateId != null && model.StateId.Count > 0)
            {
                foreach (string item in model.StateId)
                {
                    if (j > 1)
                        state = state + "," + item;
                    else
                        state = item;
                    j++;
                }
            }
            if (model.desgid != null && model.desgid.Count > 0)
            {
                foreach (string item in model.StateId)
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
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                
                    ProcedureExecute proc = new ProcedureExecute("PRC_FTSCLOSINGSTOCK_REPORT");
                    //  proc.AddVarcharPara("@Action", 100, "GetAllDropDownDetailForSalesQuotation");
                    proc.AddVarcharPara("@FROMDATE", 10, datfrmat);
                    proc.AddVarcharPara("@TODATE", 10, dattoat);
                    proc.AddVarcharPara("@STATEID", 500, state);
                    proc.AddVarcharPara("@DESIGNID", 500, desig);
                    proc.AddVarcharPara("@EMPID", 500, empcode);
                    ds = proc.GetDataSet();
                
            }
            else
            {
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
            }
            TempData["ClosingStockGridView"] = ds;
            return PartialView(ds);
        }


        public ActionResult ExportClosingStockGridView(int type)
        {
            //Rev Tanmoy get data through execute query
            //DataTable dbDashboardData = new DataTable();
            //DBEngine objdb = new DBEngine();
            //String query = TempData["DashboardGridView"].ToString();
            //dbDashboardData = objdb.GetDataTable(query);

            ViewData["ClosingStockGridView"] = TempData["ClosingStockGridView"];

            DataSet DS = (DataSet)ViewData["ClosingStockGridView"];

            //End Rev
            TempData.Keep();

            if (ViewData["ClosingStockGridView"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridView(ViewData["ClosingStockGridView"]), DS.Tables[1]);
                    //break;
                    case 2:
                        //return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["ClosingStockGridView"]), DS.Tables[1]);
                        return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["ClosingStockGridView"]), DS.Tables[1], new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridView(ViewData["ClosingStockGridView"]), DS.Tables[1]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridView(ViewData["ClosingStockGridView"]), DS.Tables[1]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridView(ViewData["ClosingStockGridView"]), DS.Tables[1]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetDashboardGridView(object dataset)
        {
            var settings = new GridViewSettings();
            settings.Name = "Closing Stock";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Closing Stock";
            String ID = Convert.ToString(TempData["ClosingStockGridView"]);
            TempData.Keep();
            DataSet dt = (DataSet)dataset;



            System.Data.DataTable dtColumnTable = dt.Tables[0];


            //dtColumnTable = Model.Tables[0];
            //System.Data.DataRow[] drrRow = dtColumnTable.Select();
            //dtColumnTable = Model.Tables[0];
            //System.Data.DataRow[] drrRow = dtColumnTable.Select();

            foreach (System.Data.DataRow dr in dtColumnTable.Rows)
            {
                settings.Columns.Add(x =>
                {
                    x.FieldName = Convert.ToString(dr["HEADSHRTNAME"]);
                    x.Caption = Convert.ToString(dr["HEADNAME"]).Trim();
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    if (Convert.ToString(dr["HEADNAME"]).Trim() == "VALUE")
                    {
                        x.PropertiesEdit.DisplayFormatString = "0.00";
                    }
                    // x.VisibleIndex = i;
                    //x.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

                });
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