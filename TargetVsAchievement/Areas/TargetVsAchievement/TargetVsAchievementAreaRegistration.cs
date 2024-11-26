using System.Web.Mvc;

namespace TargetVsAchievement.Areas.TargetVsAchievement
{
    public class TargetVsAchievementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TargetVsAchievement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TargetVsAchievement_default",
                "TargetVsAchievement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}