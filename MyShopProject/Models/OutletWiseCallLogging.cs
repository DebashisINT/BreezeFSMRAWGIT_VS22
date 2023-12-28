/******************************************************************************************************
 * Created by Priti for V2.0.44 on 19/12/2023. Work done in Controller, View and Model
 * A new report is required as Outlet wise Call Logging Details Report (Customisation). Refer: 0027064                                                      
 * *******************************************************************************************************/

using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class OutletWiseCallLogging
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }

        public List<string> StateId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> desgid { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public List<string> empcode { get; set; }

        public List<GetUsersStates> states { get; set; }

        public List<GetDesignation> designation { get; set; }

        public List<GetAllEmployee> employee { get; set; }

        public int IsShowRate { get; set; }
        public string is_pageload { get; set; }
       
        public int IsRevisitContactDetails { get; set; }        
        public List<string> BranchId { get; set; }
        public List<GetBranch> modelbranch = new List<GetBranch>();
        



    }



}