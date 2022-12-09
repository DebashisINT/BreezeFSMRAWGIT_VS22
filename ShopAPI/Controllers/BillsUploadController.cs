using ShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace ShopAPI.Controllers
{
    public class BillsUploadController : ApiController
    {

        public HttpResponseMessage Get()
        {
            var content = new MultipartContent();
            var ids = new List<int>() { 1, 2 };

            var objectContent = new ObjectContent<List<int>>(ids, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
            content.Add(objectContent);

            var file1Content = new StreamContent(new FileStream(@"c:\temp\desert.jpg", FileMode.Open));
            file1Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpeg");
            content.Add(file1Content);

            var file2Content = new StreamContent(new FileStream(@"c:\temp\test.txt", FileMode.Open));
            file2Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/plain");
            content.Add(file2Content);

            var response = new HttpResponseMessage();
            response.Content = content;
            return response;
        }

        [HttpPost]
        public HttpResponseMessage Post()
        {
            ReimbursementOutput omodel = new ReimbursementOutput();
            var httpContext = HttpContext.Current;
            ApplyReimbursementModel model = new ApplyReimbursementModel();
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            List<reimbursement_details_InputstructureImage> omedlimage = new List<reimbursement_details_InputstructureImage>();
            string sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/CommonFolder/Reimbursement/");
            string filechk = ""; 
            String FileNamesend = "";
            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    string filename = Path.GetFileName(hpf.FileName);

                    FileNamesend = FileNamesend + " , " + hpf.FileName;
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));


                        model.user_id = Convert.ToString(filename.Split('~')[0]);
                        model.date = Convert.ToString(filename.Split('~')[1]);
                        model.Expense_mapId = Convert.ToString(filename.Split('~')[2]);

                        model.visit_type_id = Convert.ToString(filename.Split('~')[3]);
                        model.Expense_Id = Convert.ToString(filename.Split('~')[4]);


                        omedlimage.Add(new reimbursement_details_InputstructureImage()
                            {
                                user_id = model.user_id,
                                date = model.date,
                                imagename = filename,
                                Expense_mapId = model.Expense_mapId,
                                visit_type_id = model.visit_type_id,
                                Expense_Id = model.Expense_Id
                            });
                    }
                }
            }
            // Return status code  
            //  return Request.CreateResponse(HttpStatusCode.Created);

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
                omodel.message = "Submitted Successfully." + FileNamesend;
            }
            var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
            return message;
        }

        [HttpPost]
        public HttpResponseMessage Apply(ApplyReimbursementModel model)
        {

            ApplyReimbursementModelOutput omodel = new ApplyReimbursementModelOutput();
            List<reimbursement_details_Inputstructure> omedl2 = new List<reimbursement_details_Inputstructure>();
            List<reimbursement_details_InputstructureImage> omedlimage = new List<reimbursement_details_InputstructureImage>();
            string from_loc_id = "";
            string to_loc_id = "";
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
                        Expense_mapId = model.Expense_mapId,
                        Subexpense_MapId = s3.Subexpense_MapId,
                        fuel_id = s3.fuel_id,
                        //Add From_loc_id and to_location id in add Bill Upload Tanmoy 22-11-2019
                        from_loc_id = s3.from_loc_id,
                        to_loc_id = s3.to_loc_id
                        //End Add From_loc_id and to_location id in add Bill Upload Tanmoy 22-11-2019
                    });
                    //Add From_loc_id and to_location id in add Bill Upload Tanmoy 22-11-2019
                    from_loc_id = s3.from_loc_id;
                    to_loc_id = s3.to_loc_id;
                    //End Add From_loc_id and to_location id in add Bill Upload Tanmoy 22-11-2019
                }

            }


            string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

            var httpContext = HttpContext.Current;

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/CommonFolder/Reimbursement/");

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // string filechk = model.user_id + model.date + Guid.NewGuid() + Path.GetFileName(hpf.FileName);

                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        //omedlimage.Add(new reimbursement_details_InputstructureImage()
                        //    {
                        //        user_id = model.user_id,
                        //        date = model.date,
                        //        imagename = filechk,
                        //        Expense_mapId = model.Expense_mapId
                        //    });

                    }
                }
            }



            //foreach (var s2 in model.expense_details)
            //{

            //    foreach (var s3 in s2.reimbursement_details)
            //    {
            //        foreach (var file in s3.files)
            //        {
            //            if (file != null && file.ContentLength > 0)
            //            {
            //                string image = Guid.NewGuid() + model.user_id + model.date;
            //                file.SaveAs(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/CommonFolder/Reimbursement"), image + Path.GetExtension(file.FileName)));
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
            // string JsonXML_Image = XmlConversion.ConvertToXml(omedlimage, 0);

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
            sqlcmd.Parameters.Add("@MapExpenseID", model.Expense_mapId);
            sqlcmd.Parameters.Add("@FROMSHOPID", from_loc_id);
            sqlcmd.Parameters.Add("@TOSHOPID", to_loc_id);

            // sqlcmd.Parameters.Add("@JsonXML_Image", JsonXML_Image);

            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["data"].ToString()=="1")
                { 
                omodel.status = "200";
                omodel.message = "Successfully Submitted .";
                }
                else
                {
                    omodel.status = "206";
                    omodel.message = "Reimbursement Found in Between Two Location .";
                }
            }
            else
            {

                omodel.status = "205";
                omodel.message = "Not Submitted .";
            }

            var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
            return message;
        }


        public HttpResponseMessage ConveyanceList(reimbursement_details_Listing_Input model)
        {

            reimbursement_details_Listing oview = new reimbursement_details_Listing();
            String weburlReimbesement = System.Configuration.ConfigurationSettings.AppSettings["Reimbersement"];


            try
            {
                if (!ModelState.IsValid)
                {
                    oview.status = "213";
                    oview.message = "Some input parameters are missing.";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, oview);
                }
                else
                {
                    String token = System.Configuration.ConfigurationSettings.AppSettings["AuthToken"];
                    String profileImg = System.Configuration.ConfigurationSettings.AppSettings["ProfileImageURL"];



                    DataSet dt = new DataSet();
                    String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                    SqlCommand sqlcmd = new SqlCommand();

                    SqlConnection sqlcon = new SqlConnection(con);
                    sqlcon.Open();


                    sqlcmd = new SqlCommand("Proc_FTS_ConveyanceConfigurationListing", sqlcon);

                    sqlcmd.Parameters.Add("@user_id", model.user_id);
                    sqlcmd.Parameters.Add("@month", model.month);
                    sqlcmd.Parameters.Add("@year", model.year);
                    sqlcmd.Parameters.Add("@visit_type", model.visit_type);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                    da.Fill(dt);
                    sqlcon.Close();




                    if (dt.Tables[0].Rows.Count > 0)
                    {

                        //  oview.visit_type = "";

                        List<reimbursement_details_listing_Expense> onview = new List<reimbursement_details_listing_Expense>();

                        for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
                        {


                            List<expense_list_details_listing> expensedetails = new List<expense_list_details_listing>();

                            for (int j = 0; j < dt.Tables[2].Rows.Count; j++)
                            {

                                List<expense_list_details_listing_Images> objimage = new List<expense_list_details_listing_Images>();
                                int i1 = 0;
                                if (Convert.ToString(dt.Tables[2].Rows[j]["expense_type_id"]) == Convert.ToString(dt.Tables[1].Rows[i]["expense_type_id"]))
                                {


                                    if (dt.Tables[3].Rows.Count > 0)
                                    {

                                        for (int l = 0; l < dt.Tables[3].Rows.Count; l++)
                                        {

                                            if (Convert.ToString(dt.Tables[2].Rows[j]["MapExpenseID"]) == Convert.ToString(dt.Tables[3].Rows[l]["MapExpenseID"])
                                                )
                                            //&& Convert.ToString(dt.Tables[2].Rows[j]["visit_type_id"]) == Convert.ToString(dt.Tables[3].Rows[l]["VisitlocId"])
                                            //&& Convert.ToString(dt.Tables[2].Rows[j]["expense_type_id"]) == Convert.ToString(dt.Tables[3].Rows[l]["ExpenseID"])

                                            //)
                                            {

                                                objimage.Add(new expense_list_details_listing_Images()
                                                    {
                                                        links = weburlReimbesement + Convert.ToString(dt.Tables[3].Rows[l]["Bills"]),
                                                        id = Convert.ToString(dt.Tables[3].Rows[l]["ApplictnimageID"])
                                                    });

                                            }


                                        }
                                    }


                                    expensedetails.Add(new expense_list_details_listing()
                                    {

                                        applied_date = Convert.ToDateTime(dt.Tables[2].Rows[j]["Date"]).ToString("yyyy-MM-dd"),
                                        travel_mode_id = Convert.ToString(dt.Tables[2].Rows[j]["Mode_of_travel"]),
                                        visit_type = Convert.ToString(dt.Tables[2].Rows[j]["visit_type"]),
                                        visit_type_id = Convert.ToString(dt.Tables[2].Rows[j]["visit_type_id"]),
                                        travel_mode = Convert.ToString(dt.Tables[2].Rows[j]["travel_mode"]),
                                        amount = Convert.ToString(dt.Tables[2].Rows[j]["Amount"]),
                                        approved_amount = Convert.ToString(dt.Tables[2].Rows[j]["approved_amount"]),
                                        hotel_name = Convert.ToString(dt.Tables[2].Rows[j]["Hotel_name"]),
                                        food_type = Convert.ToString(dt.Tables[2].Rows[j]["Food_type"]),
                                        remarks = Convert.ToString(dt.Tables[2].Rows[j]["Remark"]),
                                        from_location = Convert.ToString(dt.Tables[2].Rows[j]["From_location"]),
                                        to_location = Convert.ToString(dt.Tables[2].Rows[j]["To_location"]),
                                        hotel_location = Convert.ToString(dt.Tables[2].Rows[j]["Location"]),
                                        start_date_time = Convert.ToString(dt.Tables[2].Rows[j]["Start_date_time"]) != "" ? Convert.ToDateTime(dt.Tables[2].Rows[j]["Start_date_time"]).ToString("yyyy-MM-dd HH:mm:ss") : "",
                                        end_date_time = Convert.ToString(dt.Tables[2].Rows[j]["End_date_time"]) != "" ? Convert.ToDateTime(dt.Tables[2].Rows[j]["End_date_time"]).ToString("yyyy-MM-dd HH:mm:ss") : "",
                                        distance = Convert.ToString(dt.Tables[2].Rows[j]["Total_distance"]),
                                        fuel_id = Convert.ToString(dt.Tables[2].Rows[j]["Fuel_typeId"]),
                                        fuel_type = Convert.ToString(dt.Tables[2].Rows[j]["FuelType"]),
                                        maximum_rate = Convert.ToString(dt.Tables[2].Rows[j]["maximum_rate"]),
                                        maximum_allowance = Convert.ToString(dt.Tables[2].Rows[j]["maximum_allowance"]),
                                        maximum_distance = Convert.ToString(dt.Tables[2].Rows[j]["maximum_distance"]),
                                        status = Convert.ToString(dt.Tables[2].Rows[j]["status"]),
                                        Expense_mapId = Convert.ToString(dt.Tables[2].Rows[j]["MapExpenseID"]),
                                        Subexpense_MapId = Convert.ToString(dt.Tables[2].Rows[j]["SubExpenseID"]),
                                        isEditable = Convert.ToString(dt.Tables[2].Rows[j]["isEditable"]),
                                        image_list = objimage
                                    });



                                }


                            }

                            onview.Add(new reimbursement_details_listing_Expense()
                            {
                                expense_list_details = expensedetails,
                                expense_type_id = Convert.ToString(dt.Tables[1].Rows[i]["expense_type_id"]),
                                expense_type = Convert.ToString(dt.Tables[1].Rows[i]["expense_type"]),
                                total_amount = Convert.ToString(dt.Tables[1].Rows[i]["total_amount"])
                            });
                        }

                        oview.status = "200";
                        oview.message = "List populated.";
                        oview.total_claim_amount = Convert.ToString(dt.Tables[0].Rows[0]["total_claim_amount"]);
                        oview.total_approved_amount = Convert.ToString(dt.Tables[0].Rows[0]["total_approved_amount"]);
                        //oview.visit_type_id = Convert.ToString(dt.Tables[0].Rows[0]["visit_type_id"]);
                        //oview.visit_type = Convert.ToString(dt.Tables[0].Rows[0]["visit_type"]);
                        oview.expense_list = onview;
                    }
                    else
                    {
                        oview.status = "205";
                        oview.message = "No Data Found.";

                    }

                    var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                    return message;
                }

            }
            catch (Exception ex)
            {
                oview.status = "209";
                oview.message = ex.Message;
                var message = Request.CreateResponse(HttpStatusCode.OK, oview);
                return message;
            }

        }

        [HttpPost]
        public HttpResponseMessage DeleteReimbersment(DeleteReimbursement model)
        {

            string sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/CommonFolder/Reimbursement/");
            ReimbursementOutput omodel = new ReimbursementOutput();
            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("Proc_FTS_EmployeeConveyanceDelete", sqlcon);

            //sqlcmd.Parameters.Add("@session_token", model.session_token);
            sqlcmd.Parameters.Add("@user_id", model.user_id);
            sqlcmd.Parameters.Add("@visit_type_id", model.visit_type_id);
            sqlcmd.Parameters.Add("@date", model.date);
            sqlcmd.Parameters.Add("@Expense_mapId", model.Expense_mapId);
            sqlcmd.Parameters.Add("@Subexpense_MapId", model.Subexpense_MapId);


            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (File.Exists(sPath + Convert.ToString(dt.Rows[0]["Bills"])))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        File.Delete(sPath + Convert.ToString(dt.Rows[0]["Bills"]));
                    }

                }
                omodel.status = "200";
                omodel.message = "Deleted successfully";
            }
            var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
            return message;

        }



        [HttpPost]
        public HttpResponseMessage EDIT(ApplyReimbursementModel model)
        {

            ApplyReimbursementModelOutput omodel = new ApplyReimbursementModelOutput();
            List<reimbursement_details_Inputstructure> omedl2 = new List<reimbursement_details_Inputstructure>();
            List<reimbursement_details_InputstructureImage> omedlimage = new List<reimbursement_details_InputstructureImage>();
            string from_loc_id = "";
            string to_loc_id = "";
            String expence_type_ids = "";
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
                        Expense_mapId = model.Expense_mapId,
                        Subexpense_MapId = s3.Subexpense_MapId,
                        fuel_id = s3.fuel_id,
                        //Add From_loc_id and to_location id in add Bill Upload Tanmoy 22-11-2019
                        from_loc_id = s3.from_loc_id,
                        to_loc_id = s3.to_loc_id
                        //End Add From_loc_id and to_location id in add Bill Upload Tanmoy 22-11-2019
                    });
                    //Add From_loc_id and to_location id in add Bill Upload Tanmoy 22-11-2019
                    from_loc_id = s3.from_loc_id;
                    to_loc_id = s3.to_loc_id;
                    expence_type_ids = s2.expence_type_id;
                    //End Add From_loc_id and to_location id in add Bill Upload Tanmoy 22-11-2019
                }

            }


            string JsonXML = XmlConversion.ConvertToXml(omedl2, 0);

            var httpContext = HttpContext.Current;

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/CommonFolder/Reimbursement/");


            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("Proc_FTS_EmployeeConveyanceEdit", sqlcon);

            sqlcmd.Parameters.Add("@session_token", model.session_token);
            sqlcmd.Parameters.Add("@user_id", model.user_id);
            sqlcmd.Parameters.Add("@State_ID", model.state_id);
            sqlcmd.Parameters.Add("@date", model.date);
            sqlcmd.Parameters.Add("@JsonXML", JsonXML);
            sqlcmd.Parameters.Add("@MapExpenseID", model.Expense_mapId);
            sqlcmd.Parameters.Add("@FROMSHOPID", from_loc_id);
            sqlcmd.Parameters.Add("@TOSHOPID", to_loc_id);
            sqlcmd.Parameters.Add("@expence_type_ids", expence_type_ids);
            // sqlcmd.Parameters.Add("@JsonXML_Image", JsonXML_Image);

            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["data"].ToString()=="1")
                { 
                omodel.status = "200";
                omodel.message = "Successfully Submitted .";
                }
                else
                {
                    omodel.status = "206";
                    omodel.message = "Reimbursement Not Found in Between Two Location .";
                }
            }
            else
            {

                omodel.status = "205";
                omodel.message = "Not Submitted .";
            }

            var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
            return message;
        }


        [HttpPost]
        public HttpResponseMessage DeleteReimbersmentImage(DeleteReimbursementImage model)
        {

            string sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/CommonFolder/Reimbursement/");
            ReimbursementOutput omodel = new ReimbursementOutput();
            DataTable dt = new DataTable();
            String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            sqlcmd = new SqlCommand("Proc_FTS_EmployeeConveyanceImageDelete", sqlcon);

            sqlcmd.Parameters.Add("@user_id", model.user_id);
            sqlcmd.Parameters.Add("@ImageID", model.id);


            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dt);
            sqlcon.Close();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (File.Exists(sPath + Convert.ToString(dt.Rows[0]["Bills"])))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        File.Delete(sPath + Convert.ToString(dt.Rows[0]["Bills"]));
                    }

                }
                omodel.status = "200";
                omodel.message = "Deleted successfully";
            }

            else
            {
                omodel.status = "205";
                omodel.message = "No Record to Delete";

            }
            var message = Request.CreateResponse(HttpStatusCode.OK, omodel);
            return message;

        }



    }



}



