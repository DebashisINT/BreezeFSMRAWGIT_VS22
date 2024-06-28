using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using LMS.Models;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace LMS.Areas.LMS.Controllers
{
    public class LMSTopicsController : Controller
    {
        // GET: LMS/Topics
        public ActionResult Index()
        {
            LMSTopicsModel Dtls = new LMSTopicsModel();
            
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSTopics/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;

            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
            proc.AddPara("@ACTION", "GETDROPDOWNBINDDATA");
            ds = proc.GetDataSet();

            if (ds != null)
            {
                // Company
                List<TopicBasedOnList> TopicBasedOnList = new List<TopicBasedOnList>();
                TopicBasedOnList = APIHelperMethods.ToModelList<TopicBasedOnList>(ds.Tables[0]);
                Dtls.TopicBasedOnList = TopicBasedOnList;
            }

            return View(Dtls);
        }

        public ActionResult PartialTopicGridList(LMSTopicsModel model)
        {
            try
            {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/LMSTopics/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanEdit = rights.CanEdit;
                ViewBag.CanDelete = rights.CanDelete;

                string Is_PageLoad = string.Empty;

                if (model.Is_PageLoad == "Ispageload")
                {
                    Is_PageLoad = "is_pageload";

                }


                GetTopicListing(Is_PageLoad);

                model.Is_PageLoad = "Ispageload";

                return PartialView("PartialTopicGridList", GetTopicDetails(Is_PageLoad));

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public void GetTopicListing(string Is_PageLoad)
        {
            string user_id = Convert.ToString(Session["userid"]);

            string action = string.Empty;
            DataTable formula_dtls = new DataTable();
            DataSet dsInst = new DataSet();

            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
                proc.AddPara("@ACTION", "GETLISTINGDATA");
                proc.AddPara("@IS_PAGELOAD", Is_PageLoad);
                proc.AddPara("@USERID", Convert.ToInt32(user_id));
                dt = proc.GetTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable GetTopicDetails(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            ////////DataTable dtColmn = GetPageRetention(Session["userid"].ToString(), "CRM Contact");
            ////////if (dtColmn != null && dtColmn.Rows.Count > 0)
            ////////{
            ////////    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            ////////}

            if (Is_PageLoad != "is_pageload")
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_TOPICSMASTER_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ 
                        select d;
                return q;
            }
            else
            {
                LMSMasterDataContext dc = new LMSMasterDataContext(connectionString);
                var q = from d in dc.LMS_TOPICSMASTER_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 1111119
                        orderby d.SEQ 
                        select d;
                return q;
            }


        }

        public JsonResult GetTopicCount()
        {


            LMSTopicsModel dtl = new LMSTopicsModel();
            try
            {
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
                proc.AddPara("@Action", "GETTOPICSCOUNTDATA");
                proc.AddPara("@userid", Convert.ToString(HttpContext.Session["userid"]));
                ds = proc.GetDataSet();


                int TotalTopics = 0;
                int TotalUsedTopics = 0;
                int TotalUnusedTopics = 0;

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    TotalTopics = Convert.ToInt32(item["cnt_TotalTopics"]);
                    TotalUsedTopics = Convert.ToInt32(item["cnt_TotalUsedTopics"]);
                    TotalUnusedTopics = Convert.ToInt32(item["cnt_TotalUnusedTopics"]);

                }

                dtl.TotalTopics = TotalTopics;
                dtl.TotalUsedTopics = TotalUsedTopics;
                dtl.TotalUnusedTopics = TotalUnusedTopics;

            }
            catch
            {
            }
            return Json(dtl);
        }

        public ActionResult ExporRegisterList()
        {
            return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), GetTopicDetails(""));
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "gridLMSTopic";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "LMSTopics";

            settings.Columns.Add(x =>
            {
                x.FieldName = "SEQ";
                x.Caption = "Sr. No";
                x.VisibleIndex = 1;
                x.ExportWidth = 80;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOPICNAME";
                x.Caption = "Topic Name";
                x.VisibleIndex = 2;
                x.ExportWidth = 350;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOPICBASEDON";
                x.Caption = "Based On";
                x.VisibleIndex = 3;
                x.ExportWidth = 150;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "TOPICSTATUS";
                x.Caption = "Active";
                x.VisibleIndex = 4;
                x.ExportWidth = 150;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATEDBY";
                x.Caption = "Created by";
                x.VisibleIndex = 5;
                x.ExportWidth = 150;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATEDON";
                x.Caption = "Created on";
                x.VisibleIndex = 6;
                x.ExportWidth = 150;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UPDATEDBY";
                x.Caption = "Modified by";
                x.VisibleIndex = 7;
                x.ExportWidth = 150;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "UPDATEDON";
                x.Caption = "Modified on";
                x.VisibleIndex = 8;
                x.ExportWidth = 150;

            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;

        }

        public JsonResult GetBasedOnList(string topic_basedon)
        {
            LMSTopicsModel model = new LMSTopicsModel();
            List<TopicMapList> TopicMapList1 = new List<TopicMapList>();

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
            proc.AddPara("@Action", "GETBASEDONDATALIST");
            proc.AddPara("@TOPICBASEDON_ID", topic_basedon);
            proc.AddPara("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
            proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));

            dt = proc.GetTable();

            if (dt != null)
            {
                TopicMapList1 = APIHelperMethods.ToModelList<TopicMapList>(dt);
            }

            return Json(TopicMapList1, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public ActionResult SaveTopic(LMSTopicsModel data)
        {
            try
            {
                //string rtrnduplicatevalue = "";
                //string Userid = Convert.ToString(Session["userid"]);
                ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
                proc.AddPara("@ACTION", data.Action);
                proc.AddPara("@TOPICID", data.TopicID);
                proc.AddPara("@TOPICNAME", data.TopicName);
                proc.AddPara("@TOPICBASEDON_ID", data.TopicBasedOnId);
                proc.AddPara("@SELECTEDTOPICBASEDONMAPLIST", data.selectedTopicBasedOnMapList);
                proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
                proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                proc.AddVarcharPara("@RETURN_DUPLICATEMAPNAME", -1, "", QueryParameterDirection.Output);
                int k = proc.RunActionQuery();
                data.RETURN_VALUE = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));
                data.RETURN_DUPLICATEMAPNAME = Convert.ToString(proc.GetParaValue("@RETURN_DUPLICATEMAPNAME"));
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult ShowTopicDetails(String TopicID)
        {
            try
            {
                DataTable dt = new DataTable();
                LMSTopicsModel ret = new LMSTopicsModel();
                List<TopicMapList> TopicMapList1 = new List<TopicMapList>();

                ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
                proc.AddPara("@ACTION", "SHOWTOPIC");
                proc.AddPara("@TOPICID", TopicID);
                proc.AddPara("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
                proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    TopicMapList1 = APIHelperMethods.ToModelList<TopicMapList>(dt);
                }

                return Json(TopicMapList1, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        [HttpPost]
        public JsonResult TopicDelete(string TopicID)
        {
            string output_msg = string.Empty;
           
            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_LMSTOPICSMASTER");
                proc.AddPara("@ACTION", "DELETETOPICS");
                proc.AddPara("@TOPICID", TopicID);
                proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                dt = proc.GetTable();
                output_msg = Convert.ToString(proc.GetParaValue("@RETURN_VALUE"));
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }

            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }
    }
}