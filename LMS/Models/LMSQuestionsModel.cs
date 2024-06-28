using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DevExpress.XtraEditors.Controls;

namespace LMS.Models
{
    public class LMSQuestionsModel
    {

        public DataTable GETLOOKUPVALUE(string Action)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataTable dt = new DataTable();
            try
            {
                using (proc = new ProcedureExecute("PRC_LMS_QUESTIONS"))              {
                    proc.AddVarcharPara("@ACTION", 100, Action);
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


        public int SaveQuestion(string name, string Userid, string ID="0", string description="", string Option1= "", string Option2 = "", string Option3 = "", string Option4 = "" , string Point1 = "0" , string Point2 = "0", string Point3 = "0", string Point4 = "0"
            , string chkCorrect1 = "", string chkCorrect2 = "", string chkCorrect3 = "", string chkCorrect4 = "", string TOPIC_ID = "", string Category_ID = "")
        {
            ProcedureExecute proc;

            try
            {
                DataTable dt = new DataTable();
                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();
                sqlcmd = new SqlCommand("PRC_LMS_QUESTIONS", sqlcon);
                if (ID == "0")
                    sqlcmd.Parameters.AddWithValue("@ACTION", "ADD");
                else
                    sqlcmd.Parameters.AddWithValue("@ACTION", "UPDATE");
                sqlcmd.Parameters.AddWithValue("@QUESTIONNAME", name);
                sqlcmd.Parameters.AddWithValue("@USER_ID", Userid);
                sqlcmd.Parameters.AddWithValue("@QUESTIONDESCRIPTION", description);

                sqlcmd.Parameters.AddWithValue("@OPTION1", Option1);
                sqlcmd.Parameters.AddWithValue("@OPTION2", Option2);
                sqlcmd.Parameters.AddWithValue("@OPTION3", Option3);
                sqlcmd.Parameters.AddWithValue("@OPTION4", Option4);

                sqlcmd.Parameters.AddWithValue("@POINT1", Point1);
                sqlcmd.Parameters.AddWithValue("@POINT2", Point2);
                sqlcmd.Parameters.AddWithValue("@POINT3", Point3);
                sqlcmd.Parameters.AddWithValue("@POINT4", Point4);

                sqlcmd.Parameters.AddWithValue("@CORRECT1", chkCorrect1);
                sqlcmd.Parameters.AddWithValue("@CORRECT2", chkCorrect2);
                sqlcmd.Parameters.AddWithValue("@CORRECT3", chkCorrect3);
                sqlcmd.Parameters.AddWithValue("@CORRECT4", chkCorrect4);

                sqlcmd.Parameters.AddWithValue("@TOPIC_IDS", TOPIC_ID);
                sqlcmd.Parameters.AddWithValue("@CATEGORY_IDS", Category_ID);

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


        public int DeleteQuestion(string CategoryId)
        {
            int i;
            int rtrnvalue = 0;
            ProcedureExecute proc = new ProcedureExecute("PRC_LMS_QUESTIONS");
            proc.AddNVarcharPara("@action", 50, "DELETE");
            proc.AddNVarcharPara("@ID", 30, CategoryId);
            proc.AddVarcharPara("@ReturnValue", 200, "0", QueryParameterDirection.Output);
            i = proc.RunActionQuery();
            rtrnvalue = Convert.ToInt32(proc.GetParaValue("@ReturnValue"));
            return rtrnvalue;
        }

        public DataSet EditQuestion(string ID)
        {
            ProcedureExecute proc;
            int rtrnvalue = 0;
            DataSet dt = new DataSet();
            try
            {
                using (proc = new ProcedureExecute("PRC_LMS_QUESTIONS"))
                {
                    proc.AddVarcharPara("@ID", 100, ID);
                    proc.AddVarcharPara("@ACTION", 100, "EDIT");
                    dt = proc.GetDataSet();
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
    public class GetTopic
    {
        public Int64 TOPICID { get; set; }
        public string TOPICNAME { get; set; }
    }

    public class GetCategory
    {
        public Int64 CATEGORYID { get; set; }
        public string CATEGORYNAME { get; set; }
    }
    

}