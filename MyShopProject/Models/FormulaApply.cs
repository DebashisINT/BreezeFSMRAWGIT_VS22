using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace MyShop.Models
{

    public class P_formula_header
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string table { get; set; }


        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string short_nm { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string month { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string year { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string appl_for { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FormulaHeaderName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string tableFormulaCode { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int? TableBreakUpId { get; set; }

        public List<tbl_shoptype> Shop_Type { get; set; }
    }


    public class En_Rating
    {
        public void save(FormulaApply apply,string Action, ref string tblformulaid, ref int ReturnCode, ref string ReturnMsg)
        {

            string action = string.Empty;
            DataTable formula_dtls = new DataTable();
            DataSet dsInst = new DataSet();

            try
            {

                formula_dtls.Columns.Add("low", typeof(decimal));
                formula_dtls.Columns.Add("high", typeof(decimal));
                formula_dtls.Columns.Add("value", typeof(string));

                foreach (P_formula_dtls dtls in apply.dtls)
                {
                    DataRow dr = formula_dtls.NewRow();
                    dr["low"] = dtls.low;
                    dr["high"] = dtls.high;
                    dr["value"] = dtls.value;
                    formula_dtls.Rows.Add(dr);
                }

                if (HttpContext.Current.Session["userid"] != null)
                {
                    int user_id = Convert.ToInt32(HttpContext.Current.Session["userid"]);
                    ExecProcedure execProc = new ExecProcedure();
                    List<KeyObj> paramList = new List<KeyObj>();
                    execProc.ProcedureName = "PRC_ENTITYRATINGADDEDT";
                    if (Action != "ADD")
                    {
                        paramList.Add(new KeyObj("@Action", "EditFormula"));
                        paramList.Add(new KeyObj("@TableBreakUpId", apply.header.TableBreakUpId));
                    }
                    else
                    {
                        paramList.Add(new KeyObj("@Action", "AddFormula"));
                    }

                    paramList.Add(new KeyObj("@user_id", user_id));
                    paramList.Add(new KeyObj("@TableName", apply.header.table));
                    paramList.Add(new KeyObj("@TableFormulaCode", apply.header.tableFormulaCode));
                    paramList.Add(new KeyObj("@TableCode", apply.header.short_nm));
                    paramList.Add(new KeyObj("@month", apply.header.month));
                    paramList.Add(new KeyObj("@year", apply.header.year));
                    paramList.Add(new KeyObj("@appl_for", apply.header.appl_for));

                    paramList.Add(new KeyObj("@PARAMTABLE", formula_dtls));
                    paramList.Add(new KeyObj("@ReturnMessage", ReturnMsg, true));
                    paramList.Add(new KeyObj("@ReturnCode", ReturnCode, true));
                    paramList.Add(new KeyObj("@TableFormulaId", tblformulaid, true));
                    execProc.param = paramList;
                    execProc.ExecuteProcedureNonQuery();
                    paramList.Clear();
                    ReturnMsg = Convert.ToString(execProc.outputPara[0].value);
                    ReturnCode = Convert.ToInt32(execProc.outputPara[1].value);
                    tblformulaid = Convert.ToString(execProc.outputPara[2].value);
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string Delete(string ActionType, string code, ref int ReturnCode)
        {
            string output = string.Empty;
            string action = string.Empty;
            int NoOfRowEffected = 0;

            DataTable formula_dtls = new DataTable();

            try
            {
                if (ActionType == "Delete")
                {
                    action = "Delete";
                }

                if (HttpContext.Current.Session["userid"] != null)
                {
                    int user_id = Convert.ToInt32(HttpContext.Current.Session["userid"]);
                    ExecProcedure execProc = new ExecProcedure();
                    List<KeyObj> paramList = new List<KeyObj>();
                    execProc.ProcedureName = "PRC_ENTITYRATINGADDEDT";
                    paramList.Add(new KeyObj("@Action", action));
                    paramList.Add(new KeyObj("@user_id", user_id));//ADDEDITCATEGORYIMPORT
                    paramList.Add(new KeyObj("@TableCode", code));

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

        public FormulaApply getFormulaDetailsById(string _Code, string EditFlag, int TableBreakup_ID)
        {
            DataSet _getFormulaDtls = new DataSet();
            FormulaApply _apply = new FormulaApply();
            List<P_formula_dtls> items = new List<P_formula_dtls>();
            P_formula_header _header = new P_formula_header();
            try
            {
                ExecProcedure execProc = new ExecProcedure();
                List<KeyObj> paramList = new List<KeyObj>();
                execProc.ProcedureName = "PRC_ENTITYRATINGADDEDT";
                if (EditFlag == "I")
                {
                    paramList.Add(new KeyObj("@Action", "PopulateDetails"));
                    paramList.Add(new KeyObj("@TableCode", _Code));
                }

                else
                {
                    paramList.Add(new KeyObj("@Action", "PopulateDetails"));
                }

                paramList.Add(new KeyObj("@TableCode", _Code));

                execProc.param = paramList;
                _getFormulaDtls = execProc.ExecuteProcedureGetDataSet();

                if (_getFormulaDtls.Tables[0].Rows.Count > 0)
                {
                    _header.table = _getFormulaDtls.Tables[0].Rows[0]["Name"].ToString();
                    _header.short_nm = _getFormulaDtls.Tables[0].Rows[0]["Code"].ToString();
                    //_header.tableFormulaCode = _getFormulaDtls.Tables[0].Rows[0]["TableFormulaCode"].ToString();


                    if (EditFlag == "1")
                    {
                        _header.month = Convert.ToString(_getFormulaDtls.Tables[0].Rows[0]["month"]);
                        _header.year = Convert.ToString(_getFormulaDtls.Tables[0].Rows[0]["year"]);
                        _header.appl_for = Convert.ToString(_getFormulaDtls.Tables[0].Rows[0]["applicable_for"]);
                        

                        foreach (DataRow dr in _getFormulaDtls.Tables[1].Rows)
                        {

                            items.Add(new P_formula_dtls
                            {
                                low = dr["LowValue"].ToString(),
                                high = dr["HighValue"].ToString(),
                                value = dr["ResultValue"].ToString(),
                                ID = dr["ID"].ToString()
                                

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

    public class P_formula_dtls
    {

        public string low { get; set; }
        public string high { get; set; }
        public string value { get; set; }

        public string ID { get; set; }

    }

    public class TableFormulaHeadBreakup
    {
        public string TableFormulaCode { get; set; }
        public string TableName { get; set; }
        public string ShortName { get; set; }
        public DateTime ApplicatedFrom { get; set; }
        public DateTime ApplicatedTo { get; set; }

    }


    public class FormulaApply
    {

        public P_formula_header header { get; set; }
        public List<P_formula_dtls> dtls { get; set; }
        public List<TableFormulaHeadBreakup> outerdtls { get; set; }
        public string response_code { get; set; }
        public string response_msg { get; set; }

    }

    public class Msg
    {
        public string response_code { get; set; }
        public string response_msg { get; set; }
    }
}