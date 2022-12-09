﻿using DataAccessLayer;
using ERP.OMS.Management.Master;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace ERP.OMS.Management.Attendance.Service
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]

    public class AttdendanceService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetEmployee(string SerarchKey)
        {
            List<AttendanceEmployee> listEmp = new List<AttendanceEmployee>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SerarchKey = SerarchKey.Replace("'", "''");
                ProcedureExecute proc = new ProcedureExecute("Prc_AttendanceSystem");
                proc.AddVarcharPara("@Action", 100, "Get10Emp");
                proc.AddVarcharPara("@SearchKey", 100, SerarchKey);
                proc.AddVarcharPara("@User", -1, Convert.ToString(HttpContext.Current.Session["userid"]));
                DataTable cust = proc.GetTable(); 

                listEmp = (from DataRow dr in cust.Rows
                           select new AttendanceEmployee()
                            {
                                id = dr["cnt_internalId"].ToString(),
                                EmpCode = dr["EmpCode"].ToString(),
                                EmpName = Convert.ToString(dr["Name"]) 
                            }).ToList();
            }

            return listEmp;
        }




        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetEmployeeByBranch(string SerarchKey,string BranchId)
        {
            List<AttendanceEmployee> listEmp = new List<AttendanceEmployee>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SerarchKey = SerarchKey.Replace("'", "''");
                ProcedureExecute proc = new ProcedureExecute("Prc_AttendanceSystem");
                proc.AddVarcharPara("@Action", 100, "Get10EmpByBranch");
                proc.AddVarcharPara("@SearchKey", 100, SerarchKey);
                proc.AddVarcharPara("@branchId", 100, BranchId);
                proc.AddVarcharPara("@BranchHierchy", -1, Convert.ToString(Session["userbranchHierarchy"]).Trim());
                proc.AddVarcharPara("@User", -1, Convert.ToString(HttpContext.Current.Session["userid"]));
                DataTable cust = proc.GetTable();

                listEmp = (from DataRow dr in cust.Rows
                           select new AttendanceEmployee()
                           {
                               id = dr["cnt_internalId"].ToString(),
                               EmpCode = dr["EmpCode"].ToString(),
                               EmpName = Convert.ToString(dr["Name"])
                           }).ToList();
            }

            return listEmp;
        }




        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object SaveAttendance(string EmpId)
        {
            if (HttpContext.Current.Session["userid"] != null)
            {
                try
                {
                    ProcedureExecute proc = new ProcedureExecute("Prc_AttendanceSystem");
                    proc.AddVarcharPara("@Action", 100, "SaveAttendance");
                    proc.AddVarcharPara("@EmpId", 20, EmpId);
                    proc.RunActionQuery();

                    return new { status = "Ok", Msg = "Saved Successfully." };
                }
                catch (Exception ex) {
                    return new { status = "Error", Msg = ex.Message };
                }
            }
            else
            {
                return new { status = "Error", Msg = "Please Re-login." };
            }
            
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object WorkValueBind(string userid)
        {
            List<WorkTypeList> list = new List<WorkTypeList>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                    DataTable dt = new DataTable();
                    ProcedureExecute proc = new ProcedureExecute("proc_FTS_WorkTypes");
                    proc.AddVarcharPara("@Action", 100, "WorkTypes");
                    proc.AddVarcharPara("@User_id", 100,userid);
                    dt = proc.GetTable();
                    list = (from DataRow dr in dt.Rows
                            select new WorkTypeList()
                               {
                                   ID = dr["ID"].ToString(),
                                   Descrpton = Convert.ToString(dr["Descrpton"])
                               }).ToList();
            }
            return list;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object LeaveValueBind(string userid)
        {
            List<LeaveTypeList> list = new List<LeaveTypeList>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("proc_FTS_LeaveTypes");
                proc.AddVarcharPara("@Action", 100, "WorkTypes");
                proc.AddVarcharPara("@user_id", 100, userid);
                dt = proc.GetTable();
                list = (from DataRow dr in dt.Rows
                        select new LeaveTypeList()
                        {
                            id = dr["id"].ToString(),
                            name = Convert.ToString(dr["type_name"])
                        }).ToList();
            }
            return list;
        }


        public class AttendanceEmployee 
        {

            public string id { get; set; }
            public string EmpName { get; set; }
            public string EmpCode { get; set; }
        }

        public class WorkTypeList
        {
            public string ID { get; set; }
            public string Descrpton { get; set; }
        }

        public class LeaveTypeList
        {
            public string id { get; set; }
            public string name { get; set; }
        }
    }
}
