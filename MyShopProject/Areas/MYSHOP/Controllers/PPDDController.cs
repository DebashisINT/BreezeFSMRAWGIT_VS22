using DevExpress.Web;
using DevExpress.Web.Mvc;
using Models;
using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class PPDDController : Controller
    {
        UserList lstuser = new UserList();
        Shop objshop = new Shop();

        DataTable dtuser = new DataTable();
        DataTable dt = new DataTable();
        public ActionResult Add()
        {
          
            Shopslists omel = new Shopslists();

            dt = objshop.GetTypesList();
            omel.shptypes = APIHelperMethods.ToModelList<shopTypes>(dt);
            omel.shptypes = omel.shptypes.Where(x => x.ID == 2 | x.ID == 4).ToList();


            string userid = Session["userid"].ToString();
            dtuser = lstuser.GetUserList(userid);
            List<Usersshopassign> model = new List<Usersshopassign>();
            model = APIHelperMethods.ToModelList<Usersshopassign>(dtuser);
            omel.userslist = model;



            return View(omel);
        }


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
                string userid = Session["userid"].ToString();
             

                //int onlyThisAmount = 20;
                string ticks = DateTime.Now.Ticks.ToString();
                ticks = userid + "_" + ticks.ToString()+"_"+model.owner_contact_no;

                model.shop_Auto = ticks;
                gets = objshop.ShopPPDDAdd(userid,model.shop_Auto, model.address, model.pin_code, model.shop_name, model.owner_name, model.owner_contact_no, model.owner_email, model.Shoptype, dattdob, datdobanniv, Convert.ToString(model.Assign_To));
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



        public ActionResult _PartialPPDDList()
        {
            List<Shopslists> omel = new List<Shopslists>();
            dt = objshop.GetPPDDList();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    omel = APIHelperMethods.ToModelList<Shopslists>(dt);
                    TempData["ExportPPDDShoplist"] = omel;
                    TempData.Keep();
                }

                //return PartialView("_PartialShopList", omel);

            }

            return PartialView("_PartialPPDDList", omel);
        }


        public ActionResult ExportPPDDlist(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            ViewData["ExportPPDDShoplist"] = TempData["ExportPPDDShoplist"];
            TempData.Keep();

            if (ViewData["ExportPPDDShoplist"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), ViewData["ExportPPDDShoplist"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), ViewData["ExportPPDDShoplist"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), ViewData["ExportPPDDShoplist"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), ViewData["ExportPPDDShoplist"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), ViewData["ExportPPDDShoplist"]);
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
            settings.Name = "PPDDLIST";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "PPDDLIST";


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
                column.FieldName = "statename";

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
                column.Caption = "Created By";
                column.FieldName = "user_name";


            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Created Date";
                column.FieldName = "Shop_CreateTime";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy HH:mm:ss";

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