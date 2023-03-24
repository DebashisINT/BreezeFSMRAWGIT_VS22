#region======================================Revision History=========================================================================
//1.0   V2.0.38     Debashis    23/01/2023      Multiple contact information to be displayed in the Shops report.
//                                              Refer: 0025585
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
                if (model.Ispageload == "1")
                {
                    //Rev Pallab
                    //dt = objshop.GetShopListCounterwise(model.TypeID, "", state, Convert.ToInt32(Session["userid"])); 
                    //Rev  Mantis: 0025585
                    //dt = objshop.GetShopListCounterwise(model.TypeID, weburl, state, Convert.ToInt32(Session["userid"]));
                    dt = objshop.GetShopListCounterwise(model.TypeID, weburl, state, model.IsRevisitContactDetails, Convert.ToInt32(Session["userid"]));
                    //End of Rev  Mantis: 0025585
                    //Rev end Pallab
                    if (dt.Rows.Count > 0)
                    {
                        omel = APIHelperMethods.ToModelList<Shopslists>(dt);
                        TempData["Exportcounterist"] = omel;
                        TempData.Keep();
                    }
                    else
                    {
                        TempData["Exportcounterist"] = null;
                        TempData.Keep();

                    }
                    return PartialView("_PartialCounterListing", omel);
                }

                else
                {

                    return PartialView("_PartialCounterListing", omel);

                }

            }
            catch
            {

                //   return Redirect("~/OMS/Signoff.aspx");

                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


        public ActionResult ExportCounterlist(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            ViewData["Exportcounterist"] = TempData["Exportcounterist"];
            TempData.Keep();

            if (ViewData["Exportcounterist"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), ViewData["Exportcounterist"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), ViewData["Exportcounterist"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), ViewData["Exportcounterist"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), ViewData["Exportcounterist"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), ViewData["Exportcounterist"]);
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
            settings.Name = "Shops";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Counters";


            settings.Columns.Add(column =>
            {
                column.Caption = "User";
                column.FieldName = "UserName";

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Type";
                column.FieldName = "Shoptype";

            });
            //Rev Debashis --0024576
            settings.Columns.Add(column =>
            {
                column.Caption = "Sub Type";
                column.FieldName = "SubType";
            });
            //End of Rev Debashis -- 0024576


            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "shop_name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Code";
                column.FieldName = "EntityCode";

            });

            // Mantis Issue 25421
            settings.Columns.Add(column =>
            {
                column.Caption = "Beat";
                column.FieldName = "Beat";

            });

            // End of Mantis Issue 25421
          
            settings.Columns.Add(column =>
            {
                column.Caption = "Address";
                column.FieldName = "address";

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "statename";
            });

            //Rev Debashis -- 0024575
            settings.Columns.Add(column =>
            {
                column.Caption = "District";
                column.FieldName = "District";
            });
            //End of Rev Debashis -- 0024575

            settings.Columns.Add(column =>
            {
                column.Caption = "Pincode";
                column.FieldName = "pin_code";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Owner Name";
                column.FieldName = "owner_name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Contact";
                column.FieldName = "owner_contact_no";
            });

            //Rev Debashis -- 0024577
            settings.Columns.Add(column =>
            {
                column.Caption = "Alternate Phone No.";
                column.FieldName = "Alt_MobileNo1";
            });
            //End of Rev Debashis -- 0024577

            settings.Columns.Add(column =>
            {
                column.Caption = "Email";
                column.FieldName = "owner_email";
            });

            //Rev Debashis -- 0024577
            settings.Columns.Add(column =>
            {
                column.Caption = "Alternate Email ID";
                column.FieldName = "Shop_Owner_Email2";
            });
            //End of Rev Debashis -- 0024577

            settings.Columns.Add(column =>
            {
                column.Caption = "Prime Partner";
                column.FieldName = "PP";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Direct Distributor";
                column.FieldName = "DD";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Outlet Location";
                column.FieldName = "Entity_Location";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Outlet Status";
                column.FieldName = "Entity_Status";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Specification";
                column.FieldName = "Specification";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Pan Card";
                column.FieldName = "ShopOwner_PAN";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Aadhar Card";
                column.FieldName = "ShopOwner_Aadhar";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Created By";
                column.FieldName = "user_name";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Created Date";
                column.FieldName = "Shop_CreateTime";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy HH:mm:ss";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Owner DOB";
                column.FieldName = "dob";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Owner Anniversary";
                column.FieldName = "date_aniversary";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
            });

            //Rev Debashis -- 0024575
            settings.Columns.Add(column =>
            {
                column.Caption = "Cluster";
                column.FieldName = "Cluster";
            });
            //End of Rev Debashis -- 0024575
            //Rev work start 30.06.2022  Mantise no:0024573
            settings.Columns.Add(column =>
            {
                column.Caption = "GSTIN";
                column.FieldName = "gstn_number";
            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Trade License No.";
                column.FieldName = "trade_licence_number";
            });
            //Rev work close 30.06.2022  Mantise no:0024573
            //Rev  Mantis: 0025585
            if (TempData["IsRevisitContactDetails"].ToString() == "1")
            {
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Name1";
                    column.FieldName = "CONTACT_NAME1";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Number1";
                    column.FieldName = "CONTACT_NUMBER1";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Email1";
                    column.FieldName = "CONTACT_EMAIL1";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Anniversary1";
                    column.FieldName = "CONTACT_DOA1";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact DOB1";
                    column.FieldName = "CONTACT_DOB1";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Name2";
                    column.FieldName = "CONTACT_NAME2";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Number2";
                    column.FieldName = "CONTACT_NUMBER2";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Email2";
                    column.FieldName = "CONTACT_EMAIL2";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Anniversary2";
                    column.FieldName = "CONTACT_DOA2";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact DOB2";
                    column.FieldName = "CONTACT_DOB2";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Name3";
                    column.FieldName = "CONTACT_NAME3";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Number3";
                    column.FieldName = "CONTACT_NUMBER3";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Email3";
                    column.FieldName = "CONTACT_EMAIL3";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Anniversary3";
                    column.FieldName = "CONTACT_DOA3";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact DOB3";
                    column.FieldName = "CONTACT_DOB3";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Name1";
                    column.FieldName = "CONTACT_NAME1";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Number1";
                    column.FieldName = "CONTACT_NUMBER1";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Email4";
                    column.FieldName = "CONTACT_EMAIL4";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Anniversary4";
                    column.FieldName = "CONTACT_DOA4";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact DOB4";
                    column.FieldName = "CONTACT_DOB4";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Name5";
                    column.FieldName = "CONTACT_NAME5";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Number5";
                    column.FieldName = "CONTACT_NUMBER5";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Email5";
                    column.FieldName = "CONTACT_EMAIL5";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Anniversary5";
                    column.FieldName = "CONTACT_DOA5";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact DOB5";
                    column.FieldName = "CONTACT_DOB5";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Name6";
                    column.FieldName = "CONTACT_NAME6";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Number6";
                    column.FieldName = "CONTACT_NUMBER6";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Email6";
                    column.FieldName = "CONTACT_EMAIL6";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact Anniversary6";
                    column.FieldName = "CONTACT_DOA6";
                });
                settings.Columns.Add(column =>
                {
                    column.Caption = "Contact DOB6";
                    column.FieldName = "CONTACT_DOB6";
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
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "LASTVISITTIME";
                column.Caption = "Last Visit Time";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "LASTVISITEDBY";
                column.Caption = "Last Visited By";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.Width = 150;
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
    }
}