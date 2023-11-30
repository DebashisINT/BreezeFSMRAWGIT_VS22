using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer.SalesmanTrack;
using FSMMaster.Models;

namespace FSMMaster.Areas.MASTER.Controllers
{
    public class CountryMasterController : Controller
    {
        // GET: MASTER/CountryMaster
        CountryMasterModels obj = new CountryMasterModels();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PartialGridList(CountryMasterModels model)
        {
            try
            {
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
                sqlcmd = new SqlCommand("PRC_COUNTRYMASTER", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "GETLISTINGDETAILS");
                sqlcmd.Parameters.Add("@USER_ID", Userid);
                sqlcmd.Parameters.Add("@ISPAGELOAD", model.Is_PageLoad);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();              
                return PartialView("PartialCountryList", LGetCountryDetailsList(Is_PageLoad));
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
                MastersDataContext dc = new MastersDataContext(connectionString);
                var q = from d in dc.CountryMasterLists
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                MastersDataContext dc = new MastersDataContext(connectionString);
                var q = from d in dc.CountryMasterLists
                        where d.USERID == Convert.ToInt32(Userid) 
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }
        public JsonResult SaveCountry(string name, string id)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = obj.SaveCountry( name, Userid, id);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditCountry(string id)
        {
            DataTable output = new DataTable();
            output = obj.EditCountry(id);  

            if (output.Rows.Count > 0)
            {
                return Json(new { name = Convert.ToString(output.Rows[0]["cou_country"]) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new {  name = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Delete(string ID)
        {
            int output = 0;
            output = obj.Deletecountry(ID);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        

    }
}