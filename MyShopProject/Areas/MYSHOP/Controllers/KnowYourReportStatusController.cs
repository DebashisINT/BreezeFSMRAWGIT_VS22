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

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class KnowYourReportStatusController : Controller
    {
        //
        // GET: /MYSHOP/KnowYourReportStatus/
        public ActionResult KnowYourReportStatusIndex()
        {
            return View("~/Areas/MYSHOP/Views/KnowYourReportStatus/KnowYourReportStatusIndex.cshtml");
        }
        public ActionResult KnowYourReportBindUsergrid(string Month, string Year, string flag)
        {
            List<BindUser> listRecpt = new List<BindUser>();
            DataTable dtbindreport = new DataTable();
            try
            {
                if (Session["userid"].ToString() != null)
                {
                //   // SearchKey = SearchKey.Replace("'", "''");
                    BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                    SqlConnection con = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["DBConnectionDefault"]));
                    SqlCommand cmd = new SqlCommand("proc_BindKnowYourStateGrid", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_Id", Session["userid"].ToString());
                    cmd.Parameters.AddWithValue("@Month", Month);
                    cmd.Parameters.AddWithValue("@Year", Year);
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtbindreport);
                    TempData["EmployeesTargetListDataTable"] = dtbindreport;
                    cmd.Dispose();
                    con.Dispose();

                    //listRecpt = (from DataRow dr in recpt.Rows
                    //             select new BindUser()
                    //             {
                    //                 EmpId1 = dr["Emp id1"].ToString(),
                    //                 EmpId2 = dr["Emp id2"].ToString()

                    //             }).ToList();
                }
                    

           }
            catch (Exception ex)
            {

            }
            if (flag != "1")
            {
                foreach (DataRow row in dtbindreport.Rows)
                {
                    row.Delete();
                }
                dtbindreport.AcceptChanges();
            }
            if( dtbindreport.Columns.Contains("SlNo"))
            { 
            dtbindreport.Columns.Remove("SlNo");
            }
            TempData.Keep();
            //return Json(listRecpt);
            return PartialView("~/Areas/MYSHOP/Views/KnowYourReportStatus/_KnowYourStategrid.cshtml", dtbindreport);
        }
        //[WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public object GetRecipientsBind(string SearchKey)
        //{
        //    List<RecipientsBindModel> listRecpt = new List<RecipientsBindModel>();
        //    if (HttpContext.Current.Session["userid"] != null)
        //    {
        //        SearchKey = SearchKey.Replace("'", "''");
        //        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

        //        DataTable recpt = new DataTable();
        //        SqlConnection con = new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
        //        SqlCommand cmd = new SqlCommand("PRC_EMAILSEARCH_REPORT", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@filtertext", SearchKey);
        //        cmd.CommandTimeout = 0;
        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = cmd;
        //        da.Fill(recpt);


        //        cmd.Dispose();
        //        con.Dispose();

        //        listRecpt = (from DataRow dr in recpt.Rows
        //                     select new RecipientsBindModel()
        //                     {
        //                         ID = dr["ID"].ToString(),
        //                         Email = dr["Email"].ToString()

        //                     }).ToList();
        //    }

        //    return listRecpt;
        //}
        public ActionResult ExportEmployeesTargetList(int type)
        {
            ViewData["EmployeesRevisitList"] = TempData["EmployeesTargetListDataTable"];

            TempData.Keep();

            if (ViewData["EmployeesRevisitList"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeesTargetGridView(ViewData["EmployeesRevisitList"]), ViewData["EmployeesRevisitList"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeesTargetGridView(ViewData["EmployeesRevisitList"]), ViewData["EmployeesRevisitList"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeesTargetGridView(ViewData["EmployeesRevisitList"]), ViewData["EmployeesRevisitList"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeesTargetGridView(ViewData["EmployeesRevisitList"]), ViewData["EmployeesRevisitList"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeesTargetGridView(ViewData["EmployeesRevisitList"]), ViewData["EmployeesRevisitList"]);
                    default:
                        break;
                }
            }
            return null;
        }
        private GridViewSettings GetEmployeesTargetGridView(object datatable)
        {
            //List<EmployeesTargetSetting> obj = (List<EmployeesTargetSetting>)datatablelist;
            //ListtoDataTable lsttodt = new ListtoDataTable();
            //DataTable datatable = ConvertListToDataTable(obj); 
            var settings = new GridViewSettings();
            settings.Name = "KnowYourStateReport"+DateTime.Now;
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "EmployeesRevisitList";
            //String ID = Convert.ToString(TempData["EmployeesTargetListDataTable"]);
            TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                //if (datacolumn.ColumnName == "FOR_MONTH" || datacolumn.ColumnName == "REVISITDATE"
                //    || datacolumn.ColumnName == "LOGIN_ID" || datacolumn.ColumnName == "EMPLOYEE_NAME" || datacolumn.ColumnName == "SHOPPIN" || datacolumn.ColumnName == "SHOP_ID"
                //    || datacolumn.ColumnName == "SHOP_NAME" || datacolumn.ColumnName == "SHOP_CONTACT" || datacolumn.ColumnName == "SHOP_ADDRESS" || datacolumn.ColumnName == "SHOP_STATE" || datacolumn.ColumnName == "SHOP_TYPE")
                //{
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


	}
}