using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ChatController : Controller
    {
        DBEngine odbengine = new DBEngine();
        NotificationBL notificationbl = new NotificationBL();
        //
        // GET: /MYSHOP/Chat/
        public ActionResult Group()
        {
            CustomSMSClass notification = new CustomSMSClass();

            DataTable dt = odbengine.GetDataTable("SELECT '0' ID,'All' Name Union all select CAST(deg_id as VARCHAR(20)) ID,deg_designation Name FROM tbl_master_designation WHERE deg_id IN (SELECT desg.deg_id FROM tbl_master_employee EMP INNER JOIN (SELECT cnt.emp_cntId,desg.deg_designation,MAX(emp_id) as emp_id,desg.deg_id FROM tbl_trans_employeeCTC as cnt LEFT OUTER JOIN tbl_master_designation desg ON desg.deg_id=cnt.emp_Designation GROUP BY emp_cntId,desg.deg_designation,desg.deg_id) DESG ON DESG.emp_cntId=EMP.emp_contactId )");
            DataTable dtuser = notificationbl.FetchNotificationSP("", "", "GetFirstTimeUserWithLoginID", "");
            DataTable dtstate = notificationbl.FetchNotificationSP("", "", "GetDesignationByState", "");
            notification.SupervisorList = APIHelperMethods.ToModelList<SupervisorList>(dt);
            notification.UserList = APIHelperMethods.ToModelList<UserNotificationList>(dtuser);
            notification.StateList = APIHelperMethods.ToModelList<StateList>(dtstate);
            return View(notification);
        }
        public JsonResult SaveGroup(string grpName, string users)
        {
            string status = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_Chat", sqlcon);
                sqlcmd.Parameters.Add("@Action", "SaveGroup");
                sqlcmd.Parameters.Add("@GROUP_NAME", grpName);
                sqlcmd.Parameters.Add("@USERS", users);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    status = Convert.ToString(dt.Rows[0][0]);
                }


                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                status = "300";
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

	}
}