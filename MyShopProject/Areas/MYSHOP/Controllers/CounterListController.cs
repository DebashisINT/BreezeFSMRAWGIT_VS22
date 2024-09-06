#region======================================Revision History=========================================================================
//1.0   V2.0.38     Debashis    23/01/2023      Multiple contact information to be displayed in the Shops report.
//                                              Refer: 0025585
//2.0   V2.0.41     Sanchita    19/07/2023      Add Branch parameter in Listing of Master -> Shops report. Mantis : 26135
//3.0   V2.0.43     Sanchita    07-11-2023      0026895: System will prompt for Branch selection if the Branch hierarchy is activated.
//4.0   V2.0.45     Sanchita    22/01/2024      Supervisor name column is required in Shops report. Mantis: 27199
//5.0   V2.0.47     Priti       19/04/2024      System is getting logged out while trying to export the Shops data into excel. Mantis: 0027324
//6.0   V2.0.47     Sanchita    22-05-2024      0027405: Colum Chooser Option needs to add for the following Modules.
//7.0   V2 .0.48    Priti       08-07-2024      0027407: "Party Status" - needs to add in the following reports.
#endregion===================================End of Revision History==================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLogicLayer;
using SalesmanTrack;
using System.Data;
using UtilityLayer;
using System.Web.Script.Serialization;
using MyShop.Models;
using System.IO;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using BusinessLogicLayer.SalesTrackerReports;
using System.Collections;
using System.Configuration;


namespace MyShop.Areas.MYSHOP.Controllers
{
    public class CounterListController : Controller
    {
        // GET: /ShopList/
        UserList lstuser = new UserList();
        Shop objshop = new Shop();

        DataTable dtuser = new DataTable();
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                CounterClass omodel = new CounterClass();
                string userid = Session["userid"].ToString();
               // dtuser = lstuser.GetUserList(userid);
                // dtuser = lstuser.GetUserList();
                //List<GetUsersshops> model = new List<GetUsersshops>();

                //model = APIHelperMethods.ToModelList<GetUsersshops>(dtuser);
                //omodel.userlsit = model;
                //omodel.selectedusrid = userid;





                //omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                //omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");


                //List<CounterStates> modelstate = new List<CounterStates>();
                //DataTable dtstate = lstuser.GetStateList();
                //modelstate = APIHelperMethods.ToModelList<CounterStates>(dtstate);
                //omodel.states = modelstate;


                List<shopCounterTypes> modelcounter = new List<shopCounterTypes>();
                //Rev work start 16.06.2022 mantise 0024948: Show All checkbox required for Shops report
                //DataTable dtshoptypes = lstuser.GetShopTypes();
                DataTable dtshoptypes = lstuser.GetShopTypesData();
                //Rev work close 16.06.2022 mantise 0024948: Show All checkbox required for Shops report
                modelcounter = APIHelperMethods.ToModelList<shopCounterTypes>(dtshoptypes);
                omodel.Shoptypes = modelcounter;

                // Rev 2.0
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
                // End of Rev 2.0

                if (TempData["Exportcounterist"] != null)
                {
                    //omodel.selectedusrid = TempData["Shopuser"].ToString();
                    TempData.Clear();
                }

