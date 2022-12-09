using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class OpeningBalanceBl
    {
        public void UpdateOpeningBalance(DataTable finalDt)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["DBConnectionDefault"]);
            SqlCommand cmd = new SqlCommand("prc_updateOpeningBalance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@opTable", finalDt);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public DataTable GetOpeningBalanceDetails(string BranchHierarchy)
        {

            ProcedureExecute proc;
            DataTable rtrnvalue;
            try
            {
                using (proc = new ProcedureExecute("prc_GetOpeningbalanceDetails"))
                {
                    proc.AddVarcharPara("@BranchList", 2000, BranchHierarchy);
                    rtrnvalue = proc.GetTable();
                    return rtrnvalue;
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
}
