//====================================================== Revision History ==========================================================
//1.0  19-07-2023   V2 .0.42   Priti     0026135: Branch Parameter is required for various FSM reports
//2.0  04-04-2024   V2 .0.46   Sanchita  0027345: Two checkbox required in parameter for Order register report.
//3.0  08-07-2024   V2 .0.48   Priti     0027407: "Party Status" - needs to add in the following reports.
//4.0  29/05/2024   V2.0.47    Sanchita  0027405: Colum Chooser Option needs to add for the following Modules
//====================================================== Revision History ==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLogicLayer;
using SalesmanTrack;
using System.Data;
using UtilityLayer;
using System.Web.Script.Serialization;
using MyShop.Models;
using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Collections;
using MyShop.Models;
using System.Configuration;
using BusinessLogicLayer.SalesTrackerReports;



namespace MyShop.Areas.MYSHOP.Controllers
{
    public partial class ReportController : Controller
    {
        // GET: /MYSHOP/GPSStatus/ 
        UserList lstuser = new UserList();
        ReportOrderRegister objgps = new ReportOrderRegister();
        DataTable dtuser = new DataTable();
        DataTable dtstate = new DataTable();
        DataTable dtshop = new DataTable();

        public ActionResult Orderregister()
        {
            try
            {

                Reportorderregisterinput omodel = new Reportorderregisterinput();
                string userid = Session["userid"].ToString();
                //dtuser = lstuser.GetUserList(userid);
                //dtshop = lstuser.GetShopList();
                // dtuser = lstuser.GetUserList();
                // List<GetUserName> model = new List<GetUserName>();

                // model = APIHelperMethods.ToModelList<GetUserName>(dtuser);


                //List<GetUsersStates> modelstate = new List<GetUsersStates>();
                //DataTable dtstate = lstuser.GetStateList();
                //modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);


                //List<Getmasterstock> modelshop = new List<Getmasterstock>();
                //modelshop = APIHelperMethods.ToModelList<Getmasterstock>(dtshop);


                //omodel.userlsit = model;
                omodel.selectedusrid = userid;
                omodel.Is_PageLoad = "0";


                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                // omodel.states = modelstate;
                //omodel.shoplist = modelshop;



                //if (TempData["Orderregister"] != null)
                //{
                //    omodel.selectedusrid = TempData["Orderregister"].ToString();

                //    TempData.Clear();
                //}


                DataTable dt = new SalesSummary_Report().GetStateMandatory(Session["userid"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }
                //REV 1.0
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
                omodel.modelbranch = APIHelperMethods.ToModelList<GetBranch>(dtbranch);
                string h_id = omodel.modelbranch.First().BRANCH_ID.ToString();
                ViewBag.h_id = h_id;
                //REV 1.0 End

                // Rev 2.0
                CommonBL cbl = new CommonBL();
                ViewBag.IsViewMRPInOrder = cbl.GetSystemSettingsResult("IsViewMRPInOrder");
                ViewBag.MRPInOrder = cbl.GetSystemSettingsResult("MRPInOrder");
                ViewBag.IsDiscountInOrder = cbl.GetSystemSettingsResult("IsDiscountInOrder");
                // End of Rev 2.0

                return View(omodel);


            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetStateList()
        {
            try
            {
                List<GetUsersStates> modelstate = new List<GetUsersStates>();
                DataTable dtstate = lstuser.GetStateList();
                modelstate = APIHelperMethods.ToModelList<GetUsersStates>(dtstate);




                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_StatesPartial.cshtml", modelstate);



            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }




        public ActionResult GetRegisterreporttatusList(Reportorderregisterinput model)
        {
            try
            {
                // Rev 2.0
                ViewBag.IsShowMRP = model.IsShowMRP;
                ViewBag.IsShowDiscount = model.IsShowDiscount;

                TempData["IsShowMRP"] = model.IsShowMRP;
                TempData.Keep();

                TempData["IsShowDiscount"] = model.IsShowDiscount;
                TempData.Keep();
                // End of Rev 2.0

                string Is_PageLoad = string.Empty;
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<GpsStatusClasstOutput> omel = new List<GpsStatusClasstOutput>();

                DataTable dt = new DataTable();

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Is_PageLoad == "0") Is_PageLoad = "Ispageload";

                ViewData["ModelData"] = model;

                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];
                string Userid = Convert.ToString(Session["userid"]);

                string state = "";
                int i = 1;

                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (i > 1)
                            state = state + "," + item;
                        else
                            state = item;
                        i++;
                    }

                }

                string shop = "";
                int j = 1;

                if (model.shopId != null && model.shopId.Count > 0)
                {
                    foreach (string item in model.shopId)
                    {
                        if (j > 1)
                            shop = shop + "," + item;
                        else
                            shop = item;
                        j++;
                    }

                }

                string empcode = "";

                int k = 1;

                if (model.empcode != null && model.empcode.Count > 0)
                {
                    foreach (string item in model.empcode)
                    {
                        if (k > 1)
                            empcode = empcode + "," + item;
                        else
                            empcode = item;
                        k++;
                    }

                }
                //Rev 1.0
                string Branch_Id = "";
                int l = 1;
                if (model.BranchId != null && model.BranchId.Count > 0)
                {
                    foreach (string item in model.BranchId)
                    {
                        if (l > 1)
                            Branch_Id = Branch_Id + "," + item;
                        else
                            Branch_Id = item;
                        l++;
                    }
                }
                //Rev 1.0 End

                //Mantis Issue 24593
                if (model.IsSchemeDetails != null)
                {
                    TempData["IsSchemeDetails"] = model.IsSchemeDetails;
                    TempData.Keep();
                }
                //End of Mantis Issue 24593

                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;
                if (days <= 30)
                {
                    //Rev 1.0
                    // dt = objgps.GetReportOrderRegister(datfrmat, dattoat, Userid, state, shop, empcode);
                    // Rev 2.0
                    //dt = objgps.GetReportOrderRegisterNew(datfrmat, dattoat, Userid, state, shop, empcode, Branch_Id);
                    dt = objgps.GetReportOrderRegisterNew(datfrmat, dattoat, Userid, state, shop, empcode, Branch_Id, model.IsShowMRP, model.IsShowDiscount);
                    // End of Rev 2.0
                    //Rev 1.0 End
                }

                // Rev 4.0
                DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "ORDER REGISTER");
                if (dtColmn != null && dtColmn.Rows.Count > 0)
                {
                    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
                }
                // End of Rev 4.0

                return PartialView("_PartialOrderRegister", GetOrderRegisterList(Is_PageLoad));


            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }



