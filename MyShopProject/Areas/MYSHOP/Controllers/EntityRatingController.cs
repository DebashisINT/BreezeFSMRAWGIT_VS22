using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class EntityRatingController : Controller
    {
        //
        // GET: /MYSHOP/EntityRating/
        public ActionResult MasterSetting(string ActionType, string Ratingcode, string EditFlag, int RatingId)
        {
            En_Rating objEn_Rating = new En_Rating();
            FormulaApply _apply = new FormulaApply();
            P_formula_header _header = new P_formula_header();
            _apply.header = _header;



            Session["ActionType"] = ActionType;

            if (ActionType == "ADD" && Ratingcode == "")
            {
                _apply.header.appl_for = "1";
                _apply.header.FormulaHeaderName = "Add Rating";
                ViewBag.title = "Rating-Add";
            }
            else if (ActionType == "EDIT" && Ratingcode != "")
            {


                _apply = objEn_Rating.getFormulaDetailsById(Ratingcode, EditFlag, RatingId);
                // ViewBag.dtls = _apply.dtls;
                _apply.header.FormulaHeaderName = "Edit Rating";
                ViewBag.title = "Rating-Edit";

            }


            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            ReportsDataContext dcon = new ReportsDataContext(connectionString);

            var querycrm_CampaignTypes = (from c in dcon.tbl_shoptypes
                                          select c);
            _apply.header.Shop_Type = querycrm_CampaignTypes.ToList();

            return View(_apply);
        }

        public PartialViewResult _PartialMasterSettingGrid()
        {
            return PartialView(GetReport(""));
        }

        public ActionResult Index()
        {
            Session["ActionType"] = "";
            return View();
        }


        [HttpPost]
        public JsonResult Apply(FormulaApply apply)
        {
            string output_msg = string.Empty;
            string tblformulaid = string.Empty;
            int ReturnCode = 0;
            string ReturnMsg = "";
            En_Rating objEn_Rating = new En_Rating();
            try
            {
                objEn_Rating.save(apply,Convert.ToString(Session["ActionType"]), ref tblformulaid, ref ReturnCode, ref ReturnMsg);
                if (ReturnMsg == "Success" && ReturnCode == 1)
                {
                    apply.response_code = "Success";
                    apply.response_msg = "Success";
                    apply.header.tableFormulaCode = tblformulaid;

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
        [HttpPost]
        public JsonResult RatingDelete(string ActionType, string code)
        {

            string output_msg = string.Empty;
            int ReturnCode = 0;
            En_Rating objEn_Rating = new En_Rating();
            Msg _msg = new Msg();
            try
            {
                output_msg = objEn_Rating.Delete(ActionType, code, ref ReturnCode);
                if (output_msg == "Success" && ReturnCode == 1)
                {
                    _msg.response_code = "Success";
                    _msg.response_msg = "Success";
                }
                else if (output_msg != "Success" && ReturnCode == -1)
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

        public IEnumerable GetReport(string ispageload)
        {

            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            string Userid = Convert.ToString(Session["userid"]);

            
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.v_EntityRatingLists                        
                        orderby d.orderbyDate descending
                        select d;
                return q;
            



        }
	}
}