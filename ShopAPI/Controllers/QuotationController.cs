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
    public class QuotationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AddQuotation(QuotationAddInput model)
        {
            QuotationAddOutput omodel = new QuotationAddOutput();

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
                sqlcmd = new SqlCommand("Proc_API_QuotationAddEdit", sqlcon);
                sqlcmd.Parameters.Add("@Action", "ADD");
                sqlcmd.Parameters.Add("@QUOTATION_CODE", model.quo_id);
                sqlcmd.Parameters.Add("@QUOTATION_NO", model.quo_no);
                sqlcmd.Parameters.Add("@QUOTATION_DATE", model.date);
                sqlcmd.Parameters.Add("@SHOP_CODE", model.shop_id);
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@HYPOTHECATION", model.hypothecation);
                sqlcmd.Parameters.Add("@ACCOUNT_NO", model.account_no);
                sqlcmd.Parameters.Add("@MODEL_ID", model.model_id);
                sqlcmd.Parameters.Add("@BS_ID", model.bs_id);
                sqlcmd.Parameters.Add("@GEARBOX", model.gearbox);
                sqlcmd.Parameters.Add("@NUMBER1", model.number1);
                sqlcmd.Parameters.Add("@VALUE1", model.value1);
                sqlcmd.Parameters.Add("@VALUE2", model.value2);
                sqlcmd.Parameters.Add("@TYRES1", model.tyres1);
                sqlcmd.Parameters.Add("@NUMBER2", model.number2);
                sqlcmd.Parameters.Add("@VALUE3", model.value3);
                sqlcmd.Parameters.Add("@VALUE4", model.value4);
                sqlcmd.Parameters.Add("@TYRES2", model.tyres2);
                sqlcmd.Parameters.Add("@AMOUNT", model.amount);
                sqlcmd.Parameters.Add("@DISCOUNT", model.discount);
                sqlcmd.Parameters.Add("@CGST", model.cgst);
                sqlcmd.Parameters.Add("@SGST", model.sgst);
                sqlcmd.Parameters.Add("@TCS", model.tcs);
                sqlcmd.Parameters.Add("@INSURANCE", model.insurance);
                sqlcmd.Parameters.Add("@NET_AMOUNT", model.net_amount);
                sqlcmd.Parameters.Add("@REMARKS", model.remarks);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt!=null && dt.Rows.Count> 0)
                {
                    omodel.status = "200";
                    omodel.message = dt.Rows[0]["msg"].ToString();
                }
                else
                {
                    omodel.status = "304";
                    omodel.message = "Quotation not add.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage List(QuotationListInput model)
        {
            QuotationListOutput omodel = new QuotationListOutput();
            List<QuotationModel> oview = new List<QuotationModel>();
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
                sqlcmd = new SqlCommand("Proc_API_QuotationAddEdit", sqlcon);
                sqlcmd.Parameters.Add("@USER_ID", model.user_id);
                sqlcmd.Parameters.Add("@Action", "LIST");
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<QuotationModel>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get quotation list.";
                    omodel.quot_list = oview;
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
