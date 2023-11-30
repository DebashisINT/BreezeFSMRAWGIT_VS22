/***************************************************************************************
 * Written by Sanchita on 24/11/2023 for V2.0.43    A new design page is required as Contact (s) under CRM menu. 
 *                                                  Mantis: 27034 
 ****************************************************************************************/
using BusinessLogicLayer;
using BusinessLogicLayer.SalesmanTrack;
using DataAccessLayer;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Models;
using MyShop.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class CRMContactController : Controller
    {
        // GET: MYSHOP/CRMContact
        NotificationBL notificationbl = new NotificationBL();
        DBEngine odbengine = new DBEngine();

        public ActionResult Index()
        {
            CRMContactModel Dtls = new CRMContactModel();

            Dtls.Fromdate = DateTime.Now.ToString("dd-MM-yyyy");
            Dtls.Todate = DateTime.Now.ToString("dd-MM-yyyy");
            Dtls.Is_PageLoad = "Ispageload";
            
            EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/CRMContact/Index");
            ViewBag.CanAdd = rights.CanAdd;
            ViewBag.CanView = rights.CanView;
            ViewBag.CanExport = rights.CanExport;
            ViewBag.CanReassign = rights.CanReassign;
            ViewBag.CanAssign = rights.CanAssign;
            ViewBag.CanBulkUpdate = rights.CanBulkUpdate;

            return View(Dtls);
        }

        public ActionResult GetContactFrom()
        {
            try
            {
                List<GetEnquiryFrom> modelEnquiryFrom = new List<GetEnquiryFrom>();

                DataTable dtEnquiryFrom = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSContactListingShow");
                proc.AddPara("@ACTION_TYPE", "GetContactFrom");
                dtEnquiryFrom = proc.GetTable();

                modelEnquiryFrom = APIHelperMethods.ToModelList<GetEnquiryFrom>(dtEnquiryFrom);

                return PartialView("~/Areas/MYSHOP/Views/CRMEnquiries/_EnquiryFromPartial.cshtml", modelEnquiryFrom);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult PartialCRMContactGridList(CRMContactModel model)
        {
            try
            {
                EntityLayer.CommonELS.UserRightsForPage rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/CRMContact/Index");
                ViewBag.CanAdd = rights.CanAdd;
                ViewBag.CanView = rights.CanView;
                ViewBag.CanExport = rights.CanExport;
                ViewBag.CanReassign = rights.CanReassign;
                ViewBag.CanAssign = rights.CanAssign;
                ViewBag.CanBulkUpdate = rights.CanBulkUpdate;
               
                string ContactFrom = "";
                int i = 1;

                if (model.ContactFromDesc != null && model.ContactFromDesc.Count > 0)
                {
                    foreach (string item in model.ContactFromDesc)
                    {
                        if (i > 1)
                            ContactFrom = ContactFrom + "," + item;
                        else
                            ContactFrom = item;
                        i++;
                    }

                }


                string Is_PageLoad = string.Empty;

                if (model.Is_PageLoad == "Ispageload")
                {
                    Is_PageLoad = "is_pageload";

                }

                string datfrmat = model.Fromdate.Split('-')[2] + '-' + model.Fromdate.Split('-')[1] + '-' + model.Fromdate.Split('-')[0];
                string dattoat = model.Todate.Split('-')[2] + '-' + model.Todate.Split('-')[1] + '-' + model.Todate.Split('-')[0];


                GetCRMContactListing(ContactFrom, datfrmat, dattoat, Is_PageLoad);

                model.Is_PageLoad = "Ispageload";

                return PartialView("PartialCRMContactGridList", GetCRMContactDetails(Is_PageLoad));

            }
            catch (Exception ex)
            {
                throw ex;
              
            }

        }

        public void GetCRMContactListing(string ContactFrom, string FromDate, string ToDate, string Is_PageLoad)
        {
            string user_id = Convert.ToString(Session["userid"]);

            string action = string.Empty;
            DataTable formula_dtls = new DataTable();
            DataSet dsInst = new DataSet();

            try
            {
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_FTSContactListingShow");
                proc.AddPara("@ACTION", "SHOWDATA");
                proc.AddPara("@IS_PAGELOAD", Is_PageLoad);
                proc.AddPara("@USERID", Convert.ToInt32(user_id));
                proc.AddPara("@CONTACTSFROM", ContactFrom);
                proc.AddPara("@FROMDATE", FromDate);
                proc.AddPara("@TODATE", ToDate);
                dt = proc.GetTable();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable GetCRMContactDetails(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            
            ////////DataTable dtColmn = GetPageRetention(Session["userid"].ToString(), "CRM Contact");
            ////////if (dtColmn != null && dtColmn.Rows.Count > 0)
            ////////{
            ////////    ViewBag.RetentionColumn = dtColmn;//.Rows[0]["ColumnName"].ToString()  DataTable na class pathao ok wait
            ////////}
            
            if (Is_PageLoad != "is_pageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.CRMCONTACT_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid)
                        orderby d.SEQ
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.CRMCONTACT_LISTINGs
                        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 11111111119
                        orderby d.SEQ
                        select d;
                return q;
            }

            //if (Is_PageLoad != "Ispageload")
            //{
            //    CRM.Models.DataContext.CRMClassDataContext DC = new CRM.Models.DataContext.CRMClassDataContext(connectionString);
            //    var q = from d in DC.ENQUIRIES_LISTING1s
            //            where d.USERID == Convert.ToInt32(Userid)
            //            orderby d.SEQ
            //            select d;
            //    return q;

            //}
            //else
            //{
            //    //CRM.Models.DataContext.CRMClassDataContext DC = new CRM.Models.DataContext.CRMClassDataContext(connectionString);
            //    //var q = from d in DC.ENQUIRIES_LISTING1s
            //    //        where d.USERID == Convert.ToInt32(Userid) && d.SEQ == 11111111119
            //    //        orderby d.SEQ descending
            //    //        select d;
            //    //return q;
            //    return null;
            //}

        }

    }
}