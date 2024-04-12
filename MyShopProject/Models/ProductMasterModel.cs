using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ProductMasterModel
    {
        public string Ispageload { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal ProductMRP { get; set; }
        public decimal ProductPrice { get; set; }

        public Int32 ProductClass { get; set; }
        public List<string> ProductClassIds { get; set; }
        public string ProductClassId { get; set; }
        public List<ProductClassList> ProductClassList { get; set; }

        public Int32 ProductStrength { get; set; }
        public List<string> ProductStrengthIds { get; set; }
        public string ProductStrengthId { get; set; }
        public List<ProductStrengthList> ProductStrengthList { get; set; }

        public Int32 ProductUnit { get; set; }
        public List<string> ProductUnitIds { get; set; }
        public string ProductUnitId { get; set; }
        public List<ProductUnitList> ProductUnitList { get; set; }

        public Int32 ProductBrand { get; set; }
        public List<string> ProductBrandIds { get; set; }
        public string ProductBrandId { get; set; }
        public List<ProductBrandList> ProductBrandList { get; set; }

        public Int32 ProductStatus { get; set; }
        public List<string> ProductStatusIds { get; set; }
        public Int32 ProductStatusId { get; set; }
        public List<ProductStatusList> ProductStatusList { get; set; }
      
    }

    public class ProductClassList
    {
        public Int64 ProductClassId { get; set; }
        public string ProductClassName { get; set; }
    }
    public class ProductStrengthList
    {
        public Int64 ProductStrengthId { get; set; }
        public string ProductStrengthName { get; set; }
    }

    public class ProductUnitList
    {
        public Int64 ProductUnitId { get; set; }
        public string ProductUnitName { get; set; }
    }

    public class ProductBrandList
    {
        public Int64 ProductBrandId { get; set; }
        public string ProductBrandName { get; set; }
    }

    public class ProductStatusList
    {
        public Int64 ProductStatusId { get; set; }
        public string ProductStatusName { get; set; }
    }

    public class ProductImportLog
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemClass { get; set; }
        public string ItemBrand { get; set; }
        public string ItemStrangth { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ItemMRP { get; set; }
        public string ItemStatus { get; set; }
        public string ItemUnit { get; set; }

        public string ImportStatus { get; set; }
        public string ImportMsg { get; set; }

        public DateTime ImportDate { get; set; }
        public string UpdatedBy { get; set; }

    }

}