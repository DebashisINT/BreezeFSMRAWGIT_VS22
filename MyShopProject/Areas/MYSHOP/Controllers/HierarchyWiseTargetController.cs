using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class HierarchyWiseTargetController : Controller
    {
        TargetVsAchivementBL tarBL = new TargetVsAchivementBL();
        BusinessLogicLayer.DBEngine objdb = new BusinessLogicLayer.DBEngine();
        //
        // GET: /MYSHOP/HierarchyWiseTarget/
        public ActionResult HierarchyWiseTarget()
        {
            string userid = Session["userid"].ToString();
            DataTable statedt = tarBL.GetSummaryList("GETSTATE", "", "", Convert.ToInt32(userid));

            List<MonthList> month = new List<MonthList>();
            month.Add(new MonthList { ID = "JAN", MonthName = "January" });
            month.Add(new MonthList { ID = "FEB", MonthName = "February" });
            month.Add(new MonthList { ID = "MAR", MonthName = "March" });
            month.Add(new MonthList { ID = "APR", MonthName = "April" });
            month.Add(new MonthList { ID = "MAY", MonthName = "May" });
            month.Add(new MonthList { ID = "JUN", MonthName = "June" });
            month.Add(new MonthList { ID = "JUL", MonthName = "July" });
            month.Add(new MonthList { ID = "AUG", MonthName = "August" });
            month.Add(new MonthList { ID = "SEP", MonthName = "September" });
            month.Add(new MonthList { ID = "OCT", MonthName = "October" });
            month.Add(new MonthList { ID = "NOV", MonthName = "November" });
            month.Add(new MonthList { ID = "DEC", MonthName = "December" });

            List<YearList> year = new List<YearList>();
            DataTable dtyr = tarBL.GetYearList();
            if (dtyr!=null && dtyr.Rows.Count>0)
            {
                foreach (DataRow item in dtyr.Rows)
                {
                    year.Add(new YearList { 
                        ID = Convert.ToString(item["YEARS"]),
                        YearName = Convert.ToString(item["YEARS"]) 
                    });
                }
            }

           
            //DateTime dtnow = new DateTime();
            //dtnow = DateTime.Now;
            //year.Add(new YearList { ID = Convert.ToString(dtnow.Year - 3), YearName = Convert.ToString(dtnow.Year - 3) });
            //year.Add(new YearList { ID = Convert.ToString(dtnow.Year - 2), YearName = Convert.ToString(dtnow.Year - 2) });
            //year.Add(new YearList { ID = Convert.ToString(dtnow.Year - 1), YearName = Convert.ToString(dtnow.Year - 1) });
            //year.Add(new YearList { ID = Convert.ToString(dtnow.Year), YearName = Convert.ToString(dtnow.Year) });
            //year.Add(new YearList { ID = Convert.ToString(dtnow.Year + 1), YearName = Convert.ToString(dtnow.Year + 1) });


            HierarchyWiseTargetClass model = new HierarchyWiseTargetClass();
            // model.TargetVsAchivementList = APIHelperMethods.ToModelList<TargetVsAchivementList>(tardt);
            model.FirstLevelGrid = new List<TargetVsAchivementList>();
            model.SecondLevelGrid = new List<TargetVsAchivementList>();
            model.ThirdLevelGrid = new List<TargetVsAchivementList>();
            model.FourthLevelGrid = new List<TargetVsAchivementList>();
            model.FifthLevelGrid = new List<TargetVsAchivementList>();
            model.SixthLevelGrid = new List<TargetVsAchivementList>();
            model.SeventhLevelGrid = new List<TargetVsAchivementList>();
            model.PPDDShopList = new List<PPDDShopList>();
            model.StateListTarget = APIHelperMethods.ToModelList<StateListTarget>(statedt);
            model.MonthList = month;
            model.YearList = year;
            return View(model);
        }
        public PartialViewResult FirstLevelGrid(string StateId, string Month,String Year)
        {
            tarBL.CreateTable(Month, StateId, Year);

            DataTable Dt = new DataTable();
            if(Convert.ToString(Session["userid"])=="378")
            {
               Dt = objdb.GetDataTable("select EMPCODE,EMPNAME,CONTACTNO,STATE,DESIGNATION,RPTTOEMPCODE,TGT_NC,ACHV_NC,TGT_RV,ACHV_RV,TGT_ORDERVALUE,ACHV_ORDERVALUE,TGT_COLLECTION,ACHV_COLLECTION,REPORTTO from FTSTARGETVSACHIEVEMENTSUMMARY_REPORT  where RPTTOUSRID='" + Convert.ToString(Session["userid"]) + "' AND EMPCODE is not NULL and USERID='" + Convert.ToString(Session["userid"]) + "'");
            
            }
            else
            {
                Dt = objdb.GetDataTable("select EMPCODE,EMPNAME,CONTACTNO,STATE,DESIGNATION,RPTTOEMPCODE,TGT_NC,ACHV_NC,TGT_RV,ACHV_RV,TGT_ORDERVALUE,ACHV_ORDERVALUE,TGT_COLLECTION,ACHV_COLLECTION,REPORTTO from FTSTARGETVSACHIEVEMENTSUMMARY_REPORT  where EMPUSRID='" + Convert.ToString(Session["userid"]) + "' AND EMPCODE is not NULL and USERID='" + Convert.ToString(Session["userid"]) + "'");
                
            }

            List<TargetVsAchivementList> First = APIHelperMethods.ToModelList<TargetVsAchivementList>(Dt);

            TempData["First"] = First;
            TempData.Keep();
            return PartialView(First);
        }
        public PartialViewResult SecondLevelGrid(string EMPCODE)
        {
            List<TargetVsAchivementList> Second = new List<TargetVsAchivementList>();
            DataTable Dt = objdb.GetDataTable("select EMPCODE,EMPNAME,CONTACTNO,STATE,DESIGNATION,RPTTOEMPCODE,TGT_NC,ACHV_NC,TGT_RV,ACHV_RV,TGT_ORDERVALUE,ACHV_ORDERVALUE,TGT_COLLECTION,ACHV_COLLECTION,REPORTTO from FTSTARGETVSACHIEVEMENTSUMMARY_REPORT  where RPTTOEMPCODE='" + EMPCODE + "' and EMPCODE is not NULL and USERID='" + Convert.ToString(Session["userid"]) + "'");

            Second = APIHelperMethods.ToModelList<TargetVsAchivementList>(Dt);
            TempData["Second"] = Second;
            TempData.Keep();
            return PartialView(Second);
        }
        public PartialViewResult ThirdLevelGrid(string EMPCODE)
        {
            List<TargetVsAchivementList> Third = new List<TargetVsAchivementList>();
            DataTable Dt = objdb.GetDataTable("select EMPCODE,EMPNAME,CONTACTNO,STATE,DESIGNATION,RPTTOEMPCODE,TGT_NC,ACHV_NC,TGT_RV,ACHV_RV,TGT_ORDERVALUE,ACHV_ORDERVALUE,TGT_COLLECTION,ACHV_COLLECTION,REPORTTO from FTSTARGETVSACHIEVEMENTSUMMARY_REPORT  where RPTTOEMPCODE='" + EMPCODE + "' and EMPCODE is not NULL and USERID='" + Convert.ToString(Session["userid"]) + "'");

            Third = APIHelperMethods.ToModelList<TargetVsAchivementList>(Dt);
            TempData["Third"] = Third;
            TempData.Keep();
            return PartialView(Third);
        }
        public PartialViewResult FourthLevelGrid(string EMPCODE)
        {
            List<TargetVsAchivementList> Fourth = new List<TargetVsAchivementList>();
            DataTable Dt = objdb.GetDataTable("select EMPCODE,EMPNAME,CONTACTNO,STATE,DESIGNATION,RPTTOEMPCODE,TGT_NC,ACHV_NC,TGT_RV,ACHV_RV,TGT_ORDERVALUE,ACHV_ORDERVALUE,TGT_COLLECTION,ACHV_COLLECTION,REPORTTO from FTSTARGETVSACHIEVEMENTSUMMARY_REPORT  where RPTTOEMPCODE='" + EMPCODE + "' and EMPCODE is not NULL and USERID='" + Convert.ToString(Session["userid"]) + "'");

            Fourth = APIHelperMethods.ToModelList<TargetVsAchivementList>(Dt);
            TempData["Fourth"] = Fourth;
            TempData.Keep();
            return PartialView(Fourth);
        }
        public PartialViewResult FifthLevelGrid(string EMPCODE)
        {
            List<TargetVsAchivementList> Fifth = new List<TargetVsAchivementList>();
            DataTable Dt = objdb.GetDataTable("select EMPCODE,EMPNAME,CONTACTNO,STATE,DESIGNATION,RPTTOEMPCODE,TGT_NC,ACHV_NC,TGT_RV,ACHV_RV,TGT_ORDERVALUE,ACHV_ORDERVALUE,TGT_COLLECTION,ACHV_COLLECTION,REPORTTO from FTSTARGETVSACHIEVEMENTSUMMARY_REPORT  where RPTTOEMPCODE='" + EMPCODE + "' and EMPCODE is not NULL and USERID='" + Convert.ToString(Session["userid"]) + "'");

            Fifth = APIHelperMethods.ToModelList<TargetVsAchivementList>(Dt);
            TempData["Fifth"] = Fifth;
            TempData.Keep();
            return PartialView(Fifth);
        }
        public PartialViewResult SixthLevelGrid(string EMPCODE)
        {
            List<TargetVsAchivementList> Sixth = new List<TargetVsAchivementList>();
            DataTable Dt = objdb.GetDataTable("select EMPCODE,EMPNAME,CONTACTNO,STATE,DESIGNATION,RPTTOEMPCODE,TGT_NC,ACHV_NC,TGT_RV,ACHV_RV,TGT_ORDERVALUE,ACHV_ORDERVALUE,TGT_COLLECTION,ACHV_COLLECTION,REPORTTO from FTSTARGETVSACHIEVEMENTSUMMARY_REPORT  where RPTTOEMPCODE='" + EMPCODE + "' and EMPCODE is not NULL and USERID='" + Convert.ToString(Session["userid"]) + "'");

            Sixth = APIHelperMethods.ToModelList<TargetVsAchivementList>(Dt);
            TempData["Sixth"] = Sixth;
            TempData.Keep();
            return PartialView(Sixth);
        }
        public PartialViewResult SeventhLevelGrid(string EMPCODE)
        {


            List<TargetVsAchivementList> Seventh = new List<TargetVsAchivementList>();
            DataTable Dt = objdb.GetDataTable("select EMPCODE,EMPNAME,CONTACTNO,STATE,DESIGNATION,RPTTOEMPCODE,TGT_NC,ACHV_NC,TGT_RV,ACHV_RV,TGT_ORDERVALUE,ACHV_ORDERVALUE,TGT_COLLECTION,ACHV_COLLECTION,REPORTTO from FTSTARGETVSACHIEVEMENTSUMMARY_REPORT  where RPTTOEMPCODE='" + EMPCODE + "' and EMPCODE is not NULL and USERID='" + Convert.ToString(Session["userid"]) + "'");

            Seventh = APIHelperMethods.ToModelList<TargetVsAchivementList>(Dt);
            TempData["Seventh"] = Seventh;
            TempData.Keep();
            return PartialView(Seventh);
        }

        public PartialViewResult PPDDGrid(string EMPCODE, string Type, string Month, string Year)
        {


            List<PPDDShopList> PPDDShopList = new List<PPDDShopList>();
            DataTable dt = tarBL.GeneratePPDDList(Month, EMPCODE, Convert.ToString(Session["userid"]), Year);

            DataTable dtOutput = objdb.GetDataTable("select EMPNAME,CONTACTNO,SHOP_NAME,SHOP_TYPE,TGT_ORDERVALUE,ACHV_ORDERVALUE,TGT_COLLECTION,ACHV_COLLECTION,STATE from FTSTARGETVSACHIEVEMENTDETAILS_REPORT where USERID='" + Convert.ToString(Session["userid"]) + "' and  SHOP_TYPE IN ('PP','DD')");

            PPDDShopList = APIHelperMethods.ToModelList<PPDDShopList>(dtOutput);
            TempData["PPDDShopList"] = PPDDShopList;
            TempData.Keep();
            return PartialView(PPDDShopList);
        }

        public ActionResult ExportToExcel()
        {
            var ps = new PrintingSystemBase();

            var link1 = new PrintableComponentLinkBase(ps);
            GridViewSettings setting1 = new GridViewSettings();
            setting1.Name = "grid1";
            setting1.Columns.Add("REPORTTO", "Supervisor").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting1.Columns.Add("EMPNAME", "Employee").Width = System.Web.UI.WebControls.Unit.Pixel(170);
            setting1.Columns.Add("CONTACTNO", "Login ID.").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting1.Columns.Add("DESIGNATION", "Designation").Width = System.Web.UI.WebControls.Unit.Pixel(150);

            setting1.Styles.Row.Cursor = "pointer";
            setting1.Columns.Add(x =>
            {
                x.FieldName = "STATE";
                x.Caption = "State";
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            setting1.Columns.Add("TGT_NC", "Tgt. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting1.Columns.Add("ACHV_NC", "Achv. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting1.Columns.Add("TGT_RV", "Tgt. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting1.Columns.Add("ACHV_RV", "Achv. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);

            setting1.Columns.Add("TGT_ORDERVALUE", "Tgt. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting1.Columns.Add("ACHV_ORDERVALUE", "Achv. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting1.Columns.Add("TGT_COLLECTION", "Tgt. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting1.Columns.Add("ACHV_COLLECTION", "Achv. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);

            link1.Component = GridViewExtension.CreatePrintableObject(setting1, (List<TargetVsAchivementList>)TempData["First"]);

            var link2 = new PrintableComponentLinkBase(ps);


            GridViewSettings setting2 = new GridViewSettings();
            setting2.Name = "grid2";

            setting2.Columns.Add("REPORTTO", "Supervisor").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting2.Columns.Add("EMPNAME", "Employee").Width = System.Web.UI.WebControls.Unit.Pixel(170);
            setting2.Columns.Add("CONTACTNO", "Login ID.").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting2.Columns.Add("DESIGNATION", "Designation").Width = System.Web.UI.WebControls.Unit.Pixel(150);

            setting2.Styles.Row.Cursor = "pointer";
            setting2.Columns.Add(x =>
            {
                x.FieldName = "STATE";
                x.Caption = "State";
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            setting2.Columns.Add("TGT_NC", "Tgt. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting2.Columns.Add("ACHV_NC", "Achv. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting2.Columns.Add("TGT_RV", "Tgt. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting2.Columns.Add("ACHV_RV", "Achv. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);

            setting2.Columns.Add("TGT_ORDERVALUE", "Tgt. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting2.Columns.Add("ACHV_ORDERVALUE", "Achv. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting2.Columns.Add("TGT_COLLECTION", "Tgt. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting2.Columns.Add("ACHV_COLLECTION", "Achv. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);


            link2.Component = GridViewExtension.CreatePrintableObject(setting2, (List<TargetVsAchivementList>)TempData["Second"]);


            var link3 = new PrintableComponentLinkBase(ps);


            GridViewSettings setting3 = new GridViewSettings();
            setting3.Name = "grid3";

            setting3.Columns.Add("REPORTTO", "Supervisor").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting3.Columns.Add("EMPNAME", "Employee").Width = System.Web.UI.WebControls.Unit.Pixel(170);
            setting3.Columns.Add("CONTACTNO", "Login ID.").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting3.Columns.Add("DESIGNATION", "Designation").Width = System.Web.UI.WebControls.Unit.Pixel(150);

            setting3.Styles.Row.Cursor = "pointer";
            setting3.Columns.Add(x =>
            {
                x.FieldName = "STATE";
                x.Caption = "State";
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            setting3.Columns.Add("TGT_NC", "Tgt. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting3.Columns.Add("ACHV_NC", "Achv. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting3.Columns.Add("TGT_RV", "Tgt. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting3.Columns.Add("ACHV_RV", "Achv. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);

            setting3.Columns.Add("TGT_ORDERVALUE", "Tgt. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting3.Columns.Add("ACHV_ORDERVALUE", "Achv. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting3.Columns.Add("TGT_COLLECTION", "Tgt. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting3.Columns.Add("ACHV_COLLECTION", "Achv. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);


            link3.Component = GridViewExtension.CreatePrintableObject(setting3, (List<TargetVsAchivementList>)TempData["Third"]);


            var link4 = new PrintableComponentLinkBase(ps);


            GridViewSettings setting4 = new GridViewSettings();
            setting4.Name = "grid4";

            setting4.Columns.Add("REPORTTO", "Supervisor").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting4.Columns.Add("EMPNAME", "Employee").Width = System.Web.UI.WebControls.Unit.Pixel(170);
            setting4.Columns.Add("CONTACTNO", "Login ID.").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting4.Columns.Add("DESIGNATION", "Designation").Width = System.Web.UI.WebControls.Unit.Pixel(150);

            setting4.Styles.Row.Cursor = "pointer";
            setting4.Columns.Add(x =>
            {
                x.FieldName = "STATE";
                x.Caption = "State";
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            setting4.Columns.Add("TGT_NC", "Tgt. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting4.Columns.Add("ACHV_NC", "Achv. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting4.Columns.Add("TGT_RV", "Tgt. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting4.Columns.Add("ACHV_RV", "Achv. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);

            setting4.Columns.Add("TGT_ORDERVALUE", "Tgt. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting4.Columns.Add("ACHV_ORDERVALUE", "Achv. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting4.Columns.Add("TGT_COLLECTION", "Tgt. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting4.Columns.Add("ACHV_COLLECTION", "Achv. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);


            link4.Component = GridViewExtension.CreatePrintableObject(setting4, (List<TargetVsAchivementList>)TempData["Fourth"]);



            var link5 = new PrintableComponentLinkBase(ps);


            GridViewSettings setting5 = new GridViewSettings();

            setting5.Name = "grid5";

            setting5.Columns.Add("REPORTTO", "Supervisor").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting5.Columns.Add("EMPNAME", "Employee").Width = System.Web.UI.WebControls.Unit.Pixel(170);
            setting5.Columns.Add("CONTACTNO", "Login ID.").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting5.Columns.Add("DESIGNATION", "Designation").Width = System.Web.UI.WebControls.Unit.Pixel(150);

            setting5.Styles.Row.Cursor = "pointer";
            setting5.Columns.Add(x =>
            {
                x.FieldName = "STATE";
                x.Caption = "State";
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            setting5.Columns.Add("TGT_NC", "Tgt. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting5.Columns.Add("ACHV_NC", "Achv. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting5.Columns.Add("TGT_RV", "Tgt. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting5.Columns.Add("ACHV_RV", "Achv. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);

            setting5.Columns.Add("TGT_ORDERVALUE", "Tgt. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting5.Columns.Add("ACHV_ORDERVALUE", "Achv. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting5.Columns.Add("TGT_COLLECTION", "Tgt. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting5.Columns.Add("ACHV_COLLECTION", "Achv. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);


            link5.Component = GridViewExtension.CreatePrintableObject(setting5, (List<TargetVsAchivementList>)TempData["Fifth"]);


            var link6 = new PrintableComponentLinkBase(ps);


            GridViewSettings setting6 = new GridViewSettings();

            setting6.Name = "grid6";

            setting6.Columns.Add("REPORTTO", "Supervisor").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting6.Columns.Add("EMPNAME", "Employee").Width = System.Web.UI.WebControls.Unit.Pixel(170);
            setting6.Columns.Add("CONTACTNO", "Login ID.").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting6.Columns.Add("DESIGNATION", "Designation").Width = System.Web.UI.WebControls.Unit.Pixel(150);

            setting6.Styles.Row.Cursor = "pointer";
            setting6.Columns.Add(x =>
            {
                x.FieldName = "STATE";
                x.Caption = "State";
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            setting6.Columns.Add("TGT_NC", "Tgt. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting6.Columns.Add("ACHV_NC", "Achv. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting6.Columns.Add("TGT_RV", "Tgt. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting6.Columns.Add("ACHV_RV", "Achv. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);

            setting6.Columns.Add("TGT_ORDERVALUE", "Tgt. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting6.Columns.Add("ACHV_ORDERVALUE", "Achv. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting6.Columns.Add("TGT_COLLECTION", "Tgt. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting6.Columns.Add("ACHV_COLLECTION", "Achv. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);


            link6.Component = GridViewExtension.CreatePrintableObject(setting6, (List<TargetVsAchivementList>)TempData["Sixth"]);



            var link7 = new PrintableComponentLinkBase(ps);


            GridViewSettings setting7 = new GridViewSettings();

            setting7.Name = "grid7";


            setting7.Columns.Add("REPORTTO", "Supervisor").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting7.Columns.Add("EMPNAME", "Employee").Width = System.Web.UI.WebControls.Unit.Pixel(170);
            setting7.Columns.Add("CONTACTNO", "Login ID.").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting7.Columns.Add("DESIGNATION", "Designation").Width = System.Web.UI.WebControls.Unit.Pixel(150);

            setting7.Styles.Row.Cursor = "pointer";
            setting7.Columns.Add(x =>
            {
                x.FieldName = "STATE";
                x.Caption = "State";
                x.Width = System.Web.UI.WebControls.Unit.Pixel(100);
            });

            setting7.Columns.Add("TGT_NC", "Tgt. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting7.Columns.Add("ACHV_NC", "Achv. NC").Width = System.Web.UI.WebControls.Unit.Pixel(50);
            setting7.Columns.Add("TGT_RV", "Tgt. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting7.Columns.Add("ACHV_RV", "Achv. Re-Visit").Width = System.Web.UI.WebControls.Unit.Pixel(100);

            setting7.Columns.Add("TGT_ORDERVALUE", "Tgt. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting7.Columns.Add("ACHV_ORDERVALUE", "Achv. Value").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting7.Columns.Add("TGT_COLLECTION", "Tgt. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);
            setting7.Columns.Add("ACHV_COLLECTION", "Achv. Collection").Width = System.Web.UI.WebControls.Unit.Pixel(100);


            link7.Component = GridViewExtension.CreatePrintableObject(setting7, (List<TargetVsAchivementList>)TempData["Fifth"]);


            var compositeLink = new CompositeLinkBase(ps);
            compositeLink.Links.AddRange(new object[] { link1, link2, link3, link4, link5, link6, link7 });
            compositeLink.CreateDocument();

            FileStreamResult result = CreateExcelExportResult(compositeLink);
            ps.Dispose();


            return result;
        }

        FileStreamResult CreateExcelExportResult(CompositeLinkBase link)
        {
            MemoryStream stream = new MemoryStream();
            link.PrintingSystemBase.ExportToXls(stream);
            stream.Position = 0;
            FileStreamResult result = new FileStreamResult(stream, "application/xls");
            result.FileDownloadName = "MonthWiseTargetvsAchievement.xls";
            return result;
        }


        public void ToFile(FileResult fileResult, string fileName)
        {
            if (fileResult is FileContentResult)
            {
                System.IO.File.WriteAllBytes(fileName, ((FileContentResult)fileResult).FileContents);
            }
            else if (fileResult is FilePathResult)
            {
                System.IO.File.Copy(((FilePathResult)fileResult).FileName, fileName, true); //overwrite file if it already exists
            }
            else if (fileResult is FileStreamResult)
            {
                //from http://stackoverflow.com/questions/411592/how-do-i-save-a-stream-to-a-file-in-c
                using (var fileStream = System.IO.File.Create(fileName))
                {
                    var fileStreamResult = (FileStreamResult)fileResult;
                    fileStreamResult.FileStream.Seek(0, SeekOrigin.Begin);
                    fileStreamResult.FileStream.CopyTo(fileStream);
                    fileStreamResult.FileStream.Seek(0, SeekOrigin.Begin); //reset position to beginning. If there's any chance the FileResult will be used by a future method, this will ensure it gets left in a usable state - Suggestion by Steven Liekens
                }
            }
            else
            {
                throw new ArgumentException("Unsupported FileResult type");
            }
        }
    }
}