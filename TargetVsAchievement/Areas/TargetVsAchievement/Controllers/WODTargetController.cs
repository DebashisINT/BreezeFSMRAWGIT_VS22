using DataAccessLayer;
using DevExpress.XtraTreeList.Handler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using TargetVsAchievement.Models;
using UtilityLayer;

namespace TargetVsAchievement.Areas.TargetVsAchievement.Controllers
{
    public class WODTargetController : Controller
    {
        WODModel objdata = null;
        Int32 DetailsID = 0;
        string TargetNo = string.Empty;
        public WODTargetController()
        {
            objdata = new WODModel();
        }
        public ActionResult Index()
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/TargetSetUp/Index");


            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;

            // SELECT TARGET TYPE DROPDOWN //
            DataTable dt = new DataTable();
            dt = GetListData();

            if (dt != null)
            {
                List<LevelList> LevelList = new List<LevelList>();
                LevelList = APIHelperMethods.ToModelList<LevelList>(dt);
                objdata.LevelList = LevelList;
            }
            // SELECT TARGET TYPE DROPDOWN //


            TempData["Count"] = 1;
            TempData.Keep();

            TempData["DetailsID"] = null;
            TempData.Keep();

            TempData["LevelDetails"] = null;
            TempData.Keep();

            return View(objdata);
        }


