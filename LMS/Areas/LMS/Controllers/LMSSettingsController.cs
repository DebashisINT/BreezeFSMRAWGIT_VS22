using DataAccessLayer;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Areas.LMS.Controllers
{
    public class LMSSettingsController : Controller
    {
        // GET: LMS/LMSSettings
        public ActionResult Index()
        {
            Boolean ShowClearQuiz=false;
            DataTable dt = new DataTable();
            dt = GETSETTINGSDATA();
            if(dt.Rows.Count>0)
            {
                 ShowClearQuiz = Convert.ToBoolean(dt.Rows[0]["ShowClearQuiz"]);
                
                //if(boolShowClearQuiz == true)
                //{
                //    ShowClearQuiz = 1;
                //}
                //else
                //{
                //    ShowClearQuiz = 0;
                //}
            }
            ViewBag.ShowClearQuiz = ShowClearQuiz;
            return View();
        }
        public JsonResult ClearQuiz()
        {
            int output = 0;
            Int32 Userid = Convert.ToInt32(Session["userid"]);
            output = ClearQuizSave(Userid);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        public int ClearQuizSave(Int32 user)
        {
            ProcedureExecute proc;

            try
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMS_SETTINGS", sqlcon);
                sqlcmd.Parameters.AddWithValue("@ACTION", "ClearQuiz");
                sqlcmd.Parameters.AddWithValue("@USER_ID", user);               
                SqlParameter output = new SqlParameter("@ReturnValue", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                Int32 ReturnValue = Convert.ToInt32(output.Value);

                sqlcmd.Dispose();
                sqlcmd.Dispose();
                return ReturnValue;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc = null;
            }
        }

        public DataTable GETSETTINGSDATA()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_LMS_SETTINGS");
            proc.AddPara("@ACTION", "ShowSettings");
            proc.AddPara("@USER_ID", Convert.ToInt32(Session["userid"])); 
            dt = proc.GetTable();
            return dt;
        }

    }
}