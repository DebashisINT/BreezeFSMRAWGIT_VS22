using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyShopProject.Startup))]
namespace MyShopProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
