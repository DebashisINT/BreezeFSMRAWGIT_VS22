using BusinessLogicLayer;
using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraRichEdit.Import.Html;
using FSMMaster.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace FSMMaster.Areas.MASTER.Controllers
{
    public class WorkRoasterMasterController : Controller
    {
        // GET: MASTER/WorkRoasterMaster
        public ActionResult Index()
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/WorkRoasterMaster/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;

            WorkRoasterListModel lst = new WorkRoasterListModel();
            return View(lst);
        }

        [HttpPost]
        public JsonResult SaveRoaster(string RosterID, string RoasterName, RoasterList[] WorkingRoasterList, string Action)
        {
            
            WorkRoasterListModel apply = new WorkRoasterListModel();

            try
            {
                string Userid = Convert.ToString(Session["userid"]);

                DataTable RosterDt = new DataTable();
                RosterDt.Columns.Add("Day", typeof(System.String));
                RosterDt.Columns.Add("BeginTime", typeof(System.String));
                RosterDt.Columns.Add("EndTime", typeof(System.String));
                RosterDt.Columns.Add("Grace", typeof(System.Int64));
                DataRow WeekRow = RosterDt.NewRow();

                foreach (var item in WorkingRoasterList)
                {
                    if(item != null)
                    {
                        WeekRow = RosterDt.NewRow();
                        WeekRow["Day"] = item.Day;
                        WeekRow["BeginTime"] = item.BeginTime;
                        WeekRow["EndTime"] = item.EndTime;
                        WeekRow["Grace"] = item.Grace;
                        RosterDt.Rows.Add(WeekRow);
                    }
                    
                }

                if(Action== "EDITWORKROSTER")
                {
                    Action = "EDITSAVEWORKROSTER";
                }

                
                DataTable dtImport = new DataTable();
                ProcedureExecute proc1 = new ProcedureExecute("PRC_WORKROSTER_ADDEDITVIEW");
                proc1.AddPara("@USERID", Userid);
                proc1.AddPara("@ACTION", Action);

                proc1.AddVarcharPara("@NAME", 100, RoasterName);
                proc1.AddVarcharPara("@ID", 100, RosterID);
                proc1.AddPara("@DAYLIST", RosterDt);

                proc1.AddVarcharPara("@RETURNMESSAGE", 50, "", QueryParameterDirection.Output);
                proc1.AddVarcharPara("@RETURNCODE", 50, "", QueryParameterDirection.Output);

                dtImport = proc1.GetTable();

                apply.response_msg = Convert.ToString(proc1.GetParaValue("@RETURNMESSAGE"));
                apply.response_code = Convert.ToString(proc1.GetParaValue("@RETURNCODE"));

                //apply.response_code = "Success";
                //apply.response_msg = "Success";

            }

            catch (Exception ex)
            {
                apply.response_code = "CatchError";
                apply.response_msg = "Please try again later";
            }

            return Json(apply, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PartialWorkRosterGridList(WorkRoasterListModel model)
        {
            try
            {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/WorkRoasterMaster/Index");
                ViewBag.CanEdit = rights.CanEdit;
                ViewBag.CanDelete = rights.CanDelete;

                List<WorkRoasterDet> qmapmodel = new List<WorkRoasterDet>();

                if(model.Is_PageLoad == "1")
                {
                    DataTable dt = new DataTable();
                    ProcedureExecute proc1 = new ProcedureExecute("PRC_WORKROSTER_ADDEDITVIEW");
                    proc1.AddPara("@ACTION", "GETLISTINGDATA");
                    //proc.AddPara("@USERID", Convert.ToInt32(user_id));
                    dt = proc1.GetTable();

                    if (dt != null)
                    {
                        qmapmodel = APIHelperMethods.ToModelList<WorkRoasterDet>(dt);
                    }
                    TempData["WorkRosterGridList"] = qmapmodel;
                    TempData.Keep();

                    model.Is_PageLoad = "Ispageload";
                }
                

                return PartialView("PartialWorkRosterGridList", qmapmodel);


            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        public ActionResult ShowWorkRosterDetails(String RosterID)
        {
            try
            {
                DataTable dt = new DataTable();
                
                List<RoasterList> RoasterList = new List<RoasterList>();

                ProcedureExecute proc = new ProcedureExecute("PRC_WORKROSTER_ADDEDITVIEW");
                proc.AddPara("@ACTION", "EDITWORKROSTER");
                proc.AddPara("@ID", RosterID);
                proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    RoasterList = APIHelperMethods.ToModelList<RoasterList>(dt);
                }

                return Json(RoasterList, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


        [HttpPost]
        public JsonResult WorkRosterDelete(string RosterID)
        {
            WorkRoasterListModel apply = new WorkRoasterListModel();

            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_WORKROSTER_ADDEDITVIEW");
                proc.AddPara("@ACTION", "DELETEWORKROSTER");
                proc.AddPara("@ID", RosterID);
                //proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);

                proc.AddVarcharPara("@RETURNMESSAGE", 50, "", QueryParameterDirection.Output);
                proc.AddVarcharPara("@RETURNCODE", 50, "", QueryParameterDirection.Output);

                dt = proc.GetTable();

                apply.response_msg = Convert.ToString(proc.GetParaValue("@RETURNMESSAGE"));
                apply.response_code = Convert.ToString(proc.GetParaValue("@RETURNCODE"));

            }
            catch (Exception ex)
            {
                apply.response_code = "CatchError";
                apply.response_msg = "Please try again later";
            }

            return Json(apply, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetModuleList(string RosterID)
        {
            try
            {
                DataTable dt = new DataTable();

                List<RosterModuleMapList> RosterModuleMapList = new List<RosterModuleMapList>();

                ProcedureExecute proc = new ProcedureExecute("PRC_WORKROSTER_ADDEDITVIEW");
                proc.AddPara("@ACTION", "GETMODULELIST");
                proc.AddPara("@ID", RosterID);
                proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    RosterModuleMapList = APIHelperMethods.ToModelList<RosterModuleMapList>(dt);
                }

                return Json(RosterModuleMapList, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        [HttpPost]

        public JsonResult MapModuleListSubmit(string RosterID , List<string> Modulelist)
        {
            WorkRoasterListModel apply = new WorkRoasterListModel();

            try
            {
                Employee_BL objEmploye = new Employee_BL();
                string ModuleId = "";
                int i = 1;

                if (Modulelist != null && Modulelist.Count > 0)
                {
                    foreach (string item in Modulelist)
                    {
                        if (item == "0")
                        {
                            ModuleId = "0";
                            break;
                        }
                        else
                        {
                            if (i > 1)
                                ModuleId = ModuleId + "," + item;
                            else
                                ModuleId = item;
                            i++;
                        }
                    }

                }

                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_WORKROSTER_ADDEDITVIEW");
                proc.AddPara("@ACTION", "MODULEROSTERMAPINSERTUPDATE");
                proc.AddPara("@ID", RosterID);
                proc.AddPara("@MODULEID", ModuleId);
                proc.AddPara("@USERID", Convert.ToString(Session["userid"]));
                proc.AddVarcharPara("@RETURNMESSAGE", 50, "", QueryParameterDirection.Output);
                proc.AddVarcharPara("@RETURNCODE", 50, "", QueryParameterDirection.Output);

                dt = proc.GetTable();

                apply.response_msg = Convert.ToString(proc.GetParaValue("@RETURNMESSAGE"));
                apply.response_code = Convert.ToString(proc.GetParaValue("@RETURNCODE"));
            }
            catch (Exception ex)
            {
                apply.response_code = "CatchError";
                apply.response_msg = "Please try again later";
            }

            return Json(apply, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ExporRegisterList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDoctorBatchGridViewSettings(), TempData["WorkRosterGridList"]);
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDoctorBatchGridViewSettings(), TempData["WorkRosterGridList"]);
                case 3:
                    return GridViewExtension.ExportToXls(GetDoctorBatchGridViewSettings(), TempData["WorkRosterGridList"]);
                case 4:
                    return GridViewExtension.ExportToRtf(GetDoctorBatchGridViewSettings(), TempData["WorkRosterGridList"]);
                case 5:
                    return GridViewExtension.ExportToCsv(GetDoctorBatchGridViewSettings(), TempData["WorkRosterGridList"]);
                default:
                    break;
            }
            return null;
        }

        private GridViewSettings GetDoctorBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "gridWorkRoaster";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "WorkRoaster";


            settings.Columns.Add(x =>
            {
                x.FieldName = "SEQ";
                x.Caption = "Sr. No";
                x.VisibleIndex = 1;
                x.Width = 80;
                
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "ROSTENAME";
                x.Caption = "Roster Name";
                x.VisibleIndex = 2;
                x.Width = 110;
                
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATEDATE";
                x.Caption = "Created On";
                x.VisibleIndex = 3;
                x.Width = 100;
               
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CREATEUSER";
                x.Caption = "Created By";
                x.VisibleIndex = 4;
                x.Width = 100;
                
            });
            settings.Columns.Add(x =>
            {
                x.FieldName = "MODIFIEDDATE";
                x.Caption = "Modified By";
                x.VisibleIndex = 5;
                x.Width = 100;

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "MODIFIEDUSER";
                x.Caption = "Modified By";
                x.VisibleIndex = 6;
                x.Width = 100;

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