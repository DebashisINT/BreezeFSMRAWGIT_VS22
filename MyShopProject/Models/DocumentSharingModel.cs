using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyShop.Models
{


    public sealed class DocumentSharingModel
    {
        private DocumentSharingModel()
        {
        }
        private static DocumentSharingModel obj = null;
        public static DocumentSharingModel Obj
        {
            get
            {
                if (obj == null)
                {
                    obj = new DocumentSharingModel();
                }
                return obj;
            }
        }


        public int SaveDocumentSharing(string code, string name, string user, string ID = "0")
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            try
            {
                using (proc = new ProcedureExecute("PRC_DOCUMENTSHARING"))
                {
                    proc.AddVarcharPara("@ID", 100, ID);
                    if (ID == "0")
                        proc.AddVarcharPara("@ACTION", 100, "Add");
                    else
                        proc.AddVarcharPara("@ACTION", 100, "Update");
                    proc.AddVarcharPara("@DOC_CODE", 100, code);
                    proc.AddVarcharPara("@DOC_NAME", 100, name);
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

        public DataTable EditDocumentSharing(string ID)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_DOCUMENTSHARING"))
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
                using (proc = new ProcedureExecute("PRC_DOCUMENTSHARING"))
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


    public sealed class DocumentSharingUsers
    {
        private DocumentSharingUsers()
        {
        }
        private static DocumentSharingUsers obj = null;
        public static DocumentSharingUsers Obj
        {
            get
            {
                if (obj == null)
                {
                    obj = new DocumentSharingUsers();
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


        public int SaveDocumentSharingUser(string selected, string id)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            try
            {
                using (proc = new ProcedureExecute("PRC_DOCUMENTSHARING"))
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
                using (proc = new ProcedureExecute("PRC_DOCUMENTSHARING"))
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

    public class VideoFiles
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> FileSize { get; set; }
        public string FilePath { get; set; }
        public string Filetype { get; set; }
        public string FileDescription { get; set; }
        public string FilePathIcon { get; set; }
        public string IsActive { get; set; }

    }

}