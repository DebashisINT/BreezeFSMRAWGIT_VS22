﻿/******************************************************************************************************
 * Rev 1.0      Priti       20/02/2023      V2.0.39    	0025676: Employee Import Facility
 * Rev 2.0      Sanchita    22-05-2023      v2.0.40     The first name field of the employee master should consider 150 character from the application end. 
                                                        Refer: 26187
 *******************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BusinessLogicLayer;
using System.Reflection;

namespace BusinessLogicLayer
{
     
    public class Employee_BL
    {
        string actual = "";

        // Mantis Issue 24655 [parameters Colleague1, Colleague2 added]
        public int btnCTC_Click_BL(string emp_cntId, string emp_dateofJoining,
       string emp_Organization, string emp_JobResponsibility, string emp_Designation, string emp_type,
       string emp_Department, string emp_reportTo, string emp_deputy, string emp_colleague,
       string emp_workinghours, string emp_totalLeavePA, string emp_LeaveSchemeAppliedFrom, string emp_branch, string emp_Remarks,
       string Colleague1, string Colleague2 )
        {
            ProcedureExecute proc;
            try
            {
                using (proc = new ProcedureExecute("EmployeeCTCInsert"))
                {
                    proc.AddNVarcharPara("@emp_cntId", 100, emp_cntId);
                    proc.AddNVarcharPara("@emp_dateofJoining", 100, emp_dateofJoining);
                    proc.AddIntegerPara("@emp_Organization", Convert.ToInt32(emp_Organization));
                    proc.AddIntegerPara("@emp_JobResponsibility", Convert.ToInt32(emp_JobResponsibility));
                    proc.AddIntegerPara("@emp_Designation", Convert.ToInt32(emp_Designation));
                    proc.AddIntegerPara("@emp_type", Convert.ToInt32(emp_type));

                    proc.AddIntegerPara("@emp_Department", Convert.ToInt32(emp_Department));
                    proc.AddIntegerPara("@emp_reportTo", Convert.ToInt32(emp_reportTo));
                    proc.AddIntegerPara("@emp_deputy", Convert.ToInt32(emp_deputy));
                    proc.AddIntegerPara("@emp_colleague", Convert.ToInt32(emp_colleague));
                    proc.AddIntegerPara("@emp_workinghours", Convert.ToInt32(emp_workinghours));
                    proc.AddDecimalPara("@emp_currentCTC", 0, 18, 0);
                    proc.AddDecimalPara("@emp_basic", 0, 18, 0);
                    proc.AddDecimalPara("@emp_HRA", 0, 18, 0);
                    proc.AddDecimalPara("@emp_CCA", 0, 18, 0);
                    proc.AddDecimalPara("@emp_spAllowance", 0, 18, 0);
                    proc.AddDecimalPara("@emp_childrenAllowance", 0, 18, 0);
                    proc.AddNVarcharPara("@emp_totalLeavePA", 100, emp_totalLeavePA);

                    proc.AddDecimalPara("@emp_PF", 0, 18, 0);
                    proc.AddDecimalPara("@emp_medicalAllowance", 0, 18, 0);
                    proc.AddDecimalPara("@emp_LTA", 0, 18, 0);
                    proc.AddDecimalPara("@emp_convence", 0, 18, 0);
                    proc.AddDecimalPara("@emp_mobilePhoneExp", 0, 18, 0);
                    //proc.AddNVarcharPara("@emp_totalLeavePA", 100, emp_totalLeavePA);
                    proc.AddNVarcharPara("@emp_totalMedicalLeavePA", 100, "");

                    proc.AddIntegerPara("@userid", Convert.ToInt32(HttpContext.Current.Session["userid"]));
                    proc.AddNVarcharPara("@emp_LeaveSchemeAppliedFrom", 100, emp_LeaveSchemeAppliedFrom);
                    proc.AddIntegerPara("@emp_branch", Convert.ToInt32(emp_branch));
                    proc.AddNVarcharPara("@emp_Remarks", 100, emp_Remarks);

                    proc.AddDecimalPara("@EMP_CarAllowance", 0, 18, 0);
                    proc.AddDecimalPara("@EMP_UniformAllowance", 0, 18, 0);
                    proc.AddDecimalPara("@EMP_BooksPeriodicals", 0, 18, 0);
                    proc.AddDecimalPara("@EMP_SeminarAllowance", 0, 18, 0);
                    proc.AddDecimalPara("@EMP_OtherAllowance", 0, 18, 0);
                    // Mantis Issue 24655
                    proc.AddIntegerPara("@emp_colleague1", Convert.ToInt32(Colleague1));
                    proc.AddIntegerPara("@emp_colleague2", Convert.ToInt32(Colleague2));
                    // End of Mantis Issue 24655

                    //return
                    int NoOfRowEffected = proc.RunActionQuery();
                    return NoOfRowEffected;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        // Mantis Issue 24655 [ paarmeter ChannelType, Circle, Section added . Also ChannelDefault, CircleDefault and SectionDefault added.]
        public string btnSave_Click_BL(string cnt_ucc, string cnt_salutation, string cnt_firstName,
       string cnt_middleName, string cnt_lastName, string cnt_shortName, string cnt_branchId,
       string cnt_sex, string cnt_maritalStatus, string DOBDate, string AnniversaryDate,
       string cmbLegalStatus, string cmbEducation, string cmbSource, string referedID, string cmbBloodgroup,
       bool chkAllow, string password, string ChannelType, string Circle, string Section, string DefaultType)
        {
            ProcedureExecute proc;

            try
            {
                using (proc = new ProcedureExecute("EmployeeInsert"))
                {
                    proc.AddVarcharPara("@cnt_ucc", 100, cnt_ucc);

                    proc.AddVarcharPara("@cnt_salutation", 100, cnt_salutation);

                    // Rev 2.0
                    //proc.AddVarcharPara("@cnt_firstName", 100, cnt_firstName);
                    proc.AddVarcharPara("@cnt_firstName", 150, cnt_firstName);
                    // End of Rev 2.0

                    proc.AddVarcharPara("@cnt_middleName", 100, cnt_middleName);

                    proc.AddVarcharPara("@cnt_lastName", 100, cnt_lastName);

                    proc.AddVarcharPara("@cnt_shortName", 100, cnt_shortName);

                    proc.AddVarcharPara("@cnt_branchId", 100, cnt_branchId);

                    proc.AddVarcharPara("@cnt_sex", 100, cnt_sex);

                    proc.AddVarcharPara("@cnt_maritalStatus", 100, cnt_maritalStatus);


                    if (DOBDate != null)
                    {
                        proc.AddVarcharPara("@cnt_DOB", 100, DOBDate);
                    }
                    else
                    {
                        proc.AddVarcharPara("@cnt_DOB", 100, "");
                    }
                    if (AnniversaryDate != null)
                    {
                        proc.AddVarcharPara("@cnt_anniversaryDate", 100, AnniversaryDate);
                    }
                    else
                    {
                        proc.AddVarcharPara("@cnt_anniversaryDate", 100, "");
                    }
                    proc.AddVarcharPara("@cnt_legalStatus", 100, cmbLegalStatus);
                    proc.AddVarcharPara("@cnt_education", 100, cmbEducation);
                    proc.AddVarcharPara("@cnt_contactSource", 100, cmbSource);
                    proc.AddVarcharPara("@cnt_referedBy", 100, referedID);
                    proc.AddVarcharPara("@cnt_contactType", 100, "EM");
                    proc.AddVarcharPara("@lastModifyUser", 100, Convert.ToString(HttpContext.Current.Session["userid"]));
                    proc.AddVarcharPara("@UserContactID", 100, Convert.ToString(HttpContext.Current.Session["usercontactID"]));
                    proc.AddVarcharPara("@bloodgroup", 100, cmbBloodgroup);
                    string webLogin = "No";
                    string Password = string.Empty;
                    if (chkAllow == true)
                    {
                        webLogin = "Yes";
                        Password = password;
                    }
                    proc.AddVarcharPara("@webLogin", 100, webLogin);
                    proc.AddVarcharPara("@Password", 100, Password);
                    // Mantis Issue 24655
                    proc.AddVarcharPara("@ChannelType", 100, ChannelType);
                    proc.AddVarcharPara("@Circle", 100, Circle);
                    proc.AddVarcharPara("@Section", 100, Section);
                    proc.AddVarcharPara("@DefaultType",50, DefaultType);
                    // End of Mantis Issue 24655

                    proc.AddVarcharPara("@result", 100, "", QueryParameterDirection.Output);
                    int NoOfRowEffected = proc.RunActionQuery();
                    //string RetVal = Convert.ToString(proc.GetParaValue("@RetVal"));
                    string RetVal = Convert.ToString(proc.GetParaValue("@result"));
                    return RetVal;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }

        public DataTable GetAssignedEmployeeDetailByReportingTo(string cnt_internalId)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeeAllDtl");
            proc.AddNVarcharPara("@action", 150, "GetAssignedEmployeeDetailByReportingTo");
            proc.AddNVarcharPara("@cnt_internalId", 50, cnt_internalId);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetNewSupervisorList(string cnt_internalId, string activityid, string supervisorId, string assignedToId)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeeAllDtl");
            proc.AddNVarcharPara("@action", 150, "GetNewSupervisorList");
            proc.AddNVarcharPara("@cnt_internalId", 50, cnt_internalId);
            proc.AddNVarcharPara("@activityid", 50, activityid);
            proc.AddNVarcharPara("@supervisorId", 50, supervisorId);
            proc.AddNVarcharPara("@assignedToId", 50, assignedToId);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetReassignEmployeeDetailByReportingTo(string cnt_internalId)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeeAllDtl");
            proc.AddNVarcharPara("@action", 150, "GetReassignEmployeeDetailByReportingTo");
            proc.AddNVarcharPara("@cnt_internalId", 50, cnt_internalId);
            dt = proc.GetTable();
            return dt;
        }


        public DataTable GetReassignEmployee(string cnt_internalId)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeeAllDtl");
            proc.AddNVarcharPara("@action", 150, "GetAssignedEmployeeDetailByReportingTo");
            proc.AddNVarcharPara("@cnt_internalId", 50, cnt_internalId);
          
            dt = proc.GetTable();
            return dt;
        }


        public DataTable GetReassignUser(string cnt_internalId,string UserId)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeeAllDtl");
            proc.AddNVarcharPara("@action", 150, "GetReassignUser");
            proc.AddNVarcharPara("@cnt_internalId", 50, cnt_internalId);
            proc.AddNVarcharPara("@assignedToId", 50, UserId);
            dt = proc.GetTable();
            return dt;
        }

        //aDDED BY:SUBHABRATA
        public DataTable GetEmailAccountConfigDetails(string Create_User,int action)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_GetEmailSetUpDetails");
            proc.AddNVarcharPara("@CreateUser", 150, Create_User);
            proc.AddIntegerPara("@Action", action);
            dt = proc.GetTable();
            return dt;
        }
        //aDDED BY:Kaushik
        public DataTable GetEmailAccountSupervisorSalesmanConfigDetails(string Create_User, int action, string sls_ID)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_GetEmailSetUpDetails");
            proc.AddNVarcharPara("@CreateUser", 150, Create_User);
            proc.AddIntegerPara("@Action", action);

            proc.AddDecimalPara("@sls_ID", 2, 18, Convert.ToDecimal(sls_ID));
            dt = proc.GetTable();
            return dt;
        }

        //aDDED BY:SUBHABRATA
        public DataTable GetAllLevelUsers(string Create_User, int action)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_GetEmailSetUpDetails");
            proc.AddNVarcharPara("@CreateUser", 150, Create_User);
            proc.AddIntegerPara("@Action", action);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetEmailLevelUsersWise(int Create_User, int action,int UserLevel)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_GetEmailSetUpDetails");
            proc.AddIntegerPara("@CreateUser", Create_User);
            proc.AddIntegerPara("@Action", action);
            proc.AddIntegerPara("@User_Level", UserLevel);
            dt = proc.GetTable();
            return dt;
        }

        //Added By:Subhabrata :21-0-2017 Saturday
        public bool AddEmailTemplateDefaultDetails(string Create_User)
        {
            bool IsUpdate = false;
            try
            {

                ProcedureExecute proc = new ProcedureExecute("Prc_EmailTemplate_IsDefault_Chk");
                proc.AddNVarcharPara("@SenderType", 150, Create_User);
                proc.GetScalar();
                IsUpdate = true;
            }
            catch
            {
                IsUpdate = false;
            }
            return IsUpdate;
        }
        public bool AddCustVendHistory(string GSTIn,int CNT_id,DateTime ApplicableFrom,string CreatedBy,string Action)
        {
            bool IsUpdate = false;
            try
            {

                ProcedureExecute proc = new ProcedureExecute("Prc_SaveCustVendHistory");
                proc.AddNVarcharPara("@NewGSTIN_ID", 150, GSTIn);
                proc.AddIntegerPara("@Cnt_Id",CNT_id);
                proc.AddDateTimePara("@ApplicableFrom", ApplicableFrom);
                proc.AddNVarcharPara("@CreateBy", 150, CreatedBy);
                proc.AddNVarcharPara("@Action", 150, Action);
                proc.GetScalar();
                IsUpdate = true;
            }
            catch
            {
                IsUpdate = false;
            }
            return IsUpdate;
        }

        //Added By:Subhabrata :21-0-2017 Saturday
        public DataTable GetEmailTemplateBySenderType(int Create_User)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Prc_GetEmailTemplateBySenderType");
            proc.AddBigIntegerPara("@SenderType", Create_User);
            dt = proc.GetTable();
            return dt;
        }
        //End

        // Mantis Issue 24655
        public DataTable GetEmployeeList(string userid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("HR_Fetch_Employees");

            proc.AddPara("@FromJoinDate", "1900-01-01");
            proc.AddPara("@ToJoinDate", "1900-01-01");
            proc.AddPara("@PageSize", 10);
            proc.AddPara("@PageNumber", 1);
            proc.AddPara("@SearchString", "");
            proc.AddPara("@SearchBy", "");
            proc.AddPara("@FindOption", "");
            proc.AddPara("@ExportType", "S");
            proc.AddPara("@DevXFilterOn", "N");
            proc.AddPara("@DevXFilterString", "");
            proc.AddPara("@User_id", userid);

            ds = proc.GetTable();
            return ds;
        }
        // End of Mantis Issue 24655

        //kaushik 16_1_2017
        #region GetSupervisorEmployeeDetailByReportingTo  
        public DataTable GetSupervisorEmployeeDetailByReportingTo(string cnt_internalId)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeeAllDtl");
            proc.AddNVarcharPara("@action", 150, "GetSupervisorEmployeeDetailByReportingTo");
            proc.AddNVarcharPara("@cnt_internalId", 50, cnt_internalId);
            dt = proc.GetTable();
            return dt;
        }
        #endregion Sales Visit
        //Subhabrata
        public DataTable GetAssignedEmployeeDetailAndSendMail(string cnt_internalId)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("ProcGetEmailIDByUID");
            proc.AddNVarcharPara("@Action", 150, "Mailsend");
            proc.AddNVarcharPara("@UID", 50, cnt_internalId);
            dt = proc.GetTable();
            return dt;
        }
        //End
        public DataTable GetAssignedSubordinateEmployeeDetailByReportingTo(string cnt_internalId,string AssignId)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeeAllDtl");
            proc.AddNVarcharPara("@action", 150, "GetAssignedSubordinateEmployeeDetailByReportingTo");
            proc.AddNVarcharPara("@cnt_internalId", 50, cnt_internalId);
            proc.AddNVarcharPara("@AssignId ", 50, AssignId);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetEmployeePrefereedProducts(string Uids)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("AddNewProduct");
            proc.AddNVarcharPara("@Module", 100, "GetEmployeePrefereedProducts");
            proc.AddNVarcharPara("@Uids", 1000, Uids);
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetAllProducts()
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("AddNewProduct");
            proc.AddNVarcharPara("@Module", 100, "GetAllProducts");
          
            dt = proc.GetTable();
            return dt;
        }


        public string getChildCompany(string companyId, string ListOfCompany)
        {
            DBEngine objDBEngine = new DBEngine();

            DataTable DtSecond = objDBEngine.GetDataTable(" tbl_master_company ", " cmp_id,cmp_internalid ", " cmp_parentid= '" + companyId + "'");
            if (DtSecond.Rows.Count != 0)
            {
                for (int i = 0; i < DtSecond.Rows.Count; i++)
                {
                    ListOfCompany += DtSecond.Rows[i][0].ToString() + ",";
                    actual += DtSecond.Rows[i][0].ToString() + ",";
                    getChildCompany(DtSecond.Rows[i][1].ToString(), ListOfCompany);
                }
            }
            return actual;
        }

         public int UpdateEmployeeBranch(string empInternalID,int Branchid)
        {
            int i;
            int rtrnvalue = 0;
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeeAllDtl");
            proc.AddNVarcharPara("@action", 150, "UpdateEmployeeBranch");
            proc.AddNVarcharPara("@cnt_internalId", 50, empInternalID);
            proc.AddIntegerPara("@branchId", Branchid);
            proc.AddVarcharPara("@ReturnValue", 50, "0", QueryParameterDirection.Output);
            i = proc.RunActionQuery();
            rtrnvalue = Convert.ToInt32(proc.GetParaValue("@ReturnValue"));
            return rtrnvalue;
        }


        // Code Added By Sam on 23032017 to prevent delete data from tbl_trans_employeectc
        // if number of data in the table =1
         public DataTable NumberOfEmployeeCTC(int empctcid)
         {

             DataTable dt = new DataTable();
             ProcedureExecute proc = new ProcedureExecute("prc_EmployeeAllDtl");
             proc.AddNVarcharPara("@action", 100, "NumberOfEmployeeCTC");
             proc.AddIntegerPara("@empctcid",  empctcid);
             dt = proc.GetTable();
             return dt;
         }

         /// Coded By Samrat Roy -- 18/04/2017 
         /// To Delete Contact Type selection on Employee Type (DME/ISD)
         public int DeleteContactType(string empInternalID)
         {
             int i;
             int rtrnvalue = 0;
             ProcedureExecute proc = new ProcedureExecute("prc_DeleteContactTypeEmplyeeMapping");
             proc.AddNVarcharPara("@cnt_internalId", 20, empInternalID);
             i = proc.RunActionQuery();
             rtrnvalue = Convert.ToInt32(proc.GetParaValue("@ReturnValue"));
             return rtrnvalue;
         }

         //Mantis Issue 25001
         public DataTable SubmitEmployeeBranch(String EMPID, String BranchId, String User_id)
         {
             DataTable dt = new DataTable();
             ProcedureExecute proc = new ProcedureExecute("PRC_EmployeeBranchMapInsertUpdate");
             proc.AddPara("@EMPID", EMPID);
             proc.AddPara("@BranchId", BranchId);
             proc.AddPara("@User_id", User_id);
             dt = proc.GetTable();
             return dt;
         }
         //End of Mantis Issue 25001


        #region  Email Template Helper

         public DataTable GetEmailConfiguartion()
         {

             DataTable dt = new DataTable();
             ProcedureExecute proc = new ProcedureExecute("Proc_Email_Templatehelper");
             proc.AddPara("@Action", "Emailconfig");
             dt = proc.GetTable();
             return dt;
         }
         


         public DataTable Getemailids(string customerId)
         {

             DataTable dt = new DataTable();
             ProcedureExecute proc = new ProcedureExecute("Proc_Email_Templatehelper");
             proc.AddPara("@Action", "GetemailIdofCustomer");
             proc.AddPara("@CustomerID", customerId);

             dt = proc.GetTable();
             return dt;
         }


         public DataTable GetemailidsForChallan(string customerId)
         {

             DataTable dt = new DataTable();
             ProcedureExecute proc = new ProcedureExecute("Proc_Email_Templatehelper");
             proc.AddPara("@Action", "GetemailIdofCustomerChallan");
             proc.AddPara("@CustomerID", customerId);

             dt = proc.GetTable();
             return dt;
         }

         public DataTable Getemailtemplates(string TypeId)
         {

             DataTable dt = new DataTable();
             ProcedureExecute proc = new ProcedureExecute("Proc_Email_Templatehelper");
             proc.AddPara("@Action", "Getemailtemplates");
             proc.AddPara("@TypeID", TypeId);
             dt = proc.GetTable();
             return dt;
         }


         public DataTable Getemailtagsforpurchase(string Id,string Action)
         {

             DataTable dt = new DataTable();
             ProcedureExecute proc = new ProcedureExecute("Proc_Email_Templatehelper");
             proc.AddPara("@Action", Action);
             proc.AddPara("@Id", Id);

             dt = proc.GetTable();
             return dt;
         }


         public static string GetFormattedString<T>(Object myObj, string StringToBeFormatted)
         {
             string FormattedString = "";
             Type temp = typeof(T);
             Type objectType = myObj.GetType();
             T obj = Activator.CreateInstance<T>();

             foreach (PropertyInfo pro in temp.GetProperties())
             {
                 object value = myObj.GetType().GetProperty(pro.Name).GetValue(myObj, null);
                 string ParamName = "@" + pro.Name + "@";
                 string ValueToBeInserted = "";
                 if (value != null)
                 {
                     ValueToBeInserted = value.ToString();
                 }
                 StringToBeFormatted = StringToBeFormatted.Replace(ParamName, ValueToBeInserted);
             }

             FormattedString = StringToBeFormatted;

             return FormattedString;
         }


         public DataTable GetApporvalMail(string branchId, string Typecode)
         {

             DataTable dt = new DataTable();
             ProcedureExecute proc = new ProcedureExecute("Proc_Mailboxvisibility");
             proc.AddPara("@branchId", branchId);
             proc.AddPara("@TypeCode", Typecode);
             dt = proc.GetTable();
             return dt;
         }

         public DataTable GetSystemsettingmail(string Option)
         {

             DataTable dt = new DataTable();
             ProcedureExecute proc = new ProcedureExecute("Proc_SystemsettingforMail");
             proc.AddPara("@Option", Option);

             dt = proc.GetTable();
             return dt;
         }

         public DataTable GetSystemsettingMacAddress(string user,string mac)
         {

             DataTable dt = new DataTable();
             ProcedureExecute proc = new ProcedureExecute("Proc_Mac_accessuser");
             proc.AddPara("@UserId", user);
             proc.AddPara("@Mac", mac);
             dt = proc.GetTable();
             return dt;
         }

        #endregion

        //Rev 1.0
        public DataSet GetEmployeeLog(string Filename)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_EMPLOYEELOG");
            proc.AddVarcharPara("@action", 150, "FetchEmployeeLog");
            proc.AddVarcharPara("@FileName", 150, Filename);
            ds = proc.GetDataSet();
            return ds;
        }
       

        public int InsertEmployeeImportLOg(string empcode, int loopnumber, string empname, string userid, string filename, string description, string status)
        {

            int i;
            //int rtrnvalue = 0;
            ProcedureExecute proc = new ProcedureExecute("PRC_EMPLOYEELOG");
            proc.AddVarcharPara("@action", 150, "insertlog");
            proc.AddVarcharPara("@EmpCode", 50, empcode);
            proc.AddIntegerPara("@LoopNumber", loopnumber);
            proc.AddVarcharPara("@EmpName", 150, empname);
            proc.AddVarcharPara("@UserId", 150, userid);
            proc.AddVarcharPara("@FileName", 150, filename);
            proc.AddVarcharPara("@decription", 150, description);
            proc.AddVarcharPara("@status", 150, status);
            i = proc.RunActionQuery();

            return i;
        }
        //Rev 1.0 End

    }
}
