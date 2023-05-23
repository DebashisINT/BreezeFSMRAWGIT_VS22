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
    public class VisitRemarksController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage List(VisitRemarksInput model)
        {
            VisitRemarksOutput omodel = new VisitRemarksOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationManager.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_VisitRemarks", sqlcon);
                sqlcmd.Parameters.AddWithValue("@Action", "List");
                sqlcmd.Parameters.AddWithValue("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<VisitRemarks> oview = new List<VisitRemarks>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oview.Add(new VisitRemarks()
                        {
                            id = Convert.ToString(dt.Rows[i]["id"]),
                            name = Convert.ToString(dt.Rows[i]["name"])
                        });
                    }
                    omodel.remarks_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get list";
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
