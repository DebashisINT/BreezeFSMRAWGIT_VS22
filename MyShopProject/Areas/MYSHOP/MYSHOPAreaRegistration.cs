using System.Web.Mvc;

namespace MyShop.Areas.MYSHOP
{
    public class MYSHOPAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MYSHOP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MYSHOP_default",
                "MYSHOP/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}