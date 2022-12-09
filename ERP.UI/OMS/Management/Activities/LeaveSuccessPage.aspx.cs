using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.OMS.Management.Activities
{
    public partial class LeaveSuccessPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                String con = System.Configuration.ConfigurationSettings.AppSettings["DBConnectionDefault"];
                DataTable dt = new DataTable();
                SqlCommand sqlcmd = new SqlCommand();
                SqlConnection sqlcon = new SqlConnection(con);
                sqlcon.Open();

                sqlcmd = new SqlCommand("Proc_FTS_ApplyLeaveForApproval", sqlcon);
                sqlcmd.Parameters.Add("@Action", "GetLeaveDetailsInSuccessPage");
                sqlcmd.Parameters.Add("@user_id", Convert.ToString(Session["LEAVE_USER"]));
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
                sqlcon.Close();

                if (dt != null && dt.Rows.Count > 0)
                {
                    dvSuccessText.InnerText = Convert.ToString(dt.Rows[0][0]);
                }

            }
        }
    }
}