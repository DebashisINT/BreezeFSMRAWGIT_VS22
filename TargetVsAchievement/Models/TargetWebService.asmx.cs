using DataAccessLayer;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace TargetVsAchievement.Models
{
    /// <summary>
    /// Summary description for TargetWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class TargetWebService : System.Web.Services.WebService
    {

        string ConnectionString = String.Empty;
        public TargetWebService()
        {
            ConnectionString = Convert.ToString(System.Web.HttpContext.Current.Session["DBConnectionDefault"]);
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetTargetLevelDetailsList(string SearchKey, string Type)
        {
            
                List<SalesTargetLevelDetails> list = new List<SalesTargetLevelDetails>();         
                
                string USERID = Convert.ToString(Session["userid"]);
                SearchKey = SearchKey.Replace("'", "''");
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConnectionString);
                string Query = "";
                Query = @"   EXEC PRC_FSMTARGETASSIGN @Action='" + Type + "',@SearchKey='" + SearchKey + "',@USERID='" + USERID + "' ";

                DataTable dt = oDBEngine.GetDataTable(Query);
                if (!String.IsNullOrEmpty(Type))
                {
                    list = (from DataRow dr in dt.Rows
                            select new SalesTargetLevelDetails()
                            {
                                Level_ID = Convert.ToString(dr["ID"]),
                                Level_Name = Convert.ToString(dr["NAME"]),
                                Level_Code = Convert.ToString(dr["CODE"]),
                                

                            }).ToList();
                }
                else
                {
                    list = (from DataRow dr in dt.Rows
                            select new SalesTargetLevelDetails()
                            {
                                Level_ID = Convert.ToString(dr["ID"]),
                                Level_Name = Convert.ToString(dr["NAME"]),
                                Level_Code = Convert.ToString(dr["CODE"]),
                                
                               
                            }).ToList();
                }         

            return list;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetTargetProductDetailsList(string SearchKey)
        {

            List<ProductDetails> list = new List<ProductDetails>();

           
            SearchKey = SearchKey.Replace("'", "''");
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConnectionString);
            string Query = "";
            Query = @"   EXEC PRC_FSMTARGETASSIGN @Action='GETPRODUCTLIST',@SearchKey='" + SearchKey + "' ";

            DataTable dt = oDBEngine.GetDataTable(Query);
            if (dt.Rows.Count > 0 && dt != null)
            {
                list = (from DataRow dr in dt.Rows
                        select new ProductDetails()
                        {
                            ID = Convert.ToString(dr["ID"]),
                            Name = Convert.ToString(dr["NAME"]),
                            Code = Convert.ToString(dr["CODE"]),


                        }).ToList();
            }
            else
            {
                list = (from DataRow dr in dt.Rows
                        select new ProductDetails()
                        {
                            ID = Convert.ToString(dr["ID"]),
                            Name = Convert.ToString(dr["NAME"]),
                            Code = Convert.ToString(dr["CODE"]),


                        }).ToList();
            }

            return list;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetTargetBrandDetailsList(string SearchKey)        {

            List<BrandDetails> list = new List<BrandDetails>();           
            SearchKey = SearchKey.Replace("'", "''");
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConnectionString);
            string Query = "";
            Query = @"   EXEC PRC_FSMTARGETASSIGN @Action='GETBRANDLIST',@SearchKey='" + SearchKey + "'";

            DataTable dt = oDBEngine.GetDataTable(Query);
            if (dt.Rows.Count>0 && dt!=null)
            {
                list = (from DataRow dr in dt.Rows
                        select new BrandDetails()
                        {
                            Brand_ID = Convert.ToString(dr["ID"]),
                            Brand_Name = Convert.ToString(dr["NAME"]),                         

                        }).ToList();
            }
            else
            {
                list = (from DataRow dr in dt.Rows
                        select new BrandDetails()
                        {
                            Brand_ID = Convert.ToString(dr["ID"]),
                            Brand_Name = Convert.ToString(dr["NAME"]),
                        }).ToList();
            }

            return list;
        }

    }

    public class SalesTargetLevelDetails
    {
        public string Level_ID { get; set; }
        public string Level_Name { get; set; }
        public string Level_Code { get; set; }
        
    }

    public class ProductDetails
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        
    }

    public class BrandDetails
    {
        public string Brand_ID { get; set; }       
        public string Brand_Name { get; set; }
    }
}