                DataTable dt = new SalesSummary_Report().GetStateMandatory(Session["userid"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }

                // Rev 3.0
                DBEngine obj1 = new DBEngine();
                ViewBag.BranchMandatory = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsActivateEmployeeBranchHierarchy'").Rows[0][0]);
                // End of Rev 3.0

                return View(omodel);


            }

            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        public ActionResult GetshopIndex(string User)
        {
            TempData["Shopuser"] = User;
            return RedirectToAction("Index");
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

        //public ActionResult GetCounterlist(CounterClass model)
        //{
        //    try
        //    {
        //        String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
        //        List<Shopslists> omel = new List<Shopslists>();

        //        DataTable dt = new DataTable();
        //        string datfrmat = "";
        //        string dattoat = "";



        //        dt = objshop.GetShopListCounterwise(model.TypeID, "", model.StateId);

        //        if (dt.Rows.Count > 0)
        //        {
        //            omel = APIHelperMethods.ToModelList<Shopslists>(dt);
        //            TempData["Exportcounterist"] = omel;
        //            TempData.Keep();
        //        }
        //        else
        //        {
        //            TempData["Exportcounterist"] = null;
        //            TempData.Keep();

        //        }

        //        return PartialView("_PartialCounterListing", omel);
        //        // return PartialView("_PartialShopListgridview", omel);

        //    }
        //    catch
        //    {

        //        //   return Redirect("~/OMS/Signoff.aspx");

        //        return RedirectToAction("Logout", "Login", new { Area = "" });
        //    }
        //}



        public ActionResult GetCounterlistPartial(CounterClass model)
        {
            try
            {
                //Rev Pallab
                //String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SPath"];
                //Rev end Pallab
                List<Shopslists> omel = new List<Shopslists>();

                DataTable dt = new DataTable();
                string datfrmat = "";
                string dattoat = "";

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
                //Rev  Mantis:0025585
                if (model.IsRevisitContactDetails != null)
                {
                    TempData["IsRevisitContactDetails"] = model.IsRevisitContactDetails;
                    TempData.Keep();
                }
                //End of Rev  Mantis:0025585
                // Rev 2.0
                string Branch_Id = "";
                int b = 1;
                if (model.BranchId != null && model.BranchId.Count > 0)
                {
                    foreach (string item in model.BranchId)
                    {
                        if (b > 1)
                            Branch_Id = Branch_Id + "," + item;
                        else
                            Branch_Id = item;
                        b++;
                    }
                }
                // End of Rev 2.0

                //Rev 5.0
                //if (model.Ispageload == "1")
                //{
                //    //Rev Pallab
                //    //dt = objshop.GetShopListCounterwise(model.TypeID, "", state, Convert.ToInt32(Session["userid"])); 
                //    //Rev  Mantis: 0025585
                //    //dt = objshop.GetShopListCounterwise(model.TypeID, weburl, state, Convert.ToInt32(Session["userid"]));
                //    // Rev 2.0
                //    //dt = objshop.GetShopListCounterwise(model.TypeID, weburl, state, model.IsRevisitContactDetails, Convert.ToInt32(Session["userid"]));

                //    dt = objshop.GetShopListCounterwise(Branch_Id, model.TypeID, weburl, state, model.IsRevisitContactDetails, Convert.ToInt32(Session["userid"]), model.Ispageload);

                //    // End of Rev 2.0
                //    //End of Rev  Mantis: 0025585
                //    //Rev end Pallab
                //    if (dt.Rows.Count > 0)
                //    {
                //        omel = APIHelperMethods.ToModelList<Shopslists>(dt);
                //        TempData["Exportcounterist"] = omel;
                //        TempData.Keep();
                //    }
                //    else
                //    {
                //        TempData["Exportcounterist"] = null;
                //        TempData.Keep();

                //    }
                //    return PartialView("_PartialCounterListing", omel);
                //}
                //else
                //{
                //    return PartialView("_PartialCounterListing", omel);
                //}
                //Rev 5.0 End

                string Is_PageLoad = string.Empty;

                if (model.Ispageload == "0")
                {
                    Is_PageLoad = "is_pageload";
                }
                

                dt = objshop.GetShopListCounterwise(Branch_Id, model.TypeID, weburl, state, model.IsRevisitContactDetails, Convert.ToInt32(Session["userid"]), model.Ispageload);

                return PartialView("_PartialCounterListing", ShopsDetails(Is_PageLoad));



            }
            catch
            {

                //   return Redirect("~/OMS/Signoff.aspx");

                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


        public ActionResult ExportCounterlist(int type)
        {
            //Rev 5.0
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), ShopsDetails(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), ShopsDetails(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), ShopsDetails(""));
                //break;
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), ShopsDetails(""));
                //break;
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), ShopsDetails(""));
                default:
                    break;
            }
            //List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            //ViewData["Exportcounterist"] = TempData["Exportcounterist"];
            //TempData.Keep();

            //if (ViewData["Exportcounterist"] != null)
            //{

            //    switch (type)
            //    {
            //        case 1:
            //            return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), ViewData["Exportcounterist"]);
            //        //break;
            //        case 2:
            //            return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), ViewData["Exportcounterist"]);
            //        //break;
            //        case 3:
            //            return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), ViewData["Exportcounterist"]);
            //        //break;
            //        case 4:
            //            return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), ViewData["Exportcounterist"]);
            //        //break;
            //        case 5:
            //            return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), ViewData["Exportcounterist"]);
            //        default:
            //            break;
            //    }

            //}
            ////TempData["Exportcounterist"] = TempData["Exportcounterist"];
            ////TempData.Keep();
            //Rev 5.0
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            // Rev 6.0
            DataTable dtColmn = objshop.GetPageRetention(Session["userid"].ToString(), "SHOPS");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            // End of Rev 6.0

            var settings = new GridViewSettings();
            settings.Name = "Shops";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Counters";


            settings.Columns.Add(column =>
            {
                column.Caption = "User";
                column.FieldName = "UserName";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='UserName'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Type";
                column.FieldName = "Shoptype";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Shoptype'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            //Rev 7.0
            settings.Columns.Add(column =>
            {
                column.Caption = "Party Status";
                column.FieldName = "PARTYSTATUS";              
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PARTYSTATUS'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                
            });
            //Rev 7.0 End



            //Rev Debashis --0024576
            settings.Columns.Add(column =>
            {
                column.Caption = "Sub Type";
                column.FieldName = "SubType";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SubType'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            //End of Rev Debashis -- 0024576


            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "shop_name";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='shop_name'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Code";
                column.FieldName = "EntityCode";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EntityCode'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            // Mantis Issue 25421
            settings.Columns.Add(column =>
            {
                column.Caption = "Beat";
                column.FieldName = "Beat";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Beat'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            // End of Mantis Issue 25421

            // Rev 2.0
            settings.Columns.Add(column =>
            {
                column.Caption = "Branch";
                column.FieldName = "BRANCHDESC";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BRANCHDESC'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            // End of Rev 2.0

            settings.Columns.Add(column =>
            {
                column.Caption = "Address";
                column.FieldName = "address";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='address'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "statename";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='statename'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            //Rev Debashis -- 0024575
            settings.Columns.Add(column =>
            {
                column.Caption = "District";
                column.FieldName = "District";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='District'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            //End of Rev Debashis -- 0024575

            settings.Columns.Add(column =>
            {
                column.Caption = "Pincode";
                column.FieldName = "pin_code";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='pin_code'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Owner Name";
                column.FieldName = "owner_name";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='owner_name'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Contact";
                column.FieldName = "owner_contact_no";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='owner_contact_no'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            //Rev Debashis -- 0024577
            settings.Columns.Add(column =>
            {
                column.Caption = "Alternate Phone No.";
                column.FieldName = "Alt_MobileNo1";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Alt_MobileNo1'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            //End of Rev Debashis -- 0024577

            settings.Columns.Add(column =>
            {
                column.Caption = "Email";
                column.FieldName = "owner_email";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='owner_email'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            //Rev Debashis -- 0024577
            settings.Columns.Add(column =>
            {
                column.Caption = "Alternate Email ID";
                column.FieldName = "Shop_Owner_Email2";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Shop_Owner_Email2'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            //End of Rev Debashis -- 0024577

            settings.Columns.Add(column =>
            {
                column.Caption = "Prime Partner";
                column.FieldName = "PP";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PP'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Direct Distributor";
                column.FieldName = "DD";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DD'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Outlet Location";
                column.FieldName = "Entity_Location";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Entity_Location'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Outlet Status";
                column.FieldName = "Entity_Status";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Entity_Status'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Specification";
                column.FieldName = "Specification";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Specification'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Pan Card";
                column.FieldName = "ShopOwner_PAN";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ShopOwner_PAN'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Aadhar Card";
                column.FieldName = "ShopOwner_Aadhar";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ShopOwner_Aadhar'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Created By";
                column.FieldName = "user_name";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='user_name'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Created Date";
                column.FieldName = "Shop_CreateTime";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy HH:mm:ss";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Shop_CreateTime'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            // Rev 4.0
            settings.Columns.Add(column =>
            {
                column.Caption = "Supervisor Name";
                column.FieldName = "REPORTTO_NAME";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REPORTTO_NAME'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            // End of Rev 4.0

            settings.Columns.Add(column =>
            {
                column.Caption = "Owner DOB";
                column.FieldName = "dob";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='dob'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Owner Anniversary";
                column.FieldName = "date_aniversary";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='date_aniversary'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });

            //Rev Debashis -- 0024575
            settings.Columns.Add(column =>
            {
                column.Caption = "Cluster";
                column.FieldName = "Cluster";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Cluster'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            //End of Rev Debashis -- 0024575
            //Rev work start 30.06.2022  Mantise no:0024573
            settings.Columns.Add(column =>
            {
                column.Caption = "GSTIN";
                column.FieldName = "gstn_number";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='gstn_number'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Trade License No.";
                column.FieldName = "trade_licence_number";

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='trade_licence_number'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            //Rev work close 30.06.2022  Mantise no:0024573
            //Rev  Mantis: 0025585
            if (TempData["IsRevisitContactDetails"].ToString() == "1")
            {
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Name1";
                    column.FieldName = "CONTACT_NAME1";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NAME1'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Number1";
                    column.FieldName = "CONTACT_NUMBER1";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NUMBER1'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Email1";
                    column.FieldName = "CONTACT_EMAIL1";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_EMAIL1'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Anniversary1";
                    column.FieldName = "CONTACT_DOA1";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_DOA1'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact DOB1";
                    column.FieldName = "CONTACT_DOB1";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_DOB1'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Name2";
                    column.FieldName = "CONTACT_NAME2";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NAME2'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Number2";
                    column.FieldName = "CONTACT_NUMBER2";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NUMBER2'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Email2";
                    column.FieldName = "CONTACT_EMAIL2";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_EMAIL2'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Anniversary2";
                    column.FieldName = "CONTACT_DOA2";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_DOA2'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact DOB2";
                    column.FieldName = "CONTACT_DOB2";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_DOB2'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Name3";
                    column.FieldName = "CONTACT_NAME3";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NAME3'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Number3";
                    column.FieldName = "CONTACT_NUMBER3";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NUMBER3'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Email3";
                    column.FieldName = "CONTACT_EMAIL3";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_EMAIL3'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Anniversary3";
                    column.FieldName = "CONTACT_DOA3";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_DOA3'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact DOB3";
                    column.FieldName = "CONTACT_DOB3";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_DOB3'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Name1";
                    column.FieldName = "CONTACT_NAME1";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NAME1'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Number1";
                    column.FieldName = "CONTACT_NUMBER1";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NUMBER1'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Email4";
                    column.FieldName = "CONTACT_EMAIL4";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_EMAIL4'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Anniversary4";
                    column.FieldName = "CONTACT_DOA4";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_DOA4'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact DOB4";
                    column.FieldName = "CONTACT_DOB4";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_DOB4'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Name5";
                    column.FieldName = "CONTACT_NAME5";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NAME5'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Number5";
                    column.FieldName = "CONTACT_NUMBER5";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NUMBER5'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Email5";
                    column.FieldName = "CONTACT_EMAIL5";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_EMAIL5'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Anniversary5";
                    column.FieldName = "CONTACT_DOA5";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_DOA5'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact DOB5";
                    column.FieldName = "CONTACT_DOB5";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_DOB5'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Name6";
                    column.FieldName = "CONTACT_NAME6";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NAME6'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Number6";
                    column.FieldName = "CONTACT_NUMBER6";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_NUMBER6'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Email6";
                    column.FieldName = "CONTACT_EMAIL6";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_EMAIL6'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Anniversary6";
                    column.FieldName = "CONTACT_DOA6";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_DOA6'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact DOB6";
                    column.FieldName = "CONTACT_DOB6";

                    // Rev 6.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT_DOB6'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 6.0
                });


            }


            //End of Rev  Mantis: 0025585

            //Rev 1.0
            settings.Columns.Add(column =>
            {
                column.FieldName = "LASTVISITDATE";
                column.Caption = "Last Visit Date";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LASTVISITDATE'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "LASTVISITTIME";
                column.Caption = "Last Visit Time";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LASTVISITTIME'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "LASTVISITEDBY";
                column.Caption = "Last Visited By";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;

                // Rev 6.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LASTVISITEDBY'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 6.0
            });
            //Rev 1.0 End
            //   settings.Columns.Add(column =>
            //   {
            //       column.Caption = "Shop Visit";
            //       column.FieldName = "countactivity";

            //   });

            //settings.Columns.Add(column =>
            //   {
            //       column.Caption = "Last Visited Date";
            //       column.FieldName = "Lastactivitydate";

            //   });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public IEnumerable ShopsDetails(string Is_PageLoad)
        {
            // Rev 6.0
            DataTable dtColmn = objshop.GetPageRetention(Session["userid"].ToString(), "SHOPS");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            // End of Rev 6.0

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "is_pageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FSM_SHOPLIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.shop_id ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FSM_SHOPLIST_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) 
                        orderby d.shop_id ascending
                        select d;
                return q;
            }



        }

        // Rev 6.0
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
                int k = objshop.InsertPageRetention(Col, Session["userid"].ToString(), "SHOPS");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        // End of Rev 6.0
    }
}