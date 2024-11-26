/******************************************************************************************************
 * Rev 1.0      Sanchita    07/02/2023      V2.0.36     FSM Employee & User Master - To implement Show button. refer: 25641
 * Rev 2.0      Sanchita    15/02/2023      V2.0.39     A setting required for Employee and User Master module in FSM Portal. 
 * Rev 3.0      Priti       15/02/2023      V2.0.39    	0025676: Employee Import Facility
 * Rev 4.0      Sanchita    08-08-2023      V2.0.42     FSM - Masters - Organization - Employees - Change Supervisor should be On Demand Search. Mantis: 26700
 * Rev 5.0      Sanchita    09-08-2023      V2.0.42     FSM Portal - Enhance the Export to excel in Employee Master. Mantis : 26708
 * Rev 6.0      Sanchita    25/10/2024      V2.0.49     In employee master a new filed is required as Target Level.. Mantis : 27773
 *******************************************************************************************************/
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using BusinessLogicLayer;
using System.IO;
using EntityLayer.CommonELS;
using DataAccessLayer;
using BusinessLogicLayer.SalesERP;
using System.Web.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
/* Mantise ID:0024752: Optimize FSM Employee Master
      Rev work Swati Date:-15.03.2022*/
using ERP.Models;
using System.Linq;
using iTextSharp.text.log;
using EO.Web.Internal;
//using DevExpress.Export.Xl;
using LibDosPrint;
using System.Data.SqlClient;
/* Mantise ID:0024752: Optimize FSM Employee Master
Rev work Close Swati Date:-15.03.2022*/
namespace ERP.OMS.Management.Master
{
    public partial class management_master_Employee : ERP.OMS.ViewState_class.VSPage// System.Web.UI.Page
    {
        #region Global Variable
        public string pageAccess;
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
        GlobalSettings globalsetting = new GlobalSettings();
        //GenericExcelExport oGenericExcelExport;
        //GenericMethod oGenericMethod;
        BusinessLogicLayer.GenericExcelExport oGenericExcelExport;
        BusinessLogicLayer.GenericMethod oGenericMethod;
        BusinessLogicLayer.Converter OConvert = new BusinessLogicLayer.Converter();
        string WhichCall;
        DataSet Ds_Global;
        AspxHelper oAspxHelper = new AspxHelper();
        public EntityLayer.CommonELS.UserRightsForPage rights = new UserRightsForPage();

        ExcelFile ex = new ExcelFile();
        FileInfo FIICXCSV = null;
        StreamReader strReader;
        StringBuilder strbuilder = new StringBuilder();
        String readline = string.Empty;
        public string[] InputName = new string[20];
        public string[] InputType = new string[20];
        public string[] InputValue = new string[20];
        public string[] InputName1 = new string[20];
        public string[] InputType1 = new string[20];
        public string[] InputValue1 = new string[20];
        DataTable dt1 = new DataTable();
        string FilePath = "";
        DataSet dsdata = new DataSet();
        DataView dvUnmatched = new DataView();
        private static String path, path1, FileName, s, time, cannotParse;

        // Rev Mantis Issue 25001
        public bool ActivateEmployeeBranchHierarchy { get; set; }
        // End of Rev Mantis Issue 25001

        // Rev 5.0
        ERP.Model.ExcelFile objExcel = new ERP.Model.ExcelFile();
        DataTable CompanyInfo = new DataTable();
        DataTable dtExport = new DataTable();
        DataTable dtReportHeader = new DataTable();
        DataTable dtReportFooter = new DataTable();
        // End of Rev 5.0

        Employee_BL objEmploye = new Employee_BL();
        #endregion
        //Session Used in This Page : PageSize,FromDOJ,ToDoj,PageNumAfterNav,SerachString,SearchBy,FindOption
        #region Page Properties
        public string P_PageSize
        {
            get { return (string)ViewState["PageSize"]; }
            set { ViewState["PageSize"] = value; }
        }
        public string P_FromDOJ
        {
            get { return (string)Session["FromDOJ"]; }
            set { Session["FromDOJ"] = value; }
        }
        public string P_ToDOJ
        {
            get { return (string)Session["ToDOJ"]; }
            set { Session["ToDOJ"] = value; }
        }
        public string P_PageNumAfterNav
        {
            get { return (string)Session["PageNumAfterNav"]; }
            set { Session["PageNumAfterNav"] = value; }
        }
        public string P_SearchString
        {
            get { return (string)Session["SearchString"]; }
            set { Session["SearchString"] = value; }
        }
        public string P_SearchBy
        {
            get { return (string)Session["SearchBy"]; }
            set { Session["SearchBy"] = value; }
        }
        public string P_FindOption
        {
            get { return (string)Session["FindOption"]; }
            set { Session["FindOption"] = value; }
        }
        public string P_ShowFilter_SearchString
        {
            get { return (string)Session["ShowFilter_SearchString"]; }
            set { Session["ShowFilter_SearchString"] = value; }
        }


        #endregion
        #region User Define Methods
      // Mantis Issue 24752_Rectify
        //DataSet Fetch_EmployeeData(string FromDOJ, string ToDOJ, string PageSize, string PageNumber,
        //    string SearchString, string SearchBy, string FindOption, string ExportType, string DevXFilterOn, string DevXFilterString)
        //{
        //    string[] InputName = new string[11];
        //    string[] InputType = new string[11];
        //    string[] InputValue = new string[11];

        //    InputName[0] = "FromJoinDate";
        //    InputName[1] = "ToJoinDate";
        //    InputName[2] = "PageSize";
        //    InputName[3] = "PageNumber";
        //    InputName[4] = "SearchString";
        //    InputName[5] = "SearchBy";
        //    InputName[6] = "FindOption";
        //    InputName[7] = "ExportType";
        //    InputName[8] = "DevXFilterOn";
        //    InputName[9] = "DevXFilterString";

        //    //Add extra column for Employee hierarchy
        //    InputName[10] = "User_id";
        //    //End extra column for Employee hierarchy

        //    InputType[0] = "D";
        //    InputType[1] = "D";
        //    InputType[2] = "I";
        //    InputType[3] = "I";
        //    InputType[4] = "V";
        //    InputType[5] = "C";
        //    InputType[6] = "V";
        //    InputType[7] = "V";
        //    InputType[8] = "V";
        //    InputType[9] = "V";
        //    //Add extra column for Employee hierarchy
        //    InputType[10] = "I";
        //    //End extra column for Employee hierarchy

        //    InputValue[0] = FromDOJ;
        //    InputValue[1] = ToDOJ;
        //    InputValue[2] = PageSize;
        //    InputValue[3] = PageNumber;
        //    InputValue[4] = SearchString;
        //    InputValue[5] = SearchBy;
        //    InputValue[6] = FindOption;
        //    InputValue[7] = ExportType;
        //    InputValue[8] = DevXFilterOn;
        //    InputValue[9] = DevXFilterString;
        //    //Add extra column for Employee hierarchy
        //    InputValue[10] = Convert.ToString(HttpContext.Current.Session["userid"]);
        //    //End extra column for Employee hierarchy

        //    return BusinessLogicLayer.SQLProcedures.SelectProcedureArrDS("HR_Fetch_Employees", InputName, InputType, InputValue);
        //}
        // End of Mantis Issue 24752_Rectify

