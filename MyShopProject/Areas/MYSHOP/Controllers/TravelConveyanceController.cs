/**********************************************************************************************************************************
 * Rev 1.0   Sanchita   V2.0.40     04-05-2023      A New Expense Report is Required for BP Poddar.Refer: 25833                                                 
 * Rev 2.0   Priti      v2.0.40     19-05-2023      0026145:Modification in the ‘Configure Travelling Allowance’ page.
 * Rv  3.0   Sanchita   V2.0.42     07-08-2023      Branch and Area Coloumn needs to be implemented in the Export to file of Configure Travelling Expense module
 *                                                  Refer: 26681
 * ***********************************************************************************************************************************/
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
        CommonBL objSystemSettings = new CommonBL();
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

            // Rev 1.0
            string isExpenseFeatureAvailable = "0";
            DBEngine obj1 = new DBEngine();
            isExpenseFeatureAvailable = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='isExpenseFeatureAvailable'").Rows[0][0]);
            ViewBag.isExpenseFeatureAvailable = isExpenseFeatureAvailable;
            // End of Rev 1.0

            // Rev 2.0
            string IsShowReimbursementTypeInAttendance = objSystemSettings.GetSystemSettingsResult("IsShowReimbursementTypeInAttendance");//REV 1.0
            ViewBag.IsShowReimbursementTypeInAttendance = IsShowReimbursementTypeInAttendance;
            // Rev 2.0 End

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

            // Rev 1.0
            string isExpenseFeatureAvailable = "0";
            DBEngine obj1 = new DBEngine();
            isExpenseFeatureAvailable = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='isExpenseFeatureAvailable'").Rows[0][0]);
            ViewBag.isExpenseFeatureAvailable = isExpenseFeatureAvailable;
            // End of Rev 1.0

            // Rev 2.0
            string IsShowReimbursementTypeInAttendance = objSystemSettings.GetSystemSettingsResult("IsShowReimbursementTypeInAttendance");//REV 1.0
            ViewBag.IsShowReimbursementTypeInAttendance = IsShowReimbursementTypeInAttendance;
            // Rev 2.0 End

            return PartialView("_PartialTAConfiguration", omodel);
        }


        public ActionResult ConfigurationInsert(TravelConveyanceModelclass model)
        {

            string state = "";
            int i = 1;

            // Rev 1.0
            string isExpenseFeatureAvailable = "0";
            DBEngine obj1 = new DBEngine();
            isExpenseFeatureAvailable = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='isExpenseFeatureAvailable'").Rows[0][0]);
             // End of Rev 1.0

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
                            // Rev 1.0
                            //dttravel.Rows.Add(model.VisitlocId, model.ExpenseId, model.DesignationId, model.TravelId, item2, item, model.EligibleDistance, model.EligibleRate, model.EligibleAmtday, model.fuelID);
                            if (isExpenseFeatureAvailable == "0")
                            {
                                dttravel.Rows.Add(model.VisitlocId, model.ExpenseId, model.DesignationId, model.TravelId, item2, item, model.EligibleDistance, model.EligibleRate, model.EligibleAmtday, model.fuelID);
                            }
                            else
                            {
                                dttravel.Rows.Add(model.VisitlocId, model.ExpenseId, model.DesignationId, 0, item2, item, 0, 0, model.EligibleAmtday, 0);
                            }
                            // End of Rev 1.0                        

                        }

                    }

                }

            }
            //Rev 2.0
            DataTable dtBranch = new DataTable();
            dtBranch.Columns.AddRange(new DataColumn[]
            { 
                new DataColumn("BranchId", typeof(String))            
            });

            if (model.BranchId != null && model.BranchId.Count > 0)
            {
                foreach (string itemBranch in model.BranchId)
                {
                    dtBranch.Rows.Add(itemBranch);                    
                }
            }

            DataTable dtArea = new DataTable();
            dtArea.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("AreaId", typeof(String))
            });

            if (model.AreaId != null && model.AreaId.Count > 0)
            {
                foreach (string itemArea in model.AreaId)
                {
                    dtArea.Rows.Add(itemArea);
                }
            }
            //Rev 2.0 End




            string userid = Convert.ToString(Session["userid"]);
            // int k = lstuser.GetConveyanceInsert(model.VisitlocId,model.ExpenseId,model.DesignationId,model.TravelId,model.StateId,model.EmpgradeId,model.EligibleDistance,model.EligibleRate,model.EligibleAmtday,model.fuelID,userid,"Insert",model.IsActive);
            //Rev 2.0
            //int k = lstuser.GetConveyanceConfig(dttravel, userid, "Insert", model.IsActive);
            int k = lstuser.GetConveyanceConfig(dttravel, userid, "Insert", model.IsActive, dtBranch, dtArea);
            //Rev 2.0 End
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

            // Rev 1.0
            string isExpenseFeatureAvailable = "0";
            DBEngine obj1 = new DBEngine();
            isExpenseFeatureAvailable = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='isExpenseFeatureAvailable'").Rows[0][0]);
            // End of Rev 1.0
            // Rev 3.0
            string IsShowReimbursementTypeInAttendance = "0";
            IsShowReimbursementTypeInAttendance = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsShowReimbursementTypeInAttendance'").Rows[0][0]);
            // End of Rev 3.0

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

            // Rev 3.0
            if (IsShowReimbursementTypeInAttendance == "1")
            {
                settings.Columns.Add(x =>
                {
                    x.FieldName = "BranchName";
                    x.Caption = "Branch";
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "AreaName";
                    x.Caption = "Area";
                    x.Width = System.Web.UI.WebControls.Unit.Percentage(15);
                });
            }
            // End of Rev 3.0

            // Rev 1.0
            if (isExpenseFeatureAvailable == "0")
            {
            // End of Rev 1.0

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

            // Rev 1.0
            }
            // End of Rev 1.0

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