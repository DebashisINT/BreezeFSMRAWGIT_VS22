#region======================================Revision History=========================================================
//Written By : Debashis Talukder On 30/09/2024
//Purpose: Product Stock Management Details.Refer: Row: 982 & 984
#endregion===================================End of Revision History==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ListForProductStockInput
    {
        public long user_id { get; set; }
    }

    public class ListForProductStockOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ProductStocklistOutput> stock_list { get; set; }
    }

    public class ProductStocklistOutput
    {
        public string stock_shopcode { get; set; }
        public string stock_shopentitycode { get; set; }
        public long stock_productid { get; set; }
        public string stock_productname { get; set; }
        public decimal stock_productqty { get; set; }
        public decimal stock_productbalqty { get; set; }
    }

    public class UpdateProductBalStockInput
    {
        public long user_id { get; set; }
        public string stock_shopcode { get; set; }
        public string stock_productid { get; set; }
        public decimal submitted_qty { get; set; }
    }
    public class UpdateProductBalStockOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}