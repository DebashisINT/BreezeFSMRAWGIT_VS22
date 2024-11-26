/***************************************************************************************************************
1.0  v2 .0.36  Sanchita  12/01/2023     Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True" 
                                        then data in portal shall be populated based on Hierarchy Only. Refer: 25504
2.0  v2.0.49   Sanchita  14-10-2024     27742: Beat import format as per the attached file
***********************************************************************************************************/
using BusinessLogicLayer.SalesmanTrack;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// Mantis Issue 25536, 25535, 25542, 25543, 25544
using UtilityLayer;
// End of Mantis Issue 25536, 25535, 25542, 25543, 25544
// Rev 1.0
using DataAccessLayer;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
// End of Rev 1.0

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class BeatController : Controller
    {
        //
        // GET: /MYSHOP/Beat/
        public ActionResult Index()
        {
            // Mantis Issue 25536, 25535, 25542, 25543, 25544
            BeatModel Dtls = new BeatModel();
            DataSet ds = GroupBeat.Obj.GetListDataDetails();

            List<AreaList> AreaLst = new List<AreaList>();
            AreaLst = APIHelperMethods.ToModelList<AreaList>(ds.Tables[0]);
            Dtls.AreaList = AreaLst;
            Dtls.Area = 0;

            List<RouteList> RouteLst = new List<RouteList>();
            RouteLst = APIHelperMethods.ToModelList<RouteList>(ds.Tables[1]);
            Dtls.RouteList = RouteLst;
            Dtls.Route = 0;
            // End of Mantis Issue 25536, 25535, 25542, 25543, 25544

            // Rev 2.0
            TempData["FromManualLog"] = null;
            TempData["BeatImportLog"] = null;

            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/Beat/Index");
            ViewBag.CanBulkUpdate = rights.CanBulkUpdate;
            // End of Rev 2.0


            return View(Dtls);
        }

        public PartialViewResult PartialGrid()
        {
            return PartialView(GetList());
        }

        // Mantis Issue 25536, 25535, 25542, 25543, 25544 [area and route added]
        public JsonResult SaveGroupBeat(string code, string name, string id, int route)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = GroupBeat.Obj.SaveBeat(code, name, Userid, route, id);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveGroupBeatUser(string selected, string id)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = GroupBeatUsers.Obj.SaveBeatUser(selected, id);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        // Mantis Issue 25536, 25535, 25542, 25543, 25544 [ type added ]
        public JsonResult EditGroupBeat(string id, string type)
        {
            DataTable output = new DataTable();
            output = GroupBeat.Obj.EditBeat(id, type);  // Mantis Issue 25536, 25535, 25542, 25543, 25544 [ type added ]

            if (output.Rows.Count > 0)
            {
                return Json(new { code = Convert.ToString(output.Rows[0]["CODE"]), name = Convert.ToString(output.Rows[0]["NAME"]), area = Convert.ToString(output.Rows[0]["AREA_CODE"]), route= Convert.ToString(output.Rows[0]["ROUTE_CODE"]) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = "", name = "" }, JsonRequestBehavior.AllowGet);
            }

        }

        //Mantis Issue 25536, 25535, 25542, 25543, 25544
        public JsonResult SaveArea(string code, string name, string id)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = GroupBeat.Obj.SaveArea(code, name, Userid, id);
            return Json(output, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveRoute(string code, string name, int area, string id)
        {
            int output = 0;
            string Userid = Convert.ToString(Session["userid"]);
            output = GroupBeat.Obj.SaveRoute(code, name, area, Userid, id);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        // End of Mantis Issue 25536, 25535, 25542, 25543, 25544

        public IEnumerable GetList()
        {
            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.V_GROUPBEATLISTs
                    select d;
            return q;

        }

        public PartialViewResult PartialUserGrid(string id)
        {
            // Rev 1.0
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_GROUPBEAT");
            proc.AddPara("@ACTION", "GETMAPUSERLISTDATA");
            proc.AddPara("@USER_ID", Convert.ToString(Session["userid"]));
            ds = proc.GetDataSet();
            // End of Rev 1.0

            return PartialView(GetUserList());
        }

        public IEnumerable GetUserList()
        {
            string connectionString = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            // Rev 1.0
            //var q = from d in dc.tbl_master_users
            //        select d;

            int userid = Convert.ToInt32(Session["userid"]);

            var q = from d in dc.FTS_MapUserLists where d.userid == userid
                    orderby d.user_id
                    select d;
            // End of Rev 1.0
            return q;

        }

        public JsonResult SetUsers(string ID)
        {
            DataTable DT = GroupBeatUsers.Obj.GetUserMap(ID);
            GroupBeatUsers.ObjList.Clear();
            foreach (DataRow dr in DT.Rows)
            {                
                GroupBeatUsers.ObjList.Add(Convert.ToString(dr["USER_ID"]));
            }

            var Selected = GroupBeatUsers.ObjList;
            

            return Json(Selected);
        }

        // Mantis Issue 25536, 25535, 25542, 25543, 25544 [ type added]
        public JsonResult Delete(string ID, string type)
        {
            int output = 0;
            output = GroupBeat.Obj.Delete(ID, type);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        // Rev 2.0
        public ActionResult DownloadFormat()
        {
            string FileName = "BeatList.xlsx";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "image/jpeg";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/Commonfolder/BeatList.xlsx"));
            response.Flush();
            response.End();

            return null;
        }

        public ActionResult ImportExcel()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
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
                        fname = Path.Combine(Server.MapPath("~/Temporary/"), fname);
                        file.SaveAs(fname);
                        Import_To_Grid(fname, extension, file);
                    }
                    return Json("File Uploaded Successfully!");
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

        public Int32 Import_To_Grid(string FilePath, string Extension, HttpPostedFileBase file)
        {
            Boolean Success = false;
            Int32 HasLog = 0;
            TempData["FromManualLog"] = null;
            TempData["CurrentStockImportLog"] = null;

            if (file.FileName.Trim() != "")
            {
                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    DataTable dt = new DataTable();

                    //DataTable dtExcelData = new DataTable();
                    string conString = string.Empty;
                    conString = ConfigurationManager.AppSettings["ExcelConString"];
                    conString = string.Format(conString, FilePath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = "List$"; //ī;

                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                        {
                            oda.Fill(dt);
                        }
                        excel_con.Close();
                    }

                    // }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //** New Datatable included to resolve no. format for Phone numbers. State and Contact blank check was implemented to filter out blank rows from the excel Sheet. **//
                        DataTable dtExcelData = new DataTable();
                        dtExcelData.Columns.Add("AreaCode", typeof(string));
                        dtExcelData.Columns.Add("AreaName", typeof(string));
                        dtExcelData.Columns.Add("RouteCode", typeof(string));
                        dtExcelData.Columns.Add("RouteName", typeof(string));
                        dtExcelData.Columns.Add("BeatCode", typeof(string));
                        dtExcelData.Columns.Add("BeatName", typeof(string));
                        dtExcelData.Columns.Add("UserName", typeof(string));
                        dtExcelData.Columns.Add("UserID", typeof(string));
                        dtExcelData.Columns.Add("OutletEntityID", typeof(string));
                        dtExcelData.Columns.Add("OutletName", typeof(string));

                        foreach (DataRow row in dt.Select("[Beat Code*]<>''"))
                        {
                            if (Convert.ToString(row["Area Code*"]) == "")
                            {
                                row["Area Code*"] = "0";
                            }

                            if (Convert.ToString(row["Route Code*"]) == "")
                            {
                                row["Route Code*"] = "0";
                            }

                            if (Convert.ToString(row["Beat Code*"]) == "")
                            {
                                row["Beat Code*"] = "0";
                            }

                            if (Convert.ToString(row["User ID*"]) == "")
                            {
                                row["User ID*"] = "0";
                            }


                            dtExcelData.Rows.Add(Convert.ToString(row["Area Code*"]), Convert.ToString(row["Area Name"]), Convert.ToString(row["Route Code*"]),
                            Convert.ToString(row["Route Name"]), Convert.ToString(row["Beat Code*"]), Convert.ToString(row["Beat Name"]),
                            Convert.ToString(row["User Name"]), Convert.ToString(row["User ID*"]), Convert.ToString(row["Outlet Entity ID"])
                            , Convert.ToString(row["Outlet Name"]));

                        }

                        try
                        {
                            TempData["BeatImportLog"] = dtExcelData;
                            TempData.Keep();

                            DataTable dtCmb = new DataTable();
                            ProcedureExecute proc = new ProcedureExecute("PRC_GROUPBEAT");
                            proc.AddPara("@ACTION", "IMPORTBEAT");
                            proc.AddPara("@IMPORT_TABLE", dtExcelData);
                            proc.AddPara("@User_Id", Convert.ToInt32(Session["userid"]));
                            dtCmb = proc.GetTable();

                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
            return HasLog;
        }

        public ActionResult BeatImportLog()
        {
            List<BeatImportLogModel> list = new List<BeatImportLogModel>();
            DataTable dt = new DataTable();
            try
            {
                if (TempData["BeatImportLog"] != null)
                {
                    if (TempData["FromManualLog"] != null && Convert.ToString(TempData["FromManualLog"]) == "1")
                    {
                        dt = (DataTable)TempData["BeatImportLog"];
                    }
                    else
                    {
                        DataTable dtCmb = new DataTable();
                        ProcedureExecute proc = new ProcedureExecute("PRC_GROUPBEAT");
                        proc.AddPara("@Action", "SHOWIMPORTLOG");
                        proc.AddPara("@IMPORT_TABLE", (DataTable)TempData["BeatImportLog"]);
                        proc.AddPara("@User_Id", Convert.ToInt32(Session["userid"]));
                        dt = proc.GetTable();
                    }

                    TempData.Keep();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        BeatImportLogModel data = null;
                        foreach (DataRow row in dt.Rows)
                        {
                            data = new BeatImportLogModel();
                            data.AreaCode = Convert.ToString(row["AreaCode"]);
                            data.AreaName = Convert.ToString(row["AreaName"]);
                            data.RouteCode = Convert.ToString(row["RouteCode"]);
                            data.RouteName = Convert.ToString(row["RouteName"]);
                            data.BeatCode = Convert.ToString(row["BeatCode"]);
                            data.BeatName = Convert.ToString(row["BeatName"]);
                            data.UserName = Convert.ToString(row["UserName"]);
                            data.UserID = Convert.ToString(row["UserID"]);
                            data.OutletEntityID = Convert.ToString(row["OutletEntityID"]);
                            data.OutletName = Convert.ToString(row["OutletName"]);
                            data.ImportStatus = Convert.ToString(row["ImportStatus"]);
                            data.ImportMsg = Convert.ToString(row["ImportMsg"]);
                            data.ImportDate = Convert.ToString(row["ImportDate"]);
                            data.CreateUser = Convert.ToString(row["CreateUser"]);

                            list.Add(data);
                        }
                    }
                    //TempData["EnquiriesImportLog"] = dt;
                }

            }
            catch (Exception ex)
            {

            }
            TempData.Keep();
            return PartialView(list);
        }

        [HttpPost]
        public JsonResult BeatImportManualLog(string Fromdt, String ToDate)
        {
            string output_msg = string.Empty;
            try
            {
                string datfrmat = Fromdt.Split('-')[2] + '-' + Fromdt.Split('-')[1] + '-' + Fromdt.Split('-')[0];
                string dattoat = ToDate.Split('-')[2] + '-' + ToDate.Split('-')[1] + '-' + ToDate.Split('-')[0];

                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_GROUPBEAT");
                proc.AddPara("@ACTION", "GETBEATIMPORTLOG");
                proc.AddPara("@FromDate", datfrmat);
                proc.AddPara("@ToDate", dattoat);
                dt = proc.GetTable();

                if (dt != null && dt.Rows.Count > 0)
                {
                    TempData["BeatImportLog"] = dt;
                    TempData["FromManualLog"] = "1";
                    TempData.Keep();
                    output_msg = "True";
                }
                else
                {
                    output_msg = "Log not found.";
                }
            }
            catch (Exception ex)
            {
                output_msg = "Please try again later";
            }
            return Json(output_msg, JsonRequestBehavior.AllowGet);
        }
        // End of Rev 2.0

    }
}