using BusinessLogicLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Models;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class PrimaryFundPlanController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrimaryFundPlan()
        {
            try
            {
                Reportorderregisterinput omodel = new Reportorderregisterinput();
                string userid = Session["userid"].ToString();
                omodel.selectedusrid = userid;


                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                // omodel.states = modelstate;
                //omodel.shoplist = modelshop;



                //if (TempData["Orderregister"] != null)
                //{
                //    omodel.selectedusrid = TempData["Orderregister"].ToString();

                //    TempData.Clear();
                //}
                return View("~/Areas/MYSHOP/Views/PrimaryFundPlan/PrimaryFundPlan.cshtml", omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetFundPlan()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            //if (Is_PageLoad != "Ispageload")
            //{
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.FTS_MasterFundPlans
                    select d;
            return q;
            //}
            //else
            //{
            //    ReportsDataContext dc = new ReportsDataContext(connectionString);
            //    var q = from d in dc.FTS_MasterFundPlans
            //            select d;
            //    return q;
            //}
        }

        public ActionResult GetFundPlanList()
        {

            return PartialView("~/Areas/MYSHOP/Views/PrimaryFundPlan/PartialPrimaryFundPlanGrid.cshtml", GetFundPlan());

        }

        [HttpPost]
        public ActionResult FundPlanAddNew(FTS_MasterFundPlan ftsAdd)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            using (ReportsDataContext dc = new ReportsDataContext(connectionString))
            {
                dc.FTS_MasterFundPlans.InsertOnSubmit(ftsAdd);
                dc.SubmitChanges();
            }

            return PartialView("~/Areas/MYSHOP/Views/PrimaryFundPlan/PartialPrimaryFundPlanGrid.cshtml", GetFundPlan());

        }

        [HttpPost]
        public ActionResult FundPlanUpdate(FTS_MasterFundPlan ftsUpdate)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            using (ReportsDataContext dc = new ReportsDataContext(connectionString))
            {

                var query = (from p in dc.FTS_MasterFundPlans
                             where p.PLAN_ID == ftsUpdate.PLAN_ID
                             select p).ToList();


                // Execute the query, and change the column values
                // you want to change.
                foreach (FTS_MasterFundPlan ord in query)
                {
                    ord.STATE_NAME = ftsUpdate.STATE_NAME;
                    ord.PARTY_NAME = ftsUpdate.PARTY_NAME;
                    ord.LOCATION = ftsUpdate.LOCATION;
                    ord.CONTACT_NO = ftsUpdate.CONTACT_NO;
                    ord.UPDATE_DATE = DateTime.Now;
                    // Insert any additional changes to column values.
                }

                // Submit the changes to the database.
                try
                {
                    dc.SubmitChanges();
                }
                catch (Exception e)
                {

                    // Provide for exceptions.
                }
            }


            return PartialView("~/Areas/MYSHOP/Views/PrimaryFundPlan/PartialPrimaryFundPlanGrid.cshtml", GetFundPlan());

        }

        [HttpPost]
        public ActionResult FundPlanDelete(FTS_MasterFundPlan ftsDelete)
        {
            DBEngine objdb = new DBEngine();
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            dt = objdb.GetDataTable("Select ID from FTS_UserDalyFundPlan where PLAN_ID='" + ftsDelete.PLAN_ID + "'");
            if (dt == null || dt.Rows.Count==0)
            {
                using (ReportsDataContext dc = new ReportsDataContext(connectionString))
                {
                    var query = (from p in dc.FTS_MasterFundPlans
                                 where p.PLAN_ID == ftsDelete.PLAN_ID
                                 select p).ToList();
                    dc.FTS_MasterFundPlans.DeleteAllOnSubmit(query);
                    dc.SubmitChanges();
                }
            }
            else
            {
                ViewBag.ErrorText = "Already used.";
            }

            return PartialView("~/Areas/MYSHOP/Views/PrimaryFundPlan/PartialPrimaryFundPlanGrid.cshtml", GetFundPlan());

        }

        [HttpPost]
        public ActionResult StateName_lostFocus()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;

            return PartialView("~/Areas/MYSHOP/Views/PrimaryFundPlan/PartialPrimaryFundPlanGrid.cshtml", GetFundPlan());

        }

        public ActionResult ExporRegisterList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetFundPlan());
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetFundPlan());
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetFundPlan());
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetFundPlan());
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetFundPlan());
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {

            var settings = new GridViewSettings();
            settings.Name = "PartyDetails";
            // settings.CallbackRouteValues = new { Controller = "InvoiceDeliveryRegister", Action = "GetRegisterreporttatusList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Party Details";

            settings.Columns.Add(x =>
            {
                x.FieldName = "STATE_NAME";
                x.Caption = "State Name";
                x.VisibleIndex = 1;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TYPE";
                x.Caption = "Type";
                x.VisibleIndex = 2;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "PARTY_NAME";
                x.Caption = "Party Name";
                x.VisibleIndex = 3;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CONTACT_NO";
                x.Caption = "Contact No";
                x.VisibleIndex = 3;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(15);

            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "LOCATION";
                x.Caption = "Location";
                x.VisibleIndex = 3;
                x.Width = System.Web.UI.WebControls.Unit.Percentage(25);
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