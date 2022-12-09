using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class KnowYourStateController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage KYSDetails(KYSInput objKYSInput)
        {
            try
            {
                KYSListOutput odata = new KYSListOutput();
                if (!ModelState.IsValid)
                {
                    odata.status = "213";
                    odata.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
                }
                else
                {
                    DataTable data = new DataTable();


                    String tokenmatch = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    // String con = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
                    SqlCommand sqlcmd = new SqlCommand();
                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();

                    sqlcmd = new SqlCommand("proc_BindKnowYourStateGridAPI", sqlcon);
                    sqlcmd.Parameters.Add("@User_id", objKYSInput.user_id);
                    sqlcmd.Parameters.Add("@Year", objKYSInput.year);
                    sqlcmd.Parameters.Add("@Month", objKYSInput.month);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(data);
                    sqlcon.Close();


                    List<KYS> lstKYS = new List<KYS>();


                    foreach (DataColumn item in data.Columns)
                    {
                        KYS objKyc = new KYS();
                        objKyc.Key = Convert.ToString(item.ColumnName);
                        if (data.Rows.Count > 0)
                        {
                            objKyc.Value = Convert.ToString(data.Rows[0][item.ColumnName]);
                        }
                        else
                        {
                            objKyc.Value = "";
                        }
                        lstKYS.Add(objKyc);
                    }

                    odata.know_state_list = lstKYS;
                    odata.status = "200";
                    odata.message = "Know your state list populated";

                    var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                    return message;
                }
            }
            catch (Exception ex)
            {
                var message = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return message;
            }
        }
    }



    public class KYS
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class KYSInput
    {
        public String session_token { get; set; }
        public string user_id { get; set; }
        public string month { get; set; }
        public string year { get; set; }
    }

    public class KYSListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<KYS> know_state_list { get; set; }
    }
}