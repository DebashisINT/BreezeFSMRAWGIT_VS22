using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ChemistActivitModel
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
        public String shop_id { get; set; }
        public String chemist_visit_id { get; set; }
        public List<ChemistProduct> product_list { get; set; }
        public String isPob { get; set; }
        public List<ChemistProduct> pob_product_list { get; set; }
        public String volume { get; set; }
        public String remarks { get; set; }
        public DateTime next_visit_date { get; set; }
        public String remarks_mr { get; set; }
    }

    public class ChemistProduct
    {
        public String product_id { get; set; }
        public String product_name { get; set; }
    }

    public class ChemistActivitOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class ChemistActivitlistInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class ChemistActivityListoutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ChemistActivityList> chemist_visit_list { get; set; }
    }

    public class ChemistActivityList
    {
        public string shop_id { get; set; }
        public string chemist_visit_id { get; set; }
        public List<ChemistProduct> product_list { get; set; }
        public string isPob { get; set; }
        public List<ChemistProduct> pob_product_list { get; set; }
        public string volume { get; set; }
        public string remarks { get; set; }
        public String next_visit_date { get; set; }
        public string remarks_mr { get; set; }
    }
}