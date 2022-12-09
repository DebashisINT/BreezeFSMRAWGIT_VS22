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
    public class StageController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage List(StageModel model)
        {
            StageOutput omodel = new StageOutput();
            List<Stages> oview = new List<Stages>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_Leadlist", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@Action", "Stage");
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Stages>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get stage list.";
                    omodel.stage_list = oview;
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data found";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage FunnelStageList(StageModel model)
        {
            FunnelStageOutput omodel = new FunnelStageOutput();
            List<Stages> oview = new List<Stages>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_Leadlist", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@Action", "FunnelStage");
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<Stages>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get funnel stage list.";
                    omodel.funnel_stage_list = oview;
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data found";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }
    }
}
