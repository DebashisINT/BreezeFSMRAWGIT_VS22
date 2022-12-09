using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogicLayer
{
    public class TdsTcsBL
    {
        public DataTable PopulateMainAccountDropDownForTDSTCS()
        {
            ProcedureExecute proc;

            try
            {
                using (proc = new ProcedureExecute("prc_TDSTCS"))
                {
                    proc.AddVarcharPara("@action", 50, "PopulateMainAccountDropDownForTDSTCS");
                    return proc.GetTable();
                }
            }

            catch (Exception ex)
            {
                return null;
            }

            finally
            {
                proc = null;
            }


             
        }



        public DataTable PopulateTDSTCSInEditMode(int TDSTCSID)
        {
            ProcedureExecute proc;

            try
            {
                using (proc = new ProcedureExecute("prc_TDSTCS"))
                {
                    proc.AddVarcharPara("@action", 50, "PopulateTDSTCSInEditMode");
                    proc.AddIntegerPara("@TDSTCS_ID", TDSTCSID); 
                    return proc.GetTable();
                }
            }

            catch (Exception ex)
            {
                return null;
            }

            finally
            {
                proc = null;
            }


             
        }



        public DataTable PopulateSubAccountDropDownForTDSTCS(string MainAccountID, string branch)
        {
            ProcedureExecute proc;

            try
            {
                using (proc = new ProcedureExecute("dbo.prc_TDSTCS"))
                {
                    proc.AddVarcharPara("@action", 100, "PopulateSubAccountDropDownForTDSTCS");
                    proc.AddVarcharPara("@MainAccountID", 100, MainAccountID);
                    proc.AddVarcharPara("@branch", 500, branch); 
                    return proc.GetTable();
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