        void ExportToExcel(DataSet DsExport, string FromDOJ, string ToDOJ, string SearchString, string SearchBy, string FindOption)
        {
            oGenericExcelExport = new BusinessLogicLayer.GenericExcelExport();
            DataTable DtExport = new DataTable();
            string strHeader = String.Empty;
            string[] ReportHeader = new string[1];
            string strSavePath = String.Empty;

            strHeader = "Employee Detail From " + Convert.ToDateTime(P_FromDOJ == "" ? "1900-01-01" : P_FromDOJ).ToString("dd-MMM-yyyy") + " To " +
                Convert.ToDateTime(P_ToDOJ == "" ? "9999-12-31" : P_ToDOJ).ToString("dd-MMM-yyyy");

            strHeader = strHeader + (SearchBy != "" ? (SearchBy == "EC" ? " (Search By : Employee Code " + (FindOption == "0" ? " Like '" : " = '") + SearchString :
                " (Search By : Employee Name " + (FindOption == "0" ? " Like '" : " = '") + SearchString) : "");

            string exlDateTime = oDBEngine.GetDate(113).ToString();
            string exlTime = exlDateTime.Replace(":", "");
            exlTime = exlTime.Replace(" ", "");

            if (DsExport.Tables.Count > 0)
                if (DsExport.Tables[0].Rows.Count > 0)
                {
                    DtExport = DsExport.Tables[0];
                    ReportHeader[0] = strHeader;
                    string FileName = "EmployeeDetail_" + exlTime;
                    strSavePath = "~/Documents/";

                    //SRLNO,Name,FatherName,DOJ,Department,BranchName,CTC,ReportTo,Designation,Company,
                    //Email_Ids,PhoneMobile_Numbers,PanCardNumber,CreatedBy
                    string[] ColumnType = { "V", "V", "V", "V", "V", "V", "V", "V", "V", "V", "V", "V", "V", "V" };
                    string[] ColumnSize = { "10", "150", "150", "50", "50", "100", "100", "100", "100", "150", "150", "150", "150", "150" };
                    string[] ColumnWidthSize = { "5", "30", "30", "12", "22", "23", "15", "25", "23", "25", "25", "25", "20", "20" };

                    oGenericExcelExport.ExportToExcel(ColumnType, ColumnSize, ColumnWidthSize, DtExport, Server.MapPath(strSavePath), "2007", FileName, ReportHeader, null);
                }
        }
        #endregion

        #region Business Logic
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Code  Added and Commented By Priti on 16122016 to add Convert.ToString instead of ToString()
            if (!IsPostBack)
            {
                //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'
                //string sPath = HttpContext.Current.Request.Url.ToString();
                string sPath = Convert.ToString(HttpContext.Current.Request.Url);
                oDBEngine.Call_CheckPageaccessebility(sPath);

                string[] PageSession = { "PageSize", "FromDOJ", "ToDoj", "PageNumAfterNav", "SerachString", "SearchBy", "FindOption", "ShowFilter_SearchString" };
                oGenericMethod = new BusinessLogicLayer.GenericMethod();
                oGenericMethod.PageInitializer(BusinessLogicLayer.GenericMethod.WhichCall.DistroyUnWantedSession_AllExceptPage, PageSession);
                oGenericMethod.PageInitializer(BusinessLogicLayer.GenericMethod.WhichCall.DistroyUnWantedSession_Page, PageSession);

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/Master/employee.aspx");

            // Rev Mantis Issue 25001
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSInsertUpdateUser");
            proc.AddPara("@ACTION", "ShowSettingsActivateEmployeeBranchHierarchy");
            dt = proc.GetTable();
            if (dt.Rows.Count > 0)
            {
                string mastersettings = Convert.ToString(dt.Rows[0]["Value"]);
                if (mastersettings == "0")
                {
                    ActivateEmployeeBranchHierarchy = false;
                }
                else
                {
                    ActivateEmployeeBranchHierarchy = true;
                }
            }
            // End of Rev Mantis Issue 25001

            if (HttpContext.Current.Session["userid"] == null)
            {
                ////Page.ClientScript.RegisterStartupScript(GetType(), "SighOff", "<script>SignOff();</script>");
            }



            if (!IsPostBack)
            {
                // Rev 4.0
                //GetemployeeSupervisor();
                // End of Rev 4.0

                //Page.ClientScript.RegisterStartupScript(GetType(), "PageLD", "<script>Pageload();</script>");
                //Initialize Variable
                P_PageSize = "10";
                DtFrom.Date = oDBEngine.GetDate().AddDays(-30);
                DtTo.Date = oDBEngine.GetDate();
                //code Added By Priti on 21122016 to use Export Header,date
                Session["exportval"] = null;
                //....end...
                // Rev 2.0
                string IsShowEmpAndUserSearchInMaster = "0";
                DBEngine obj1 = new DBEngine();
                IsShowEmpAndUserSearchInMaster = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsShowEmpAndUserSearchInMaster'").Rows[0][0]);

                if (IsShowEmpAndUserSearchInMaster == "1")
                {
                    divEmp.Visible = true;
                }
                else
                {
                    divEmp.Visible = false;
                }
                // End of Rev 2.0
            }

            /* Mantise ID:0024752: Optimize FSM Employee Master
            Rev work Swati Date:-15.03.2022*/
            //BindGrid();
            /* Mantise ID:0024752: Optimize FSM Employee Master
                Rev work close Swati Date:-15.03.2022*/

            //else { Page.ClientScript.RegisterStartupScript(GetType(), "PageLD", "<script>Pageload();</script>"); }
            //if (hdn_GridBindOrNotBind.Value != "False")
            //{
            //    if (P_FromDOJ != null && P_FromDOJ.Trim() != String.Empty && P_ToDOJ != null && P_ToDOJ.Trim() != String.Empty &&
            //        P_PageSize != null && P_PageNumAfterNav != null)
            //    {
            //        //When Show Filter Active With SearchString
            //        //Then Grid Bind With SearchString Cretaria
            //        //Otherwise normally as it bind
            //        if (P_ShowFilter_SearchString != null)
            //            if ( Convert.ToString(P_ShowFilter_SearchString) != String.Empty)
            //                oAspxHelper.BindGrid(GrdEmployee, Fetch_EmployeeData(String.IsNullOrEmpty(P_FromDOJ ) ? "1900-01-01" : P_FromDOJ, String.IsNullOrEmpty(P_ToDOJ) ? "9999-12-31" : P_ToDOJ, P_PageSize,
            //                   Convert.ToString(P_PageNumAfterNav), P_SearchString, P_SearchBy, P_FindOption, "S", "Y", P_ShowFilter_SearchString));
            //            else
            //                oAspxHelper.BindGrid(GrdEmployee, Fetch_EmployeeData(String.IsNullOrEmpty(P_FromDOJ) ? "1900-01-01" : P_FromDOJ, String.IsNullOrEmpty(P_ToDOJ) ? "9999-12-31" : P_ToDOJ, P_PageSize,
            //                Convert.ToString(P_PageNumAfterNav), P_SearchString, P_SearchBy, P_FindOption, "S", "N", String.Empty));
            //        else
            //            oAspxHelper.BindGrid(GrdEmployee, Fetch_EmployeeData(String.IsNullOrEmpty(P_FromDOJ ) ? "1900-01-01" : P_FromDOJ, String.IsNullOrEmpty(P_ToDOJ) ? "9999-12-31" : P_ToDOJ, P_PageSize,
            //            Convert.ToString(P_PageNumAfterNav), P_SearchString, P_SearchBy, P_FindOption, "S", "N", String.Empty));
            //    }
            //  }
            // 
        }

        // Mantis Issue 24752_Rectify
        //protected void BindGrid()
        //{
        //    string strFromDOJ = String.Empty, strToDOJ = String.Empty, strSearchString = String.Empty,
        //        strSearchBy = String.Empty, strFindOption = String.Empty;

        //    Ds_Global = new DataSet();
        //    Ds_Global = Fetch_EmployeeData(strFromDOJ == "" ? "1900-01-01" : strFromDOJ, strToDOJ == "" ? "9999-12-31" : strToDOJ, P_PageSize,
        //        "1", strSearchString, strSearchBy, strFindOption, "S", "N", String.Empty);
        //    if (Ds_Global.Tables.Count > 0)
        //    {
        //        //Debjyoti 070217
        //        //Reason : Filter all employee to current employee 
        //        string CurrentComp = Convert.ToString(HttpContext.Current.Session["LastCompany"]);

        //        // Code Commented And Modified By Sam due to Show All child 
        //        //company employee with parent company if we log in with Parent Company 
        //        //version 1.0.0.1
        //        //string[] cmpId = oDBEngine.GetFieldValue1("tbl_master_company", "cmp_id", " cmp_internalid='" + CurrentComp + "'", 1);


        //        // DataRow[] extraRow= Ds_Global.Tables[0].Select("organizationid <>" + cmpId[0]);
        //        //foreach (DataRow dr in extraRow)
        //        //{
        //        //    Ds_Global.Tables[0].Rows.Remove(dr);
        //        //} 
        //        // GrdEmployee.DataSource = Ds_Global.Tables[0];
        //        // GrdEmployee.DataBind();

