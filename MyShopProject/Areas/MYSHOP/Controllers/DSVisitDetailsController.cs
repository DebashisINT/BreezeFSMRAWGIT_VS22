#region======================================Revision History=======================================================================================================
//1.0   V2.0.41    Debashis     25/07/2023     DS Visit Details - Two Columns Required.Refer: 0026474
//2.0   V2.0.41    Debashis     09/08/2023     A coloumn named as Gender needs to be added in all the ITC reports.Refer: 0026680
//3.0   V2 .0.44   Debashis     27/02/2024     'Sale Value' Field required in DS Visit Details/DS Visit Summary.Refer: 0027276
//4.0   V2 .0.45   Debashis     29/03/2024     In DS Visit Details Report a new coloumn required 'New Outlets Visited' as like DS Visit
//                                             Summary report.Refer: 0027328
//5.0   V2.0.47    Debashis    03/06/2024      A new coloumn shall be added in the below mentioned reports.Refer: 0027402
//6.0   V2.0.47    Debashis    03/06/2024      The respective Sales Value coloumn in the below mentioned reports shall be replaced with “Delivery value”.Refer: 0027499
//7.0   V2.0.47    Debashis    10/06/2024      Add a new column at the end named as “Total CDM Days" in selected date range.Refer: 0027510
//8.0   v2.0.48    Sanchita    26-08-2024      Working Hour customization for ITC users require. Mantis: 27661
#endregion===================================End of Revision History================================================================================================

