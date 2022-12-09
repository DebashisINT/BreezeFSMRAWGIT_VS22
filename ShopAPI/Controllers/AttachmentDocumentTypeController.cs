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
    public class AttachmentDocumentTypeController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage DocumentType(AttachmentDocumentTypeModel model)
        {
            AttachmentDocumentTypeList omodel = new AttachmentDocumentTypeList();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String DocumentTypeUrl = System.Configuration.ConfigurationSettings.AppSettings["DocumentTypeImage"];
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PROC_APIAttachmentDocumentType", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "DOCUMENT_TYPE");
                sqlcmd.Parameters.Add("@URL", DocumentTypeUrl);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<DocumentType> oview = new List<DocumentType>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oview.Add(new DocumentType()
                        {
                            id = Convert.ToString(dt.Rows[i]["dty_id"]),
                            type = Convert.ToString(dt.Rows[i]["dty_documentType"]),
                            image = Convert.ToString(dt.Rows[i]["image"]),
                            //Rev Debashis
                            IsForOrganization = Convert.ToBoolean(dt.Rows[i]["IsForOrganization"]),
                            IsForOwn = Convert.ToBoolean(dt.Rows[i]["IsForOwn"])
                            //End of Rev Debashis
                        });
                    }
                    omodel.type_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get document type";
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
        public HttpResponseMessage DocumentList(AttachmentDocumentTypeModel model)
        {
            AttachmentDocumentList omodel = new AttachmentDocumentList();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String AttachmentUrl = System.Configuration.ConfigurationSettings.AppSettings["DocumentAttachment"];
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PROC_APIAttachmentDocumentType", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@ACTION", "DOCUMENT_List");
                //Rev Debashis
                sqlcmd.Parameters.Add("@type_id", model.type_id);
                //End of Rev Debashis
                sqlcmd.Parameters.Add("@URL", AttachmentUrl);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    List<Documentes> oview = new List<Documentes>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        oview.Add(new Documentes()
                        {
                            id = Convert.ToString(dt.Rows[i]["ATTACHMENT_CODE"]),
                            type_id = Convert.ToString(dt.Rows[i]["TYPE_ID"]),
                            date_time = Convert.ToDateTime(dt.Rows[i]["DATE_TIME"]),
                            attachment = Convert.ToString(dt.Rows[i]["ATTACHMENT"]),
                            //Rev Debashis
                            document_name = Convert.ToString(dt.Rows[i]["DOCUMENT_NAME"])
                            //End of Rev Debashis
                        });
                    }
                    omodel.doc_list = oview;
                    omodel.status = "200";
                    omodel.message = "Successfully get document list";
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
        public HttpResponseMessage DeleteDocument(AttachmentDocumentDeleteInput model)
        {
            AttachmentDocumentDeleteOutput omodel = new AttachmentDocumentDeleteOutput();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                string sessionId = "";
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PROC_APIAttachmentDocumentType", sqlcon);
                sqlcmd.Parameters.Add("@ATTACHMENT_CODE", model.id);
                sqlcmd.Parameters.Add("@ACTION", "DOCUMENT_DELETE");

                sqlcmd.CommandType = CommandType.StoredProcedure;
                int isd = sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                if (isd > 0)
                {
                    omodel.status = "200";
                    omodel.message = "Successfully deleted document";
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
