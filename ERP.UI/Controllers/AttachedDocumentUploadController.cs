using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class AttachedDocumentUploadController : Controller
    {
        [AcceptVerbs("POST")]
        public JsonResult OrganizationDocUpload(Docuploads model)
        {
            if (model.documents != null)
            {
                if (!string.IsNullOrEmpty(model.DOCUMENT_NAME))
                {
                    string fileName = Path.GetFileName(model.documents.FileName);
                    int fileSize = model.documents.ContentLength;
                    int Size = fileSize / 1000;
                    string FileType = System.IO.Path.GetExtension(fileName);
                    int SizeIcon = 0;
                    if (System.IO.File.Exists(Server.MapPath("~/Commonfolder/OrganizationDocument/" + fileName)))
                    {
                        fileName = DateTime.Now.ToString("hhmmss") + fileName;
                    }
                    model.documents.SaveAs(Server.MapPath("~/Commonfolder/OrganizationDocument/" + fileName));
                    string CS = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        SqlCommand cmd = new SqlCommand("PRC_DocumentUploadFromOrganization", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@DOCUMENT_NAME", model.DOCUMENT_NAME);
                        cmd.Parameters.AddWithValue("@ATTACHMENT_CODE", Convert.ToString(Session["userid"])+model.ATTACHMENT_CODE);
                        cmd.Parameters.AddWithValue("@TYPE_ID", model.TYPE_ID);
                        cmd.Parameters.AddWithValue("@ATTACHMENT", "/Commonfolder/OrganizationDocument/" + fileName);
                        cmd.Parameters.AddWithValue("@CREATED_BY", Convert.ToString(Session["userid"]));
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    return Json("Please enter description.");
                }
            }
            else
            {
                return Json("Please select file.");
            }
            return Json("OK");
        }

        public class Docuploads
        {
            public string DOCUMENT_NAME { get; set; }
            public string ATTACHMENT_CODE { get; set; }
            public string TYPE_ID { get; set; }
            public HttpPostedFileBase documents { get; set; }
        }
	}
}