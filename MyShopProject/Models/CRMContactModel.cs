/***************************************************************************************
 * Written by Sanchita on 24/11/2023 for V2.0.43    A new design page is required as Contact (s) under CRM menu. 
 *                                                  Mantis: 27034 
 ****************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class CRMContactModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Fromdate { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Todate { get; set; }

        public List<GetContactFrom> ContactFrom { get; set; }
        public List<string> ContactFromDesc { get; set; }
        public string Is_PageLoad { get; set; }

        public String FirstName { get; set; }
    }
    public class GetContactFrom
    {
        public string ContactFromDesc { get; set; }
    }
}