using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class MoaterialDetailsReportOutput
    {
        public List<MaterialName> mateerial { get; set; }
        public List<MaterialName> mateerialpop { get; set; }
        public List<Marketingetails> markeingdetails { get; set; }

    }
    public class MaterialName
    {
        public string Material_name { get; set; }
        public string Types { get; set; }
        public string TypesName { get; set; }
        public int Id { get; set; }
    }


    public class Marketingetails
    {
        public string Shop_Name { get; set; }
        public string UserName { get; set; }
        public string ShopID { get; set; }

        public string Address { get; set; }
        public string Mobile { get; set; }

        public string Material_1 { get; set; }
        public string Material_2 { get; set; }
        public string Material_3 { get; set; }
        public string Material_4 { get; set; }
        public string Material_5 { get; set; }
        public string Material_6 { get; set; }
        public string Material_7 { get; set; }
        public string Material_8 { get; set; }
        public string Material_9 { get; set; }
        public string Material_10 { get; set; }
        public string Material_11 { get; set; }
        public string Material_12 { get; set; }
        public string Material_13 { get; set; }
        public string Material_14 { get; set; }
        public string Material_15 { get; set; }
    }

    public class MaterialImages
    {
        public string Image_name { get; set; }

    }

}