using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CollegeWebApplication.Startup))]
namespace CollegeWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
