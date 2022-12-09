using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer.SalesmanTrack;
using System.Data;
using MyShop.Models;
using UtilityLayer;
using BusinessLogicLayer;
namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ContentManagementController : Controller
    {
        Contentmanagementclass contentclass = new Contentmanagementclass();
        DataTable dttemplate = new DataTable();
        ModelContentmanagement omodel = new ModelContentmanagement();
        public ActionResult Template()
        {


            dttemplate = contentclass.GetTemplates();
            List<TemplateList> model = new List<TemplateList>();

            model = APIHelperMethods.ToModelList<TemplateList>(dttemplate);
            omodel.templates = model;
            return View(omodel);
        }
        public ActionResult FetchtemplateList()
        {
            return Json(null);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SubmitContent(string templateid, string templatetext)
        {
            dynamic showMessageString = string.Empty;
            dttemplate = contentclass.TemplateAdd(templateid, templatetext);
            showMessageString = new
            {
                param1 = 200,
                param2 = "Template added Successfully !!!"
            };
            return Json(showMessageString, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetTemplate(string TemplateID)
        {
            DBEngine odbengine = new DBEngine();
            string content = "";
            DataTable dt = odbengine.GetDataTable("select  Contenttext  from tbl_FTS_Contenttemplate where TemplateID=" + TemplateID);
            if (dt.Rows.Count > 0)
            {
                content = Convert.ToString(dt.Rows[0][0]);
            }
            return Json(content, JsonRequestBehavior.AllowGet);
        }


    }
}