using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class LMSReportTopicListModel
    {
        public string is_pageload { get; set; }

        public void CreateTable(string userid, string ISPAGELOAD)
        {
            ProcedureExecute proc = new ProcedureExecute("PRC_LMS_REPORTS_TOPICLIST");
            //proc.AddPara("@ACTION", "GETLISTINGDETAILS");           
            proc.AddPara("@ISPAGELOAD", ISPAGELOAD); 
            proc.AddPara("@USER_ID", userid);        
            proc.GetScalar();

        }
    }
}