using DevExpress.Web.Mvc;
using DevExpress.Web;
using LMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Areas.LMS.Controllers
{
    public class LMSCategoryController : Controller
    {
        // GET: LMS/LMSCategory
        LMSCategoryModel obj = new LMSCategoryModel();
        public ActionResult Index()
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSCategory/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit= rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;

            return View();
           
        }
        public ActionResult PartialGridList(LMSCategoryModel model)
        {
            try
            {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSCategory/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanEdit = rights.CanEdit;
                ViewBag.CanDelete = rights.CanDelete;

                string Is_PageLoad = string.Empty;
                DataTable dt = new DataTable();

                if (model.Is_PageLoad != "1")
                    Is_PageLoad = "Ispageload";

                ViewData["ModelData"] = model;
                string Userid = Convert.ToString(Session["userid"]);

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMS_CATEGORYMASTER", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "GETLISTINGDETAILS");
                sqlcmd.Parameters.Add("@USER_ID", Userid);
                sqlcmd.Parameters.Add("@ISPAGELOAD", model.Is_PageLoad);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                return PartialView("PartialCategorylisting", LGetCountryDetailsList(Is_PageLoad));
                
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable LGetCountryDetailsList(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            if (Is_PageLoad != "Ispageload")
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_CATEGORYMASTERLISTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_CATEGORYMASTERLISTs
                        where d.SEQ == 0
                        select d;
                return q;
            }
        }
        public JsonResult SaveCategory(string name, string id,string description, string ActiveStatus)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = obj.SaveCategory(name, Userid, id, description, ActiveStatus);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditCategory(string id)
        {
            DataTable output = new DataTable();
            output = obj.EditCategory(id);

            if (output.Rows.Count > 0)
            {
                return Json(new {
                    NAME = Convert.ToString(output.Rows[0]["CATEGORYNAME"]) ,
                    DESCRIPTION = Convert.ToString(output.Rows[0]["CATEGORYDESCRIPTION"]),
                    STATUS = Convert.ToString(output.Rows[0]["CATEGORYSTATUS"])
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { name = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Delete(string ID)
        {
            int output = 0;
            output = obj.DeleteCategory(ID);
            return Json(output, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ExporCategoryList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToXlsx(GetCategoryGridViewSettings(), LGetCountryDetailsList(""));
                //case 2:
                //    return GridViewExtension.ExportToPdf(GetCategoryGridViewSettings(), LGetCountryDetailsList(""));
                //case 3:
                //    return GridViewExtension.ExportToXls(GetCategoryGridViewSettings(), LGetCountryDetailsList(""));
                //case 4:
                //    return GridViewExtension.ExportToRtf(GetCategoryGridViewSettings(), LGetCountryDetailsList(""));
                //case 5:
                //    return GridViewExtension.ExportToCsv(GetCategoryGridViewSettings(), LGetCountryDetailsList(""));
                default:
                    break;
            }
            return null;
        }

        private GridViewSettings GetCategoryGridViewSettings()
        {
            

            var settings = new GridViewSettings();
            settings.Name = "PartialGridList";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Category";

            settings.Columns.Add(x =>
            {
                x.FieldName = "CATEGORYNAME";
                x.Caption = "Category Name";
                x.VisibleIndex = 1;
                x.Width = 200;                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CATEGORYDESCRIPTION";
                x.Caption = "Category Description";
                x.VisibleIndex = 2;
                x.Width = 200;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CATEGORYSTATUS";
                x.Caption = "Active";
                x.VisibleIndex = 3;
                x.Width = 200;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CreateUser";
                x.Caption = "Created By";
                x.VisibleIndex = 4;
                x.Width = 200;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "CreateDate";
                x.Caption = "Created On";
                x.VisibleIndex = 5;
                x.Width = 200;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ModifyUser";
                x.Caption = "Updated By";
                x.VisibleIndex = 6;
                x.Width = 200;
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "ModifyDate";
                x.Caption = "Updated On";
                x.VisibleIndex = 7;
                x.Width = 200;
                x.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
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