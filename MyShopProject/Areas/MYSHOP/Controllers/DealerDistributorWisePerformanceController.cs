/* ****************************************************************************************************************************
 * Rev 1.0		Sanchita	V2.0.39		16/03/2023		All months are not showing for Previous year while selecting parameter 
 *                                                      in Dealer/Distributor wise Sales report. Refer: 25732
**************************************************************************************************************************** */

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

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class DealerDistributorWisePerformanceController : Controller
    {
        //
        // GET: /MYSHOP/DealerDistributorWisePerformance/
        UserList lstuser = new UserList();
        DealerDistributorWisePerformance objgps = null;

        public DealerDistributorWisePerformanceController()
        {
            objgps = new DealerDistributorWisePerformance();
        }
        // Rev 1.0
        public JsonResult PupolateMonthList(string years)
        {
            DealerDistributorWisePerformanceModel omodel = new DealerDistributorWisePerformanceModel();
            List<DDPMonth> Pmonth = new List<DDPMonth>();

            if (years == null || years == "")
            {
                years = Session["years"].ToString();
            }


            Session["years"] = years;

            DataTable dtmnth = objgps.GetMonthList(years);
            if (dtmnth != null && dtmnth.Rows.Count > 0)
            {
                foreach (DataRow item in dtmnth.Rows)
                {
                    Pmonth.Add(new DDPMonth
                    {
                        MID = Convert.ToString(item["MID"]),
                        MonthName = Convert.ToString(item["MONTHNAMEOFYEAR"])
                    });

                }
            }
            
            var jsonResult = Json(Pmonth, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        // End of Rev 1.0
        public ActionResult DealerDistributorWisePerformance()
        {
            try
            {
                DealerDistributorWisePerformanceModel omodel = new DealerDistributorWisePerformanceModel();
                string userid = Session["userid"].ToString();

                // Rev 1.0
                //List<DDPMonth> Pmonth = new List<DDPMonth>();

                //DataTable dtmnth = objgps.GetMonthList();
                //if(dtmnth!=null && dtmnth.Rows.Count>0)
                //{
                //    foreach(DataRow item in dtmnth.Rows)
                //    {
                //        Pmonth.Add(new DDPMonth
                //        {
                //            MID = Convert.ToString(item["MID"]),
                //            MonthName = Convert.ToString(item["MONTHNAMEOFYEAR"])
                //        });
                //    }
                //}
                // End of Rev 1.0

                List<DDPYears> year = new List<DDPYears>();

                DataTable dtyr = objgps.GetYearList();
                if (dtyr != null && dtyr.Rows.Count > 0)
                {
                    foreach (DataRow item in dtyr.Rows)
                    {
                        year.Add(new DDPYears
                        {
                            ID = Convert.ToString(item["YEARS"]),
                            YearName = Convert.ToString(item["YEARS"])
                        });
                    }
                }

                // Rev 1.0
                //omodel.MonthList = Pmonth;
                // End of Rev 1.0

                omodel.YearList = year;

                // Rev 1.0
                string years = omodel.YearList.First().ID.ToString();
                ViewBag.years = years;


                List<DDPMonth> Pmonth = new List<DDPMonth>();

                DataTable dtmnth = objgps.GetMonthList(years);
                if (dtmnth != null && dtmnth.Rows.Count > 0)
                {
                    foreach (DataRow item in dtmnth.Rows)
                    {
                        Pmonth.Add(new DDPMonth
                        {
                            MID = Convert.ToString(item["MID"]),
                            MonthName = Convert.ToString(item["MONTHNAMEOFYEAR"])
                        });
                    }
                }
                omodel.MonthList = Pmonth;
                // End of Rev 1.0

                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public PartialViewResult PartialDealerDistributorWisePerformance(DealerDistributorWisePerformanceModel DealerDistributorWisePerformance)
        {
            DataTable dt = new DataTable();
            string frmdate = string.Empty;

            if (DealerDistributorWisePerformance.is_pageload == "0")
            {
                frmdate = "Ispageload";
            }

            if (DealerDistributorWisePerformance.is_procfirst == 1)
            {
                ViewData["ModelData"] = DealerDistributorWisePerformance;

                string month = DealerDistributorWisePerformance.Month;
                string year = DealerDistributorWisePerformance.Year;
                string Userid = Convert.ToString(Session["userid"]);

                string state = "";
                int i = 1;

                if (DealerDistributorWisePerformance.StateId != null && DealerDistributorWisePerformance.StateId.Count > 0)
                {
                    foreach (string item in DealerDistributorWisePerformance.StateId)
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

                //if (DealerDistributorWisePerformance.desgid != null && DealerDistributorWisePerformance.desgid.Count > 0)
                //{
                //    foreach (string item in DealerDistributorWisePerformance.desgid)
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

                if (DealerDistributorWisePerformance.empcode != null && DealerDistributorWisePerformance.empcode.Count > 0)
                {
                    foreach (string item in DealerDistributorWisePerformance.empcode)
                    {
                        if (k > 1)
                            empcode = empcode + "," + item;
                        else
                            empcode = item;
                        k++;
                    }
                }
                dt = objgps.GetDealerDistributorWisePerformanceReport(month, state, empcode, year,Userid);
            }
            return PartialView("PartialDealerDistributorWisePerformance", GetDDWisePerformance(frmdate));
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

        public ActionResult GetEmpList(ReimbursementReport model)
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

        public IEnumerable GetDDWisePerformance(string frmdate)
        {
            try
            {
                DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "DEALER/DISTRIBUTOR WISE SALES");
                if (dtColmn != null && dtColmn.Rows.Count > 0)
                {
                    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
                }

                string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
                string Userid = Convert.ToString(Session["userid"]);

                if (frmdate != "Ispageload")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.FTSDEALERDISTRIBUTORWISEPERFORMANCE_REPORTs
                            where d.USERID == Convert.ToInt32(Userid)
                            orderby d.SEQ ascending
                            select d;
                    return q;
                }
                else
                {
                    if (frmdate != "Ispageload")
                    {
                        ReportsDataContext dc = new ReportsDataContext(connectionString);
                        var q = from d in dc.FTSDEALERDISTRIBUTORWISEPERFORMANCE_REPORTs
                                where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                                orderby d.SEQ ascending
                                select d;
                        return q;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public ActionResult ExporDDWisePerformanceList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(PartialDealerDistributorWisePerformance(), GetDDWisePerformance(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(PartialDealerDistributorWisePerformance(), GetDDWisePerformance(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(PartialDealerDistributorWisePerformance(), GetDDWisePerformance(""));
                case 4:
                    return GridViewExtension.ExportToRtf(PartialDealerDistributorWisePerformance(), GetDDWisePerformance(""));
                case 5:
                    return GridViewExtension.ExportToCsv(PartialDealerDistributorWisePerformance(), GetDDWisePerformance(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings PartialDealerDistributorWisePerformance()
        {
            var settings = new GridViewSettings();
            settings.Name = "Dealer/Distributor Wise Sales";
            settings.CallbackRouteValues = new { Controller = "DealerDistributorWisePerformance", Action = "PartialDealerDistributorWisePerformance" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "DealerDistributorWiseSales";

            settings.Columns.Add(column =>
            {
                column.Caption = "Dealer/Distributor Name";
                column.FieldName = "SHOP_NAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Type";
                column.FieldName = "SHOP_TYPE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "City/District";
                column.FieldName = "SHOPCITY";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "STATE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Sales Person Name";
                column.FieldName = "EMPNAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Revenue (Total Order Value)";
                column.FieldName = "TOTALORDERVALUE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Average Order Value (Total Order Value/ Number of successful order count for that shop)";
                column.FieldName = "AVGORDVALUE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Average Value Generated Per Visit (Total Order value/total number (Visit/revisit)of visit count)";
                column.FieldName = "AVGVISITVALUE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "DEALER/DISTRIBUTOR WISE SALES");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
	}
}