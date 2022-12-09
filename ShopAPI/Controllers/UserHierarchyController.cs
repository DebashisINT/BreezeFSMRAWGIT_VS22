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
    public class UserHierarchyController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage HierarchyMemberList(useHierarchyinput model)
        {
            useHierarchyOutput omodel = new useHierarchyOutput();
            List<User_list> oview = new List<User_list>();
          
            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
           
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIHierarchyWiseUser", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@ACTION", "MEMBER");
                sqlcmd.Parameters.Add("@isFirstScreen", model.isFirstScreen);
                sqlcmd.Parameters.Add("@isAllTeam", model.isAllTeam);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<User_list>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get member list.";
                    omodel.member_list = oview;
                }
                else
                {
                    omodel.status = "205";
                    omodel.message = "No data found.";
                }
                var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage HierarchyShopList(UseHierarchyShopInput model)
        {
            useHierarchyShopOutput omodel = new useHierarchyShopOutput();
            List<UserShopList> oview = new List<UserShopList>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_APIHierarchyWiseUser", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@ACTION", "SHOPLIST");
                sqlcmd.Parameters.Add("@area_id", model.area_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<UserShopList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get shop list.";
                    omodel.shop_list = oview;
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
        public HttpResponseMessage ShopTypewiseUserShopList(UserSopTypeShopInput model)
        {
            useHierarchyShopOutput omodel = new useHierarchyShopOutput();
            List<UserShopList> oview = new List<UserShopList>();

            if (!ModelState.IsValid)
            {
                omodel.status = "213";
                omodel.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, omodel);
            }
            else
            {
                String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_FTSAPI_ShopHierarchy", sqlcon);
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@SHOP_CODE", model.shop_id);
                sqlcmd.Parameters.Add("@area_id", model.area_id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    oview = APIHelperMethods.ToModelList<UserShopList>(dt);
                    omodel.status = "200";
                    omodel.message = "Successfully get shop list.";
                    omodel.shop_list = oview;
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

        //Rev Debashis
        [HttpPost]
        public HttpResponseMessage UserReportToInfo(UserReportToInfoInput model)
        {
            UserReportToInfoOutput odata = new UserReportToInfoOutput();

            if (!ModelState.IsValid)
            {
                odata.status = "213";
                odata.message = "Some input parameters are missing.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, odata);
            }
            else
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("PRC_APIHierarchyWiseUser", sqlcon);
                sqlcmd.Parameters.Add("@ACTION", "USERREPORTTO");
                sqlcmd.Parameters.Add("@user_id", model.user_id);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    odata.status = "200";
                    odata.message = "Successfully get Information.";
                    odata.user_id = dt.Rows[0]["user_id"].ToString();
                    odata.report_to_user_id = dt.Rows[0]["report_to_user_id"].ToString();
                    odata.report_to_user_name = dt.Rows[0]["report_to_user_name"].ToString();
                }
                else
                {
                    odata.status = "205";
                    odata.message = "No data found.";
                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }
        //End of Rev Debashis
    }
}
