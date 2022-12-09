﻿using DataAccessLayer;
using EntityLayer.CommonELS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogicLayer.CommonBLS
{
    public class CommonHelperProcedures
    {
        public const string Proc_Common = "[dbo].[Proc_Common]";
    }

    public class CommonBL
    {
        public static void CreateUserRightSession(string url)
        {
            DestroyUserRightSession();
            UserRightsForPage rights = GetUserRightsForPage(url);
            HttpContext.Current.Session["UserRightSession"+url] = rights;
        }

        public static void DestroyUserRightSession()
        {
            if (HttpContext.Current.Session["UserRightSession"] != null)
            {
                HttpContext.Current.Session["UserRightSession"] = null;
            }
        }

        public static UserRightsForPage GetUserRightSession(string url)
        {

            
            if (HttpContext.Current.Session["UserRightSession" + url] != null)
            {
                try
                {
                    UserRightsForPage rights = (UserRightsForPage)HttpContext.Current.Session["UserRightSession" + url];
                    return rights;
                }
                catch
                {

                }
            }

            CreateUserRightSession(url);
            return (UserRightsForPage)HttpContext.Current.Session["UserRightSession" + url];
        }

        public static UserRightsForPage GetUserRightsForPage(string url)
        {
            int usergroupid = 0;

            try
            {
                if (HttpContext.Current.Session["usergoup"] != null && !string.IsNullOrWhiteSpace(url))
                {
                    if (int.TryParse(Convert.ToString(HttpContext.Current.Session["usergoup"]), out usergroupid))
                    {
                        ProcedureExecute Proc = new ProcedureExecute(CommonHelperProcedures.Proc_Common);
                        Proc.AddPara("@usergroupid", usergroupid);
                        Proc.AddPara("@url", url);
                        Proc.AddPara("@mode", Proc_Common_Modes.GetUserRightsForPage.ToString());
                        DataTable dt = Proc.GetTable();
                        return DbHelpers.ToModel<UserRightsForPage>(dt);
                    }
                }
            }
            catch
            {

            }
            return new UserRightsForPage()
            {
                CanAdd = false,
                CanDelete = false,
                CanEdit = false,
                CanView = false
            };
        }



    }

    public enum Proc_Common_Modes
    {
        GetUserRightsForPage = 1
    }

   

}
