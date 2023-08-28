using DataAccessLayer;
using FSMMaster.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FSMMaster.Areas.MASTER.Controllers
{
    public class CityMasterController : Controller
    {
        // GET: MASTER/CityMaster
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCityDetailsList(CityMasterModel model)
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
                sqlcmd = new SqlCommand("PRC_FTSCityMaster", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "GETLISTINGDETAILS");
                sqlcmd.Parameters.Add("@USERID", Userid);
                sqlcmd.Parameters.Add("@ISPAGELOAD", model.Is_PageLoad);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                //return Json(Userid, JsonRequestBehavior.AllowGet);
                return PartialView("PartialCityMasterGridList", LGetCityDetailsList(Is_PageLoad));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable LGetCityDetailsList(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            if (Is_PageLoad != "Ispageload")
            {
                MastersDataContext dc = new MastersDataContext(connectionString);
                var q = from d in dc.FTS_CityMaster_Lists
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                MastersDataContext dc = new MastersDataContext(connectionString);
                var q = from d in dc.FTS_CityMaster_Lists
                        where d.USERID == Convert.ToInt32(Userid) && d.city_id == 0
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        
    }
}