        //        //version 1.0.0.1 End
        //        Employee_BL objEmploye = new Employee_BL();
        //        string ListOfCompany = "";
        //        string[] cmpId = oDBEngine.GetFieldValue1("tbl_master_company", "cmp_id", " cmp_internalid='" + CurrentComp + "'", 1);
        //        string Companyid = Convert.ToString(cmpId[0]);
        //        string Allcompany = "";
        //        string ChildCompanyid = objEmploye.getChildCompany(CurrentComp, ListOfCompany);
        //        if (ChildCompanyid != "")
        //        {
        //            Allcompany = Companyid + "," + ChildCompanyid;
        //            Allcompany = Allcompany.TrimEnd(',');
        //        }
        //        else
        //        {
        //            Allcompany = Companyid;
        //        }
        //        DataRow[] extraRow = Ds_Global.Tables[0].Select("organizationid not in(" + Allcompany + ")");
        //        foreach (DataRow dr in extraRow)
        //        {
        //            Ds_Global.Tables[0].Rows.Remove(dr);
        //        }
        //        GrdEmployee.DataSource = Ds_Global.Tables[0];
        //        GrdEmployee.DataBind();


        //    }

        //}
        // End of Mantis Issue 24752_Rectify


        /*Mantise ID:0024752: Optimize FSM Employee Master
         Rev work Swati Date:-15.03.2022*/
        // Mantis Issue 24752_Rectify
        //public DataTable GetEmpListRecord()
        public DataSet GetEmpListRecord()
            // End of Mantis Issue 24752_Rectify
        {
            //DataTable ds = new DataTable();
            DataSet Ds = new DataSet();
            try
            {
                string strFromDOJ = String.Empty, strToDOJ = String.Empty, strSearchString = String.Empty,
                strSearchBy = String.Empty, strFindOption = String.Empty;

                ProcedureExecute proc = new ProcedureExecute("FSM_HR_Fetch_Employees");
                proc.AddPara("@FromJoinDate", strFromDOJ == "" ? "1900-01-01" : strFromDOJ);
                proc.AddPara("@ToJoinDate", strToDOJ == "" ? "9999-12-31" : strToDOJ);
                proc.AddPara("@PageSize", P_PageSize);
                proc.AddPara("@PageNumber", "1");
                proc.AddPara("@SearchString", strSearchString);
                proc.AddPara("@SearchBy", strSearchBy);
                proc.AddPara("@FindOption", strFindOption);
                proc.AddPara("@ExportType", "S");
                proc.AddPara("@DevXFilterOn", "N");
                proc.AddPara("@DevXFilterString", String.Empty);
                proc.AddPara("@User_id", Convert.ToInt32(Session["userid"]));
                // Rev 2.0
                proc.AddPara("@Employees", Convert.ToString(txtEmployee_hidden.Value));
                // End of Rev 2.0

                // Mantis Issue 24752_Rectify
                //ds = proc.GetTable();
                Ds = proc.GetDataSet();
                // End of Mantis Issue 24752_Rectify
            }
            catch
            {
                // return RedirectToAction("Logout", "Login", new { Area = "" });

            }
            return Ds;
        }
        protected void EntityServerModelogDataSource_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            e.KeyExpression = "cnt_id";
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            // Mantis Issue 24752_Rectify
            //DataTable dtCmb = GetEmpListRecord();
            // End of Mantis Issue 24752_Rectify
            // Rev 1.0
            string IsFilter = Convert.ToString(hfIsFilter.Value);
            // End of Rev 1.0

            ERPDataClassesDataContext dc1 = new ERPDataClassesDataContext(connectionString);

            // Rev 1.0
            if (IsFilter == "Y")
            {
                // End of Rev 1.0
                var q = from d in dc1.FSMEmployee_Masters
                        where d.USERID == Convert.ToInt64(HttpContext.Current.Session["userid"].ToString())
                        // Mantis Issue 24752_Rectify
                        orderby d.cnt_id descending
                        // End of Mantis Issue 24752_Rectify
                        select d;
                e.QueryableSource = q;
                // Rev 1.0
            }
            else
            {
                var q = from d in dc1.FSMEmployee_Masters
                        where d.SRLNO == 0
                        select d;
                e.QueryableSource = q;
            }
            // End of Rev 1.0
        }
        /*Mantise ID:0024752: Optimize FSM Employee Master
       Rev work Close Swati Date:-15.03.2022*/
        protected void GrdEmployee_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GrdEmployee.JSProperties["cpPagerSetting"] = null;
            GrdEmployee.JSProperties["cpExcelExport"] = null;
            GrdEmployee.JSProperties["cpRefreshNavPanel"] = null;
            GrdEmployee.JSProperties["cpCallOtherWhichCallCondition"] = null;
            // Mantis Issue 24752_Rectify
            GrdEmployee.JSProperties["cpLoadData"] = null;
            // End of Mantis Issue 24752_Rectify

            // Code  Added  By Priti on 16122016 to use for delete employee
            GrdEmployee.JSProperties["cpDelete"] = null;
            WhichCall = e.Parameters.Split('~')[0];
            string strFromDOJ = String.Empty, strToDOJ = String.Empty, strSearchString = String.Empty,
            strSearchBy = String.Empty, strFindOption = String.Empty;
            string WhichType = null;

            if (Convert.ToString(e.Parameters).Contains("~"))
            {
                WhichType = Convert.ToString(e.Parameters).Split('~')[1];
            }
            if (WhichCall == "Delete")
            {

                DataTable dtUser = new DataTable();
                dtUser = oDBEngine.GetDataTable("tbl_master_user", " top 1 * ", "user_contactId='" + WhichType + "'");

                /// Coded By Samrat Roy -- 18/04/2017 
                /// To Delete Contact Type selection on Employee Type (DME/ISD)
                Employee_BL objEmployee_BL = new Employee_BL();
                objEmployee_BL.DeleteContactType(WhichType);

                if (dtUser.Rows.Count > 0)
                {

                    //  globalsetting.EmployeeDeleteBySelctName(Convert.ToString(WhichType), Convert.ToString(dtUser.Rows[0]["user_id"]));
                    //  this.Page.ClientScript.RegisterStartupScript(GetType(), "height12", "<script>alert('Employee already tagged with User.');</script>");
                    GrdEmployee.JSProperties["cpDelete"] = "Failure";
                    // BindGrid();


                    // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ValdationMessage", "loginMessage();", true);
                }
                else
                {
                    globalsetting.EmployeeDeleteBySelctName(Convert.ToString(WhichType), DBNull.Value.ToString());
                    this.Page.ClientScript.RegisterStartupScript(GetType(), "height15", "<script>alert('Employee has been deleted.');</script>");
                    GrdEmployee.JSProperties["cpDelete"] = "Success";
                    /*Mantise ID:0024752: Optimize FSM Employee Master
                    Rev work Swati Date:-15.03.2022*/
                    //BindGrid();
                    // Mantis Issue 24752_Rectify
                    //GrdEmployee.DataBind();
                    // End of Mantis Issue 24752_Rectify
                    /*Mantise ID:0024752: Optimize FSM Employee Master
                        Rev work Close Swati Date:-15.03.2022*/

                }
            }

            //.............end........................








            //common parameter
            //  strFromDOJ = oDBEngine.GetDate(BusinessLogicLayer.DBEngine.DateConvertFrom.UTCToOnlyDate, e.Parameters.Split('~')[1]);
            //  strToDOJ = oDBEngine.GetDate(BusinessLogicLayer.DBEngine.DateConvertFrom.UTCToOnlyDate, e.Parameters.Split('~')[2]);

