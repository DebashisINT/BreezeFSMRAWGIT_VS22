using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ShopslistHiarchyInput
    {
        public string selectedusrid { get; set; }

        public string Desgid { get; set; }
        public string StateId { get; set; }
        public List<GetUsersshopshiarchy> userlsit { get; set; }
        public List<GetUserDesignationShoplist> designation { get; set; }
        public List<GetUserStates> states { get; set; }

        public string Fromdate { get; set; }


        public string Todate { get; set; }
        public int Uniquecont { get; set; }

        public int pageNumber { get; set; }
        public int Pagecount { get; set; }

    }

    public class GetUsersshopshiarchy
    {
        public string UserID { get; set; }
        public string username { get; set; }

    }
    public class GetUserDesignationShoplist
    {
        public string ID { get; set; }
        public string DesgName { get; set; }
    }

    public class GetUserStates
    {
        public string ID { get; set; }
        public string StateName { get; set; }

    }




    public class ShopslistsHiarchy
    {

        public string shop_Auto { get; set; }
        public string UserName { get; set; }
        public string shop_id { get; set; }
      
        public string shop_name { get; set; }
     
        public string address { get; set; }
    
        public string pin_code { get; set; }

        public string shop_lat { get; set; }

        public string shop_long { get; set; }
   
        public string owner_name { get; set; }
      
        public string owner_contact_no { get; set; }


      
        public string owner_email { get; set; }

        public string Shop_Image { get; set; }



        public DateTime? Shop_CreateTime { get; set; }

        public DateTime? dob { get; set; }

        public DateTime? date_aniversary { get; set; }
        public string Shoptype { get; set; }

 

        public string dobstr { get; set; }

        public string date_aniversarystr { get; set; }
        public string time_shop { get; set; }

        public int countactivity { get; set; }

        public string Designation { get; set; }
        public string StateName { get; set; }
        public int totalcount { get; set; }


    }


    public class AllhierarchyShoplist
    {

        public List<ShopslistsHiarchy> shoplist { get; set; }


        public int CurrentPageIndex { get; set; }


        public int PageCount { get; set; }

        public int Totalcount { get; set; }

        public Pager pager { get; set; }
    }




    public class IndexViewModel
    {
        public IEnumerable<string> Items { get; set; }
        public Pager Pager { get; set; }
    }

    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 20)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }

}