using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class AreaController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AreaList(AreaModel model)
        {
            AreaOutPut odata = new AreaOutPut();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {

                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                List<AreaLocation> omedl2 = new List<AreaLocation>();

                DataTable ds = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("prc_API_AreaList", sqlcon);
                sqlcmd.Parameters.Add("@User_Id", model.user_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds.Rows.Count > 0)
                {
                    omedl2 = APIHelperMethods.ToModelList<AreaLocation>(ds);
                    odata.message = "Successfully get location list";
                    odata.status = "200";
                    odata.loc_list = omedl2;
                }
                else
                {
                    odata.message = "No Data found.";
                    odata.status = "205";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }

        }

        [HttpPost]
        public HttpResponseMessage DistanceList(DistanceListInput model)
        {
            DistanceListOutPut odata = new DistanceListOutPut();
            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";

                DataTable ds = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSFromToAreaDistance", sqlcon);
                sqlcmd.Parameters.Add("@From_AreaID", model.from_id);
                sqlcmd.Parameters.Add("@To_AreaID", model.to_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(ds);
                sqlcon.Close();
                if (ds != null && ds.Rows.Count > 0)
                {
                    odata.message = "Successfully get distance";
                    odata.status = "200";
                    odata.distance = ds.Rows[0]["DISTANCE"].ToString();
                }
                else
                {
                    odata.message = "No Data found.";
                    odata.status = "205";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
    }
}
