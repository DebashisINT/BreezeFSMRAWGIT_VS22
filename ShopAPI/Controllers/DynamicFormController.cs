using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace ShopAPI.Controllers
{
    public class DynamicFormController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetForm(DynamicFormInput model)
        {

            DynamicFormOutput odata = new DynamicFormOutput();

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

                List<view> omedl2 = new List<view>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();


                sqlcmd = new SqlCommand("PRC_DYNAMIC_LAYOUT", sqlcon);
                sqlcmd.Parameters.Add("@action", "ListItems");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@dynamicFormName", model.dynamicFormName);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();



                if (dt.Rows.Count > 0)
                {

                    odata.status = "200";
                    odata.message = "Successfully get form.";


                    foreach (DataRow dr in dt.Rows)
                    {
                        view oview = new view();
                        oview.header = Convert.ToString(dr["LAYOUT_HEADER"]);
                        oview.id = Convert.ToString(dr["DETAILS_ID"]);
                        oview.value = Convert.ToString("");
                        oview.type = Convert.ToString(dr["LAYOUT_TYPE"]);
                        oview.text_type = Convert.ToString(dr["LAYOUT_DATATYPE"]);
                        oview.max_length = Convert.ToString(dr["LAYOUT_MAXLENGTH"]);

                        List<item> lstItem = new List<item>();

                        if (Convert.ToString(dr["LAYOUT_TYPE"]) == "dropdown" || Convert.ToString(dr["LAYOUT_TYPE"]) == "check"
                            || Convert.ToString(dr["LAYOUT_TYPE"]) == "radio" || Convert.ToString(dr["LAYOUT_TYPE"]) == "date"
                            || Convert.ToString(dr["LAYOUT_TYPE"]) == "attachment")
                        {

                            view oview1 = new view();
                            oview1.header = Convert.ToString(dr["LAYOUT_HEADER"]) + " :";
                            oview1.id = "10000" +
                                Convert.ToString(dr["DETAILS_ID"]);
                            oview1.value = Convert.ToString("");
                            oview1.type = "text";
                            omedl2.Add(oview1);

                            string itm = Convert.ToString(dr["LAYOUT_ITEM"]);

                            string[] str = itm.Split(',');

                            int i = 1;
                            foreach (string st in str)
                            {
                                item it = new item();
                                it.id = Convert.ToString(dr["DETAILS_ID"]) + i;
                                it.items = st;
                                it.isSelected = "false";
                                lstItem.Add(it);
                                i = i + 1;
                            }



                        }

                        oview.item_list = lstItem;
                        omedl2.Add(oview);


                    }

                    odata.view_list = omedl2;


                }
                else
                {
                    odata.status = "205";
                    odata.message = "Some input parameters are missing.";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage SaveForm(DynamicFormSaveInput model)
        {

            DynamicFormSaveOutput odata = new DynamicFormSaveOutput();

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

                string result = "";
                using (var stringwriter = new System.IO.StringWriter())
                {
                    var serializer = new XmlSerializer(model.view_list.GetType());
                    serializer.Serialize(stringwriter, model.view_list);
                    result = stringwriter.ToString();
                }



                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();





                sqlcmd = new SqlCommand("PRC_DYNAMIC_LAYOUT", sqlcon);
                sqlcmd.Parameters.Add("@action", "SaveItems");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@result", result);
                sqlcmd.Parameters.Add("@dynamicFormName", model.dynamicFormName);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();



                if (dt.Rows.Count > 0)
                {

                    odata.status = "200";
                    odata.message = "Saved Successfully.";

                }
                else
                {
                    odata.status = "205";
                    odata.message = "Some input parameters are missing.";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }


        [HttpPost]
        public HttpResponseMessage GetFormList(DynamicFormListInput model)
        {

            DynamicFormListOutput odata = new DynamicFormListOutput();

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

                List<FormList> omedl2 = new List<FormList>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();


                sqlcmd = new SqlCommand("PRC_DYNAMIC_LAYOUT", sqlcon);
                sqlcmd.Parameters.Add("@action", "FormList");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@dynamicFormName", model.dynamicFormName);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();



                if (dt.Rows.Count > 0)
                {

                    odata.status = "200";
                    odata.message = "Successfully get form.";

                    omedl2 = (from DataRow dr in dt.Rows
                              select new FormList()
                              {
                                  id = Convert.ToString(dr["ID"]),
                                  name = Convert.ToString(dr["LAYOUT_NAME"])
                              }).ToList();

                    odata.form_list = omedl2;


                }
                else
                {
                    odata.status = "201";
                    odata.message = "No data found.";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }


        [HttpPost]
        public HttpResponseMessage GetFormWiseList(DynamicFormWiseListInput model)
        {

            DynamicFormWiseListOutput odata = new DynamicFormWiseListOutput();

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

                List<FormWiseList> omedl2 = new List<FormWiseList>();

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();


                sqlcmd = new SqlCommand("PRC_DYNAMIC_LAYOUT", sqlcon);
                sqlcmd.Parameters.Add("@action", "FormWiseList");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@key", model.id);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();



                if (dt.Rows.Count > 0)
                {

                    odata.status = "200";
                    odata.message = "Successfully get form.";

                    //omedl2 = (from DataRow dr in dt.Rows
                    //          select new FormWiseList()
                    //          {
                    //              id = Convert.ToString(dr["ID"]),
                    //              date = Convert.ToDateTime(dr["CREATED_DATE"]).ToString("yyyy-MM-dd")
                    //          }).ToList();

                    foreach (DataRow item in dt.Rows)
                    {
                        FormWiseList obj = new FormWiseList();
                        obj.date = Convert.ToDateTime(item["CREATED_DATE"]).ToString("yyyy-MM-dd");
                        obj.id = Convert.ToString(item["ID"]);
                        obj.super_id = Convert.ToString(model.id);
                        List<FormWiseItem> lstitem = new List<FormWiseItem>();
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName != "CREATED_DATE" && dc.ColumnName != "ID" && dc.ColumnName != "USER_ID")
                            {
                                FormWiseItem sc = new FormWiseItem();
                                sc.key = dc.ColumnName + " : " + Convert.ToString(item[dc.ColumnName]);
                                lstitem.Add(sc);
                            }
                        }
                        obj.field_list = lstitem;
                        omedl2.Add(obj);

                    }

                    odata.info_list = omedl2;


                }
                else
                {
                    odata.status = "201";
                    odata.message = "No data found.";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }


        [HttpPost]
        public HttpResponseMessage SaveFormEdit(DynamicFormSaveEditInput model)
        {

            DynamicFormSaveOutput odata = new DynamicFormSaveOutput();

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

                string result = "";
                using (var stringwriter = new System.IO.StringWriter())
                {
                    var serializer = new XmlSerializer(model.view_list.GetType());
                    serializer.Serialize(stringwriter, model.view_list);
                    result = stringwriter.ToString();
                }



                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();





                sqlcmd = new SqlCommand("PRC_DYNAMIC_LAYOUT", sqlcon);
                sqlcmd.Parameters.Add("@action", "SaveItemsEdit");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@key", model.id);
                sqlcmd.Parameters.Add("@result", result);
                sqlcmd.Parameters.Add("@filename", model.attachments);
                sqlcmd.Parameters.Add("@dynamicFormName", model.dynamicFormName);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();



                if (dt.Rows.Count > 0)
                {

                    odata.status = "200";
                    odata.message = "Saved Successfully.";

                }
                else
                {
                    odata.status = "205";
                    odata.message = "Some input parameters are missing.";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

        [HttpPost]
        public HttpResponseMessage GetFormEdit(DynamicFormEditInput model)
        {

            DynamicFormOutput odata = new DynamicFormOutput();

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

                List<view> omedl2 = new List<view>();

                DataSet dt = new DataSet();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();


                sqlcmd = new SqlCommand("PRC_DYNAMIC_LAYOUT", sqlcon);
                sqlcmd.Parameters.Add("@action", "ListItemsEdit");
                sqlcmd.Parameters.Add("@user_id", model.user_id);
                sqlcmd.Parameters.Add("@key", model.id);
                sqlcmd.Parameters.Add("@dynamicFormName", model.dynamicFormName);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();



                if (dt.Tables[0].Rows.Count > 0)
                {

                    odata.status = "200";
                    odata.message = "Successfully get form.";


                    foreach (DataRow dr in dt.Tables[0].Rows)
                    {
                        view oview = new view();
                        oview.header = Convert.ToString(dr["LAYOUT_HEADER"]);
                        oview.id = Convert.ToString(dr["DETAILS_ID"]);
                        if (Convert.ToString(dr["LAYOUT_TYPE"]) == "attachment")
                        {
                            oview.value = Convert.ToString(dt.Tables[1].Rows[0]["FILE_NAME"]);
                        }
                        else
                        {
                            oview.value = Convert.ToString(dt.Tables[1].Rows[0][Convert.ToString(dr["LAYOUT_HEADER"])]);
                        }
                        oview.type = Convert.ToString(dr["LAYOUT_TYPE"]);
                        oview.text_type = Convert.ToString(dr["LAYOUT_DATATYPE"]);
                        oview.max_length = Convert.ToString(dr["LAYOUT_MAXLENGTH"]);

                        List<item> lstItem = new List<item>();

                        if (Convert.ToString(dr["LAYOUT_TYPE"]) == "dropdown" || Convert.ToString(dr["LAYOUT_TYPE"]) == "check"
                            || Convert.ToString(dr["LAYOUT_TYPE"]) == "radio" || Convert.ToString(dr["LAYOUT_TYPE"]) == "date"
                            || Convert.ToString(dr["LAYOUT_TYPE"]) == "attachment")
                        {

                            view oview1 = new view();
                            oview1.header = Convert.ToString(dr["LAYOUT_HEADER"]) + " :";
                            oview1.id = "10000" +
                                Convert.ToString(dr["DETAILS_ID"]);
                            oview1.value = Convert.ToString("");
                            oview1.type = "text";
                            omedl2.Add(oview1);

                            string[] stSelected = Convert.ToString(dt.Tables[1].Rows[0][Convert.ToString(dr["LAYOUT_HEADER"])]).Split(',');

                            string itm = Convert.ToString(dr["LAYOUT_ITEM"]);

                            string[] str = itm.Split(',');

                            int i = 1;
                            foreach (string st in str)
                            {
                                item it = new item();
                                it.id = Convert.ToString(dr["DETAILS_ID"]) + i;
                                it.items = st;
                                if (stSelected.Contains(st))
                                    it.isSelected = "true";
                                else
                                    it.isSelected = "false";
                                lstItem.Add(it);
                                i = i + 1;
                            }



                        }

                        

                        oview.item_list = lstItem;
                        omedl2.Add(oview);


                    }

                    odata.view_list = omedl2;


                }
                else
                {
                    odata.status = "205";
                    odata.message = "Some input parameters are missing.";

                }

                var message = Request.CreateResponse(HttpStatusCode.OK, odata);
                return message;
            }
        }

    }
}
