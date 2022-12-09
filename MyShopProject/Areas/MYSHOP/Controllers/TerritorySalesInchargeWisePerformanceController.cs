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
    public class TerritorySalesInchargeWisePerformanceController : Controller
    {
        //
        // GET: /MYSHOP/TerritorySalesInchargeWisePerformance/
        UserList lstuser = new UserList();
        TerritorySalesInchargeWisePerformance objgps = null;
        public TerritorySalesInchargeWisePerformanceController()
        {
            objgps = new TerritorySalesInchargeWisePerformance();
        }
        public ActionResult TerritorySalesInchargeWisePerformance()
        {
            try
            {
                TerritorySalesInchargeWisePerformanceModel omodel = new TerritorySalesInchargeWisePerformanceModel();
                string userid = Session["userid"].ToString();

                List<TDDPMonth> Pmonth = new List<TDDPMonth>();

                DataTable dtmnth = objgps.GetMonthList();
                if(dtmnth!=null && dtmnth.Rows.Count>0)
                {
                    foreach(DataRow item in dtmnth.Rows)
                    {
                        Pmonth.Add(new TDDPMonth
                        {
                            TMID = Convert.ToString(item["MID"]),
                            TMonthName = Convert.ToString(item["MONTHNAMEOFYEAR"])
                        });
                    }
                }

                List<TDDPYears> year = new List<TDDPYears>();

                DataTable dtyr = objgps.GetYearList();
                if (dtyr != null && dtyr.Rows.Count > 0)
                {
                    foreach (DataRow item in dtyr.Rows)
                    {
                        year.Add(new TDDPYears
                        {
                            TID = Convert.ToString(item["YEARS"]),
                            TYearName = Convert.ToString(item["YEARS"])
                        });
                    }
                }

                omodel.MonthList = Pmonth;
                omodel.YearList = year;
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public PartialViewResult PartialTerritorySalesInchargeWisePerformance(TerritorySalesInchargeWisePerformanceModel TerritorySalesInchargeWisePerformance)
        {
            DataTable dt = new DataTable();
            string frmdate = string.Empty;

            if (TerritorySalesInchargeWisePerformance.is_pageload == "0")
            {
                frmdate = "Ispageload";
            }

            if (TerritorySalesInchargeWisePerformance.is_procfirst == 1)
            {
                ViewData["ModelData"] = TerritorySalesInchargeWisePerformance;

                string month = TerritorySalesInchargeWisePerformance.Month;
                string year = TerritorySalesInchargeWisePerformance.Year;
                string Userid = Convert.ToString(Session["userid"]);

                string state = "";
                int i = 1;

                if (TerritorySalesInchargeWisePerformance.StateId != null && TerritorySalesInchargeWisePerformance.StateId.Count > 0)
                {
                    foreach (string item in TerritorySalesInchargeWisePerformance.StateId)
                    {
                        if (i > 1)
                            state = state + "," + item;
                        else
                            state = item;
                        i++;
                    }
                }

                string empcode = "";
                int k = 1;

                if (TerritorySalesInchargeWisePerformance.empcode != null && TerritorySalesInchargeWisePerformance.empcode.Count > 0)
                {
                    foreach (string item in TerritorySalesInchargeWisePerformance.empcode)
                    {
                        if (k > 1)
                            empcode = empcode + "," + item;
                        else
                            empcode = item;
                        k++;
                    }
                }
                dt = objgps.GetTerritorySalesInchargeWisePerformanceReport(month, state, empcode, year,Userid);
            }
            return PartialView("PartialTerritorySalesInchargeWisePerformance", GetTSIWisePerformance(frmdate));
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

        public IEnumerable GetTSIWisePerformance(string frmdate)
        {
            try
            {
                DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "TERRITORY SALES INCHARGE WISE PERFORMANCE");
                if (dtColmn != null && dtColmn.Rows.Count > 0)
                {
                    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
                }

                string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
                string Userid = Convert.ToString(Session["userid"]);

                if (frmdate != "Ispageload")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.FTSTERRITORYSALESINCHARGEWISEPERFORMANCE_REPORTs
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
                        var q = from d in dc.FTSTERRITORYSALESINCHARGEWISEPERFORMANCE_REPORTs
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

        public ActionResult ExporTSIWisePerformanceList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(PartialTerritorySalesInchargeWisePerformance(), GetTSIWisePerformance(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(PartialTerritorySalesInchargeWisePerformance(), GetTSIWisePerformance(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(PartialTerritorySalesInchargeWisePerformance(), GetTSIWisePerformance(""));
                case 4:
                    return GridViewExtension.ExportToRtf(PartialTerritorySalesInchargeWisePerformance(), GetTSIWisePerformance(""));
                case 5:
                    return GridViewExtension.ExportToCsv(PartialTerritorySalesInchargeWisePerformance(), GetTSIWisePerformance(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings PartialTerritorySalesInchargeWisePerformance()
        {
            var settings = new GridViewSettings();
            settings.Name = "Territory Sales incharge Wise Performance";
            settings.CallbackRouteValues = new { Controller = "TerritorySalesInchargeWisePerformance", Action = "PartialTerritorySalesInchargeWisePerformance" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "TerritorySalesIncharge";

            settings.Columns.Add(column =>
            {
                column.Caption = "TSI Name";
                column.FieldName = "EMPNAME";
            });           

            settings.Columns.Add(column =>
            {
                column.Caption = "District";
                column.FieldName = "EMPCITY";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "STATE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Supervisor";
                column.FieldName = "REPORTTO";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Revenue (Total Order Booked)";
                column.FieldName = "TOTALORDERVALUE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Targeted Revenue (Total Targeted Value)";
                column.FieldName = "TARGETORDVALUE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Average Order Value (Order Value/Order Count)";
                column.FieldName = "AVGORDERVALUE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Average Revenue per store (Total Order Value/Total Outlets)";
                column.FieldName = "AVGREVPERSTORE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Revenue/Expenses Ratio (Total Order Value/Approved expenses)";
                column.FieldName = "APPROVEEXPAMT";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Average Revenue Generated Per Visit (Total Order Value/Total Visit Count)";
                column.FieldName = "AVGREVPERVISIT";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Number of Visits (Total Visits)";
                column.FieldName = "TOTAL_VISIT";
                column.PropertiesEdit.DisplayFormatString = "0";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Number of New Visits (New Visit)";
                column.FieldName = "NEWSHOP_VISITED";
                column.PropertiesEdit.DisplayFormatString = "0";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Number of Re-visits (Re-visit)";
                column.FieldName = "RE_VISITED";
                column.PropertiesEdit.DisplayFormatString = "0";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Visit Conversion Rate (%) (Number of Productive visit(Order)/Total number of Visits)";
                column.FieldName = "VISITCONVRATE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "New Visit Conversion Rate (Total New Productive Visit(Order)/Total number of New visits)";
                column.FieldName = "NEWVISITCONVRATE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Re-visit Conversion Rate (Total revisit productive (Order)/Total number of revisit)";
                column.FieldName = "REVISITCONVRATE";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Visit Time (HH:MM)";
                column.FieldName = "TOTALVISITTIME";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Average Visit Time (HH:MM)";
                column.FieldName = "AVGVISITTIME";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Number of Days with No Visits (If no revisit/new visit for day, calculate as 1 count)";
                column.FieldName = "NOVISITDAYS";
                column.PropertiesEdit.DisplayFormatString = "0";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Average Time to First Call (SUM(Time at First Call-Log in Time)/Attendance Count)";
                column.FieldName = "AVGTIMETOFSTCALL";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Average Work Duration (HH:MM) (Total Attendance Hours/Attendance count in Month)";
                column.FieldName = "AVGWORKDURATION";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total KM travelled";
                column.FieldName = "TOTALKMTRAVELLED";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Average KM travelled";
                column.FieldName = "AVGKMTRAVELLED";
                column.PropertiesEdit.DisplayFormatString = "0.00";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Number of Full Days (Work duration above 7 hours 45 mins)";
                column.FieldName = "TOTNOOFFULLDAYS";
                column.PropertiesEdit.DisplayFormatString = "0";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Number of Half Days (Work Duration between 7:44 & 2 hours)";
                column.FieldName = "TOTNOOFHALFDAYS";
                column.PropertiesEdit.DisplayFormatString = "0";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Number of Leave Days (Approved Leave only)";
                column.FieldName = "TOTLEAVEDAYS";
                column.PropertiesEdit.DisplayFormatString = "0";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Number of Absent (Not providing attendance+less than 2 hours work+leave rejected/pending)";
                column.FieldName = "TOTALABSENT";
                column.PropertiesEdit.DisplayFormatString = "0";
                column.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                column.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "TERRITORY SALES INCHARGE WISE PERFORMANCE");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
	}
}