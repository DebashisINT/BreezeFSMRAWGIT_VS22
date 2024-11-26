using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using DataAccessLayer;
//using MyShop.Models;

namespace LMS.Models
{
    public class LMSPointModel
    {
        // Route
        public Int32 Section { get; set; }
        public List<string> SectionIds { get; set; }
        public string SectionId { get; set; }
        public List<SectionList> SectionList { get; set; }
        //
        public string Is_PageLoad { get; set; }
        public int SavePoint(string Section, string user, string ID = "0", string Points = "", string ActiveStatus = "0")
        {
            ProcedureExecute proc;

            try
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMS_POINTSMASTER", sqlcon);
                if (ID == "0")
                    sqlcmd.Parameters.AddWithValue("@ACTION", "ADD");
                else
                    sqlcmd.Parameters.AddWithValue("@ACTION", "UPDATE");
                sqlcmd.Parameters.AddWithValue("@POINTSECTION", Section);
                sqlcmd.Parameters.AddWithValue("@USER_ID", user);
                sqlcmd.Parameters.AddWithValue("@POINTS", Points);
                sqlcmd.Parameters.AddWithValue("@POINTSETUPSTATUS", ActiveStatus);
                sqlcmd.Parameters.AddWithValue("@ID", ID);

                SqlParameter output = new SqlParameter("@ReturnValue", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                sqlcmd.Parameters.Add(output);

                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                Int32 ReturnValue = Convert.ToInt32(output.Value);

                sqlcmd.Dispose();
                sqlcmd.Dispose();
                return ReturnValue;

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

        public int DeletePoint(string PointsID)
        {
            int i;
            int rtrnvalue = 0;
            ProcedureExecute proc = new ProcedureExecute("PRC_LMS_POINTSMASTER");
            proc.AddNVarcharPara("@action", 50, "DELETE");
            proc.AddNVarcharPara("@ID", 30, PointsID);
            proc.AddVarcharPara("@ReturnValue", 200, "0", QueryParameterDirection.Output);
            i = proc.RunActionQuery();
            rtrnvalue = Convert.ToInt32(proc.GetParaValue("@ReturnValue"));
            return rtrnvalue;
        }

        public DataTable EditPoint(string ID)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_LMS_POINTSMASTER"))
                {
                    proc.AddVarcharPara("@ID", 100, ID);
                    proc.AddVarcharPara("@ACTION", 100, "EDIT");
                    dt = proc.GetTable();
                    return dt;
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

        
        public DataTable GetPointSection()
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_LMS_POINTSMASTER"))
                {
                    proc.AddVarcharPara("@ACTION", 100, "GetSection");
                    dt = proc.GetTable();
                    return dt;
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
    }

    public class SectionList
    {
        public Int64 SectionId { get; set; }
        public string SectionName { get; set; }
    }
}