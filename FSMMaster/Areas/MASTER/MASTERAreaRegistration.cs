using System.Web.Mvc;

namespace FSMMaster.Areas.MASTER
{
    public class MASTERAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MASTER";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MASTER_default",
                "MASTER/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}