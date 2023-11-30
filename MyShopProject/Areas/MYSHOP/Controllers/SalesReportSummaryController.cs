//====================================================== Revision History ==========================================================
//1.0  19-07-2023   V2.0.42   Priti     0026135: Branch Parameter is required for various FSM reports
//2.0  07-11-2023   V2.0.43   Sanchita  0026895: System will prompt for Branch selection if the Branch hierarchy is activated.
//====================================================== Revision History ==========================================================
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
using BusinessLogicLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class SalesReportSummaryController : Controller
    {

        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();
        // GET: MYSHOP/SalesReportSalary
        public ActionResult Report()
        {

            try
            {

                SalesSummaryReport omodel = new SalesSummaryReport();
                string userid = Session["userid"].ToString();


                //List<GetUsersStates> modelstate = new List<GetUsersStates>();
                //DataTable dtstate = lstuser.GetStateList();
                //modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);

                //DataTable dtdesig = lstuser.Getdesiglist();
                //List<GetDesignation> modeldesig = new List<GetDesignation>();
                //modeldesig = APIHelperMethods.ToModelList<GetDesignation>(dtdesig);


                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                //omodel.states = modelstate;
                //omodel.designation = modeldesig;

                DataTable dt = objgps.GetStateMandatory(Session["userid"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }
                // Rev 2.0
                DBEngine obj1 = new DBEngine();
                ViewBag.BranchMandatory = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsActivateEmployeeBranchHierarchy'").Rows[0][0]);
                // End of Rev 2.0

                //REV 1.0
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
                //REV 1.0 End
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
        public ActionResult GetSalesSummaryList(SalesSummaryReport model)
        {
            //try
            //{

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
            //Rev 1.0
            string Branch_Id = "";
            int l = 1;
            if (model.BranchId != null && model.BranchId.Count > 0)
            {
                foreach (string item in model.BranchId)
                {
                    if (l > 1)
                        Branch_Id = Branch_Id + "," + item;
                    else
                        Branch_Id = item;
                    l++;
                }
            }
            //Rev 1.0 End
            if (model.is_pageload != "0")
            {
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    //REV 1.0
                    //dt = objgps.GetSalesSummaryReport(datfrmat, dattoat, Userid, state, desig, empcode);
                    dt = objgps.GetSalesSummaryReport(datfrmat, dattoat, Userid, state, desig, empcode, Branch_Id);
                    //REV 1.0 END
                }
            }


            return PartialView("PartialGetSalesSummaryList", GetSalesSummary(frmdate));


            //}
            //catch
            //{
            //    return RedirectToAction("Logout", "Login", new { Area = "" });

            //}
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

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSSALESSUMMARY_REPORTs
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
                    var q = from d in dc.FTSSALESSUMMARY_REPORTs
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


        public ActionResult ExporSummaryList(int type)
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
            var settings = new GridViewSettings();
            //settings.Name = "Summary Report";
            settings.Name = "Employee Summary Report";
            settings.CallbackRouteValues = new { Controller = "SalesReportSummary", Action = "GetSalesSummaryList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            //settings.SettingsExport.FileName = "Summary Report";
            settings.SettingsExport.FileName = "Employee Summary Report";

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Login ID";
            //    column.FieldName = "USER_LOGINID";
            //});

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Employee";
            //    column.FieldName = "EMPNAME";
            //});

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "State";
            //    column.FieldName = "STATE";

            //});
            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Designation";
            //    column.FieldName = "DESIGNATION";

            //});

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Attendance Type";
            //    column.FieldName = "ATTEN_STATUS";
            //    // x.Width = System.Web.UI.WebControls.Unit.Percentage(10);

            //});

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Reports To";
            //    column.FieldName = "REPORTTO";


            //});

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Login Time";
            //    column.FieldName = "LOGGEDIN";


            //});

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Logout Time";
            //    column.FieldName = "LOGEDOUT";


            //});

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Total Working Duration (HH:MM)";
            //    column.FieldName = "TOTAL_HRS_WORKED";


            //});
            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Shop Visited";
            //    column.FieldName = "SHOPS_VISITED";

            //});

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Order Value";
            //    column.FieldName = "TOTAL_ORDER_BOOKED_VALUE";
            //    column.PropertiesEdit.DisplayFormatString = "0.00";  // add this line to avoid dollar sign
            //});

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Collection Amount";
            //    column.FieldName = "TOTAL_COLLECTION";
            //    column.PropertiesEdit.DisplayFormatString = "0.00"; // add this line to avoid dollar sign

            //});

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "GPS Inactive Duration (HH:MM)";
            //    column.FieldName = "GPS_INACTIVE_DURATION";

            //});

            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Idle Time (HH:MM)";
            //    column.FieldName = "IDEAL_TIME";
            //});


            //settings.Columns.Add(column =>
            //{
            //    column.Caption = "Idle Time Count";
            //    column.FieldName = "IDEALTIME_CNT";
            //});

            settings.Columns.Add(column =>
            {
                column.Caption = "Login ID";
                column.FieldName = "USER_LOGINID";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee ID";
                column.FieldName = "Employee_ID";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee(s)";
                column.FieldName = "EMPNAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "STATE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Branch";
                column.FieldName = "BRANCHDESC";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "DESIGNATION";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Supervisor";
                column.FieldName = "REPORTTO";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Attendance Type";
                column.FieldName = "ATTEN_STATUS";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Work/Leave Type";
                column.FieldName = "WORK_LEAVE_TYPE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Login Time";
                column.FieldName = "LOGGEDIN";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Logout Time";
                column.FieldName = "LOGEDOUT";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Working Duration (HH:MM)";
                column.FieldName = "TOTAL_HRS_WORKED";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Undertime (HH:MM)";
                column.FieldName = "UNDERTIME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "GPS Inactive Duration (HH:MM)";
                column.FieldName = "GPS_INACTIVE_DURATION";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Idle Time (HH:MM)";
                column.FieldName = "IDEAL_TIME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Idle Time Count";
                column.FieldName = "IDEALTIME_CNT";
                
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Visited";
                column.FieldName = "SHOPS_VISITED";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Value";
                column.FieldName = "TOTAL_ORDER_BOOKED_VALUE";
                column.PropertiesEdit.DisplayFormatString = "0.00";  // add this line to avoid dollar sign
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Collection Amount";
                column.FieldName = "TOTAL_COLLECTION";
                column.PropertiesEdit.DisplayFormatString = "0.00"; // add this line to avoid dollar sign
            });

         //   settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "SHOPS_VISITED").ShowInColumn = "Total: {0}";

            


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
    }
}