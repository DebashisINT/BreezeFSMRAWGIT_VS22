#region======================================Revision History=========================================================================
//1.0   V2.0.41    Debashis     09/08/2023      A coloumn named as Gender needs to be added in all the ITC reports.Refer: 0026680
#endregion===================================End of Revision History==================================================================

using BusinessLogicLayer.SalesTrackerReports;
using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Models;
using MyShop.Models;
using SalesmanTrack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class DSVisitChannelHierarchyController : Controller
    {
        //
        // GET: /MYSHOP/DSVisitChannelHierarchy/
        SalesSummary_Report objgps = new SalesSummary_Report();
        public ActionResult DSVisitChannelHierarchy()
        {
            try
            {
                UserList lstuser = new UserList();
                DSVisitChannelHierarchy omodel = new DSVisitChannelHierarchy();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                DataTable dtbranch = lstuser.GetHeadBranchList(Convert.ToString(Session["userbranchHierarchy"]), "HO");
                DataTable dtBranchChild = new DataTable();
                if (dtbranch.Rows.Count > 0)
                {
                    dtBranchChild = lstuser.GetChildBranch(Convert.ToString(Session["userbranchHierarchy"]));
                    if (dtBranchChild.Rows.Count > 0)
                    {
                        DataRow dr;
                        dr = dtbranch.NewRow();
                        dr[0] = 0;
                        dr[1] = "All";
                        dtbranch.Rows.Add(dr);
                        dtbranch.DefaultView.Sort = "BRANCH_ID ASC";
                        dtbranch = dtbranch.DefaultView.ToTable();
                    }
                }
                omodel.modelbranch = APIHelperMethods.ToModelList<GetBranchDSCH>(dtbranch);
                string h_id = omodel.modelbranch.First().BRANCH_ID.ToString();
                ViewBag.HeadBranch = omodel.modelbranch;
                ViewBag.h_id = h_id;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                ds.Tables.Add(dt);
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                return View(ds);
                //return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetDSVisitChannelHierarchyList(DSVisitChannelHierarchy model)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<DSVisitChannelHierarchyModel> omel = new List<DSVisitChannelHierarchyModel>();

                DataTable dt = new DataTable();

                string Employee = "";
                int i = 1;

                if (model.empcode != null && model.empcode.Count > 0)
                {
                    foreach (string item in model.empcode)
                    {
                        if (i > 1)
                            Employee = Employee + "," + item;
                        else
                            Employee = item;
                        i++;
                    }
                }

                string Branch_Id = "";
                int j = 1;
                if (model.BranchId != null && model.BranchId.Count > 0)
                {
                    foreach (string item in model.BranchId)
                    {
                        if (j > 1)
                            Branch_Id = Branch_Id + "," + item;
                        else
                            Branch_Id = item;
                        j++;
                    }
                }

                string Channel_Id = "";
                int k = 1;
                if (model.ChannelId != null && model.ChannelId.Count > 0)
                {
                    foreach (string item in model.ChannelId)
                    {
                        if (k > 1)
                            Channel_Id = Channel_Id + "," + item;
                        else
                            Channel_Id = item;
                        k++;
                    }
                }

                string Is_PageLoad = string.Empty;

                if (model.is_pageload == "0")
                {
                    Is_PageLoad = "is_pageload";
                    model.Fromdate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    model.Todate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {

                    if (model.is_pageload == "0")
                    {
                        model.Fromdate = model.Fromdate.Split('-')[0] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[2];
                    }
                    else
                    {
                        model.Fromdate = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                    }

                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }
                else
                {

                    if (model.is_pageload == "0")
                    {
                        model.Todate = model.Todate.Split('-')[0] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[2];
                    }
                    else
                    {
                        model.Todate = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                    }

                }

                string FromDate = model.Fromdate;
                string ToDate = model.Todate;

                if (model.is_pageload == "1")
                {

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        omel = APIHelperMethods.ToModelList<DSVisitChannelHierarchyModel>(dt);
                        TempData["ExportDSVisitChannelHierarchy"] = omel;
                        TempData.Keep();
                    }
                    else
                    {
                        TempData["ExportDSVisitChannelHierarchy"] = null;
                        TempData.Keep();
                    }

                }
                if (model.is_pageload == "1")
                {
                    double days = (Convert.ToDateTime(ToDate) - Convert.ToDateTime(FromDate)).TotalDays;
                    if (days <= 35)
                    {
                        dt = GetDSVisitChannelHierarchy(Employee, FromDate, ToDate, Branch_Id,Channel_Id);
                    }
                }

                return PartialView("_PartialGridDSVisitChannelHierarchy", LGetDSVisitChannelHierarchy(Is_PageLoad));
            }
            catch
            {
                //   return Redirect("~/OMS/Signoff.aspx");
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

        }

        public DataTable GetDSVisitChannelHierarchy(string Employee, string start_date, string end_date, string Branch_Id, string Channel_Id)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDSVISITDETAILSHIERARCHYCHANNELWISE_REPORT");
            proc.AddPara("@FROMDATE", start_date);
            proc.AddPara("@TODATE", end_date);
            proc.AddPara("@BRANCHID", Branch_Id);
            proc.AddPara("@EMPID", Employee);
            proc.AddPara("@CHANNELID", Channel_Id);
            proc.AddPara("@USERID", Convert.ToInt32(Session["userid"]));
            ds = proc.GetTable();
            return ds;
        }

        public IEnumerable LGetDSVisitChannelHierarchy(string Is_PageLoad)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "DS Visit - Hierarchy & Channel Wise");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "is_pageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDSVISITDETAILSHIERARCHYCHANNELWISE_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDSVISITDETAILSHIERARCHYCHANNELWISE_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult ExportDSVisitChannelHierarchyList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDSSummaryGridViewSettings(), LGetDSVisitChannelHierarchy(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDSSummaryGridViewSettings(), LGetDSVisitChannelHierarchy(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetDSSummaryGridViewSettings(), LGetDSVisitChannelHierarchy(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDSSummaryGridViewSettings(), LGetDSVisitChannelHierarchy(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDSSummaryGridViewSettings(), LGetDSVisitChannelHierarchy(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetDSSummaryGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "DS Visit - Hierarchy & Channel Wise");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }
            var settings = new GridViewSettings();
            settings.Name = "DSVisitHierarchyChannelWise";
            settings.CallbackRouteValues = new { Controller = "DSVisitChannelHierarchy", Action = "GetDSVisitChannelHierarchy" };

            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "DSVisitHierarchyChannelWise";

            settings.Columns.Add(x =>
            {
                x.FieldName = "BRANCHDESC";
                x.Caption = "Branch";
                x.VisibleIndex = 1;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BRANCHDESC'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 180;
                }

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "REPORTTOUIDAE";
                x.Caption = "AE ID";
                x.VisibleIndex = 2;
                x.PropertiesEdit.DisplayFormatString = "0.00";

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REPORTTOUIDAE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 180;
                }

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "REPORTTOUIDWD";
                x.Caption = "WD ID";
                x.VisibleIndex = 3;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REPORTTOUIDWD'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 180;
                }

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPID";
                x.Caption = "DS ID";
                x.VisibleIndex = 4;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPID'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 120;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                x.Caption = "DS Name";
                x.VisibleIndex = 5;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPNAME'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 180;
                }
            });

            //Rev 1.0 Mantis: 0026680
            settings.Columns.Add(x =>
            {
                x.FieldName = "GENDERDESC";
                x.Caption = "Gender";
                x.VisibleIndex = 6;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='GENDERDESC'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 100;
                }
            });
            //End of Rev 1.0 Mantis: 0026680

            settings.Columns.Add(x =>
            {
                x.FieldName = "DSTYPE";
                x.Caption = "DS Type";
                x.VisibleIndex = 7;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DSTYPE'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 180;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CHANNEL";
                x.Caption = "Channel";
                x.VisibleIndex = 8;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CHANNEL'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 180;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SECTION";
                x.Caption = "Section";
                x.VisibleIndex = 9;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SECTION'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 180;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "CIRCLE";
                x.Caption = "Circle";
                x.VisibleIndex = 10;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CIRCLE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 180;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "LOGIN_DATETIME";
                x.Caption = "Visit Date";
                x.VisibleIndex = 11;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='LOGIN_DATETIME'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 150;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 150;
                }

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OUTLETSMAPPED";
                x.Caption = "Outlets Mapped(Added)";
                x.VisibleIndex = 12;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='OUTLETSMAPPED'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 140;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 140;
                }
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });


            settings.Columns.Add(x =>
            {
                x.FieldName = "RE_VISITED";
                x.Caption = "Outlets Re-Visited";
                x.VisibleIndex = 13;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RE_VISITED'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 120;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 120;
                }
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DISTANCE_TRAVELLED";
                x.Caption = "Distance Travelled(Km.Mtr)";
                x.VisibleIndex = 14;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DISTANCE_TRAVELLED'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 180;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 180;
                }
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "AVGTIMESPENTINMARKET";
                x.Caption = "Total time spent in the market(HH:MM)";
                x.VisibleIndex = 15;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='AVGTIMESPENTINMARKET'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 220;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 220;
                }
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DAYSTTIME";
                x.Caption = "Day Start(HH:MM)";
                x.VisibleIndex = 16;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DAYSTTIME'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 130;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 130;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DAYENDTIME";
                x.Caption = "Day End(HH:MM)";
                x.VisibleIndex = 17;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DAYENDTIME'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 130;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 130;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "AVGSPENTDURATION";
                x.Caption = "Avg time spent in OL(CFT-New&Revisit)(HH:MM)";
                x.VisibleIndex = 18;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='AVGSPENTDURATION'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 320;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 320;
                }
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult PageRetention(List<String> Columns)
        {
            try
            {
                String Col = "";
                int i = 1;
                if (Columns != null && Columns.Count > 0)
                {
                    Col = string.Join(",", Columns);
                }
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "DS VISIT DETAILS");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
	}
}