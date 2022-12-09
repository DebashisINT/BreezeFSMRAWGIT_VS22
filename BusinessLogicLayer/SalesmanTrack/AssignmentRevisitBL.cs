using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
    public class AssignmentRevisitBL
    {
        public DataTable GetEmployeeList(Int32 userid, Int32 EmpTypeID, Int32 CounterType, string State, string Designation, Int32 SettingMonth, Int32 SettingYear, String Type)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetList_Get");
            proc.AddPara("@EMPTYPEID", EmpTypeID);
            proc.AddPara("@STATE", State);
            proc.AddPara("@DESIGNATION", Designation);
            proc.AddPara("@USERID", userid);
            proc.AddPara("@SETTINGMONTH", SettingMonth);
            proc.AddPara("@SETTINGYEAR", SettingYear);
            proc.AddPara("@COUNTERTYPE", CounterType);
            proc.AddPara("@TYPE", Type);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetEmployeesCounterTargetList(string EmployeeCode, Int32 EmployeeTargetSettingID, Int32 EmployeesCounterTargetID = 0)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetSettingCounterTargetList_Get");
            proc.AddPara("@EMPLOYEECODE", EmployeeCode);
            proc.AddPara("@FKEMPLOYEETARGETSETTINGID", EmployeeTargetSettingID);
            proc.AddPara("@EMPLOYEESCOUNTERTARGETID", EmployeesCounterTargetID);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetRevisitingSettingImportLog(string fromdate = null, string todate = null)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_REVISITLOGVIEW");
            proc.AddPara("@FROMDATE", fromdate);
            proc.AddPara("@TODATE", todate);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetEmployeeStateList(Int32 userid, String Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesStateList_Get");
            proc.AddPara("@USERID", userid);
            proc.AddPara("@ACTION", Action);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetEmployeeDesignationList(Int32 userid, String Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesDesignationList_Get");
            proc.AddPara("@USERID", userid);
            proc.AddPara("@ACTION", Action);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetEmployeeTypeList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetSettingEmpTypeList_Get");
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetEmployeesTargetByCode(String employeecode, String settingtype)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetSetting_Get");
            proc.AddPara("@EMPLOYEECODE", employeecode);
            proc.AddPara("@SETTINGTYPE", settingtype);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetProductBrandList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetSettingProductBrandList_Get");
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetVisitTypeList(String settingtype)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetSettingTypeList_Get");
            proc.AddPara("@SETTINGTYPE", settingtype);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetShopPPList(Int32 CounterType, String Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_ShopPPList_Get");
            proc.AddPara("@COUNTERTYPE", CounterType);
            proc.AddPara("@ACTION", Action);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetEmployeesCounterWiseTargetByCode(String employeecode, Int32 employeetargetsettingid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetSettingCounterTargetList_Get");
            proc.AddPara("@EMPLOYEECODE", employeecode);
            proc.AddPara("@FKEMPLOYEETARGETSETTINGID", employeetargetsettingid);
            ds = proc.GetTable();
            return ds;
        }


        public DataTable GetProductCategoryList()
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetSettingProductCategoryList_Get");
            ds = proc.GetTable();
            return ds;
        }

        public DataTable EmployeesTargetByCodeInsertUpdate(Int32 EmployeeTargetSettingID, Int32 FKEmployeesTargetSettingEmpTypeID, Int32 fkemployeescountertype, String EmployeeCode, Int32 SettingMonth, Int32 SettingYear, Decimal OrderValue, Int32 NewCounter, Decimal Collection, Int32 Revisit)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetSetting_InsertUpdate");
            proc.AddPara("@EMPLOYEETARGETSETTINGID", EmployeeTargetSettingID);
            proc.AddPara("@FKEMPLOYEESTARGETSETTINGEMPTYPEID", FKEmployeesTargetSettingEmpTypeID);
            proc.AddPara("@EMPLOYEECODE", EmployeeCode);
            proc.AddPara("@FKEMPLOYEESCOUNTERTYPE", fkemployeescountertype);
            proc.AddPara("@SETTINGMONTH", SettingMonth);
            proc.AddPara("@SETTINGYEAR", SettingYear);
            proc.AddPara("@ORDERVALUE", OrderValue);
            proc.AddPara("@NEWCOUNTER", NewCounter);
            proc.AddPara("@COLLECTION", Collection);
            proc.AddPara("@REVISIT", Revisit);


            ds = proc.GetTable();
            return ds;
        }

        public DataTable EmployeesTargetByExcelInsertUpdate(string LOGINID, string NEWCOUNTER, string REVISIT, string ORDERVALUE, string COLLECTION, int SETTINGMONTH,
            int SETTINGYEAR, string TYPE, string NAME, string EMPDESIGNATION, string STATE, string SUPERVISOR_ASSIGNED, string DESIGNATION, string Action)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetSetting_ExcelInsertUpdate");
            proc.AddPara("@LOGINID", LOGINID);
            proc.AddPara("@NEWCOUNTER", NEWCOUNTER);
            proc.AddPara("@REVISIT", REVISIT);
            proc.AddPara("@ORDERVALUE", ORDERVALUE);
            proc.AddPara("@COLLECTION", COLLECTION);
            proc.AddPara("@SETTINGMONTH", SETTINGMONTH);
            proc.AddPara("@SETTINGYEAR", SETTINGYEAR);
            proc.AddPara("@TYPE", TYPE);
            proc.AddPara("@NAME", NAME);
            proc.AddPara("@EMPDESIGNATION", EMPDESIGNATION);
            proc.AddPara("@STATE", STATE);
            proc.AddPara("@SUPERVISOR_ASSIGNED", SUPERVISOR_ASSIGNED);
            proc.AddPara("@DESIGNATION", DESIGNATION);
            proc.AddPara("@ACTION", Action);
            ds = proc.GetTable();
            return ds;
        }


        public DataTable GetEmployeesTargetByID(Int32 employeetargetsettingid, String settingtype)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetList_GetByID");
            proc.AddPara("@EMPLOYEETARGETSETTINGID", employeetargetsettingid);
            proc.AddPara("@SETTINGTYPE", settingtype);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable EmployeesTargetRemove(Int32 employeetargetsettingid)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTarget_Remove");
            proc.AddPara("@EMPLOYEETARGETSETTINGID", employeetargetsettingid);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable EmployeesCounterWiseTargetRemove(String employeecode, Int32 employeetargetsettingid, String Type)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetCounterWiseTarget_Remove");
            proc.AddPara("@EMPLOYEECODE", employeecode);
            proc.AddPara("@EMPLOYEETARGETSETTINGID", employeetargetsettingid);
            proc.AddPara("@TYPE", Type);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable EmployeesCounterTargetInsertUpdate(String EmpPPType, Decimal OrderValue, Decimal CollectionValue, String employeecode, Int32 employeetargetsettingid, Int32 employeescountertargetid, Int32 FKEmployeesTargetSettingEmpTypeID, Int32 fkemployeescountertype, Int32 SettingMonth, Int32 SettingYear)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetSettingCounterTarget_InsertUpdate");
            proc.AddPara("@EMPLOYEESCOUNTERTARGETID", employeescountertargetid);
            proc.AddPara("@EMPLOYEECODE", employeecode);
            proc.AddPara("@FKEMPLOYEETARGETSETTINGID", employeetargetsettingid);
            proc.AddPara("@SHOP_CODE", EmpPPType);
            proc.AddPara("@ORDERVALUE", OrderValue);
            proc.AddPara("@COLLECTIONVALUE", CollectionValue);


            proc.AddPara("@FKEMPLOYEESTARGETSETTINGEMPTYPEID", FKEmployeesTargetSettingEmpTypeID);
            proc.AddPara("@FKEMPLOYEESCOUNTERTYPE", fkemployeescountertype);
            proc.AddPara("@SETTINGMONTH", SettingMonth);
            proc.AddPara("@SETTINGYEAR", SettingYear);

            ds = proc.GetTable();
            return ds;
        }

        public DataTable EmployeesTargetItemsList(Int32 Category)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesTargetItemsList");
            proc.AddPara("@PRODUCTCLASS_CODE", Category);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetEmployeeTargetDateRange(String EmployeeCode, Int32 TypeID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeeTargetDateRange_Get");
            proc.AddPara("@EMPLOYEECODE", EmployeeCode);
            proc.AddPara("@TYPEID", TypeID);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetEmployeesListByStateDesignation(String State, String Designation)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_EmployeesListByStateDesignation");
            proc.AddPara("@STATE", State);
            proc.AddPara("@DESIGNATION", Designation);
            ds = proc.GetTable();
            return ds;
        }


        public DataTable GetReVisitList(string Employee, string MONTH, string YEAR, string stateID, string DESIGNID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_REVISITSHOP");
            proc.AddPara("@Employee", Employee);
            proc.AddPara("@MONTH", MONTH);
            proc.AddPara("@YEAR", YEAR);
            proc.AddPara("@stateID", stateID);
            proc.AddPara("@DESIGNID", DESIGNID);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetReVisitEmport(DataTable userDT, string CREATE_BY)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_REVISITINMORT");
            proc.AddPara("@REVISIT", userDT);
            proc.AddPara("@CREATE_BY", CREATE_BY);
            ds = proc.GetTable();

            return ds;
        }

        public DataTable GetRevisitSettingImportLogMonthWise(string YEAR, string MONTH)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Proc_FTS_REVISITMONTHWISE");
            proc.AddPara("@YEAR", YEAR);
            proc.AddPara("@MONTH", MONTH);
            ds = proc.GetTable();

            return ds;
        }
    }
}
