using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace LMS.Models
{
    /// <summary>
    /// Summary description for LMSService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class LMSService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetUserList(string SearchKey)
        {
            List<UserModel> listUser = new List<UserModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                //BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                //DataTable Shop = oDBEngine.GetDataTable("select top(10)cnt_internalId,Replace(ISNULL(cnt_firstName,'')+' '+ISNULL(cnt_middleName,'')+ ' '+ISNULL(cnt_lastName,''),'''','&#39;') AS Employee_Name,cnt_UCC from tbl_master_contact where (cnt_firstName like '%" + SearchKey + "%') or  (cnt_middleName like '%" + SearchKey + "%') or  (cnt_lastName like '%" + SearchKey + "%')");
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_LMS_REPORTS");
                proc.AddVarcharPara("@ACTION", 100, "GETUSER");
                proc.AddPara("@SearchKey", SearchKey);
                dt = proc.GetTable();

                listUser = (from DataRow dr in dt.Rows
                                select new UserModel()
                                {
                                    user_id = Convert.ToString(dr["user_id"]),
                                    user_name = Convert.ToString(dr["user_name"]),
                                    user_loginId = Convert.ToString(dr["user_loginId"])
                                }).ToList();
            }

            return listUser;
        }

    }

    public class UserModel
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string user_loginId { get; set; }
    }
}