using BusinessLogicLayer.SalesTrackerReports;
using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DocumentFormat.OpenXml.Vml.Office;
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
    public class DSVisitDetailsController : Controller
    {
        //
        // GET: /MYSHOP/DSVisitDetails/
        SalesSummary_Report objgps = new SalesSummary_Report();
        public ActionResult DSVisitDetails()
        {
            try
            {
                UserList lstuser = new UserList();
                DSVisitDetailsReport omodel = new DSVisitDetailsReport();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                DataTable dtbranch = lstuser.GetHeadBranchList(Convert.ToString(Session["userbranchHierarchy"]), "HO");
                DataTable dtBranchChild = new DataTable();
                if (dtbranch.Rows.Count > 0)
                {
                    dtBranchChild = lstuser.GetChildBranch(Convert.ToString(Session["userbranchHierarchy"]));
                    //Rev Debashis 0025198
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
                    //End of Rev Debashis 0025198
                }
                omodel.modelbranch = APIHelperMethods.ToModelList<GetBranch>(dtbranch);
                string h_id = omodel.modelbranch.First().BRANCH_ID.ToString();
                ViewBag.h_id = h_id;
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetDSVisitDetailsList(DSVisitDetailsReport model)
        {
            try
            {
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<DSVisitDetailsModel> omel = new List<DSVisitDetailsModel>();

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
                        omel = APIHelperMethods.ToModelList<DSVisitDetailsModel>(dt);
                        TempData["ExportDSVisitDetails"] = omel;
                        TempData.Keep();
                    }
                    else
                    {
                        TempData["ExportDSVisitDetails"] = null;
                        TempData.Keep();
                    }
                    
                }
                //Rev 1.0 Mantis: 0026474
                ////Rev Debashis 0024906
                //if (model.is_pageload == "1")
                //{
                //    //End of Rev Debashis 0024906
                //    double days = (Convert.ToDateTime(ToDate) - Convert.ToDateTime(FromDate)).TotalDays;
                //    if (days <= 35)
                //    {
                //        dt = GetDSVisitDetails(Employee, FromDate, ToDate, Branch_Id);
                //    }
                ////Rev Debashis 0024906
                //}
                ////End of Rev Debashis 0024906
                double days = (Convert.ToDateTime(ToDate) - Convert.ToDateTime(FromDate)).TotalDays;
                if (days <= 35)
                {
                    dt = GetDSVisitDetails(Employee, FromDate, ToDate, Branch_Id,model.is_pageload);
                }
                //End of Rev 1.0 Mantis: 0026474

                return PartialView("_PartialGridDSVisitDetails", LGetDSVisitDetails(Is_PageLoad));
            }
            catch
            {
                //   return Redirect("~/OMS/Signoff.aspx");
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

        }

        //Rev 1.0 Mantis: 0026474
        //public DataTable GetDSVisitDetails(string Employee, string start_date, string end_date, string Branch_Id)
        public DataTable GetDSVisitDetails(string Employee, string start_date, string end_date, string Branch_Id, string IsPageLoad)
        //End of Rev 1.0 Mantis: 0026474
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSDSVISITDETAILS_REPORT");
            proc.AddPara("@FROMDATE", start_date);
            proc.AddPara("@TODATE", end_date);
            proc.AddPara("@BRANCHID", Branch_Id);
            proc.AddPara("@EMPID", Employee);
            //Rev 1.0 Mantis: 0026474
            proc.AddPara("@ISPAGELOAD", IsPageLoad);
            //End of Rev 1.0 Mantis: 0026474
            proc.AddPara("@USERID", Convert.ToInt32(Session["userid"]));
            ds = proc.GetTable();
            return ds;
        }

        public IEnumerable LGetDSVisitDetails(string Is_PageLoad)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "DS Visit Details");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "is_pageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDSVISITDETAILS_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSDSVISITDETAILS_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }



        }

        public ActionResult ExportDSVisitDetailsList(int type)
        {


            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetDSSummaryGridViewSettings(), LGetDSVisitDetails(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetDSSummaryGridViewSettings(), LGetDSVisitDetails(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetDSSummaryGridViewSettings(), LGetDSVisitDetails(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetDSSummaryGridViewSettings(), LGetDSVisitDetails(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetDSSummaryGridViewSettings(), LGetDSVisitDetails(""));
                //break;

                default:
                    break;
            }

            return null;
        }
        private GridViewSettings GetDSSummaryGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "DS Visit Details");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }
            var settings = new GridViewSettings();
            settings.Name = "DSVisitDetails";
            settings.CallbackRouteValues = new { Controller = "DSVisitDetails", Action = "GetDSVisitDetailsList" };

            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "DS Visit Details Report";


            settings.Columns.Add(x =>
            {
                x.FieldName = "BRANCHDESC";
                x.Caption = "Branch";
                x.VisibleIndex = 1;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BRANCHDESC'");
                    if (row != null && row.Length > 0)  
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });
            //Rev Debashis Mantis:0025218
            settings.Columns.Add(x =>
            {
                x.FieldName = "CIRCLE";
                x.Caption = "Circle";
                x.VisibleIndex = 2;
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
            //End of Rev Debashis Mantis:0025218

            settings.Columns.Add(x =>
            {
                x.FieldName = "HREPORTTOUID";
                x.Caption = "AE ID";
                x.VisibleIndex = 3;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='HREPORTTOUID'");
                    if (row != null && row.Length > 0) 
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

           
            settings.Columns.Add(x =>
            {

                x.FieldName = "REPORTTOUID";
                x.Caption = "WD ID";
                x.VisibleIndex = 4;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REPORTTOUID'");
                    if (row != null && row.Length > 0)  
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            
            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPID";
                //Rev Debashis Mantis:0025218
                //x.Caption = "DS/TL ID";
                x.Caption = "DS ID";
                //End of Rev Debashis Mantis:0025218
                x.VisibleIndex = 5;
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
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                //Rev Debashis Mantis:0025218
                //x.Caption = "DS/TL Name";
                x.Caption = "DS Name";
                //End of Rev Debashis Mantis:0025218
                x.VisibleIndex = 6;
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
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });
            //Rev Debashis Mantis:0025218
            //Rev 2.0 Mantis: 0026680
            settings.Columns.Add(x =>
            {
                x.FieldName = "GENDERDESC";
                x.Caption = "Gender";
                x.VisibleIndex = 7;
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
            //End of Rev 2.0 Mantis: 0026680
            settings.Columns.Add(x =>
            {
                x.FieldName = "DSTYPE";
                x.Caption = "DS Type";
                x.VisibleIndex = 8;
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
            //End of Rev Debashis Mantis:0025218

            settings.Columns.Add(x =>
            {
                x.FieldName = "LOGIN_DATETIME";
                x.Caption = "Visit Date";
                x.VisibleIndex = 9;
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
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "OUTLETSMAPPED";
                x.Caption = "Outlets Mapped(Added)";
                x.VisibleIndex = 10;
 
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
                        
                    }
                }
                else
                {
                    x.Visible = true;                    
                }

            });

            //Rev 4.0 Mantis: 0027328
            settings.Columns.Add(x =>
            {
                x.FieldName = "NEWSHOP_VISITED";
                x.Caption = "New Outlets Visited";
                x.VisibleIndex = 11;

                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='NEWSHOP_VISITED'");
                    if (row != null && row.Length > 0)
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });
            //End of Rev 4.0 Mantis: 0027328

            settings.Columns.Add(x =>
            {
                x.FieldName = "RE_VISITED";
                x.Caption = "Outlets Re-Visited";
                x.VisibleIndex = 12;
                
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
                    }
                }
                else
                {
                    x.Visible = true;                   
                }

            });

            //Rev 1.0 Mantis: 0026474
            settings.Columns.Add(x =>
            {
                x.FieldName = "QUALIFIEDPRESENT";
                x.Caption = "Qualified";
                x.VisibleIndex = 13;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='QUALIFIEDPRESENT'");
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
                x.FieldName = "ATTENDANCE";
                x.Caption = "Present/Absent";
                x.VisibleIndex = 14;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ATTENDANCE'");
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
            //End of Rev 1.0 Mantis: 0026474

            settings.Columns.Add(x =>
            {
                x.FieldName = "DISTANCE_TRAVELLED";
                x.Caption = "Distance Travelled(Km.Mtr)";
                //Rev Debashis 0024906
                x.PropertiesEdit.DisplayFormatString = "0.00";
                //End of Rev Debashis 0024906
                x.VisibleIndex = 15;
                
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
                        
                    }
                }
                else
                {
                    x.Visible = true;                    
                }

            });

            
            settings.Columns.Add(x =>
            {
                x.FieldName = "AVGTIMESPENTINMARKET";
                //Rev Debashis 0024906
                //x.Caption = "Avg time spent in the market(HH:MM)";
                x.Caption = "Total time spent in the market(HH:MM)";
                //End of Rev Debashis 0024906
                x.VisibleIndex = 16;
                
                x.PropertiesEdit.DisplayFormatString = "0.00";
                
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
                    }
                }
                else
                {
                    x.Visible = true;                    
                }

            });

            //Rev Debashis 0024956
            settings.Columns.Add(x =>
            {
                x.FieldName = "DAYSTTIME";
                x.Caption = "Day Start(HH:MM)";
                x.VisibleIndex = 17;

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
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "DAYENDTIME";
                x.Caption = "Day End(HH:MM)";
                x.VisibleIndex = 18;

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
                    }
                }
                else
                {
                    x.Visible = true;
                }
            });
            //End of Rev Debashis 0024956

            //Rev 3.0 Mantis: 0027276
            settings.Columns.Add(x =>
            {
                x.FieldName = "SALE_VALUE";
                //Rev 6.0 Mantis: 0027402
                //x.Caption = "Sale Value";
                x.Caption = "Delivery Value";
                //End of Rev 6.0 Mantis: 0027402
                x.VisibleIndex = 19;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SALE_VALUE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }

            });
            //End of Rev 3.0 Mantis: 0027276

            //Rev 5.0 Mantis: 0027402
            settings.Columns.Add(x =>
            {
                x.FieldName = "ORDER_VALUE";
                x.Caption = "Order Value";
                x.VisibleIndex = 20;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ORDER_VALUE'");
                    if (row != null && row.Length > 0)  /// Check now
                    {
                        x.Visible = false;
                    }
                    else
                    {
                        x.Visible = true;
                    }
                }
                else
                {
                    x.Visible = true;
                }

            });
            //End of Rev 5.0 Mantis: 0027402

            settings.Columns.Add(x =>
            {
                x.FieldName = "AVGSPENTDURATION";
                //Rev Debashis 0024906
                //x.Caption = "Avg time spent in OL(CFT-Customer Facing Time)(HH:MM)";
                x.Caption = "Avg time spent in OL(CFT-New&Revisit)(HH:MM)";
                //End of Rev Debashis 0024906
                x.VisibleIndex = 21;
                
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
                    }
                }
                else
                {
                    x.Visible = true;                    
                }

            });

            //Rev 7.0 Mantis: 0027510
            settings.Columns.Add(x =>
            {
                x.FieldName = "TOTALCDMDAYS";
                x.Caption = "Total CDM Days";
                x.VisibleIndex = 22;
                x.PropertiesEdit.DisplayFormatString = "0";
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='TOTALCDMDAYS'");
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
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            });
            //End of Rev 7.0 Mantis: 0027510

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

        // Rev 8.0
        public JsonResult CheckWorkingRoster(string module_ID)
        {
            //CommonBL ComBL = new CommonBL();
            //string STBTransactionsRestrictBeyondTheWorkingDays = ComBL.GetSystemSettingsResult("STBTransactionsRestrictBeyondTheWorkingDays");

            DSVisitDetailsReport apply = new DSVisitDetailsReport();

            string output = string.Empty;
            try
            {
                ProcedureExecute proc = new ProcedureExecute("PRC_WORKROSTER_ADDEDITVIEW");
                proc.AddPara("@ACTION", "GETMODULEROSTERSTATUS");
                proc.AddPara("@ModuleId", module_ID);
                proc.AddPara("@USERID", Session["userid"].ToString() );
                DataSet ds = proc.GetDataSet();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["returnvalue"].ToString() == "true")
                    {
                        //output = "true";
                        apply.response_msg = "true";
                        apply.response_begintime = "";
                        apply.response_endtime = "";
                    }
                    else if (ds.Tables[0].Rows[0]["returnvalue"].ToString() == "false")
                    {
                        //output = "false~" + ds.Tables[1].Rows[0]["BeginTime"].ToString() + "~" + ds.Tables[1].Rows[0]["EndTime"].ToString();

                        apply.response_msg = "false";
                        apply.response_begintime = ds.Tables[1].Rows[0]["BeginTime"].ToString();
                        apply.response_endtime = ds.Tables[1].Rows[0]["EndTime"].ToString();
                    }

                }
                else
                {
                    // output = "false";
                    apply.response_msg = "false";
                    apply.response_begintime = "";
                    apply.response_endtime = "";
                }
            }
            catch (Exception ex)
            {
                //output = ex.Message.ToString();
                apply.response_msg = ex.Message.ToString();
                apply.response_begintime = "";
                apply.response_endtime = "";
            }

            return Json(apply, JsonRequestBehavior.AllowGet); 
        }
        // End of Rev 8.0
    }
}