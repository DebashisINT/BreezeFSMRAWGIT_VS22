using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{

    public sealed class GroupBeat
    {
        private GroupBeat()
        {
        }
        private static GroupBeat obj = null;
        public static GroupBeat Obj
        {
            get
            {
                if (obj == null)
                {
                    obj = new GroupBeat();
                }
                return obj;
            }
        }


        public int SaveBeat(string code, string name, string user, string ID = "0")
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            try
            {
                using (proc = new ProcedureExecute("PRC_GROUPBEAT"))
                {
                    proc.AddVarcharPara("@ID", 100, ID);
                    if (ID == "0")
                        proc.AddVarcharPara("@ACTION", 100, "Add");
                    else
                        proc.AddVarcharPara("@ACTION", 100, "Update");
                    proc.AddVarcharPara("@BEAT_CODE", 100, code);
                    proc.AddVarcharPara("@BEAT_NAME", 100, name);
                    proc.AddVarcharPara("@USER_ID", 100, user);
                    proc.AddVarcharPara("@RETURN_VALUE", 50, "", QueryParameterDirection.Output);
                    int i = proc.RunActionQuery();
                    rtrnvalue = Convert.ToInt32(proc.GetParaValue("@RETURN_VALUE"));
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

        public DataTable EditBeat(string ID)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_GROUPBEAT"))
                {
                    proc.AddVarcharPara("@ID", 100, ID);
                    proc.AddVarcharPara("@ACTION", 100, "Edit");
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

        public int Delete(string ID)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_GROUPBEAT"))
                {
                    proc.AddVarcharPara("@ID", 100, ID);
                    proc.AddVarcharPara("@ACTION", 100, "Delete");
                    int i = proc.RunActionQuery();
                    rtrnvalue = Convert.ToInt32(proc.GetParaValue("@RETURN_VALUE"));
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


    public sealed class GroupBeatUsers
    {
        private GroupBeatUsers()
        {
        }
        private static GroupBeatUsers obj = null;
        public static GroupBeatUsers Obj
        {
            get
            {
                if (obj == null)
                {
                    obj = new GroupBeatUsers();
                }
                return obj;
            }
        }

        private static List<String> objList = null;
        public static List<String> ObjList
        {
            get
            {
                if (objList == null)
                {
                    objList = new List<String>();
                }
                return objList;
            }
        }


        public int SaveBeatUser(string selected, string id)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            try
            {
                using (proc = new ProcedureExecute("PRC_GROUPBEAT"))
                {
                    proc.AddVarcharPara("@ID", 100, id);
                    proc.AddVarcharPara("@ACTION", 100, "UpdateUserMap");
                    proc.AddVarcharPara("@selected", -1, selected);
                    proc.AddVarcharPara("@RETURN_VALUE", 50, "", QueryParameterDirection.Output);
                    int i = proc.RunActionQuery();
                    rtrnvalue = Convert.ToInt32(proc.GetParaValue("@RETURN_VALUE"));
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
        public DataTable GetUserMap(string ID)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_GROUPBEAT"))
                {
                    proc.AddVarcharPara("@ID", 100, ID);
                    proc.AddVarcharPara("@ACTION", 100, "GetUserMap");
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

}
