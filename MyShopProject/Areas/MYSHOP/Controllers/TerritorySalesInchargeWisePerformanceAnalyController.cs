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
    public class TerritorySalesInchargeWisePerformanceAnalyController : Controller
    {
        //
        // GET: /MYSHOP/TerritorySalesInchargeWisePerformanceAnaly/
        UserList lstuser = new UserList();
        TerritorySalesInchargeWisePerformanceAnaly objgps = null;

        public TerritorySalesInchargeWisePerformanceAnalyController()
        {
            objgps = new TerritorySalesInchargeWisePerformanceAnaly();
        }
        public ActionResult TerritorySalesInchargeWisePerformanceAnaly()
        {
            try
            {
                TerritorySalesInchargeWisePerformanceAnalyModel omodel = new TerritorySalesInchargeWisePerformanceAnalyModel();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public PartialViewResult PartialTerritorySalesInchargeWisePerformanceAnaly(TerritorySalesInchargeWisePerformanceAnalyModel model)
        {
            DataTable dt = new DataTable();
            string frmdate = string.Empty;
            string Is_PageLoad = string.Empty;

            if (model.is_pageload == "0")
            {
                Is_PageLoad = "Is_pageload";
                model.Fromdate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                model.Todate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }

            if (model.is_procfirst == 1)
            {
                ViewData["ModelData"] = model;

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

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {

                    if (model.is_pageload == "0")
                    {
                        model.Fromdate = model.Fromdate.Split('-')[0] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[2];
                    }
                    else
                    {
                        model.Fromdate = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                    }

                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {

                    if (model.is_pageload == "0")
                    {
                        model.Todate = model.Todate.Split('-')[0] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[2];
                    }
                    else
                    {
                        model.Todate = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                    }

                }

                string FromDate = model.Fromdate;
                string ToDate = model.Todate;
                
                if (model.is_pageload == "1")
                {
                    double days = (Convert.ToDateTime(ToDate) - Convert.ToDateTime(FromDate)).TotalDays;
                    if (days <= 366)
                    {
                        dt = objgps.GetTerritorySalesInchargeWisePerformanceAnalyReport(FromDate,ToDate, state, empcode, Userid);
                    }
                }
            }
            return PartialView("PartialTerritorySalesInchargeWisePerformanceAnaly", GetTSIWisePerformanceAnaly(Is_PageLoad));
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

        public IEnumerable GetTSIWisePerformanceAnaly(string Is_PageLoad)
        {
            try
            {
                DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "TERRITORY SALES INCHARGE WISE PERFORMANCE ANALYTICS");
                if (dtColmn != null && dtColmn.Rows.Count > 0)
                {
                    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
                }

                string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
                string Userid = Convert.ToString(Session["userid"]);

                if (Is_PageLoad != "Is_pageload")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.FTSTERRITORYSALESINCHARGEPERFORMANCEANALY_REPORTs
                            where d.USERID == Convert.ToInt32(Userid)
                            orderby d.SEQ ascending
                            select d;
                    return q;
                }
                else
                {
                    if (Is_PageLoad != "Is_pageload")
                    {
                        ReportsDataContext dc = new ReportsDataContext(connectionString);
                        var q = from d in dc.FTSTERRITORYSALESINCHARGEPERFORMANCEANALY_REPORTs
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

        public ActionResult ExporTSIWisePerformanceAnalyList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(PartialTerritorySalesInchargeWisePerformanceAnaly(), GetTSIWisePerformanceAnaly(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(PartialTerritorySalesInchargeWisePerformanceAnaly(), GetTSIWisePerformanceAnaly(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(PartialTerritorySalesInchargeWisePerformanceAnaly(), GetTSIWisePerformanceAnaly(""));
                case 4:
                    return GridViewExtension.ExportToRtf(PartialTerritorySalesInchargeWisePerformanceAnaly(), GetTSIWisePerformanceAnaly(""));
                case 5:
                    return GridViewExtension.ExportToCsv(PartialTerritorySalesInchargeWisePerformanceAnaly(), GetTSIWisePerformanceAnaly(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings PartialTerritorySalesInchargeWisePerformanceAnaly()
        {
            var settings = new GridViewSettings();
            settings.Name = "Territory Sales incharge Wise Performance Analytics";
            settings.CallbackRouteValues = new { Controller = "TerritorySalesInchargeWisePerformanceAnaly", Action = "PartialTerritorySalesInchargeWisePerformanceAnaly" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "TerritorySalesInchargeAnaly";

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
                column.Caption = "TTotal Order (Total Order without Visit)";
                column.FieldName = "TOTALORDVALWITHOUTVISIT";
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