/**************************************************************************************************
 * 1.0      Sanchita    V2.0.38     02/02/2023      Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True"
 *                                                  then data in portal shall be populated based on Hierarchy Only. Refer: 25504
 * ****************************************************************************************************/
using BusinessLogicLayer.SalesmanTrack;
using BusinessLogicLayer.SalesTrackerReports;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class ReimbursementAllowanceReportController : Controller
    {
        ReimbursementAllowanceReport objBl = null;
        public ReimbursementAllowanceReportController()
        {
            objBl = new ReimbursementAllowanceReport();
        }
        // GET: MYSHOP/ReimbursementAllowanceReport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReimbursementAllowanceReport()
        {
            ReimbursementAllowanceReportViewModel data = new ReimbursementAllowanceReportViewModel();
            data.StateList = GetDropDownListData("GetSateList");
            data.ExpenseTypeList = GetDropDownListData("GetExpenseTypeList");
            data.VisitLocationList = GetDropDownListData("GetVisitLocationList");
            data.EmployeeGradeList = GetDropDownListData("GetEmployeeGradeList");
            data.ModeOfTravelList = GetDropDownListData("GetModeOfTravelList");
            data.FuelTypeList = GetDropDownListData("GetFuelTypeList");

            return View(data);
        }

        public ActionResult GetReimbursementAllowanceReport(string stateid = "", string expensetype = "", string visitlocation = "", string employeegrade = "", string modeoftravel = "", string fueltype = "")
        {
            List<ReimbursementAllowanceReportViewModel> list = new List<ReimbursementAllowanceReportViewModel>();
            ReimbursementAllowanceReportViewModel data = null;
            try
            {
                if (stateid == "")
                    stateid = null;
                if (expensetype == "")
                    expensetype = null;
                if (visitlocation == "")
                    visitlocation = null;
                if (employeegrade == "")
                    employeegrade = null;
                if (modeoftravel == "")
                    modeoftravel = null;
                if (fueltype == "")
                    fueltype = null;
                // Rev 1.0
                string Userid = Convert.ToString(Session["userid"]);
                // End of Rev 1.0

                DataTable dt = objBl.GetReimbursementAllowanceReport(stateid, expensetype, visitlocation, employeegrade, modeoftravel, fueltype, "GetReport", Userid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        data = new Models.ReimbursementAllowanceReportViewModel();
                        data.AppliedOn = Convert.ToDateTime(row["AppliedOn"]);
                        data.ForDate = Convert.ToDateTime(row["ForDate"]);
                        data.LoginID = Convert.ToString(row["LoginID"]);
                        data.EmpName = Convert.ToString(row["EmpName"]);
                        data.Grade = Convert.ToString(row["Grade"]);
                        data.Supervisor = Convert.ToString(row["Supervisor"]);
                        data.StateName = Convert.ToString(row["StateName"]);
                        data.VisitLocation = Convert.ToString(row["VisitLocation"]);
                        data.ExpenseType = Convert.ToString(row["ExpenseType"]);

                        data.ModeOfTravel = Convert.ToString(row["ModeOfTravel"]);
                        data.FuelType = Convert.ToString(row["FuelType"]);
                        data.EligibleDistance = Convert.ToDecimal(row["EligibleDistance"]);

                        data.AppliedDistance = Convert.ToDecimal(row["AppliedDistance"]);
                        data.TotalTravelled = Convert.ToDecimal(row["TotalTravelled"]);
                        data.ApprovedDistance = Convert.ToDecimal(row["ApprovedDistance"]);
                        data.EligibleRate = Convert.ToDecimal(row["EligibleRate"]);
                        data.AppliedRate = Convert.ToDecimal(row["AppliedRate"]);
                        data.EligibleAmount = Convert.ToDecimal(row["EligibleAmount"]);


                        data.AppliedAmount = Convert.ToDecimal(row["AppliedAmount"]);
                        data.ApprovedAmount = Convert.ToDecimal(row["ApprovedAmount"]);
                        data.Status = Convert.ToString(row["Status"]);

                        list.Add(data);
                    }
                    TempData["ReimbursementAllowanceReport"] = dt;
                }
            }
            catch { }
            TempData.Keep();
            return PartialView("_ReimbursementAllowanceReportList", list);
        }

        public List<DropDownList> GetDropDownListData(string Action)
        {
            List<DropDownList> list = new List<DropDownList>();
            // Rev 1.0
            //DataTable dt = objBl.GetReimbursementAllowanceReport(null, null, null, null, null, null, Action);
            DataTable dt = objBl.GetReimbursementAllowanceReport(null, null, null, null, null, null, Action,null);
            // End of Rev 1.0
            if (dt != null && dt.Rows.Count > 0)
            {
                DropDownList data  = null;
                //data = new DropDownList();
                //data.Text = "Select";
                //data.Value = "";
                //list.Add(data);
                foreach (DataRow row in dt.Rows)
                {
                    data = new DropDownList();
                    data.Value = Convert.ToString(row[0]);
                    data.Text = Convert.ToString(row[1]);
                    list.Add(data);
                }
            }
            return list;
        }


        public ActionResult ExportReimbursementAllowanceReportList(int type)
        {
            ViewData["ReimbursementAllowanceReport"] = TempData["ReimbursementAllowanceReport"];

            TempData.Keep();

            if (ViewData["ReimbursementAllowanceReport"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetReimbursementAllowanceReportGridView(ViewData["ReimbursementAllowanceReport"]), ViewData["ReimbursementAllowanceReport"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetReimbursementAllowanceReportGridView(ViewData["ReimbursementAllowanceReport"]), ViewData["ReimbursementAllowanceReport"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetReimbursementAllowanceReportGridView(ViewData["ReimbursementAllowanceReport"]), ViewData["ReimbursementAllowanceReport"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetReimbursementAllowanceReportGridView(ViewData["ReimbursementAllowanceReport"]), ViewData["ReimbursementAllowanceReport"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetReimbursementAllowanceReportGridView(ViewData["ReimbursementAllowanceReport"]), ViewData["ReimbursementAllowanceReport"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetReimbursementAllowanceReportGridView(object datatable)
        {
            var settings = new GridViewSettings();
            settings.Name = "TravelAllowance";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "TravelAllowance";
            TempData.Keep();
            DataTable dt = (DataTable)datatable;

            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName == "AppliedOn" || datacolumn.ColumnName == "ForDate"
                    || datacolumn.ColumnName == "LoginID" || datacolumn.ColumnName == "EmpName" ||
                    datacolumn.ColumnName == "Grade" || datacolumn.ColumnName == "Supervisor"
                    || datacolumn.ColumnName == "StateName" || datacolumn.ColumnName == "VisitLocation"
                    || datacolumn.ColumnName == "ExpenseType" || datacolumn.ColumnName == "ModeOfTravel"
                    || datacolumn.ColumnName == "EligibleDistance" || datacolumn.ColumnName == "AppliedDistance"
                    || datacolumn.ColumnName == "ApprovedDistance" || datacolumn.ColumnName == "EligibleRate"
                    || datacolumn.ColumnName == "AppliedRate" || datacolumn.ColumnName == "EligibleAmount"

                    || datacolumn.ColumnName == "AppliedAmount" || datacolumn.ColumnName == "ApprovedAmount"
                    || datacolumn.ColumnName == "Status" || datacolumn.ColumnName == "TotalTravelled"

                    || datacolumn.ColumnName == "FuelType")
                {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "AppliedOn")
                        {
                            column.Caption = "Applied On";
                            column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                        }
                        else if (datacolumn.ColumnName == "ForDate")
                        {
                            column.Caption = "For Date";
                            column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                        }
                        else if (datacolumn.ColumnName == "LoginID")
                        {
                            column.Caption = "Login ID";
                        }
                        else if (datacolumn.ColumnName == "EmpName")
                        {
                            column.Caption = "Emp Name";
                        }
                        else if (datacolumn.ColumnName == "StateName")
                        {
                            column.Caption = "State";
                        }
                        else if (datacolumn.ColumnName == "VisitLocation")
                        {
                            column.Caption = "Travel Location";
                        }
                        else if (datacolumn.ColumnName == "ExpenseType")
                        {
                            column.Caption = "Exp Type";
                        }
                        else if (datacolumn.ColumnName == "ModeOfTravel")
                        {
                            column.Caption = "Mode of Travel";
                        }
                        else if (datacolumn.ColumnName == "FuelType")
                        {
                            column.Caption = "Fuel Type";
                        }
                        else if (datacolumn.ColumnName == "EligibleDistance")
                        {
                            column.Caption = "Eligible Distance";
                        }
                        else if (datacolumn.ColumnName == "AppliedDistance")
                        {
                            column.Caption = "Applied Distance";
                        }
                        else if (datacolumn.ColumnName == "ApprovedDistance")
                        {
                            column.Caption = "Approved Distance";
                        }
                        else if (datacolumn.ColumnName == "EligibleRate")
                        {
                            column.Caption = "Eligible Rate";
                        }
                        else if (datacolumn.ColumnName == "AppliedRate")
                        {
                            column.Caption = "Applied Rate";
                        }
                        else if (datacolumn.ColumnName == "EligibleAmount")
                        {
                            column.Caption = "Eligible Amount";
                        }

                        else if (datacolumn.ColumnName == "AppliedAmount")
                        {
                            column.Caption = "Applied Amount";
                        }
                        else if (datacolumn.ColumnName == "ApprovedAmount")
                        {
                            column.Caption = "Approved Amount";
                        }
                        else if (datacolumn.ColumnName == "TotalTravelled")
                        {
                            column.Caption = "Total Travelled[KM]";
                        }
                        else
                        {
                            column.Caption = datacolumn.ColumnName;
                        }
                        column.FieldName = datacolumn.ColumnName;
                        if (datacolumn.DataType.FullName == "System.Decimal")
                        {
                            column.PropertiesEdit.DisplayFormatString = "0.00";
                        }
                    });
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