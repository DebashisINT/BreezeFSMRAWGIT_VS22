using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class DoctorActivityModel
    {
        [Required]
        public String session_token { get; set; }
        [Required]
        public String user_id { get; set; }
        [Required]
        public String shop_id { get; set; }
        [Required]
        public String doc_visit_id { get; set; }
        public List<DoctorProduct> product_list { get; set; }
        public String doc_remarks { get; set; }
        public String is_prescriber { get; set; }
        public String is_qty { get; set; }
        public String qty_vol_text { get; set; }
        public List<DoctorProduct> qty_product_list { get; set; }
        public String is_sample { get; set; }
        public List<DoctorProduct> sample_product_list { get; set; }
        public String is_crm { get; set; }
        public String is_money { get; set; }
        public String amount { get; set; }
        public String what { get; set; }
        public DateTime? from_cme_date { get; set; }
        public DateTime? to_crm_date { get; set; }
        public String crm_volume { get; set; }
        public String is_gift { get; set; }
        public String which_kind { get; set; }
        [Required]
        public DateTime next_visit_date { get; set; }
        [Required]
        public String remarks_mr { get; set; }
    }

    public class DoctorProduct
    {
        public String product_id { get; set; }
        public String product_name { get; set; }
    }

    public class DoctorActivitOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class DoctorActivitlistInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class DoctorActivityListoutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<DoctorActivityList> doc_visit_list { get; set; }
    }

    public class DoctorActivityList
    {
        public string shop_id { get; set; }
        public string doc_visit_id { get; set; }
        public List<DoctorProduct> product_list { get; set; }
        public string doc_remarks { get; set; }
        public string is_prescriber { get; set; }
        public string is_qty { get; set; }
        public string qty_vol_text { get; set; }
        public List<DoctorProduct> qty_product_list { get; set; }
        public string is_sample { get; set; }
        public List<DoctorProduct> sample_product_list { get; set; }
        public string is_crm { get; set; }
        public string is_money { get; set; }
        public string amount { get; set; }
        public string what { get; set; }
        public String from_cme_date { get; set; }
        public String to_crm_date { get; set; }
        public string crm_volume { get; set; }
        public string is_gift { get; set; }
        public string which_kind { get; set; }
        public String next_visit_date { get; set; }
        public string remarks_mr { get; set; }
    }
}