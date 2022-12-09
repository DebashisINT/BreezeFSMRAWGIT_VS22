using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using System.Data;
using MyShop.Models;
using UtilityLayer;
using DevExpress.Web.Mvc;
using DevExpress.Web;
namespace MyShop.Areas.MYSHOP.Controllers
{
    public class TravelConveyanceController : Controller
    {

        DataTable dtvisitloc = new DataTable();
        DataTable dtexpensetype = new DataTable();
        DataTable dtdesignation = new DataTable();
        DataTable dttravelmode = new DataTable();
        DataTable dtgrade = new DataTable();
        DataTable dtstate = new DataTable();
        DataTable dtconveyance = new DataTable();
        DataTable dtfuel = new DataTable();
        TravelConveyanceClass lstuser = new TravelConveyanceClass();

        List<Class_Master> modelvisit = new List<Class_Master>();
        List<Class_Master> modelexpense = new List<Class_Master>();
        List<Class_Master> modeldesgnation = new List<Class_Master>();
        List<Class_Master> modeltravelmode = new List<Class_Master>();
        List<Class_Master> modelfuelmode = new List<Class_Master>();

        List<Class_Master> modelemployeegrade = new List<Class_Master>();
        List<Class_Master> modelsate = new List<Class_Master>();

        TravelConveyanceModelclass omodel = new TravelConveyanceModelclass();
        public ActionResult Configuration()
        {
           


            string userid = Convert.ToString(Session["userid"]);
            dtvisitloc = lstuser.GetConveyanceTypes("VisitLocation");
            dtexpensetype = lstuser.GetConveyanceTypes("Expense");
            dtdesignation = lstuser.GetConveyanceTypes("Designation");

            dttravelmode = lstuser.GetConveyanceTypes("TravelMode");

            dtgrade = lstuser.GetConveyanceTypes("EmpGrade");
            dtstate = lstuser.GetConveyanceTypes("State");
            dtfuel = lstuser.GetConveyanceTypes("Fuel");


            modelfuelmode = APIHelperMethods.ToModelList<Class_Master>(dtfuel);
            modelvisit = APIHelperMethods.ToModelList<Class_Master>(dtvisitloc);
            modelexpense = APIHelperMethods.ToModelList<Class_Master>(dtexpensetype);
            modeldesgnation = APIHelperMethods.ToModelList<Class_Master>(dtdesignation);
            modeltravelmode = APIHelperMethods.ToModelList<Class_Master>(dttravelmode);
            modelemployeegrade = APIHelperMethods.ToModelList<Class_Master>(dtgrade);
            modelsate = APIHelperMethods.ToModelList<Class_Master>(dtstate);
           
            omodel.visitloc = modelvisit;
            omodel.expensetype = modelexpense;
            omodel.designation = modeldesgnation;
            omodel.travelmode = modeltravelmode;
            omodel.empgrade = modelemployeegrade;
            omodel.state = modelsate;
            omodel.fueltype = modelfuelmode;

            return View(omodel);


        }

        public ActionResult ConfigurationPartial()
        {
            dtconveyance = lstuser.GetConveyanceTypes("TravelAllowance");
            List<TravelConveyanceModelclass> modelallowance = new List<TravelConveyanceModelclass>();
            if (dtconveyance.Rows.Count > 0)
            {
                modelallowance = APIHelperMethods.ToModelList<TravelConveyanceModelclass>(dtconveyance);
                omodel.conveyancemode = modelallowance;
               TempData["ExportConveyance"] = omodel.conveyancemode;
               TempData.Keep();
            }
            else
            {
                TempData["ExportConveyance"] = null;
                TempData.Clear();

            }

            return PartialView("_PartialTAConfiguration", omodel);
        }


