/****************************************************************************************************************************
*   1.0     v2.0.36     Sanchita    10/01/2023      Appconfig and User wise setting "IsAllDataInPortalwithHeirarchy = True" then 
*                                                   data in portal shall be populated based on Hierarchy Only. Refer: 25504
*********************************************************************************************************************************/

using System;
using System.Data;
using System.Web;
using DevExpress.Web;
using BusinessLogicLayer;
using EntityLayer.CommonELS;


using System.Collections.Generic;
using System.Linq;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DevExpress.XtraPrinting;
using System.Net.Mime;
using EO.Web.Internal;
using DataAccessLayer;

namespace ERP.OMS.Management.Master
{
    public partial class management_master_frmContactMain : ERP.OMS.ViewState_class.VSPage //System.Web.UI.Page
    {
        int NoOfRow = 0;
        string AllUserCntId;
        string cBranchId = "";
        public string pageAccess = "";
        static string Checking = null;
        public static string IsLighterCustomePage = string.Empty;
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
        public static EntityLayer.CommonELS.UserRightsForPage rights;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'
                string sPath = Convert.ToString(HttpContext.Current.Request.Url);
                oDBEngine.Call_CheckPageaccessebility(sPath);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (HttpContext.Current.Session["userid"] == null)
            {

            }

            if (Session["ExchangeSegmentID"] == null)
            {
                TxtSeg.Value = "N";
            }
            else
            {
                TxtSeg.Value = "Y";
            }
            //Add 
            // Rev 1.0
            //DataTable DMSFeatureOn = oDBEngine.GetDataTable("tbl_master_user", "IsDMSFeatureOn", " user_id in(" + HttpContext.Current.Session["userid"] + ")");
            //if (DMSFeatureOn != null && DMSFeatureOn.Rows.Count > 0)
            //{
            //    hdnIsDMSFeatureOn.Value = Convert.ToString(DMSFeatureOn.Rows[0]["IsDMSFeatureOn"]);
            //}

            DataTable DMSFeatureOn = oDBEngine.GetDataTable("tbl_master_user", "IsAllDataInPortalwithHeirarchy", " user_id in(" + HttpContext.Current.Session["userid"] + ")");
            if (DMSFeatureOn != null && DMSFeatureOn.Rows.Count > 0)
            {
                hdnIsDMSFeatureOn.Value = Convert.ToString(DMSFeatureOn.Rows[0]["IsAllDataInPortalwithHeirarchy"]);
            }
            // End of Rev 1.0

