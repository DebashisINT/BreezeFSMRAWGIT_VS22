using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.OMS.Management.Attendance
{
    public partial class Frm_Attendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProcedureExecute proc = new ProcedureExecute("Prc_AttendanceSystem");
                proc.AddVarcharPara("@Action", 100, "GetEmpNameByUserid");
                proc.AddIntegerPara("@User", Convert.ToInt32(Session["userid"]));
                DataTable dt = proc.GetTable();
                hdEmpName.Value = dt.Rows[0][0].ToString();
                EmpId.Value = dt.Rows[0][1].ToString();


                ProcedureExecute procd = new ProcedureExecute("PRC_GetAttendanceStatus");
                procd.AddPara("@EMP_ID", EmpId.Value);
                DataTable dtattnd = procd.GetTable();
                hdnIsLeaveonApprovval.Value = dtattnd.Rows[0]["IsLeaveonApprovval"].ToString();
                hdnisGivenAttendance.Value = dtattnd.Rows[0]["isGivenAttendance"].ToString();

                hdnUserID.Value = Convert.ToString(Session["userid"]);

                cmbDOJ.EditFormatString = "dd-MM-yyyy";
                cmbDOJ.DisplayFormatString = "dd-MM-yyyy";
                cmbDOJ.Date = DateTime.Today;
                cmbDOJ.MinDate = DateTime.Today;

                cmbLeaveEff.UseMaskBehavior = true;
                cmbLeaveEff.EditFormatString = "dd-MM-yyyy";
                cmbLeaveEff.DisplayFormatString = "dd-MM-yyyy";
                cmbLeaveEff.Date = DateTime.Today;
                cmbLeaveEff.MinDate = DateTime.Today;
            }
        }

        [AcceptVerbs("POST")]
        [WebMethod]
        public static object AttendanceShop(AttendancemanageInput model)
        {
            //String weburl = System.Configuration.ConfigurationSettings.AppSettings["PortalShopEdit"];
            string apiUrl = "http://3.7.30.86:82/API/ShopAttendance/AttendanceSubmit";
            //"http://localhost:16126/ShopRegisterPortal/EditShop"


            //string apiUrl = "http://localhost:26404/api/CustomerAPI";
            //var input = new
            //{
            //    Name = txtName.Text.Trim(),
            //};
            string inputJson = (new JavaScriptSerializer()).Serialize(model);
            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl , inputContent).Result;

            return (response.ReasonPhrase);
        }

        [AcceptVerbs("POST")]
        [WebMethod]
        public static object LogOutSubmit(Model_Logout model)
        {
            //String weburl = System.Configuration.ConfigurationSettings.AppSettings["PortalShopEdit"];
            string apiUrl = "http://3.7.30.86:82/API/Logout/UserLogout";
            string inputJson = (new JavaScriptSerializer()).Serialize(model);
            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, inputContent).Result;

            return (response.ReasonPhrase);
        }

        [AcceptVerbs("POST")]
        [WebMethod]
        public static object LeaveApprovalSubmit(LeaveApprovalRecord model)
        {
            //String weburl = System.Configuration.ConfigurationSettings.AppSettings["PortalShopEdit"];
            string apiUrl = "http://3.7.30.86:82/API/LeaveApproval/Records";
            string inputJson = (new JavaScriptSerializer()).Serialize(model);
            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiUrl, inputContent).Result;

            return (response.ReasonPhrase);
        }

        [WebMethod]
        public static object EmpAttendance(String EmpID)
        {
            EmpAttendanceStatus cls = new EmpAttendanceStatus();
            ProcedureExecute procd = new ProcedureExecute("PRC_GetAttendanceStatus");
            procd.AddPara("@EMP_ID", EmpID);
            DataTable dtattnd = procd.GetTable();
            cls.IsLeaveonApprovval = dtattnd.Rows[0]["IsLeaveonApprovval"].ToString();
            cls.isGivenAttendance = dtattnd.Rows[0]["isGivenAttendance"].ToString();
            cls.user_id = dtattnd.Rows[0]["user_id"].ToString();
            return cls;
        }
    }

    public class AttendancemanageInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string work_type { get; set; }
        public string work_desc { get; set; }
        public string work_lat { get; set; }
        public string work_long { get; set; }
        public string work_address { get; set; }
        public string work_date_time { get; set; }
        public string is_on_leave { get; set; }
        public string add_attendence_time { get; set; }
        public string route { get; set; }
        public string leave_from_date { get; set; }
        public string leave_to_date { get; set; }
        public string leave_type { get; set; }
        public string Distributor_Name { get; set; }
        public string Market_Worked { get; set; }
        public string order_taken { get; set; }
        public string collection_taken { get; set; }
        public string new_shop_visit { get; set; }
        public string revisit_shop { get; set; }
        public string state_id { get; set; }
        public string IsNoPlanUpdate { get; set; }
        public List<shopAttendance> shop_list { get; set; }
        public List<StatewiseTraget> primary_value_list { get; set; }
        public List<UpdatePlanList> Update_Plan_List { get; set; }
        public string leave_reason { get; set; }
        public string from_id { get; set; }
        public string to_id { get; set; }
        public string distance { get; set; }
    }
    
    public class StatewiseTraget
    {
        public string id { get; set; }
        public string primary_value { get; set; }
    }

    public class shopAttendance
    {
        public string route { get; set; }
        public string shop_id { get; set; }
    }
   
    public class UpdatePlanList
    {
        public string plan_id { get; set; }
        public DateTime plan_date { get; set; }
        public decimal? plan_value { get; set; }
        public string plan_remarks { get; set; }
        public decimal? achievement_value { get; set; }
        public string acheivement_remarks { get; set; }
    }

    public class Model_Logout
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string logout_time { get; set; }
        public string Autologout { get; set; }
        public string distance { get; set; }
        public string address { get; set; }
    }

    public class EmpAttendanceStatus
    {
        public String IsLeaveonApprovval { get; set; }
        public String isGivenAttendance { get; set; }
        public String user_id { get; set; }
    }

    public class LeaveApprovalRecord
    {
        public string user_id { get; set; }
        public string session_token { get; set; }
        public string leave_type { get; set; }
        public string leave_to_date { get; set; }
        public string leave_reason { get; set; }
        public string leave_from_date { get; set; }
        public string leave_long { get; set; }
        public string leave_lat { get; set; }
        public string leave_add { get; set; }
    }
}