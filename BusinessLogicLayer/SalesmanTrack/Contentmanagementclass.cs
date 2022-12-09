using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.SalesmanTrack
{
   public  class Contentmanagementclass
    {
       public DataTable GetTemplates()
       {
           DataTable dt = new DataTable();
           ProcedureExecute proc = new ProcedureExecute("GetTemplates");
           dt = proc.GetTable();
           return dt;
       }

       public DataTable TemplateAdd(string templateID,string Template)
       {
           DataTable dt = new DataTable();
           ProcedureExecute proc = new ProcedureExecute("Proc_FTS_ContentManage");
           proc.AddPara("@TemplateID", templateID);
           proc.AddPara("@TemplateContent", Template);
           proc.AddPara("@Action", "Manage");
        
           dt = proc.GetTable();
           return dt;
       }


   

    }
}
