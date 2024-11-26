using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;
using TargetVsAchievement.Models;

namespace TargetVsAchievement.Controllers
{
    public class LoginController : Controller
    {
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
            // Rev 1.0
            //return Redirect("/OMS/Management/ToolsUtilities/frmchangeuserspassword.aspx");
            return Redirect("/OMS/Management/ToolsUtilities/frmchangepassword.aspx");
            // End of Rev 1.0
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
                return RedirectToAction("Index", "TargetVsAchievement/TargetSetUp");
            }

            else
            {
                return View();
            }
        }
    }
}