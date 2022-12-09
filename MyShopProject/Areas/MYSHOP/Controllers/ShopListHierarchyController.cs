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
using FTSEntityframework;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ShopListHierarchyController : Controller
    {

        DataTable dtdesig = new DataTable();

        DataTable dtstate = new DataTable();
        UserList lstuser = new UserList();
        Shop objshop = new Shop();
        DataTable dtuser = new DataTable();
        String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];

        public ActionResult Index()
        {
            return View(this.GetCustomers(1));
        }

        [HttpPost]
        public ActionResult Index(int currentPageIndex)
        {
            return View(this.GetCustomers(currentPageIndex));
        }
        public ActionResult HierarchywiseShop()
        {
            try
            {
                ShopslistHiarchyInput omodel = new ShopslistHiarchyInput();
                string userid = Session["userid"].ToString();
                List<GetUserDesignationShoplist> modeldesg = new List<GetUserDesignationShoplist>();
                dtdesig = lstuser.GetDesignationList();
                modeldesg = APIHelperMethods.ToModelList<GetUserDesignationShoplist>(dtdesig);
                omodel.designation = modeldesg;

                dtuser = lstuser.GetUserList(userid);

                List<GetUsersshopshiarchy> model = new List<GetUsersshopshiarchy>();

                model = APIHelperMethods.ToModelList<GetUsersshopshiarchy>(dtuser);
                omodel.userlsit = model;
                omodel.Fromdate = null;
                omodel.Todate = null;


                List<GetUserStates> modelstate = new List<GetUserStates>();
                dtstate = lstuser.GetStateList();
                modelstate = APIHelperMethods.ToModelList<GetUserStates>(dtstate);
                omodel.states = modelstate;


                if (TempData["ShopHiarchyuser"] != null)
                {
                    omodel.selectedusrid = TempData["ShopHiarchyuser"].ToString();
                }
                return View(omodel);
            }


            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

        }


        public ActionResult GetshopHiarchy(string User)
        {
            TempData["ShopHiarchyuser"] = User;
            TempData.Keep();
            return RedirectToAction("HierarchywiseShop");
        }


        public ActionResult GetshoplistHierarchy(ShopslistHiarchyInput model)
        {
            try
            {


                List<ShopslistsHiarchy> omel = new List<ShopslistsHiarchy>();
                AllhierarchyShoplist omodel = new AllhierarchyShoplist();
                DataSet ds = new DataSet();




                //model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");


                //model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                string datfrmat = "";
                string dattoat = "";


                if (model.Fromdate != null)
                {
                    datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                    dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                }


                ds = objshop.GetShopListHierarchy(model.selectedusrid, datfrmat, dattoat, weburl, model.Desgid, model.StateId, model.pageNumber == 0 ? 1 : model.pageNumber, model.Pagecount == 0 ? 20 : model.Pagecount);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    omel = APIHelperMethods.ToModelList<ShopslistsHiarchy>(ds.Tables[0]);
                    //TempData["ExportShopHlist"] = omel;
                    //TempData.Keep();
                    int total = Convert.ToInt32(ds.Tables[1].Rows.Count);
                    double pageCount = (double)((decimal)total / Convert.ToDecimal(model.Pagecount == 0 ? 20 : model.Pagecount));
                    omodel.PageCount = (int)Math.Ceiling(pageCount);
                    omodel.CurrentPageIndex = model.pageNumber == 0 ? 1 : model.pageNumber;
                    omodel.shoplist = omel;
                    omodel.Totalcount = (int)total;


                    var pager = new MyShop.Models.Pager(total, model.pageNumber == 0 ? 1 : model.pageNumber);

                    omodel.pager = pager;



                }

                return PartialView("_PartialHierarchywiseShop", omodel);

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


        public ActionResult ExportShoplist(int type, string Fromdate, string Todate, string Desgid, string StateId, int user)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();

            string datfrmat = "";
            string dattoat = "";

            DataSet ds = new DataSet();

            if (!string.IsNullOrEmpty(Fromdate) && !string.IsNullOrEmpty(Todate))
            {
                datfrmat = Fromdate.Split('-')[2] + '-' + Fromdate.Split('-')[1] + '-' + Fromdate.Split('-')[0];
                dattoat = Todate.Split('-')[2] + '-' + Todate.Split('-')[1] + '-' + Todate.Split('-')[0];
            }


            ds = objshop.GetShopListHierarchy(user.ToString(), datfrmat, dattoat, weburl, Desgid, StateId, 0, 0);
            List<ShopslistsHiarchy> omel = new List<ShopslistsHiarchy>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                omel = APIHelperMethods.ToModelList<ShopslistsHiarchy>(ds.Tables[0]);
                TempData["ExportShopHlist"] = omel;

            }
            if (TempData["ExportShopHlist"] != null)
            {


                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), TempData["ExportShopHlist"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), TempData["ExportShopHlist"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), TempData["ExportShopHlist"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), TempData["ExportShopHlist"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), TempData["ExportShopHlist"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Shop Hierarchy List";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Shop Hierarchy List Report";


            settings.Columns.Add(column =>
            {
                column.Caption = "User";
                column.FieldName = "UserName";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "Designation";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Type";
                column.FieldName = "Shoptype";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "shop_name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Address";
                column.FieldName = "address";

            });

            settings.Columns.Add(column =>
                    {
                        column.Caption = "State";
                        column.FieldName = "StateName";

                    });


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



            settings.Columns.Add(column =>
            {
                column.Caption = "Email";
                column.FieldName = "owner_email";


            });


            settings.Columns.Add(column =>
            {
                column.Caption = "Date";
                column.FieldName = "Shop_CreateTime";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy HH:mm:ss";

            });



            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Visit";
                column.FieldName = "countactivity";

            });



            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }



        private FTSEntityShoplist GetCustomers(int currentPage)
        {
            int maxRows = 30;
            using (SalesTrackerEntities entities = new SalesTrackerEntities())
            {
                FTSEntityShoplist shopModel = new FTSEntityShoplist();
                shopModel.shoplist = (from shop in entities.tbl_Master_shop
                                      select shop)
                            .OrderBy(FTSEntityShoplist => FTSEntityShoplist.Shop_Name)
                            .Skip((currentPage - 1) * maxRows)
                            .Take(maxRows).ToList();

                double pageCount = (double)((decimal)entities.tbl_Master_shop.Count() / Convert.ToDecimal(maxRows));
                shopModel.PageCount = (int)Math.Ceiling(pageCount);
                shopModel.CurrentPageIndex = currentPage;
                return shopModel;

            }
        }




    }
}