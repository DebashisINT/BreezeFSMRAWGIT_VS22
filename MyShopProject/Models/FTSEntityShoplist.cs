using FTSEntityframework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class FTSEntityShoplist
    {

        public List<tbl_Master_shop> shoplist { get; set; }

      
        public int CurrentPageIndex { get; set; }

    
        public int PageCount { get; set; }
    }


}