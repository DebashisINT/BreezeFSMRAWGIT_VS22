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
    public class ContentController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetList(ContentModelInput model)
        {

            ContentModelOutput odata = new ContentModelOutput();


            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();


            sqlcmd = new SqlCommand("Proc_FTS_Get_ContentList", sqlcon);
            //sqlcmd.Parameters.Add("@Action", "City");


            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            List<ContentModel> omodel = new List<ContentModel>();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    omodel.Add(new ContentModel()
                    {
                        TemplateID = Convert.ToString(dt.Rows[i]["TemplateID"]),
                        Templatename = Convert.ToString(dt.Rows[i]["Templatename"]),
                        content = Convert.ToString(dt.Rows[i]["content"])
                    });
                }

                odata.status = "200";
                odata.message = "Success";
                odata.contentlist = omodel;

            }

            var message = Request.CreateResponse(HttpStatusCode.OK, odata);
            return message;

        }
    }
}
