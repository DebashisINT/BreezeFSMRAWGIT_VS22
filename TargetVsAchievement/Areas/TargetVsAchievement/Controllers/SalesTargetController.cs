using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TargetVsAchievement.Models;


using BusinessLogicLayer;
using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using EntityLayer.CommonELS;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web.Services;
using UtilityLayer;
using Antlr.Runtime.Misc;
using System.Security.Cryptography.X509Certificates;

namespace TargetVsAchievement.Areas.TargetVsAchievement.Controllers
{
    public class SalesTargetController : Controller
    {
        SalesTargetModel objdata = null;      
        Int32 DetailsID = 0;
        string SalesTargetNo = string.Empty;
        public SalesTargetController()
        {
            objdata = new SalesTargetModel();         
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
                objdata.SALESTARGET_ID = Convert.ToInt64(TempData["DetailsID"]);
                TempData.Keep();

                if (Convert.ToInt64(objdata.SALESTARGET_ID) > 0)
                {
                    DataTable objData = objdata.GETSALESTARGETASSIGNDETAILSBYID("GETHEADERSALESTARGET", objdata.SALESTARGET_ID);
                    if (objData != null && objData.Rows.Count > 0)
                    {
                        DataTable dt = objData;
                        foreach (DataRow row in dt.Rows)
                        {
                            objdata.SALESTARGET_ID = Convert.ToInt64(row["SALESTARGET_ID"]);
                            objdata.SalesTargetLevel = Convert.ToString(row["TARGETLEVELID"]);
                            objdata.SalesTargetNo = Convert.ToString(row["TARGETDOCNUMBER"]);
                            objdata.SalesTargetDate = Convert.ToDateTime(row["TARGETDATE"]);
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
            return PartialView("~/Areas/TargetVsAchievement/Views/SalesTarget/Index.cshtml", objdata);
           
        }
        public ActionResult GetProductEntryList()
        {
            SalesTargetProduct productdataobj = new SalesTargetProduct();
            List<SalesTargetProduct> productdata = new List<SalesTargetProduct>();
            Int64 DetailsID = 0;
            try
            {
                DataTable dt = new DataTable();
                if (TempData["DetailsID"] != null)
                {
                    DetailsID = Convert.ToInt64(TempData["DetailsID"]);
                    TempData.Keep();
                }
                if (DetailsID > 0 && TempData["LevelDetails"] == null)
                {
                    DataTable objData = objdata.GETSALESTARGETASSIGNDETAILSBYID("GETDETAILSSALESTARGET", DetailsID);
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
                        dtable.Columns.Add("TARGETLEVELID", typeof(System.String));
                        dtable.Columns.Add("INTERNALID", typeof(System.String));
                        dtable.Columns.Add("NEWVISIT", typeof(System.String));
                        dtable.Columns.Add("REVISIT", typeof(System.String));
                        dtable.Columns.Add("ORDERAMOUNT", typeof(System.String));
                        dtable.Columns.Add("COLLECTION", typeof(System.String));
                        dtable.Columns.Add("ORDERQTY", typeof(System.String));

                        String Gid = "";

                        foreach (DataRow row in dt.Rows)
                        {
                            Gid = Guid.NewGuid().ToString();
                            productdataobj = new SalesTargetProduct();
                            productdataobj.SlNO = Convert.ToString(row["SlNO"]);
                            //productdataobj.TARGETDOCNUMBER = Convert.ToString(row["TARGETDOCNUMBER"]);
                            productdataobj.TARGETLEVELID = Convert.ToString(row["TARGETLEVELID"]);
                            productdataobj.TARGETLEVEL = Convert.ToString(row["TARGETLEVEL"]);
                            productdataobj.INTERNALID = Convert.ToString(row["INTERNALID"]);

                            productdataobj.TIMEFRAME = Convert.ToString(row["TIMEFRAME"]);
                            productdataobj.STARTEDATE = Convert.ToString(row["STARTEDATE"]);
                            productdataobj.ENDDATE = Convert.ToString(row["ENDDATE"]);

                            productdataobj.NEWVISIT = Convert.ToString(row["NEWVISIT"]);
                            productdataobj.REVISIT = Convert.ToString(row["REVISIT"]);
                            productdataobj.ORDERAMOUNT = Convert.ToString(row["ORDERAMOUNT"]);
                            productdataobj.COLLECTION = Convert.ToString(row["COLLECTION"]);
                            productdataobj.ORDERQTY = Convert.ToString(row["ORDERQTY"]);

                            productdataobj.Guids = Gid;

                            productdata.Add(productdataobj);

                            object[] trow = { Gid, row["SlNO"] , Convert.ToString(row["TARGETLEVEL"]), Convert.ToString(row["TIMEFRAME"]),
                                        Convert.ToString(row["STARTEDATE"]), Convert.ToString(row["ENDDATE"]),
                                        Convert.ToString(row["TARGETLEVELID"]), Convert.ToString(row["INTERNALID"]),
                                        Convert.ToString(row["NEWVISIT"]), Convert.ToString(row["REVISIT"]), Convert.ToString(row["ORDERAMOUNT"]),
                                        Convert.ToString(row["COLLECTION"]), Convert.ToString(row["ORDERQTY"]) };
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
                            productdataobj = new SalesTargetProduct();
                            productdataobj.SlNO = Convert.ToString(row["SlNO"]);
                            //productdataobj.ActualSL = Convert.ToString(row["WODTARGETDETAILS_ID"]);
                            //productdataobj.TARGETDOCNUMBER = Convert.ToString(row["TARGETDOCNUMBER"]);
                            productdataobj.TARGETLEVELID = Convert.ToString(row["TARGETLEVELID"]);
                            productdataobj.TARGETLEVEL = Convert.ToString(row["TARGETLEVEL"]);
                            productdataobj.INTERNALID = Convert.ToString(row["INTERNALID"]);

                            productdataobj.TIMEFRAME = Convert.ToString(row["TIMEFRAME"]);
                            productdataobj.STARTEDATE = Convert.ToString(row["STARTEDATE"]);
                            productdataobj.ENDDATE = Convert.ToString(row["ENDDATE"]);

                            productdataobj.NEWVISIT = Convert.ToString(row["NEWVISIT"]);
                            productdataobj.REVISIT = Convert.ToString(row["REVISIT"]);
                            productdataobj.ORDERAMOUNT = Convert.ToString(row["ORDERAMOUNT"]);
                            productdataobj.COLLECTION = Convert.ToString(row["COLLECTION"]);
                            productdataobj.ORDERQTY = Convert.ToString(row["ORDERQTY"]);

                            productdataobj.Guids = Convert.ToString(row["HIddenID"]);
                            productdata.Add(productdataobj);

                        }
                    }

                   
                }
                TempData["LevelDetails"] = dt;
                TempData.Keep();
            }
            catch { }
            return PartialView("~/Areas/TargetVsAchievement/Views/SalesTarget/_PartialSalesTargetEntry.cshtml", productdata);
         
        }

        [WebMethod]
        public JsonResult SaveSalesTarget(SalesTargetModel Details)
        {
            String Message = "";
            Boolean Success = false;
            DataSet dt = new DataSet();
            DataTable dt_Details = (DataTable)TempData["LevelDetails"];
            
            List<udtSalesTarget> udt = new List<udtSalesTarget>();

            foreach (DataRow item in dt_Details.Rows)
            {
                udtSalesTarget obj1 = new udtSalesTarget();
                obj1.TARGETLEVELID = Convert.ToInt64(item["TARGETLEVELID"]);
                obj1.TARGETLEVEL = Convert.ToString(item["TARGETLEVEL"]);
                obj1.INTERNALID = Convert.ToString(item["INTERNALID"]);
                obj1.TIMEFRAME = Convert.ToString(item["TIMEFRAME"]);
                obj1.STARTEDATE = DateTime.ParseExact(Convert.ToString(item["STARTEDATE"]), "dd-MM-yyyy", null);
                obj1.ENDDATE = DateTime.ParseExact(Convert.ToString(item["ENDDATE"]), "dd-MM-yyyy", null);
                obj1.SlNO = Convert.ToString(item["SlNO"]);
                obj1.NEWVISIT = Convert.ToInt64(item["NEWVISIT"]);
                obj1.REVISIT = Convert.ToInt64(item["REVISIT"]);
                obj1.ORDERAMOUNT = Convert.ToDecimal(item["ORDERAMOUNT"]);
                obj1.COLLECTION = Convert.ToDecimal(item["COLLECTION"]);
                obj1.ORDERQTY = Convert.ToDecimal(item["ORDERQTY"]);

                udt.Add(obj1);
            }



            DataTable dtSalesTarget = new DataTable();
            dtSalesTarget = ToDataTable(udt);

            
            if (Convert.ToInt64(Details.SALESTARGET_ID) > 0 && Convert.ToInt16(TempData["IsView"]) == 0)
            {
                dt = objdata.SalesTargetEntryInsertUpdate("UPDATESALESTARGET", Convert.ToDateTime(Details.SalesTargetDate), Convert.ToInt64(Details.SALESTARGET_ID), Details.TargetType, Details.SalesTargetNo
                       , dtSalesTarget, Convert.ToInt64(Session["userid"]));
            }
            else
            {
                dt = objdata.SalesTargetEntryInsertUpdate("INSERTSALESTARGET", Convert.ToDateTime(Details.SalesTargetDate), Convert.ToInt64(Details.SALESTARGET_ID), Details.TargetType, Details.SalesTargetNo
                       , dtSalesTarget, Convert.ToInt64(Session["userid"]));

            }


            if (dt != null && dt.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dt.Tables[0].Rows)
                {
                    Success = Convert.ToBoolean(row["Success"]);
                    DetailsID = Convert.ToInt32(row["DetailsID"]);
                    SalesTargetNo = Convert.ToString(Details.SalesTargetNo);
                }
            }

            String retuenMsg = Success + "~" + DetailsID + "~" + Details.SalesTargetNo + "~" + Message;
            return Json(retuenMsg);

        }

        
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

        public JsonResult CHECKUNIQUETARGETDOCNUMBER(string SalesTargetNo, string TargetID)
        {
            
            var retData = 0;
            try
            {
                ProcedureExecute proc;
                using (proc = new ProcedureExecute("PRC_SALESTARGETASSIGN"))
                {
                    proc.AddVarcharPara("@action", 100, "CHECKUNIQUETARGETDOCNUMBER");
                    proc.AddIntegerPara("@ReturnValue", 0,QueryParameterDirection.Output);
                    proc.AddVarcharPara("@TargetNo", 100, SalesTargetNo);
                    proc.AddVarcharPara("@TARGET_ID", 100, TargetID);
                    int i = proc.RunActionQuery();
                    retData =Convert.ToInt32(proc.GetParaValue("@ReturnValue"));
                    
                }
            }
            catch { }
            return Json(retData);
        }

        [WebMethod]
        public JsonResult AddLevelDetails(SalesTargetProduct prod)
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
                dtable.Columns.Add("TARGETLEVELID", typeof(System.String));
                dtable.Columns.Add("INTERNALID", typeof(System.String));
                dtable.Columns.Add("NEWVISIT", typeof(System.String));
                dtable.Columns.Add("REVISIT", typeof(System.String));
                dtable.Columns.Add("ORDERAMOUNT", typeof(System.String));
                dtable.Columns.Add("COLLECTION", typeof(System.String));
                dtable.Columns.Add("ORDERQTY", typeof(System.String));


                object[] trow = { Guid.NewGuid(), 1, prod.TARGETLEVEL, prod.TIMEFRAME,
                        DateTime.ParseExact(prod.STARTEDATE, "yyyy-MM-dd", null).ToString("dd-MM-yyyy"),
                        DateTime.ParseExact(prod.ENDDATE, "yyyy-MM-dd", null).ToString("dd-MM-yyyy"),
                        prod.TARGETLEVELID, prod.INTERNALID,
                        prod.NEWVISIT, prod.REVISIT, prod.ORDERAMOUNT, prod.COLLECTION, prod.ORDERQTY };
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
                        prod.TARGETLEVELID, prod.INTERNALID,
                        prod.NEWVISIT, prod.REVISIT, prod.ORDERAMOUNT, prod.COLLECTION, prod.ORDERQTY};


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
                                item["TARGETLEVELID"] = prod.TARGETLEVELID;
                                item["INTERNALID"] = prod.INTERNALID;
                                item["NEWVISIT"] = prod.NEWVISIT;
                                item["REVISIT"] = prod.REVISIT;
                                item["ORDERAMOUNT"] = prod.ORDERAMOUNT;
                                item["COLLECTION"] = prod.COLLECTION;
                                item["ORDERQTY"] = prod.ORDERQTY;
                            }
                        }
                    }
                }
                TempData["LevelDetails"] = dt;
                TempData.Keep();
            }
            return Json("");
        }

        public JsonResult CHECKUNIQUETARGETDETAILS(string TargetNo, string TargetType, string TARGETLEVELID, string TARGETLEVEL, string INTERNALID, string TimeFrame, string STARTEDATE, string ENDDATE)
        {
            var retData = 0;
            try
            {
                ProcedureExecute proc;
                using (proc = new ProcedureExecute("PRC_SALESTARGETASSIGN"))
                {
                    proc.AddVarcharPara("@action", 100, "CHECKUNIQUETARGETDETAILS");
                    proc.AddIntegerPara("@ReturnValue", 0, QueryParameterDirection.Output);
                    proc.AddVarcharPara("@TargetType", 100, TargetType);
                    proc.AddVarcharPara("@TargetNo", 100, TargetNo);
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
            SalesTargetProduct ret = new SalesTargetProduct();

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
                        ret.TARGETLEVELID = item["TARGETLEVELID"].ToString();
                        ret.INTERNALID = item["INTERNALID"].ToString();
                        ret.NEWVISIT = item["NEWVISIT"].ToString();
                        ret.REVISIT = item["REVISIT"].ToString();
                        ret.ORDERAMOUNT = item["ORDERAMOUNT"].ToString();
                        ret.COLLECTION = item["COLLECTION"].ToString();
                        ret.ORDERQTY = item["ORDERQTY"].ToString();

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