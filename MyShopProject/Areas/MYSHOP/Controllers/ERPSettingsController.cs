using DevExpress.Web.Mvc;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ERPSettingsController : Controller
    {
        public ActionResult ERPGridBind()
        {
            ErpSettingList ErpSetL = new ErpSettingList();
            BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
            DataTable dt = new DataTable();
            dt = objEngine.GetDataTable("select [Key],Value,Description from FTS_APP_CONFIG_SETTINGS");
            List<ErpSettProp> erpset = new List<ErpSettProp>();
            erpset = APIHelperMethods.ToModelList<ErpSettProp>(dt);
            ErpSetL.ErpSettProp = erpset;
            return View(ErpSetL);
            //"~/Views/NewCompany/ERPSettings/ERPSettings.cshtml",
        }

        public ActionResult PartialGridBind()
        {
            ErpSettingList ErpSetL = new ErpSettingList();
            BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
            DataTable dt = new DataTable();
            dt = objEngine.GetDataTable("select [Key],Value,Description from FTS_APP_CONFIG_SETTINGS");
            List<ErpSettProp> erpset = new List<ErpSettProp>();
            erpset = APIHelperMethods.ToModelList<ErpSettProp>(dt);
            ErpSetL.ErpSettProp = erpset;
            return PartialView(ErpSetL);
            //"~/Views/NewCompany/ERPSettings/_ERPSettingsGridView.cshtml",
        }

        [ValidateInput(false)]
        public ActionResult SaveERPSettings(MVCxGridViewBatchUpdateValues<ErpSettProp, int> updateValues, ErpSettProp options)
        {
            if (options != null)
            {
                ViewBag.Message = "Successfully Updated";
            }
            else
            {
                ViewBag.Message = "";
            }
            BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
            DataTable dt = new DataTable();
            if (updateValues != null)
            {

                foreach (ErpSettProp item in updateValues.Update)
                {
                    dt = objEngine.GetDataTable("update dbo.FTS_APP_CONFIG_SETTINGS set Value='" + item.Value + "',Description='" + item.Description + "'  where [Key]='" + item.Key + "'");
                }
            }

            return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);
        }

    }
}