        public IEnumerable GetOrderRegisterList(string Is_PageLoad)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSORDERREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;

            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSORDERREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }

        }



        public ActionResult ExporRegisterList(int type)
        {

            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), GetOrderRegisterList(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), GetOrderRegisterList(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), GetOrderRegisterList(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), GetOrderRegisterList(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), GetOrderRegisterList(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            // Rev 4.0
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "ORDER REGISTER");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            // End of Rev 4.0


            var settings = new GridViewSettings();
            settings.Name = "gridorderregister";
            settings.CallbackRouteValues = new { Controller = "Report", Action = "GetRegisterreporttatusList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Order Register List";

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee ID";
                column.FieldName = "Employee_ID";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Employee_ID'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "EMPNAME";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMPNAME'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Branch";
                column.FieldName = "BRANCHDESC";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BRANCHDESC'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "SHOPNAME";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPNAME'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Code";
                column.FieldName = "ENTITYCODE";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ENTITYCODE'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Address";
                column.FieldName = "ADDRESS";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ADDRESS'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Contact";
                column.FieldName = "CONTACT";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Type";
                column.FieldName = "SHOPTYPE";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPTYPE'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });
            //REV 3.0
            settings.Columns.Add(column =>
            {
                column.Caption = "Party Status";
                column.FieldName = "PARTYSTATUS";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PARTYSTATUS'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });
            //REV 3.0 End
            settings.Columns.Add(column =>
            {
                column.Caption = "PP Name";
                column.FieldName = "PPName";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PPName'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "DD Name";
                column.FieldName = "DDName";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DDName'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Date";
                column.FieldName = "ORDDATE";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ORDDATE'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order No";
                column.FieldName = "ORDRNO";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ORDRNO'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Product";
                column.FieldName = "PRODUCT";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PRODUCT'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Quantity";
                column.FieldName = "QUANTITY";
                column.PropertiesEdit.DisplayFormatString = "0.00";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='QUANTITY'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });
            // Rev 2.0
            if (TempData["IsShowMRP"].ToString() == "1")
            {
                settings.Columns.Add(column =>
                {
                    column.Caption = "MRP";
                    column.FieldName = "MRP";
                    column.PropertiesEdit.DisplayFormatString = "0.00";

                    // Rev 4.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='MRP'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 4.0
                });
            }

            if (TempData["IsShowDiscount"].ToString() == "1")
            {
                settings.Columns.Add(column =>
                {
                    column.Caption = "Discount";
                    column.FieldName = "DISCOUNT";
                    column.PropertiesEdit.DisplayFormatString = "0.00";

                    // Rev 4.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DISCOUNT'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 4.0
                });
            }
            // End of Rev 2.0
            settings.Columns.Add(column =>
            {
                column.Caption = "Rate";
                column.FieldName = "RATE";
                column.PropertiesEdit.DisplayFormatString = "0.00";

                // Rev 4.0
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RATE'");
                    if (row != null && row.Length > 0)
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                else
                {
                    column.Visible = true;
                }
                // End of Rev 4.0
            });

            settings.Columns.Add(column =>
               {
                   column.Caption = "Order Value";
                   column.FieldName = "ORDVALUE";
                   column.PropertiesEdit.DisplayFormatString = "0.00";

                   // Rev 4.0
                   if (ViewBag.RetentionColumn != null)
                   {
                       System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ORDVALUE'");
                       if (row != null && row.Length > 0)
                       {
                           column.Visible = false;
                       }
                       else
                       {
                           column.Visible = true;
                       }
                   }
                   else
                   {
                       column.Visible = true;
                   }
                   // End of Rev 4.0
               });
            //Mantis Issue 24593
            if (TempData["IsSchemeDetails"].ToString() == "1")
            {
                settings.Columns.Add(column =>
                {
                    column.FieldName = "Scheme_Qty";
                    column.Caption = "Scheme Qty";
                    column.PropertiesEdit.DisplayFormatString = "0.00";

                    // Rev 4.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Scheme_Qty'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 4.0
                });
                settings.Columns.Add(column =>
                {
                    column.FieldName = "Scheme_Rate";
                    column.Caption = "Scheme Rate";
                    column.PropertiesEdit.DisplayFormatString = "0.00";

                    // Rev 4.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Scheme_Rate'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 4.0
                });
                settings.Columns.Add(column =>
                {
                    column.FieldName = "Total_Scheme_Price";
                    column.Caption = "Scheme Value";
                    column.PropertiesEdit.DisplayFormatString = "0.00";

                    // Rev 4.0
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='Total_Scheme_Price'");
                        if (row != null && row.Length > 0)
                        {
                            column.Visible = false;
                        }
                        else
                        {
                            column.Visible = true;
                        }
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    // End of Rev 4.0
                });
            }
            //End of Mantis Issue 24593
            
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        // Rev 4.0
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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "ORDER REGISTER");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
        // End of Rev 4.0
    }
}