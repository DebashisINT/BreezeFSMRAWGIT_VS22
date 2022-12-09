using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer;

namespace BusinessLogicLayer.EmailTemplate
{
    public class Emailtemplate
    {


        public static DataTable GetEmailTags(string Type)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("proc_Email_Template_Helper");
            proc.AddPara("@Typeid", Type);
            ds = proc.GetTable();
            return ds;
        }


     
    }
    public class Emailtags
    {
        public string EmailTags { get; set; }

        public int Id { get; set; }
        public int StageId { get; set; }
    }


    public class CommonResult
    {

        public object AddonData { get; set; }
    }

}
