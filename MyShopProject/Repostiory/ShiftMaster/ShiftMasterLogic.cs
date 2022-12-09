using DataAccessLayer;
using DataAccessLayer.Model;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Repostiory.ShiftMaster
{
    public class ShiftMasterLogic : IShiftMasterLogic
    {
        public void ShiftMasterSubmit(ShiftMasterEngine model, ref int strIsComplete, ref string strMessage)
        {
            try
            {
                DataSet dsInst = new DataSet();
                SqlConnection con = new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]));
                SqlCommand cmd = new SqlCommand("proll_ShiftMaster_AddModify", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (Convert.ToString(model.Shift_Id) == "")
                {
                    cmd.Parameters.AddWithValue("@Action", "Add");
                    cmd.Parameters.AddWithValue("@ShiftId", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Action", "Edit");
                    cmd.Parameters.AddWithValue("@ShiftId", model.Shift_Id);
                }
                cmd.Parameters.AddWithValue("@ShiftName", model.Shift_Name);

                cmd.Parameters.AddWithValue("@ShiftStartTime", model.Shift_Start);
                cmd.Parameters.AddWithValue("@ShiftEndTime", model.Shift_End);
                cmd.Parameters.AddWithValue("@ShiftBreak", model.Break);
                cmd.Parameters.AddWithValue("@ShiftDays", model.Shift_EndDay);
                cmd.Parameters.AddWithValue("@Grace", model.Grace);

                cmd.Parameters.Add("@ReturnValue", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@ReturnMessage", SqlDbType.VarChar, 500);

                cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
                cmd.Parameters["@ReturnMessage"].Direction = ParameterDirection.Output;

                cmd.CommandTimeout = 0;
                SqlDataAdapter Adap = new SqlDataAdapter();
                Adap.SelectCommand = cmd;
                Adap.Fill(dsInst);

                strIsComplete = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
                strMessage = Convert.ToString(cmd.Parameters["@ReturnMessage"].Value.ToString());

                cmd.Dispose();
                con.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public ShiftMasterEngine GetShiftById(string ShiftId, ref int strIsComplete, ref string strMessage)
        {
            DataTable _getShiftDetails = new DataTable();
            ShiftMasterEngine objModel = new ShiftMasterEngine();
            try
            {

                ExecProcedure execProc = new ExecProcedure();
                List<KeyObj> paramList = new List<KeyObj>();
                execProc.ProcedureName = "proll_ShiftMaster_AddModify";
                paramList.Add(new KeyObj("@Action", "GetShiftById"));
                paramList.Add(new KeyObj("@ShiftId", ShiftId));
                paramList.Add(new KeyObj("@ReturnMessage", strMessage, true));
                paramList.Add(new KeyObj("@ReturnValue", strIsComplete, true));
                execProc.param = paramList;
                _getShiftDetails = execProc.ExecuteProcedureGetTable();
                strMessage = Convert.ToString(execProc.outputPara[0].value);
                strIsComplete = Convert.ToInt32(execProc.outputPara[1].value);


                paramList.Clear();

                if (_getShiftDetails.Rows.Count > 0 && _getShiftDetails != null)
                {
                    objModel.Shift_Id = _getShiftDetails.Rows[0]["ShiftID"].ToString();
                    objModel.Shift_Name = _getShiftDetails.Rows[0]["ShiftName"].ToString();
                    objModel.Shift_Start = _getShiftDetails.Rows[0]["ShiftStartTime"].ToString();
                    objModel.Shift_End = _getShiftDetails.Rows[0]["ShiftEndTime"].ToString();
                    objModel.Break = _getShiftDetails.Rows[0]["ShiftBreak"].ToString();
                    objModel.Shift_EndDay = _getShiftDetails.Rows[0]["ShiftDay"].ToString();
                    //objModel.Shift_Time = _getShiftDetails.Rows[0]["Shift_Time"].ToString();
                    objModel.Shift_Break_Time = _getShiftDetails.Rows[0]["ShiftBreak"].ToString();
                    objModel.Grace = _getShiftDetails.Rows[0]["Grace"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return objModel;
        }

        public string Delete(string ActionType, string id, ref int strIsComplete)
        {
            string output = string.Empty;
            string action = string.Empty;
            int NoOfRowEffected = 0;
            try
            {
                if (HttpContext.Current.Session["userid"] != null)
                {
                    int user_id = Convert.ToInt32(HttpContext.Current.Session["userid"]);
                    ExecProcedure execProc = new ExecProcedure();
                    List<KeyObj> paramList = new List<KeyObj>();
                    execProc.ProcedureName = "PRC_ShiftMasterInsertUpdate";
                    paramList.Add(new KeyObj("@Action", ActionType));
                    //paramList.Add(new KeyObj("@user_id", user_id));//ADDEDITCATEGORYIMPORT
                    paramList.Add(new KeyObj("@ShiftId", id));

                    paramList.Add(new KeyObj("@ReturnMessage", output, true));
                    paramList.Add(new KeyObj("@ReturnCode", strIsComplete, true));
                    execProc.param = paramList;
                    execProc.ExecuteProcedureNonQuery();
                    paramList.Clear();
                    NoOfRowEffected = execProc.NoOfRows;
                    output = Convert.ToString(execProc.outputPara[0].value);
                    strIsComplete = Convert.ToInt32(execProc.outputPara[1].value);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return output;
        }

    }
}