            //if (Rb_SearchBy.SelectedItem.Value.ToString() != "N")
            //{
            //    if (Rb_SearchBy.SelectedItem.Value.ToString() == "EN")//Find By Emp Name
            //    {
            //        strSearchString = txtEmpName.Text;
            //        strSearchBy = "EN";
            //        strFindOption = cmbEmpNameFindOption.SelectedItem.Value.ToString();
            //    }
            //    else //Find By Emp Code
            //    {
            //        strSearchString = txtEmpCode.Text;
            //        strSearchBy = "EC";
            //        strFindOption = cmbEmpCodeFindOption.SelectedItem.Value.ToString();
            //    }
            //}
            int TotalItems = 0;
            int TotalPage = 0;
            //if (WhichCall == "Show")
            //{
            // Rev 1.0
            if (WhichCall == "Show" || WhichCall== "Delete") { 
                //Set Show filter's FilterExpression Empty and For Fresh Record Fetch
                GrdEmployee.FilterExpression = string.Empty;
                P_ShowFilter_SearchString = null;

                Ds_Global = new DataSet();
                // Mantis Issue 24752_Rectify
                //Ds_Global = Fetch_EmployeeData(strFromDOJ == "" ? "1900-01-01" : strFromDOJ, strToDOJ == "" ? "9999-12-31" : strToDOJ, P_PageSize,
                //    "1", strSearchString, strSearchBy, strFindOption, "S", "N", String.Empty);     
                Ds_Global = GetEmpListRecord();
                // End of Mantis Issue 24752_Rectify

                if (Ds_Global.Tables.Count > 0)
                {
                    // Count Employee Grid Data Start

                    string CurrentComp = Convert.ToString(HttpContext.Current.Session["LastCompany"]);

                    Employee_BL objEmploye = new Employee_BL();
                    string ListOfCompany = "";
                    string[] cmpId = oDBEngine.GetFieldValue1("tbl_master_company", "cmp_id", " cmp_internalid='" + CurrentComp + "'", 1);
                    string Companyid = Convert.ToString(cmpId[0]);
                    string Allcompany = "";
                    string ChildCompanyid = objEmploye.getChildCompany(CurrentComp, ListOfCompany);
                    if (ChildCompanyid != "")
                    {
                        Allcompany = Companyid + "," + ChildCompanyid;
                        Allcompany = Allcompany.TrimEnd(',');
                    }
                    else
                    {
                        Allcompany = Companyid;
                    }
                    DataRow[] extraRow = Ds_Global.Tables[0].Select("organizationid not in(" + Allcompany + ")");
                    foreach (DataRow dr in extraRow)
                    {
                        Ds_Global.Tables[0].Rows.Remove(dr);
                    }

                    // Count Employee Grid Data End

                    if (Ds_Global.Tables[0].Rows.Count > 0)
                    {
                        TotalItems = Convert.ToInt32(Ds_Global.Tables[0].Rows[0]["TotalRecord"].ToString());
                        TotalPage = TotalItems % Convert.ToInt32(P_PageSize) == 0 ? (TotalItems / Convert.ToInt32(P_PageSize)) : (TotalItems / Convert.ToInt32(P_PageSize)) + 1;
                        GrdEmployee.JSProperties["cpRefreshNavPanel"] = "ShowBtnClick~1~" + TotalPage.ToString() + '~' + TotalItems.ToString();
                        // Mantis Issue 24752_Rectify
                        GrdEmployee.JSProperties["cpLoadData"] = "Success";
                        // End of Mantis Issue 24752_Rectify
                        /* Mantise ID:0024752: Optimize FSM Employee Master
                        Rev work Swati Date:-15.03.2022*/
                        // oAspxHelper.BindGrid(GrdEmployee, Ds_Global);
                        // Mantis Issue 24752_Rectify
                        //  GrdEmployee.DataBind();
                        // End of Rev Sanhita
                        /*Mantise ID:0024752: Optimize FSM Employee Master
                Rev work Close Swati Date:-15.03.2022*/
                    }
                }
                // Mantis Issue 24752_Rectify
                //    else
                //        /* Mantise ID:0024752: Optimize FSM Employee Master
                //  Rev work Swati Date:-15.03.2022*/
                //      //  oAspxHelper.BindGrid(GrdEmployee);
                //        GrdEmployee.DataBind();
                //    /*Mantise ID:0024752: Optimize FSM Employee Master
                //Rev work Close Swati Date:-15.03.2022*/
                //}
            // Rev 1.0
            }
            // End of Rev 1.0
            //else
            //    /* Mantise ID:0024752: Optimize FSM Employee Master
            //  Rev work Swati Date:-15.03.2022*/
            //    //oAspxHelper.BindGrid(GrdEmployee);
            //GrdEmployee.DataBind();
                // End of Mantis Issue 24752_Rectify
            /*Mantise ID:0024752: Optimize FSM Employee Master
             Rev work Close Swati Date:-15.03.2022*/

            P_PageNumAfterNav = "1";
            //For All Date
            if (rbDOJ_Specific_All.SelectedItem.Value.ToString() == "A")
            {
                P_FromDOJ = String.Empty;
                P_ToDOJ = String.Empty;
            }
            //}
            //if (WhichCall == "SearchByNavigation")
            //{
            //    //strFromDOJ = oDBEngine.GetDate(BusinessLogicLayer.DBEngine.DateConvertFrom.UTCToOnlyDate, e.Parameters.Split('~')[1]);
            //    //strToDOJ = oDBEngine.GetDate(BusinessLogicLayer.DBEngine.DateConvertFrom.UTCToOnlyDate, e.Parameters.Split('~')[2]);
            //    string strPageNum = String.Empty;
            //    string strNavDirection = String.Empty;
            //    int PageNumAfterNav = 0;
            //    strPageNum = e.Parameters.Split('~')[3];
            //    strNavDirection = e.Parameters.Split('~')[4];

            //    //Set Page Number
            //    if (strNavDirection == "RightNav")
            //        PageNumAfterNav = Convert.ToInt32(strPageNum) + 10;
            //    if (strNavDirection == "LeftNav")
            //        PageNumAfterNav = Convert.ToInt32(strPageNum) - 10;
            //    if (strNavDirection == "PageNav")
            //        PageNumAfterNav = Convert.ToInt32(strPageNum);

            //    Ds_Global = new DataSet();

            //    //When Show Filter Active With SearchString
            //    //Then Grid Bind With SearchString Cretaria
            //    //other normally as it bind
            //    if (P_ShowFilter_SearchString != null)
            //        if (P_ShowFilter_SearchString.ToString() != String.Empty)
            //            Ds_Global = Fetch_EmployeeData(P_FromDOJ == "" ? "1900-01-01" : P_FromDOJ, P_ToDOJ == "" ? "9999-12-31" : P_ToDOJ, P_PageSize,
            //                        PageNumAfterNav.ToString(), "", "", "", "S", "Y", P_ShowFilter_SearchString.ToString());
            //        else
            //            Ds_Global = Fetch_EmployeeData(strFromDOJ == "" ? "1900-01-01" : strFromDOJ, strToDOJ == "" ? "9999-12-31" : strToDOJ, P_PageSize,
            //                        PageNumAfterNav.ToString(), strSearchString, strSearchBy, strFindOption, "S", "N", String.Empty);
            //    else
            //        Ds_Global = Fetch_EmployeeData(strFromDOJ == "" ? "1900-01-01" : strFromDOJ, strToDOJ == "" ? "9999-12-31" : strToDOJ, P_PageSize,
            //                    PageNumAfterNav.ToString(), strSearchString, strSearchBy, strFindOption, "S", "N", String.Empty);

            //    if (Ds_Global.Tables.Count > 0)
            //    {
            //        if (Ds_Global.Tables[0].Rows.Count > 0)
            //        {
            //            TotalItems = Convert.ToInt32(Ds_Global.Tables[0].Rows[0]["TotalRecord"].ToString());
            //            TotalPage = TotalItems % Convert.ToInt32(P_PageSize) == 0 ? (TotalItems / Convert.ToInt32(P_PageSize)) : (TotalItems / Convert.ToInt32(P_PageSize)) + 1;
            //            //GrdEmployee.JSProperties["cpPagerSetting"] = strPageNum + "~" + TotalPage + "~" + TotalItems;
            //            oAspxHelper.BindGrid(GrdEmployee, Ds_Global);
            //            GrdEmployee.JSProperties["cpRefreshNavPanel"] = strNavDirection + '~' + strPageNum + '~' + TotalPage.ToString() + '~' + TotalItems.ToString();
            //        }
            //        else
            //            oAspxHelper.BindGrid(GrdEmployee);
            //    }
            //    else
            //        oAspxHelper.BindGrid(GrdEmployee);

            //    P_PageNumAfterNav = PageNumAfterNav.ToString();
            //}
            if (WhichCall == "ExcelExport")
            {
                GrdEmployee.JSProperties["cpExcelExport"] = "T";
            }
            if (WhichCall == "ShowHideFilter")
            {
                if (e.Parameters.Split('~')[3] == "s")
                    GrdEmployee.Settings.ShowFilterRow = true;

                if (e.Parameters.Split('~')[3] == "All")
                {
                    GrdEmployee.FilterExpression = string.Empty;
                    P_ShowFilter_SearchString = null; //Close Search Cretaria When All Record Show filter On
                    GrdEmployee.JSProperties["cpCallOtherWhichCallCondition"] = "Show";//Reset On Starting Position
                }
            }

