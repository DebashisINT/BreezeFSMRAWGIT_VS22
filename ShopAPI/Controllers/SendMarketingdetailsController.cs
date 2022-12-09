using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml;


namespace ShopAPI.Controllers
{
    public class SendMarketingdetailsController : Controller
    {
        [AcceptVerbs("POST")]
        public JsonResult Marketing(MarketingDetails model)
        {
            MarketingDetailsoutput omodel = new MarketingDetailsoutput();
            try
            {
                string ImageName = "";
                string UploadFileDirectory = "~/CommonFolder/MaterialImage";
                DataJsonCollection jonmarketingInfos = JsonConvert.DeserializeObject<DataJsonCollection>(model.marketing_detail);

                List<Maerialdetails> marketing_detail = new List<Maerialdetails>();
                Maerialdetails ommodel = new Maerialdetails();
                string shop_id = jonmarketingInfos.shop_id;
                string user_id = jonmarketingInfos.user_id;
                List<MarketingDetailImages> modelimage = new List<MarketingDetailImages>();
                string JsonimageXML = "";
                if (!string.IsNullOrEmpty(Convert.ToString(model.material_image)))
                {
                    foreach (HttpPostedFileBase postedFile in model.material_image)
                    {
                        if (postedFile != null)
                        {
                            string fileName = Path.GetFileName(postedFile.FileName);
                            ImageName = shop_id + '_' + fileName;
                            string vPath = Path.Combine(Server.MapPath("~/CommonFolder/MaterialImage"), ImageName);
                            postedFile.SaveAs(vPath);
                            modelimage.Add(new MarketingDetailImages()
                            {
                                material_images = ImageName
                            });
                        }
                    }

                    JsonimageXML = ConvertToXml(modelimage, 0);
                }
                //var details = JObject.Parse(model.marketing_detail);
                foreach (var s in jonmarketingInfos.marketing_detail)
                {
                    ommodel.date = s.date;
                    ommodel.material_id = s.material_id;
                    ommodel.typeid = s.typeid;


                    marketing_detail.Add(new Maerialdetails()
                    {
                        date = ommodel.date,
                        material_id = ommodel.material_id,
                        typeid = ommodel.typeid
                    });

                }

                string JsonXML = ConvertToXml(marketing_detail, 0);


                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();


                sqlcmd = new SqlCommand("Sp_ApiSendMarketingDetails", sqlcon);
                sqlcmd.Parameters.Add("@shop_id", shop_id);
                sqlcmd.Parameters.Add("@user_id", user_id);
                sqlcmd.Parameters.Add("@marketing_json", JsonXML);
                sqlcmd.Parameters.Add("@marketingimages_json", JsonimageXML);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {

                    omodel.status = "200";
                    omodel.message = "success";

                }
                else
                {

                    omodel.status = "200";
                    omodel.message = "success";
                }

                return Json(omodel);
            }
            catch(Exception ex)
            {
                omodel.status = "204";
                omodel.status = ex.Message;
                return Json(omodel);
            }
        }

        public class DataJsonCollection
        {
            public string user_id { get; set; }
            public string shop_id { get; set; }
            public List<Maerialdetails> marketing_detail { get; set; }
        }

        public class Maerialdetails
        {
            public string material_id { get; set; }
            public string date { get; set; }
            public string typeid { get; set; }
        }



        #region ******************************************** Xml Conversion  ********************************************
        public static string ConvertToXml<T>(List<T> table, int metaIndex = 0)
        {
            XmlDocument ChoiceXML = new XmlDocument();
            ChoiceXML.AppendChild(ChoiceXML.CreateElement("root"));
            Type temp = typeof(T);

            foreach (var item in table)
            {
                XmlElement element = ChoiceXML.CreateElement("data");

                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    element.AppendChild(ChoiceXML.CreateElement(pro.Name)).InnerText = Convert.ToString(item.GetType().GetProperty(pro.Name).GetValue(item, null));
                }
                ChoiceXML.DocumentElement.AppendChild(element);
            }

            return ChoiceXML.InnerXml.ToString();
        }
        #endregion


    }
}