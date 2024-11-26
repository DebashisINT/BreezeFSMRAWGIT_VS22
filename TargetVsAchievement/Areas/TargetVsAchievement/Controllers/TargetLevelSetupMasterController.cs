using DataAccessLayer;
//using DevExpress.Web;
//using DevExpress.Web.Mvc;
using TargetVsAchievement.Models;
//using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UtilityLayer;

namespace TargetVsAchievement.Areas.TargetVsAchievement.Controllers
{
    public class TargetLevelSetupMasterController : Controller
    {
        // GET: TargetVsAchievement/TargetLevelSetupMaster
        public ActionResult Index()
        {
            TargetLevelSetupModel Dtls = new TargetLevelSetupModel();

            Dtls.LevelBasedOn = 0;

            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_TARGETLEVELSETUP_MASTER");
            proc.AddPara("@ACTION", "GETDROPDOWNBINDDATA");
            ds = proc.GetDataSet();

            if (ds != null)
            {
                // Company
                List<DesignationList> DesignationList = new List<DesignationList>();
                DesignationList = APIHelperMethods.ToModelList<DesignationList>(ds.Tables[0]);
                Dtls.DesignationList = DesignationList;
            }

            return View(Dtls);
        }

        public JsonResult GetEmployeeList(string deg_id, string SalesmanLevel, string action)
        {
            if(deg_id == null)
            {
                deg_id = "0";
            }

            TargetLevelSetupModel model = new TargetLevelSetupModel();
            List<EmployeeMapList> EmployeeMapList = new List<EmployeeMapList>();

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_TARGETLEVELSETUP_MASTER");
            proc.AddPara("@Action", action);
            proc.AddPara("@DESIG_ID", deg_id);
            proc.AddPara("@SALESMANLEVEL", SalesmanLevel);
            proc.AddPara("@BRANCHID", Convert.ToString(Session["userbranchHierarchy"]));
            proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));

            dt = proc.GetTable();

            if (dt != null)
            {
                EmployeeMapList = APIHelperMethods.ToModelList<EmployeeMapList>(dt);
            }

            return Json(EmployeeMapList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveTargetEmpMap(TargetLevelSetupModel data)
        {
            try
            {
                //string rtrnduplicatevalue = "";
                //string Userid = Convert.ToString(Session["userid"]);
                ProcedureExecute proc = new ProcedureExecute("PRC_TARGETLEVELSETUP_MASTER");
                proc.AddPara("@ACTION", data.Action);
                proc.AddPara("@SALESMANLEVEL", data.SalesmanLevel);
                proc.AddPara("@BASEDON", data.BasedOn);
                proc.AddPara("@SELECTEDEMPLOYEEBASEDONMAPLIST", data.selectedEmployeeBasedOnMapList);
                proc.AddPara("@USERID", Convert.ToString(HttpContext.Session["userid"]));
                proc.AddVarcharPara("@RETURN_VALUE", 500, "", QueryParameterDirection.Output);
                int k = proc.RunActionQuery();
                data.RETURN_VALUE = Convert.ToString(proc.GetParaValue("@RETURN_VALUE")); // WILL RETURN NEW TOPIC ID
                
                
                
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
    }
}