using BusinessLogicLayer.SalesmanTrack;
using MyShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class MasterDynamicFormController : Controller
    {
        //
        // GET: /MYSHOP/MasterDynamicForm/
        public ActionResult Master(string key)
        {
            Session["Key"] = key;
            if (Session["DynamicList"] == null)
            {
                DataTable dt = new DataTable();
                DynamicLayout obj = new DynamicLayout();
                dt = obj.GetLayout(key);
                Session["DynamicList"] = dt;
            }
            return View();
        }

        public PartialViewResult _partialGrid()
        {
            DataTable dt = new DataTable();
            if (Session["DynamicList"] != null)
            {
                dt = (DataTable)Session["DynamicList"];
            }
            else
            {
                dt.Columns.Add("SLNO", typeof(int));
                dt.Columns.Add("TYPE", typeof(string));
                dt.Columns.Add("HEADER", typeof(string));
                dt.Columns.Add("DESCRIPTION", typeof(string));
                dt.Columns.Add("ITEM", typeof(string));
                dt.Columns.Add("MAX_LENGTH", typeof(string));
                dt.Columns.Add("DATATYPE", typeof(string));
                Session["DynamicList"] = dt;
            }
            return PartialView(dt);
        }

        public PartialViewResult _partialGridList()
        {
            return PartialView(GetList());
        }

        public JsonResult save(string type, string header, string description, string items, string Lenght, string datatype)
        {
            int sl = 1;
            string output = "";
            DataTable dt = new DataTable();
            if (Session["DynamicList"] != null)
            {
                dt = (DataTable)Session["DynamicList"];
            }
            else
            {
                dt.Columns.Add("SLNO", typeof(int));
                dt.Columns.Add("TYPE", typeof(string));
                dt.Columns.Add("HEADER", typeof(string));
                dt.Columns.Add("DESCRIPTION", typeof(string));
                dt.Columns.Add("ITEM", typeof(string));
                dt.Columns.Add("MAX_LENGTH", typeof(string));
                dt.Columns.Add("DATATYPE", typeof(string));
                Session["DynamicList"] = dt;
            }

            if (dt.Rows.Count > 0)
            {
                var max = (from row in dt.AsEnumerable()
                           select row.Field<string>("SLNO")).Max().ToString();


                if (!string.IsNullOrEmpty(max))
                {
                    sl = Convert.ToInt32(max) + 1;
                }

            }
            dt.Rows.Add(sl, type, header, description, items, Lenght, datatype);
            Session["DynamicList"] = dt;


            return Json(output);

        }

        public ActionResult MasterList()
        {

            return View();
        }

        public JsonResult saveLayout(string name, string desc)
        {
            DynamicLayout obj = new DynamicLayout();
            string output = obj.SaveLayout(name, desc);
            return Json(output);

        }

        public IEnumerable GetList()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            ReportsDataContext dc = new ReportsDataContext(connectionString);
            var q = from d in dc.MASTER_DYNAMICLAYOUT1s
                    orderby d.ID descending
                    select d;
            return q;

        }

        public JsonResult Finalsave()
        {
            int sl = 1;

            DataTable dt = new DataTable { TableName = "FinalTable" };
            //dt.TableName = "FinalTable";
            if (Session["DynamicList"] != null)
            {
                dt = (DataTable)Session["DynamicList"];
            }
            dt.TableName = "FinalTable";
            string result;
            using (StringWriter sw = new StringWriter())
            {
                dt.WriteXml(sw);
                result = sw.ToString();
            }

            DynamicLayout obj = new DynamicLayout();
            string output = obj.SaveDetails(Convert.ToString(Session["Key"]), result);


            return Json(output);

        }

    }
}