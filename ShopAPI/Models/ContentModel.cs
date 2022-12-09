using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ContentModelInput
    {
        public string session_token { get; set; }
    }
    public class ContentModelOutput
    {

        public string status { get; set; }
        public string message { get; set; }
        public List<ContentModel> contentlist { get; set; }
     

    }

    public class ContentModel
    {
        public string content { get; set; }
        public string TemplateID { get; set; }
        public string Templatename { get; set; }
    }
}