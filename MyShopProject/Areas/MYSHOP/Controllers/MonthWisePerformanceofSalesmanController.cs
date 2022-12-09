using DataAccessLayer;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class MonthWisePerformanceofSalesmanController : Controller
    {
        public ActionResult MonthWisePerformanceofSalesman()
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

        public PartialViewResult PartialMonthWiseSalesGridView(MonthwisePerformanceofSalesmanModel model)
        {

            string frmdate = string.Empty;

            if (model.is_pageload == "0")
            {
                frmdate = "Ispageload";
            }

            string Userid = Convert.ToString(Session["userid"]);

            string prod = "";
            int j = 1;

            if (model.ProductID != null && model.ProductID.Count > 0)
            {
                foreach (string item in model.ProductID)
                {
                    if (j > 1)
                        prod = prod + "," + item;
                    else
                        prod = item;
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
            if (model.is_pageload != "0")
            {
               
                    ProcedureExecute proc = new ProcedureExecute("PRC_FTSMONTHLYPERFORMANCEOFSALESPERSONNEL_REPORT");
                    proc.AddVarcharPara("@MONTH", 3, model.MonthID);
                    proc.AddVarcharPara("@YEAR", 10, model.YearID);
                    proc.AddVarcharPara("@EMPID", 500, empcode);
                    proc.AddVarcharPara("@PRODUCT_ID", 500, prod);
                    ds = proc.GetDataSet();

                    IEnumerable<DataRow> rows = ds.Tables[1].Rows.Cast<DataRow>().Where(r => r["Date"].ToString() == "01-12-2099");
                    // Loop through the rows and change the name.
                    rows.ToList().ForEach(r => r.SetField("Date", " "));
            }
            TempData["SalesGridView"] = ds;
            return PartialView(ds);
        }

        public ActionResult ExportSalesGridView(int type)
        {
            ViewData["SalesGridView"] = TempData["SalesGridView"];

            DataSet DS = (DataSet)ViewData["SalesGridView"];

            TempData.Keep();

            if (ViewData["SalesGridView"] != null)
            {
                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridView(ViewData["SalesGridView"]), DS.Tables[1]);
                    //break;
                    case 2:
                        //return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["SalesGridView"]), DS.Tables[1]);
                        return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["SalesGridView"]), DS.Tables[1], new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridView(ViewData["SalesGridView"]), DS.Tables[1]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridView(ViewData["SalesGridView"]), DS.Tables[1]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridView(ViewData["SalesGridView"]), DS.Tables[1]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetDashboardGridView(object dataset)
        {
            var settings = new GridViewSettings();
            settings.Name = "Monthly Performance Report of Sales Personnel";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Monthly Performance Report of Sales Personnel";
            String ID = Convert.ToString(TempData["SalesGridView"]);
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