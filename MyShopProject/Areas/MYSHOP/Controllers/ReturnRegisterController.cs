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
    public class ReturnRegisterController : Controller
    {
        //
        // GET: /MYSHOP/ReturnRegister/
        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();
        public ActionResult ReturnRegister()
        {
            try
            {
                ReturnRegisterModel omodel = new ReturnRegisterModel();
                string userid = Session["userid"].ToString();
                omodel.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                DataTable dt = objgps.GetStateMandatory(Session["userid"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewBag.StateMandatory = dt.Rows[0]["IsStateMandatoryinReport"].ToString();
                }
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetReturnRegisterList(ReturnRegisterModel model)
        {
            try
            {
                DataTable dt = new DataTable();
                string Ispageload = string.Empty;

                if (model.is_pageload == "0")
                {
                    Ispageload = "Ispageload";
                }

                if (model.Fromdate == null)
                {
                    model.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
                }

                if (model.Todate == null)
                {
                    model.Todate = DateTime.Now.ToString("dd-MM-yyyy");
                }

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

                string desig = "";
                int j = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (j > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
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
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;

                //if (days <= 7)
                //if (days <= 30)
                //{
                //    dt = GetReturnRegisterReport(datfrmat, dattoat, Userid, state, desig, empcode);
                //}
                dt = GetReturnRegisterReport(datfrmat, dattoat, Userid, state, desig, empcode);

                return PartialView("PartialGetReturnRegisterReport", GetReturnRegister(Ispageload));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }
        public DataTable GetReturnRegisterReport(string fromdate, string todate, string userid, string stateID, string desigid, string empid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSRETURNREGISTER_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@DESIGNID", desigid);
            proc.AddPara("@EMPID", empid);
            proc.AddPara("@USERID", userid);            
            ds = proc.GetTable();

            return ds;
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

        public ActionResult GetDesigList()
        {
            try
            {
                DataTable dtdesig = lstuser.Getdesiglist();
                List<GetDesignation> modeldesig = new List<GetDesignation>();
                modeldesig = APIHelperMethods.ToModelList<GetDesignation>(dtdesig);
                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_DesigPartial.cshtml", modeldesig);                
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetEmpList(SalesSummaryReport model)
        {
            try
            {
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

                string desig = "";
                int k = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (k > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
                        k++;
                    }
                }

                DataTable dtemp = lstuser.Getemplist(state, desig);
                List<GetAllEmployee> modelemp = new List<GetAllEmployee>();
                modelemp = APIHelperMethods.ToModelList<GetAllEmployee>(dtemp);

                return PartialView("~/Areas/MYSHOP/Views/SearchingInputs/_EmpPartial.cshtml", modelemp);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetReturnRegister(string Ispageload)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Return Register");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (Ispageload != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSRETURNREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSRETURNREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        public ActionResult ExporReturnRegisterList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetReturnRegisterGridViewSettings(), GetReturnRegister(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetReturnRegisterGridViewSettings(), GetReturnRegister(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetReturnRegisterGridViewSettings(), GetReturnRegister(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetReturnRegisterGridViewSettings(), GetReturnRegister(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetReturnRegisterGridViewSettings(), GetReturnRegister(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetReturnRegisterGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "RETURN REGISTER");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            var settings = new GridViewSettings();
            settings.Name = "Return Register Report";
            settings.CallbackRouteValues = new { Controller = "ReturnRegister", Action = "GetReturnRegisterList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Return Register Report";

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPID";
                x.Caption = "Employee ID";
                x.VisibleIndex = 1;
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
                        x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 100;
                }

            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "EMPNAME";
                x.Caption = "Employee Name";
                x.VisibleIndex = 2;
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
                        x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 100;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "BRANCHDESC";
                x.Caption = "Branch";
                x.VisibleIndex = 3;
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
                        x.Width = 100;
                    }
                }
                else
                {
                    x.Visible = true;
                    x.Width = 100;
                }
            });

            settings.Columns.Add(x =>
            {
                x.FieldName = "SHOP_NAME";
                x.Caption = "Shop Name";
                x.VisibleIndex = 4;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_NAME'");
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

            settings.Columns.Add(x =>
            {
                x.FieldName = "ENTITYCODE";
                x.Caption = "Code";
                x.VisibleIndex = 5;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ENTITYCODE'");
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
                x.FieldName = "SHOP_ADDRESS";
                x.Caption = "Address";
                x.VisibleIndex = 6;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_ADDRESS'");
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
                x.FieldName = "SHOPCONTACTNO";
                x.Caption = "Contact";
                x.VisibleIndex = 7;
                x.ColumnType = MVCxGridViewColumnType.TextBox;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPCONTACTNO'");
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
                x.FieldName = "SHOP_TYPE";
                x.Caption = "Shop Type";
                x.VisibleIndex = 8;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOP_TYPE'");
                    if (row != null && row.Length > 0)
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

            settings.Columns.Add(x =>
            {
                x.FieldName = "PP_NAME";
                x.Caption = "PP Name";
                x.VisibleIndex = 9;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PP_NAME'");
                    if (row != null && row.Length > 0)
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

            settings.Columns.Add(x =>
            {
                x.FieldName = "DD_NAME";
                x.Caption = "DD Name";
                x.VisibleIndex = 10;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DD_NAME'");
                    if (row != null && row.Length > 0)
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

            settings.Columns.Add(x =>
            {
                x.FieldName = "RETURNDATE";
                x.Caption = "Return Date";
                x.VisibleIndex = 11;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RETURNDATE'");
                    if (row != null && row.Length > 0)
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


            settings.Columns.Add(x =>
            {
                x.FieldName = "RETURNNO";
                x.Caption = "Return No";
                x.VisibleIndex = 12;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RETURNNO'");
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
                x.FieldName = "PRODUCT_NAME";
                x.Caption = "Product";
                x.VisibleIndex = 13;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PRODUCT_NAME'");
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
                x.FieldName = "QUANTITY";
                x.Caption = "Quantity";
                x.VisibleIndex = 14;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='QUANTITY'");
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
                x.FieldName = "RATE";
                x.Caption = "Rate";
                x.VisibleIndex = 15;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='RATE'");
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
                x.FieldName = "AMOUNT";
                x.Caption = "Return Value";
                x.VisibleIndex = 16;
                x.PropertiesEdit.DisplayFormatString = "0.00";
                x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                if (ViewBag.RetentionColumn != null)
                {
                    System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='AMOUNT'");
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

            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "QUANTITY").DisplayFormat = "0.00";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "AMOUNT").DisplayFormat = "0.00";

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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "RETURN REGISTER");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
	}
}