        public ActionResult ConfigurationInsert(TravelConveyanceModelclass model)
        {

            string state = "";
            int i = 1;
           
            DataTable dttravel = new DataTable(); 
            DataColumn dc =new DataColumn();

            dttravel.Columns.AddRange(new DataColumn[]
         {
            new DataColumn("VisitlocId", typeof(String)),
            new DataColumn("ExpenseId", typeof(String)),
            new DataColumn("DesignationId", typeof(String)),
            new DataColumn("TravelId", typeof(String)),
            new DataColumn("StateId", typeof(String)),
            new DataColumn("EmpgradeId", typeof(String)),
            new DataColumn("EligibleDistance", typeof(String)),  
            new DataColumn("EligibleRate", typeof(String)),   
            new DataColumn("EligibleAmtday", typeof(String)),   
            new DataColumn("fuelID", typeof(String))   
         });


            //         dc = new DataColumn("VisitlocId", typeof(String
            //      dc = new DataColumn("ExpenseId", typeof(String));
            //        dc = new DataColumn("DesignationId", typeof(String));
            //dc = new DataColumn("TravelId", typeof(String));
            //dc = new DataColumn("StateId", typeof(String));
            //dc = new DataColumn("EmpgradeId", typeof(String));
            //dc = new DataColumn("EligibleDistance", typeof(String));
            //dc = new DataColumn("EligibleRate", typeof(String));
            //dc = new DataColumn("EligibleAmtday", typeof(String));
            //dc = new DataColumn("fuelID", typeof(String));
            //dttravel.Columns.Add(dc);


            //if (model.StateId != null && model.StateId.Count > 0)
            //{
            //    foreach (string item in model.StateId)
            //    {
            //        if (i > 1)
            //            state = state + "," + item;
            //        else
            //            state = item;
            //        i++;
            //    }

            //}


            string Grade = "";
            int j = 1;

            if (model.EmpgradeId != null && model.EmpgradeId.Count > 0)
            {
                foreach (string item in model.EmpgradeId)
                {
                    
                    if (model.StateId != null && model.StateId.Count > 0)
                    {
                        foreach (string item2 in model.StateId)
                        {

                            dttravel.Rows.Add(model.VisitlocId, model.ExpenseId, model.DesignationId, model.TravelId, item2, item, model.EligibleDistance, model.EligibleRate, model.EligibleAmtday, model.fuelID);

                        }

                    }


                }

            }


            string userid = Convert.ToString(Session["userid"]);
          // int k = lstuser.GetConveyanceInsert(model.VisitlocId,model.ExpenseId,model.DesignationId,model.TravelId,model.StateId,model.EmpgradeId,model.EligibleDistance,model.EligibleRate,model.EligibleAmtday,model.fuelID,userid,"Insert",model.IsActive);
            int k = lstuser.GetConveyanceConfig(dttravel, userid, "Insert", model.IsActive);
    
            if (k > 0)
            {
                return Json("Success");
            }
            else
            {
                return Json("Failure");

            }

        }

        public ActionResult ConfigurationUpdate(TravelConveyanceModelclass model)
        {

            string userid = Convert.ToString(Session["userid"]);
          // int i = lstuser.GetConveyanceInsert(model.VisitlocId, model.ExpenseId, model.DesignationId, model.TravelId, model.StateId, model.EmpgradeId, model.EligibleDistance, model.EligibleRate, model.EligibleAmtday,model.fuelID, userid, "Update", model.IsActive,model.TCId);

            
           int i =   lstuser.GetConveyanceInsert(model.VisitlocId, model.ExpenseId, model.DesignationId, model.TravelId, model.StateIdfetch, model.EmpgradeIdfetch, model.EligibleDistance, model.EligibleRate, model.EligibleAmtday, model.fuelID, userid, "Update", model.IsActive, model.TCId);

           
            if (i > 0)
            {
                return Json("Success");
            }
            else
            {
                return Json("Failure");

            }

        }
        public ActionResult EditConveyance(string Tsid)
        {
           
            string userid = Convert.ToString(Session["userid"]);
            DataTable dt = new DataTable();
            dt = lstuser.GetConveyanceTypes("TravelAllowancebyID","", Tsid);
            TravelConveyanceModelclass modelallowance = new TravelConveyanceModelclass();
            modelallowance = APIHelperMethods.ToModel<TravelConveyanceModelclass>(dt);


            omodel = modelallowance;
            return Json(omodel);

        }

        public ActionResult DeleteConveyance(string Tsid)
        {
           
            string userid = Convert.ToString(Session["userid"]);
            DataTable dt = new DataTable();
            int i = lstuser.GetConveyanceDelete("Delete", Tsid);
            if (i > 0)
            {
                return Json("Success");
            }
            else
            {
                return Json("Failure");

            }
            return View(omodel);

        }