        public ActionResult EDITINDEX()
        {
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/TargetSetUp/Index");

            // SELECT TARGET TYPE DROPDOWN //
            DataTable dt1 = new DataTable();
            dt1 = GetListData();

            if (dt1 != null)
            {
                List<LevelList> LevelList = new List<LevelList>();
                LevelList = APIHelperMethods.ToModelList<LevelList>(dt1);
                objdata.LevelList = LevelList;
            }
            // SELECT TARGET TYPE DROPDOWN //

            if (TempData["DetailsID"] != null)
            {
                objdata.TARGET_ID = Convert.ToInt64(TempData["DetailsID"]);
                TempData.Keep();

                if (Convert.ToInt64(objdata.TARGET_ID) > 0)
                {
                    DataTable objData = objdata.GETTARGETASSIGNDETAILSBYID("GETHEADERWODTARGET", objdata.TARGET_ID);
                    if (objData != null && objData.Rows.Count > 0)
                    {
                        DataTable dt = objData;
                        foreach (DataRow row in dt.Rows)
                        {
                            objdata.TARGET_ID = Convert.ToInt64(row["WODTARGET_ID"]);
                            objdata.TargetType = Convert.ToString(row["TARGETLEVEL"]);
                            objdata.TargetNo = Convert.ToString(row["TARGETDOCNUMBER"]);
                            objdata.TargetDate = Convert.ToDateTime(row["TARGETDATE"]);
                        }
                    }
                }
            }
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanEdit = rights.CanEdit;
            ViewBag.CanDelete = rights.CanDelete;

            TempData["Count"] = 1;
            TempData.Keep();
            return PartialView("~/Areas/TargetVsAchievement/Views/WODTarget/Index.cshtml", objdata);

        }
        public ActionResult GetWODTargetEntryList()
        {
            WODTARGETGRIDLIST productdataobj = new WODTARGETGRIDLIST();
            List<WODTARGETGRIDLIST> productdata = new List<WODTARGETGRIDLIST>();
            Int64 DetailsID = 0;
            try
            {
                DataTable dt=new DataTable();
                if (TempData["DetailsID"] != null)
                {
                    DetailsID = Convert.ToInt64(TempData["DetailsID"]);
                    TempData.Keep();
                }
                if (DetailsID > 0 && TempData["LevelDetails"] == null)
                {
                    DataTable objData = objdata.GETTARGETASSIGNDETAILSBYID("GETDETAILSWODTARGET", DetailsID);
                    if (objData != null && objData.Rows.Count > 0)
                    {
                        dt = objData;

                        DataTable dtable = new DataTable();

                        dtable.Clear();
                        dtable.Columns.Add("HIddenID", typeof(System.Guid));
                        dtable.Columns.Add("SlNO", typeof(System.String));
                        dtable.Columns.Add("TARGETLEVEL", typeof(System.String));
                        dtable.Columns.Add("TIMEFRAME", typeof(System.String));
                        dtable.Columns.Add("STARTEDATE", typeof(System.String));
                        dtable.Columns.Add("ENDDATE", typeof(System.String));
                        dtable.Columns.Add("WODCOUNT", typeof(System.String));
                        dtable.Columns.Add("TARGETLEVELID", typeof(System.String));
                        dtable.Columns.Add("INTERNALID", typeof(System.String));


                        String Gid = "";
                        
                        foreach (DataRow row in dt.Rows)
                        {
                            Gid = Guid.NewGuid().ToString();
                            productdataobj = new WODTARGETGRIDLIST();
                            productdataobj.SlNO = Convert.ToString(row["SlNO"]);
                            //productdataobj.ActualSL = Convert.ToString(row["WODTARGETDETAILS_ID"]);
                            //productdataobj.TARGETDOCNUMBER = Convert.ToString(row["TARGETDOCNUMBER"]);
                            productdataobj.TARGETLEVELID = Convert.ToString(row["TARGETLEVELID"]);
                            productdataobj.TARGETLEVEL = Convert.ToString(row["TARGETLEVEL"]);
                            productdataobj.INTERNALID = Convert.ToString(row["INTERNALID"]);

                            productdataobj.TIMEFRAME = Convert.ToString(row["TIMEFRAME"]);
                            productdataobj.STARTEDATE = Convert.ToString(row["STARTEDATE"]);
                            productdataobj.ENDDATE = Convert.ToString(row["ENDDATE"]);
                            productdataobj.WODCOUNT = Convert.ToString(row["WODCOUNT"]);
                            productdataobj.Guids = Gid;
                            productdata.Add(productdataobj);


                            object[] trow = { Gid, row["SlNO"] , Convert.ToString(row["TARGETLEVEL"]), Convert.ToString(row["TIMEFRAME"]),
                                        Convert.ToString(row["STARTEDATE"]), Convert.ToString(row["ENDDATE"]), Convert.ToString(row["WODCOUNT"]),
                                        Convert.ToString(row["TARGETLEVELID"]), Convert.ToString(row["INTERNALID"]) };
                            dtable.Rows.Add(trow);

                           

                        }

                        dt = dtable;
                    }
                }
                else
                {
                    dt = (DataTable)TempData["LevelDetails"];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            productdataobj = new WODTARGETGRIDLIST();
                            productdataobj.SlNO = Convert.ToString(row["SlNO"]);
                            //productdataobj.ActualSL = Convert.ToString(row["WODTARGETDETAILS_ID"]);
                            //productdataobj.TARGETDOCNUMBER = Convert.ToString(row["TARGETDOCNUMBER"]);
                            productdataobj.TARGETLEVELID = Convert.ToString(row["TARGETLEVELID"]);
                            productdataobj.TARGETLEVEL = Convert.ToString(row["TARGETLEVEL"]);
                            productdataobj.INTERNALID = Convert.ToString(row["INTERNALID"]);

                            productdataobj.TIMEFRAME = Convert.ToString(row["TIMEFRAME"]);
                            productdataobj.STARTEDATE = Convert.ToString(row["STARTEDATE"]);
                            productdataobj.ENDDATE = Convert.ToString(row["ENDDATE"]);
                            productdataobj.WODCOUNT = Convert.ToString(row["WODCOUNT"]);
                            productdataobj.Guids = Convert.ToString(row["HIddenID"]);
                            productdata.Add(productdataobj);

                        }
                    }
                    
                }
                TempData["LevelDetails"] = dt;
                TempData.Keep();
            }
            catch { }
            return PartialView("~/Areas/TargetVsAchievement/Views/WODTarget/_PartialWODTargetEntry.cshtml", productdata);

        }

        [WebMethod]
        public JsonResult AddLevelDetails(WODTARGETGRIDLIST prod)
        {
            DataTable dt = (DataTable)TempData["LevelDetails"];
            DataTable dt2 = new DataTable();

            if (dt == null)
            {
                DataTable dtable = new DataTable();

                dtable.Clear();
                dtable.Columns.Add("HIddenID", typeof(System.Guid));
                dtable.Columns.Add("SlNO", typeof(System.String));
                dtable.Columns.Add("TARGETLEVEL", typeof(System.String));
                dtable.Columns.Add("TIMEFRAME", typeof(System.String));
                dtable.Columns.Add("STARTEDATE", typeof(System.String));
                dtable.Columns.Add("ENDDATE", typeof(System.String));
                dtable.Columns.Add("WODCOUNT", typeof(System.String));
                dtable.Columns.Add("TARGETLEVELID", typeof(System.String));
                dtable.Columns.Add("INTERNALID", typeof(System.String));


                object[] trow = { Guid.NewGuid(), 1, prod.TARGETLEVEL, prod.TIMEFRAME,
                        DateTime.ParseExact(prod.STARTEDATE, "yyyy-MM-dd", null).ToString("dd-MM-yyyy"),
                        DateTime.ParseExact(prod.ENDDATE, "yyyy-MM-dd", null).ToString("dd-MM-yyyy"), 
                    prod.WODCOUNT, prod.TARGETLEVELID, prod.INTERNALID };
                dtable.Rows.Add(trow);
                TempData["LevelDetails"] = dtable;
                TempData.Keep();
            }
            else
            {
                if (string.IsNullOrEmpty(prod.Guids))
                {
                    object[] trow = { Guid.NewGuid(), Convert.ToInt32(dt.Rows.Count) + 1, prod.TARGETLEVEL, prod.TIMEFRAME,
                        DateTime.ParseExact(prod.STARTEDATE, "yyyy-MM-dd", null).ToString("dd-MM-yyyy"),
                        DateTime.ParseExact(prod.ENDDATE, "yyyy-MM-dd", null).ToString("dd-MM-yyyy"), 
                        prod.WODCOUNT, prod.TARGETLEVELID, prod.INTERNALID };


                    dt.Rows.Add(trow);
                    TempData["LevelDetails"] = dt;
                    TempData.Keep();
                }
                else
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            if (prod.Guids.ToString() == item["HIddenID"].ToString())
                            {
                                // item["SlNO"] = prod.SlNO;
                                item["TARGETLEVEL"] = prod.TARGETLEVEL;
                                item["TIMEFRAME"] = prod.TIMEFRAME;
                                item["STARTEDATE"] = DateTime.ParseExact(prod.STARTEDATE, "yyyy-MM-dd", null).ToString("dd-MM-yyyy");
                                item["ENDDATE"] = DateTime.ParseExact(prod.ENDDATE, "yyyy-MM-dd", null).ToString("dd-MM-yyyy");
                                item["WODCOUNT"] = prod.WODCOUNT;
                                item["TARGETLEVELID"] = prod.TARGETLEVELID;
                                item["INTERNALID"] = prod.INTERNALID;
                                
                            }
                        }
                    }
                }
                TempData["LevelDetails"] = dt;
                TempData.Keep();
            }
            return Json("");
        }


        [WebMethod]
        public JsonResult SaveWOD(WODModel Details)
        {
            String Message = "";
            Boolean Success = false;
            DataSet dt = new DataSet();
            DataTable dt_Details = (DataTable)TempData["LevelDetails"];

            DataView dvData = new DataView(dt_Details);           
            DataTable dt_temp = dvData.ToTable();

            var duplicateRecords = dt_temp.AsEnumerable()
           .GroupBy(r => (r["TARGETLEVEL"], r["TIMEFRAME"], r["STARTEDATE"], r["ENDDATE"], r["TARGETLEVELID"], r["INTERNALID"]))
           .Where(gr => gr.Count() > 1)
            .Select(g => g.Key);

            string validate = "";
            foreach (var d in duplicateRecords)
            {
                validate = "duplicateLevel";
            }

            if (validate == "duplicateLevel")
            {
                Message = "duplicateLevel";
            }
            else
            {

                List<UDTWODTARGET> udt = new List<UDTWODTARGET>();

                foreach (DataRow item in dt_Details.Rows)
                {
                    UDTWODTARGET obj1 = new UDTWODTARGET();
                    obj1.TARGETLEVELID = Convert.ToInt64(item["TARGETLEVELID"]);
                    obj1.TARGETLEVEL = Convert.ToString(item["TARGETLEVEL"]);
                    obj1.INTERNALID = Convert.ToString(item["INTERNALID"]);
                    obj1.TIMEFRAME = Convert.ToString(item["TIMEFRAME"]);
                    obj1.STARTEDATE = DateTime.ParseExact(Convert.ToString(item["STARTEDATE"]), "dd-MM-yyyy", null);
                    obj1.ENDDATE = DateTime.ParseExact(Convert.ToString(item["ENDDATE"]), "dd-MM-yyyy", null);
                    obj1.WODCOUNT = Convert.ToInt64(item["WODCOUNT"]);
                    obj1.SlNO = Convert.ToString(item["SlNO"]);

                    udt.Add(obj1);
                }



                DataTable dtTarget = new DataTable();
                dtTarget = ToDataTable(udt);

                if (Convert.ToInt64(Details.TARGET_ID) > 0)
                {
                    dt = objdata.TargetEntryInsertUpdate("UPDATEWODTARGET", Convert.ToDateTime(Details.TargetDate), Convert.ToInt64(Details.TARGET_ID), Details.TargetType, Details.TargetNo
                           , dtTarget, Convert.ToInt64(Session["userid"]));
                }
                else
                {
                    dt = objdata.TargetEntryInsertUpdate("INSERTWODTARGET", Convert.ToDateTime(Details.TargetDate), Convert.ToInt64(Details.TARGET_ID), Details.TargetType, Details.TargetNo
                           , dtTarget, Convert.ToInt64(Session["userid"]));

                }
                if (dt != null && dt.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Tables[0].Rows)
                    {
                        Success = Convert.ToBoolean(row["Success"]);
                        DetailsID = Convert.ToInt32(row["DetailsID"]);
                        TargetNo = Convert.ToString(Details.TargetNo);
                    }
                }

            }
            //TempData["DetailsID"] = null;
            //TempData.Keep();
            //ViewData["DetailsID"] = DetailsID;
            //ViewData["TargetNo"] = obj2.TargetNo;
            //ViewData["Success"] = Success;
            //ViewData["Message"] = Message;

            String retuenMsg = Success + "~" + DetailsID  + "~" + Details.TargetNo + "~" + Message;
            return Json(retuenMsg);
        
    }

        //[HttpPost, ValidateInput(false)]
        //public ActionResult BatchEditingUpdateTargetEntry(DevExpress.Web.Mvc.MVCxGridViewBatchUpdateValues<WODTARGETGRIDLIST, int> updateValues, BrandVolumeValueTargetModel options)
        //{
        //    TempData["Count"] = (int)TempData["Count"] + 1;
        //    TempData.Keep();

        //    String Message = "";
        //    Int64 SaveDataArea = 0;

        //    List<UDTWODTARGET> udt = new List<UDTWODTARGET>();

        //    if ((int)TempData["Count"] != 2)
        //    {
        //        Boolean IsProcess = false;

        //        if (updateValues.Insert.Count > 0 && Convert.ToInt64(options.TARGET_ID) < 1)
        //        {
        //            List<WODTARGETGRIDLIST> udtlist = new List<WODTARGETGRIDLIST>();
        //            WODTARGETGRIDLIST obj = null;

        //            foreach (var item in updateValues.Insert)
        //            {
        //                if (Convert.ToInt64(item.TARGETLEVELID) > 0)
        //                {
        //                    obj = new WODTARGETGRIDLIST();
        //                    obj.TARGETLEVELID = item.TARGETLEVELID;
        //                    obj.TARGETLEVEL = item.TARGETLEVEL;
        //                    obj.INTERNALID = item.INTERNALID;
        //                    obj.TIMEFRAME = item.TIMEFRAME;
        //                    obj.STARTEDATE = item.STARTEDATE;
        //                    obj.ENDDATE = item.ENDDATE;
        //                    obj.WODCOUNT = item.WODCOUNT;

        //                    obj.SlNO = item.SlNO;
        //                    udtlist.Add(obj);
        //                }
        //            }
        //            if (udtlist.Count > 0)
        //            {
        //                SaveDataArea = 1;

        //                foreach (var item in udtlist)
        //                {
        //                    UDTWODTARGET obj1 = new UDTWODTARGET();
        //                    obj1.TARGETLEVELID = Convert.ToInt64(item.TARGETLEVELID);
        //                    obj1.TARGETLEVEL = item.TARGETLEVEL;
        //                    obj1.INTERNALID = item.INTERNALID;
        //                    obj1.TIMEFRAME = item.TIMEFRAME;
        //                    obj1.STARTEDATE = item.STARTEDATE;
        //                    obj1.ENDDATE = item.ENDDATE;
        //                    obj1.WODCOUNT = item.WODCOUNT;                           
        //                    obj1.SlNO = item.SlNO;
        //                    udt.Add(obj1);
        //                }
        //                IsProcess = TargetInsertUpdate(udt, options);


        //            }

        //        }
        //        if (((updateValues.Update.Count > 0 && Convert.ToInt64(options.TARGET_ID) > 0) || (updateValues.Insert.Count > 0 && Convert.ToInt64(options.TARGET_ID) < 1)) && SaveDataArea == 0)
        //        {
        //            List<WODTARGETGRIDLIST> udtlist = new List<WODTARGETGRIDLIST>();
        //            WODTARGETGRIDLIST obj = null;
        //            foreach (var item in updateValues.Update)
        //            {
        //                if (Convert.ToInt64(item.TARGETLEVELID) > 0)
        //                {
        //                    obj = new WODTARGETGRIDLIST();
        //                    obj.TARGETLEVELID = item.TARGETLEVELID;
        //                    obj.TARGETLEVEL = item.TARGETLEVEL;
        //                    obj.INTERNALID = item.INTERNALID;
        //                    obj.TIMEFRAME = item.TIMEFRAME;
        //                    obj.STARTEDATE = item.STARTEDATE;
        //                    obj.ENDDATE = item.ENDDATE;
        //                    obj.WODCOUNT = item.WODCOUNT;                            
        //                    obj.SlNO = item.ActualSL;
        //                    udtlist.Add(obj);
        //                }
        //            }

        //            foreach (var item in updateValues.Insert)
        //            {
        //                if (Convert.ToInt64(item.TARGETLEVELID) > 0)
        //                {
        //                    obj = new WODTARGETGRIDLIST();
        //                    obj.TARGETLEVELID = item.TARGETLEVELID;
        //                    obj.TARGETLEVEL = item.TARGETLEVEL;
        //                    obj.INTERNALID = item.INTERNALID;
        //                    obj.TIMEFRAME = item.TIMEFRAME;
        //                    obj.STARTEDATE = item.STARTEDATE;
        //                    obj.ENDDATE = item.ENDDATE;
        //                    obj.WODCOUNT = item.WODCOUNT;                            
        //                    obj.SlNO = "0";
        //                    udtlist.Add(obj);
        //                }
        //            }

        //            foreach (var item in updateValues.DeleteKeys)
        //            {
        //                Int32 delId = Convert.ToInt32(item);

        //                foreach (var item1 in udtlist.ToList())
        //                {
        //                    Int32 delId1 = Convert.ToInt32(item1.ActualSL);

        //                    if (delId1 == delId)
        //                    {
        //                        udtlist.Remove(item1);
        //                    }
        //                }
        //            }


        //            if (udtlist.Count > 0)
        //            {
        //                SaveDataArea = 1;

        //                foreach (var item in udtlist)
        //                {
        //                    UDTWODTARGET obj1 = new UDTWODTARGET();
        //                    obj1.TARGETLEVELID = Convert.ToInt64(item.TARGETLEVELID);
        //                    obj1.TARGETLEVEL = item.TARGETLEVEL;
        //                    obj1.INTERNALID = item.INTERNALID;
        //                    obj1.TIMEFRAME = item.TIMEFRAME;
        //                    obj1.STARTEDATE = item.STARTEDATE;
        //                    obj1.ENDDATE = item.ENDDATE;
        //                    obj1.WODCOUNT = item.WODCOUNT;                           
        //                    obj1.SlNO = item.SlNO;
        //                    udt.Add(obj1);
        //                }

        //                IsProcess = TargetInsertUpdate(udt, options);
        //            }
        //        }

        //        TempData["Count"] = 1;
        //        TempData.Keep();
        //        TempData["DetailsID"] = null;
        //        TempData.Keep();
        //        ViewData["DetailsID"] = DetailsID;
        //        ViewData["TargetNo"] = options.TargetNo;
        //        ViewData["Success"] = IsProcess;
        //        ViewData["Message"] = Message;
        //    }

        //    return PartialView("~/Areas/TargetVsAchievement/Views/WODTarget/_PartialWODTargetEntry.cshtml", updateValues.Update);
        //}

        //public Boolean TargetInsertUpdate(List<UDTWODTARGET> obj, BrandVolumeValueTargetModel obj2)
        //{
        //    Boolean Success = false;
        //    try
        //    {
        //        DataTable dtSalesTarget = new DataTable();
        //        dtSalesTarget = ToDataTable(obj);
        //        DataColumnCollection dtC = dtSalesTarget.Columns;
        //        if (dtC.Contains("UpdateEdit"))
        //        {
        //            dtSalesTarget.Columns.Remove("UpdateEdit");
        //        }
        //        if (dtC.Contains("TARGETDETAILS_ID"))
        //        {
        //            dtSalesTarget.Columns.Remove("TARGETDETAILS_ID");
        //        }


        //        DataSet dt = new DataSet();

        //        if (Convert.ToInt64(obj2.TARGET_ID) > 0 && Convert.ToInt16(TempData["IsView"]) == 0)
        //        {
        //            dt = objdata.TargetEntryInsertUpdate("UPDATEWODTARGET", Convert.ToDateTime(obj2.TargetDate), Convert.ToInt64(obj2.TARGET_ID), obj2.TargetType, obj2.TargetNo
        //                   , dtSalesTarget, Convert.ToInt64(Session["userid"]));
        //        }
        //        else
        //        {
        //            dt = objdata.TargetEntryInsertUpdate("INSERTWODTARGET", Convert.ToDateTime(obj2.TargetDate), Convert.ToInt64(obj2.TARGET_ID), obj2.TargetType, obj2.TargetNo
        //                   , dtSalesTarget, Convert.ToInt64(Session["userid"]));

        //        }
        //        if (dt != null && dt.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Tables[0].Rows)
        //            {
        //                Success = Convert.ToBoolean(row["Success"]);
        //                DetailsID = Convert.ToInt32(row["DetailsID"]);
        //                TargetNo = Convert.ToString(obj2.TargetNo);
        //            }
        //        }
        //    }
        //    catch { }
        //    return Success;
        //}
        public DataTable ToDataTable<T>(List<T> items)
        {

            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {

                //Setting column names as Property names

                dataTable.Columns.Add(prop.Name);

            }

            foreach (T item in items)
            {

                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)
                {

                    //inserting property values to datatable rows

                    values[i] = Props[i].GetValue(item, null);

                }

                dataTable.Rows.Add(values);

            }

            //put a breakpoint here and check datatable

            return dataTable;

        }

        public JsonResult SetDataByID(Int64 detailsid = 0, Int16 IsView = 0)
        {
            Boolean Success = false;
            try
            {
                TempData["LevelDetails"] = null;

                TempData["DetailsID"] = detailsid;
                TempData["IsView"] = IsView;
                TempData.Keep();
                Success = true;
            }
            catch { }
            return Json(Success);
        }

        public JsonResult CHECKUNIQUETARGETDOCNUMBER(string TargetNo,string TargetID)
        {

            var retData = 0;
            try
            {
                ProcedureExecute proc;
                using (proc = new ProcedureExecute("PRC_WODTARGETASSIGN"))
                {
                    proc.AddVarcharPara("@action", 100, "CHECKUNIQUETARGETDOCNUMBER");
                    proc.AddIntegerPara("@ReturnValue", 0, QueryParameterDirection.Output);
                    proc.AddVarcharPara("@TargetNo", 100, TargetNo);
                    proc.AddVarcharPara("@TARGET_ID", 100, TargetID);
                    int i = proc.RunActionQuery();
                    retData = Convert.ToInt32(proc.GetParaValue("@ReturnValue"));

                }
            }
            catch { }
            return Json(retData);
        }

        public JsonResult CHECKUNIQUETARGETDETAILS(string TargetType, string TARGETLEVELID, string TARGETLEVEL, string INTERNALID, string TimeFrame,string STARTEDATE, string ENDDATE)
        {          
            var retData = 0;
            try
            {
                ProcedureExecute proc;
                using (proc = new ProcedureExecute("PRC_WODTARGETASSIGN"))
                {
                    proc.AddVarcharPara("@action", 100, "CHECKUNIQUETARGETDETAILS");
                    proc.AddIntegerPara("@ReturnValue", 0, QueryParameterDirection.Output);
                    proc.AddVarcharPara("@TargetType", 100, TargetType);
                    proc.AddVarcharPara("@UNIQUETARGETLEVEL", 100, TARGETLEVEL);
                    proc.AddVarcharPara("@UNIQUEINTERNALID", 100, INTERNALID);
                    proc.AddVarcharPara("@UNIQUETARGETLEVELID", 100, TARGETLEVELID);
                    proc.AddVarcharPara("@UNIQUETIMEFRAME", 100, TimeFrame);
                    proc.AddVarcharPara("@UNIQUESTARTEDATE", 100, STARTEDATE);
                    proc.AddVarcharPara("@UNIQUEENDDATE", 100, ENDDATE);                    
                    int i = proc.RunActionQuery();
                    retData = Convert.ToInt32(proc.GetParaValue("@ReturnValue"));

                }
            }
            catch { }
            return Json(retData);
        }



        [WebMethod]
        public JsonResult EditTargetData(String HiddenID)
        {
            WODTARGETGRIDLIST ret = new WODTARGETGRIDLIST();

            DataTable dt = (DataTable)TempData["LevelDetails"];

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (HiddenID.ToString() == item["HIddenID"].ToString())
                    {
                        ret.SlNO = item["SlNO"].ToString();
                        ret.TARGETLEVEL = item["TARGETLEVEL"].ToString();
                        ret.TIMEFRAME = item["TIMEFRAME"].ToString();
                        ret.Guids = item["HIddenID"].ToString();
                        ret.STARTEDATE = item["STARTEDATE"].ToString();
                        ret.ENDDATE = item["ENDDATE"].ToString();
                        ret.WODCOUNT = item["WODCOUNT"].ToString();
                        //ret.Warehouse = item["Warehouse"].ToString();
                        ret.TARGETLEVELID = item["TARGETLEVELID"].ToString();
                        ret.INTERNALID = item["INTERNALID"].ToString();
                        
                        break;
                    }
                }
            }
            TempData["LevelDetails"] = dt;
            TempData.Keep();
            return Json(ret);
        }
        public JsonResult DeleteLevelData(string HiddenID)
        {
            DataTable dt = (DataTable)TempData["LevelDetails"];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (HiddenID.ToString() == item["HIddenID"].ToString())
                    {
                        dt.Rows.Remove(item);
                        break;
                    }
                }
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                int conut = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    dr["SlNO"] = conut;
                    conut++;
                }
            }

            TempData["LevelDetails"] = dt;
            TempData.Keep();
            return Json("Level Removed Successfully.");
        }

        // SELECT TARGET TYPE DROPDOWN //
        public DataTable GetListData()
        {
            DataTable dt = new DataTable();

            ProcedureExecute proc = new ProcedureExecute("PRC_SALESTARGETASSIGN");
            proc.AddPara("@ACTION", "GETDROPDOWNBINDDATA");
            dt = proc.GetTable();
            return dt;
        }
        // SELECT TARGET TYPE DROPDOWN //
    }
}