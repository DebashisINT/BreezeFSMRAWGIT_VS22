using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class OpeningStockModel
    {
        public String Distributor { get; set; }
        public String WreaHouse_id { get; set; }

        public List<DistributerList> Distributer_List { get; set; }

        public List<warehouse> warehouse_List { get; set; }
    }

    public class ProductStockList
    {
        public String SlNO { get; set; }
        public String product_id { get; set; }
        public String Product_Name { get; set; }
        public String CurrentStock { get; set; }
        public String newStock { get; set; }
        public String ClosingStock { get; set; }
        public String ProductClass_Name { get; set; }
        public String Brand_Name { get; set; }
        public String PRODUCT_CODE { get; set; }
    }

    public class warehouse
    {
        public String WAREHOUSE_ID { get; set; }
        public String WAREHOUSE_NAME { get; set; }
    }

    public class udtStockProducts
    {
        public Int64 product_id { get; set; }
        public String Product_Name { get; set; }
        public Decimal CurrentStock { get; set; }
        public Decimal newStock { get; set; }
        public Decimal ClosingStock { get; set; }
        public String SlNO { get; set; }
    }

    public class udtStockProduct
    {
        public Int64 product_id { get; set; }
        public String Product_Name { get; set; }
        public Decimal CurrentStock { get; set; }
        public Decimal newStock { get; set; }
        public Decimal ClosingStock { get; set; }
        public String SlNO { get; set; }
    }
}