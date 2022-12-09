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
    public class DashboardReportController : Controller
    {
        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();

        public ActionResult Report(string Type)
        {
            try
            {
                TempData["Type"] = Type;

                SalesSummaryReport omodel = new SalesSummaryReport();
                string userid = Session["userid"].ToString();
                // dtuser = lstuser.GetUserList(userid);
                //dtshop = lstuser.GetShopList();
                // dtuser = lstuser.GetUserList();
                // List<GetUserName> model = new List<GetUserName>();

                // model = APIHelperMethods.ToModelList<GetUserName>(dtuser);


                List<GetUsersStates> modelstate = new List<GetUsersStates>();
                DataTable dtstate = lstuser.GetStateList();
                modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);

                DataTable dtdesig = lstuser.Getdesiglist();
                List<GetDesignation> modeldesig = new List<GetDesignation>();
                modeldesig = APIHelperMethods.ToModelList<GetDesignation>(dtdesig);

                //DataTable dtemp = lstuser.Getemplist();
                //List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
                //modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(dtemp);


                //omodel.userlsit = model;
                //omodel.selectedusrid = userid;


                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.states = modelstate;
                omodel.designation = modeldesig;
                //omodel.employee = modelemp;



                //if (TempData["Orderregister"] != null)
                //{
                //    omodel.selectedusrid = TempData["Orderregister"].ToString();

                //    TempData.Clear();
                //}
                return View(omodel);


            }
            catch(Exception ex)
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult GetSalesSummaryList(SalesSummaryReport model)
        {
            //try
            //{
                //String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                //List<GpsStatusClasstOutput> omel = new List<GpsStatusClasstOutput>();

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

                string Type = Convert.ToString(TempData["Type"]);
                TempData.Keep();

                // Rev Sanchita
               // dt = objgps._GetSalesSummaryReport(dattoat, Userid, state, desig, empcode, Type);
                dt = objgps._GetSalesSummaryReport(dattoat, Userid, state, desig, empcode, Type,"");
            // end of Rev SAnchita
                //dt = objgps.GetSalesSummaryReport(datfrmat, dattoat, Userid, state, desig, empcode);

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

                //modelstate.Add(new GetUsersStates()
                //{
                //    ID="",
                //    StateName="All"
                //});

                //for (int i = 0; i < dtstate.Rows.Count;i++ )
                //{


                //    modelstate.Add(new GetUsersStates()
                //    {
                //        ID = Convert.ToString(dtstate.Rows[i]["ID"]),
                //        StateName = Convert.ToString(dtstate.Rows[i]["StateName"])
                //    });
                //}
                // modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);



                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_StatesPartial.cshtml", modelstate);

                //return PartialView("~/Views/HRPayroll/payrollTableFormula/PartialFormulaGrid.cshtml", modelstate);

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

                modeldesig.Add(new GetDesignation()
                {
                    desgid = "",
                    designame = "All"
                });

                for (int i = 0; i < dtdesig.Rows.Count; i++)
                {


                    modeldesig.Add(new GetDesignation()
                    {
                        desgid = Convert.ToString(dtdesig.Rows[i]["desgid"]),
                        designame = Convert.ToString(dtdesig.Rows[i]["designame"])
                    });
                }




                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_DesigPartial.cshtml", modeldesig);

                //return PartialView("~/Views/HRPayroll/payrollTableFormula/PartialFormulaGrid.cshtml", modelstate);

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

                //modelemp.Add(new GetAllEmployee()
                //{
                //    empcode = "",
                //    empname = "All"
                //});

                //for (int j = 0; j < dtemp.Rows.Count; j++)
                //{


                //    modelemp.Add(new GetAllEmployee()
                //    {
                //        empcode = Convert.ToString(dtemp.Rows[j]["empcode"]),
                //        empname = Convert.ToString(dtemp.Rows[j]["empname"])
                //    });
                //}




                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmpPartial.cshtml", modelemp);

                //return PartialView("~/Views/HRPayroll/payrollTableFormula/PartialFormulaGrid.cshtml", modelstate);

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
            string Type = Convert.ToString(TempData["Type"]);
            TempData.Keep();

            if (frmdate != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDASHBOARD_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.ACTION == Type
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDASHBOARD_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.DESIGNATION == frmdate && d.ACTION == Type
                        orderby d.SEQ ascending
                        select d;
                return q;
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
                //break;

                default:
                    break;
            }

            return null;
        }
        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Summary Report";
            settings.CallbackRouteValues = new { Controller = "DashboardReport", Action = "GetSalesSummaryList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Summary Report";


            settings.Columns.Add(column =>
            {
                column.Caption = "Employee";
                column.FieldName = "EMPNAME";
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
                column.Caption = "Contact No.";
                column.FieldName = "REPORTTO";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
    }
}