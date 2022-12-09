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
    public class PerformanceVisitRegisterController : Controller
    {
        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();

        public ActionResult Report()
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

                return View(omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult GetSalesSummaryList1()
        {
            return PartialView("PartialGetSalesSummaryList", GetPerformanceVisitSummary("0"));
        }
        public ActionResult GetPerformanceVisitList(SalesSummaryReport model)
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
                if (days <= 30)
                {
                    dt = objgps.GetPerformanceVisitRegReport(datfrmat, dattoat, Userid, state, desig, empcode);
                }

                return PartialView("PartialGetPerformanceVisitSummary", GetPerformanceVisitSummary(frmdate));


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

        public IEnumerable GetPerformanceVisitSummary(string frmdate)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "PERFORMANCE VISIT REGISTER");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEPERFORMANCEVISITREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSEMPLOYEEPERFORMANCEVISITREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }

        public ActionResult ExportPerformanceVisitList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetPerformanceVisitSummary(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetPerformanceVisitSummary(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetPerformanceVisitSummary(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetPerformanceVisitSummary(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetPerformanceVisitSummary(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "PerformanceVisitRegisterReport";
            settings.CallbackRouteValues = new { Controller = "PerformanceVisitRegister", Action = "GetPerformanceVisitList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Performance Visit Register";

            settings.Columns.Add(x =>
            {
                x.FieldName = "WORK_DATE";
                x.Caption = "Date";
                x.VisibleIndex = 1;
                x.FixedStyle = GridViewColumnFixedStyle.Left;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                x.Caption = "Emp. Name";
                x.VisibleIndex = 2;
                x.FixedStyle = GridViewColumnFixedStyle.Left;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "STATE";
                x.Caption = "State";
                x.VisibleIndex = 3;
                x.Width = 100;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "LOGGEDIN";
                x.Caption = "Login Time";
                x.VisibleIndex = 4;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "LOGEDOUT";
                x.Caption = "Logout Time";
                x.VisibleIndex = 5;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "LOGINOUTLOCATION";
                x.Caption = "Login/Logout Location";
                x.VisibleIndex = 6;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACTNO";
                x.Caption = "Use id";
                x.VisibleIndex = 7;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "BRANCHDESC";
                x.Caption = "Branch";
                x.VisibleIndex = 8;
                x.Width = 180;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OFFICE_ADDRESS";
                x.Caption = "Office Address";
                x.VisibleIndex = 9;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ATTEN_STATUS";
                x.Caption = "Attendance";
                x.VisibleIndex = 10;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "WORK_LEAVE_TYPE";
                x.Caption = "Work/Leave Type";
                x.VisibleIndex = 11;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "REMARKS";
                x.Caption = "Remarks";
                x.VisibleIndex = 12;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPID";
                x.Caption = "Emp. ID";
                x.VisibleIndex = 13;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DESIGNATION";
                x.Caption = "Designation";
                x.VisibleIndex = 14;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DATEOFJOINING";
                x.Caption = "DOJ";
                x.VisibleIndex = 15;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "REPORTTO";
                x.Caption = "Supervisor";
                x.VisibleIndex = 16;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "RPTTODESG";
                x.Caption = "Supervisor Desg.";
                x.VisibleIndex = 17;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "COMPNAME";
                x.Caption = "Company Name";
                x.VisibleIndex = 18;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DISTRIBUTORNAME";
                x.Caption = "Distributor Name";
                x.VisibleIndex = 19;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DISTRIBUTORADD";
                x.Caption = "Dist. Address";
                x.VisibleIndex = 20;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DISTRIBUTORMOB";
                x.Caption = "Dist. Mobile no.";
                x.VisibleIndex = 21;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CUSTNAME";
                x.Caption = "Customer/Shop Name";
                x.VisibleIndex = 22;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CUSTTYPE";
                x.Caption = "Customer Type";
                x.VisibleIndex = 23;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CUSTADD";
                x.Caption = "Customer Address";
                x.VisibleIndex = 24;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CUSTMOB";
                x.Caption = "Customer Mobile no.";
                x.VisibleIndex = 25;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "VISITTYPE";
                x.Caption = "Visit Type";
                x.VisibleIndex = 26;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "MEETINGREMARKS";
                x.Caption = "Meeting Remarks";
                x.VisibleIndex = 27;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "VISITREMARKS";
                x.Caption = "Feedback";
                x.VisibleIndex = 28;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SHPVISITDATETIME";
                x.Caption = "Visit Date/Time (DD-MM-YYYY HH:MM:SS)";
                x.VisibleIndex = 29;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SPENT_DURATION";
                x.Caption = "Visit Duration";
                x.VisibleIndex = 30;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTAL_VISIT";
                x.Caption = "Visit Count";
                x.VisibleIndex = 31;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "RE_VISITED";
                x.Caption = "Re Visit";
                x.VisibleIndex = 32;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "NEWSHOP_VISITED";
                x.Caption = "New Visit";
                x.VisibleIndex = 33;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTMETTING";
                x.Caption = "Meeting";
                x.VisibleIndex = 34;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DISTANCE_TRAVELLED";
                x.Caption = "Travelled(KM)";
                x.VisibleIndex = 35;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ORDERSTATUS";
                x.Caption = "Visit Status";
                x.VisibleIndex = 36;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ORDERREMARKS";
                x.Caption = "Visit Remarks";
                x.VisibleIndex = 37;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTAL_ORDER_BOOKED_VALUE";
                x.Caption = "Sale Value";
                x.VisibleIndex = 38;
                x.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTAL_COLLECTION";
                x.Caption = "Collection value";
                x.VisibleIndex = 39;
                x.PropertiesEdit.DisplayFormatString = "0.00";
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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "PERFORMANCE VISIT REGISTER");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
	}
}