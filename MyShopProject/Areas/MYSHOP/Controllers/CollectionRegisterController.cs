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
    public class CollectionRegisterController : Controller
    {
        //
        // GET: /MYSHOP/CollectionRegister/
        UserList lstuser = new UserList();
        SalesSummary_Report objgps = new SalesSummary_Report();
        public ActionResult CollectionRegister()
        {
            try
            {
                CollectionRegisterModel omodel = new CollectionRegisterModel();
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

        public ActionResult GetCollectionRegisterList(CollectionRegisterModel model)
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

                if(model.IsPaitentDetails!=null)
                {
                    TempData["IsPaitentDetails"] = model.IsPaitentDetails;
                    TempData.Keep();
                }
                double days = (Convert.ToDateTime(dattoat) - Convert.ToDateTime(datfrmat)).TotalDays;

                //if (days <= 7)
                //if (days <= 30)
                //{
                //    dt = GetCollectionRegisterReport(datfrmat, dattoat, Userid, state, desig, empcode);
                //}
                dt = GetCollectionRegisterReport(datfrmat, dattoat, Userid, state, shop, empcode);

                return PartialView("PartialGetCollectionRegisterReport", GetCollectionRegister(Ispageload));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });

            }
        }
        public DataTable GetCollectionRegisterReport(string fromdate, string todate, string userid, string stateID, string shopid, string empid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSCOLLECTIONREGISTER_REPORT");

            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            proc.AddPara("@STATEID", stateID);
            proc.AddPara("@SHOPID", shopid);
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

        public IEnumerable GetCollectionRegister(string Ispageload)
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "Coleection Register");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);

            if (Ispageload != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSCOLLECTIONREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTSCOLLECTIONREGISTER_REPORTs
                        where d.USERID == Convert.ToInt32(Userid) && d.EMPCODE == "0"
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }


        public ActionResult ExporCollectionRegisterList(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetCollectionRegisterGridViewSettings(), GetCollectionRegister(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetCollectionRegisterGridViewSettings(), GetCollectionRegister(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetCollectionRegisterGridViewSettings(), GetCollectionRegister(""));
                case 4:
                    return GridViewExtension.ExportToRtf(GetCollectionRegisterGridViewSettings(), GetCollectionRegister(""));
                case 5:
                    return GridViewExtension.ExportToCsv(GetCollectionRegisterGridViewSettings(), GetCollectionRegister(""));
                //break;

                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetCollectionRegisterGridViewSettings()
        {
            DataTable dtColmn = objgps.GetPageRetention(Session["userid"].ToString(), "COLLECTION REGISTER");
            if (dtColmn != null && dtColmn.Rows.Count > 0)
            {
                ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            }
            var settings = new GridViewSettings();
            settings.Name = "Collection Register Report";
            settings.CallbackRouteValues = new { Controller = "CollectionRegister", Action = "GetCollectionRegisterList" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Collection Register Report";


            if (TempData["IsPaitentDetails"].ToString() == "1")
            {
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
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
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
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOPNAME";
                    x.Caption = "Shop Name";
                    x.VisibleIndex = 4;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPNAME'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
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
                    x.FieldName = "ADDRESS";
                    x.Caption = "Address";
                    x.VisibleIndex = 6;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ADDRESS'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 250;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 250;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "CONTACT";
                    x.Caption = "Contact";
                    x.VisibleIndex = 7;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT'");
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
                    x.FieldName = "SHOPTYPE";
                    x.Caption = "Shop Type";
                    x.VisibleIndex = 8;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPTYPE'");
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
                    x.FieldName = "PPNAME";
                    x.Caption = "PP Name";
                    x.VisibleIndex = 9;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PPNAME'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DDNAME";
                    x.Caption = "DD Name";
                    x.VisibleIndex = 10;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DDNAME'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "PATIENT_NAME";
                    x.Caption = "Patient Name";
                    x.VisibleIndex = 11;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PATIENT_NAME'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "PATIENT_PHONE_NO";
                    x.Caption = "Patient Phone No.";
                    x.VisibleIndex = 12;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PATIENT_PHONE_NO'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "PATIENT_ADDRESS";
                    x.Caption = "Patient Address";
                    x.VisibleIndex = 13;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PATIENT_ADDRESS'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "HOSPITAL";
                    x.Caption = "Patient Hospital";
                    x.VisibleIndex = 14;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='HOSPITAL'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "EMAIL_ADDRESS";
                    x.Caption = "Patient Email Address";
                    x.VisibleIndex = 15;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='EMAIL_ADDRESS'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "COLLECTIONNO";
                    x.Caption = "Collection No.";
                    x.VisibleIndex = 16;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='COLLECTIONNO'");
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
                    x.FieldName = "COLLECTIONAMT";
                    x.Caption = "Collection Amount";
                    x.VisibleIndex = 17;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='COLLECTIONAMT'");
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
                    x.FieldName = "PAYMENTMODE";
                    x.Caption = "Payment Mode";
                    x.VisibleIndex = 18;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PAYMENTMODE'");
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
                    x.FieldName = "INSTRUMENT_NO";
                    x.Caption = "Instrument No.";
                    x.VisibleIndex = 19;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='INSTRUMENT_NO'");
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
                    x.FieldName = "BANKNAME";
                    x.Caption = "Bank Name";
                    x.VisibleIndex = 20;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BANKNAME'");
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
                    x.FieldName = "COLLECTIONDATE";
                    x.Caption = "Payment Date";
                    x.VisibleIndex = 21;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='COLLECTIONDATE'");
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
                    x.FieldName = "REMARKS";
                    x.Caption = "Remarks";
                    x.VisibleIndex = 22;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REMARKS'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });
            }

            if (TempData["IsPaitentDetails"].ToString() == "0")
            {
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
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
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
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "SHOPNAME";
                    x.Caption = "Shop Name";
                    x.VisibleIndex = 4;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPNAME'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
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
                    x.FieldName = "ADDRESS";
                    x.Caption = "Address";
                    x.VisibleIndex = 6;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='ADDRESS'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 250;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 250;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "CONTACT";
                    x.Caption = "Contact";
                    x.VisibleIndex = 7;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='CONTACT'");
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
                    x.FieldName = "SHOPTYPE";
                    x.Caption = "Shop Type";
                    x.VisibleIndex = 8;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='SHOPTYPE'");
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
                    x.FieldName = "PPNAME";
                    x.Caption = "PP Name";
                    x.VisibleIndex = 9;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PPNAME'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "DDNAME";
                    x.Caption = "DD Name";
                    x.VisibleIndex = 10;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='DDNAME'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });

                settings.Columns.Add(x =>
                {
                    x.FieldName = "COLLECTIONNO";
                    x.Caption = "Collection No.";
                    x.VisibleIndex = 11;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='COLLECTIONNO'");
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
                    x.FieldName = "COLLECTIONAMT";
                    x.Caption = "Collection Amount";
                    x.VisibleIndex = 12;
                    x.PropertiesEdit.DisplayFormatString = "0.00";
                    x.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    x.CellStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='COLLECTIONAMT'");
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
                    x.FieldName = "PAYMENTMODE";
                    x.Caption = "Payment Mode";
                    x.VisibleIndex = 13;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='PAYMENTMODE'");
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
                    x.FieldName = "INSTRUMENT_NO";
                    x.Caption = "Instrument No.";
                    x.VisibleIndex = 14;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='INSTRUMENT_NO'");
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
                    x.FieldName = "BANKNAME";
                    x.Caption = "Bank Name";
                    x.VisibleIndex = 15;
                    x.ColumnType = MVCxGridViewColumnType.TextBox;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='BANKNAME'");
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
                    x.FieldName = "COLLECTIONDATE";
                    x.Caption = "Payment Date";
                    x.VisibleIndex = 16;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='COLLECTIONDATE'");
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
                    x.FieldName = "REMARKS";
                    x.Caption = "Remarks";
                    x.VisibleIndex = 17;
                    if (ViewBag.RetentionColumn != null)
                    {
                        System.Data.DataRow[] row = ViewBag.RetentionColumn.Select("ColumnName='REMARKS'");
                        if (row != null && row.Length > 0)
                        {
                            x.Visible = false;
                        }
                        else
                        {
                            x.Visible = true;
                            x.Width = 200;
                        }
                    }
                    else
                    {
                        x.Visible = true;
                        x.Width = 200;
                    }
                });
            }

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
                int k = objgps.InsertPageRetention(Col, Session["userid"].ToString(), "COLLECTION REGISTER");

                return Json(k, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
	}
}