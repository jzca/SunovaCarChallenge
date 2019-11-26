using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SunovaCarList.Startup))]
namespace SunovaCarList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
