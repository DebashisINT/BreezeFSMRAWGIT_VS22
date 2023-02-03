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

        // Mantis Issue 25536, 25535, 25542, 25543, 25544 [area and route added]
        public int SaveBeat(string code, string name, string user, int route, string ID = "0")
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            try
            {
                using (proc = new ProcedureExecute("PRC_GROUPBEAT"))
                {
                    proc.AddVarcharPara("@ID", 100, ID);
                    if (ID == "0")
                        proc.AddVarcharPara("@ACTION", 100, "ADD");
                    else
                        proc.AddVarcharPara("@ACTION", 100, "UPDATE");
                    proc.AddVarcharPara("@BEAT_CODE", 100, code);
                    proc.AddVarcharPara("@BEAT_NAME", 100, name);
                    proc.AddVarcharPara("@USER_ID", 100, user);
                    proc.AddIntegerPara("@ROUTE_CODE", route);
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

        // Rev Sanchiat [ type added]
        public DataTable EditBeat(string ID, string type)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_GROUPBEAT"))
                {
                    proc.AddVarcharPara("@ID", 100, ID);
                    // Mantis Issue 25536, 25535, 25542, 25543, 25544
                    proc.AddVarcharPara("@CODE_TYPE", 100, type);
                    // End of Mantis Issue 25536, 25535, 25542, 25543, 25544
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

        // Mantis Issue 25536, 25535, 25542, 25543, 25544 [ type added ]
        public int Delete(string ID, string type)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_GROUPBEAT"))
                {
                    proc.AddVarcharPara("@ID", 100, ID);
                    proc.AddVarcharPara("@ACTION", 100, "DELETE");
                    proc.AddVarcharPara("@CODE_TYPE", 100, type);
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

        // Mantis Issue 25536, 25535, 25542, 25543, 25544
        public int SaveArea(string code, string name, string user, string ID = "0")
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            try
            {
                using (proc = new ProcedureExecute("PRC_GROUPBEAT"))
                {
                    proc.AddVarcharPara("@ID", 100, ID);
                    if (ID == "0")
                        proc.AddVarcharPara("@ACTION", 100, "ADDAREA");
                    else
                        proc.AddVarcharPara("@ACTION", 100, "UPDATEAREA");
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
        public DataSet GetListDataDetails()
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("PRC_GROUPBEAT");
            proc.AddPara("@ACTION", "GETLISTDATA");
            ds = proc.GetDataSet();
            return ds;
        }

        public int SaveRoute(string code, string name, int area, string user, string ID = "0")
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            try
            {
                using (proc = new ProcedureExecute("PRC_GROUPBEAT"))
                {
                    proc.AddVarcharPara("@ID", 100, ID);
                    if (ID == "0")
                        proc.AddVarcharPara("@ACTION", 100, "ADDROUTE");
                    else
                        proc.AddVarcharPara("@ACTION", 100, "UPDATEROUTE");
                    proc.AddVarcharPara("@BEAT_CODE", 100, code);
                    proc.AddVarcharPara("@BEAT_NAME", 100, name);
                    proc.AddIntegerPara("@AREA_CODE", area);
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
        // End of Mantis Issue 25536, 25535, 25542, 25543, 25544

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