            if (!Page.IsPostBack)
            {
                rights = new UserRightsForPage();
                rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/master/frmContactMain.aspx?requesttype=" + Convert.ToString(Request.QueryString["requesttype"]) + "");

                hidIsLigherContactPage.Value = "0";
                IsLighterCustomePage = "";

                Session["exportval"] = null;

                string requesttype = Convert.ToString(Request.QueryString["requesttype"]);
                Session["Contactrequesttype"] = requesttype;

                string ContType = "";
                switch (requesttype)
                {
                    case "customer":
                        ContType = "Customer/Client";
                        break;
                    case "OtherEntity":
                        ContType = "OtherEntity";
                        break;

                    case "subbroker":
                        ContType = "Sub Broker";
                        break;
                    case "franchisee":
                        ContType = "Franchisee";
                        break;
                    case "referalagent":
                        ContType = "Relationship Partners";
                        break;
                    case "broker":
                        ContType = "Broker";
                        break;
                    case "agent":

                        ContType = "Salesman/Agents";
                        break;
                    case "datavendor":
                        ContType = "Data Vendor";
                        break;
                    case "vendor":
                        ContType = "Vendor";
                        break;
                    case "partner":
                        ContType = "Partner";
                        break;
                    case "consultant":
                        ContType = "Consultant";
                        break;
                    case "shareholder":
                        ContType = "Share Holder";
                        break;
                    case "creditor":
                        ContType = "Creditors";
                        break;
                    case "debtor":
                        ContType = "Debtor";
                        break;
                    case "leadmanagers":
                        ContType = "Lead managers";
                        break;
                    case "bookrunners":
                        ContType = "Book Runners";
                        break;
                    case "listedcompanies":
                        ContType = "Companies-Listed";
                        break;
                    case "recruitmentagent":
                        ContType = "Relationship Manager";
                        break;
                    case "businesspartner":
                        ContType = "Business Partner";
                        break;
                    case "Transporter":
                        ContType = "Transporter";
                        break;
                    //For Leads
                    case "Lead":
                        ContType = "Lead";
                        break;
                }



                if (ContType == "Broker")
                {
                    td_broker1.Visible = true;
                    td_contact1.Visible = false;
                }
                else
                {
                    td_broker1.Visible = false;
                    td_contact1.Visible = true;
                }



                if (ContType == "Customer/Client")
                {
                    lblHeadTitle.Text = "Customers / Clients";
                    this.Title = "Customers / Clients";

                    CommonBL cbl = new CommonBL();
                    string ISLigherpage = cbl.GetSystemSettingsResult("LighterCustomerEntryPage");
                    if (!String.IsNullOrEmpty(ISLigherpage))
                    {
                        if (ISLigherpage == "Yes")
                        {
                            hidIsLigherContactPage.Value = "1";
                            IsLighterCustomePage = "1";
                        }
                    }
                }
                else if (ContType == "Franchisee")
                {
                    lblHeadTitle.Text = "Franchisees";
                    this.Title = "Franchisees";
                }

                else if (ContType == "Salesman/Agents")
                {

                    lblHeadTitle.Text = "Salesman/Agents";
                    this.Title = "Salesman/Agents";
                }
                else if (ContType == "Creditors")
                {
                    lblHeadTitle.Text = "Creditors";
                    this.Title = "Creditors";
                }
                else if (ContType == "Business Partner")
                {
                    lblHeadTitle.Text = "Business Partners";
                    this.Title = "Business Partners";
                }
                else if (ContType == "Book Runners")
                {
                    lblHeadTitle.Text = "Book Runners";
                    this.Title = "Book Runners";
                }
                else if (ContType == "Relationship Partners")
                {
                    lblHeadTitle.Text = "Relationship Partners";
                    this.Title = "Relationship Partners";
                }
                else if (ContType == "Partner")
                {
                    lblHeadTitle.Text = "Business Partners";
                    this.Title = "Business Partners";
                }
                else if (ContType == "OtherEntity")
                {
                    lblHeadTitle.Text = "Other Entity";
                    this.Title = "Other Entity";
                }
                else if (ContType == "Transporter")
                {
                    lblHeadTitle.Text = "Transporter";
                    this.Title = "Transporter";
                }
                else
                {
                    lblHeadTitle.Text = ContType + "s";
                    this.Title = ContType + "s";
                }

                //........................ Code Commented By Sam on 01112016.....................................
                Session["requesttype"] = ContType;
                AssignQuery();
            }
            else
            {
                AssignQuery1();
            }

        }
        protected void AssignQuery()
        {
            string requesttype1 = Convert.ToString(Session["requesttype"]);

            // Rev 1.0
            //if (requesttype1 == "Customer/Client")
            //{
            //    DataSet CntId = oDBEngine.PopulateData("user_contactid", "tbl_master_user", " user_id in(" + HttpContext.Current.Session["userchildHierarchy"] + ")");
            //    for (int i = 0; i < CntId.Tables[0].Rows.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            AllUserCntId = Convert.ToString(CntId.Tables[0].Rows[i]["user_contactid"]);
            //        }
            //        else
            //        {
            //            AllUserCntId += "," + Convert.ToString(CntId.Tables[0].Rows[i]["user_contactid"]);
            //        }

            //    }


            //    EmployeeDataSource.SelectCommand = "select * from (select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin, (select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select top 1 ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,'' AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select top 1 ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status, Case tbl_master_contact.Statustype when 'A' then 'Active' when 'D' then 'Dormant' END as Activetype,  tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where (tbl_master_contact.cnt_branchid in (select branch_id from tbl_master_branch) or tbl_master_contact.cnt_RelationShipManager in ('" + AllUserCntId + "') or tbl_master_contact.cnt_salesrepresentative in ('" + AllUserCntId + "')) and cnt_contactType= 'CL' ) as D order by CrDate desc ";


            //}

            //else if (requesttype1 == "Franchisee")
            //{
            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //                  " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join  tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'FR%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}

            //else if (requesttype1 == "Salesman/Agents")
            //{
            //    DataSet CntId = oDBEngine.PopulateData("user_contactid", "tbl_master_user", " user_id in(" + HttpContext.Current.Session["userchildHierarchy"] + ")");
            //    for (int i = 0; i < CntId.Tables[0].Rows.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            AllUserCntId = "'" + Convert.ToString(CntId.Tables[0].Rows[i]["user_contactid"]) + "'";
            //        }
            //        else
            //        {
            //            AllUserCntId += ",'" + Convert.ToString(CntId.Tables[0].Rows[i]["user_contactid"]) + "'";
            //        }
            //    }

            //    if (hdnIsDMSFeatureOn.Value == "True")
            //    {
            //        EmployeeDataSource.SelectCommand = "select * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select  top 1 ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //                " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //           " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //           " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //           " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'AG%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ") and tbl_master_contact.cnt_AssociatedEmp in (" + AllUserCntId + ")) as D order by CrDate desc";
            //    }
            //    else
            //    {
            //        EmployeeDataSource.SelectCommand = "select * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select  top 1 ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //                                   " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //                              " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //                              " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //                              " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'AG%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";
            //    }
            //}
            //else if (requesttype1 == "OtherEntity")
            //{
            //    EmployeeDataSource.SelectCommand = "select *  from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //                  " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id  inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'XC%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}

            //else if (requesttype1 == "Salesman/Agents")
            //{
            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 phf_phoneNumber from tbl_master_phonefax where ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //                 " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'RC%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}
            //else if (requesttype1 == "Data Vendor")
            //{
            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //                  " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join  tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'DV%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}
            //else if (requesttype1 == "Vendor")
            //{
            //    EmployeeDataSource.SelectCommand = "select * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'VR%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}
            //else if (requesttype1 == "Partner")
            //{
            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //             " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join  tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'PR%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}
            //else if (requesttype1 == "Consultant")
            //{
            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'CS%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}
            //else if (requesttype1 == "Share Holder")
            //{
            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'SH%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}
            //else if (requesttype1 == "Creditors")
            //{
            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'CR%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}
            //else if (requesttype1 == "Debtor")
            //{
            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate " +
            //              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'DR%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}
            //else if (requesttype1 == "Lead managers")
            //{
            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //             " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'LM%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}
            //else if (requesttype1 == "Book Runners")
            //{
            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'BS%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}
            //else if (requesttype1 == "Companies-Listed")
            //{
            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //            " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'LC%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";
            //}

            //else if (requesttype1 == "Relationship Partners")
            //{

            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'RA%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            //}
            //else if (requesttype1 == "Transporter")
            //{
            //    EmployeeDataSource.SelectCommand = "select * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,case when tbl_master_branch.branch_description is null then 'ALL' else  tbl_master_branch.branch_description end AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select  top 1 ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate " +
            //            " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //           " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //           " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //           " from tbl_master_contact left JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'TR%' and (tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ") or tbl_master_contact.cnt_branchid =0)) as D order by CrDate desc";

            //}
            ////For Leads
            //else if (requesttype1 == "Lead")
            //{
            //    string userbranchHierarchy = Convert.ToString(Session["userbranchHierarchy"]);

            //    EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select  top 1 ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Due' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
            //           " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
            //            " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
            //            " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
            //            " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_contactType= 'LD' and tbl_master_contact.cnt_branchid in (" + userbranchHierarchy + ")) as D order by CrDate desc";


            //}


            //EmployeeGrid.DataBind();

            AllUserCntId = "";

            if (requesttype1 == "Customer/Client")
            {
                DataSet CntId = oDBEngine.PopulateData("user_contactid", "tbl_master_user", " user_id in(" + HttpContext.Current.Session["userchildHierarchy"] + ")");
                for (int i = 0; i < CntId.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        AllUserCntId = Convert.ToString(CntId.Tables[0].Rows[i]["user_contactid"]);
                    }
                    else
                    {
                        AllUserCntId += "," + Convert.ToString(CntId.Tables[0].Rows[i]["user_contactid"]);
                    }

                }
            }
            else if (requesttype1 == "Salesman/Agents")
            {
                DataSet CntId = oDBEngine.PopulateData("user_contactid", "tbl_master_user", " user_id in(" + HttpContext.Current.Session["userchildHierarchy"] + ")");
                for (int i = 0; i < CntId.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        AllUserCntId = Convert.ToString(CntId.Tables[0].Rows[i]["user_contactid"]);
                    }
                    else
                    {
                        AllUserCntId += "," + Convert.ToString(CntId.Tables[0].Rows[i]["user_contactid"]) ;
                    }
                }
            }

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_FTSContactMain_List");
            proc.AddVarcharPara("@REQUESTTYPE1", 100, requesttype1);
            proc.AddVarcharPara("@ALLUSERCNTID", -1, AllUserCntId);
            proc.AddVarcharPara("@HDNISDMSFEATUREON", 100, hdnIsDMSFeatureOn.Value);
            proc.AddVarcharPara("@USERBRANCHHIERARCHY", -1, Convert.ToString(Session["userbranchHierarchy"]) );
            proc.AddVarcharPara("@USERID", 15, Convert.ToString(HttpContext.Current.Session["userid"]) );
            dt = proc.GetTable();

            EmployeeGrid.DataSource=dt;
            EmployeeGrid.DataBind();
            // End of Rev 1.0
        }

        protected void AssignQuery1()
        {

            string requesttype1 = Convert.ToString(Session["requesttype"]);

            if (requesttype1 == "Customer/Client")
            {
                DataSet CntId = oDBEngine.PopulateData("user_contactid", "tbl_master_user", " user_id in(" + HttpContext.Current.Session["userchildHierarchy"] + ")");
                for (int i = 0; i < CntId.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        AllUserCntId = Convert.ToString(CntId.Tables[0].Rows[i]["user_contactid"]);
                    }
                    else
                    {
                        AllUserCntId += "," + Convert.ToString(CntId.Tables[0].Rows[i]["user_contactid"]);
                    }
                }

                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin, (select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select top 1 ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select top 1 ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,Case tbl_master_contact.Statustype when 'A' then 'Active' when 'D' then 'Dormant' END as Activetype,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where (tbl_master_contact.cnt_branchid in (select branch_id from tbl_master_branch) or tbl_master_contact.cnt_RelationShipManager in ('" + AllUserCntId + "') or tbl_master_contact.cnt_salesrepresentative in ('" + AllUserCntId + "'))   and cnt_contactType= 'CL' ) as D order by CrDate desc ";

            }
            else if (requesttype1 == "Sub Broker")
            {

                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate " +
                             " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id  inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'SB%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Franchisee")
            {

                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                            " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join  tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'FR%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Relationship Partners")
            {
                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select  top 1 ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                             " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'RA%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }

            else if (requesttype1 == "OtherEntity")
            {
                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                             " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id  inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'XC%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }

            else if (requesttype1 == "Salesman/Agents")
            {

                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate " +
                             " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'AG%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Data Vendor")
            {

                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate " +
                             " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join  tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'DV%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Vendor")
            {
                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'VR%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Partner")
            {

                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                             " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join  tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'PR%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Consultant")
            {
                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                             " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'CS%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Share Holder")
            {

                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'SH%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Creditors")
            {
                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                            " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'CR%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Debtor")
            {

                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                             " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'DR%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Lead managers")
            {

                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'LM%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Book Runners")
            {

                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                             " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'BS%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Companies-Listed")
            {

                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'LC%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }

            else if (requesttype1 == "Relationship Partners")
            {
                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>''  and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                             " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                        " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'RA%' and tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ")) as D order by CrDate desc";

            }
            else if (requesttype1 == "Transporter")
            {
                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,case when tbl_master_branch.branch_description is null then 'ALL' else  tbl_master_branch.branch_description end AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Lead' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                           " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                      " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                      " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                      " from tbl_master_contact left JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'TR%' and (tbl_master_contact.cnt_branchid in(" + HttpContext.Current.Session["userbranchHierarchy"] + ") or tbl_master_contact.cnt_branchid =0)) as D order by CrDate desc";

            }
            //For Leads
            else if (requesttype1 == "Lead")
            {
                EmployeeDataSource.SelectCommand = "select  * from(select tbl_master_contact.cnt_id AS cnt_Id,'' as CRG_TCODE,cnt_gstin gstin,(select top 1 crg_number  from tbl_master_contactRegistration where crg_type='Pancard' and crg_cntid=tbl_master_contact.cnt_internalId) as PanNumber,tbl_master_contact.cnt_internalId AS Id,(select top 1 ISNULL(phf_countryCode, '') + ' ' + ISNULL(phf_areaCode, '') + ' ' +ISNULL(phf_phoneNumber,'') from tbl_master_phonefax where phf_phoneNumber is not null and LTRIM(RTRIM(phf_phoneNumber)) <>'' and tbl_master_contact.cnt_internalid=phf_cntId) as phf_phoneNumber,(select top 1  ISNULL(eml_email,'') from tbl_master_email where eml_email is not null and LTRIM(RTRIM(eml_email)) <>'' and ltrim(rtrim(eml_type))='official' and tbl_master_contact.cnt_internalid=eml_cntId) as eml_email,(select ISNULL(con.cnt_firstName, '') + ' ' + ISNULL(con.cnt_middleName, '') + ' ' + ISNULL(con.cnt_lastName, '') +'['+con.cnt_shortname+']' from tbl_master_contact con,tbl_master_contact con1 where con.cnt_internalId=con1.cnt_referedBy and con1.cnt_internalId=tbl_master_contact.cnt_internalId) AS Reference,tbl_master_branch.branch_description AS BranchName,ISNULL(cnt_firstName, '') + ' ' + ISNULL(cnt_middleName, '') + ' ' + ISNULL(cnt_lastName, '') AS Name,tbl_master_contact.cnt_UCC as Code,(select  top 1 ISNULL(contact.cnt_firstName, '') + ' ' + ISNULL(contact.cnt_middleName, '') + ' ' + ISNULL(contact.cnt_lastName, '') +'['+contact.cnt_shortname+']' AS Name from tbl_master_contact contact,tbl_trans_contactInfo info where contact.cnt_internalId=info.Rep_partnerid and info.cnt_internalId=tbl_master_contact.cnt_internalId and info.ToDate is null) as RM,case tbl_master_contact.cnt_Lead_Stage when 1 then 'Due' when 2 then 'Opportunity' when 3 then 'Sales/Pipeline' when 4 then 'Converted' when 5 then 'Lost' End as Status,tbl_master_contactstatus.cntstu_contactStatus,case when tbl_master_contact.lastmodifydate is null then  tbl_master_contact.createdate else tbl_master_contact.lastmodifydate end as CrDate" +
                              " ,(Select user_name from tbl_master_user where user_id=tbl_master_contact.CreateUser)as EnterBy," +
                        " CONVERT(VARCHAR(11),tbl_master_contact.LastModifyDate, 105) + ' ' + CONVERT(VARCHAR(8), tbl_master_contact.LastModifyDate, 108) as ModifyDateTime," +
                        " (Select user_name from tbl_master_user where user_id=tbl_master_contact.LastModifyUser)as ModifyUser" +
                         " from tbl_master_contact INNER JOIN tbl_master_branch ON tbl_master_contact.cnt_branchid = tbl_master_branch.branch_id inner join tbl_master_contactstatus on tbl_master_contact.cnt_contactStatus=tbl_master_contactstatus.cntstu_id where  cnt_internalId like 'LD%' and tbl_master_contact.cnt_branchid in(select branch_id from tbl_master_branch)) as D order by CrDate desc";



            }
            EmployeeGrid.DataBind();

        }

        public void bindexport(int Filter)
        {
            EmployeeGrid.Columns[7].Visible = false;
            //MainAccountGrid.Columns[20].Visible = false;
            // MainAccountGrid.Columns[21].Visible = false;
            string filename = Convert.ToString((Session["Contactrequesttype"] ?? "Lead"));
            exporter.FileName = filename;

            exporter.PageHeader.Left = Convert.ToString((Session["Contactrequesttype"] ?? "Lead"));
            exporter.PageFooter.Center = "[Page # of Pages #]";
            exporter.PageFooter.Right = "[Date Printed]";

            switch (Filter)
            {
                case 1:
                    exporter.WritePdfToResponse();
                    break;
                case 2:
                    exporter.WriteXlsToResponse();
                    break;
                case 3:
                    exporter.WriteRtfToResponse();
                    break;
                case 4:
                    exporter.WriteCsvToResponse();
                    break;
            }
        }
        protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 Filter = int.Parse(Convert.ToString(drdExport.SelectedItem.Value));
            if (Filter != 0)
            {
                if (Session["exportval"] == null)
                {
                    Session["exportval"] = Filter;
                    bindexport(Filter);
                }
                else if (Convert.ToInt32(Session["exportval"]) != Filter)
                {
                    Session["exportval"] = Filter;
                    bindexport(Filter);
                }
            }
        }


        protected void addnew(object sender, EventArgs e)
        {
            EmployeeGrid.AddNewRow();
            ASPxPageControl pageControl = EmployeeGrid.FindEditFormTemplateControl("ASPxPageControl1") as ASPxPageControl;
            TabPage corres = pageControl.TabPages.FindByName("General");
            corres.Visible = false;

        }
        protected void EmployeeGrid_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpInsertError"] = "ADD";
        }
        protected void EmployeeGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            EmployeeGrid.JSProperties["cpDelete"] = null;
            string WhichCall = Convert.ToString(e.Parameters).Split('~')[0];
            string WhichType = null;
            int deletecnt = 0;
            if (Convert.ToString(e.Parameters).Contains("~"))
            {
                if (Convert.ToString(e.Parameters).Split('~')[1] != "")
                {
                    WhichType = Convert.ToString(e.Parameters).Split('~')[1];

                }
                if (WhichCall == "Delete")
                {
                    MasterDataCheckingBL objMasterDataCheckingBL = new MasterDataCheckingBL();

                    deletecnt = objMasterDataCheckingBL.DeleteLeadOrContact(WhichType);
                    if (deletecnt > 0)
                    {

                        EmployeeGrid.JSProperties["cpDelete"] = "Success";
                        AssignQuery1();
                    }
                    else
                        EmployeeGrid.JSProperties["cpDelete"] = "Fail";
                }
            }
            EmployeeGrid.ClearSort();

            if (e.Parameters == "ssss")
            {

                EmployeeGrid.Settings.ShowFilterRow = true;



            }
            if (e.Parameters == "All")
            {
                EmployeeGrid.FilterExpression = string.Empty;
                Checking = "All";
                AssignQuery1();
            }
            if (e.Parameters == "")
            {

                AssignQuery1();
            }


        }


        protected void EmployeeGrid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                // .............................Code Commented and Added by Sam on 07122016 to use Convert.tostring instead of tostring(). ................
                string ContactID = Convert.ToString(e.GetValue("Id"));
                string CntName = Convert.ToString(e.GetValue("Name"));
                e.Row.Cells[0].Style.Add("cursor", "hand");
                e.Row.Cells[0].Attributes.Add("onclick", "javascript:ShowMissingData('" + ContactID + "','" + CntName + "');");
                e.Row.Cells[0].ToolTip = "Click to View Not Available Records!";
                // .............................Code Above Commented and Added by Sam on 07122016 to use Convert.tostring instead of tostring(). ..................................... 

                //if (Convert.ToString(Session["requesttype"]) == "Customer/Client")
                //{
                //    e.Row.Cells[2].Style.Add("display", "none");
                //}
                //else { e.Row.Cells[2].Style.Add("display", "block"); }

            }

        }

        protected void EmployeeGrid_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            if (Convert.ToString(Session["Contactrequesttype"]) != "Lead")
            {
                foreach (GridViewDataColumn c in grid.Columns)
                {
                    if ((Convert.ToString(c.FieldName)).StartsWith("Status"))
                    {
                        c.Visible = false;
                    }
                }

            }
            if (Convert.ToString(Session["Contactrequesttype"]) != "customer")
            {
                foreach (GridViewDataColumn d in grid.Columns)
                {
                    if ((Convert.ToString(d.FieldName)).StartsWith("Activetype"))
                    {
                        d.Visible = false;
                    }
                    if ((Convert.ToString(d.FieldName)).StartsWith("gstin"))
                    {
                        d.Visible = false;
                    }
                }

            }
        }
    }
}
