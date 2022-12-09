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
using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using DataAccessLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class SystemsettingsController : Controller
    {
        //
        // GET: /MYSHOP/Systemsettings/
        public ActionResult Index()
        {
            

            try
            {

                List<SystemsettingsModel> omel = new List<SystemsettingsModel>();


                DataTable ds = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSSystemConfiguration");
                proc.AddPara("@ACTION", "GETLISTING");
                ds = proc.GetTable();

                if (ds.Rows.Count > 0)
                {
                    omel = APIHelperMethods.ToModelList<SystemsettingsModel>(ds);

                }
                else
                {
                    return View(omel);

                }
                //}
                return View(omel);
            }
            catch
            {

                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

            return View();
        }


        [HttpPost]
        public JsonResult Addsystemsettings(SystemsettingsModel data)
        {
            string ReturnCode = "";
            string ReturnMsg = "";

            try
            {
                string VALUE = null;
                string SETTING_KEY = data.Key;
                string Userid = Convert.ToString(Session["userid"]);
                
                if (data.ControlType == "YESNO")
                {
                    VALUE = data.selValue;
                }
                if (data.ControlType == "BIT")
                {
                    VALUE = data.selBitValue;
                }
                else if (data.ControlType == "NUMBERBOX")
                {
                   VALUE = data.numValue.ToString() ;
                } 
                else if(data.ControlType == "TEXTBOX")
                {
                   VALUE = data.txtValue ;
                }
                else if(data.ControlType == "DATE")
                {
                    VALUE = data.dtValue ;
                }
                else if(data.ControlType == "TIME")
                {
                    VALUE = data.tmValue;
                }

                ProcedureExecute proc = new ProcedureExecute("PRC_FTSSystemConfiguration");
                proc.AddPara("@ACTION", "SAVESETTINGS");
                proc.AddPara("@CONTROL_TYPE", data.ControlType);
                proc.AddPara("@VALUE", VALUE);
                proc.AddPara("@SETTING_KEY", SETTING_KEY);
                proc.AddPara("@USER_ID", Userid);
                proc.AddVarcharPara("@RETURNMESSAGE", 50, "", QueryParameterDirection.Output);
                proc.AddVarcharPara("@RETURNCODE", 50, "", QueryParameterDirection.Output);
                int k = proc.RunActionQuery();

                ReturnMsg = Convert.ToString(proc.GetParaValue("@RETURNMESSAGE"));
                ReturnCode = Convert.ToString(proc.GetParaValue("@RETURNCODE"));

              
                //    enquiries.save(apply, uniqueid, ref ReturnCode, ref ReturnMsg);
                if (ReturnMsg == "Success" && ReturnCode == "1")
                {
                    data.response_code = "Success";
                    data.response_msg = "Success";
                }
                else if (ReturnMsg != "Success" && ReturnCode == "-1")
                {
                    data.response_code = "Error";
                    data.response_msg = ReturnMsg;
                }
                else
                {
                    data.response_code = "Error";
                    data.response_msg = "Please try again later";
                }

            }
            catch (Exception ex)
            {
                data.response_code = "CatchError";
                data.response_msg = "Please try again later";
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}