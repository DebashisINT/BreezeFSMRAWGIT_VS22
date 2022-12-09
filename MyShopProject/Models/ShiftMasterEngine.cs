using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Models
{
    public class ShiftMasterEngine
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FormulaHeaderName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Shift_Id { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Shift_Name { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Shift_Start { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Shift_End { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Break { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ResponseCode { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ResponseMessage { get; set; }

        public String Shift_EndDay { get; set; }

        public String Shift_Time { get; set; }

        public String Shift_Break_Time { get; set; }

        public String Grace { get; set; }
    }

    public class ShiftApply
    {
        public ShiftMasterEngine header { get; set; }
        public List<Shift_dtls> dtls { get; set; }
        //public List<TableFormulaHeadBreakup> outerdtls { get; set; }
        public string response_code { get; set; }
        public string response_msg { get; set; }
    }

    public class Shift_dtls
    {
        public string ID { get; set; }
        public string ShiftDay { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string BreakTime { get; set; }
        public string grace { get; set; }
        public string AltInTime { get; set; }
        public string AltOutTime { get; set; }
        public string AltBreakTime { get; set; }
        public string Altgrace { get; set; }
        //Mantis Issue 25112
        public string FullDayWorkingHour { get; set; }
        public string HalfDayWorkingHour { get; set; }
        public string AbsentWorkingHour { get; set; }
        //End of Mantis Issue 25112
    }

    public class Shift_Rating
    {
        public void save(ShiftApply apply, string Action, ref string tblformulaid, ref int ReturnCode, ref string ReturnMsg)
        {

            string action = string.Empty;
            DataTable formula_dtls = new DataTable();
            DataSet dsInst = new DataSet();

            try
            {
                formula_dtls.Columns.Add("ID", typeof(int));
                formula_dtls.Columns.Add("ShiftDay", typeof(string));
                formula_dtls.Columns.Add("InTime", typeof(string));
                formula_dtls.Columns.Add("OutTime", typeof(string));
                formula_dtls.Columns.Add("BreakTime", typeof(string));
                formula_dtls.Columns.Add("grace", typeof(int));
                formula_dtls.Columns.Add("AltInTime", typeof(string));
                formula_dtls.Columns.Add("AltOutTime", typeof(string));
                formula_dtls.Columns.Add("AltBreakTime", typeof(string));
                formula_dtls.Columns.Add("Altgrace", typeof(int));
                //Mantis Issue 25112
                formula_dtls.Columns.Add("FullDayWorkingHour", typeof(string));
                formula_dtls.Columns.Add("HalfDayWorkingHour", typeof(string));
                formula_dtls.Columns.Add("AbsentWorkingHour", typeof(string));
                //End of Mantis Issue 25112
                int i = 1;

                foreach (Shift_dtls dtls in apply.dtls)
                {
                    DataRow dr = formula_dtls.NewRow();
                    dr["ID"] = i;
                    dr["ShiftDay"] = dtls.ShiftDay;
                    dr["InTime"] = dtls.InTime;
                    dr["OutTime"] = dtls.OutTime;
                    dr["BreakTime"] = dtls.BreakTime;
                    dr["grace"] = dtls.grace;
                    dr["AltInTime"] = dtls.AltInTime;
                    dr["AltOutTime"] = dtls.AltOutTime;
                    dr["AltBreakTime"] = dtls.AltBreakTime;
                    dr["Altgrace"] = dtls.Altgrace;
                    //Mantis Issue 25112
                    dr["FullDayWorkingHour"] = dtls.FullDayWorkingHour;
                    dr["HalfDayWorkingHour"] = dtls.HalfDayWorkingHour;
                    dr["AbsentWorkingHour"] = dtls.AbsentWorkingHour;
                    //End of Mantis Issue 25112
                    formula_dtls.Rows.Add(dr);
                    i++;
                }

                if (HttpContext.Current.Session["userid"] != null)
                {
                    int user_id = Convert.ToInt32(HttpContext.Current.Session["userid"]);
                    ExecProcedure execProc = new ExecProcedure();
                    List<KeyObj> paramList = new List<KeyObj>();
                    execProc.ProcedureName = "PRC_ShiftMasterInsertUpdate";
                    if (Action != "ADD")
                    {
                        paramList.Add(new KeyObj("@Action", "Edit"));
                        paramList.Add(new KeyObj("@ShiftId", apply.header.Shift_Id));
                    }
                    else
                    {
                        paramList.Add(new KeyObj("@Action", "Add"));
                    }

                    paramList.Add(new KeyObj("@UserID", user_id));
                    paramList.Add(new KeyObj("@ShiftName", apply.header.Shift_Name));

                    paramList.Add(new KeyObj("@PARAMTABLE", formula_dtls));
                    paramList.Add(new KeyObj("@ReturnMessage", ReturnMsg, true));
                    paramList.Add(new KeyObj("@ReturnCode", ReturnCode, true));
                    //paramList.Add(new KeyObj("@TableFormulaId", tblformulaid, true));
                    execProc.param = paramList;
                    execProc.ExecuteProcedureNonQuery();
                    paramList.Clear();
                    ReturnMsg = Convert.ToString(execProc.outputPara[0].value);
                    ReturnCode = Convert.ToInt32(execProc.outputPara[1].value);
                    //tblformulaid = Convert.ToString(execProc.outputPara[2].value);
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string Delete(string ActionType, string id, ref int ReturnCode)
        {
            string output = string.Empty;
            string action = string.Empty;
            int NoOfRowEffected = 0;

            DataTable formula_dtls = new DataTable();

            try
            {
                if (ActionType == "Delete")
                {
                    action = "DeleteFormula";
                }

                if (HttpContext.Current.Session["userid"] != null)
                {
                    int user_id = Convert.ToInt32(HttpContext.Current.Session["userid"]);
                    ExecProcedure execProc = new ExecProcedure();
                    List<KeyObj> paramList = new List<KeyObj>();
                    execProc.ProcedureName = "PRC_ENTITYRATINGADDEDT";
                    paramList.Add(new KeyObj("@Action", action));
                    paramList.Add(new KeyObj("@user_id", user_id));//ADDEDITCATEGORYIMPORT
                    paramList.Add(new KeyObj("@TableFormulaCode", id));

                    paramList.Add(new KeyObj("@ReturnMessage", output, true));
                    paramList.Add(new KeyObj("@ReturnCode", ReturnCode, true));
                    execProc.param = paramList;
                    execProc.ExecuteProcedureNonQuery();
                    paramList.Clear();
                    NoOfRowEffected = execProc.NoOfRows;
                    output = Convert.ToString(execProc.outputPara[0].value);
                    ReturnCode = Convert.ToInt32(execProc.outputPara[1].value);
                }


            }
            catch (Exception ex)
            {
                throw;
            }



            return output;

        }

        public ShiftApply getFormulaDetailsById(string _Code, string EditFlag, int TableBreakup_ID)
        {
            DataSet _getDtls = new DataSet();
            ShiftApply _apply = new ShiftApply();
            List<Shift_dtls> items = new List<Shift_dtls>();
            ShiftMasterEngine _header = new ShiftMasterEngine();
            try
            {
                int returnCode = 0; String _ReturnMessage = "";

                ExecProcedure execProc = new ExecProcedure();
                List<KeyObj> paramList = new List<KeyObj>();
                execProc.ProcedureName = "PRC_ShiftMasterInsertUpdate";
                paramList.Add(new KeyObj("@Action", "GetShiftById"));
                paramList.Add(new KeyObj("@ShiftId", _Code));
                paramList.Add(new KeyObj("@ReturnMessage", _ReturnMessage, true));
                paramList.Add(new KeyObj("@ReturnCode", returnCode, true));
                execProc.param = paramList;
                _getDtls = execProc.ExecuteProcedureGetDataSet();
                _ReturnMessage = Convert.ToString(execProc.outputPara[0].value);
                returnCode = Convert.ToInt32(execProc.outputPara[1].value);

                if (_getDtls.Tables[0].Rows.Count > 0)
                {
                    _header.Shift_Name = _getDtls.Tables[0].Rows[0]["Name"].ToString();
                    _header.Shift_Id = _getDtls.Tables[0].Rows[0]["ID"].ToString();

                    if (EditFlag == "1")
                    {
                        foreach (DataRow dr in _getDtls.Tables[1].Rows)
                        {
                            items.Add(new Shift_dtls
                            {
                                InTime = dr["BeginTime"].ToString(),
                                OutTime = dr["EndTime"].ToString(),
                                BreakTime = dr["BreakTime"].ToString(),
                                grace = dr["Grace"].ToString(),
                                AltInTime = dr["ALT_BeginTime"].ToString(),
                                AltOutTime = dr["ALT_EndTime"].ToString(),
                                AltBreakTime = dr["ALT_BreakTime"].ToString(),
                                Altgrace = dr["ALT_Grace"].ToString(),
                                ShiftDay = dr["ShiftDay"].ToString(),
                                //Mantis Issue 25112
                                FullDayWorkingHour = dr["FullDayWorkingHour"].ToString(),
                                HalfDayWorkingHour = dr["HalfDayWorkingHour"].ToString(),
                                AbsentWorkingHour = dr["AbsentWorkingHour"].ToString(),
                                //End of Mantis Issue 25112
                                ID = dr["DayWeek"].ToString()
                            });
                        }
                        _apply.dtls = items;
                    }
                    _apply.header = _header;
                }
            }
            catch (Exception ex)
            {
            }
            return _apply;
        }

    }
}