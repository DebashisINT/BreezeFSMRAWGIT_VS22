using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class QuotationModel
    {
        public String quo_id { get; set; }
        public String shop_id { get; set; }
        public String quo_no { get; set; }
        public String date { get; set; }
        public String hypothecation { get; set; }
        public String account_no { get; set; }
        public String model_id { get; set; }
        public String bs_id { get; set; }
        public String gearbox { get; set; }
        public String number1 { get; set; }
        public String value1 { get; set; }
        public String value2 { get; set; }
        public String tyres1 { get; set; }
        public String number2 { get; set; }
        public String value3 { get; set; }
        public String value4 { get; set; }
        public String tyres2 { get; set; }
        public Decimal amount { get; set; }
        public Decimal discount { get; set; }
        public Decimal cgst { get; set; }
        public Decimal sgst { get; set; }
        public Decimal tcs { get; set; }
        public Decimal indurance { get; set; }
        public Decimal net_amount { get; set; }
        public String remarks { get; set; }
    }

    public class QuotationAddInput
    {
        [Required]
        public String session_token { get; set; }
        [Required]
        public String user_id { get; set; }
        [Required]
        public String shop_id { get; set; }
        [Required]
        public String quo_id { get; set; }
        public String quo_no { get; set; }
        public String date { get; set; }
        public String hypothecation { get; set; }
        public String account_no { get; set; }
        public String model_id { get; set; }
        public String bs_id { get; set; }
        public String gearbox { get; set; }
        public String number1 { get; set; }
        public String value1 { get; set; }
        public String value2 { get; set; }
        public String tyres1 { get; set; }
        public String number2 { get; set; }
        public String value3 { get; set; }
        public String value4 { get; set; }
        public String tyres2 { get; set; }
        public String amount { get; set; }
        public String discount { get; set; }
        public String cgst { get; set; }
        public String sgst { get; set; }
        public String tcs { get; set; }
        public String insurance { get; set; }
        public String net_amount { get; set; }
        public String remarks { get; set; }
    }

    public class QuotationAddOutput
    {
        public String status { get; set; }
        public String message { get; set; }
    }

    public class QuotationListInput
    {
        public String session_token { get; set; }
        public String user_id { get; set; }
    }

    public class QuotationListOutput
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<QuotationModel> quot_list { get; set; }
    }
}