using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using FSMMaster.Models;
using BusinessLogicLayer;
using UtilityLayer;

namespace FSMMaster.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        LoginModel model = new LoginModel();
        DBEngine oDBEngine = new DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return Redirect("/OMS/Signoff.aspx");
        }

        public ActionResult ChangePassword()
        {
            return Redirect("/OMS/Management/ToolsUtilities/frmchangeuserspassword.aspx");
        }
        public ActionResult SubmitForm(LoginModel omodel)
        {

            Encryption epasswrd = new Encryption();
            string Encryptpass = epasswrd.Encrypt(omodel.password.Trim());

            string Validuser;
            Validuser = oDBEngine.AuthenticateUser(omodel.username, Encryptpass).ToString();
            if (Validuser == "Y")
            {
                return RedirectToAction("FSMDashboard", "MASTER/DashboardMenu",new { area = "" });
            }

            else
            {
                return View();
            }
        }

    }
}