        public ActionResult GetTravelMode(string ExpenseId)
        {
            dttravelmode = lstuser.GetConveyanceTypes("TravelMode");
            modeltravelmode = APIHelperMethods.ToModelList<Class_Master>(dttravelmode);

            int statId;
            List<SelectListItem> travelmodes = new List<SelectListItem>();
            if (ExpenseId != "")
            {

                modeltravelmode = modeltravelmode.Where(x => x.Expense_Id == Int32.Parse(ExpenseId)).ToList();

                //modeltravelmode.ForEach(x =>
                //{
                //    travelmodes.Add(new SelectListItem { Text = x.Name, Value = x.ID.ToString() });
                //});


            }
            return Json(modeltravelmode, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetFuelMode(string ModeiD)
        {
            dtfuel = lstuser.GetConveyanceTypes("Fuel");
            modelfuelmode = APIHelperMethods.ToModelList<Class_Master>(dtfuel);

            int statId;

            List<SelectListItem> fuel = new List<SelectListItem>();
            if (ModeiD != "")
            {

                modelfuelmode = modelfuelmode.Where(x => x.Mode == Int32.Parse(ModeiD)).ToList();

                modelfuelmode.ForEach(x =>
                {
                    fuel.Add(new SelectListItem { Text = x.Name, Value = x.ID.ToString() });
                });


            }
            return Json(fuel, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ExportConveyance(int type)
        {

            ViewData["ExportConveyance"] = TempData["ExportConveyance"];
            TempData.Keep();

            if (ViewData["ExportConveyance"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), ViewData["ExportConveyance"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), ViewData["ExportConveyance"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), ViewData["ExportConveyance"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), ViewData["ExportConveyance"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), ViewData["ExportConveyance"]);
                    default:
                        break;
                }
            }
            //TempData["Exportcounterist"] = TempData["Exportcounterist"];
            //TempData.Keep();
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Travel Conveyance";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Travel Conveyance";




            settings.Columns.Add(x =>
            {
                x.FieldName = "Slno";
                x.Caption = "Serial";
                x.Width = System.Web.UI.WebControls.Unit.Percentage(5);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DateConveyance";
                x.Caption = "Date";
                x.Width = System.Web.UI.WebControls.Unit.Percentage(20);
            });
                

            settings.Columns.Add(x =>
            {
                x.FieldName = "VisitlocName";
                x.Caption = "Visit Location";
                x.Width = System.Web.UI.WebControls.Unit.Percentage(12);




            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "EmpgradeName";
                x.Caption = "Emp Grade";
                x.Width = System.Web.UI.WebControls.Unit.Percentage(10);



            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ExpenseName";
                x.Caption = "Expense Type";
                x.Width = System.Web.UI.WebControls.Unit.Percentage(15);

               


            });


            //settings.Columns.Add(x =>
            //{
            //    x.FieldName = "DesignationName";
            //    x.Caption = "Designation";
            //    x.Width = System.Web.UI.WebControls.Unit.Percentage(15);





            //});


            settings.Columns.Add(x =>
            {
                x.FieldName = "StateName";
                x.Caption = "State";
                x.Width = System.Web.UI.WebControls.Unit.Percentage(15);



            });


        

            settings.Columns.Add(x =>
            {
                x.FieldName = "TravelName";
                x.Caption = "Mode Of Travel";
                x.Width = System.Web.UI.WebControls.Unit.Percentage(13);




            });

     
            settings.Columns.Add(x =>
            {
                x.FieldName = "EligibleDistance";
                x.Caption = "Eligible Distance(Km)";
                x.PropertiesEdit.DisplayFormatString = "0.00";  
                x.Width = System.Web.UI.WebControls.Unit.Percentage(15);



            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "EligibleRate";
                x.Caption = "Eligible Rate";
                x.PropertiesEdit.DisplayFormatString = "0.00";  
                x.Width = System.Web.UI.WebControls.Unit.Percentage(10);
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EligibleAmtday";
                x.Caption = "Eligible Amount/Day";
                x.PropertiesEdit.DisplayFormatString = "0.00";  
                x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "IsActivename";
                x.Caption = "Status";

                x.Width = System.Web.UI.WebControls.Unit.Percentage(7);
            });


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }


        public ActionResult GetEmployeeGradeList()
        {
            try
            {
                dtgrade = lstuser.GetConveyanceTypes("EmpGrade");
                modelemployeegrade = APIHelperMethods.ToModelList<Class_Master>(dtgrade);

                return PartialView("_PartialEmployeeGrade", modelemployeegrade);



            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }
    }
}