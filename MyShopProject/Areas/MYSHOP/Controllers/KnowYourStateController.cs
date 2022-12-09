using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BusinessLogicLayer;
using System.Configuration;
using System.Data.SqlClient;
using DataAccessLayer;



namespace MyShop.Areas.MYSHOP.Controllers
{
    public class KnowYourStateController : Controller
    {
        //
        // GET: /MYSHOP/KnowYourState/
        public ActionResult KnowYourStateIndex()
        {
            return View("~/Areas/MYSHOP/Views/KnowYourState/KnowYourStateIndex.cshtml");
        }
        public ActionResult GetEmployeesListTemplateByStateDesignation()
        {
            string Employee = "";
            int i = 1;
            //string[] words = Employees.Split(',');

            //foreach (string item in words)
            //{
            //    if (i > 1)
            //        Employee = Employee + "," + item;
            //    else
            //        Employee = item;
            //    i++;
            //}

            string state = "";
            int k = 1;

            DataTable dt = new DataTable();

            try
            {
                DBEngine odbengine = new DBEngine();
                dt = odbengine.GetDataTable("select * from tbl_KnowYourStateDetails");
              //  DataTable dt = odbengine.GetDataTable("select [Coloumn Name] from tbl_KnowYourStateDetails_Settings Where IsActive=1");
               //dt = objemployee.GetReVisitList(Employee, month, Year, State, Designation);
                //string Employee, string MONTH, string YEAR, string stateID, string DESIGNID
                foreach (DataRow row in dt.Rows)
                {
                    row.Delete();
                }
                dt.AcceptChanges();
                if (dt.Columns.Contains("Created_date"))
                {
                    dt.Columns.Remove("Created_date");
                }
                if (dt.Columns.Contains("Created_By"))
                {
                    dt.Columns.Remove("Created_By");
                }
                if (dt.Columns.Contains("Month"))
                {
                    dt.Columns.Remove("Month");
                }
                if (dt.Columns.Contains("Year"))
                {
                    dt.Columns.Remove("Year");
                }
                if (dt.Columns.Contains("Imported By"))
                {
                    dt.Columns.Remove("Imported By");
                }
                  if (dt.Columns.Contains("Imported Date"))
                {
                    dt.Columns.Remove("Imported Date");
                }
                DataTable dtactivestatus = odbengine.GetDataTable("select [Coloumn Name] from tbl_KnowYourStateDetails_Settings Where IsActive=0");
                if (dtactivestatus.Rows.Count > 0)
                {
                    for (int j = 0; j < dtactivestatus.Rows.Count; j++)
                    {
                        dt.Columns.Remove(dtactivestatus.Rows[j]["Coloumn Name"].ToString());
                    }
                }

            }
            catch
            {
            }
         
          
            //return GridViewExtension.ExportToXlsx(GetEmployeesListTemplateByStateDesignationExcel(dt, monthName), dt);
            return GridViewExtension.ExportToXlsx(GetEmployeesListTemplateByStateDesignationExcel(dt), dt);
        }
        private GridViewSettings GetEmployeesListTemplateByStateDesignationExcel(object datatable)
        {
            var settings = new GridViewSettings();
            //settings.Name = "EmployeesRevisit_" + monthName;
            settings.Name = "KnowYourState" + DateTime.Now;
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            //settings.SettingsExport.FileName = "EmployeesRevisit_" + monthName;
            settings.SettingsExport.FileName = "KnowYourState" + DateTime.Now;
            //String ID = Convert.ToString(TempData["EmployeesTargetListDataTable"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;



            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                //if (datacolumn.ColumnName == "FortheMonth" || datacolumn.ColumnName == "Enter_Date_for_Revisit" || datacolumn.ColumnName == "Employee_State"
                //    || datacolumn.ColumnName == "Login_IDMobile" || datacolumn.ColumnName == "Employee_Name" || datacolumn.ColumnName == "Area_PIN" || datacolumn.ColumnName == "Shop_ID"
                //    || datacolumn.ColumnName == "Assigned_ShopName" || datacolumn.ColumnName == "Contact_No" || datacolumn.ColumnName == "Address" || datacolumn.ColumnName == "State"
                //    || datacolumn.ColumnName == "Type")
               // {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "State")
                        {
                            column.Caption = "State";

                        }
                        else if (datacolumn.ColumnName == "STATE HEAD")
                        {
                            column.Caption = "STATE HEAD";

                        }
                        else if (datacolumn.ColumnName == "EMP ID")
                        {
                            column.Caption = "EMP ID";
                        }
                        else if (datacolumn.ColumnName == "Emp id1")
                        {
                            column.Caption = "Emp id1";
                        }
                        else if (datacolumn.ColumnName == "Emp id2")
                        {
                            column.Caption = "Emp id2";
                        }
                        else if (datacolumn.ColumnName == "No# Of Outlet")
                        {
                            column.Caption = "No# Of Outlet";
                        }
                        else if (datacolumn.ColumnName == "Average Visit")
                        {
                            column.Caption = "Average Visit";
                        }
                        else if (datacolumn.ColumnName == "Order (Inc# Tax)")
                        {
                            column.Caption = "Order (Inc# Tax)";
                        }
                        else if (datacolumn.ColumnName == "Delivery (Inc# Tax)")
                        {
                            column.Caption = "Delivery (Inc# Tax)";
                        }
                        else if (datacolumn.ColumnName == "Pre Order (Ex# Tax)")
                        {
                            column.Caption = "Pre Order (Ex# Tax)";
                        }

                        else if (datacolumn.ColumnName == "Pre Delivery (Ex# Tax)")
                        {
                            column.Caption = "Pre Delivery (Ex# Tax)";
                        }
                        else if (datacolumn.ColumnName == "Delivery Pending (Ex# Tax)")
                        {
                            column.Caption = "Delivery Pending (Ex# Tax)";
                        }
                        else if (datacolumn.ColumnName == "Delivery Pending (Ex# Tax)")
                        {
                            column.Caption = "Delivery Pending (Ex# Tax)";
                        }
                        else if (datacolumn.ColumnName == "No Of Outlet Order Taken")
                        {
                            column.Caption = "No Of Outlet Order Taken";
                        }
                        else if (datacolumn.ColumnName == "No Of Outlet Delivery Done")
                        {
                            column.Caption = "No Of Outlet Delivery Done";
                        }
                        else if (datacolumn.ColumnName == "Repeat Outlet Delivery Done")
                        {
                            column.Caption = "Repeat Outlet Delivery Done";
                        }
                      else if (datacolumn.ColumnName == "Durantion Spend In Outlet - 0 Min to 5 Min")
                        {
                            column.Caption = "Durantion Spend In Outlet - 0 Min to 5 Min";
                        }
                        else if (datacolumn.ColumnName == "Durantion Spend In Outlet - 6 Min To 15 Min")
                        {
                            column.Caption = "Durantion Spend In Outlet - 6 Min To 15 Min";
                        }
                        else if (datacolumn.ColumnName == "Durantion Spend In Outlet - 16 Min Above")
                        {
                            column.Caption = "Durantion Spend In Outlet - 16 Min Above";
                        }
                        else if (datacolumn.ColumnName == "Batten Others Watts")
                        {
                            column.Caption = "Batten Others Watts";
                        }
                        else if (datacolumn.ColumnName == "Batten-18W")
                        {
                            column.Caption = "Batten-18W";
                        }
                      else if (datacolumn.ColumnName == "Bulb -9W")
                        {
                            column.Caption = "Bulb -9W";
                        }
                        else if (datacolumn.ColumnName == "Bulb Other Watts")
                        {
                            column.Caption = "Bulb Other Watts";
                        }
                        else if (datacolumn.ColumnName == "Bulb Tri-Colour")
                        {
                            column.Caption = "Bulb Tri-Colour";
                        }
                        else if (datacolumn.ColumnName == "Cabinet")
                        {
                            column.Caption = "Cabinet";
                        }
                        else if (datacolumn.ColumnName == "Candle")
                        {
                            column.Caption = "Candle";
                        }
                        else if (datacolumn.ColumnName == "Cob")
                        {
                            column.Caption = "Cob";
                        }
                        else if (datacolumn.ColumnName == "Downlight")
                        {
                            column.Caption = "Downlight";
                        }
                        else if (datacolumn.ColumnName == "Driver")
                        {
                            column.Caption = "Driver";
                        }
                        else if (datacolumn.ColumnName == "Emergency-Bulb")
                        {
                            column.Caption = "Emergency-Bulb";
                        }
                        else if (datacolumn.ColumnName == "Flood Light")
                        {
                            column.Caption = "Flood Light";
                        }
                      else if (datacolumn.ColumnName == "Night Bulb")
                        {
                            column.Caption = "Night Bulb";
                        }
                        else if (datacolumn.ColumnName == "Ring")
                        {
                            column.Caption = "Ring";
                        }
                        else if (datacolumn.ColumnName == "Slim Panel")
                        {
                            column.Caption = "Slim Panel";
                        }
                        else if (datacolumn.ColumnName == "Spot Light")
                        {
                            column.Caption = "Spot Light";
                        }
                        else if (datacolumn.ColumnName == "Street Light")
                        {
                            column.Caption = "Street Light";
                        }
                        else if (datacolumn.ColumnName == "Strip Light (Nova Strip)")
                        {
                            column.Caption = "Strip Light (Nova Strip)";
                        }
                        else if (datacolumn.ColumnName == "Track Light")
                        {
                            column.Caption = "Track Light";
                        }
                        else if (datacolumn.ColumnName == "Zoom Light")
                        {
                            column.Caption = "Zoom Light";
                        }
                        else if (datacolumn.ColumnName == "Vacant-1")
                        {
                            column.Caption = "Vacant-1";
                        }
                        else if (datacolumn.ColumnName == "Vacant-2")
                        {
                            column.Caption = "Vacant-2";
                        }
                        else if (datacolumn.ColumnName == "Vacant-3")
                        {
                            column.Caption = "Vacant-3";
                        }
                        else if (datacolumn.ColumnName == "Vacant-4")
                        {
                            column.Caption = "Vacant-4";
                        }
                        else if (datacolumn.ColumnName == "Vacant-5")
                        {
                            column.Caption = "Vacant-5";
                        }
                        else if (datacolumn.ColumnName == "Total Qty")
                        {
                            column.Caption = "Total Qty";
                        }
                        column.FieldName = datacolumn.ColumnName;
                        //Rev Rajdip
                        //if (datacolumn.DataType.FullName == "System.Decimal")
                        //{
                        //    column.PropertiesEdit.DisplayFormatString = "0.00";
                        //}

                        //if (datacolumn.DataType.FullName == "System.DateTime")
                        //{
                        //    column.PropertiesEdit.DisplayFormatString = "DD-MM-YYYY";
                        //}
                        //end Rev rajdip
                        //DataColumn colTimeSpan = new DataColumn("Enter_Date_for_Revisit");
                        //colTimeSpan.DataType = System.Type.GetType("System.String");
                        // column.ColumnType=typeof(string);

                    });
               // }

            }


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

   
        [HttpPost]
        public ActionResult GetUploadFilesEmployeeSettings(string month, string Year,string flag)
        {
            TempData["EmployeesTargetSettingImportLog"] = null;
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        String extension = Path.GetExtension(fname);
                        fname = DateTime.Now.Ticks.ToString() + extension;
                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/KnowMyStateFileBackUp/"), fname);

                        file.SaveAs(fname);
                        //Rev rajdip
                      //  Import_To_Grid(fname, extension, file);
                     //SqlConnection con =new SqlConnection(ConfigurationManager.AppSettings["DBConnectionDefault"]);
                     //SqlCommand com = new SqlCommand("DELETE FROM tbl_KnowYourStateDetails WHERE MONTH=@Month AND YEAR=@Year",con);
                     // com.Parameters.AddWithValue("@Month",month);
                     // com.Parameters.AddWithValue("@Year", Year);
                     // com.ExecuteNonQuery();
                        string excelPath = Server.MapPath("~/KnowMyStateFileBackUp/") + Path.GetFileName(file.FileName);
                        file.SaveAs(excelPath);
                         using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["DBConnectionDefault"]))
                          {
                            connection.Open();
                            SqlCommand command = connection.CreateCommand();
                            SqlTransaction transaction;
                            transaction = connection.BeginTransaction("SampleTransaction");
                            command.Connection = connection;
                            command.Transaction = transaction;
                            try
                            {
                                command.CommandText = "DELETE FROM tbl_KnowYourStateDetails WHERE MONTH=@Month AND YEAR=@Year";
                                command.Parameters.AddWithValue("@Month", month);
                                command.Parameters.AddWithValue("@Year", Year);
                                command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    transaction.Rollback();
                                }
                                catch (Exception ex2)
                                {

                                }
                            }
                        }
                        string conString = string.Empty;
                        extension = Path.GetExtension(file.FileName);
                        switch (extension)
                        {
                            case ".xls": 
                                conString = ConfigurationManager.AppSettings["DBConnectionDefault"];
                                break;
                            case ".xlsx": 
                                conString = ConfigurationManager.AppSettings["DBConnectionDefault"];
                                break;

                        }
                        conString = ConfigurationManager.AppSettings["ExcelConString"];
                        conString = string.Format(conString, excelPath);
                        using (OleDbConnection excel_con = new OleDbConnection(conString))
                        {
                            excel_con.Open();
                            string sheet1 ="Sheet1$"; //excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[1]["TABLE_NAME"].ToString();
                            DataTable dtExcelData = new DataTable();
                            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                            DBEngine odbengine = new DBEngine();
                            DataTable dtactivestatus = odbengine.GetDataTable("select [Coloumn Name] from tbl_KnowYourStateDetails_Settings Where IsActive=1");  //Where IsActive=1
                            if (dtactivestatus.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtactivestatus.Rows.Count; j++)
                                {
                                    dtExcelData.Columns.Add(dtactivestatus.Rows[j]["Coloumn Name"].ToString(), typeof(string));                   
                                }
                            }
                          
                             using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                            {
                                oda.Fill(dtExcelData);
                                //dtExcelData.Columns.Add("Created_date", typeof(DateTime));
                                if (dtExcelData.Columns.Contains("Imported By"))
                                {
                                    dtExcelData.Columns.Remove("Imported By");
                                }
                                 if (dtExcelData.Columns.Contains("Created_By"))
                                {
                                    dtExcelData.Columns.Remove("Created_By");
                                }
                                 if (dtExcelData.Columns.Contains("Created_date"))
                                 {
                                     dtExcelData.Columns.Remove("Created_date");
                                 }
                                 if (dtExcelData.Columns.Contains("Imported Date"))
                                 {
                                     dtExcelData.Columns.Remove("Imported Date");
                                 }
                                dtExcelData.Columns.Add("Imported By", typeof(string));
                                dtExcelData.Columns.Add("Imported Date", typeof(string));
                                //dtExcelData.Columns.Add("Month", typeof(int));
                                //dtExcelData.Columns.Add("Year", typeof(int));
                                foreach (DataRow row in dtExcelData.Rows)
                                {
                                    row["Imported Date"] = DateTime.Now;
                                    row["Imported By"] = Session["userid"].ToString();
                                    row["Month"] = month;
                                    row["Year"] = Year;
                                }
                            }
                            excel_con.Close();


                            #region Create New Column

                            DataTable dtColumns = odbengine.GetDataTable("select [Coloumn Name] C_Name,[Coloumn Type] C_type from tbl_KnowYourStateDetails_Settings");
                            DataTable dtTableColumns = odbengine.GetDataTable("SELECT [COLUMN_NAME] C_Name FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'tbl_KnowYourStateDetails'");


                            foreach (DataRow dr in dtColumns.Rows)
                            {
                                DataView dv = new System.Data.DataView(dtTableColumns);
                                dv.RowFilter = "C_Name='" + Convert.ToString(dr["C_Name"]) + "'";
                                DataTable dt = dv.ToTable();

                                if (dt != null && dt.Rows.Count > 0)
                                {

                                }
                                else
                                {
                                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["DBConnectionDefault"]))
                                    {
                                        connection.Open();
                                        SqlCommand command = connection.CreateCommand();
                                        SqlTransaction transaction;
                                        transaction = connection.BeginTransaction("SampleTransaction");
                                        command.Connection = connection;
                                        command.Transaction = transaction;
                                        try
                                        {
                                            command.CommandText = "Alter Table tbl_KnowYourStateDetails Add [" + Convert.ToString(dr["C_Name"]) + "] " + Convert.ToString(dr["C_type"]) + " ";
                                            command.ExecuteNonQuery();
                                            transaction.Commit();
                                        }
                                        catch (Exception ex)
                                        {
                                            try
                                            {
                                                transaction.Rollback();
                                            }
                                            catch (Exception ex2)
                                            {

                                            }
                                        }
                                    }

                                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["DBConnectionDefault"]))
                                    {
                                        connection.Open();
                                        SqlCommand command = connection.CreateCommand();
                                        SqlTransaction transaction;
                                        transaction = connection.BeginTransaction("SampleTransaction");
                                        command.Connection = connection;
                                        command.Transaction = transaction;
                                        try
                                        {
                                            command.CommandText = "Alter Table tbl_KnowYourStateDetailsLog  Add [" + Convert.ToString(dr["C_Name"]) + "] " + Convert.ToString(dr["C_type"]) + " ";
                                            command.ExecuteNonQuery();
                                            transaction.Commit();
                                        }
                                        catch (Exception ex4)
                                        {
                                            try
                                            {
                                                transaction.Rollback();
                                            }
                                            catch (Exception ex5)
                                            {

                                            }
                                        }
                                    }
                                 }

                            }


                            #endregion
                            DataTable dtCloned = dtExcelData.Clone();

                            dtColumns = odbengine.GetDataTable("select [Coloumn Name] C_Name,[Coloumn Type] C_type from tbl_KnowYourStateDetails_Settings");
                            dtTableColumns = odbengine.GetDataTable("SELECT [COLUMN_NAME] C_Name FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'tbl_KnowYourStateDetails'");

                            foreach (DataRow dr in dtColumns.Rows)
                            {
                                DataView dv = new System.Data.DataView(dtTableColumns);
                                dv.RowFilter = "C_Name='" + Convert.ToString(dr["C_Name"]) + "'";
                                DataTable dt = dv.ToTable();

                                if (dt != null && dt.Rows.Count > 0 && Convert.ToString(dr["C_type"]).Contains("numeric"))
                                {
                                    
                                    dtCloned.Columns[Convert.ToString(dr["C_Name"])].DataType = typeof(Decimal);
                                    
                                }
                            }

                            foreach (DataRow row in dtExcelData.Rows)
                            {
                                dtCloned.ImportRow(row);
                            }


                            string consString = ConfigurationManager.AppSettings["DBConnectionDefault"];
                            using (SqlConnection con = new SqlConnection(consString))
                            {
                                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                                {
                                    sqlBulkCopy.DestinationTableName = "dbo.tbl_KnowYourStateDetails";

                                    if (dtactivestatus.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < dtactivestatus.Rows.Count; j++)
                                        {
                                            sqlBulkCopy.ColumnMappings.Add(dtactivestatus.Rows[j]["Coloumn Name"].ToString(), dtactivestatus.Rows[j]["Coloumn Name"].ToString());
                                        }
                                    }
                           
                                    con.Open();
                                    sqlBulkCopy.WriteToServer(dtCloned);
                                    
                                    con.Close();
                                }

                                
                            }


                            





                            using (SqlConnection con = new SqlConnection(consString))
                            {
                                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                                {
                                    //Set the database table name
                                    sqlBulkCopy.DestinationTableName = "dbo.tbl_KnowYourStateDetailsLog";
                                    if (dtactivestatus.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < dtactivestatus.Rows.Count; j++)
                                        {
                                            sqlBulkCopy.ColumnMappings.Add(dtactivestatus.Rows[j]["Coloumn Name"].ToString(), dtactivestatus.Rows[j]["Coloumn Name"].ToString());
                                        }
                                    }
                                 
                                    con.Open();
                                    sqlBulkCopy.WriteToServer(dtExcelData);
                                    con.Close();
                                }
                            }
                         }
                         //End Rev rajdip
                        //ReadExcel(fname, extension, file);
                    }
                    // Returns message that successfully uploaded  
               return Json("Import Process Completed!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public ActionResult GetImportTargetList(KnowYourStateModel model, string Month, string Year, string flag)
        {   
            
            List<ItemImport> list = new List<ItemImport>();
            DataTable dtsm = null;
            //if (model.SettingMonth > 0 && model.SettingYear > 0)
            //{
            DBEngine odbengine = new DBEngine();
            //dtsm = odbengine.GetDataTable("select SlNo=ROW_NUMBER() OVER (ORDER BY State),* from dbo.tbl_KnowYourStateDetails where Month='" + Month + "' and Year='" + Year + "'");           
                string consString = ConfigurationManager.AppSettings["DBConnectionDefault"];
                //SqlConnection con = new SqlConnection(consString);
                //SqlCommand cmd = new SqlCommand("EXEC proc_ColoumnSettingForReport", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //adp.Fill(dtsm);
                //adp.SelectCommand = cmd;
                //return DS.Tables[0];
                if (Month == "" || Month == null)
                {
                    Month ="0";
                }
                if (Year == "" || Year == null)
                {
                    Year = "0";
                }
                ProcedureExecute proc = new ProcedureExecute("proc_ColoumnSettingForReport");
                proc.AddPara("@Month", Month);
                proc.AddPara("@Year", Year);
                dtsm = proc.GetTable();
                //return ds;
                try
                {
                    if (dtsm != null && dtsm.Rows.Count > 0)
                    {
                        ItemImport data = new ItemImport();
                    //    foreach (DataRow row in dtsm.Rows)
                    //    {                           
                    //        data.State = Convert.ToString(row["State"]);
                    //        data.STATE_HEAD = Convert.ToString(row["STATE HEAD"]);
                    //        data.EMP_ID = Convert.ToString(row["EMP ID"]);
                    //        data.SUPERVISOR = Convert.ToString(row["SUPERVISOR"]);
                    //        data.Emp_id1 = Convert.ToString(row["Emp id1"]);
                    //        data.Name = Convert.ToString(row["Name"]);
                    //        data.Emp_id2 = Convert.ToString(row["Emp id2"]);
                    //        data.No_Of_Outlet = Convert.ToString(row["No# Of Outlet"]);
                    //        data.Total_Visit = Convert.ToString(row["Total Visit"]);
                    //        data.Average_Visit = Convert.ToString(row["Average Visit"]);
                    //        data.Order_Inc_Tax = Convert.ToString(row["Order (Inc# Tax)"]);
                    //        data.Delivery_Inc_Tax = Convert.ToString(row["Delivery (Inc# Tax)"]);
                    //        data.Pre_Order_Ex_Tax = Convert.ToString(row["Pre Order (Ex# Tax)"]);
                    //        data.Pre_Delivery_Ex_Tax = Convert.ToString(row["Pre Delivery (Ex# Tax)"]);
                    //        data.Delivery_Pending_Ex_Tax = Convert.ToString(row["Delivery Pending (Ex# Tax)"]);
                    //        data.No_Of_Outlet_Order_Taken = Convert.ToString(row["No Of Outlet Order Taken"]);
                    //        data.No_Of_Outlet_Delivery_Done = Convert.ToString(row["No Of Outlet Delivery Done"]);
                    //        data.Repeat_Outlet_Delivery_Done = Convert.ToString(row["Repeat Outlet Delivery Done"]);
                    //        data.Durantion_Spend_In_Outlet_Min_to_5_Min = Convert.ToString(row["Durantion Spend In Outlet - 0 Min to 5 Min"]);
                    //        data.Durantion_Spend_In_Outlet_Min_To_15_Min = Convert.ToString(row["Durantion Spend In Outlet - 6 Min To 15 Min"]);
                    //        data.Durantion_Spend_In_Outlet_16_Min_Above = Convert.ToString(row["Durantion Spend In Outlet - 16 Min Above"]);
                    //        data.Batten_Others_Watts = Convert.ToString(row["Batten Others Watts"]);
                    //        data.Batten_18W = Convert.ToString(row["Batten-18W"]);
                    //        data.Bulb_9W = Convert.ToString(row["Bulb -9W"]);
                    //        data.Bulb_Other_Watts = Convert.ToString(row["Bulb Other Watts"]);
                    //        data.Bulb_Tri_Colour = Convert.ToString(row["Bulb Tri-Colour"]);
                    //        data.Cabinet = Convert.ToString(row["Cabinet"]);
                    //        data.Candle = Convert.ToString(row["Candle"]);
                    //        data.Cob = Convert.ToString(row["Cob "]);
                    //        data.Downlight = Convert.ToString(row["Downlight"]);
                    //        data.Driver = Convert.ToString(row["Driver"]);
                    //        data.Emergency_Bulb = Convert.ToString(row["Emergency-Bulb"]);
                    //        data.Flood_Light = Convert.ToString(row["Flood Light"]);
                    //        data.Night_Bulb = Convert.ToString(row["Night Bulb"]);
                    //        data.Ring=Convert.ToString(row["Ring"]);
                    //        data.Slim_Panel = Convert.ToString(row["Slim Panel"]);
                    //        data.Spot_Light = Convert.ToString(row["Spot Light"]);
                    //        data.Street_Light = Convert.ToString(row["Street Light"]);
                    //        data.Strip_Light_Nova_Strip = Convert.ToString(row["Strip Light (Nova Strip)"]);
                    //        data.Track_Light = Convert.ToString(row["Track Light"]);
                    //        data.Zoom_Light = Convert.ToString(row["Zoom Light"]);
                    //        data.Vacant_1 = Convert.ToString(row["Vacant-1"]);
                    //        data.Vacant_2 = Convert.ToString(row["Vacant-2"]);
                    //        data.Vacant_3 = Convert.ToString(row["Vacant-3"]);
                    //        data.Vacant_4 = Convert.ToString(row["Vacant-4"]);
                    //        data.Vacant_5 = Convert.ToString(row["Vacant-5"]);
                    //        data.Total_Qty = Convert.ToString(row["Total Qty"]);
                    //        data.Created_date = Convert.ToString(row["Created_date"]);
                    //        data.Created_By = Convert.ToString(row["Imported Date"]);
                    //        data.Month = Convert.ToString(row["Month"]);
                    //        data.Year = Convert.ToString(row["Year"]);
                           
                    //        list.Add(data);
                    //    }
                    }
                    ViewBag.CounterType = "";


                }
                catch (Exception ex)
                {

                }
                DataTable dtsettings = odbengine.GetDataTable("select * from tbl_KnowYourStateDetails_Settings where IsshowInGrid=0");
                //if (dtsettings.Rows.Count>0)
                //{ 
                // for (int k = 0; k < dtsettings.Rows.Count; k++)
                //{
                //    dtsm.Columns.Remove(dtsettings.Rows[k]["Coloumn Name"].ToString());
                //}
                //}
            TempData["EmployeesTargetListDataTable"] = dtsm;
            TempData.Keep();
           

            if (dtsm.Rows.Count > 0)
            {
                for (int l = 0; l < dtsm.Rows.Count; l++)
                {
                  //  dtsm.Columns.Remove("Month");
                    //if (Month == "01")
                    //{
                    //    dtsm.Rows[l]["Month"] = "January";

                    //}
                    //if (dtsm.Rows[l]["Month"].ToString() == "2")
                    //{
                    //    dtsm.Rows[l]["Month"] ="February";
                    //}
                    //if (dtsm.Rows[l]["Month"].ToString() == "3")
                    //{
                    //    dtsm.Rows[l]["Month"] = "March";
                    //}
                    //if (dtsm.Rows[l]["Month"].ToString() == "4")
                    //{
                    //    dtsm.Rows[l]["Month"] = "April";
                    //}
                    //if (dtsm.Rows[l]["Month"].ToString() == "5")
                    //{
                    //    dtsm.Rows[l]["Month"] = "May";
                    //}
                    //if (dtsm.Rows[l]["Month"].ToString() == "6")
                    //{
                    //    dtsm.Rows[l]["Month"] = "June";
                    //}
                    //if (dtsm.Rows[l]["Month"].ToString() == "7")
                    //{
                    //    dtsm.Rows[l]["Month"] = "July";
                    //}
                    //if (dtsm.Rows[l]["Month"].ToString() == "8")
                    //{
                    //    dtsm.Rows[l]["Month"] = "August";
                    //}
                    //if (dtsm.Rows[l]["Month"].ToString() == "9")
                    //{
                    //    dtsm.Rows[l]["Month"] = "September";
                    //}
                    //if (dtsm.Rows[l]["Month"].ToString() == "10")
                    //{
                    //    dtsm.Rows[l]["Month"] = "October";
                    //}
                    //if (dtsm.Rows[l]["Month"].ToString() == "11")
                    //{
                    //    dtsm.Rows[l]["Month"] = "November";
                    //}
                    //if (dtsm.Rows[l]["Month"].ToString() == "12")
                    //{
                    //    dtsm.Rows[l]["Month"] = "December";
                    //}

                }

            }

            if (flag != "1")
            {
              
                foreach (DataRow row in dtsm.Rows)
                {
                    row.Delete();
                }
              
            }
          
           // foreach (DataRow row in dtsm.Rows)
            //{
            //    row["Imported Date"] = DateTime.Now;
            //    row["Imported By"] = Session["userid"].ToString();
            //    row["Month"] = month;
            //    row["Year"] = Year;
            //}
                dtsm.AcceptChanges();
                dtsm.Columns.Remove("SlNo");
                
            //ViewBag.Data = obj;
            //return PartialView("_EmployeesTargetList", list);
            return PartialView("~/Areas/MYSHOP/Views/KnowYourState/_KnowYourStateList.cshtml", dtsm);
        }
    }
}
