using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ModelContentmanagement
    {

        public string templatetext { get; set; }

        public List<TemplateList> templates { get; set; }

        public string TemplateID { get; set; }

    }

    public class TemplateList
    {
        public int Id { get; set; }

        public string Name { get; set; }


    }
}