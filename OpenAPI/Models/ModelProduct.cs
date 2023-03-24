using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenAPI.Models
{
    public class ModelProduct
    {
    }

    public class ProductDetailsInput
    {
        public string session_token { get; set; }
        public string ProductCode { get; set; }

    }

    public class ProductDetailsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public string ProductsCode { get; set; }
        public string ProductsName { get; set; }
        public string ProductsDescription { get; set; }

        public string STKUOMNAME { get; set; }
        public string SALESUOMNAME { get; set; }
        public string CLASSCODE { get; set; }

        public string BRANDNAME { get; set; }
    }

    public class OrderDetailsInput
    {
        public string session_token { get; set; }
        public string OrderCode { get; set; }

        // public List<OrderSignaturelist> product_list { get; set; }
    }


    public class ProductInput
    {
        public string session_token { get; set; }
        public string SearchKey { get; set; }
        public int Uniquecont { get; set; }

    }
    public class ProductslistOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public Datalists data { get; set; }
    }
    public class Datalists
    {
        public List<Productslists> product_list { get; set; }

    }

    public class Productslists
    {
        public string ProductsCode { get; set; }
        public string ProductsName { get; set; }
        public string ProductsDescription { get; set; }

        public string STKUOMNAME { get; set; }
        public string SALESUOMNAME { get; set; }
        public string CLASSCODE { get; set; }
        public string BRANDNAME { get; set; }

    }
}