#region======================================Revision History=========================================================================
//1.0   V2 .0.39    PRITI       13/02/2023      0025663:Last Visit fields shall be available in Outlet Reports
//2.0   V2.0.44     Sanchita    20-12-2023      27110: Contact Name column is required in the Shop list report
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


namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ShopListController : Controller
    {
        //
        // GET: /ShopList/
        UserList lstuser = new UserList();
        Shop objshop = new Shop();

        DataTable dtuser = new DataTable();
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
              //  string key = "AIzaSyCbYMZjnt8T6yivYfIa4_R9oy-L3SIYyrQ";
                string key = Convert.ToString(Session["ApiKey"]);
                ShopslistInput omodel = new ShopslistInput();
                string userid = Session["userid"].ToString();
                dtuser = lstuser.GetUserList(userid);
                // dtuser = lstuser.GetUserList();
                List<GetUsersshops> model = new List<GetUsersshops>();

                model = APIHelperMethods.ToModelList<GetUsersshops>(dtuser);
                omodel.userlsit = model;
                omodel.selectedusrid = userid;
                //omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                //omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");

                List<GetUsersStates> modelstate = new List<GetUsersStates>();
                DataTable dtstate = lstuser.GetStateList(userid);
                modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);

                omodel.states = modelstate;

                omodel.KeyId = key;

                //Rev Debashis 0025198
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
                //End of Rev Debashis 0025198

                if (TempData["Shopuser"] != null)
                {
                    omodel.selectedusrid = TempData["Shopuser"].ToString();

                    TempData.Clear();
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


        public ActionResult Getshoplist(ShopslistInput model)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<Shopslists> omel = new List<Shopslists>();

                DataTable dt = new DataTable();
                string datfrmat = "";
                string dattoat = "";

                if (model.Fromdate != null)
                {
                    //  model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                    datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                }

                if (model.Todate != null)
                {
                    //  model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                    dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                }
                //Rev Debashis 0025198
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
                //End of Rev Debashis 0025198
                if (model.Ispageload != "0")
                {
                    //Mantis Issue 24728
                    double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                    if (days <= 35)
                    {
                        //Rev Debashis 0025198
                        //dt = objshop.GetShopList(model.selectedusrid, datfrmat, dattoat, "", weburl, model.StateId, Convert.ToInt32(Session["userid"]));
                        dt = objshop.GetShopList(Branch_Id, model.selectedusrid, datfrmat, dattoat, "", weburl, model.StateId, Convert.ToInt32(Session["userid"]));
                        //End of Rev Debashis 0025198
                    }
                    //dt = objshop.GetShopList(model.selectedusrid, datfrmat, dattoat, "", weburl, model.StateId, Convert.ToInt32(Session["userid"]));
                    //End of Mantis Issue 24728
                    
                }
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        omel = APIHelperMethods.ToModelList<Shopslists>(dt);
                        TempData["ExportShoplist"] = omel;
                        TempData.Keep();
                    }

                    //return PartialView("_PartialShopList", omel);

                }
                return PartialView("PartialShopListgridview", omel);
            }
            catch
            {

                //   return Redirect("~/OMS/Signoff.aspx");

                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult ShopListModify(string ShopUniqueId)
        {
            DataTable dt = new DataTable();


            dt = objshop.ShopGetDetails(ShopUniqueId);
            Shopslists omel = new Shopslists();
            omel = APIHelperMethods.ToModel<Shopslists>(dt);


            dt = objshop.GetTypesList();
            omel.shptypes = APIHelperMethods.ToModelList<shopTypes>(dt);



            string userid = Session["userid"].ToString();
            dtuser = lstuser.GetUserList(userid);
            List<Usersshopassign> model = new List<Usersshopassign>();
            model = APIHelperMethods.ToModelList<Usersshopassign>(dtuser);
            omel.userslist = model;


            return PartialView("_PartialModifyShop", omel);

        }
        [HttpPost]
        public ActionResult DeleteShopList(string ShopUniqueId)
        {
            int gets = 0;
            gets = objshop.ShopDelete(ShopUniqueId);
            if (gets > 0)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        [HttpPost]
        public ActionResult ShopSubmit(Shopslists model)
        {
            // return Json("false",JsonRequestBehavior.AllowGet);
            string dattdob = "";
            string datdobanniv = "";

            if (ModelState.IsValid)
            {
                if (model.dobstr != null && model.dobstr != "")
                {
                    dattdob = model.dobstr.Split('-')[2] + '-' + model.dobstr.Split('-')[1] + '-' + model.dobstr.Split('-')[0];
                }
                if (model.date_aniversarystr != null && model.date_aniversarystr != "")
                {
                    datdobanniv = model.date_aniversarystr.Split('-')[2] + '-' + model.date_aniversarystr.Split('-')[1] + '-' + model.date_aniversarystr.Split('-')[0];
                }


                int gets = 0;
                gets = objshop.ShopModify(model.shop_Auto, model.address, model.pin_code, model.shop_name, model.owner_name, model.owner_contact_no, model.owner_email,model.Shoptype, dattdob, datdobanniv,Convert.ToString(model.Assign_To));
                if (gets > 0)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                return Json(false);
            }

        }


        [HttpPost, ValidateInput(false)]
        public ActionResult FileFileUploadforShop(string shopimagename)
        {
            if (Request.Files[0].ContentLength > 0)
            {
                string filesplit = Convert.ToString(Request.Files[0].FileName).Replace(" ", "");

                string fileLocation = Path.Combine(HttpContext.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["CandidateOfferletter"] + ""), shopimagename);

                if (System.IO.File.Exists(fileLocation))
                {

                    System.IO.File.Delete(fileLocation);

                }


                Request.Files[0].SaveAs(fileLocation);
            }
            return Json("Success");
        }



        public ActionResult ExportShoplist(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            if (TempData["ExportShoplist"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), TempData["ExportShoplist"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), TempData["ExportShoplist"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), TempData["ExportShoplist"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), TempData["ExportShoplist"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), TempData["ExportShoplist"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Shop List";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Shop List Report";


                    settings.Columns.Add(column =>
                    {
                    column.Caption = "Emp ID";
                    column.FieldName = "EMPCODE";
                    column.ExportWidth = 160;
                    });

                    settings.Columns.Add(column =>
                    {
                    column.Caption = "Emp Name";
                    column.FieldName = "EMPNAME";
                    column.ExportWidth = 180;
                    });

                    //Rev Debashis 0025198
                    settings.Columns.Add(column =>
                    {
                        column.Caption = "Branch";
                        column.FieldName = "BRANCHDESC";
                        column.ExportWidth = 180;
                    });
                    //End of Rev Debashis 0025198

                    // Rev 2.0
                    settings.Columns.Add(column =>
                    {
                    column.Caption = "Contact Person";
                    column.FieldName = "owner_name";
                    column.ExportWidth = 180;
                    });
                    // End of Rev 2.0

                    settings.Columns.Add(column =>
                    {
                    column.Caption = "Contact No.";
                    column.FieldName = "user_loginId";
                    column.ExportWidth = 120;
                    });

                    settings.Columns.Add(column =>
                    {
                    column.Caption = "Shop Name";
                    column.FieldName = "shop_name";
                        column.ExportWidth = 120;
                    });

                    settings.Columns.Add(column =>
                    {
                        column.Caption = "Code";
                        column.FieldName = "EntityCode";
                        column.ExportWidth = 100;
                    });

                    settings.Columns.Add(column =>
                    {
                    column.Caption = "Type";
                    column.FieldName = "Shoptype";
                        column.ExportWidth = 100;
                    });

                    // Mantis Issue 25421
                    settings.Columns.Add(column =>
                    {
                        column.Caption = "Beat";
                        column.FieldName = "Beat";
                        column.ExportWidth = 100;
                    });
                    // End of Mantis Issue 25421

                    settings.Columns.Add(column =>
                    {
                        column.Caption = "Owner Contact No.";
                        column.FieldName = "owner_contact_no";
                        column.ExportWidth = 150;
                    });

                    //Rev Debashis -- 0024577
                    settings.Columns.Add(column =>
                    {
                        column.Caption = "Alternate Phone No.";
                        column.FieldName = "Alt_MobileNo1";
                        column.ExportWidth = 150;
                    });

                    settings.Columns.Add(column =>
                    {
                        column.Caption = "Alternate Email ID";
                        column.FieldName = "Shop_Owner_Email2";
                        column.ExportWidth = 100;
                    });
                    //End of Rev Debashis -- 0024577

                    settings.Columns.Add(column =>
                    {
                    column.Caption = "Report To";
                    column.FieldName = "REPORTTO";

                    });

                    settings.Columns.Add(column =>
                    {
                    column.Caption = "State";
                    column.FieldName = "STATE";

                    });

                    //Rev Debashis
                    settings.Columns.Add(column =>
                    {
                        column.FieldName = "District";
                        column.Caption = "District";
                    });

                    settings.Columns.Add(column =>
                    {
                        column.FieldName = "pin_code";
                        column.Caption = "Pincode";
                    });

                    //End of Rev Debashis

                    settings.Columns.Add(column =>
                    {
                    column.Caption = "Address";
                    column.FieldName = "address";

                    });

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
                        column.Caption = "Owner DOB";
                        column.FieldName = "dob";
                        column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy HH:mm:ss";
                    });

                    settings.Columns.Add(column =>
                    {
                        column.Caption = "Owner Anniversary";
                        column.FieldName = "date_aniversary";
                        column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy HH:mm:ss";
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
                    column.Caption = "Added Date";
                    column.FieldName = "Shop_CreateTime";
                    column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy HH:mm:ss";

                    });

                    //Rev Debashis
                    settings.Columns.Add(column =>
                    {
                        column.FieldName = "Cluster";
                        column.Caption = "Cluster";
                    });
                    //End of Rev Debashis

                    //Rev 1.0
                    settings.Columns.Add(column =>
                    {
                        column.FieldName = "LASTVISITDATE";
                        column.Caption = "Last Visit Date";
                    });
                    settings.Columns.Add(column =>
                    {
                        column.FieldName = "LASTVISITTIME";
                        column.Caption = "Last Visit Time";
                    });
                    settings.Columns.Add(column =>
                    {
                        column.FieldName = "LASTVISITEDBY";
                        column.Caption = "Last Visited By";
                    });
                    //Rev 1.0 End

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
    }
}