using Newtonsoft.Json.Linq;
using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAPI.Controllers
{
    public class ReimbursementFormController : Controller
    {
        public ActionResult Apply(ApplyReimbursementModel model)
        {

            ApplyReimbursementModelOutput omodel = new ApplyReimbursementModelOutput();
            List<reimbursement_details_Inputstructure> omedl2 = new List<reimbursement_details_Inputstructure>();
            List<reimbursement_details_InputstructureImage> omedlimage = new List<reimbursement_details_InputstructureImage>();
            foreach (var s2 in model.expense_details)
            {

                foreach (var s3 in s2.reimbursement_details)
                {


                    omedl2.Add(new reimbursement_details_Inputstructure()
                    {
                        user_id = model.user_id,
                        date = model.date,
                        visit_type_id = model.visit_type_id,
                        expence_type_id = s2.expence_type_id,
                        expence_type = s2.expence_type,
                        mode_of_travel = s3.mode_of_travel,
                        from_location = s3.from_location,
                        to_location = s3.to_location,
                        amount = s3.amount,
                        total_distance = s3.total_distance,
                        remark = s3.remark,
                        start_date_time = s3.start_date_time,
                        end_date_time = s3.end_date_time,
                        location = s3.location,
                        hotel_name = s3.hotel_name,
                        food_type = s3.food_type,
                        Expense_mapId=model.Expense_mapId,
                        Subexpense_MapId=s3.Subexpense_MapId,
                        fuel_id=s3.fuel_id
                    });
                }

            }


            string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/CommonFolder/Reimbursement/");


            //foreach (var s2 in model.expense_details)
            //{

            //    foreach (var s3 in s2.reimbursement_details)
            //    {
            //        foreach (var file in s3.files)
            //        {
            //            if (file != null && file.ContentLength > 0)
            //            {
            //                string image = Guid.NewGuid() + model.user_id + model.date;
            //                file.SaveAs(Path.Combine(Server.MapPath("~/CommonFolder/Reimbursement"), image + Path.GetExtension(file.FileName)));
            //                omedlimage.Add(new reimbursement_details_InputstructureImage()
            //                {
            //                    user_id = model.user_id,
            //                    date = model.date,
            //                    imagename = image,
            //                    Expense_mapId = model.Expense_mapId,
            //                    Subexpense_MapId = s3.Subexpense_MapId
            //                });

            //            }
            //        }

            //    }

            //}
            string JsonXML_Image = XmlConversion.ConvertToXml(omedlimage, 0);

            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("Proc_FTS_EmployeeConveyanceApply", sqlcon);

            sqlcmd.Parameters.Add("@session_token", model.session_token);
            sqlcmd.Parameters.Add("@user_id", model.user_id);
            sqlcmd.Parameters.Add("@State_ID", model.state_id);
            sqlcmd.Parameters.Add("@date", model.date);
            sqlcmd.Parameters.Add("@JsonXML", JsonXML);
            sqlcmd.Parameters.Add("@JsonXML_Image", JsonXML_Image);


            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            if (dt.Rows.Count > 0)
            {       

                omodel.status = "200";
                omodel.message = "Successfully Submitted .";
            }
            else
            {

                omodel.status = "205";
                omodel.message = "Not Submitted .";
            }

            return Json(omodel);
        }


       public ActionResult ApplyImage(InputstructureImage model)
        {
            string uploadtext = "~/CommonFolder/Log/Image.txt";
            try
            {


                using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                {
                    stream.WriteLine(DateTime.Now.ToString() + model.data.ToString());
                }

                ApplyReimbursementModelOutput omodel = new ApplyReimbursementModelOutput();
                List<reimbursement_details_Inputstructure> omedl2 = new List<reimbursement_details_Inputstructure>();
                List<reimbursement_details_InputstructureImage> omedlimage = new List<reimbursement_details_InputstructureImage>();
                reimbursement_details_InputstructureImage omm = new reimbursement_details_InputstructureImage();




                // JArray a = JArray.Parse(model.data);

                //foreach (JObject o in a.Children<JObject>())
                //{
                //    foreach (JProperty p in o.Properties())
                //    {
                //        string name = p.Name;
                //        string value = (string)p.Value;
                //        Console.WriteLine(name + " -- " + value);
                //    }
                //}
                 var details = JObject.Parse(model.data);

                 foreach (var item in details)
                 {
                     string param = item.Key;
                     string value = Convert.ToString(item.Value);
                     switch (param)
                     {

                         case "subexpenseid":
                             {
                                 omm.Subexpense_MapId = value;
                                 break;
                             }

                         case "user_id":
                             {
                                 omm.user_id = value;
                                 break;
                             }

                         case "date":
                             {
                                 omm.date = value;
                                 break;
                             }


                     }

                 }

                 

                string JsonXML_Image = XmlConversion.ConvertToXml(omedlimage, 0);

                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("Proc_FTS_EmployeeConveyanceApply", sqlcon);


                sqlcmd.Parameters.Add("@JsonXML_Image", JsonXML_Image);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {

                    omodel.status = "200";
                    omodel.message = "Successfully Submitted .";
                }
                else
                {

                    omodel.status = "205";
                    omodel.message = "Not Submitted .";
                }
                return Json(omodel);

            }


           catch(Exception ex)
            {


                using (StreamWriter stream = new FileInfo((Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(uploadtext)))).AppendText())
                {
                    stream.WriteLine(ex.Message.ToString()+" "+DateTime.Now.ToString()+model.data.ToString());
                }

                return Json(null);
            }
     
        }

    }
}