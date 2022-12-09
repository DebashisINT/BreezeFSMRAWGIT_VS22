using Models;
using MyShop.Models;
using Repostiory.ShiftMaster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ShiftMasterController : Controller
    {
        IShiftMasterLogic objIShiftMasterLogic;
        ShiftMasterEngine objShiftMasterEngine = new ShiftMasterEngine();
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult PartialShiftEntry()
        {
            ShiftMasterEngine model = new ShiftMasterEngine();
            return PartialView(model);
        }

        public ActionResult PartialShiftByID(string ShiftId)
        {
            ShiftMasterEngine model = new ShiftMasterEngine();
            if (ShiftId != "")
            {
                int strIsComplete = 0;
                string strMessage = "";

                objIShiftMasterLogic = new ShiftMasterLogic();
                model = objIShiftMasterLogic.GetShiftById(ShiftId, ref strIsComplete, ref strMessage);
                if (strIsComplete == 1)
                {
                    model.ResponseCode = "Success";
                    model.ResponseMessage = "Success";
                }
                else
                {
                    model.ResponseCode = "Error";
                    model.ResponseMessage = strMessage;
                }
            }
            return PartialView("~/Areas/MYSHOP/Views/ShiftMaster/PartialShiftEntry.cshtml", model);
        }


        public PartialViewResult PartialShiftGrid()
        {
            return PartialView(GetShiftList());
        }

        public IEnumerable GetShiftList()
        {
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.V_ShiftMasters
                    orderby d.ShiftID descending
                    select d;
            return q;
        }


        [HttpPost]
        public JsonResult ShiftMasterSubmit(ShiftMasterEngine model)
        {
            if (Convert.ToString(model.Shift_Name) == "")
            {
                model.ResponseCode = "Error";
                model.ResponseMessage = "Shift Name is mandatory";
            }
            else if (Convert.ToString(model.Shift_Start) == "")
            {
                model.ResponseCode = "Error";
                model.ResponseMessage = "Shift Start is mandatory";
            }
            else if (Convert.ToString(model.Shift_End) == "")
            {
                model.ResponseCode = "Error";
                model.ResponseMessage = "Shift End is mandatory";
            }
            else
            {
                int strIsComplete = 0;
                string strMessage = "";

                objIShiftMasterLogic = new ShiftMasterLogic();
                objIShiftMasterLogic.ShiftMasterSubmit(model, ref strIsComplete, ref strMessage);
                if (strIsComplete == 1)
                {
                    model.ResponseCode = "Success";
                    model.ResponseMessage = "Success";
                }
                else
                {
                    model.ResponseCode = "Error";
                    model.ResponseMessage = strMessage;
                }
            }
            return Json(model);
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        [HttpPost]
        public JsonResult ShiftDelete(string ActionType, string id)
        {

            string output_msg = string.Empty;
            int strIsComplete = 0;
            objIShiftMasterLogic = new ShiftMasterLogic(); ;
            Msg _msg = new Msg();
            try
            {
                output_msg = objIShiftMasterLogic.Delete(ActionType, id, ref strIsComplete);
                if (output_msg == "Success" && strIsComplete == 1)
                {
                    _msg.response_code = "Success";
                    _msg.response_msg = "Success";
                }
                else if (output_msg != "Success" && strIsComplete == -1)
                {
                    _msg.response_code = "Error";
                    _msg.response_msg = output_msg;
                }
                else
                {
                    _msg.response_code = "Error";
                    _msg.response_msg = "Please try again later";
                }
            }
            catch (Exception ex)
            {
                _msg.response_code = "CatchError";
                _msg.response_msg = "Please try again later";
            }

            return Json(_msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LeavingLateShiftByID(string id)
        {
            ShiftMasterEngine objModel = new ShiftMasterEngine();
            int strIsComplete = 0;
            string strMessage = "";
            objIShiftMasterLogic = new ShiftMasterLogic();
            //   objModel = objIShiftMasterLogic.LeavingLateShiftByID(id, ref strIsComplete, ref strMessage);
            return Json(objModel);
        }

        public JsonResult RotationalShiftShiftByID(string id)
        {
            ShiftMasterEngine objModel = new ShiftMasterEngine();
            int strIsComplete = 0;
            string strMessage = "";
            objIShiftMasterLogic = new ShiftMasterLogic();
            // objModel = objIShiftMasterLogic.RotationalShiftShiftByID(id, ref strIsComplete, ref strMessage);
            return Json(objModel);
        }

        public ActionResult MasterSetting(string ActionType, string Shiftcode, string EditFlag, int ShiftId)
        {
            Shift_Rating objEn_Rating = new Shift_Rating();
            ShiftApply _apply = new ShiftApply();
            List<Shift_dtls> ShftDtls = new List<Shift_dtls>();
            ShiftMasterEngine _header = new ShiftMasterEngine();
            _apply.header = _header;



            Session["ActionType"] = ActionType;

            if (ActionType == "ADD" && Shiftcode == "0")
            {
                ShftDtls.Add(new Shift_dtls
                {
                    ID = "1",
                    ShiftDay = "ShiftDay"
                    //ShiftDay = "Sunday"
                });
                //ShftDtls.Add(new Shift_dtls
                //{
                //    ID = "2",
                //    ShiftDay = "Monday"
                //});
                //ShftDtls.Add(new Shift_dtls
                //{
                //    ID = "3",
                //    ShiftDay = "Tuesday"
                //});
                //ShftDtls.Add(new Shift_dtls
                //{
                //    ID = "4",
                //    ShiftDay = "Wednesday"
                //});
                //ShftDtls.Add(new Shift_dtls
                //{
                //    ID = "5",
                //    ShiftDay = "Thursday"
                //});
                //ShftDtls.Add(new Shift_dtls
                //{
                //    ID = "6",
                //    ShiftDay = "Friday"
                //});
                //ShftDtls.Add(new Shift_dtls
                //{
                //    ID = "7",
                //    ShiftDay = "Saturday"
                //});

                _apply.dtls = ShftDtls;
                _apply.header.Shift_Id = "1";
                _apply.header.FormulaHeaderName = "Add Shift";
                ViewBag.title = "Rating-Add";
            }
            else if (ActionType == "EDIT" && Shiftcode != "0")
            {


                _apply = objEn_Rating.getFormulaDetailsById(Shiftcode, EditFlag, ShiftId);
                ViewBag.dtls = _apply.dtls;
                _apply.header.FormulaHeaderName = "Edit Shift";
                ViewBag.title = "Shift-Edit";

            }


            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            ReportsDataContext dcon = new ReportsDataContext(connectionString);

            var querycrm_CampaignTypes = (from c in dcon.tbl_shoptypes
                                          select c);
            //   _apply.header.Shop_Type = querycrm_CampaignTypes.ToList();

            return View(_apply);
        }

        [HttpPost]
        public JsonResult Apply(ShiftApply apply)
        {
            string output_msg = string.Empty;
            string tblformulaid = string.Empty;
            int ReturnCode = 0;
            string ReturnMsg = "";
            Shift_Rating objEn_Rating = new Shift_Rating();
            try
            {
                objEn_Rating.save(apply, Convert.ToString(Session["ActionType"]), ref tblformulaid, ref ReturnCode, ref ReturnMsg);
                if (ReturnMsg == "Success" && ReturnCode == 1)
                {
                    apply.response_code = "Success";
                    apply.response_msg = "Success";
                   // apply.header.tableFormulaCode = tblformulaid;

                }
                else if (ReturnMsg != "Success" && ReturnCode == -1)
                {
                    apply.response_code = "Error";
                    apply.response_msg = ReturnMsg;
                }
                else
                {
                    apply.response_code = "Error";
                    apply.response_msg = "Please try again later";
                }
            }
            catch (Exception ex)
            {
                apply.response_code = "CatchError";
                apply.response_msg = "Please try again later";
            }
            return Json(apply, JsonRequestBehavior.AllowGet);
        }
    }
}