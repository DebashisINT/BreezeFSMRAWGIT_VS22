using DataAccessLayer;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using Models;
using MyShop.Models;
using SalesmanTrack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class TeamVisitChannelHierarchyController : Controller
    {
        //
        // GET: /MYSHOP/TeamVisitChannelHierarchy/
        public ActionResult TeamVisitChannelHierarchy()
        {
            try
            {
                UserList lstuser = new UserList();
                TeamVisitChannelHierarchyModel omodel = new TeamVisitChannelHierarchyModel();
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
                omodel.modelbranch = APIHelperMethods.ToModelList<GetBranchTVCH>(dtbranch);
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
        public PartialViewResult TeamVisitChannelHierarchyGridViewCallback(TeamVisitChannelHierarchyModel model)
        {

            string frmdate = string.Empty;
            String Path = System.Configuration.ConfigurationSettings.AppSettings["Path"];
            String weburl = Path + "AttendanceImageDemo/";

            if (model.is_pageload == "0")
            {
                frmdate = "Ispageload";
            }

            if (model.Fromdate == null)
            {
                model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
            }

            if (model.Todate == null)
            {
                model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
            }


            string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
            string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
            string Userid = Convert.ToString(Session["userid"]);

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

            DataSet ds = new DataSet();
            if (model.is_pageload != "0" && model.is_pageload != null)
            {
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 35)
                {
                    ProcedureExecute proc = new ProcedureExecute("PRC_FTSTEAMVISITATTENDANCEHIERARCHYCHANNELWISE_REPORT");
                    proc.AddPara("@FROMDATE", datfrmat);
                    proc.AddPara("@TODATE", dattoat);
                    proc.AddPara("@BRANCHID", Branch_Id);
                    proc.AddPara("@EMPID", Employee);
                    proc.AddPara("@CHANNELID", Channel_Id);
                    proc.AddPara("@USERID", Convert.ToInt32(Session["userid"]));
                    ds = proc.GetDataSet();
                }
            }
            else
            {
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
            }
            TempData["ChannelgridView"] = ds;
            return PartialView(ds);
        }

        public ActionResult ExportAttendanceGridView(int type)
        {

            ViewData["ChannelgridView"] = TempData["ChannelgridView"];

            DataSet DS = (DataSet)ViewData["ChannelgridView"];

            //End Rev
            TempData.Keep();

            if (ViewData["ChannelgridView"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetDashboardGridView(ViewData["ChannelgridView"]), DS.Tables[1]);
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetDashboardGridView(ViewData["ChannelgridView"]), DS.Tables[1], new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    case 3:
                        return GridViewExtension.ExportToXls(GetDashboardGridView(ViewData["ChannelgridView"]), DS.Tables[1]);
                    case 4:
                        return GridViewExtension.ExportToRtf(GetDashboardGridView(ViewData["ChannelgridView"]), DS.Tables[1]);
                    case 5:
                        return GridViewExtension.ExportToCsv(GetDashboardGridView(ViewData["ChannelgridView"]), DS.Tables[1]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetDashboardGridView(object dataset)
        {
            var settings = new GridViewSettings();
            settings.Name = "Team Visit - Hierarchy & Channel Wise";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "TeamVisitHierarchyChannelWise";
            String ID = Convert.ToString(TempData["ChannelgridView"]);
            TempData.Keep();
            DataSet dt = (DataSet)dataset;

            System.Data.DataTable dtColumnTable = new System.Data.DataTable();

            if (dt != null && dt.Tables.Count > 0)
            {

                dtColumnTable = dt.Tables[0];
                if (dtColumnTable != null && dtColumnTable.Rows.Count > 0)
                {
                    System.Data.DataRow[] drr = dtColumnTable.Select("PARRENTID=0");
                    int i = 0;
                    foreach (System.Data.DataRow dr in drr)
                    {
                        i = i + 1;
                        System.Data.DataRow[] drrRow = dtColumnTable.Select("PARRENTID='" + Convert.ToString(dr["HEADID"]) + "'");

                        if (drrRow.Length > 0)
                        {

                            settings.Columns.AddBand(x =>
                            {
                                x.Caption = Convert.ToString(dr["HEADNAME"]).Trim();
                                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                                x.VisibleIndex = i;

                                foreach (System.Data.DataRow drrs in drrRow)
                                {
                                    System.Data.DataRow[] drrRows = dtColumnTable.Select("PARRENTID='" + Convert.ToString(drrs["HEADID"]) + "'");

                                    if (drrRows.Length > 0)
                                    {
                                        x.Columns.AddBand(xSecond =>
                                        {
                                            xSecond.Caption = Convert.ToString(drrs["HEADNAME"]).Trim();
                                            xSecond.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                                            foreach (System.Data.DataRow drrss in drrRows)
                                            {
                                                System.Data.DataRow[] drrRowss = dtColumnTable.Select("PARRENTID='" + Convert.ToString(drrss["HEADID"]) + "'");
                                                if (drrRowss.Length > 0)
                                                {

                                                    xSecond.Columns.AddBand(xThird =>
                                                    {
                                                        xThird.Caption = Convert.ToString(drrss["HEADNAME"]).Trim();
                                                        xThird.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                                                        foreach (System.Data.DataRow drrrrs in drrRowss)
                                                        {
                                                            xThird.Columns.Add(xFourth =>
                                                            {
                                                                xFourth.Caption = Convert.ToString(drrrrs["HEADNAME"]).Trim();
                                                                xFourth.FieldName = Convert.ToString(drrrrs["HEADSHRTNAME"]).Trim();
                                                                xFourth.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                                                            });
                                                        }
                                                    });
                                                }
                                                else
                                                {
                                                    xSecond.Columns.Add(xThird =>
                                                    {
                                                        xThird.Caption = Convert.ToString(drrss["HEADNAME"]).Trim();
                                                        xThird.FieldName = Convert.ToString(drrss["HEADSHRTNAME"]).Trim();
                                                        xThird.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                                                    });
                                                }
                                            }
                                        });
                                    }
                                    else
                                    {
                                        x.Columns.Add(xSecond =>
                                        {
                                            xSecond.Caption = Convert.ToString(drrs["HEADNAME"]).Trim();
                                            xSecond.FieldName = Convert.ToString(drrs["HEADSHRTNAME"]).Trim();
                                            xSecond.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

                                        });
                                    }
                                }
                            });
                        }
                        else
                        {
                            settings.Columns.Add(x =>
                            {
                                x.Caption = Convert.ToString(dr["HEADNAME"]).Trim();
                                x.FieldName = Convert.ToString(dr["HEADSHRTNAME"]).Trim();
                                x.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                                x.VisibleIndex = i;

                            });
                        }

                    }
                }
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