            //Assing Value in Properties(Contain Session) To Maintain Call Back Value To Be Used On Server Side Events
            P_FromDOJ = strFromDOJ;
            P_ToDOJ = strToDOJ;
            P_SearchString = strSearchString;
            P_FindOption = strFindOption;
            P_SearchBy = strSearchBy;

            //Dispose Object
            if (Ds_Global != null)
                Ds_Global.Dispose();

            // Page.ClientScript.RegisterStartupScript(GetType(), "PageLD", "<script>Pageload();</script>");

        }
        //protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Ds_Global = new DataSet();
        //    string strSearchString = String.Empty,
        //       strSearchBy = String.Empty, strFindOption = String.Empty;
        //    if (Rb_SearchBy.SelectedItem.Value.ToString() != "N")
        //    {
        //        if (Rb_SearchBy.SelectedItem.Value.ToString() == "EN")
        //        {
        //            strSearchString = txtEmpName.Text;
        //            strSearchBy = "EN";
        //            strFindOption = cmbEmpNameFindOption.SelectedItem.Value.ToString();
        //        }
        //        else
        //        {
        //            strSearchString = txtEmpCode.Text;
        //            strSearchBy = "EC";
        //            strFindOption = cmbEmpCodeFindOption.SelectedItem.Value.ToString();
        //        }
        //    }

        //    if (P_ShowFilter_SearchString != null)
        //        if (P_ShowFilter_SearchString.ToString() != String.Empty)
        //            Ds_Global = Fetch_EmployeeData(P_FromDOJ == "" ? "1900-01-01" : P_FromDOJ, P_ToDOJ == "" ? "9999-12-31" : P_ToDOJ, "0",
        //                        "0", strSearchString, strSearchBy, strFindOption, "E", "Y", P_ShowFilter_SearchString.ToString());
        //        else
        //            Ds_Global = Fetch_EmployeeData(P_FromDOJ == "" ? "1900-01-01" : P_FromDOJ, P_ToDOJ == "" ? "9999-12-31" : P_ToDOJ, "0",
        //                        "0", strSearchString, strSearchBy, strFindOption, "E", "N", String.Empty);
        //    else
        //        Ds_Global = Fetch_EmployeeData(P_FromDOJ == "" ? "1900-01-01" : P_FromDOJ, P_ToDOJ == "" ? "9999-12-31" : P_ToDOJ, "0",
        //                    "0", strSearchString, strSearchBy, strFindOption, "E", "N", String.Empty);
        //    ExportToExcel(Ds_Global, P_FromDOJ, P_ToDOJ, strSearchString, strSearchBy, strFindOption);

        //    //Dispose Object
        //    if (Ds_Global != null)
        //        Ds_Global.Dispose();

        //}

        #endregion

        //protected void GrdEmployee_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        //{
        //    if (P_ShowFilter_SearchString != null)
        //        if (P_ShowFilter_SearchString.ToString().Trim() != String.Empty)
        //            P_ShowFilter_SearchString = null;

        //    Ds_Global = new DataSet();
        //    int TotalItems = 0;
        //    int TotalPage = 0;
        //    //For All Date
        //    if (rbDOJ_Specific_All.SelectedItem.Value.ToString() == "A")
        //    {
        //        P_FromDOJ = String.Empty;
        //        P_ToDOJ = String.Empty;
        //    }
        //    if (P_ShowFilter_SearchString != null)
        //    {
        //        if (P_ShowFilter_SearchString.ToString().Trim() != String.Empty)
        //            P_ShowFilter_SearchString = P_ShowFilter_SearchString.ToString().Trim() + " And " + e.Criteria.ToString().Trim();
        //        else
        //            P_ShowFilter_SearchString = e.Criteria.ToString().Trim();
        //    }
        //    else
        //        P_ShowFilter_SearchString = e.Criteria.ToString().Trim();

        //    Ds_Global = Fetch_EmployeeData(P_FromDOJ == "" ? "1900-01-01" : P_FromDOJ, P_ToDOJ == "" ? "9999-12-31" : P_ToDOJ, P_PageSize,
        //           P_PageNumAfterNav, "", "", "", "S", "Y", P_ShowFilter_SearchString.ToString());

        //    if (Ds_Global.Tables.Count > 0)
        //    {
        //        if (Ds_Global.Tables[0].Rows.Count > 0)
        //        {
        //            TotalItems = Convert.ToInt32(Ds_Global.Tables[0].Rows[0]["TotalRecord"].ToString());
        //            TotalPage = TotalItems % Convert.ToInt32(P_PageSize) == 0 ? (TotalItems / Convert.ToInt32(P_PageSize)) : (TotalItems / Convert.ToInt32(P_PageSize)) + 1;
        //            oAspxHelper.BindGrid(GrdEmployee, Ds_Global);
        //            //Here I Passed ShowBtnClick Parameter Cause It ReInitialize Grid Like Show Button Functionality
        //            GrdEmployee.JSProperties["cpRefreshNavPanel"] = "ShowBtnClick" + '~' + P_PageNumAfterNav + '~' + TotalPage.ToString() + '~' + TotalItems.ToString();
        //        }
        //        else
        //            oAspxHelper.BindGrid(GrdEmployee);
        //    }
        //    else
        //        oAspxHelper.BindGrid(GrdEmployee);

        //    //Dispose Object
        //    if (Ds_Global != null)
        //        Ds_Global.Dispose();


        //}
        public void bindexport(int Filter)
        {
            //Code  Added and Commented By Priti on 21122016 to use Export Header,date
            GrdEmployee.Columns[10].Visible = false;
            string filename = "Employees";
            exporter.FileName = filename;

            // Rev 5.0
            //exporter.PageHeader.Left = "Employees";
            //exporter.MaxColumnWidth = 70;
            //// exporter.LeftMargin = 0;
            //// exporter.Styles.Cell.Font.Size = 8;
            //exporter.PageFooter.Center = "[Page # of Pages #]";
            //exporter.PageFooter.Right = "[Date Printed]";
            //switch (Filter)
            //{
            //    case 1:
            //        exporter.WritePdfToResponse();
            //        break;
            //    case 2:
            //        exporter.WriteXlsToResponse();
            //        break;
            //    case 3:
            //        exporter.WriteRtfToResponse();
            //        break;
            //    case 4:
            //        exporter.WriteCsvToResponse();
            //        break;
            //}

            if (Filter == 1 || Filter == 2)
            {
                SqlConnection con = new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
                con.Open();
                // Rev 6.0
                //string selectQuery = "SELECT Name [Name], Code as [Employee Code] ,Employee_Grade [Grade] , cnt_OtherID [Other ID], Company [Company], BranchName [Branch] ,Department [Department], Designation [Designation],CTC [CTC],DOJ [Joining On], ReportTo [Report To], AdditionalReportingHead [Additional Reporting Head], Colleague [Colleague], Colleague1 [Colleague1], Colleague2 [Colleague2] from FSMEmployee_Master Where USERID=" + Convert.ToInt32(Session["userid"]) + " order by cnt_id desc";
                string selectQuery = "SELECT Name [Name], Code as [Employee Code] ,Employee_Grade [Grade] , cnt_OtherID [Other ID], Company [Company], BranchName [Branch] ,Department [Department], Designation [Designation],CTC [CTC],DOJ [Joining On], ReportTo [Report To], AdditionalReportingHead [Additional Reporting Head], Colleague [Colleague], Colleague1 [Colleague1], Colleague2 [Colleague2], EmpTargetLevel [EmpTargetLevel] from FSMEmployee_Master Where USERID=" + Convert.ToInt32(Session["userid"]) + " order by cnt_id desc";
                // End of Rev 6.0
                SqlDataAdapter myCommand = new SqlDataAdapter(selectQuery, con);

                // Create and fill a DataSet.
                DataSet ds = new DataSet();
                myCommand.Fill(ds, "Main");
                //myCommand = new SqlDataAdapter("Select DOC_TYPE,CONVERT(DECIMAL(18,2),REPLACE(REPLACE(BAL_AMOUNT,'(',CASE WHEN SUBSTRING(BAL_AMOUNT,1,1)='(' THEN '-' ELSE '' END),')','')) AS BAL_AMOUNT from PARTYOUTSTANDINGDET_REPORT Where USERID=" + Convert.ToInt32(Session["userid"]) + " AND SLNO=999999999 AND DOC_TYPE='Gross Outstanding:' AND PARTYTYPE='C'", con);
                //myCommand.Fill(ds, "GrossTotal");
                myCommand.Dispose();
                con.Dispose();
                Session["exportcustoutdetdataset"] = ds;

                //string strduedatechk = (chkduedate.Checked) ? "1" : "0";
                //string strprintdatechk = (chkprintdays.Checked) ? "1" : "0";
                //string strsalesman = (chksalesman.Checked) ? "1" : "0";
                //string strpartyordnodt = (chkPartyOrdNoDt.Checked) ? "1" : "0";
                //string ProjectSelectInEntryModule = cbl.GetSystemSettingsResult("ProjectSelectInEntryModule");

                dtExport = ds.Tables[0].Copy();
                //dtExport.Clear();
                //dtExport.Columns.Add(new DataColumn("Name", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("Grade", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("Other ID", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("Joining On", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("Department", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("Branch", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("CTC", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("Report To", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("Additional Reporting Head", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("Colleague", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("Colleague1", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("Colleague2", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("Designation", typeof(string)));
                //dtExport.Columns.Add(new DataColumn("Company", typeof(string)));
               
                //foreach (DataRow dr1 in ds.Tables[0].Rows)
                //{
                //    DataRow row2 = dtExport.NewRow();

                //    row2["Name"] = dr1["Name"];
                //    row2["Grade"] = dr1["Employee_Grade"];
                //    row2["Other ID"] = dr1["cnt_OtherID"];
                //    row2["Joining On"] = dr1["DOJ"];
                //    row2["Department"] = dr1["Department"];
                //    row2["Branch"] = dr1["BranchName"];
                //    row2["CTC"] = dr1["CTC"];
                //    row2["Report To"] = dr1["ReportTo"];
                //    row2["Additional Reporting Head"] = dr1["AdditionalReportingHead"];
                //    row2["Colleague"] = dr1["Colleague"];
                //    row2["Colleague1"] = dr1["Colleague1"];
                //    row2["Colleague2"] = dr1["Colleague2"];
                //    row2["Designation"] = dr1["Designation"];
                //    row2["Company"] = dr1["Company"];
                    
                //    dtExport.Rows.Add(row2);
                //}

                
                //For Excel/PDF Header
                BusinessLogicLayer.Reports GridHeaderDet = new BusinessLogicLayer.Reports();
                dtReportHeader.Columns.Add(new DataColumn("Header", typeof(String)));

                string GridHeader = "";
                GridHeader = GridHeaderDet.CommonReportHeader(Convert.ToString(Session["LastCompany"]), Convert.ToString(Session["LastFinYear"]), true, false, false, false, false);
                DataRow HeaderRow = dtReportHeader.NewRow();
                HeaderRow[0] = GridHeader.ToString();
                dtReportHeader.Rows.Add(HeaderRow);
                DataRow HeaderRow1 = dtReportHeader.NewRow();
                HeaderRow1[0] = Convert.ToString(Session["BranchNames"]);
                dtReportHeader.Rows.Add(HeaderRow1);
                GridHeader = GridHeaderDet.CommonReportHeader(Convert.ToString(Session["LastCompany"]), Convert.ToString(Session["LastFinYear"]), false, true, false, false, false);
                DataRow HeaderRow2 = dtReportHeader.NewRow();
                HeaderRow2[0] = GridHeader.ToString();
                dtReportHeader.Rows.Add(HeaderRow2);
                GridHeader = GridHeaderDet.CommonReportHeader(Convert.ToString(Session["LastCompany"]), Convert.ToString(Session["LastFinYear"]), false, false, true, false, false);
                DataRow HeaderRow3 = dtReportHeader.NewRow();
                HeaderRow3[0] = GridHeader.ToString();
                dtReportHeader.Rows.Add(HeaderRow3);
                GridHeader = GridHeaderDet.CommonReportHeader(Convert.ToString(Session["LastCompany"]), Convert.ToString(Session["LastFinYear"]), false, false, false, true, false);
                DataRow HeaderRow4 = dtReportHeader.NewRow();
                HeaderRow4[0] = GridHeader.ToString();
                dtReportHeader.Rows.Add(HeaderRow4);
                GridHeader = GridHeaderDet.CommonReportHeader(Convert.ToString(Session["LastCompany"]), Convert.ToString(Session["LastFinYear"]), false, false, false, false,true);
                DataRow HeaderRow5 = dtReportHeader.NewRow();
                HeaderRow5[0] = GridHeader.ToString();
                dtReportHeader.Rows.Add(HeaderRow5);
                DataRow HeaderRow6 = dtReportHeader.NewRow();
                HeaderRow6[0] = "Employee Master";
                dtReportHeader.Rows.Add(HeaderRow6);
                //DataRow HeaderRow7 = dtReportHeader.NewRow();
                //HeaderRow7[0] = "As On: " + Convert.ToDateTime(ASPxAsOnDate.Date).ToString("dd-MM-yyyy");
                //dtReportHeader.Rows.Add(HeaderRow7);

                //For Excel/PDF Footer
                dtReportFooter.Columns.Add(new DataColumn("Footer", typeof(String))); //0
                DataRow FooterRow1 = dtReportFooter.NewRow();
                dtReportFooter.Rows.Add(FooterRow1);
                DataRow FooterRow2 = dtReportFooter.NewRow();
                dtReportFooter.Rows.Add(FooterRow2);
                DataRow FooterRow = dtReportFooter.NewRow();
                FooterRow[0] = "* * *  End Of Report * * *   ";
                dtReportFooter.Rows.Add(FooterRow);
            }
            else
            {
                exporter.PageHeader.Font.Size = 10;
                exporter.PageHeader.Font.Name = "Tahoma";
                exporter.GridViewID = "GrdEmployee";
            }

            switch (Filter)
            {
                case 1:
                    objExcel.ExportToExcelforExcel(dtExport, "Employee Master", "NOcompareString", "NOcompareString1", dtReportHeader, dtReportFooter);
                    break;
                case 2:
                    objExcel.ExportToPDF(dtExport, "Employee Master", "NOcompareString", "NOcompareString1",  dtReportHeader, dtReportFooter);
                    break;
                case 3:
                    exporter.WriteCsvToResponse();
                    break;

                default:
                    return;
            }
            // End of Rev 5.0
        }
        protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bindUserGroups();           
            Int32 Filter = int.Parse(Convert.ToString(drdExport.SelectedItem.Value));
            //Code  Added and Commented By Priti on 21122016 to use Export Header,date
            if (Filter != 0)
            {
                if (Session["exportval"] == null)
                {
                    Session["exportval"] = Filter;
                    bindexport(Filter);
                }
                else if (Convert.ToInt32(Session["exportval"]) != Filter)
                {
                    Session["exportval"] = Filter;
                    bindexport(Filter);
                }
            }
        }
        protected void WriteToResponse(string fileName, bool saveAsFile, string fileFormat, MemoryStream stream)
        {
            if (Page == null || Page.Response == null) return;
            string disposition = saveAsFile ? "attachment" : "inline";
            Page.Response.Clear();
            Page.Response.Buffer = false;
            Page.Response.AppendHeader("Content-Type", string.Format("application/{0}", fileFormat));
            Page.Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Page.Response.AppendHeader("Content-Disposition", string.Format("{0}; filename={1}.{2}", disposition, HttpUtility.UrlEncode(fileName).Replace("+", "%20"), fileFormat));
            if (stream.Length > 0)
                Page.Response.BinaryWrite(stream.ToArray());
            //Page.Response.End();
        }
        protected void SelectPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            //string a = "";


            string strSplitCommand = e.Parameter.Split('~')[0];
            string strEmpId = e.Parameter.Split('~')[1];
            hdnContactId.Value = strEmpId;
            if (strSplitCommand == "Bindalldesignes")
            {

                SelectPanel.JSProperties["cpResult"] = "";
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("Fetch_Employee_DataSet");
                proc.AddVarcharPara("@Action", 500, "FetchEmployeeData");
                //  proc.AddVarcharPara("@IBRef", 200, Convert.ToString(Session["IBRef"]));
                proc.AddVarcharPara("@EmpCode", 200, strEmpId);
                ds = proc.GetDataSet();


                if (ds.Tables[0].Rows.Count > 0)
                {
                    CmbDesignName.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["emp_IsDeActive"]);
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        TxtReason.Text = Convert.ToString(ds.Tables[1].Rows[0]["emp_ReasonOfDeactivate"]);
                    }
                    else
                    {
                        TxtReason.Text = "";
                    }

                }
                else
                {
                    CmbDesignName.Checked = false;
                }

            }
            else if (strSplitCommand == "SaveDetails")
            {

                string strReason = "";
                bool strActivation = Convert.ToBoolean(CmbDesignName.Value);
                if (strActivation)
                {
                    strReason = Convert.ToString(TxtReason.Text);
                }
                string strUser = Convert.ToString(HttpContext.Current.Session["userid"]);
                DataSet ds = new DataSet();
                ProcedureExecute proc = new ProcedureExecute("Fetch_Employee_DataSet");
                proc.AddVarcharPara("@Action", 500, "SaveEmployeeData");
                proc.AddVarcharPara("@UserId", 200, strUser);
                proc.AddVarcharPara("@EmpCode", 200, strEmpId);
                proc.AddBooleanPara("@IsActive", strActivation);
                proc.AddVarcharPara("@Reason", 200, strReason);
                ds = proc.GetDataSet();


                if (Convert.ToString(ds.Tables[0].Rows[0]["Result"]) == "Success")
                {
                    SelectPanel.JSProperties["cpResult"] = "Success";
                }
                else if (Convert.ToString(ds.Tables[0].Rows[0]["Result"]) == "Problem")
                {
                    SelectPanel.JSProperties["cpResult"] = "Problem";
                }
            }

        }



        SalesPersontracking ob = new SalesPersontracking();

        // Rev 4.0
        //public void GetemployeeSupervisor()
        //{
        //    try
        //    {

        //        DataTable dtfromtosumervisor = ob.FetchEmployeeFTS("Past", Convert.ToString(Session["userid"]));

        //        fromsuper.DataSource = dtfromtosumervisor;
        //        fromsuper.DataTextField = "Name";
        //        fromsuper.DataValueField = "Id";
        //        fromsuper.DataBind();

        //        DataTable dtfromtosumervisornew = ob.FetchEmployeeFTS("New", Convert.ToString(Session["userid"]));
        //        tosupervisor.DataSource = dtfromtosumervisornew;
        //        tosupervisor.DataTextField = "Name";
        //        tosupervisor.DataValueField = "Id";
        //        tosupervisor.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(GetType(), "PageLD", ex.Message);

        //    }
        //}

        public class EmployeeSuperModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
        }
        [WebMethod]
        public static object GetOnDemandEmployeefromsuper(string SearchKey, string Action)
        {
            List<EmployeeSuperModel> listSuperEmployee = new List<EmployeeSuperModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("Proc_FTS_ERP_EmployeeList");
                proc.AddPara("@SearchKey", SearchKey);
                proc.AddPara("@Action", Action);
                proc.AddPara("@userid", Convert.ToInt32(HttpContext.Current.Session["userid"]));
                dt = proc.GetTable();

                listSuperEmployee = (from DataRow dr in dt.Rows
                                select new EmployeeSuperModel()
                                {
                                    Id = Convert.ToString(dr["Id"]),
                                    Name = Convert.ToString(dr["Name"]),
                                    Code = Convert.ToString(dr["cnt_UCC"])
                                }).ToList();
            }

            return listSuperEmployee;
        }
        // End of Rev 4.0


        [WebMethod]
        public static string Submitsupervisor(string fromsuper, string tosuper)
        {

            DataTable dtfromtosumervisor = SalesPersontracking.SubmitSupervisorEmployeeFTS(fromsuper, tosuper, Convert.ToString(HttpContext.Current.Session["userid"]));

            return fromsuper;
        }

        [WebMethod]
        public static bool GetStateListSubmit(string EMPID, List<string> Statelist)
        {
            string StateId = "";
            int i = 1;

            if (Statelist != null && Statelist.Count > 0)
            {
                foreach (string item in Statelist)
                {
                    if (item == "0")
                    {
                        StateId = "0";
                        break;
                    }
                    else
                    {
                        if (i > 1)
                            StateId = StateId + "," + item;
                        else
                            StateId = item;
                        i++;
                    }
                }

            }

            DataTable dtfromtosumervisor = SalesPersontracking.SubmitEmployeeState(EMPID, StateId, Convert.ToString(HttpContext.Current.Session["userid"]));

            return true;
        }
        [WebMethod]
        public static List<StateList> GetStateList(string EMPID)
        {
            BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
            string query = "Select  state as StateName ,id as StateID from tbl_master_state  order by state";
            List<StateList> omodel = new List<StateList>();
            // DataTable dt = objEngine.GetDataTable(query);
            DataTable dt = new DataTable();

            //  DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("Proc_User_StateMAP");

            proc.AddPara("@EMPID", EMPID);

            dt = proc.GetTable();

            if (dt != null && dt.Rows.Count > 0)
            {

                omodel = UtilityLayer.APIHelperMethods.ToModelList<StateList>(dt);

            }

            return omodel;

        }

        [WebMethod]
        public static String GetEmployeeID(string EMPID)
        {
            String Return = null;
            BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
            string query = "Select cnt_ucc from tbl_master_contact where cnt_internalId='"+EMPID+"' ";
            
            DataTable dt = objEngine.GetDataTable(query);
            if (dt != null && dt.Rows.Count > 0)
            {
                Return = dt.Rows[0]["cnt_ucc"].ToString();
            }

            return Return;
        }

        [WebMethod]
        public static String GetEmployeeIDUpdate(string EMPID, String newEmpID)
        {
            String sreturn="";
            DataTable dtfromtosumervisor = SalesPersontracking.UpdateEmployeeID(EMPID, newEmpID, Convert.ToString(HttpContext.Current.Session["userid"]));

            if (dtfromtosumervisor!=null && dtfromtosumervisor.Rows.Count>0)
            {
                sreturn = dtfromtosumervisor.Rows[0]["msg"].ToString();
            }

            return sreturn;
        }
        //Mantis Issue 24982
        [WebMethod]
        public static List<EmployeeChannel> GetEmployeeChannel(string EMPID)
        {
            BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
            List<EmployeeChannel> omodel = new List<EmployeeChannel>();
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FetchEmployeeChannelCircleSection");

            proc.AddPara("@emp_cntId", EMPID);

            dt = proc.GetTable();

            if (dt != null && dt.Rows.Count > 0)
            {

                omodel = UtilityLayer.APIHelperMethods.ToModelList<EmployeeChannel>(dt);

            }

            return omodel;
        }
        public class EmployeeChannel
        {
            public string EmpName { get; set; }
            public string ReportToEmpName { get; set; }
            public string DeputyEmpName { get; set; }
            public string ColleagueName { get; set; }
            public string Colleague_1Name { get; set; }
            public string Colleague_2Name { get; set; }
            public string EmpChannel { get; set; }
            public string ReportToChannel { get; set; }
            public string DeputyChannel { get; set; }
            public string ColleagueChannel { get; set; }
            public string ColleagueChannel_1 { get; set; }
            public string ColleagueChannel_2 { get; set; }
            public string EmployeeCircle { get; set; }
            public string ReportToCircle { get; set; }
            public string DeputyCircle { get; set; }
            public string ColleagueCircle { get; set; }
            public string ColleagueCircle_1 { get; set; }
            public string ColleagueCircle_2 { get; set; }
            public string EmployeeSection { get; set; }
            public string ReportToSection { get; set; }
            public string DeputySection { get; set; }
            public string ColleagueSection { get; set; }
            public string ColleagueSection_1 { get; set; }
            public string ColleagueSection_2 { get; set; }

        }
        //End of Mantis Issue 24982
        //Mantis Issue 25001
        [WebMethod]
        public static List<BranchList> GetBranchList(string EMPID)
        {
            BusinessLogicLayer.DBEngine objEngine = new BusinessLogicLayer.DBEngine();
            List<BranchList> omodel = new List<BranchList>();
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_User_BranchMAP");
            proc.AddPara("@EMPID", EMPID);
            dt = proc.GetTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                omodel = UtilityLayer.APIHelperMethods.ToModelList<BranchList>(dt);
            }
            return omodel;
        }

        [WebMethod]
        public static bool GetBranchListSubmit(string EMPID, List<string> Branchlist)
        {
            Employee_BL objEmploye = new Employee_BL();
            string BranchId = "";
            int i = 1;

            if (Branchlist != null && Branchlist.Count > 0)
            {
                foreach (string item in Branchlist)
                {
                    if (item == "0")
                    {
                        BranchId = "0";
                        break;
                    }
                    else
                    {
                        if (i > 1)
                            BranchId = BranchId + "," + item;
                        else
                            BranchId = item;
                        i++;
                    }
                }

            }

            DataTable dtfromtosumervisor = objEmploye.SubmitEmployeeBranch(EMPID, BranchId, Convert.ToString(HttpContext.Current.Session["userid"]));

            return true;
        }
        public class BranchList
        {
            public long branch_id { get; set; }
            public String branch_description { get; set; }
            public bool IsChecked { get; set; }
            public string status { get; set; }
        }
        //End of Mantis Issue 25001
        public class StateList
        {

            public int StateID { get; set; }
            public string StateName { get; set; }
            public string status { get; set; }

            public bool IsChecked { get; set; }
        }

        // Rev 2.0
        public class EmployeeModel
        {
            public string id { get; set; }
            public string Employee_Name { get; set; }
            public string Employee_Code { get; set; }
        }
        [WebMethod]
        public static object GetOnDemandEmployee(string SearchKey)
        {
            List<EmployeeModel> listEmployee = new List<EmployeeModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_EmployeeNameSearch");
                proc.AddPara("@USER_ID", Convert.ToInt32(HttpContext.Current.Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                dt = proc.GetTable();

                listEmployee = (from DataRow dr in dt.Rows
                                select new EmployeeModel()
                                {
                                    id = Convert.ToString(dr["cnt_internalId"]),
                                    Employee_Code = Convert.ToString(dr["cnt_UCC"]),
                                    Employee_Name = Convert.ToString(dr["Employee_Name"])
                                }).ToList();
            }

            return listEmployee;
        }

       

        // End of Rev 2.0

        //Import Employee User

        protected void lnlDownloaderexcel_Click(object sender, EventArgs e)
        {
            string strFileName = "Employee Master Data.xlsx";
            string strPath = (Convert.ToString(System.AppDomain.CurrentDomain.BaseDirectory) + "/CommonFolder/" + strFileName);

            Response.ContentType = "application/xlsx";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Employee Master Data.xlsx");
            Response.TransmitFile(strPath);
            Response.End();
        }


        protected void BtnSaveexcel_Click1(object sender, EventArgs e)
        {
            string fName = string.Empty;
            Boolean HasLog = false;
            if (OFDBankSelect.FileContent.Length != 0)
            {
                path = String.Empty;
                path1 = String.Empty;
                FileName = String.Empty;
                s = String.Empty;
                time = String.Empty;
                cannotParse = String.Empty;
                string strmodule = "InsertTradeData";

                BusinessLogicLayer.TransctionDescription td = new BusinessLogicLayer.TransctionDescription();

                FilePath = Path.GetFullPath(OFDBankSelect.PostedFile.FileName);
                FileName = Path.GetFileName(FilePath);
                string fileExtension = Path.GetExtension(FileName);

                if (fileExtension.ToUpper() != ".XLS" && fileExtension.ToUpper() != ".XLSX")
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "PageScript", "<script language='javascript'>jAlert('Uploaded file format not supported by the system');</script>");
                    return;
                }

                if (fileExtension.Equals(".xlsx"))
                {
                    fName = FileName.Replace(".xlsx", DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx");
                }

                else if (fileExtension.Equals(".xls"))
                {
                    fName = FileName.Replace(".xls", DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls");
                }

                else if (fileExtension.Equals(".csv"))
                {
                    fName = FileName.Replace(".csv", DateTime.Now.ToString("ddMMyyyyhhmmss") + ".csv");
                }

                Session["FileName"] = fName;

                String UploadPath = Server.MapPath((Convert.ToString(ConfigurationManager.AppSettings["SaveCSV"]) + Session["FileName"].ToString()));
                OFDBankSelect.PostedFile.SaveAs(UploadPath);
                ClearArray();
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                try
                {
                    HttpPostedFile file = OFDBankSelect.PostedFile;
                    String extension = Path.GetExtension(FileName);
                    HasLog = Import_To_Grid(UploadPath, extension, file);
                }
                catch (Exception ex)
                {
                    HasLog = false;
                }
                Page.ClientScript.RegisterStartupScript(GetType(), "PageScript", "<script language='javascript'>jAlert('Import Process Successfully Completed!'); ShowLogData('" + HasLog + "');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "PageScript", "<script language='javascript'>jAlert('Selected File Cannot Be Blank');</script>");
            }
        }

        public void ClearArray()
        {
            Array.Clear(InputName, 0, InputName.Length - 1);
            Array.Clear(InputType, 0, InputType.Length - 1);
            Array.Clear(InputValue, 0, InputValue.Length - 1);
        }

        public Boolean Import_To_Grid(string FilePath, string Extension, HttpPostedFile file)
        {
            Boolean Success = false;
            Boolean HasLog = false;
            int loopcounter = 1;
            if (file.FileName.Trim() != "")
            {
                if (Extension.ToUpper() == ".XLS" || Extension.ToUpper() == ".XLSX")
                {
                    DataSet ds = new DataSet();
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FilePath, false))
                    {
                        Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                        IEnumerable<Sheet> sheets = doc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                                                
                        foreach (var item in sheets)
                        {
                            DataTable dt = new DataTable();
                            Worksheet worksheet = (doc.WorkbookPart.GetPartById(item.Id.Value) as WorksheetPart).Worksheet;
                            IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                            foreach (Row row in rows)
                            {
                                if (row.RowIndex.Value == 1)
                                {
                                    foreach (Cell cell in row.Descendants<Cell>())
                                    {
                                        if (cell.CellValue != null)
                                        {
                                            dt.Columns.Add(GetValue(doc, cell));
                                        }
                                    }
                                }
                                else
                                {
                                    dt.Rows.Add();
                                    int i = 0;
                                    foreach (Cell cell in row.Descendants<Cell>())
                                    {
                                        if (cell.CellValue != null)
                                        {
                                            dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                                        }
                                        i++;
                                    }
                                }
                            }

                            ds.Tables.Add(dt);
                        }
                        

                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string EmployeeCode = string.Empty;

                        //foreach (DataRow row in ds.Tables[0].Rows)
                        //{
                        //    loopcounter++;
                           // EmployeeCode = Convert.ToString(row["Emp. Code*"]);
                            try
                            {
                                string File_Name = Session["FileName"].ToString();
                                DataTable dtCmb = new DataTable();
                                ProcedureExecute proc = new ProcedureExecute("PRC_EmployeeUserInsertFromExcel");
                                proc.AddPara("@ImportEmployee", ds.Tables[0]);
                                //Rev 3.0
                                //proc.AddPara("@ImportUser", ds.Tables[1]);
                                proc.AddPara("@FileName", File_Name);
                                //Rev 3.0 END
                                proc.AddPara("@CreateUser_Id", Convert.ToInt32(Session["userid"]));
                                dtCmb = proc.GetTable();
                                HasLog = true;
                            }
                            catch (Exception ex)
                            {
                                HasLog = false;
                                int loginsert = objEmploye.InsertEmployeeImportLOg(EmployeeCode, loopcounter, "", "", Session["FileName"].ToString(), ex.Message.ToString(), "Failed");

                            }
                        //}
                    }
                }
            }
            return HasLog;
        }
        //Rev 3.0
        protected void GvImportDetailsSearch_DataBinding(object sender, EventArgs e)
        {
           
            string fileName = Convert.ToString(Session["FileName"]);
            DataSet dt2 = objEmploye.GetEmployeeLog(fileName);
            GvImportDetailsSearch.DataSource = dt2.Tables[0];
        }
        //Rev 3.0 End
        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }


    }
}


