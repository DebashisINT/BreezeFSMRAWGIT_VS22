using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class TAModelInput
    {
        public string data { get; set; }

        public HttpPostedFileBase document { get; set; }
       
    }

  public class TAModel
  {
      public string session_token { get; set; }
     public string user_id { get; set; }
     public string from_date { get; set; }
     public string to_date { get; set; }
     public string total_amount { get; set; }
     public string description { get; set; }
     public string email { get; set; }

  }
    public class TAinsertionoutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }



    public class TAfetchInput
    {


        public string session_token { get; set; }
        public string user_id { get; set; }
      


    }


    public class TAListfetchOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<TAModelFetchCat> ta_list { get; set; }

    }

    public class TAModelFetchCat
    {
       
        public string user_id { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string total_amount { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string document { get; set; }
    }

}