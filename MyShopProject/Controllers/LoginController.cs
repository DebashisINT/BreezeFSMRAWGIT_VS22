using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using UtilityLayer;
using System.Configuration;

namespace MyShop.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/


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
            //Mantise issue: 0025122
            //return Redirect("/OMS/Management/ToolsUtilities/frmchangepassword.aspx");
            return Redirect("/OMS/Management/ToolsUtilities/frmchangeuserspassword.aspx");
            //End of  Mantise issue: 0025122
        }
        public ActionResult SubmitForm(LoginModel omodel)
        {

            Encryption epasswrd = new Encryption();
            string Encryptpass = epasswrd.Encrypt(omodel.password.Trim());

            string Validuser;
            Validuser = oDBEngine.AuthenticateUser(omodel.username, Encryptpass).ToString();
            if (Validuser == "Y")
            {
                return RedirectToAction("FSMDashboard", "MYSHOP/DashboardMenu");
            }

            else
            {
                return View();
            }
        }
    }
}